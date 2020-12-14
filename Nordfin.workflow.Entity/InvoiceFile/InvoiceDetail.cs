namespace Nordfin.workflow.Entity
{
    using System;
    using System.Collections.Generic;
    using System.Xml.Serialization;

    [Serializable]
    public class InvoiceDetail
    {
        public InvoiceDetail()
        {
            Rows = new List<Row>();
        }

        [XmlElement(IsNullable = true, Order = 0, ElementName = "Row")]
        public List<Row> Rows { get; set; }
    }
}
