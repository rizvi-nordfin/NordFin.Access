using Nordfin.workflow.BusinessLayer;
using Nordfin.workflow.PresentationBusinessLayer;
using System;
using System.Data;
using System.IO;

namespace Nordfin
{
    public partial class frmUserLoginInformation : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            ClearSession();
            if (!IsPostBack)
            {
                IUserLoginInformationPresentationBusinessLayer objLoginInformLayer = new UserLoginInformationBusinessLayer();
                DataSet ds = objLoginInformLayer.GetUserLoginInformation(ClientSession.UserName, System.DateTimeOffset.Now.ToUnixTimeSeconds());
                grdUserLoginInformation.DataSource = ds.Tables[0];
                grdUserLoginInformation.DataBind();

                grdActiveUser.DataSource = ds.Tables[1];
                grdActiveUser.DataBind();
            }
        }

        public void ClearSession()
        {
            if (ClientSession.ClientID.Trim() == "")
            {
                try
                {

                    string sDirectory = "~/Documents/" + Session.SessionID;
                    if (Directory.Exists(Server.MapPath(sDirectory)))
                    {
                        Directory.Delete(Server.MapPath(sDirectory), true);
                    }
                }
                catch
                {
                    //catch the issue
                }
                Session.Abandon();
                Response.Redirect("frmLogin.aspx");

            }
        }
    }
}