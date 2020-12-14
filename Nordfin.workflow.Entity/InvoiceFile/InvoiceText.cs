namespace Nordfin.workflow.Entity
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Xml.Serialization;

    [Serializable]
    public class InvoiceText
    {
        public InvoiceText()
        {
            Rows = new List<Row>();
        }

        [XmlElement(IsNullable = true, Order = 0, ElementName = "Row")]
        public List<Row> Rows { get; set; }
    }
}
