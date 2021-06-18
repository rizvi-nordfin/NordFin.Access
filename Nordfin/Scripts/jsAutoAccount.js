
function ParentModal() {
    document.getElementById("ExportModal").innerText = "Auto Account";
    document.getElementById("FrameMaster").src = "frmAutoAccount.aspx";
    document.getElementById("FrameMaster").style.height = "425px";
    $('#ModalMaster').modal({ backdrop: 'static', keyboard: false }, 'show');

    return false;
}

function GuideModal() {
    window.parent.document.getElementById("FrameMaster").style.height = "465px";
    $('#mdlAutoAccountConfirm').modal({ backdrop: 'static', keyboard: false }, 'show');
    
}

function PdfDownloadClick() {
    debugger;
    $('#mdlAutoAccountConfirm').modal('hide');
    window.parent.document.getElementById("FrameMaster").style.height = "465px";
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
  
    if (/^[a-zA-Z0-9.!#$%&'*+/=?^_`{|}~-]+@[a-zA-Z0-9-]+(?:\.[a-zA-Z0-9-]+)*$/.test($('#txtEMail').val())) {
        return true;
    }
    else {
        window.parent.$('#mdlMasterConfirm').modal({ backdrop: 'static', keyboard: false }, 'show');
        window.parent.$('#mdlMastercontent').css({ left: 150 });
        window.parent.$('#spnMasterInfo').text('You have entered an invalid email address!');
        return false;
    }
}


function CustomerSupprot() {
    window.parent.$('#mdlMasterConfirm').modal({ backdrop: 'static', keyboard: false }, 'show');
    window.parent.$('#mdlMastercontent').css({ left: 150 });
    window.parent.document.getElementById('FrameMaster').style.height = '425px';
    window.parent.$('#spnMasterInfo').text('Please contact your nordfin contact');
}