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
    public sealed class TelesonGroupBusinessLayer: ITelsonGroupPresentationBusinessLayer
    {
        ITelsonGroupBusinessDataLayer telsonGroupBusinessData = new TelesonGroupDataAccessLayer();
        Tuple<IList<TelsonGroup>, IList<TelsonChart>> ITelsonGroupPresentationBusinessLayer.GetTelsonGroupData(string ClientID)
        {
            return telsonGroupBusinessData.GetTelsonGroupData(ClientID);
        }
    }
}
