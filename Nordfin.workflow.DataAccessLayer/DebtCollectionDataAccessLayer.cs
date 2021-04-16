using Dapper;
using Nordfin.workflow.BusinessDataLayerInterface;
using Nordfin.workflow.Entity;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nordfin.workflow.DataAccessLayer
{
    public class DebtCollectionDataAccessLayer : DBBase, IDebtCollectionBusinessDataLayer
    {
        private string connectionString = ConfigurationManager.ConnectionStrings["NordfinConnec"].ToString();
        IList<DebtCollectionList> IDebtCollectionBusinessDataLayer.GetDebtCollectionLists(int ClientID)
        {
            
            using (SqlConnection db = new SqlConnection(connectionString))
            {
                var procedure = "[usp_getDebtCollectionList]";

                var values = new DynamicParameters();

                values.Add("@clientID", ClientID);
               return  db.Query<DebtCollectionList>(procedure, values, commandType: CommandType.StoredProcedure).ToList();
            };

           
        }

        bool IDebtCollectionBusinessDataLayer.setCollectionStop(int InvoiceID)
        {
            try
            {
                DBInitialize("usp_setCollectionStopInDebtcollection");

                DatabaseName.AddInParameter(DBBaseCommand, "@invoiceID", DbType.Int32, InvoiceID);
                int result = DatabaseName.ExecuteNonQuery(DBBaseCommand);
                return result > 0;
            }
            catch
            {
                return false;
            }
        }

    }
}
