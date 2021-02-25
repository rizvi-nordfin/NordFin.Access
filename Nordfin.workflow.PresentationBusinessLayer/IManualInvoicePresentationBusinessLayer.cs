using Nordfin.workflow.Entity;
using System.Collections.Generic;

namespace Nordfin.workflow.PresentationBusinessLayer
{
    public interface IManualInvoicePresentationBusinessLayer
    {
        CustomerInfo GetCustomerInfoForClient(string customerNumber, int clientID);

        int GetNumberSeries(string seriesName);

        void UpdateNumberSeries(string seriesName, int newSeries);

        bool ImportManualInvoice(string standardXml);

        Client GetClientPrintDetail(int clientId);

        List<ManualInvoiceMapping> GetTransformationMappings(int clientId);

        List<TransformationHeader> GetTransformationHeaders(int clientId);
    }
}
