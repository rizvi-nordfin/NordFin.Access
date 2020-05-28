window.onload = function () {



    var ctx = document.getElementById("myChart");
    var myChart = new Chart(ctx, {
        type: 'pie',
        data: {
            labels: ['Success', 'Fail'],
            datasets: [{
                label: '# of Tomatoes',
                data: [parseInt(document.getElementById("NordfinContentHolder_hdnSuccess").value), parseInt(document.getElementById("NordfinContentHolder_hdnFail").value)],
                backgroundColor: [
                    'rgba(62, 75, 100, 1)',
                    'rgba(35, 45, 65, 1)'
                      
                ],
                borderColor: [
                    'rgba(62, 75, 100, 1)',
                    'rgba(35, 45, 65, 1)'
                   
                ],
                borderWidth: 1
            }]
        },
        options: {
            cutoutPercentage: 40,
            responsive: false,
            legend: {
                display: false
            },
        }

    });






};

var jq13 = jQuery.noConflict();

var app = angular.module("myApp", []);

app.controller("myCtrl", function ($scope, $http) {

    $http({
        url: "frmTrafficDetails.aspx/GetGraphData",
        dataType: 'json',
        method: 'POST',
        data: '{}',
        headers: {
            "Content-Type": "application/json; charset=utf-8",
            "X-Requested-With": "XMLHttpRequest"
        }
    }).then(function (response) {

        const resData = response.data.d;
        const result = JSON.parse(resData);
        const graphData = result.Result;
        var data1 = [];
        var data2 = [];
        for (var i = 0; i < graphData.length; i++) {
            data1.push([graphData[i].LogDate, graphData[i].Success]);
            data2.push([graphData[i].LogDate, graphData[i].Failure]);
        }
        $scope.DrawGraph(data1, data2)



    }, function (error) {


    });



    $scope.DrawGraph = function (data1, data2) {

       
        jq13('#jqChart').jqChart({
            title: { text: ' ' },
            animation: { duration: 1 },
            shadows: {
                enabled: false
            },
            mouseover: function (e) {
                alert(e.dataSeries.type + ", dataPoint { x:" + e.dataPoint.x + ", y: " + e.dataPoint.y + " }");
            },
            options: {
                responsive: true,
            },
           
            series: [
                {
                    type: 'column',
                    title: 'Success',
                    fillStyle: '#3e4b64',
                    
                    data: data1
                 
                },
                {
                    type: 'column',
                    title: 'Fail',
                    fillStyle: '#232d41',
                    data: data2
                },
              
            ]     });
    }
});