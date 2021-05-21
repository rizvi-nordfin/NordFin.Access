<%@ Page Title="NFC ACCESS" Language="C#" MasterPageFile="~/Nordfin.Master" AutoEventWireup="true" CodeBehind="frmAddNew.aspx.cs" Inherits="Nordfin.frmAddNew" %>

<asp:Content ID="Content1" ContentPlaceHolderID="NordfinContentHolder" runat="server">
    <meta name="viewport" content="width=device-width, initial-scale=1">
    <!-- <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/css/bootstrap.min.css"> -->
    <!-- <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.2.1/jquery.min.js"></script> -->
    <!-- <script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/js/bootstrap.min.js"></script> -->
    <script src="Scripts/jAddNew.js?version=<%=ConfigurationManager.AppSettings["VersionConfiguration"].ToString() %>"></script>
    <link href="Styles/AddNew.css?version=<%=ConfigurationManager.AppSettings["VersionConfiguration"].ToString() %>" rel="stylesheet" />
    <%--<script src="//code.jquery.com/jquery-1.10.2.js"></script>
    <script src="//code.jquery.com/ui/1.11.1/jquery-ui.js"></script>--%>
    <link rel="stylesheet" href="Styles/jquery-ui-NordFin.css" />
    <div class="dashboardContainer">
        <div class="container-fluid">

            <div class="dashboardHeader">
                <div class="dashboardHeadline">Add New</div>
            </div>

            <div class="container-fluid">

                <div class="row">

                    <div class="reportsCardContainer col-md-3 col-md-offset-1;">
                        <div class="reportsCard">
                            <div class="info-icon"></div>
                            <div class="reportsCardContent">
                                <i class="far fa-user fa-7x" style="color:white"></i>
                                <div class="reportsCardCaption">New Customer</div>
                            </div>
                            <div class="reportsCardBottom">
                                <asp:Button CssClass="button reportsCardButton form-control" runat="server" Text="Create" ID="btnAddNewCustomer" OnClientClick="return OpenAddCustomer();" CausesValidation="false"></asp:Button>
                            </div>
                        </div>
                    </div>

                    <asp:HiddenField Id="hdnPrivate" runat="server" Value="true" />
                    <asp:HiddenField Id="hdnPhoneCode" runat="server" />

                    <div class="modal" id="mdlAddCustomer" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
                        <div class="modal-dialog" role="document">
                            <div class="modal-content" style="top: -10px; background: none; border: none;">
                                <div class="modal-header dashboardHeadline" style="background-color: #323e53; color: #fff; font-size: 16px;">
                                    <h5 class="modal-title modalTextcolor dashboardHeadlineModal" id="informModalLabel">NEW CUSTOMER</h5>
                                    <button type="button" class="modalcloseButton" data-dismiss="modal" aria-label="Close" style="top: 35px; right: 20px;" onclick="CloseAddCustomer()">
                                        <span aria-hidden="true">✕</span>
                                    </button>
                                </div>
                                <div class="modal-body" style="background-color: #323e53; color: #fff;">
                                    <div class="row addCustomerRowHeight" >
                                        <div class="col-md-3" style="padding-top:20px">
                                            <div class="custom-control custom-switch">
                                             <input type="checkbox" class="custom-control-input" id="swtchPrivate" onchange="customerTypePrivateChanged()" checked>
                                             <label class="custom-control-label" for="swtchPrivate">Private</label>
                                             </div>
                                        </div>
                                        <div class="col-md-3" style="padding-top:20px">
                                            <div class="custom-control custom-switch">
                                            <input type="checkbox" class="custom-control-input" id="swtchCorporate" onchange="customerTypeCorporateChanged()">
                                             <label class="custom-control-label" for="swtchCorporate">Corporate</label>
                                            </div>
                                        </div>
                                         <div class="col-md-6" style="padding-top:20px">
                                            <asp:DropDownList ID="drpCountry" runat="server" CssClass="form-control dropdown textboxModalColor" onchange="countryChanged()">
                                                <asp:ListItem Text="Select Country" Value="None"></asp:ListItem>
                                                <asp:ListItem Text="Sweden" Value="SE"></asp:ListItem>
                                                <asp:ListItem Text="Finland" Value="FI"></asp:ListItem>
                                            </asp:DropDownList>
                                             <asp:CompareValidator  ID="cmpCountry" runat="server" ControlToValidate="drpCountry" Operator="NotEqual" ValueToCompare="None" Type="String" ErrorMessage="Country is required." ForeColor="Red" Font-Size="Smaller" />
                                        </div>
                                    </div>
                                    <div class="row addCustomerRowHeight" >
                                        <div class="col-md-12">
                                            <asp:Label CssClass="addCustomerLabel" runat="server" ID="Label1" Text="Customer Name"></asp:Label>
                                            <asp:TextBox ID="txtCustomerName" runat="server" autocomplete="off" CssClass="form-control textboxModalColor" MaxLength="100"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rfvCustomerName" runat="server" ControlToValidate="txtCustomerName" ErrorMessage="Customer Name is required." ForeColor="Red" Font-Size="Smaller" Display="Dynamic" />
                                            <asp:CompareValidator runat="server" Operator="DataTypeCheck" Type="String" ControlToValidate="txtCustomerName" ErrorMessage="Value must be only text" CssClass="errorMsgPosition" />
                                        </div>
                                    </div>
                                    <div class="row addCustomerRowHeight">
                                        <div class="col-md-6">
                                            <asp:Label CssClass="addCustomerLabel" runat="server" ID="spnCustomerNumber" Text="Customer Number"></asp:Label>
                                            <asp:TextBox ID="txtCustomerNumber" runat="server" autocomplete="off" CssClass="form-control textboxModalColor" MaxLength="10"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rfvCustomerNumber" runat="server" ControlToValidate="txtCustomerNumber" ErrorMessage="Customer Number is required." ForeColor="Red" Font-Size="Smaller" Display="Dynamic" />
                                            <asp:CompareValidator runat="server" Operator="DataTypeCheck" Type="Integer" ControlToValidate="txtCustomerNumber" ErrorMessage="Value must be only number" ForeColor="Red" Font-Size="Smaller" />
                                        </div>
                                        <div class="col-md-6">
                                            <asp:Label CssClass="addCustomerLabel" runat="server" ID="spnPersonalNumber" Text="Social Security Number"></asp:Label>
                                            <asp:TextBox ID="txtPersonalNumber" runat="server" autocomplete="off" CssClass="form-control textboxModalColor" placeholder="YYMMDDNNNN" MaxLength="10"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rfvPersonalNumber" runat="server" ControlToValidate="txtPersonalNumber" ErrorMessage="Social Security Number is required." ForeColor="Red" Font-Size="Smaller" Display="Dynamic"/>
                                            <asp:CompareValidator runat="server" Operator="DataTypeCheck" Type="Integer" ControlToValidate="txtPersonalNumber" ErrorMessage="Value must be only number and in specified format" ForeColor="Red" Font-Size="Smaller"/>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-12">
                                            <asp:Label CssClass="addCustomerLabel" ID="spnAddress1" Text="Address Line 1" runat="server"></asp:Label>
                                            <asp:TextBox ID="txtAddress1" runat="server" autocomplete="off" CssClass="form-control textboxModalColor" MaxLength="100"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rfvAddress1" runat="server" ControlToValidate="txtAddress1" ErrorMessage="Address Line 1 is required." ForeColor="Red" Font-Size="Smaller" />
                                        </div>
                                    </div>
                                    <div class="row addCustomerRowHeight">
                                        <div class="col-md-12">
                                            <asp:Label CssClass="addCustomerLabel" runat="server" Text="Address Line 2" ID="spnAddress2"> </asp:Label>
                                            <asp:TextBox ID="txtAddress2" runat="server" autocomplete="off" CssClass="form-control textboxModalColor" MaxLength="100"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-4">
                                            <asp:Label CssClass="addCustomerLabel" runat="server" Text="Postal Code" ID="spnPostalCode"> </asp:Label>
                                            <asp:TextBox ID="txtPostalCode" runat="server" autocomplete="off" CssClass="form-control textboxModalColor" MaxLength="10"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rfvPostalCode" runat="server" ControlToValidate="txtPostalCode" ErrorMessage="Postal Code is required." ForeColor="Red" Font-Size="Smaller" />
                                        </div>

                                        <div class="col-md-8">
                                            <asp:Label CssClass="addCustomerLabel" runat="server" ID="spnCity" Text="City"></asp:Label>
                                            <asp:TextBox ID="txtCity" runat="server" autocomplete="off" CssClass="form-control textboxModalColor" MaxLength="50"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rfvCity" runat="server" ControlToValidate="txtCity" ErrorMessage="City is required." ForeColor="Red" Font-Size="Smaller" />
                                        </div>
                                    </div>
                                    <div class="row addCustomerRowHeight">
                                        <div class="col-md-12">
                                            <asp:Label CssClass="addCustomerLabel" runat="server" ID="spnModalEmail" Text="Email"> </asp:Label>
                                            <asp:TextBox ID="txtEmail" runat="server" autocomplete="off" CssClass="form-control textboxModalColor" MaxLength="100"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rfvEmail" runat="server" ControlToValidate="txtEmail" ErrorMessage="E-Mail is required." ForeColor="Red" Font-Size="Smaller" Display="Dynamic"/>
                                            <asp:RegularExpressionValidator ID="revEmailValid" runat="server" ValidationExpression="\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" ControlToValidate="txtEmail" ErrorMessage="Invalid Email Format" ForeColor="Red" Font-Size="Smaller"/>
                                        </div>
                                    </div>
                                    <div class="row addCustomerRowHeight">
                                        <div class="col-md-3" style="padding-right: 0px;">
                                            <asp:Label CssClass="addCustomerLabel" runat="server" ID="spnPhonenumber" Text="Phone Number"> </asp:Label>
                                            <asp:TextBox ID="txtPhoneCode" runat="server" Enabled="false" autocomplete="off" CssClass="form-control textboxModalColor"></asp:TextBox>
                                        </div>
                                        <div class="col-md-9" style="padding-top:24px">
                                            <asp:TextBox ID="txtPhoneNumber" runat="server" autocomplete="off" CssClass="form-control textboxModalColor" placeholder="Enter 9 digit number" MaxLength="9"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rfvPhone" runat="server" ControlToValidate="txtPhoneNumber" ErrorMessage="Phone Number is required." ForeColor="Red" Font-Size="Smaller" Display="Dynamic"/>
                                            <asp:CompareValidator runat="server" Operator="DataTypeCheck" Type="Integer" ControlToValidate="txtPhoneNumber" ErrorMessage="Enter only numbers" ForeColor="Red" Font-Size="Smaller"/>
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
                                    <div class="modal-content errorModel">
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

                    <div class="modal" id="mdlConfirm" data-backdrop="static">
                        <div class="modal-dialog" role="document">
                            <div class="modal-content errorModel">
                                <div class="modal-body" style="background-color: #323e53; color: #fff;">
                                    <div class="container-fluid">
                                        <div class="row">
                                            <div class="col-md-1">
                                                <i class="far fa-times-circle errorIcon"></i>
                                            </div>
                                            <div class="col-md-11">
                                                <p style="color: white; font-size: 15px !important;" id="txtConfirm" />
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-md-6">
                                            </div>
                                            <div class="col-md-3">
                                                <asp:Button runat="server" class="export rowButton" ID="Button1" Text="Yes" OnClick="btnConfirmOk_Click" CausesValidation="false"></asp:Button>
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

                    <div class="modal" id="mdlSuccess" tabindex="-1" role="dialog">
                                <div class="modal-dialog" role="document">
                                    <div class="modal-content errorModel">
                                        <div class="modal-body" style="background-color: #323e53; color: #fff;">
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
                                                        <asp:Button runat="server" class="export rowButton" ID="Button2" Text="OK" OnClick="btnSuccessOk_Click" CausesValidation="false"></asp:Button>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
            
        </div>
    </div>
</asp:Content>
