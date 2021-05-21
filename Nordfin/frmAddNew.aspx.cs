using Nordfin.workflow.Business;
using Nordfin.workflow.Entity;
using Nordfin.workflow.PresentationBusinessLayer;
using System;
using System.Web.UI;

namespace Nordfin
{
    public partial class frmAddNew : Page
    {
        IInvoicesPresentationBusinessLayer objInvoicesLayer = new InvoicesBusinessLayer();
        protected void Page_Load(object sender, EventArgs e)
        {
            if(IsPostBack)
            {

            }
        }

        protected void btnAddCustomer_Click(object sender, EventArgs e)
        {
            CustomerInfo customerInfo = new CustomerInfo
            {
                Name = txtCustomerName.Text,
                Address1 = txtAddress1.Text,
                Address2 = txtAddress2.Text ?? null,
                PostalCode = txtPostalCode.Text,
                City = txtCity.Text,
                Country = drpCountry.SelectedValue ?? null,
                Email = txtEmail.Text ?? null,
                PhoneNumber = hdnPhoneCode.Value + txtPhoneNumber.Text,
                UserID = Convert.ToInt32(ClientSession.UserID),
                CustomerNumber = txtCustomerNumber.Text,
                ClientID = int.Parse(ClientSession.ClientID),
                PersonalNumber = !string.IsNullOrWhiteSpace(txtPersonalNumber.Text) ? txtPersonalNumber.Text.Replace("-", "") : null,
                CustomerType = bool.Parse(hdnPrivate.Value) ? "PRV" : "FTG"
            };
            ViewState["customerInfo"] = customerInfo;
            var alreadyExistsDictionary = objInvoicesLayer.CheckCustomerAlreadyExists(customerInfo.CustomerNumber, customerInfo.PersonalNumber, customerInfo.ClientID);
            alreadyExistsDictionary.TryGetValue("CustomerNumber", out string customerNumberExists);
            if (!string.IsNullOrWhiteSpace(customerNumberExists))
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "Pop", "showErrorModal('" + customerNumberExists + "');", true);
                return;
            }

            alreadyExistsDictionary.TryGetValue("PersonalNumber", out string personalNumberExists);
            if (!string.IsNullOrWhiteSpace(personalNumberExists))
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "Pop", "showConfirmModal('" + personalNumberExists + "');", true);
            }
            else
            {
                SaveCustomer();
            }
        }

        protected void btnConfirmOk_Click(object sender, EventArgs e)
        {
            SaveCustomer();
        }

        protected void btnSuccessOk_Click(object sender, EventArgs e)
        {
            Response.Redirect(Request.Url.AbsoluteUri);
        }

        private void SaveCustomer()
        {
            if (ViewState["customerInfo"] != null)
            {
                var customerInfo = (CustomerInfo)ViewState["customerInfo"];
                bool added = objInvoicesLayer.AddNewCustomerInfo(customerInfo);
                if (added)
                {
                    ResetControls();
                    ScriptManager.RegisterStartupScript(this, GetType(), "Pop", "showSuccessModal();", true); 
                    return;
                }
                else
                {
                    var errorMessage = "Failed to add customer. Close & try again.";
                    ScriptManager.RegisterStartupScript(this, GetType(), "Pop", "showErrorModal('" + errorMessage + "');", true);
                    return;
                }
            }
        }

        private void ResetControls()
        {
            txtCustomerName.Text = string.Empty;
            txtCustomerNumber.Text = string.Empty;
            txtPersonalNumber.Text = string.Empty;
            txtAddress1.Text = string.Empty;
            txtAddress2.Text = string.Empty;
            txtPostalCode.Text = string.Empty;
            txtCity.Text = string.Empty;
            txtEmail.Text = string.Empty;
            txtPhoneNumber.Text = string.Empty;
            drpCountry.SelectedValue = "None";

            ViewState["customerInfo"] = null;
            var validators = Page.Validators;
        }
    }
}