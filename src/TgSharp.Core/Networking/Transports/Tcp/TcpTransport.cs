using BeetleX;
using BeetleX.Clients;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TgSharp.Common.MTProto.Crypto;

namespace TgSharp.Core.Networking.Transports.Tcp
{
    public class TcpTransport : ITransport
    {
        public event Action<int, byte[]> DataReceived;
        public event Action Connected;
        public event Action Disconnected;

        private readonly AsyncTcpClient client;
        private int sendCounter = 0;

        public TcpTransport(string address, int port)
        {
            try
            {
                client = SocketFactory.CreateClient<AsyncTcpClient>(address, port);
                client.Connect(out _);
                client.Connected += (client) => Connected?.Invoke();
                client.Disconnected += (IClient client) =>
                {
                    Disconnected?.Invoke();
                    Console.WriteLine(client.Connect(out _));
                };

                client.DataReceive = async (o, e) =>
                {

                    var packetLengthBytes = new byte[4];
                    if (await e.Stream.ReadAsync(packetLengthBytes, 0, 4).ConfigureAwait(false) != 4)
                        throw new InvalidOperationException("Couldn't read the packet length");
                    int packetLength = BitConverter.ToInt32(packetLengthBytes, 0);

                    var seqBytes = new byte[4];
                    if (await e.Stream.ReadAsync(seqBytes, 0, 4).ConfigureAwait(false) != 4)
                        throw new InvalidOperationException("Couldn't read the sequence");
                    int seq = BitConverter.ToInt32(seqBytes, 0);

                    int readBytes = 0;
                    var body = new byte[packetLength - 12];
                    int neededToRead = packetLength - 12;

                    do
                    {
                        var bodyByte = new byte[packetLength - 12];
                        var availableBytes = await e.Stream.ReadAsync(bodyByte, 0, neededToRead).ConfigureAwait(false);
                        neededToRead -= availableBytes;
                        Buffer.BlockCopy(bodyByte, 0, body, readBytes, availableBytes);
                        readBytes += availableBytes;
                    }
                    while (readBytes != packetLength - 12);

                    var crcBytes = new byte[4];
                    if (await e.Stream.ReadAsync(crcBytes, 0, 4).ConfigureAwait(false) != 4)
                        throw new InvalidOperationException("Couldn't read the crc");

                    byte[] rv = new byte[packetLengthBytes.Length + seqBytes.Length + body.Length];

                    Buffer.BlockCopy(packetLengthBytes, 0, rv, 0, packetLengthBytes.Length);
                    Buffer.BlockCopy(seqBytes, 0, rv, packetLengthBytes.Length, seqBytes.Length);
                    Buffer.BlockCopy(body, 0, rv, packetLengthBytes.Length + seqBytes.Length, body.Length);
                    var crc32 = new Crc32();
                    var computedChecksum = crc32.ComputeHash(rv).Reverse();

                    if (!crcBytes.SequenceEqual(computedChecksum))
                    {
                        throw new InvalidOperationException("invalid checksum! skip");
                    }

                    DataReceived?.Invoke(seq, body);
                };
            }
            catch (Exception ex)
            {
                throw new Exception($"Problem when trying to connect to {address}:{port} ; either there's no internet connection or the IP address version is not compatible (if the latter, consider using DataCenterIPVersion enum)",
                                     ex);
            }
        }

        public void Send(byte[] data)
        {
            if (!client.IsConnected)
                throw new InvalidOperationException("Client not connected to server.");

            var tcpMessage = new TcpMessage(sendCounter, data);
            BytesHandler bytes = tcpMessage.Encode();

            client.Send(
                bytes
                );

            sendCounter++;
        }

        public bool IsConnected() =>
            client.IsConnected;

        public void Dispose()
        {
            Console.WriteLine("closed");
            client.DisConnect();
            client.Dispose();
        }
    }
}
