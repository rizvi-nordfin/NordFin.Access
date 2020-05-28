String.prototype.reverse = function () {
    return this.split('').reverse().join('');
}

var CustomerRegion = [];
var sMarkerRegion = "";
var app = angular.module("myApp", []);

app.controller("myCtrl", function ($scope, $http) {




    if ($("#NordfinContentHolder_btnInvoiceData").hasClass("buttonDefault"))
    {

        $http({
            url: "frmDashboard.aspx/LoadBatchVolume",
            dataType: 'json',
            method: 'POST',
            data: '{}',
            headers: {
                "Content-Type": "application/json; charset=utf-8",
                "X-Requested-With": "XMLHttpRequest"
            }
        }).then(function (response) {


            const resData = response.data.d;
            const batchvloumeList = JSON.parse(resData);

            $scope.BatchVolumeList = batchvloumeList.BatchVolumeList;


        }, function (error) {

           
        });





        $scope.InvoiceCustData = function (divVisible, $event, divInVisible, buttonClick, buttonUnclick) {

            document.getElementById(divVisible).style.display = "block";
            document.getElementById(buttonClick).className = "button form-control";
            document.getElementById(buttonUnclick).className = "button form-control";
            document.getElementById(buttonClick).blur();

            document.getElementById(divInVisible).style.display = "none";




            $event.preventDefault();
        }





        $scope.totalChart = function ($event) {
            document.getElementById("divChart").style.display = "block";

            document.getElementById("divCurrentChart").style.display = "none";
            document.getElementById("NordfinContentHolder_btnTotal").className = "button form-control";
            document.getElementById("NordfinContentHolder_btnThisYear").className = "button form-control";
            document.getElementById("NordfinContentHolder_btnTotal").blur();
            if ($event != undefined)
                $event.preventDefault();

            //ThisCurrent.data.datasets[0].data[0] = parseFloat(document.getElementById('NordfinContentHolder_hdnOntimeAmount').value.replace(',', '.'));
            //ThisCurrent.update(); 
        }

        $scope.thisyearChart = function ($event) {
            $scope.CurrentChart();
            document.getElementById("divChart").style.display = "none";

            document.getElementById("divCurrentChart").style.display = "block";
            document.getElementById("NordfinContentHolder_btnThisYear").className = "button form-control";
            document.getElementById("NordfinContentHolder_btnTotal").className = "button form-control";
            document.getElementById("NordfinContentHolder_btnThisYear").blur();
            $event.preventDefault();

        }


        $scope.fillChart = function (OntimeAmount, LateAmount, RemainAmount, DCAmount, ExtAmount, Ontime, Late, Remain, DC, Ext) {


            //get the bar chart canvas
            var ctxCurrent = document.getElementById("myChart");


            //bar chart data
            var dataCurrent = {

                labels: ["On Time", "Late", "Reminders", "DC", "Ext"],

                datasets: [
                    {

                        label: "Amount",
                        data: [OntimeAmount, LateAmount, RemainAmount, DCAmount, ExtAmount],
                        backgroundColor: [
                            "rgb(255,177,0)",
                            "rgb(255,177,0)",
                            "rgb(255,177,0)",
                            "rgb(255,177,0)",
                            "rgb(255,177,0)"
                        ],
                        borderColor: [
                            "rgb(255,177,0)",
                            "rgb(255,177,0)",
                            "rgb(255,177,0)",
                            "rgb(255,177,0)",
                            "rgb(255,177,0)"
                        ],
                        borderWidth: 1
                    },

                    {
                        label: "Number",
                        indexLabelFontColor: "white",
                        data: [Ontime, Late, Remain, DC, Ext],
                        backgroundColor: [
                            "rgba(50,150,200,0.3)",
                            "rgba(50,150,200,0.3)",
                            "rgba(50,150,200,0.3)",
                            "rgba(50,150,200,0.3)",
                            "rgba(50,150,200,0.3)"
                        ],
                        borderColor: [
                            "rgba(50,150,200,1)",
                            "rgba(50,150,200,1)",
                            "rgba(50,150,200,1)",
                            "rgba(50,150,200,1)",
                            "rgba(50,150,200,1)"
                        ],
                        borderWidth: 1
                    }
                ]
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
                            return data.datasets[tooltipItems.datasetIndex].label + ': ' + tooltipItems.yLabel + '%';
                        }
                    }
                },
                scales: {
                    yAxes: [{
                        ticks: {
                            min: 0


                        }
                    }]
                }
            };

            //create Chart class object
            var ThisCurrent = new Chart(ctxCurrent, {
                type: "bar",
                data: dataCurrent,
                options: options
            });




            Chart.defaults.global.defaultFontColor = 'white';
            Chart.defaults.global.defaultFontFamily = 'Arial';
            Chart.defaults.global.defaultFontSize = 11;


            $scope.totalChart();
        }



        $scope.fillCurrentChart = function (OntimeAmount1, LateAmount, RemainAmount, DCAmount, ExtAmount, Ontime, Late, Remain, DC, Ext) {


            //get the bar chart canvas
            var ctxCurrent = document.getElementById("currentChart");


            //bar chart data
            var dataCurrent = {
                indexLabelFontColor: "white",
                labels: ["On Time", "Late", "Reminders", "DC", "Ext"],

                datasets: [
                    {

                        label: "Amount",
                        data: [OntimeAmount1, LateAmount, RemainAmount, DCAmount, ExtAmount],
                        backgroundColor: [
                            "rgb(255,177,0)",
                            "rgb(255,177,0)",
                            "rgb(255,177,0)",
                            "rgb(255,177,0)",
                            "rgb(255,177,0)"
                        ],
                        borderColor: [
                            "rgb(255,177,0)",
                            "rgb(255,177,0)",
                            "rgb(255,177,0)",
                            "rgb(255,177,0)",
                            "rgb(255,177,0)"
                        ],
                        borderWidth: 1
                    },

                    {
                        label: "Number",
                        indexLabelFontColor: "white",
                        data: [Ontime, Late, Remain, DC, Ext],
                        backgroundColor: [
                            "rgba(50,150,200,0.3)",
                            "rgba(50,150,200,0.3)",
                            "rgba(50,150,200,0.3)",
                            "rgba(50,150,200,0.3)",
                            "rgba(50,150,200,0.3)"
                        ],
                        borderColor: [
                            "rgba(50,150,200,1)",
                            "rgba(50,150,200,1)",
                            "rgba(50,150,200,1)",
                            "rgba(50,150,200,1)",
                            "rgba(50,150,200,1)"
                        ],
                        borderWidth: 1
                    }
                ]
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
                            return data.datasets[tooltipItems.datasetIndex].label + ': ' + tooltipItems.yLabel + '%';
                        }
                    }
                },
                scales: {
                    yAxes: [{
                        ticks: {
                            min: 0
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




        }

        $scope.CurrentChart = function () {
            $http({
                url: "frmDashboard.aspx/LoadCurrentYearChart",
                dataType: 'json',
                method: 'POST',
                data: '{}',
                headers: {
                    "Content-Type": "application/json; charset=utf-8",
                    "X-Requested-With": "XMLHttpRequest"
                }
            }).then(function (response) {


                const resData = response.data.d;
                const resValues = JSON.parse(resData);

                const chartValues = resValues.Chart;
                $scope.fillCurrentChart(parseFloat(chartValues.OntimeAmount.replace(',', '.')), parseFloat(chartValues.LateAmount.replace(',', '.')), parseFloat(chartValues.RemainAmount.replace(',', '.')),
                    parseFloat(chartValues.DCAmount.replace(',', '.')), parseFloat(chartValues.ExtAmount.replace(',', '.')),
                    parseFloat(chartValues.Ontime.replace(',', '.')), parseFloat(chartValues.Late.replace(',', '.')), parseFloat(chartValues.Remain.replace(',', '.')), parseFloat(chartValues.DC.replace(',', '.')),
                    parseFloat(chartValues.Ext.replace(',', '.')));

            }, function (error) {

                
            });
        }
        $scope.fillChart(parseFloat(document.getElementById('NordfinContentHolder_hdnOntimeAmount').value.replace(',', '.')), parseFloat(document.getElementById('NordfinContentHolder_hdnLateAmount').value.replace(',', '.')),
            parseFloat(document.getElementById('NordfinContentHolder_hdnRemainAmount').value.replace(',', '.')), parseFloat(document.getElementById('NordfinContentHolder_hdnDCAmount').value.replace(',', '.')),
            parseFloat(document.getElementById('NordfinContentHolder_hdnExtAmount').value.replace(',', '.')), parseFloat(document.getElementById('NordfinContentHolder_hdnOntime').value.replace(',', '.')),
            parseFloat(document.getElementById('NordfinContentHolder_hdnLate').value.replace(',', '.')), parseFloat(document.getElementById('NordfinContentHolder_hdnRemain').value.replace(',', '.')),
            parseFloat(document.getElementById('NordfinContentHolder_hdnDC').value.replace(',', '.')), parseFloat(document.getElementById('NordfinContentHolder_hdnExt').value.replace(',', '.')));



      
    }

    
    else if ($("#NordfinContentHolder_btnCustomerData").hasClass("buttonDefault")) {

        $http({
            url: "frmDashboard.aspx/LoadNoofCustomerData",
            dataType: 'json',
            method: 'POST',
            data: '{}',
            
            headers: {
                "Content-Type": "application/json; charset=utf-8",
                "X-Requested-With": "XMLHttpRequest"    
            }
        }).then(function (response) {


            const resData = response.data.d;
            const custDataList = JSON.parse(resData);

            $scope.CustomerData = custDataList.CustomerList;

            $scope.Demographics = custDataList.Demographics;
            $scope.TotalCust = custDataList.TotalCust;
            //arrayMap.push(custDataList.CustomerMapList);
           // $scope.regionMapList(custDataList.CustomerMapList);  
            try {
                const CustomerRegionList = custDataList.CustomerRegionList;

                for (var i = 0; i < CustomerRegionList.length; i++) {
                    CustomerRegion.push({ name: CustomerRegionList[i].CustRegion.trim(), total: CustomerRegionList[i].CustTotal });
                }
            }
            catch(err){

            }

            const InvoiceAmount = custDataList.InvoiceAmount;
            const InvoiceNumber = custDataList.InvoiceNumber;
            var data1 = [];
            var data2 = [];
            for (var i = 0; i < InvoiceAmount.length; i++) {
                data1.push([InvoiceAmount[i].InvoiceDate, parseInt(InvoiceAmount[i].Total.replace(/\s/g, ''))]);
              
            }
         
            let MaxValue = 0;
            for (var i = 0; i < InvoiceNumber.length; i++) {
                const Total = parseInt(InvoiceNumber[i].Total.replace(/\s/g, ''));
                data2.push([InvoiceNumber[i].InvoiceDate, Total ]);
                if (Total > MaxValue)
                    MaxValue = Total;

            }

          
            $scope.DrawGraph(data1, data2, MaxValue)


            try {


               
                var custArray = [];
                var PercentArray = [];
                var MaleCount = custDataList.CustomerList.filter(function (list) {
                    return list.custType.toUpperCase() == 'MALE';
                });

             
                var FemaleCount = custDataList.CustomerList.filter(function (list) {
                    return list.custType.toUpperCase() == 'FEMALE';
                });

                if (FemaleCount.length > 0) {
                    custArray.push(parseInt(FemaleCount[0].custNumber.replace(/\s/g, '')))
                    PercentArray.push(FemaleCount[0].custPercentage);
                }
                else {
                    custArray.push(0);
                }

                if (MaleCount.length > 0) {
                    custArray.push(parseInt(MaleCount[0].custNumber.replace(/\s/g, '')))
                    PercentArray.push(MaleCount[0].custPercentage);
                }
                else {
                    custArray.push(0);
                }

                var CompanyCount = custDataList.CustomerList.filter(function (list) {
                    return list.custType.toUpperCase() == 'COMPANY';
                });
               
                if (CompanyCount.length > 0) {
                    custArray.push(parseInt(CompanyCount[0].custNumber.replace(/\s/g, '')))
                    PercentArray.push(CompanyCount[0].custPercentage);
                  
                }
                else {
                    custArray.push(0);
                }
               
                var ctx = document.getElementById("PieChart");
                var myChart = new Chart(ctx, {
                    type: 'doughnut',
                    data: {
                        labels: ['Female', 'Male', 'Organisation'],
                        datasets: [{

                            data: [custArray[0], custArray[1], custArray[2]],

                            //    parseInt(custDataList.CustomerList[0].custNumber.replace(/\s/g, '')), parseInt(custDataList.CustomerList[1].custNumber.replace(/\s/g, '')),
                            //parseInt(custDataList.CustomerList[2].custNumber.replace(/\s/g, ''))],
                            backgroundColor: [
                                'rgba(62, 75, 100, 1)',
                                'rgba(35, 45, 65, 1)',
                                'rgb(73, 102, 152)'

                            ],
                            borderColor: [
                                'rgba(62, 75, 100, 1)',
                                'rgba(35, 45, 65, 1)',
                                'rgb(50, 62, 83)'

                            ],
                            borderWidth: 1,
                            hoverBackgroundColor: ['rgb(255, 177, 0)',
                                'rgb(255, 177, 0)',
                                'rgb(255, 177, 0)',
                                ]
                        }]
                    },
                    options: {
                        cutoutPercentage: 40,

                        responsive: true,
                        legend: {
                            display: true,
                            position: 'top',
                            labels: {
                                fontColor: '#A9BFD5',
                              
                            },
                            onHover: function (e) {
                                e.target.style.cursor = 'pointer';
                            }
                        },

                        tooltips: {
                            callbacks: {

                                label: function (tooltipItem, data) {
                                    debugger;
                                    var dataset = PercentArray[tooltipItem.index];

                                    return dataset + "%";

                                    //var dataset = data.datasets[tooltipItem.datasetIndex];
                                    //var total = dataset.data.reduce(function (previousValue, currentValue, currentIndex, array) {
                                    //    return previousValue + currentValue;
                                    //});
                                    //var currentValue = dataset.data[tooltipItem.index];
                                    //var precentage = ((currentValue / total) * 100).toFixed(2);
                                    //return precentage + "%";
                                }
                            }
                        }


                    },

                });


            }

            catch (err) { }

            $scope.DrawAgePieChart(custDataList.Demographics);
            $scope.MapRegionSweden(CustomerRegion);
            $scope.MapRegionFinland(CustomerRegion);

            $scope.VectorMap(custDataList.MarkerRegion);
            $scope.CustomerGrowth = custDataList.CustomerGrowth;
            sMarkerRegion = custDataList.MarkerRegion;
            $scope.CustomerRegion =custDataList.CustomerRegionList;
        }, function (error) {

           
        });


        $scope.VectorMap = function (MarkerValues) {
        
            var arrMarkers = [];
            var Marker = MarkerValues.split('^');
            for (var i = 0; i < Marker.length; i++) {
                var sMarkers = [];
                var lon = [];
                sMarkers.name = Marker[i].split('|')[0];
                lon.push(Marker[i].split('|')[1].split(',')[0]);
                lon.push(Marker[i].split('|')[1].split(',')[1]);
                sMarkers.lanlon = lon;
                arrMarkers.push(sMarkers);
            }
            $('#world-map-markers').vectorMap({
                map: 'world_mill_en',
                normalizeFunction: 'polynomial',
                hoverOpacity: 0.7,
                hoverColor: false,
                backgroundColor: 'transparent',
                regionStyle: {
                    initial: {
                        fill: 'rgba(210, 214, 222, 1)',
                        'fill-opacity': 5,
                        stroke: 'none',
                        'stroke-width': 0,
                        'stroke-opacity': 1
                    },
                    hover: {
                        'fill-opacity': 0.7,
                        cursor: 'pointer'
                    },
                    selected: {
                        fill: 'yellow'
                    },
                    selectedHover: {}
                },
                markerStyle: {
                    initial: {
                        fill: '#FFB100',
                        stroke: '#111'
                    }
                },
                focusOn: {
                    x: 0.6,
                    y: -0.6,
                    scale: 2
                },
                //for(var i = 0; i<sMarkers.split(',').length; i++) {

                markers:

                    arrMarkers.map(function (h) {
                      
                        return {

                            name: h.name,
                            latLng:

                                h.lanlon
                        }
                    })




                //}

                ,

                onRegionClick: function (event, code) {
                   
                    $('#divSweden').hide();
                    $('#divFinland').hide();
                    $('#updateInfoModalLabel').text('');
                    if (code == "SE" && sMarkerRegion.toUpperCase().indexOf("SWEDEN")>-1) {
                        $('#divSweden').show();
                        $('#updateInfoModalLabel').text('SWEDEN'); 
                    }
                    else if (code == "FI" && sMarkerRegion.toUpperCase().indexOf("FINLAND") > -1) {
                        $('#divFinland').show();
                        $('#updateInfoModalLabel').text('FINLAND'); 
                    }
                    if ($('#updateInfoModalLabel').text() != "") {
                        // $('.featureNotAvailableBG').toggleClass('hidden');
                      
                        var isChrome = !!window.chrome && (!!window.chrome.webstore || !!window.chrome.runtime);
                        if (isChrome)
                            $('#mdlUpdateInfo').modal({ backdrop: 'static', keyboard: false }, 'show');
                        else
                            $('.featureNotAvailableBG').toggleClass('hidden');

                    }
                    //$("#svg_7")[0].textContent = '700';
                },
            });


            function getRegions(data) {
                var result = {};
                $.each(data, function (i, country) {
                    result[data[i].Country] = {
                        tooltip: 'Test',
                        attr: {
                            fill: data[i].Color
                        }
                    };
                });
                return result;
            }




        }

        $scope.MapRegion = function ($event) {

            
            $http({
                url: "frmDashboard.aspx/GetMapRegion",
                dataType: 'json',
                method: 'POST',
                data: '{IsMatch:"'+$("#chkMatch").is(":checked").toString()+'" }',

                headers: {
                    "Content-Type": "application/json; charset=utf-8",
                    "X-Requested-With": "XMLHttpRequest"
                }
            }).then(function (response) {


                const resData = response.data.d;
                const custDataList = JSON.parse(resData);
                $scope.regionMapList(custDataList.CustomerMapList);

            }, function (error) {


            });

            $event.preventDefault();
        }
   
        $scope.MapRegionSweden = function (CustomerRegion) {
           
           var SwedenRegion = new Array();
           SwedenRegion.push({ title: 'SE-AB', name: 'Stockholm' });
           SwedenRegion.push({ title: 'SE-AC', name: 'Västerbotten' });
           SwedenRegion.push({ title: 'SE-BD', name: 'Norrbotten' });
           SwedenRegion.push({ title: 'SE-C', name: 'Uppsala' });
           SwedenRegion.push({ title: 'SE-D', name: 'Södermanland' });
           SwedenRegion.push({ title: 'SE-E', name: 'Östergötland' });
           SwedenRegion.push({ title: 'SE-F', name: 'Jönköping' });
           SwedenRegion.push({ title: 'SE-G', name: 'Kronoberg' });
           SwedenRegion.push({ title: 'SE-H', name: 'Kalmar' });
           SwedenRegion.push({ title: 'SE-I', name: 'Gotland' });
           SwedenRegion.push({ title: 'SE-K', name: 'Blekinge' });
           SwedenRegion.push({ title: 'SE-M', name: 'Skåne' });
           SwedenRegion.push({ title: 'SE-N', name: 'Halland' });
           SwedenRegion.push({ title: 'SE-O', name: 'Västra Götaland' });
           SwedenRegion.push({ title: 'SE-S', name: 'Värmland' });
           SwedenRegion.push({ title: 'SE-T', name: 'Örebro' });
           SwedenRegion.push({ title: 'SE-U', name: 'Västmanland' });
           SwedenRegion.push({ title: 'SE-W', name: 'Dalarna' });
           SwedenRegion.push({ title: 'SE-X', name: 'Gävleborg' });
           SwedenRegion.push({ title: 'SE-Y', name: 'Västernorrland' });
           SwedenRegion.push({ title: 'SE-Z', name: 'Jämtland' });

            var ResultRegion = [];

            var gaugeCount = 100;
            for (var k = 0; k < CustomerRegion.length; k++) {


                var Values = SwedenRegion.filter(function (e) {
                    return e.name == CustomerRegion[k].name;
                });

                if (Values.length > 0) {
                    ResultRegion[Values[0].title] = { id: Values[0].title, title: Values[0].name + '<br/>' + '<b>Customers:</b>' + CustomerRegion[k].total, gaugeValue: gaugeCount };
                 
                    gaugeCount = gaugeCount - 5;
                }

            }
            $("#mapsvgSweden").mapSvg({
                width: 345.62482, height: 792.52374,
                colors: {
                    baseDefault: "#000000", background: "#38445d",
                    selected: 40, hover: "#FFB100", directory: "#fafafa", status: {}, base: "#ffffff", stroke: "#2e2424"
                }, regions: {
                    ResultRegion


                }, viewBox: [0, 0, 345.62482, 792.52374], zoom: {
                    on: false, limit: [-30, 100], delta: 2,
                    buttons: { on: true, location: "right" }, mousewheel: true
                }, tooltips: { mode: "title", on: false, priority: "local", position: "bottom-right" },
                gauge: {
                    on: true, labels: { low: "low", high: "high" },
                    colors: {
                        lowRGB: { r: 85, g: 0, b: 0, a: 1 },
                        highRGB: { r: 238, g: 0, b: 0, a: 1 }, low: "#dadae0", high: "#2c3850", diffRGB: { r: 153, g: 0, b: 0, a: 0 }
                    }, min: 0, max: false
                }, source: "/Images//sweden.svg", title: "Sweden", responsive: "0"
            });
     

        }
      

        $scope.MapRegionFinland = function (CustomerRegion) {
            var FinlandRegion = new Array();

            FinlandRegion.push({ title: 'FI-01', name: 'Åland Islands' });
            FinlandRegion.push({ title: 'FI-02', name: 'South Karelia' });
            FinlandRegion.push({ title: 'FI-03', name: 'Southern Ostrobothnia' });
            FinlandRegion.push({ title: 'FI-04', name: 'Southern Savonia' });
            FinlandRegion.push({ title: 'FI-05', name: 'Kainuu' });
            FinlandRegion.push({ title: 'FI-06', name: 'Tavastia Proper' });
            FinlandRegion.push({ title: 'FI-07', name: 'Central Ostrobothnia' });
            FinlandRegion.push({ title: 'FI-08', name: 'Central Finland' });
            FinlandRegion.push({ title: 'FI-09', name: 'Kymenlaakso' });
            FinlandRegion.push({ title: 'FI-10', name: 'Lapland' });
            FinlandRegion.push({ title: 'FI-11', name: 'Pirkanmaa' });
            FinlandRegion.push({ title: 'FI-12', name: 'Ostrobothnia' });
            FinlandRegion.push({ title: 'FI-13', name: 'North Karelia' });
            FinlandRegion.push({ title: 'FI-14', name: 'Northern Ostrobothnia' });
            FinlandRegion.push({ title: 'FI-15', name: 'Northern Savonia' });
            FinlandRegion.push({ title: 'FI-16', name: 'Päijänne Tavastia' });
            FinlandRegion.push({ title: 'FI-17', name: 'Satakunta' });
            FinlandRegion.push({ title: 'FI-18', name: 'Uusimaa' });
            FinlandRegion.push({ title: 'FI-19', name: 'Finland Proper' });


            var ResultRegion = [];

            var gaugeCount = 100;
            for (var k = 0; k < CustomerRegion.length; k++) {


                var Values = FinlandRegion.filter(function (e) {
                    return e.name == CustomerRegion[k].name;
                });

                if (Values.length > 0) {
                    ResultRegion[Values[0].title] = { id: Values[0].title, title: Values[0].name + '<br/>' + '<b>Customers:</b>' + CustomerRegion[k].total, gaugeValue: gaugeCount  };
                    gaugeCount = gaugeCount - 5;
                }

            }
           
          
            
        
            


      

            $("#mapsvgFinland").mapSvg({
                width: 387.89114, height: 787, lockAspectRatio: false, colors:
                {
                    baseDefault: "#000000", background: "#38445d", selected: 40, hover: "#FFB100", directory: "#fafafa", status: {},
                    base: "#ffffff", stroke: "#38445d"
                }, regions: {
                    ResultRegion
                  

                }, viewBox: [0, -0.010224999999991269, 387.89114, 787],
                gauge: {
                    on: true, labels: { low: "low", high: "high" }
                }
                , zoom: {
                    on: false, limit: [-30, 48], delta: 2, buttons:
                        { on: true, location: "right" }, mousewheel: true
                }, tooltips: { mode: "title", on: false, priority: "local", position: "bottom-right" }, gauge: {
                    on: true, labels: { low: "low", high: "high" },
                    colors: {
                        lowRGB: { r: 85, g: 0, b: 0, a: 1 },
                        highRGB: { r: 238, g: 0, b: 0, a: 1 }, low: "#dadae0", high: "#2c3850", diffRGB: { r: 153, g: 0, b: 0, a: 0 }
                    }, min: 0, max: false
                }, source: "/Images/finland.svg", title: "Finland", responsive: "0"
            });
        }
        $scope.regionMapList = function (CustomerMapList) {

            //const city = 'Juankoski';
            //const proxyurl = "https://cors-anywhere.herokuapp.com/";
            //const url = "https://www.geonames.org/postalcode-search.html?q='" + city + "'&country=FI";

            //const request =
            //    $http({
            //        url: proxyurl + url,
            //        dataType: 'json',
            //        method: 'GET',
            //        data: '{}',
            //        async: false,
            //        headers: {
            //            "Content-Type": "application/json; charset=utf-8",
            //            "X-Requested-With": "XMLHttpRequest"
            //        }
            //    }).then(function (response) {

                   
            //        const region = response.data.substring(response.data.indexOf('Finland</td><td>'));
            //        //alert(response.data.substring(response.data.indexOf('Finland</td><td>'), response.data.indexOf('Finland</td><td>') + response.data.indexOf('</td>')));
                  

            //    }, function (error) {


            //    });

            //const urls = [];
            //CustomerMapList.map((item) => {
            //Sweden Region
            const proxyurl = "https://cors-anywhere.herokuapp.com/";
            const url = "https://www.geonames.org/postalcode-search.html?q=" + CustomerMapList[0].CustomerCity + "&country=SE";


            $http({
                url: proxyurl + url,
                dataType: 'json',
                method: 'GET',
                data: '{}',
                async: false,
                headers: {
                    "Content-Type": "application/json; charset=utf-8",
                    "X-Requested-With": "XMLHttpRequest"
                }
            }).then(function (response) {

                var region = response.data.substring(response.data.indexOf('Sweden</td><td>')).replace("Sweden</td><td>", "");
                region = region.substring(0, region.indexOf("</td>"));
                if (response.data.indexOf("No rows found for") > 0) {
                    region = "-1";
                }
                $scope.updateRegion(region, CustomerMapList[0].CustomerCity, CustomerMapList);
            }, function (error) {


            });
               
            //const proxyurl = "https://cors-anywhere.herokuapp.com/";
            //const url = "https://www.geonames.org/postalcode-search.html?q=" + CustomerMapList[0].CustomerCity + "&country=FI";

               
            //$http({
            //    url: proxyurl + url,
            //    dataType: 'json',
            //    method: 'GET',
            //    data: '{}',
            //    async: false,
            //    headers: {
            //        "Content-Type": "application/json; charset=utf-8",
            //        "X-Requested-With": "XMLHttpRequest"
            //    }
            //}).then(function (response) {

            //    var region = response.data.substring(response.data.indexOf('Finland</td><td>')).replace("Finland</td><td>", "");
            //    region = region.substring(0, region.indexOf("</td>"));
            //    if (response.data.indexOf("No rows found for") > 0) {
            //        region = "-1";
            //    }
            //    $scope.updateRegion(region, CustomerMapList[0].CustomerCity, CustomerMapList);
            //}, function (error) {


            //});

               // urls.push(request);        
                
            //})
           
           
           // Promise.all(urls);//.then((data) => res.json(data));


        }
        $scope.Excel = function ($event) {
            $http({
                url: "frmDashboard.aspx/ExcelDownload",
                dataType: 'json',
                method: 'POST',
                data: {},
                async: false,
                headers: {
                    "Content-Type": "application/json; charset=utf-8",
                    "X-Requested-With": "XMLHttpRequest"
                }
            }).then(function (response) {

               

            }, function (error) {


            });
    $event.preventDefault();
}
        $scope.updateRegion = function (region, city ,CustomerMapList) {

            var regionData = {
                CountryRegion : region,
                CountryPostalCode: city,
                IsMatch:$("#chkMatch").is(":checked").toString(),
            };

            $http({
                url: "frmDashboard.aspx/UpdateCustomerRegion",
                dataType: 'json',
                method: 'POST',
                data: JSON.stringify(regionData),
                async: false,
                headers: {
                    "Content-Type": "application/json; charset=utf-8",
                    "X-Requested-With": "XMLHttpRequest"
                }
            }).then(function (response) {

                CustomerMapList.shift();

                if (CustomerMapList.length != 0)
                    $scope.regionMapList(CustomerMapList);
              
            }, function (error) {


            });

        }

        $scope.reverse = function (sNumber) {
        return this.split('').reverse().join('');
        }
        $scope.DrawAgePieChart = function (demographics) {
           
            //parseFloat(demographics[1].custPercentage.replace(",", ".")).toFixed(2)

            var columnArray=[];
            for (var i = 0; i < demographics.length; i++) {
                columnArray[0] += demographics[i].custColName + ",";
            }
            var ctx = document.getElementById("PieAgeChart");
            var myChart = new Chart(ctx, {
                type: 'doughnut',
                data: {
                    labels: [demographics[0].custColName, demographics[1].custColName, demographics[2].custColName, demographics[3].custColName],
                    datasets: [{

                        data: [parseFloat(demographics[0].custPercentage.replace(",", ".")).toFixed(2), parseFloat(demographics[1].custPercentage.replace(",", ".")).toFixed(2),
                            parseFloat(demographics[2].custPercentage.replace(",", ".")).toFixed(2), parseFloat(demographics[3].custPercentage.replace(",", ".")).toFixed(2)],

                       
                        backgroundColor: [
                            'rgba(62, 75, 100, 1)',
                            'rgba(35, 45, 65, 1)',
                            'rgb(73, 102, 152)',
                            'rgba(43, 68, 118, 1)',

                        ],
                        borderColor: [
                            'rgba(62, 75, 100, 1)',
                            'rgba(35, 45, 65, 1)',

                            'rgb(50, 62, 83)',
                            'rgba(43, 68, 118, 1)',

                        ],
                        borderWidth: 1,
                        hoverBackgroundColor: ['rgb(255, 177, 0)',
                            'rgb(255, 177, 0)',
                            'rgb(255, 177, 0)',
                            'rgb(255, 177, 0)',]
                    }]
                },
                options: {
                    cutoutPercentage: 40,

                    responsive: true,
                    legend: {
                        display: true,
                        position: 'top',
                        labels: {
                            fontColor: '#A9BFD5',

                        },
                        onHover: function (e) {
                            e.target.style.cursor = 'pointer';
                        }
                    },

                    tooltips: {
                        callbacks: {

                            label: function (tooltipItem, data) {
                                
                                var dataset = data.datasets[tooltipItem.datasetIndex];
                               
                                return dataset.data[tooltipItem.index] + "%";
                            }
                        }
                    }


                }

               


            });


        }
        
        var tooltipData = [];
        $scope.DrawGraph = function (data1, data2, MaxValue) {
            debugger;

            var maxNumber= Math.max.apply(Math, data1.map(function (o) {
              
                return o[1];
            }))

            var lineData = [];
            for (var i = 0; i < data1.length; i++) {
                let lineValue = (data1[i][1] / maxNumber) * 100;
                lineValue = lineValue * 500;
                if (lineValue == 50000)
                    lineValue = lineValue - 2500;
                lineData.push(lineValue);
                tooltipData.push(data1[i][1]);
            }
         
            $('#jqChart').jqChart({
                title: { text: ' ' },
                animation: { duration: 1 },
                shadows: {
                    enabled: false
                },
               
                options: {
                    responsive: true,
                },
                legend: {
                    display: false,
                   
                },
               
                axes: [
                    {
                        name: 'Amount',
                        type: 'linear',
                        location: 'left',
                        minimum: 0,
                        maximum: 50000,
                        interval: 5000
                    },
                    {
                        name: 'Number',
                        type: 'linear',
                        location: 'right',
                        minimum: 1000,
                        maximum: MaxValue,
                        interval:1000
                    }
                ], 

                series: [
                    {
                        type: 'column',
                        title: 'Amount',
                        fillStyle: '#3e527c',
                        axisY: 'Amount',
                        data: data1,


                        //pointWidth: 0.8,
                        //markers: {
                        //    size: 10, type: 'circle',
                        //    strokeStyle: 'black', lineWidth: 1
                        //},

                    },
                  
                    {
                        type: 'column',
                        title: 'Number',
                        fillStyle: '#232d41',
                        axisY: 'Number',
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
                        data: lineData,
                      
                    },
                   

                ]
            });


            $('#jqChart').bind('tooltipFormat', function (e, data) {

                if (data.series.type == "line") {
                    return "<b>" + data.x + "</b><br />" +
                        "<span style='color:#2C3850;'>" + data.series.title + "</span>" + " : " + "<b>" +
                        tooltipData[data.index].toString().split('').reverse().join('').replace(/((?:\d{2})\d)/g, '$1 ').split('').reverse().join('') + "</b>" + "<br />"
                  }
                else {
                    return "<b>" + data.x + "</b><br />" +
                        "<span style='color:#2C3850;'>" + data.series.title + "</span>" + " : " + "<b>" + data.y.toString().split('').reverse().join('').replace(/((?:\d{2})\d)/g, '$1 ').split('').reverse().join('') + "</b>" + "<br />"
                  }
             
            });
           
        }
    }
});