using Nordfin.workflow.BusinessDataLayerInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nordfin.workflow.DataAccessLayer
{
    public class PaymentInformation : DBBase, IPaymentInformationBusinessDataLayer
    {
        PaymentInformation IPaymentInformationBusinessDataLayer.GetPaymentInformation(string InvoiceNumber)
        {
            PaymentInformation objpaymentinfo = new PaymentInformation();

            return objpaymentinfo;

        }
    }
}
