using System.Data;

namespace Nordfin.workflow.PresentationBusinessLayer
{
    public interface IBatchPresentationBusinessLayer
    {
        DataSet getInvoicesBatches(int Isdownload);
        DataSet getInvoicesBatchesReports(string ClientID);
    }
}
