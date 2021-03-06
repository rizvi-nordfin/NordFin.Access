namespace Nordfin.workflow.Entity
{
    using System;
    using System.Collections.Generic;

    [Serializable]
    public class Print
    {
        public Print()
        {
            InvoiceDetail = new InvoiceDetail();
            Specification = new Specification();
            InvoiceText = new InvoiceText();
        }

        public InvoiceDetail InvoiceDetail { get; set; }

        public Specification Specification { get; set; }

        public InvoiceText InvoiceText { get; set; }

        public string BoundTo { get; set; }
    }
}
