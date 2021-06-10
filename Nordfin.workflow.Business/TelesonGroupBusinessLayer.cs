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
    public sealed class TelesonGroupBusinessLayer: ITelsonGroupPresentationBusinessLayer
    {
        ITelsonGroupBusinessDataLayer telsonGroupBusinessData = new TelesonGroupDataAccessLayer();

        DataSet ITelsonGroupPresentationBusinessLayer.getContractList(string clientID)
        {
            return telsonGroupBusinessData.getContractList(clientID);
        }

        Tuple<IList<TelsonGroup>, IList<TelsonChart>, IList<ClientContracts>> ITelsonGroupPresentationBusinessLayer.GetTelsonGroupData(string ClientID)
        {
            return telsonGroupBusinessData.GetTelsonGroupData(ClientID);
        }

        Tuple<IList<TelsonGroup>, IList<TelsonChart>> ITelsonGroupPresentationBusinessLayer.GetTelecomData()
        {
            return telsonGroupBusinessData.GetTelecomData();
        }
        int ITelsonGroupPresentationBusinessLayer.setCreditCheck(CreditCheck creditCheck)
        {
            return telsonGroupBusinessData.setCreditCheck(creditCheck);
        }

        bool ITelsonGroupPresentationBusinessLayer.setCreditAutoAccount(CreditAutoAccount autoAccount)
        {
            return telsonGroupBusinessData.setCreditAutoAccount(autoAccount);
        }

        CreditAutoAccount ITelsonGroupPresentationBusinessLayer.getCreditAutoAccountDetails(int ClientID)
        {
            return telsonGroupBusinessData.getCreditAutoAccountDetails(ClientID);
        }
    }
}
