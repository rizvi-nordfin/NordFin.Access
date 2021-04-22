using Newtonsoft.Json;
using Nordfin.workflow.Business;
using Nordfin.workflow.BusinessLayer;
using Nordfin.workflow.Entity;
using Nordfin.workflow.PresentationBusinessLayer;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Nordfin
{
    public partial class frmPSInformation : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                try
                {
                    string InvoiceNumber = Request.QueryString["Invoice"];
                    IApiOutgoingPresentationBusinessLayer objApiLayer = new ApiOutgoingBusinessLayer();
                    ApiOutgoing apiOutgoing = objApiLayer.GetApiOutgoing(Convert.ToInt32(ClientSession.ClientID));

                    if (apiOutgoing != null)
                    {

                        PSApiResponse resultPsIDInfo = new JavaScriptSerializer().Deserialize<PSApiResponse>(GetResponsePSApi("https://api.pssync.com/v1/Invoice?invoiceid=" + InvoiceNumber, apiOutgoing));
                        if (resultPsIDInfo != null && !string.IsNullOrEmpty(resultPsIDInfo.Result.PsInvoiceId))
                        {
                            PSApiInvoiceCaseResponse invoiceCaseResponse = JsonConvert.DeserializeObject<PSApiInvoiceCaseResponse>(GetResponsePSApi("https://pssync.com/api/v1/invoicecase?psinvoiceid=" + resultPsIDInfo.Result.PsInvoiceId, apiOutgoing));
                            if(invoiceCaseResponse!=null && invoiceCaseResponse.Result !=null&& invoiceCaseResponse.Result.Balance !=null)
                            {
                                txtDebtAMount.Text = FormatingString(invoiceCaseResponse.Result.Balance.DebtAmount);
                                txtRemainingAmount.Text = FormatingString(invoiceCaseResponse.Result.Balance.RemainingAmount);
                                txtRemainInterest.Text = FormatingString(invoiceCaseResponse.Result.Balance.RemainingInterest);
                                txtRemainFees.Text = FormatingString(invoiceCaseResponse.Result.Balance.RemainingFees);
                                txtRemainTotal.Text = FormatingString(invoiceCaseResponse.Result.Balance.RemainingTotal);
                                txtInternalStatus.Text = invoiceCaseResponse.Result.Status.InternalStatus;
                                txtLatestEventDate.Text = invoiceCaseResponse.Result.Event.LatestEventDate;
                            }
                        }
                    }
                }
                catch { }
            }
        }

        protected string GetResponsePSApi(string url, ApiOutgoing apiOutgoing)
        {
            string responseText = string.Empty;
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Headers.Add("APIKey", apiOutgoing.ApiKey);
            request.Headers.Add("Token", apiOutgoing.ApiToken);
            request.Headers.Add("Secret", apiOutgoing.APiSecret);
            request.ContentType = "application/json";
            request.Method = "GET";
            try
            {

                var httpResponse = (WebResponse)request.GetResponse();
                using (var reader = new System.IO.StreamReader(httpResponse.GetResponseStream()))
                {
                    return reader.ReadToEnd();
                    //return reader.ReadToEnd();
                }
            }
            catch (WebException ex)
            {

            }
            return responseText;

        }
        protected string FormatingString(string Numbers)
        {
            return string.Format("{0:#,0.00}", Convert.ToDecimal(Numbers, CultureInfo.InvariantCulture));
        }
    }
}