using System;


namespace Nordfin.workflow.Entity
{
    public class Payout
    {
        public string BankAccount { get; set; }
        public int InvoiceID { get; set; }
        public int OverCreditID { get; set; }
        public int UserID { get; set; }
        public int Active { get; set; }
        public int OverpayCreditID { get; set; }
        public int PayoutID { get; set; }
    }
}
