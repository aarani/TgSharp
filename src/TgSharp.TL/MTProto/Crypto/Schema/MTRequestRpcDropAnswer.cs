using System.IO;

using TgSharp.Common;
namespace TgSharp.Common.MTProto.Schema
{
    [TLObject(1491380032)]
    public class MTRequestRpcDropAnswer : TLMethod
    {
        public override int Constructor => 1491380032;

        public long ReqMsgId { get; set; }
        public MTAbsRpcDropAnswer Response { get; set; }


        public void ComputeFlags()
        {
        }

        public override void DeserializeBody(BinaryReader br)
        {
            ReqMsgId = br.ReadInt64();
        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            bw.Write(ReqMsgId);
        }

        public override void DeserializeResponse(BinaryReader br)
        {
            Response = (MTAbsRpcDropAnswer)ObjectUtils.DeserializeObject(br);
        }
    }
}