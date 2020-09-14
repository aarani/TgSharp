using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using TgSharp.Core.Networking.MTProto;
using TgSharp.Core.Networking.Transports.Tcp;
using TgSharp.Common;

namespace TgSharp.Core.MTProto
{
    public class MTProtoClient
    {
        private  MTProtoSender MTProtoSender;
        private readonly ConcurrentDictionary<int, TaskCompletionSource<TLObject>> Tasks
            = new ConcurrentDictionary<int, TaskCompletionSource<TLObject>>();

        public Session Session
        {
            get
            {
                return Session;
            }
            set
            {
                MTProtoSender.Session = Session;
            }
        }

        public MTProtoClient()
        {
            
        }

        public void Connect()
        {
            MTProtoSender = new MTProtoSender(new TcpTransport(Session.defaultConnectionAddress, Session.defaultConnectionPort));
            MTProtoSender.MessageReceived += MTProtoSender_MessageReceived;
        }

        private void MTProtoSender_MessageReceived(MTProtoMessage obj)
        {
            using (var ms = new MemoryStream(obj.Payload))
            using (var br = new BinaryReader(ms))
            {
                int Constructor = br.ReadInt32();
                Console.WriteLine($"MTPROTO Message: #{Constructor}");
                Tasks.TryGetValue(Constructor, out var task);
                if (task != null)
                {
                    ms.Position = 0;
                    task.SetResult((TLObject)ObjectUtils.DeserializeObject(br));
                }
            }
        }

        public long Send(TLMethod request, bool authenticated)
        {
            return MTProtoSender.Send(request, authenticated);
        }

        public long Send(byte[] data, bool confirmed = false, bool authenticated = false)
        {
            return MTProtoSender.Send(data, confirmed, authenticated);
        }
        private static string ByteArrayToString(byte[] ba)
        {
            StringBuilder hex = new StringBuilder(ba.Length * 2);
            foreach (byte b in ba)
                hex.AppendFormat("{0:x2}", b);
            return hex.ToString();
        }

        public async Task<T> ReceiveObject<T>() where T : TLObject
        {
            TaskCompletionSource<TLObject> completionSource = new TaskCompletionSource<TLObject>();
            Tasks[GetConstructor(typeof(T))] = completionSource;
            Console.WriteLine(5);
            return (T)await completionSource.Task;
        }

        private int GetConstructor(Type type)
        {
            return type.GetCustomAttribute<TLObjectAttribute>().Constructor;
        }
    }
}
