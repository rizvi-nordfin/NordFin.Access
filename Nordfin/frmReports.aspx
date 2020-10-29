<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Nordfin.Master" Title="NordfinCapital" CodeBehind="frmReports.aspx.cs" Inherits="Nordfin.frmReports" %>

<asp:Content ID="Content1" ContentPlaceHolderID="NordfinContentHolder" runat="server" style="background-color: #232D41;">


    <meta name="viewport" content="width=device-width, initial-scale=1">
    <!-- <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/css/bootstrap.min.css"> -->
    <!-- <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.2.1/jquery.min.js"></script> -->
    <!-- <script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/js/bootstrap.min.js"></script> -->
    <script src="Scripts/jsReports.js?version=<%=ConfigurationManager.AppSettings["VersionConfiguration"].ToString() %>"></script>
    <link href="Styles/Reports.css?version=<%=ConfigurationManager.AppSettings["VersionConfiguration"].ToString() %>" rel="stylesheet" />
       <script src="//code.jquery.com/jquery-1.10.2.js"></script>
    <script src="//code.jquery.com/ui/1.11.1/jquery-ui.js"></script>    

      <link href="Styles/PaymentInformation.css?version=<%=ConfigurationManager.AppSettings["VersionConfiguration"].ToString() %>" rel="stylesheet" />
     <link rel="stylesheet" href="Styles/jquery-ui-NordFin.css" />

    <div class="dashboardContainer">
        <div class="container-fluid">

            <div class="dashboardHeader">
                <div class="dashboardHeadline">Reports</div>
            </div>

            <div class="container-fluid">

                <div class="row">

                    <div class="reportsCardContainer col-md-3 col-md-offset-1;">
                        <div class="reportsCard">
                            <div class="info-icon"></div>
                            <div class="reportsCardContent">
                                <img src="Images/NFC_reports_batches.svg" alt="" />
                                <div class="reportsCardCaption">Batches</div>
                            </div>
                            <div class="reportsCardBottom">
                                <asp:Button CssClass="button reportsCardButton form-control" runat="server" Text="Download" ID="btnBatchesReport" OnClick="btnBatchesReport_Click"></asp:Button>
                            </div>
                        </div>
                    </div>

                    <div class="reportsCardContainer col-md-3">
                        <div class="reportsCard">
                            <div class="info-icon"></div>
                            <div class="reportsCardContent">
                                <img src="Images/NFC_reports_ledgerlist.svg" alt="" />
                                <div class="reportsCardCaption">Ledgerlist</div>
                            </div>
                            <div class="reportsCardBottom">
                                <asp:Button CssClass="button reportsCardButton form-control" runat="server" Text="Download" ID="Button1" OnClick="btnLederlistReport_Click"></asp:Button>
                            </div>
                        </div>
                    </div>

                    <div class="reportsCardContainer col-md-3">
                        <div class="reportsCard">
                            <div class="info-icon"></div>
                            <div class="reportsCardContent">
                                <img src="Images/NFC_reports_customerlist.svg" alt="" />
                                <div class="reportsCardCaption">Customer List</div>
                            </div>
                            <div class="reportsCardBottom">
                                <asp:Button CssClass="button reportsCardButton form-control" runat="server" Text="Download" ID="btnCustomerList" OnClick="btnCustomerList_Click"></asp:Button>
                            </div>
                        </div>
                    </div>


                    <div class="reportsCardContainer col-md-3" style="display:none;">
                        <div class="reportsCard">
                            <div class="info-icon"></div>
                            <div class="reportsCardContent">
                                <img src="Images/NFC_reports_batches.svg" alt="" />
                                <div class="reportsCardCaption">Daily Payments</div>
                            </div>
                            <div class="reportsCardBottom">
                                <asp:Button CssClass="button reportsCardButton form-control" runat="server" Text="Download" ID="Button3" OnClick="btnLederlistReport_Click"></asp:Button>
                            </div>
                        </div>
                    </div>

                     <div class="reportsCardContainer col-md-3" >
                        <div class="reportsCard">
                            <div class="info-icon"></div>
                            <div class="reportsCardContent">
                                <img src="Images/NFC_reports_invoicelist.svg" alt="" />
                                <div class="reportsCardCaption">Invoices Period</div>
                            </div>
                            <div class="reportsCardBottom">
                                <asp:Button CssClass="button reportsCardButton form-control" runat="server" Text="Download" ID="Button2" OnClientClick="return DateSelection('Invoices Period');" ></asp:Button>
                            </div>
                        </div>
                    </div>


                     <div class="reportsCardContainer col-md-3" >
                        <div class="reportsCard">
                            <div class="info-icon"></div>
                            <div class="reportsCardContent">
                                <img src="Images/NFC_reports_invoicelist.svg" alt="" />
                                <div class="reportsCardCaption">Periodic report<br>Bookkeeping</div>
                            </div>
                            <div class="reportsCardBottom">
                                <asp:Button CssClass="button reportsCardButton form-control" runat="server" Text="Download" ID="Button4" OnClientClick="return DateSelection('Periodic report');" ></asp:Button>
                            </div>
                        </div>
                    </div>


                        <div class="reportsCardContainer col-md-3" >
                        <div class="reportsCard">
                            <div class="info-icon"></div>
                            <div class="reportsCardContent">
                                <img src="Images/NFC_reports_invoice-transactions.svg" alt="" />
                                <div class="reportsCardCaption"> Transactions in period <br>Bookkeeping</div>
                            </div>
                            <div class="reportsCardBottom">
                                <asp:Button CssClass="button reportsCardButton form-control" runat="server" Text="Download" ID="Button5" OnClientClick="return Message();" ></asp:Button>
                            </div>
                        </div>
                    </div>

                      <div class="reportsCardContainer col-md-3">
                        <div class="reportsCard">
                            <div class="info-icon"></div>
                            <div class="reportsCardContent">
                                <img src="Images/NFC_reports_ledgerlist.svg" alt="" />
                                <div class="reportsCardCaption">Contested Invoices</div>
                            </div>
                            <div class="reportsCardBottom">
                                <asp:Button CssClass="button reportsCardButton form-control" runat="server" Text="Download" ID="btnContested" OnClick="btnContested_Click"></asp:Button>
                            </div>
                        </div>
                    </div>


                    <div class="modal fade" id="mdlReport" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
                        <div class="modal-dialog" role="document">
                            <div class="modal-content" style="top: 200px;    background: none;border:none;">
                                <div class="modal-header dashboardHeadline" style="background-color: #323e53; color: #fff;font-size:16px;width: 85%;">
                                    <h5 class="modal-title" id="informModalLabel"></h5>
                                    <button type="button" class="modalcloseButton" data-dismiss="modal" aria-label="Close" style="top: 35px;right: 100px;">
                                        <span aria-hidden="true">✕</span>
                                    </button>
                                </div>
                                <div class="modal-body" style="background-color: #323e53; color: #fff;width: 85%;">
                                    <div class="dropdownAdvanceSearchInput">
                                        <label>
                                            <span style="color: #A9BFD5;    text-transform: uppercase;font-size:12px;"> From Date</span>
                        <asp:TextBox ID="txtFromDate" runat="server" autocomplete="off"  CssClass="form-control textboxColor"  ></asp:TextBox>
                                        </label>
                                    </div>

                                     <div class="dropdownAdvanceSearchInput">
                                        <label>
                                             <span style="color: #A9BFD5;    text-transform: uppercase;font-size:12px;">To Date</span>
                                            
                        <asp:TextBox ID="txtToDate" runat="server" autocomplete="off"  CssClass="form-control textboxColor" ></asp:TextBox>
                                        </label>
                                    </div>

                                    <div class="dropdownAdvanceSearchInput">
                                        <label>
                                           
                          <asp:button  CssClass="modalbutton" id="btnSubmit"   Text="Submit" runat="server" OnClientClick="submitClick();" OnClick="btnSubmit_Click"></asp:button>
                                        </label>
                                    </div>
                                </div>
                              
                            </div>
                        </div>
                    </div>

                </div>

            </div>




        </div>
    </div>
     <asp:HiddenField ID="hdnExport" Value="0" runat="server" />
      <asp:HiddenField ID="hdnAdmin" Value="0" runat="server" />
</asp:Content>
