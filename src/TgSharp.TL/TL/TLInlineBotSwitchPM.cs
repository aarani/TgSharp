using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using TgSharp.Common;

namespace TgSharp.Common
{
    [TLObject(1008755359)]
    public class TLInlineBotSwitchPM : TLObject
    {
        public override int Constructor
        {
            get
            {
                return 1008755359;
            }
        }

        public string Text { get; set; }
        public string StartParam { get; set; }

        public void ComputeFlags()
        {
            // do nothing
        }

        public override void DeserializeBody(BinaryReader br)
        {
            Text = StringUtil.Deserialize(br);
            StartParam = StringUtil.Deserialize(br);
        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            StringUtil.Serialize(Text, bw);
            StringUtil.Serialize(StartParam, bw);
        }
    }
}
