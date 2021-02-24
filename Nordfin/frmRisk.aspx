<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Nordfin.Master" CodeBehind="frmRisk.aspx.cs" Inherits="Nordfin.frmRisk" %>

<asp:Content ID="Content1" ContentPlaceHolderID="NordfinContentHolder" runat="server">
    <script src="https://cdnjs.cloudflare.com/ajax/libs/Chart.js/2.4.0/Chart.min.js"></script>
    <script src="Scripts/jsTelsonGroup.js?version=<%=ConfigurationManager.AppSettings["VersionConfiguration"].ToString() %>"></script>
    <%--<link href="Styles/LoginInformation.css?version=<%=ConfigurationManager.AppSettings["VersionConfiguration"].ToString() %>" rel="stylesheet" />--%>
    <link href="Styles/AccountSettings.css?version=<%=ConfigurationManager.AppSettings["VersionConfiguration"].ToString() %>" rel="stylesheet" />
    <link href="Styles/TelsonGroup.css?version=<%=ConfigurationManager.AppSettings["VersionConfiguration"].ToString() %>" rel="stylesheet" />
    <script src="Scripts/jquery.jqChart.min.js?version=<%=ConfigurationManager.AppSettings["VersionConfiguration"].ToString()%> "></script>
    <link href="Styles/jquery.jqChart.css" rel="stylesheet" />
    <%--//2c3e64--%>
    <script src="Scripts/jquery-1.11.1.min.js"></script>
    <style>
        .revenuelabelBody {
            font-size: 23px;
            line-height: 2em;
        }

        .revenuelabelHeader {
            text-transform: uppercase;
            color: #A9BFD5;
        }

        .divPaddingLeft {
            text-transform: uppercase;
        }

        .dashboardTable {
            height: auto;
            margin-top: 5px;
        }
    </style>

    <div class="dashboardContainer">
        <div class="container-fluid">
            <div class="dashboardHeader">
                <div class="dashboardHeadline" style="margin-left: 9px;">Dashboard</div>
                <div data-ng-app="myApp" data-ng-controller="myCtrl">

                    <div class="row " style="margin-left: 0px;">
                        <div class="col-lg-4 col-md-12 divPadding customerInfoSidebar" style="color: #FFFFFF; display: none;" id="divTelson">


                              <div class="divPaddingLeft" style="background-color: #475672">
                                <span class="customerdivHeading">{{ClientName}}</span>

                                <asp:Label CssClass="customerdivText"  runat="server" ID="lblFirstName"></asp:Label>

                                  <hr class="divHrLine" />
                            </div>

                           <div class="row innerDiv col-md-12 m-0" >
                               <div class="col-md-6" data-ng-repeat="telson in telsonData|limitTo:6">

                                    <div class="revenue">

                                        <div>
                                            <asp:Label Text="{{telson.ColumnName}}" class="revenuelabelHeader" runat="server" /></div>
                                        <div>
                                            <asp:Label ID="lblTotalSentoutAmount" class="revenuelabelBody" Text="{{telson.RowValue}}{{telson.SpecialCharc}}" runat="server" /></div>

                                    </div>
                                  
                               </div>

                                </div>

                              <div class="row innerDiv col-md-12 mt-2 m-0" >
                               <div class="col-md-6" data-ng-repeat="telson in telsonData|limitTo:8:6">

                                    <div class="revenue">

                                        <div>
                                            <asp:Label Text="{{telson.ColumnName}}" class="revenuelabelHeader" runat="server" /></div>
                                        <div>
                                            <asp:Label ID="Label1" class="revenuelabelBody" Text="{{telson.ColorCode}}{{telson.RowValue}}{{telson.SpecialCharc}}" runat="server" /></div>

                                    </div>
                                  
                               </div>

                                </div>

                             <div class="row innerDiv col-md-12 mt-2 m-0" style="padding:2px;" >
                               <div class="col-md-6" data-ng-repeat="telson in telsonData|limitTo:6:14">

                                    <div class="revenue">

                                        <div>
                                            <asp:Label Text="{{telson.ColumnName}}" class="revenuelabelHeader" runat="server" /></div>
                                        <div>
                                            <asp:Label ID="Label2" class="revenuelabelBody" Text="{{telson.RowValue}}{{telson.SpecialCharc}}" runat="server" /></div>

                                    </div>
                                  
                               </div>

                                </div>

                             <div class="row col-md-12 mt-2" style="margin-left:-9px"  >
                                 <div class="w-100">
                                 <div style="background-color: #475672;    width: 103.5%;">
                                    <table>
                                           <tbody>
                                         <tr style="text-align:center;font-size: small;color: #A9BFD5;font-weight:400 !important;height: 37px;margin-top: 10px;" >

                                                 
                                                <td colspan="3">CREDIT SCORING</td>
                                            </tr>
                                               <tbody>
                                    </table>
                                </div>
                                <div class="dashboardTable" style="   width: 103.5%;">
                                    <table>
                                        
                                        <tbody>
                                          
                                            <tr data-ng-repeat="telson in telsonData|limitTo:3:20">
                                                <td class="tableLeftAlign" style="color: #A9BFD5;">
                                                    <span >{{telson.ColumnName}}</span></td>
                                               
                                                <td>
                                                    <span >{{telson.StaticValue}}</span>
                                                   </td>

                                                  <td>
                                                       <span >{{telson.RowValue}} {{telson.SpecialCharc}}</span>
                                                    </td>
                                            </tr>
                                           
                                           

                                           
                                        </tbody>
                                    </table>
                                    </div>
                              
                                     </div>
                                </div>
                        </div>



                        <div class="col-lg-8  col-md-12  divPadding customerInfoSidebar marginleft" style="display: none;"  id="divPayment" >

                            <div class="row col-md-12" >
                                <div class="col-md-12">
                                <div class="col-md-12 col-lg-12 jqdivChart" id="divChart" style="margin-left: -16px;">

                                    <div id="jqChart1" class="jquiChart"></div>
                                </div>
                                    </div>

                            </div>

                             <div class="row col-md-12" style="margin-top: -6px;">
                                 <div class="col-md-12 col-lg-3" >


                                     <div  data-ng-repeat="telson in telsonData|limitTo:1:23" class="heightDiv"
                                style="background-color: #38445D;display:flex;
                          align-items:center;justify-content: center; flex-direction: column;margin-left: -15px;">
                                

                                    <asp:Label Text="{{telson.ColumnName}}" style="font-size:18px;" class="revenuelabelHeader" runat="server" />
                                    
                                    <asp:Label ID="Label5" class="revenuelabelBody" Text="{{telson.RowValue}}" runat="server" />
                                
                                   </div>
                            
                                 </div>
                                   <div class="col-lg-3 col-md-12" style="margin-left: -17px;">

                                <div data-ng-repeat="telson in telsonData|limitTo:1:24">
                                    <div class="col-md-12 heightPaid widthPaid" data-ng-style="telson.ColorCode != '' && {'background-color':'{{telson.ColorCode}}'} || {'background-color': '#38445D'}"
                                        
                                        style=" display:flex;align-items:center;flex-direction:column;justify-content:center">
                                        <asp:Label Text="{{telson.ColumnName}}" style="font-size:18px;" class="revenuelabelHeader" runat="server" />

                                        <asp:Label ID="Label3" class="revenuelabelBody" Text="{{telson.RowValue}}{{telson.SpecialCharc}}" runat="server" />
                                    </div>
                                    <div class="col-md-12 widthPaid " style="background: #3e4c6a; margin-top: -15px;text-align:center">

                                        <asp:Label ID="Label7" class="revenuelabelBody" style="font-size:16px;" Text="{{telson.PaymentValue}}" runat="server" />
                                    </div>
                                </div>
                                <div data-ng-repeat="telson in telsonData|limitTo:1:25">
                                    <div class="col-md-12 heightUnpaid widthPaid" data-ng-style="telson.ColorCode != '' && {'background-color':'{{telson.ColorCode}}'} || {'background-color': '#38445D'}"
                                        style="display:flex;align-items:center;flex-direction:column;justify-content:center">
                                         <asp:Label Text="{{telson.ColumnName}}" style="font-size:18px;" class="revenuelabelHeader" runat="server" />

                                        <asp:Label ID="Label8" class="revenuelabelBody" Text="{{telson.RowValue}}{{telson.SpecialCharc}}" runat="server" />

                                    </div>

                                    <div class="col-md-12 widthPaid " style="background: #3e4c6a; margin-top: -15px;text-align:center">
                                         <asp:Label ID="Label9" class="revenuelabelBody" style="font-size:16px;" Text="{{telson.PaymentValue}}" runat="server" />
                                    </div>
                                </div>

                            
                                       
                                 </div>
                                   <div class="col-md-3" style="font-size: 19px;">


                                 <div data-ng-repeat="telson in telsonData|limitTo:5:26" >
                                    <div data-ng-style="telson.ColorCode != '' && {'background-color':'{{telson.ColorCode}}'} || {'background-color': '#38445D'}" 
                                        style="margin-left: -18px;">
                                <div class="col-md-12 container"  style="text-align: center;">

                                    <div>

                                        <div>
                                            <asp:Label Text="{{telson.ColumnName}}" class="revenuelabelHeader" runat="server" />
                                        </div>
                                        <div>
                                            <asp:Label class="revenuelabelBody" Text="{{telson.RowValue}}{{telson.SpecialCharc}}" runat="server" />
                                        </div>

                                    </div>

                                </div>

                                  <div class="col-md-12" style="background:#3e4c6a;text-align:center ">
                                       <asp:Label ID="Label10" class="revenuelabelBody" style="font-size:16px;" Text="{{telson.PaymentValue}}" runat="server" />
                                </div>

                                        </div>
                            </div>
                                 </div>
                                   <div class="col-md-3" style="font-size: 19px;">


                               
                                          <div data-ng-repeat="telson in telsonData|limitTo:5:31">
                            <div data-ng-if="telson.ColumnName!='None'"  data-ng-style="telson.ColorCode != '' && {'background-color':'{{telson.ColorCode}}'} || {'background-color': '#38445D'}"
                                style="margin-left: -18px; ">
                                <div class="col-md-12 container"  style="text-align: center;">

                                    <div>

                                        <div>
                                            <asp:Label Text="{{telson.ColumnName}}" class="revenuelabelHeader" runat="server" />
                                        </div>
                                        <div>
                                            <asp:Label class="revenuelabelBody" Text="{{telson.RowValue}} {{telson.SpecialCharc}}" runat="server" />
                                        </div>

                                    </div>

                                </div>

                                  <div class="col-md-12" style="background:#3e4c6a;text-align:center">
 <asp:Label ID="Label12" class="revenuelabelBody" style="font-size:16px;" Text="{{telson.PaymentValue}}" runat="server" />
                                </div>
                            </div>

                                        <div data-ng-if="telson.ColumnName=='None'"   
                                            style="margin-left: -4px; background-color: #2C3850;" class="visibleDiv">
                                <div class="col-md-12 container"  style="text-align: center;">

                                    <div>

                                        <div>
                                            <asp:Label Text="{{telson.ColumnName}}" class="revenuelabelHeader" runat="server" />
                                        </div>
                                        <div>
                                            <asp:Label class="revenuelabelBody" Text="{{telson.RowValue}} {{telson.SpecialCharc}}" runat="server" />
                                        </div>

                                    </div>

                                </div>

                                  <div class="col-md-12" style="background:#3e4c6a; text-align:center">
                                         <asp:Label ID="Label11" class="revenuelabelBody" style="font-size:16px;" Text="{{telson.PaymentValue}}" runat="server" />
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
