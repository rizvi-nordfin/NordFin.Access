<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="frmCreditCheck.aspx.cs" MasterPageFile="~/Nordfin.Master" Inherits="Nordfin.frmCreditCheck" %>
<asp:Content ID="Content1" ContentPlaceHolderID="NordfinContentHolder" runat="server"   style="background-color: #232D41;">
    <asp:Panel DefaultButton="btnCreditCheck" runat="server">
    <link href="Styles/AccountSettings.css?version=<%=ConfigurationManager.AppSettings["VersionConfiguration"].ToString() %>" rel="stylesheet" />

    <script src="Scripts/jsAccountSettings.js?version=<%=ConfigurationManager.AppSettings["VersionConfiguration"].ToString() %>"></script>
        <style>
            .sideContractMenuButtonStatistics {
                background-color: rgb(44, 56, 80);
            }

                .sideContractMenuButtonStatistics img.sideMenuIcon {
                    filter: brightness(0) saturate(100%) invert(78%) sepia(49%) saturate(3809%) hue-rotate(359deg) brightness(101%) contrast(106%);
                }
        </style>
    <div class="dashboardContainer">
        <div class="container-fluid">
            <div class="dashboardHeader">
                <div class="dashboardHeadline">CreditCheck</div>
            </div>



            <div>
                <div class="row " style="height: 50px; margin-left: 0px;">


                    <div class="col-md-2 divPadding customerInfoSidebar" style="color: #FFFFFF;">
                        <div style="background-color: #3E4B64;">


                            <div class="divPaddingLeft" style="background-color: #475672">
                                <asp:Label runat="server" CssClass="customerdivHeading" ID="lblClientName"></asp:Label>

                                

                                  <hr class="divHrLine" />
                            </div>

                              <div class="divPaddingLeft">
                                <span class="customerdivHeading">UserName</span>
                                  <asp:TextBox CssClass="customerdivText form-control textboxColor" style="width: 90%;" runat="server" AutoCompleteType="Disabled"  ID="txtUserName"></asp:TextBox>

                            </div>
                            <hr class="divHrLine" />

                             <div class="divPaddingLeft">
                                <span class="customerdivHeading">Password</span>
                                 <%--<input  name="password" id="password" runat="server" AutoCompleteType="Disabled" class="customerdivText form-control textboxColor"/>--%>
                                  <asp:TextBox CssClass="customerdivText form-control textboxColor" style="width: 90%;" runat="server"  AutoCompleteType="Disabled"  ID="txtPassword"></asp:TextBox>

                            </div>
                            <hr class="divHrLine" />
                          
                            
                             <div class="divPaddingLeft">
                                <span class="customerdivHeading">CustomerType</span>
                                    <asp:DropDownList runat="server" ID="cboCustomerType" style="width: 90%;"  CssClass="form-control customerdivText textboxColor">
                        <asp:ListItem Text="" Value="-1"></asp:ListItem>
                        <asp:ListItem Text="FTG" Value="0"></asp:ListItem>
                        <asp:ListItem Text="PRV" Value="1"></asp:ListItem>
                    </asp:DropDownList>

                            </div>
                            <hr class="divHrLine" />
                              <div class="divPaddingLeft">
                                <span class="customerdivHeading">PersonalNumber</span>
                                    <asp:TextBox CssClass="customerdivText form-control textboxColor" style="width: 90%;" runat="server" AutoCompleteType="Disabled" ID="txtPersonalNumber"></asp:TextBox>
                            </div>
                            <hr class="divHrLine" />

                        </div>
                       
                         <div class="updateInfoButtonContainer text-center">
                          <asp:Button Text="Credit Check"  class="button panelButton form-control" style="width:250px;" runat="server" OnClick="btnCreditCheck_Click" ID="btnCreditCheck"/>
                        </div>



                    </div>





                    <div class="col-md-10 tableFixHead table-responsive customerTable" style="background-color: #2C3850;">

                        
                        <asp:GridView ID="grdCreditCheck" runat="server" EmptyDataRowStyle-CssClass="Emptyrow" AutoGenerateColumns="False" ViewStateMode="Enabled" Visible="true" Style="color: white; font-size: small;" ShowHeaderWhenEmpty="true" CssClass="table">
                            <HeaderStyle BackColor="#475672" />
                            <Columns>
                                 <asp:BoundField DataField="Name" HeaderStyle-CssClass="Notesalign" ItemStyle-CssClass="Notesalign" HeaderText="Name" SortExpression="Name" />
                                 <asp:BoundField DataField="Address" HeaderStyle-CssClass="Notesalign" ItemStyle-CssClass="Notesalign" HeaderText="Address" SortExpression="Address" />
                                 <asp:BoundField DataField="Status" HeaderStyle-CssClass="Notesalign" ItemStyle-CssClass="Notesalign" HeaderText="Status" SortExpression="Status" />
                                    <asp:BoundField DataField="Error" HeaderStyle-CssClass="Notesalign" ItemStyle-CssClass="Notesalign" HeaderText="Reject Code" SortExpression="Error" />
                                 <%--<asp:BoundField DataField="ErrorMessage" HeaderStyle-CssClass="Notesalign" ItemStyle-CssClass="Notesalign" HeaderText="Reject Text" SortExpression="ErrorMessage" />--%>
                                     <asp:BoundField DataField="City" HeaderStyle-CssClass="Notesalign" ItemStyle-CssClass="Notesalign" HeaderText="City" SortExpression="City" />
                                     <asp:BoundField DataField="PostalCode" HeaderStyle-CssClass="Notesalign" ItemStyle-CssClass="Notesalign" HeaderText="Postal Code" SortExpression="PostalCode" />
                                   

                                   
                                  
                            </Columns>
                            <EmptyDataTemplate>No Record Available</EmptyDataTemplate>
                        </asp:GridView>
                      
                     
                      
                    </div>



                </div>
             

               




            </div>

          
          

         

        </div>
        </div>
        </asp:Panel>
</asp:Content>

