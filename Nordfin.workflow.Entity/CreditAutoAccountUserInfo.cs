using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Nordfin.workflow.Entity
{


	[XmlRoot(ElementName = "body")]
	public class AutoUserInfo
	{
		[XmlElement(ElementName = "username")]
		public string Username { get; set; }
	}
	[XmlRoot(ElementName = "xmlresponse")]
	public class CreditAutoAccountUserInfo
	{
		[XmlElement(ElementName = "body")]
		public AutoUserInfo Userinfo { get; set; }

	}
}
