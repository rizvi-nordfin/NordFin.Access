using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Nordfin
{
    public partial class frmPdfMultiDownload : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string fileName = Request.QueryString["FileName"];
                Response.ContentType = "application/pdf";
                Response.AppendHeader("Content-Disposition", "attachment;filename=" + fileName);
                Response.TransmitFile(Server.MapPath("~/Documents/" + Session.SessionID + "/" + fileName));
                Response.End();
            }
        }
    }
}