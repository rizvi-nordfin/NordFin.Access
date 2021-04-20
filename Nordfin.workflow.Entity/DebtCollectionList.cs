using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nordfin.workflow.Entity
{
    public class DebtCollectionList
    {
        public int InvoiceID { get; set; }
        public string CustomerNumber { get; set; }
        public string InvoiceNumber { get; set; }
        public string RemainingAmount { get; set; }
        public string InvoiceAmount { get; set; }
        public string DueDate { get; set; }
        public string ExtDate { get; set; }
        public string CustomerName { get; set; }
    }
}
