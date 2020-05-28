using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nordfin.workflow.Entity
{
    public class Transaction
    {
        public string BookingDate { get; set; }
        public string TransactionType { get; set; }
        public string TransactionText { get; set; }
        public string TransactionAmount { get; set; }
     
    }
}
