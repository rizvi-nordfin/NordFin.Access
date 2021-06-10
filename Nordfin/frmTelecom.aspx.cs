using Nordfin.workflow.BusinessLayer;
using Nordfin.workflow.PresentationBusinessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Nordfin
{
    public partial class frmTelecom : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        [WebMethod]
        public static string GetTelecomData()
        {

            ITelsonGroupPresentationBusinessLayer objTelsonData = new TelesonGroupBusinessLayer();
            var telecomData = objTelsonData.GetTelecomData();
            string jsonResult = new JavaScriptSerializer().Serialize(telecomData.Item1);
            string jsonChart = new JavaScriptSerializer().Serialize(telecomData.Item2);
            string sResultList = "{\"TelecomList\" :" + jsonResult + "," + "\"TelecomChart\" :" + jsonChart + "}";

            return sResultList;



        }
    }
}