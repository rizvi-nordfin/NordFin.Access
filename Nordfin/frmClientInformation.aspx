<%@ Page Language="C#" AutoEventWireup="true"  MasterPageFile="~/Nordfin.Master" CodeBehind="frmClientInformation.aspx.cs" Inherits="Nordfin.frmClientInformation" %>
<asp:Content ID="Content1" ContentPlaceHolderID="NordfinContentHolder" runat="server">
       <link href="Styles/AccountSettings.css?version=<%=ConfigurationManager.AppSettings["VersionConfiguration"].ToString() %>" rel="stylesheet" />
    <script src="Scripts/jsClientInformation.js?version=<%=ConfigurationManager.AppSettings["VersionConfiguration"].ToString() %>"></script>
      <script src="https://cdnjs.cloudflare.com/ajax/libs/Chart.js/2.4.0/Chart.min.js"></script> 
       <div data-ng-app="myApp" data-ng-controller="myCtrl">
    <div class="dashboardContainer">
        <div class="container-fluid">

            <div class="dashboardHeader">
                <div class="dashboardHeadline"><asp:Label runat="server" ID="lblClientName"></asp:Label></div>
            </div>
            <div>
                   <div class="row " style="height: 50px; margin-left: 0px;">


                       <div class="col-md-2 divPadding customerInfoSidebar" style="color: #FFFFFF;">
                           <div style="background-color: #3E4B64;">


                               <div class="divPaddingLeft" style="background-color: #475672">
                                   <span class="customerdivHeading">FINANCE</span>

                                   <asp:Label CssClass="customerdivText" runat="server" ID="lblFinance"></asp:Label>

                                   <hr class="divHrLine" />
                               </div>




                               <div class="divPaddingLeft">
                                   <span class="customerdivHeading">TOTAL INVOICES SECURED</span>

                                   <asp:Label CssClass="customerdivText" runat="server" ID="lblTotalInvoices"></asp:Label>


                               </div>
                               <hr class="divHrLine" />
                               <div class="divPaddingLeft">
                                   <span class="customerdivHeading">% FINANCED</span>

                                   <asp:Label CssClass="customerdivText" runat="server" ID="lblPercentFinanced"></asp:Label>

                               </div>
                               <hr class="divHrLine" />

                                  <div class="divPaddingLeft">
                                   <span class="customerdivHeading">REG.NUMBER</span>

                                   <asp:Label CssClass="customerdivText" runat="server" ID="lblRegNumber"></asp:Label>

                               </div>
                               <hr class="divHrLine" />


                               <div class="divPaddingLeft">
                                   <span class="customerdivHeading">ADDRESS</span>

                                   <asp:Label CssClass="customerdivText" runat="server" ID="lblAddress"></asp:Label>

                               </div>
                               <hr class="divHrLine" />
                              <div class="divPaddingLeft">
                                   <span class="customerdivHeading">POSTAL CODE</span>

                                   <asp:Label CssClass="customerdivText" runat="server" ID="lblPostalCode"></asp:Label>

                               </div>
                               <hr class="divHrLine" />


                                 <div class="divPaddingLeft">
                                   <span class="customerdivHeading">CITY</span>

                                   <asp:Label CssClass="customerdivText" runat="server" ID="lblCity"></asp:Label>

                               </div>
                               <hr class="divHrLine" />


                                 <div class="divPaddingLeft">
                                   <span class="customerdivHeading">COUNTRY</span>

                                   <asp:Label CssClass="customerdivText" runat="server" ID="lblCountry"></asp:Label>

                               </div>
                               <hr class="divHrLine" />



                                  <div class="divPaddingLeft">
                                   <span class="customerdivHeading">EMAIL</span>

                                   <asp:Label CssClass="customerdivText" runat="server" ID="lblEMail"></asp:Label>

                               </div>
                               <hr class="divHrLine" />

                                

                           </div>

                           <div class="updateInfoButtonContainer">
                           </div>



                       </div>
                       <div class="col-md-3">
                           <div class="chart-container" style="height:500px;padding-top:30px;" id="divChart">
                               <canvas id="myChart"></canvas>
                           </div>
                       </div>

                        <div class="col-md-7">
                           
                       </div>


                   </div>
            </div>
        </div>
    </div>
        </div>
</asp:Content>
