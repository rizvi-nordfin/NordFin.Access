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
using System.Drawing;

namespace Nordfin
{
    public partial class ucManualInvoice : UserControl
    {
        private IManualInvoicePresentationBusinessLayer businessLayerObj = new ManualInvoiceBusinessLayer();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                lblInvoiceNumber.Text = businessLayerObj.GetNumberSeries("Telson").ToString();
                InitializeGrid();
            }
        }

        protected void ClosePopup(object sender, EventArgs e)
        {
            ResetAndClose();
        }

        protected void AddRows_Click(object sender, EventArgs e)
        {
            var tempTable = (DataTable)ViewState["gridData"];
            if(tempTable.Columns.Count==0)
            {
                tempTable.Columns.Add("Article");
                tempTable.Columns.Add("Description");
                tempTable.Columns.Add("Unit");
                tempTable.Columns.Add("Quantity");
                tempTable.Columns.Add("InvoiceAmount");
                tempTable.Columns.Add("VATPercent");
                tempTable.Columns.Add("VATAmount");
                tempTable.Columns.Add("TotalAmount");
            }
            var tempRow = tempTable.NewRow();
            tempRow["Article"] = txtArticle.Text?.Trim();
            tempRow["Description"] = txtDescription.Text?.Trim();
            tempRow["Unit"] = txtUnit.Text?.Trim();
            tempRow["Quantity"] = txtQuantity.Text?.Trim();
            tempRow["InvoiceAmount"] = txtInvAmount.Text?.Trim();
            tempRow["VATAmount"] = txtVat.Text?.Trim();
            tempRow["VATPercent"] = drpVat.Text?.Trim();
            tempRow["TotalAmount"] = txtAmount.Text?.Trim();

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
            var invoiceFile = new InvoiceFile();
            var invNumber = lblInvoiceNumber.Text?.Trim();
            var fileName = $"ManualInv_FA_" + ClientSession.ClientName.Split(' ')[0] + "_" + invNumber + ".xml";
            var invoice = new Invoice
            {
                InvoiceNumber = invNumber,
                ConnectionId = invNumber,
                BillDate = txtInvDate.Text.Trim(),
                DueDate = txtDueDate.Text.Trim(),
                CustomerNumber = txtCustNum.Text.Trim(),
                OrderNumber = txtCustNum.Text.Trim(),
                ClientID = ClientSession.ClientID,
                Purchased = "0",
                FileName = fileName,
                Delivery = drpInvDelivery.SelectedValue?.Trim(),
                PaymentReference = Utilities.BuildOcr(lblInvoiceNumber.Text?.Trim(), 9, "9", "Sweden"),
                CurrencyCode = drpCurrency.Text.Trim(),
                InvoiceAmount = txtTotalAmount.Text.Trim(),
                RemainingAmount = txtTotalAmount.Text.Trim(),
                InvoiceVATAmount = txtTotalVat.Text.Trim()
            };

            var customer = new Customer
            {
                CustomerNumber = txtCustNum.Text.Trim(),
                CustomerName = txtCustName.Text.Trim(),
                CustomerAddress = txtCustContact.Text.Trim(),
                CustomerAddress2 = txtCustAddress.Text.Trim(),
                CustomerCity = txtCity.Text.Trim(),
                CustomerPostalCode = txtPostCode.Text.Trim(),
                CustomerType = "PRV",
                ClientId = ClientSession.ClientID
            };

            var tempTable = (DataTable)ViewState["gridData"];
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
                    Period = item["Unit"].ToString(),
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

            var file = GenerateStandardXml(invoiceFile);
            int.TryParse(invNumber, out int oldSeries);
            lblInvoiceNumber.Text = (oldSeries + 1).ToString();
            businessLayerObj.UpdateNumberSeries("Telson", oldSeries + 1);
            var ftpStatus = new FTPFileProcess().UploadStandardXml(file, fileName);
            if (ftpStatus == FtpStatusCode.ClosingData)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "Pop", "showSucessModel();", true);
                //modalSuccess.Show();
                ResetAndClose();    
            }
        }

        protected void grdInvoiceRows_OnRowDataBound(object sender, System.Web.UI.WebControls.GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Attributes["onclick"] = Page.ClientScript.GetPostBackClientHyperlink(grdInvoiceRows, "Select$" + e.Row.RowIndex);
                e.Row.Attributes["ondblclick"] = Page.ClientScript.GetPostBackClientHyperlink(grdInvoiceRows, "Edit$" + e.Row.RowIndex);
                e.Row.ToolTip = "Select the row to Delete";
            }
        }

        protected void grdInvoiceRows_SelectedIndexChanged(object sender, EventArgs e)
        {
            
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
            StringWriter stringWriter = new Utf8StringWriter();
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
            txtInvDate.Text = string.Empty;
            txtDueDate.Text = string.Empty;
            txtDescription.Text = string.Empty;
            txtUnit.Text = string.Empty;
            txtQuantity.Text = string.Empty;
            txtArticle.Text = string.Empty;
            txtVat.Text = string.Empty;
            txtAmount.Text = string.Empty;
            txtTotalVat.Text = string.Empty;
            txtTotalInv.Text = string.Empty;
            txtTotalAmount.Text = string.Empty;
            InitializeGrid();
        }

        private void ResetAndClose()
        {
            ResetControls();
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "CloseManualInvoice", "CloseManualInvoice();", true);
            //var popupExtn = (AjaxControlToolkit.ModalPopupExtender)Parent.FindControl("mpeModal");
            //popupExtn.Hide();

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
            double.TryParse(txtAmount.Text?.Trim(), out double rowTotal);
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
    }
}