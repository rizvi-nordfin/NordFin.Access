<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Nordfin.Master" CodeBehind="frmTeleson.aspx.cs" Inherits="Nordfin.frmTeleson" %>

<asp:Content ID="Content1" ContentPlaceHolderID="NordfinContentHolder" runat="server" >
       <script src="https://cdnjs.cloudflare.com/ajax/libs/Chart.js/2.4.0/Chart.min.js"></script>
    <script src="Scripts/jsTelsonGroup.js?version=<%=ConfigurationManager.AppSettings["VersionConfiguration"].ToString() %>"></script>
      <link href="Styles/LoginInformation.css?version=<%=ConfigurationManager.AppSettings["VersionConfiguration"].ToString() %>" rel="stylesheet" />
    <link href="Styles/AccountSettings.css?version=<%=ConfigurationManager.AppSettings["VersionConfiguration"].ToString() %>" rel="stylesheet" />
     <link href="Styles/TelsonGroup.css?version=<%=ConfigurationManager.AppSettings["VersionConfiguration"].ToString() %>" rel="stylesheet" />
    <script src="Scripts/jquery.jqChart.min.js?version=<%=ConfigurationManager.AppSettings["VersionConfiguration"].ToString()%> "></script>
    <link href="Styles/jquery.jqChart.css" rel="stylesheet" />
    <%--//2c3e64--%>
    <script src="Scripts/jquery-1.11.1.min.js"></script>
    <style>
      .revenuelabelBody{
          font-size:25px;
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
  
        </style>
        <div class="dashboardContainer" >
        <div class="container-fluid">
            <div class="dashboardHeader">
                <div class="dashboardHeadline">Dashboard</div>
            </div>

              <div data-ng-app="myApp" data-ng-controller="myCtrl" >
                <div class="row " style="margin-left: 0px;">

                      <div class="col-md-4 divPadding customerInfoSidebar" style="color: #FFFFFF;display:none;" id="divTelson" >

                         


                            <div class="divPaddingLeft" style="background-color: #475672">
                                <span class="customerdivHeading">{{ClientName}}</span>

                                <asp:Label CssClass="customerdivText"  runat="server" ID="lblFirstName"></asp:Label>

                                  <hr class="divHrLine" />
                            </div>

                           <div class="row innerDiv col-md-12" style="margin:0px;">
                               <div class="col-md-6" data-ng-repeat="telson in telsonData|limitTo:4">

                                    <div class="revenue">

                                        <div>
                                            <asp:Label Text="{{telson.ColumnName}}" class="revenuelabelHeader" runat="server" /></div>
                                        <div>
                                            <asp:Label ID="lblTotalSentoutAmount" class="revenuelabelBody" Text="{{telson.RowValue}}{{telson.SpecialCharc}}" runat="server" /></div>

                                    </div>
                                  
                               </div>

                                </div>

                          <div class="row innerDiv col-md-12" style="margin:0px;margin-top:10px;">
                               <div class="col-md-6" data-ng-repeat="telson in telsonData|limitTo:4:4">

                                    <div class="revenue">

                                        <div>
                                            <asp:Label Text="{{telson.ColumnName}}" class="revenuelabelHeader" runat="server" /></div>
                                        <div>
                                            <asp:Label ID="Label1" class="revenuelabelBody" Text="{{telson.RowValue}}{{telson.SpecialCharc}}" runat="server" /></div>

                                    </div>
                               </div>

                                </div>

                        
                            
                         
                              </div>

                       

                      <div class="ml-1 col-md-8" style="color: #FFFFFF;" id="divChart">
                             <div class="row" >

                        <div class="col-md-12 col-lg-12 jqdivChart">

                            <div id="jqChart1" class="jquiChart" ></div>
                        </div>

                    </div>
                        </div>
                       

                          </div>

                    <div class="row " style="margin-left: 0px;display:none" id="divPayment" >

                        <div class="col-md-4 divPadding customerInfoSidebar" style="color: #FFFFFF;">
                            <div class="innerDiv" style="display:flex; margin-top: 5px;flex-direction:row;justify-content:center;align-items:center;height:32%">
                            <div class="row col-md-12" >
                                <div class="col-md-6" data-ng-repeat="telson in telsonData|limitTo:4:8">

                                    <div class="revenue">

                                        <div>
                                            <asp:Label Text="{{telson.ColumnName}}" class="revenuelabelHeader" runat="server" />
                                        </div>
                                        <div>
                                            <asp:Label ID="Label2" class="revenuelabelBody" Text="{{telson.RowValue}}{{telson.SpecialCharc}}" runat="server" />
                                        </div>

                                    </div>

                                </div>

                            </div>
</div>
                             <div class="innerDiv" style="display:flex; margin-top: 10px;flex-direction:row;justify-content:center;align-items:center;height:32%">
                            <div class="row col-md-12" >
                                <div class="col-md-6" data-ng-repeat="telson in telsonData|limitTo:2:12">

                                    <div class="revenue">

                                        <div>
                                            <asp:Label Text="{{telson.ColumnName}}" class="revenuelabelHeader" runat="server" />
                                        </div>
                                        <div>
                                            <asp:Label ID="Label3" class="revenuelabelBody" Text="{{telson.RowValue}}{{telson.SpecialCharc}}" runat="server" />
                                        </div>

                                    </div>

                                </div>

                            </div>
                                 </div>
                              <div class="col-md-12" style="margin-left: -6px;margin-top: 10px;">
                           


                                    <div  style="margin-left: -6px;">
                                 <div style="background-color: #475672;margin-top: 5px;width: 103.3%;">
                                    <table>
                                           <tbody>
                                         <tr style="text-align:center;font-size: small;color: #A9BFD5;font-weight:400 !important;height: 37px;margin-top: 10px;" >

                                                 
                                                <td colspan="3">CREDIT SCORING</td>
                                            </tr>
                                               <tbody>
                                    </table>
                                </div>
                                <div class="dashboardTable" style="height: 139px;width: 103.3%;">
                                    <table>
                                        
                                        <tbody>
                                          
                                            <tr data-ng-repeat="telson in telsonData|limitTo:3:14">
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
                              <%--  <div class="col-md-6" data-ng-repeat="telson in telsonData|limitTo:2:13">

                                    <div class="revenue">

                                        <div>
                                            <asp:Label Text="{{telson.ColumnName}}" class="revenuelabelHeader" runat="server" />
                                        </div>
                                        <div>
                                            <asp:Label ID="Label4" class="revenuelabelBody" Text="{{telson.StaticValue}}" runat="server" />
                                        </div>

                                    </div>

                                </div>--%>

                          
                                  </div>
                        </div>


                        <div class="col-md-2" >
                            <div class="col-md-12 container" data-ng-repeat="telson in telsonData|limitTo:1:17" 
                                style="margin-top: 5px; height: 98.5%; background-color: #38445D;display:flex;
                          align-items:center;justify-content: center; flex-direction: column;margin-left: -6px;">

                               
                                

                                    <asp:Label Text="{{telson.ColumnName}}" style="font-size:18px;" class="revenuelabelHeader" runat="server" />
                                    
                                    <asp:Label ID="Label5" class="revenuelabelBody" Text="{{telson.RowValue}}" runat="server" />
                                
                                   
                            </div>
                          

                        </div>

                        <div class="col-md-2">
                            <div style="margin-left: -13px; margin-top: 5px; ">
                                <div data-ng-repeat="telson in telsonData|limitTo:1:18">
                                    <div class="col-md-12" data-ng-style="telson.ColorCode != '' && {'background-color':'{{telson.ColorCode}}'} || {'background-color': '#38445D'}"
                                        
                                        style="height: 321px; display:flex;align-items:center;flex-direction:column;justify-content:center">
                                        <asp:Label Text="{{telson.ColumnName}}" style="font-size:18px;" class="revenuelabelHeader" runat="server" />

                                        <asp:Label ID="Label6" class="revenuelabelBody" Text="{{telson.RowValue}}{{telson.SpecialCharc}}" runat="server" />
                                    </div>
                                    <div class="col-md-12 " style="background: #3e4c6a; margin-top: -15px;text-align:center">

                                        <asp:Label ID="Label7" class="revenuelabelBody" style="font-size:16px;" Text="{{telson.PaymentValue}}" runat="server" />
                                    </div>
                                </div>
                                <div data-ng-repeat="telson in telsonData|limitTo:1:19">
                                    <div class="col-md-12" data-ng-style="telson.ColorCode != '' && {'background-color':'{{telson.ColorCode}}'} || {'background-color': '#38445D'}"
                                        style="height: 203px; display:flex;align-items:center;flex-direction:column;justify-content:center">
                                         <asp:Label Text="{{telson.ColumnName}}" style="font-size:18px;" class="revenuelabelHeader" runat="server" />

                                        <asp:Label ID="Label8" class="revenuelabelBody" Text="{{telson.RowValue}}{{telson.SpecialCharc}}" runat="server" />

                                    </div>

                                    <div class="col-md-12 " style="background: #3e4c6a; margin-top: -15px;text-align:center">
                                         <asp:Label ID="Label9" class="revenuelabelBody" style="font-size:16px;" Text="{{telson.PaymentValue}}" runat="server" />
                                    </div>
                                </div>

                            </div>
                            </div>
                      

                        <div class="col-md-2">
                                <div data-ng-repeat="telson in telsonData|limitTo:5:20" >
                                    <div data-ng-style="telson.ColorCode != '' && {'background-color':'{{telson.ColorCode}}'} || {'background-color': '#38445D'}" 
                                        style="margin-left: -18px; margin-top: 5px; ">
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

                      
                            <div class="col-md-2">
                                <div data-ng-repeat="telson in telsonData|limitTo:4:25">
                            <div data-ng-if="telson.ColumnName!='None'"  data-ng-style="telson.ColorCode != '' && {'background-color':'{{telson.ColorCode}}'} || {'background-color': '#38445D'}"
                                style="margin-left: -18px; margin-top: 5px; ">
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
                                            style="margin-left: -4px; margin-top: 5px; background-color: #2C3850;visibility:hidden">
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


                        <%--   <div class="col-md-3">
                      
                        

                        </div>--%>
                       <%--  <div class="col-md-4">
                      
                            <div class="divWidth" style="margin-left: -6px;">
                                <div style="background-color: #475672;margin-top: 5px;">
                                    <table>
                                           <tbody>
                                         <tr style="text-align:center;font-size: small;color: #A9BFD5;font-weight:400 !important;height: 37px;margin-top: 10px;" >

                                                 
                                                <td colspan="3">PAYMENT STATISTICS</td>
                                            </tr>
                                               <tbody>
                                    </table>
                                </div>
                                
                                <div class="dashboardTable" style="margin-top:5px;">
                                     <table>
                                       
                                        <tbody>
                                           
                                            <tr style="color:#A9BFD5">
                                                <td></td>
                                                <td>ACTUAL</td>
                                                <td>REF.</td>
                                                <td>DIFF.</td>
                                                </tr>
                                           
                                            <tr data-ng-repeat="telson in telsonData|limitTo:5:15">
                                                <td class="tableLeftAlign" style="color: #A9BFD5;">
                                                    <span id="NordfinContentHolder_lblSentout">{{telson.ColumnName}}</span></td>
                                                <td>
                                                    <div data-ng-if="telson.NumberCast!=2"><span >{{telson.PaymentValue}}{{telson.SpecialCharc}}</span></div>
                                                    <div data-ng-if="telson.NumberCast==2"><span >N/A</span></div>
                                                    
                                            </td>
                                                <td>
                                                    <span >{{telson.StaticValue}}{{telson.SpecialCharc}}</span>
                                            </td>
                                                
                                                <td>
                                                      <div data-ng-if="telson.NumberCast!=2"><span >{{telson.RowValue}}{{telson.SpecialCharc}}</span></div>
                                                    <div data-ng-if="telson.NumberCast==2"><span >N/A</span></div>
                                                   
                                            </tr>
                                           
                                           

                                           
                                        </tbody>
                                    </table>
                                </div>
                                   
                                </div>

                        </div>--%>
                        </div>

                  
                    </div>
                 </div>

       </div>    
       

    </asp:Content>