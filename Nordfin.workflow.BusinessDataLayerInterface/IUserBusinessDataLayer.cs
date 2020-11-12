using Nordfin.workflow.Entity;
using System.Collections.Generic;

namespace Nordfin.workflow.BusinessDataLayerInterface
{
    public interface IUserBusinessDataLayer
    {
        Users GetUser(string UserName, string Password);

        IList<ClientList> GetClientList(int Admin, int ClientID, string UserID, out string BatchValues);
        void UpdateClientID(string UserName, string ClientID);
        int InsertLoginUserInformation(LoginUserInformation loginUserInformation);
        int UpdateSessionID(string UserName, long SessionID);
        int checkEmailVerification(string token);

    }
}
