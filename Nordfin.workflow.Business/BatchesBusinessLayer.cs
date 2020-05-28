using Nordfin.workflow.BusinessDataLayerInterface;
using Nordfin.workflow.DataAccessLayer;
using Nordfin.workflow.PresentationBusinessLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nordfin.workflow.BusinessLayer
{
    public sealed class BatchesBusinessLayer : IBatchPresentationBusinessLayer
    {
        IBatchesBusinessDataLayer objBusinessDataLayer = new BatchesDataAccessLayer();
        DataSet IBatchPresentationBusinessLayer.getInvoicesBatches(int Isdownload)
        {
            return objBusinessDataLayer.getInvoicesBatches(Isdownload);
        }

        DataSet IBatchPresentationBusinessLayer.getInvoicesBatchesReports(string ClientID)
        {
            return objBusinessDataLayer.getInvoicesBatchesReports(ClientID);
        }
    }
}
