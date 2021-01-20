namespace Nordfin.workflow.Entity
{
    using System.IO;
    using System.Text;

    public class UtfStringWriter : StringWriter
    {
        private Encoding desiredEncoding;
        public UtfStringWriter(Encoding encoding)
        {
            desiredEncoding = encoding;
        }

        public override Encoding Encoding => desiredEncoding;
    }
}
