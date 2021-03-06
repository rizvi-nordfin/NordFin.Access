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
using System.Globalization;
using System.Threading;

namespace Nordfin
{
    public partial class ucManualInvoice : UserControl
    {
        private readonly IManualInvoicePresentationBusinessLayer businessLayerObj = new ManualInvoiceBusinessLayer();
        private string standardFile = string.Empty;
        private string fileName = string.Empty;
        private string invoiceNumber = string.Empty;
        private readonly CultureInfo culture = CultureInfo.InvariantCulture;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
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

                var client = businessLayerObj.GetClientPrintDetail(Convert.ToInt32(ClientSession.ClientID));
                var transformationMappings = businessLayerObj.GetTransformationMappings(client.ClientId);
                var transformationHeaders = businessLayerObj.GetTransformationHeaders(client.ClientId);

                var delayMilliSeconds = new Random().Next(100, 1000);
                Thread.Sleep(delayMilliSeconds);
                invoiceNumber = businessLayerObj.GetAndUpdateNumberSeries("Telson").ToString();
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
                    Delivery = hdnDelivery.Value?.Trim(),
                    PaymentReference = Utilities.BuildOcr(invoiceNumber?.Trim(), (invoiceNumber?.Trim().Length).Value + 3, "9", client.Country),
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
                    CustomerType = hdnCustomerType.Value,
                    ClientId = ClientSession.ClientID,
                };
                var invoiceRows = new List<InvoiceRow>();

                var rows = tempTable?.AsEnumerable().ToList();
                int id = 1;
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
                    InvoiceRows = invoiceRows,
                };

                var invoiceFile = Utilities.ConstructInvoiceFile(inv, client, transformationMappings, transformationHeaders);
                standardFile = GenerateStandardXml(invoiceFile);

                ViewState["standardFile"] = standardFile;
                var plainTextBytes = Encoding.UTF8.GetBytes(standardFile);
                var base64Xml = Convert.ToBase64String(plainTextBytes);
                var x = new ManualInvoiceLayout.Input.Xml("NordfinConnec", null, null);
                var base64Pdf = x.ReadFile(base64Xml);
                ViewState["base64Pdf"] = base64Pdf;
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "Pop", "showPDFViewer('" + base64Pdf + "');", true);

                //lblDelivery.Text = hdnDelivery.Value?.Trim();
                //lblInvoiceAmount.Text = txtTotalAmount.Text?.Trim();
                //lblDueDate.Text = txtDueDate.Text?.Trim();
                //ScriptManager.RegisterStartupScript(this, GetType(), "Pop", "showConfirmModal();", true);
                return;
            }
            catch (Exception ex)
            {
                ShowErrorDialog("Error while creating invoice PDF. Try Again!");
                return;
            }
        }

        protected void CreateInvoicePdf(object sender, EventArgs e)
        {
            var client = businessLayerObj.GetClientPrintDetail(Convert.ToInt32(ClientSession.ClientID));
            var transformationMappings = businessLayerObj.GetTransformationMappings(client.ClientId);
            var transformationHeaders = businessLayerObj.GetTransformationHeaders(client.ClientId);

            var delayMilliSeconds = new Random().Next(100, 1000);
            Thread.Sleep(delayMilliSeconds);
            invoiceNumber = businessLayerObj.GetAndUpdateNumberSeries("Telson").ToString();
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
                Delivery = hdnDelivery.Value?.Trim(),
                PaymentReference = Utilities.BuildOcr(invoiceNumber?.Trim(), (invoiceNumber?.Trim().Length).Value + 3, "9", client.Country),
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
                CustomerType = hdnCustomerType.Value,
                ClientId = ClientSession.ClientID,
            };
            var invoiceRows = new List<InvoiceRow>();

            var tempTable = (DataTable)ViewState["gridData"];
            var rows = tempTable?.AsEnumerable().ToList();
            int id = 1;
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
                InvoiceRows = invoiceRows,
            };

            var invoiceFile = Utilities.ConstructInvoiceFile(inv, client, transformationMappings, transformationHeaders);
            standardFile = GenerateStandardXml(invoiceFile);

            ViewState["standardFile"] = standardFile;
            var plainTextBytes = Encoding.UTF8.GetBytes(standardFile);
            var base64Xml = Convert.ToBase64String(plainTextBytes);
            var x = new ManualInvoiceLayout.Input.Xml("NordfinConnec", null, null);
            var base64Pdf = x.ReadFile(base64Xml);
            ViewState["base64Pdf"] = base64Pdf;
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "Pop", "showPDFViewer('" + base64Pdf + "');", true);
        }

        protected void ImportManualInvoice(object sender, EventArgs e)
        {
            invoiceNumber = hdnInvoiceNumber.Value;
            standardFile = (string)ViewState["standardFile"];
            fileName = hdnFileName.Value;
            try
            {
                bool imported = businessLayerObj.ImportManualInvoice(standardFile);
                if (!imported)
                {
                    ShowErrorDialog("Error while importing the invoice. Try Again!");
                    return;
                }

                var pdfUploaded = UploadInvoicePdfToFtp();
                if (!pdfUploaded)
                {
                    ShowErrorDialog("Error while uploading invoice PDF. Try Again!");
                    return;
                }

                var ftpStatus = new FTPFileProcess().UploadStandardXml(standardFile, fileName, bool.Parse(hdnSendToPrint.Value));
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
            var rowInvoice = Convert.ToDouble(!string.IsNullOrWhiteSpace(txtInvAmount.Text) ? txtInvAmount.Text.Trim() : "0", culture);
            var rowVat = Convert.ToDouble(!string.IsNullOrWhiteSpace(txtVat.Text) ? txtVat.Text.Trim() : "0", culture);
            var rowTotal = Convert.ToDouble(!string.IsNullOrWhiteSpace(txtRowTotal.Text) ? txtRowTotal.Text.Trim() : "0", culture);
            var totalInvoice = Convert.ToDouble(!string.IsNullOrWhiteSpace(txtTotalInv.Text) ? txtTotalInv.Text.Trim() : "0", culture);
            var totalVat = Convert.ToDouble(!string.IsNullOrWhiteSpace(txtTotalVat.Text) ? txtTotalVat.Text.Trim() : "0", culture);
            var totalAmount = Convert.ToDouble(!string.IsNullOrWhiteSpace(txtTotalAmount.Text) ? txtTotalAmount.Text.Trim() : "0", culture);
            totalInvoice += rowInvoice;
            totalVat += rowVat;
            totalAmount += rowTotal;
            txtTotalInv.Text = totalInvoice.ToString().Replace(',', '.');
            txtTotalVat.Text = totalVat.ToString().Replace(',', '.');
            txtTotalAmount.Text = totalAmount.ToString().Replace(',', '.');
        }

        private void DeleteRow_SetTotalAmounts(DataRow rowToDelete)
        {
            var rowInvoice = Convert.ToDouble(rowToDelete["InvoiceAmount"].ToString(), culture);
            var rowVat = Convert.ToDouble(rowToDelete["VATAmount"].ToString(), culture);
            var rowTotal = Convert.ToDouble(rowToDelete["TotalAmount"].ToString(), culture);
            var totalInvoice = Convert.ToDouble(!string.IsNullOrWhiteSpace(txtTotalInv.Text) ? txtTotalInv.Text.Trim() : "0", culture);
            var totalVat = Convert.ToDouble(!string.IsNullOrWhiteSpace(txtTotalVat.Text) ? txtTotalVat.Text.Trim() : "0", culture);
            var totalAmount = Convert.ToDouble(!string.IsNullOrWhiteSpace(txtTotalAmount.Text) ? txtTotalAmount.Text.Trim() : "0", culture);
            totalInvoice -= rowInvoice;
            totalVat -= rowVat;
            totalAmount -= rowTotal;
            txtTotalInv.Text = totalInvoice.ToString().Replace(',', '.');
            txtTotalVat.Text = totalVat.ToString().Replace(',', '.');
            txtTotalAmount.Text = totalAmount.ToString().Replace(',', '.');
        } 

        private void ShowErrorDialog(string errorMessage)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "Pop", "showErrorModal('" + errorMessage + "');", true);
        }

        private bool UploadInvoicePdfToFtp()
        {
            var folderName = (HiddenField)Parent.FindControl("hdnClientName");
            var clientName = ClientSession.ClientName;
            string subFolderName = folderName.Value.Substring(folderName.Value.LastIndexOf("/") + 1) + Utilities.Execute(hdnInvoiceNumber.Value);
            string sFileExt = ConfigurationManager.AppSettings["FileExtension"].ToString();
            string sFileName = (clientName.IndexOf('_') == -1 ? clientName : clientName.Substring(0, clientName.IndexOf('_'))) + "_" + hdnInvoiceNumber.Value + "_" + "inv" + "." + sFileExt;
            string base64Pdf = ViewState["base64Pdf"]?.ToString();
            var bResult = new FTPFileProcess().UploadInvoicePdf(base64Pdf, folderName.Value, subFolderName, sFileName);
            return bResult == FtpStatusCode.ClosingData;
        }
    }
}