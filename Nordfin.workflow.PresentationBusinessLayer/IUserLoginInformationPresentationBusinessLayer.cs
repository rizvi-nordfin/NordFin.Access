using System.Data;

namespace Nordfin.workflow.PresentationBusinessLayer
{
    public interface IUserLoginInformationPresentationBusinessLayer
    {
        DataSet GetUserLoginInformation(string UserName, long Date);
        DataSet GetTrafficDetails();

    }
}
