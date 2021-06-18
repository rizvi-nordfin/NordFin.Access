using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nordfin.workflow.Entity
{
    public class EMailInvoices
    {
        public int UserID { get; set; }
        public string CustomerNumber { get; set; }

        public bool DisplayError { get; set; }
    }
}
