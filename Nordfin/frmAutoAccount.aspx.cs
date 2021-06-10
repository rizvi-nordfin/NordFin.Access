﻿using Nordfin.AutoAccount;
using Nordfin.workflow.BusinessLayer;
using Nordfin.workflow.Entity;
using Nordfin.workflow.PresentationBusinessLayer;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Serialization;

namespace Nordfin
{
    public partial class frmAutoAccount : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            if (chkTerms.Checked)
            {
                var autoAccountService = new AutoAccountServiceSoapClient();
                try
                {
                    CreditAutoAccount creditAutoAccount = new CreditAutoAccount();
                    creditAutoAccount.ClientID = Convert.ToInt32(ClientSession.ClientID);
                    creditAutoAccount.UserID = Convert.ToInt32(ClientSession.UserID);
                    creditAutoAccount.Name = txtFirstName.Text + " " + txtLastName.Text;
                    creditAutoAccount.OrgNumber = txtOrgNumber.Text;
                    creditAutoAccount.PhoneNumber = txtCountryCode.Text + "" + txtMobileNumber.Text;
                    creditAutoAccount.Email = txtEMail.Text;
                    creditAutoAccount.ConditionTimeStamp = DateTime.Now;
                    string result = autoAccountService.AutoAccount("<xmlrequest> <header> <account> <username>NORDFABIN</username> " +
                        "<password>Nordfintest@123</password> <grouporgnumber>5591234900</grouporgnumber> <servicesequence>1</servicesequence> <language>SE</language> </account> </header> </xmlrequest>");

                    XmlSerializer serializer = new XmlSerializer(typeof(CreditAutoAccountReponse));
                    CreditAutoAccountReponse autoAccountReponse = null;
                    using (StringReader reader = new StringReader(result))
                    {
                        autoAccountReponse = (CreditAutoAccountReponse)serializer.Deserialize(reader);
                    }
                    string requestpackagexml = "<xmlrequest> <header> <account> <username>NORDFABIN</username> <password>Nordfintest@123</password> " +
                           "<grouporgnumber>5591234900</grouporgnumber> <servicesequence>2</servicesequence> <language>EN</language> </account> </header> <body> <token>" + autoAccountReponse.Body.Returndetail.Token + "</token> " +
                           "<timestamp>" + autoAccountReponse.Body.Returndetail.Timestamp + "</timestamp> <contactperson>" + creditAutoAccount.Name + "</contactperson> " +
                           "<emailaddress>" + creditAutoAccount.Email + "</emailaddress> " +
                           "<phonenumber>" + txtMobileNumber.Text + "</phonenumber> <mobilecountrycode>" + txtCountryCode.Text + "</mobilecountrycode> " +
                           "<mobile>" + txtMobileNumber.Text + "</mobile>" +
                           " <orgnumber>" + creditAutoAccount.OrgNumber + "</orgnumber> <package>NORDFINTEST_AA</package> </body> </xmlrequest>";


                    string UserInfoResponse = autoAccountService.AutoAccount(requestpackagexml);
                    XmlSerializer serializerUserInfo = new XmlSerializer(typeof(CreditAutoAccountUserInfo));
                    CreditAutoAccountUserInfo autoAccountUserInfo = null;
                    using (StringReader readerUserinfo = new StringReader(UserInfoResponse))
                    {
                        autoAccountUserInfo = (CreditAutoAccountUserInfo)serializerUserInfo.Deserialize(readerUserinfo);
                    }
                    creditAutoAccount.CreditUserName = autoAccountUserInfo.Userinfo.Username;

                    ITelsonGroupPresentationBusinessLayer objTelsonData = new TelesonGroupBusinessLayer();
                    bool bInsert = objTelsonData.setCreditAutoAccount(creditAutoAccount);
                    autoAccountService.Close();
                }
                catch(Exception ex)
                {
                    autoAccountService.Close();
                }
            }
            

        }
    }
}