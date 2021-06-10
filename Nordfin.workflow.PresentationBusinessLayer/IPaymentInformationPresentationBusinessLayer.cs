using Nordfin.workflow.Entity;
using System;
using System.Collections.Generic;


namespace Nordfin.workflow.PresentationBusinessLayer
{
    public interface IPaymentInformationPresentationBusinessLayer
    {
        Tuple<PaymentInformation, IList<Notes>> GetPaymentInformation(string InvoiceNumber, string ClientID);
        PaymentInformationDTO GetPaymentInformationPayments(string InvoiceID);
        IList<Fees> savePaymentFeeDetails(FessDetails objFeeDetails);
        int savePayout(Payout objpayout);
        IList<Interest> updateInterest(int InteresrID);
        Tuple<Notes, IList<Notes>> insertInterest(Notes objNotes);
        int removePayout(int payoutID);
        IList<Notes> insertServerJob(ServerJob serverJob);
    }
}
