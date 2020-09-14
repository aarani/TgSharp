using System.IO;
using TgSharp.Common.MTProto.Crypto;

using TgSharp.Common;
namespace TgSharp.Common.MTProto.Schema
{
    [TLObject(1003222836)]
    public class MTDHGenOk : MTAbsSetClientDHParamsAnswer
    {
        public override int Constructor => 1003222836;

        public BigInteger Nonce { get; set; }
        public BigInteger ServerNonce { get; set; }
        public BigInteger NewNonceHash1 { get; set; }


        public void ComputeFlags()
        {
        }

        public override void DeserializeBody(BinaryReader br)
        {
            Nonce = new BigInteger(1, br.ReadBytes(16));
            ServerNonce = new BigInteger(1, br.ReadBytes(16));
            NewNonceHash1 = new BigInteger(1, br.ReadBytes(16));

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            bw.Write(Nonce.ToByteArray());
            bw.Write(ServerNonce.ToByteArray());
            bw.Write(NewNonceHash1.ToByteArray());

        }
    }
}