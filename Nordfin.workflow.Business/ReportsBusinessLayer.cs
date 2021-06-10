using Nordfin.workflow.BusinessDataLayerInterface;
using Nordfin.workflow.DataAccessLayer;
using Nordfin.workflow.PresentationBusinessLayer;
using System.Data;

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
        DataSet IReportsPresentationBusinessLayer.GetContestedReport(string ClientID)
        {
            return objReportlist.GetContestedReport(ClientID);
        }

        DataSet IReportsPresentationBusinessLayer.GetStoppedReport(string ClientID)
        {
            return objReportlist.GetStoppedReport(ClientID);
        }

        DataSet IReportsPresentationBusinessLayer.GetTransactionReport(string ClientID, string sFromDate, string sToDate)
        {
            return objReportlist.GetTransactionReport(ClientID, sFromDate, sToDate);
        }
        DataSet IReportsPresentationBusinessLayer.GetInvoicesBatchesReport(string ClientID)
        {
            return objReportlist.GetInvoicesBatchesReport(ClientID);
        }

    }
}
