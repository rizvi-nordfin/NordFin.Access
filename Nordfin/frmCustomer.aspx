<%@ Page Language="C#" MasterPageFile="~/Nordfin.Master" AutoEventWireup="true" Title="NordfinCapital" CodeBehind="frmCustomer.aspx.cs" Inherits="Nordfin.frmCustomer" %>


<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ModalWindow" %>
<asp:Content ID="Content1" ContentPlaceHolderID="NordfinContentHolder" runat="server" style="background-color: #232D41;">
    <link href="Styles/Customer.css?version=<%=ConfigurationManager.AppSettings["VersionConfiguration"].ToString() %>" rel="stylesheet" />

    <script src="Scripts/jsCustomer.js?version=<%=ConfigurationManager.AppSettings["VersionConfiguration"].ToString() %>"></script>


    <div class="dashboardContainer">
        <div class="container-fluid">
            <div class="dashboardHeader">
                <div class="dashboardHeadline">Customer</div>
                <div style="position:absolute;">
                 <asp:Button Text="Match Credit" id="btnInvoice" class="updateInfoButton export"  OnClientClick="return InvoiceInfo();"  runat="server"  Style="margin-top: -20px; margin-right: 100px;display:none;" />
                    <asp:Button Text="Export" id="btnExport" class="updateInfoButton export" Visible="false"   runat="server" OnClick="btnExport_Click"  Style="margin-top: -25px;    margin-right: 18px;   width: 75px;" />
                    </div>
                <%--<div>OnClientClick="return InvoiceInfo();"--%>
               
                <%--OnClientClick="return ExportExcel();"--%>
                  <%--<asp:Button Text="Export Detail" id="btnExportDetail" class="updateInfoButton export" OnClick="btnExportDetail_Click"  runat="server"  Style="margin-top: -25px; margin-right: 10px;" />--%>
                </div>
            </div>
        <div></div>
 
            <div>
                <div class="row " style="height: 50px; margin-left: 0px;">


                    <div class="col-md-2 divPadding customerInfoSidebar" style="color: #FFFFFF;">
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
                        <asp:Panel runat="server" ID="pnlUpdate" class="updateInfoButtonContainer" style="height:70px;">
                            <asp:Button Text="Update info" class="button updateInfoButton form-control" runat="server" ID="btnUpdateInfo" OnClientClick="return UpdateInfo();" Width="150px" />
                           
                          
                        </asp:Panel>

                          <asp:Panel runat="server" ID="pnlCreditCheck" class="updateInfoButtonContainer" style="height:70px;">
                            <asp:Button Text="Credit Check" class="button updateInfoButton form-control" runat="server" ID="btnCreditCheck" OnClientClick="return CreditCheck();" Width="150px" />
                           
                          
                        </asp:Panel>
                          <asp:Panel runat="server" ID="pnlReset" Visible="false" CssClass="updateInfoButtonContainer">
                            <asp:Button Text="Reset mypages" class="button updateInfoButton form-control" runat="server" OnClientClick="return RestoreMypage();" Width="150px" />
                           
                          
                        </asp:Panel>




                    </div>

                    <asp:Button ID="btnOpenModal" runat="server" Style="display: none;" />
                    <ModalWindow:ModalPopupExtender ID="mp1" runat="server" PopupControlID="pnlModal" BehaviorID="MPE" TargetControlID="btnOpenModal"
                        CancelControlID="closeButton">
                    </ModalWindow:ModalPopupExtender>
                    <asp:Panel ID="pnlModal" runat="server" CssClass="Popup" align="center" Style="display: none; overflow-x: hidden; overflow-y: hidden;">
                        <iframe id="iframeModal" style="height: 90%; width: 95%; overflow-x: hidden; overflow-y: hidden;" runat="server"></iframe>

                        <asp:Button ID="closeButton" runat="server" Style="display: none;" />
                    </asp:Panel>



                    <div class="col-md-10 tableFixHead table-responsive customerTable tableMarginTop" style="background-color: #2C3850;">


                        <asp:GridView ID="grdCustomer" runat="server" EmptyDataRowStyle-CssClass="Emptyrow" AutoGenerateColumns="False" ViewStateMode="Enabled" Visible="true" Style="color: white; font-size: small;    margin-top: -4px;" ShowHeaderWhenEmpty="true" CssClass="table">
                            <HeaderStyle BackColor="#475672" />
                            <Columns>
                                <asp:TemplateField ItemStyle-CssClass="labelcolor itemalign" HeaderStyle-CssClass="itemalign">
                                    <ItemTemplate>

                                        <asp:LinkButton CssClass="linkcss" Text='<%# Bind("Invoicenumber") %>' overpaymentData='<%# Eval("Overpayment") %>'
                                            remainData='<%# Eval("Remainingamount") %>' invoiceData='<%# Eval("Customernumber") +"|"+ Eval("InvoiceID")%>' OnClientClick="return LinkClick(this);" ID="gridLink" OnClick="gridLink_Click" runat="server" />
                                    </ItemTemplate>
                                    <HeaderTemplate>
                                        INVOICE
                                    </HeaderTemplate>
                                </asp:TemplateField>

                                <asp:BoundField DataField="CurrencyCode" HeaderStyle-CssClass="itemalign" ItemStyle-CssClass="itemalign" HeaderText="CURRENCY" SortExpression="CurrencyCode" />
                                <asp:BoundField DataField="Invoiceamount" HeaderStyle-CssClass="itemalign" ItemStyle-CssClass="itemalign" HeaderText="AMOUNT" SortExpression="Invoiceamount" />
                                <asp:BoundField DataField="Fees" HeaderStyle-CssClass="itemalign" ItemStyle-CssClass="itemalign" HeaderText="FEES" SortExpression="Fees" />

                                <asp:BoundField DataField="Billdate" HeaderStyle-CssClass="itemalign" ItemStyle-CssClass="itemalign tableNoWrap" HeaderText="BILL DATE" SortExpression="Billdate" />
                                <asp:BoundField DataField="Duedate" HeaderStyle-CssClass="itemalign" ItemStyle-CssClass="itemalign tableNoWrap" HeaderText="DUE DATE" SortExpression="Duedate" />
                                <asp:BoundField DataField="Remainingamount" HeaderStyle-CssClass="itemalign" ItemStyle-CssClass="itemalign" HeaderText="REMAIN" SortExpression="Remainingamount" />

                                <asp:BoundField DataField="TotalRemaining" HeaderStyle-CssClass="itemalign" ItemStyle-CssClass="itemalign" HeaderText="TOTAL REMAIN" SortExpression="TotalRemaining" />
                                  <asp:BoundField DataField="Collectionstatus" HeaderStyle-CssClass="itemalign" ItemStyle-CssClass="itemalign" HeaderText="Collection Status" SortExpression="Collectionstatus" />

                                <asp:BoundField DataField="Paymentreference" HeaderStyle-CssClass="itemalign" ItemStyle-CssClass="itemalign" HeaderText="PAY REF" SortExpression="Paymentreference" />
                                <asp:BoundField DataField="Overpayment" HeaderStyle-CssClass="itemalign" ItemStyle-CssClass="itemalign" HeaderText="OVER PAID" SortExpression="Overpayment" />
                                <asp:TemplateField ItemStyle-CssClass="itemalign" HeaderStyle-CssClass="itemalign">
                                    <ItemTemplate>


                                        <asp:Button runat="server" CssClass="button button-table downloadButton" ID="btnPDFDownload" combineInvoice='<%# Eval("CombineInvoice") %>' CommandArgument=' <%# Eval("CombineInvoice") %>' OnClick="btnPDFDownload_Click" Text="Download" />
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

                    <div class="col-md-2">
                    </div>
                    <div class="col-md-10 table-responsive customerNotes">
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
                            <div> <asp:Label ID="lblRemain" runat="server" /></div>
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


        <div class="modal fade" id="mdlUpdateConfirm"  tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
            <div class="modal-dialog" role="document">
                <div class="modal-content" style="background: none; border: none; width: 125%;top:125px;">
                    <div class="modal-header dashboardHeadline" style="background-color: #323e53; color: #fff; font-size: 16px;">
                        <h5 class="modal-title" id="exampleModalLabel">INVOICE INFO</h5>
                        <div  class="modalcloseButton" id="pnlClose" aria-label="Close" style="top: 35px; right: 20px;cursor:pointer;">
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
                                         <asp:BoundField DataField="InvoiceID" HeaderStyle-CssClass="Notesalign GridInvisible" ItemStyle-CssClass="Notesalign GridInvisible"   />
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

                          <asp:Button ID="btnYes" runat="server" CssClass="modalbutton"  OnClientClick="return ShowPopup();" Text="Match" />
                    </div>
                </div>
            </div>
        </div>


        <div class="modal fade" id="mdlUpdateInfo" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
                        <div class="modal-dialog" role="document">
                            <div class="modal-content" style="top: 200px;background: none;border:none;">
                                <div class="modal-header dashboardHeadline" style="background-color: #323e53; color: #fff;font-size:16px;">
                                    <h5 class="modal-title modalTextcolor dashboardHeadlineModal" id="updateInfoModalLabel">Email</h5>
                                     <button type="button" class="modalcloseButton" data-dismiss="modal" aria-label="Close" style="top: 35px;right: 20px;">
                                        <span aria-hidden="true">✕</span>
                                    </button>
                                </div>
                                <div class="modal-body" style="background-color: #323e53; color: #fff;">
                                 

                                     <div id="pnlEmail">
                                        <label>
                                          
                                           
                        <asp:TextBox ID="txtCustEmail" runat="server" autocomplete="off" CssClass="form-control textboxModalColor" onkeyup='checkEmail(this)' ></asp:TextBox>
                                        </label>

                                        <span id="EmailValid" class="short">
                                            <span id="spnEmail" class="hide" style="color:#f83030">Enter a valid email</span>
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
                    <div class="modal-content" style="background: none;border:none;width: 85%;">
                        <div class="modal-header dashboardHeadline" style="background-color: #323e53; color: #fff;font-size:16px;">
                            <h5 class="modal-title" id="info">Information</h5>
                            <button type="button" class="modalcloseButton" data-dismiss="modal" aria-label="Close" style="top: 30px;right: 0px;">
                                        <span aria-hidden="true">✕</span>
                                    </button>
                        </div>
                        <div class="modal-body" style="background-color: #323e53; color: #fff;">
                            <div>
                                <span id="msgText" style="color: #A9BFD5;text-transform: uppercase;font-size:12px;"> Are you sure you want to reset the customers My pages login? </span>
                            </div>
                        </div>

                           <div class="modal-footer" style="background-color: #323E53;padding:0px;">
                               <asp:Button runat="server" CssClass="modalbutton" id="btnOk"  Text="Yes" OnClick="btnOk_Click"></asp:Button>
                            <button type="button" class="modalbutton" id="btnNo" data-dismiss="modal">No</button>
                            
                            </div>
                        
                    </div>
                </div>
            </div>




        <div class="modal fade" id="mdlAccessInfo" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
                        <div class="modal-dialog" role="document">
                            <div class="modal-content" style="                                    top: -50px;
                                    background: none;
                                    border: none;">
                                <div class="modal-header dashboardHeadline" style="background-color: #323e53; color: #fff;font-size:16px;">
                                    <h5 class="modal-title modalTextcolor dashboardHeadlineModal" id="informModalLabel">UPDATE INFO</h5>
                                     <button type="button" class="modalcloseButton" data-dismiss="modal" aria-label="Close" style="top: 35px;right: 20px;">
                                        <span aria-hidden="true">✕</span>
                                    </button>
                                </div>
                                <div class="modal-body" style="background-color: #323e53; color: #fff;">
                                    <div>
                                        <label>
                                             <asp:Label style="color: #A9BFD5;text-transform: uppercase;font-size:12px;" runat="server" ID="spnCustomerName" Text="Customer Name"></asp:Label>
                                           
                        <asp:TextBox ID="txtCustomerName" runat="server" autocomplete="off" CssClass="form-control textboxModalColor"></asp:TextBox>
                                        </label>

                                     

                                    </div>
                                    <div style="margin-top: 12px;">
                                        <label>
                                              <asp:Label style="color: #A9BFD5;    text-transform: uppercase;font-size:12px;" ID="spnAddress1" Text="Address1" runat="server"></asp:Label>
                                          
                        <asp:TextBox ID="txtAddress1" runat="server" autocomplete="off" CssClass="form-control textboxModalColor" ></asp:TextBox>
                                        </label>

                                      
                                    </div>

                                    <div style="margin-top: 12px;">
                                        <label>
                                              <asp:Label style="color: #A9BFD5;    text-transform: uppercase;font-size:12px;" runat="server" Text="Address2" ID="spnAddress2"> </asp:Label>
                                          
                        <asp:TextBox ID="txtAddress2" runat="server" autocomplete="off" CssClass="form-control textboxModalColor"></asp:TextBox>
                                        </label>

                                   
                                    </div>

                                      <div style="margin-top: 12px;">
                                        <label>
                                              <asp:Label style="color: #A9BFD5;    text-transform: uppercase;font-size:12px;" runat="server" Text="Postal Code" ID="spnPostalCode"> </asp:Label>
                                          
                        <asp:TextBox ID="txtPostalCode" runat="server" autocomplete="off" CssClass="form-control textboxModalColor" ></asp:TextBox>
                                        </label>

                                   
                                    </div>

                                        <div style="margin-top: 12px;">
                                        <label>
                                              <asp:Label style="color: #A9BFD5;    text-transform: uppercase;font-size:12px;" runat="server" ID="spnCity" Text="City"></asp:Label>
                                          
                        <asp:TextBox ID="txtCity" runat="server" autocomplete="off" CssClass="form-control textboxModalColor" ></asp:TextBox>
                                        </label>

                                   
                                    </div>
                                        <div style="margin-top: 12px;">
                                        <label>
                                              <asp:Label style="color: #A9BFD5;    text-transform: uppercase;font-size:12px;" runat="server" ID="spnCountry" Text="Country"> </asp:Label>
                                          
                        <asp:TextBox ID="txtCountry" runat="server" autocomplete="off" CssClass="form-control textboxModalColor" ></asp:TextBox>
                                        </label>

                                   
                                    </div>
                                     <div style="margin-top: 12px;">
                                        <label>
                                              <asp:Label style="color: #A9BFD5;    text-transform: uppercase;font-size:12px;" runat="server" ID="spnModalEmail" Text="Email"> </asp:Label>
                                          
                        <asp:TextBox ID="txtEmail" runat="server" autocomplete="off" CssClass="form-control textboxModalColor" ></asp:TextBox>
                                        </label>

                                   
                                    </div>

                                     <div style="margin-top: 12px;">
                                        <label>
                                              <asp:Label style="color: #A9BFD5;    text-transform: uppercase;font-size:12px;" runat="server" ID="spnPhonenumber" Text="Phone Number"> </asp:Label>
                                          
                        <asp:TextBox ID="txtPhoneNumber" runat="server" autocomplete="off" CssClass="form-control textboxModalColor" onkeypress="return AllowNumbersPlus(this);"></asp:TextBox>
                                        </label>

                                   
                                    </div>

                                </div>
                                <div class="modal-footer" style="background-color: #323E53; padding: 0px;margin-top:-15px;">
                                    <asp:Button runat="server" class="modalbutton" ID="btnSubmit" Text="Submit" OnClick="btnSubmit_Click"></asp:Button>

                                </div>

                             

                            </div>
                        </div>
                    </div>



        <div class="modal fade" id="mdlExport" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
                <div class="modal-dialog" role="document">
                    <div class="modal-content" style="top:200px; background: none;border:none;width: 85%;">
                        <div class="modal-header dashboardHeadline" style="background-color: #323e53; color: #fff;font-size:16px;">
                            <h5 class="modal-title" id="hdnInfo">Export</h5>
                            <button type="button" class="modalcloseButton" data-dismiss="modal" aria-label="Close" style="top: 30px;right: 0px;">
                                        <span aria-hidden="true">✕</span>
                                    </button>
                        </div>
                        <div class="modal-body" style="background-color: #323e53; color: #fff;text-align:center">
                              <asp:Button Text="Export report" id="btnExportreport" CssClass="modalExportbutton" OnClick="btnExport_Click"  runat="server" style="width:40%;"  />

                              <asp:Button Text="Export Detail" id="btnExportDetail" CssClass="modalExportbutton" OnClick="btnExportDetail_Click"  runat="server" style="width:40%;"  />
                           
                        </div>

                      
                        
                    </div>
                </div>
            </div>



    

        <div class="modal fade" id="mdlPopConfirm" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
                <div class="modal-dialog" role="document">
                    <div class="modal-content" style="background: none;border:none;width: 85%;margin-left:100px;">
                        <div class="modal-header dashboardHeadline" style="background-color: #3a475d; color: #fff;font-size:16px;">
                            <h5 class="modal-title" id="hinfo">Information</h5>
                            <button type="button" class="modalcloseButton" data-dismiss="modal" aria-label="Close" style="top: 30px;right: 0px;">
                                        <span aria-hidden="true">✕</span>
                                    </button>
                        </div>
                        <div class="modal-body" style="background-color: #3a475d; color: #fff;">
                            <div>
                                <span id="spntext" style="color: #A9BFD5;text-transform: uppercase;font-size:12px;"> Match credit XX with invoice YY? </span>
                            </div>
                        </div>

                           <div class="modal-footer" style="background-color: #3a475d;padding:0px;">
                            <asp:Button runat="server" CssClass="modalbutton" id="btnMatchOk"  Text="Ok" OnClientClick="return Match();"></asp:Button>
                            <button type="button" class="modalbutton" id="btnCancel" data-dismiss="modal">Cancel</button>
                            
                            </div>
                        
                    </div>
                </div>
            </div>



         <asp:HiddenField ID="hdnInvoiceNumber" runat="server" />
             <asp:HiddenField ID="hdnEmailID" runat="server" />
        <asp:HiddenField ID="hdnMatch" runat="server" Value="true" />
 
      <asp:TextBox ID="txtCustomerID" runat="server" autocomplete="off" Visible="false" ></asp:TextBox>
</asp:Content>
