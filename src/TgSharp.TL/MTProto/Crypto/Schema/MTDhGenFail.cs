using System.IO;
using TgSharp.Common.MTProto.Crypto;

using TgSharp.Common;
namespace TgSharp.Common.MTProto.Schema
{
    [TLObject(-1499615742)]
    public class MTDHGenFail : MTAbsSetClientDHParamsAnswer
    {
        public override int Constructor => -1499615742;

        public BigInteger Nonce { get; set; }
        public BigInteger ServerNonce { get; set; }
        public BigInteger NewNonceHash3 { get; set; }


        public void ComputeFlags()
        {
        }

        public override void DeserializeBody(BinaryReader br)
        {
            Nonce = new BigInteger(1, br.ReadBytes(16));
            ServerNonce = new BigInteger(1, br.ReadBytes(16));
            NewNonceHash3 = new BigInteger(1, br.ReadBytes(16));

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            bw.Write(Nonce.ToByteArrayUnsigned());
            bw.Write(ServerNonce.ToByteArrayUnsigned());
            bw.Write(NewNonceHash3.ToByteArrayUnsigned());
        }
    }
}