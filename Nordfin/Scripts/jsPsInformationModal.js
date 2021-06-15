
function ParentModal(linkStatus) {

    document.getElementById("ExportModal").innerText = "External DC Status";
    document.getElementById("FrameMaster").style.height = "400px";
    document.getElementById("FrameMaster").src = "frmPSInformation.aspx?Invoice=" + linkStatus.getAttribute("invoice");

    $('#ModalMaster').modal({ backdrop: 'static', keyboard: false }, 'show');
   
    return false;
}