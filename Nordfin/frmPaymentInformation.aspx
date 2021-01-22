﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="frmPaymentInformation.aspx.cs" Inherits="Nordfin.frmPaymentInformation" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml" style="font-size:small;overflow-y:hidden;" id="popupHTML">
<head runat="server">
    <title></title>
     
    <link href="Styles/bootstrap.min.css" rel="stylesheet" />
    <link href="Styles/NordfinMaster.css?version=<%=ConfigurationManager.AppSettings["VersionConfiguration"].ToString() %>" rel="stylesheet" />
    <script src="Scripts/jquery-3.3.1.slim.min.js"></script>
    <script src="Scripts/popper.min.js"></script>
    <script src="Scripts/bootstrap.min.js"></script>
    <script src="Scripts/angular.min.js"></script>
    <script src="Scripts/jsPaymentInformation.js?version=<%=ConfigurationManager.AppSettings["VersionConfiguration"].ToString() %>"></script>
    <link rel="stylesheet" href="Styles/jquery-ui-NordFin.css" />

   <script src="//code.jquery.com/jquery-1.10.2.js"></script>
    <script src="//code.jquery.com/ui/1.11.1/jquery-ui.js"></script>
    <link href="Styles/PaymentInformation.css?version=<%=ConfigurationManager.AppSettings["VersionConfiguration"].ToString() %>" rel="stylesheet" />

    <style>
        #divScroll
        {
            height:485px;
            
            overflow-y:scroll;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div data-ng-app="myApp" id="AngularDiv" data-ng-controller="myCtrl" class="paymentInfoModal">



        <div class="paymentInfoModalHeader">
          <asp:Label runat="server" ID="lblInvoiceNum" Style="color: #FFB100; text-align: left; font-size: medium;"></asp:Label>
          <div class="paymentInfoButtons">
            <button class="button paymentInfoButton" data-ng-click="notesUpdate($event)" runat="server">Update</button>
            <asp:Button Text="Download" class="button paymentInfoButton" id="btnDownload" data-ng-click="btnDownload($event)"  runat="server"></asp:Button>
          </div>

              
                 <button class="button closeButton" onclick="return closex(this.event);">✕</button>
              
        </div>

        <div style="background-color: #323e53; padding-top: 30px;" class="container-fluid">

            <div class="row">
                <div class="col-md-6" id="divInformation">
                    <div class="container containerColor">
                        <div class="fontHeaderText">
                            PAYMENT INFORMATION
                        </div>
                        <br />

                        <div class="row">

                        </div>

                        <div class="row" style="padding-top:5px;">
                            <div style="color: white; font-size: small;" class="col-md-6">
                              <asp:Label ID="txtPaymentInfoReferenceLabel"
                                         Text="Payment Reference"
                                         AssociatedControlID="txtPaymentInfoReference"
                                         runat="server">
                                         <asp:TextBox runat="server" class="form-control controlColor" disabled="disabled" ID="txtPaymentInfoReference"></asp:TextBox>
                              </asp:Label>
                                
                            </div>
                            <div style="color: white; font-size: small;" class="col-md-6">
                              <asp:Label ID="txtPaymentInfoDeliveryLabel"
                                         Text="Delivery"
                                         AssociatedControlID="txtPaymentInfoDelivery"
                                         runat="server">
                                         <asp:TextBox runat="server" class="form-control controlColor" disabled="disabled" ID="txtPaymentInfoDelivery"></asp:TextBox>
                              </asp:Label>
                                
                            </div>
                        </div>
                        <br />
                        <div class="row">
                        </div>

                        <div class="row" style="padding-top:5px;">
                            <div class="col-md-6">
                              <asp:Label ID="txtCollectionStatusLabel"
                                         Text="Collection Status:"
                                         AssociatedControlID="txtCollectionStatus"
                                         runat="server">
                                  <asp:TextBox runat="server" class="form-control controlColor" disabled="disabled" ID="txtCollectionStatus"></asp:TextBox>
                              </asp:Label>

                              
                            </div>
                            <div class="col-md-3">
                              <asp:Label ID="txtCollectionDateLabel"
                                         Text="Collection Date:"
                                         AssociatedControlID="txtCollectionDate"
                                         runat="server">
                                  <asp:TextBox runat="server" class="form-control controlColor" disabled="disabled" ID="txtCollectionDate"></asp:TextBox>
                              </asp:Label>
                            </div>

                            <div style="color: white; font-size: small;" class="col-md-3">
                              <asp:Label ID="txtDueDateLabel"
                                         Text="Due Date:"
                                         AssociatedControlID="txtDueDate"
                                         runat="server">
                                  <asp:TextBox runat="server" class="form-control controlColor" AutoCompleteType="Disabled" ID="txtDueDate"></asp:TextBox>
                              </asp:Label>
                                
                            </div>
                        </div>
                        <br />


                        <div class="row" style="padding-top:5px;">
                            <div class="col-md-3">
                              <asp:Label ID="cboCollectionStopLabel"
                                         Text="Collection Stop"
                                         AssociatedControlID="cboCollectionStop"
                                         runat="server">
                                         <asp:DropDownList runat="server" ID="cboCollectionStop"  CssClass="form-control selectColor">
                                             <asp:ListItem Text="NO" Value="0" ></asp:ListItem>
                                             <asp:ListItem Text="YES" Value="1"></asp:ListItem>
                                         </asp:DropDownList>
                              </asp:Label>
                              
                              
                                
                            </div>
                            <div class="col-md-3">
                              <asp:Label ID="txtCollectionStopUntilLabel"
                                         Text="Coll. Stop Until"
                                         AssociatedControlID="txtCollectionStopUntil"
                                         runat="server">
                                         <asp:TextBox runat="server" CssClass="form-control controlColor" AutoCompleteType="Disabled" ID="txtCollectionStopUntil"></asp:TextBox>
                              </asp:Label>
                                
                            </div>

                            <div class="col-md-3">
                              <asp:Label ID="lblContested"
                                         Text="Contested"
                                         AssociatedControlID="cboContested"
                                         runat="server">
                                        <asp:DropDownList runat="server" ID="cboContested"  CssClass="form-control selectColor">
                                             <asp:ListItem Text="NO" Value="0" ></asp:ListItem>
                                             <asp:ListItem Text="YES" Value="1"></asp:ListItem>
                                         </asp:DropDownList>
                              </asp:Label>
                                
                            </div>


                            <div class="col-md-3">
                              <asp:Label ID="lblContestedDate"
                                         Text="Contested Date"
                                         AssociatedControlID="txtContestedDate"
                                         runat="server">
                                         <asp:TextBox runat="server" CssClass="form-control controlColor" disabled="disabled" AutoCompleteType="Disabled" ID="txtContestedDate"></asp:TextBox>
                              </asp:Label>
                                
                            </div>
                          

                        </div>
                        <br />
                        <div class="row" style="padding-top:5px;">
                            <div class="col-md-6">
                              <asp:Label ID="txtPaymentMethodLabel"
                                         Text="Payment Method"
                                         AssociatedControlID="txtPaymentMethod"
                                         runat="server">
                                         <asp:TextBox runat="server" CssClass="form-control controlColor" disabled="disabled" ID="txtPaymentMethod"></asp:TextBox>
                              </asp:Label>
                                
                            </div>
                             </div>

                    </div>

                    <div class="container containerColor">
                        <br />
                        <div class="row">

                        </div>

                        <div class="row" style="padding-top:5px;">
                            <div class="col-md-12">
                              <asp:Label ID="txtNotesLabel"
                                         Text="Notes"
                                         AssociatedControlID="txtNotes"
                                         runat="server">
                                         <asp:TextBox ID="txtNotes" TextMode="MultiLine" Style="height: 100px;color:white;" runat="server" CssClass="form-control controlColor"></asp:TextBox>
                              </asp:Label>
                              
                            </div>

                        </div>

                        <br />
                    </div>
                    <br />

                 
                </div>


                <div class="col-md-6" id="divScroll">
                    <div class="container containerColor">


                        <div class="fontHeaderText">
                            PAYMENTS 
                        </div>
                        <br />
                        <div data-ng-repeat="payment in PaymentsList">
                            <div class="row">
                            </div>

                            <div class="row" style="padding-top:5px;">
                                <br />
                                <div class="col-md-3">
                                    <label>Date</label>
                                    <asp:TextBox runat="server" CssClass="form-control controlColor" AutoCompleteType="Disabled" Text="{{ payment.PayDate }}" controlID="txtPaymentsDate{{$index}}" disabled="disabled"  ID="txtPaymentsDate">    </asp:TextBox>
                                
                                </div>
                                <div class="col-md-3 fontClass">
                                    <label>Amount</label>
                                    <asp:TextBox runat="server" CssClass="form-control controlColor" AutoCompleteType="Disabled" Text="{{ payment.PayAmount }}" disabled="disabled" ID="txtPaymentsAmount"></asp:TextBox>
                                </div>

                                <div class="col-md-6 fontClass">
                                    <label>Reference</label>
                                    <asp:TextBox runat="server" CssClass="form-control controlColor" Text="{{ payment.PayRef }}" disabled="disabled" ID="txtPaymentReference"></asp:TextBox>
                                </div>
                                <br />

                            </div>
                        </div>
                        <br />


                    </div>
                    <div class="container containerColor">
                        <div class="fontHeaderText">
                            FEE 
                        </div>
                        <br />
                        <div data-ng-repeat="fees in FeesList">

                            <div class="row" style="padding-top:5px;">
                                <div class="col-md-3">
                                    <label>Date</label>
                                    <asp:TextBox runat="server" CssClass="form-control controlColor"  disabled="disabled" Text="{{ fees.FeeDate }}" ID="txtFeeDate"></asp:TextBox>
                                 

                                </div>
                                <div class="col-md-3">
                                    <label>Fee</label>
                                    <asp:TextBox runat="server" CssClass="form-control controlColor" disabled="disabled" Text="{{ fees.FeeAmount }}" ID="txtFee"></asp:TextBox>

                                </div>

                                <div class="col-md-2">
                                    <label>Remain</label>
                                    <asp:TextBox runat="server" CssClass="form-control controlColor" disabled="disabled" Text="{{ fees.FeeRemainingAmount }}" ID="txtFeeRemain"></asp:TextBox>

                                </div>
                                <div class="col-md-2">
                                    <label>Type</label>
                                    <asp:TextBox runat="server" CssClass="form-control controlColor" disabled="disabled" Text="{{ fees.FeeType }}" ID="txtFeeType"></asp:TextBox>

                                </div>
                                <div class="col-md-2">
                                    <div data-ng-if = "fees.FeeRemainingAmount > 0.00">
                                        <button  class="form-control" style="background-color: #232D41; border: 1px solid #3DADC5; color: #3DADC5; display: inline-block;margin-top:22px;" data-ng-click="feeRemove($event,fees)" id="btnFeeRemove">Remove</button>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <br />
                    </div>
                    <div class="container containerColor">
                        <div class="fontHeaderText">
                            INTEREST 
                        </div>
                        <br />
                        <div data-ng-repeat="interest in InterestList">

                            <div class="row" style="padding-top:5px;">
                                <div class="col-md-3">
                                    <label>Date</label>
                                    <asp:TextBox runat="server" CssClass="form-control controlColor" Text="{{ interest.InterestDate }}" disabled="disabled" ID="txtInterestDate"></asp:TextBox>


                                </div>

                                <div class="col-md-3">
                                    <label>Amount</label>
                                    <asp:TextBox runat="server" CssClass="form-control controlColor" Text="{{ interest.InterestAmount }}" disabled="disabled" ID="txtInterestAmount"></asp:TextBox>

                                </div>
                                <div class="col-md-4">
                                    <label>Remain</label>
                                    <asp:TextBox runat="server" CssClass="form-control controlColor" Text="{{ interest.InterestRemainingAmount }}" disabled="disabled" ID="txtInterestRemain" />
                                </div>
                                <div class="col-md-2">
                                 
                                    <div data-ng-if="interest.InterestRemainingAmount > 0.00">
                                        <button class="form-control" data-ng-click="updateInterest($event,interest)" style="background-color: #232D41; border: 1px solid #3DADC5; color: #3DADC5; display: inline-block;margin-top:22px;" id="btnInterestRemove">
                                            Remove</button>
                                    </div>
                                </div>
                                <br />
                            </div>
                        </div>

                        <br />
                    </div>

                    <div class="container containerColor">
                        <div class="fontHeaderText">
                            TRANSACTIONS 
                        </div>
                        <br />
                        <div data-ng-repeat="transaction in TransactionList">

                            <div class="row" style="padding-top:5px;">
                                <div class="col-md-3">
                                    <label>Date</label>
                                    <asp:TextBox runat="server" CssClass="form-control controlColor" ID="txtTransactionDate" disabled="disabled" Text="{{ transaction.BookingDate }}"></asp:TextBox>

                                </div>

                                <div class="col-md-3">
                                    <label>Type</label>
                                    <asp:TextBox runat="server" CssClass="form-control controlColor" ID="txtTransactionType" disabled="disabled" Text="{{ transaction.TransactionType }}"></asp:TextBox>

                                </div>
                                <div class="col-md-4">
                                    <label>Text</label>

                                    <div class="form-control controlColor" style="color:white;min-height:50px; height:auto;">{{ transaction.TransactionText }}</div>
                                    <%--<asp:TextBox TextMode="MultiLine" runat="server" CssClass="form-control controlColor" ID="txtTransactionText" disabled="disabled" Text="{{ transaction.TransactionText }}"></asp:TextBox>--%>

                                </div>
                                <div class="col-md-2">
                                    <label>Amount</label>
                                    <asp:TextBox runat="server" CssClass="form-control controlColor" ID="txtTransactionAmount" disabled="disabled" Text="{{ transaction.TransactionAmount }}"></asp:TextBox>

                                </div>
                                <br />
                            </div>
                        </div>
                        <br />
                    </div>


                    <div class="container containerColor">
                        <div class="fontHeaderText">
                            PAYOUT 
                        </div>
                        <br />
                        
                        <div class="row" style="padding-top:5px;">
                            <div class="col-md-3">
                                <label>Over Payment</label>
                                <asp:TextBox runat="server" CssClass="form-control controlColor" style="color:white;" AutoCompleteType="Disabled" ID="txtPayoutOverPayment"></asp:TextBox>

                            </div>

                         
                            <div class="col-md-6">
                                <label>Bank Account</label>
                                <asp:TextBox runat="server" CssClass="form-control controlColor" style="color:white;" AutoCompleteType="Disabled" ID="txtOverPaymentBankAccount" ></asp:TextBox>

                            </div>
                            <div class="col-md-3">
                              <button  class="form-control modalPayoutButton" style="background-color: #232D41; border: 1px solid #3DADC5; color: #3DADC5; display: inline-block; margin-top:22px;" id="btnOverPaymentPayout"  data-ng-click="InsertPayout('txtOverPaymentBankAccount',$event,'1','btnOverPaymentPayout')" >Payout</button>
                            </div>
                            <br />
                        </div>


                        <div class="row" style="padding-top:25px;">
                            <div class="col-md-3">
                                <label>Credit Payment</label>
                                <asp:TextBox runat="server" CssClass="form-control controlColor" AutoCompleteType="Disabled" ID="txtPayoutCreditPayment"></asp:TextBox>

                            </div>

                            
                            <div class="col-md-6">
                                <label>Bank Account</label>
                                <asp:TextBox runat="server" CssClass="form-control controlColor" style="color:white;" AutoCompleteType="Disabled" ID="txtCreditPaymentBankAccount"></asp:TextBox>

                            </div>
                            <div class="col-md-3">
                                  <button  class="form-control modalPayoutButton" style="background-color: #232D41; border: 1px solid #3DADC5; color: #3DADC5; display: inline-block; margin-top:22px;" id="btnCreditPaymentPayout" data-ng-click="InsertPayout('txtCreditPaymentBankAccount',$event,'0','btnCreditPaymentPayout')" >Payout</button>
                            </div>
                            <br />
                        </div>
                        <br />
                    </div>

                       <div class="container containerColor ">
                       
                        <div class="row fontHeaderText">
                            <div class="col-md-3">SMS </div>

                        </div>
                        <br />


                        <div class="row">
                            <div class="col-md-3">
                                <label>Confirmation</label>
                                <asp:TextBox runat="server" CssClass="form-control controlColor" ID="txtConfirmation"></asp:TextBox>
                            </div>
                            <div class="col-md-3">
                                <label>Date</label>
                                <asp:TextBox runat="server" ID="txtDate" CssClass="form-control controlColor"></asp:TextBox>
                            </div>
                            <div></div>
                        </div>
                        <br />

                    </div>

                    <div class="container containerColor">
                        <div  class="fontHeaderText">
                            NOTES 
                        </div>
                        <br />

                        <div style="max-width: 100%; overflow-x: scroll;">
                            <asp:GridView ID="grdNotes" runat="server" AutoGenerateColumns="False" ViewStateMode="Enabled" Visible="true" Style="color: white; font-size: small;border:1px solid #475672; min-width: 700px;" ShowHeaderWhenEmpty="true" CssClass="table table-borderless">
                                <HeaderStyle BackColor="#475672" />
                                <Columns>
                                    <asp:BoundField DataField="InvoiceNumber" HeaderStyle-CssClass="Notesalign" ItemStyle-CssClass="Notesalign" HeaderText="INVOICE" SortExpression="InvoiceNumber" />
                                    <asp:BoundField DataField="NoteType" HeaderStyle-CssClass="Notesalign" ItemStyle-CssClass="Notesalign" HeaderText="Type" SortExpression="NoteType" />
                                    <asp:BoundField DataField="NoteDate" HeaderStyle-CssClass="Notesalign" ItemStyle-CssClass="Notesalign" HeaderText="Date" SortExpression="NoteDate" />
                                    <asp:BoundField DataField="UserName" HeaderStyle-CssClass="Notesalign" ItemStyle-CssClass="Notesalign" HeaderText="User" SortExpression="UserName" />
                                    <asp:BoundField DataField="NoteText" HeaderStyle-CssClass="Notesalign" ItemStyle-CssClass="Notesalign" HeaderText="Text" SortExpression="NoteText" />
                                </Columns>
                                <EmptyDataTemplate>No Record Available</EmptyDataTemplate>
                            </asp:GridView>
                        </div>
                    </div>
                </div>
            </div>



        </div>

  
            <div class="modal fade" style="top:100px !important;" id="mdlDeleteConfirm" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
                <div class="modal-dialog" role="document">
                    <div class="modal-content" style="background: none;border:none;width: 75%;">
                        <div class="modal-header dashboardHeadline" style="background-color: #323e53; color: #fff;font-size:16px;">
                            <h5 class="modal-title" id="exampleModalLabel">Information</h5>
                            <button type="button" class="modalcloseButton" data-dismiss="modal" aria-label="Close" style="top: 30px;right: 0px;">
                                        <span aria-hidden="true">✕</span>
                                    </button>
                        </div>
                        <div class="modal-body" style="background-color: #323e53; color: #fff;">
                            <div>
                                <span id="spnBody" style="color: #A9BFD5;text-transform: uppercase;font-size:12px;"> Are you sure you want to remove? </span>
                            </div>
                        </div>

                           <div class="modal-footer" style="background-color: #323E53;padding:0px;">
                               <button type="button" class="modalbutton" id="btnYes" >Yes</button>
                            <button type="button" class="modalbutton" id="btnNo" data-dismiss="modal">No</button>
                            
                            </div>
                        
                    </div>
                </div>
            </div>


            
            <div class="modal fade" id="mdlInformConfirm" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
                <div class="modal-dialog" role="document">
                    <div class="modal-content" style="background: none;border:none;width: 83%;">
                        <div class="modal-header dashboardHeadline" style="background-color: #323e53; color: #fff;font-size:16px;">
                            <h5 class="modal-title" id="informModalLabel">Information</h5>
                             <button type="button" class="modalcloseButton" data-dismiss="modal" aria-label="Close" style="top: 30px;right: 13px;">
                                        <span aria-hidden="true">✕</span>
                                    </button>
                        </div>
                        <div class="modal-body" style="background-color: #323e53; color: #fff;">
                            <div>
                                <span id="spnInformBody" style="color: #A9BFD5;text-transform: uppercase;font-size:12px;"> Please enter the account information before marking for payout </span>
                            </div>
                        </div>
                        <div class="modal-footer" style="background-color: #323E53;padding:0px;">
                          
                            <button type="button" class="modalbutton" id="btnOk" data-dismiss="modal">Ok</button>
                        </div>
                    </div>
                </div>
            </div>


               <div class="modal fade" id="mdlPayout" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
                <div class="modal-dialog" role="document">
                    <div class="modal-content" style="background: none;border:none;width: 83%;">
                        <div class="modal-header dashboardHeadline" style="background-color: #323e53; color: #fff;font-size:16px;">
                            <h5 class="modal-title" id="informPayout">Payout</h5>
                             <button type="button" class="modalcloseButton" data-dismiss="modal" aria-label="Close" style="top: 30px;right: 13px;">
                                        <span aria-hidden="true">✕</span>
                                    </button>
                        </div>
                        <div class="modal-body" style="background-color: #323e53; color: #fff;">
                            <div class="row" style="padding-top: 25px;">
                                <div class="col-md-6">
                                    <label id="lblPayment"></label>
                                    <asp:TextBox runat="server" CssClass="form-control controlColor" ReadOnly="true" AutoCompleteType="Disabled" ID="txtPayment"></asp:TextBox>

                                </div>


                                <div class="col-md-6">
                                    <label>Bank Account</label>
                                    <asp:TextBox runat="server" CssClass="form-control controlColor" ReadOnly="true" Style="color: white;" AutoCompleteType="Disabled" ID="txtAccount"></asp:TextBox>

                                </div>
                              

                            </div>
                        </div>
                        <div class="modal-footer" style="background-color: #323E53;padding:0px;">
                          
                            <button type="button" class="modalbutton" style="width:125px;" id="btnRemove" data-ng-click="RemovePayout()" data-dismiss="modal">Remove Payout</button>
                        </div>
                    </div>
                </div>
            </div>

            <asp:HiddenField ID="hdnDueDate" runat="server"/>
            <asp:HiddenField ID="hdnCollectionStopUntil" runat="server"/>
               <asp:HiddenField ID="hdnCollectionStop" runat="server"/>

             <asp:HiddenField ID="hdnPaymentCheck" runat="server"/>
            <asp:HiddenField ID="hdnPurchased" runat="server"/>


              <asp:HiddenField ID="hdnClientName" runat="server" />
    <asp:HiddenField ID="hdnFileName" runat="server" />
  
    <a id="pdfViewer" href="" runat="server" target="_blank"></a>

</div>
    </form>
</body>
</html>