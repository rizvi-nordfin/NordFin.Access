using Nordfin.workflow.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nordfin.workflow.PresentationBusinessLayer
{
    public interface IDebtCollectionPresentationBusinessLayer
    {
        IList<DebtCollectionList> GetDebtCollectionLists(int ClientID);
        bool setCollectionStop(int InvoiceID);
    }
}
