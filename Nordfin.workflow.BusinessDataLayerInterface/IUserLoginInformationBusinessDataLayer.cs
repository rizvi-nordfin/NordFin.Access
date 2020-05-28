using System.Collections.Generic;
using System.Data;
using Nordfin.workflow.Entity;

namespace Nordfin.workflow.BusinessDataLayerInterface
{
    public interface IUserLoginInformationBusinessDataLayer
    {
        DataSet GetUserLoginInformation(string UserName, long Date);
        DataSet GetTrafficDetails();
    }
}
