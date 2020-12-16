var jq13 = jQuery.noConflict();

var app = angular.module("myApp", []);

app.controller("myCtrl", function ($scope, $http) {

    $http({
        url: "frmTeleson.aspx/GetTelsonData",
        dataType: 'json',
        method: 'POST',
        data: '{}',
        headers: {
            "Content-Type": "application/json; charset=utf-8",
            "X-Requested-With": "XMLHttpRequest"
        }
    }).then(function (response) {
        debugger;
        const resData = response.data.d;
        const telsonData = JSON.parse(resData);

        $scope.telsonData = telsonData.TelsonList;



        const telsonList = telsonData.TelsonChart;
        var data1 = [];
        var data2 = [];
        let MaxValue = 0;
        for (var i = 0; i < telsonList.length; i++) {
            data1.push([telsonList[i].Column, parseInt(telsonList[i].Number)]);
            data2.push([telsonList[i].Column, parseInt(telsonList[i].Amount)]);

            const Total = parseInt(telsonList[i].Number);
            if (Total > MaxValue)
                MaxValue = Total;

        }
      
     
        $scope.DrawGraph(data1, data2, MaxValue)

    }, function (error) {


    });

    $scope.DrawGraph = function (data1, data2, MaxValue) {

     
        jq13('#jqChart1').jqChart({
            title: { text: ' ' },
            animation: { duration: 1 },
            shadows: {
                enabled: false
            },

            options: {
                responsive: true,
                maintainAspectRatio: false
            },
            legend: {
                display: false,

            },

            axes: [
                {
                    name: 'Amount',
                    type: 'linear',
                    location: 'left'
                    //minimum: 0,
                    //maximum: 750000,
                    //interval: 20000,

                },
                {
                    name: 'Number',
                    type: 'linear',
                    location: 'right'
                    //minimum: 0,
                    //maximum: MaxValue,
                    //interval: 50,
                }
                //,
                //},
                //{

                //    location: 'bottom',

                //    labels: {
                //        angle:-45
                //    }
                //}
            ],

            series: [
                {
                    type: 'column',
                    title: 'Number',
                    fillStyle: '#3e527c',
                    axisY: 'Number',
                    data: data1,


                    //pointWidth: 0.8,
                    //markers: {
                    //    size: 10, type: 'circle',
                    //    strokeStyle: 'black', lineWidth: 1
                    //},

                },

                {
                    type: 'column',
                    title: 'Amount',
                    fillStyle: '#232d41',
                    axisY: 'Amount',
                    data: data2,
                    //pointWidth: 0.8,
                    //markers: {
                    //    size: 10, type: 'circle',
                    //    strokeStyle: 'black', lineWidth: 1
                    //},
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