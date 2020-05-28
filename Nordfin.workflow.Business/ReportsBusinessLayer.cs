using System.Data;
using Nordfin.workflow.BusinessDataLayerInterface;
using Nordfin.workflow.DataAccessLayer;
using Nordfin.workflow.PresentationBusinessLayer;

namespace Nordfin.workflow.Business
{
    public sealed class ReportsBusinessLayer : IReportsPresentationBusinessLayer
    {

        IReportsBusinessDataLayer objReportlist = new ReportsDataAccessLayer();

        DataSet IReportsPresentationBusinessLayer.GetLedgerlistReport(string ClientID)
        {
            return objReportlist.GetLedgerlistReport(ClientID);
        }
        DataSet IReportsPresentationBusinessLayer.GetBatchesReport(string ClientID)
        {
            return objReportlist.GetBatchesReport(ClientID);
        }
        DataSet IReportsPresentationBusinessLayer.GetCustomerListReport(string ClientID)
        {
            return objReportlist.GetCustomerListReport(ClientID);
        }
        DataSet IReportsPresentationBusinessLayer.usp_getInvoicePeriodReport(string ClientID, string sFromDate, string sToDate)
        {
            return objReportlist.usp_getInvoicePeriodReport(ClientID, sFromDate, sToDate);
        }
        DataSet IReportsPresentationBusinessLayer.usp_getPeriodicReport(string ClientID, string sFromDate, string sToDate)
        {
            return objReportlist.usp_getPeriodicReport(ClientID, sFromDate, sToDate);
        }
    }
}
