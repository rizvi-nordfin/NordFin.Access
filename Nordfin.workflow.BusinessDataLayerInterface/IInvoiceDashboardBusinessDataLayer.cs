using Nordfin.workflow.Entity;
using System.Collections.Generic;
using System.Data;


namespace Nordfin.workflow.BusinessDataLayerInterface
{
    public interface IInvoiceDashboardBusinessDataLayer
    {
        DataSet getOverviewDashboard(string ClientID);
        IList<BatchVolume> getBatchVolumeDashboard(string ClientID);
        DataSet getCurrentYearChart(string ClientID);
        IList<Notification> getNotificationNotes(string clientID);
        CustomerInfoDTO getCustomerData(string clientID);
        int setCustomerRegion(string sPostalCode, string sCustomerRegion, string ClientID, string IsMatch);
        IList<CustomerMap> GetCustomerMapRegion(string ClientID, string IsMatch);
    }
}
