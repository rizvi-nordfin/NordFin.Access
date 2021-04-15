using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Nordfin
{
    public partial class frmCreditCheck : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {


            if (!IsPostBack)
            {
                grdCreditCheck.DataSource = new List<string>();
                grdCreditCheck.DataBind();
            }
        }

        protected void btnCreditCheck_Click(object sender, EventArgs e)
        {
           
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

        public  void SetCookie(string cookieName,string Name)
        {
            
         
            HttpCookie cookie = HttpContext.Current.Response.Cookies.AllKeys.Contains(cookieName)? HttpContext.Current.Response.Cookies[cookieName]
                                 : HttpContext.Current.Request.Cookies[cookieName];
            if (cookie == null)
            {
                cookie = new HttpCookie(cookieName);
                if (!String.IsNullOrEmpty(Name))
                    cookie.Values.Set(Name, txtUserName.Text);
                cookie.HttpOnly = true;
                cookie.Secure = true;
                cookie.SameSite = SameSiteMode.Strict;
                HttpContext.Current.Response.Cookies.Set(cookie);
            }
           
        }
    }
}