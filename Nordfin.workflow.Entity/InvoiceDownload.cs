using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nordfin.workflow.Entity
{
    public class InvoiceDownload
    {
        public string InvoiceName { get; set; }
        public string Status { get; set; }
        public string PDFArchive { get; set; }
    }
}
