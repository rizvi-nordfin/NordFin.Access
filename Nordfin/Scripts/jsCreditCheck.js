


function setCreditScore(score) {
   
    demoGauge = new Gauge(document.getElementById("demo"));
    var opts = {
        angle:-0.15 ,
        lineWidth: 0.1,
        radiusScale: 0.9,
        pointer: {
            length: 0.5,
            strokeWidth: 0.05,
            color: '#FFB100'
        },
        staticLabels: {
            font: "16px sans-serif",
            labels: [20, 40, 70, 100],
            fractionDigits: 0,
            color: '#fff'
        },
        staticZones: [
            { strokeStyle: "#B03820", min: 0, max: 20    },
            { strokeStyle: "#F03E3E", min: 20, max: 40 },
            { strokeStyle: "#178F33", min: 40, max: 70 },
            { strokeStyle: "#30B32D", min: 70, max: 100 }
        ],
        limitMax: false,
        limitMin: false,
        highDpiSupport: true
    };
    demoGauge.setOptions(opts);
    demoGauge.setTextField(document.getElementById("preview-textfield"));
    demoGauge.minValue = 0;
    demoGauge.maxValue = 100;
    demoGauge.set(score);
};

