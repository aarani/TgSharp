using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using TgSharp.Common;

namespace TgSharp.Common
{
    [TLObject(-1564789301)]
    public class TLPhoneCallProtocol : TLObject
    {
        public override int Constructor
        {
            get
            {
                return -1564789301;
            }
        }

        public int Flags { get; set; }
        public bool UdpP2p { get; set; }
        public bool UdpReflector { get; set; }
        public int MinLayer { get; set; }
        public int MaxLayer { get; set; }

        public void ComputeFlags()
        {
            // do nothing
        }

        public override void DeserializeBody(BinaryReader br)
        {
            Flags = br.ReadInt32();
            UdpP2p = (Flags & 1) != 0;
            UdpReflector = (Flags & 2) != 0;
            MinLayer = br.ReadInt32();
            MaxLayer = br.ReadInt32();
        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            bw.Write(Flags);
            bw.Write(MinLayer);
            bw.Write(MaxLayer);
        }
    }
}
