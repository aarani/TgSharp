using System.IO;

using TgSharp.Common;
namespace TgSharp.Common.MTProto.Schema
{
    [TLObject(-530561358)]
    public class MTMsgCopy : TLObject
    {
        public override int Constructor => -530561358;

        public MTMessage OrigMessage { get; set; }


        public void ComputeFlags()
        {
        }

        public override void DeserializeBody(BinaryReader br)
        {
            OrigMessage = (MTMessage)ObjectUtils.DeserializeObject(br);
        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            ObjectUtils.SerializeObject(OrigMessage, bw);
        }
    }
}