using System;
using System.Threading;
using System.Threading.Tasks;
using TgSharp.Core.MTProto;
using TgSharp.Common.MTProto.Crypto;
using TgSharp.Common.MTProto.Schema;
using TgSharp.Core.Networking.MTProto;
using TgSharp.Core.Networking.Transports;

namespace TgSharp.Core.Auth
{
    public static class Authenticator
    {
        public static async Task DoAuthentication(this MTProtoClient client, CancellationToken token = default(CancellationToken))
        {
            token.ThrowIfCancellationRequested();
            byte[] nonceBytes = new byte[16];
            new Random().NextBytes(nonceBytes);
            var step1 = new MTRequestReqPQMulti() { Nonce =  new BigInteger(1, nonceBytes)};

            client.Send(step1.Serialize(), false, false);
            MTResPq result = await client.ReceiveObject<MTResPq>();
            Console.WriteLine("MTResPq Received");
        }
    }
}