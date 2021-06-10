var jq10 = jQuery.noConflict();
function showPDFViewer(base64Pdf) {
    var src = 'data:application/pdf;base64,' + base64Pdf;
    jq10('#iInvoicePdf').attr('src', src)
    if ($(window).width() < 800) {
        jq10('#pdfViewer').css('top', '200px');
    }
    jq10('#pdfViewer').modal({ backdrop: 'static', keyboard: false }, 'show');
    setupModalDialog();
    return false;
}

function closePDFViewer() {
    jq10('#pdfViewer').modal('hide');
    jq10('#pdfViewer').trigger('click');
    return false;
}

function closeErrorModal() {
    jq10('#mdlError').modal('hide');
    return false;
};

function showErrorModal(errorMessage) {
    jq10('#txtError').text(errorMessage);
    var parent = $('#mdlManualInvoice').parent();
    if (parent.length != 0 && parent.attr('id').includes('AngularDiv')) {
        jq10('#mdlError').css('left', '200px');
    }
    if ($(window).width() < 800) {
        jq10('#mdlError').css('left', '-50px');
        jq10('#mdlSuccess').css('left', '-50px');
    }
    jq10('#mdlError').modal({ backdrop: 'static', keyboard: false }, 'show');
    return false;
};

function closeSuccessModal() {
    jq10('#mdlSuccess').modal('hide');
    closeManualInvoice();
    if (!window.location.href.includes("frmPaymentInformation.aspx")) {
        window.location.reload();
    }
    return false;
};

function closeManualInvoice() {
    if (jq10('#NordfinContentHolder_mdlManualInvoice').length === 0) {
        jq10('#mdlManualInvoice').modal('hide');
        return false;
    }
    jq10('#NordfinContentHolder_mdlManualInvoice').modal('hide');
    return false;
};

function showSuccessModal() {
    var zIndex = 1040 + (10 * jq10('.modal:visible').length);
    jq10('#mdlSuccess').css('z-index', zIndex);
    var parent = $('#mdlManualInvoice').parent();
    if (parent.length != 0 && parent.attr('id').includes('AngularDiv')) {
        jq10('#mdlSuccess').css('left', '200px');
    }
    jq10('#mdlSuccess').modal({ backdrop: 'static', keyboard: false }, 'show');
    return false;
};

function showConfirmModal() {
    var zIndex = 1040 + (10 * jq10('.modal:visible').length);
    jq10('#mdlConfirmData').css('z-index', zIndex);
    var parent = $('#mdlManualInvoice').parent();
    //if (parent.length != 0 && parent.attr('id').includes('AngularDiv')) {
    //    jq10('#mdlConfirmData').css('left', '200px');
    //}
    jq10('#mdlConfirmData').modal({ backdrop: 'static', keyboard: false }, 'show');
    return false;
};

function closeConfirmModal() {
    jq10('#mdlConfirmData').modal('hide');
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

function RestrictToTwoDecimal(e) {
    var t = e.value;
    e.value = (t.indexOf(".") >= 0) ? (t.substr(0, t.indexOf(".")) + t.substr(t.indexOf("."), 3)) : t;
}

function setupModalDialog() {
    var zIndex = 1040 + (10 * jq10('.modal:visible').length);
    jq10(this).css('z-index', zIndex);
    setTimeout(function () {
        jq10('.modal-backdrop').not('.modal-stack').css('z-index', zIndex - 1).addClass('modal-stack');
    }, 0);
}

jq10(document).on('show.bs.modal', '.modal', function (event) {
    setupModalDialog();
});

jQuery(document).ready(function () {
    var jq10 = jQuery.noConflict();
    jq10("#NordfinContentHolder_ucManualInvoice_txtInvDate").datepicker({ dateFormat: 'yy-mm-dd' });
    jq10("#NordfinContentHolder_ucManualInvoice_txtDueDate").datepicker({ dateFormat: 'yy-mm-dd' });
});

