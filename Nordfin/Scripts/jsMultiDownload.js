function closex(test) {

    var mpu = window.parent.$("#NordfinContentHolder_closeButton");
    mpu.click();
    return false;
}


function ExportClick() {
    $('#mdlUpdateInfo').modal({ backdrop: 'static', keyboard: false }, 'show');
}


function ProgressBarDisplay() {
    $("#Pnlprogress").css("display", "block");

}


function PdfDownloadClick() {
    PdfDownloadMsgNone();
    $("#PnlDownloadprogress").css("display", "block");
}

function PdfDownloadMsgNone() {
    $("#PnlDownloadMsg").css("display", "none");
    $("#spnDownloadMsg").text("");
}

function ProcessingModal() {
    $('.featureNotAvailablePnlBG').toggleClass('hidden');
}

function MailClose() {
    $(".modal-backdrop").remove();
}

function Downloadlimit() {
    $('.featureNotAvailableBG').toggleClass('hidden');
}


function msgNoneClick() {
    $('.featureNotAvailableBG').toggleClass('hidden');
}
