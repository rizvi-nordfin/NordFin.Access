using Nordfin.workflow.Entity;
using System.Collections.Generic;

namespace Nordfin.workflow.BusinessDataLayerInterface
{
    public interface IManualInvoiceBusinessDataLayer
    {
        CustomerInfo GetCustomerInfoForClient(string customerNumbner, int clientID);

        int GetNumberSeries(string seriesName);

        void UpdateNumberSeries(string seriesName, int newSeries);

        bool ImportManualInvoice(string standardXml);

        Client GetClientPrintDetail(int clientId);

        List<ManualInvoiceMapping> GetTransformationMappings(int clientId);

        List<TransformationHeader> GetTransformationHeaders(int clientId);
    }
}
