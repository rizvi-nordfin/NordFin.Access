var appModule = angular.module("InvoiceBatchesApp", []);

appModule.controller("InvoiceBatchesControl", function ($scope, $http) {


    $http({
        url: "frmInvoiceBatches.aspx/LoadInvoiceBatches",
        dataType: 'json',
        method: 'POST',
        data: '{}',
        headers: {
            "Content-Type": "application/json; charset=utf-8",
            "X-Requested-With": "XMLHttpRequest"
        }
    }).then(function (response) {

       
        const resData = response.data.d;
        const InvBatchesList = JSON.parse(resData);
        $scope.InvoiceBatches = InvBatchesList.InvoiceBatchesList;
        $scope.SummaryBatch = InvBatchesList.SummaryBatchList;

    }, function (error) {

        
    });


    $scope.showName = function (branchName) {
      
        return branchName.replace(/\d+/g, '')
    }


    $scope.AmountSpaces = function (key, num) {
       
        if (key == "Year")
            return num.toString();
         
        return num.toString().replace(/\B(?=(\d{3})+(?!\d))/g, " ");
           
    }

 
});

var tableToExcel = (function () {
    var uri = 'data:application/vnd.ms-excel;base64,'
        , template = '<html xmlns: o="urn:schemas-microsoft-com:office:office" xmlns:x="urn:schemas-microsoft-com:office:excel" xmlns="http://www.w3.org/TR/REC-html40"><head><!--[if gte mso 9]><xml><x: ExcelWorkbook><x: ExcelWorksheets><x: ExcelWorksheet><x: Name>{worksheet}</x: Name><x: WorksheetOptions><x: DisplayGridlines/></x: WorksheetOptions></x: ExcelWorksheet ></x: ExcelWorksheets ></x: ExcelWorkbook ></xml > <meta http-equiv="content-type" content="text/plain; charset=UTF-8" /></head > <body><table class="check">{table}</table></body></html > '
        , base64 = function (s) { return window.btoa(unescape(encodeURIComponent(s))) }
        , format = function (s, c) { return s.replace(/{(\w+)}/g, function (m, p) { return c[p]; }) }
    return function (table, name) {

     
        

       
        name += year + "_" + month + "_" + date;
        if (!table.nodeType) table = document.getElementById(table)
        var ctx = { worksheet: name || 'Worksheet', table: table.innerHTML }
        window.location.href = uri + base64(format(template, ctx))
    }
})()




var tableToExcel = (function () {

    var d = new Date();
    var date = d.getDate();
    var month = d.getMonth() + 1;
    var year = d.getFullYear();
    var name = "Payment_Cohort_Analysis_" + year + "_" + month + "_" + date;
    $("#invoiceData").table2excel({
        exclude: ".noExport",
        filename: name
    });
})

