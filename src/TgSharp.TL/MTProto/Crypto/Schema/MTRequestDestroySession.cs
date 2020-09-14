using System.IO;

using TgSharp.Common;
namespace TgSharp.Common.MTProto.Schema
{
    [TLObject(-414113498)]
    public class MTRequestDestroySession : TLMethod
    {
        public override int Constructor => -414113498;

        public long SessionId { get; set; }
        public MTAbsDestroySessionRes Response { get; set; }


        public void ComputeFlags()
        {
        }

        public override void DeserializeBody(BinaryReader br)
        {
            SessionId = br.ReadInt64();
        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            bw.Write(SessionId);
        }

        public override void DeserializeResponse(BinaryReader br)
        {
            Response = (MTAbsDestroySessionRes)ObjectUtils.DeserializeObject(br);
        }
    }
}