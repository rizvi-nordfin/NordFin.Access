using Nordfin.workflow.BusinessLayer;
using Nordfin.workflow.Entity;
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
    public partial class frmTeleson : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        [WebMethod]
        public static string GetTelsonData()
        {
           
            ITelsonGroupPresentationBusinessLayer objTelsonData= new TelesonGroupBusinessLayer();
            var telsonGroupData = objTelsonData.GetTelsonGroupData(ClientSession.ClientID);
            string jsonResult = new JavaScriptSerializer().Serialize(telsonGroupData.Item1);
            string jsonChart = new JavaScriptSerializer().Serialize(telsonGroupData.Item2);
            string sResultList = "{\"TelsonList\" :" + jsonResult + "," + "\"TelsonChart\" :" + jsonChart + "}";
          
            return sResultList;



        }
    }
}