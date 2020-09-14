using System.IO;

using TgSharp.Common;
namespace TgSharp.Common.MTProto.Schema
{
    [TLObject(-213746804)]
    public class MTRequestPingDelayDisconnect : TLMethod
    {
        public override int Constructor => -213746804;

        public long PingId { get; set; }
        public int DisconnectDelay { get; set; }
        public MTPong Response { get; set; }


        public void ComputeFlags()
        {
        }

        public override void DeserializeBody(BinaryReader br)
        {
            PingId = br.ReadInt64();
            DisconnectDelay = br.ReadInt32();
        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            bw.Write(PingId);
            bw.Write(DisconnectDelay);
        }

        public override void DeserializeResponse(BinaryReader br)
        {
            Response = (MTPong)ObjectUtils.DeserializeObject(br);
        }
    }
}