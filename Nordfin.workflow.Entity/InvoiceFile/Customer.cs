namespace Nordfin.workflow.Entity
{
    using System;
    using System.Xml.Serialization;

    [Serializable]
    public class Customer
    {
        [XmlElement(ElementName = "Customername")]
        public string CustomerName { get; set; } = string.Empty;

        [XmlElement(ElementName = "Customeradress")]
        public string CustomerAddress { get; set; } = string.Empty;

        [XmlElement(ElementName = "Customeradress2")]
        public string CustomerAddress2 { get; set; } = string.Empty;

        [XmlElement(ElementName = "CustomerPostalCode")]
        public string CustomerPostalCode { get; set; } = string.Empty;

        [XmlElement(ElementName = "Customercity")]
        public string CustomerCity { get; set; } = string.Empty;

        [XmlElement(ElementName = "Customerland")]
        public string CustomerLand { get; set; } = string.Empty;

        [XmlElement(ElementName = "Customertype")]
        public string CustomerType { get; set; } = string.Empty;

        [XmlElement(ElementName = "ClientID")]
        public string ClientId { get; set; } = string.Empty;

        [XmlElement(ElementName = "Customernumber")]
        public string CustomerNumber { get; set; } = string.Empty;

        [XmlElement(ElementName = "CustomerEmail")]
        public string CustomerEmail { get; set; } = string.Empty;
    }
}
