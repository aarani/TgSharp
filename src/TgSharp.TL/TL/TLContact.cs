using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using TgSharp.Common;

namespace TgSharp.Common
{
    [TLObject(-116274796)]
    public class TLContact : TLObject
    {
        public override int Constructor
        {
            get
            {
                return -116274796;
            }
        }

        public int UserId { get; set; }
        public bool Mutual { get; set; }

        public void ComputeFlags()
        {
            // do nothing
        }

        public override void DeserializeBody(BinaryReader br)
        {
            UserId = br.ReadInt32();
            Mutual = BoolUtil.Deserialize(br);
        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            bw.Write(UserId);
            BoolUtil.Serialize(Mutual, bw);
        }
    }
}
