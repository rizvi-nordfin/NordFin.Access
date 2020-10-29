using Nordfin.workflow.Entity;
using System;

namespace Nordfin.workflow.PresentationBusinessLayer
{
    public interface IClientInformationPresentationBusinessLayer
    {
        Tuple<ClientInformation, ClientDetails> getClientInformationLogin(string ClientID, string UserID);
    }
}
