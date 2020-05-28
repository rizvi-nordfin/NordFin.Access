using Nordfin.workflow.BusinessLayer;
using Nordfin.workflow.Entity;
using Nordfin.workflow.PresentationBusinessLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Nordfin
{
    public partial class frmTrafficDetails : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ClearSession();
                IUserLoginInformationPresentationBusinessLayer objLoginInformLayer = new UserLoginInformationBusinessLayer();
                DataSet ds = objLoginInformLayer.GetTrafficDetails();
                if (ds.Tables[0].Rows.Count > 0)
                {
                    grdTrafficDetails.DataSource = ds.Tables[0];
                    grdTrafficDetails.DataBind();
                }
                if (ds.Tables[1].Rows.Count > 0)
                {
                    hdnSuccess.Value =Convert.ToString(ds.Tables[1].Rows[0].Field<int>("Success"));
                    hdnFail.Value = Convert.ToString(ds.Tables[1].Rows[0].Field<int>("Fail"));
                }
                try
                {
                    Session["GarphData"] = ds.Tables[2];
                }
                catch { }
            }
        }

        public void ClearSession()
        {
            if (ClientSession.ClientID.Trim() == "")
            {
                try
                {

                    string sDirectory = "~/Documents/" + Session.SessionID;
                    if (Directory.Exists(Server.MapPath(sDirectory)))
                    {
                        Directory.Delete(Server.MapPath(sDirectory), true);
                    }
                }
                catch { }
                Session.Abandon();
                Response.Redirect("frmLogin.aspx");

            }
        }


        [WebMethod]
        public static string GetGraphData()
        {
            IList<UserChartData> userChartDataList = new List<UserChartData>();

            DataTable dataTable = (DataTable)HttpContext.Current.Session["GarphData"];



            if (dataTable != null && dataTable.Rows.Count > 0)
            {
                userChartDataList = dataTable.AsEnumerable().Select(dataRow => new UserChartData
                {
                    LogDate = Convert.ToString(dataRow.Field<int>("LogDate")),
                    Success = dataRow.Field<int>("Success"),
                    Failure = dataRow.Field<int>("Fail")


                }).ToList();
            }

            string jsonResult = new JavaScriptSerializer().Serialize(userChartDataList);
            string sResultList = "{\"Result\" :" + jsonResult + "}";
            return sResultList;


           
        }
    }
}