
var app = angular.module("myApp", []);

app.controller("myCtrl", function ($scope, $http) {

    $http({
        url: "frmClientInformation.aspx/getClientInfo",
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
        const ClientInfo = result.ClientInfo;


        $scope.DrawGraph(ClientInfo);

    }, function (error) {


    });


    $scope.DrawGraph = function (ClientInfo) {

        var ctxCurrent = document.getElementById("myChart");


        var dataCurrent = {
            indexLabelFontColor: "white",
            labels: ["Access", "Admin"],


            datasets: [{
                label: "Success",
                data: [ClientInfo.AccessSuccess, ClientInfo.AccessFail],
                backgroundColor: [
                    "rgba(62, 75, 100, 1)",
                    "rgba(62, 75, 100, 1)"

                ],
                borderColor: [
                    "rgba(62, 75, 100, 1)",
                    "rgba(62, 75, 100, 1)"


                ],

            },
            {
                label: "Fail",
                data: [ClientInfo.MypageSuccess, ClientInfo.MypageFail],
                backgroundColor: [
                    "rgba(35, 45, 65, 1)",
                    "rgba(35, 45, 65, 1)"

                ],
                borderColor: [
                    "rgba(50,150,200,1)",
                    "rgba(50,150,200,1)"


                ],

            }]

        };



        //options
        var options = {
            maintainAspectRatio: false,
            responsive: true,
            title: {
                display: false,
                position: "top",

                fontSize: 18,
                fontColor: "#111"
            },
            legend: {
                display: false,
                position: "bottom",
                labels: {
                    fontColor: "White",
                    fontSize: 16
                }
            },
            tooltips: {
                enabled: true,
                mode: 'index',
                callbacks: {
                    label: function (tooltipItems, data) {
                        return data.datasets[tooltipItems.datasetIndex].label + ': ' + tooltipItems.yLabel + '';
                    }
                }
            },
            scales: {
                yAxes: [{
                    ticks: {
                        beginAtZero: true
                    }
                }]
            }
        };

        //create Chart class object
        var chartCurrent = new Chart(ctxCurrent, {
            type: "bar",
            data: dataCurrent,
            options: options
        });

        Chart.defaults.global.defaultFontColor = 'white';
        Chart.defaults.global.defaultFontFamily = 'Arial';
        Chart.defaults.global.defaultFontSize = 11;
    }

});