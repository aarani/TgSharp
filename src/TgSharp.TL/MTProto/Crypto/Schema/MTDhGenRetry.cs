using System.IO;
using TgSharp.Common.MTProto.Crypto;

using TgSharp.Common;
namespace TgSharp.Common.MTProto.Schema
{
    [TLObject(1188831161)]
    public class MTDHGenRetry : MTAbsSetClientDHParamsAnswer
    {
        public override int Constructor => 1188831161;

        public BigInteger Nonce { get; set; }
        public BigInteger ServerNonce { get; set; }
        public BigInteger NewNonceHash2 { get; set; }


        public void ComputeFlags()
        {
        }

        public override void DeserializeBody(BinaryReader br)
        {
            Nonce = new BigInteger(1, br.ReadBytes(16));
            ServerNonce = new BigInteger(1, br.ReadBytes(16));
            NewNonceHash2 = new BigInteger(1, br.ReadBytes(16));

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            bw.Write(Nonce.ToByteArrayUnsigned());
            bw.Write(ServerNonce.ToByteArrayUnsigned());
            bw.Write(NewNonceHash2.ToByteArrayUnsigned());

        }
    }
}