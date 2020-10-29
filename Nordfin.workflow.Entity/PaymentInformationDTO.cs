using System.Collections.Generic;


namespace Nordfin.workflow.Entity
{
    public class PaymentInformationDTO
    {
        public IList<Payments> objPaymentsList = new List<Payments>();
        public IList<Fees> objFeesList = new List<Fees>();
        public IList<Interest> objInterestList = new List<Interest>();
        public IList<Payout> objPayoutList = new List<Payout>();
        public IList<Transaction> objTransList = new List<Transaction>();
    }
}
