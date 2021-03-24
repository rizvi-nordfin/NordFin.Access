<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Nordfin.Master" CodeBehind="frmContractOverview.aspx.cs" Inherits="Nordfin.frmContractOverview" %>


<asp:Content ID="Content1" ContentPlaceHolderID="NordfinContentHolder" runat="server" style="padding: 5px;">

    <script src="Scripts/jsInvoices.js?version=<%=ConfigurationManager.AppSettings["VersionConfiguration"].ToString() %>"></script>

    <link href="Styles/ContractsOverview.css?version=<%=ConfigurationManager.AppSettings["VersionConfiguration"].ToString() %>" rel="stylesheet" />

 

    <div class="dashboardContainer">
        <div class="container-fluid">

            <div class="row dashboardHeader">
                <div class="col-lg-12 dashboardHeadline">Contract: Overview  
                    <div style="margin-top: 10px;margin-right: -28px;float:right;">
                        <asp:Button ID="btnExport" Text="Download" CssClass="export" Style="width: 100px;" runat="server" OnClick="btnExport_Click" />
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

           
            <asp:UpdatePanel runat="server" ID="UpdatePanel1">
                <ContentTemplate>
                    <div class="tableFixHead tableMarginBg">
                       <asp:GridView ID="grdContract" runat="server" AutoGenerateColumns="False" AllowSorting="true" OnSorting="grdContract_Sorting" EmptyDataRowStyle-CssClass="Emptyrow" ViewStateMode="Enabled" Visible="true" Style="color: white; overflow-x: scroll; overflow-y: scroll;"
                            ShowHeaderWhenEmpty="true" CssClass="table table-borderless">
                            <HeaderStyle BackColor="#475672" CssClass="GVFixedHeader" />
                            <Columns>
                                
                                 <asp:TemplateField ItemStyle-CssClass="itemalign" HeaderText="CUSTOMER" SortExpression="CustomerNumber" HeaderStyle-CssClass="itemalign">
                                    <ItemTemplate>

                                        <asp:LinkButton CssClass="linkcss" Text='<%# Bind("CustomerNumber") %>' CommandName="Sort" ID="gridLinkCustNum" CommandArgument=' <%# Bind("CustomerNumber") %>' OnClick="gridLinkCustNum_Click" runat="server" />
                                    </ItemTemplate>

                                </asp:TemplateField>
                             

                                <asp:BoundField DataField="Name" HeaderStyle-CssClass="itemalign leftalign" ItemStyle-CssClass="itemalign leftalign" HeaderText="Name" SortExpression="Name" />

                                  <asp:BoundField DataField="OrgNumber" HeaderStyle-CssClass="itemalign" ItemStyle-CssClass="itemalign" HeaderText="Reg. Number" SortExpression="OrgNumber" />
                                <asp:BoundField DataField="Mortage" DataFormatString="{0:#,0.00}" HeaderStyle-CssClass="itemalign" ItemStyle-CssClass="itemalign" HeaderText="Contract Value" SortExpression="Mortage" />
                                <asp:BoundField DataField="Monthleft" HeaderStyle-CssClass="itemalign" ItemStyle-CssClass="itemalign" HeaderText="Months left" SortExpression="Monthleft" />

                                <asp:BoundField DataField="CreditScore" HeaderStyle-CssClass="itemalign" ItemStyle-CssClass="itemalign tableNoWrap" HeaderText="Credit" SortExpression="CreditScore" />
                                

                             
                            </Columns>
                            <EmptyDataTemplate>No Record Available</EmptyDataTemplate>
                        </asp:GridView>
                    </div>
                     </ContentTemplate>
            </asp:UpdatePanel>
             
          
                     
        
        </div>

    </div>
</asp:Content>