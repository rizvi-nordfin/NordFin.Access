
namespace Nordfin.workflow.Entity
{
    public class ClientList
    {

        public int ClientID { get; set; }
        public string ClientName { get; set; }

        public string ClientLand { get; set; }

        public int AllowManualInvoice { get; set; }

        public bool Archive { get; set; }
    }
}
