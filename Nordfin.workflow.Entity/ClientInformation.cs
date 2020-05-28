using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nordfin.workflow.Entity
{
    public class ClientInformation
    {
        public int MypageCount { get; set; }
        public int MypageSuccess { get; set; }
        public int MypageFail { get; set; }
        public int AccessCount { get; set; }
        public int AccessSuccess { get; set; }
        public int AccessFail { get; set; }
    }
}
