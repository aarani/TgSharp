using System.IO;

using TgSharp.Common;
namespace TgSharp.Common.MTProto.Schema
{
    [TLObject(-1933520591)]
    public class MTMsgsAllInfo : TLObject
    {
        public override int Constructor => -1933520591;

        public TLVector<long> MsgIds { get; set; }
        public byte[] Info { get; set; }


        public void ComputeFlags()
        {
        }

        public override void DeserializeBody(BinaryReader br)
        {
            MsgIds = ObjectUtils.DeserializeVector<long>(br);
            Info = BytesUtil.Deserialize(br);
        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            ObjectUtils.SerializeObject(MsgIds, bw);
            BytesUtil.Serialize(Info, bw);
        }
    }
}