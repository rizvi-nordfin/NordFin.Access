<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Nordfin.Master" CodeBehind="frmTelecom.aspx.cs" Inherits="Nordfin.frmTelecom" %>

<asp:Content ID="Content1" ContentPlaceHolderID="NordfinContentHolder" runat="server">
    <script src="https://cdnjs.cloudflare.com/ajax/libs/Chart.js/2.4.0/Chart.min.js"></script>
    <script src="Scripts/jsTelecom.js?version=<%=ConfigurationManager.AppSettings["VersionConfiguration"].ToString() %>"></script>
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
        .jquiChart {
   
    height: 667px !important;
}
    </style>
    <div class="dashboardContainer">
        <div class="container-fluid">
            <div class="dashboardHeader">
                <div class="dashboardHeadline" style="margin-left: 9px;">Telecom</div>

                <div data-ng-app="myApp" data-ng-controller="myCtrl">
                       <div class="row " style="margin-left: 0px;">
                             <div class="col-lg-4 col-md-12 divPadding customerInfoSidebar" style="color: #FFFFFF;" id="divTelson">


                              <div class="divPaddingLeft" style="background-color: #475672">
                                <span class="customerdivHeading">Telecom</span>

                                <asp:Label CssClass="customerdivText"  runat="server" ID="lblFirstName"></asp:Label>

                                  <hr class="divHrLine" />
                            </div>

                           <div class="row innerDiv col-md-12 m-0" >
                               <div class="col-md-6" data-ng-repeat="telson in telsonData|limitTo:4">

                                    <div class="revenue">

                                        <div>
                                            <asp:Label Text="{{telson.ColumnName}}" class="revenuelabelHeader" runat="server" /></div>
                                        <div>
                                            <asp:Label ID="lblTotalSentoutAmount" class="revenuelabelBody" Text="{{telson.RowValue}}{{telson.SpecialCharc}}" runat="server" /></div>


                                    </div>
                                  
                               </div>

                                </div>

                              <div class="row innerDiv col-md-12 mt-2 m-0" >
                               <div class="col-md-6" data-ng-repeat="telson in telsonData|limitTo:4:4">

                                    <div class="revenue">

                                        <div>
                                            <asp:Label Text="{{telson.ColumnName}}" class="revenuelabelHeader" runat="server" /></div>
                                        <div  data-ng-if="telson.SpecialCharc=='-'||telson.SpecialCharc=='+' ">
                                            <asp:Label ID="Label1" class="revenuelabelBody" Text="{{telson.SpecialCharc}}{{telson.RowValue}}" runat="server" /></div>
                                            <div  data-ng-if="telson.SpecialCharc!='-'&&telson.SpecialCharc!='+' ">
                                            <asp:Label ID="Label3" class="revenuelabelBody" Text="{{telson.RowValue}}{{telson.SpecialCharc}}" runat="server" /></div>

                                    </div>
                                  
                               </div>

                                </div>

                             <div class="row innerDiv col-md-12 mt-2 m-0" style="padding:2px;" >
                               <div class="col-md-6" data-ng-repeat="telson in telsonData|limitTo:4:8">

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
                                          
                                            <tr data-ng-repeat="telson in telsonData|limitTo:3:12">
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


                           <div class="col-lg-8  col-md-12  divPadding customerInfoSidebar marginleft" id="divPayment">

                               <div class="row col-md-12">
                                   <div class="col-md-12">
                                       <div class="col-md-12 col-lg-12 jqdivChart" id="divChart" style="margin-left: -16px;background:rgb(56, 68, 93);">

                                           <div id="jqChart1" class="jquiChart"></div>
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
