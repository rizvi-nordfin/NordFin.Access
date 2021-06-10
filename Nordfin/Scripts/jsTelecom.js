var jq13 = jQuery.noConflict();

var app = angular.module("myApp", []);

app.controller("myCtrl", function ($scope, $http) {

    $http({
        url: "frmTelecom.aspx/GetTelecomData",
        dataType: 'json',
        method: 'POST',
        data: '{}',
        headers: {
            "Content-Type": "application/json; charset=utf-8",
            "X-Requested-With": "XMLHttpRequest"
        }
    }).then(function (response) {

        const resData = response.data.d;
        const telsonData = JSON.parse(resData);

        $scope.telsonData = telsonData.TelecomList;


        //$scope.ClientName = telsonData.ClientName;
        const telsonList = telsonData.TelecomChart;
        var data1 = [];
        var data2 = [];
       
        for (var i = 0; i < telsonList.length; i++) {
            data1.push([telsonList[i].Column, parseInt(telsonList[i].Number)]);
            data2.push([telsonList[i].Column, parseInt(telsonList[i].Amount)]);
        }
      


        $scope.DrawGraph(data1, data2); 

            document.getElementById("divTelson").style.display = "";
        
        document.getElementById("divChart").style.background = "#38445d";
        //$scope.ContractsList = telsonData.Contracts;

    }, function (error) {


    });

    $scope.DrawGraph = function (data1, data2) {


        jq13('#jqChart1').jqChart({
            title: { text: ' ' },

            animation: { duration: 1 },
            shadows: {
                enabled: false
            },

            options: {
                responsive: true,
                maintainAspectRatio: false,


            },
            legend: {
                display: true,
                location: 'top'

            },

            axes: [
                {
                    name: 'Amount',
                    type: 'linear',
                    location: 'left',
                    labels: {
                        stringFormat: ','
                    }


                },
                {
                    name: 'Number',
                    type: 'linear',
                    location: 'right'

                }

            ],


            series: [
                {
                    type: 'column',
                    title: 'Number',
                    fillStyle: '#3e527c',
                    axisY: 'Number',
                    data: data1,

                },

                {
                    type: 'column',
                    title: 'Amount',
                    fillStyle: '#232d41',
                    axisY: 'Amount',
                    data: data2,

                },
                {
                    type: 'line',
                    title: 'Amount',
                    strokeStyle: '#FFB100',
                    fillStyle: '#FFB100',
                    lineWidth: 1,
                    data: data2,


                },


            ]
        });





    }


});