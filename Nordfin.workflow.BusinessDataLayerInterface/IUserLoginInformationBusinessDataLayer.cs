using System.Data;

namespace Nordfin.workflow.BusinessDataLayerInterface
{
    public interface IUserLoginInformationBusinessDataLayer
    {
        DataSet GetUserLoginInformation(string UserName, long Date);
        DataSet GetTrafficDetails();
    }
}
