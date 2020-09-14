using System.IO;
using TgSharp.Common;
namespace TgSharp.Common.MTProto.Schema
{
    [TLObject(1658015945)]
    public class MTDestroySessionNone : MTAbsDestroySessionRes
    {
        public override int Constructor => 1658015945;

        public long SessionId { get; set; }


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
    }
}