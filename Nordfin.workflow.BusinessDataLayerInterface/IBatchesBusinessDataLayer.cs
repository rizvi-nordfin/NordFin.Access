using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nordfin.workflow.BusinessDataLayerInterface
{
    public interface IBatchesBusinessDataLayer
    {
        DataSet getInvoicesBatches(int Isdownload);
        DataSet getInvoicesBatchesReports(string ClientID);
    }
}
