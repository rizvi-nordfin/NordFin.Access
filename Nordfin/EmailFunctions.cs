﻿using Nordfin.workflow.Entity;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text.RegularExpressions;
using System.Web;

namespace Nordfin
{
    public class EmailFunctions
    {
        public bool GetInvoiceExsits(string ClientName,string FileName,string InvoiceNumber,string sCollectionStatus,bool bArchive)
        {
            string subFolderName = ClientName.Substring(ClientName.LastIndexOf("/") + 1) + Utilities.Execute(InvoiceNumber);
            FTPFileProcess fileProcess = new FTPFileProcess();
            string sFileExt = System.Configuration.ConfigurationManager.AppSettings["FileExtension"].ToString();
            string sFileName = FileName + "_" + sCollectionStatus + InvoiceNumber + "_" + "inv" + "." + sFileExt;
            return fileProcess.FileDownload(ClientName, subFolderName, sFileName, out string ResultFile, bArchive);
        }

        public string GetFileName(string FileName,string InvoiceNumber,string CollectionSatus,out bool bExsits)
        {
            string sFileExt = System.Configuration.ConfigurationManager.AppSettings["FileExtension"].ToString();
            string sFileName = FileName + "_" + CollectionSatus + InvoiceNumber + "_" + "inv" + "." + sFileExt;
            if (File.Exists(HttpContext.Current.Server.MapPath("~/Documents/" + HttpContext.Current.Session.SessionID + "/" + sFileName)))
                bExsits = true;
            else
                bExsits = false;
            return sFileName;
        }

        public bool SendMail(string toEmail,string EmailHeader,string EmailBody,List<PDFMultiDownload> multiDownloads)
        {
            try
            {
                string sEmail = System.Configuration.ConfigurationManager.AppSettings["Email"].ToString();
                string sEmailPassword = System.Configuration.ConfigurationManager.AppSettings["EmailPassword"].ToString();
                string sEmailPort = System.Configuration.ConfigurationManager.AppSettings["EmailPort"].ToString();
                using (SmtpClient SmtpServer = new SmtpClient("in-v3.mailjet.com", 587))
                {
                    SmtpServer.Credentials = new NetworkCredential("1867ce5eecca8ce8ab72cda1fefe8d47", "3e70a81b01b2926189dd8875294b3c13");

                    using (System.Net.Mail.MailMessage mail = new System.Net.Mail.MailMessage())
                    {
                        string sBody = "";

                        sBody = EmailBody.Replace("\n", "<br>");// "<!doctype html> <html xmlns='http://www.w3.org/1999/xhtml' xmlns:v='urn:schemas-microsoft-com:vml' xmlns:o='urn:schemas-microsoft-com:office:office'> <head> <!-- NAME: 1 COLUMN --> <!--[if gte mso 15]> <xml> <o:OfficeDocumentSettings> <o:AllowPNG/> <o:PixelsPerInch>96</o:PixelsPerInch> </o:OfficeDocumentSettings> </xml> <![endif]--> <meta charset='UTF-8'> <meta http-equiv='X-UA-Compatible' content='IE=edge'> <meta name='viewport' content='width=device-width, initial-scale=1'> <title>Nordfin</title><style type='text/css'> 		p{ 			margin:10px 0; 			padding:0; 		} 		table{ 			border-collapse:collapse;     background: #323e53;		} 		h1,h2,h3,h4,h5,h6{ 			display:block; 			margin:0; 			padding:0; 		} 		img,a img{ 			border:0; 			height:auto; 			outline:none; 			text-decoration:none; 		} 		body,#bodyTable,#bodyCell{ 			height:100%; 			margin:0; 			padding:0; 			width:100%; 		} 		#outlook a{ 			padding:0; 		} 		img{ 			-ms-interpolation-mode:bicubic; 		} 		table{ 			mso-table-lspace:0pt; 			mso-table-rspace:0pt; 		} 		.ReadMsgBody{ 			width:100%; 		} 		p,a,li,td,blockquote{ 			mso-line-height-rule:exactly; 		} 		a[href^=tel],a[href^=sms]{ 			color:inherit; 			cursor:default; 			text-decoration:none; 		} 		p,a,li,td,body,table,blockquote{ 			-ms-text-size-adjust:100%; 			-webkit-text-size-adjust:100%; 		} 		a[x-apple-data-detectors]{ 			color:inherit !important; 			text-decoration:none !important; 			font-size:inherit !important; 			font-family:inherit !important; 			font-weight:inherit !important; 			line-height:inherit !important; 		} 		#bodyCell{ 			padding:10px; 		} 		.templateContainer{ 			max-width:600px !important; 		} 		a.mcnButton{ 			display:block; 		} 		.mcnImage,.mcnRetinaImage{ 			vertical-align:bottom; 		} 		.mcnTextContent{ 			word-break:break-word; 		} 		.mcnTextContent img{ 			height:auto !important; 		} 		.mcnDividerBlock{ 			table-layout:fixed !important; 		} 		body,#bodyTable{ 			background-color:#c0c0c0; 		} 		#bodyCell{ 			border-top:0; 		} 		.templateContainer{ 			border:0; 		} 		h1{ 			color:#202020; 			font-family:Helvetica; 			font-size:26px; 			font-style:normal; 			font-weight:bold; 			line-height:125%; 			letter-spacing:normal; 			text-align:left; 		} 		h2{ 			color:#202020; 			font-family:Helvetica; 			font-size:22px; 			font-style:normal; 			font-weight:bold; 			line-height:125%; 			letter-spacing:normal; 			text-align:left; 		} 		h3{ 			color:#202020; 			font-family:Helvetica; 			font-size:20px; 			font-style:normal; 			font-weight:bold; 			line-height:125%; 			letter-spacing:normal; 			text-align:left; 		} 		h4{ 			color:#202020; 			font-family:Helvetica; 			font-size:18px; 			font-style:normal; 			font-weight:bold; 			line-height:125%; 			letter-spacing:normal; 			text-align:left; 		} 		#templatePreheader{ 			background-color:#transparent; 			background-image:none; 			background-repeat:no-repeat; 			background-position:center; 			background-size:cover; 			border-top:0; 			border-bottom:0; 			padding-top:0px; 			padding-bottom:0px; 		} 		#templatePreheader .mcnTextContent,#templatePreheader .mcnTextContent p{ 			color:#656565; 			font-family:Helvetica; 			font-size:12px; 			line-height:150%; 			text-align:left; 		} 		#templatePreheader .mcnTextContent a,#templatePreheader .mcnTextContent p a{ 			color:#656565; 			font-weight:normal; 			text-decoration:underline; 		} 		#templateHeader{ 			background-color:#293746; 			background-image:none; 			background-repeat:no-repeat; 			background-position:center; 			background-size:cover; 			border-top:0; 			border-bottom:0; 			padding-top:20px; 			padding-bottom:20px; 		} 		#templateHeader .mcnTextContent,#templateHeader .mcnTextContent p{ 			color:#ffffff; 			font-family:Helvetica; 			font-size:16px; 			line-height:150%; 			text-align:left; 		} 		#templateHeader .mcnTextContent a,#templateHeader .mcnTextContent p a{ 			color:#ffffff; 			font-weight:normal; 			text-decoration:underline; 		} 		#templateBody{ 			background-color:#3e4b64; 			background-image:none; 			background-repeat:no-repeat; 			background-position:center; 			background-size:cover; 			border-top:0; 			border-bottom:2px solid #51627c; 			padding-top:10px; 			padding-bottom:10px; 		} 		#templateBody .mcnTextContent,#templateBody .mcnTextContent p{ 			color:#ffffff; 			font-family:Helvetica, Arial, Verdana, sans-serif; 			font-size:16px; 			line-height:150%; 			text-align:left; 		} 		#templateBody .mcnTextContent a,#templateBody .mcnTextContent p a{ 			color:#ffffff; 			font-weight:normal; 			text-decoration:underline; 		} 		#templateFooter{ 			background-color:#2c3850; 			background-image:none; 			background-repeat:no-repeat; 			background-position:center; 			background-size:cover; 			border-top:0; 			border-bottom:0; 			padding-top:9px; 			padding-bottom:9px; 		} 		#templateFooter .mcnTextContent,#templateFooter .mcnTextContent p{ 			color:#c5c5c5; 			font-family:Helvetica; 			font-size:12px; 			line-height:150%; 			text-align:center; 		} 		#templateFooter .mcnTextContent a,#templateFooter .mcnTextContent p a{ 			color:#ffffff; 			font-weight:normal; 			text-decoration:underline; 		} 	@media only screen and (min-width:768px){ 		.templateContainer{ 			width:600px !important; 		} }	@media only screen and (max-width: 480px){ 		body,table,td,p,a,li,blockquote{ 			-webkit-text-size-adjust:none !important; 		} }	@media only screen and (max-width: 480px){ 		body{ 			width:100% !important; 			min-width:100% !important; 		} }	@media only screen and (max-width: 480px){ 		#bodyCell{ 			padding-top:10px !important; 		} }	@media only screen and (max-width: 480px){ 		.mcnRetinaImage{ 			max-width:100% !important; 		} }	@media only screen and (max-width: 480px){ 		.mcnImage{ 			width:100% !important; 		} }	@media only screen and (max-width: 480px){ 		.mcnCartContainer,.mcnCaptionTopContent,.mcnRecContentContainer,.mcnCaptionBottomContent,.mcnTextContentContainer,.mcnBoxedTextContentContainer,.mcnImageGroupContentContainer,.mcnCaptionLeftTextContentContainer,.mcnCaptionRightTextContentContainer,.mcnCaptionLeftImageContentContainer,.mcnCaptionRightImageContentContainer,.mcnImageCardLeftTextContentContainer,.mcnImageCardRightTextContentContainer,.mcnImageCardLeftImageContentContainer,.mcnImageCardRightImageContentContainer{ 			max-width:100% !important; 			width:100% !important; 		} }	@media only screen and (max-width: 480px){ 		.mcnBoxedTextContentContainer{ 			min-width:100% !important; 		} }	@media only screen and (max-width: 480px){ 		.mcnImageGroupContent{ 			padding:9px !important; 		} }	@media only screen and (max-width: 480px){ 		.mcnCaptionLeftContentOuter .mcnTextContent,.mcnCaptionRightContentOuter .mcnTextContent{ 			padding-top:9px !important; 		} }	@media only screen and (max-width: 480px){ 		.mcnImageCardTopImageContent,.mcnCaptionBottomContent:last-child .mcnCaptionBottomImageContent,.mcnCaptionBlockInner .mcnCaptionTopContent:last-child .mcnTextContent{ 			padding-top:18px !important; 		} }	@media only screen and (max-width: 480px){ 		.mcnImageCardBottomImageContent{ 			padding-bottom:9px !important; 		} }	@media only screen and (max-width: 480px){ 		.mcnImageGroupBlockInner{ 			padding-top:0 !important; 			padding-bottom:0 !important; 		} }	@media only screen and (max-width: 480px){ 		.mcnImageGroupBlockOuter{ 			padding-top:9px !important; 			padding-bottom:9px !important; 		} }	@media only screen and (max-width: 480px){ 		.mcnTextContent,.mcnBoxedTextContentColumn{ 			padding-right:18px !important; 			padding-left:18px !important; 		} }	@media only screen and (max-width: 480px){ 		.mcnImageCardLeftImageContent,.mcnImageCardRightImageContent{ 			padding-right:18px !important; 			padding-bottom:0 !important; 			padding-left:18px !important; 		} }	@media only screen and (max-width: 480px){ 		.mcpreview-image-uploader{ 			display:none !important; 			width:100% !important; 		} }	@media only screen and (max-width: 480px){ 		h1{ 			font-size:22px !important; 			line-height:125% !important; 		} }	@media only screen and (max-width: 480px){ 		h2{ 			font-size:20px !important; 			line-height:125% !important; 		} }	@media only screen and (max-width: 480px){ 		h3{ 			font-size:18px !important; 			line-height:125% !important; 		} }	@media only screen and (max-width: 480px){ 		h4{ 			font-size:16px !important; 			line-height:150% !important; 		} }	@media only screen and (max-width: 480px){ 		.mcnBoxedTextContentContainer .mcnTextContent,.mcnBoxedTextContentContainer .mcnTextContent p{ 			font-size:14px !important; 			line-height:150% !important; 		} }	@media only screen and (max-width: 480px){ 		#templatePreheader{ 			display:block !important; 		} }	@media only screen and (max-width: 480px){ 		#templatePreheader .mcnTextContent,#templatePreheader .mcnTextContent p{ 			font-size:14px !important; 			line-height:150% !important; 		} }	@media only screen and (max-width: 480px){ 		#templateHeader .mcnTextContent,#templateHeader .mcnTextContent p{ 			font-size:16px !important; 			line-height:150% !important; 		} }	@media only screen and (max-width: 480px){ 		#templateBody .mcnTextContent,#templateBody .mcnTextContent p{ 			font-size:16px !important; 			line-height:150% !important; 		} }	@media only screen and (max-width: 480px){ 		#templateFooter .mcnTextContent,#templateFooter .mcnTextContent p{ 			font-size:14px !important; 			line-height:150% !important; 		} }</style></head> <body> <center> <table align='center' border='0' cellpadding='0' cellspacing='0' height='100%' width='100%' id='bodyTable'> <tr> <td align='center' valign='top' id='bodyCell'> <!-- BEGIN TEMPLATE // --> <!--[if (gte mso 9)|(IE)]> <table align='center' border='0' cellspacing='0' cellpadding='0' width='600' style='width:600px;'> <tr> <td align='center' valign='top' width='600' style='width:600px;'> <![endif]--> <table border='0' cellpadding='0' cellspacing='0' width='100%' class='templateContainer'> <tr> <td valign='top' id='templatePreheader'><table border='0' cellpadding='0' cellspacing='0' width='100%' class='mcnTextBlock' style='min-width:100%;'> <tbody class='mcnTextBlockOuter'> <tr> <td valign='top' class='mcnTextBlockInner' style='padding-top:9px;'> 	<!--[if mso]> 				<table align='left' border='0' cellspacing='0' cellpadding='0' width='100%' style='width:100%;'> 				<tr> 				<![endif]--> 			 				<!--[if mso]> 				<td valign='top' width='600' style='width:600px;'> 				<![endif]--> <table align='left' border='0' cellpadding='0' cellspacing='0' style='max-width:100%; min-width:100%;' width='100%' class='mcnTextContentContainer'> <tbody><tr><td valign='top' class='mcnTextContent' style='padding-top:0; padding-right:18px; padding-bottom:9px; padding-left:18px;'> </td> </tr> </tbody></table> 				<!--[if mso]> 				</td> 				<![endif]-->				<!--[if mso]> 				</tr> 				</table> 				<![endif]--> </td> </tr> </tbody> </table></td> </tr> <tr> <td valign='top' id='templateHeader'><table border='0' cellpadding='0' cellspacing='0' width='100%' class='mcnImageBlock' style='min-width:100%;'> <tbody class='mcnImageBlockOuter'> <tr> <td valign='top' style='padding:10px' class='mcnImageBlockInner'> <table align='left' width='100%' border='0' cellpadding='0' cellspacing='0' class='mcnImageContentContainer' style='min-width:100%;'> <tbody><tr> <td class='mcnImageContent' valign='top' style='padding-right: 10px; padding-left: 10px; padding-top: 0; padding-bottom: 0;'><a href='https://www.nordfincapital.com/' title={ class={ target='_blank'> <img align='left' alt={ src='https://cenk.co/images/Nordfin.png' width='250' style='max-width:500px; padding-bottom: 0; display: inline !important; vertical-align: bottom;' class='mcnRetinaImage'> </a></td> </tr> </tbody></table> </td> </tr> </tbody> </table></td> </tr> <tr> <td valign='top' id='templateBody'><table border='0' cellpadding='0' cellspacing='0' width='100%' class='mcnTextBlock' style='min-width:100%;'> <tbody class='mcnTextBlockOuter'> <tr> <td valign='top' class='mcnTextBlockInner' style='padding-top:9px;'> 	<!--[if mso]> 				<table align='left' border='0' cellspacing='0' cellpadding='0' width='100%' style='width:100%;'> 				<tr> 				<![endif]--> 			 				<!--[if mso]> 				<td valign='top' width='600' style='width:600px;'> 				<![endif]--> <table align='left' border='0' cellpadding='0' cellspacing='0' style='max-width:100%; min-width:100%;' width='100%' class='mcnTextContentContainer'> <tbody><tr><td valign='top' class='mcnTextContent' style='padding-top:0; padding-right:18px; padding-bottom:9px; padding-left:18px;'> " + + " <br> &nbsp; </td> </tr> </tbody></table> 				<!--[if mso]> 				</td> 				<![endif]-->				<!--[if mso]> 				</tr> 				</table> 				<![endif]--> </td> </tr> </tbody> </table><table border='0' cellpadding='0' cellspacing='0' width='100%' class='mcnButtonBlock' style='min-width:100%;display:none;'> <tbody class='mcnButtonBlockOuter'> <tr> <td style='padding-top:0; padding-right:18px; padding-bottom:18px; padding-left:18px;' valign='top' align='center' class='mcnButtonBlockInner'>  </td> </tr> </tbody> </table><table border='0' cellpadding='0' cellspacing='0' width='100%' class='mcnTextBlock' style='min-width:100%;'> <tbody class='mcnTextBlockOuter'> <tr> <td valign='top' class='mcnTextBlockInner' style='padding-top:9px;'> 	<!--[if mso]> 				<table align='left' border='0' cellspacing='0' cellpadding='0' width='100%' style='width:100%;'> 				<tr> 				<![endif]--> 			 				<!--[if mso]> 				<td valign='top' width='600' style='width:600px;'> 				<![endif]--> 				<!--[if mso]> 				</td> 				<![endif]-->				<!--[if mso]> 				</tr> 				</table> 				<![endif]--> </td> </tr> </tbody> </table></td> </tr>  				</table> 				<![endif]--> </td> </tr> </tbody> </table></td> </tr> </table> <!--[if (gte mso 9)|(IE)]> </td> </tr> </table> <![endif]--> <!-- // END TEMPLATE --> </td> </tr> </table> </center> </body> </html> ";

                        mail.From = new MailAddress(sEmail, ClientSession.ClientName);

                        mail.To.Add(toEmail.Trim());

                        mail.Subject = EmailHeader;

                        mail.Body = sBody;
                        mail.IsBodyHtml = true;
                        string serverPath = "~/Documents/" + HttpContext.Current.Session.SessionID + "/";
                        for (int i = 0; i < multiDownloads.Count; i++)
                        {
                            Attachment attachment=new Attachment(HttpContext.Current.Server.MapPath(serverPath+ multiDownloads[i].FileName));
                            mail.Attachments.Add(attachment);
                        }
                        SmtpServer.EnableSsl = true;
                        SmtpServer.Send(mail);
                    }

                    return true;
                }

            }

            catch (Exception ex)
            {
                return false;
                //catch the issue
            }
        }

        public static string Execute(string invNumber)
        {
            var r = Regex.Replace(invNumber, "[^0-9]", "");

            var n = int.TryParse(r, out var x) ? x : 0;


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