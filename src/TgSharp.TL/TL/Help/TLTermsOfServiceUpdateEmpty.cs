using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using TgSharp.TL;

namespace TgSharp.TL.Help
{
    [TLObject(-483352705)]
    public class TLTermsOfServiceUpdateEmpty : TLAbsTermsOfServiceUpdate
    {
        public override int Constructor
        {
            get
            {
                return -483352705;
            }
        }

        public int Expires { get; set; }

        public void ComputeFlags()
        {
            // do nothing
        }

        public override void DeserializeBody(BinaryReader br)
        {
            Expires = br.ReadInt32();
        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            bw.Write(Expires);
        }
    }
}
