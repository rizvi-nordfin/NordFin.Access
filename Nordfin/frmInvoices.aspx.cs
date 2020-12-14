using ClosedXML.Excel;
using Nordfin.workflow.Business;
using Nordfin.workflow.Entity;
using Nordfin.workflow.PresentationBusinessLayer;
using System;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;


namespace Nordfin
{
    public partial class frmInvoices : System.Web.UI.Page
    {
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
                    lblSumAmount.Text = String.Format(CultureInfo.GetCultureInfo("sv-SE"), "{0:#,0.00}", ds.Tables[0].AsEnumerable()
                    .Sum(r => ConvertStringToDecimal(Regex.Replace(r.Field<decimal>("Invoiceamount").ToString().Trim(), @"\s", "").Replace(",", "."))));



                    lblFeesAmount.Text = String.Format(CultureInfo.GetCultureInfo("sv-SE"), "{0:#,0.00}", ds.Tables[0].AsEnumerable()
                    .Sum(r => ConvertStringToDecimal(Regex.Replace(r.Field<decimal>("Fees").ToString().Trim(), @"\s", "").Replace(",", "."))));


                    lblTotalRemain.Text = String.Format(CultureInfo.GetCultureInfo("sv-SE"), "{0:#,0.00}", ds.Tables[0].AsEnumerable()
                    .Sum(r => ConvertStringToDecimal(Regex.Replace(r.Field<decimal>("TotalRemaining").ToString().Trim(), @"\s", "").Replace(",", "."))));


                    lblOverPaid.Text = String.Format(CultureInfo.GetCultureInfo("sv-SE"), "{0:#,0.00}", ds.Tables[0].AsEnumerable()
                    .Sum(r => ConvertStringToDecimal(Regex.Replace(r.Field<decimal>("Overpayment").ToString().Trim(), @"\s", "").Replace(",", "."))));

                    lblRemain.Text = String.Format(CultureInfo.GetCultureInfo("sv-SE"), "{0:#,0.00}", ds.Tables[0].AsEnumerable()
                   .Sum(r => ConvertStringToDecimal(Regex.Replace(r.Field<decimal>("Remainingamount").ToString().Trim(), @"\s", "").Replace(",", "."))));
                }


            }

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
            string subFolderName = hdnClientName.Value.Substring(hdnClientName.Value.LastIndexOf("/") + 1) + Execute(((Button)sender).CommandArgument.Trim().Replace("INV-", ""));
            FTPFileProcess fileProcess = new FTPFileProcess();
            string sFileExt = System.Configuration.ConfigurationManager.AppSettings["FileExtension"].ToString();
            string sFileName = hdnFileName.Value + "_" + ((Button)sender).CommandArgument.Trim() + "_" + "inv" + "." + sFileExt;
            string sPDFViewerLink = "";
            bool bResult = false;
            string ResultFile = "";
            if (sFileName != "")
                bResult = fileProcess.FileDownload(hdnClientName.Value, subFolderName, sFileName, out ResultFile);
            if (!bResult)
            {
                sPDFViewerLink = hdnArchiveLink.Value + ((Button)sender).CommandArgument.Trim();
            }
            if (ResultFile != "")
            {
                sFileName = ResultFile;
            }

            Button btn = (Button)sender;
            ScriptManager.RegisterStartupScript(this, this.GetType(), "PDFViewerNewTab", "PDFViewer('" + sFileName + "','" + sPDFViewerLink + "','" + Session.SessionID + "','" + bResult + "','" + btn.ClientID + "');", true);
        }

        [WebMethod]
        public static string PDFDownload(string hdnValue, string InvoiceNum, string hdnArchieveLink, string hdnClientName)
        {
            FTPFileProcess fileProcess = new FTPFileProcess();
            string sFileName = hdnValue + "_" + InvoiceNum;
            string sResultFileName = fileProcess.GetFilesDetailsFromFTP(hdnClientName, sFileName);
            string sPDFViewerLink = "";
            string ResultFile = "";
            if (sResultFileName != "")
                fileProcess.FileDownload(hdnClientName, "", sResultFileName, out ResultFile);
            else
            {
                sPDFViewerLink = hdnArchieveLink + InvoiceNum;
            }
            if (ResultFile != "")
            {
                sResultFileName = ResultFile;
            }
            return sResultFileName + "~" + sPDFViewerLink;
        }


        [WebMethod]
        public static string UnloadPage(string Test)
        {

            return Test;
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

        public decimal ConvertStringToDecimal(string sDecimal)
        {
            CultureInfo cultures = new CultureInfo("en-US");

            return Convert.ToDecimal(sDecimal, cultures);
        }


        [WebMethod]
        public static string GetCustEmail(string custNumber)
        {

            IInvoicesPresentationBusinessLayer objInvoicesLayer = new InvoicesBusinessLayer();
            string scustEmail = objInvoicesLayer.GetCustInvoiceEmailID(ClientSession.ClientID, custNumber);
            return scustEmail;

        }

        public void SendMail(string InvoiceNumber, string toEmail)
        {
            try
            {



                string sEmail = System.Configuration.ConfigurationManager.AppSettings["Email"].ToString();
                string sEmailPassword = System.Configuration.ConfigurationManager.AppSettings["EmailPassword"].ToString();
                string sEmailPort = System.Configuration.ConfigurationManager.AppSettings["EmailPort"].ToString();
                MailMessage mail = new MailMessage();

                SmtpClient SmtpServer = new SmtpClient("send.one.com");
                string sBody = "";

                sBody = "<!doctype html> <html xmlns='http://www.w3.org/1999/xhtml' xmlns:v='urn:schemas-microsoft-com:vml' xmlns:o='urn:schemas-microsoft-com:office:office'> <head> <!-- NAME: 1 COLUMN --> <!--[if gte mso 15]> <xml> <o:OfficeDocumentSettings> <o:AllowPNG/> <o:PixelsPerInch>96</o:PixelsPerInch> </o:OfficeDocumentSettings> </xml> <![endif]--> <meta charset='UTF-8'> <meta http-equiv='X-UA-Compatible' content='IE=edge'> <meta name='viewport' content='width=device-width, initial-scale=1'> <title>Nordfin</title><style type='text/css'> 		p{ 			margin:10px 0; 			padding:0; 		} 		table{ 			border-collapse:collapse; 		} 		h1,h2,h3,h4,h5,h6{ 			display:block; 			margin:0; 			padding:0; 		} 		img,a img{ 			border:0; 			height:auto; 			outline:none; 			text-decoration:none; 		} 		body,#bodyTable,#bodyCell{ 			height:100%; 			margin:0; 			padding:0; 			width:100%; 		} 		#outlook a{ 			padding:0; 		} 		img{ 			-ms-interpolation-mode:bicubic; 		} 		table{ 			mso-table-lspace:0pt; 			mso-table-rspace:0pt; 		} 		.ReadMsgBody{ 			width:100%; 		} 		p,a,li,td,blockquote{ 			mso-line-height-rule:exactly; 		} 		a[href^=tel],a[href^=sms]{ 			color:inherit; 			cursor:default; 			text-decoration:none; 		} 		p,a,li,td,body,table,blockquote{ 			-ms-text-size-adjust:100%; 			-webkit-text-size-adjust:100%; 		} 		a[x-apple-data-detectors]{ 			color:inherit !important; 			text-decoration:none !important; 			font-size:inherit !important; 			font-family:inherit !important; 			font-weight:inherit !important; 			line-height:inherit !important; 		} 		#bodyCell{ 			padding:10px; 		} 		.templateContainer{ 			max-width:600px !important; 		} 		a.mcnButton{ 			display:block; 		} 		.mcnImage,.mcnRetinaImage{ 			vertical-align:bottom; 		} 		.mcnTextContent{ 			word-break:break-word; 		} 		.mcnTextContent img{ 			height:auto !important; 		} 		.mcnDividerBlock{ 			table-layout:fixed !important; 		} 		body,#bodyTable{ 			background-color:#c0c0c0; 		} 		#bodyCell{ 			border-top:0; 		} 		.templateContainer{ 			border:0; 		} 		h1{ 			color:#202020; 			font-family:Helvetica; 			font-size:26px; 			font-style:normal; 			font-weight:bold; 			line-height:125%; 			letter-spacing:normal; 			text-align:left; 		} 		h2{ 			color:#202020; 			font-family:Helvetica; 			font-size:22px; 			font-style:normal; 			font-weight:bold; 			line-height:125%; 			letter-spacing:normal; 			text-align:left; 		} 		h3{ 			color:#202020; 			font-family:Helvetica; 			font-size:20px; 			font-style:normal; 			font-weight:bold; 			line-height:125%; 			letter-spacing:normal; 			text-align:left; 		} 		h4{ 			color:#202020; 			font-family:Helvetica; 			font-size:18px; 			font-style:normal; 			font-weight:bold; 			line-height:125%; 			letter-spacing:normal; 			text-align:left; 		} 		#templatePreheader{ 			background-color:#transparent; 			background-image:none; 			background-repeat:no-repeat; 			background-position:center; 			background-size:cover; 			border-top:0; 			border-bottom:0; 			padding-top:0px; 			padding-bottom:0px; 		} 		#templatePreheader .mcnTextContent,#templatePreheader .mcnTextContent p{ 			color:#656565; 			font-family:Helvetica; 			font-size:12px; 			line-height:150%; 			text-align:left; 		} 		#templatePreheader .mcnTextContent a,#templatePreheader .mcnTextContent p a{ 			color:#656565; 			font-weight:normal; 			text-decoration:underline; 		} 		#templateHeader{ 			background-color:#293746; 			background-image:none; 			background-repeat:no-repeat; 			background-position:center; 			background-size:cover; 			border-top:0; 			border-bottom:0; 			padding-top:20px; 			padding-bottom:20px; 		} 		#templateHeader .mcnTextContent,#templateHeader .mcnTextContent p{ 			color:#ffffff; 			font-family:Helvetica; 			font-size:16px; 			line-height:150%; 			text-align:left; 		} 		#templateHeader .mcnTextContent a,#templateHeader .mcnTextContent p a{ 			color:#ffffff; 			font-weight:normal; 			text-decoration:underline; 		} 		#templateBody{ 			background-color:#3e4b64; 			background-image:none; 			background-repeat:no-repeat; 			background-position:center; 			background-size:cover; 			border-top:0; 			border-bottom:2px solid #51627c; 			padding-top:10px; 			padding-bottom:10px; 		} 		#templateBody .mcnTextContent,#templateBody .mcnTextContent p{ 			color:#ffffff; 			font-family:Helvetica, Arial, Verdana, sans-serif; 			font-size:16px; 			line-height:150%; 			text-align:left; 		} 		#templateBody .mcnTextContent a,#templateBody .mcnTextContent p a{ 			color:#ffffff; 			font-weight:normal; 			text-decoration:underline; 		} 		#templateFooter{ 			background-color:#2c3850; 			background-image:none; 			background-repeat:no-repeat; 			background-position:center; 			background-size:cover; 			border-top:0; 			border-bottom:0; 			padding-top:9px; 			padding-bottom:9px; 		} 		#templateFooter .mcnTextContent,#templateFooter .mcnTextContent p{ 			color:#c5c5c5; 			font-family:Helvetica; 			font-size:12px; 			line-height:150%; 			text-align:center; 		} 		#templateFooter .mcnTextContent a,#templateFooter .mcnTextContent p a{ 			color:#ffffff; 			font-weight:normal; 			text-decoration:underline; 		} 	@media only screen and (min-width:768px){ 		.templateContainer{ 			width:600px !important; 		} }	@media only screen and (max-width: 480px){ 		body,table,td,p,a,li,blockquote{ 			-webkit-text-size-adjust:none !important; 		} }	@media only screen and (max-width: 480px){ 		body{ 			width:100% !important; 			min-width:100% !important; 		} }	@media only screen and (max-width: 480px){ 		#bodyCell{ 			padding-top:10px !important; 		} }	@media only screen and (max-width: 480px){ 		.mcnRetinaImage{ 			max-width:100% !important; 		} }	@media only screen and (max-width: 480px){ 		.mcnImage{ 			width:100% !important; 		} }	@media only screen and (max-width: 480px){ 		.mcnCartContainer,.mcnCaptionTopContent,.mcnRecContentContainer,.mcnCaptionBottomContent,.mcnTextContentContainer,.mcnBoxedTextContentContainer,.mcnImageGroupContentContainer,.mcnCaptionLeftTextContentContainer,.mcnCaptionRightTextContentContainer,.mcnCaptionLeftImageContentContainer,.mcnCaptionRightImageContentContainer,.mcnImageCardLeftTextContentContainer,.mcnImageCardRightTextContentContainer,.mcnImageCardLeftImageContentContainer,.mcnImageCardRightImageContentContainer{ 			max-width:100% !important; 			width:100% !important; 		} }	@media only screen and (max-width: 480px){ 		.mcnBoxedTextContentContainer{ 			min-width:100% !important; 		} }	@media only screen and (max-width: 480px){ 		.mcnImageGroupContent{ 			padding:9px !important; 		} }	@media only screen and (max-width: 480px){ 		.mcnCaptionLeftContentOuter .mcnTextContent,.mcnCaptionRightContentOuter .mcnTextContent{ 			padding-top:9px !important; 		} }	@media only screen and (max-width: 480px){ 		.mcnImageCardTopImageContent,.mcnCaptionBottomContent:last-child .mcnCaptionBottomImageContent,.mcnCaptionBlockInner .mcnCaptionTopContent:last-child .mcnTextContent{ 			padding-top:18px !important; 		} }	@media only screen and (max-width: 480px){ 		.mcnImageCardBottomImageContent{ 			padding-bottom:9px !important; 		} }	@media only screen and (max-width: 480px){ 		.mcnImageGroupBlockInner{ 			padding-top:0 !important; 			padding-bottom:0 !important; 		} }	@media only screen and (max-width: 480px){ 		.mcnImageGroupBlockOuter{ 			padding-top:9px !important; 			padding-bottom:9px !important; 		} }	@media only screen and (max-width: 480px){ 		.mcnTextContent,.mcnBoxedTextContentColumn{ 			padding-right:18px !important; 			padding-left:18px !important; 		} }	@media only screen and (max-width: 480px){ 		.mcnImageCardLeftImageContent,.mcnImageCardRightImageContent{ 			padding-right:18px !important; 			padding-bottom:0 !important; 			padding-left:18px !important; 		} }	@media only screen and (max-width: 480px){ 		.mcpreview-image-uploader{ 			display:none !important; 			width:100% !important; 		} }	@media only screen and (max-width: 480px){ 		h1{ 			font-size:22px !important; 			line-height:125% !important; 		} }	@media only screen and (max-width: 480px){ 		h2{ 			font-size:20px !important; 			line-height:125% !important; 		} }	@media only screen and (max-width: 480px){ 		h3{ 			font-size:18px !important; 			line-height:125% !important; 		} }	@media only screen and (max-width: 480px){ 		h4{ 			font-size:16px !important; 			line-height:150% !important; 		} }	@media only screen and (max-width: 480px){ 		.mcnBoxedTextContentContainer .mcnTextContent,.mcnBoxedTextContentContainer .mcnTextContent p{ 			font-size:14px !important; 			line-height:150% !important; 		} }	@media only screen and (max-width: 480px){ 		#templatePreheader{ 			display:block !important; 		} }	@media only screen and (max-width: 480px){ 		#templatePreheader .mcnTextContent,#templatePreheader .mcnTextContent p{ 			font-size:14px !important; 			line-height:150% !important; 		} }	@media only screen and (max-width: 480px){ 		#templateHeader .mcnTextContent,#templateHeader .mcnTextContent p{ 			font-size:16px !important; 			line-height:150% !important; 		} }	@media only screen and (max-width: 480px){ 		#templateBody .mcnTextContent,#templateBody .mcnTextContent p{ 			font-size:16px !important; 			line-height:150% !important; 		} }	@media only screen and (max-width: 480px){ 		#templateFooter .mcnTextContent,#templateFooter .mcnTextContent p{ 			font-size:14px !important; 			line-height:150% !important; 		} }</style></head> <body> <center> <table align='center' border='0' cellpadding='0' cellspacing='0' height='100%' width='100%' id='bodyTable'> <tr> <td align='center' valign='top' id='bodyCell'> <!-- BEGIN TEMPLATE // --> <!--[if (gte mso 9)|(IE)]> <table align='center' border='0' cellspacing='0' cellpadding='0' width='600' style='width:600px;'> <tr> <td align='center' valign='top' width='600' style='width:600px;'> <![endif]--> <table border='0' cellpadding='0' cellspacing='0' width='100%' class='templateContainer'> <tr> <td valign='top' id='templatePreheader'><table border='0' cellpadding='0' cellspacing='0' width='100%' class='mcnTextBlock' style='min-width:100%;'> <tbody class='mcnTextBlockOuter'> <tr> <td valign='top' class='mcnTextBlockInner' style='padding-top:9px;'> 	<!--[if mso]> 				<table align='left' border='0' cellspacing='0' cellpadding='0' width='100%' style='width:100%;'> 				<tr> 				<![endif]--> 			 				<!--[if mso]> 				<td valign='top' width='600' style='width:600px;'> 				<![endif]--> <table align='left' border='0' cellpadding='0' cellspacing='0' style='max-width:100%; min-width:100%;' width='100%' class='mcnTextContentContainer'> <tbody><tr><td valign='top' class='mcnTextContent' style='padding-top:0; padding-right:18px; padding-bottom:9px; padding-left:18px;'> </td> </tr> </tbody></table> 				<!--[if mso]> 				</td> 				<![endif]-->				<!--[if mso]> 				</tr> 				</table> 				<![endif]--> </td> </tr> </tbody> </table></td> </tr> <tr> <td valign='top' id='templateHeader'><table border='0' cellpadding='0' cellspacing='0' width='100%' class='mcnImageBlock' style='min-width:100%;'> <tbody class='mcnImageBlockOuter'> <tr> <td valign='top' style='padding:10px' class='mcnImageBlockInner'> <table align='left' width='100%' border='0' cellpadding='0' cellspacing='0' class='mcnImageContentContainer' style='min-width:100%;'> <tbody><tr> <td class='mcnImageContent' valign='top' style='padding-right: 10px; padding-left: 10px; padding-top: 0; padding-bottom: 0;'><a href='https://www.nordfincapital.com/' title={ class={ target='_blank'> <img align='left' alt={ src='https://cenk.co/images/Nordfin.png' width='250' style='max-width:500px; padding-bottom: 0; display: inline !important; vertical-align: bottom;' class='mcnRetinaImage'> </a></td> </tr> </tbody></table> </td> </tr> </tbody> </table></td> </tr> <tr> <td valign='top' id='templateBody'><table border='0' cellpadding='0' cellspacing='0' width='100%' class='mcnTextBlock' style='min-width:100%;'> <tbody class='mcnTextBlockOuter'> <tr> <td valign='top' class='mcnTextBlockInner' style='padding-top:9px;'> 	<!--[if mso]> 				<table align='left' border='0' cellspacing='0' cellpadding='0' width='100%' style='width:100%;'> 				<tr> 				<![endif]--> 			 				<!--[if mso]> 				<td valign='top' width='600' style='width:600px;'> 				<![endif]--> <table align='left' border='0' cellpadding='0' cellspacing='0' style='max-width:100%; min-width:100%;' width='100%' class='mcnTextContentContainer'> <tbody><tr><td valign='top' class='mcnTextContent' style='padding-top:0; padding-right:18px; padding-bottom:9px; padding-left:18px;'>Hello there,<br> <br> Good news! Your invoice has been attached :-)<br> &nbsp; </td> </tr> </tbody></table> 				<!--[if mso]> 				</td> 				<![endif]-->				<!--[if mso]> 				</tr> 				</table> 				<![endif]--> </td> </tr> </tbody> </table><table border='0' cellpadding='0' cellspacing='0' width='100%' class='mcnButtonBlock' style='min-width:100%;display:none;'> <tbody class='mcnButtonBlockOuter'> <tr> <td style='padding-top:0; padding-right:18px; padding-bottom:18px; padding-left:18px;' valign='top' align='center' class='mcnButtonBlockInner'> <table border='0' cellpadding='0' cellspacing='0' width='100%' class='mcnButtonContentContainer' style='border-collapse: separate !important;border-radius: 7px;background-color: #FFB100;'> <tbody> <tr> <td align='center' valign='middle' class='mcnButtonContent' style='font-family: &quot;Helvetica Neue&quot;, Helvetica, Arial, Verdana, sans-serif; font-size: 16px; padding: 18px;'> </td> </tr> </tbody> </table> </td> </tr> </tbody> </table><table border='0' cellpadding='0' cellspacing='0' width='100%' class='mcnTextBlock' style='min-width:100%;'> <tbody class='mcnTextBlockOuter'> <tr> <td valign='top' class='mcnTextBlockInner' style='padding-top:9px;'> 	<!--[if mso]> 				<table align='left' border='0' cellspacing='0' cellpadding='0' width='100%' style='width:100%;'> 				<tr> 				<![endif]--> 			 				<!--[if mso]> 				<td valign='top' width='600' style='width:600px;'> 				<![endif]--> <table align='left' border='0' cellpadding='0' cellspacing='0' style='max-width:100%; min-width:100%;' width='100%' class='mcnTextContentContainer'> <tbody><tr><td valign='top' class='mcnTextContent' style='padding-top:0; padding-right:18px; padding-bottom:9px; padding-left:18px;'> Best Regards,<br> Nordfin Capital Team<br> &nbsp; </td> </tr> </tbody></table> 				<!--[if mso]> 				</td> 				<![endif]-->				<!--[if mso]> 				</tr> 				</table> 				<![endif]--> </td> </tr> </tbody> </table></td> </tr> <tr> <td valign='top' id='templateFooter'><table border='0' cellpadding='0' cellspacing='0' width='100%' class='mcnTextBlock' style='min-width:100%;'> <tbody class='mcnTextBlockOuter'> <tr> <td valign='top' class='mcnTextBlockInner' style='padding-top:9px;'> 	<!--[if mso]> 				<table align='left' border='0' cellspacing='0' cellpadding='0' width='100%' style='width:100%;'> 				<tr> 				<![endif]--> 			 				<!--[if mso]> 				<td valign='top' width='600' style='width:600px;'> 				<![endif]--> <table align='left' border='0' cellpadding='0' cellspacing='0' style='max-width:100%; min-width:100%;' width='100%' class='mcnTextContentContainer'> <tbody><tr><td valign='top' class='mcnTextContent' style='padding-top:0; padding-right:18px; padding-bottom:9px; padding-left:18px;'><a href='https://www.nordfincapital.com/' target='_blank'>Nordfin Capital Group AB</a><br>  Org.nr 559123-4900 &nbsp;&nbsp;<br> © NordFin Capital Group AB </td> </tr> </tbody></table> 				<!--[if mso]> 				</td> 				<![endif]-->				<!--[if mso]> 				</tr> 				</table> 				<![endif]--> </td> </tr> </tbody> </table></td> </tr> </table> <!--[if (gte mso 9)|(IE)]> </td> </tr> </table> <![endif]--> <!-- // END TEMPLATE --> </td> </tr> </table> </center> </body> </html> ";

                mail.From = new MailAddress(sEmail);

                mail.To.Add(toEmail.Trim());

                mail.Subject = "Invoice" + " " + InvoiceNumber;

                mail.Body = sBody;
                mail.IsBodyHtml = true;
                string sFileExt = System.Configuration.ConfigurationManager.AppSettings["FileExtension"].ToString();
                string sFileName = hdnFileName.Value + "_" + InvoiceNumber + "_" + "inv" + "." + sFileExt;
                string sFolderPath = "~/Documents/" + Session.SessionID + "/" + sFileName;
                System.Net.Mail.Attachment attachment;
                attachment = new System.Net.Mail.Attachment(Server.MapPath(sFolderPath));
                mail.Attachments.Add(attachment);

                SmtpServer.Port = Convert.ToInt32(sEmailPort);

                SmtpServer.Credentials = new System.Net.NetworkCredential(sEmail, sEmailPassword);

                SmtpServer.EnableSsl = true;



                SmtpServer.Send(mail);



            }

            catch (Exception ex)
            {
                //catch the issue
            }
        }

        protected void btnExport_Click(object sender, EventArgs e)
        {
            HttpResponse response = HttpContext.Current.Response;
            response.Clear();
            response.Charset = "";

            DataTable dataTable = (DataTable)Session["InvoiceGrid"];
            try
            {

                dataTable.Columns.Remove("Customername");
                dataTable.Columns.Remove("CombineInvoice");
                dataTable.Columns.Remove("InvoiceID");
                dataTable.Columns.Remove("OrderID");
            }
            catch
            {
                //catch the issue
            }


            if (dataTable.Rows.Count > 0)
            {
                try
                {
                    dataTable.Columns.Add("InvoiceAmount", typeof(decimal));
                    dataTable.Columns.Add("RemainingAmount", typeof(decimal));
                    dataTable.Columns.Add("Totalremaining", typeof(decimal));
                    dataTable.Columns.Add("fees", typeof(decimal));
                    dataTable.Columns.Add("overpayment", typeof(decimal));
                    dataTable.AsEnumerable().ToList<DataRow>().ForEach(a =>
                    {
                        a["InvoiceAmount"] = ConvertStringToDecimal(Regex.Replace(a.Field<string>("Invoiceamount").Trim(), @"\s", "").Replace(",", "."));
                        a["RemainingAmount"] = ConvertStringToDecimal(Regex.Replace(a.Field<string>("Remainingamount").Trim(), @"\s", "").Replace(",", "."));
                        a["Totalremaining"] = ConvertStringToDecimal(Regex.Replace(a.Field<string>("TotalRemaining").Trim(), @"\s", "").Replace(",", "."));

                        a["fees"] = ConvertStringToDecimal(Regex.Replace(a.Field<string>("Fees").Trim(), @"\s", "").Replace(",", "."));
                        a["overpayment"] = ConvertStringToDecimal(Regex.Replace(a.Field<string>("Overpayment").Trim(), @"\s", "").Replace(",", "."));

                    });

                    dataTable.Columns.RemoveAt(3);
                    dataTable.Columns.RemoveAt(3);
                    dataTable.Columns.RemoveAt(5);
                    dataTable.Columns.RemoveAt(5);
                    dataTable.Columns.RemoveAt(5);
                    dataTable.Columns["InvoiceAmount"].SetOrdinal(3);

                    dataTable.Columns["Fees"].SetOrdinal(4);

                    dataTable.Columns["RemainingAmount"].SetOrdinal(7);
                    dataTable.Columns["TotalRemaining"].SetOrdinal(8);

                    dataTable.Columns["Overpayment"].SetOrdinal(11);
                }
                catch
                {
                    //catch the issue
                }

            }
            foreach (DataColumn column in dataTable.Columns)
                column.ColumnName = column.ColumnName.ToUpper();
            using (XLWorkbook wb = new XLWorkbook())
            {


                wb.Worksheets.Add(dataTable);

                Response.Clear();
                Response.Buffer = true;
                Response.Charset = "";
                string filename = "Invoices" + DateTime.Now.ToString("yyyy-MM-dd hh:mm tt") + ".xlsx";
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



    }
}