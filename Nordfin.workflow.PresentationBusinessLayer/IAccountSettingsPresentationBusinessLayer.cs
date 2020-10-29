using Nordfin.workflow.Entity;
using System;
using System.Data;

namespace Nordfin.workflow.PresentationBusinessLayer
{
    public interface IAccountSettingsPresentationBusinessLayer
    {
        Tuple<AccountSettings, DataTable> getAccountSettingsInfo(string ClientID, string UserID);
        int checkPasswordExists(string sUserID, string sPasswordHash);
        int UpdateUserPassword(string userID, string password, string phoneNumber);
        int UpdateUserEmail(string userID, UserEmail userEmail);
    }
}
