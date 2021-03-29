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
        "<th class='itemalign trHead'>" + "" + "</th> " + "<th class='itemalign trHead'>" + '' + "</th> " + "<th class='itemalign trHead'>" + '' + "</th> " +
        "<th class='itemalign trHead'>" + '' + "</th> " + "<th class='itemalign trHead'>"
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
    const collectionStatus = document.getElementById(linkValues.id).getAttribute("collectionStatus").replace(/\s/g, '');
    const combineInvoice = document.getElementById(linkValues.id).getAttribute("combineInvoice").replace(/\s/g, '');
    const custInvoice = document.getElementById(linkValues.id).getAttribute("custInvoice").replace(/\s/g, '');
    
    document.getElementById("NordfinContentHolder_btnOpenModal").click();


    const sFileName = encodeURIComponent(document.getElementById("NordfinContentHolder_hdnFileName").value);//+ "_" + linkValues.text + "_" + "inv" + ".";

    const sClientName = document.getElementById("NordfinContentHolder_hdnClientName").value;

    var customerJson = "";

    document.getElementById("NordfinContentHolder_iframeModal").src = "frmPaymentInformation.aspx?InvoiceData=" + paramValues + "&CombineInvoice=" + combineInvoice + "&CustomerNumber=" + custInvoice + "&CollectionStatus=" + collectionStatus +  "&Remain=" + remainAmt + "&OverPaid=" + overpaidAmt + "&FileName=" + sFileName + "&ClientName=" + sClientName + "&Customer=" + customerJson + " ";

 
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
   
    $("#PnlDownloadMsg").css("display", "block");
   
    $("#spnDownloadMsg").text("Downloaded Successfully!");
   
}

function EmailIDEnable() {
    
    var splitArray = [];

    splitArray = document.getElementById("NordfinContentHolder_hdnEmailID").value.split('|');
    var emailID = splitArray.unique();
    for (var i = 0; i < emailID.length; i++) {
        document.getElementById("NordfinContentHolder_grdInvoices_btnEmail_" + emailID[i]).setAttribute("download", "1");
    }
}

function ExportClick(IsEmail, pdfArchive, bSent,bMultidownlaod) {
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

        if (pdfArchive == undefined || pdfArchive == "")
            $('#mdlExport').modal({ backdrop: 'static', keyboard: false }, 'show');
        else {
            PDFViewerArchive(pdfArchive);
            if (bMultidownlaod)
                $('#mdlExport').modal({ backdrop: 'static', keyboard: false }, 'show');
        }
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

function PDFDownloadClick(buttonValues) {
    const sInvoiceNumer = document.getElementById(buttonValues.id).getAttribute("combineInvoice");

    $.ajax({
        type: "POST",
        url: "frmInvoices.aspx/PDFDownload",
        data: JSON.stringify({ hdnValue: document.getElementById("NordfinContentHolder_hdnFileName").value, InvoiceNum: sInvoiceNumer, hdnArchieveLink: document.getElementById("NordfinContentHolder_hdnArchiveLink").value, hdnClientName: document.getElementById("NordfinContentHolder_hdnClientName").value }),

        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {

            sFileName = response.d.split('~')[0];
            sPDFViewerLink = response.d.split('~')[1];
            if (sFileName == "")
                document.getElementById("NordfinContentHolder_pdfViewer").href = sPDFViewerLink;
            else
                document.getElementById("NordfinContentHolder_pdfViewer").href = "Documents/" + sFileName;
            document.getElementById("NordfinContentHolder_pdfViewer").click();
            document.getElementById("NordfinContentHolder_pdfViewer").href = "";
        },
        error: function OnError(xhr) {

        }
    });


    return false;
}


