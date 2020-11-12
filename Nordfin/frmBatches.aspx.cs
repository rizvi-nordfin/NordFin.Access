using ClosedXML.Excel;
using Nordfin.workflow.BusinessLayer;
using Nordfin.workflow.PresentationBusinessLayer;
using System;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;

namespace Nordfin
{
    public partial class frmBatches : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            ClearSession();
            if (!IsPostBack)
            {
                IBatchPresentationBusinessLayer objInvoicesLayer = new BatchesBusinessLayer();
                DataSet ds = objInvoicesLayer.getInvoicesBatches(0);
                grdInvoicesPaid.DataSource = ds.Tables[0];
                grdInvoicesPaid.DataBind();

                grdInvoicesUnPaid.DataSource = ds.Tables[1];
                grdInvoicesUnPaid.DataBind();


                ds.Tables[2].AsEnumerable().ToList<DataRow>().ForEach(a =>
                {
                    for (int i = 2; i < a.ItemArray.Length; i++)
                    {
                        int number = int.Parse(a.ItemArray[i].ToString());
                        if (number > 0)
                            a[i] = String.Format(CultureInfo.GetCultureInfo("sv-SE"), "{0:### ###}", number);
                    }

                });


                grdInvoicesPerClient.DataSource = ds.Tables[2];
                grdInvoicesPerClient.DataBind();



                for (int i = 0; i < ds.Tables[3].Rows.Count; i++)
                {

                    for (int j = 2; j < ds.Tables[3].Rows[i].ItemArray.Length; j++)
                    {
                        if (ds.Tables[3].Rows[i].ItemArray[j].ToString().IndexOf(",") > 0)
                            ds.Tables[3].Rows[i][j] = ds.Tables[3].Rows[i].ItemArray[j].ToString().Substring(0, ds.Tables[3].Rows[i].ItemArray[j].ToString().IndexOf(','));
                    }
                    ds.Tables[3].Rows[i]["Total"] = string.Format(CultureInfo.GetCultureInfo("sv-SE"), "{0:#,###0}", decimal.Truncate(ConvertStringToDecimal(ds.Tables[3].Rows[i]["Total"].ToString())));
                }
                grdInvoicesAmount.DataSource = ds.Tables[3];
                grdInvoicesAmount.DataBind();
            }
        }




        public decimal ConvertStringToDecimal(string sDecimal)
        {
            CultureInfo cultures = new CultureInfo("en-US");

            return Convert.ToDecimal(sDecimal.Replace(" ", ""), cultures);
        }

        protected void btnPaidBatches_Click(object sender, EventArgs e)
        {
            IBatchPresentationBusinessLayer objInvoicesLayer = new BatchesBusinessLayer();
            DataSet ds = objInvoicesLayer.getInvoicesBatches(1);
            ExporttoExcel(ds.Tables[0], "Invoice_Paid_Batches_");
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
                catch
                {
                    //catch the issue
                }
                Session.Abandon();
                Response.Redirect("frmLogin.aspx");

            }
        }

        protected void btnUnpaidBatches_Click(object sender, EventArgs e)
        {
            IBatchPresentationBusinessLayer objInvoicesLayer = new BatchesBusinessLayer();
            DataSet ds = objInvoicesLayer.getInvoicesBatches(1);
            ExporttoExcel(ds.Tables[1], "Invoice_UnPaid_Batches_");
        }

        protected void btnTotalInvoice_Click(object sender, EventArgs e)
        {
            IBatchPresentationBusinessLayer objInvoicesLayer = new BatchesBusinessLayer();
            DataSet ds = objInvoicesLayer.getInvoicesBatches(1);
            ExporttoExcel(ds.Tables[2], "TotalInvoicePerClient_");
        }

        protected void btnTotalInvoiceAmount_Click(object sender, EventArgs e)
        {
            IBatchPresentationBusinessLayer objInvoicesLayer = new BatchesBusinessLayer();
            DataSet ds = objInvoicesLayer.getInvoicesBatches(1);
            ExporttoExcel(ds.Tables[3], "TotalInvoiceAmountPerClient_");
        }

        public void ExporttoExcel(DataTable dataTable, string sFileName)
        {
            HttpResponse response = HttpContext.Current.Response;
            response.Clear();
            response.Charset = "";

            using (XLWorkbook wb = new XLWorkbook())
            {
                wb.Worksheets.Add(dataTable);
                Response.Clear();
                Response.Buffer = true;
                Response.Charset = "";
                string filename = sFileName + DateTime.Now.ToString("yyyy-MM-dd hh:mm tt") + ".xlsx";
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                Response.AddHeader("Content-Disposition", "attachment;filename=\"" + filename + "\"");
                using (MemoryStream MyMemoryStream = new MemoryStream())
                {
                    wb.SaveAs(MyMemoryStream);
                    MyMemoryStream.WriteTo(Response.OutputStream);
                    Response.Flush();
                    Response.End();
                }
            }


        }
    }
}