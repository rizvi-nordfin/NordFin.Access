
using Nordfin.CreditSafeTemplate;
using Nordfin.workflow.BusinessLayer;
using Nordfin.workflow.Entity;
using Nordfin.workflow.PresentationBusinessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Nordfin
{
    public partial class frmCreditCheck : System.Web.UI.Page
    {
        string key { get; set; } = "B832002DA297545CFC6B5F5C9B5";
        protected void Page_Load(object sender, EventArgs e)
        {


            if (!IsPostBack)
            {
                txtPassword.Attributes["type"] = "password";
                txtUserName.Text = Decrypt(GetFromCookie("CreditUser", "UserName"));
                txtPassword.Text = Decrypt(GetFromCookie("CreditToken", "Token"));
                lblClientName.Text = ClientSession.ClientName;
                grdCreditCheck.DataSource = new List<string>();
                grdCreditCheck.DataBind();
            }
        }

        protected void btnCreditCheck_Click(object sender, EventArgs e)
        {
            if (cboCustomerType.SelectedItem.Text == "")
                return;
            IList<CreditCheck> creditCheckList = cboCustomerType.SelectedItem.Text.ToUpper() == "PRV" ? GetPersonCreditDetails() : GetCompanyCreditDetails();
            grdCreditCheck.DataSource = creditCheckList;
            grdCreditCheck.DataBind();
        }


        private IList<CreditCheck> GetCompanyCreditDetails()
        {
            var cas = new CreditSafeTemplate.Cas_ServiceSoapClient();
            IList<CreditCheck> creditCheckList = null;
            ITelsonGroupPresentationBusinessLayer objTelsonData = new TelesonGroupBusinessLayer();
            string serachNumber = txtPersonalNumber.Text.Replace("-", "").Trim();

            CAS_COMPANY_REQUEST companyRequest = new CAS_COMPANY_REQUEST();
            CreditSafeTemplate.Account accountTemp = new CreditSafeTemplate.Account();
            accountTemp.UserName = txtUserName.Text;// "NORDFIN";
            accountTemp.Password = txtPassword.Text;// "4!c3KFxpzXLQR69";
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
                        creditCheck.Error+= (creditCheck.Error == "") ? Company_RESPONSE.ErrorList[k].Cause_of_Reject+"-"+ Company_RESPONSE.ErrorList[k].Reject_text : ";" + Company_RESPONSE.ErrorList[k].Cause_of_Reject+"-"+ Company_RESPONSE.ErrorList[k].Reject_text;
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
                SetCookie("CreditUser", "UserName", txtUserName.Text);
                SetCookie("CreditToken", "Token", txtPassword.Text);
                creditCheckList = new List<CreditCheck>();
                

                int Result = objTelsonData.setCreditCheck(creditCheck);
                
                creditCheckList.Add(creditCheck);
            }
            return creditCheckList;
        }


        private IList<CreditCheck> GetPersonCreditDetails()
        {
            var cas = new CreditSafeTemplate.Cas_ServiceSoapClient();
            IList<CreditCheck> creditCheckList = null;
            ITelsonGroupPresentationBusinessLayer objTelsonData = new TelesonGroupBusinessLayer();
            string serachNumber = txtPersonalNumber.Text.Replace("-", "").Trim();

            CAS_PERSON_REQUEST personRequest = new CAS_PERSON_REQUEST();
            CreditSafeTemplate.Account accountTemp = new CreditSafeTemplate.Account();
            accountTemp.UserName = txtUserName.Text;// "NORDFIN";
            accountTemp.Password = txtPassword.Text;// "4!c3KFxpzXLQR69";
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
                SetCookie("CreditUser", "UserName", txtUserName.Text);
                SetCookie("CreditToken", "Token", txtPassword.Text);
                creditCheckList = new List<CreditCheck>();


                int Result = objTelsonData.setCreditCheck(creditCheck);

                creditCheckList.Add(creditCheck);
            }
            return creditCheckList;
        }

      
        public  string GetFromCookie(string cookieName, string keyName)
        {
            HttpCookie cookie = HttpContext.Current.Request.Cookies[cookieName];
            if (cookie != null)
            {
                string val = (!String.IsNullOrEmpty(keyName)) ? cookie[keyName] : cookie.Value;
                if (!String.IsNullOrEmpty(val)) return Uri.UnescapeDataString(val);
            }
            return "";
        }

        public  void SetCookie(string cookieName,string Name,string Value)
        {
           
            
            HttpCookie cookie = HttpContext.Current.Response.Cookies.AllKeys.Contains(cookieName)? HttpContext.Current.Response.Cookies[cookieName]
                                 : HttpContext.Current.Request.Cookies[cookieName];
            if (cookie == null)
            {
                cookie = new HttpCookie(cookieName);
                if (!String.IsNullOrEmpty(Name))
                    cookie.Values.Set(Name, Encrypt(Value));
                cookie.HttpOnly = true;
                cookie.Secure = true;
                cookie.SameSite = SameSiteMode.Strict;
                HttpContext.Current.Response.Cookies.Set(cookie);
            }
           
        }

        public string Encrypt(string text)
        {
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

        public  string Decrypt(string cipher)
        {
            if(cipher=="")
            {
                return "";
            }
            using (var md5 = new MD5CryptoServiceProvider())
            {
                using (var tdes = new TripleDESCryptoServiceProvider())
                {
                    tdes.Key = md5.ComputeHash(UTF8Encoding.UTF8.GetBytes(key));
                    tdes.Mode = CipherMode.ECB;
                    tdes.Padding = PaddingMode.PKCS7;

                    using (var transform = tdes.CreateDecryptor())
                    {
                        byte[] cipherBytes = Convert.FromBase64String(cipher);
                        byte[] bytes = transform.TransformFinalBlock(cipherBytes, 0, cipherBytes.Length);
                        return UTF8Encoding.UTF8.GetString(bytes);
                    }
                }
            }
        }
    }
}