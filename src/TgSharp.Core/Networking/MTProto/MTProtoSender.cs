using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using TgSharp.Core.MtProto;
using TgSharp.Core.MTProto.Crypto;
using TgSharp.Core.Networking.Transports;
using TgSharp.Core.Utils;
using TgSharp.TL;

namespace TgSharp.Core.Networking.MtProto
{
    public class MTProtoSender
    {
        private int timeOffset;
        private long lastMessageId;
        private Random random;
        private ITransport transport;

        public event Action<MTProtoMessage> MessageReceived;

        public Session Session { get; set; }

        public MTProtoSender(ITransport transport)
        {
            this.transport = transport;
            transport.DataReceived += Transport_DataReceived;
            random = new Random();
        }
        private void Transport_DataReceived(int seqNo, byte[] packet)
        {
            using (var memoryStream = new MemoryStream(packet))
            {
                using (BinaryReader binaryReader = new BinaryReader(memoryStream))
                {
                    ulong authKeyid = binaryReader.ReadUInt64();

                    if (authKeyid == 0L)
                    {
                        ulong messageId = binaryReader.ReadUInt64();
                        int messageLength = binaryReader.ReadInt32();

                        byte[] response = binaryReader.ReadBytes(messageLength);

                        MessageReceived?.Invoke(new MTProtoMessage { Authenticated = false, Payload = response, RemoteMessageId = messageId});
                    }
                    else
                    {
                        //check auth_key_id
                        if (authKeyid != Session.AuthKey.Id)
                            throw new InvalidOperationException();
                        
                        //todo: check msg_key
                        byte[] msgKey = binaryReader.ReadBytes(16);
                        AESKeyData keyData = Helpers.CalcKey(Session.AuthKey.Data, msgKey, false);

                        byte[] plaintext = AES.DecryptAES(keyData, binaryReader.ReadBytes((int)(memoryStream.Length - memoryStream.Position)));

                        using (MemoryStream plaintextStream = new MemoryStream(plaintext))
                        using (BinaryReader plaintextReader = new BinaryReader(plaintextStream))
                        {
                            var remoteSalt = plaintextReader.ReadUInt64();
                            var remoteSessionId = plaintextReader.ReadUInt64();
                            var remoteMessageId = plaintextReader.ReadUInt64();
                            var remoteSequence = plaintextReader.ReadInt32();
                            int msgLen = plaintextReader.ReadInt32();
                            var message = plaintextReader.ReadBytes(msgLen);

                            MessageReceived?.Invoke(new MTProtoMessage { RemoteSequence = remoteSequence, Authenticated = true, Payload = message, RemoteMessageId = remoteMessageId, RemoteSalt = remoteSalt, RemoteSessionId = remoteSessionId });
                        }

                    }
                }
            }
        }
        public long Send(TLMethod request, bool authenticated)
        {
            return Send(request.Serialize(), request.Confirmed, authenticated);
        }
        public long Send(byte[] data, bool confirmed = false, bool authenticated = false)
        {
            if (!authenticated || session == null)
            {
                var messageId = GetNewMessageId();
                using (var memoryStream = new MemoryStream())
                {
                    using (var binaryWriter = new BinaryWriter(memoryStream))
                    {
                        binaryWriter.Write((long)0);
                        binaryWriter.Write(messageId);
                        binaryWriter.Write(data.Length);
                        binaryWriter.Write(data);

                        byte[] packet = memoryStream.ToArray();

                        transport.Send(packet);
                    }
                }

                return messageId;
            }
            else if (authenticated && session != null)
            {
                var messageId = session.GetNewMessageId();

                byte[] msgKey;
                byte[] ciphertext;
                using (MemoryStream plaintextPacket = MakeMemory(8 + 8 + 8 + 4 + 4 + data.Length))
                {
                    using (BinaryWriter plaintextWriter = new BinaryWriter(plaintextPacket))
                    {
                        plaintextWriter.Write(session.Salt);
                        plaintextWriter.Write(session.Id);
                        plaintextWriter.Write(messageId);
                        plaintextWriter.Write(GenerateSequence(confirmed));
                        plaintextWriter.Write(data.Length);
                        plaintextWriter.Write(data);

                        msgKey = Helpers.CalcMsgKey(plaintextPacket.GetBuffer());
                        ciphertext = AES.EncryptAES(Helpers.CalcKey(session.AuthKey.Data, msgKey, true), plaintextPacket.GetBuffer());
                    }
                }

                using (MemoryStream ciphertextPacket = MakeMemory(8 + 16 + ciphertext.Length))
                {
                    using (BinaryWriter writer = new BinaryWriter(ciphertextPacket))
                    {
                        writer.Write(session.AuthKey.Id);
                        writer.Write(msgKey);
                        writer.Write(ciphertext);

                        transport.Send(ciphertextPacket.GetBuffer());
                    }
                }
            }

            throw new NotImplementedException();
        }
        private int GenerateSequence(bool confirmed)
        {
            return confirmed ? session.Sequence++ * 2 + 1 : session.Sequence * 2;
        }
        private MemoryStream MakeMemory(int Len)
        {
            return new MemoryStream(new byte[Len], 0, Len, true, true);
        }
        private long GetNewMessageId()
        {
            long time = Convert.ToInt64((DateTime.UtcNow - new DateTime(1970, 1, 1)).TotalMilliseconds);
            long newMessageId = ((time / 1000 + timeOffset) << 32) |
                                ((time % 1000) << 22) |
                                (random.Next(524288) << 2); // 2^19
                                                            // [ unix timestamp : 32 bit] [ milliseconds : 10 bit ] [ buffer space : 1 bit ] [ random : 19 bit ] [ msg_id type : 2 bit ] = [ msg_id : 64 bit ]

            if (lastMessageId >= newMessageId)
            {
                newMessageId = lastMessageId + 4;
            }

            lastMessageId = newMessageId;
            return newMessageId;
        }
    }
}
