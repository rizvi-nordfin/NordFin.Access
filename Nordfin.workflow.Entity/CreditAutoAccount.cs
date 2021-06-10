using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nordfin.workflow.Entity
{
    [Table("CreditAutoAccount")]
    public class CreditAutoAccount
    {
        [Key]
        public int ID { get; set; }
        public int ClientID { get; set; }
        public int UserID { get; set; }
        public string OrgNumber { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime ConditionTimeStamp { get; set; }
        public string CreditUserName { get; set; }
        public string CreditPassword { get; set; }
        
     
    }
}
