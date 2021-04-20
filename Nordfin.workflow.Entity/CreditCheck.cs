using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Nordfin.workflow.Entity
{
    public class CreditCheck
    {
        public string Name { get; set; }

        public string Address { get; set; }
        public string Status { get; set; }

        public string City { get; set; }
        public string  PostalCode { get; set; }

        public string Error { get; set; } = "";
        public string ErrorMessage { get; set; } = "";

        public string CreditStatus { get; set; }

        public string CreditInfo { get; set; } = "";
        public string PersonalNumber { get; set; }
        public int ClientID { get; set; }

        public int CreditScoreAccepted { get; set; }
    }

	// using System.Xml.Serialization;
	// XmlSerializer serializer = new XmlSerializer(typeof(NewDataSet));
	// using (StringReader reader = new StringReader(xml))
	// {
	//    var test = (NewDataSet)serializer.Deserialize(reader);
	// }

	[XmlRoot(ElementName = "GETDATA_RESPONSE")]
	public class GETDATARESPONSE
	{

		[XmlElement(ElementName = "ORGNR")]
		public double ORGNR { get; set; }

		[XmlElement(ElementName = "NAME")]
		public string NAME { get; set; }

		[XmlElement(ElementName = "ADDRESS")]
		public string ADDRESS { get; set; }
		[XmlElement(ElementName = "ZIPCODE")]
		public int ZIPCODE { get; set; }

		[XmlElement(ElementName = "TOWN")]
		public string TOWN { get; set; }

		[XmlElement(ElementName = "REGION")]
		public string REGION { get; set; }

		[XmlElement(ElementName = "EMAIL_ADRESS")]
		public object EMAILADRESS { get; set; }

		[XmlElement(ElementName = "RATING")]
		public int RATING { get; set; }

		[XmlElement(ElementName = "RATING_TEXT")]
		public string RATINGTEXT { get; set; }

		[XmlElement(ElementName = "RISK_PROGNOSIS")]
		public double RISKPROGNOSIS { get; set; }

		[XmlElement(ElementName = "RATING_HISTORY")]
		public int RATINGHISTORY { get; set; }

		
	}
















	[XmlRoot(ElementName = "NewDataSet")]
	public class NewDataSet
	{

		[XmlElement(ElementName = "GETDATA_RESPONSE")]
		public GETDATARESPONSE GETDATARESPONSE { get; set; }

		

		
	}


}
