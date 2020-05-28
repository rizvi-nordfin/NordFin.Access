window.onload = function () {
 
};
var jq3 = jQuery.noConflict();
jQuery(document).ready(function () {
  
    var jq10 = jQuery.noConflict();
    jq10("#NordfinContentHolder_txtFromDate").datepicker({ dateFormat: 'yy-mm-dd' });
    jq10("#NordfinContentHolder_txtToDate").datepicker({ dateFormat: 'yy-mm-dd' });
});
function DateSelection(sHeading) {
  
    jq3("#NordfinContentHolder_txtFromDate").val('');
    jq3("#NordfinContentHolder_txtToDate").val('');
    jq3("#informModalLabel").text(sHeading);
    jq3('#mdlReport').modal({ backdrop: 'static', keyboard: false }, 'show');

    return false;
}


function submitClick() {

    document.getElementById("NordfinContentHolder_hdnExport").value =  jq3("#informModalLabel").text();
    $('#mdlReport').modal('hide');
}


function Message() {
    $('.featureNotAvailableMsgBG').toggleClass('hidden');
    return false;
}