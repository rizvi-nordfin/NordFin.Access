using Nordfin.workflow.BusinessDataLayerInterface;
using Nordfin.workflow.DataAccessLayer;
using Nordfin.workflow.PresentationBusinessLayer;
using System.Data;

namespace Nordfin.workflow.BusinessLayer
{
    public sealed class UserLoginInformationBusinessLayer : IUserLoginInformationPresentationBusinessLayer
    {
        IUserLoginInformationBusinessDataLayer objuserLoginInform = new UserLoginInformationDataAccessLayer();
        DataSet IUserLoginInformationPresentationBusinessLayer.GetUserLoginInformation(string UserName, long Date)
        {
            return objuserLoginInform.GetUserLoginInformation(UserName, Date);
        }
        DataSet IUserLoginInformationPresentationBusinessLayer.GetTrafficDetails()
        {
            return objuserLoginInform.GetTrafficDetails();
        }
    }
}
