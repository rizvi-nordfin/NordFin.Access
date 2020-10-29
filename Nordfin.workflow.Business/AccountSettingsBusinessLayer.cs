using Nordfin.workflow.BusinessDataLayerInterface;
using Nordfin.workflow.DataAccessLayer;
using Nordfin.workflow.Entity;
using Nordfin.workflow.PresentationBusinessLayer;
using System;
using System.Data;

namespace Nordfin.workflow.BusinessLayer
{
    public sealed class AccountSettingsBusinessLayer : IAccountSettingsPresentationBusinessLayer
    {
        IAccountSettingsBusinessDataLayer objAccountBusinessDataLayer = new AccountSettingsDataAccessLayer();
        Tuple<AccountSettings, DataTable> IAccountSettingsPresentationBusinessLayer.getAccountSettingsInfo(string ClientID, string UserID)
        {
            return objAccountBusinessDataLayer.getAccountSettingsInfo(ClientID, UserID);
        }
        int IAccountSettingsPresentationBusinessLayer.checkPasswordExists(string sUserID, string sPasswordHash)
        {
            return objAccountBusinessDataLayer.checkPasswordExists(sUserID, sPasswordHash);
        }
        int IAccountSettingsPresentationBusinessLayer.UpdateUserPassword(string userID, string password, string phoneNumber)
        {
            return objAccountBusinessDataLayer.UpdateUserPassword(userID, password, phoneNumber);
        }
        int IAccountSettingsPresentationBusinessLayer.UpdateUserEmail(string userID, UserEmail userEmail)
        {
            return objAccountBusinessDataLayer.UpdateUserEmail(userID, userEmail);
        }
    }
}
