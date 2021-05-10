using Nordfin.workflow.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nordfin.workflow.BusinessDataLayerInterface
{
    public interface IApiOutgoingBusinessDataLayer
    {
        ApiOutgoing GetApiOutgoing(int ClientID);
    }
}
