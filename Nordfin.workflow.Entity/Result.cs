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
}
