using System.IO;

using TgSharp.Common;
namespace TgSharp.Common.MTProto.Schema
{
    [TLObject(1945237724)]
    public class MTMsgContainer : TLObject
    {
        public override int Constructor => 1945237724;

        public TLVector<MTMessage> Messages
        {
            get;
            set;
        }


        public void ComputeFlags()
        {
        }

        public override void DeserializeBody(BinaryReader br)
        {
            Messages = (TLVector<MTMessage>)ObjectUtils.DeserializeVector<MTMessage>(br);
        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            ObjectUtils.SerializeObject(Messages, bw);
        }
    }
}