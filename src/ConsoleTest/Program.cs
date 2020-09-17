using System;
using TgSharp.Core.MTProto;
using TgSharp.Core.Auth;
using System.Threading.Tasks;
using System.Threading;
using System.Runtime.InteropServices;

namespace ConsoleTest
{
    class Program
    {
        static async Task Main(string[] args)
        {
            //TelegramClient client = new TelegramClient();

            //await client.ConnectAsync();
            //await client.Authorize();

            MTProtoClient client = new MTProtoClient();
            client.Connect();
            await client.DoAuthentication();

            await Task.Delay(-1);
        }
    }
}
