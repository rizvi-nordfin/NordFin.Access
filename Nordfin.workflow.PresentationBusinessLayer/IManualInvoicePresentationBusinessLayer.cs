﻿using Nordfin.workflow.Entity;

namespace Nordfin.workflow.PresentationBusinessLayer
{
    public interface IManualInvoicePresentationBusinessLayer
    {
        CustomerInfo GetCustomerInfoForClient(string customerNumber, int clientID);

        int GetAndUpdateNumberSeries(string seriesName);

        void UpdateNumberSeries(string seriesName, int newSeries);

        bool ImportManualInvoice(string standardXml);
    }
}
