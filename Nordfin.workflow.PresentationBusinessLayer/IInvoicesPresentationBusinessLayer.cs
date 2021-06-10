using Nordfin.workflow.Entity;
using System.Collections.Generic;
using System.Data;

namespace Nordfin.workflow.PresentationBusinessLayer
{
    public interface IInvoicesPresentationBusinessLayer
    {
        DataSet getInvoicesList(string custorinvoiceNum, string clientID, bool bCustomer, AdvanceSearch advanceSearch);
        string GetCustInvoiceEmailID(string ClientID, string CustomerNumber);
        int DeleteCustomerLogin(string ClientID, string CustomerNumber);
        int UpdateCustomerInfo(CustomerInfo customerInfo, string ClientID);
        IList<MatchInvoices> GetMatchedInvoices(string NegativeInvoices, string PositiveInvoices, string UserID);
        DataSet getExportdetails(string custorinvoiceNum, string clientID);
        IList<Notes> InsertInvoiceInfo(Notes notes);

        bool AddNewCustomerInfo(CustomerInfo customerInfo);
        int setEmailSentAccessLog(AccessLog accessLog);

        Dictionary<string, string> CheckCustomerAlreadyExists(string customerNumber, string personalNumber, int clientId);
    }
}
