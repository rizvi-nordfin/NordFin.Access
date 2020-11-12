using ClosedXML.Excel;
using Nordfin.workflow.Business;
using Nordfin.workflow.PresentationBusinessLayer;
using System;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
namespace Nordfin
{
    public partial class frmReports : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            ClearSession();

            hdnAdmin.Value = ClientSession.Admin;
        }

        protected void btnLederlistReport_Click(object sender, EventArgs e)
        {
            IReportsPresentationBusinessLayer objReportsLayer = new ReportsBusinessLayer();
            ExporttoExcel(objReportsLayer.GetLedgerlistReport(ClientSession.ClientID), "LedgerList");
        }

        protected void btnBatchesReport_Click(object sender, EventArgs e)
        {
            IReportsPresentationBusinessLayer objReportsLayer = new ReportsBusinessLayer();
            ExporttoExcel(objReportsLayer.GetBatchesReport(ClientSession.ClientID), "Batches");
        }

        public void ExporttoExcel(DataSet dataSet, string sFileName)
        {
            HttpResponse response = HttpContext.Current.Response;
            response.Clear();
            response.Charset = "";


            using (XLWorkbook wb = new XLWorkbook())
            {
                wb.Worksheets.Add(dataSet.Tables[0]);
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

        protected void btnCustomerList_Click(object sender, EventArgs e)
        {
            IReportsPresentationBusinessLayer objReportsLayer = new ReportsBusinessLayer();
            ExporttoExcel(objReportsLayer.GetCustomerListReport(ClientSession.ClientID), "CustomerList");
        }



        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            IReportsPresentationBusinessLayer objReportsLayer = new ReportsBusinessLayer();
            if (hdnExport.Value == "Periodic report")
            {
                ExportPeriodicreport(objReportsLayer.usp_getPeriodicReport(ClientSession.ClientID, txtFromDate.Text.Trim(), txtToDate.Text.Trim()), "PeriodicReport_" + txtFromDate.Text.Trim() + "_" + txtToDate.Text.Trim());

            }
            else
            {

                ExporttoExcel(objReportsLayer.usp_getInvoicePeriodReport(ClientSession.ClientID, txtFromDate.Text.Trim(), txtToDate.Text.Trim()), "InvoicePeriod");
            }
        }

        public void ExportPeriodicreport(DataSet dataSet, string sFileName)
        {
            HttpResponse response = HttpContext.Current.Response;
            response.Clear();
            response.Charset = "";


            using (XLWorkbook wb = new XLWorkbook())
            {

                if (dataSet.Tables.Count > 0)
                {

                    for (int i = 0; i < dataSet.Tables[0].Columns.Count; i++)
                    {
                        int iNum = 0;
                        bool bResult = int.TryParse(dataSet.Tables[0].Columns[i].ColumnName, out iNum);
                        if (bResult)
                        {
                            string sColumnName = " ";
                            for (int j = 0; j < iNum; j++)
                            {
                                sColumnName += " ";
                            }
                            dataSet.Tables[0].Columns[i].ColumnName = sColumnName;
                        }


                    }

                    for (int i = 0; i < dataSet.Tables[2].Columns.Count; i++)
                    {
                        int iNum = 0;
                        bool bResult = int.TryParse(dataSet.Tables[2].Columns[0].ColumnName, out iNum);
                        if (bResult)
                        {
                            string sColumnName = " ";
                            for (int j = 0; j < iNum; j++)
                            {
                                sColumnName += " ";
                            }
                            dataSet.Tables[2].Columns[i].ColumnName = sColumnName;
                        }


                    }


                    for (int i = 0; i < dataSet.Tables[4].Columns.Count; i++)
                    {
                        int iNum = 0;
                        bool bResult = int.TryParse(dataSet.Tables[4].Columns[i].ColumnName, out iNum);
                        if (bResult)
                        {
                            string sColumnName = " ";
                            for (int j = 0; j < iNum; j++)
                            {
                                sColumnName += " ";
                            }
                            dataSet.Tables[4].Columns[i].ColumnName = sColumnName;
                        }


                    }



                }


                var ws = wb.Worksheets.Add(dataSet.Tables[0]);
                ws.Tables.FirstOrDefault().SetShowAutoFilter(false);

                var ws1 = wb.Worksheet(1).Cell(7, 1).InsertTable(dataSet.Tables[1]);
                ws1.SetShowAutoFilter(false);


                var ws2 = wb.Worksheet(1).Cell(9, 1).InsertTable(dataSet.Tables[2]);
                ws2.SetShowAutoFilter(false);

                var ws3 = wb.Worksheet(1).Cell(19, 1).InsertTable(dataSet.Tables[3]);
                ws3.SetShowAutoFilter(false);

                var ws4 = wb.Worksheet(1).Cell(21, 1).InsertTable(dataSet.Tables[4]);
                ws4.SetShowAutoFilter(false);

                wb.Worksheet(1).Columns().AdjustToContents();




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

        protected void btnContested_Click(object sender, EventArgs e)
        {
            IReportsPresentationBusinessLayer objReportsLayer = new ReportsBusinessLayer();
            ExporttoExcel(objReportsLayer.GetContestedReport(ClientSession.ClientID), "ContestedList");
        }
    }
}