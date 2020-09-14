using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TgSharp.Core.Networking.Transports
{
    public interface ITransport : IDisposable
    {
        event Action<int, byte[]> DataReceived;
        event Action Connected, Disconnected;

        void Send(byte[] data);
        bool IsConnected();

    }
}
