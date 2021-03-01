using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nordfin.workflow.Entity
{
    public class FilesDownload
    {
        public string FileName { get; set; }
        public byte[] Bytes { get; set; }
    }
}
