<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Nordfin.Master" Title="NordfinCapital" CodeBehind="frmUserLoginInformation.aspx.cs" Inherits="Nordfin.frmUserLoginInformation" %>

<asp:Content ID="Content1" ContentPlaceHolderID="NordfinContentHolder" runat="server">
   
    <link href="Styles/LoginInformation.css" rel="stylesheet" />

    <div class="dashboardContainer">
        <div class="container-fluid ">
            <div class="form-group">
             

                <div class="row">
                       
                    <div class="col-sm-12 col-lg-6">
                        <div class="dashboardHeader">
                            <div class="dashboardHeadline">Complete user login information </div>

                        </div>
                        <div class="scrollableTableContainer">

                            <asp:GridView ID="grdUserLoginInformation" runat="server" AutoGenerateColumns="true" ViewStateMode="Enabled" Visible="true" ShowHeaderWhenEmpty="true" CssClass="table table-borderless grdUserLoginInformation">
                            </asp:GridView>

                        </div>
                    </div>
                     <div class="col-sm-12 col-lg-6">
                         <div class="dashboardHeader">
                             <div class="dashboardHeadline">User online right now </div>

                         </div>
                        <div class="scrollableTableContainer">

                            <asp:GridView ID="grdActiveUser" runat="server" AutoGenerateColumns="true" ViewStateMode="Enabled" Visible="true" ShowHeaderWhenEmpty="true" CssClass="table table-borderless grdUserLoginInformation">
                            </asp:GridView>

                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
