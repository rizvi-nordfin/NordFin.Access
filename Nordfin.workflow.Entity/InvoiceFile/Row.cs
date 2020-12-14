namespace Nordfin.workflow.Entity
{
    using System;
    using System.Collections.Generic;
    using System.Xml.Serialization;

    [Serializable]
    public class Row
    {
        public Row()
        {
            Col = new List<Column>();
        }

        [XmlAttribute(AttributeName = "type")]
        public string Type { get; set; }

        [XmlElement(IsNullable = true, Order = 0)]
        public List<Column> Col { get; set; }
    }
}
