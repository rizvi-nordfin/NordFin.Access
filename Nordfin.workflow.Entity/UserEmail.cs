using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nordfin.workflow.Entity
{
    public class UserEmail
    {
        public string Email { get; set; }
        public int EmailConfirm { get; set; }
        public string token { get; set; }
    }
}
