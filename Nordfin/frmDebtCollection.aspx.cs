using Nordfin.workflow.Business;
using Nordfin.workflow.BusinessLayer;
using Nordfin.workflow.Entity;
using Nordfin.workflow.PresentationBusinessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Nordfin
{
    public partial class frmDebtCollection : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                IDebtCollectionPresentationBusinessLayer objDebtCollectionLayer = new DebtCollectionBusinessLayer();
                IList<DebtCollectionList> debtCollectionList = objDebtCollectionLayer.GetDebtCollectionLists(Convert.ToInt32(ClientSession.ClientID));
                grdDebtCollection.DataSource = debtCollectionList;
                grdDebtCollection.DataBind();
            }
        }

        protected void btnCollectionStop_Click(object sender, EventArgs e)
        {
            IDebtCollectionPresentationBusinessLayer objDebtCollectionLayer = new DebtCollectionBusinessLayer();
            int InvoiceID = Convert.ToInt32(((Button)sender).CommandArgument);

            bool bResult = objDebtCollectionLayer.setCollectionStop(InvoiceID);
        }
    }
}