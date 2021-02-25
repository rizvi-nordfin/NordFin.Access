namespace Nordfin.workflow.Entity
{
    public class TransformationMapping
    {
        public int Id { get; set; }

        public string ClientName { get; set; }

        public string SectionName { get; set; }

        public string InputTag { get; set; }

        public string OutputTag { get; set; }

        public int MappingId { get; set; }

        public int RowNumber { get; set; }

        public string AdditionalText { get; set; }

        public string ManualInvoiceTag { get; set; }
    }
}
