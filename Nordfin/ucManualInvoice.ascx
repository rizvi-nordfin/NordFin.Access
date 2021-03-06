  <%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ucManualInvoice.ascx.cs" Inherits="Nordfin.ucManualInvoice" %>

<link rel="stylesheet" href="Styles/jquery-ui-NordFin.css" />
      <script src="//code.jquery.com/jquery-1.10.2.js"></script>
    <script src="//code.jquery.com/ui/1.11.1/jquery-ui.js"></script>  

<script>
    $(document).ready(function () {
        let today = new Date().toISOString().slice(0, 10);
        let thirtyDays = new Date();
        thirtyDays.setDate(new Date().getDate() + 30);
        $("#<%= txtInvDate.ClientID %>").datepicker({ dateFormat: 'yy-mm-dd' });
        $("#<%= txtDueDate.ClientID %>").datepicker({ dateFormat: 'yy-mm-dd' });
        $("#<%= txtInvDate.ClientID %>").val(today);
        $("#<%= txtDueDate.ClientID %>").val(thirtyDays.toISOString().slice(0, 10));
        $("#<%= txtQuantity.ClientID %>").val(1);
        <%--var dropdown = document.getElementById("<%= drpInvDelivery.ClientID %>");
        dropdown.options[0] = new Option('PDF Only', 'PDF Only', true);--%>

    });
    function SetTotalAmount() {
        if ($("#<%= hdnVatChanged.ClientID %>").val() == "true") {
            return false;
        }
        var itemPrice = $("#<%= txtAmount.ClientID %>").val();
        var percent = $("#<%= drpVat.ClientID %>").val();
        var quantity = $("#<%= txtQuantity.ClientID %>").val();
        var price = itemPrice.length != 0 ? parseFloat(itemPrice) : 0.0;
        var invoiceAmount = quantity.length == 0 ? price : price * parseInt(quantity);
        var vatPercent = percent.length != 0 ? parseInt(percent) : 0;
        var vatAmount = vatPercent == 0 ? 0 : (invoiceAmount * vatPercent) / 100;
        var total = vatAmount + invoiceAmount;
        $("#<%= txtInvAmount.ClientID %>").val(invoiceAmount.toFixed(2));
        $("#<%= txtVat.ClientID %>").val(vatAmount.toFixed(2));
        $("#<%= txtRowTotal.ClientID %>").val(total.toFixed(2));
    }
    function SetAmountFromTotal() {
        if ($("#<%= hdnVatChanged.ClientID %>").val() == "true") {
            return false;
        }
        var totalAmount = $("#<%= txtRowTotal.ClientID %>").val();
        var percent = $("#<%= drpVat.ClientID %>").val();
        var quantity = $("#<%= txtQuantity.ClientID %>").val();
        var rowTotal = (totalAmount.length != 0 ? parseFloat(totalAmount) : 0.0);
        var vatPercent = (percent.length != 0 ? parseInt(percent) : 0);
        var invoiceAmount = (rowTotal / (100 + vatPercent)) * 100;
        var itemPrice = quantity.length == 0 ? invoiceAmount : invoiceAmount / parseInt(quantity);
        var totalVat = (itemPrice * vatPercent) / 100;
        $("#<%= txtInvAmount.ClientID %>").val(invoiceAmount.toFixed(2));
        $("#<%= txtVat.ClientID %>").val(totalVat.toFixed(2));
        $("#<%= txtAmount.ClientID %>").val(itemPrice.toFixed(2));
    }

    function SetRowTotal() {
        var invoiceAmount = $("#<%= txtInvAmount.ClientID %>").val();
        var vatAmount = $("#<%= txtVat.ClientID %>").val();
        var rowTotal = parseFloat(invoiceAmount) + parseFloat(vatAmount);
        $("#<%= txtRowTotal.ClientID %>").val(rowTotal.toFixed(2));
    }

    function SetVatChanged(txt, evt, vatChanged) {
        $("#<%= hdnVatChanged.ClientID %>").val(vatChanged);
        return ValidateAmount(txt, evt);
    }

    function setPrint() {
        $("#<%= hdnSendToPrint.ClientID %>").val($('#swtchSendToPrint').prop('checked'));
        var dropdown = document.getElementById("drpInvDelivery");
        if ($('#swtchSendToPrint').prop('checked')) {
            dropdown.options[0] = new Option('Paper', 'PAPER', true);
            dropdown.options[1] = new Option('E-Mail', 'EMAIL');
        }
        else {
            dropdown.options.length = 0;
            dropdown.options[0] = new Option('PDF Only', 'PDF Only', true);
        }
        $("#<%= hdnDelivery.ClientID %>").val($('#drpInvDelivery').val());
    }

    function setInvoiceDelivery() {
        $("#<%= hdnDelivery.ClientID %>").val($('#drpInvDelivery').val());
    }
</script>
<div>
    <div class="container-fluid">
        <div class="row mt-3">
            <div class="col-md-11">
                <span class="header" id="spnTitle" runat="server"></span>
            </div>
            <div class="col-md-1">
                <asp:Button ID="btnManualInvClose" Text ="✕" CssClass="modalcloseButton" style="float:right" OnClick="ClosePopup" runat="server" CausesValidation="false" />
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
                <asp:TextBox Text="" runat="server" ID="txtCustPostCode" CssClass="form-control controls" Enabled="false"></asp:TextBox>
            </div>
            <div class="col-md-2">
                <span class="title">City</span>
                <asp:TextBox Text="" runat="server" ID="txtCustCity" CssClass="form-control controls" Enabled="false"></asp:TextBox>
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
            <div class="col-md-2">
                <span class="title">Invoice Date</span>
                <asp:TextBox ID="txtInvDate" runat="server" autocomplete="off" CssClass="form-control controls textboxColor"></asp:TextBox>
            </div>
            <div class="col-md-2">
                <span class="title">Due Date</span>
                <asp:TextBox ID="txtDueDate" runat="server" autocomplete="off" CssClass="form-control controls textboxColor"></asp:TextBox>
            </div>
            <div class="col-md-2">
                <span class="title">Currency</span>
                <asp:DropDownList ID="drpCurrency" runat="server" CssClass="form-control dropdown controls" Height="">
                </asp:DropDownList>
            </div>
            <div class="col-md-2">
                <div class="custom-control custom-switch">
                <input type="checkbox" class="custom-control-input" id="swtchSendToPrint" style="position:relative" onchange="setPrint()">
                <label id="lblSendToPrint" class="custom-control-label title" for="swtchSendToPrint">Send To Print</label>
                </div>
            </div>
            <div class="col-md-2">
                <span class="title">Delivery Mode</span>
                <select id="drpInvDelivery" class="form-control dropdown controls" onchange="setInvoiceDelivery()">
                    <option value="PDF Only" selected="selected">PDF Only</option>
                </select>
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
            <div class="col-md-2">
                <span class="title">Period</span>
                <asp:TextBox ID="txtPeriod" runat="server" autocomplete="off" CssClass="form-control controls"></asp:TextBox>
            </div>
            <div class="col-md-2">
                <span class="title">Quantity</span>
                <asp:TextBox ID="txtQuantity" runat="server" autocomplete="off" CssClass="form-control controls" TextMode="Number" onblur="SetTotalAmount(); return false;"></asp:TextBox>
            </div>
            <div class="col-md-2">
                <span class="title">Unit</span>
                <asp:TextBox ID="txtUnit" runat="server" autocomplete="off" CssClass="form-control controls"></asp:TextBox>
            </div> 
        </div>
        <div class="row">
            <div class="col-md-2">
                <span class="title">Amount excl. VAT</span>
                <asp:TextBox ID="txtAmount" runat="server" autocomplete="off" CssClass="form-control controls" ClientIDMode="Static" onkeypress="return SetVatChanged(this, event,'false');" oninput="RestrictToTwoDecimal(this)" onblur="SetTotalAmount(); return false;"></asp:TextBox>
            </div>
            <div class="col-md-2">
                <span class="title">VAT %</span>
                <asp:DropDownList ID="drpVat" runat="server" CssClass="form-control dropdown controls" Height="" AutoPostBack="true" onchange="SetTotalAmount(); return false;">
                </asp:DropDownList>
            </div>
            <div class="col-md-2">
                <span class="title">Invoice Amount</span>
                <asp:TextBox ID="txtInvAmount" runat="server" autocomplete="off" CssClass="form-control controls" ClientIDMode="Static" Enabled="false"></asp:TextBox>

            </div>
            <div class="col-md-2">
                <span class="title">VAT Amount</span>
                <asp:TextBox ID="txtVat" runat="server" autocomplete="off" CssClass="form-control controls" ClientIDMode="Static" onkeypress="return SetVatChanged(this, event,'true');" onblur="SetRowTotal();"></asp:TextBox>
            </div>
            <div class="col-md-2">
                <span class="title">Row Total</span>
                <asp:TextBox ID="txtRowTotal" runat="server" autocomplete="off" CssClass="form-control controls" onkeypress="return SetVatChanged(this, event,'false');" onblur="SetAmountFromTotal(); return false;"></asp:TextBox>
            </div>
            <div class="col-md-2" style="padding-top: 26px">
                <asp:Button ID="btnAddRow" Text="Add Row" CssClass="export rowButton" OnClick="AddRows_Click" runat="server" />
            </div>
        </div>
    </div>
    <br />
    <div class="container-fluid">
        <div class="row">
            <div class="col-md-2">
                <asp:Button ID="btnGridDelete" Text="Delete Row" Style="padding-left: 10px;" CssClass="export rowButton" runat="server" Enabled="false" OnClick="DeleteRows" />
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
                        <asp:BoundField DataField="Period" HeaderStyle-CssClass="itemalign" ItemStyle-CssClass="itemalign" HeaderText="Period" ItemStyle-HorizontalAlign="Center" />
                        <asp:BoundField DataField="Unit" HeaderStyle-CssClass="itemalign" ItemStyle-CssClass="itemalign" HeaderText="Unit" ItemStyle-HorizontalAlign="Center" />
                        <asp:BoundField DataField="Quantity" HeaderStyle-CssClass="itemalign" ItemStyle-CssClass="itemalign" HeaderText="Qty" ItemStyle-HorizontalAlign="Center" />
                        <asp:BoundField DataField="ItemPrice" HeaderStyle-CssClass="itemalign" ItemStyle-CssClass="itemalign" HeaderText="Item Price" ItemStyle-HorizontalAlign="Center" />
                        <asp:BoundField DataField="VATPercent" HeaderStyle-CssClass="itemalign" ItemStyle-CssClass="itemalign" HeaderText="VAT %" ItemStyle-HorizontalAlign="Center" />
                        <asp:BoundField DataField="InvoiceAmount" HeaderStyle-CssClass="itemalign" ItemStyle-CssClass="itemalign" HeaderText="Invoice Amount" ItemStyle-HorizontalAlign="Center" />
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
            <div class="col-md-2" style="padding-top: 26px">
                <asp:Button ID="btnCreate" Text="Create" CssClass="export rowButton" OnClick="CreateManualInvoice" runat="server" />
            </div>
        </div>
    </div>
</div>

<div class="modal" id="pdfViewer">
    <div class="modal-dialog" role="document">
        <div class="modal-content pdfViewerModal">
            <div class="modal-body pdfViewerModal" style="padding: 5px;">
                <iframe id="iInvoicePdf" class="pdfViewerIframe"></iframe>
                <br />
                <div class="container-fluid">
                    <div class="row">
                        <div class="col-md-8 pdfMessageAlign">
                            <p class="pdfMessage">Please check the invoice before importing.</p>
                        </div>
                        <div class="col-md-2 pdfMessageAlign">
                            <asp:Button ID="btnImport" Text="Import" CssClass="export rowButton" OnClick="ImportManualInvoice" OnClientClick="closePDFViewer();" runat="server" />
                        </div>
                        <div class="col-md-2 pdfMessageAlign">
                            <button type="button" class="export rowButton" onclick="closePDFViewer();">Cancel</button>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</div>

<div class="modal" id="mdlError">
    <div class="modal-dialog" role="document">
        <div class="modal-content errorModel">
            <div class="modal-body" style="background-color: #323e53; color: #fff;">
                <div class="container-fluid">
                    <div class="row">
                        <div class="col-md-1">
                            <i class="far fa-times-circle errorIcon"></i>
                            </div>
                        <div class="col-md-11">
                            <p class="modelDialogText" id="txtError"/>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-9">
                            </div>
                        <div class="col-md-3">
                            <button type="button" class="export rowButton" style="float:right" onclick="closeErrorModal();">OK</button>
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
                            <p class="modelDialogText">Invoice Imported Successfully.</p>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-9">
                            </div>
                        <div class="col-md-3">
                            <button type="button" class="export rowButton" style="float:right" onclick="closeSuccessModal();">OK</button>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</div>

<div class="modal" id="mdlConfirmData" tabindex="-1" role="dialog">
    <div class="modal-dialog" role="document">
        <div class="modal-content errorModel" style="width: 120% !important;height: 200px">
            <div class="modal-body" style="background-color: #323e53; color: #fff; text-align: center">
                <div class="container-fluid">
                    <div class="row" style="height:50px">
                        <div class="col-md-12">
                            <p class="modelDialogText">Are you sure to create invoice with following details?</p>
                        </div>
                    </div>
                    <div class="row" style="height:70px;text-align: left">
                        <div class="col-md-4">
                            <span class="title">Invoice Amount:</span>
                            <asp:Label ID="lblInvoiceAmount" runat="server" CssClass="confirmData"></asp:Label>
                        </div>
                        <div class="col-md-4">
                            <span class="title">Due Date:</span>
                            <asp:Label ID="lblDueDate" runat="server" CssClass="confirmData"></asp:Label>
                        </div>
                        <div class="col-md-4">
                            <span class="title">Delivery:</span>
                            <asp:Label ID="lblDelivery" runat="server" CssClass="confirmData"></asp:Label>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-6">
                        </div>
                        <div class="col-md-3">
                            <asp:Button ID="btnCreateInvoice" Text="Yes" CssClass="export rowButton" OnClick="CreateInvoicePdf" OnClientClick="closeConfirmModal();" runat="server" />
                        </div>
                        <div class="col-md-3">
                            <button type="button" class="export rowButton" style="float: right" onclick="closeConfirmModal();">No</button>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</div>

<asp:HiddenField ID="hdnInvoiceNumber" runat="server" />
<asp:HiddenField ID="hdnFileName" runat="server" />
<asp:HiddenField ID="hdnTitle" runat="server" />
<asp:HiddenField ID="hdnCustomerType" runat="server" />
<asp:HiddenField ID="hdnSendToPrint" runat="server" Value="false" />
<asp:HiddenField ID="hdnDelivery" runat="server" Value="PDF Only"/>
<asp:HiddenField ID="hdnVatChanged" runat="server" Value="false"/>