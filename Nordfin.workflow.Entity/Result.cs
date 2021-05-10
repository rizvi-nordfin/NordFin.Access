using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nordfin.workflow.Entity
{
    public class ApiResult
    {
        public string PsInvoiceId { get; set; }
    }

    public class PSApiResponse
    {
        public ApiResult Result { get; set; }
       

    }
    public class ApiInvoiceCaseResult
    {
        public PSApiBalance Balance { get; set; }
        public PSApiStatus Status { get; set; }
        public PSApiEvent Event { get; set; }
    }
    public class PSApiInvoiceCaseResponse
    {
        public ApiInvoiceCaseResult Result { get; set; }
    }



    public class PSApiStatus
    {
      
        public string InternalStatus { get; set; }
       
    }

    public class PSApiEvent
    {
 
        public string LatestEventDate { get; set; }
    }

    public class PSApiBalance
    {
        public string DebtAmount { get; set; }
        public string RemainingAmount { get; set; }
        public string RemainingInterest { get; set; }
        public string RemainingFees { get; set; }
        public string RemainingTotal { get; set; }
      
    }

}
