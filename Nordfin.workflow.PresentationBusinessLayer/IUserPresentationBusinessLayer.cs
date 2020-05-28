using System.Collections.Generic;
using Nordfin.workflow.Entity;

namespace Nordfin.workflow.PresentationBusinessLayer
{
    public interface IUserPresentationBusinessLayer
    {
        Users GetUser(string UserName, string Password);

        IList<ClientList> GetClientList(int Admin, int ClientID, string UserID, out string BatchValues);

        void UpdateClientID(string UserName, string ClientID);

        int InsertLoginUserInformation(LoginUserInformation loginUserInformation);
        int UpdateSessionID(string UserName, long SessionID);
        int checkEmailVerification(string token);
    }
}
