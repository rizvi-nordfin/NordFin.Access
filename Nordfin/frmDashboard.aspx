<%@ Page Language="C#" MasterPageFile="~/Nordfin.Master" AutoEventWireup="true" Title="NordfinCapital" CodeBehind="frmDashboard.aspx.cs" Inherits="Nordfin.frmDashboard" %>

<asp:Content ID="Content1" ContentPlaceHolderID="NordfinContentHolder" runat="server">
    <link href="Styles/Dashboard.css?version=<%=ConfigurationManager.AppSettings["VersionConfiguration"].ToString() %>" rel="stylesheet" />
    <script src="Scripts/jsDashboard.js?version=<%=ConfigurationManager.AppSettings["VersionConfiguration"].ToString() %>"></script>
   
    <link href="Styles/jquery-jvectormap.css" rel="stylesheet" />
    <script src="Scripts/jquery-jvectormap-1.2.2.min.js"></script>
    <script src="Scripts/jquery-jvectormap-world-mill-en.js"></script>

     <link href="Styles/mapsvg.css" rel="stylesheet" />
    <link href="Styles/nanoscroller.css" rel="stylesheet" />
    <script src="Scripts/jquery.jqChart.min.js"></script>
    <link href="Styles/jquery.jqChart.css" rel="stylesheet" />
    <script src="Scripts/jquery.mousewheel.min.js"></script>
    <script src="Scripts/jquery.nanoscroller.min.js"></script>
    <script src="Scripts/mapsvg.js"></script>
    <div data-ng-app="myApp" data-ng-controller="myCtrl">
        <div class="dashboardContainer">
            <div class="container-fluid ">

                <div class="dashboardHeader">
                    <div class="dashboardHeadline">Dashboard</div>
                    <div class="dashboardDataSwitch">

                        <asp:Button  CssClass="form-control buttonDefault buttonWidth" runat="server" id="btnInvoiceData"  OnClick="btnInvoiceData_Click" Text="Invoice Data"></asp:Button>
                        <%----%>
                        <asp:Button CssClass="form-control buttonClass buttonWidth"  OnClick="btnCustomerData_Click" id="btnCustomerData" Text="Customer Data"  runat="server"    ></asp:Button>

                        <div class="row col-lg-12 col-md-12 btnExportRight" style="flex-direction:column-reverse;float:right;">

                        <asp:UpdatePanel ID="updatepnl" runat="server">
                            <%--OnClientClick="return Excel();"--%>
                            <ContentTemplate>
                                <asp:Button CssClass="export" OnClick="btnExcel_Click" Style="float: right; margin-right: 3px; margin-top: 24px;"  ID="btnExcel" Text="Export" runat="server"></asp:Button>



                            </ContentTemplate>
                        </asp:UpdatePanel>
                            </div>
                        <div style="margin-left: 78%;margin-top: 24px;">
                        <input type="checkbox" style="display:none;" id="chkMatch"/>
                        <button runat="server" class="export" style="height: 25px;width: 150px;" visible="false" id="btnMatchRegion" data-ng-click="MapRegion($event)" >Match Region</button>
                            </div>
                        <%--<button runat="server" class="button form-control" style="float:right;display:none;" id="btnMatchRegion" data-ng-click="MapRegion($event)" >Match Region</button>--%>
                         <%--<button  class="form-control buttonDefault" style="float:right;" runat="server"  id="btnMatchRegion"  data-ng-click="MapRegion($event)">Match Region</button>--%>
                         <%--<button type="button" class="form-control buttonDefault" id="btnInvoiceData" data-ng-click="InvoiceCustData('divInvoiceData',$event,'divCostomerData','btnInvoiceData','btnCustomerData')">Invoice Data</button>--%>
                        <%--<button type="button" class="button form-control buttonClass featureNotAvailableTrigger" style="width: 150px; height: 50px;" id="btnCustomerData" >Customer Data</button>--%>




                         <%--<asp:Button  CssClass="form-control buttonDefault" runat="server" id="btnInvoiceData" Text="Invoice Data"></asp:Button>--%>
                 
                        <%--<button type="button" class="button form-control buttonClass featureNotAvailableTrigger" style="width: 150px; height: 50px;" id="btnCustomerData" >Customer Data</button>--%>
                    </div>
                </div>


                <asp:Panel runat="server" id="divInvoiceData">
                    <div class="container-fluid dashboardAmountBoxes">
                        <div class="row">

                            <div class="col-md-12 col-lg-3">


                                <div class="innerDiv divHeight search">
                                    <img src="Images/NFC_revenue.svg" alt="" />

                                    <div class="revenue">

                                        <div>
                                            <asp:Label Text="Invoice Amount" class="revenuelabelHeader" runat="server" /></div>
                                        <div>
                                            <asp:Label ID="lblTotalSentoutAmount" class="revenuelabelBody" runat="server" /></div>

                                    </div>


                                </div>





                            </div>

                            <div class="col-md-12 col-lg-3">

                                <div class="innerDiv divHeight search">
                                    <img src="Images/NFC_revenue.svg" alt="" />

                                    <div class="revenue">

                                        <div>
                                            <asp:Label Text="Unpaid Amount" class="revenuelabelHeader" runat="server" /></div>
                                        <div>
                                            <asp:Label ID="lblTotalUnpaidAmount" class="revenuelabelBody" runat="server" /></div>

                                    </div>


                                </div>



                            </div>

                            <div class="col-md-12 col-lg-3">

                                <div class="innerDiv divHeight search">
                                    <img src="Images/NFC_revenue.svg" alt="" />

                                    <div class="revenue">

                                        <div>
                                            <asp:Label Text="Paid %" class="revenuelabelHeader" runat="server" />
                                        </div>
                                        <div>
                                            <asp:Label ID="lblTotalAvgPaidDay" class="revenuelabelBody" runat="server" /></div>

                                    </div>


                                </div>




                            </div>

                            <div class="col-md-12 col-lg-3">

                                <div class="innerDiv divHeight search">
                                    <img src="Images/NFC_revenue.svg" alt="" />

                                    <div class="revenue">

                                        <div>
                                            <asp:Label Text="Avg Pmt Day" class="revenuelabelHeader" runat="server" />
                                        </div>
                                        <div>
                                            <asp:Label Text="" ID="lblTotalAvgPaymentDay" class="revenuelabelBody" runat="server" /></div>

                                    </div>


                                </div>



                            </div>

                        </div>


                    </div>

                    <div class="container-fluid" style="margin-top: 30px;">



                        <div class="row">



                            <div class="col-md-12 col-lg-3 dashboardOverViewBox">
                                <div class="row headingDiv tableDiv overviewHeading" style="margin-right:0px !important;">
                                    <span class="spanOverview">Overview</span>
                                </div>


                                <!-- Overview Table -->
                                <div class="dashboardTable">
                                    <table>
                                        <tbody>
                                            <tr class="dashboardTableHeader">
                                                <th class="tableLeftAlign">Status</th>
                                                <th>Invoices</th>
                                                <th>Amount</th>
                                                <th>%</th>
                                            </tr>
                                            <tr>
                                                <td class="tableLeftAlign">
                                                    <asp:Label Text="Sent out" ID="lblSentout" runat="server" /></td>
                                                <td>
                                                    <asp:Label ID="lblSentoutNumber" runat="server" /></td>
                                                <td>
                                                    <asp:Label ID="lblSenoutAmount" runat="server" /></td>
                                                <td>
                                                    <asp:Label ID="lblSentoutPercent" Text="0%" runat="server" /></td>
                                            </tr>
                                            <tr>
                                                <td class="tableLeftAlign">
                                                    <asp:Label ID="lblUnpaid" Text="Unpaid" runat="server" /></td>
                                                <td>
                                                    <asp:Label ID="lblUnpaidNumber" runat="server" /></td>
                                                <td>
                                                    <asp:Label runat="server" ID="lblUnpaidAmount" /></td>
                                                <td>
                                                    <asp:Label ID="lblUnpaidPrecent" Text="0%" runat="server" /></td>
                                            </tr>
                                            <tr>
                                                <td class="tableLeftAlign">
                                                    <asp:Label ID="lblPaid" Text="Paid" runat="server" /></td>
                                                <td>
                                                    <asp:Label ID="lblPaidNumber" runat="server" /></td>
                                                <td>
                                                    <asp:Label ID="lblPaidAmount" runat="server" /></td>
                                                <td>
                                                    <asp:Label ID="lblPaidPercent" Text="0%" runat="server" /></td>
                                            </tr>
                                             <tr>
                                                <td class="tableLeftAlign">
                                                    <asp:Label ID="lblCredited" Text="Credited" runat="server" /></td>
                                                <td>
                                                    <asp:Label ID="lblCreditedNumber" runat="server" /></td>
                                                <td>
                                                    <asp:Label ID="lblCreditedAmount" runat="server" /></td>
                                                <td>
                                                    <asp:Label ID="lblCreditedPercent" Text="0%" runat="server" /></td>
                                            </tr>

                                            <tr>
                                                <td class="tableLeftAlign">
                                                    <asp:Label ID="lblRemainder" Text="Reminder" runat="server" /></td>
                                                <td>
                                                    <asp:Label ID="lblRemainderNumber" runat="server" /></td>
                                                <td>
                                                    <asp:Label ID="lblRemainderAmount" runat="server" /></td>
                                                <td>
                                                    <asp:Label ID="lblRemainderPercent" Text="0%" runat="server" /></td>
                                            </tr>
                                            <tr>
                                                <td class="tableLeftAlign">
                                                    <asp:Label ID="lblDC" Text="DC" runat="server" /></td>
                                                <td>
                                                    <asp:Label ID="lblDCNumber" runat="server" /></td>
                                                <td>
                                                    <asp:Label ID="lblDCAmount" runat="server" /></td>
                                                <td>
                                                    <asp:Label ID="lblDCPercent" Text="0%" runat="server" /></td>
                                            </tr>
                                            <tr>
                                                <td class="tableLeftAlign">
                                                    <asp:Label ID="lblExt" Text="Ext" runat="server" /></td>
                                                <td>
                                                    <asp:Label ID="lblExtNumber" runat="server" /></td>
                                                <td>
                                                    <asp:Label ID="lblExtAmount" runat="server" /></td>
                                                <td>
                                                    <asp:Label ID="lblExtPercent" Text="0%" runat="server" /></td>
                                            </tr>
                                        </tbody>
                                    </table>
                                </div>
                            </div>

                            <div class="col-md-12 col-lg-3 dashboardOverViewBox">
                                <div class="row headingDiv tableDiv overviewHeading" style="margin-right:0px !important;">
                                    <span class="spanOverview">Batch Volume</span>
                                </div>
                                <!-- Batch Volume Table -->
                                <div class="dashboardTable">
                                    <table>
                                        <tbody>
                                            <tr class="dashboardTableHeader">
                                                <th class="tableLeftAlign">Year</th>
                                                <th class="tableCenterAlign tableMonth">Month</th>
                                                <th>Invoices</th>
                                                <th>Amount</th>
                                            </tr>

                                            <tr data-ng-repeat="batchvolume in BatchVolumeList">
                                                <td class="tableLeftAlign">
                                                    <asp:Label ID="Label1" Text="{{batchvolume.Year}}" runat="server" />
                                                </td>
                                                <td class="tableCenterAlign tableMonth">
                                                    <asp:Label ID="Label2" Text="{{batchvolume.Month}}" runat="server" />
                                                </td>
                                                <td>
                                                    <asp:Label ID="Label3" Text="{{batchvolume.InvoiceNumber}}" runat="server" />
                                                </td>
                                                <td>
                                                    <asp:Label ID="Label4" Text="{{batchvolume.InvoiceAmount}}" runat="server" />
                                                </td>
                                            </tr>

                                        </tbody>
                                    </table>
                                </div>



                            </div>
                            <div class="col-md-12 col-lg-3 dashboardOverViewBox">
                                <div class="row headingDiv tableDiv overviewHeading" style="margin-right:0px !important;">
                                    <span class="spanOverview">Daily Production</span>
                                </div>

                                <div class="dashboardDailyProdBox dashboardDailyProdBoxInvoices">
                                    <div class="dashboardDailyProdBoxHeight">
                                        <div class="dashboardDailyProdBoxHeadline">Invoices</div>
                                        <asp:Label runat="server" Text="" ID="lblInvoices" class="dashRes"></asp:Label>
                                    </div>
                                    <div class="dashboardDailyProdBoxHeadline dashboardDailyProdBoxHeadlineAmount">
                                        <asp:Label runat="server" Text="" ID="lblInvoicesAmount" class="dashResAmount"></asp:Label>
                                    </div>
                                </div>

                                <div class="dashboardDailyProdBox dashboardDailyProdBoxPayments">
                                    <div class="dashboardDailyProdBoxHeight">
                                        <div class="dashboardDailyProdBoxHeadline">Payments</div>
                                        <asp:Label runat="server" Text="" ID="lblPayments" class="dashRes"></asp:Label>
                                    </div>
                                    <div class="dashboardDailyProdBoxHeadline dashboardDailyProdBoxHeadlineAmount">
                                        <asp:Label runat="server" Text="" ID="lblPaymentsAmount" class="dashResAmount"></asp:Label>
                                    </div>
                                </div>

                                <div class="dashboardDailyProdBox dashboardDailyProdBoxReminder">
                                    <div class="dashboardDailyProdBoxHeight">
                                        <div class="dashboardDailyProdBoxHeadline">Reminder</div>
                                        <asp:Label runat="server" ID="lblReminder" Text="" class="dashRes"></asp:Label>
                                    </div>
                                    <div class="dashboardDailyProdBoxHeadline dashboardDailyProdBoxHeadlineAmount">
                                        <asp:Label runat="server" Text="" ID="lblReminderAmount" class="dashResAmount"></asp:Label>
                                    </div>
                                </div>
                                <div class="dashboardDailyProdBox dashboardDailyProdBoxDC">
                                    <div class="dashboardDailyProdBoxHeight">
                                        <div class="dashboardDailyProdBoxHeadline">DC</div>
                                        <asp:Label runat="server" ID="lblTotalDC" Text="" class="dashRes"></asp:Label>
                                    </div>
                                    <div class="dashboardDailyProdBoxHeadline dashboardDailyProdBoxHeadlineAmount">
                                        <asp:Label runat="server" Text="" ID="lblTotalDCAmount" class="dashResAmount"></asp:Label>
                                    </div>
                                </div>
                                <div class="clear"></div>
                            </div>

                            <div class="col-md-12 col-lg-3 dashboardOverViewBox">

                                <div class="row headingDiv tableDiv overviewHeading" style="margin-right:0px !important;">
                                    <span class="spanOverview">Collection</span>
                                </div>


                                <div class="row tableDiv col-md-12">
                                    <div class="dashboardCollectionBoxButtons">
                                        <button runat="server" class="button form-control" id="btnTotal" data-ng-click="totalChart($event)" text="Total">Total</button>
                                        <button runat="server" class="button form-control" id="btnThisYear" data-ng-click="thisyearChart($event)">This Year</button>
                                    </div>
                                    <div class="row" style="margin-top: 18px; width: 100%;    margin-right: 0px !important;">
                                        <div class="chart-container" id="divChart" style="display: none;">
                                            <canvas id="myChart"></canvas>
                                        </div>
                                        <div class="chart-container" id="divCurrentChart" style="display: none;">
                                            <canvas id="currentChart"></canvas>
                                        </div>
                                    </div>
                                </div>

                            </div>
                        </div>
                    </div>
                </asp:Panel>

                <asp:Panel runat="server" ID="pnlCustomerData" CssClass="pnlMarginTop" Visible="false">
                    <div class="row">
                        <div class="col-lg-6">
                            <div class="container-fluid dashboardAmountBoxes">
                                <div class="row">
                                     <div class="col-md-12 col-lg-6">


                                    <div class="innerDiv divHeight search" style="margin-right:-7px;">
                                        <img src="Images/NFC_revenue.svg" alt="" />

                                        <div class="revenue">

                                            <div>
                                                <asp:Label Text="Total Customers" class="revenuelabelHeader" runat="server" />
                                            </div>
                                            <div>
                                                <asp:Label ID="Label9" class="revenuelabelBody" Text="{{TotalCust}}" runat="server" />
                                            </div>

                                        </div>


                                    </div>





                                </div>
                                    <div class="col-md-12 col-lg-6">

                                    <div class="innerDiv divHeight search"  style="margin-right:-7px;">
                                        <img src="Images/NFC_revenue.svg" alt="" />

                                        <div class="revenue">
                                          
                                                <div>
                                                    <asp:Label class="revenuelabelHeader" Text="MoM Growth" runat="server" />
                                                </div>
                                                <div>
                                                    <asp:Label ID="Label10" class="revenuelabelBody" Text="{{CustomerGrowth[0].MonthCount}}" runat="server" />

                                                </div>
                                          
                                        </div>
                                        <div class="revenue">
                                          
                                                <div>
                                                    <asp:Label class="revenuelabelHeader" Text="YoY Growth" runat="server" />
                                                </div>
                                                <div>
                                                    <asp:Label ID="Label13" class="revenuelabelBody" Text="{{CustomerGrowth[0].YearCount}}" runat="server" />
                                                </div>
                                          
                                        </div>


                                    </div>



                                </div>
                                </div>
                                <%--<asp:button ID="btn" Text="Test" runat="server"/>--%>
                            </div>



                            <div class="container-fluid" style="margin-top: 30px;">
                                 <div class="row">
                                     <div class="col-md-12 col-lg-6 dashboardOverViewBox">
                                    <div class="row headingDiv tableDiv overviewHeading">
                                        <span class="spanOverview">Number of Customers</span>
                                    </div>

                                    <div class="dashboardTable  dashboardCustTable">
                                        <table id="tblCust" runat="server">
                                            <tbody>
                                                <tr class="dashboardTableHeader">
                                                    <th class="tableLeftAlign">Customer Type</th>
                                                    <th class="tableCenterAlign tableMonth">Number</th>
                                                    <th class="tableCenterAlign tableMonth">%</th>

                                                </tr>

                                                <tr data-ng-repeat="custData in CustomerData" data-ng-style="custData.custType.toUpperCase().indexOf('INVOICE')>-1 ?{'background-color':'rgb(51, 58, 74)'}:''">
                                                    <td class="tableLeftAlign">
                                                        <asp:Label ID="Label5" Text="{{custData.custType}}" runat="server" />
                                                    </td>
                                                    <td class="tableCenterAlign tableMonth">
                                                        <asp:Label ID="Label6" Text="{{custData.custNumber}}" runat="server" />
                                                    </td>

                                                    <td class="tableCenterAlign tableMonth">
                                                        <asp:Label ID="Label7" Text="{{custData.custPercentage}}%" runat="server" />
                                                    </td>

                                                </tr>

                                            </tbody>
                                        </table>
                                    </div>



                                </div>

                                     <div class="col-md-12 col-lg-6 dashboardOverViewBox">
                                    <div class="row headingDiv tableDiv overviewHeading">
                                        <span class="spanOverview">Demographics</span>
                                    </div>

                                    <div class="dashboardTable dashboardCustTable">
                                        <table>
                                            <tbody>
                                                <tr class="dashboardTableHeader">
                                                    <th class="tableLeftAlign">Age Group</th>
                                                    <th class="tableCenterAlign tableMonth">Number</th>
                                                    <th class="tableCenterAlign tableMonth">%</th>
                                                </tr>

                                                <tr data-ng-repeat="demographic in Demographics">
                                                    <td class="tableLeftAlign">
                                                        <asp:Label ID="lbldemoAgeGroup" Text="{{demographic.custColName}}" runat="server" />
                                                    </td>
                                                    <td class="tableCenterAlign tableMonth">
                                                        <asp:Label ID="lbldemoNumber" Text="{{demographic.custRowValue}}" runat="server" />
                                                    </td>
                                                    <td class="tableCenterAlign tableMonth">
                                                        <asp:Label ID="Label8" Text="{{demographic.custPercentage}}%" runat="server" />
                                                    </td>

                                                </tr>

                                            </tbody>
                                        </table>
                                    </div>



                                </div>
                                     </div>
                            </div>


                            <div class="container-fluid" style="margin-top: 5px;">
                                <div class="row">
                                    <div class="col-md-12 col-lg-6 dashboardOverViewBox" style="text-align:center;margin:0px;">

                                        
                                        <div class="dashboardTable  dashboardCustTable dashboardPieChart" >
                                           
                                            <div class="piechart-container" id="divPieChart" style="position: relative;  max-width:74%;">

                                                 <canvas id="PieChart"  ></canvas>

                                            </div>
                                        </div>



                                    </div>

                                     <div class="col-md-12 col-lg-6 dashboardOverViewBox">


                                    <div class="dashboardTable  dashboardCustTable dashboardPieChart">

                                          <div class="piechart-container" id="divPieAgeChart" style="position: relative;  max-width:74%;">

                                                 <canvas id="PieAgeChart"  ></canvas>

                                            </div>
                                    </div>



                                </div>
                                </div>

                                 <div class="row">
                                       <div class="col-sm-12 col-md-12 col-lg-12 dashboardOverViewBox">


                                    <div class="dashboardTable dashboardCustTable dashboardMarginTop" style="margin-top:-60px;">



                                        <div id="jqChart" class="jqDashboardChart"></div>


                                    </div>


                                </div>
                                    </div>
                            </div>

                           
                        </div>

                        <div class="col-lg-6" style="margin-left: -5px;">
                            <div class="row">
                                <div class="col-md-12 col-lg-12 dashboardOverViewBox">


                                    <div class="dashboardTable dashboardCustTable" style="height: 507px;">
                                        <div id="world-map-markers" style="height: 450px; margin: 20px;"></div>
                                    </div>

                                   


                                </div>
                            </div>
                            <div class="row dashboardMarginTop" style="margin-top: 140px;">

                                <div class="col-md-12 col-lg-6 dashboardOverViewBox">


                                    <div class="dashboardTable dashboardCustTable dashboardPieChart">

                                          <table>
                                            <tbody>
                                                <tr class="dashboardTableHeader">
                                                    <th class="tableLeftAlign">Region</th>
                                                    <th class="tableCenterAlign tableMonth">No .of Customers</th>
                                                    

                                                </tr>

                                                <tr data-ng-repeat="region in CustomerRegion | limitTo:6">
                                                    <td class="tableLeftAlign">
                                                        <asp:Label ID="lblCustRegion" Text="{{region.CustRegion}}" runat="server" />
                                                    </td>
                                                    <td class="tableCenterAlign tableMonth">
                                                        <asp:Label ID="Label12" Text="{{region.CustTotal}}" runat="server" />
                                                    </td>

                                                   
                                                </tr>

                                            </tbody>
                                        </table>    

                                       


                                    </div>


                                </div>
                            </div>
                        </div>
                    </div>
                </asp:Panel>


                <div class="modal fade" id="mdlUpdateInfo" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
                        <div class="modal-dialog" role="document">
                            <div class="modal-content" style="background: none;border:none;">
                                <div class="modal-header dashboardHeadline" style="background-color: #323e53; color: #fff;font-size:16px;">
                                    <h5 class="modal-title modalTextcolor dashboardHeadlineModal" id="updateInfoModalLabel"></h5>
                                     <button type="button" class="modalcloseButton" data-dismiss="modal" aria-label="Close" style="top: 35px;right: 20px;">
                                        <span aria-hidden="true">✕</span>
                                    </button>
                                </div>
                                <div class="modal-body" id="divFinland" style=" background-color: #323e53; color: #fff; display: none;">
                                        <div id="mapsvgFinland"></div>
                                       
                                </div>
                                 <div class="modal-body" id="divSweden" style="background-color: #323e53; color: #fff;display:none;">
                                      
                                        <div id="mapsvgSweden"></div>
                                </div>
                              <%--  <div class="modal-footer" style="background-color: #323E53; padding: 5px;">
                                    <asp:Button Text="Submit" class="button updateInfoButton form-control" runat="server" ID="btnUpdateInfo" OnClientClick="return confirmUpdate();" OnClick="btnUpdateInfo_Click"  Width="128px" />
                                </div>--%>

                             

                            </div>
                        </div>
                    </div>
<%--<p style="display:none;" id="response"></p>--%>

            </div>

            <asp:HiddenField ID="hdnDC" Value="0" runat="server" />
            <asp:HiddenField ID="hdnExt" Value="0" runat="server" />
            <asp:HiddenField ID="hdnRemain" Value="0" runat="server" />
            <asp:HiddenField ID="hdnOntime" Value="0" runat="server" />
            <asp:HiddenField ID="hdnLate" Value="0" runat="server" />


            <asp:HiddenField ID="hdnDCAmount" Value="0" runat="server" />
            <asp:HiddenField ID="hdnExtAmount" Value="0" runat="server" />
            <asp:HiddenField ID="hdnRemainAmount" Value="0" runat="server" />
            <asp:HiddenField ID="hdnOntimeAmount" Value="0" runat="server" />
            <asp:HiddenField ID="hdnLateAmount" Value="0" runat="server" />

        </div>
    </div>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/Chart.js/2.4.0/Chart.min.js"></script>
</asp:Content>

