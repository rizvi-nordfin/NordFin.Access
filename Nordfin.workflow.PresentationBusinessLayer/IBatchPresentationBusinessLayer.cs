using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nordfin.workflow.PresentationBusinessLayer
{
    public interface IBatchPresentationBusinessLayer
    {
        DataSet getInvoicesBatches(int Isdownload);
        DataSet getInvoicesBatchesReports(string ClientID);
    }
}
