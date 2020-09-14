using System.IO;

using TgSharp.Common;
namespace TgSharp.Common.MTProto.Schema
{
    [TLObject(558156313)]
    public class MTRpcError : TLObject
    {
        public override int Constructor => 558156313;

        public int ErrorCode { get; set; }
        public string ErrorMessage { get; set; }


        public void ComputeFlags()
        {
        }

        public override void DeserializeBody(BinaryReader br)
        {
            ErrorCode = br.ReadInt32();
            ErrorMessage = StringUtil.Deserialize(br);
        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            bw.Write(ErrorCode);
            StringUtil.Serialize(ErrorMessage, bw);
        }
    }
}