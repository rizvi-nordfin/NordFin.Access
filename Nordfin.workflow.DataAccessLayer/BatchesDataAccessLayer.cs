using Nordfin.workflow.BusinessDataLayerInterface;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nordfin.workflow.DataAccessLayer
{
    public class BatchesDataAccessLayer : DBBase, IBatchesBusinessDataLayer
    {

        DataSet IBatchesBusinessDataLayer.getInvoicesBatches(int Isdownload)
        {
            DBInitialize("usp_getInvoiceBatches");
            DatabaseName.AddInParameter(DBBaseCommand, "@Isdownload", System.Data.DbType.Int32, Isdownload);
            DataSet dataSet = DatabaseName.ExecuteDataSet(DBBaseCommand);
            return dataSet;
        }

        DataSet IBatchesBusinessDataLayer.getInvoicesBatchesReports(string ClientID)
        {
            DBInitialize("usp_getInvoiceBatchesReport");
            DatabaseName.AddInParameter(DBBaseCommand, "@ClientID", System.Data.DbType.Int32, Convert.ToInt32(ClientID));
            DataSet ds = DatabaseName.ExecuteDataSet(DBBaseCommand);
            return ds;
        }
    }
}
