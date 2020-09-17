using System.IO;
using TgSharp.Common.MTProto.Crypto;
using TgSharp.Common;
namespace TgSharp.Common.MTProto.Schema
{
    [TLObject(1715713620)]
    public class MTClientDHInnerData : TLObject
    {
        public override int Constructor => 1715713620;

        public BigInteger Nonce { get; set; }
        public BigInteger ServerNonce { get; set; }
        public long RetryId { get; set; }
        public byte[] Gb { get; set; }


        public void ComputeFlags()
        {
        }

        public override void DeserializeBody(BinaryReader br)
        {
            Nonce = new BigInteger(1, br.ReadBytes(16));
            ServerNonce = new BigInteger(1, br.ReadBytes(16));
            RetryId = br.ReadInt64();
            Gb = BytesUtil.Deserialize(br);
        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            bw.Write(Nonce.ToByteArrayUnsigned());
            bw.Write(ServerNonce.ToByteArrayUnsigned());
            bw.Write(RetryId);
            BytesUtil.Serialize(Gb, bw);
        }
    }
}