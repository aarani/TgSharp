using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using TgSharp.Common;

namespace TgSharp.Common
{
    [TLObject(-1066346178)]
    public class TLPageBlockPreformatted : TLAbsPageBlock
    {
        public override int Constructor
        {
            get
            {
                return -1066346178;
            }
        }

        public TLAbsRichText Text { get; set; }
        public string Language { get; set; }

        public void ComputeFlags()
        {
            // do nothing
        }

        public override void DeserializeBody(BinaryReader br)
        {
            Text = (TLAbsRichText)ObjectUtils.DeserializeObject(br);
            Language = StringUtil.Deserialize(br);
        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            ObjectUtils.SerializeObject(Text, bw);
            StringUtil.Serialize(Language, bw);
        }
    }
}
