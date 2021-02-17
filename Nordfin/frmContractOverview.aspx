<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Nordfin.Master" CodeBehind="frmContractOverview.aspx.cs" Inherits="Nordfin.frmContractOverview" %>


<asp:Content ID="Content1" ContentPlaceHolderID="NordfinContentHolder" runat="server" style="padding: 5px;">

    <script src="Scripts/jsInvoices.js?version=<%=ConfigurationManager.AppSettings["VersionConfiguration"].ToString() %>"></script>

    <link href="Styles/ContractsOverview.css?version=<%=ConfigurationManager.AppSettings["VersionConfiguration"].ToString() %>" rel="stylesheet" />

 

    <div class="dashboardContainer">
        <div class="container-fluid">

            <div class="row dashboardHeader">
                <div class="col-lg-12 dashboardHeadline">Contract: Overview   
                    <asp:Button ID="btnExport" Text="Export" CssClass="export" Style="width: 75px;display:none;"  runat="server" /></div>

               

         

               

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
                                

                                <asp:BoundField DataField="CustomerNumber" HeaderStyle-CssClass="itemalign" ItemStyle-CssClass="itemalign" HeaderText="CustomerNumber" SortExpression="CustomerNumber" />

                                <asp:BoundField DataField="Name" HeaderStyle-CssClass="itemalign" ItemStyle-CssClass="itemalign" HeaderText="Name" SortExpression="Name" />

                                  <asp:BoundField DataField="OrgNumber" HeaderStyle-CssClass="itemalign" ItemStyle-CssClass="itemalign" HeaderText="OrgNumber" SortExpression="OrgNumber" />
                                <asp:BoundField DataField="Mortage" DataFormatString="{0:#,0.00}" HeaderStyle-CssClass="itemalign" ItemStyle-CssClass="itemalign" HeaderText="Mortage" SortExpression="Mortage" />
                                <asp:BoundField DataField="Monthleft" HeaderStyle-CssClass="itemalign" ItemStyle-CssClass="itemalign" HeaderText="Month left" SortExpression="Monthleft" />

                                <asp:BoundField DataField="CreditScore" HeaderStyle-CssClass="itemalign" ItemStyle-CssClass="itemalign tableNoWrap" HeaderText="CreditScore" SortExpression="CreditScore" />
                                

                             
                            </Columns>
                            <EmptyDataTemplate>No Record Available</EmptyDataTemplate>
                        </asp:GridView>
                    </div>
                     </ContentTemplate>
            </asp:UpdatePanel>
             
          
                     
        
        </div>

    </div>
</asp:Content>