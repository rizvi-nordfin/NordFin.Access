using Nordfin.workflow.Business;
using Nordfin.workflow.Entity;
using Nordfin.workflow.PresentationBusinessLayer;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Nordfin
{
    public partial class Nordfin : System.Web.UI.MasterPage
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            ClearSession();

           
            if (!IsPostBack)
            {

                lblUserName.Text = ClientSession.LabelUser;
                IUserPresentationBusinessLayer objUserLayer = new UserBusinessLayer();
                string BatchValues = "";
                int Contracts = 0;
                IList<ClientList> objClientList = objUserLayer.GetClientList(Convert.ToInt32(ClientSession.Admin), Convert.ToInt32(ClientSession.ClientID), ClientSession.UserID, out BatchValues,out Contracts);
                var newClientList = (from l in objClientList
                                     select new ClientList
                                     {
                                         ClientName = l.ClientName,
                                         ClientID = l.ClientID
                                     }).ToList();
                grdClientName.DataSource = newClientList;

                grdClientName.DataBind();

                if (objClientList.Count > 0 && objClientList.Any(a => a.ClientID == Convert.ToInt32(ClientSession.ClientID)))
                {
                    txtClientName.Text = objClientList.Where(a => a.ClientID == Convert.ToInt32(ClientSession.ClientID)).ToList()[0].ClientName;
                    txtClientName.Attributes.Add("ClientID", ClientSession.ClientID);
                    ClientSession.ClientName = txtClientName.Text;
                    ClientSession.ClientLand = objClientList.Where(a => a.ClientID == Convert.ToInt32(ClientSession.ClientID)).ToList()[0].ClientLand;
                    ClientSession.AllowManualInvoice = objClientList.Where(a => a.ClientID == Convert.ToInt32(ClientSession.ClientID)).ToList()[0].AllowManualInvoice;
                    ClientSession.ClientArchive= objClientList.Where(a => a.ClientID == Convert.ToInt32(ClientSession.ClientID)).ToList()[0].Archive;
                }
                if (BatchValues.ToUpper() == "FALSE")
                {
                    pnlsideMenuButton.Visible = false;
                    btnStatistics.Visible = false;
                    imgStatistics.Visible = false;
                   // pnlContractsMenuButton.Visible = false;
                }

                if (ClientSession.Admin != "0" && ClientSession.Admin != "1")
                {
                    pnlStatistics.Visible = false;
                    pnlInvoiceBatches.Visible = false;
                    pnlTraffic.Visible = false;
                    pnlTrafficDetails.Visible = false;
                    pnlNotification.Visible = false;
                    pnlTeleson.Visible = false;
                    pnlTelecom.Visible = false;


                    pnlSideMenuContracts.CssClass = "sideContractMenuStatistics pnlSideMenuContractsTop hidden";//.Add("top", "551px !important");

                }
    
                if(ClientSession.AllowManualInvoice == 1)
                {
                    pnlSideMenuAdd.Visible = true;
                }
              

                if (Contracts == 0)
                {
                    pnlTeleson.Visible = false;
                    pnlContractsMenuButton.Visible = false;
                }
                  
             

                hdnClientID.Value = ClientSession.ClientID;

                if (Application["EmailDetails"] != null)
                {
                    IList<EMailInvoices> EmailDetails = (IList<EMailInvoices>)Application["EmailDetails"];
                    if (EmailDetails.Any(a => a.UserID == Convert.ToInt32(ClientSession.UserID) && !a.DisplayError))
                    {
                        EmailDetails.Where(a => a.UserID == Convert.ToInt32(ClientSession.UserID)).ToList().ForEach(b => b.DisplayError = true);
                        string ErrorMsg = "The Email is failed for the customer " + string.Join(" ", EmailDetails.Select(a => a.CustomerNumber)) + " due to limit exceed";
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "showMasterModalInfo", "$('#mdlMasterConfirm').modal({ backdrop: 'static', keyboard: false }, 'show');" +
                       "$('#spnMasterInfo').text('" + ErrorMsg + "');", true);


                    }
                    else
                    {
                        EmailDetails = EmailDetails.Where(a => a.UserID != Convert.ToInt32(ClientSession.UserID)).ToList();
                        if (EmailDetails.Count == 0)
                            Application.Clear();
                    }

                }

            }



            ScriptManager.RegisterStartupScript(this, this.GetType(), "CallAlert", "save();", true);
        }




        protected void btnUsd_ServerClick(object sender, EventArgs e)
        {
            Response.Redirect("frmInvoices.aspx");
        }



        protected void btnCustomer_ServerClick(object sender, EventArgs e)
        {
            Response.Redirect("frmCustomer.aspx");
        }

        protected void Search_Click(object sender, EventArgs e)
        {
            Session["InvoiceorCustNum"] = txtSearch.Text;
            if (hdnAdvanceSearch.Value == "1")
            {
                AdvanceSearch advanceSearch = new AdvanceSearch();
                advanceSearch.Name = txtCustomerName.Text;
                advanceSearch.Email = txtEmail.Text;
                advanceSearch.PersonalNumber = txtPersonalNumber.Text;
                advanceSearch.Number = "0";

                Session["AdvanceSearch"] = advanceSearch;
            }
            else
                Session["AdvanceSearch"] = null;
            btnUsd_ServerClick(sender, e);
        }



        protected void btnChart_ServerClick(object sender, EventArgs e)
        {
            Response.Redirect("frmDashboard.aspx");
        }

        protected void btnPaymentInfo_ServerClick(object sender, EventArgs e)
        {

            Response.Redirect("frmReports.aspx");
        }

        protected void btnAddNew_ServerClick(object sender, EventArgs e)
        {

            Response.Redirect("frmAddNew.aspx");
        }

        protected void lstStatistics_SelectedIndexChanged(object sender, EventArgs e)
        {
            Response.Redirect("frmBatches.aspx");
        }





        protected void btnClientName_Click(object sender, EventArgs e)
        {
            ClientSession.FlushSession();
            txtClientName.Text = hdnClientName.Value.Trim();
            IUserPresentationBusinessLayer objUserLayer = new UserBusinessLayer();
            objUserLayer.UpdateClientID(ClientSession.UserName.Trim(), hdnClientID.Value);
            ClientSession.ClientID = hdnClientID.Value;

            Response.Redirect("frmDashboard.aspx");
            ScriptManager.RegisterStartupScript(this, this.GetType(), "CallAlert", "save();", true);
        }

        protected void lnkLogout_Click(object sender, EventArgs e)
        {
            try
            {
                string sDirectory = "~/Documents/" + Session.SessionID;
                if (Directory.Exists(HttpContext.Current.Server.MapPath(sDirectory)))
                {
                    Directory.Delete(HttpContext.Current.Server.MapPath(sDirectory), true);
                }
            }
            catch
            {
                //catch the issue
            }
            IUserPresentationBusinessLayer objUserLayer = new UserBusinessLayer();
            int iResult = objUserLayer.UpdateSessionID(ClientSession.UserName.Trim(), System.DateTimeOffset.Now.ToUnixTimeSeconds());
            Session.Abandon();
            Response.Redirect("frmLogin.aspx");

        }

        protected void btnAccountSettings_Click(object sender, EventArgs e)
        {
            Response.Redirect("frmAccountSettings.aspx");
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

       

        protected void btnCreditCheck_ServerClick(object sender, EventArgs e)
        {
            Response.Redirect("frmCreditCheck.aspx");
        }
    }
}