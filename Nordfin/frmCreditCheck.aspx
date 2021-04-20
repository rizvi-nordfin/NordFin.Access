<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="frmCreditCheck.aspx.cs" MasterPageFile="~/Nordfin.Master" Inherits="Nordfin.frmCreditCheck" %>
<asp:Content ID="Content1" ContentPlaceHolderID="NordfinContentHolder" runat="server"   style="background-color: #232D41;">
    <asp:Panel DefaultButton="btnCreditCheck" runat="server">
    <link href="Styles/AccountSettings.css?version=<%=ConfigurationManager.AppSettings["VersionConfiguration"].ToString() %>" rel="stylesheet" />
         <link href="Styles/CreditCheck.css?version=<%=ConfigurationManager.AppSettings["VersionConfiguration"].ToString() %>" rel="stylesheet" />
        <script src="Scripts/gauge.js?version=<%=ConfigurationManager.AppSettings["VersionConfiguration"].ToString() %>"></script>
    <script src="Scripts/jsCreditCheck.js?version=<%=ConfigurationManager.AppSettings["VersionConfiguration"].ToString() %>"></script>
       <%-- <script src="Scripts/gauge.min.js"></script>--%>
        <style>
         
               
    
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
                        <asp:ListItem Text="Corporate" Value="0"></asp:ListItem>
                        <asp:ListItem Text="Private" Value="1"></asp:ListItem>
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
                          <asp:Button Text="Credit Check"  class="button panelButton form-control" style="width:250px;" runat="server" OnClick="btnCreditCheck_Click" OnClientClick="return CreditCheck();" ID="btnCreditCheck"/>
                        </div>



                    </div>





                    <div class="ml-1 col-md-3 text-center" >
                        <div class="row headingDiv tableDiv overviewHeading" style="color:lightgreen;">
                           APPROVED
                        </div>
                        <div id="preview" style="background-color: #3e4a66;margin-top:-5px;">
                            <div style="margin-top:0px" >
                                <canvas width="400" height="350" id="demo"></canvas>
                                <div style="background:#38445D;height:42px;display: flex;justify-content: center;">
                                <div id="preview-textfield" style="font-size: 20px;font-weight: bold; color: #fff;margin-top: 8px;"></div>
                                    </div>
                            </div>
                        </div>

                 
                      
                     
                      
                    </div>


                    <div class="ml-1 col-md-2 divPadding customerInfoSidebar" style="color: #FFFFFF;">
                        <div style="background-color: #3E4B64;">


                            <div class="divPaddingLeft" style="background-color: #475672">
                                <asp:Label runat="server" CssClass="customerdivHeading" ID="lblCreditName">Name</asp:Label>

                                  <asp:Label CssClass="customerdivText ResultHeight" Text="" runat="server" ID="lblResultName">Muthusamy</asp:Label>

                                  <hr class="divHrLine" />
                            </div>

                              <div class="divPaddingLeft">
                                <span class="customerdivHeading">SSN/Reg.Number</span>
                                    <asp:Label CssClass="customerdivText ResultHeight" Text="" runat="server" ID="lblRegNumber"></asp:Label>

                            </div>
                            <hr class="divHrLine" />

                             <div class="divPaddingLeft">
                                <span class="customerdivHeading">Address</span>
                                 <%--<input  name="password" id="password" runat="server" AutoCompleteType="Disabled" class="customerdivText form-control textboxColor"/>--%>
                                 <asp:Label CssClass="customerdivText ResultHeight" Text="" runat="server" ID="lblAddress"></asp:Label>

                            </div>
                            <hr class="divHrLine" />
                          
                            
                           
                              <div class="divPaddingLeft">
                                <span class="customerdivHeading">PostalCode</span>
                                 <asp:Label CssClass="customerdivText ResultHeight" Text="" runat="server" ID="lblPostalCode"></asp:Label>
                            </div>
                            <hr class="divHrLine" />
                             <div class="divPaddingLeft">
                                <span class="customerdivHeading">City</span>
                                 <asp:Label CssClass="customerdivText ResultHeight" Text="" runat="server" ID="lblCity"></asp:Label>
                            </div>
                            <hr class="divHrLine" />
                               <div class="divPaddingLeft">
                                <span class="customerdivHeading">Country</span>
                                 <asp:Label CssClass="customerdivText ResultHeight" Text="" runat="server" ID="lblCountry"></asp:Label>
                            </div>
                            <hr class="divHrLine" />
                                <div class="divPaddingLeft">
                                <span class="customerdivHeading">Status</span>
                                 <asp:Label CssClass="customerdivText ResultHeight" Text="" runat="server" ID="lblStatus"></asp:Label>
                            </div>
                            <hr class="divHrLine" />
                               <div class="divPaddingLeft">
                                <span class="customerdivHeading">Reject Code</span>
                                 <asp:Label CssClass="customerdivText ResultHeight" Text="" runat="server" ID="lblRejectCode"></asp:Label>
                            </div>
                            <hr class="divHrLine" />

                        </div>
                    </div>

                </div>
             

               




            </div>

          
          

         

        </div>
        </div>
          <asp:HiddenField ID="hdnCreditScore" Value="0" runat="server" />
        </asp:Panel>
</asp:Content>

