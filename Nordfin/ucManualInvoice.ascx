<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ucManualInvoice.ascx.cs" Inherits="Nordfin.ucManualInvoice" %>

<script>
    $(document).ready(function () {
        let today = new Date().toISOString().slice(0, 10);
        let thirtyDays = new Date();
        thirtyDays.setDate(new Date().getDate() + 30);
        $("#<%= txtInvDate.ClientID %>").val(today);
        $("#<%= txtDueDate.ClientID %>").val(thirtyDays.toISOString().slice(0, 10));
    });
    function SetTotalAmount() {
        var invAmount = $("#<%= txtInvAmount.ClientID %>").val();
        var percent = $("#<%= drpVat.ClientID %>").val();
        var invoiceAmount = (invAmount.length != 0 ? parseFloat(invAmount) : 0.0);
        var vatPercent = (percent.length != 0 ? parseInt(percent) : 0);
        var vatAmount = (invoiceAmount * vatPercent) / 100;
        var total = vatAmount + invoiceAmount;
        $("#<%= txtVat.ClientID %>").val(vatAmount);
        $("#<%= txtAmount.ClientID %>").val(total);
    }
</script>
<div>
    <div class="container-fluid">
      <div class="row mt-3">
          <div class="col-md-4">
              <span class="header">Manual Invoice</span>
            </div>
          <div class="col-md-4">
                <div class="custom-control custom-switch" style="padding-top:8px">
                  <input type="checkbox" class="custom-control-input" id="swtchCreditInvoice">
                  <label class="custom-control-label header" for="swtchCreditInvoice" style="font-size:15px !important">Credit Invoice</label>
                </div>
          </div>
          <div class="col-md-4">          
              <asp:Button ID="btnManualInvClose"  Text="✕" CssClass="modalcloseButton" style="float:right" OnClick="ClosePopup" runat="server" />
            </div>
      </div>
     </div>
    <br />
    <div class="container-fluid">
      <div class="row" id="manualCustomer">
          <div class="col-md-2">
                <span class="title">Customer Number</span>
                <asp:TextBox Text="" runat="server" ID="txtCustNum" CssClass="form-control controls" Enabled="false"></asp:TextBox>
            </div>
          <div class="col-md-2">
                <span class="title">Customer Name</span>
                <asp:TextBox ID="txtCustName" runat="server" autocomplete="off" CssClass="form-control controls" Enabled="false"></asp:TextBox>
            </div>
            <div class="col-md-2">
                <span class="title">Address 1</span>
                <asp:TextBox ID="txtCustContact" runat="server" autocomplete="off" CssClass="form-control controls" Enabled="false"></asp:TextBox>
            </div>
            <div class="col-md-2">
                <span class="title">Address 2</span>
                <asp:TextBox ID="txtCustAddress" runat="server" autocomplete="off" CssClass="form-control controls" Enabled="false"></asp:TextBox>
            </div>
          <div class="col-md-2">
                <span class="title">Post Code</span>
                <asp:TextBox Text="" runat="server" ID="txtPostCode" CssClass="form-control controls" Enabled="false"></asp:TextBox>
            </div>
          <div class="col-md-2">
                <span class="title">City</span>
                <asp:TextBox Text="" runat="server" ID="txtCity" CssClass="form-control controls" Enabled="false"></asp:TextBox>
            </div>
      </div>
      </div>
    <br />
    <div class="container-fluid">
      <div class="row">
            <div class="col-md-2">
                <span class="title">Invoice Number</span>
                <asp:Label Text="" runat="server" ID="lblInvoiceNumber" CssClass="invNumber"></asp:Label>
            </div>
            <div class="col-md-3">
                <span class="title">Invoice Date</span>
                <asp:TextBox ID="txtInvDate" runat="server" autocomplete="off" CssClass="form-control controls" TextMode="Date"></asp:TextBox>
            </div>
            <div class="col-md-3">
                <span class="title">Due Date</span>
                <asp:TextBox ID="txtDueDate" runat="server" autocomplete="off" CssClass="form-control controls" TextMode="Date"></asp:TextBox>
            </div>
          <div class="col-md-2">
                <span class="title">Currency</span>
                <asp:DropDownList ID="drpCurrency" runat="server" CssClass="form-control dropdown controls" Height="">
                    <asp:ListItem>SEK</asp:ListItem>
                    <asp:ListItem>EUR</asp:ListItem>
                </asp:DropDownList>
          </div>
            <div class="col-md-2">
                <span class="title">Delivery Mode</span>
                <asp:DropDownList ID="drpInvDelivery" runat="server" CssClass="form-control dropdown controls" Height="">
                    <asp:ListItem>Paper</asp:ListItem>
                    <asp:ListItem>E-Mail</asp:ListItem>
                </asp:DropDownList>
          </div>
     </div>
  </div>
    <br />
    <div class="container-fluid">
      <div class="row">
          <span class="sectionHeader">Invoice Rows</span>
     </div>
    </div>
    <div class="container-fluid">
      <div class="row">
            <div class="col-md-3">
                <span class="title">Article</span>
                <asp:TextBox ID="txtArticle" runat="server" autocomplete="off" CssClass="form-control controls"></asp:TextBox>
            </div>
            <div class="col-md-3">
                <span class="title">Description</span>
                <asp:TextBox ID="txtDescription" runat="server" autocomplete="off" CssClass="form-control controls"></asp:TextBox>
            </div>
          <div class="col-md-3">
                <span class="title">Unit</span>
                <asp:TextBox ID="txtUnit" runat="server" autocomplete="off" CssClass="form-control controls"></asp:TextBox>
            </div>
            <div class="col-md-3">
                <span class="title">Quantity</span>
                <asp:TextBox ID="txtQuantity" runat="server" autocomplete="off" CssClass="form-control controls" TextMode="Number"></asp:TextBox>
            </div>
          </div>
        <div class="row">
           <div class="col-md-3">
                <span class="title">Amount excl. VAT</span>
                <asp:TextBox ID="txtInvAmount" runat="server" autocomplete="off" CssClass="form-control controls" ClientIDMode="Static" onkeypress="return ValidateAmount(this, event);" onblur="SetTotalAmount(); return false;"></asp:TextBox>
           </div>
            <div class="col-md-3">
                <span class="title">VAT %</span>
                <asp:DropDownList ID="drpVat" runat="server" CssClass="form-control dropdown controls" Height="" AutoPostBack="true" onchange="SetTotalAmount(); return false;">
                    <asp:ListItem>25</asp:ListItem>
                    <asp:ListItem>24</asp:ListItem>
                </asp:DropDownList>
          </div>
            <div class="col-md-2">
                <span class="title">VAT Amount</span>
                <asp:TextBox ID="txtVat" runat="server" autocomplete="off" CssClass="form-control controls" ClientIDMode="Static" onkeypress="return ValidateAmount(this, event);"></asp:TextBox>
                
          </div>
          <div class="col-md-2">
                <span class="title">Row Total</span>
                <asp:TextBox ID="txtAmount" runat="server" autocomplete="off" CssClass="form-control controls" Enabled="false"></asp:TextBox>
            </div>
           <div class="col-md-2" style="padding-top:26px">
                <asp:Button ID="btnAddRow"  Text="Add Row" CssClass="export rowButton"  OnClick="AddRows_Click" runat="server" />
            </div>
          </div>
        </div>
    <br />
    <div class="container-fluid">
    <div class="row">
        <div class="col-md-2">
               <asp:Button ID="btnGridDelete"  Text="Delete Row" style="padding-left: 10px;" CssClass="export rowButton" runat="server" Enabled="false" OnClick="DeleteRows" />
           </div>
           <%--<div class="col-md-2">
               <asp:Button ID="btnGridEdit"  Text="Edit Row" CssClass="export" style="width:100%;height:30px"  runat="server" Enabled="false" />
           </div>--%>
        </div>
        </div>
    <br />
    <div class="container-fluid">
      <div class="row">
          <div class="col table-responsive">
          <asp:GridView ID="grdInvoiceRows" runat="server" ShowFooter="true" AutoGenerateColumns="false" ShowHeaderWhenEmpty="True" EmptyDataRowStyle-HorizontalAlign="Center" EmptyDataRowStyle-VerticalAlign="Middle" EmptyDataText="No Data Found" 
              OnRowDataBound="grdInvoiceRows_OnRowDataBound" OnSelectedIndexChanged="grdInvoiceRows_SelectedIndexChanged" SelectedRowStyle-BackColor="#475672" CssClass="invoiceRow" EmptyDataRowStyle-CssClass="Emptyrow" GridLines="None">
              <Columns>
                  <asp:BoundField DataField="Article" HeaderStyle-CssClass="itemalign" ItemStyle-CssClass="itemalign" HeaderText="Article" ItemStyle-HorizontalAlign="Center" />
                  <asp:BoundField DataField="Description" HeaderStyle-CssClass="itemalign" ItemStyle-CssClass="itemalign" HeaderText="Description" ItemStyle-HorizontalAlign="Center" />
                  <asp:BoundField DataField="Unit" HeaderStyle-CssClass="itemalign" ItemStyle-CssClass="itemalign" HeaderText="Unit" ItemStyle-HorizontalAlign="Center" />
                  <asp:BoundField DataField="Quantity" HeaderStyle-CssClass="itemalign" ItemStyle-CssClass="itemalign" HeaderText="Quantity" ItemStyle-HorizontalAlign="Center" />
                  <asp:BoundField DataField="InvoiceAmount" HeaderStyle-CssClass="itemalign" ItemStyle-CssClass="itemalign" HeaderText="Item Price" ItemStyle-HorizontalAlign="Center" />
                  <asp:BoundField DataField="VATPercent" HeaderStyle-CssClass="itemalign" ItemStyle-CssClass="itemalign" HeaderText="VAT %" ItemStyle-HorizontalAlign="Center" />
                  <asp:BoundField DataField="VATAmount" HeaderStyle-CssClass="itemalign" ItemStyle-CssClass="itemalign" HeaderText="VAT Amount" ItemStyle-HorizontalAlign="Center" />
                  <asp:BoundField DataField="TotalAmount" HeaderStyle-CssClass="itemalign" ItemStyle-CssClass="itemalign" HeaderText="Row Total" ItemStyle-HorizontalAlign="Center" />
                </Columns>
          </asp:GridView>
              </div>
     </div>
    </div>
    <br />
    <div class="container-fluid">
      <div class="row">
          <div class="col-md-1"></div>
          <div class="col-md-3">
                <span class="title">Invoice Amount</span>
                <asp:TextBox ID="txtTotalInv" runat="server" autocomplete="off" CssClass="form-control controls"></asp:TextBox>
            </div>
            <div class="col-md-3">
                <span class="title">Total VAT</span>
                <asp:TextBox ID="txtTotalVat" runat="server" autocomplete="off" CssClass="form-control controls"></asp:TextBox>
            </div>
          <div class="col-md-3">
                <span class="title">Total Amount</span>
                <asp:TextBox ID="txtTotalAmount" runat="server" autocomplete="off" CssClass="form-control controls"></asp:TextBox>
            </div>
          <div class="col-md-2" style="padding-top:26px">
            <asp:Button ID="btnCreate"  Text="Create" CssClass="export rowButton" OnClick="CreateManualInvoice" runat="server" />
          </div>
          </div>
    </div>
 </div>
<asp:HiddenField ID="hdnInvoiceAmount" runat="server" />
<asp:HiddenField ID="hdnBillDate" runat="server" />
<asp:HiddenField ID="hdnDueDate" runat="server" />
<asp:HiddenField ID="hdnRemainingAmount" runat="server" />
<asp:HiddenField ID="hdnTotalAmount" runat="server" />
<%--<asp:LinkButton ID="LinkButton2" runat="server">LinkButton</asp:LinkButton>
            <asp:Button ID="Button1" runat="server" Text="Close" />
<asp:Panel runat="server" id="successPanel">
    
    </asp:Panel>
<ajaxToolkit:ModalPopupExtender ID="modalSuccess" runat="server" BackgroundCssClass="modalBackground"
                                            PopupControlID="successPanel" CancelControlID="Button1" TargetControlID="LinkButton2">
</ajaxToolkit:ModalPopupExtender>--%>
<%--<div class="modal fade" id="successModal" tabindex="-1" role="dialog" aria-labelledby="exampleModalCenterTitle" aria-hidden="true">
                  <div class="modal-dialog modal-dialog-centered" role="document">
                    <div class="modal-content">
                      <div class="modal-header">
                        <h5 class="modal-title" id="exampleModalLongTitle">Success</h5>
                        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                          <span aria-hidden="true">&times;</span>
                        </button>
                      </div>
                      <div class="modal-body">
                        Manual Invoice Created Successfully!
                      </div>
                      <div class="modal-footer">
                        <button type="button" class="btn btn-secondary" data-dismiss="modal">Close</button>
                      </div>
                    </div>
                  </div>
                </div>--%>