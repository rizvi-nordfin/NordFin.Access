<%@ Page Language="C#" AutoEventWireup="true" Async="true" CodeBehind="frmMultiDownlaod.aspx.cs" Inherits="Nordfin.frmMultiDownlaod" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml" style="overflow:hidden;">
<head runat="server">
    <title></title>
        <link href="Styles/bootstrap.min.css" rel="stylesheet" />
      <script src="https://code.jquery.com/jquery-3.4.1.min.js"></script>
     <script src="Scripts/bootstrap.min.js"></script>
    <link href="Styles/NordfinMaster.css?version=<%=ConfigurationManager.AppSettings["VersionConfiguration"].ToString() %>" rel="stylesheet" />
     <link href="Styles/Customer.css?version=<%=ConfigurationManager.AppSettings["VersionConfiguration"].ToString() %>" rel="stylesheet" />
        <script src="Scripts/jsMultiDownload.js?version=<%=ConfigurationManager.AppSettings["VersionConfiguration"].ToString() %>"></script>

</head>
<body>
    <form id="form1" runat="server">
     
 
            <div style="margin-top:-10px;">
                <div class="modal-header dashboardHeadline" style="background-color: #323e53; color: #fff; font-size: 16px;">
                    <h5 class="modal-title" id="hdnInfo">Export</h5>
                    <button type="button" class="modalcloseButton" data-dismiss="modal" aria-label="Close" style="top: 14px; right: 14px;" onclick="return closex(this.event);">
                        <span aria-hidden="true">✕</span>
                    </button>
                </div>
                <div class="modal-body" style="background-color: #323e53; color: #fff; text-align: center">
                  <asp:Panel runat="server" ID="pnlExport" CssClass="text-left">
                        <asp:CheckBox ID="chkExport" AutoPostBack="false" Checked="true" Style="margin-top: 5px;" CssClass="checkbox"
                            runat="server" Text="Export"></asp:CheckBox>
                    </asp:Panel>
                    <asp:Panel runat="server" ID="pnlExportDetail"  CssClass="text-left">
                        <asp:CheckBox ID="chkExportDetail" AutoPostBack="false" Checked="true" Style="margin-top: 5px;" CssClass="checkbox"
                            runat="server" Text="Export Detail"></asp:CheckBox>
                    </asp:Panel>



                    <div style="height:300px;overflow-y:scroll;">
                         
                               <asp:GridView ID="grdInvoiceDownlaod" runat="server" EmptyDataRowStyle-CssClass="Emptyrow" AllowSorting="true"  AutoGenerateColumns="False" ViewStateMode="Enabled" Visible="true" 
                            Style="color: white; font-size: small; margin-top: -4px;" ShowHeaderWhenEmpty="true" CssClass="table"  SelectedRowStyle-BackColor="#475672">
                            <HeaderStyle BackColor="#475672" />
                            <Columns>
                                <asp:TemplateField ItemStyle-CssClass="labelcolor itemalign" HeaderText="INVOICE Number" SortExpression="Invoicenumber" HeaderStyle-CssClass="itemalign">
                                    <ItemTemplate>

                                        <asp:LinkButton CssClass="linkcss" Text='<%# Bind("Invoicenumber") %>' overpaymentData='<%# Eval("Overpayment") %>' collectionStatus=' <%# Eval("Collectionstatus") %>'
                                             combineInvoice='<%# Eval("CombineInvoice") %>'  custInvoice=' <%# Eval("Customernumber") %>'  CommandArgument=' <%# Eval("CombineInvoice") %>'
                                            remainData='<%# Eval("Remainingamount") %>' invoiceData='<%# Eval("Customernumber") +"|"+ Eval("InvoiceID")%>' ID="gridLink"  runat="server" />
                                    </ItemTemplate>

                                </asp:TemplateField>
                                   <asp:TemplateField ItemStyle-CssClass="itemalign" HeaderStyle-CssClass="itemalign">
                                    <ItemTemplate>

                                        <asp:CheckBox runat="server"  ID="chkMultiInvoices" collectionStatus=' <%# Eval("Collectionstatus") %>'
                                            custInvoice=' <%# Eval("Customernumber") %>' combineInvoice='<%# Eval("CombineInvoice") %>'
                                            CommandArgument=' <%# Eval("CombineInvoice") %>'  />
                                    </ItemTemplate>
                                    <HeaderTemplate>
                                        Invoice
                                    </HeaderTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField ItemStyle-CssClass="itemalign" HeaderStyle-CssClass="itemalign">
                                    <ItemTemplate>

                                        <asp:CheckBox runat="server" AutoPostBack="false"  ID="chkMultiRemind" collectionStatus=' <%# Eval("Collectionstatus") %>'
                                            Visible='<%# Eval("Collectionstatus").ToString().ToUpper() == "REMIND" || Eval("Collectionstatus").ToString().ToUpper() == "DC" || Eval("Collectionstatus").ToString().ToUpper() == "EXT" ? true :false %>' 
                                            custInvoice=' <%# Eval("Customernumber") %>' combineInvoice='<%# Eval("CombineInvoice") %>'
                                            CommandArgument=' <%# Eval("CombineInvoice") %>'  />
                                    </ItemTemplate>
                                    <HeaderTemplate>
                                        Remind
                                    </HeaderTemplate>
                                </asp:TemplateField>
                                    <asp:TemplateField ItemStyle-CssClass="itemalign" HeaderStyle-CssClass="itemalign">
                                    <ItemTemplate>

                                        <asp:CheckBox runat="server" AutoPostBack="false"  ID="chkMultiDC" collectionStatus=' <%# Eval("Collectionstatus") %>'

                                            Visible='<%#  Eval("Collectionstatus").ToString().ToUpper() == "DC" || Eval("Collectionstatus").ToString().ToUpper() == "EXT"? true :false %>' 

                                            custInvoice=' <%# Eval("Customernumber") %>' combineInvoice='<%# Eval("CombineInvoice") %>'
                                            CommandArgument=' <%# Eval("CombineInvoice") %>'  />
                                    </ItemTemplate>
                                    <HeaderTemplate>
                                        DC
                                    </HeaderTemplate>
                                </asp:TemplateField>
                              

                            </Columns>
                            <EmptyDataTemplate>No Record Available</EmptyDataTemplate>
                        </asp:GridView>
                             
                    </div>
                     
              <%--      <asp:Button Text="Export report" ID="btnExportreport" CssClass="modalExportbutton" OnClick="btnExport_Click" runat="server" Style="width: 40%;" />

                    <asp:Button Text="Export Detail" ID="btnExportDetail" CssClass="modalExportbutton" OnClick="btnExportDetail_Click" runat="server" Style="width: 40%;" />--%>

                </div>

                
                <div class="modal-footer" style="background-color: #3a475d; padding: 0px;">

                      <asp:Panel runat="server" ID="pnlSelectAll" CssClass="text-left" style="margin: auto;margin-left: 20px;">
                        <asp:CheckBox ID="chkSelectAll" AutoPostBack="true" Checked="false" Style="margin-top: 5px;" OnCheckedChanged="chkSelectAll_CheckedChanged" CssClass="checkbox"
                            runat="server" Text="Select All"></asp:CheckBox>
                    </asp:Panel>
                    <asp:Button runat="server" CssClass="modalbutton" ID="btnMultiDownload" Text="Download" OnClientClick="PdfDownloadClick();" OnClick="btnMultiDownload_Click"></asp:Button>
                     <asp:Button runat="server" CssClass="modalbutton" ID="btnMultiMail" Text="Mail" OnClick="btnMultiMail_Click" ></asp:Button>

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

          <div class="modal" id="mdlUpdateInfo" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
                        <div class="modal-dialog" role="document">
                            <div class="modal-content" style="top: -30px; background: none; border: none;">
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
                                        <asp:Button Text="Send" class="button updateInfoButton form-control" runat="server" ID="btnSend" OnClientClick="ProgressBarDisplay();" OnClick="btnSend_Click"  Width="128px" />


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
        <div style="display: none;">
            <iframe id="pdfZip" runat="server"></iframe>
            <iframe id="pdfExport" runat="server"></iframe>
            <iframe id="pdfExportDetail" runat="server"></iframe>
        </div>
          <asp:HiddenField ID="hdnClientName" runat="server" />
            <asp:HiddenField ID="hdnFileName" runat="server" />
    </form>
</body>
</html>
