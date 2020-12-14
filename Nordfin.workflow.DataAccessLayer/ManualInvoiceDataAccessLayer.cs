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

        IList<CustomerInfo> IManualInvoiceBusinessDataLayer.GetCustomerInfoForClient(int clientId)
        {   
            var customerInfo = new List<CustomerInfo>();
            using (SqlConnection db = new SqlConnection(connectionString))
            {
                customerInfo = db.Query<CustomerInfo>("SELECT Customername AS [Name],Customeradress AS Address1, Customeradress2 AS Address2, CustomerPostalCode AS PostalCode, CustomerCity AS City, Customernumber AS CustomerNumber, Customerid AS CustomerId FROM Customers WHERE ClientID = @ClientId", new { ClientId = clientId }).ToList();
            };
           
            return customerInfo;
        }

        public List<ContractInvoice> GetContractInvoices()
        {
            var contracts = new List<ContractInvoice>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                contracts = connection.Query<ContractInvoice>("SELECT * FROM ContractInvoicing").ToList();
                var contractRows = connection.Query<ContractInvoiceRow>("SELECT * FROM ContractInvoicingRows").ToList();
                foreach (var item in contracts)
                {
                    item.ContractInvoiceRows = contractRows.Where(c => c.ContractNumber == item.ContractNumber).Select(c => c).ToList();
                }
            }

            return contracts;
        }

        public int GetNumberSeries(string seriesName)
        {
            int number = 0;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                number = connection.Query<int>("SELECT Number FROM NumberSeries WHERE Series LIKE @SeriesName", new { SeriesName = "%" + seriesName + "%" }).FirstOrDefault();
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
    }
}
