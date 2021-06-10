using Nordfin.workflow.BusinessDataLayerInterface;
using Nordfin.workflow.DataAccessLayer;
using Nordfin.workflow.Entity;
using Nordfin.workflow.PresentationBusinessLayer;
using System.Collections.Generic;
using System.Data;

namespace Nordfin.workflow.Business
{
    public sealed class InvoicesBusinessLayer : IInvoicesPresentationBusinessLayer
    {
        IInvoicesBusinessDataLayer objuser = new InvoicesDataAccessLayer();
        DataSet IInvoicesPresentationBusinessLayer.getInvoicesList(string custorinvoiceNum, string clientID, bool bCustomer, AdvanceSearch advanceSearch)
        {
            return objuser.getInvoicesList(custorinvoiceNum, clientID, bCustomer, advanceSearch);
        }

        string IInvoicesPresentationBusinessLayer.GetCustInvoiceEmailID(string ClientID, string CustomerNumber)
        {
            return objuser.GetCustInvoiceEmailID(ClientID, CustomerNumber);
        }
        int IInvoicesPresentationBusinessLayer.DeleteCustomerLogin(string ClientID, string CustomerNumber)
        {
            return objuser.DeleteCustomerLogin(ClientID, CustomerNumber);

        }
        int IInvoicesPresentationBusinessLayer.UpdateCustomerInfo(CustomerInfo customerInfo, string ClientID)
        {
            return objuser.UpdateCustomerInfo(customerInfo, ClientID);
        }
        IList<MatchInvoices> IInvoicesPresentationBusinessLayer.GetMatchedInvoices(string NegativeInvoices, string PositiveInvoices, string UserID)
        {
            return objuser.GetMatchedInvoices(NegativeInvoices, PositiveInvoices, UserID);
        }
        DataSet IInvoicesPresentationBusinessLayer.getExportdetails(string custorinvoiceNum, string clientID)
        {
            return objuser.getExportdetails(custorinvoiceNum, clientID);
        }

        IList<Notes> IInvoicesPresentationBusinessLayer.InsertInvoiceInfo(Notes notes)
        {
            return objuser.InsertInvoiceInfo(notes);
        }

        public bool AddNewCustomerInfo(CustomerInfo customerInfo)
        {
            return objuser.AddNewCustomerInfo(customerInfo);
        }

        public Dictionary<string, string> CheckCustomerAlreadyExists(string customerNumber, string personalNumber, int clientId)
        {
            return objuser.CheckCustomerAlreadyExists(customerNumber, personalNumber, clientId);
        }
    }

}
