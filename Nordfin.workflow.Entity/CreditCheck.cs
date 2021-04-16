using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nordfin.workflow.Entity
{
    public class CreditCheck
    {
        public string Name { get; set; }

        public string Address { get; set; }
        public string Status { get; set; }

        public string City { get; set; }
        public string  PostalCode { get; set; }

        public string Error { get; set; } = "";
        public string ErrorMessage { get; set; } = "";

        public string CreditStatus { get; set; }

        public string CreditInfo { get; set; } = "";
        public string PersonalNumber { get; set; }
        public int ClientID { get; set; }

        public int CreditScoreAccepted { get; set; }
    }
}
