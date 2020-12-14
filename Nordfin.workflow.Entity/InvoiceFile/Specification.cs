namespace Nordfin.workflow.Entity
{
    using System;
    using System.Collections.Generic;
    using System.Xml.Serialization;

    [Serializable]
    public class Specification
    {
        public Specification()
        {
            Rows = new List<Row>();
        }

        [XmlElement(IsNullable = true, Order = 0, ElementName = "Row")]
        public List<Row> Rows { get; set; }
    }
}
