using Nordfin.workflow.BusinessDataLayerInterface;
using Nordfin.workflow.DataAccessLayer;
using Nordfin.workflow.Entity;
using Nordfin.workflow.PresentationBusinessLayer;
using System;
using System.Collections.Generic;


namespace Nordfin.workflow.Business
{
    public sealed class PaymentInformationBusinessLayer : IPaymentInformationPresentationBusinessLayer
    {
        IPaymentInformationBusinessDataLayer objuser = new PaymentInformationDataAccessLayer();
        Tuple<PaymentInformation, IList<Notes>> IPaymentInformationPresentationBusinessLayer.GetPaymentInformation(string InvoiceNumber, string ClientID)
        {
            return objuser.GetPaymentInformation(InvoiceNumber, ClientID);
        }
        PaymentInformationDTO IPaymentInformationPresentationBusinessLayer.GetPaymentInformationPayments(string InvoiceID)

        {
            return objuser.GetPaymentInformationPayments(InvoiceID);
        }
        IList<Fees> IPaymentInformationPresentationBusinessLayer.savePaymentFeeDetails(FessDetails objFeeDetails)
        {
            return objuser.savePaymentFeeDetails(objFeeDetails);
        }
        int IPaymentInformationPresentationBusinessLayer.savePayout(Payout objpayout)
        {
            return objuser.savePayout(objpayout);
        }
        IList<Interest> IPaymentInformationPresentationBusinessLayer.updateInterest(int InteresrID)
        {
            return objuser.updateInterest(InteresrID);
        }
        Tuple<Notes, IList<Notes>> IPaymentInformationPresentationBusinessLayer.insertInterest(Notes objNotes)
        {
            return objuser.insertInterest(objNotes);
        }
        int IPaymentInformationPresentationBusinessLayer.removePayout(int payoutID)
        {
            return objuser.removePayout(payoutID);
        }
    }
}
