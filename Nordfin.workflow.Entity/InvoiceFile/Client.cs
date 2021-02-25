namespace Nordfin.workflow.Entity
{
    using System;
    using System.Xml.Serialization;

    [Serializable]
    public class Client
    {
        public int ClientId { get; set; }

        [XmlIgnore]
        public string ClientCurrency { get; set; }

        public string ClientReference { get; set; }

        public string LedgerName { get; set; }

        public string Name { get; set; }

        public string Address { get; set; }

        public string PostalCode { get; set; }

        public string City { get; set; }

        public string Country { get; set; }

        public string Phone { get; set; }

        public string Email { get; set; }

        public string Web { get; set; }

        public string OrgNo { get; set; }

        public string VatNo { get; set; }

        public string BG { get; set; }

        public string ContactPerson { get; set; }

        public string ContactPhone { get; set; }

        public string ContactEmail { get; set; }

        public string IBAN { get; set; }

        public string VatInfo { get; set; }

        public string BIC_Swift { get; set; }

        public string ReturnAddress1 { get; set; }

        public string ReturnAddress2 { get; set; }

        public string ClientNote { get; set; }

        public string OpeningHours { get; set; }

        public string EmailTextHtml { get; set; }

        public string LegalText { get; set; }

        public string ClientPrintName { get; set; }

        public string Department { get; set; }

        [XmlIgnore]
        public string FreeText { get; set; }
    }
}
