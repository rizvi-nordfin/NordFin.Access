using Nordfin.workflow.BusinessDataLayerInterface;
using Nordfin.workflow.Entity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;

namespace Nordfin.workflow.DataAccessLayer
{
    public class InvoicesDataAccessLayer : DBBase, IInvoicesBusinessDataLayer
    {
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
            if (ds.Tables[0].Rows.Count > 0)
            {
                try
                {
                    ds.Tables[0].AsEnumerable().ToList<DataRow>().ForEach(a =>
                    {
                        a["Invoiceamount"] = String.Format(CultureInfo.GetCultureInfo("sv-SE"), "{0:#,0.00}", ConvertStringToDecimal(a.Field<string>("Invoiceamount")));
                        a["Remainingamount"] = String.Format(CultureInfo.GetCultureInfo("sv-SE"), "{0:#,0.00}", ConvertStringToDecimal(a.Field<string>("Remainingamount")));
                        a["TotalRemaining"] = String.Format(CultureInfo.GetCultureInfo("sv-SE"), "{0:#,0.00}", ConvertStringToDecimal(a.Field<string>("TotalRemaining")));
                        a["Fees"] = String.Format(CultureInfo.GetCultureInfo("sv-SE"), "{0:#,0.00}", ConvertStringToDecimal(a.Field<string>("Fees")));
                        a["Overpayment"] = String.Format(CultureInfo.GetCultureInfo("sv-SE"), "{0:#,0.00}", ConvertStringToDecimal(a.Field<string>("Overpayment")));
                    });
                }
                catch
                {
                    //catch the issue
                }
            }
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
    }
}
