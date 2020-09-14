using System.IO;

using TgSharp.Common;
namespace TgSharp.Common.MTProto.Schema
{
    [TLObject(-1188971260)]
    public class MTRequestGetFutureSalts : TLMethod
    {
        public override int Constructor => -1188971260;

        public int Num { get; set; }
        public MTFutureSalts Response { get; set; }


        public void ComputeFlags()
        {
        }

        public override void DeserializeBody(BinaryReader br)
        {
            Num = br.ReadInt32();
        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            bw.Write(Num);
        }

        public override void DeserializeResponse(BinaryReader br)
        {
            Response = (MTFutureSalts)ObjectUtils.DeserializeObject(br);
        }
    }
}