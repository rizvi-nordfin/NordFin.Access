

namespace Nordfin.workflow.Entity
{
    public class Notes
    {
        public string CustomerID { get; set; }
        public int UserID { get; set; }
        public string NoteType { get; set; }
        public int ClientID { get; set; }
        public string UserName { get; set; }
        public string InvoiceNumber { get; set; }
        public string DueDateNewValue { get; set; }
        public string DueDateOldValue { get; set; }
        public string CollectionStopUntilNewValue { get; set; }
        public string CollectionStopUntilOldValue { get; set; }
        public string CollectionStopNewValue { get; set; }
        public string CollectionStopOldValue { get; set; }
        public string NoteText { get; set; }
        public int InvoiceID { get; set; }
        public string NoteDate { get; set; }
        public string Contested { get; set; }
        public string ContestedDate { get; set; }

    }
}
