using System.IO;
using TgSharp.Common.MTProto.Crypto;
using TgSharp.Common;

namespace TgSharp.Common.MTProto.Schema
{
    [TLObject(85337187)]
    public class MTResPq : TLObject
    {
        public override int Constructor => 85337187;

        public BigInteger Nonce { get; set; }
        public BigInteger ServerNonce { get; set; }
        public byte[] Pq { get; set; }
        public TLVector<long> ServerPublicKeyFingerprints { get; set; }


        public void ComputeFlags()
        {
        }

        public override void DeserializeBody(BinaryReader br)
        {
            Nonce = new BigInteger(1, br.ReadBytes(16));
            ServerNonce = new BigInteger(1, br.ReadBytes(16));
            Pq = BytesUtil.Deserialize(br);
            ServerPublicKeyFingerprints = ObjectUtils.DeserializeVector<long>(br);
        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            bw.Write(Nonce.ToByteArray());
            bw.Write(ServerNonce.ToByteArray());
            BytesUtil.Serialize(Pq, bw);
            ObjectUtils.SerializeObject(ServerPublicKeyFingerprints, bw);
        }
    }
}