
using System.Data;


namespace Nordfin.workflow.BusinessDataLayerInterface
{
    public interface IBatchesBusinessDataLayer
    {
        DataSet getInvoicesBatches(int Isdownload);
        DataSet getInvoicesBatchesReports(string ClientID);
    }
}
