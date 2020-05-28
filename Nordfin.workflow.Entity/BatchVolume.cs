using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nordfin.workflow.Entity
{
    public class BatchVolume
    {
        public int Year { get; set; }
        public int Month { get; set; }
        public string InvoiceNumber { get; set; }
        public string InvoiceAmount { get; set; }
    }
}
