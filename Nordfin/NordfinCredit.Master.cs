using Nordfin.workflow.Business;
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