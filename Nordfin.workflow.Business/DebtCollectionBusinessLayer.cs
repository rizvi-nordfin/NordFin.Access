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
    public class DebtCollectionBusinessLayer : IDebtCollectionPresentationBusinessLayer
    {
        IDebtCollectionBusinessDataLayer objDebtColllectionDataLayer = new DebtCollectionDataAccessLayer();

        IList<DebtCollectionList> IDebtCollectionPresentationBusinessLayer.GetDebtCollectionLists(int ClientID)
        {
            return objDebtColllectionDataLayer.GetDebtCollectionLists(ClientID);
        }
        bool IDebtCollectionPresentationBusinessLayer.setCollectionStop(int InvoiceID)
        {
            return objDebtColllectionDataLayer.setCollectionStop(InvoiceID);
        }
    }
}
