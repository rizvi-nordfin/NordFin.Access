var your_site_key = '6Lej8boUAAAAANp_ap-WB_vpkfLCLOBskZ9jmw6M';
var renderRecaptcha = function () {
    grecaptcha.render('ReCaptchContainer', {
        'sitekey': '6Lej8boUAAAAANp_ap-WB_vpkfLCLOBskZ9jmw6M',
        'callback': reCaptchaCallback,
        theme: 'dark', //light or dark
        type: 'image'
    });
};
var reCaptchaCallback = function (response) {
    if (response !== '') {
        document.getElementById('lblMessage1').innerHTML = "";
    }
};