using System.IO;
using TgSharp.Common.MTProto.Crypto;

using TgSharp.Common;
namespace TgSharp.Common.MTProto.Schema
{
    [TLObject(-2083955988)]
    public class MTPQInnerData : MTAbsPQInnerData
    {
        public override int Constructor => -2083955988;

        public byte[] PQ { get; set; }
        public byte[] P { get; set; }
        public byte[] Q { get; set; }
        public BigInteger Nonce { get; set; }
        public BigInteger ServerNonce { get; set; }
        public BigInteger NewNonce { get; set; }


        public void ComputeFlags()
        {
        }

        public override void DeserializeBody(BinaryReader br)
        {
            PQ = BytesUtil.Deserialize(br);
            P = BytesUtil.Deserialize(br);
            Q = BytesUtil.Deserialize(br);
            Nonce = new BigInteger(1, br.ReadBytes(16));
            ServerNonce = new BigInteger(1, br.ReadBytes(16));
            NewNonce = new BigInteger(1, br.ReadBytes(32));
        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            BytesUtil.Serialize(PQ, bw);
            BytesUtil.Serialize(P, bw);
            BytesUtil.Serialize(Q, bw);
            bw.Write(Nonce.ToByteArray());
            bw.Write(ServerNonce.ToByteArray());
            bw.Write(NewNonce.ToByteArray());
        }
    }
}