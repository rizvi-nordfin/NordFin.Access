using Nordfin.workflow.BusinessLayer;
using Nordfin.workflow.PresentationBusinessLayer;
using Nordfin.workflow.Entity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Web.UI;
using System.Linq;
using System.IO;
using System.Xml.Serialization;
using System.Xml;
using System.Net;
using System.Text;
using System.Web.UI.WebControls;
using System.Configuration;

namespace Nordfin
{
    public partial class ucManualInvoice : UserControl
    {
        private readonly IManualInvoicePresentationBusinessLayer businessLayerObj = new ManualInvoiceBusinessLayer();
        private string standardFile = string.Empty;
        private string fileName = string.Empty;
        private string invoiceNumber = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                lblInvoiceNumber.Text = businessLayerObj.GetNumberSeries("Telson").ToString();
                var vatlistItems = new List<ListItem>();
                var currencylistItems = new List<ListItem>();
                if (ClientSession.ClientLand == "FI")
                {
                    vatlistItems.Add(new ListItem("24%", "24"));
                    currencylistItems.Add(new ListItem("EUR", "EUR"));
                }
                if (ClientSession.ClientLand == "SE")
                {
                    vatlistItems.Add(new ListItem("25%", "25"));
                    currencylistItems.Add(new ListItem("SEK", "SEK"));
                }
                vatlistItems.Add(new ListItem("0%", "0"));
                drpVat.DataSource = vatlistItems;
                drpVat.DataValueField = "Value";
                drpVat.DataTextField = "Text";
                drpVat.DataBind();

                drpCurrency.DataSource = currencylistItems;
                drpCurrency.DataValueField = "Value";
                drpCurrency.DataTextField = "Text";
                drpCurrency.DataBind();
                InitializeGrid();
            }
            else
            {
                spnTitle.InnerText = hdnTitle.Value;
            }
        }

        protected void ClosePopup(object sender, EventArgs e)
        {
            ResetAndClose();
        }

        protected void AddRows_Click(object sender, EventArgs e)
        {
            var tempTable = (DataTable)ViewState["gridData"];
            if (tempTable.Columns.Count == 0)
            {
                tempTable.Columns.Add("Article");
                tempTable.Columns.Add("Description");
                tempTable.Columns.Add("Period");
                tempTable.Columns.Add("Unit");
                tempTable.Columns.Add("Quantity");
                tempTable.Columns.Add("ItemPrice");
                tempTable.Columns.Add("VATPercent");
                tempTable.Columns.Add("InvoiceAmount");
                tempTable.Columns.Add("VATAmount");
                tempTable.Columns.Add("TotalAmount");
            }
            var tempRow = tempTable.NewRow();
            tempRow["Article"] = txtArticle.Text?.Trim();
            tempRow["Description"] = txtDescription.Text?.Trim();
            tempRow["Period"] = txtPeriod.Text?.Trim();
            tempRow["Unit"] = txtUnit.Text?.Trim();
            tempRow["Quantity"] = txtQuantity.Text?.Trim();
            tempRow["ItemPrice"] = txtAmount.Text?.Trim();
            tempRow["InvoiceAmount"] = txtInvAmount.Text?.Trim();
            tempRow["VATAmount"] = txtVat.Text?.Trim();
            tempRow["VATPercent"] = drpVat.Text?.Trim();
            tempRow["TotalAmount"] = txtRowTotal.Text?.Trim();

            tempTable.Rows.Add(tempRow);
            ViewState["gridData"] = tempTable;

            // Attach Gridview Datasource to datatable
            grdInvoiceRows.DataSource = tempTable;
            grdInvoiceRows.DataBind();
            btnGridDelete.Enabled = true;
            AddRow_SetTotalAmounts();
        }

        protected void CreateManualInvoice(object sender, EventArgs e)
        {
            try
            {
                var tempTable = (DataTable)ViewState["gridData"];
                if (tempTable.Rows.Count == 0)
                {
                    string errorMessage = "Add atleast one invoice row to create the invoice";
                    ScriptManager.RegisterStartupScript(this, GetType(), "Pop", "showErrorModal('" + errorMessage + "');", true);
                    return;
                }
                var invoiceFile = new InvoiceFile();
                invoiceNumber = lblInvoiceNumber.Text?.Trim();
                hdnInvoiceNumber.Value = invoiceNumber;
                fileName = $"ManualInv_FA_" + ClientSession.ClientName.Split(' ')[0] + "_" + invoiceNumber + ".xml";
                hdnFileName.Value = fileName;
                var invoice = new Invoice
                {
                    InvoiceNumber = invoiceNumber,
                    ConnectionId = "0",
                    BillDate = !string.IsNullOrEmpty(txtInvDate.Text?.Trim()) ? txtInvDate.Text?.Trim() : DateTime.Today.ToString("yyyy-MM-dd"),
                    DueDate = !string.IsNullOrEmpty(txtDueDate.Text?.Trim()) ? txtDueDate.Text?.Trim() : DateTime.Today.AddDays(30).ToString("yyyy-MM-dd"),
                    CustomerNumber = txtCustNum.Text?.Trim(),
                    OrderNumber = txtCustNum.Text?.Trim(),
                    ClientID = ClientSession.ClientID,
                    Purchased = "0",
                    FileName = fileName,
                    Delivery = drpInvDelivery.SelectedValue?.Trim(),
                    PaymentReference = Utilities.BuildOcr(lblInvoiceNumber.Text?.Trim(), (lblInvoiceNumber.Text?.Trim().Length).Value + 3, "9", "Sweden"),
                    CurrencyCode = drpCurrency.Text?.Trim(),
                    InvoiceAmount = txtTotalAmount.Text?.Trim(),
                    RemainingAmount = txtTotalAmount.Text?.Trim(),
                    InvoiceVATAmount = txtTotalVat.Text?.Trim()
                };

                var customer = new Customer
                {
                    CustomerNumber = txtCustNum.Text?.Trim(),
                    CustomerName = txtCustName.Text?.Trim(),
                    CustomerAddress = txtCustContact.Text?.Trim(),
                    CustomerAddress2 = txtCustAddress.Text?.Trim(),
                    CustomerCity = txtCustCity.Text?.Trim(),
                    CustomerPostalCode = txtCustPostCode.Text?.Trim(),
                    CustomerType = "PRV",
                    ClientId = ClientSession.ClientID,
                };

                var invoiceRows = new List<InvoiceRow>();
                var rows = tempTable?.AsEnumerable().ToList();
                int id = 1;
                var firstDayOfMonth = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);
                var lastDayOfMonth = firstDayOfMonth.AddMonths(1).AddDays(-1);
                foreach (var item in rows)
                {
                    var invoiceRow = new InvoiceRow
                    {
                        Id = id,
                        Number = item["Article"].ToString(),
                        Description = item["Description"].ToString(),
                        Period = item["Period"].ToString(),
                        Unit = item["Unit"].ToString(),
                        Quantity = item["Quantity"].ToString(),
                        Total = item["TotalAmount"].ToString(),
                        Price = item["InvoiceAmount"].ToString(),
                        VatAmount = item["VATAmount"].ToString(),
                        VatPercent = item["VATPercent"].ToString(),
                    };
                    invoiceRows.Add(invoiceRow);
                    id++;
                }

                var inv = new Inv
                {
                    Invoice = invoice,
                    Customer = customer,
                    Print = new Print
                    {
                        InvoiceRows = invoiceRows
                    }
                };
                var client = new Client
                {
                    ClientId = ClientSession.ClientID,
                    ClientName = ClientSession.ClientName
                };

                invoiceFile.Client = client;
                invoiceFile.Invoices.Add(inv);

                standardFile = GenerateStandardXml(invoiceFile);
                ViewState["standardFile"] = standardFile;

                //Generate PDF
                var plainTextBytes = Encoding.UTF8.GetBytes(standardFile);
                var base64Xml = Convert.ToBase64String(plainTextBytes);
                string connString = ConfigurationManager.ConnectionStrings["NordfinConnec"].ToString();
                var x = new ManualInvoiceLayout.Input.Xml(connString);
                var base64Pdf = x.ReadFile(base64Xml);
                ViewState["base64Pdf"] = base64Pdf;
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "Pop", "showPDFViewer('" + base64Pdf + "');", true);
            }
            catch(Exception ex)
            {
                ShowErrorDialog("Error while creating invoice PDF. Try Again!");
                return;
            }
        }

        protected void ImportManualInvoice(object sender, EventArgs e)
        {
            invoiceNumber = hdnInvoiceNumber.Value;
            standardFile = (string)ViewState["standardFile"];
            fileName = hdnFileName.Value;
            try
            {
                var pdfUploaded = UploadInvoicePdfToFtp();
                if (!pdfUploaded)
                {
                    ShowErrorDialog("Error while uploading invoice PDF. Try Again!");
                    return;
                }

                bool imported = businessLayerObj.ImportManualInvoice(standardFile);
                int.TryParse(invoiceNumber, out int oldSeries);
                businessLayerObj.UpdateNumberSeries("Telson", oldSeries + 1);
                if (!imported)
                {
                    ShowErrorDialog("Error while importing the invoice. Try Again!");
                    return;
                }

                var ftpStatus = new FTPFileProcess().UploadStandardXml(standardFile, fileName);
                if (ftpStatus != FtpStatusCode.ClosingData)
                {
                    ShowErrorDialog("Error while uploading invoice XML. Try Again!");
                    return;
                }

                ResetControls();
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ClosePDF", "showSuccessModal();", true);
                grdInvoiceRows.SelectedIndex = -1;
            }
            catch
            {
                ShowErrorDialog("Error while importing the invoice. Try Again!");
            }
        }

        protected void grdInvoiceRows_OnRowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Attributes["onclick"] = Page.ClientScript.GetPostBackClientHyperlink(grdInvoiceRows, "Select$" + e.Row.RowIndex);
                e.Row.ToolTip = "Select the row to Delete";
            }
        }

        protected void grdInvoiceRows_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        protected override void Render(HtmlTextWriter writer)
        {
            foreach (GridViewRow r in grdInvoiceRows.Rows)
            {
                if (r.RowType == DataControlRowType.DataRow)
                {
                    Page.ClientScript.RegisterForEventValidation(grdInvoiceRows.UniqueID, "Select$" + r.RowIndex);
                }
            }

            base.Render(writer);
        }

        protected void DeleteRows(object sender, EventArgs e)
        {
            var selectedRow = grdInvoiceRows.SelectedRow;
            var gridData = (DataTable)ViewState["gridData"];
            var selectedRowData = gridData.AsEnumerable().ElementAt(selectedRow.RowIndex);
            DeleteRow_SetTotalAmounts(selectedRowData);
            selectedRowData?.Delete();
            ViewState["gridData"] = gridData;
            grdInvoiceRows.DataSource = gridData;
            grdInvoiceRows.DataBind();
        }

        private string GenerateStandardXml(InvoiceFile invoiceFile)
        {
            StringWriter stringWriter = new StringWriter();
            try
            {
                XmlSerializer serializer = new XmlSerializer(invoiceFile.GetType());
                XmlTextWriter textWriter = new XmlTextWriter(stringWriter);
                serializer.Serialize(textWriter, invoiceFile);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return stringWriter.ToString();
        }

        private void ResetControls()
        {
            txtInvAmount.Text = string.Empty;
            txtDescription.Text = string.Empty;
            txtUnit.Text = string.Empty;
            txtQuantity.Text = "1";
            txtArticle.Text = string.Empty;
            txtPeriod.Text = string.Empty;
            txtVat.Text = string.Empty;
            txtAmount.Text = string.Empty;
            txtTotalVat.Text = string.Empty;
            txtTotalInv.Text = string.Empty;
            txtTotalAmount.Text = string.Empty;
            hdnFileName.Value = string.Empty;
            hdnInvoiceNumber.Value = string.Empty;
            InitializeGrid();
        }

        private void ResetAndClose()
        {
            ResetControls();
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "CloseManualInvoice", "closeManualInvoice();", true);
        }

        private void InitializeGrid()
        {
            var tempTable = new DataTable();
            ViewState["gridData"] = tempTable;

            // Attach Gridview Datasource to datatable
            grdInvoiceRows.DataSource = tempTable;
            grdInvoiceRows.DataBind();
        }

        private void AddRow_SetTotalAmounts()
        {
            double.TryParse(txtInvAmount.Text?.Trim(), out double rowInvoice);
            double.TryParse(txtVat.Text?.Trim(), out double rowVat);
            double.TryParse(txtRowTotal.Text?.Trim(), out double rowTotal);
            double.TryParse(txtTotalInv.Text?.Trim(), out double totalInvoice);
            double.TryParse(txtTotalVat.Text?.Trim(), out double totalVat);
            double.TryParse(txtTotalAmount.Text?.Trim(), out double totalAmount);
            totalInvoice += rowInvoice;
            totalVat += rowVat;
            totalAmount += rowTotal;
            txtTotalInv.Text = totalInvoice.ToString();
            txtTotalVat.Text = totalVat.ToString();
            txtTotalAmount.Text = totalAmount.ToString();
        }

        private void DeleteRow_SetTotalAmounts(DataRow rowToDelete)
        {
            double.TryParse(rowToDelete["InvoiceAmount"].ToString(), out double rowInvoice);
            double.TryParse(rowToDelete["VATAmount"].ToString(), out double rowVat);
            double.TryParse(rowToDelete["TotalAmount"].ToString(), out double rowTotal);
            double.TryParse(txtTotalInv.Text?.Trim(), out double totalInvoice);
            double.TryParse(txtTotalVat.Text?.Trim(), out double totalVat);
            double.TryParse(txtTotalAmount.Text?.Trim(), out double totalAmount);
            totalInvoice -= rowInvoice;
            totalVat -= rowVat;
            totalAmount -= rowTotal;
            txtTotalInv.Text = totalInvoice.ToString();
            txtTotalVat.Text = totalVat.ToString();
            txtTotalAmount.Text = totalAmount.ToString();
        }

        private void ShowErrorDialog(string errorMessage)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "Pop", "showErrorModal('" + errorMessage + "');", true);
        }

        private bool UploadInvoicePdfToFtp()
        {
            var hdnClientName = (HiddenField)Parent.FindControl("hdnClientName");
            var hdnFileName = (HiddenField)Parent.FindControl("hdnFileName");
            string subFolderName = hdnClientName.Value.Substring(hdnClientName.Value.LastIndexOf("/") + 1) + Utilities.Execute(hdnInvoiceNumber.Value);
            string sFileExt = ConfigurationManager.AppSettings["FileExtension"].ToString();
            string sFileName = (hdnFileName.Value.IndexOf('_') == -1 ? hdnFileName.Value : hdnFileName.Value.Substring(0, hdnFileName.Value.IndexOf('_'))) + "_" + hdnInvoiceNumber.Value + "_" + "inv" + "." + sFileExt;
            string base64Pdf = ViewState["base64Pdf"]?.ToString();
            var bResult = new FTPFileProcess().UploadInvoicePdf(base64Pdf, hdnClientName.Value, subFolderName, sFileName);
            return bResult == FtpStatusCode.ClosingData;
        }
    }
}