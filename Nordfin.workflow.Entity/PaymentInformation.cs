using System;


namespace Nordfin.workflow.Entity
{
    public class PaymentInformation
    {

        public string PaymentReference { get; set; }
        public string Delivery { get; set; }
        public string Collectionstatus { get; set; }
        public DateTime CollectionDate { get; set; }
        public DateTime DueDate { get; set; }
        public string Collectionstop { get; set; }
        public string Collectionstopuntil { get; set; }
        public string Paymentmethod { get; set; }
        public string Purchased { get; set; }
        public string Contested { get; set; }
        public string ContestedDate { get; set; }

    }

}
