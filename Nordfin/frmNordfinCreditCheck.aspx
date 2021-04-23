<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="frmNordfinCreditCheck.aspx.cs" MasterPageFile="~/NordfinCredit.Master" Inherits="Nordfin.frmNordfinCreditCheck" %>
<asp:Content ID="Content1" ContentPlaceHolderID="NordfinContentHolder" runat="server"   style="background-color: #232D41;">
    <asp:Panel DefaultButton="btnCreditCheck" runat="server">
    <link href="Styles/AccountSettings.css?version=<%=ConfigurationManager.AppSettings["VersionConfiguration"].ToString() %>" rel="stylesheet" />
         <link href="Styles/CreditCheck.css?version=<%=ConfigurationManager.AppSettings["VersionConfiguration"].ToString() %>" rel="stylesheet" />
        <script src="Scripts/gauge.js?version=<%=ConfigurationManager.AppSettings["VersionConfiguration"].ToString() %>"></script>
    <script src="Scripts/jsNordfinCreditCheck.js?version=<%=ConfigurationManager.AppSettings["VersionConfiguration"].ToString() %>"></script>
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


                    <div class="col-md-12 col-lg-3  divPadding customerInfoSidebar" style="color: #FFFFFF;">
                         <div class="row headingDiv tableDiv overviewHeading text-center" style="background-color: #475672;height:54px;font-size:16px;">
                                <asp:Label runat="server" style="text-transform:uppercase;" Text="Login" ID="lblClientName"></asp:Label>

                                

                                  <hr class="divHrLine" />
                            </div>
                        <div  style="background-color: #3E4B64;margin-top:-7px;">


                           

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
                             <hr class="divHrLine" style="border:.7px solid #3E4B64;" />
                           </div>
                        <div style="background-color: #3E4B64;margin-top:-2px;">
                            <div class="row headingDiv tableDiv overviewHeading text-center" style="background-color: #475672; height: 54px;">
                                <asp:Label runat="server" CssClass="customerdivHeading" Style="font-size: 16px;" ID="lblSearch" Text="Search"></asp:Label>



                                <hr class="divHrLine" />
                            </div>
                        </div>
                        <div style="background-color: #3E4B64;margin-top:-7px;">
                            <div class="divPaddingLeft">
                                <span class="customerdivHeading">CustomerType</span>
                                <asp:DropDownList runat="server" ID="cboCustomerType" Style="width: 90%;" CssClass="form-control customerdivText textboxColor">
                                    <asp:ListItem Text="" Value="-1"></asp:ListItem>
                                    <asp:ListItem Text="Corporate" Value="0"></asp:ListItem>
                                    <asp:ListItem Text="Private" Value="1"></asp:ListItem>
                                </asp:DropDownList>

                            </div>
                            <hr class="divHrLine" />
                            <div class="divPaddingLeft">
                                <span class="customerdivHeading">Org./PersonalNumber</span>
                                <asp:TextBox CssClass="customerdivText form-control textboxColor" Style="width: 90%;" runat="server" AutoCompleteType="Disabled" ID="txtPersonalNumber"></asp:TextBox>
                            </div>
                            <hr class="divHrLine" />



                            <div class="updateInfoButtonContainer text-center">
                                <asp:Button Text="Credit Check" class="button panelButton form-control" Style="width: 250px; margin-top: -7px;" runat="server" OnClick="btnCreditCheck_Click" OnClientClick="return CreditCheck();" ID="btnCreditCheck" />
                            </div>

                        </div>

                    </div>





                    <div class="col-md-12 col-lg-4 text-center creditMeterMl"  >
                        <asp:Panel runat="server" ID="pnlStatus"  CssClass="row headingDiv tableDiv overviewHeading w-mb" >
                              <asp:Label runat="server" ID="lblResultStatus" Text="STATUS"></asp:Label>
                        </asp:Panel>
                        <div id="preview"  class="w-mb"  style="background-color: #3e4a66;margin-top:-7px;">
                            <div style="margin-top:0px" >
                                <canvas class="creditMeter" id="demo"></canvas>
                                <div style="background:#38445D;height:73px;display: flex;justify-content: center;margin-top:-14px;">
                                <div id="preview-textfield" style="font-size: 30px;color: #A9BFD5;margin-top: 15px;"></div>
                                    </div>
                            </div>
                        </div>

                 
                      
                     
                      
                    </div>


                    <div class="col-md-12 col-lg-3  divPadding customerInfoSidebar mt-mb" style="color: #FFFFFF;">
                        <div style="background-color: #3E4B64;">


                            <div class="divPaddingLeft" style="background-color: #475672">
                                <asp:Label runat="server" CssClass="customerdivHeading" ID="lblCreditName">Name</asp:Label>

                                <asp:Label CssClass="customerdivText ResultMainHeadingHeight" Text="" runat="server" ID="lblResultName"></asp:Label>

                                <hr class="divHrLine" />
                            </div>
                        </div>
                        <div style="background-color: #3E4B64;margin-top:-2px;">
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
                                <asp:Label CssClass="customerdivText ResultLastHeight" Text="" runat="server" ID="lblRejectCode"></asp:Label>
                            </div>
                            <hr class="divHrLine" />

                        </div>
                    </div>

                </div>
             

               




            </div>

          
          

         

        </div>
        </div>
        
        </asp:Panel>

  <%--   <div class="featureNotAvailableBG hidden">
        <div class="featureNotAvailable">
            <div class="featureNotAvailableX">✕</div>
           In order to use the credit scoring function please contact your nordfin contact
        </div>
    </div>--%>
</asp:Content>

