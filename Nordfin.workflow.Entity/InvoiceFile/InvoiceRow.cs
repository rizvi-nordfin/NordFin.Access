namespace Nordfin.workflow.Entity
{
    using System;
    using System.Xml.Serialization;

    [Serializable]
    public class InvoiceRow : Base
    {
        private string vatAmount;
        private string invoiceNoVatAmount;
        private string price;
        private string total;

        [XmlElement(Order = 1)]
        public int Id { get; set; }

        [XmlElement(Order = 2)]
        public string Number { get; set; }

        [XmlElement(Order = 3)]
        public string Description { get; set; }

        [XmlElement(Order = 4)]
        public string Period { get; set; }

        [XmlElement(Order = 5)]
        public string Quantity { get; set; }

        [XmlElement(Order = 6)]
        public string Unit { get; set; }

        [XmlElement(Order = 7)]
        public string VatPercent { get; set; }

        [XmlElement(Order = 8)]
        public string VatAmount
        {
            get
            {
                return !string.IsNullOrWhiteSpace(vatAmount) ? FormatAmount(vatAmount) : vatAmount;
            }

            set
            {
                vatAmount = value;
            }
        }

        [XmlElement(Order = 9)]
        public string Price
        {
            get
            {
                return !string.IsNullOrWhiteSpace(price) ? FormatAmount(price) : price;
            }

            set
            {
                price = value;
            }
        }

        [XmlElement(Order = 10)]
        public string Total
        {
            get
            {
                return !string.IsNullOrWhiteSpace(total) ? FormatAmount(total) : total;
            }

            set
            {
                total = value;
            }
        }

        [XmlIgnore]
        public string InvoiceNoVATAmount
        {
            get
            {
                return !string.IsNullOrWhiteSpace(invoiceNoVatAmount) ? FormatAmount(invoiceNoVatAmount) : invoiceNoVatAmount;
            }

            set
            {
                invoiceNoVatAmount = value;
            }
        }

        [XmlIgnore]
        public double Amount
        {
            get
            {
                return double.Parse(Price, System.Globalization.CultureInfo.InvariantCulture);
            }
        }

        [XmlIgnore]
        public double Vat
        {
            get
            {
                return double.Parse(VatAmount, System.Globalization.CultureInfo.InvariantCulture);
            }
        }
    }
}
