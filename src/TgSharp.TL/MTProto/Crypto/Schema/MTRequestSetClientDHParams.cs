using System.IO;
using TgSharp.Common.MTProto.Crypto;

using TgSharp.Common;
namespace TgSharp.Common.MTProto.Schema
{
    [TLObject(-184262881)]
    public class MTRequestSetClientDHParams : TLMethod
    {
        public override int Constructor => -184262881;

        public BigInteger Nonce { get; set; }
        public BigInteger ServerNonce { get; set; }
        public byte[] EncryptedData { get; set; }
        public MTAbsSetClientDHParamsAnswer Response { get; set; }


        public void ComputeFlags()
        {
        }

        public override void DeserializeBody(BinaryReader br)
        {
            Nonce = new BigInteger(1, br.ReadBytes(16));
            ServerNonce = new BigInteger(1, br.ReadBytes(16));
            EncryptedData = BytesUtil.Deserialize(br);
        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            bw.Write(Nonce.ToByteArrayUnsigned());
            bw.Write(ServerNonce.ToByteArrayUnsigned());
            BytesUtil.Serialize(EncryptedData, bw);
        }

        public override void DeserializeResponse(BinaryReader br)
        {
            Response = (MTAbsSetClientDHParamsAnswer)ObjectUtils.DeserializeObject(br);
        }
    }
}