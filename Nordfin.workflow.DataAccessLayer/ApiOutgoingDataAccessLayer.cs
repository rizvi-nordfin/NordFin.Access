using Dapper;
using Nordfin.workflow.BusinessDataLayerInterface;
using Nordfin.workflow.Entity;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nordfin.workflow.DataAccessLayer
{
    public class ApiOutgoingDataAccessLayer : DBBase, IApiOutgoingBusinessDataLayer
    {
        private string connectionString = ConfigurationManager.ConnectionStrings["NordfinConnec"].ToString();
        ApiOutgoing IApiOutgoingBusinessDataLayer.GetApiOutgoing(int ClientID)
        {
            var apiOutgoing = new ApiOutgoing();
            using (SqlConnection db = new SqlConnection(connectionString))
            {
                apiOutgoing = db.Query<ApiOutgoing>("SELECT ApiKey,ApiToken,APiSecret from api_outgoing where ClientID=@ClientID", new { ClientID }).FirstOrDefault();
            };

            return apiOutgoing;
        }
    }
}
