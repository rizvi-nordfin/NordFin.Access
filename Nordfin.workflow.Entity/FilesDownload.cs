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
        public string ClientName { get; set; }
        public string FolderName { get; set; }
        public bool ClientArchive { get; set; }
        public string ToMail { get; set; }

        public string EmailBody { get; set; }
        public string EmailHeader { get; set; }
        public string SenderName { get; set; }
        public string Subject { get; set; }
    }
}
