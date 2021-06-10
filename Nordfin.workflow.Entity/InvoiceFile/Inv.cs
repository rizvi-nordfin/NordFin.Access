using System.Collections.Generic;

namespace Nordfin.workflow.Entity
{
    public class Inv
    {
        public Inv()
        {
            Invoice = new Invoice();
            Customer = new Customer();
            InvoiceRows = new List<InvoiceRow>();
            Print = new Print();
        }

        public Invoice Invoice { get; set; }

        public Customer Customer { get; set; }

        public List<InvoiceRow> InvoiceRows { get; set; }

        public Print Print { get; set; }
    }
}
