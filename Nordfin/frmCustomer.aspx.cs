using ClosedXML.Excel;
using Nordfin.workflow.Business;
using Nordfin.workflow.Entity;
using Nordfin.workflow.PresentationBusinessLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Dynamic;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Nordfin
{
    public partial class frmCustomer : System.Web.UI.Page
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
                string InvoiceNum = (string)Session["custNum"];
                hdnAdmin.Value = ClientSession.Admin;
                if (InvoiceNum != null && ClientSession.ClientID != null)
                {
                    IInvoicesPresentationBusinessLayer objInvoicesLayer = new InvoicesBusinessLayer();
                    DataSet ds = objInvoicesLayer.getInvoicesList(InvoiceNum.Trim(), ClientSession.ClientID, true, null);
                    Session["CustomerGrid"] = ds.Tables[0];
                    grdCustomer.DataSource = ds;
                    grdCustomer.DataBind();

                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        lblSumAmount.Text = string.Format("{0:#,0.00}", (ds.Tables[0].AsEnumerable().Sum(r => r.Field<decimal>("Invoiceamount"))));
                        lblFeesAmount.Text = string.Format("{0:#,0.00}", ds.Tables[0].AsEnumerable().Sum(r => r.Field<decimal>("Fees")));
                        lblTotalRemain.Text = string.Format("{0:#,0.00}", ds.Tables[0].AsEnumerable().Sum(r => r.Field<decimal>("TotalRemaining")));
                        lblOverPaid.Text = string.Format("{0:#,0.00}", ds.Tables[0].AsEnumerable().Sum(r => r.Field<decimal>("Overpayment")));
                        lblRemain.Text = string.Format("{0:#,0.00}", ds.Tables[0].AsEnumerable().Sum(r => r.Field<decimal>("Remainingamount")));
                    }

                    if (ds.Tables.Count > 1)
                    {
                        DataTable dtResult = ds.Tables[1];
                        if (dtResult.Rows.Count > 0)
                        {
                            lblName.Text = dtResult.Rows[0].ItemArray[0].ToString();
                            lblPersonNumber.Text = dtResult.Rows[0].ItemArray[1].ToString();
                            lblAddress.Text = dtResult.Rows[0].ItemArray[2].ToString();

                            lblPostalCode.Text = dtResult.Rows[0].ItemArray[3].ToString();
                            lblCity.Text = dtResult.Rows[0].ItemArray[4].ToString();
                            lblCountry.Text = dtResult.Rows[0].ItemArray[5].ToString();
                            lblEmail.Text = dtResult.Rows[0].ItemArray[6].ToString();
                            lblPhone.Text = dtResult.Rows[0].ItemArray[7].ToString();
                            lblCustomerNumber.Text = dtResult.Rows[0].ItemArray[8].ToString();
                            txtCustomerID.Text = Convert.ToString(dtResult.Rows[0].Field<int>("Customerid"));


                            if (dtResult.Rows[0].Field<int>("IsActive") > 0)
                            {
                                //pnlReset.Visible = true;
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "showResetPage", "$('#divResetRow').show(); $('#divResetPages').show();", true);
                            }
                            else
                            {
                                pnlUpdate.Style.Add("height", "70px");
                                pnlUpdate.Style.Add("padding-top", "10px");


                            }
                            try
                            {
                                lblAddress1.Text = dtResult.Rows[0].ItemArray[15].ToString();
                                if (lblAddress1.Text.Trim() == "")
                                {
                                    pnlAddress.Visible = false;
                                }
                            }
                            catch
                            {
                                pnlAddress.Visible = false;
                            }
                            try
                            {
                                lblInsurer.Text = dtResult.Rows[0].ItemArray[9].ToString();
                                lblReference.Text = dtResult.Rows[0].ItemArray[10].ToString();


                                lblCurrency.Text = dtResult.Rows[0].ItemArray[12].ToString();
                                try
                                {
                                    lblInsuredAmount.Text = String.Format(CultureInfo.GetCultureInfo("sv-SE"), "{0:#,0.00}", ConvertStringToDecimal(dtResult.Rows[0].ItemArray[11].ToString()));
                                    lblRemainingIns.Text = String.Format(CultureInfo.GetCultureInfo("sv-SE"), "{0:#,0.00}", ConvertStringToDecimal(dtResult.Rows[0].ItemArray[14].ToString()));
                                }
                                catch
                                {
                                    //catch the issue
                                }
                                pnlInsuredClient.Visible = Convert.ToBoolean(dtResult.Rows[0].ItemArray[13]);

                                if (!pnlInsuredClient.Visible)
                                {
                                    pnlPhone.Attributes.CssStyle.Add("padding-bottom", "25px");
                                }
                            }
                            catch
                            {
                                //catch the issue
                            }
                        }


                    }
                    if (ds.Tables.Count >= 3)
                    {
                        DataTable dtResult1 = ds.Tables[2];
                        if (dtResult1.Rows.Count > 0)
                        {
                            hdnClientName.Value = dtResult1.Rows[0].ItemArray[0].ToString();
                            hdnFileName.Value = dtResult1.Rows[0].ItemArray[1].ToString();
                            hdnArchiveLink.Value = dtResult1.Rows[0].ItemArray[2].ToString();
                        }


                    }
                    if (ds.Tables.Count >= 4)
                    {
                        DataTable dtResult2 = ds.Tables[3];
                        if (dtResult2.Rows.Count > 0)
                        {
                            grdNotes.DataSource = dtResult2;
                            grdNotes.DataBind();
                        }
                        else
                        {
                            grdNotes.DataSource = new List<string>();
                            grdNotes.DataBind();
                        }

                    }
                    if (ds.Tables.Count == 5)
                    {
                        DataTable dtInvoiceremain = ds.Tables[4];

                        try
                        {
                            dtInvoiceremain.AsEnumerable().ToList<DataRow>().ForEach(a =>
                            {
                                a["Invoiceamount"] = String.Format(CultureInfo.GetCultureInfo("sv-SE"), "{0:#,0.00}", ConvertStringToDecimal(a.Field<string>("Invoiceamount")));
                                a["Remainingamount"] = String.Format(CultureInfo.GetCultureInfo("sv-SE"), "{0:#,0.00}", ConvertStringToDecimal(a.Field<string>("Remainingamount")));


                            });
                        }
                        catch
                        {
                            //catch the issue                            
                        }


                        grdInvoiceRemaining.DataSource = dtInvoiceremain;
                        grdInvoiceRemaining.DataBind();
                    }
                    else
                    {
                        //divMatch.Visible = false;
                    }

                }
                else
                {
                    grdCustomer.DataSource = new List<string>();
                    grdCustomer.DataBind();
                }

                if (ClientSession.AllowManualInvoice)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "showManualInvoiceButton", "$('#divManualInvoiceRow').show(); $('#divManualInvoice').show();", true);
                }

            }
            ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), "CreateControl", "CreateControl();", true);
        }

        protected void gridLink_Click(object sender, EventArgs e)
        {
            Session["InvoiceNum"] = ((LinkButton)sender).Text.Trim();
            Response.Redirect("frmPaymentInformation.aspx");
        }

        protected void btnPDFDownload_Click(object sender, EventArgs e)
        {
            string subFolderName = hdnClientName.Value.Substring(hdnClientName.Value.LastIndexOf("/") + 1) + Utilities.Execute(((Button)sender).CommandArgument.Trim());
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

        private dynamic LoadManualInvoiceData()
        {
            dynamic dynObject = new ExpandoObject();
            var selectedRow = grdCustomer.SelectedRow;
            if (selectedRow != null)
            {
                var gridData = (DataTable)Session["CustomerGrid"];
                var selectedRowData = gridData.AsEnumerable().ElementAt(selectedRow.RowIndex);
                dynObject.invoiceAmount = selectedRowData.ItemArray[5].ToString();
                dynObject.billDate = selectedRowData.ItemArray[7].ToString();
                dynObject.dueDate = selectedRowData.ItemArray[8].ToString();
                dynObject.remainingAmount = selectedRowData.ItemArray[6].ToString();
                dynObject.totalAmount = selectedRowData.ItemArray[11].ToString();
            }
            return dynObject;
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

            DataTable dataTable = (DataTable)Session["CustomerGrid"];
            //try
            //{

            //    dataTable.Columns.Remove("Customername");
            //    dataTable.Columns.Remove("CombineInvoice");
            //    dataTable.Columns.Remove("InvoiceID");
            //    dataTable.Columns.Remove("OrderID");
            //}
            //catch
            //{
            //    //catch the issue
            //}


            //if (dataTable.Rows.Count > 0)
            //{
            //    try
            //    {
            //        dataTable.Columns.Add("InvoiceAmount", typeof(decimal));
            //        dataTable.Columns.Add("RemainingAmount", typeof(decimal));
            //        dataTable.Columns.Add("Totalremaining", typeof(decimal));
            //        dataTable.Columns.Add("fees", typeof(decimal));
            //        dataTable.Columns.Add("overpayment", typeof(decimal));
            //        dataTable.AsEnumerable().ToList<DataRow>().ForEach(a =>
            //        {
            //            a["InvoiceAmount"] = ConvertStringToDecimal(Regex.Replace(a.Field<string>("Invoiceamount").Trim(), @"\s", "").Replace(",", "."));
            //            a["RemainingAmount"] = ConvertStringToDecimal(Regex.Replace(a.Field<string>("Remainingamount").Trim(), @"\s", "").Replace(",", "."));
            //            a["Totalremaining"] = ConvertStringToDecimal(Regex.Replace(a.Field<string>("TotalRemaining").Trim(), @"\s", "").Replace(",", "."));

            //            a["fees"] = ConvertStringToDecimal(Regex.Replace(a.Field<string>("Fees").Trim(), @"\s", "").Replace(",", "."));
            //            a["overpayment"] = ConvertStringToDecimal(Regex.Replace(a.Field<string>("Overpayment").Trim(), @"\s", "").Replace(",", "."));

            //        });

            //        dataTable.Columns.RemoveAt(3);
            //        dataTable.Columns.RemoveAt(3);
            //        dataTable.Columns.RemoveAt(5);
            //        dataTable.Columns.RemoveAt(5);
            //        dataTable.Columns.RemoveAt(5);
            //        dataTable.Columns["InvoiceAmount"].SetOrdinal(3);
            //        dataTable.Columns["Fees"].SetOrdinal(4);
            //        dataTable.Columns["RemainingAmount"].SetOrdinal(7);
            //        dataTable.Columns["TotalRemaining"].SetOrdinal(8);
            //        dataTable.Columns["Overpayment"].SetOrdinal(11);
            //    }
            //    catch
            //    {
            //        //catch the issue
            //    }

            //}
            foreach (DataColumn column in dataTable.Columns)
                column.ColumnName = column.ColumnName.ToUpper();
            using (XLWorkbook wb = new XLWorkbook())
            {


                var ws = wb.Worksheets.Add(dataTable);
                ws.Unprotect();
                ws.Column(1).Delete();
                ws.Column(1).Delete();
                ws.Column(13).Delete();
                ws.Column(14).Delete();
                ws.Column(13).Delete();

                


                Response.Clear();
                Response.Buffer = true;
                Response.Charset = "";
                string filename = "CustomerInvoices" + DateTime.Now.ToString("yyyy-MM-dd hh:mm tt") + ".xlsx";
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

        protected void btnOk_Click(object sender, EventArgs e)
        {
            IInvoicesPresentationBusinessLayer objInvoicesLayer = new InvoicesBusinessLayer();
            int Result = objInvoicesLayer.DeleteCustomerLogin(ClientSession.ClientID, lblCustomerNumber.Text.Trim());
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            string sComments = "";

            if (lblName.Text != txtCustomerName.Text)
                sComments += xmlString(spnCustomerName.Text, lblName.Text, txtCustomerName.Text);
            if (lblAddress.Text != txtAddress1.Text)
                sComments += xmlString(spnAddress1.Text, lblAddress.Text, txtAddress1.Text);
            if (lblAddress1.Text != txtAddress2.Text)
                sComments += xmlString(spnAddress2.Text, lblAddress1.Text, txtAddress2.Text);

            if (lblPostalCode.Text != txtPostalCode.Text)
                sComments += xmlString(spnPostalCode.Text, lblPostalCode.Text, txtPostalCode.Text);
            if (lblCity.Text != txtCity.Text)
                sComments += xmlString(spnCity.Text, lblCity.Text, txtCity.Text);
            if (lblCountry.Text != txtCountry.Text)
                sComments += xmlString(spnCountry.Text, lblCountry.Text, txtCountry.Text);

            if (lblEmail.Text != txtEmail.Text)
                sComments += xmlString(spnModalEmail.Text, lblEmail.Text, txtEmail.Text);
            if (lblPhone.Text != txtPhoneNumber.Text)
                sComments += xmlString(spnPhonenumber.Text, lblPhone.Text, txtPhoneNumber.Text);

            CustomerInfo customerInfo = new CustomerInfo();
            customerInfo.Name = txtCustomerName.Text;
            customerInfo.Address1 = txtAddress1.Text;
            customerInfo.Address2 = txtAddress2.Text;
            customerInfo.PostalCode = txtPostalCode.Text;
            customerInfo.City = txtCity.Text;
            customerInfo.Country = txtCountry.Text;
            customerInfo.Email = txtEmail.Text;
            customerInfo.PhoneNumber = txtPhoneNumber.Text;
            customerInfo.Comments = sComments;
            customerInfo.CustomerID = Convert.ToInt32(txtCustomerID.Text);
            customerInfo.UserID = Convert.ToInt32(ClientSession.UserID);
            customerInfo.CustomerNumber = lblCustomerNumber.Text;



            IInvoicesPresentationBusinessLayer objInvoicesLayer = new InvoicesBusinessLayer();
            int scustEmail = objInvoicesLayer.UpdateCustomerInfo(customerInfo, ClientSession.ClientID);


            lblName.Text = txtCustomerName.Text;
            lblAddress.Text = txtAddress1.Text;
            lblAddress1.Text = txtAddress2.Text;
            lblPostalCode.Text = txtPostalCode.Text;
            lblCity.Text = txtCity.Text;
            lblCountry.Text = txtCountry.Text;
            lblEmail.Text = txtEmail.Text;
            lblPhone.Text = txtPhoneNumber.Text;


            if (lblAddress.Text == "" && lblAddress1.Text != "")
            {
                lblAddress.Text = txtAddress2.Text;
            }
            else if (lblAddress1.Text.Trim() == "")
            {
                pnlAddress.Visible = false;
            }

        }

        protected string xmlString(string labelText, string oldValue, string newValue)
        {
            return "<" + labelText.Replace(" ", "") + "><old>" + oldValue + "</old>" + "<new>" + newValue + "</new>" + "</" + labelText.Replace(" ", "") + ">";
        }


        [WebMethod]
        public static string GetMatchInvoices(string NegativeinvAmount, string PositiveinvAmount)
        {

            IInvoicesPresentationBusinessLayer objInvoicesLayer = new InvoicesBusinessLayer();
            IList<MatchInvoices> Result = objInvoicesLayer.GetMatchedInvoices(Regex.Replace(NegativeinvAmount.Trim(), @"\s", "").Replace(",", "."), Regex.Replace(PositiveinvAmount.Trim(), @"\s", "").Replace(",", "."), ClientSession.UserID);
            string jsonMatchedInv = new JavaScriptSerializer().Serialize(Result);
            string MatchedInv = "{\"MatchInvoice\" :" + jsonMatchedInv + "}";
            return MatchedInv;

        }

        protected void btnExportDetail_Click(object sender, EventArgs e)
        {
            IInvoicesPresentationBusinessLayer objInvoicesLayer = new InvoicesBusinessLayer();
            DataSet dataSet = objInvoicesLayer.getExportdetails((string)Session["custNum"], ClientSession.ClientID);

            using (XLWorkbook wb = new XLWorkbook())
            {
                var ws = wb.Worksheets.Add(dataSet.Tables[0]);
                ws.Tables.FirstOrDefault().ShowAutoFilter = false;
                ws.Tables.FirstOrDefault().Theme = XLTableTheme.TableStyleLight16;
                wb.Worksheet(1).Columns().AdjustToContents();
                DataTable dataTable = dataSet.Tables[0];
                for (int i = 0; i < dataTable.Rows.Count; i++)
                {
                    string str = "A" + (i + 2).ToString() + ":" + "Q" + (i + 2).ToString();
                    if (dataTable.Rows[i].ItemArray[0].ToString().ToUpper() == "INVOICES")
                    {
                        ws.Range(str).Style.Fill.BackgroundColor= XLColor.FromHtml("#CCCCFF");
                    }
                    else
                    {
                        ws.Range(str).Style.Fill.BackgroundColor = XLColor.FromHtml("#FFFFFF");
                    }

                }
                Response.Clear();
                Response.Buffer = true;
                Response.Charset = "";
                string filename = "CustomersDetailsReoprt" + DateTime.Now.ToString("yyyy-MM-dd hh:mm tt") + ".xlsx";
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



        protected void grdCustomer_Sorting(object sender, GridViewSortEventArgs e)
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

        protected void grdCustomer_OnRowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Attributes["onclick"] = Page.ClientScript.GetPostBackClientHyperlink(grdCustomer, "Select$" + e.Row.RowIndex);
                e.Row.ToolTip = "click to create Credit Invoice";
            }
        }

        private void SortGridView(string sortExpression, string direction)
        {

            DataTable dt = (DataTable)Session["CustomerGrid"];

            DataView dv = new DataView(dt);

            dv.Sort = sortExpression + direction;

            grdCustomer.DataSource = dv;
            grdCustomer.DataBind();
        }

        protected override void Render(HtmlTextWriter writer)
        {
            foreach (GridViewRow r in grdCustomer.Rows)
            {
                if (r.RowType == DataControlRowType.DataRow)
                {
                    Page.ClientScript.RegisterForEventValidation(grdCustomer.UniqueID, "Select$" + r.RowIndex);
                }
            }

            base.Render(writer);
        }
    }
}