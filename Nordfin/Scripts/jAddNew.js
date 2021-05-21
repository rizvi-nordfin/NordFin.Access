var $ = jQuery.noConflict();
function OpenAddCustomer() {
    $('#mdlAddCustomer').modal({ backdrop: 'static', keyboard: false }, 'show');
    return false;
}

function CloseAddCustomer() {
    ResetControls();
    Page_ClientValidateReset();
    $(".modal-backdrop").remove();
    $('#mdlAddCustomer').modal('hide');
    return false;
}

function showErrorModal(errorMessage) {
    $(".modal-backdrop").remove();
    $('#mdlAddCustomer').modal({ backdrop: 'static', keyboard: false }, 'show');
    var zIndex = 1170 + (10 * $('.modal:visible').length);
    $('#mdlError').css('z-index', zIndex);
    $('#txtError').text(errorMessage);
    $('#mdlError').modal({ backdrop: 'static', keyboard: false }, 'show');
    return false;
};

function showConfirmModal(errorMessage) {
    $(".modal-backdrop").remove();
    $('#mdlAddCustomer').modal({ backdrop: 'static', keyboard: false }, 'show');
    var zIndex = 1170 + (10 * $('.modal:visible').length);
    $('#mdlConfirm').css('z-index', zIndex);
    $('#txtConfirm').text(errorMessage);
    $('#mdlConfirm').modal({ backdrop: 'static', keyboard: false }, 'show');
    return false;
};

function closeErrorModal() {
    $('#mdlError').modal('hide');
    return false;
};

function closeConfirmModal() {
    $('#mdlConfirm').modal('hide');
    return false;
};


function showSuccessModal() {
    $(".modal-backdrop").remove();
    $('#mdlAddCustomer').modal({ backdrop: 'static', keyboard: false }, 'show');
    var zIndex = 1170 + (10 * $('.modal:visible').length);
    $('#mdlSuccess').css('z-index', zIndex);
    $('#mdlSuccess').modal({ backdrop: 'static', keyboard: false }, 'show');
    return false;
};

function customerTypePrivateChanged() {
    if ($('#swtchCorporate').prop('checked')) {
        $('#swtchCorporate').prop('checked', false);
    }

    if ($('#swtchPrivate').prop('checked')) {
        $('#NordfinContentHolder_spnPersonalNumber').text("Social Security Number"); 
        $('#NordfinContentHolder_rfvPersonalNumber').text("Social Security Number is required");
    }
    
    $('#NordfinContentHolder_hdnPrivate').val($('#swtchPrivate').prop('checked'));
    countryChanged();
}

function customerTypeCorporateChanged() {
    if ($('#swtchPrivate').prop('checked')) {
        $('#swtchPrivate').prop('checked', false);
    }

    if ($('#swtchCorporate').prop('checked')) {
        $('#NordfinContentHolder_spnPersonalNumber').text("Registration Number");
        $('#NordfinContentHolder_rfvPersonalNumber').text("Registration Number is required");
    }
    $('#NordfinContentHolder_hdnPrivate').val($('#swtchPrivate').prop('checked'));
    countryChanged();
}

function countryChanged() {
    if ($('#NordfinContentHolder_drpCountry').val() == "SE") {
        $('#NordfinContentHolder_txtPhoneCode').val("+46");
        $('#NordfinContentHolder_hdnPhoneCode').val("+46");
        if ($('#swtchCorporate').prop('checked')) {
            $('#NordfinContentHolder_txtPersonalNumber').prop("placeholder", 'NNNNNNNNNN');
        }
        else if ($('#swtchPrivate').prop('checked')) {
            $('#NordfinContentHolder_txtPersonalNumber').prop("placeholder", 'YYMMDDNNNN');
        }
    }

    if ($('#NordfinContentHolder_drpCountry').val() == "FI") {
        $('#NordfinContentHolder_txtPhoneCode').val("+358");
        $('#NordfinContentHolder_hdnPhoneCode').val("+358");
        if ($('#swtchCorporate').prop('checked')) {
            $('#NordfinContentHolder_txtPersonalNumber').prop("placeholder", 'NNNNNNNN');
        }
        else if ($('#swtchPrivate').prop('checked')) {
            $('#NordfinContentHolder_txtPersonalNumber').prop("placeholder", 'NNNNNNNNNN');
        }
    }
}

function ResetControls() {
    $('#NordfinContentHolder_txtCustomerName').val("");
    $('#NordfinContentHolder_txtCustomerNumber').val("");
    $('#NordfinContentHolder_txtPersonalNumber').val("");
    $('#NordfinContentHolder_txtAddress1').val("");
    $('#NordfinContentHolder_txtAddress2').val("");
    $('#NordfinContentHolder_txtPostalCode').val("");
    $('#NordfinContentHolder_txtCity').val("");
    $('#NordfinContentHolder_txtEmail').val("");
    $('#NordfinContentHolder_txtPhoneNumber').val("");
}

function Page_ClientValidateReset() {
    if (typeof (Page_Validators) != "undefined") {
        for (var i = 0; i < Page_Validators.length; i++) {
            var validator = Page_Validators[i];
            validator.isvalid = true;
            ValidatorUpdateDisplay(validator);
        }
    }
}