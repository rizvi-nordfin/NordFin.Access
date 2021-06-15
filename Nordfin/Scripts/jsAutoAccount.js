
function ParentModal() {
    document.getElementById("ExportModal").innerText = "Auto Account";
    document.getElementById("FrameMaster").src = "frmAutoAccount.aspx";
    document.getElementById("FrameMaster").style.height = "465px";
    $('#ModalMaster').modal({ backdrop: 'static', keyboard: false }, 'show');

    return false;
}

function GuideModal() {
    $('#mdlAutoAccountConfirm').modal({ backdrop: 'static', keyboard: false }, 'show');
}

function PdfDownloadClick() {
    debugger;
    $('#mdlAutoAccountConfirm').modal('hide');
    window.parent.document.getElementById("FrameMaster").style.height = "500px";
    $("#PnlDownloadprogress").css("display", "block");

}

function ParentReload() {
    window.parent.document.getElementById("NordfinContentHolder_btnAutoAccount").style.display = "none";
    window.parent.$('#mdlMastercontent').css({ left: 0 });
    window.parent.$("#ModalMaster").modal('hide');
}

function isNumber(evt) {
    evt = (evt) ? evt : window.event;
    var charCode = (evt.which) ? evt.which : evt.keyCode;
    if (charCode > 31 && (charCode < 48 || charCode > 57)) {
        return false;
    }
    return true;
}


function EmailValidation() {
    debugger;
    if (/^[a-zA-Z0-9.!#$%&'*+/=?^_`{|}~-]+@[a-zA-Z0-9-]+(?:\.[a-zA-Z0-9-]+)*$/.test($('#txtEMail').val())) {
        return true;
    }
    else {
        alert("You have entered an invalid email address!")
        return false;
    }
}