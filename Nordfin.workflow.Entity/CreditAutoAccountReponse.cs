using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Nordfin.workflow.Entity
{
	[XmlRoot(ElementName = "returndetail")]
	public class Returndetail
	{

		[XmlElement(ElementName = "timestamp")]
		public string Timestamp { get; set; }

		[XmlElement(ElementName = "token")]
		public string Token { get; set; }

		[XmlElement(ElementName = "terms")]
		public string Terms { get; set; }
	}

	[XmlRoot(ElementName = "body")]
	public class Body
	{

		[XmlElement(ElementName = "returndetail")]
		public Returndetail Returndetail { get; set; }
	}

	[XmlRoot(ElementName = "xmlresponse")]
	public class CreditAutoAccountReponse
    {
		[XmlElement(ElementName = "body")]
		public Body Body { get; set; }
		
	}
}
