using System.IO;
using System.IO.Compression;

using TgSharp.Common;
namespace TgSharp.Common.MTProto.Schema
{
    [TLObject(812830625)]
    public class MTGzipPacked : TLObject
    {
        public override int Constructor => 812830625;

        public TLObject UnpackedObject { get; set; }

        public void ComputeFlags()
        {
        }

        public override void DeserializeBody(BinaryReader br)
        {
            var packedData = BytesUtil.Deserialize(br);
            ObjectUtils.DeserializeObject(Decompress(packedData));
        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);

            var UnpackedData = UnpackedObject.Serialize();

            var packedData = Compress(UnpackedData);
            BytesUtil.Serialize(packedData, bw);
        }

        private byte[] Decompress(byte[] data)
        {
            using (var compressedStream = new MemoryStream(data))
            using (var zipStream = new GZipStream(compressedStream, CompressionMode.Decompress))
            using (var resultStream = new MemoryStream())
            {
                zipStream.CopyTo(resultStream);
                return resultStream.ToArray();
            }
        }

        private byte[] Compress(byte[] data)
        {
            using (var compressedStream = new MemoryStream())
            using (var zipStream = new GZipStream(compressedStream, CompressionMode.Compress))
            {
                zipStream.Write(data, 0, data.Length);
                zipStream.Close();
                return compressedStream.ToArray();
            }
        }
    }
}