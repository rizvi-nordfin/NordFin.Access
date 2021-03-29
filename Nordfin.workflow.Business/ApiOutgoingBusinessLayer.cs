using Nordfin.workflow.BusinessDataLayerInterface;
using Nordfin.workflow.DataAccessLayer;
using Nordfin.workflow.Entity;
using Nordfin.workflow.PresentationBusinessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nordfin.workflow.BusinessLayer
{
    public sealed class ApiOutgoingBusinessLayer : IApiOutgoingPresentationBusinessLayer
    {
        IApiOutgoingBusinessDataLayer objApiBusinessDataLayer = new ApiOutgoingDataAccessLayer();
        public ApiOutgoing GetApiOutgoing(int ClientID)
        {
            return objApiBusinessDataLayer.GetApiOutgoing(ClientID);
        }
    }
}
