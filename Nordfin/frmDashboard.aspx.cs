using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using ClosedXML.Excel;
using Nordfin.workflow.Business;
using Nordfin.workflow.Entity;
using Nordfin.workflow.PresentationBusinessLayer;

namespace Nordfin
{
    public partial class frmDashboard : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                ClearSession();
                IInvoiceDashboardPresentationBusinessLayer objInvoiceDashboard = new InvoiceDashboardBusinessLayer();
                DataSet dataset = null;
                if (Session["InvoiceData"] == null)
                {
                    dataset = objInvoiceDashboard.getOverviewDashboard(ClientSession.ClientID);
                    Session["InvoiceData"] = dataset;
                }
                else
                {
                    dataset = (DataSet)Session["InvoiceData"];
                }


                if (dataset.Tables.Count > 0)
                {


                    DataTable dataTable = dataset.Tables[0];

                    lblSentoutNumber.Text = dataTable.Rows[0].ItemArray[1].ToString();

                    lblSenoutAmount.Text = string.Format(CultureInfo.GetCultureInfo("sv-SE"), "{0:#,###0}", decimal.Truncate(ConvertStringToDecimal(dataTable.Rows[0].ItemArray[2].ToString())));// String.Format(daDK, "{0:00.00}",ConvertStringToDecimal(dataTable.Rows[0].ItemArray[2].ToString()));

                    if (lblSentoutNumber.Text == "0" && lblSenoutAmount.Text == "0")
                    {
                        lblSentoutPercent.Text = "0%";
                    }
                    else
                    {
                        lblSentoutPercent.Text = "100%";
                    }
                    decimal dSendout = ConvertStringToDecimal(dataTable.Rows[0].ItemArray[2].ToString());
                    decimal dUnpaid = ConvertStringToDecimal(dataTable.Rows[1].ItemArray[2].ToString());
                    decimal dRemainder = ConvertStringToDecimal(dataTable.Rows[3].ItemArray[2].ToString());
                    decimal dDC = ConvertStringToDecimal(dataTable.Rows[4].ItemArray[2].ToString());
                    decimal dExt = ConvertStringToDecimal(dataTable.Rows[5].ItemArray[2].ToString());

                    lblUnpaidNumber.Text = dataTable.Rows[1].ItemArray[1].ToString();
                    lblUnpaidAmount.Text = string.Format(CultureInfo.GetCultureInfo("sv-SE"), "{0:#,###0}", decimal.Truncate(dUnpaid));
                    try
                    {
                        lblUnpaidPrecent.Text = Convert.ToString(Math.Round(((dUnpaid / dSendout) * 100), 2)) + "%";
                    }
                    catch
                    {

                    }

                    lblPaidNumber.Text = dataTable.Rows[2].ItemArray[1].ToString();
                    lblPaidAmount.Text = string.Format(CultureInfo.GetCultureInfo("sv-SE"), "{0:#,###0}", decimal.Truncate(ConvertStringToDecimal(dataTable.Rows[2].ItemArray[2].ToString())));
                    try
                    {
                        lblPaidPercent.Text = Convert.ToString(100 - Convert.ToDecimal(lblUnpaidPrecent.Text.Replace("%", ""))) + "%";
                    }

                    catch
                    {

                    }
                    lblRemainderNumber.Text = dataTable.Rows[3].ItemArray[1].ToString();
                    lblRemainderAmount.Text = string.Format(CultureInfo.GetCultureInfo("sv-SE"), "{0:#,###0}", decimal.Truncate(dRemainder));
                    try
                    {
                        lblRemainderPercent.Text = Convert.ToString(Math.Round(((dRemainder / dSendout) * 100), 2)) + "%";
                    }

                    catch
                    {

                    }
                    lblDCNumber.Text = dataTable.Rows[4].ItemArray[1].ToString();
                    lblDCAmount.Text = string.Format(CultureInfo.GetCultureInfo("sv-SE"), "{0:#,###0}", decimal.Truncate(dDC));
                    try
                    {
                        lblDCPercent.Text = Convert.ToString(Math.Round(((dDC / dSendout) * 100), 2)) + "%";
                    }
                    catch
                    {

                    }

                    lblExtNumber.Text = dataTable.Rows[5].ItemArray[1].ToString();
                    lblExtAmount.Text = string.Format(CultureInfo.GetCultureInfo("sv-SE"), "{0:#,###0}", decimal.Truncate(dExt));
                    try
                    {
                        lblExtPercent.Text = Convert.ToString(Math.Round(((dExt / dSendout) * 100), 2)) + "%";
                    }
                    catch
                    {

                    }

                    lblTotalSentoutAmount.Text = lblSenoutAmount.Text;
                    lblTotalUnpaidAmount.Text = lblUnpaidAmount.Text;
                    lblTotalAvgPaidDay.Text = lblPaidPercent.Text;





                }
                if (dataset.Tables.Count >= 2)
                {
                    DataTable dataTable = dataset.Tables[1];
                    lblPayments.Text = Convert.ToString(dataTable.Rows[0].Field<int>("Payments"));
                    lblInvoices.Text = Convert.ToString(dataTable.Rows[0].Field<int>("Invoices"));
                    lblReminder.Text = Convert.ToString(dataTable.Rows[0].Field<int>("Reminders"));
                    lblTotalDC.Text = Convert.ToString(dataTable.Rows[0].Field<int>("DC"));
                    lblTotalAvgPaymentDay.Text = Convert.ToString(dataTable.Rows[0].Field<int>("AvgPaymentDay"));
                }
                if (dataset.Tables.Count >= 3)
                {
                    DataTable dataTable = dataset.Tables[2];

                    int iOntime = dataTable.Rows[0].Field<int>("CollectionStatus") - dataTable.Rows[0].Field<int>("Late Count");
                    try
                    {
                        hdnDC.Value = Convert.ToString(Math.Round(((Convert.ToDecimal(dataTable.Rows[0].Field<int>("DC")) / Convert.ToDecimal(dataTable.Rows[0].Field<int>("Total Amount"))) * 100), 2));
                    }
                    catch
                    {
                        hdnDC.Value = "0";
                    }
                    try
                    {
                        hdnExt.Value = Convert.ToString(Math.Round(((Convert.ToDecimal(dataTable.Rows[0].Field<int>("EXT")) / Convert.ToDecimal(dataTable.Rows[0].Field<int>("Total Amount"))) * 100), 2));
                    }
                    catch
                    {
                        hdnExt.Value = "0";
                    }
                    try
                    {
                        hdnRemain.Value = Convert.ToString(Math.Round(((Convert.ToDecimal(dataTable.Rows[0].Field<int>("REMIND")) / Convert.ToDecimal(dataTable.Rows[0].Field<int>("Total Amount"))) * 100), 2));
                    }
                    catch
                    {
                        hdnRemain.Value = "0";
                    }
                    try
                    {
                        hdnOntime.Value = Convert.ToString(Math.Round(((Convert.ToDecimal(iOntime) / Convert.ToDecimal(dataTable.Rows[0].Field<int>("Total Amount"))) * 100), 2));
                    }
                    catch
                    {
                        hdnOntime.Value = "0";
                    }
                    try
                    {
                        hdnLate.Value = Convert.ToString(Math.Round(((Convert.ToDecimal(dataTable.Rows[0].Field<int>("Late Count")) / Convert.ToDecimal(dataTable.Rows[0].Field<int>("Total Amount"))) * 100), 2));
                    }
                    catch
                    {
                        hdnLate.Value = "0";
                    }
                }

                if (dataset.Tables.Count >= 4)
                {
                    DataTable dataTable = dataset.Tables[3];
                    try
                    {
                        int iOntimeAmount = dataTable.Rows[0].Field<int>("CollectionStatus") - dataTable.Rows[0].Field<int>("Late Count");
                        hdnDCAmount.Value = Convert.ToString(Math.Round(((Convert.ToDecimal(dataTable.Rows[0].Field<int>("DC")) / Convert.ToDecimal(dataTable.Rows[0].Field<int>("Total Amount"))) * 100), 2));
                        hdnExtAmount.Value = Convert.ToString(Math.Round(((Convert.ToDecimal(dataTable.Rows[0].Field<int>("EXT")) / Convert.ToDecimal(dataTable.Rows[0].Field<int>("Total Amount"))) * 100), 2));
                        hdnRemainAmount.Value = Convert.ToString(Math.Round(((Convert.ToDecimal(dataTable.Rows[0].Field<int>("REMIND")) / Convert.ToDecimal(dataTable.Rows[0].Field<int>("Total Amount"))) * 100), 2));
                        hdnOntimeAmount.Value = Convert.ToString(Math.Round(((Convert.ToDecimal(iOntimeAmount) / Convert.ToDecimal(dataTable.Rows[0].Field<int>("Total Amount"))) * 100), 2));
                        hdnLateAmount.Value = Convert.ToString(Math.Round(((Convert.ToDecimal(dataTable.Rows[0].Field<int>("Late Count")) / Convert.ToDecimal(dataTable.Rows[0].Field<int>("Total Amount"))) * 100), 2));
                    }
                    catch
                    {

                    }
                }

                if (dataset.Tables.Count >= 5)
                {
                    DataTable dataTable = dataset.Tables[4];
                    try
                    {

                        lblInvoicesAmount.Text = String.Format(CultureInfo.GetCultureInfo("sv-SE"), "{0:#,0}", decimal.Truncate(ConvertStringToDecimal(dataTable.Rows[0].Field<string>("Invoices"))));
                        lblPaymentsAmount.Text = String.Format(CultureInfo.GetCultureInfo("sv-SE"), "{0:#,0}", decimal.Truncate(ConvertStringToDecimal(dataTable.Rows[0].Field<string>("Payments"))));
                        lblReminderAmount.Text = String.Format(CultureInfo.GetCultureInfo("sv-SE"), "{0:#,0}", decimal.Truncate(ConvertStringToDecimal(dataTable.Rows[0].Field<string>("Reminders"))));
                        lblTotalDCAmount.Text = String.Format(CultureInfo.GetCultureInfo("sv-SE"), "{0:#,0}", decimal.Truncate(ConvertStringToDecimal(dataTable.Rows[0].Field<string>("DC"))));

                    }
                    catch
                    {

                    }
                }

                btnExcel.Visible = false;


            }
            ScriptManager.GetCurrent(this).RegisterPostBackControl(btnExcel);
        }

        public decimal ConvertStringToDecimal(string sDecimal)
        {
            CultureInfo cultures = new CultureInfo("en-US");

            return Convert.ToDecimal(sDecimal, cultures);
        }



        [WebMethod]
        public static string LoadBatchVolume()
        {
            ClearSession();
            IInvoiceDashboardPresentationBusinessLayer objInvoiceDashboard = new InvoiceDashboardBusinessLayer();
            IList<BatchVolume> objBatchVolumeList = objInvoiceDashboard.getBatchVolumeDashboard(ClientSession.ClientID);

            string jsonBatchVolumeList = new JavaScriptSerializer().Serialize(objBatchVolumeList);



            string sResultList = "{\"BatchVolumeList\" :" + jsonBatchVolumeList + "}";

            return sResultList;
        }


        [WebMethod]
        public static string LoadCurrentYearChart()
        {
            ClearSession();
            try
            {
                IInvoiceDashboardPresentationBusinessLayer objInvoiceDashboard = new InvoiceDashboardBusinessLayer();
                DataSet dataset = null;
                if (HttpContext.Current.Session["CurrentYearGraph"] == null)
                {
                    dataset = objInvoiceDashboard.getCurrentYearChart(ClientSession.ClientID);
                    HttpContext.Current.Session["CurrentYearGraph"] = dataset;
                }
                else
                {
                    dataset = (DataSet)HttpContext.Current.Session["CurrentYearGraph"];
                }

                Chart chart = new Chart();
                DataTable dataTable = dataset.Tables[0];

                int iOntime = dataTable.Rows[0].Field<int>("CollectionStatus") - dataTable.Rows[0].Field<int>("Late Count");
                if (dataTable.Rows.Count > 0 && dataTable.Rows[0].Field<int>("Total Amount") != 0)
                {
                    chart.DC = Convert.ToString(Math.Round(((Convert.ToDecimal(dataTable.Rows[0].Field<int>("DC")) / Convert.ToDecimal(dataTable.Rows[0].Field<int>("Total Amount"))) * 100), 2));
                    chart.Ext = Convert.ToString(Math.Round(((Convert.ToDecimal(dataTable.Rows[0].Field<int>("EXT")) / Convert.ToDecimal(dataTable.Rows[0].Field<int>("Total Amount"))) * 100), 2));
                    chart.Remain = Convert.ToString(Math.Round(((Convert.ToDecimal(dataTable.Rows[0].Field<int>("REMIND")) / Convert.ToDecimal(dataTable.Rows[0].Field<int>("Total Amount"))) * 100), 2));
                    chart.Ontime = Convert.ToString(Math.Round(((Convert.ToDecimal(iOntime) / Convert.ToDecimal(dataTable.Rows[0].Field<int>("Total Amount"))) * 100), 2));
                    chart.Late = Convert.ToString(Math.Round(((Convert.ToDecimal(dataTable.Rows[0].Field<int>("Late Count")) / Convert.ToDecimal(dataTable.Rows[0].Field<int>("Total Amount"))) * 100), 2));
                }
                else
                {
                    chart.DC = "0";
                    chart.Ext = "0";
                    chart.Remain = "0";
                    chart.Ontime = "0";
                    chart.Late = "0";
                }

                DataTable dataTableAmount = dataset.Tables[1];

                if (dataTableAmount.Rows.Count > 0 && dataTableAmount.Rows[0].Field<int>("Total Amount") != 0)
                {
                    int iOntimeAmount = dataTableAmount.Rows[0].Field<int>("CollectionStatus") - dataTableAmount.Rows[0].Field<int>("Late Count");
                    chart.DCAmount = Convert.ToString(Math.Round(((Convert.ToDecimal(dataTableAmount.Rows[0].Field<int>("DC")) / Convert.ToDecimal(dataTableAmount.Rows[0].Field<int>("Total Amount"))) * 100), 2));
                    chart.ExtAmount = Convert.ToString(Math.Round(((Convert.ToDecimal(dataTableAmount.Rows[0].Field<int>("EXT")) / Convert.ToDecimal(dataTableAmount.Rows[0].Field<int>("Total Amount"))) * 100), 2));
                    chart.RemainAmount = Convert.ToString(Math.Round(((Convert.ToDecimal(dataTableAmount.Rows[0].Field<int>("REMIND")) / Convert.ToDecimal(dataTableAmount.Rows[0].Field<int>("Total Amount"))) * 100), 2));
                    chart.OntimeAmount = Convert.ToString(Math.Round(((Convert.ToDecimal(iOntimeAmount) / Convert.ToDecimal(dataTableAmount.Rows[0].Field<int>("Total Amount"))) * 100), 2));
                    chart.LateAmount = Convert.ToString(Math.Round(((Convert.ToDecimal(dataTableAmount.Rows[0].Field<int>("Late Count")) / Convert.ToDecimal(dataTableAmount.Rows[0].Field<int>("Total Amount"))) * 100), 2));
                }
                else
                {
                    chart.DCAmount = "0";
                    chart.ExtAmount = "0";
                    chart.RemainAmount = "0";
                    chart.OntimeAmount = "0";

                    chart.LateAmount = "0";

                }
                string jsonchart = new JavaScriptSerializer().Serialize(chart);
                string sResultchart = "{\"Chart\" :" + jsonchart + "}";
                return sResultchart;
            }
            catch (Exception ex)
            {
                return "";
            }



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
                catch { }
                HttpContext.Current.Session.Abandon();
                HttpContext.Current.Response.Redirect("frmLogin.aspx");

            }
        }

        [WebMethod]
        public static string Notification()
        {
            IInvoiceDashboardPresentationBusinessLayer objInvoiceDashboard = new InvoiceDashboardBusinessLayer();
            IList<Notification> objNotificationList = objInvoiceDashboard.getNotificationNotes(ClientSession.ClientID);
            string jsonNotification = new JavaScriptSerializer().Serialize(objNotificationList);
            string sResultNotification = "{\"NotificationList\" :" + jsonNotification + "}";
            return sResultNotification;
        }


        [WebMethod]
        public static string NotificationInvoice(string InvoiceNum)
        {
            try
            {
                HttpContext.Current.Session["InvoiceorCustNum"] = InvoiceNum;
                return "true";
            }
            catch
            {
                return "false";
            }
        }

        protected void btnInvoiceData_Click(object sender, EventArgs e)
        {
            pnlCustomerData.Visible = false;
            divInvoiceData.Visible = true;
            btnCustomerData.CssClass = "form-control form-control buttonClass";
            btnInvoiceData.CssClass = "form-control form-control buttonDefault";
            btnExcel.Visible = false;
        }

        protected void btnCustomerData_Click(object sender, EventArgs e)
        {
            pnlCustomerData.Visible = true;
            divInvoiceData.Visible = false;
            
            btnCustomerData.CssClass = "form-control form-control buttonDefault";
            btnInvoiceData.CssClass = "form-control form-control buttonClass";
            if (ClientSession.Admin == "0" && ClientSession.Admin == "1")
            {
                btnExcel.Visible = false;
            }
            else
            {
                btnExcel.Visible = true;
            }
        }

        [WebMethod]
        public static string LoadNoofCustomerData()
        {
            try
            {
                IInvoiceDashboardPresentationBusinessLayer objInvoiceDashboard = new InvoiceDashboardBusinessLayer();

                CustomerInfoDTO objCustomerDatasList = objInvoiceDashboard.getCustomerData(ClientSession.ClientID);
                string jsonCustomerData = new JavaScriptSerializer().Serialize(objCustomerDatasList.objCustomerData);
                string jsonDemographicsData = new JavaScriptSerializer().Serialize(objCustomerDatasList.objDemographicsList);
                string jsonCustomerMap = new JavaScriptSerializer().Serialize(objCustomerDatasList.objCustomerMapList);
                string jsonCustomerRegion = new JavaScriptSerializer().Serialize(objCustomerDatasList.objCustomerRegionList);
                string jsonTotalCust = new JavaScriptSerializer().Serialize(objCustomerDatasList.objCustomerData[0].TotalCust);
                string jsonInvoiceNumber = new JavaScriptSerializer().Serialize(objCustomerDatasList.objInvoiceNumberList);
                string jsonInvoiceAmount = new JavaScriptSerializer().Serialize(objCustomerDatasList.objInvoiceAmountList);
                string jsonMarkerRegion = new JavaScriptSerializer().Serialize(objCustomerDatasList.objClientLand);
                string jsonCustomerGrowth = new JavaScriptSerializer().Serialize(objCustomerDatasList.objCustomerGrowthList);
                HttpContext.Current.Session["CustomerData"] = objCustomerDatasList;
                string sResultCustomerData = "{\"CustomerList\" :" + jsonCustomerData + "," + "\"InvoiceAmount\" :" + jsonInvoiceAmount + "," + "\"InvoiceNumber\" :" + jsonInvoiceNumber + "," + "\"CustomerGrowth\" :" + jsonCustomerGrowth + "," + "\"TotalCust\" :" + jsonTotalCust + "," + "\"Demographics\" :" + jsonDemographicsData + "," + "\"CustomerMapList\" :" + jsonCustomerMap + "," + "\"MarkerRegion\" :" + jsonMarkerRegion + "," + "\"CustomerRegionList\" :" + jsonCustomerRegion + "}";
                return sResultCustomerData;

            }
            catch
            {
                return "";
            }
        }

        [WebMethod]
        public static string UpdateCustomerRegion(string CountryRegion, string CountryPostalCode,string IsMatch)
        {


            IInvoiceDashboardPresentationBusinessLayer objInvoiceDashboard = new InvoiceDashboardBusinessLayer();

            int iResult = objInvoiceDashboard.setCustomerRegion(CountryPostalCode, CountryRegion, ClientSession.ClientID, IsMatch);



            return "";
        }

        [WebMethod]
        public static string GetMapRegion(string IsMatch)
        {

            IInvoiceDashboardPresentationBusinessLayer objInvoiceDashboard = new InvoiceDashboardBusinessLayer();

            IList<CustomerMap> objCustMapList = objInvoiceDashboard.GetCustomerMapRegion(ClientSession.ClientID,IsMatch);
            string jsonCustomerMap = new JavaScriptSerializer().Serialize(objCustMapList);

            string CustMapList = "{\"CustomerMapList\" :" + jsonCustomerMap + "}";
            return CustMapList;

            
        }

        protected void btnExcel_Click(object sender, EventArgs e)
        {
            CustomerInfoDTO  customerInfoDTO= (CustomerInfoDTO)Session["CustomerData"];


            
            using (XLWorkbook wb = new XLWorkbook())
            {


                DataSet CustomerTable = GetCustomerTable(customerInfoDTO.objCustomerData);
                wb.Worksheets.Add(CustomerTable.Tables[0]);

                DataSet demographicsTable = GetDemographicsTable(customerInfoDTO.objDemographicsList);
                wb.Worksheet(1).Cell(9, 1).InsertTable(demographicsTable.Tables[0]);

                DataSet custRegionTable = GetCustomerRegionTable(customerInfoDTO.objCustomerRegionList);
                wb.Worksheet(1).Cell(15, 1).InsertTable(custRegionTable.Tables[0]);

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

        protected DataSet GetCustomerTable(IList<CustomerData> customerData)
        {
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("CUSTOMER TYPE", typeof(string));
            dataTable.Columns.Add("NUMBER", typeof(string));
            dataTable.Columns.Add("%", typeof(string));

            for (int i = 0; i < customerData.Count - 1; i++)
            {
                dataTable.Rows.Add(customerData[i].custType, customerData[i].custNumber, customerData[i].custPercentage + "%");
            }
            DataSet dataSet = new DataSet();
            dataSet.Tables.Add(dataTable);
            return dataSet;
        }


        protected DataSet GetDemographicsTable(IList<Demographics> demographics)
        {
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("AGE GROUP", typeof(string));
            dataTable.Columns.Add("NUMBER", typeof(string));
            dataTable.Columns.Add("%", typeof(string));

            for (int i = 0; i < demographics.Count; i++)
            {
                dataTable.Rows.Add(demographics[i].custColName, demographics[i].custRowValue, demographics[i].custPercentage + "%");
            }

            DataSet dataSet = new DataSet();
            dataSet.Tables.Add(dataTable);
            return dataSet;
        }
        protected DataSet GetCustomerRegionTable(IList<CustomerRegion> customerRegions)
        {
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("REGION", typeof(string));
            dataTable.Columns.Add("NO. OF CUSTOMERS", typeof(string));
          

            for (int i = 0; i < customerRegions.Count; i++)
            {
                dataTable.Rows.Add(customerRegions[i].CustRegion, customerRegions[i].CustTotal);
            }
            DataSet dataSet = new DataSet();
            dataSet.Tables.Add(dataTable);
            return dataSet;
        }
    }
}