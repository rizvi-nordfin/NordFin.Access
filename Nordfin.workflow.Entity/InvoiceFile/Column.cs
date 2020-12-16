namespace Nordfin.workflow.Entity
{
    using System;
    using System.Xml.Serialization;

    [Serializable]
    public class Column
    {
        [XmlAttribute(AttributeName = "index")]
        public string Index { get; set; }

        [XmlText]
        public string Col { get; set; }
    }
}
