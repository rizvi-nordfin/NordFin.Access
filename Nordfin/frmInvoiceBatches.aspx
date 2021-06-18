<%@ Page Language="C#" MasterPageFile="~/Nordfin.Master" Title="NFC ACCESS" Inherits="Nordfin.frmInvoiceBatches" %>

<script runat="server">

   

   
</script>


<asp:Content ID="Content1" ContentPlaceHolderID="NordfinContentHolder" runat="server">
       
    <link href="Styles/InvoiceBatches.css?version=<%=ConfigurationManager.AppSettings["VersionConfiguration"].ToString() %>" rel="stylesheet" />
    <script src="Scripts/jsInvoiceBatches.js?version=<%=ConfigurationManager.AppSettings["VersionConfiguration"].ToString() %>"></script>

   <%--    <script src="//ajax.googleapis.com/ajax/libs/jquery/2.2.4/jquery.min.js"></script>--%>

    <script src="Scripts/jquery.table2excel.js"></script>

    <script src="https://cdnjs.cloudflare.com/ajax/libs/xlsx/0.7.7/xlsx.core.min.js"></script>
     <%--<script src="https://rawgit.com/unconditional/jquery-table2excel/master/src/jquery.table2excel.js"></script>--%>
    <div class="dashboardContainer">
        <div class="container-fluid ">
            <div class="form-group">
                <div class="dashboardHeader">
                    <div class="dashboardHeadline">Invoice batches</div>
                   <asp:Button Text="Download" CssClass="button button-table batchesDownloadButton"  OnClick="btnPaidBatches_Click" ID="btnPaidBatches" runat="server" />
                </div>

                <div class="row">
                    <div class="col-md-12">
                        <div class="scrollableTableContainer" style="max-width: calc(100vw - 90px); margin-left: 15px;">
                            <div data-ng-app="InvoiceBatchesApp" data-ng-controller="InvoiceBatchesControl">
                                <table id="invoiceData" class="table table-borderless invBatchesTable">
                                    <tr>
                                        <th colspan="2">Timeline</th>
                                        <th colspan="2">Invoices</th>
                                         <th colspan="3">Credited</th>
                                        <th colspan="3">Unpaid</th>
                                        <th colspan="3">Paid</th>
                                        <th colspan="3">Reminder</th>
                                        <th colspan="3">DC</th>
                                        <th colspan="3">EXT</th>

                                    </tr>
                                    <tr>
                                        <td data-ng-repeat="(key,value) in InvoiceBatches[0]">{{showName(key)}}
                                        </td>
                                    </tr>

                                    <tr data-ng-repeat="InvBatch in InvoiceBatches">
                                        <td data-ng-repeat="(key,value) in InvBatch">{{AmountSpaces(key,value)}}
                                        </td>
                                    </tr>


                                    <tr class="InvoiceBatchesTableSummary">
                                        <td colspan="2"><span>Summary</span></td>

                                        <td data-ng-repeat="(key,value) in SummaryBatch[0]">
                                            <div data-ng-if="key.indexOf('Amount')>0">
                                              {{AmountSpaces(key,value)}}
                                            </div>
                                            <div data-ng-if="key.indexOf('Amount')<0">{{value}}</div>

                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>


    <script>
      
      setTimeout(function (){
        // Table vertical Zebtra stripes
        $('.table td').hover(function() {
        var t = parseInt($(this).index()) + 1;
        $(this).parents('table').find('td:nth-child(' + t + ')').addClass('highlighted');
        },
        function() {
            var t = parseInt($(this).index()) + 1;
            $(this).parents('table').find('td:nth-child(' + t + ')').removeClass('highlighted');
        });
      
    }, 500);
  </script>
  
</asp:Content>
