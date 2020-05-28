using Nordfin.workflow.BusinessDataLayerInterface;
using Nordfin.workflow.DataAccessLayer;
using Nordfin.workflow.Entity;
using Nordfin.workflow.PresentationBusinessLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
