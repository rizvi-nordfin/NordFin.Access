using Nordfin.workflow.BusinessDataLayerInterface;
using Nordfin.workflow.DataAccessLayer;
using Nordfin.workflow.Entity;
using Nordfin.workflow.PresentationBusinessLayer;
using System;

namespace Nordfin.workflow.BusinessLayer
{
    public sealed class ClientInformationBusinessLayer : IClientInformationPresentationBusinessLayer
    {
        IClientInformationBusinessDataLayer objClientDataLayer = new ClientInformationDataAccessLayer();
        Tuple<ClientInformation, ClientDetails> IClientInformationPresentationBusinessLayer.getClientInformationLogin(string ClientID, string UserID)
        {
            return objClientDataLayer.getClientInformationLogin(ClientID, UserID);
        }
    }
}
