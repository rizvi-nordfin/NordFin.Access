using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Nordfin.workflow.Business;
using Nordfin.workflow.Entity;
using Nordfin.workflow.PresentationBusinessLayer;
using System;
using System.IO;
using System.Net;
using System.Web;

namespace Nordfin
{
    public partial class frmLogin : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["emailConf"] != null)
            {
                IUserPresentationBusinessLayer objUserLayer = new UserBusinessLayer();

                int emailCount = objUserLayer.checkEmailVerification(Convert.ToString(Request.QueryString["emailConf"]));

                if (emailCount > 0)
                {

                    Response.Redirect("frmLogin.aspx");


                }

            }



        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {

            if (IsReCaptchValid())
            {
                DeleteDirectory();
                IUserPresentationBusinessLayer objUserLayer = new UserBusinessLayer();

                Users objuser = objUserLayer.GetUser(txtUserName.Value, GetHashPassword());
                LoginUserInformation userInformation = null;
                bool bUserInfo = false;
                try
                {
                    string IPAddress = GetIPAddress();

                    string sUserDetails = GetUserIPDetails("http://ipinfo.io/" + IPAddress + "/json");

                    userInformation = JsonConvert.DeserializeObject<LoginUserInformation>(sUserDetails);
                    userInformation.Email = txtUserName.Value;
                    userInformation.IP = IPAddress;
                    userInformation.HostName = (sUserDetails.ToLower().Contains("host")) ? System.Net.Dns.GetHostEntry(userInformation.IP).HostName : "";
                    userInformation.BrowserName = Request.Browser.Browser;
                    userInformation.Version = Request.Browser.Version;
                    string platform = "";

                    var os = Request.Browser.Platform;
                    if (os.ToUpper() == "UNKNOWN")
                    {
                        platform = "mac";
                    }
                    else if (os.ToUpper().ToUpper().Contains("WIN"))
                    {
                        platform = "windows";
                    }
                    else if (os.ToUpper().ToUpper().Contains("LIN"))
                    {
                        platform = "linux";
                    }
                    userInformation.OS = platform;
                    userInformation.CILastReGenerate = Convert.ToString(System.DateTimeOffset.Now.ToUnixTimeSeconds());
                    if (userInformation.HostName == null)
                        userInformation.HostName = "Can't find";

                    bUserInfo = true;
                    userInformation.IsMobile = Request.Browser.IsMobileDevice;

                    userInformation.IsAccess = true;
                    if (userInformation.IsMobile)
                    {
                        userInformation.OS = GetUserPlatform(Request.UserAgent);
                    }

                }
                catch
                {
                    //catch the issue
                }

                if (objuser.UserName != null)
                {
                    if (bUserInfo)
                    {
                        userInformation.iStatus = 1;


                      //  int Result = objUserLayer.InsertLoginUserInformation(userInformation);
                    }

                    ClientSession.UserName = objuser.UserName;
                    ClientSession.Admin = Convert.ToString(objuser.Admin);
                    ClientSession.ClientID = Convert.ToString(objuser.ClientID);
                    ClientSession.UserID = Convert.ToString(objuser.UserID);
                    ClientSession.LabelUser = objuser.LabelUser;
                    ClientSession.CreditUser = objuser.CreditUser;
                    if (ClientSession.CreditUser == 2)
                    {
                        Response.Redirect("frmNordfinCreditCheck.aspx");
                    }
                    else
                        Response.Redirect("frmDashboard.aspx");
                }
                else
                {
                    lblMessage1.Text = "Invalid User";
                    if (bUserInfo)
                    {
                        userInformation.iStatus = 0;
                        int Result = objUserLayer.InsertLoginUserInformation(userInformation);
                    }
                }
            }
            else
            {
                lblMessage1.Text = "Invalid Captcha";
            }




        }


        public String GetUserPlatform(string ua)
        {


            if (ua.Contains("Android"))
                return "Android";

            if (ua.Contains("iPad"))
                return "iPad";

            if (ua.Contains("iPhone"))
                return "iPhone";

            if (ua.Contains("Linux") && ua.Contains("KFAPWI"))
                return "Kindle Fire";

            if (ua.Contains("RIM Tablet") || (ua.Contains("BB") && ua.Contains("Mobile")))
                return "Black Berry";


            if (ua.Contains("Mac OS"))
                return "Mac";

            if (ua.Contains("Windows NT 5.1") || ua.Contains("Windows NT 5.2"))
                return "Windows XP";

            if (ua.Contains("Windows NT 6.0"))
                return "Windows";

            if (ua.Contains("Windows NT 6.1"))
                return "Windows";

            if (ua.Contains("Windows NT 6.2"))
                return "Windows";

            if (ua.Contains("Windows NT 6.3"))
                return "Windows";

            if (ua.Contains("Windows NT 10"))
                return "Windows";

            //fallback to basic platform:
            return "";
        }


        public String GetMobileVersion(string userAgent, string device)
        {
            var temp = userAgent.Substring(userAgent.IndexOf(device) + device.Length).TrimStart();
            var version = string.Empty;

            foreach (var character in temp)
            {
                var validCharacter = false;
                int test = 0;

                if (Int32.TryParse(character.ToString(), out test))
                {
                    version += character;
                    validCharacter = true;
                }

                if (character == '.' || character == '_')
                {
                    version += '.';
                    validCharacter = true;
                }

                if (validCharacter == false)
                    break;
            }

            return version;
        }
        protected string GetUserIPDetails(string url)
        {
            HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
            HttpWebResponse httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            StreamReader streamReader = new StreamReader(httpWebResponse.GetResponseStream());
            string strResponseRead = streamReader.ReadToEnd();
            streamReader.Close();
            streamReader.Dispose();
            return strResponseRead;

        }
        protected string GetIPAddress()
        {
            System.Web.HttpContext context = System.Web.HttpContext.Current;
            string ipAddress = context.Request.ServerVariables["REMOTE_ADDR"];

            if (!string.IsNullOrEmpty(ipAddress))
            {
                string[] addresses = ipAddress.Split(',');
                if (addresses.Length != 0)
                {
                    return addresses[0];
                }
            }

            return context.Request.ServerVariables["REMOTE_ADDR"];
        }
        private void DeleteDirectory()
        {
            try
            {
                string sDirectory = "~/Documents/" + Session.SessionID;
                if (Directory.Exists(HttpContext.Current.Server.MapPath(sDirectory)))
                {
                    Directory.Delete(HttpContext.Current.Server.MapPath(sDirectory), true);
                }
            }
            catch
            {
                //catch the issue
            }
        }

        public bool IsReCaptchValid()
        {
            var result = false;
            var captchaResponse = Request.Form["g-recaptcha-response"];
            var secretKey = "6Lej8boUAAAAAGtDVURmwIEHkYVdPZemESpU4-k2";
            var apiUrl = "https://www.google.com/recaptcha/api/siteverify?secret={0}&response={1}";
            var requestUri = string.Format(apiUrl, secretKey, captchaResponse);
            var request = (HttpWebRequest)WebRequest.Create(requestUri);

            using (WebResponse response = request.GetResponse())
            {
                using (StreamReader stream = new StreamReader(response.GetResponseStream()))
                {
                    JObject jResponse = JObject.Parse(stream.ReadToEnd());
                    var isSuccess = jResponse.Value<bool>("success");
                    result = (isSuccess) ? true : false;
                }
            }
            return result;
        }
        private string GetHashPassword()
        {
            System.Security.Cryptography.MD5CryptoServiceProvider mdservice = new System.Security.Cryptography.MD5CryptoServiceProvider();
            byte[] bs = System.Text.Encoding.UTF8.GetBytes(txtPassword.Value);
            bs = mdservice.ComputeHash(bs);
            System.Text.StringBuilder s = new System.Text.StringBuilder();
            foreach (byte b in bs)
            {
                s.Append(b.ToString("x2"));
            }
            return s.ToString();
        }

        public static string getOperatinSystemDetails(string browserDetails)
        {
            try
            {
                switch (browserDetails.Substring(browserDetails.LastIndexOf("Windows NT") + 11, 3).Trim())
                {
                    case "6.2":
                        return "Windows 8";
                    case "6.1":
                        return "Windows 7";
                    case "6.0":
                        return "Windows Vista";
                    case "5.2":
                        return "Windows XP 64-Bit Edition";
                    case "5.1":
                        return "Windows XP";
                    case "5.0":
                        return "Windows 2000";
                    default:
                        return browserDetails.Substring(browserDetails.LastIndexOf("Windows NT"), 14);
                }
            }
            catch
            {
                if (browserDetails.Length > 149)
                    return browserDetails.Substring(0, 149);
                else
                    return browserDetails;
            }
        }

    }
}