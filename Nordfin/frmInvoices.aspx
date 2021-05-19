<%@ Page Language="C#" MasterPageFile="~/Nordfin.Master" AutoEventWireup="true" Title="NFC ACCESS" CodeBehind="frmInvoices.aspx.cs" Inherits="Nordfin.frmInvoices"
    UICulture="sv-SE" Culture="sv-SE" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ModalWindow" %>
<asp:Content ID="Content1" ContentPlaceHolderID="NordfinContentHolder" runat="server" style="padding: 5px;">

    <script src="Scripts/jsInvoices.js?version=<%=ConfigurationManager.AppSettings["VersionConfiguration"].ToString() %>"></script>

    <link href="Styles/Invoices.css?version=<%=ConfigurationManager.AppSettings["VersionConfiguration"].ToString() %>" rel="stylesheet" />

    <script src="Scripts/jsPsInformationModal.js?version=<%=ConfigurationManager.AppSettings["VersionConfiguration"].ToString() %>"></script>
    <div class="dashboardContainer">
        <div class="container-fluid">
            <div class="row">
                <div class="col-lg-3 dashboardHeadline" style="margin-top: 0px !important">
                    Invoices: Overview   
                    <asp:Button ID="btnExport" Text="Export" CssClass="export" Style="width: 75px; display: none;" OnClick="btnExport_Click" runat="server" CausesValidation="false" />
                </div>
                <div class="col-lg-9" style="padding-right: 38px;">
                 <asp:Button Text="New Customer" id="btnCreateCustomer" CssClass="newCustomer" OnClientClick="return OpenAddCustomer();" runat="server" CausesValidation="false"  />
                </div>
                </div>
            <div class="row dashboardHeader">
                <div class="col-lg-3 dashboardHeadline">
                </div>
                <div class="col-lg-9 summaryRow">
                    <div class="summaryInvoices" style="margin-top: 60px !important">
                        <div class="summaryHeading">
                            <div class="textOrange" style="background: #38445D;">

                                <div style="float: left; display: inline;">
                                    Amount
                                </div>
                                <div style="display: inline;">
                                    <asp:Label runat="server" CssClass="textWhite" ID="lblSumAmount"></asp:Label>
                                </div>

                            </div>
                        </div>
                        <div class="summaryHeading">
                            <div class="textOrange" style="background: #38445D;">
                                <div style="float: left; display: inline;">
                                    FEES 
                                </div>
                                <div style="display: inline;">
                                    <asp:Label runat="server" CssClass="textWhite" ID="lblFeesAmount"></asp:Label>
                                </div>
                            </div>
                        </div>
                        <div class="summaryHeading">
                            <div class="textOrange" style="background: #38445D;">
                                <div style="float: left; display: inline;">
                                    REMAIN 
                                </div>
                                <div style="display: inline;">
                                    <asp:Label ID="lblRemain" CssClass="textWhite" runat="server" />
                                </div>
                            </div>
                        </div>
                        <div class="summaryHeading">
                            <div class="textOrange" style="background: #38445D;">
                                <div style="float: left; display: inline;">
                                    TOTAL REMAIN
                                </div>
                                <div style="display: inline;">
                                    <asp:Label ID="lblTotalRemain" CssClass="textWhite" runat="server" />
                                </div>
                            </div>
                        </div>
                        <div class="summaryHeading">
                            <div class="textOrange" style="background: #38445D;">
                                <div style="float: left; display: inline;">
                                    OVERPAID 
                                </div>
                                <div style="display: inline;">
                                    <asp:Label ID="lblOverPaid" CssClass="textWhite" runat="server" />
                                </div>
                            </div>
                        </div>
                        
                    </div>
                </div>


                <div class="hidden">
                    <asp:Button Text="Download As" class="form-control invoicesDownloadAsButton" runat="server" CausesValidation="false"/>
                </div>
            </div>

            <asp:Button ID="btnOpenModal" runat="server" Style="display: none;" CausesValidation="false" />
            <ModalWindow:ModalPopupExtender ID="mp1" runat="server" PopupControlID="pnlModal" TargetControlID="btnOpenModal"
                CancelControlID="closeButton">
            </ModalWindow:ModalPopupExtender>
            <div id="divPnl">
                <asp:Panel ID="pnlModal" runat="server" CssClass="Popup" align="center" Style="display: none">
                    <iframe id="iframeModal" style="height: 90%; width: 95%" runat="server"></iframe>

                    <asp:Button ID="closeButton" runat="server" Style="display: none;" />
                </asp:Panel>
            </div>
            <asp:UpdatePanel runat="server" ID="UpdatePanel1" >
                <ContentTemplate>
                    <div class="tableFixHead tableMarginBg">
                        <asp:GridView ID="grdInvoices" runat="server" AutoGenerateColumns="False" AllowSorting="true" OnSorting="grdInvoices_Sorting" OnRowDataBound="grdInvoices_RowDataBound" EmptyDataRowStyle-CssClass="Emptyrow" ViewStateMode="Enabled" Visible="true" Style="color: white; overflow-x: scroll; overflow-y: scroll;"
                            ShowHeaderWhenEmpty="true" CssClass="table table-borderless">
                            <HeaderStyle BackColor="#475672" CssClass="GVFixedHeader" />
                            <Columns>
                                <asp:TemplateField ItemStyle-CssClass="itemalign" HeaderText="CUSTOMER" SortExpression="Customernumber" HeaderStyle-CssClass="itemalign">
                                    <ItemTemplate>

                                        <asp:LinkButton CausesValidation="false" CssClass="linkcss" Text='<%# Bind("Customernumber") %>' CommandName="Sort" ID="gridLinkCustNum" CommandArgument=' <%# Bind("Customernumber") %>' OnClick="gridLinkCustNum_Click" runat="server" />
                                    </ItemTemplate>

                                </asp:TemplateField>
                                <asp:TemplateField ItemStyle-CssClass="itemalign" HeaderText="INVOICE" SortExpression="Invoicenumber" HeaderStyle-CssClass="itemalign">
                                    <ItemTemplate>

                                        <asp:LinkButton CausesValidation="false" CssClass="linkcss" Text='<%# Bind("Invoicenumber") %>' OnClientClick="return LinkClick(this);" overpaymentData='<%# Eval("Overpayment") %>'
                                            collectionStatus=' <%# Eval("Collectionstatus") %>'   combineInvoice='<%# Eval("CombineInvoice") %>'  custInvoice=' <%# Eval("Customernumber") %>'
                                            remainData='<%# Eval("Remainingamount") %>' invoiceData='<%# Eval("Customernumber") +"|"+ Eval("InvoiceID")%>' ID="gridLink" CommandArgument=' <%# Eval("Customernumber") +"|"+ Eval("InvoiceID")%>' runat="server" />
                                    </ItemTemplate>

                                </asp:TemplateField>

                                <asp:BoundField DataField="CustomerName" HeaderStyle-CssClass="itemalign" ItemStyle-CssClass="itemalign" HeaderText="Name" SortExpression="CustomerName" />

                                <asp:BoundField DataField="CurrencyCode" HeaderStyle-CssClass="itemalign" ItemStyle-CssClass="itemalign" HeaderText="CURRENCY" SortExpression="CurrencyCode" />
                                <asp:BoundField DataField="Invoiceamount" DataFormatString="{0:#,0.00}" HeaderStyle-CssClass="itemalign" ItemStyle-CssClass="itemalign" HeaderText="AMOUNT" SortExpression="Invoiceamount" />
                                <asp:BoundField DataField="Fees" HeaderStyle-CssClass="itemalign" ItemStyle-CssClass="itemalign" HeaderText="FEES" SortExpression="Fees" />

                                <asp:BoundField DataField="Billdate" HeaderStyle-CssClass="itemalign" ItemStyle-CssClass="itemalign tableNoWrap" HeaderText="BILL DATE" SortExpression="Billdate" />
                                <asp:BoundField DataField="Duedate" HeaderStyle-CssClass="itemalign" ItemStyle-CssClass="itemalign tableNoWrap" HeaderText="DUE DATE" SortExpression="Duedate" />
                                <asp:BoundField DataField="Remainingamount" DataFormatString="{0:#,0.00}" HeaderStyle-CssClass="itemalign" ItemStyle-CssClass="itemalign" HeaderText="REMAIN" SortExpression="Remainingamount" />


                                <asp:BoundField DataField="TotalRemaining" DataFormatString="{0:#,0.00}" HeaderStyle-CssClass="itemalign" ItemStyle-CssClass="itemalign" HeaderText="TOTAL REMAIN" SortExpression="TotalRemaining" />

                                <asp:TemplateField ItemStyle-CssClass="itemalign" HeaderText="Collection Status" SortExpression="Collectionstatus" HeaderStyle-CssClass="itemalign">
                                    <ItemTemplate>

                                        <asp:LinkButton CausesValidation="false" CssClass="linkNormalcss" Text='<%# Bind("Collectionstatus") %>' CommandName="Sort" ID="gridLinkCollectionSatatus" 
                                           invoice='<%# Eval("Invoicenumber") %>'   runat="server" />
                                    </ItemTemplate>

                                </asp:TemplateField>


                                  <asp:BoundField DataField="Credited" HeaderStyle-CssClass="itemalign" ItemStyle-CssClass="itemalign" HeaderText="Credited" SortExpression="Credited" />
                                <asp:BoundField DataField="ClosedCredit" HeaderStyle-CssClass="itemalign" ItemStyle-CssClass="itemalign" HeaderText="Closed with credit" SortExpression="ClosedCredit" />


                                <asp:BoundField DataField="Paymentreference" HeaderStyle-CssClass="itemalign" ItemStyle-CssClass="itemalign" HeaderText="PAY REF" SortExpression="Paymentreference" />
                                <asp:BoundField DataField="Overpayment" DataFormatString="{0:#,0.00}" HeaderStyle-CssClass="itemalign" ItemStyle-CssClass="itemalign" HeaderText="OVER PAID" SortExpression="Overpayment" />
                                <asp:TemplateField ItemStyle-CssClass="itemalign" HeaderStyle-CssClass="itemalign">
                                    <ItemTemplate>

                                        <asp:Button CausesValidation="false" runat="server" CssClass="invoicesDownloadButton button button-table" ID="btnPDFDownload" collectionStatus=' <%# Eval("Collectionstatus") %>'
                                            custInvoice=' <%# Eval("Customernumber") %>' combineInvoice='<%# Eval("CombineInvoice") %>'
                                            CommandArgument=' <%# Eval("CombineInvoice") %>' OnClientClick="return ProcessingModal();" OnClick="btnPDFDownload_Click" Text="Export" />
                                    </ItemTemplate>
                                    <HeaderTemplate>
                                        PDF
                                    </HeaderTemplate>
                                </asp:TemplateField>

                            </Columns>
                            <EmptyDataTemplate>No Record Available</EmptyDataTemplate>
                        </asp:GridView>
                    </div>

                    <div class="featureNotAvailablePnlBG hidden">
                        <div class="featureNotAvailablePnl">

                            <div id="PnlProcessing">
                                <div style="text-align: center; color: #09abdc;">
                                    <span></span>
                                </div>
                                <div class="progress">

                                    <div class="progress-bar progress-bar-striped progress-bar-animated" role="progressbar" style="width: 100%" aria-valuenow="25" aria-valuemin="0" aria-valuemax="100"></div>

                                </div>
                            </div>
                        </div>
                    </div>


                    <div class="modal" id="mdlUpdateInfo" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
                        <div class="modal-dialog" role="document">
                            <div class="modal-content" style="top: 100px; background: none; border: none;">
                                <div class="modal-header dashboardHeadline" style="background-color: #323e53; color: #fff; font-size: 16px;">
                                    <h5 class="modal-title modalTextcolor dashboardHeadlineModal" id="updateInfoModalLabel">Email</h5>
                                    <button type="button" class="modalcloseButton" data-dismiss="modal" aria-label="Close" style="top: 35px; right: 20px;">
                                        <span aria-hidden="true">✕</span>
                                    </button>
                                </div>
                                <div class="modal-body" style="background-color: #323e53; color: #fff;">


                                    <div id="pnlEmail" class="form-group">
                                        <label>
                                            Email
                                    <asp:TextBox ID="txtCustEmail" runat="server" autocomplete="nope" CssClass="form-control textboxModalColor"></asp:TextBox>
                                        </label>

                                        <span id="EmailValid" class="short">
                                            <span id="spnEmail" class="hide" style="color: #f83030">Enter a valid email</span>
                                        </span>

                                    </div>

                                    <div class="form-group">
                                        <label>
                                            Header
                                    <asp:TextBox ID="txtEmailHeader" runat="server" autocomplete="nope" CssClass="form-control textboxModalColor"></asp:TextBox>
                                        </label>


                                    </div>

                                    <div class="form-group">
                                        <label>
                                            Body
                                    <asp:TextBox TextMode="MultiLine" ID="txtEmailBody" runat="server" autocomplete="nope" CssClass="form-control textboxModalColor textareaHeight"></asp:TextBox>
                                        </label>


                                    </div>

                                    <div class="form-group">
                                        <asp:Button Text="Send" class="button updateInfoButton form-control" runat="server" ID="btnSend" OnClientClick="ProgressBarDisplay();" OnClick="btnSend_Click" Width="128px" CausesValidation="false"/>


                                    </div>

                                    <div id="PnlMsg" style="display: none;" class="alert alert-success" role="alert">
                                        <span id="spnMsg"></span>
                                    </div>
                                    <div id="Pnlprogress" style="display: none;">
                                        <div style="text-align: center; color: #3DADC5;">
                                            <span>Processing please wait...</span>
                                        </div>
                                        <div class="progress">

                                            <div class="progress-bar progress-bar-striped progress-bar-animated" role="progressbar" style="width: 100%" aria-valuenow="25" aria-valuemin="0" aria-valuemax="100"></div>
                                        </div>
                                    </div>


                                </div>
                                <div class="modal-footer" style="background-color: #323E53; padding: 5px;">
                                    <%--<asp:Button Text="Send" class="button updateInfoButton form-control" runat="server" ID="btnEmail" OnClick="btnEmail_Click"  Width="128px" />--%>
                                </div>



                            </div>
                        </div>
                    </div>

                    <div class="modal" id="mdlExport" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
                        <div class="modal-dialog" role="document">
                            <div class="modal-content" style="top: 200px; background: none; border: none; width: 75%;">
                                <div class="modal-header dashboardHeadline" style="background-color: #323e53; color: #fff; font-size: 16px; width: 86%;">
                                    <div class="ml-3">
                                        <h5 class="modal-title modalTextcolor dashboardHeadlineModal" id="ExportModal">Export</h5>
                                    </div>
                                    <button type="button" class="modalcloseButton" data-dismiss="modal" aria-label="Close" style="top: 35px; right: 82px;">
                                        <span aria-hidden="true">✕</span>
                                    </button>
                                </div>
                                <div class="modal-body" style="background-color: #323e53; color: #fff; width: 86%;">



                                    <div class="col-md-12 form-inline">
                                        <div style="float: left; width: 65%">
                                            <asp:Panel runat="server" ID="pnlInvoices">
                                                <asp:CheckBox ID="chkInvoices" AutoPostBack="false" Checked="true" Style="margin-top: 5px;" CssClass="checkbox"
                                                    runat="server" Text="Invoice"></asp:CheckBox>
                                            </asp:Panel>
                                            <asp:Panel runat="server" ID="pnlRemind" >
                                                <asp:CheckBox ID="chkRemind" AutoPostBack="false" Checked="true" Style="margin-top: 5px;" CssClass="checkbox"
                                                    runat="server" Text="Reminder"></asp:CheckBox>
                                            </asp:Panel>
                                            <asp:Panel runat="server" ID="pnlDC">
                                                <asp:CheckBox ID="chkDC" AutoPostBack="false" Checked="true" Style="margin-top: 5px;" CssClass="checkbox"
                                                    runat="server" Text="Debt Collection"></asp:CheckBox>
                                            </asp:Panel>
                                        </div>

                                        <div class="ml-3" style="float: right;">
                                            <div>
                                                <asp:Button runat="server" CssClass="invoicesDownloadButton button button-table" OnClientClick="PdfDownloadClick();" OnClick="btnDownload_Click" ID="btnDownload" Text="Download" CausesValidation="false" />
                                            </div>
                                            <div class="mt-3">
                                                <asp:Button runat="server" CssClass="invoicesDownloadButton button button-table" OnClick="btnEmail_Click" ID="btnEmail" Text="Email" CausesValidation="false" />
                                            </div>
                                        </div>


                                    </div>


                                    <div id="PnlDownloadMsg" style="display: none;" class="mt-2 alert alert-success" role="alert">
                                        <span id="spnDownloadMsg"></span>
                                    </div>
                                    <div id="PnlDownloadprogress" style="display: none;">
                                        <div style="text-align: center; color: #3DADC5;">
                                            <span>Downloading please wait...</span>
                                        </div>
                                        <div class="progress">

                                            <div class="progress-bar progress-bar-striped progress-bar-animated" role="progressbar" style="width: 100%" aria-valuenow="25" aria-valuemin="0" aria-valuemax="100"></div>
                                        </div>
                                    </div>






                                </div>




                            </div>
                        </div>
                    </div>

                    <div class="modal" id="mdlAddCustomer" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true" runat="server">
                        <div class="modal-dialog" role="document">
                            <div class="modal-content" style="top: -10px; background: none; border: none;">
                                <div class="modal-header dashboardHeadline" style="background-color: #323e53; color: #fff; font-size: 16px;">
                                    <h5 class="modal-title modalTextcolor dashboardHeadlineModal" id="informModalLabel">NEW CUSTOMER</h5>
                                    <button type="button" class="modalcloseButton" data-dismiss="modal" aria-label="Close" style="top: 35px; right: 20px;">
                                        <span aria-hidden="true">✕</span>
                                    </button>
                                </div>
                                <div class="modal-body" style="background-color: #323e53; color: #fff;">
                                    <div class="row addCustomerRowHeight" >
                                        <div class="col-md-8">
                                            <asp:Label CssClass="addCustomerLabel" runat="server" ID="spnCustomerName" Text="Customer Name"></asp:Label>
                                            <asp:TextBox ID="txtCustomerName" runat="server" autocomplete="off" CssClass="form-control textboxModalColor"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rfvCustomerName" runat="server" ControlToValidate="txtCustomerName" ErrorMessage="Customer Name is required." ForeColor="Red" Font-Size="Smaller" />
                                            <asp:CompareValidator runat="server" Operator="DataTypeCheck" Type="String" ControlToValidate="txtCustomerName" ErrorMessage="Value must be only text" />
                                        </div>
                                        <div class="col-md-2" style="padding-top:20px">
                                            <asp:RadioButton GroupName="CustomerType" runat="server" Text="Private" Value="PRV" ID="rbPrivate" CssClass="addCustomerLabel"  />
                                        </div>
                                        <div class="col-md-2" style="padding-top:20px">
                                            <asp:RadioButton GroupName="CustomerType" runat="server" Text="Company" Value="FTG" ID="rbCompany" CssClass="addCustomerLabel" />
                                        </div>
                                    </div>
                                    <div class="row addCustomerRowHeight">
                                        <div class="col-md-6">
                                            <asp:Label CssClass="addCustomerLabel" runat="server" ID="spnCustomerNumber" Text="Customer Number"></asp:Label>
                                            <asp:TextBox ID="txtCustomerNumber" runat="server" autocomplete="off" CssClass="form-control textboxModalColor"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rfvCustomerNumber" runat="server" ControlToValidate="txtCustomerNumber" ErrorMessage="Customer Number is required." ForeColor="Red" Font-Size="Smaller" />
                                            <asp:CompareValidator runat="server" Operator="DataTypeCheck" Type="Integer" ControlToValidate="txtCustomerNumber" ErrorMessage="Value must be only number" ForeColor="Red" Font-Size="Smaller" />
                                        </div>
                                        <div class="col-md-6">
                                            <asp:Label CssClass="addCustomerLabel" runat="server" ID="spnPersonalNumber" Text="Personal Number"></asp:Label>
                                            <asp:TextBox ID="txtPersonalNumber" runat="server" autocomplete="off" CssClass="form-control textboxModalColor" placeholder="YYMMDDNNNN"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rfvPersonalNumber" runat="server" ControlToValidate="txtPersonalNumber" ErrorMessage="Personal Number is required." ForeColor="Red" Font-Size="Smaller"/>
                                            <asp:CompareValidator runat="server" Operator="DataTypeCheck" Type="Integer" ControlToValidate="txtPersonalNumber" ErrorMessage="Value must be only number and in specified format (YYMMDDNNNN)" />
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-12">
                                            <asp:Label CssClass="addCustomerLabel" ID="spnAddress1" Text="Address Line 1" runat="server"></asp:Label>
                                            <asp:TextBox ID="txtAddress1" runat="server" autocomplete="off" CssClass="form-control textboxModalColor"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rfvAddress1" runat="server" ControlToValidate="txtAddress1" ErrorMessage="Address Line 1 is required." ForeColor="Red" Font-Size="Smaller" />
                                        </div>
                                    </div>
                                    <div class="row addCustomerRowHeight">
                                        <div class="col-md-12">
                                            <asp:Label CssClass="addCustomerLabel" runat="server" Text="Address Line 2" ID="spnAddress2"> </asp:Label>
                                            <asp:TextBox ID="txtAddress2" runat="server" autocomplete="off" CssClass="form-control textboxModalColor"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-4">
                                            <asp:Label CssClass="addCustomerLabel" runat="server" Text="Postal Code" ID="spnPostalCode"> </asp:Label>
                                            <asp:TextBox ID="txtPostalCode" runat="server" autocomplete="off" CssClass="form-control textboxModalColor"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rfvPostalCode" runat="server" ControlToValidate="txtPostalCode" ErrorMessage="Postal Code is required." ForeColor="Red" Font-Size="Smaller" />
                                        </div>

                                        <div class="col-md-4">
                                            <asp:Label CssClass="addCustomerLabel" runat="server" ID="spnCity" Text="City"></asp:Label>
                                            <asp:TextBox ID="txtCity" runat="server" autocomplete="off" CssClass="form-control textboxModalColor"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rfvCity" runat="server" ControlToValidate="txtCity" ErrorMessage="City is required." ForeColor="Red" Font-Size="Smaller" />
                                        </div>
                                        <div class="col-md-4">
                                            <asp:Label CssClass="addCustomerLabel" runat="server" ID="spnCountry" Text="Country"> </asp:Label>
                                            <asp:TextBox ID="txtCountry" runat="server" autocomplete="off" CssClass="form-control textboxModalColor"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="row addCustomerRowHeight">
                                        <div class="col-md-12">
                                            <asp:Label CssClass="addCustomerLabel" runat="server" ID="spnModalEmail" Text="Email"> </asp:Label>
                                            <asp:TextBox ID="txtEmail" runat="server" autocomplete="off" CssClass="form-control textboxModalColor"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-12">
                                            <asp:Label CssClass="addCustomerLabel" runat="server" ID="spnPhonenumber" Text="Phone Number"> </asp:Label>
                                            <asp:TextBox ID="txtPhoneNumber" runat="server" autocomplete="off" CssClass="form-control textboxModalColor"></asp:TextBox>
                                        </div>
                                    </div>
                                <div class="modal-footer" style="background-color: #323E53; padding: 0px; margin-top: -15px;">
                                    <asp:Button runat="server" class="modalbutton" ID="btnAddCustomer" Text="Add" OnClick="btnAddCustomer_Click"></asp:Button>
                                </div>
                            </div>
                        </div>
                    </div>
                 </div>

                    <div class="modal" id="mdlError"  data-backdrop="static">
                        <div class="modal-dialog" role="document">
                            <div class="modal-content">
                                <div class="modal-body" style="background-color: #323e53; color: #fff;">
                                    <div class="container-fluid">
                                        <div class="row">
                                            <div class="col-md-1">
                            <i class="far fa-times-circle errorIcon"></i>
                            </div>
                        <div class="col-md-11">
                                                <p style="color: white; font-size: 15px !important;" id="txtError" />
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-md-9">
                                            </div>
                                            <div class="col-md-3">
                                                <button type="button" class="export rowButton" style="float: right" onclick="closeErrorModal();">OK</button>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>

                    <div class="modal" id="mdlSuccess" tabindex="-1" role="dialog">
                        <div class="modal-dialog" role="document">
                            <div class="modal-content errorModel">
                                <div class="modal-body" style="background-color: #323e53; color: #fff; text-align: center">
                                    <div class="container-fluid">
                                        <div class="row">
                                            <div class="col-md-1">
                                                <i class="far fa-thumbs-up successIcon"></i>
                                            </div>
                                            <div class="col-md-11">
                                                <p style="color: white; font-size: 15px !important; text-align: left">Customer Added Successfully.</p>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-md-9">
                                            </div>
                                            <div class="col-md-3">
                                                <button type="button" class="export rowButton" style="float: right" onclick="closeSuccessModal();">OK</button>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>

                    <div style="display: none;">
                        <iframe id="pdfInvoices" runat="server"></iframe>
                        <iframe id="pdfDC" runat="server"></iframe>
                        <iframe id="pdfRemind" runat="server"></iframe>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>

            <asp:HiddenField ID="hdnClientName" runat="server" />
            <asp:HiddenField ID="hdnCombineInvoice" runat="server" />
            <asp:HiddenField ID="hdnFileName" runat="server" />
            <asp:HiddenField ID="hdnArchiveLink" runat="server" />
            <asp:HiddenField ID="hdnInvoiceNumber" runat="server" />
            <asp:HiddenField ID="hdnEmailID" runat="server" />
            <asp:HiddenField ID="hdnCollectionStatus" runat="server" />
            <a id="pdfViewer" href="" runat="server" target="_blank"></a>
        </div>

    </div>
</asp:Content>

