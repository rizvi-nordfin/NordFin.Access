

$(document).ready(function () {
    setCreditScore($('#NordfinContentHolder_hdnCreditScore').val(), $('#NordfinContentHolder_hdnCreditVisible').val())
    function setCreditScore(score, CreditUser) {
        demoGauge = new Gauge(document.getElementById("demo"));
        var opts = {
            angle: -0.15,
            lineWidth: 0.1,
            radiusScale: 0.9,
            pointer: {
                length: 0.5,
                strokeWidth: 0.05,
                color: '#C5E2E6'
            },
            staticLabels: {
                font: "16px sans-serif",
                labels: [20, 40, 70, 100],
                fractionDigits: 0,
                color: '#A9BFD5'
            },
            staticZones: [
                { strokeStyle: "#298496", min: 0, max: 20 },
                { strokeStyle: "#2F99AD", min: 20, max: 40 },
                { strokeStyle: "#45E0FF", min: 40, max: 70 },
                { strokeStyle: "#6AFFF3", min: 70, max: 100 }
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

        if (CreditUser != undefined && CreditUser == 0)
            $('.featureNotAvailableBG').toggleClass('hidden');
    };
});

