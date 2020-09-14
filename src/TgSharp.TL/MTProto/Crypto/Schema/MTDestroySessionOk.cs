using System.IO;
using TgSharp.Common;
namespace TgSharp.Common.MTProto.Schema
{
    [TLObject(-501201412)]
    public class MTDestroySessionOk : MTAbsDestroySessionRes
    {
        public override int Constructor => -501201412;

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