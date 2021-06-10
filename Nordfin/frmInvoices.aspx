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
                    <asp:Button ID="btnExport" Text="Export" CssClass="export" Style="width: 75px; display: none;" OnClick="btnExport_Click" runat="server" />
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
                    <asp:Button Text="Download As" class="form-control invoicesDownloadAsButton" runat="server"/>
                </div>
            </div>

            <asp:Button ID="btnOpenModal" runat="server" Style="display: none;" />
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

                                        <asp:LinkButton CssClass="linkcss" Text='<%# Bind("Customernumber") %>' CommandName="Sort" ID="gridLinkCustNum" CommandArgument=' <%# Bind("Customernumber") %>' OnClick="gridLinkCustNum_Click" runat="server" />
                                    </ItemTemplate>

                                </asp:TemplateField>
                                <asp:TemplateField ItemStyle-CssClass="itemalign" HeaderText="INVOICE" SortExpression="Invoicenumber" HeaderStyle-CssClass="itemalign">
                                    <ItemTemplate>

                                        <asp:LinkButton CssClass="linkcss" Text='<%# Bind("Invoicenumber") %>' OnClientClick="return LinkClick(this);" overpaymentData='<%# Eval("Overpayment") %>'
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

                                        <asp:LinkButton CssClass="linkNormalcss" Text='<%# Bind("Collectionstatus") %>' CommandName="Sort" ID="gridLinkCollectionSatatus" 
                                           invoice='<%# Eval("Invoicenumber") %>'   runat="server" />
                                    </ItemTemplate>

                                </asp:TemplateField>


                                  <asp:BoundField DataField="Credited" HeaderStyle-CssClass="itemalign" ItemStyle-CssClass="itemalign" HeaderText="Credited" SortExpression="Credited" />
                                <asp:BoundField DataField="ClosedCredit" HeaderStyle-CssClass="itemalign" ItemStyle-CssClass="itemalign" HeaderText="Closed with credit" SortExpression="ClosedCredit" />


                                <asp:BoundField DataField="Paymentreference" HeaderStyle-CssClass="itemalign" ItemStyle-CssClass="itemalign" HeaderText="PAY REF" SortExpression="Paymentreference" />
                                <asp:BoundField DataField="Overpayment" DataFormatString="{0:#,0.00}" HeaderStyle-CssClass="itemalign" ItemStyle-CssClass="itemalign" HeaderText="OVER PAID" SortExpression="Overpayment" />
                                <asp:TemplateField ItemStyle-CssClass="itemalign" HeaderStyle-CssClass="itemalign">
                                    <ItemTemplate>

                                        <asp:Button runat="server" CssClass="invoicesDownloadButton button button-table" ID="btnPDFDownload" collectionStatus=' <%# Eval("Collectionstatus") %>'
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
                                        <asp:Button Text="Send" class="button updateInfoButton form-control" runat="server" ID="btnSend" OnClientClick="ProgressBarDisplay();" OnClick="btnSend_Click" Width="128px"/>


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
                                                <asp:Button runat="server" CssClass="invoicesDownloadButton button button-table" OnClientClick="PdfDownloadClick();" OnClick="btnDownload_Click" ID="btnDownload" Text="Download" />
                                            </div>
                                            <div class="mt-3">
                                                <asp:Button runat="server" CssClass="invoicesDownloadButton button button-table" OnClick="btnEmail_Click" ID="btnEmail" Text="Email" />
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

