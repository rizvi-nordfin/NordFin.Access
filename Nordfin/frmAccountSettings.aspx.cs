
using Nordfin.workflow.BusinessLayer;
using Nordfin.workflow.Entity;
using Nordfin.workflow.PresentationBusinessLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Nordfin
{ 
    public partial class frmAccountSettings : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            ClearSession();
            if (!IsPostBack)
            {

                IAccountSettingsPresentationBusinessLayer objAccountSettingsList = new AccountSettingsBusinessLayer();
                Tuple<AccountSettings, DataTable> tuple = objAccountSettingsList.getAccountSettingsInfo(ClientSession.ClientID, ClientSession.UserID);
                AccountSettings accountSettings = tuple.Item1;
                if (accountSettings != null)
                {
                    lblFirstName.Text = accountSettings.FirstName;
                    lblLastName.Text = accountSettings.LastName;
                    txtEmail.Text = accountSettings.Email;
                    lblUserName.Text = accountSettings.UserName;
                    txtPhone.Text = accountSettings.PhoneNumber;
                    lblCompanyName.Text = accountSettings.CompanyName;


                }

                DataTable dataTable = tuple.Item2;
                if (dataTable != null && dataTable.Rows.Count > 0)
                {
                    grdActivityLog.DataSource = dataTable;
                    grdActivityLog.DataBind();
                }
                else
                {
                    grdActivityLog.DataSource = new List<string>();
                    grdActivityLog.DataBind();
                }
            }
        }
           
        

      


        [WebMethod]
        public static string OldPasswordCheck(string sPassword)
        {
    

            
            IAccountSettingsPresentationBusinessLayer objAccountSettingsList = new AccountSettingsBusinessLayer();

            int iResult = objAccountSettingsList.checkPasswordExists(ClientSession.UserID, GetHashPassword(sPassword));

            string jsonPasswordCount = new JavaScriptSerializer().Serialize(iResult);

            string sResultList = "{\"PasswordExsists\" :" + jsonPasswordCount + "}";


            return sResultList;


        }

        private static string GetHashPassword(string Password)
        {
            System.Security.Cryptography.MD5CryptoServiceProvider mdservice = new System.Security.Cryptography.MD5CryptoServiceProvider();
            byte[] bs = System.Text.Encoding.UTF8.GetBytes(Password);
            bs = mdservice.ComputeHash(bs);
            System.Text.StringBuilder s = new System.Text.StringBuilder();
            foreach (byte b in bs)
            {
                s.Append(b.ToString("x2"));
            }
            return s.ToString();
        }

        protected void submit_Click(object sender, EventArgs e)
        {
            ClearSession();
            IAccountSettingsPresentationBusinessLayer objAccountSettingsList = new AccountSettingsBusinessLayer();


            int iResult = objAccountSettingsList.UpdateUserPassword(ClientSession.UserID, GetHashPassword(txtPassword.Text), "");

            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "panelInfo", " $('.featureNotAvailableBGpnlPass').toggleClass('hidden');", true);
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
                catch { }
                Session.Abandon();
                Response.Redirect("frmLogin.aspx");

            }
        }

        protected void btnUpdateInfo_Click(object sender, EventArgs e)
        {
            ClearSession();
          
            IAccountSettingsPresentationBusinessLayer objAccountSettingsList = new AccountSettingsBusinessLayer();


            int iResult = objAccountSettingsList.UpdateUserPassword(ClientSession.UserID, "", txtNewNumber.Text);


            if (txtNewEmail.Text != string.Empty)
            {
                EmailConfirmation(iResult);
                txtEmail.Text = txtNewEmail.Text;
            }

            if (txtNewNumber.Text!="")
            {
                txtPhone.Text = txtNewNumber.Text;
            }
          
            txtNewNumber.Text = "";
            txtNewEmail.Text = "";
        }


        public void EmailConfirmation(int Result)
        {
            try
            {
                
                string sRandomNumber = Session.SessionID.Substring(3, 9) + RandomString();
                string urlRandomNumber = HttpContext.Current.Request.Url.AbsoluteUri.Replace("frmAccountSettings.aspx", "frmLogin.aspx") + "?emailConf=" + sRandomNumber;
                string sEmail = System.Configuration.ConfigurationManager.AppSettings["Email"].ToString();
                string sEmailPassword = System.Configuration.ConfigurationManager.AppSettings["EmailPassword"].ToString();
                string sEmailPort = System.Configuration.ConfigurationManager.AppSettings["EmailPort"].ToString();
                MailMessage mail = new MailMessage();

                SmtpClient SmtpServer = new SmtpClient("send.one.com");
                string sBody = "";
                if (Result == 1)
                {
                    sBody = "<!doctype html> <html xmlns='http://www.w3.org/1999/xhtml' xmlns:v='urn:schemas-microsoft-com:vml' xmlns:o='urn:schemas-microsoft-com:office:office'> <head> <!-- NAME: 1 COLUMN --> <!--[if gte mso 15]> <xml> <o:OfficeDocumentSettings> <o:AllowPNG/> <o:PixelsPerInch>96</o:PixelsPerInch> </o:OfficeDocumentSettings> </xml> <![endif]--> <meta charset='UTF-8'> <meta http-equiv='X-UA-Compatible' content='IE=edge'> <meta name='viewport' content='width=device-width, initial-scale=1'> <title>Nordfin</title><style type='text/css'> 		p{ 			margin:10px 0; 			padding:0; 		} 		table{ 			border-collapse:collapse; 		} 		h1,h2,h3,h4,h5,h6{ 			display:block; 			margin:0; 			padding:0; 		} 		img,a img{ 			border:0; 			height:auto; 			outline:none; 			text-decoration:none; 		} 		body,#bodyTable,#bodyCell{ 			height:100%; 			margin:0; 			padding:0; 			width:100%; 		} 		#outlook a{ 			padding:0; 		} 		img{ 			-ms-interpolation-mode:bicubic; 		} 		table{ 			mso-table-lspace:0pt; 			mso-table-rspace:0pt; 		} 		.ReadMsgBody{ 			width:100%; 		} 		p,a,li,td,blockquote{ 			mso-line-height-rule:exactly; 		} 		a[href^=tel],a[href^=sms]{ 			color:inherit; 			cursor:default; 			text-decoration:none; 		} 		p,a,li,td,body,table,blockquote{ 			-ms-text-size-adjust:100%; 			-webkit-text-size-adjust:100%; 		} 		a[x-apple-data-detectors]{ 			color:inherit !important; 			text-decoration:none !important; 			font-size:inherit !important; 			font-family:inherit !important; 			font-weight:inherit !important; 			line-height:inherit !important; 		} 		#bodyCell{ 			padding:10px; 		} 		.templateContainer{ 			max-width:600px !important; 		} 		a.mcnButton{ 			display:block; 		} 		.mcnImage,.mcnRetinaImage{ 			vertical-align:bottom; 		} 		.mcnTextContent{ 			word-break:break-word; 		} 		.mcnTextContent img{ 			height:auto !important; 		} 		.mcnDividerBlock{ 			table-layout:fixed !important; 		} 		body,#bodyTable{ 			background-color:#c0c0c0; 		} 		#bodyCell{ 			border-top:0; 		} 		.templateContainer{ 			border:0; 		} 		h1{ 			color:#202020; 			font-family:Helvetica; 			font-size:26px; 			font-style:normal; 			font-weight:bold; 			line-height:125%; 			letter-spacing:normal; 			text-align:left; 		} 		h2{ 			color:#202020; 			font-family:Helvetica; 			font-size:22px; 			font-style:normal; 			font-weight:bold; 			line-height:125%; 			letter-spacing:normal; 			text-align:left; 		} 		h3{ 			color:#202020; 			font-family:Helvetica; 			font-size:20px; 			font-style:normal; 			font-weight:bold; 			line-height:125%; 			letter-spacing:normal; 			text-align:left; 		} 		h4{ 			color:#202020; 			font-family:Helvetica; 			font-size:18px; 			font-style:normal; 			font-weight:bold; 			line-height:125%; 			letter-spacing:normal; 			text-align:left; 		} 		#templatePreheader{ 			background-color:#transparent; 			background-image:none; 			background-repeat:no-repeat; 			background-position:center; 			background-size:cover; 			border-top:0; 			border-bottom:0; 			padding-top:0px; 			padding-bottom:0px; 		} 		#templatePreheader .mcnTextContent,#templatePreheader .mcnTextContent p{ 			color:#656565; 			font-family:Helvetica; 			font-size:12px; 			line-height:150%; 			text-align:left; 		} 		#templatePreheader .mcnTextContent a,#templatePreheader .mcnTextContent p a{ 			color:#656565; 			font-weight:normal; 			text-decoration:underline; 		} 		#templateHeader{ 			background-color:#293746; 			background-image:none; 			background-repeat:no-repeat; 			background-position:center; 			background-size:cover; 			border-top:0; 			border-bottom:0; 			padding-top:20px; 			padding-bottom:20px; 		} 		#templateHeader .mcnTextContent,#templateHeader .mcnTextContent p{ 			color:#ffffff; 			font-family:Helvetica; 			font-size:16px; 			line-height:150%; 			text-align:left; 		} 		#templateHeader .mcnTextContent a,#templateHeader .mcnTextContent p a{ 			color:#ffffff; 			font-weight:normal; 			text-decoration:underline; 		} 		#templateBody{ 			background-color:#3e4b64; 			background-image:none; 			background-repeat:no-repeat; 			background-position:center; 			background-size:cover; 			border-top:0; 			border-bottom:2px solid #51627c; 			padding-top:10px; 			padding-bottom:10px; 		} 		#templateBody .mcnTextContent,#templateBody .mcnTextContent p{ 			color:#ffffff; 			font-family:Helvetica, Arial, Verdana, sans-serif; 			font-size:16px; 			line-height:150%; 			text-align:left; 		} 		#templateBody .mcnTextContent a,#templateBody .mcnTextContent p a{ 			color:#ffffff; 			font-weight:normal; 			text-decoration:underline; 		} 		#templateFooter{ 			background-color:#2c3850; 			background-image:none; 			background-repeat:no-repeat; 			background-position:center; 			background-size:cover; 			border-top:0; 			border-bottom:0; 			padding-top:9px; 			padding-bottom:9px; 		} 		#templateFooter .mcnTextContent,#templateFooter .mcnTextContent p{ 			color:#c5c5c5; 			font-family:Helvetica; 			font-size:12px; 			line-height:150%; 			text-align:center; 		} 		#templateFooter .mcnTextContent a,#templateFooter .mcnTextContent p a{ 			color:#ffffff; 			font-weight:normal; 			text-decoration:underline; 		} 	@media only screen and (min-width:768px){ 		.templateContainer{ 			width:600px !important; 		} }	@media only screen and (max-width: 480px){ 		body,table,td,p,a,li,blockquote{ 			-webkit-text-size-adjust:none !important; 		} }	@media only screen and (max-width: 480px){ 		body{ 			width:100% !important; 			min-width:100% !important; 		} }	@media only screen and (max-width: 480px){ 		#bodyCell{ 			padding-top:10px !important; 		} }	@media only screen and (max-width: 480px){ 		.mcnRetinaImage{ 			max-width:100% !important; 		} }	@media only screen and (max-width: 480px){ 		.mcnImage{ 			width:100% !important; 		} }	@media only screen and (max-width: 480px){ 		.mcnCartContainer,.mcnCaptionTopContent,.mcnRecContentContainer,.mcnCaptionBottomContent,.mcnTextContentContainer,.mcnBoxedTextContentContainer,.mcnImageGroupContentContainer,.mcnCaptionLeftTextContentContainer,.mcnCaptionRightTextContentContainer,.mcnCaptionLeftImageContentContainer,.mcnCaptionRightImageContentContainer,.mcnImageCardLeftTextContentContainer,.mcnImageCardRightTextContentContainer,.mcnImageCardLeftImageContentContainer,.mcnImageCardRightImageContentContainer{ 			max-width:100% !important; 			width:100% !important; 		} }	@media only screen and (max-width: 480px){ 		.mcnBoxedTextContentContainer{ 			min-width:100% !important; 		} }	@media only screen and (max-width: 480px){ 		.mcnImageGroupContent{ 			padding:9px !important; 		} }	@media only screen and (max-width: 480px){ 		.mcnCaptionLeftContentOuter .mcnTextContent,.mcnCaptionRightContentOuter .mcnTextContent{ 			padding-top:9px !important; 		} }	@media only screen and (max-width: 480px){ 		.mcnImageCardTopImageContent,.mcnCaptionBottomContent:last-child .mcnCaptionBottomImageContent,.mcnCaptionBlockInner .mcnCaptionTopContent:last-child .mcnTextContent{ 			padding-top:18px !important; 		} }	@media only screen and (max-width: 480px){ 		.mcnImageCardBottomImageContent{ 			padding-bottom:9px !important; 		} }	@media only screen and (max-width: 480px){ 		.mcnImageGroupBlockInner{ 			padding-top:0 !important; 			padding-bottom:0 !important; 		} }	@media only screen and (max-width: 480px){ 		.mcnImageGroupBlockOuter{ 			padding-top:9px !important; 			padding-bottom:9px !important; 		} }	@media only screen and (max-width: 480px){ 		.mcnTextContent,.mcnBoxedTextContentColumn{ 			padding-right:18px !important; 			padding-left:18px !important; 		} }	@media only screen and (max-width: 480px){ 		.mcnImageCardLeftImageContent,.mcnImageCardRightImageContent{ 			padding-right:18px !important; 			padding-bottom:0 !important; 			padding-left:18px !important; 		} }	@media only screen and (max-width: 480px){ 		.mcpreview-image-uploader{ 			display:none !important; 			width:100% !important; 		} }	@media only screen and (max-width: 480px){ 		h1{ 			font-size:22px !important; 			line-height:125% !important; 		} }	@media only screen and (max-width: 480px){ 		h2{ 			font-size:20px !important; 			line-height:125% !important; 		} }	@media only screen and (max-width: 480px){ 		h3{ 			font-size:18px !important; 			line-height:125% !important; 		} }	@media only screen and (max-width: 480px){ 		h4{ 			font-size:16px !important; 			line-height:150% !important; 		} }	@media only screen and (max-width: 480px){ 		.mcnBoxedTextContentContainer .mcnTextContent,.mcnBoxedTextContentContainer .mcnTextContent p{ 			font-size:14px !important; 			line-height:150% !important; 		} }	@media only screen and (max-width: 480px){ 		#templatePreheader{ 			display:block !important; 		} }	@media only screen and (max-width: 480px){ 		#templatePreheader .mcnTextContent,#templatePreheader .mcnTextContent p{ 			font-size:14px !important; 			line-height:150% !important; 		} }	@media only screen and (max-width: 480px){ 		#templateHeader .mcnTextContent,#templateHeader .mcnTextContent p{ 			font-size:16px !important; 			line-height:150% !important; 		} }	@media only screen and (max-width: 480px){ 		#templateBody .mcnTextContent,#templateBody .mcnTextContent p{ 			font-size:16px !important; 			line-height:150% !important; 		} }	@media only screen and (max-width: 480px){ 		#templateFooter .mcnTextContent,#templateFooter .mcnTextContent p{ 			font-size:14px !important; 			line-height:150% !important; 		} }</style></head> <body> <center> <table align='center' border='0' cellpadding='0' cellspacing='0' height='100%' width='100%' id='bodyTable'> <tr> <td align='center' valign='top' id='bodyCell'> <!-- BEGIN TEMPLATE // --> <!--[if (gte mso 9)|(IE)]> <table align='center' border='0' cellspacing='0' cellpadding='0' width='600' style='width:600px;'> <tr> <td align='center' valign='top' width='600' style='width:600px;'> <![endif]--> <table border='0' cellpadding='0' cellspacing='0' width='100%' class='templateContainer'> <tr> <td valign='top' id='templatePreheader'><table border='0' cellpadding='0' cellspacing='0' width='100%' class='mcnTextBlock' style='min-width:100%;'> <tbody class='mcnTextBlockOuter'> <tr> <td valign='top' class='mcnTextBlockInner' style='padding-top:9px;'> 	<!--[if mso]> 				<table align='left' border='0' cellspacing='0' cellpadding='0' width='100%' style='width:100%;'> 				<tr> 				<![endif]--> 			 				<!--[if mso]> 				<td valign='top' width='600' style='width:600px;'> 				<![endif]--> <table align='left' border='0' cellpadding='0' cellspacing='0' style='max-width:100%; min-width:100%;' width='100%' class='mcnTextContentContainer'> <tbody><tr><td valign='top' class='mcnTextContent' style='padding-top:0; padding-right:18px; padding-bottom:9px; padding-left:18px;'> </td> </tr> </tbody></table> 				<!--[if mso]> 				</td> 				<![endif]-->				<!--[if mso]> 				</tr> 				</table> 				<![endif]--> </td> </tr> </tbody> </table></td> </tr> <tr> <td valign='top' id='templateHeader'><table border='0' cellpadding='0' cellspacing='0' width='100%' class='mcnImageBlock' style='min-width:100%;'> <tbody class='mcnImageBlockOuter'> <tr> <td valign='top' style='padding:10px' class='mcnImageBlockInner'> <table align='left' width='100%' border='0' cellpadding='0' cellspacing='0' class='mcnImageContentContainer' style='min-width:100%;'> <tbody><tr> <td class='mcnImageContent' valign='top' style='padding-right: 10px; padding-left: 10px; padding-top: 0; padding-bottom: 0;'><a href='https://www.nordfincapital.com/' title={ class={ target='_blank'> <img align='left' alt={ src='https://cenk.co/images/Nordfin.png' width='250' style='max-width:500px; padding-bottom: 0; display: inline !important; vertical-align: bottom;' class='mcnRetinaImage'> </a></td> </tr> </tbody></table> </td> </tr> </tbody> </table></td> </tr> <tr> <td valign='top' id='templateBody'><table border='0' cellpadding='0' cellspacing='0' width='100%' class='mcnTextBlock' style='min-width:100%;'> <tbody class='mcnTextBlockOuter'> <tr> <td valign='top' class='mcnTextBlockInner' style='padding-top:9px;'> 	<!--[if mso]> 				<table align='left' border='0' cellspacing='0' cellpadding='0' width='100%' style='width:100%;'> 				<tr> 				<![endif]--> 			 				<!--[if mso]> 				<td valign='top' width='600' style='width:600px;'> 				<![endif]--> <table align='left' border='0' cellpadding='0' cellspacing='0' style='max-width:100%; min-width:100%;' width='100%' class='mcnTextContentContainer'> <tbody><tr><td valign='top' class='mcnTextContent' style='padding-top:0; padding-right:18px; padding-bottom:9px; padding-left:18px;'>Hello there,<br> <br> Good news! Your email has been updated :-)<br> <br> Click on the link below to finish setting up your new email address. <br> Link is valid for 24 hours<br> &nbsp; </td> </tr> </tbody></table> 				<!--[if mso]> 				</td> 				<![endif]-->				<!--[if mso]> 				</tr> 				</table> 				<![endif]--> </td> </tr> </tbody> </table><table border='0' cellpadding='0' cellspacing='0' width='100%' class='mcnButtonBlock' style='min-width:100%;'> <tbody class='mcnButtonBlockOuter'> <tr> <td style='padding-top:0; padding-right:18px; padding-bottom:18px; padding-left:18px;' valign='top' align='center' class='mcnButtonBlockInner'> <table border='0' cellpadding='0' cellspacing='0' width='100%' class='mcnButtonContentContainer' style='border-collapse: separate !important;border-radius: 7px;background-color: #FFB100;'> <tbody> <tr> <td align='center' valign='middle' class='mcnButtonContent' style='font-family: &quot;Helvetica Neue&quot;, Helvetica, Arial, Verdana, sans-serif; font-size: 16px; padding: 18px;'> <a class='mcnButton ' title='Update My Email Address →' href='" + urlRandomNumber + "' target='_blank' style='font-weight: bold;letter-spacing: normal;line-height: 100%;text-align: center;text-decoration: none;color: #232D41;'>Update My Email Address →</a> </td> </tr> </tbody> </table> </td> </tr> </tbody> </table><table border='0' cellpadding='0' cellspacing='0' width='100%' class='mcnTextBlock' style='min-width:100%;'> <tbody class='mcnTextBlockOuter'> <tr> <td valign='top' class='mcnTextBlockInner' style='padding-top:9px;'> 	<!--[if mso]> 				<table align='left' border='0' cellspacing='0' cellpadding='0' width='100%' style='width:100%;'> 				<tr> 				<![endif]--> 			 				<!--[if mso]> 				<td valign='top' width='600' style='width:600px;'> 				<![endif]--> <table align='left' border='0' cellpadding='0' cellspacing='0' style='max-width:100%; min-width:100%;' width='100%' class='mcnTextContentContainer'> <tbody><tr><td valign='top' class='mcnTextContent' style='padding-top:0; padding-right:18px; padding-bottom:9px; padding-left:18px;'> Best Regards,<br> Nordfin Capital Team<br> &nbsp; </td> </tr> </tbody></table> 				<!--[if mso]> 				</td> 				<![endif]-->				<!--[if mso]> 				</tr> 				</table> 				<![endif]--> </td> </tr> </tbody> </table></td> </tr> <tr> <td valign='top' id='templateFooter'><table border='0' cellpadding='0' cellspacing='0' width='100%' class='mcnTextBlock' style='min-width:100%;'> <tbody class='mcnTextBlockOuter'> <tr> <td valign='top' class='mcnTextBlockInner' style='padding-top:9px;'> 	<!--[if mso]> 				<table align='left' border='0' cellspacing='0' cellpadding='0' width='100%' style='width:100%;'> 				<tr> 				<![endif]--> 			 				<!--[if mso]> 				<td valign='top' width='600' style='width:600px;'> 				<![endif]--> <table align='left' border='0' cellpadding='0' cellspacing='0' style='max-width:100%; min-width:100%;' width='100%' class='mcnTextContentContainer'> <tbody><tr><td valign='top' class='mcnTextContent' style='padding-top:0; padding-right:18px; padding-bottom:9px; padding-left:18px;'><a href='https://www.nordfincapital.com/' target='_blank'>Nordfin Capital Oy</a><br>  Y-tunnus:2701303-9 &nbsp;&nbsp;<br> © NordFin Capital OY </td> </tr> </tbody></table> 				<!--[if mso]> 				</td> 				<![endif]-->				<!--[if mso]> 				</tr> 				</table> 				<![endif]--> </td> </tr> </tbody> </table></td> </tr> </table> <!--[if (gte mso 9)|(IE)]> </td> </tr> </table> <![endif]--> <!-- // END TEMPLATE --> </td> </tr> </table> </center> </body> </html> ";
                }
                else
                {
                    sBody = "<!doctype html> <html xmlns='http://www.w3.org/1999/xhtml' xmlns:v='urn:schemas-microsoft-com:vml' xmlns:o='urn:schemas-microsoft-com:office:office'> <head> <!-- NAME: 1 COLUMN --> <!--[if gte mso 15]> <xml> <o:OfficeDocumentSettings> <o:AllowPNG/> <o:PixelsPerInch>96</o:PixelsPerInch> </o:OfficeDocumentSettings> </xml> <![endif]--> <meta charset='UTF-8'> <meta http-equiv='X-UA-Compatible' content='IE=edge'> <meta name='viewport' content='width=device-width, initial-scale=1'> <title>Nordfin</title><style type='text/css'> 		p{ 			margin:10px 0; 			padding:0; 		} 		table{ 			border-collapse:collapse; 		} 		h1,h2,h3,h4,h5,h6{ 			display:block; 			margin:0; 			padding:0; 		} 		img,a img{ 			border:0; 			height:auto; 			outline:none; 			text-decoration:none; 		} 		body,#bodyTable,#bodyCell{ 			height:100%; 			margin:0; 			padding:0; 			width:100%; 		} 		#outlook a{ 			padding:0; 		} 		img{ 			-ms-interpolation-mode:bicubic; 		} 		table{ 			mso-table-lspace:0pt; 			mso-table-rspace:0pt; 		} 		.ReadMsgBody{ 			width:100%; 		} 		p,a,li,td,blockquote{ 			mso-line-height-rule:exactly; 		} 		a[href^=tel],a[href^=sms]{ 			color:inherit; 			cursor:default; 			text-decoration:none; 		} 		p,a,li,td,body,table,blockquote{ 			-ms-text-size-adjust:100%; 			-webkit-text-size-adjust:100%; 		} 		a[x-apple-data-detectors]{ 			color:inherit !important; 			text-decoration:none !important; 			font-size:inherit !important; 			font-family:inherit !important; 			font-weight:inherit !important; 			line-height:inherit !important; 		} 		#bodyCell{ 			padding:10px; 		} 		.templateContainer{ 			max-width:600px !important; 		} 		a.mcnButton{ 			display:block; 		} 		.mcnImage,.mcnRetinaImage{ 			vertical-align:bottom; 		} 		.mcnTextContent{ 			word-break:break-word; 		} 		.mcnTextContent img{ 			height:auto !important; 		} 		.mcnDividerBlock{ 			table-layout:fixed !important; 		} 		body,#bodyTable{ 			background-color:#c0c0c0; 		} 		#bodyCell{ 			border-top:0; 		} 		.templateContainer{ 			border:0; 		} 		h1{ 			color:#202020; 			font-family:Helvetica; 			font-size:26px; 			font-style:normal; 			font-weight:bold; 			line-height:125%; 			letter-spacing:normal; 			text-align:left; 		} 		h2{ 			color:#202020; 			font-family:Helvetica; 			font-size:22px; 			font-style:normal; 			font-weight:bold; 			line-height:125%; 			letter-spacing:normal; 			text-align:left; 		} 		h3{ 			color:#202020; 			font-family:Helvetica; 			font-size:20px; 			font-style:normal; 			font-weight:bold; 			line-height:125%; 			letter-spacing:normal; 			text-align:left; 		} 		h4{ 			color:#202020; 			font-family:Helvetica; 			font-size:18px; 			font-style:normal; 			font-weight:bold; 			line-height:125%; 			letter-spacing:normal; 			text-align:left; 		} 		#templatePreheader{ 			background-color:#transparent; 			background-image:none; 			background-repeat:no-repeat; 			background-position:center; 			background-size:cover; 			border-top:0; 			border-bottom:0; 			padding-top:0px; 			padding-bottom:0px; 		} 		#templatePreheader .mcnTextContent,#templatePreheader .mcnTextContent p{ 			color:#656565; 			font-family:Helvetica; 			font-size:12px; 			line-height:150%; 			text-align:left; 		} 		#templatePreheader .mcnTextContent a,#templatePreheader .mcnTextContent p a{ 			color:#656565; 			font-weight:normal; 			text-decoration:underline; 		} 		#templateHeader{ 			background-color:#293746; 			background-image:none; 			background-repeat:no-repeat; 			background-position:center; 			background-size:cover; 			border-top:0; 			border-bottom:0; 			padding-top:20px; 			padding-bottom:20px; 		} 		#templateHeader .mcnTextContent,#templateHeader .mcnTextContent p{ 			color:#ffffff; 			font-family:Helvetica; 			font-size:16px; 			line-height:150%; 			text-align:left; 		} 		#templateHeader .mcnTextContent a,#templateHeader .mcnTextContent p a{ 			color:#ffffff; 			font-weight:normal; 			text-decoration:underline; 		} 		#templateBody{ 			background-color:#3e4b64; 			background-image:none; 			background-repeat:no-repeat; 			background-position:center; 			background-size:cover; 			border-top:0; 			border-bottom:2px solid #51627c; 			padding-top:10px; 			padding-bottom:10px; 		} 		#templateBody .mcnTextContent,#templateBody .mcnTextContent p{ 			color:#ffffff; 			font-family:Helvetica, Arial, Verdana, sans-serif; 			font-size:16px; 			line-height:150%; 			text-align:left; 		} 		#templateBody .mcnTextContent a,#templateBody .mcnTextContent p a{ 			color:#ffffff; 			font-weight:normal; 			text-decoration:underline; 		} 		#templateFooter{ 			background-color:#2c3850; 			background-image:none; 			background-repeat:no-repeat; 			background-position:center; 			background-size:cover; 			border-top:0; 			border-bottom:0; 			padding-top:9px; 			padding-bottom:9px; 		} 		#templateFooter .mcnTextContent,#templateFooter .mcnTextContent p{ 			color:#c5c5c5; 			font-family:Helvetica; 			font-size:12px; 			line-height:150%; 			text-align:center; 		} 		#templateFooter .mcnTextContent a,#templateFooter .mcnTextContent p a{ 			color:#ffffff; 			font-weight:normal; 			text-decoration:underline; 		} 	@media only screen and (min-width:768px){ 		.templateContainer{ 			width:600px !important; 		} }	@media only screen and (max-width: 480px){ 		body,table,td,p,a,li,blockquote{ 			-webkit-text-size-adjust:none !important; 		} }	@media only screen and (max-width: 480px){ 		body{ 			width:100% !important; 			min-width:100% !important; 		} }	@media only screen and (max-width: 480px){ 		#bodyCell{ 			padding-top:10px !important; 		} }	@media only screen and (max-width: 480px){ 		.mcnRetinaImage{ 			max-width:100% !important; 		} }	@media only screen and (max-width: 480px){ 		.mcnImage{ 			width:100% !important; 		} }	@media only screen and (max-width: 480px){ 		.mcnCartContainer,.mcnCaptionTopContent,.mcnRecContentContainer,.mcnCaptionBottomContent,.mcnTextContentContainer,.mcnBoxedTextContentContainer,.mcnImageGroupContentContainer,.mcnCaptionLeftTextContentContainer,.mcnCaptionRightTextContentContainer,.mcnCaptionLeftImageContentContainer,.mcnCaptionRightImageContentContainer,.mcnImageCardLeftTextContentContainer,.mcnImageCardRightTextContentContainer,.mcnImageCardLeftImageContentContainer,.mcnImageCardRightImageContentContainer{ 			max-width:100% !important; 			width:100% !important; 		} }	@media only screen and (max-width: 480px){ 		.mcnBoxedTextContentContainer{ 			min-width:100% !important; 		} }	@media only screen and (max-width: 480px){ 		.mcnImageGroupContent{ 			padding:9px !important; 		} }	@media only screen and (max-width: 480px){ 		.mcnCaptionLeftContentOuter .mcnTextContent,.mcnCaptionRightContentOuter .mcnTextContent{ 			padding-top:9px !important; 		} }	@media only screen and (max-width: 480px){ 		.mcnImageCardTopImageContent,.mcnCaptionBottomContent:last-child .mcnCaptionBottomImageContent,.mcnCaptionBlockInner .mcnCaptionTopContent:last-child .mcnTextContent{ 			padding-top:18px !important; 		} }	@media only screen and (max-width: 480px){ 		.mcnImageCardBottomImageContent{ 			padding-bottom:9px !important; 		} }	@media only screen and (max-width: 480px){ 		.mcnImageGroupBlockInner{ 			padding-top:0 !important; 			padding-bottom:0 !important; 		} }	@media only screen and (max-width: 480px){ 		.mcnImageGroupBlockOuter{ 			padding-top:9px !important; 			padding-bottom:9px !important; 		} }	@media only screen and (max-width: 480px){ 		.mcnTextContent,.mcnBoxedTextContentColumn{ 			padding-right:18px !important; 			padding-left:18px !important; 		} }	@media only screen and (max-width: 480px){ 		.mcnImageCardLeftImageContent,.mcnImageCardRightImageContent{ 			padding-right:18px !important; 			padding-bottom:0 !important; 			padding-left:18px !important; 		} }	@media only screen and (max-width: 480px){ 		.mcpreview-image-uploader{ 			display:none !important; 			width:100% !important; 		} }	@media only screen and (max-width: 480px){ 		h1{ 			font-size:22px !important; 			line-height:125% !important; 		} }	@media only screen and (max-width: 480px){ 		h2{ 			font-size:20px !important; 			line-height:125% !important; 		} }	@media only screen and (max-width: 480px){ 		h3{ 			font-size:18px !important; 			line-height:125% !important; 		} }	@media only screen and (max-width: 480px){ 		h4{ 			font-size:16px !important; 			line-height:150% !important; 		} }	@media only screen and (max-width: 480px){ 		.mcnBoxedTextContentContainer .mcnTextContent,.mcnBoxedTextContentContainer .mcnTextContent p{ 			font-size:14px !important; 			line-height:150% !important; 		} }	@media only screen and (max-width: 480px){ 		#templatePreheader{ 			display:block !important; 		} }	@media only screen and (max-width: 480px){ 		#templatePreheader .mcnTextContent,#templatePreheader .mcnTextContent p{ 			font-size:14px !important; 			line-height:150% !important; 		} }	@media only screen and (max-width: 480px){ 		#templateHeader .mcnTextContent,#templateHeader .mcnTextContent p{ 			font-size:16px !important; 			line-height:150% !important; 		} }	@media only screen and (max-width: 480px){ 		#templateBody .mcnTextContent,#templateBody .mcnTextContent p{ 			font-size:16px !important; 			line-height:150% !important; 		} }	@media only screen and (max-width: 480px){ 		#templateFooter .mcnTextContent,#templateFooter .mcnTextContent p{ 			font-size:14px !important; 			line-height:150% !important; 		} }</style></head> <body> <center> <table align='center' border='0' cellpadding='0' cellspacing='0' height='100%' width='100%' id='bodyTable'> <tr> <td align='center' valign='top' id='bodyCell'> <!-- BEGIN TEMPLATE // --> <!--[if (gte mso 9)|(IE)]> <table align='center' border='0' cellspacing='0' cellpadding='0' width='600' style='width:600px;'> <tr> <td align='center' valign='top' width='600' style='width:600px;'> <![endif]--> <table border='0' cellpadding='0' cellspacing='0' width='100%' class='templateContainer'> <tr> <td valign='top' id='templatePreheader'><table border='0' cellpadding='0' cellspacing='0' width='100%' class='mcnTextBlock' style='min-width:100%;'> <tbody class='mcnTextBlockOuter'> <tr> <td valign='top' class='mcnTextBlockInner' style='padding-top:9px;'> 	<!--[if mso]> 				<table align='left' border='0' cellspacing='0' cellpadding='0' width='100%' style='width:100%;'> 				<tr> 				<![endif]--> 			 				<!--[if mso]> 				<td valign='top' width='600' style='width:600px;'> 				<![endif]--> <table align='left' border='0' cellpadding='0' cellspacing='0' style='max-width:100%; min-width:100%;' width='100%' class='mcnTextContentContainer'> <tbody><tr><td valign='top' class='mcnTextContent' style='padding-top:0; padding-right:18px; padding-bottom:9px; padding-left:18px;'> </td> </tr> </tbody></table> 				<!--[if mso]> 				</td> 				<![endif]-->				<!--[if mso]> 				</tr> 				</table> 				<![endif]--> </td> </tr> </tbody> </table></td> </tr> <tr> <td valign='top' id='templateHeader'><table border='0' cellpadding='0' cellspacing='0' width='100%' class='mcnImageBlock' style='min-width:100%;'> <tbody class='mcnImageBlockOuter'> <tr> <td valign='top' style='padding:10px' class='mcnImageBlockInner'> <table align='left' width='100%' border='0' cellpadding='0' cellspacing='0' class='mcnImageContentContainer' style='min-width:100%;'> <tbody><tr> <td class='mcnImageContent' valign='top' style='padding-right: 10px; padding-left: 10px; padding-top: 0; padding-bottom: 0;'><a href='https://www.nordfincapital.com/' title={ class={ target='_blank'> <img align='left' alt={ src='https://cenk.co/images/Nordfin.png' width='250' style='max-width:500px; padding-bottom: 0; display: inline !important; vertical-align: bottom;' class='mcnRetinaImage'> </a></td> </tr> </tbody></table> </td> </tr> </tbody> </table></td> </tr> <tr> <td valign='top' id='templateBody'><table border='0' cellpadding='0' cellspacing='0' width='100%' class='mcnTextBlock' style='min-width:100%;'> <tbody class='mcnTextBlockOuter'> <tr> <td valign='top' class='mcnTextBlockInner' style='padding-top:9px;'> 	<!--[if mso]> 				<table align='left' border='0' cellspacing='0' cellpadding='0' width='100%' style='width:100%;'> 				<tr> 				<![endif]--> 			 				<!--[if mso]> 				<td valign='top' width='600' style='width:600px;'> 				<![endif]--> <table align='left' border='0' cellpadding='0' cellspacing='0' style='max-width:100%; min-width:100%;' width='100%' class='mcnTextContentContainer'> <tbody><tr><td valign='top' class='mcnTextContent' style='padding-top:0; padding-right:18px; padding-bottom:9px; padding-left:18px;'>Hello there,<br> <br> Good news! Your email has been updated :-)<br> <br> Click on the link below to finish setting up your new email address.<br> Link is valid for 24 hours<br> &nbsp; </td> </tr> </tbody></table> 				<!--[if mso]> 				</td> 				<![endif]-->				<!--[if mso]> 				</tr> 				</table> 				<![endif]--> </td> </tr> </tbody> </table><table border='0' cellpadding='0' cellspacing='0' width='100%' class='mcnButtonBlock' style='min-width:100%;'> <tbody class='mcnButtonBlockOuter'> <tr> <td style='padding-top:0; padding-right:18px; padding-bottom:18px; padding-left:18px;' valign='top' align='center' class='mcnButtonBlockInner'> <table border='0' cellpadding='0' cellspacing='0' width='100%' class='mcnButtonContentContainer' style='border-collapse: separate !important;border-radius: 7px;background-color: #FFB100;'> <tbody> <tr> <td align='center' valign='middle' class='mcnButtonContent' style='font-family: &quot;Helvetica Neue&quot;, Helvetica, Arial, Verdana, sans-serif; font-size: 16px; padding: 18px;'> <a class='mcnButton ' title='Update My Email Address →' href='" + urlRandomNumber + "' target='_blank' style='font-weight: bold;letter-spacing: normal;line-height: 100%;text-align: center;text-decoration: none;color: #232D41;'>Update My Email Address →</a> </td> </tr> </tbody> </table> </td> </tr> </tbody> </table><table border='0' cellpadding='0' cellspacing='0' width='100%' class='mcnTextBlock' style='min-width:100%;'> <tbody class='mcnTextBlockOuter'> <tr> <td valign='top' class='mcnTextBlockInner' style='padding-top:9px;'> 	<!--[if mso]> 				<table align='left' border='0' cellspacing='0' cellpadding='0' width='100%' style='width:100%;'> 				<tr> 				<![endif]--> 			 				<!--[if mso]> 				<td valign='top' width='600' style='width:600px;'> 				<![endif]--> <table align='left' border='0' cellpadding='0' cellspacing='0' style='max-width:100%; min-width:100%;' width='100%' class='mcnTextContentContainer'> <tbody><tr><td valign='top' class='mcnTextContent' style='padding-top:0; padding-right:18px; padding-bottom:9px; padding-left:18px;'> Best Regards,<br> Nordfin Capital Team<br> &nbsp; </td> </tr> </tbody></table> 				<!--[if mso]> 				</td> 				<![endif]-->				<!--[if mso]> 				</tr> 				</table> 				<![endif]--> </td> </tr> </tbody> </table></td> </tr> <tr> <td valign='top' id='templateFooter'><table border='0' cellpadding='0' cellspacing='0' width='100%' class='mcnTextBlock' style='min-width:100%;'> <tbody class='mcnTextBlockOuter'> <tr> <td valign='top' class='mcnTextBlockInner' style='padding-top:9px;'> 	<!--[if mso]> 				<table align='left' border='0' cellspacing='0' cellpadding='0' width='100%' style='width:100%;'> 				<tr> 				<![endif]--> 			 				<!--[if mso]> 				<td valign='top' width='600' style='width:600px;'> 				<![endif]--> <table align='left' border='0' cellpadding='0' cellspacing='0' style='max-width:100%; min-width:100%;' width='100%' class='mcnTextContentContainer'> <tbody><tr><td valign='top' class='mcnTextContent' style='padding-top:0; padding-right:18px; padding-bottom:9px; padding-left:18px;'><a href='https://www.nordfincapital.com/' target='_blank'>Nordfin Capital Group AB</a><br>  Org.nr 559123-4900 &nbsp;&nbsp;<br> © NordFin Capital Group AB </td> </tr> </tbody></table> 				<!--[if mso]> 				</td> 				<![endif]-->				<!--[if mso]> 				</tr> 				</table> 				<![endif]--> </td> </tr> </tbody> </table></td> </tr> </table> <!--[if (gte mso 9)|(IE)]> </td> </tr> </table> <![endif]--> <!-- // END TEMPLATE --> </td> </tr> </table> </center> </body> </html> ";
                }
                mail.From = new MailAddress(sEmail);

                mail.To.Add(txtNewEmail.Text);

                mail.Subject = "Email confirmation";

                mail.Body = sBody;
                mail.IsBodyHtml = true;


                SmtpServer.Port = Convert.ToInt32(sEmailPort);

                SmtpServer.Credentials = new System.Net.NetworkCredential(sEmail, sEmailPassword);

                SmtpServer.EnableSsl = true;    



                SmtpServer.Send(mail);



                UserEmail userEmail = new UserEmail();
                userEmail.token = sRandomNumber;
                userEmail.Email = txtNewEmail.Text;


                IAccountSettingsPresentationBusinessLayer objAccountSettingsList = new AccountSettingsBusinessLayer();
                int iResult = objAccountSettingsList.UpdateUserEmail(ClientSession.UserID, userEmail);
               
                pnlInfo.Visible = true;
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "panelInfo", " $('.featureNotAvailableBGpnl').toggleClass('hidden');", true);
            }

            catch (Exception ex)

            {



            }
        }


        public string RandomString()
        {
            Random random = new Random();
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, 25)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }
}