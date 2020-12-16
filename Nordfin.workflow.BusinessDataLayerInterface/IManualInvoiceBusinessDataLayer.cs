﻿using Nordfin.workflow.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nordfin.workflow.BusinessDataLayerInterface
{
    public interface IManualInvoiceBusinessDataLayer
    {
        IList<CustomerInfo> GetCustomerInfoForClient(int clientID);

        int GetNumberSeries(string seriesName);

        void UpdateNumberSeries(string seriesName, int newSeries);
    }
}
