using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using TgSharp.Common;

namespace TgSharp.Common
{
    [TLObject(-1425052898)]
    public class TLUpdatePhoneCall : TLAbsUpdate
    {
        public override int Constructor
        {
            get
            {
                return -1425052898;
            }
        }

        public TLAbsPhoneCall PhoneCall { get; set; }

        public void ComputeFlags()
        {
            // do nothing
        }

        public override void DeserializeBody(BinaryReader br)
        {
            PhoneCall = (TLAbsPhoneCall)ObjectUtils.DeserializeObject(br);
        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            ObjectUtils.SerializeObject(PhoneCall, bw);
        }
    }
}
