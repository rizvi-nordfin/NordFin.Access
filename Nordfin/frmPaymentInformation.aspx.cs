using Newtonsoft.Json;
using Nordfin.workflow.Business;
using Nordfin.workflow.BusinessLayer;
using Nordfin.workflow.Entity;
using Nordfin.workflow.PresentationBusinessLayer;
using System;
using System.Collections.Generic;
using System.IO;
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

                if (ClientSession.Admin == "0" || ClientSession.Admin == "1")
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
    }
}