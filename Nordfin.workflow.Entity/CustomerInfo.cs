using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nordfin.workflow.Entity
{
    public class CustomerInfo
    {
        public string Name { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string PostalCode { get; set; }

        public string City { get; set; }

        public string Country { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public int CustomerID { get; set; }
        public string CustomerNumber { get; set; }
        public string Comments { get; set; }

        public int UserID { get; set; }


    }
}
