using System.IO;
using TgSharp.Common.MTProto.Crypto;

using TgSharp.Common;
namespace TgSharp.Common.MTProto.Schema
{
    [TLObject(1615239032)]
    public class MTRequestReqPQ : TLMethod
    {
        public override int Constructor => 1615239032;

        public BigInteger Nonce { get; set; }
        public MTResPq Response { get; set; }


        public void ComputeFlags()
        {
        }

        public override void DeserializeBody(BinaryReader br)
        {
            Nonce = new BigInteger(1, br.ReadBytes(16));
        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            bw.Write(Nonce.ToByteArrayUnsigned());
        }

        public override void DeserializeResponse(BinaryReader br)
        {
            Response = (MTResPq)ObjectUtils.DeserializeObject(br);
        }
    }
}