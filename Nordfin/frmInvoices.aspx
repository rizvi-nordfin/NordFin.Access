<%@ Page Language="C#" MasterPageFile="~/Nordfin.Master" AutoEventWireup="true" Title="NordfinCapital" CodeBehind="frmInvoices.aspx.cs" Inherits="Nordfin.frmInvoices"
    UICulture="sv-SE" Culture="sv-SE" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ModalWindow" %>
<asp:Content ID="Content1" ContentPlaceHolderID="NordfinContentHolder" runat="server" style="padding: 5px;">

    <script src="Scripts/jsInvoices.js?version=<%=ConfigurationManager.AppSettings["VersionConfiguration"].ToString() %>"></script>

    <link href="Styles/Invoices.css?version=<%=ConfigurationManager.AppSettings["VersionConfiguration"].ToString() %>" rel="stylesheet" />



    <div class="dashboardContainer">
        <div class="container-fluid">

            <div class="row dashboardHeader">
                <div class="col-lg-3 dashboardHeadline">Invoices: Overview   
                    <asp:Button ID="btnExport" Text="Export" CssClass="export" Style="width: 75px;display:none;" OnClick="btnExport_Click" runat="server" /></div>

               

         

                 <div class="col-lg-9">

                 <div class="summaryInvoices">

                     <div  class="summaryHeading"> 
                         <div class="textOrange" style="background: #38445D; ">
                                    Amount
                                    &nbsp; 
                                    <asp:Label runat="server" CssClass="textWhite" ID="lblSumAmount"></asp:Label>
                                </div></div>
                        <div  class="summaryHeading" > 
                             <div class="textOrange" style="background: #38445D;">
                                    FEES
                                      &nbsp;&nbsp;
                                    <asp:Label runat="server" CssClass="textWhite" ID="lblFeesAmount"></asp:Label>
                                </div></div>
                        <div  class="summaryHeading"> 
                              <div class="textOrange" style="background: #38445D;">
                                    REMAIN
                                      &nbsp;&nbsp;
                                    <asp:Label ID="lblRemain" CssClass="textWhite" runat="server" />
                                </div></div>
                         <div  class="summaryHeading"> 
                              <div class="textOrange" style="background: #38445D;">
                                    TOTAL REMAIN
                                      &nbsp;&nbsp;
                                    <asp:Label ID="lblTotalRemain" CssClass="textWhite" runat="server" />
                                </div></div>
                        <div class="summaryHeading"> 
                              <div class="textOrange" style="background: #38445D;">
                                    OVERPAID
                                      &nbsp;&nbsp;
                                   <asp:Label ID="lblOverPaid" CssClass="textWhite" runat="server" />
                                </div></div>

                </div>  
                     </div>

                <%-- <div class="col-lg-8     dashboardSummary">
                    <div class="row">

                        <div class="summarycolor col-lg-3 row">
                           
                            <div>
                            <span class="summaryHead">AMOUNT</span>
                           
                                </div>
                        </div>



                        <div class="summarycolor col-lg-2 row">
                           
                            <div>
                            <span class="summaryHead">FEES</span>
                           
                                </div>
                        </div>


                      <div class="summarycolor col-lg-3 row">
                           
                            <div>
                            <span class="summaryHead"> TOTAL REMAIN</span>
                            
                                </div>
                        </div>
                       
                        
                      <div class="summarycolor col-lg-3 row">
                           
                            <div>
                            <span class="summaryHead"> OVERPAID</span>
                          
                                </div>
                        </div>
                        
                    </div>
                    
                    
                </div>--%>
                <div class="hidden">
                    <asp:Button Text="Download As" class="form-control invoicesDownloadAsButton" runat="server" />
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
            <div class="tableFixHead tableMarginBg" >
                <asp:GridView ID="grdInvoices" runat="server" AutoGenerateColumns="False" AllowSorting="true" OnSorting="grdInvoices_Sorting" EmptyDataRowStyle-CssClass="Emptyrow" ViewStateMode="Enabled" Visible="true" Style="                        color: white;
                        overflow-x: scroll;
                        overflow-y: scroll;
                " ShowHeaderWhenEmpty="true" CssClass="table table-borderless">
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

                        <asp:BoundField DataField="Collectionstatus" HeaderStyle-CssClass="itemalign" ItemStyle-CssClass="itemalign" HeaderText="Collection Status" SortExpression="Collectionstatus" />

                        <asp:BoundField DataField="Paymentreference" HeaderStyle-CssClass="itemalign" ItemStyle-CssClass="itemalign" HeaderText="PAY REF" SortExpression="Paymentreference" />
                        <asp:BoundField DataField="Overpayment" DataFormatString="{0:#,0.00}" HeaderStyle-CssClass="itemalign" ItemStyle-CssClass="itemalign" HeaderText="OVER PAID" SortExpression="Overpayment" />
                        <asp:TemplateField ItemStyle-CssClass="itemalign" HeaderStyle-CssClass="itemalign">
                            <ItemTemplate>

                                <asp:Button runat="server" CssClass="invoicesDownloadButton button button-table" ID="btnPDFDownload" combineInvoice='<%# Eval("CombineInvoice") %>' CommandArgument=' <%# Eval("CombineInvoice") %>' OnClick="btnPDFDownload_Click" Text="Download" />
                            </ItemTemplate>
                            <HeaderTemplate>
                                PDF
                            </HeaderTemplate>
                        </asp:TemplateField>

                        <%-- <asp:TemplateField ItemStyle-CssClass="itemalign" HeaderStyle-CssClass="itemalign">
                            <ItemTemplate>

                                <asp:Button runat="server" CssClass="invoicesDownloadButton button button-table" ID="btnEmail" download="0" custInvoice=' <%# Eval("Customernumber") %>' combineInvoice='<%# Eval("CombineInvoice") %>'  OnClientClick="return Email(this);" Text="Email" />
                            </ItemTemplate>
                            <HeaderTemplate>
                                MAIL
                            </HeaderTemplate>
                        </asp:TemplateField>--%>
                    </Columns>
                    <EmptyDataTemplate>No Record Available</EmptyDataTemplate>
                </asp:GridView>
            </div>


            <div class="modal fade" id="mdlUpdateInfo" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
                <div class="modal-dialog" role="document">
                    <div class="modal-content" style="top: 200px; background: none; border: none;">
                        <div class="modal-header dashboardHeadline" style="background-color: #323e53; color: #fff; font-size: 16px;">
                            <h5 class="modal-title modalTextcolor dashboardHeadlineModal" id="updateInfoModalLabel">Email</h5>
                            <button type="button" class="modalcloseButton" data-dismiss="modal" aria-label="Close" style="top: 35px; right: 20px;">
                                <span aria-hidden="true">✕</span>
                            </button>
                        </div>
                        <div class="modal-body" style="background-color: #323e53; color: #fff;">


                            <div id="pnlEmail">
                                <label>


                                    <asp:TextBox ID="txtCustEmail" runat="server" autocomplete="off" CssClass="form-control textboxModalColor" onkeyup='checkEmail(this)'></asp:TextBox>
                                </label>

                                <span id="EmailValid" class="short">
                                    <span id="spnEmail" class="hide" style="color: #f83030">Enter a valid email</span>
                                </span>

                            </div>





                        </div>
                        <div class="modal-footer" style="background-color: #323E53; padding: 5px;">
                            <%--<asp:Button Text="Send" class="button updateInfoButton form-control" runat="server" ID="btnEmail" OnClick="btnEmail_Click"  Width="128px" />--%>
                        </div>



                    </div>
                </div>
            </div>
            <asp:HiddenField ID="hdnClientName" runat="server" />
            <asp:HiddenField ID="hdnFileName" runat="server" />
            <asp:HiddenField ID="hdnArchiveLink" runat="server" />
            <asp:HiddenField ID="hdnInvoiceNumber" runat="server" />
            <asp:HiddenField ID="hdnEmailID" runat="server" />
            <a id="pdfViewer" href="" runat="server" target="_blank"></a>
        </div>

    </div>
</asp:Content>

