using Nordfin.workflow.Entity;
using System;

namespace Nordfin.workflow.BusinessDataLayerInterface
{
    public interface IClientInformationBusinessDataLayer
    {
        Tuple<ClientInformation, ClientDetails> getClientInformationLogin(string ClientID, string UserID);
    }
}
