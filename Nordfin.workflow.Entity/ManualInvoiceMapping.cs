namespace Nordfin.workflow.Entity
{
    public class ManualInvoiceMapping
    {
        public int Id { get; set; }

        public string SectionName { get; set; }

        public string MappingValue { get; set; }

        public string OutputValue { get; set; }

        public int MappingId { get; set; }

        public int RowNumber { get; set; }

        public string AdditionalText { get; set; }
    }
}
