using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nordfin.workflow.Entity
{
    public class ClientDetails
    {
        public string Orgnumber { get; set; }
        public string Clientaddress { get; set; }
        public string ClientPostalCode { get; set; }
        public string ClientCity { get; set; }
        public string ClientLand { get; set; }
        public string ContactEmail { get; set; }
    }
}
