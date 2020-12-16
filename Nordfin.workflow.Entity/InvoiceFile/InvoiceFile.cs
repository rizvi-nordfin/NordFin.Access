namespace Nordfin.workflow.Entity
{
    using System;
    using System.Collections.Generic;

    [Serializable]
    public class InvoiceFile
    {
        public InvoiceFile()
        {
            Invoices = new List<Inv>();
        }

        public Client Client { get; set; }

        public List<Inv> Invoices { get; set; }
    }
}
