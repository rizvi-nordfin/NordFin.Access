using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nordfin.workflow.Entity
{
    public class AccessLog
    {
        public int CustomerID { get; set; }
        public string CustomerNumber { get; set; }
        public int ClientID { get; set; }
        public int UserID { get; set; }
        public string Comments { get; set; }
    }
}
