using Newtonsoft.Json;
using Nordfin.workflow.BusinessLayer;
using Nordfin.workflow.PresentationBusinessLayer;
using System;
using System.Data;
using System.IO;
using System.Web;
using System.Web.Services;

namespace Nordfin
{
    public partial class frmInvoiceBatches : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            ClearSession();

        }



        [WebMethod]
        public static string LoadInvoiceBatches()
        {
            ClearSession();
            IBatchPresentationBusinessLayer objInvoiceBatchesLayer = new BatchesBusinessLayer();
            DataSet ds = objInvoiceBatchesLayer.getInvoicesBatchesReports(ClientSession.ClientID);

            string jsonBatchList = DataTableToJSONWithJSONNet(ds.Tables[0]);

            string jsonSummaryBatchList = DataTableToJSONWithJSONNet(ds.Tables[1]);

            string sResultList = "{\"InvoiceBatchesList\" :" + jsonBatchList + "," + "\"SummaryBatchList\" :" + jsonSummaryBatchList + "}";

            return sResultList;
        }

        public static string DataTableToJSONWithJSONNet(DataTable table)
        {
            string JSONString = string.Empty;
            JSONString = JsonConvert.SerializeObject(table);
            return JSONString;
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


    }
}