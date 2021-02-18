<%@ Page Language="C#" MasterPageFile="~/Nordfin.Master" AutoEventWireup="true" Title="NordfinCapital" EnableEventValidation="true" CodeBehind="frmCustomer.aspx.cs" Inherits="Nordfin.frmCustomer" %>


<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ModalWindow" %>
<%@ Register Src="~/ucManualInvoice.ascx" TagPrefix="uc1" TagName="ucManualInvoice" %>
<asp:Content ID="Content1" ContentPlaceHolderID="NordfinContentHolder" runat="server" style="background-color: #232D41;">
    <link href="Styles/Customer.css?version=<%=ConfigurationManager.AppSettings["VersionConfiguration"].ToString() %>" rel="stylesheet" />
    <link href="Styles/ManualInvoice.css?version=<%=ConfigurationManager.AppSettings["VersionConfiguration"].ToString() %>"" rel="stylesheet" />
    <script src="Scripts/jsManualInvoice.js?version=<%=ConfigurationManager.AppSettings["VersionConfiguration"].ToString() %>""></script>
    <script src="Scripts/jsCustomer.js?version=<%=ConfigurationManager.AppSettings["VersionConfiguration"].ToString() %>"></script>
    

    <div class="dashboardContainer">
        <div class="container-fluid">
            <div class="dashboardHeader">
                <div class="dashboardHeadline">
                    Customer
                </div>
                <div style="position: absolute;">
                  
                    <asp:Button Text="Export" ID="btnExport" class="updateInfoButton export" Visible="false" runat="server" OnClick="btnExport_Click" Style="margin-top: -25px; margin-right: 18px; width: 75px;" />
                </div>

            </div>


            <div>
                <div class="row " style="height: 50px; margin-left: 0px;margin-top:20px">


                    <div class="col-md-3 divPadding customerWidth22" style="color: #FFFFFF;">
                        <div style="background-color: #3E4B64;">


                            <div class="divPaddingLeft" style="background-color: #475672">
                                <span class="customerdivHeading">NAME</span>

                                <asp:Label CssClass="customerdivText" Text="" runat="server" ID="lblName"></asp:Label>
                                <hr class="divHrLine" />

                            </div>


                            <div class="divPaddingLeft">
                                <span class="customerdivHeading">CUSTOMER NUMBER</span>

                                <asp:Label CssClass="customerdivText" Text="" runat="server" ID="lblCustomerNumber"></asp:Label>

                            </div>
                            <hr class="divHrLine" />
                            <div class="divPaddingLeft">
                                <span class="customerdivHeading">SSN/REG.NUMBER</span>

                                <asp:Label CssClass="customerdivText" Text="" runat="server" ID="lblPersonNumber"></asp:Label>

                            </div>
                            <hr class="divHrLine" />
                            <div class="divPaddingLeft">
                                <span class="customerdivHeading">ADDRESS</span>

                                <asp:Label CssClass="customerdivText" Text="" runat="server" ID="lblAddress"></asp:Label>

                            </div>
                            <hr class="divHrLine" />
                            <asp:Panel ID="pnlAddress" runat="server">
                                <div class="divPaddingLeft">
                                    <span class="customerdivHeading"></span>

                                    <asp:Label CssClass="customerdivText" Text="" runat="server" ID="lblAddress1"></asp:Label>

                                </div>
                                <hr class="divHrLine" />
                            </asp:Panel>
                            <div class="divPaddingLeft">
                                <span class="customerdivHeading">POSTAL CODE</span>

                                <asp:Label CssClass="customerdivText" Text="" runat="server" ID="lblPostalCode"></asp:Label>

                            </div>
                            <hr class="divHrLine" />
                            <div class="divPaddingLeft">
                                <span class="customerdivHeading">CITY</span>

                                <asp:Label CssClass="customerdivText" Text="" runat="server" ID="lblCity"></asp:Label>

                            </div>
                            <hr class="divHrLine" />
                            <div class="divPaddingLeft">
                                <span class="customerdivHeading">COUNTRY</span>

                                <asp:Label CssClass="customerdivText" Text="" runat="server" ID="lblCountry"></asp:Label>

                            </div>
                            <hr class="divHrLine" />
                            <div class="divPaddingLeft">
                                <span class="customerdivHeading">EMAIL</span>

                                <asp:Label CssClass="customerdivText" Text="" runat="server" ID="lblEmail"></asp:Label>

                            </div>
                            <hr class="divHrLine" />
                            <asp:Panel runat="server" CssClass="divPaddingLeft" ID="pnlPhone">
                                <span class="customerdivHeading">PHONE</span>

                                <asp:Label CssClass="customerdivText" Text="" runat="server" ID="lblPhone"></asp:Label>

                            </asp:Panel>
                            
                            <asp:Panel runat="server" Visible="false" ID="pnlInsuredClient">
                                <hr class="divHrLine" />
                                <div class="divPaddingLeft divcollapse" id="divCollapse" data-toggle="collapse" data-target="#demo" style="cursor: pointer;">
                                    <span class="customerdivHeading">INSURER</span>
                                    <span id="spnPlus" class="plusIcon" style="color: #FFB100; font-weight: bold;">(+)</span>

                                    <span id="spnMinus" class="plusIcon minusIcon" style="color: #FFB100; font-weight: bold;">(-)</span>

                                    <asp:Label CssClass="customerdivText" Text="" runat="server" ID="lblInsurer"></asp:Label>

                                </div>



                                <div id="demo" class="collapse">
                                    <hr class="divHrLine" />
                                    <div class="divPaddingLeft">
                                        <span class="customerdivHeading">REFERENCE</span>

                                        <asp:Label CssClass="customerdivText" Text="" runat="server" ID="lblReference"></asp:Label>

                                    </div>


                                    <hr class="divHrLine" />
                                    <div class="divPaddingLeft">
                                        <span class="customerdivHeading">INSURED AMOUNT</span>

                                        <asp:Label CssClass="customerdivText" Text="" runat="server" ID="lblInsuredAmount"></asp:Label>

                                    </div>


                                    <hr class="divHrLine" />
                                    <div class="divPaddingLeft">
                                        <span class="customerdivHeading">CURRENCY</span>

                                        <asp:Label CssClass="customerdivText" Text="" runat="server" ID="lblCurrency"></asp:Label>

                                    </div>
                                    <hr class="divHrLine" />
                                    <div class="divPaddingLeft" style="padding-bottom: 25px;">
                                        <span class="customerdivHeading">REMAINING</span>

                                        <asp:Label CssClass="customerdivText" Text="" runat="server" ID="lblRemainingIns"></asp:Label>

                                    </div>
                                </div>
                            </asp:Panel>

                        </div>
                        <div class="container-fluid" style="padding-left: 10px; background-color: #475672;">
                            <div class="row">
                                <div class="col-md-6 actionButtonColumn">
                                    <asp:Panel runat="server" ID="pnlUpdate" class="updateInfoButtonContainer">
                                        <asp:Button Text="Update info" class="button panelButton form-control" runat="server" ID="btnUpdateInfo" OnClientClick="return UpdateInfo();" />
                                    </asp:Panel>
                                </div>
                                <div class="col-md-6 actionButtonColumn">
                                    <asp:Panel runat="server" ID="pnlCreditCheck" class="updateInfoButtonContainer">
                                        <asp:Button Text="Credit Check" class="button panelButton form-control" runat="server" ID="btnCreditCheck" OnClientClick="return CreditCheck();" />
                                    </asp:Panel>
                                </div>
                            </div>
                            <div class="row" id="divManualInvoiceRow" style="display: none">
                                <div class="col-md-6 actionButtonColumn" id="divManualInvoice" style="display: none">
                                    <asp:Panel runat="server" ID="pnlManualInvoice" Visible="true" class="updateInfoButtonContainer">
                                        <asp:Button CssClass="button panelButton form-control" ID="btnManualInv" Text="Manual Invoice" runat="server" OnClientClick="return showManualInvoice();" Style="padding-left: 8px;"></asp:Button>
                                    </asp:Panel>
                                </div>
                                <div class="col-md-6 actionButtonColumn" id="divMatch" style="display: none">
                                    <asp:Panel runat="server" ID="pnlMatch" Visible="true" CssClass="updateInfoButtonContainer">
                                        <asp:Button Text="Match Credit" ID="btnInvoice" class="button panelButton form-control" OnClientClick="return InvoiceInfo();" runat="server" />
                                    </asp:Panel>
                                </div>
                            </div>
                            <div class="row" id="divResetRow" style="display: none">
                                <div class="col-lg-6 actionButtonColumn" style="display: none" id="divResetPages">
                                    <asp:Panel runat="server" ID="pnlReset" Visible="true" CssClass="updateInfoButtonContainer">
                                        <asp:Button Text="Reset mypages" class="button panelButton form-control" runat="server" OnClientClick="return RestoreMypage();" Style="padding-left: 10px;" />
                                    </asp:Panel>
                                </div>
                            </div>
                        </div>
                    </div>

                   <div class="col-md-9 table-responsive customerTable tableMarginTop customerWidth78">

                          <asp:UpdatePanel runat="server" ID="UpdatePanel1">
                <ContentTemplate>
                        <asp:GridView ID="grdCustomer" runat="server" EmptyDataRowStyle-CssClass="Emptyrow" AllowSorting="true" OnSorting="grdCustomer_Sorting" AutoGenerateColumns="False" ViewStateMode="Enabled" Visible="true" 
                            Style="color: white; font-size: small; margin-top: -4px;" ShowHeaderWhenEmpty="true" CssClass="table" OnRowDataBound="grdCustomer_OnRowDataBound" SelectedRowStyle-BackColor="#475672">
                            <HeaderStyle BackColor="#475672" />
                            <Columns>
                                <asp:TemplateField ItemStyle-CssClass="labelcolor itemalign" HeaderText="INVOICE" SortExpression="Invoicenumber" HeaderStyle-CssClass="itemalign">
                                    <ItemTemplate>

                                        <asp:LinkButton CssClass="linkcss" Text='<%# Bind("Invoicenumber") %>' overpaymentData='<%# Eval("Overpayment") %>'
                                            remainData='<%# Eval("Remainingamount") %>' invoiceData='<%# Eval("Customernumber") +"|"+ Eval("InvoiceID")%>' OnClientClick="return LinkClick(this);" ID="gridLink" OnClick="gridLink_Click" runat="server" />
                                    </ItemTemplate>

                                </asp:TemplateField>

                                <asp:BoundField DataField="CurrencyCode" HeaderStyle-CssClass="itemalign" ItemStyle-CssClass="itemalign" HeaderText="CURRENCY" SortExpression="CurrencyCode" />
                                <asp:BoundField DataField="Invoiceamount" DataFormatString="{0:#,0.00}" HeaderStyle-CssClass="itemalign" ItemStyle-CssClass="itemalign" HeaderText="AMOUNT" SortExpression="Invoiceamount" />
                                <asp:BoundField DataField="Fees" DataFormatString="{0:#,0.00}" HeaderStyle-CssClass="itemalign" ItemStyle-CssClass="itemalign" HeaderText="FEES" SortExpression="Fees" />

                                <asp:BoundField DataField="Billdate" HeaderStyle-CssClass="itemalign" ItemStyle-CssClass="itemalign tableNoWrap" HeaderText="BILL DATE" SortExpression="Billdate" />
                                <asp:BoundField DataField="Duedate" HeaderStyle-CssClass="itemalign" ItemStyle-CssClass="itemalign tableNoWrap" HeaderText="DUE DATE" SortExpression="Duedate" />
                                <asp:BoundField DataField="Remainingamount" DataFormatString="{0:#,0.00}" HeaderStyle-CssClass="itemalign" ItemStyle-CssClass="itemalign" HeaderText="REMAIN" SortExpression="Remainingamount" />

                                <asp:BoundField DataField="TotalRemaining" DataFormatString="{0:#,0.00}" HeaderStyle-CssClass="itemalign" ItemStyle-CssClass="itemalign" HeaderText="TOTAL REMAIN" SortExpression="TotalRemaining" />
                                <asp:BoundField DataField="Collectionstatus" HeaderStyle-CssClass="itemalign" ItemStyle-CssClass="itemalign" HeaderText="Collection Status" SortExpression="Collectionstatus" />

                                <asp:BoundField DataField="Paymentreference" HeaderStyle-CssClass="itemalign" ItemStyle-CssClass="itemalign" HeaderText="PAY REF" SortExpression="Paymentreference" />
                                <asp:BoundField DataField="Overpayment" DataFormatString="{0:#,0.00}" HeaderStyle-CssClass="itemalign" ItemStyle-CssClass="itemalign" HeaderText="OVER PAID" SortExpression="Overpayment" />
                                <asp:TemplateField ItemStyle-CssClass="itemalign" HeaderStyle-CssClass="itemalign">
                                    <ItemTemplate>


                                        <asp:Button runat="server" CssClass="button button-table downloadButton" ID="btnPDFDownload" combineInvoice='<%# Eval("CombineInvoice") %>' CommandArgument=' <%# Eval("CombineInvoice") %>' OnClick="btnPDFDownload_Click" Text="Download" />
                                    </ItemTemplate>
                                    <HeaderTemplate>
                                        PDF
                                    </HeaderTemplate>
                                </asp:TemplateField>

                            </Columns>
                            <EmptyDataTemplate>No Record Available</EmptyDataTemplate>
                        </asp:GridView>
                    
                    </ContentTemplate>
            </asp:UpdatePanel>

                    </div>
                    <asp:Button ID="btnOpenModal" runat="server" Style="display: none;" />
                    <ModalWindow:ModalPopupExtender ID="mp1" runat="server" PopupControlID="pnlModal" BehaviorID="MPE" TargetControlID="btnOpenModal" CancelControlID="closeButton"></ModalWindow:ModalPopupExtender>
                    <asp:Panel ID="pnlModal" runat="server" CssClass="Popup" align="center" Style="display: none; overflow-x: hidden; overflow-y: hidden;">
                        <iframe id="iframeModal" style="height: 90%; width: 95%; overflow-x: hidden; overflow-y: hidden;" runat="server"></iframe>
                        <asp:Button ID="closeButton" runat="server" Style="display: none;" />
                    </asp:Panel>

                   <div class="col-md-3 customerWidth22">
                    </div>
                    <div class="col-md-9 table-responsive customerNotes customerWidth78">
                        <div class="row totalsum" style="display: none;">


                            <div style="color: #fff">Summary</div>
                            <div></div>
                            <div>
                                <asp:Label runat="server" ID="lblSumAmount"></asp:Label>

                            </div>
                            <div style="padding-left: 0px; margin-left: -40px;">
                                <asp:Label ID="lblFeesAmount" runat="server" />
                            </div>
                            <div></div>
                            <div></div>
                            <div style="margin-left: -45px;">
                                <asp:Label ID="lblTotalRemain" runat="server" />
                            </div>
                            <div>
                                <asp:Label ID="lblOverPaid" runat="server" />
                            </div>
                            <div>
                                <asp:Label ID="lblRemain" runat="server" /></div>
                            <div></div>


                        </div>


                        <div class="row" style="text-align: left; height: 50px; background-color: #2C3850; padding-top: 13px;">
                            <div class="col-md-6">

                                <span class="divTopHeading">NOTES</span>
                            </div>

                        </div>

                        <div>
                            <asp:GridView ID="grdNotes" runat="server" EmptyDataRowStyle-CssClass="Emptyrow" AutoGenerateColumns="False" ViewStateMode="Enabled" Visible="true" Style="color: white; font-size: small;" ShowHeaderWhenEmpty="true" CssClass="table">
                                <HeaderStyle BackColor="#475672" />
                                <Columns>



                                    <asp:BoundField DataField="InvoiceNumber" HeaderStyle-CssClass="Notesalign" ItemStyle-CssClass="Notesalign" HeaderText="INVOICE" SortExpression="InvoiceNumber" />
                                    <asp:BoundField DataField="NoteType" HeaderStyle-CssClass="Notesalign" ItemStyle-CssClass="Notesalign" HeaderText="TYPE" SortExpression="NoteType" />
                                    <asp:BoundField DataField="NoteDate" HeaderStyle-CssClass="Notesalign" ItemStyle-CssClass="Notesalign" HeaderText="DATE" SortExpression="NoteDate" />

                                    <asp:BoundField DataField="UserName" HeaderStyle-CssClass="Notesalign" ItemStyle-CssClass="Notesalign" HeaderText="USER" SortExpression="UserName" />
                                    <asp:BoundField DataField="NoteText" HeaderStyle-CssClass="Notesalign" ItemStyle-CssClass="Notesalign" HeaderText="TEXT" SortExpression="NoteText" />
                                </Columns>
                                <EmptyDataTemplate>No Record Available</EmptyDataTemplate>
                            </asp:GridView>
                        </div>
                    </div>

                </div>
            </div>


            <asp:HiddenField ID="hdnClientName" runat="server" />
            <asp:HiddenField ID="hdnFileName" runat="server" />
            <asp:HiddenField ID="hdnArchiveLink" runat="server" />
            <a id="pdfViewer" href="" runat="server" target="_blank"></a>

        </div>


        <div class="modal fade" id="mdlUpdateConfirm" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
            <div class="modal-dialog" role="document">
                <div class="modal-content" style="background: none; border: none; width: 125%; top: 125px;">
                    <div class="modal-header dashboardHeadline" style="background-color: #323e53; color: #fff; font-size: 16px;">
                        <h5 class="modal-title" id="exampleModalLabel">INVOICE INFO</h5>
                        <div class="modalcloseButton" id="pnlClose" aria-label="Close" style="top: 35px; right: 20px; cursor: pointer;">
                            ✕
                        </div>
                    </div>
                    <div class="modal-body" style="background-color: #323e53; color: #fff;">
                        <div>
                            <span id="spnBody" style="color: #A9BFD5;">

                                <asp:GridView ID="grdInvoiceRemaining" runat="server" EmptyDataRowStyle-CssClass="Emptyrow" AutoGenerateColumns="False" ViewStateMode="Enabled" Visible="true" Style="color: white; font-size: small;" ShowHeaderWhenEmpty="true" CssClass="table">
                                    <HeaderStyle BackColor="#475672" />
                                    <Columns>


                                        <%--<asp:TemplateField ItemStyle-CssClass="itemalign" HeaderStyle-CssClass="itemalign">
                                            <ItemTemplate>


                                                <asp:CheckBox ID="cbSelect" Style="margin-top: 5px;" CssClass="checkbox"
                                                    runat="server"></asp:CheckBox>
                                            </ItemTemplate>
                                            <HeaderTemplate>
                                                Select
                                            </HeaderTemplate>
                                        </asp:TemplateField>--%>
                                        <asp:BoundField DataField="InvoiceID" HeaderStyle-CssClass="Notesalign GridInvisible" ItemStyle-CssClass="Notesalign GridInvisible" />
                                        <asp:BoundField DataField="InvoiceNumber" HeaderStyle-CssClass="Notesalign" ItemStyle-CssClass="Notesalign" HeaderText="INVOICE" SortExpression="InvoiceNumber" />
                                        <asp:BoundField DataField="CurrencyCode" HeaderStyle-CssClass="itemalign" ItemStyle-CssClass="itemalign" HeaderText="CURRENCY" SortExpression="CurrencyCode" />
                                        <asp:BoundField DataField="Invoiceamount" HeaderStyle-CssClass="itemalign" ItemStyle-CssClass="itemalign" HeaderText="AMOUNT" SortExpression="Invoiceamount" />
                                        <asp:BoundField DataField="Billdate" HeaderStyle-CssClass="itemalign" ItemStyle-CssClass="itemalign tableNoWrap" HeaderText="BILL DATE" SortExpression="Billdate" />
                                        <asp:BoundField DataField="Duedate" HeaderStyle-CssClass="itemalign" ItemStyle-CssClass="itemalign tableNoWrap" HeaderText="DUE DATE" SortExpression="Duedate" />
                                        <asp:BoundField DataField="Remainingamount" HeaderStyle-CssClass="itemalign" ItemStyle-CssClass="itemalign" HeaderText="REMAIN" SortExpression="Remainingamount" />

                                    </Columns>
                                    <EmptyDataTemplate>No Record Available</EmptyDataTemplate>
                                </asp:GridView>

                            </span>
                        </div>
                    </div>
                    <div class="modal-footer" style="background-color: #323E53; padding: 0px;">

                        <asp:Button ID="btnYes" runat="server" CssClass="modalbutton" OnClientClick="return ShowPopup();" Text="Match" />
                    </div>
                </div>
            </div>
        </div>


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



    <div class="modal fade" id="mdlDeleteConfirm" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
        <div class="modal-dialog" role="document">
            <div class="modal-content" style="background: none; border: none; width: 85%;">
                <div class="modal-header dashboardHeadline" style="background-color: #323e53; color: #fff; font-size: 16px;">
                    <h5 class="modal-title" id="info">Information</h5>
                    <button type="button" class="modalcloseButton" data-dismiss="modal" aria-label="Close" style="top: 30px; right: 0px;">
                        <span aria-hidden="true">✕</span>
                    </button>
                </div>
                <div class="modal-body" style="background-color: #323e53; color: #fff;">
                    <div>
                        <span id="msgText" style="color: #A9BFD5; text-transform: uppercase; font-size: 12px;">Are you sure you want to reset the customers My pages login? </span>
                    </div>
                </div>


                <div class="modal-footer" style="background-color: #323E53; padding: 0px;">
                    <asp:Button runat="server" CssClass="modalbutton" ID="btnOk" Text="Yes" OnClick="btnOk_Click"></asp:Button>
                    <button type="button" class="modalbutton" id="btnNo" data-dismiss="modal">No</button>

                </div>

            </div>
        </div>
    </div>




    <div class="modal fade" id="mdlAccessInfo" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
        <div class="modal-dialog" role="document">
            <div class="modal-content" style="top: -10px; background: none; border: none;">
                <div class="modal-header dashboardHeadline" style="background-color: #323e53; color: #fff; font-size: 16px;">
                    <h5 class="modal-title modalTextcolor dashboardHeadlineModal" id="informModalLabel">UPDATE INFO</h5>
                    <button type="button" class="modalcloseButton" data-dismiss="modal" aria-label="Close" style="top: 35px; right: 20px;">
                        <span aria-hidden="true">✕</span>
                    </button>
                </div>
                <div class="modal-body" style="background-color: #323e53; color: #fff;">
                    <div>
                        <label>
                            <asp:Label Style="color: #A9BFD5; text-transform: uppercase; font-size: 12px;" runat="server" ID="spnCustomerName" Text="Customer Name"></asp:Label>

                            <asp:TextBox ID="txtCustomerName" runat="server" autocomplete="off" CssClass="form-control textboxModalColor"></asp:TextBox>
                        </label>



                    </div>
                    <div style="margin-top: 12px;">
                        <label>
                            <asp:Label Style="color: #A9BFD5; text-transform: uppercase; font-size: 12px;" ID="spnAddress1" Text="Address1" runat="server"></asp:Label>

                            <asp:TextBox ID="txtAddress1" runat="server" autocomplete="off" CssClass="form-control textboxModalColor"></asp:TextBox>
                        </label>


                    </div>

                    <div style="margin-top: 12px;">
                        <label>
                            <asp:Label Style="color: #A9BFD5; text-transform: uppercase; font-size: 12px;" runat="server" Text="Address2" ID="spnAddress2"> </asp:Label>

                            <asp:TextBox ID="txtAddress2" runat="server" autocomplete="off" CssClass="form-control textboxModalColor"></asp:TextBox>
                        </label>


                    </div>

                    <div style="margin-top: 12px;">
                        <label>
                            <asp:Label Style="color: #A9BFD5; text-transform: uppercase; font-size: 12px;" runat="server" Text="Postal Code" ID="spnPostalCode"> </asp:Label>

                            <asp:TextBox ID="txtPostalCode" runat="server" autocomplete="off" CssClass="form-control textboxModalColor"></asp:TextBox>
                        </label>


                    </div>

                    <div style="margin-top: 12px;">
                        <label>
                            <asp:Label Style="color: #A9BFD5; text-transform: uppercase; font-size: 12px;" runat="server" ID="spnCity" Text="City"></asp:Label>

                            <asp:TextBox ID="txtCity" runat="server" autocomplete="off" CssClass="form-control textboxModalColor"></asp:TextBox>
                        </label>


                    </div>
                    <div style="margin-top: 12px;">
                        <label>
                            <asp:Label Style="color: #A9BFD5; text-transform: uppercase; font-size: 12px;" runat="server" ID="spnCountry" Text="Country"> </asp:Label>

                            <asp:TextBox ID="txtCountry" runat="server" autocomplete="off" CssClass="form-control textboxModalColor"></asp:TextBox>
                        </label>


                    </div>
                    <div style="margin-top: 12px;">
                        <label>
                            <asp:Label Style="color: #A9BFD5; text-transform: uppercase; font-size: 12px;" runat="server" ID="spnModalEmail" Text="Email"> </asp:Label>

                            <asp:TextBox ID="txtEmail" runat="server" autocomplete="off" CssClass="form-control textboxModalColor"></asp:TextBox>
                        </label>


                    </div>

                    <div style="margin-top: 12px;">
                        <label>
                            <asp:Label Style="color: #A9BFD5; text-transform: uppercase; font-size: 12px;" runat="server" ID="spnPhonenumber" Text="Phone Number"> </asp:Label>

                            <asp:TextBox ID="txtPhoneNumber" runat="server" autocomplete="off" CssClass="form-control textboxModalColor" onkeypress="return AllowNumbersPlus(this);"></asp:TextBox>
                        </label>


                    </div>

                </div>
                <div class="modal-footer" style="background-color: #323E53; padding: 0px; margin-top: -15px;">
                    <asp:Button runat="server" class="modalbutton" ID="btnSubmit" Text="Submit" OnClick="btnSubmit_Click"></asp:Button>

                </div>



            </div>
        </div>
    </div>



    <div class="modal fade" id="mdlExport" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
        <div class="modal-dialog" role="document">
            <div class="modal-content" style="top: 200px; background: none; border: none; width: 85%;">
                <div class="modal-header dashboardHeadline" style="background-color: #323e53; color: #fff; font-size: 16px;">
                    <h5 class="modal-title" id="hdnInfo">Export</h5>
                    <button type="button" class="modalcloseButton" data-dismiss="modal" aria-label="Close" style="top: 30px; right: 0px;">
                        <span aria-hidden="true">✕</span>
                    </button>
                </div>
                <div class="modal-body" style="background-color: #323e53; color: #fff; text-align: center">
                    <asp:Button Text="Export report" ID="btnExportreport" CssClass="modalExportbutton" OnClick="btnExport_Click" runat="server" Style="width: 40%;" />

                    <asp:Button Text="Export Detail" ID="btnExportDetail" CssClass="modalExportbutton" OnClick="btnExportDetail_Click" runat="server" Style="width: 40%;" />

                </div>



            </div>
        </div>
    </div>





    <div class="modal fade" id="mdlPopConfirm" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
        <div class="modal-dialog" role="document">
            <div class="modal-content" style="background: none; border: none; width: 85%; margin-left: 100px;">
                <div class="modal-header dashboardHeadline" style="background-color: #3a475d; color: #fff; font-size: 16px;">
                    <h5 class="modal-title" id="hinfo">Information</h5>
                    <button type="button" class="modalcloseButton" data-dismiss="modal" aria-label="Close" style="top: 30px; right: 0px;">
                        <span aria-hidden="true">✕</span>
                    </button>
                </div>
                <div class="modal-body" style="background-color: #3a475d; color: #fff;">
                    <div>
                        <span id="spntext" style="color: #A9BFD5; text-transform: uppercase; font-size: 12px;">Match credit XX with invoice YY? </span>
                    </div>
                </div>

                <div class="modal-footer" style="background-color: #3a475d; padding: 0px;">
                    <asp:Button runat="server" CssClass="modalbutton" ID="btnMatchOk" Text="Ok" OnClientClick="return Match();"></asp:Button>
                    <button type="button" class="modalbutton" id="btnCancel" data-dismiss="modal">Cancel</button>

                </div>

            </div>
        </div>
    </div>

    <div class="modal manulInvoiceModal" id="mdlManualInvoice" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true" runat="server">
        <div class="modal-dialog" role="document" style="overflow-y: initial !important;">
            <div class="modal-content" style="height: 100%; width: 160%; background: none; border: none">
                <div class="modal-body" style="overflow-y: scroll; background-color: #323e53 !important">
                    <asp:UpdatePanel runat="server" ID="pnlManualInv" UpdateMode="Conditional">
                        <ContentTemplate>
                            <uc1:ucManualInvoice runat="server" ID="ucManualInvoice" />
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </div>
        </div>
    </div>

    <asp:HiddenField ID="hdnInvoiceNumber" runat="server" />
    <asp:HiddenField ID="hdnEmailID" runat="server" />
    <asp:HiddenField ID="hdnMatch" runat="server" Value="true" />
     <asp:HiddenField ID="hdnAdmin" runat="server" Value="true" />
    <asp:TextBox ID="txtCustomerID" runat="server" autocomplete="off" Visible="false"></asp:TextBox>
</asp:Content>
