using Nordfin.workflow.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nordfin.workflow.PresentationBusinessLayer
{
    public interface IClientInformationPresentationBusinessLayer
    {
        Tuple<ClientInformation, ClientDetails> getClientInformationLogin(string ClientID, string UserID);
    }
}
