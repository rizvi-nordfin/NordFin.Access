using Nordfin.workflow.Business;
using Nordfin.workflow.Entity;
using Nordfin.workflow.PresentationBusinessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Nordfin
{
    public partial class NordfinCredit : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                lblUserName.Text = ClientSession.LabelUser;

                IUserPresentationBusinessLayer objUserLayer = new UserBusinessLayer();
                string BatchValues = "";
                int Contracts = 0;
                IList<ClientList> objClientList = objUserLayer.GetClientList(Convert.ToInt32(ClientSession.Admin), Convert.ToInt32(ClientSession.ClientID), ClientSession.UserID, out BatchValues, out Contracts);
                if (objClientList.Count > 0 && objClientList.Any(a => a.ClientID == Convert.ToInt32(ClientSession.ClientID)))
                    txtClientName.Text = objClientList.Where(a => a.ClientID == Convert.ToInt32(ClientSession.ClientID)).ToList()[0].ClientName;
            }
            ScriptManager.RegisterStartupScript(this, this.GetType(), "CallAlert", "save();", true);
        }

        protected void btnlogout_Click(object sender, EventArgs e)
        {
            IUserPresentationBusinessLayer objUserLayer = new UserBusinessLayer();
            int iResult = objUserLayer.UpdateSessionID(ClientSession.UserName.Trim(), System.DateTimeOffset.Now.ToUnixTimeSeconds());
            Session.Abandon();
            Response.Redirect("frmLogin.aspx");
        }
    }
}