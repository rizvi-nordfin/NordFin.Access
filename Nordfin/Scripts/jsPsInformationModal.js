﻿
function ParentModal(linkStatus) {

  
    document.getElementById("FrameMaster").src = "frmPSInformation.aspx?Invoice=" + linkStatus.getAttribute("invoice");

    $('#ModalMaster').modal({ backdrop: 'static', keyboard: false }, 'show');
   
    return false;
}