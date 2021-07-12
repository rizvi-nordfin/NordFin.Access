
using Nordfin.AutoAccount;
using Nordfin.CreditSafeTemplate;
using Nordfin.GetDataTemplate;

using Nordfin.workflow.BusinessLayer;
using Nordfin.workflow.Entity;
using Nordfin.workflow.PresentationBusinessLayer;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;
using System.Xml.Serialization;

namespace Nordfin
{
    public partial class frmCreditCheck : System.Web.UI.Page
    {
        string key { get; set; } = "B832002DA297545CFC6B5F5C9B5";
        protected void Page_Load(object sender, EventArgs e)
        {


            if (!IsPostBack)
            {
                ITelsonGroupPresentationBusinessLayer objTelsonData = new TelesonGroupBusinessLayer();
                txtPassword.Attributes["type"] = "password";
                lblClientName.Text = ClientSession.ClientName;
                hdnCreditScore.Value = "0";
                hdnCreditVisible.Value = Convert.ToString(ClientSession.CreditUser);
                CreditAutoAccount creditAutoAccount = objTelsonData.getCreditAutoAccountDetails(Convert.ToInt32(ClientSession.ClientID));
                if (creditAutoAccount == null)
                {
                    btnAutoAccount.Visible = true;
                    txtUserName.Text = "";
                    txtPassword.Text = "";
                    hdnCreditVisible.Value = "0";
                }
                else
                {
                    btnAutoAccount.Visible = false;
                    txtUserName.Text = creditAutoAccount.CreditUserName;
                    txtPassword.Text = creditAutoAccount.CreditPassword;
                    hdnCreditVisible.Value = "1";
                }


            }
        }

        protected void btnCreditCheck_Click(object sender, EventArgs e)
        {
            if (!btnAutoAccount.Visible)
            {

                if (cboCustomerType.SelectedItem.Text == "" || string.IsNullOrWhiteSpace(txtUserName.Text) || string.IsNullOrWhiteSpace(txtPersonalNumber.Text) || string.IsNullOrWhiteSpace(txtPassword.Text))
                {
                    hdnCreditScore.Value = "0";
                    return;
                }

                if (cboCustomerType.SelectedItem.Value.ToUpper() == "1")
                {
                    string sValue = GetFromCookie("PrivateMsg");

                    int Hours = (!string.IsNullOrEmpty(sValue)) ? (DateTime.Now - Convert.ToDateTime(sValue)).Hours : 0;

                    if (string.IsNullOrEmpty(sValue) || Hours > 8)
                    {

                        SetCookie("PrivateMsg");
                        DisplayMessage();
                        return;
                    }
                    GetPersonCreditDetails();
                }
                else
                    GetCompanyCreditDetails();
            }

        }


        private void GetCompanyCreditDetails()
        {
            bool isEncrpt;
            var test = new GetDataSoapClient();
            GETDATA_REQUEST gETDATA_REQUEST = new GETDATA_REQUEST();
            var account = new GetDataTemplate.Account();
            account.UserName = txtUserName.Text;
            account.Password = Decrypt(txtPassword.Text,out isEncrpt);
            account.Language = GetDataTemplate.LANGUAGE.EN;
            gETDATA_REQUEST.account = account;
            gETDATA_REQUEST.Block_Name = "NORDFIN_C_CREDIT";
            gETDATA_REQUEST.SearchNumber = txtPersonalNumber.Text;
            gETDATA_REQUEST.FormattedOutput = "1";

            GETDATA_RESPONSE dataResponse = test.GetDataBySecure(gETDATA_REQUEST);
            if (dataResponse.Error != null)
            {
                hdnCreditScore.Value = "0";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "showModalInfo", "$('#mdlMasterConfirm').modal({ backdrop: 'static', keyboard: false }, 'show');" +
                    "$('#spnMasterInfo').text('" + dataResponse.Error.Reject_text + "');", true);
                return;
            }
             
            string xmlResponse = dataResponse.Parameters.GetXml();
            XmlDocument document = new XmlDocument();
            document.LoadXml(xmlResponse);
            CreditDataSet datasetReponse = new CreditDataSet();
            XmlSerializer serializer = new XmlSerializer(typeof(CreditDataSet));
            using (StringReader reader = new StringReader(xmlResponse))
            {
                datasetReponse = (CreditDataSet)serializer.Deserialize(reader);
            }
            FillData(datasetReponse);

            var cas = new CreditSafeTemplate.Cas_ServiceSoapClient();
            IList<CreditCheck> creditCheckList = null;
            ITelsonGroupPresentationBusinessLayer objTelsonData = new TelesonGroupBusinessLayer();
            string serachNumber = txtPersonalNumber.Text.Replace("-", "").Trim();

            CAS_COMPANY_REQUEST companyRequest = new CAS_COMPANY_REQUEST();
            CreditSafeTemplate.Account accountTemp = new CreditSafeTemplate.Account();
            accountTemp.UserName = txtUserName.Text;// "NORDFIN";
            accountTemp.Password = Decrypt(txtPassword.Text, out isEncrpt);// "4!c3KFxpzXLQR69";
            accountTemp.Language = CreditSafeTemplate.LANGUAGE.EN;

            companyRequest.account = accountTemp;
            companyRequest.SearchNumber = serachNumber;
            companyRequest.Templates = "NORDFIN_CAS_C1";

            CAS_COMPANY_RESPONSE Company_RESPONSE = cas.CasCompanyService(companyRequest);
            CreditCheck creditCheck = new CreditCheck();
            if (Company_RESPONSE.Status != null)
            {
                creditCheck.Name = Company_RESPONSE.Name;
                creditCheck.Address = Company_RESPONSE.Address;
                creditCheck.City = Company_RESPONSE.Town;
                creditCheck.PostalCode = Company_RESPONSE.ZIP;
                creditCheck.Status = Company_RESPONSE.Status_Text;

                if (Company_RESPONSE.ErrorList != null && Company_RESPONSE.ErrorList.Count() > 0)
                {
                    for (int k = 0; k < Company_RESPONSE.ErrorList.Count(); k++)
                    {
                        creditCheck.CreditInfo += (creditCheck.Error == "") ? Company_RESPONSE.ErrorList[k].Cause_of_Reject : "#" + Company_RESPONSE.ErrorList[k].Cause_of_Reject;
                        creditCheck.ErrorMessage += (creditCheck.ErrorMessage == "") ? Company_RESPONSE.ErrorList[k].Reject_text : "#" + Company_RESPONSE.ErrorList[k].Reject_text;
                        creditCheck.Error += (creditCheck.Error == "") ? Company_RESPONSE.ErrorList[k].Cause_of_Reject + "-" + Company_RESPONSE.ErrorList[k].Reject_text : ";" + Company_RESPONSE.ErrorList[k].Cause_of_Reject + "-" + Company_RESPONSE.ErrorList[k].Reject_text;
                    }
                }
                else
                {
                    creditCheck.CreditInfo = "OK";
                    creditCheck.ErrorMessage = "OK";
                }

                if (Company_RESPONSE.Status != "1")
                {
                    creditCheck.CreditScoreAccepted = 0;
                    creditCheck.CreditStatus = "1";
                }
                else
                {
                    creditCheck.CreditScoreAccepted = 1;
                    creditCheck.CreditStatus = "0";
                }
                creditCheck.PersonalNumber = serachNumber;
                creditCheck.ClientID = Convert.ToInt32(ClientSession.ClientID);
                creditCheck.CreditScore = Convert.ToInt32(datasetReponse.CreditGetData.RATING);
                creditCheck.CreditPassword = isEncrpt ? "" : Encrypt(txtPassword.Text);
                //SetCookie("CreditUser", "UserName", txtUserName.Text);
                //SetCookie("CreditToken", "Token", txtPassword.Text);
                creditCheckList = new List<CreditCheck>();


                int Result = objTelsonData.setCreditCheck(creditCheck);
                CasData(creditCheck.Status, creditCheck.Error);

            }
            hdnCreditScore.Value = datasetReponse.CreditGetData.RATING;
            


        }


        private void GetPersonCreditDetails()
        {
            bool isEncrpt;
            var test = new GetDataSoapClient();
            GETDATA_REQUEST gETDATA_REQUEST = new GETDATA_REQUEST();
            var account = new GetDataTemplate.Account();
            account.UserName = txtUserName.Text;
            account.Password = Decrypt(txtPassword.Text, out isEncrpt);
            account.Language = GetDataTemplate.LANGUAGE.EN;
            gETDATA_REQUEST.account = account;
            gETDATA_REQUEST.Block_Name = "NORDFIN_P_CREDIT";
            gETDATA_REQUEST.SearchNumber = txtPersonalNumber.Text;
            gETDATA_REQUEST.FormattedOutput = "1";
            GETDATA_RESPONSE dataResponse = test.GetDataBySecure(gETDATA_REQUEST);
            if (dataResponse.Error != null)
            {
                hdnCreditScore.Value = "0";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "showModalInfo", "$('#mdlMasterConfirm').modal({ backdrop: 'static', keyboard: false }, 'show');" +
                    "$('#spnMasterInfo').text('" + dataResponse.Error.Reject_text + "');", true);
                return;
            }
            string xmlResponse = dataResponse.Parameters.GetXml();
            XmlDocument document = new XmlDocument();
            document.LoadXml(xmlResponse);
            CreditDataSet datasetReponse = new CreditDataSet();
            XmlSerializer serializer = new XmlSerializer(typeof(CreditDataSet));
            using (StringReader reader = new StringReader(xmlResponse))
            {
                datasetReponse = (CreditDataSet)serializer.Deserialize(reader);
            }
            FillData(datasetReponse);



            var cas = new CreditSafeTemplate.Cas_ServiceSoapClient();
            IList<CreditCheck> creditCheckList = null;
            ITelsonGroupPresentationBusinessLayer objTelsonData = new TelesonGroupBusinessLayer();
            string serachNumber = txtPersonalNumber.Text.Replace("-", "").Trim();

            CAS_PERSON_REQUEST personRequest = new CAS_PERSON_REQUEST();
            CreditSafeTemplate.Account accountTemp = new CreditSafeTemplate.Account();
            accountTemp.UserName = txtUserName.Text;// "NORDFIN";
            accountTemp.Password = Decrypt(txtPassword.Text, out isEncrpt);// "4!c3KFxpzXLQR69";
            accountTemp.Language = CreditSafeTemplate.LANGUAGE.EN;

            personRequest.account = accountTemp;
            personRequest.SearchNumber = serachNumber;
            personRequest.Templates = "NORDFIN_CAS_P1";

            CAS_PERSON_RESPONSE Person_RESPONSE = cas.CasPersonService(personRequest);
            CreditCheck creditCheck = new CreditCheck();
            if (Person_RESPONSE.Status != null)
            {
                creditCheck.Name = Person_RESPONSE.GivenName;
                creditCheck.Address = Person_RESPONSE.Address;
                creditCheck.City = Person_RESPONSE.Town;
                creditCheck.PostalCode = Person_RESPONSE.ZIP;
                creditCheck.Status = Person_RESPONSE.Status_Text;

                if (Person_RESPONSE.ErrorList != null && Person_RESPONSE.ErrorList.Count() > 0)
                {
                    for (int k = 0; k < Person_RESPONSE.ErrorList.Count(); k++)
                    {
                        creditCheck.CreditInfo += (creditCheck.Error == "") ? Person_RESPONSE.ErrorList[k].Cause_of_Reject : "#" + Person_RESPONSE.ErrorList[k].Cause_of_Reject;
                        creditCheck.ErrorMessage += (creditCheck.ErrorMessage == "") ? Person_RESPONSE.ErrorList[k].Reject_text : "#" + Person_RESPONSE.ErrorList[k].Reject_text;
                        creditCheck.Error += (creditCheck.Error == "") ? Person_RESPONSE.ErrorList[k].Cause_of_Reject + "-" + Person_RESPONSE.ErrorList[k].Reject_text : ";" + Person_RESPONSE.ErrorList[k].Cause_of_Reject + "-" + Person_RESPONSE.ErrorList[k].Reject_text;
                    }
                }
                else
                {
                    creditCheck.CreditInfo = "OK";
                    creditCheck.ErrorMessage = "OK";
                }

                if (Person_RESPONSE.Status != "1")
                {
                    creditCheck.CreditScoreAccepted = 0;
                    creditCheck.CreditStatus = "1";
                }
                else
                {
                    creditCheck.CreditScoreAccepted = 1;
                    creditCheck.CreditStatus = "0";
                }
                creditCheck.PersonalNumber = serachNumber;
                creditCheck.ClientID = Convert.ToInt32(ClientSession.ClientID);
                creditCheck.CreditScore = Convert.ToInt32(datasetReponse.CreditGetData.SCORING);
                creditCheck.CreditPassword = isEncrpt ? "" : Encrypt(txtPassword.Text); 
                //SetCookie("CreditUser", "UserName", txtUserName.Text);
                //SetCookie("CreditToken", "Token", txtPassword.Text);
                creditCheckList = new List<CreditCheck>();
                CasData(creditCheck.Status, creditCheck.Error);
                int Result = objTelsonData.setCreditCheck(creditCheck);

                
            }
            hdnCreditScore.Value = datasetReponse.CreditGetData.SCORING;
           
        }

        private void FillData(CreditDataSet creditData)
        {
            lblResultName.Text = creditData.CreditGetData.NAME??creditData.CreditGetData.GIVENNAME;
            lblRegNumber.Text = creditData.CreditGetData.ORGNR?? creditData.CreditGetData.PNR;
            lblAddress.Text = creditData.CreditGetData.ADDRESS;
            lblPostalCode.Text = creditData.CreditGetData.ZIPCODE;
            lblCity.Text = creditData.CreditGetData.TOWN;
            lblCountry.Text = ClientSession.ClientLand;

        }
        private void CasData(string Status,string Code)
        {
            lblStatus.Text = Status.ToUpper() != "APPROVED" ? "DECLIEND" : Status.ToUpper();
            lblResultStatus.Text = lblStatus.Text;
            lblResultStatus.Style.Add("color", lblStatus.Text == "APPROVED" ? "lightgreen" : "#f83030");
            lblRejectCode.Text = Code ?? "";
        }
        public string Encrypt(string text)
        {
            if (string.IsNullOrEmpty(text))
                return "";
            using (var md5 = new MD5CryptoServiceProvider())
            {
                using (var tdes = new TripleDESCryptoServiceProvider())
                {
                    tdes.Key = md5.ComputeHash(UTF8Encoding.UTF8.GetBytes(key));
                    tdes.Mode = CipherMode.ECB;
                    tdes.Padding = PaddingMode.PKCS7;

                    using (var transform = tdes.CreateEncryptor())
                    {
                        byte[] textBytes = UTF8Encoding.UTF8.GetBytes(text);
                        byte[] bytes = transform.TransformFinalBlock(textBytes, 0, textBytes.Length);
                        return Convert.ToBase64String(bytes, 0, bytes.Length);
                    }
                }
            }
        }

        public string Decrypt(string creditPassword,out bool bIsEncrypt)
        {
            try
            {
                using (var md5 = new MD5CryptoServiceProvider())
                {
                    using (var tdes = new TripleDESCryptoServiceProvider())
                    {
                        tdes.Key = md5.ComputeHash(UTF8Encoding.UTF8.GetBytes(key));
                        tdes.Mode = CipherMode.ECB;
                        tdes.Padding = PaddingMode.PKCS7;

                        using (var transform = tdes.CreateDecryptor())
                        {
                            byte[] cipherBytes = Convert.FromBase64String(creditPassword);
                            byte[] bytes = transform.TransformFinalBlock(cipherBytes, 0, cipherBytes.Length);
                            bIsEncrypt = true;
                            return UTF8Encoding.UTF8.GetString(bytes);
                        }
                    }
                }
            }
            catch
            {
                bIsEncrypt = false;
                return creditPassword;
            }
        }
        public string GetFromCookie(string cookieName)
        {
            HttpCookie cookie = HttpContext.Current.Request.Cookies[cookieName];
            if (cookie != null)
            {
                string val = (!String.IsNullOrEmpty(cookieName)) ? cookie[cookieName] : cookie.Value;
                if (!String.IsNullOrEmpty(val)) return Uri.UnescapeDataString(val);
            }
            return "";
        }

        public void SetCookie(string cookieName)
        {


            HttpCookie cookie = HttpContext.Current.Response.Cookies.AllKeys.Contains(cookieName) ? HttpContext.Current.Response.Cookies[cookieName]
                                 : HttpContext.Current.Request.Cookies[cookieName];
            cookie = new HttpCookie(cookieName);
            cookie[cookieName] = DateTime.Now.ToString("yyyy-MM-dd HH:mm");
            cookie.HttpOnly = true;
            cookie.Secure = true;
            cookie.SameSite = SameSiteMode.Strict;
            HttpContext.Current.Response.Cookies.Set(cookie);
         

        }
        public void DisplayMessage()
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "AlertPrivateCusttype", "AlertPrivateCustomer();", true);
        }
    
    }
}