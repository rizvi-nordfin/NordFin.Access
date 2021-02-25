using Nordfin.workflow.BusinessDataLayerInterface;
using Nordfin.workflow.DataAccessLayer;
using Nordfin.workflow.Entity;
using Nordfin.workflow.PresentationBusinessLayer;
using System.Collections.Generic;

namespace Nordfin.workflow.BusinessLayer
{
    public class ManualInvoiceBusinessLayer : IManualInvoicePresentationBusinessLayer
    {
        IManualInvoiceBusinessDataLayer objManualInvoice = new ManualInvoiceDataAccessLayer();

        public CustomerInfo GetCustomerInfoForClient(string customerNumber, int clientID)
        {
            return objManualInvoice.GetCustomerInfoForClient(customerNumber, clientID);
        }

        public int GetNumberSeries(string seriesName)
        {
            return objManualInvoice.GetNumberSeries(seriesName);
        }

        public void UpdateNumberSeries(string seriesName, int newSeries)
        {
            objManualInvoice.UpdateNumberSeries(seriesName, newSeries);
        }

        public bool ImportManualInvoice(string standardXml)
        {
            return objManualInvoice.ImportManualInvoice(standardXml);
        }

        public Client GetClientPrintDetail(int clientId)
        {
            return objManualInvoice.GetClientPrintDetail(clientId);
        }

        public List<ManualInvoiceMapping> GetTransformationMappings(int clientId)
        {
            return objManualInvoice.GetTransformationMappings(clientId);
        }

        public List<TransformationHeader> GetTransformationHeaders(int clientId)
        {
            return objManualInvoice.GetTransformationHeaders(clientId);
        }
    }
}
