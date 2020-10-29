using Nordfin.workflow.PresentationBusinessLayer;
using Nordfin.workflow.BusinessDataLayerInterface;
using Nordfin.workflow.DataAccessLayer;
using System.Data;
using Nordfin.workflow.Entity;
using System.Collections.Generic;


namespace Nordfin.workflow.Business
{
    public sealed class InvoiceDashboardBusinessLayer : IInvoiceDashboardPresentationBusinessLayer
    {
        IInvoiceDashboardBusinessDataLayer objInvoiceDashboardlayer = new InvoiceDashboardDataAccessLayer();

        DataSet IInvoiceDashboardPresentationBusinessLayer.getOverviewDashboard(string ClientID)
        {
            return objInvoiceDashboardlayer.getOverviewDashboard(ClientID);
        }

        IList<BatchVolume> IInvoiceDashboardPresentationBusinessLayer.getBatchVolumeDashboard(string ClientID)
        {
            return objInvoiceDashboardlayer.getBatchVolumeDashboard(ClientID);
        }
        DataSet IInvoiceDashboardPresentationBusinessLayer.getCurrentYearChart(string ClientID)
        {
            return objInvoiceDashboardlayer.getCurrentYearChart(ClientID);
        }
      
        IList<Notification> IInvoiceDashboardPresentationBusinessLayer.getNotificationNotes(string clientID)
        {
            return objInvoiceDashboardlayer.getNotificationNotes(clientID);
        }
        CustomerInfoDTO IInvoiceDashboardPresentationBusinessLayer.getCustomerData(string clientID)
        {
            return objInvoiceDashboardlayer.getCustomerData(clientID);
        }
        int IInvoiceDashboardPresentationBusinessLayer.setCustomerRegion(string sPostalCode, string sCustomerRegion, string ClientID, string IsMatch)
        {
            return objInvoiceDashboardlayer.setCustomerRegion(sPostalCode, sCustomerRegion, ClientID, IsMatch);
        }
        IList<CustomerMap> IInvoiceDashboardPresentationBusinessLayer.GetCustomerMapRegion(string ClientID, string IsMatch)
        {
            return objInvoiceDashboardlayer.GetCustomerMapRegion(ClientID,IsMatch);
        }
       
    }
}
