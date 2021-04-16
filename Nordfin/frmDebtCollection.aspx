<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Nordfin.Master" CodeBehind="frmDebtCollection.aspx.cs" Inherits="Nordfin.frmDebtCollection" %>


<asp:Content ID="Content1" ContentPlaceHolderID="NordfinContentHolder" runat="server" style="padding: 5px;">
      <script src="Scripts/jsInvoices.js?version=<%=ConfigurationManager.AppSettings["VersionConfiguration"].ToString() %>"></script>
    <link href="Styles/DebtCollection.css?version=<%=ConfigurationManager.AppSettings["VersionConfiguration"].ToString() %>" rel="stylesheet" />
    <style>
    
        </style>
 

    <div class="dashboardContainer">
        <div class="container-fluid">

            <div class="row dashboardHeader">
                <div class="col-lg-12 dashboardHeadline">
                    Debt Collection List
                  
                </div>

            </div>

           
            <asp:UpdatePanel runat="server" ID="UpdatePanel1">
                <ContentTemplate>
                    <div class="tableFixHead tableMarginBg">
                       <asp:GridView ID="grdDebtCollection" runat="server" AutoGenerateColumns="False" AllowSorting="true"  EmptyDataRowStyle-CssClass="Emptyrow" ViewStateMode="Enabled" Visible="true" Style="color: white; overflow-x: scroll; overflow-y: scroll;"
                            ShowHeaderWhenEmpty="true" CssClass="table table-borderless">
                            <HeaderStyle BackColor="#475672" CssClass="GVFixedHeader" />
                            <Columns>
                                
                                 <asp:TemplateField ItemStyle-CssClass="itemalign" HeaderText="CUSTOMER" SortExpression="CustomerNumber" HeaderStyle-CssClass="itemalign">
                                    <ItemTemplate>

                                        <asp:LinkButton CssClass="linkcss" Text='<%# Bind("CustomerNumber") %>' CommandName="Sort" ID="gridLinkCustNum" CommandArgument=' <%# Bind("CustomerNumber") %>'  runat="server" />
                                    </ItemTemplate>

                                </asp:TemplateField>
                              <asp:BoundField DataField="InvoiceNumber" HeaderStyle-CssClass="itemalign" ItemStyle-CssClass="itemalign" HeaderText="Invoice" SortExpression="InvoiceNumber" />
                                 <asp:BoundField DataField="InvoiceAmount" DataFormatString="{0:#,0.00}" HeaderStyle-CssClass="itemalign" ItemStyle-CssClass="itemalign" HeaderText="AMOUNT" SortExpression="Invoiceamount" />
                                <asp:BoundField DataField="RemainingAmount" DataFormatString="{0:#,0.00}" HeaderStyle-CssClass="itemalign" ItemStyle-CssClass="itemalign" HeaderText="REMAIN" SortExpression="Remainingamount" />
                                <asp:BoundField DataField="DueDate" HeaderStyle-CssClass="itemalign" ItemStyle-CssClass="itemalign tableNoWrap" HeaderText="DUE DATE" SortExpression="Duedate" />
                               
                                <asp:BoundField DataField="ExtDate" HeaderStyle-CssClass="itemalign" ItemStyle-CssClass="itemalign tableNoWrap" HeaderText="Ext DATE" SortExpression="ExtDate" />
                             <asp:TemplateField ItemStyle-CssClass="itemalign" HeaderStyle-CssClass="itemalign">
                                    <ItemTemplate>

                                        <asp:Button runat="server" CssClass="invoicesDownloadButton button button-table" ID="btnCollectionStop" OnClick="btnCollectionStop_Click"
                                          CommandArgument=' <%# Bind("InvoiceID") %>'
                                            Text="Collection Stop" />
                                    </ItemTemplate>
                                    <HeaderTemplate>
                                        Collection Stop
                                    </HeaderTemplate>
                                </asp:TemplateField>
                            </Columns>
                            <EmptyDataTemplate>No Record Available</EmptyDataTemplate>
                        </asp:GridView>
                    </div>
                     </ContentTemplate>
            </asp:UpdatePanel>
             
          
                     
        
        </div>

    </div>
    </asp:Content>
