using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nordfin.workflow.Entity
{
    public class ServerJob
    {
       

        public string ClientID { get; set; }
        public string JobData { get; set; }
        public string CustomerID { get; set; }
        public int UserID { get; set; }
        public string UserName { get; set; }
        public string InvoiceNumber { get; set; }
    }

}
