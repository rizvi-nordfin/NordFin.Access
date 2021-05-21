using Nordfin.workflow.BusinessDataLayerInterface;
using Nordfin.workflow.DataAccessLayer;
using Nordfin.workflow.Entity;
using Nordfin.workflow.PresentationBusinessLayer;

namespace Nordfin.workflow.BusinessLayer
{
    public class ManualInvoiceBusinessLayer : IManualInvoicePresentationBusinessLayer
    {
        IManualInvoiceBusinessDataLayer objManualInvoice = new ManualInvoiceDataAccessLayer();

        public CustomerInfo GetCustomerInfoForClient(string customerNumber, int clientID)
        {
            return objManualInvoice.GetCustomerInfoForClient(customerNumber, clientID);
        }

        public int GetAndUpdateNumberSeries(string seriesName)
        {
            return objManualInvoice.GetAndUpdateNumberSeries(seriesName);
        }

        public void UpdateNumberSeries(string seriesName, int newSeries)
        {
            objManualInvoice.UpdateNumberSeries(seriesName, newSeries);
        }

        public bool ImportManualInvoice(string standardXml)
        {
            return objManualInvoice.ImportManualInvoice(standardXml);
        }
    }
}
