using Nordfin.workflow.BusinessDataLayerInterface;
using System;
using System.Data;

namespace Nordfin.workflow.DataAccessLayer
{
    public class ReportsDataAccessLayer : DBBase, IReportsBusinessDataLayer
    {
        DataSet IReportsBusinessDataLayer.GetLedgerlistReport(string ClientID)
        {
            DBInitialize("usp_getLedgerListReport");
            DatabaseName.AddInParameter(DBBaseCommand, "@ClientID", System.Data.DbType.Int32, Convert.ToInt32(ClientID));
            DataSet dataSet = DatabaseName.ExecuteDataSet(DBBaseCommand);
            return dataSet;
        }


        DataSet IReportsBusinessDataLayer.GetBatchesReport(string ClientID)
        {
            DBInitialize("usp_getBatchesClientReport");
            DatabaseName.AddInParameter(DBBaseCommand, "@ClientID", System.Data.DbType.Int32, Convert.ToInt32(ClientID));
            DataSet dataSet = DatabaseName.ExecuteDataSet(DBBaseCommand);
            return dataSet;
        }

        DataSet IReportsBusinessDataLayer.GetCustomerListReport(string ClientID)
        {
            DBInitialize("usp_getClientListReport");
            DatabaseName.AddInParameter(DBBaseCommand, "@ClientID", System.Data.DbType.Int32, Convert.ToInt32(ClientID));
            DataSet dataSet = DatabaseName.ExecuteDataSet(DBBaseCommand);
            return dataSet;
        }
        DataSet IReportsBusinessDataLayer.usp_getInvoicePeriodReport(string ClientID, string sFromDate, string sToDate)
        {
            DBInitialize("usp_getInvoicePeriodReport");
            DatabaseName.AddInParameter(DBBaseCommand, "@ClientID", System.Data.DbType.Int32, Convert.ToInt32(ClientID));
            DatabaseName.AddInParameter(DBBaseCommand, "@startDate", System.Data.DbType.String, sFromDate);
            DatabaseName.AddInParameter(DBBaseCommand, "@endDate", System.Data.DbType.String, sToDate);
            DataSet dataSet = DatabaseName.ExecuteDataSet(DBBaseCommand);
            return dataSet;
        }

        DataSet IReportsBusinessDataLayer.usp_getPeriodicReport(string ClientID, string sFromDate, string sToDate)
        {
            DBInitialize("usp_getPeriodicReport");
            DatabaseName.AddInParameter(DBBaseCommand, "@clientID", System.Data.DbType.Int32, Convert.ToInt32(ClientID));
            DatabaseName.AddInParameter(DBBaseCommand, "@startDate", System.Data.DbType.String, sFromDate);
            DatabaseName.AddInParameter(DBBaseCommand, "@endDate", System.Data.DbType.String, sToDate);
            DataSet dataSet = DatabaseName.ExecuteDataSet(DBBaseCommand);
            return dataSet;
        }

        DataSet IReportsBusinessDataLayer.GetContestedReport(string ClientID)
        {
            DBInitialize("usp_getContestedReport");
            DatabaseName.AddInParameter(DBBaseCommand, "@ClientID", System.Data.DbType.Int32, Convert.ToInt32(ClientID));
            DataSet dataSet = DatabaseName.ExecuteDataSet(DBBaseCommand);
            return dataSet;
        }

        DataSet IReportsBusinessDataLayer.GetStoppedReport(string ClientID)
        {
            DBInitialize("usp_getStoppedReport");
            DatabaseName.AddInParameter(DBBaseCommand, "@ClientID", System.Data.DbType.Int32, Convert.ToInt32(ClientID));
            DataSet dataSet = DatabaseName.ExecuteDataSet(DBBaseCommand);
            return dataSet;
        }

        DataSet IReportsBusinessDataLayer.GetTransactionReport(string ClientID, string sFromDate, string sToDate)
        {
            DBInitialize("usp_getTransactionReport");
            DatabaseName.AddInParameter(DBBaseCommand, "@ClientID", System.Data.DbType.Int32, Convert.ToInt32(ClientID));
            DatabaseName.AddInParameter(DBBaseCommand, "@startDate", System.Data.DbType.String, sFromDate);
            DatabaseName.AddInParameter(DBBaseCommand, "@endDate", System.Data.DbType.String, sToDate);
            DataSet dataSet = DatabaseName.ExecuteDataSet(DBBaseCommand);
            return dataSet;
        }

        DataSet IReportsBusinessDataLayer.GetInvoicesBatchesReport(string ClientID)
        {
            DBInitialize("usp_getInvoiceBatchesInfo");
            DatabaseName.AddInParameter(DBBaseCommand, "@ClientID", System.Data.DbType.Int32, Convert.ToInt32(ClientID));
            DataSet dataSet = DatabaseName.ExecuteDataSet(DBBaseCommand);
            return dataSet;
        }


    }
}
