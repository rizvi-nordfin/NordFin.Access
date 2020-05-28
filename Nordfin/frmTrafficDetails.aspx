<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Nordfin.Master" Title="NordfinCapital" CodeBehind="frmTrafficDetails.aspx.cs" Inherits="Nordfin.frmTrafficDetails" %>

<asp:Content ID="Content1" ContentPlaceHolderID="NordfinContentHolder" runat="server">
    <script src="https://cdnjs.cloudflare.com/ajax/libs/Chart.js/2.4.0/Chart.min.js"></script>
    <script src="Scripts/jsTrafficDetails.js?version=<%=ConfigurationManager.AppSettings["VersionConfiguration"].ToString() %>"></script>
    <link href="Styles/LoginInformation.css?version=<%=ConfigurationManager.AppSettings["VersionConfiguration"].ToString() %>" rel="stylesheet" />
    <script src="Scripts/jquery.jqChart.min.js"></script>
    <link href="Styles/jquery.jqChart.css" rel="stylesheet" />

    <script src="Scripts/jquery-1.11.1.min.js"></script>

    <div class="dashboardContainer">
        <div class="container-fluid ">
            <div class="form-group">
                <div data-ng-app="myApp" data-ng-controller="myCtrl">
                    <div class="row" style="margin-top: 18px; width: 100%;">
                        <div class="col-md-12 col-lg-3">
                        <div class="chart-container" id="divChart">
                            <canvas id="myChart" width="400" height="400"></canvas>
                        </div>
                        </div>
                        <div class="col-md-12 col-lg-9 jqdivChart">

                            <div id="jqChart" class="jquiChart" ></div>
                        </div>
                    </div>
                </div>
                <div class="row">

                    <div class="col-sm-12 col-lg-12">
                        <div class="dashboardHeader">
                            <div class="dashboardHeadline">User login info</div>

                        </div>
                        <div class="scrollableTableContainer">

                            <asp:GridView ID="grdTrafficDetails" runat="server" AutoGenerateColumns="true" ViewStateMode="Enabled" Visible="true" ShowHeaderWhenEmpty="true" CssClass="table table-borderless grdTrafficDetails">
                            </asp:GridView>

                        </div>
                    </div>

                </div>
            </div>
        </div>
    </div>
    <asp:HiddenField ID="hdnSuccess" Value="0" runat="server" />
    <asp:HiddenField ID="hdnFail" Value="0" runat="server" />
</asp:Content>
