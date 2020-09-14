using System.IO;

using TgSharp.Common;
namespace TgSharp.Common.MTProto.Schema
{
    [TLObject(2059302892)]
    public class MTRequestPing : TLMethod
    {
        public override int Constructor => 2059302892;

        public long PingId { get; set; }
        public MTPong Response { get; set; }


        public void ComputeFlags()
        {
        }

        public override void DeserializeBody(BinaryReader br)
        {
            PingId = br.ReadInt64();
        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            bw.Write(PingId);
        }

        public override void DeserializeResponse(BinaryReader br)
        {
            Response = (MTPong)ObjectUtils.DeserializeObject(br);
        }
    }
}