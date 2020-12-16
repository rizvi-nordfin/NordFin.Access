<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Nordfin.Master" CodeBehind="frmTeleson.aspx.cs" Inherits="Nordfin.frmTeleson" %>

<asp:Content ID="Content1" ContentPlaceHolderID="NordfinContentHolder" runat="server" >
       <script src="https://cdnjs.cloudflare.com/ajax/libs/Chart.js/2.4.0/Chart.min.js"></script>
    <script src="Scripts/jsTelsonGroup.js?version=<%=ConfigurationManager.AppSettings["VersionConfiguration"].ToString() %>"></script>
      <link href="Styles/LoginInformation.css?version=<%=ConfigurationManager.AppSettings["VersionConfiguration"].ToString() %>" rel="stylesheet" />
    <link href="Styles/AccountSettings.css?version=<%=ConfigurationManager.AppSettings["VersionConfiguration"].ToString() %>" rel="stylesheet" />
     <link href="Styles/TelsonGroup.css?version=<%=ConfigurationManager.AppSettings["VersionConfiguration"].ToString() %>" rel="stylesheet" />
    <script src="Scripts/jquery.jqChart.min.js"></script>
    <link href="Styles/jquery.jqChart.css" rel="stylesheet" />

    <script src="Scripts/jquery-1.11.1.min.js"></script>
    <style>
      .revenuelabelBody{
          font-size:30px;
          line-height:2em;
      }
        .revenuelabelHeader {
            text-transform:uppercase;
            color: #A9BFD5;

        }
        .divPaddingLeft{
              text-transform:uppercase;
            
        }
        .dashboardTable {
            height:auto;
            margin-top:5px;
        }
        .dashboardTable tr {
    height: 45.5px;
}
        </style>
        <div class="dashboardContainer">
        <div class="container-fluid">
            <div class="dashboardHeader">
                <div class="dashboardHeadline">Dashboard</div>
            </div>

              <div data-ng-app="myApp" data-ng-controller="myCtrl">
                <div class="row " style="margin-left: 0px;">

                      <div class="col-md-4 divPadding customerInfoSidebar" style="color: #FFFFFF;">

                         


                            <div class="divPaddingLeft" style="background-color: #475672">
                                <span class="customerdivHeading">Teleson Clients</span>

                                <asp:Label CssClass="customerdivText"  runat="server" ID="lblFirstName"></asp:Label>

                                  <hr class="divHrLine" />
                            </div>

                           <div class="row innerDiv col-md-12" style="margin:0px;">
                               <div class="col-md-6" data-ng-repeat="telson in telsonData|limitTo:3">

                                    <div class="revenue">

                                        <div>
                                            <asp:Label Text="{{telson.ColumnName}}" class="revenuelabelHeader" runat="server" /></div>
                                        <div>
                                            <asp:Label ID="lblTotalSentoutAmount" class="revenuelabelBody" Text="{{telson.RowValue}}{{telson.SpecialCharc}}" runat="server" /></div>

                                    </div>
                                  
                               </div>

                                </div>

                          <div class="row innerDiv col-md-12" style="margin:0px;margin-top:10px;">
                               <div class="col-md-6" data-ng-repeat="telson in telsonData|limitTo:4:3">

                                    <div class="revenue">

                                        <div>
                                            <asp:Label Text="{{telson.ColumnName}}" class="revenuelabelHeader" runat="server" /></div>
                                        <div>
                                            <asp:Label ID="Label1" class="revenuelabelBody" Text="{{telson.RowValue}}{{telson.SpecialCharc}}" runat="server" /></div>

                                    </div>
                               </div>

                                </div>

                        
                            
                         
                              </div>

                       

                      <div class="ml-1 col-md-7" style="color: #FFFFFF;background: #38445d;">
                             <div class="row">

                        <div class="col-md-12 col-lg-12 jqdivChart">

                            <div id="jqChart1" class="jquiChart" ></div>
                        </div>

                    </div>
                        </div>
                       

                          </div>

                    <div class="row " style="margin-left: 0px;">

                        <div class="col-md-4 divPadding customerInfoSidebar" style="color: #FFFFFF;">

                               <div class="row innerDiv col-md-12" style="margin:0px;margin-top:5px">
                               <div class="col-md-6" data-ng-repeat="telson in telsonData|limitTo:2:7">

                                    <div class="revenue">

                                        <div>
                                            <asp:Label Text="{{telson.ColumnName}}" class="revenuelabelHeader" runat="server" /></div>
                                        <div>
                                            <asp:Label ID="Label2" class="revenuelabelBody" Text="{{telson.RowValue}}{{telson.SpecialCharc}}" runat="server" /></div>

                                    </div>
                                  
                               </div>

                                </div>

                             <div class="row innerDiv col-md-12" style="margin:0px;margin-top:10px;">
                               <div class="col-md-6" data-ng-repeat="telson in telsonData|limitTo:2:9">

                                    <div class="revenue">

                                        <div>
                                            <asp:Label Text="{{telson.ColumnName}}" class="revenuelabelHeader" runat="server" /></div>
                                        <div>
                                            <asp:Label ID="Label3" class="revenuelabelBody" Text="{{telson.RowValue}}{{telson.SpecialCharc}}" runat="server" /></div>

                                    </div>
                                  
                               </div>

                                </div>
                        </div>


                        <div class="col-md-4">
                      
                            <div class="dashboardTable" style="margin-left: -6px;">
                                    <table>
                                        <tbody>
                                           
                                            <tr data-ng-repeat="telson in telsonData|limitTo:5:11">
                                                <td class="tableLeftAlign" style="color: #A9BFD5;">
                                                    <span id="NordfinContentHolder_lblSentout">{{telson.ColumnName}}</span></td>
                                                <td>
                                                    <span id="NordfinContentHolder_lblSentoutNumber"></span></td>
                                            
                                                <td>
                                                    <span id="NordfinContentHolder_lblSentoutPercent">{{telson.RowValue}}{{telson.SpecialCharc}}</span></td>
                                            </tr>
                                           
                                           

                                           
                                        </tbody>
                                    </table>
                                </div>

                        </div>
                        </div>

                  
                    </div>
                 </div>

        </div>    
       

    </asp:Content>