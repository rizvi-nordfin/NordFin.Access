using ClosedXML.Excel;
using Nordfin.workflow.BusinessLayer;
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
    public partial class frmContractOverview : System.Web.UI.Page
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
                ITelsonGroupPresentationBusinessLayer objTelsonData = new TelesonGroupBusinessLayer();

                DataSet ds = objTelsonData.getContractList(ClientSession.ClientID);
                grdContract.DataSource = ds;
                grdContract.DataBind();
                Session["ContractGrid"] = ds.Tables[0];
            }
        }

        protected void grdContract_Sorting(object sender, GridViewSortEventArgs e)
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

        private void SortGridView(string sortExpression, string direction)
        {

            DataTable dt = (DataTable)Session["ContractGrid"];
            DataView dv = new DataView(dt);
            dv.Sort = sortExpression + direction;
            grdContract.DataSource = dv;
            grdContract.DataBind();
        }

        protected void gridLinkCustNum_Click(object sender, EventArgs e)
        {
            Session["custNum"] = ((LinkButton)sender).Text.Trim();
            Response.Redirect("frmCustomer.aspx");
        }

        protected void btnExport_Click(object sender, EventArgs e)
        {
            HttpResponse response = HttpContext.Current.Response;
            response.Clear();
            response.Charset = "";
            DataTable dataTable = (DataTable)Session["ContractGrid"];
            foreach (DataColumn column in dataTable.Columns)
                column.ColumnName = column.ColumnName.ToUpper();
            try

            {

                using (XLWorkbook wb = new XLWorkbook())
                {

                    var ws = wb.Worksheets.Add(dataTable);

                    Response.Clear();
                    Response.Buffer = true;
                    Response.Charset = "";
                    string filename = "Contracts" + DateTime.Now.ToString("yyyy-MM-dd hh:mm tt") + ".xlsx";
                    //ws.Column(1).Delete();
                    //ws.Column(1).Delete();
                    //ws.Column(13).Delete();
                    //ws.Column(14).Delete();
                    //ws.Column(13).Delete();
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
            catch (Exception ex)
            {
                //catch the issue
            }
        }
    }
}