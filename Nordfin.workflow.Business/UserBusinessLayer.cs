using System.Collections.Generic;
using Nordfin.workflow.BusinessDataLayerInterface;
using Nordfin.workflow.DataAccessLayer;
using Nordfin.workflow.Entity;
using Nordfin.workflow.PresentationBusinessLayer;

namespace Nordfin.workflow.Business
{
    public sealed class UserBusinessLayer : IUserPresentationBusinessLayer
    {
        IUserBusinessDataLayer objuser = new UserDataAccessLayer();
        Users IUserPresentationBusinessLayer.GetUser(string UserName, string Password)
        {
            return objuser.GetUser(UserName, Password);
        }

        IList<ClientList> IUserPresentationBusinessLayer.GetClientList(int Admin, int ClientID, string UserID, out string BatchValues)
        {
            return objuser.GetClientList(Admin, ClientID, UserID,out BatchValues);
        }
        void IUserPresentationBusinessLayer.UpdateClientID(string UserName, string ClientID)
        {
            objuser.UpdateClientID(UserName, ClientID);
        }

        int IUserPresentationBusinessLayer.InsertLoginUserInformation(LoginUserInformation loginUserInformation)
        {
            return objuser.InsertLoginUserInformation(loginUserInformation);
        }
        int IUserPresentationBusinessLayer.UpdateSessionID(string UserName, long SessionID)
        {
            return objuser.UpdateSessionID(UserName, SessionID);
        }
        int IUserPresentationBusinessLayer.checkEmailVerification(string token)
        {
            return objuser.checkEmailVerification(token);

        }
    }
}
