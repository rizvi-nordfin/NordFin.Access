using Ionic.Zip;
using Nordfin.workflow.Business;
using Nordfin.workflow.Entity;
using Nordfin.workflow.PresentationBusinessLayer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;

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


                    if (ds.Tables.Count >= 3)
                    {
                        DataTable dtResult1 = ds.Tables[2];
                        if (dtResult1.Rows.Count > 0)
                        {
                            hdnClientName.Value = dtResult1.Rows[0].ItemArray[0].ToString();
                            hdnFileName.Value = dtResult1.Rows[0].ItemArray[1].ToString();
                          
                        }


                    }
                }
            }

            pdfExport.Src = "";
            pdfExportDetail.Src = "";
            pdfZip.Src = "";

        }



        void backgroundTASK()
        {
            string InvoiceNum = (string)Session["custNum"];
            List<FilesDownload> fileList = (List<FilesDownload>)Session["MailList"];
            if (fileList.Count > 0)
            {
                try
                {

                    FTPFileProcess fileProcess = new FTPFileProcess();





                    string sEmail = System.Configuration.ConfigurationManager.AppSettings["Email"].ToString();
                    string sEmailPassword = System.Configuration.ConfigurationManager.AppSettings["EmailPassword"].ToString();
                    string sEmailPort = System.Configuration.ConfigurationManager.AppSettings["EmailPort"].ToString();
                    using (SmtpClient SmtpServer = new SmtpClient("in-v3.mailjet.com", 587))
                    {
                        SmtpServer.Credentials = new NetworkCredential("1867ce5eecca8ce8ab72cda1fefe8d47", "3e70a81b01b2926189dd8875294b3c13");

                        using (System.Net.Mail.MailMessage mail = new System.Net.Mail.MailMessage())
                        {
                            mail.From = new MailAddress(sEmail, fileList[0].SenderName);

                            mail.To.Add(fileList[0].ToMail);

                            mail.Subject = fileList[0].EmailHeader;

                            mail.Body = fileList[0].EmailBody.Replace("\n", "<br>");
                            mail.IsBodyHtml = true;
                            for (int i = 0; i < fileList.Count; i++)
                            {
                                byte[] bytes = fileProcess.FileDownload(fileList[i].ClientName, fileList[i].FolderName, out string ResultFileName, fileList[i].ClientArchive);
                                fileList[i].Bytes = bytes;

                            }
                            using (MemoryStream memoryStream = new MemoryStream())
                            {
                                using (ZipFile zip = new ZipFile())
                                {
                                    zip.AlternateEncodingUsage = ZipOption.AsNecessary;
                                    zip.UseZip64WhenSaving = Zip64Option.AsNecessary;// ZipOption.AsNecessary;


                                    foreach (FilesDownload file in fileList)
                                    {
                                        if (file.Bytes.Length > 64)
                                            zip.AddEntry(file.FileName, file.Bytes);
                                    }
                                    zip.Save(memoryStream);
                                }

                                MemoryStream attachmentStream = new MemoryStream(memoryStream.ToArray());

                                Attachment attachment = new Attachment(attachmentStream, "Invoices" + ".zip", MediaTypeNames.Application.Zip);
                                mail.Attachments.Add(attachment);
                            }
                            SmtpServer.EnableSsl = true;
                            SmtpServer.Send(mail);
                        }

                        IInvoicesPresentationBusinessLayer objInvoicesLayer = new InvoicesBusinessLayer();
                        AccessLog accessLog = new AccessLog();
                        accessLog.ClientID = fileList[0].ClientID;
                        accessLog.CustomerNumber = fileList[0].CustomerNumber;
                        accessLog.UserID = fileList[0].UserID;
                        accessLog.Comments = "<EMailDetails>" + string.Join(",", fileList.Select(a => a.InvoiceNumber.ToString())) + "</EMailDetails>" + "<Status>Success</Status>";


                        int scustEmail = objInvoicesLayer.setEmailSentAccessLog(accessLog);
                    }
                }
                catch
                {
                    IInvoicesPresentationBusinessLayer objInvoicesLayer = new InvoicesBusinessLayer();
                    AccessLog accessLog = new AccessLog();
                    accessLog.ClientID = fileList[0].ClientID;
                    accessLog.CustomerNumber = fileList[0].CustomerNumber;
                    accessLog.UserID = fileList[0].UserID;
                    accessLog.Comments = "<EMailDetails>" + string.Join(",", fileList.Select(a => a.InvoiceNumber.ToString())) + "</EMailDetails>" + "<Status>Failure</Status>";


                    int scustEmail = objInvoicesLayer.setEmailSentAccessLog(accessLog);
                }
            }
                //ClientScript.RegisterStartupScript(Page.GetType(),"alert", "<script language=javascript>alert('test');</script>");




            
        }
        protected void btnMultiDownload_Click(object sender, EventArgs e)
        {
           
            string InvoiceNum = (string)Session["custNum"];
            List<FilesDownload> fileList = GetFileBytes();
            Session["FileList"] = fileList;
            long totalBytes = fileList.Select(a => a.Bytes.Length).Sum();

            //if (ConvertBytesToMegabytes(totalBytes) > 0)
            //{
            //    ScriptManager.RegisterStartupScript(this, this.GetType(), "Downloadlimit", "Downloadlimit();", true);
            //}
            if (chkExport.Checked)
                pdfExport.Src = "frmPdfMultiDownload.aspx?FileName=" + "Export";
            if (chkExportDetail.Checked )
                pdfExportDetail.Src = "frmPdfMultiDownload.aspx?FileName=" + "ExportDetail";
            if (fileList.Count > 0)
                pdfZip.Src = "frmPdfMultiDownload.aspx?FileName=" + "Zip";

          
           

        }

        static double ConvertBytesToMegabytes(long bytes)
        {
            return (bytes / 1024f) / 1024f;
        }


        protected void chkSelectAll_CheckedChanged(object sender, EventArgs e)
        {
            foreach (GridViewRow row in grdInvoiceDownlaod.Rows)
            {
                (row.FindControl("chkMultiInvoices") as CheckBox).Checked = chkSelectAll.Checked ? true : false;

                //(row.FindControl("chkMultiDC") as CheckBox).Checked = chkSelectAll.Checked ? true : false;

                //(row.FindControl("chkMultiRemind") as CheckBox).Checked = chkSelectAll.Checked ? true : false;
            }
        }

        protected void btnMultiMail_Click(object sender, EventArgs e)
        {
            string InvoiceNum = (string)Session["custNum"];
            if (InvoiceNum != null)
            {
                IInvoicesPresentationBusinessLayer objInvoicesLayer = new InvoicesBusinessLayer();
                string scustEmail = objInvoicesLayer.GetCustInvoiceEmailID(ClientSession.ClientID, (string)Session["custNum"]);
                txtCustEmail.Text = scustEmail;
                txtEmailHeader.Text = ClientSession.ClientName + "; Invoice" + " " + (string)Session["custNum"];
                txtEmailBody.Text = "Hei, Hi, Hej, Hallo!" + "\n\n" + "Your invoice copy has been attached" + "\n\n" + "Have a great day :-)" + "\n\n" + "Best Regards," + "\n" + ClientSession.ClientName;
                ScriptManager.RegisterStartupScript(this, this.GetType(), "ExportClick", "ExportClick();", true);
            }
          
        }


        public List<FilesDownload> GetFileBytes(bool  IsMail=false)
        {
            string InvoiceNum = (string)Session["custNum"];
            List<FilesDownload> fileList = new List<FilesDownload>();
            FTPFileProcess fileProcess = new FTPFileProcess();
            string sFileExt = System.Configuration.ConfigurationManager.AppSettings["FileExtension"].ToString();


            int i = 0;
            foreach (GridViewRow row in grdInvoiceDownlaod.Rows)
            {
                if (IsMail)
                {
                   
                    if ((row.FindControl("chkMultiInvoices") as CheckBox).Checked)
                    {
                        LinkButton linkButton = (row.FindControl("gridLink") as LinkButton);
                        string subFolderName = hdnClientName.Value.Substring(hdnClientName.Value.LastIndexOf("/") + 1) + Utilities.Execute(linkButton.CommandArgument);
                        string sFileName = hdnFileName.Value + "_" + linkButton.CommandArgument + "_" + "inv" + "." + sFileExt;
                        string fileNameDownload = hdnFileName.Value + "_" + linkButton.Text + "_" + DateTime.Now.ToString("hhmmss")+i.ToString() + "_" + "inv" + "." + sFileExt;
                        fileList.Add(new FilesDownload() { ClientArchive=ClientSession.ClientArchive,EmailBody=txtEmailBody.Text,EmailHeader=txtEmailHeader.Text
                                                          ,ClientName= hdnClientName.Value,FolderName= subFolderName + "/" + sFileName,ToMail=txtCustEmail.Text,
                                                          FileName= fileNameDownload,SenderName = ClientSession.ClientName,InvoiceNumber=linkButton.Text
                           
                        });
                    }
                    if ((row.FindControl("chkMultiRemind") as CheckBox).Checked && (row.FindControl("chkMultiRemind") as CheckBox).Visible)
                    {
                        LinkButton linkButton = (row.FindControl("gridLink") as LinkButton);
                        string subFolderName = hdnClientName.Value.Substring(hdnClientName.Value.LastIndexOf("/") + 1) + Utilities.Execute(linkButton.CommandArgument);
                        string sFileName = hdnFileName.Value + "_" + "Rem" + linkButton.CommandArgument + "_" + "inv" + "." + sFileExt;
                        string fileNameDownload = hdnFileName.Value + "_" + "Rem" + linkButton.Text + "_" + DateTime.Now.ToString("hhmmss") + i.ToString() + "_" + "inv" + "." + sFileExt;
                        fileList.Add(new FilesDownload()
                        {
                            ClientArchive = ClientSession.ClientArchive,
                            EmailBody = txtEmailBody.Text,
                            EmailHeader = txtEmailHeader.Text
                                                          ,
                            ClientName = hdnClientName.Value,
                            FolderName = subFolderName + "/" + sFileName,
                            ToMail = txtCustEmail.Text,
                            FileName = fileNameDownload,
                            SenderName= ClientSession.ClientName,
                            InvoiceNumber = linkButton.Text
                        });
                    }
                    if ((row.FindControl("chkMultiDC") as CheckBox).Checked && (row.FindControl("chkMultiRemind") as CheckBox).Visible)
                    {
                        LinkButton linkButton = (row.FindControl("gridLink") as LinkButton);
                        string subFolderName = hdnClientName.Value.Substring(hdnClientName.Value.LastIndexOf("/") + 1) + Utilities.Execute(linkButton.CommandArgument);
                        string sFileName = hdnFileName.Value + "_" + "DC" + linkButton.CommandArgument + "_" + "inv" + "." + sFileExt;
                        string fileNameDownload = hdnFileName.Value + "_" + "DC" + linkButton.Text + "_" + DateTime.Now.ToString("hhmmss") + i.ToString() + "_" + "inv" + "." + sFileExt;
                        fileList.Add(new FilesDownload()
                        {
                            ClientArchive = ClientSession.ClientArchive,
                            EmailBody = txtEmailBody.Text,
                            EmailHeader = txtEmailHeader.Text
                                                          ,
                            ClientName = hdnClientName.Value,
                            FolderName = subFolderName + "/" + sFileName,
                            ToMail = txtCustEmail.Text,
                            FileName = fileNameDownload,
                            SenderName = ClientSession.ClientName,
                            InvoiceNumber = linkButton.Text
                        });
                    }
                    

                }
                else
                {
                    if ((row.FindControl("chkMultiInvoices") as CheckBox).Checked)
                    {
                        LinkButton linkButton = (row.FindControl("gridLink") as LinkButton);
                        string subFolderName = hdnClientName.Value.Substring(hdnClientName.Value.LastIndexOf("/") + 1) + Utilities.Execute(linkButton.CommandArgument);
                        string sFileName = hdnFileName.Value + "_" + linkButton.CommandArgument + "_" + "inv" + "." + sFileExt;
                        byte[] bytes = fileProcess.FileDownload(hdnClientName.Value, subFolderName + "/" + sFileName, out string ResultFileName, ClientSession.ClientArchive);
                        string fileNameDownload = hdnFileName.Value + "_" + linkButton.Text + "_" + DateTime.Now.ToString("hhmmss") + i.ToString() + "_" + "inv" + "." + sFileExt;
                        if (bytes.Length > 64)
                            fileList.Add(new FilesDownload() { FileName = fileNameDownload, Bytes = bytes });
                    }
                    if ((row.FindControl("chkMultiRemind") as CheckBox).Checked && (row.FindControl("chkMultiRemind") as CheckBox).Visible)
                    {
                        LinkButton linkButton = (row.FindControl("gridLink") as LinkButton);
                        string subFolderName = hdnClientName.Value.Substring(hdnClientName.Value.LastIndexOf("/") + 1) + Utilities.Execute(linkButton.CommandArgument);
                        string sFileName = hdnFileName.Value + "_" + "Rem" + linkButton.CommandArgument + "_" + "inv" + "." + sFileExt;
                        byte[] bytes = fileProcess.FileDownload(hdnClientName.Value, subFolderName + "/" + sFileName, out string ResultFileName, true);
                        string fileNameDownload = hdnFileName.Value + "_" + "Rem" + linkButton.Text + "_" + DateTime.Now.ToString("hhmmss") + i.ToString() + "_" + "inv" + "." + sFileExt;
                        if (bytes.Length > 64)
                            fileList.Add(new FilesDownload() { FileName = fileNameDownload, Bytes = bytes });
                    }
                    if ((row.FindControl("chkMultiDC") as CheckBox).Checked && (row.FindControl("chkMultiRemind") as CheckBox).Visible)
                    {
                        LinkButton linkButton = (row.FindControl("gridLink") as LinkButton);
                        string subFolderName = hdnClientName.Value.Substring(hdnClientName.Value.LastIndexOf("/") + 1) + Utilities.Execute(linkButton.CommandArgument);
                        string sFileName = hdnFileName.Value + "_" + "DC" + linkButton.CommandArgument + "_" + "inv" + "." + sFileExt;
                        byte[] bytes = fileProcess.FileDownload(hdnClientName.Value, subFolderName + "/" + sFileName, out string ResultFileName, true);
                        string fileNameDownload = hdnFileName.Value + "_" + "DC" + linkButton.Text + "_" + DateTime.Now.ToString("hhmmss") + i.ToString() + "_" + "inv" + "." + sFileExt;
                        if (bytes.Length > 64)
                            fileList.Add(new FilesDownload() { FileName = fileNameDownload, Bytes = bytes });
                    }
                }
                i++;

            }

            if(fileList.Count>0)
            {
                fileList[0].ClientID =Convert.ToInt32(ClientSession.ClientID);
                fileList[0].CustomerNumber = InvoiceNum;
                fileList[0].UserID = Convert.ToInt32(ClientSession.UserID);

            
            }
            return fileList;
        }

      
        protected void btnSend_Click(object sender, EventArgs e)
        {
            try
            {
                Session["MailList"] = null;
                List<FilesDownload> fileList = GetFileBytes(true);

                Session["MailList"] = fileList;

                System.Threading.Thread  thread = new System.Threading.Thread(backgroundTASK);
                thread.IsBackground = true;
                thread.Name = "EMailBackGround";
                thread.Start();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "MailClose", "MailClose();", true);

                //if (fileList.Count > 0)
                //{
                //    string sEmail = System.Configuration.ConfigurationManager.AppSettings["Email"].ToString();
                //    string sEmailPassword = System.Configuration.ConfigurationManager.AppSettings["EmailPassword"].ToString();
                //    string sEmailPort = System.Configuration.ConfigurationManager.AppSettings["EmailPort"].ToString();
                //    using (SmtpClient SmtpServer = new SmtpClient("in-v3.mailjet.com", 587))
                //    {
                //        SmtpServer.Credentials = new NetworkCredential("1867ce5eecca8ce8ab72cda1fefe8d47", "3e70a81b01b2926189dd8875294b3c13");

                //        using (System.Net.Mail.MailMessage mail = new System.Net.Mail.MailMessage())
                //        {
                //            string sBody = "";

                //            sBody = "<!doctype html> <html xmlns='http://www.w3.org/1999/xhtml' xmlns:v='urn:schemas-microsoft-com:vml' xmlns:o='urn:schemas-microsoft-com:office:office'> <head> <!-- NAME: 1 COLUMN --> <!--[if gte mso 15]> <xml> <o:OfficeDocumentSettings> <o:AllowPNG/> <o:PixelsPerInch>96</o:PixelsPerInch> </o:OfficeDocumentSettings> </xml> <![endif]--> <meta charset='UTF-8'> <meta http-equiv='X-UA-Compatible' content='IE=edge'> <meta name='viewport' content='width=device-width, initial-scale=1'> <title>Nordfin</title><style type='text/css'> 		p{ 			margin:10px 0; 			padding:0; 		} 		table{ 			border-collapse:collapse;     background: #323e53;		} 		h1,h2,h3,h4,h5,h6{ 			display:block; 			margin:0; 			padding:0; 		} 		img,a img{ 			border:0; 			height:auto; 			outline:none; 			text-decoration:none; 		} 		body,#bodyTable,#bodyCell{ 			height:100%; 			margin:0; 			padding:0; 			width:100%; 		} 		#outlook a{ 			padding:0; 		} 		img{ 			-ms-interpolation-mode:bicubic; 		} 		table{ 			mso-table-lspace:0pt; 			mso-table-rspace:0pt; 		} 		.ReadMsgBody{ 			width:100%; 		} 		p,a,li,td,blockquote{ 			mso-line-height-rule:exactly; 		} 		a[href^=tel],a[href^=sms]{ 			color:inherit; 			cursor:default; 			text-decoration:none; 		} 		p,a,li,td,body,table,blockquote{ 			-ms-text-size-adjust:100%; 			-webkit-text-size-adjust:100%; 		} 		a[x-apple-data-detectors]{ 			color:inherit !important; 			text-decoration:none !important; 			font-size:inherit !important; 			font-family:inherit !important; 			font-weight:inherit !important; 			line-height:inherit !important; 		} 		#bodyCell{ 			padding:10px; 		} 		.templateContainer{ 			max-width:600px !important; 		} 		a.mcnButton{ 			display:block; 		} 		.mcnImage,.mcnRetinaImage{ 			vertical-align:bottom; 		} 		.mcnTextContent{ 			word-break:break-word; 		} 		.mcnTextContent img{ 			height:auto !important; 		} 		.mcnDividerBlock{ 			table-layout:fixed !important; 		} 		body,#bodyTable{ 			background-color:#c0c0c0; 		} 		#bodyCell{ 			border-top:0; 		} 		.templateContainer{ 			border:0; 		} 		h1{ 			color:#202020; 			font-family:Helvetica; 			font-size:26px; 			font-style:normal; 			font-weight:bold; 			line-height:125%; 			letter-spacing:normal; 			text-align:left; 		} 		h2{ 			color:#202020; 			font-family:Helvetica; 			font-size:22px; 			font-style:normal; 			font-weight:bold; 			line-height:125%; 			letter-spacing:normal; 			text-align:left; 		} 		h3{ 			color:#202020; 			font-family:Helvetica; 			font-size:20px; 			font-style:normal; 			font-weight:bold; 			line-height:125%; 			letter-spacing:normal; 			text-align:left; 		} 		h4{ 			color:#202020; 			font-family:Helvetica; 			font-size:18px; 			font-style:normal; 			font-weight:bold; 			line-height:125%; 			letter-spacing:normal; 			text-align:left; 		} 		#templatePreheader{ 			background-color:#transparent; 			background-image:none; 			background-repeat:no-repeat; 			background-position:center; 			background-size:cover; 			border-top:0; 			border-bottom:0; 			padding-top:0px; 			padding-bottom:0px; 		} 		#templatePreheader .mcnTextContent,#templatePreheader .mcnTextContent p{ 			color:#656565; 			font-family:Helvetica; 			font-size:12px; 			line-height:150%; 			text-align:left; 		} 		#templatePreheader .mcnTextContent a,#templatePreheader .mcnTextContent p a{ 			color:#656565; 			font-weight:normal; 			text-decoration:underline; 		} 		#templateHeader{ 			background-color:#293746; 			background-image:none; 			background-repeat:no-repeat; 			background-position:center; 			background-size:cover; 			border-top:0; 			border-bottom:0; 			padding-top:20px; 			padding-bottom:20px; 		} 		#templateHeader .mcnTextContent,#templateHeader .mcnTextContent p{ 			color:#ffffff; 			font-family:Helvetica; 			font-size:16px; 			line-height:150%; 			text-align:left; 		} 		#templateHeader .mcnTextContent a,#templateHeader .mcnTextContent p a{ 			color:#ffffff; 			font-weight:normal; 			text-decoration:underline; 		} 		#templateBody{ 			background-color:#3e4b64; 			background-image:none; 			background-repeat:no-repeat; 			background-position:center; 			background-size:cover; 			border-top:0; 			border-bottom:2px solid #51627c; 			padding-top:10px; 			padding-bottom:10px; 		} 		#templateBody .mcnTextContent,#templateBody .mcnTextContent p{ 			color:#ffffff; 			font-family:Helvetica, Arial, Verdana, sans-serif; 			font-size:16px; 			line-height:150%; 			text-align:left; 		} 		#templateBody .mcnTextContent a,#templateBody .mcnTextContent p a{ 			color:#ffffff; 			font-weight:normal; 			text-decoration:underline; 		} 		#templateFooter{ 			background-color:#2c3850; 			background-image:none; 			background-repeat:no-repeat; 			background-position:center; 			background-size:cover; 			border-top:0; 			border-bottom:0; 			padding-top:9px; 			padding-bottom:9px; 		} 		#templateFooter .mcnTextContent,#templateFooter .mcnTextContent p{ 			color:#c5c5c5; 			font-family:Helvetica; 			font-size:12px; 			line-height:150%; 			text-align:center; 		} 		#templateFooter .mcnTextContent a,#templateFooter .mcnTextContent p a{ 			color:#ffffff; 			font-weight:normal; 			text-decoration:underline; 		} 	@media only screen and (min-width:768px){ 		.templateContainer{ 			width:600px !important; 		} }	@media only screen and (max-width: 480px){ 		body,table,td,p,a,li,blockquote{ 			-webkit-text-size-adjust:none !important; 		} }	@media only screen and (max-width: 480px){ 		body{ 			width:100% !important; 			min-width:100% !important; 		} }	@media only screen and (max-width: 480px){ 		#bodyCell{ 			padding-top:10px !important; 		} }	@media only screen and (max-width: 480px){ 		.mcnRetinaImage{ 			max-width:100% !important; 		} }	@media only screen and (max-width: 480px){ 		.mcnImage{ 			width:100% !important; 		} }	@media only screen and (max-width: 480px){ 		.mcnCartContainer,.mcnCaptionTopContent,.mcnRecContentContainer,.mcnCaptionBottomContent,.mcnTextContentContainer,.mcnBoxedTextContentContainer,.mcnImageGroupContentContainer,.mcnCaptionLeftTextContentContainer,.mcnCaptionRightTextContentContainer,.mcnCaptionLeftImageContentContainer,.mcnCaptionRightImageContentContainer,.mcnImageCardLeftTextContentContainer,.mcnImageCardRightTextContentContainer,.mcnImageCardLeftImageContentContainer,.mcnImageCardRightImageContentContainer{ 			max-width:100% !important; 			width:100% !important; 		} }	@media only screen and (max-width: 480px){ 		.mcnBoxedTextContentContainer{ 			min-width:100% !important; 		} }	@media only screen and (max-width: 480px){ 		.mcnImageGroupContent{ 			padding:9px !important; 		} }	@media only screen and (max-width: 480px){ 		.mcnCaptionLeftContentOuter .mcnTextContent,.mcnCaptionRightContentOuter .mcnTextContent{ 			padding-top:9px !important; 		} }	@media only screen and (max-width: 480px){ 		.mcnImageCardTopImageContent,.mcnCaptionBottomContent:last-child .mcnCaptionBottomImageContent,.mcnCaptionBlockInner .mcnCaptionTopContent:last-child .mcnTextContent{ 			padding-top:18px !important; 		} }	@media only screen and (max-width: 480px){ 		.mcnImageCardBottomImageContent{ 			padding-bottom:9px !important; 		} }	@media only screen and (max-width: 480px){ 		.mcnImageGroupBlockInner{ 			padding-top:0 !important; 			padding-bottom:0 !important; 		} }	@media only screen and (max-width: 480px){ 		.mcnImageGroupBlockOuter{ 			padding-top:9px !important; 			padding-bottom:9px !important; 		} }	@media only screen and (max-width: 480px){ 		.mcnTextContent,.mcnBoxedTextContentColumn{ 			padding-right:18px !important; 			padding-left:18px !important; 		} }	@media only screen and (max-width: 480px){ 		.mcnImageCardLeftImageContent,.mcnImageCardRightImageContent{ 			padding-right:18px !important; 			padding-bottom:0 !important; 			padding-left:18px !important; 		} }	@media only screen and (max-width: 480px){ 		.mcpreview-image-uploader{ 			display:none !important; 			width:100% !important; 		} }	@media only screen and (max-width: 480px){ 		h1{ 			font-size:22px !important; 			line-height:125% !important; 		} }	@media only screen and (max-width: 480px){ 		h2{ 			font-size:20px !important; 			line-height:125% !important; 		} }	@media only screen and (max-width: 480px){ 		h3{ 			font-size:18px !important; 			line-height:125% !important; 		} }	@media only screen and (max-width: 480px){ 		h4{ 			font-size:16px !important; 			line-height:150% !important; 		} }	@media only screen and (max-width: 480px){ 		.mcnBoxedTextContentContainer .mcnTextContent,.mcnBoxedTextContentContainer .mcnTextContent p{ 			font-size:14px !important; 			line-height:150% !important; 		} }	@media only screen and (max-width: 480px){ 		#templatePreheader{ 			display:block !important; 		} }	@media only screen and (max-width: 480px){ 		#templatePreheader .mcnTextContent,#templatePreheader .mcnTextContent p{ 			font-size:14px !important; 			line-height:150% !important; 		} }	@media only screen and (max-width: 480px){ 		#templateHeader .mcnTextContent,#templateHeader .mcnTextContent p{ 			font-size:16px !important; 			line-height:150% !important; 		} }	@media only screen and (max-width: 480px){ 		#templateBody .mcnTextContent,#templateBody .mcnTextContent p{ 			font-size:16px !important; 			line-height:150% !important; 		} }	@media only screen and (max-width: 480px){ 		#templateFooter .mcnTextContent,#templateFooter .mcnTextContent p{ 			font-size:14px !important; 			line-height:150% !important; 		} }</style></head> <body> <center> <table align='center' border='0' cellpadding='0' cellspacing='0' height='100%' width='100%' id='bodyTable'> <tr> <td align='center' valign='top' id='bodyCell'> <!-- BEGIN TEMPLATE // --> <!--[if (gte mso 9)|(IE)]> <table align='center' border='0' cellspacing='0' cellpadding='0' width='600' style='width:600px;'> <tr> <td align='center' valign='top' width='600' style='width:600px;'> <![endif]--> <table border='0' cellpadding='0' cellspacing='0' width='100%' class='templateContainer'> <tr> <td valign='top' id='templatePreheader'><table border='0' cellpadding='0' cellspacing='0' width='100%' class='mcnTextBlock' style='min-width:100%;'> <tbody class='mcnTextBlockOuter'> <tr> <td valign='top' class='mcnTextBlockInner' style='padding-top:9px;'> 	<!--[if mso]> 				<table align='left' border='0' cellspacing='0' cellpadding='0' width='100%' style='width:100%;'> 				<tr> 				<![endif]--> 			 				<!--[if mso]> 				<td valign='top' width='600' style='width:600px;'> 				<![endif]--> <table align='left' border='0' cellpadding='0' cellspacing='0' style='max-width:100%; min-width:100%;' width='100%' class='mcnTextContentContainer'> <tbody><tr><td valign='top' class='mcnTextContent' style='padding-top:0; padding-right:18px; padding-bottom:9px; padding-left:18px;'> </td> </tr> </tbody></table> 				<!--[if mso]> 				</td> 				<![endif]-->				<!--[if mso]> 				</tr> 				</table> 				<![endif]--> </td> </tr> </tbody> </table></td> </tr> <tr> <td valign='top' id='templateHeader'><table border='0' cellpadding='0' cellspacing='0' width='100%' class='mcnImageBlock' style='min-width:100%;'> <tbody class='mcnImageBlockOuter'> <tr> <td valign='top' style='padding:10px' class='mcnImageBlockInner'> <table align='left' width='100%' border='0' cellpadding='0' cellspacing='0' class='mcnImageContentContainer' style='min-width:100%;'> <tbody><tr> <td class='mcnImageContent' valign='top' style='padding-right: 10px; padding-left: 10px; padding-top: 0; padding-bottom: 0;'><a href='https://www.nordfincapital.com/' title={ class={ target='_blank'> <img align='left' alt={ src='https://cenk.co/images/Nordfin.png' width='250' style='max-width:500px; padding-bottom: 0; display: inline !important; vertical-align: bottom;' class='mcnRetinaImage'> </a></td> </tr> </tbody></table> </td> </tr> </tbody> </table></td> </tr> <tr> <td valign='top' id='templateBody'><table border='0' cellpadding='0' cellspacing='0' width='100%' class='mcnTextBlock' style='min-width:100%;'> <tbody class='mcnTextBlockOuter'> <tr> <td valign='top' class='mcnTextBlockInner' style='padding-top:9px;'> 	<!--[if mso]> 				<table align='left' border='0' cellspacing='0' cellpadding='0' width='100%' style='width:100%;'> 				<tr> 				<![endif]--> 			 				<!--[if mso]> 				<td valign='top' width='600' style='width:600px;'> 				<![endif]--> <table align='left' border='0' cellpadding='0' cellspacing='0' style='max-width:100%; min-width:100%;' width='100%' class='mcnTextContentContainer'> <tbody><tr><td valign='top' class='mcnTextContent' style='padding-top:0; padding-right:18px; padding-bottom:9px; padding-left:18px;'> " + txtEmailBody.Text.Replace("\n", "<br>") + " <br> &nbsp; </td> </tr> </tbody></table> 				<!--[if mso]> 				</td> 				<![endif]-->				<!--[if mso]> 				</tr> 				</table> 				<![endif]--> </td> </tr> </tbody> </table><table border='0' cellpadding='0' cellspacing='0' width='100%' class='mcnButtonBlock' style='min-width:100%;display:none;'> <tbody class='mcnButtonBlockOuter'> <tr> <td style='padding-top:0; padding-right:18px; padding-bottom:18px; padding-left:18px;' valign='top' align='center' class='mcnButtonBlockInner'>  </td> </tr> </tbody> </table><table border='0' cellpadding='0' cellspacing='0' width='100%' class='mcnTextBlock' style='min-width:100%;'> <tbody class='mcnTextBlockOuter'> <tr> <td valign='top' class='mcnTextBlockInner' style='padding-top:9px;'> 	<!--[if mso]> 				<table align='left' border='0' cellspacing='0' cellpadding='0' width='100%' style='width:100%;'> 				<tr> 				<![endif]--> 			 				<!--[if mso]> 				<td valign='top' width='600' style='width:600px;'> 				<![endif]--> 				<!--[if mso]> 				</td> 				<![endif]-->				<!--[if mso]> 				</tr> 				</table> 				<![endif]--> </td> </tr> </tbody> </table></td> </tr>  				</table> 				<![endif]--> </td> </tr> </tbody> </table></td> </tr> </table> <!--[if (gte mso 9)|(IE)]> </td> </tr> </table> <![endif]--> <!-- // END TEMPLATE --> </td> </tr> </table> </center> </body> </html> ";

                //            mail.From = new MailAddress(sEmail, ClientSession.ClientName);

                //            mail.To.Add(txtCustEmail.Text.Trim());

                //            mail.Subject = txtEmailHeader.Text;

                //            mail.Body = sBody;
                //            mail.IsBodyHtml = true;

                //            using (MemoryStream memoryStream = new MemoryStream())
                //            {
                //                using (ZipFile zip = new ZipFile())
                //                {
                //                    zip.AlternateEncodingUsage = ZipOption.AsNecessary;
                //                    zip.UseZip64WhenSaving = Zip64Option.AsNecessary;// ZipOption.AsNecessary;


                //                    foreach (FilesDownload file in fileList)
                //                    {
                //                        zip.AddEntry(file.FileName, file.Bytes);
                //                    }
                //                    zip.Save(memoryStream);
                //                }

                //                MemoryStream attachmentStream = new MemoryStream(memoryStream.ToArray());

                //                Attachment attachment = new Attachment(attachmentStream, "Invoices" + ".zip", MediaTypeNames.Application.Zip);
                //                mail.Attachments.Add(attachment);
                //            }
                //            SmtpServer.EnableSsl = true;
                //            SmtpServer.Send(mail);
                //        }


                //    }
                //}

            }

            catch (Exception ex)
            {
               
                //catch the issue
            }
        }



        protected void chkUnpaid_CheckedChanged(object sender, EventArgs e)
        {
            foreach (GridViewRow row in grdInvoiceDownlaod.Rows)
            {
                LinkButton linkButton = (row.FindControl("gridLink") as LinkButton);
                if (decimal.Parse(linkButton.Attributes["totalRemain"], System.Globalization.CultureInfo.CreateSpecificCulture("sv-SE"))>0)
                {
                    (row.FindControl("chkMultiInvoices") as CheckBox).Checked = chkUnpaid.Checked ? true : false;
                }
               
            }
        }
    }
}