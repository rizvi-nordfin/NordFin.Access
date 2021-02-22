using Newtonsoft.Json;
using Nordfin.workflow.Business;
using Nordfin.workflow.BusinessLayer;
using Nordfin.workflow.Entity;
using Nordfin.workflow.PresentationBusinessLayer;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Services;
using System.Web.UI;

namespace Nordfin
{
    public partial class frmPaymentInformation : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                ClearSession();

                string InvoiceData = Request.QueryString["InvoiceData"];
                string sRemainAmount = Request.QueryString["Remain"];
                string sOverPaidAmount = Request.QueryString["OverPaid"];
                string InvoiceNum = "";
                hdnFileName.Value = Request.QueryString["FileName"];
                hdnClientName.Value = Request.QueryString["ClientName"];
                hdnInvoiceAmount.Value = sRemainAmount.Trim().Replace(',', '.');

                btnDownload.CommandArgument = Request.QueryString["CombineInvoice"];

                btnEmail.Attributes["combineInvoice"] = Request.QueryString["CombineInvoice"];

                btnEmail.Attributes["collectionStatus"] = Request.QueryString["CollectionStatus"];
                btnEmail.Attributes["custInvoice"] = Request.QueryString["CustomerNumber"];

                if (InvoiceData.Split('|').Length > 1)
                {
                    Session["custNum"] = InvoiceData.Split('|')[0];
                    Session["InvoiceID"] = InvoiceData.Split('|')[1];
                    InvoiceNum = InvoiceData.Split('|')[2];
                }
                else
                {
                    InvoiceNum = InvoiceData;
                }

                if (string.IsNullOrWhiteSpace(Request.QueryString["Customer"]))
                {
                    IManualInvoicePresentationBusinessLayer objManualInvoices = new ManualInvoiceBusinessLayer();
                    var clientId = int.Parse(ClientSession.ClientID);
                    var customerInfo = objManualInvoices.GetCustomerInfoForClient(Session["custNum"]?.ToString(), clientId);
                    hdnCustomerData.Value = JsonConvert.SerializeObject(customerInfo);
                }
                else
                {
                    hdnCustomerData.Value = Request.QueryString["Customer"];
                }
                
                Session["InvoiceNum"] = InvoiceNum;
                lblInvoiceNum.Text = InvoiceNum;
                IPaymentInformationPresentationBusinessLayer objPresentationInfoLayer = new PaymentInformationBusinessLayer();
                var objPaymentNotesList = objPresentationInfoLayer.GetPaymentInformation(InvoiceNum, ClientSession.ClientID);
                PaymentInformation objPaymentInfo = objPaymentNotesList.Item1;
                txtPaymentInfoReference.Text = objPaymentInfo.PaymentReference;
                txtPaymentInfoDelivery.Text = objPaymentInfo.Delivery;
                txtCollectionStatus.Text = objPaymentInfo.Collectionstatus;
                txtCollectionDate.Text = Convert.ToString((objPaymentInfo.CollectionDate == DateTime.MinValue) ? "" : objPaymentInfo.CollectionDate.ToString("yyyy-MM-dd"));
                cboCollectionStop.SelectedValue = objPaymentInfo.Collectionstop;
                txtDueDate.Text = Convert.ToString((objPaymentInfo.DueDate == DateTime.MinValue) ? "" : objPaymentInfo.DueDate.ToString("yyyy-MM-dd"));
                txtCollectionStopUntil.Text = string.IsNullOrEmpty(objPaymentInfo.Collectionstopuntil) ? "" : objPaymentInfo.Collectionstopuntil;
                txtPaymentMethod.Text = string.IsNullOrEmpty(objPaymentInfo.Paymentmethod) ? "" : objPaymentInfo.Paymentmethod;
                hdnPurchased.Value = objPaymentInfo.Purchased;
                cboContested.SelectedValue = cboContested.Items.FindByText(objPaymentInfo.Contested).Value;
                txtContestedDate.Text = objPaymentInfo.ContestedDate;
                if (objPaymentInfo.Contested.ToUpper() == "YES")
                {
                    cboCollectionStop.Attributes.Add("disabled", "disabled");
                    txtCollectionStopUntil.Attributes.Add("disabled", "disabled");
                    txtCollectionStopUntil.Text = "";
                }

                if (Convert.ToDouble(sRemainAmount.Replace(" ", "").Replace(".", ",")) < 0)
                {
                    txtPayoutCreditPayment.Text = sRemainAmount.Trim();
                    hdnPaymentCheck.Value = "credit";
                }

                if (Convert.ToDouble(sOverPaidAmount.Replace(" ", "").Replace(".", ",")) > 0)
                {
                    txtPayoutOverPayment.Text = sOverPaidAmount.Trim();
                    hdnPaymentCheck.Value += "|" + "overpayment";
                }
                hdnDueDate.Value = txtDueDate.Text;
                hdnCollectionStopUntil.Value = txtCollectionStopUntil.Text;
                hdnCollectionStop.Value = cboCollectionStop.SelectedValue;

                if (objPaymentNotesList.Item2.Count > 0)
                {
                    grdNotes.DataSource = objPaymentNotesList.Item2;
                    grdNotes.DataBind();
                }
                else
                {
                    grdNotes.DataSource = new List<string>();
                    grdNotes.DataBind();
                }

                if (ClientSession.AllowManualInvoice)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "showManualInvoiceButton", "$('#btnCreditInvoice').show();", true);
                }
            }

        }

        [WebMethod]
        public static string LoadPaymentTable()
        {
            ClearSession();
            string sInvoiceID = (string)HttpContext.Current.Session["InvoiceID"];
            IPaymentInformationPresentationBusinessLayer objPresentationInfoLayer = new PaymentInformationBusinessLayer();
            PaymentInformationDTO objPaymentsList = objPresentationInfoLayer.GetPaymentInformationPayments(sInvoiceID);

            string jsonPaymentList = new JavaScriptSerializer().Serialize(objPaymentsList.objPaymentsList);

            string jsonFeesList = new JavaScriptSerializer().Serialize(objPaymentsList.objFeesList);

            string jsonInterestList = new JavaScriptSerializer().Serialize(objPaymentsList.objInterestList);

            string jsonPayoutList = new JavaScriptSerializer().Serialize(objPaymentsList.objPayoutList);

            string jsonTransactionList = new JavaScriptSerializer().Serialize(objPaymentsList.objTransList);

            string sResultList = "{\"PaymentsList\" :" + jsonPaymentList + "," + "\"FeesList\" :" + jsonFeesList + "," + "\"InterestList\" :" + jsonInterestList + "," + "\"PayoutList\" :" + jsonPayoutList + "," + "\"TransactionList\" :" + jsonTransactionList + "}";

            return sResultList;
        }


        [WebMethod]
        public static string updateFeeList(string FeeID, string FeeRemainingAmount, string FeesCurrency)
        {
            ClearSession();
            IPaymentInformationPresentationBusinessLayer objPresentationInfoLayer = new PaymentInformationBusinessLayer();
            FessDetails objFeeDetails = new FessDetails();
            objFeeDetails.ClientID = ClientSession.ClientID;
            objFeeDetails.CustNum = (string)HttpContext.Current.Session["custNum"];
            objFeeDetails.Feecurrency = FeesCurrency;
            objFeeDetails.InvoiceID = (string)HttpContext.Current.Session["InvoiceID"];
            objFeeDetails.InvoiceNumber = (string)HttpContext.Current.Session["InvoiceNum"];
            objFeeDetails.PayAmount = FeeRemainingAmount;
            objFeeDetails.PayFeeID = FeeID;
            objFeeDetails.UserName = ClientSession.UserName;
            objFeeDetails.UserID = ClientSession.UserID;
            IList<Fees> resultFees = objPresentationInfoLayer.savePaymentFeeDetails(objFeeDetails);
            string jsonResult = new JavaScriptSerializer().Serialize(resultFees);
            string sResultList = "{\"FeeResult\" :" + jsonResult + "}";
            return sResultList;
        }
        //insertPayout
        [WebMethod]
        public static string insertPayout(string sBankAccount, string sOverpayCreditID)
        {
            ClearSession();
            IPaymentInformationPresentationBusinessLayer objPresentationInfoLayer = new PaymentInformationBusinessLayer();
            Payout objpayout = new Payout();
            objpayout.BankAccount = sBankAccount;
            objpayout.Active = 1;
            objpayout.InvoiceID = Convert.ToInt32(HttpContext.Current.Session["InvoiceID"]);
            objpayout.UserID = Convert.ToInt32(ClientSession.UserID);
            objpayout.OverpayCreditID = Convert.ToInt32(sOverpayCreditID);
            int iResult = objPresentationInfoLayer.savePayout(objpayout);
            string jsonResult = new JavaScriptSerializer().Serialize(iResult);
            string sResultList = "{\"Result\" :" + jsonResult + "}";
            return sResultList;

        }
        [WebMethod]
        public static string updateInterest(string InterestID)
        {
            ClearSession();
            IPaymentInformationPresentationBusinessLayer objPresentationInfoLayer = new PaymentInformationBusinessLayer();
            IList<Interest> resultInterest = objPresentationInfoLayer.updateInterest(Convert.ToInt32(InterestID));
            string jsonResult = new JavaScriptSerializer().Serialize(resultInterest);
            string sResultList = "{\"InterestList\" :" + jsonResult + "}";
            return sResultList;
            //updateInterest
        }

        [WebMethod]
        public static string insertNotes(object name)
        {
            ClearSession();
            var json = new JavaScriptSerializer().Serialize(name);
            Notes objNotes = JsonConvert.DeserializeObject<Notes>(json);
            objNotes.InvoiceID = Convert.ToInt32(HttpContext.Current.Session["InvoiceID"]);
            objNotes.InvoiceNumber = (string)HttpContext.Current.Session["InvoiceNum"];
            objNotes.CustomerID = (string)HttpContext.Current.Session["custNum"];
            objNotes.UserID = Convert.ToInt32(ClientSession.UserID);
            objNotes.ClientID = Convert.ToInt32(ClientSession.ClientID);
            objNotes.UserName = ClientSession.UserName;
            IPaymentInformationPresentationBusinessLayer objPresentationInfoLayer = new PaymentInformationBusinessLayer();
            var objNotesInformation = objPresentationInfoLayer.insertInterest(objNotes);

            string jsonResult = new JavaScriptSerializer().Serialize(objNotesInformation.Item1);
            string jsonNotesListResult = new JavaScriptSerializer().Serialize(objNotesInformation.Item2);
            string sResultList = "{\"NotesInfo\" :" + jsonResult + "," + "\"NotesList\" :" + jsonNotesListResult + "}";
            return sResultList;
            //updateInterest
        }




        [WebMethod]
        public static string PDFDownload(string ClientName, string FileName)
        {
            ClearSession();
            FTPFileProcess fileProcess = new FTPFileProcess();
            string sFileExt = System.Configuration.ConfigurationManager.AppSettings["FileExtension"].ToString();
            string sFileName = FileName + sFileExt;
            string sPDFViewerLink = "";
            bool bResult = false;
            string ResultFile = "";
            string subfolder = ClientName.Substring(ClientName.LastIndexOf("/") + 1) + Utilities.Execute(sFileName.Split('_')[1].Trim());
            if (sFileName != "")
                bResult = fileProcess.FileDownload(ClientName, subfolder, sFileName, out ResultFile);
            if (!bResult)
            {
                sPDFViewerLink = (string)HttpContext.Current.Session["InvoiceNum"];
            }

            string jsonFileName = new JavaScriptSerializer().Serialize(sFileName);
            string jsonViewerLink = new JavaScriptSerializer().Serialize(sPDFViewerLink);
            string jsonSession = new JavaScriptSerializer().Serialize(HttpContext.Current.Session.SessionID);
            string jsonBoolResult = new JavaScriptSerializer().Serialize(bResult);
            if (ResultFile != "")
            {
                jsonFileName = ResultFile;
            }
            string sResultList = "{\"FileName\" :" + jsonFileName + "," + "\"ViewerLink\" :" + jsonViewerLink + "," + "\"SessionID\" :" + jsonSession + "," + "\"BoolResult\" :" + jsonBoolResult + "}";
            return sResultList;



            //updateInterest
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

        [WebMethod]
        public static string RemovePayout(string PayoutID)
        {
            ClearSession();
            IPaymentInformationPresentationBusinessLayer objPresentationInfoLayer = new PaymentInformationBusinessLayer();

            int iResult = objPresentationInfoLayer.removePayout(Convert.ToInt32(PayoutID));
            string jsonResult = new JavaScriptSerializer().Serialize(iResult);
            string sResultList = "{\"Result\" :" + jsonResult + "}";
            return sResultList;
        }

        public static void ClearSession()
        {
            if (ClientSession.ClientID.Trim() == "")
            {
                try
                {

                    string sDirectory = "~/Documents/" + HttpContext.Current.Session.SessionID;
                    if (Directory.Exists(HttpContext.Current.Server.MapPath(sDirectory)))
                    {
                        Directory.Delete(HttpContext.Current.Server.MapPath(sDirectory), true);
                    }
                }
                catch
                {
                    //catch the issue
                }
                HttpContext.Current.Session.Abandon();
                HttpContext.Current.Response.Redirect("frmLogin.aspx");

            }
        }


        protected void ManualInvoice_Click(object sender, EventArgs e)
        {
           
        }

       

        protected void btnPDFDownload_Click(object sender, EventArgs e)
        {
            List<InvoiceDownload> listDownload = InvoiceDownload(btnDownload.CommandArgument.Trim(), btnEmail.Attributes["collectionStatus"]);

            string jsonInvoiceList = new JavaScriptSerializer().Serialize(listDownload);
            Session["InvoiceDownloadList"] = jsonInvoiceList;
            ScriptManager.RegisterStartupScript(this, this.GetType(), "ExportClick", "ExportClick(0,'" + jsonInvoiceList + "');", true);
        }
        private List<InvoiceDownload> InvoiceDownload(string InvoiceNumber, string collectionStatus)
        {
            if (Session["InvoiceDownloadList"] != null)
            {
                Session["InvoiceDownloadList"] = null;
            }
            string sCollectionStatus = "";
            bool bDC = false;
            bool bRemind = false;
            bool bInvoices = false;
            bool bResult = false;
            bool bArchive = false;
            List<InvoiceDownload> invoiceDownloads = new List<InvoiceDownload>();
        fileDownload:
            if (!chkInvoices.Checked && !bInvoices)
            {
                bInvoices = true;
                goto fileDC;
            }
            string subFolderName = hdnClientName.Value.Substring(hdnClientName.Value.LastIndexOf("/") + 1) + Utilities.Execute(InvoiceNumber);
            FTPFileProcess fileProcess = new FTPFileProcess();
            string sFileExt = System.Configuration.ConfigurationManager.AppSettings["FileExtension"].ToString();
            string sFileName = hdnFileName.Value + "_" + sCollectionStatus + InvoiceNumber + "_" + "inv" + "." + sFileExt;


            string ResultFile = "";
            if (sFileName != "")
                bResult = fileProcess.FileDownload(hdnClientName.Value, subFolderName, sFileName, out ResultFile, bArchive);
            bArchive = false;
            InvoiceDownload download = new InvoiceDownload();
            download.InvoiceName = sCollectionStatus;
            download.Status = (bResult) ? "visible" : "hidden";
            download.PDFArchive = "";// hdnArchiveLink.Value + InvoiceNumber;
            invoiceDownloads.Add(download);
        fileDC:
            if (collectionStatus != "")
            {
                if (!bRemind && (collectionStatus.ToUpper() == "DC" || collectionStatus.ToUpper() == "REMIND"))
                {
                    bRemind = true;
                    bArchive = true;
                    sCollectionStatus = "Rem";
                    goto fileDownload;
                }
                else if (!bDC && collectionStatus.ToUpper() == "DC")
                {
                    bDC = true;
                    bArchive = true;
                    sCollectionStatus = "DC";
                    goto fileDownload;
                }

            }

          
            return invoiceDownloads;
        }


        protected void btnDownload_Click(object sender, EventArgs e)
        {

            string sCollectionStatus = "";
            List<PDFMultiDownload> filePathList = new List<PDFMultiDownload>();
            bool bDC = false;
            bool bRemind = false;
            bool bInvoices = false;
            string collectionStatus = btnEmail.Attributes["collectionStatus"];
        fileDownload:
            if (!chkInvoices.Checked && !bInvoices)
            {
                bInvoices = true;
                goto fileDC;
            }
            string sFileExt = System.Configuration.ConfigurationManager.AppSettings["FileExtension"].ToString();
            string sFileName = hdnFileName.Value + "_" + sCollectionStatus + btnDownload.CommandArgument.Trim() + "_" + "inv" + "." + sFileExt;
            PDFMultiDownload multiDownload = new PDFMultiDownload();
            multiDownload.FileType = sCollectionStatus;

            if (File.Exists(Server.MapPath("~/Documents/" + HttpContext.Current.Session.SessionID + "/" + sFileName)))
            {
                multiDownload.FileName = sFileName;
            }
            else
                multiDownload.FileName = "";
            filePathList.Add(multiDownload);


        fileDC:
            if (collectionStatus != "")
            {
                if (collectionStatus.ToUpper() == "DC" && chkDC.Checked && !bDC)
                {
                    bDC = true;
                    sCollectionStatus = "DC";
                    goto fileDownload;
                }
                else if ((collectionStatus.ToUpper() == "DC" || collectionStatus.ToUpper() == "REMIND") && chkRemind.Checked && !bRemind)
                {
                    bRemind = true;
                    sCollectionStatus = "Rem";
                    goto fileDownload;
                }
            }

            string jsonPathList = new JavaScriptSerializer().Serialize(filePathList);
            string jsonCollectionList = (string)Session["InvoiceDownloadList"];
            
            ScriptManager.RegisterStartupScript(this, this.GetType(), "PDFViewerNewTab", "PDFViewer('" + jsonPathList + "','"  + jsonCollectionList + "');", true);
        }


        protected void btnEmail_Click(object sender, EventArgs e)
        {
            IInvoicesPresentationBusinessLayer objInvoicesLayer = new InvoicesBusinessLayer();
            string scustEmail = objInvoicesLayer.GetCustInvoiceEmailID(ClientSession.ClientID, btnEmail.Attributes["custInvoice"].ToString());
            txtCustEmail.Text = scustEmail;
            txtEmailHeader.Text = ClientSession.ClientName + ",Invoice" + " " + btnEmail.Attributes["combineInvoice"];
            txtEmailBody.Text = "Hei, Hi, Hej, Hallo!" + "\n\n" + "Your invoice copy has been attached" + "\n\n" + "Have a great day :-)" + "\n\n" + "Best Regards," + "\n" + ClientSession.ClientName;
            ScriptManager.RegisterStartupScript(this, this.GetType(), "ExportClick", "ExportClick(1);", true);
        }

        protected void btnSend_Click(object sender, EventArgs e)
        {
            try
            {
                SendMail(btnEmail.Attributes["combineInvoice"], txtCustEmail.Text);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "ExportClick", "ExportClick(1,'',true);", true);
            }
            catch
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "ExportClick", "ExportClick(1,'',false);", true);
            }
        }
        public bool SendMail(string InvoiceNumber, string toEmail)
        {
            try
            {



                string sEmail = System.Configuration.ConfigurationManager.AppSettings["Email"].ToString();
                string sEmailPassword = System.Configuration.ConfigurationManager.AppSettings["EmailPassword"].ToString();
                string sEmailPort = System.Configuration.ConfigurationManager.AppSettings["EmailPort"].ToString();

                string collectionStatus = btnEmail.Attributes["collectionStatus"];
                using (SmtpClient SmtpServer = new SmtpClient("in-v3.mailjet.com", 587))
                {
                    SmtpServer.Credentials = new NetworkCredential("1867ce5eecca8ce8ab72cda1fefe8d47", "3e70a81b01b2926189dd8875294b3c13");

                    using (System.Net.Mail.MailMessage mail = new System.Net.Mail.MailMessage())
                    {
                        string sBody = "";

                        sBody = "<!doctype html> <html xmlns='http://www.w3.org/1999/xhtml' xmlns:v='urn:schemas-microsoft-com:vml' xmlns:o='urn:schemas-microsoft-com:office:office'> <head> <!-- NAME: 1 COLUMN --> <!--[if gte mso 15]> <xml> <o:OfficeDocumentSettings> <o:AllowPNG/> <o:PixelsPerInch>96</o:PixelsPerInch> </o:OfficeDocumentSettings> </xml> <![endif]--> <meta charset='UTF-8'> <meta http-equiv='X-UA-Compatible' content='IE=edge'> <meta name='viewport' content='width=device-width, initial-scale=1'> <title>Nordfin</title><style type='text/css'> 		p{ 			margin:10px 0; 			padding:0; 		} 		table{ 			border-collapse:collapse;     background: #323e53;		} 		h1,h2,h3,h4,h5,h6{ 			display:block; 			margin:0; 			padding:0; 		} 		img,a img{ 			border:0; 			height:auto; 			outline:none; 			text-decoration:none; 		} 		body,#bodyTable,#bodyCell{ 			height:100%; 			margin:0; 			padding:0; 			width:100%; 		} 		#outlook a{ 			padding:0; 		} 		img{ 			-ms-interpolation-mode:bicubic; 		} 		table{ 			mso-table-lspace:0pt; 			mso-table-rspace:0pt; 		} 		.ReadMsgBody{ 			width:100%; 		} 		p,a,li,td,blockquote{ 			mso-line-height-rule:exactly; 		} 		a[href^=tel],a[href^=sms]{ 			color:inherit; 			cursor:default; 			text-decoration:none; 		} 		p,a,li,td,body,table,blockquote{ 			-ms-text-size-adjust:100%; 			-webkit-text-size-adjust:100%; 		} 		a[x-apple-data-detectors]{ 			color:inherit !important; 			text-decoration:none !important; 			font-size:inherit !important; 			font-family:inherit !important; 			font-weight:inherit !important; 			line-height:inherit !important; 		} 		#bodyCell{ 			padding:10px; 		} 		.templateContainer{ 			max-width:600px !important; 		} 		a.mcnButton{ 			display:block; 		} 		.mcnImage,.mcnRetinaImage{ 			vertical-align:bottom; 		} 		.mcnTextContent{ 			word-break:break-word; 		} 		.mcnTextContent img{ 			height:auto !important; 		} 		.mcnDividerBlock{ 			table-layout:fixed !important; 		} 		body,#bodyTable{ 			background-color:#c0c0c0; 		} 		#bodyCell{ 			border-top:0; 		} 		.templateContainer{ 			border:0; 		} 		h1{ 			color:#202020; 			font-family:Helvetica; 			font-size:26px; 			font-style:normal; 			font-weight:bold; 			line-height:125%; 			letter-spacing:normal; 			text-align:left; 		} 		h2{ 			color:#202020; 			font-family:Helvetica; 			font-size:22px; 			font-style:normal; 			font-weight:bold; 			line-height:125%; 			letter-spacing:normal; 			text-align:left; 		} 		h3{ 			color:#202020; 			font-family:Helvetica; 			font-size:20px; 			font-style:normal; 			font-weight:bold; 			line-height:125%; 			letter-spacing:normal; 			text-align:left; 		} 		h4{ 			color:#202020; 			font-family:Helvetica; 			font-size:18px; 			font-style:normal; 			font-weight:bold; 			line-height:125%; 			letter-spacing:normal; 			text-align:left; 		} 		#templatePreheader{ 			background-color:#transparent; 			background-image:none; 			background-repeat:no-repeat; 			background-position:center; 			background-size:cover; 			border-top:0; 			border-bottom:0; 			padding-top:0px; 			padding-bottom:0px; 		} 		#templatePreheader .mcnTextContent,#templatePreheader .mcnTextContent p{ 			color:#656565; 			font-family:Helvetica; 			font-size:12px; 			line-height:150%; 			text-align:left; 		} 		#templatePreheader .mcnTextContent a,#templatePreheader .mcnTextContent p a{ 			color:#656565; 			font-weight:normal; 			text-decoration:underline; 		} 		#templateHeader{ 			background-color:#293746; 			background-image:none; 			background-repeat:no-repeat; 			background-position:center; 			background-size:cover; 			border-top:0; 			border-bottom:0; 			padding-top:20px; 			padding-bottom:20px; 		} 		#templateHeader .mcnTextContent,#templateHeader .mcnTextContent p{ 			color:#ffffff; 			font-family:Helvetica; 			font-size:16px; 			line-height:150%; 			text-align:left; 		} 		#templateHeader .mcnTextContent a,#templateHeader .mcnTextContent p a{ 			color:#ffffff; 			font-weight:normal; 			text-decoration:underline; 		} 		#templateBody{ 			background-color:#3e4b64; 			background-image:none; 			background-repeat:no-repeat; 			background-position:center; 			background-size:cover; 			border-top:0; 			border-bottom:2px solid #51627c; 			padding-top:10px; 			padding-bottom:10px; 		} 		#templateBody .mcnTextContent,#templateBody .mcnTextContent p{ 			color:#ffffff; 			font-family:Helvetica, Arial, Verdana, sans-serif; 			font-size:16px; 			line-height:150%; 			text-align:left; 		} 		#templateBody .mcnTextContent a,#templateBody .mcnTextContent p a{ 			color:#ffffff; 			font-weight:normal; 			text-decoration:underline; 		} 		#templateFooter{ 			background-color:#2c3850; 			background-image:none; 			background-repeat:no-repeat; 			background-position:center; 			background-size:cover; 			border-top:0; 			border-bottom:0; 			padding-top:9px; 			padding-bottom:9px; 		} 		#templateFooter .mcnTextContent,#templateFooter .mcnTextContent p{ 			color:#c5c5c5; 			font-family:Helvetica; 			font-size:12px; 			line-height:150%; 			text-align:center; 		} 		#templateFooter .mcnTextContent a,#templateFooter .mcnTextContent p a{ 			color:#ffffff; 			font-weight:normal; 			text-decoration:underline; 		} 	@media only screen and (min-width:768px){ 		.templateContainer{ 			width:600px !important; 		} }	@media only screen and (max-width: 480px){ 		body,table,td,p,a,li,blockquote{ 			-webkit-text-size-adjust:none !important; 		} }	@media only screen and (max-width: 480px){ 		body{ 			width:100% !important; 			min-width:100% !important; 		} }	@media only screen and (max-width: 480px){ 		#bodyCell{ 			padding-top:10px !important; 		} }	@media only screen and (max-width: 480px){ 		.mcnRetinaImage{ 			max-width:100% !important; 		} }	@media only screen and (max-width: 480px){ 		.mcnImage{ 			width:100% !important; 		} }	@media only screen and (max-width: 480px){ 		.mcnCartContainer,.mcnCaptionTopContent,.mcnRecContentContainer,.mcnCaptionBottomContent,.mcnTextContentContainer,.mcnBoxedTextContentContainer,.mcnImageGroupContentContainer,.mcnCaptionLeftTextContentContainer,.mcnCaptionRightTextContentContainer,.mcnCaptionLeftImageContentContainer,.mcnCaptionRightImageContentContainer,.mcnImageCardLeftTextContentContainer,.mcnImageCardRightTextContentContainer,.mcnImageCardLeftImageContentContainer,.mcnImageCardRightImageContentContainer{ 			max-width:100% !important; 			width:100% !important; 		} }	@media only screen and (max-width: 480px){ 		.mcnBoxedTextContentContainer{ 			min-width:100% !important; 		} }	@media only screen and (max-width: 480px){ 		.mcnImageGroupContent{ 			padding:9px !important; 		} }	@media only screen and (max-width: 480px){ 		.mcnCaptionLeftContentOuter .mcnTextContent,.mcnCaptionRightContentOuter .mcnTextContent{ 			padding-top:9px !important; 		} }	@media only screen and (max-width: 480px){ 		.mcnImageCardTopImageContent,.mcnCaptionBottomContent:last-child .mcnCaptionBottomImageContent,.mcnCaptionBlockInner .mcnCaptionTopContent:last-child .mcnTextContent{ 			padding-top:18px !important; 		} }	@media only screen and (max-width: 480px){ 		.mcnImageCardBottomImageContent{ 			padding-bottom:9px !important; 		} }	@media only screen and (max-width: 480px){ 		.mcnImageGroupBlockInner{ 			padding-top:0 !important; 			padding-bottom:0 !important; 		} }	@media only screen and (max-width: 480px){ 		.mcnImageGroupBlockOuter{ 			padding-top:9px !important; 			padding-bottom:9px !important; 		} }	@media only screen and (max-width: 480px){ 		.mcnTextContent,.mcnBoxedTextContentColumn{ 			padding-right:18px !important; 			padding-left:18px !important; 		} }	@media only screen and (max-width: 480px){ 		.mcnImageCardLeftImageContent,.mcnImageCardRightImageContent{ 			padding-right:18px !important; 			padding-bottom:0 !important; 			padding-left:18px !important; 		} }	@media only screen and (max-width: 480px){ 		.mcpreview-image-uploader{ 			display:none !important; 			width:100% !important; 		} }	@media only screen and (max-width: 480px){ 		h1{ 			font-size:22px !important; 			line-height:125% !important; 		} }	@media only screen and (max-width: 480px){ 		h2{ 			font-size:20px !important; 			line-height:125% !important; 		} }	@media only screen and (max-width: 480px){ 		h3{ 			font-size:18px !important; 			line-height:125% !important; 		} }	@media only screen and (max-width: 480px){ 		h4{ 			font-size:16px !important; 			line-height:150% !important; 		} }	@media only screen and (max-width: 480px){ 		.mcnBoxedTextContentContainer .mcnTextContent,.mcnBoxedTextContentContainer .mcnTextContent p{ 			font-size:14px !important; 			line-height:150% !important; 		} }	@media only screen and (max-width: 480px){ 		#templatePreheader{ 			display:block !important; 		} }	@media only screen and (max-width: 480px){ 		#templatePreheader .mcnTextContent,#templatePreheader .mcnTextContent p{ 			font-size:14px !important; 			line-height:150% !important; 		} }	@media only screen and (max-width: 480px){ 		#templateHeader .mcnTextContent,#templateHeader .mcnTextContent p{ 			font-size:16px !important; 			line-height:150% !important; 		} }	@media only screen and (max-width: 480px){ 		#templateBody .mcnTextContent,#templateBody .mcnTextContent p{ 			font-size:16px !important; 			line-height:150% !important; 		} }	@media only screen and (max-width: 480px){ 		#templateFooter .mcnTextContent,#templateFooter .mcnTextContent p{ 			font-size:14px !important; 			line-height:150% !important; 		} }</style></head> <body> <center> <table align='center' border='0' cellpadding='0' cellspacing='0' height='100%' width='100%' id='bodyTable'> <tr> <td align='center' valign='top' id='bodyCell'> <!-- BEGIN TEMPLATE // --> <!--[if (gte mso 9)|(IE)]> <table align='center' border='0' cellspacing='0' cellpadding='0' width='600' style='width:600px;'> <tr> <td align='center' valign='top' width='600' style='width:600px;'> <![endif]--> <table border='0' cellpadding='0' cellspacing='0' width='100%' class='templateContainer'> <tr> <td valign='top' id='templatePreheader'><table border='0' cellpadding='0' cellspacing='0' width='100%' class='mcnTextBlock' style='min-width:100%;'> <tbody class='mcnTextBlockOuter'> <tr> <td valign='top' class='mcnTextBlockInner' style='padding-top:9px;'> 	<!--[if mso]> 				<table align='left' border='0' cellspacing='0' cellpadding='0' width='100%' style='width:100%;'> 				<tr> 				<![endif]--> 			 				<!--[if mso]> 				<td valign='top' width='600' style='width:600px;'> 				<![endif]--> <table align='left' border='0' cellpadding='0' cellspacing='0' style='max-width:100%; min-width:100%;' width='100%' class='mcnTextContentContainer'> <tbody><tr><td valign='top' class='mcnTextContent' style='padding-top:0; padding-right:18px; padding-bottom:9px; padding-left:18px;'> </td> </tr> </tbody></table> 				<!--[if mso]> 				</td> 				<![endif]-->				<!--[if mso]> 				</tr> 				</table> 				<![endif]--> </td> </tr> </tbody> </table></td> </tr> <tr> <td valign='top' id='templateHeader'><table border='0' cellpadding='0' cellspacing='0' width='100%' class='mcnImageBlock' style='min-width:100%;'> <tbody class='mcnImageBlockOuter'> <tr> <td valign='top' style='padding:10px' class='mcnImageBlockInner'> <table align='left' width='100%' border='0' cellpadding='0' cellspacing='0' class='mcnImageContentContainer' style='min-width:100%;'> <tbody><tr> <td class='mcnImageContent' valign='top' style='padding-right: 10px; padding-left: 10px; padding-top: 0; padding-bottom: 0;'><a href='https://www.nordfincapital.com/' title={ class={ target='_blank'> <img align='left' alt={ src='https://cenk.co/images/Nordfin.png' width='250' style='max-width:500px; padding-bottom: 0; display: inline !important; vertical-align: bottom;' class='mcnRetinaImage'> </a></td> </tr> </tbody></table> </td> </tr> </tbody> </table></td> </tr> <tr> <td valign='top' id='templateBody'><table border='0' cellpadding='0' cellspacing='0' width='100%' class='mcnTextBlock' style='min-width:100%;'> <tbody class='mcnTextBlockOuter'> <tr> <td valign='top' class='mcnTextBlockInner' style='padding-top:9px;'> 	<!--[if mso]> 				<table align='left' border='0' cellspacing='0' cellpadding='0' width='100%' style='width:100%;'> 				<tr> 				<![endif]--> 			 				<!--[if mso]> 				<td valign='top' width='600' style='width:600px;'> 				<![endif]--> <table align='left' border='0' cellpadding='0' cellspacing='0' style='max-width:100%; min-width:100%;' width='100%' class='mcnTextContentContainer'> <tbody><tr><td valign='top' class='mcnTextContent' style='padding-top:0; padding-right:18px; padding-bottom:9px; padding-left:18px;'> " + txtEmailBody.Text.Replace("\n", "<br>") + " <br> &nbsp; </td> </tr> </tbody></table> 				<!--[if mso]> 				</td> 				<![endif]-->				<!--[if mso]> 				</tr> 				</table> 				<![endif]--> </td> </tr> </tbody> </table><table border='0' cellpadding='0' cellspacing='0' width='100%' class='mcnButtonBlock' style='min-width:100%;display:none;'> <tbody class='mcnButtonBlockOuter'> <tr> <td style='padding-top:0; padding-right:18px; padding-bottom:18px; padding-left:18px;' valign='top' align='center' class='mcnButtonBlockInner'>  </td> </tr> </tbody> </table><table border='0' cellpadding='0' cellspacing='0' width='100%' class='mcnTextBlock' style='min-width:100%;'> <tbody class='mcnTextBlockOuter'> <tr> <td valign='top' class='mcnTextBlockInner' style='padding-top:9px;'> 	<!--[if mso]> 				<table align='left' border='0' cellspacing='0' cellpadding='0' width='100%' style='width:100%;'> 				<tr> 				<![endif]--> 			 				<!--[if mso]> 				<td valign='top' width='600' style='width:600px;'> 				<![endif]--> 				<!--[if mso]> 				</td> 				<![endif]-->				<!--[if mso]> 				</tr> 				</table> 				<![endif]--> </td> </tr> </tbody> </table></td> </tr> <tr> <td valign='top' id='templateFooter'><table border='0' cellpadding='0' cellspacing='0' width='100%' class='mcnTextBlock' style='min-width:100%;'> <tbody class='mcnTextBlockOuter'> <tr> <td valign='top' class='mcnTextBlockInner' style='padding-top:9px;'> 	<!--[if mso]> 				<table align='left' border='0' cellspacing='0' cellpadding='0' width='100%' style='width:100%;'> 				<tr> 				<![endif]--> 			 				<!--[if mso]> 				<td valign='top' width='600' style='width:600px;'> 				<![endif]--> <table align='left' border='0' cellpadding='0' cellspacing='0' style='max-width:100%; min-width:100%;' width='100%' class='mcnTextContentContainer'> <tbody><tr><td valign='top' class='mcnTextContent' style='padding-top:0; padding-right:18px; padding-bottom:9px; padding-left:18px;'><a href='https://www.nordfincapital.com/' target='_blank'>Nordfin Capital Group AB</a><br>  Org.nr 559123-4900 &nbsp;&nbsp;<br> © NordFin Capital Group AB </td> </tr> </tbody></table> 				<!--[if mso]> 				</td> 				<![endif]-->				<!--[if mso]> 				</tr> 				</table> 				<![endif]--> </td> </tr> </tbody> </table></td> </tr> </table> <!--[if (gte mso 9)|(IE)]> </td> </tr> </table> <![endif]--> <!-- // END TEMPLATE --> </td> </tr> </table> </center> </body> </html> ";

                        mail.From = new MailAddress(sEmail, ClientSession.ClientName);

                        mail.To.Add(toEmail.Trim());

                        mail.Subject = txtEmailHeader.Text;

                        mail.Body = sBody;
                        mail.IsBodyHtml = true;

                        string sCollectionStatus = "";
                        bool bDC = false;
                        bool bRemind = false;
                        bool bInvoices = false;

                    emailDownload:
                        if (!chkInvoices.Checked && !bInvoices)
                        {
                            bInvoices = true;
                            goto emailDC;
                        }
                        string sFileExt = System.Configuration.ConfigurationManager.AppSettings["FileExtension"].ToString();
                        string sFileName = hdnFileName.Value + "_" + sCollectionStatus + InvoiceNumber + "_" + "inv" + "." + sFileExt;
                        string sFolderPath = "~/Documents/" + Session.SessionID + "/" + sFileName;
                        if (File.Exists(Server.MapPath(sFolderPath)))
                        {
                            System.Net.Mail.Attachment attachment;
                            attachment = new System.Net.Mail.Attachment(Server.MapPath(sFolderPath));
                            mail.Attachments.Add(attachment);
                        }

                    emailDC:
                        if (collectionStatus != "")
                        {
                            if (collectionStatus.ToUpper() == "DC" && chkDC.Checked && !bDC)
                            {
                                bDC = true;
                                sCollectionStatus = "DC";
                                goto emailDownload;
                            }
                            else if ((collectionStatus.ToUpper() == "DC" || collectionStatus.ToUpper() == "REMIND") && chkRemind.Checked && !bRemind)
                            {
                                bRemind = true;
                                sCollectionStatus = "Rem";
                                goto emailDownload;
                            }
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

    }
}