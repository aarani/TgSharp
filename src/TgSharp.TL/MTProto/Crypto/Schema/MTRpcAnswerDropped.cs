using System.IO;

using TgSharp.Common;
namespace TgSharp.Common.MTProto.Schema
{
    [TLObject(-1539647305)]
    public class MTRpcAnswerDropped : MTAbsRpcDropAnswer
    {
        public override int Constructor => -1539647305;

        public long MsgId { get; set; }
        public int SeqNo { get; set; }
        public int Bytes { get; set; }


        public void ComputeFlags()
        {
        }

        public override void DeserializeBody(BinaryReader br)
        {
            MsgId = br.ReadInt64();
            SeqNo = br.ReadInt32();
            Bytes = br.ReadInt32();
        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            bw.Write(MsgId);
            bw.Write(SeqNo);
            bw.Write(Bytes);
        }
    }
}