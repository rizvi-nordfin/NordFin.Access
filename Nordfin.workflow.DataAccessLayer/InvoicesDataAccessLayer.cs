using Dapper;
using Nordfin.workflow.BusinessDataLayerInterface;
using Nordfin.workflow.Entity;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;

namespace Nordfin.workflow.DataAccessLayer
{
    public class InvoicesDataAccessLayer : DBBase, IInvoicesBusinessDataLayer
    {
        private string connectionString = ConfigurationManager.ConnectionStrings["NordfinConnec"].ToString();

        DataSet IInvoicesBusinessDataLayer.getInvoicesList(string custorinvoiceNum, string clientID, bool bCustomer, AdvanceSearch advanceSearch)
        {
            if (advanceSearch != null)
            {
                DBInitialize("usp_getInvoicesAdvanceSearch");
                DatabaseName.AddInParameter(DBBaseCommand, "@clientID", System.Data.DbType.Int32, Convert.ToInt32(clientID));
                DatabaseName.AddInParameter(DBBaseCommand, "@custName", System.Data.DbType.String, advanceSearch.Name);
                DatabaseName.AddInParameter(DBBaseCommand, "@custEmail", System.Data.DbType.String, advanceSearch.Email);
                DatabaseName.AddInParameter(DBBaseCommand, "@personalNumber", System.Data.DbType.String, advanceSearch.PersonalNumber);
            }
            else if (custorinvoiceNum == "0")
            {
                DBInitialize("usp_getInvoicesList");
                DatabaseName.AddInParameter(DBBaseCommand, "@clientID", System.Data.DbType.Int32, Convert.ToInt32(clientID));
            }
            else
            {
                DBInitialize("usp_getInvoices");
                DatabaseName.AddInParameter(DBBaseCommand, "@custInvoicesNum", System.Data.DbType.String, custorinvoiceNum);
                DatabaseName.AddInParameter(DBBaseCommand, "@clientID", System.Data.DbType.Int32, Convert.ToInt32(clientID));
                DatabaseName.AddInParameter(DBBaseCommand, "@bCustomer", System.Data.DbType.Boolean, bCustomer);
            }
            DataSet ds = DatabaseName.ExecuteDataSet(DBBaseCommand);
            //if (ds.Tables[0].Rows.Count > 0)
            //{
            //    try
            //    {
            //        ds.Tables[0].AsEnumerable().ToList<DataRow>().ForEach(a =>
            //        {
            //            a["Invoiceamount"] = String.Format(CultureInfo.GetCultureInfo("sv-SE"), "{0:#,0.00}", ConvertStringToDecimal(a.Field<string>("Invoiceamount")));
            //            a["Remainingamount"] = String.Format(CultureInfo.GetCultureInfo("sv-SE"), "{0:#,0.00}", ConvertStringToDecimal(a.Field<string>("Remainingamount")));
            //            a["TotalRemaining"] = String.Format(CultureInfo.GetCultureInfo("sv-SE"), "{0:#,0.00}", ConvertStringToDecimal(a.Field<string>("TotalRemaining")));
            //            a["Fees"] = String.Format(CultureInfo.GetCultureInfo("sv-SE"), "{0:#,0.00}", ConvertStringToDecimal(a.Field<string>("Fees")));
            //            a["Overpayment"] = String.Format(CultureInfo.GetCultureInfo("sv-SE"), "{0:#,0.00}", ConvertStringToDecimal(a.Field<string>("Overpayment")));
            //        });
            //    }
            //    catch
            //    {
            //        //catch the issue
            //    }
            //}
            return ds;
        }

        public decimal ConvertStringToDecimal(string sDecimal)
        {
            CultureInfo cultures = new CultureInfo("en-US");

            return Convert.ToDecimal(sDecimal, cultures);
        }


        string IInvoicesBusinessDataLayer.GetCustInvoiceEmailID(string ClientID, string CustomerNumber)
        {
            DBInitialize("usp_getCustomerInvoiceEmail");

            DatabaseName.AddInParameter(DBBaseCommand, "@clientID", System.Data.DbType.Int32, Convert.ToInt32(ClientID));
            DatabaseName.AddInParameter(DBBaseCommand, "@custNumber", System.Data.DbType.String, CustomerNumber);
            DatabaseName.AddOutParameter(DBBaseCommand, "@custEmail", DbType.String, 500);
            DatabaseName.ExecuteNonQuery(DBBaseCommand);
            string custEmail = Convert.ToString(DatabaseName.GetParameterValue(DBBaseCommand, "custEmail"));
            return custEmail;
        }

        int IInvoicesBusinessDataLayer.DeleteCustomerLogin(string ClientID, string CustomerNumber)
        {
            DBInitialize("usp_setCustomerLoginInactive");

            DatabaseName.AddInParameter(DBBaseCommand, "@clientID", System.Data.DbType.Int32, Convert.ToInt32(ClientID));
            DatabaseName.AddInParameter(DBBaseCommand, "@custNumber", System.Data.DbType.String, CustomerNumber);
            DatabaseName.ExecuteNonQuery(DBBaseCommand);

            return 0;
        }


        int IInvoicesBusinessDataLayer.UpdateCustomerInfo(CustomerInfo customerInfo, string ClientID)
        {


            DBInitialize("usp_setCustomerInfo");


            DatabaseName.AddInParameter(DBBaseCommand, "@custName", System.Data.DbType.String, customerInfo.Name);

            DatabaseName.AddInParameter(DBBaseCommand, "@Address1", System.Data.DbType.String, customerInfo.Address1);
            DatabaseName.AddInParameter(DBBaseCommand, "@Address2", System.Data.DbType.String, customerInfo.Address2);
            DatabaseName.AddInParameter(DBBaseCommand, "@PostalCode", System.Data.DbType.String, customerInfo.PostalCode);
            DatabaseName.AddInParameter(DBBaseCommand, "@City", System.Data.DbType.String, customerInfo.City);
            DatabaseName.AddInParameter(DBBaseCommand, "@Country", System.Data.DbType.String, customerInfo.Country);
            DatabaseName.AddInParameter(DBBaseCommand, "@Email", System.Data.DbType.String, customerInfo.Email);
            DatabaseName.AddInParameter(DBBaseCommand, "@Phone", System.Data.DbType.String, customerInfo.PhoneNumber);
            DatabaseName.AddInParameter(DBBaseCommand, "@custID", System.Data.DbType.Int32, customerInfo.CustomerID);

            DatabaseName.AddInParameter(DBBaseCommand, "@custnumber", System.Data.DbType.String, customerInfo.CustomerNumber);
            DatabaseName.AddInParameter(DBBaseCommand, "@comments", System.Data.DbType.String, customerInfo.Comments);

            DatabaseName.AddInParameter(DBBaseCommand, "@clientID", System.Data.DbType.Int32, Convert.ToInt32(ClientID));
            DatabaseName.AddInParameter(DBBaseCommand, "@userID", System.Data.DbType.Int32, customerInfo.UserID);
            DatabaseName.ExecuteNonQuery(DBBaseCommand);
            return 0;
        }

        IList<MatchInvoices> IInvoicesBusinessDataLayer.GetMatchedInvoices(string NegativeInvoices, string PositiveInvoices, string UserID)
        {
            DBInitialize("usp_setMatchedInvoice");
            DatabaseName.AddInParameter(DBBaseCommand, "@negativeInvoices", System.Data.DbType.String, NegativeInvoices);
            DatabaseName.AddInParameter(DBBaseCommand, "@positiveInvoices", System.Data.DbType.String, PositiveInvoices);
            DatabaseName.AddInParameter(DBBaseCommand, "@userID", System.Data.DbType.Int32, Convert.ToInt32(UserID));


            DataSet ds = DatabaseName.ExecuteDataSet(DBBaseCommand);


            IList<MatchInvoices> objMatchInvoices = new List<MatchInvoices>();
            if (ds.Tables[0].Rows.Count > 0)
            {

                objMatchInvoices = ds.Tables[0].AsEnumerable().Select(dataRow => new MatchInvoices
                {
                    InvoiceID = Convert.ToString(dataRow.Field<int>("InvoiceID")),
                    Remainingamount = String.Format(CultureInfo.GetCultureInfo("sv-SE"), "{0:#,0.00}", ConvertStringToDecimal(dataRow.Field<string>("Remainingamount")))
                }).ToList();
            }

            return objMatchInvoices;
        }

        DataSet IInvoicesBusinessDataLayer.getExportdetails(string custorinvoiceNum, string clientID)
        {
            DBInitialize("usp_getExportDetail");
            DatabaseName.AddInParameter(DBBaseCommand, "@custInvoicesNum", System.Data.DbType.String, custorinvoiceNum);
            DatabaseName.AddInParameter(DBBaseCommand, "@clientID", System.Data.DbType.Int32, Convert.ToInt32(clientID));

            DataSet ds = DatabaseName.ExecuteDataSet(DBBaseCommand);

            return ds;
        }

        IList<Notes> IInvoicesBusinessDataLayer.InsertInvoiceInfo(Notes objNotes)
        {
            DBInitialize("usp_setInsertNotes");
            DatabaseName.AddInParameter(DBBaseCommand, "@NotesText", System.Data.DbType.String, objNotes.NoteText);
            DatabaseName.AddInParameter(DBBaseCommand, "@InvoiceID", System.Data.DbType.Int32, Convert.ToInt32(objNotes.InvoiceID));
            DatabaseName.AddInParameter(DBBaseCommand, "@CustomerID", System.Data.DbType.String, objNotes.CustomerID);
            DatabaseName.AddInParameter(DBBaseCommand, "@UserID", System.Data.DbType.Int32, Convert.ToInt32(objNotes.UserID));
            DatabaseName.AddInParameter(DBBaseCommand, "@ClientID", System.Data.DbType.Int32, Convert.ToInt32(objNotes.ClientID));
            DatabaseName.AddInParameter(DBBaseCommand, "@UserName", System.Data.DbType.String, objNotes.UserName);
            DatabaseName.AddInParameter(DBBaseCommand, "@InvoiceNum", System.Data.DbType.String, objNotes.InvoiceNumber);


            DataSet ds = DatabaseName.ExecuteDataSet(DBBaseCommand);

            IList<Notes> objlstNotes = new List<Notes>();
            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                objlstNotes = ds.Tables[0].AsEnumerable().Select(dataRow => new Notes
                {
                    InvoiceNumber = dataRow.Field<string>("InvoiceNumber"),
                    NoteType = dataRow.Field<string>("NoteType"),
                    NoteDate = Convert.ToDateTime(dataRow.Field<string>("NoteDate")).ToString("yyyy-MM-dd HH:mm"),
                    UserName = dataRow.Field<string>("UserName"),
                    NoteText = dataRow.Field<string>("NoteText")

                }).ToList();
            }
            return objlstNotes;
        }

        bool IInvoicesBusinessDataLayer.AddNewCustomerInfo(CustomerInfo customerInfo)
        {
            int result = 0;
            using (SqlConnection db = new SqlConnection(connectionString))
            {
                var sqlStatement = @"INSERT INTO [dbo].[Customers]
                                       ([Customernumber]
                                       ,[Customername]
                                       ,[Personalnumber]
                                       ,[Customeradress]
                                       ,[CustomerPostalCode]
                                       ,[CustomerCity]
                                       ,[CustomerLand]
                                       ,[CustomerEmail]
                                       ,[ClientID]
                                       ,[Customertype]
                                       ,[CustomerPhone]
                                       ,[Customeradress2])
                                        VALUES
                                       (@CustomerNumber,
                                       @Name,  
                                       @PersonalNumber,
                                       @Address1,
                                       @PostalCode,
                                       @City, 
                                       @Country, 
                                       @Email,
                                       @ClientID,     
                                       @CustomerType, 
                                       @PhoneNumber,     
                                       @Address2)";

                result =  db.Execute(sqlStatement, customerInfo);
            };

            return result > 0;
        }

        Dictionary<string, string> IInvoicesBusinessDataLayer.CheckCustomerAlreadyExists(string customerNumber, string personalNumber, int clientId)
        {
            var result = new Dictionary<string, string>();
            using (SqlConnection db = new SqlConnection(connectionString))
            {
                var custNumber = db.Query<string>("SELECT CustomerNumber FROM Customers WHERE CustomerNumber = @CustomerNumber AND ClientId = @ClientId", new { CustomerNumber = customerNumber, ClientId = clientId }).FirstOrDefault();
                if (!string.IsNullOrWhiteSpace(custNumber))
                {
                    result.Add("CustomerNumber", "Customer Number already exist. Change the number to proceed");
                }

                var pNumber = db.Query<string>("SELECT PersonalNumber FROM Customers WHERE PersonalNumber = @PersonalNumber", new { PersonalNumber = personalNumber }).FirstOrDefault();
                if (!string.IsNullOrWhiteSpace(pNumber))
                {
                    result.Add("PersonalNumber", "Personal Number already exist. Do you want to proceed?");
                }
            };

            return result;
        }
    }
}
