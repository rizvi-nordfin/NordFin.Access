using Nordfin.workflow.Entity;

namespace Nordfin.workflow.BusinessDataLayerInterface
{
    public interface IManualInvoiceBusinessDataLayer
    {
        CustomerInfo GetCustomerInfoForClient(string customerNumbner, int clientID);

        int GetAndUpdateNumberSeries(string seriesName);

        void UpdateNumberSeries(string seriesName, int newSeries);

        bool ImportManualInvoice(string standardXml);
    }
}
