using Newtonsoft.Json;
using Nordfin.workflow.Business;
using Nordfin.workflow.Entity;
using Nordfin.workflow.PresentationBusinessLayer;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;


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

              

                if (InvoiceData.Split('|').Length > 1)
                {
                    Session["custNum"] = InvoiceData.Split('|')[0];
                    Session["InvoiceID"] = InvoiceData.Split('|')[1];
                    InvoiceNum = InvoiceData.Split('|')[2];
                }
                else
                    InvoiceNum = InvoiceData;
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

                if (Convert.ToDouble(sRemainAmount.Replace(" ","").Replace(".", ",")) < 0)
                {
                    txtPayoutCreditPayment.Text = sRemainAmount.Trim();
                    hdnPaymentCheck.Value = "credit";
                }

                if (Convert.ToDouble(sOverPaidAmount.Replace(" ", "").Replace(".", ",")) > 0)
                {
                    txtPayoutOverPayment.Text = sOverPaidAmount.Trim();
                    hdnPaymentCheck.Value += "|"+"overpayment";
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
            //JavaScriptSerializer json_serializer = new JavaScriptSerializer();
            //Notes dc = json_serializer.Deserialize<Notes>(json);
            //Notes listNumber = ((Notes)name);

            //var h = JsonConvert.DeserializeObject<List<Notes>>(name);
            objNotes.InvoiceID = Convert.ToInt32(HttpContext.Current.Session["InvoiceID"]);
            objNotes.InvoiceNumber = (string)HttpContext.Current.Session["InvoiceNum"];
            objNotes.CustomerID = (string)HttpContext.Current.Session["custNum"];
            objNotes.UserID =Convert.ToInt32(ClientSession.UserID);
            objNotes.ClientID = Convert.ToInt32(ClientSession.ClientID);
            objNotes.UserName = ClientSession.UserName;
            IPaymentInformationPresentationBusinessLayer objPresentationInfoLayer = new PaymentInformationBusinessLayer();
            var objNotesInformation = objPresentationInfoLayer.insertInterest(objNotes);
           
            string jsonResult = new JavaScriptSerializer().Serialize(objNotesInformation.Item1);
            string jsonNotesListResult = new JavaScriptSerializer().Serialize(objNotesInformation.Item2);
            string sResultList = "{\"NotesInfo\" :" + jsonResult +","+ "\"NotesList\" :" + jsonNotesListResult + "}";
            return sResultList;
            //updateInterest
        }

      
     

        [WebMethod]
        public static string PDFDownload(string ClientName, string FileName)
        {
            ClearSession();
            FTPFileProcess fileProcess = new FTPFileProcess();
            // fileProcess.WinscpConnection();
            string sFileExt = System.Configuration.ConfigurationManager.AppSettings["FileExtension"].ToString();
            string sFileName = FileName + sFileExt;
            string sPDFViewerLink = "";
            bool bResult = false;
            string ResultFile = "";
            if (sFileName != "")
                bResult = fileProcess.FileDownload(ClientName, sFileName, out ResultFile);
            if (!bResult)
            {
                sPDFViewerLink =  (string)HttpContext.Current.Session["InvoiceNum"];
            }

            string jsonFileName = new JavaScriptSerializer().Serialize(sFileName);
            string jsonViewerLink = new JavaScriptSerializer().Serialize(sPDFViewerLink);
            string jsonSession= new JavaScriptSerializer().Serialize(HttpContext.Current.Session.SessionID);
            string jsonBoolResult = new JavaScriptSerializer().Serialize(bResult);
            if (ResultFile != "")
            {
                jsonFileName = ResultFile;
            }
            string sResultList = "{\"FileName\" :" + jsonFileName + "," + "\"ViewerLink\" :" + jsonViewerLink + "," + "\"SessionID\" :" + jsonSession + "," + "\"BoolResult\" :" + jsonBoolResult + "}";
            return sResultList;


           
            //updateInterest
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
                catch { }
                HttpContext.Current.Session.Abandon();
                HttpContext.Current.Response.Redirect("frmLogin.aspx");

            }
        }
    }
}