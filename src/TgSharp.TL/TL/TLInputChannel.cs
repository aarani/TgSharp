using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using TgSharp.Common;

namespace TgSharp.Common
{
    [TLObject(-1343524562)]
    public class TLInputChannel : TLAbsInputChannel
    {
        public override int Constructor
        {
            get
            {
                return -1343524562;
            }
        }

        public int ChannelId { get; set; }
        public long AccessHash { get; set; }

        public void ComputeFlags()
        {
            // do nothing
        }

        public override void DeserializeBody(BinaryReader br)
        {
            ChannelId = br.ReadInt32();
            AccessHash = br.ReadInt64();
        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            bw.Write(ChannelId);
            bw.Write(AccessHash);
        }
    }
}
