using ClosedXML.Excel;
using DocumentFormat.OpenXml.Spreadsheet;
using Nordfin.workflow.Business;
using Nordfin.workflow.Entity;
using Nordfin.workflow.PresentationBusinessLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;

using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;


namespace Nordfin
{
    public partial class frmInvoices : System.Web.UI.Page
    {
        private const string ASCENDING = " ASC";
        private const string DESCENDING = " DESC";

        public SortDirection GridViewSortDirection
        {
            get
            {
                if (ViewState["sortDirection"] == null)
                    ViewState["sortDirection"] = SortDirection.Ascending;

                return (SortDirection)ViewState["sortDirection"];
            }
            set { ViewState["sortDirection"] = value; }
        }
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {

                ClearSession();
                string InvoiceorCustNum = (string)Session["InvoiceorCustNum"];
                if (string.IsNullOrEmpty(InvoiceorCustNum))
                {
                    InvoiceorCustNum = "0";
                }
                AdvanceSearch advanceSearch = (AdvanceSearch)Session["AdvanceSearch"];
                IInvoicesPresentationBusinessLayer objInvoicesLayer = new InvoicesBusinessLayer();
                DataSet ds = objInvoicesLayer.getInvoicesList(InvoiceorCustNum.Trim(), ClientSession.ClientID, false, advanceSearch);
                grdInvoices.DataSource = ds;
                grdInvoices.DataBind();
                Session["InvoiceGrid"] = ds.Tables[0];
                if (advanceSearch == null)
                    grdInvoices.Columns[2].Visible = false;
                if (ds.Tables[1].Rows.Count > 0)
                {
                    DataTable dtResult = ds.Tables[1];

                    hdnClientName.Value = dtResult.Rows[0].ItemArray[0].ToString();
                    hdnFileName.Value = dtResult.Rows[0].ItemArray[1].ToString();
                    hdnArchiveLink.Value = dtResult.Rows[0].ItemArray[2].ToString();


                }
                if (ds.Tables[0].Rows.Count > 0)
                {
                    lblSumAmount.Text =string.Format("{0:#,0.00}", (ds.Tables[0].AsEnumerable().Sum(r => r.Field<decimal>("Invoiceamount"))));
                    lblFeesAmount.Text = string.Format("{0:#,0.00}", ds.Tables[0].AsEnumerable().Sum(r => r.Field<decimal>("Fees")));
                    lblTotalRemain.Text = string.Format( "{0:#,0.00}", ds.Tables[0].AsEnumerable().Sum(r =>r.Field<decimal>("TotalRemaining")));
                    lblOverPaid.Text = string.Format("{0:#,0.00}", ds.Tables[0].AsEnumerable().Sum(r => r.Field<decimal>("Overpayment")));
                    lblRemain.Text = string.Format("{0:#,0.00}", ds.Tables[0].AsEnumerable().Sum(r => r.Field<decimal>("Remainingamount")));
                }
               
             
            }

            pdfInvoices.Src = "";
            pdfRemind.Src = "";
            pdfDC.Src = "";

            ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), "CreateControl", "CreateControl();", true);
        }

        protected void gridLink_Click(object sender, EventArgs e)
        {
            Session["InvoiceNum"] = ((LinkButton)sender).Text.Trim();
            Session["custNum"] = ((LinkButton)sender).CommandArgument.Split('|')[0].Trim();
            Session["InvoiceID"] = ((LinkButton)sender).CommandArgument.Split('|')[1].Trim();
        }
        protected void gridLinkCustNum_Click(object sender, EventArgs e)
        {
            Session["custNum"] = ((LinkButton)sender).Text.Trim();
            Response.Redirect("frmCustomer.aspx");
        }

        protected void btnPDFDownload_Click(object sender, EventArgs e)
        {
            EmailFunctions emailFunctions = new EmailFunctions();
            string PDFArchive = "";
            btnDownload.CommandArgument = ((Button)sender).CommandArgument.Trim().Replace("INV-", "");
            btnEmail.Attributes["custInvoice"] = ((Button)sender).Attributes["custInvoice"].ToString();
            btnEmail.Attributes["combineInvoice"] = ((Button)sender).Attributes["combineInvoice"].ToString();
            btnEmail.Attributes["collectionStatus"] = ((Button)sender).Attributes["collectionStatus"].ToString();

            chkInvoices.Visible = emailFunctions.GetInvoiceExsits(hdnClientName.Value, hdnFileName.Value, btnDownload.CommandArgument, "", false);
            chkDC.Visible = (btnEmail.Attributes["collectionStatus"] == "DC" || btnEmail.Attributes["collectionStatus"].ToUpper() == "EXT") ? emailFunctions.GetInvoiceExsits(hdnClientName.Value, hdnFileName.Value, btnDownload.CommandArgument, "DC", true) : false;
            chkRemind.Visible = (btnEmail.Attributes["collectionStatus"] == "DC" || btnEmail.Attributes["collectionStatus"] == "REMIND" || btnEmail.Attributes["collectionStatus"].ToUpper() == "EXT") ? emailFunctions.GetInvoiceExsits(hdnClientName.Value, hdnFileName.Value, btnDownload.CommandArgument, "Rem", true) : false;
            chkInvoices.Checked = chkInvoices.Visible;
            chkDC.Checked = chkDC.Visible;
            chkRemind.Checked = chkRemind.Visible;
            if (!chkInvoices.Visible)
                PDFArchive = hdnArchiveLink.Value + btnDownload.CommandArgument;
           
            ScriptManager.RegisterStartupScript(this, this.GetType(), "ExportClick", "ExportClick(0,'" + PDFArchive + "');", true);
          
        }
        //private List<InvoiceDownload> InvoiceDownloadNew(string InvoiceNumber, string collectionStatus)
        //{
        //    List<InvoiceDownload> invoiceDownloads = new List<InvoiceDownload>();



        //    return invoiceDownloads;
        //}


        //private List<InvoiceDownload> InvoiceDownload(string InvoiceNumber,string collectionStatus)
        //{
        //    if (Session["InvoiceDownloadList"] != null)
        //    {
        //        Session["InvoiceDownloadList"] = null;
        //    }
        //    string sCollectionStatus = "";
        //    bool bDC = false;
        //    bool bRemind = false;
        //    bool bInvoices = false;
        //    bool bResult = false;
        //    bool bArchive = false;
        //    List<InvoiceDownload> invoiceDownloads = new List<InvoiceDownload>();
        //fileDownload:
        //    if (!chkInvoices.Checked && !bInvoices)
        //    {
        //        bInvoices = true;
        //        goto fileDC;
        //    }
        //    string subFolderName = hdnClientName.Value.Substring(hdnClientName.Value.LastIndexOf("/") + 1) + Utilities.Execute(InvoiceNumber);
        //    FTPFileProcess fileProcess = new FTPFileProcess();
        //    string sFileExt = System.Configuration.ConfigurationManager.AppSettings["FileExtension"].ToString();
        //    string sFileName = hdnFileName.Value + "_" + sCollectionStatus + InvoiceNumber + "_" + "inv" + "." + sFileExt;


        //    string ResultFile = "";
        //    if (sFileName != "")
        //        bResult = fileProcess.FileDownload(hdnClientName.Value, subFolderName, sFileName, out ResultFile, bArchive);
        //    bArchive = false;
        //    InvoiceDownload download = new InvoiceDownload();
        //    download.InvoiceName = sCollectionStatus;
        //    download.Status = (bResult) ? "visible" : "hidden";
        //    download.PDFArchive = hdnArchiveLink.Value + InvoiceNumber;
        //    invoiceDownloads.Add(download);
        //fileDC:
        //    if (collectionStatus != "")
        //    {
        //         if (!bRemind && (collectionStatus.ToUpper() == "DC"|| collectionStatus.ToUpper()=="REMIND"))
        //        {
        //            bRemind = true;
        //            bArchive = true;
        //            sCollectionStatus = "Rem";
        //            goto fileDownload;
        //        }
        //        else if (!bDC && collectionStatus.ToUpper() == "DC")
        //        {
        //            bDC = true;
        //            bArchive = true;
        //            sCollectionStatus = "DC";
        //            goto fileDownload;
        //        }

        //    }
        //    return invoiceDownloads;
        //}


        protected void btnDownload_Click(object sender, EventArgs e)
        {
           
            string FileStartName = hdnFileName.Value;
            string InvoiceNumber= ((Button)sender).CommandArgument.Trim();
            EmailFunctions emailFunctions = new EmailFunctions();
            if (chkInvoices.Checked && chkInvoices.Visible)
            {
                string sFileName = emailFunctions.GetFileName(FileStartName, InvoiceNumber, "",out bool bExist);
                if (bExist)
                    pdfInvoices.Src = "frmPdfMultiDownload.aspx?FileName=" + sFileName;
            }
            if (chkRemind.Checked && chkRemind.Visible)
            {
                string sFileName = emailFunctions.GetFileName(FileStartName, InvoiceNumber, "Rem", out bool bExist);
                if (bExist)
                    pdfRemind.Src = "frmPdfMultiDownload.aspx?FileName=" + sFileName;
            }
            if (chkDC.Checked && chkDC.Visible)
            {
                string sFileName = emailFunctions.GetFileName(FileStartName, InvoiceNumber, "DC", out bool bExist);
                if (bExist)
                    pdfDC.Src = "frmPdfMultiDownload.aspx?FileName=" + sFileName;
            }
            
            ScriptManager.RegisterStartupScript(this, this.GetType(), "ExportClick", "ExportClick(0);", true);
        }
      

        protected void btnEmail_Click(object sender, EventArgs e)
        {
            IInvoicesPresentationBusinessLayer objInvoicesLayer = new InvoicesBusinessLayer();
            string scustEmail = objInvoicesLayer.GetCustInvoiceEmailID(ClientSession.ClientID, btnEmail.Attributes["custInvoice"].ToString());
            txtCustEmail.Text = scustEmail;
            txtEmailHeader.Text = ClientSession.ClientName + "; Invoice" + " " + btnEmail.Attributes["combineInvoice"];
            txtEmailBody.Text = "Hei, Hi, Hej, Hallo!" + "\n\n" + "Your invoice copy has been attached" + "\n\n"+ "Have a great day :-)" +"\n\n"  + "Best Regards," + "\n" + ClientSession.ClientName;
            ScriptManager.RegisterStartupScript(this, this.GetType(), "ExportClick", "ExportClick(1);", true);
        }
   
        protected void btnSend_Click(object sender, EventArgs e)
        {
            try
            {
                EmailFunctions emailFunctions = new EmailFunctions();
                string FileStartName = hdnFileName.Value;
                string InvoiceNumber = btnEmail.Attributes["combineInvoice"];
                List<PDFMultiDownload> multiDownloads = new List<PDFMultiDownload>();
                if (chkInvoices.Checked && chkInvoices.Visible)
                {
                    string sFileName = emailFunctions.GetFileName(FileStartName, InvoiceNumber, "", out bool bExist);
                    if (bExist)
                        multiDownloads.Add(new PDFMultiDownload() { FileName = sFileName });
                }
                if (chkRemind.Checked && chkRemind.Visible)
                {
                    string sFileName = emailFunctions.GetFileName(FileStartName, InvoiceNumber, "Rem", out bool bExist);
                    if (bExist)
                        multiDownloads.Add(new PDFMultiDownload() { FileName = sFileName });
                }
                if (chkDC.Checked && chkDC.Visible)
                {
                    string sFileName = emailFunctions.GetFileName(FileStartName, InvoiceNumber, "DC", out bool bExist);
                    if (bExist)
                        multiDownloads.Add(new PDFMultiDownload() { FileName = sFileName });
                }

                bool bEmail =emailFunctions.SendMail(txtCustEmail.Text, txtEmailHeader.Text, txtEmailBody.Text, multiDownloads);

                ScriptManager.RegisterStartupScript(this, this.GetType(), "ExportClick", "ExportClick(1,'','" + bEmail + "');", true);
            }
            catch
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "ExportClick", "ExportClick(1,'','" + false + "');", true);
            }
        }
       

        protected void btnExport_Click(object sender, EventArgs e)
        {
            HttpResponse response = HttpContext.Current.Response;
            response.Clear();
            response.Charset = "";
            DataTable dataTable = (DataTable)Session["InvoiceGrid"];
            foreach (DataColumn column in dataTable.Columns)
                column.ColumnName = column.ColumnName.ToUpper();
            try

            {

                using (XLWorkbook wb = new XLWorkbook())
                {

                    var ws = wb.Worksheets.Add(dataTable);
                  
                    Response.Clear();
                    Response.Buffer = true;
                    Response.Charset = "";
                    string filename = "Invoices" + DateTime.Now.ToString("yyyy-MM-dd hh:mm tt") + ".xlsx";
                    ws.Column(1).Delete();
                    ws.Column(1).Delete();
                    ws.Column(13).Delete();
                    ws.Column(14).Delete();
                    ws.Column(13).Delete();
                    Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                    Response.AddHeader("Content-Disposition", "attachment;filename=\"" + filename + "\"");
                    using (MemoryStream MyMemoryStream = new MemoryStream())
                    {
                        wb.SaveAs(MyMemoryStream);
                        MyMemoryStream.WriteTo(Response.OutputStream);
                        Response.Flush();
                        Response.End();
                    }
                }
            }
            catch(Exception ex)
            {
                //catch the issue
            }

        }

        public static string Execute(string invNumber)
        {
            var r = Regex.Replace(invNumber, "[^0-9]", "");

            var n = int.TryParse(r, out var x) ? x : 0;

            // n is invoice number which is int or only numbers

            /* if invoice number is more than 9 digits it will keep only 9 digits from the right and remove the rest but 
             it will not change invoice number in the file name */

            if (r.Length > 9)
                n = int.TryParse(r.Remove(0, r.Length - 9), out var m) ? m : 0;
            const int i = 1000000000;
            const int n2 = 100000;
            var newPath = "";
            var index = 0;
            var n1 = 0;
            var n3 = n2;

            while (index < i)
            {
                index++;
                if (n > n1 && n <= n3)
                {
                    newPath = n1 + "_" + n3;
                    break;
                }
                n1 += n2;
                n3 += n2;
            }
            return newPath;
        }

        protected void grdInvoices_Sorting(object sender, GridViewSortEventArgs e)
        {
            string sortExpression = e.SortExpression;

            if (GridViewSortDirection == SortDirection.Ascending)
            {
                GridViewSortDirection = SortDirection.Descending;
                SortGridView(sortExpression, DESCENDING);
            }
            else
            {
                GridViewSortDirection = SortDirection.Ascending;
                SortGridView(sortExpression, ASCENDING);
            }
          
        }
        private void SortGridView(string sortExpression, string direction)
        {
           
            DataTable dt = (DataTable)Session["InvoiceGrid"];
            DataView dv = new DataView(dt);
            dv.Sort = sortExpression + direction;
            grdInvoices.DataSource = dv;
            grdInvoices.DataBind();
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