using System.IO;
using TgSharp.Common.MTProto.Crypto;

using TgSharp.Common;
namespace TgSharp.Common.MTProto.Schema
{
    [TLObject(-790100132)]
    public class MTServerDhParamsOk : MTAbsServerDHParams
    {
        public override int Constructor => -790100132;

        public BigInteger Nonce { get; set; }
        public BigInteger ServerNonce { get; set; }
        public byte[] EncryptedAnswer { get; set; }


        public void ComputeFlags()
        {
        }

        public override void DeserializeBody(BinaryReader br)
        {
            Nonce = new BigInteger(1, br.ReadBytes(16));
            ServerNonce = new BigInteger(1, br.ReadBytes(16));
            EncryptedAnswer = BytesUtil.Deserialize(br);
        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            bw.Write(Nonce.ToByteArray());
            bw.Write(ServerNonce.ToByteArray());
            BytesUtil.Serialize(EncryptedAnswer, bw);
        }
    }
}