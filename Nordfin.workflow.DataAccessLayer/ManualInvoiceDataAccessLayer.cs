using Nordfin.workflow.BusinessDataLayerInterface;
using Nordfin.workflow.Entity;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using Dapper;
using System.Linq;

namespace Nordfin.workflow.DataAccessLayer
{
    public class ManualInvoiceDataAccessLayer: DBBase, IManualInvoiceBusinessDataLayer
    {
        private string connectionString = ConfigurationManager.ConnectionStrings["NordfinConnec"].ToString();

        CustomerInfo IManualInvoiceBusinessDataLayer.GetCustomerInfoForClient(string customerNumber, int clientId)
        {   
            var customerInfo = new CustomerInfo();
            using (SqlConnection db = new SqlConnection(connectionString))
            {
                customerInfo = db.Query<CustomerInfo>("SELECT Customername AS [Name],Customeradress AS Address1, Customeradress2 AS Address2, CustomerPostalCode AS PostalCode, CustomerCity AS City, Customernumber AS CustomerNumber, " +
                                                       "Customerid AS CustomerId, CustomerType FROM Customers WHERE ClientID = @ClientId AND Customernumber = @customerNumber", new { clientId, customerNumber }).FirstOrDefault();
            };
           
            return customerInfo;
        }

        public int GetAndUpdateNumberSeries(string seriesName)
        {
            int number = 0;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                number = connection.Query<int>("SELECT Number FROM NumberSeries WHERE Series LIKE @SeriesName", new { SeriesName = "%" + seriesName + "%" }).FirstOrDefault();
                connection.Execute("UPDATE NumberSeries SET Number = @NewNumber WHERE Series LIKE @SeriesName", new { NewNumber = number + 1, SeriesName = "%" + seriesName + "%" });
            }

            return number;
        }

        public void UpdateNumberSeries(string seriesName, int newSeries)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Execute("UPDATE NumberSeries SET Number = @NewNumber WHERE Series LIKE @SeriesName", new { NewNumber = newSeries, SeriesName = "%" + seriesName + "%" });

            }
        }

        public bool ImportManualInvoice(string standardXml)
        {
            try
            {
                DBInitialize("usp_InsertCustomerInvoiceXmlData");

                DatabaseName.AddInParameter(DBBaseCommand, "@xmlCustomerInvoices", DbType.String, standardXml);
                int result = DatabaseName.ExecuteNonQuery(DBBaseCommand);
                return result > 0;
            }
            catch
            {
                return false;
            }
        }

        public Client GetClientPrintDetail(int clientId)
        {
            Client client;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                client = connection.Query<Client>(
                                            "SELECT CL.ClientId,ClientCurrency,ClientReference,SpvName AS LedgerName,CP.* FROM ClientList CL " +
                                            "INNER JOIN ClientPrintLayout CP ON CL.ClientId = CP.ClientId " +
                                            "LEFT JOIN Spv S ON S.id = CL.SpvId " +
                                            "WHERE CL.ClientId = @ClientId AND Active = 1",
                                            new { ClientId = clientId }).FirstOrDefault();
            }

            return client;
        }
        
        public List<ManualInvoiceMapping> GetTransformationMappings(int clientId)
        {
            var mappings = new List<ManualInvoiceMapping>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                mappings = connection.Query<ManualInvoiceMapping>("SELECT M.* FROM ManualInvoiceMapping M INNER JOIN ClientTransformationMapping CM ON M.MappingId = CM.MappingId WHERE ClientID = @ClientId", new { clientId }).ToList();
            }

            return mappings;
        }


        public List<TransformationHeader> GetTransformationHeaders(int clientId)
        {
            var headers = new List<TransformationHeader>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                headers = connection.Query<TransformationHeader>("SELECT TH.* FROM TransformationHeader TH INNER JOIN ClientTransformationMapping CM ON TH.MappingId = CM.MappingId  WHERE ClientID = @ClientId", new { clientId }).ToList();
            }

            return headers;
        }
    }
}
