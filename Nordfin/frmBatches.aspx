<%@ Page Language="C#" MasterPageFile="~/Nordfin.Master" AutoEventWireup="true" CodeBehind="frmBatches.aspx.cs" Inherits="Nordfin.frmBatches" %>

<asp:Content ID="Content1" ContentPlaceHolderID="NordfinContentHolder" runat="server">

    <link href="Styles/Batches.css?version=<%=ConfigurationManager.AppSettings["VersionConfiguration"].ToString() %>" rel="stylesheet" />

    <div class="dashboardContainer">
        <div class="container-fluid ">
            <div class="form-group">


                <div class="dashboardHeader">
                    <div class="dashboardHeadline">Invoice paid batches</div>
                    <asp:Button Text="Download" CssClass="button button-table batchesDownloadButton" ID="btnPaidBatches" OnClick="btnPaidBatches_Click" runat="server" />
                </div>

                <div class="row">
                    <div class="col-md-12">
                        <div class="scrollableTableContainer">
                            <asp:GridView ID="grdInvoicesPaid" runat="server" AutoGenerateColumns="true" ViewStateMode="Enabled" Visible="true" Style="table-layout: fixed; color: white; overflow-x: scroll; overflow-y: scroll; min-width: 1300px;" ShowHeaderWhenEmpty="true" CssClass="table table-borderless">
                            </asp:GridView>
                        </div>
                    </div>
                </div>
            </div>

            <div class="form-group">
                <div class="dashboardHeader">
                    <div class="dashboardHeadline">Invoice unpaid batches</div>
                    <asp:Button Text="Download" CssClass="button button-table batchesDownloadButton" ID="btnUnpaidBatches" OnClick="btnUnpaidBatches_Click" runat="server" />
                </div>

                <div class="row">
                    <div class="col-md-12">
                        <div class="scrollableTableContainer">
                            <asp:GridView ID="grdInvoicesUnPaid" runat="server" AutoGenerateColumns="true" ViewStateMode="Enabled" Visible="true" Style="table-layout: fixed; color: white; overflow-x: scroll; overflow-y: scroll; min-width: 1300px;" ShowHeaderWhenEmpty="true" CssClass="table table-borderless">
                            </asp:GridView>
                        </div>
                    </div>
                </div>
            </div>


            <div class="form-group">

                <div class="dashboardHeader">
                    <div class="dashboardHeadline">Total invoices per client</div>
                    <asp:Button Text="Download" CssClass="button button-table batchesDownloadButton" ID="btnTotalInvoice" OnClick="btnTotalInvoice_Click" runat="server" />
                </div>


                <div class="row">
                    <div class="col-md-12">
                        <div class="scrollableTableContainer">
                            <asp:GridView ID="grdInvoicesPerClient" runat="server" AutoGenerateColumns="true" ViewStateMode="Enabled" Visible="true" Style="table-layout: fixed; color: white; overflow-x: scroll; overflow-y: scroll; min-width: 1300px;" ShowHeaderWhenEmpty="true" CssClass="table batchesTableWithTotal table-borderless">
                            </asp:GridView>
                        </div>
                    </div>
                </div>
            </div>



            <div class="form-group">

                <div class="dashboardHeader">
                    <div class="dashboardHeadline">Total invoices amount per client</div>
                    <asp:Button Text="Download" CssClass="button button-table batchesDownloadButton" AccessKey ID="btnTotalInvoiceAmount" OnClick="btnTotalInvoiceAmount_Click" runat="server" />
                </div>




                <div class="row">
                    <div class="col-md-12">
                        <div class="scrollableTableContainer">
                            <asp:GridView ID="grdInvoicesAmount" runat="server" AutoGenerateColumns="true" ViewStateMode="Enabled" Visible="true" Style="table-layout: fixed; color: white; overflow-x: scroll; overflow-y: scroll; min-width: 1300px" ShowHeaderWhenEmpty="true" CssClass="table batchesTableWithTotal table-borderless">
                            </asp:GridView>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
