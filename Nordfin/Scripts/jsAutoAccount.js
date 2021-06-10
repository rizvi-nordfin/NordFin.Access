
function ParentModal() {
    debugger;

    document.getElementById("ExportModal").innerText = "Auto Account";
    document.getElementById("FrameMaster").src = "frmAutoAccount.aspx";

    $('#ModalMaster').modal({ backdrop: 'static', keyboard: false }, 'show');

    return false;
}