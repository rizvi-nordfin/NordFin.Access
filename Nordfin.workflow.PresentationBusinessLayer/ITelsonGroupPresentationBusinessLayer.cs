using Nordfin.workflow.Entity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nordfin.workflow.PresentationBusinessLayer
{
    public interface ITelsonGroupPresentationBusinessLayer
    {
        Tuple<IList<TelsonGroup>, IList<TelsonChart>, IList<ClientContracts>> GetTelsonGroupData(string ClientID);
        DataSet getContractList(string clientID);
    }
}
