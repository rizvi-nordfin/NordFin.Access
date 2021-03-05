using ClosedXML.Excel;
using Ionic.Zip;
using Nordfin.workflow.Business;
using Nordfin.workflow.Entity;
using Nordfin.workflow.PresentationBusinessLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Nordfin
{
    public partial class frmPdfMultiDownload : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string fileName = Request.QueryString["FileName"];
                if (fileName.ToUpper() == "ZIP")
                {
                    DownloadZipFile();
                }
                else if(fileName.ToUpper() == "EXPORT")
                {
                    DownloadExport();
                }
                else if (fileName.ToUpper() == "EXPORTDETAIL")
                {
                    DownloadExportDetail();
                }
                else
                {
                    Response.ContentType = "application/pdf";
                    Response.AppendHeader("Content-Disposition", "attachment;filename=" + fileName);
                    Response.TransmitFile(Server.MapPath("~/Documents/" + Session.SessionID + "/" + fileName));
                    Response.End();
                }
            }
        }

        public void DownloadZipFile()
        {
            string InvoiceNum= (string)Session["custNum"];
            List<FilesDownload> fileList = (List<FilesDownload>)Session["FileList"];
            using (ZipFile zip = new ZipFile())
            {
                zip.AlternateEncodingUsage = ZipOption.AsNecessary;
                zip.UseZip64WhenSaving = Zip64Option.AsNecessary;// ZipOption.AsNecessary;


                foreach (FilesDownload file in fileList)
                {
                    zip.AddEntry(file.FileName, file.Bytes);
                }

                Response.Clear();
                Response.BufferOutput = false;
                Response.Buffer = false;
                string zipName = "Invoices_" + InvoiceNum + ".zip";
                Response.ContentType = "application/zip";
                Response.AddHeader("content-disposition", "attachment; filename=" + zipName);
                zip.Save(Response.OutputStream);
                


                Response.End();
            }
        }
        public void DownloadExport()
        {
            HttpResponse response = HttpContext.Current.Response;
            response.Clear();
            response.Charset = "";

            DataTable dataTable = (DataTable)Session["CustomerGrid"];
          
            foreach (DataColumn column in dataTable.Columns)
                column.ColumnName = column.ColumnName.ToUpper();
            using (XLWorkbook wb = new XLWorkbook())
            {


                var ws = wb.Worksheets.Add(dataTable);
                ws.Unprotect();
                ws.Column(1).Delete();
                ws.Column(1).Delete();
                ws.Column(13).Delete();
                ws.Column(14).Delete();
                ws.Column(13).Delete();




                Response.Clear();
                Response.Buffer = true;
                Response.Charset = "";
                string filename = "CustomerInvoices" + DateTime.Now.ToString("yyyy-MM-dd hh:mm tt") + ".xlsx";
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
        public void DownloadExportDetail()
        {
            IInvoicesPresentationBusinessLayer objInvoicesLayer = new InvoicesBusinessLayer();
            DataSet dataSet = objInvoicesLayer.getExportdetails((string)Session["custNum"], ClientSession.ClientID);

            using (XLWorkbook wb = new XLWorkbook())
            {
                var ws = wb.Worksheets.Add(dataSet.Tables[0]);
                ws.Tables.FirstOrDefault().ShowAutoFilter = true;
                ws.Tables.FirstOrDefault().Theme = XLTableTheme.TableStyleLight16;
                wb.Worksheet(1).Columns().AdjustToContents();
                DataTable dataTable = dataSet.Tables[0];
                for (int i = 0; i < dataTable.Rows.Count; i++)
                {
                    string str = "A" + (i + 2).ToString() + ":" + "Q" + (i + 2).ToString();
                    if (dataTable.Rows[i].ItemArray[0].ToString().ToUpper() == "INVOICES")
                    {
                        ws.Range(str).Style.Fill.BackgroundColor = XLColor.FromHtml("#CCCCFF");
                    }
                    else
                    {
                        ws.Range(str).Style.Fill.BackgroundColor = XLColor.FromHtml("#FFFFFF");
                    }

                }
                Response.Clear();
                Response.Buffer = true;
                Response.Charset = "";
                string filename = "CustomersDetailsReoprt" + DateTime.Now.ToString("yyyy-MM-dd hh:mm tt") + ".xlsx";
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