
using Nordfin.workflow.BusinessLayer;
using Nordfin.workflow.Entity;
using Nordfin.workflow.PresentationBusinessLayer;
using System;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Services;

namespace Nordfin
{
    public partial class frmClientInformation : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                lblClientName.Text = ClientSession.ClientName;


                IClientInformationPresentationBusinessLayer objClientInformationList = new ClientInformationBusinessLayer();
                Tuple<ClientInformation, ClientDetails> clientInformation = objClientInformationList.getClientInformationLogin(ClientSession.ClientID, ClientSession.UserID);
                Session["ClientInfo"] = clientInformation.Item1;

                lblRegNumber.Text = clientInformation.Item2.Orgnumber;
                lblAddress.Text = clientInformation.Item2.Clientaddress;
                lblPostalCode.Text = clientInformation.Item2.ClientPostalCode;
                lblCity.Text = clientInformation.Item2.ClientCity;
                lblCountry.Text = clientInformation.Item2.ClientLand;
                lblEMail.Text = clientInformation.Item2.ContactEmail;
            }
        }


        [WebMethod]
        public static string getClientInfo()
        {



            ClientInformation clientInformation = (ClientInformation)HttpContext.Current.Session["ClientInfo"];

            string jsonClientInfo = new JavaScriptSerializer().Serialize(clientInformation);

            string sResultList = "{\"ClientInfo\" :" + jsonClientInfo + "}";


            return sResultList;


        }
    }
}