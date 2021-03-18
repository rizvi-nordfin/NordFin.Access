using ClosedXML.Excel;
using Nordfin.workflow.Business;
using Nordfin.workflow.Entity;
using Nordfin.workflow.PresentationBusinessLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Dynamic;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net;

namespace Nordfin
{
    public partial class frmCustomer : System.Web.UI.Page
    {
        private const string ASCENDING = " ASC";
        private const string DESCENDING = " DESC";
        public SortDirection GridViewSortDirection
        {
            get
            {
                if (ViewState["sortDirection"] == null)
                    ViewState["sortDirection"] = SortDirection.Ascending;

                return (SortDirection)ViewState["sortDirection"];
            }
            set { ViewState["sortDirection"] = value; }
        }
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                ClearSession();
                string InvoiceNum = (string)Session["custNum"];
                hdnAdmin.Value = ClientSession.Admin;
                if (InvoiceNum != null && ClientSession.ClientID != null)
                {
                    IInvoicesPresentationBusinessLayer objInvoicesLayer = new InvoicesBusinessLayer();
                    DataSet ds = objInvoicesLayer.getInvoicesList(InvoiceNum.Trim(), ClientSession.ClientID, true, null);
                    Session["CustomerGrid"] = ds.Tables[0];
                    grdCustomer.DataSource = ds;
                    grdCustomer.DataBind();
                   
                    cboInvoiceNumber.DataSource = ds.Tables[0] ;
                    cboInvoiceNumber.DataTextField = "Invoicenumber";
                    cboInvoiceNumber.DataValueField = "InvoiceID";
                    cboInvoiceNumber.DataTextField = "Invoicenumber";
                   
                    cboInvoiceNumber.DataBind();
                    cboInvoiceNumber.Items.Insert(0, new ListItem(String.Empty, String.Empty));
                    cboInvoiceNumber.SelectedIndex = 0;
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        lblSumAmount.Text = string.Format("{0:#,0.00}", (ds.Tables[0].AsEnumerable().Sum(r => r.Field<decimal>("Invoiceamount"))));
                        lblFeesAmount.Text = string.Format("{0:#,0.00}", ds.Tables[0].AsEnumerable().Sum(r => r.Field<decimal>("Fees")));
                        lblTotalRemain.Text = string.Format("{0:#,0.00}", ds.Tables[0].AsEnumerable().Sum(r => r.Field<decimal>("TotalRemaining")));
                        lblOverPaid.Text = string.Format("{0:#,0.00}", ds.Tables[0].AsEnumerable().Sum(r => r.Field<decimal>("Overpayment")));
                        lblRemain.Text = string.Format("{0:#,0.00}", ds.Tables[0].AsEnumerable().Sum(r => r.Field<decimal>("Remainingamount")));
                    }

                    if (ds.Tables.Count > 1)
                    {
                        DataTable dtResult = ds.Tables[1];
                        if (dtResult.Rows.Count > 0)
                        {
                            lblName.Text = dtResult.Rows[0].ItemArray[0].ToString();
                            lblPersonNumber.Text = dtResult.Rows[0].ItemArray[1].ToString();
                            lblAddress.Text = dtResult.Rows[0].ItemArray[2].ToString();

                            lblPostalCode.Text = dtResult.Rows[0].ItemArray[3].ToString();
                            lblCity.Text = dtResult.Rows[0].ItemArray[4].ToString();
                            lblCountry.Text = dtResult.Rows[0].ItemArray[5].ToString();
                            lblEmail.Text = dtResult.Rows[0].ItemArray[6].ToString();
                            lblPhone.Text = dtResult.Rows[0].ItemArray[7].ToString();
                            lblCustomerNumber.Text = dtResult.Rows[0].ItemArray[8].ToString();
                            txtCustomerID.Text = Convert.ToString(dtResult.Rows[0].Field<int>("Customerid"));


                            if (dtResult.Rows[0].Field<int>("IsActive") > 0)
                            {
                                //pnlReset.Visible = true;
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "showResetPage", "$('#divResetRow').show(); $('#divResetPages').show();", true);
                            }
                            else
                            {
                                pnlUpdate.Style.Add("height", "70px");
                                pnlUpdate.Style.Add("padding-top", "10px");


                            }
                            try
                            {
                                lblAddress1.Text = dtResult.Rows[0].ItemArray[15].ToString();
                                if (lblAddress1.Text.Trim() == "")
                                {
                                    pnlAddress.Visible = false;
                                }
                            }
                            catch
                            {
                                pnlAddress.Visible = false;
                            }
                            try
                            {
                                lblInsurer.Text = dtResult.Rows[0].ItemArray[9].ToString();
                                lblReference.Text = dtResult.Rows[0].ItemArray[10].ToString();


                                lblCurrency.Text = dtResult.Rows[0].ItemArray[12].ToString();
                                try
                                {
                                    lblInsuredAmount.Text = String.Format(CultureInfo.GetCultureInfo("sv-SE"), "{0:#,0.00}", ConvertStringToDecimal(dtResult.Rows[0].ItemArray[11].ToString()));
                                    lblRemainingIns.Text = String.Format(CultureInfo.GetCultureInfo("sv-SE"), "{0:#,0.00}", ConvertStringToDecimal(dtResult.Rows[0].ItemArray[14].ToString()));
                                }
                                catch
                                {
                                    //catch the issue
                                }
                                pnlInsuredClient.Visible = Convert.ToBoolean(dtResult.Rows[0].ItemArray[13]);

                                if (!pnlInsuredClient.Visible)
                                {
                                    pnlPhone.Attributes.CssStyle.Add("padding-bottom", "25px");
                                }
                            }
                            catch
                            {
                                //catch the issue
                            }
                        }


                    }
                    if (ds.Tables.Count >= 3)
                    {
                        DataTable dtResult1 = ds.Tables[2];
                        if (dtResult1.Rows.Count > 0)
                        {
                            hdnClientName.Value = dtResult1.Rows[0].ItemArray[0].ToString();
                            hdnFileName.Value = dtResult1.Rows[0].ItemArray[1].ToString();
                            hdnArchiveLink.Value = dtResult1.Rows[0].ItemArray[2].ToString();
                        }


                    }
                    if (ds.Tables.Count >= 4)
                    {
                        DataTable dtResult2 = ds.Tables[3];
                        if (dtResult2.Rows.Count > 0)
                        {
                            grdNotes.DataSource = dtResult2;
                            grdNotes.DataBind();
                        }
                        else
                        {
                            grdNotes.DataSource = new List<string>();
                            grdNotes.DataBind();
                        }

                    }
                    if (ds.Tables.Count == 5)
                    {
                        DataTable dtInvoiceremain = ds.Tables[4];

                        try
                        {
                            dtInvoiceremain.AsEnumerable().ToList<DataRow>().ForEach(a =>
                            {
                                a["Invoiceamount"] = String.Format(CultureInfo.GetCultureInfo("sv-SE"), "{0:#,0.00}", ConvertStringToDecimal(a.Field<string>("Invoiceamount")));
                                a["Remainingamount"] = String.Format(CultureInfo.GetCultureInfo("sv-SE"), "{0:#,0.00}", ConvertStringToDecimal(a.Field<string>("Remainingamount")));


                            });
                        }
                        catch
                        {
                            //catch the issue                            
                        }


                        grdInvoiceRemaining.DataSource = dtInvoiceremain;
                        grdInvoiceRemaining.DataBind();
                    }
                    else
                    {
                        //divMatch.Visible = false;
                    }

                }
                else
                {
                    grdCustomer.DataSource = new List<string>();
                    grdCustomer.DataBind();
                }

            }
            if (ClientSession.AllowManualInvoice)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "showManualInvoiceButton", "$('#divManualInvoiceRow').show(); $('#divManualInvoice').show();", true);
            }
            pdfInvoices.Src = "";
            pdfRemind.Src = "";
            pdfDC.Src = "";
            ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), "CreateControl", "CreateControl();", true);
        }

        protected void gridLink_Click(object sender, EventArgs e)
        {
            Session["InvoiceNum"] = ((LinkButton)sender).Text.Trim();
            Response.Redirect("frmPaymentInformation.aspx");
        }

        protected void btnPDFDownload_Click(object sender, EventArgs e)
        {
            EmailFunctions emailFunctions = new EmailFunctions();
            string PDFArchive = "";
            btnDownload.CommandArgument = ((Button)sender).CommandArgument.Trim().Replace("INV-", "");
            btnEmail.Attributes["custInvoice"] = ((Button)sender).Attributes["custInvoice"].ToString();
            btnEmail.Attributes["combineInvoice"] = ((Button)sender).Attributes["combineInvoice"].ToString();
            btnEmail.Attributes["collectionStatus"] = ((Button)sender).Attributes["collectionStatus"].ToString();

            chkInvoices.Visible = emailFunctions.GetInvoiceExsits(hdnClientName.Value, hdnFileName.Value, btnDownload.CommandArgument, "", false);
            chkDC.Visible = (btnEmail.Attributes["collectionStatus"] == "DC" || btnEmail.Attributes["collectionStatus"].ToUpper() == "EXT") ? emailFunctions.GetInvoiceExsits(hdnClientName.Value, hdnFileName.Value, btnDownload.CommandArgument, "DC", true) : false;
            chkRemind.Visible = (btnEmail.Attributes["collectionStatus"] == "DC" || btnEmail.Attributes["collectionStatus"] == "REMIND" || btnEmail.Attributes["collectionStatus"].ToUpper() == "EXT") ? emailFunctions.GetInvoiceExsits(hdnClientName.Value, hdnFileName.Value, btnDownload.CommandArgument, "Rem", true) : false;
            chkInvoices.Checked = chkInvoices.Visible;
            chkDC.Checked = chkDC.Visible;
            chkRemind.Checked = chkRemind.Visible;
            if (!chkInvoices.Visible)
                PDFArchive = hdnArchiveLink.Value + btnDownload.CommandArgument;

            ScriptManager.RegisterStartupScript(this, this.GetType(), "ExportClick", "ExportClick(0,'" + PDFArchive + "');", true);
          
        }


      

        private dynamic LoadManualInvoiceData()
        {
            dynamic dynObject = new ExpandoObject();
            var selectedRow = grdCustomer.SelectedRow;
            if (selectedRow != null)
            {
                var gridData = (DataTable)Session["CustomerGrid"];
                var selectedRowData = gridData.AsEnumerable().ElementAt(selectedRow.RowIndex);
                dynObject.invoiceAmount = selectedRowData.ItemArray[5].ToString();
                dynObject.billDate = selectedRowData.ItemArray[7].ToString();
                dynObject.dueDate = selectedRowData.ItemArray[8].ToString();
                dynObject.remainingAmount = selectedRowData.ItemArray[6].ToString();
                dynObject.totalAmount = selectedRowData.ItemArray[11].ToString();
            }
            return dynObject;
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

        public decimal ConvertStringToDecimal(string sDecimal)
        {
            CultureInfo cultures = new CultureInfo("en-US");

            return Convert.ToDecimal(sDecimal, cultures);
        }


      
        protected void btnExport_Click(object sender, EventArgs e)
        {
            HttpResponse response = HttpContext.Current.Response;
            response.Clear();
            response.Charset = "";

            DataTable dataTable = (DataTable)Session["CustomerGrid"];
            //try
            //{

            //    dataTable.Columns.Remove("Customername");
            //    dataTable.Columns.Remove("CombineInvoice");
            //    dataTable.Columns.Remove("InvoiceID");
            //    dataTable.Columns.Remove("OrderID");
            //}
            //catch
            //{
            //    //catch the issue
            //}


            //if (dataTable.Rows.Count > 0)
            //{
            //    try
            //    {
            //        dataTable.Columns.Add("InvoiceAmount", typeof(decimal));
            //        dataTable.Columns.Add("RemainingAmount", typeof(decimal));
            //        dataTable.Columns.Add("Totalremaining", typeof(decimal));
            //        dataTable.Columns.Add("fees", typeof(decimal));
            //        dataTable.Columns.Add("overpayment", typeof(decimal));
            //        dataTable.AsEnumerable().ToList<DataRow>().ForEach(a =>
            //        {
            //            a["InvoiceAmount"] = ConvertStringToDecimal(Regex.Replace(a.Field<string>("Invoiceamount").Trim(), @"\s", "").Replace(",", "."));
            //            a["RemainingAmount"] = ConvertStringToDecimal(Regex.Replace(a.Field<string>("Remainingamount").Trim(), @"\s", "").Replace(",", "."));
            //            a["Totalremaining"] = ConvertStringToDecimal(Regex.Replace(a.Field<string>("TotalRemaining").Trim(), @"\s", "").Replace(",", "."));

            //            a["fees"] = ConvertStringToDecimal(Regex.Replace(a.Field<string>("Fees").Trim(), @"\s", "").Replace(",", "."));
            //            a["overpayment"] = ConvertStringToDecimal(Regex.Replace(a.Field<string>("Overpayment").Trim(), @"\s", "").Replace(",", "."));

            //        });

            //        dataTable.Columns.RemoveAt(3);
            //        dataTable.Columns.RemoveAt(3);
            //        dataTable.Columns.RemoveAt(5);
            //        dataTable.Columns.RemoveAt(5);
            //        dataTable.Columns.RemoveAt(5);
            //        dataTable.Columns["InvoiceAmount"].SetOrdinal(3);
            //        dataTable.Columns["Fees"].SetOrdinal(4);
            //        dataTable.Columns["RemainingAmount"].SetOrdinal(7);
            //        dataTable.Columns["TotalRemaining"].SetOrdinal(8);
            //        dataTable.Columns["Overpayment"].SetOrdinal(11);
            //    }
            //    catch
            //    {
            //        //catch the issue
            //    }

            //}
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

        protected void btnOk_Click(object sender, EventArgs e)
        {
            IInvoicesPresentationBusinessLayer objInvoicesLayer = new InvoicesBusinessLayer();
            int Result = objInvoicesLayer.DeleteCustomerLogin(ClientSession.ClientID, lblCustomerNumber.Text.Trim());
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            string sComments = "";

            if (lblName.Text != txtCustomerName.Text)
                sComments += xmlString(spnCustomerName.Text, lblName.Text, txtCustomerName.Text);
            if (lblAddress.Text != txtAddress1.Text)
                sComments += xmlString(spnAddress1.Text, lblAddress.Text, txtAddress1.Text);
            if (lblAddress1.Text != txtAddress2.Text)
                sComments += xmlString(spnAddress2.Text, lblAddress1.Text, txtAddress2.Text);

            if (lblPostalCode.Text != txtPostalCode.Text)
                sComments += xmlString(spnPostalCode.Text, lblPostalCode.Text, txtPostalCode.Text);
            if (lblCity.Text != txtCity.Text)
                sComments += xmlString(spnCity.Text, lblCity.Text, txtCity.Text);
            if (lblCountry.Text != txtCountry.Text)
                sComments += xmlString(spnCountry.Text, lblCountry.Text, txtCountry.Text);

            if (lblEmail.Text != txtEmail.Text)
                sComments += xmlString(spnModalEmail.Text, lblEmail.Text, txtEmail.Text);
            if (lblPhone.Text != txtPhoneNumber.Text)
                sComments += xmlString(spnPhonenumber.Text, lblPhone.Text, txtPhoneNumber.Text);

            CustomerInfo customerInfo = new CustomerInfo();
            customerInfo.Name = txtCustomerName.Text;
            customerInfo.Address1 = txtAddress1.Text;
            customerInfo.Address2 = txtAddress2.Text;
            customerInfo.PostalCode = txtPostalCode.Text;
            customerInfo.City = txtCity.Text;
            customerInfo.Country = txtCountry.Text;
            customerInfo.Email = txtEmail.Text;
            customerInfo.PhoneNumber = txtPhoneNumber.Text;
            customerInfo.Comments = sComments;
            customerInfo.CustomerID = Convert.ToInt32(txtCustomerID.Text);
            customerInfo.UserID = Convert.ToInt32(ClientSession.UserID);
            customerInfo.CustomerNumber = lblCustomerNumber.Text;



            IInvoicesPresentationBusinessLayer objInvoicesLayer = new InvoicesBusinessLayer();
            int scustEmail = objInvoicesLayer.UpdateCustomerInfo(customerInfo, ClientSession.ClientID);


            lblName.Text = txtCustomerName.Text;
            lblAddress.Text = txtAddress1.Text;
            lblAddress1.Text = txtAddress2.Text;
            lblPostalCode.Text = txtPostalCode.Text;
            lblCity.Text = txtCity.Text;
            lblCountry.Text = txtCountry.Text;
            lblEmail.Text = txtEmail.Text;
            lblPhone.Text = txtPhoneNumber.Text;


            if (lblAddress.Text == "" && lblAddress1.Text != "")
            {
                lblAddress.Text = txtAddress2.Text;
            }
            else if (lblAddress1.Text.Trim() == "")
            {
                pnlAddress.Visible = false;
            }

        }

        protected string xmlString(string labelText, string oldValue, string newValue)
        {
            return "<" + labelText.Replace(" ", "") + "><old>" + oldValue + "</old>" + "<new>" + newValue + "</new>" + "</" + labelText.Replace(" ", "") + ">";
        }


        [WebMethod]
        public static string GetMatchInvoices(string NegativeinvAmount, string PositiveinvAmount)
        {

            IInvoicesPresentationBusinessLayer objInvoicesLayer = new InvoicesBusinessLayer();
            IList<MatchInvoices> Result = objInvoicesLayer.GetMatchedInvoices(Regex.Replace(NegativeinvAmount.Trim(), @"\s", "").Replace(",", "."), Regex.Replace(PositiveinvAmount.Trim(), @"\s", "").Replace(",", "."), ClientSession.UserID);
            string jsonMatchedInv = new JavaScriptSerializer().Serialize(Result);
            string MatchedInv = "{\"MatchInvoice\" :" + jsonMatchedInv + "}";
            return MatchedInv;

        }

        protected void btnExportDetail_Click(object sender, EventArgs e)
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
                        ws.Range(str).Style.Fill.BackgroundColor= XLColor.FromHtml("#CCCCFF");
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

        public static string Execute(string invNumber)
        {
            var r = Regex.Replace(invNumber, "[^0-9]", "");

            var n = int.TryParse(r, out var x) ? x : 0;

            // n is invoice number which is int or only numbers

            /* if invoice number is more than 9 digits it will keep only 9 digits from the right and remove the rest but 
             it will not change invoice number in the file name */

            if (r.Length > 9)
                n = int.TryParse(r.Remove(0, r.Length - 9), out var m) ? m : 0;
            const int i = 1000000000;
            const int n2 = 100000;
            var newPath = "";
            var index = 0;
            var n1 = 0;
            var n3 = n2;

            while (index < i)
            {
                index++;
                if (n > n1 && n <= n3)
                {
                    newPath = n1 + "_" + n3;
                    break;
                }
                n1 += n2;
                n3 += n2;
            }
            return newPath;
        }



        protected void grdCustomer_Sorting(object sender, GridViewSortEventArgs e)
        {
            string sortExpression = e.SortExpression;

            if (GridViewSortDirection == SortDirection.Ascending)
            {
                GridViewSortDirection = SortDirection.Descending;
                SortGridView(sortExpression, DESCENDING);
            }
            else
            {
                GridViewSortDirection = SortDirection.Ascending;
                SortGridView(sortExpression, ASCENDING);
            }
        }

        protected void grdCustomer_OnRowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Attributes["onclick"] = Page.ClientScript.GetPostBackClientHyperlink(grdCustomer, "Select$" + e.Row.RowIndex);
                e.Row.ToolTip = "click to create Credit Invoice";
            }
        }

        private void SortGridView(string sortExpression, string direction)
        {

            DataTable dt = (DataTable)Session["CustomerGrid"];

            DataView dv = new DataView(dt);

            dv.Sort = sortExpression + direction;

            grdCustomer.DataSource = dv;
            grdCustomer.DataBind();
        }

        protected override void Render(HtmlTextWriter writer)
        {
            foreach (GridViewRow r in grdCustomer.Rows)
            {
                if (r.RowType == DataControlRowType.DataRow)
                {
                    Page.ClientScript.RegisterForEventValidation(grdCustomer.UniqueID, "Select$" + r.RowIndex);
                }
            }

            base.Render(writer);
        }

        protected void btnDownload_Click(object sender, EventArgs e)
        {

            string FileStartName = hdnFileName.Value;
            string InvoiceNumber = ((Button)sender).CommandArgument.Trim();
            EmailFunctions emailFunctions = new EmailFunctions();
            if (chkInvoices.Checked && chkInvoices.Visible)
            {
                string sFileName = emailFunctions.GetFileName(FileStartName, InvoiceNumber, "", out bool bExist);
                if (bExist)
                    pdfInvoices.Src = "frmPdfMultiDownload.aspx?FileName=" + Server.UrlEncode(sFileName); 
            }
            if (chkRemind.Checked && chkRemind.Visible)
            {
                string sFileName = emailFunctions.GetFileName(FileStartName, InvoiceNumber, "Rem", out bool bExist);
                if (bExist)
                    pdfRemind.Src = "frmPdfMultiDownload.aspx?FileName=" + Server.UrlEncode(sFileName);
            }
            if (chkDC.Checked && chkDC.Visible)
            {
                string sFileName = emailFunctions.GetFileName(FileStartName, InvoiceNumber, "DC", out bool bExist);
                if (bExist)
                    pdfDC.Src = "frmPdfMultiDownload.aspx?FileName=" + Server.UrlEncode(sFileName);
            }

            ScriptManager.RegisterStartupScript(this, this.GetType(), "ExportClick", "ExportClick(0);", true);
        }


        protected void btnEmail_Click(object sender, EventArgs e)
        {
            IInvoicesPresentationBusinessLayer objInvoicesLayer = new InvoicesBusinessLayer();
            string scustEmail = objInvoicesLayer.GetCustInvoiceEmailID(ClientSession.ClientID, btnEmail.Attributes["custInvoice"].ToString());
            txtCustEmail.Text = scustEmail;
            txtEmailHeader.Text = ClientSession.ClientName + "; Invoice" + " " + btnEmail.Attributes["combineInvoice"];
            txtEmailBody.Text = "Hei, Hi, Hej, Hallo!" + "\n\n" + "Your invoice copy has been attached" + "\n\n" + "Have a great day :-)" + "\n\n" + "Best Regards," + "\n" + ClientSession.ClientName;
            ScriptManager.RegisterStartupScript(this, this.GetType(), "ExportClick", "ExportClick(1);", true);
        }

        protected void btnSend_Click(object sender, EventArgs e)
        {
            try
            {
                EmailFunctions emailFunctions = new EmailFunctions();
                string FileStartName = hdnFileName.Value;
                string InvoiceNumber = btnEmail.Attributes["combineInvoice"];
                List<PDFMultiDownload> multiDownloads = new List<PDFMultiDownload>();
                if (chkInvoices.Checked && chkInvoices.Visible)
                {
                    string sFileName = emailFunctions.GetFileName(FileStartName, InvoiceNumber, "", out bool bExist);
                    if (bExist)
                        multiDownloads.Add(new PDFMultiDownload() { FileName = sFileName });
                }
                if (chkRemind.Checked && chkRemind.Visible)
                {
                    string sFileName = emailFunctions.GetFileName(FileStartName, InvoiceNumber, "Rem", out bool bExist);
                    if (bExist)
                        multiDownloads.Add(new PDFMultiDownload() { FileName = sFileName });
                }
                if (chkDC.Checked && chkDC.Visible)
                {
                    string sFileName = emailFunctions.GetFileName(FileStartName, InvoiceNumber, "DC", out bool bExist);
                    if (bExist)
                        multiDownloads.Add(new PDFMultiDownload() { FileName = sFileName });
                }

                bool bEmail = emailFunctions.SendMail(txtCustEmail.Text, txtEmailHeader.Text, txtEmailBody.Text, multiDownloads);

                ScriptManager.RegisterStartupScript(this, this.GetType(), "ExportClick", "ExportClick(1,'','" + bEmail + "');", true);
            }
            catch
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "ExportClick", "ExportClick(1,'','" + false + "');", true);
            }
        }
      



        protected void btnNotes_Click(object sender, EventArgs e)
        {
            Notes objNotes = new Notes
            {
                InvoiceID = Convert.ToInt32(cboInvoiceNumber.SelectedItem.Value),
                InvoiceNumber = cboInvoiceNumber.SelectedItem.Text,
                CustomerID = lblCustomerNumber.Text,
                UserID = Convert.ToInt32(ClientSession.UserID),
                ClientID = Convert.ToInt32(ClientSession.ClientID),
                UserName = ClientSession.UserName,
                NoteText = txtNotes.Text
            };

            IInvoicesPresentationBusinessLayer objInvoicesLayer = new InvoicesBusinessLayer();
       
            var objNotesInformation = objInvoicesLayer.InsertInvoiceInfo(objNotes);

            grdNotes.DataSource = objNotesInformation;
            grdNotes.DataBind();
            cboInvoiceNumber.SelectedIndex = 0;
            txtNotes.Text = "";
        }
    }
}