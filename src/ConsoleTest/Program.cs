using System;
using TgSharp.Core.MTProto;
using TgSharp.Core.Auth;
using System.Threading.Tasks;
using System.Threading;

namespace ConsoleTest
{
    class Program
    {
        static async Task Main(string[] args)
        {
            MTProtoClient client = new MTProtoClient();
            client.Connect();

            //System.Threading.Thread.Sleep(-1);
            await client.DoAuthentication().ConfigureAwait(false);

            await Task.Delay(TimeSpan.FromMilliseconds(-1));
        }
    }
}
