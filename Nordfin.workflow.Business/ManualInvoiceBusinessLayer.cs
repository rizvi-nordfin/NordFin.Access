using Nordfin.workflow.BusinessDataLayerInterface;
using Nordfin.workflow.DataAccessLayer;
using Nordfin.workflow.Entity;
using Nordfin.workflow.PresentationBusinessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nordfin.workflow.BusinessLayer
{
    public class ManualInvoiceBusinessLayer : IManualInvoicePresentationBusinessLayer
    {
        IManualInvoiceBusinessDataLayer objManualInvoice = new ManualInvoiceDataAccessLayer();

        public IList<CustomerInfo> GetCustomerInfoForClient(int clientID)
        {
            return objManualInvoice.GetCustomerInfoForClient(clientID);
        }

        public int GetNumberSeries(string seriesName)
        {
            return objManualInvoice.GetNumberSeries(seriesName);
        }

        public void UpdateNumberSeries(string seriesName, int newSeries)
        {
            objManualInvoice.UpdateNumberSeries(seriesName, newSeries);
        }
    }
}
