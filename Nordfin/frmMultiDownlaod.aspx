<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="frmMultiDownlaod.aspx.cs" Inherits="Nordfin.frmMultiDownlaod" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml" style="overflow:hidden;">
<head runat="server">
    <title></title>
        <link href="Styles/bootstrap.min.css" rel="stylesheet" />
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
                                             combineInvoice='<%# Eval("CombineInvoice") %>'  custInvoice=' <%# Eval("Customernumber") %>'
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

                                        <asp:CheckBox runat="server" AutoPostBack="false"  ID="chkMultiDC" collectionStatus=' <%# Eval("Collectionstatus") %>'
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

                                        <asp:CheckBox runat="server" AutoPostBack="false"  ID="chkMultiRemind" collectionStatus=' <%# Eval("Collectionstatus") %>'

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
                    <asp:Button runat="server" CssClass="modalbutton" ID="btnMultiDownload" Text="Download" OnClick="btnMultiDownload_Click"></asp:Button>
                     <asp:Button runat="server" CssClass="modalbutton" ID="btnMultiMail" Text="Mail" ></asp:Button>

                </div>

            </div>
       

    </form>
</body>
</html>
