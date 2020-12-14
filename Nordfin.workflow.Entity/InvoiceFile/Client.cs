namespace Nordfin.workflow.Entity
{
    using System;
    using System.Xml.Serialization;

    [Serializable]
    public class Client
    {
        public string ClientId { get; set; }

        public string ClientName { get; set; }

        [XmlIgnore]
        public string ClientCurrency { get; set; }

        public string ClientReference { get; set; }

        [XmlIgnore]
        public string FUI { get; set; }
    }
}
