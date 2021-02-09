using Nordfin.workflow.Entity;

namespace Nordfin.workflow.PresentationBusinessLayer
{
    public interface IManualInvoicePresentationBusinessLayer
    {
        CustomerInfo GetCustomerInfoForClient(string customerNumber, int clientID);

        int GetNumberSeries(string seriesName);

        void UpdateNumberSeries(string seriesName, int newSeries);

        bool ImportManualInvoice(string standardXml);

        Client GetClientPrintDetail(int clientId);
    }
}
