using System.IO;

using TgSharp.Common;
namespace TgSharp.Common.MTProto.Schema
{
    [TLObject(-1370486635)]
    public class MTFutureSalts : TLObject
    {
        public override int Constructor => -1370486635;

        public long ReqMsgId { get; set; }
        public int Now { get; set; }
        public TLVector<MTFutureSalt> Salts { get; set; }


        public void ComputeFlags()
        {
        }

        public override void DeserializeBody(BinaryReader br)
        {
            ReqMsgId = br.ReadInt64();
            Now = br.ReadInt32();
            Salts = (TLVector<MTFutureSalt>)ObjectUtils.DeserializeVector<MTFutureSalt>(br);
        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            bw.Write(ReqMsgId);
            bw.Write(Now);
            ObjectUtils.SerializeObject(Salts, bw);
        }
    }
}