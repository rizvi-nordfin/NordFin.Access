namespace Nordfin.workflow.Entity
{
    using System;
    using System.Xml.Serialization;

    [Serializable]
    public class Invoice : Base
    {
        private string invoiceAmount;
        private string invoiceVATAmount;
        private string remainingAmount;
        private string billDate;
        private string dueDate;
        private string delivery;

        [XmlElement(ElementName = "Customernumber")]
        public string CustomerNumber { get; set; }

        [XmlElement(ElementName = "Invoicenumber")]
        public string InvoiceNumber { get; set; }

        [XmlElement(ElementName = "CombineInvoice")]
        public string CombineInvoiceNumber { get; set; }

        [XmlElement(ElementName = "CombineAmount")]
        public string CombineAmount { get; set; }

        [XmlElement(ElementName = "Invoiceamount")]
        public string InvoiceAmount
        {
            get
            {
                return !string.IsNullOrWhiteSpace(invoiceAmount) ? FormatAmount(invoiceAmount) : invoiceAmount;
            }

            set
            {
                invoiceAmount = value;
            }
        }

        [XmlElement(ElementName = "Remainingamount")]
        public string RemainingAmount
        {
            get
            {
                return !string.IsNullOrWhiteSpace(remainingAmount) ? FormatAmount(remainingAmount) : remainingAmount;
            }

            set
            {
                remainingAmount = value;
            }
        }

        [XmlElement(ElementName = "Billdate")]
        public string BillDate
        {
            get
            {
                return FormatDateString(billDate);
            }

            set
            {
                billDate = value;
            }
        }

        [XmlElement(ElementName = "ClientID")]
        public string ClientID { get; set; }

        [XmlElement(ElementName = "Duedate")]
        public string DueDate
        {
            get
            {
                return FormatDateString(dueDate);
            }

            set
            {
                dueDate = value;
            }
        }

        [XmlElement(ElementName = "Paymentreference")]
        public string PaymentReference { get; set; }

        [XmlElement(ElementName = "InvoiceVATAmount")]
        public string InvoiceVATAmount
        {
            get
            {
                return !string.IsNullOrWhiteSpace(invoiceVATAmount) ? FormatAmount(invoiceVATAmount) : invoiceVATAmount;
            }

            set
            {
                invoiceVATAmount = value;
            }
        }

        [XmlElement(ElementName = "Purchased")]
        public string Purchased { get; set; }

        [XmlElement(ElementName = "CurrencyCode")]
        public string CurrencyCode { get; set; }

        [XmlElement(ElementName = "Filename")]
        public string FileName { get; set; }

        [XmlElement(ElementName = "OrderNumber")]
        public string OrderNumber { get; set; }

        [XmlElement(ElementName = "ConnectionID")]
        public string ConnectionId { get; set; }

        [XmlElement(ElementName = "Delivery")]
        public string Delivery
        {
            get
            {
                if (int.TryParse(delivery, out int result))
                {
                    switch (result)
                    {
                        case 1:
                            return "PAPER";
                        case 11:
                            return "EMAIL";
                        case 74:
                        case 75:
                            return "E-NOTE";
                        case 94:
                            return "E-INVOICE";
                        default:
                            return "PAPER";
                    }
                }
                else
                {
                    return delivery;
                }
            }

            set
            {
                delivery = value;
            }
        }

        [XmlIgnore]
        public string CustomerType { get; set; }

        [XmlIgnore]
        public double Amount
        {
            get
            {
                return double.Parse(InvoiceAmount, System.Globalization.CultureInfo.InvariantCulture);
            }
        }

        [XmlIgnore]
        public double VatAmount
        {
            get
            {
                return double.Parse(InvoiceVATAmount, System.Globalization.CultureInfo.InvariantCulture);
            }
        }
    }
}
