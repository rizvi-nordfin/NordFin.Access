function showSucessModels()  {
    $('#successModel').modal({ backdrop: 'static', keyboard: false }, 'show');
    return false;
}

function ShowPopup() {
    $(function () {
        alert($('#txtCustName').val());
        $('#Dialog').modal({ backdrop: 'static', keyboard: false }, 'show');
    });
    return false;
};

function CloseManualInvoice() {
    $(function () {
        $('#NordfinContentHolder_mdlManualInvoice').modal('hide');
    });
    return false;
};


function ValidateAmount(txt, evt) {
    var charCode = (evt.which) ? evt.which : evt.keyCode;
    if (charCode == 45 || charCode == 46) {
        //Check if the text already contains the . character
        if (txt.value.indexOf('.') === -1 || txt.value.indexOf('-') === -1) {
            return true;
        } else {
            return false;
        }
    } else {
        if (charCode > 31 &&
            (charCode < 48 || charCode > 57))
            return false;
    }
    return true;
}

