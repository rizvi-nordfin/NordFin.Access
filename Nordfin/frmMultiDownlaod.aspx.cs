using Nordfin.workflow.Business;
using Nordfin.workflow.PresentationBusinessLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Nordfin
{
    public partial class frmMultiDownlaod : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string InvoiceNum = (string)Session["custNum"];
              
                if (InvoiceNum != null && ClientSession.ClientID != null)
                {
                    IInvoicesPresentationBusinessLayer objInvoicesLayer = new InvoicesBusinessLayer();
                    DataSet ds = objInvoicesLayer.getInvoicesList(InvoiceNum.Trim(), ClientSession.ClientID, true, null);
                    grdInvoiceDownlaod.DataSource = ds;
                    grdInvoiceDownlaod.DataBind();
                }
            }

        }

        protected void btnMultiDownload_Click(object sender, EventArgs e)
        {
            //List<Files> fileList = new List<Files>();
            //foreach (GridViewRow row in gvFiles.Rows)
            //{
            //    if ((row.FindControl("chkSelect") as CheckBox).Checked)
            //    {
            //        string fileName = row.Cells[1].Text.Trim();
            //        string ftp = "ftp://yourserver.com/";
            //        string ftpFolder = "Uploads/";
            //        try
            //        {
            //            WebClient request = new WebClient();
            //            string url = ftp + ftpFolder + fileName;
            //            request.Credentials = new NetworkCredential("Username", "Password");
            //            byte[] bytes = request.DownloadData(url);
            //            fileList.Add(new Files() { FileName = fileName, Bytes = bytes });
            //        }
            //        catch (WebException ex)
            //        {
            //            throw new Exception((ex.Response as FtpWebResponse).StatusDescription);
            //        }
            //    }
            //}
        }
    }
}