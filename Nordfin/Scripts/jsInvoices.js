window.onload = function () {
    $addHandler(document, "keydown", onKeyDown);

   
}
Array.prototype.contains = function (v) {
    for (var i = 0; i < this.length; i++) {
        if (this[i] === v) return true;
    }
    return false;
};

Array.prototype.unique = function () {
    var arr = [];
    for (var i = 0; i < this.length; i++) {
        if (!arr.contains(this[i])) {
            arr.push(this[i]);
        }
    }
    return arr;
}

function CreateControl() {
   
     $("#NordfinContentHolder_grdInvoices tr:first").before("<tr id='trHead' class='trHead' ><th class='labelcolor itemalign trHead'> " + "" + "</th>" + "<th class='itemalign trHead'>" + "" + "</th> " +
        "<th class='itemalign trHead' >" + "" + "</th> " + "<th class='itemalign trHead'>" + '' + "</th>" + "<th class='itemalign trHead'>" + "" + "</th> " +
        "<th class='itemalign trHead'>" + "" + "</th> " + "<th class='itemalign trHead'>" + '' + "</th> " + "<th class='itemalign trHead'>" + '' + "</th> " + + "<th class='itemalign trHead'>"
        + '' + "</th> " + "<th class='itemalign trHead'>" + "" + "</th> " + "<th class='itemalign trHead'>" + "" + "</th> " + "<th id='tdMatch' class='itemalign trHead'>" + '' + "</th> " + "<th id='tdMatch' class='itemalign trHead'>" + '' + "</th> "
        + "<th id='tdExport' class='itemalign trHead'>" + '' + "</th>" +

        + "<th id='tdCheck' class='itemaligntrHead'>" + '' + "</th> " + "</tr>");


    addButton();
   

}
function addButton() {

        var element = document.createElement("input");
        element.type = 'button';
        element.value = 'Download';
        element.name = 'btncheck';
        element.onclick = function () {
            document.getElementById("NordfinContentHolder_btnExport").click();
        };
        element.classList = 'invoicesDownloadButton button button-table';
        var foo = document.getElementById("tdExport");
        foo.appendChild(element);
    }


function LinkClick(linkValues) {

    


 


    const paramValues = document.getElementById(linkValues.id).getAttribute("invoiceData") + "|" + linkValues.text;

    const overpaidAmt = document.getElementById(linkValues.id).getAttribute("overpaymentData").replace(/\s/g, '');
    const remainAmt = document.getElementById(linkValues.id).getAttribute("remainData").replace(/\s/g, '');
    
    document.getElementById("NordfinContentHolder_btnOpenModal").click();


    const sFileName = document.getElementById("NordfinContentHolder_hdnFileName").value + "_" + linkValues.text + "_" + "inv" + ".";

    const sClientName = document.getElementById("NordfinContentHolder_hdnClientName").value;

    var customerJson = "";

    document.getElementById("NordfinContentHolder_iframeModal").src = "frmPaymentInformation.aspx?InvoiceData=" + paramValues + "&Remain=" + remainAmt + "&OverPaid=" + overpaidAmt + "&FileName=" + sFileName + "&ClientName=" + sClientName + "&Customer=" + customerJson + " ";

 
    $(window).scrollTop(0);
    return false;
   
}


function onKeyDown(e) {
    if (e && e.keyCode == Sys.UI.Key.esc) {
        var mpu =$("#NordfinContentHolder_closeButton");
        mpu.click();
    }
}


function PDFViewer(sFileName, sPDFViewerLink, sSessionId, bResult, buttonID, collectionStatus) {
  
    const pathData = JSON.parse(sFileName);
    $(".modal-backdrop").remove();
    ExportClick(2, collectionStatus);
    PDFDownloadMultiClick(pathData)
    $("#PnlDownloadMsg").css("display", "block");
   
    $("#spnDownloadMsg").text("Downloaded Successfully!");
   
   
}


function PDFDownloadMultiClick(pathData) {
   
    if (pathData[0].FileName != "")
        document.getElementById("pdfInvoices").contentWindow.document.location.href = "frmPdfMultiDownload.aspx?FileName=" + pathData[0].FileName;
    if (pathData[1].FileName != "")
        document.getElementById("pdfDC").contentWindow.document.location.href = "frmPdfMultiDownload.aspx?FileName=" + pathData[1].FileName;
    if (pathData[2].FileName != "")
        document.getElementById("pdfRemind").contentWindow.document.location.href = "frmPdfMultiDownload.aspx?FileName=" + pathData[2].FileName;
   

}

function EmailIDEnable() {
    
    var splitArray = [];

    splitArray = document.getElementById("NordfinContentHolder_hdnEmailID").value.split('|');
    var emailID = splitArray.unique();
    for (var i = 0; i < emailID.length; i++) {
        document.getElementById("NordfinContentHolder_grdInvoices_btnEmail_" + emailID[i]).setAttribute("download", "1");
    }
}


function ExportClick(IsEmail, downloadList,bSent) {
   
    $(".modal-backdrop").remove();
    $(".modal-backdrop").remove();
    if (IsEmail == 1) {
        $('#mdlUpdateInfo').modal({ backdrop: 'static', keyboard: false }, 'show');
        $('#mdlExport').modal('hide');
        if (bSent != undefined && bSent != null) {
            $("#PnlMsg").css("display", "block");
            if (bSent) {
                $("#spnMsg").text("Mail Sent Successfully!");
            }
            else {
                $("#spnMsg").text("Something went wrong please contact nordfin!");
            }
            
        }
       
    }
    else {
        let bModal = false;
        let pdfArchive = "";
        if (downloadList != undefined && downloadList != null && downloadList!="") {
           
            const downloadData = JSON.parse(downloadList);
            for (var i = 0; i < downloadData.length; i++) {

                if (downloadData[i].InvoiceName == "") {
                  
                }
                else if (downloadData[i].InvoiceName.toUpperCase() == "DC")
                    $("#NordfinContentHolder_pnlDC").css("visibility", downloadData[i].Status);
                else if (downloadData[i].InvoiceName.toUpperCase() == "REM")
                    $("#NordfinContentHolder_pnlRemind").css("visibility", downloadData[i].Status);
            }
            
 
            if (IsEmail != 2) {
                $('#NordfinContentHolder_chkRemind').attr('checked', true);
                $('#NordfinContentHolder_chkDC').attr('checked', true);
            }
            bModal = downloadData.find(function (item, i) {
                if (item.Status != "hidden") {

                    return true;
                }
                else {
                    pdfArchive = downloadData[i].PDFArchive
                }
            });
        }
       
       
        if (bModal)
            $('#mdlExport').modal({ backdrop: 'static', keyboard: false }, 'show');
        else
            PDFViewerArchive(pdfArchive);
    }
   
   
}



function ProgressBarDisplay() {
    $("#Pnlprogress").css("display", "block");
    
}

function PDFViewerArchive(sPDFViewerLink) {
    document.getElementById("NordfinContentHolder_pdfViewer").href = sPDFViewerLink;
    document.getElementById("NordfinContentHolder_pdfViewer").click();
    document.getElementById("NordfinContentHolder_pdfViewer").href = "";
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