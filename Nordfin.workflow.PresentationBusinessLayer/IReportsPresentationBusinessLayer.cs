
using System.Data;


namespace Nordfin.workflow.PresentationBusinessLayer
{
    public interface IReportsPresentationBusinessLayer
    {
        DataSet GetLedgerlistReport(string ClientID);
        DataSet GetBatchesReport(string ClientID);
        DataSet GetCustomerListReport(string ClientID);
        DataSet usp_getInvoicePeriodReport(string ClientID, string sFromDate, string sToDate);
        DataSet usp_getPeriodicReport(string ClientID, string sFromDate, string sToDate);

    }
}
