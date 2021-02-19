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


function PDFViewer(sFileName, sPDFViewerLink, sSessionId, bResult, buttonID) {
  
    if (bResult == "False")
        document.getElementById("NordfinContentHolder_pdfViewer").href = sPDFViewerLink;
    else
        document.getElementById("NordfinContentHolder_pdfViewer").href = "Documents/" + sSessionId + "/" + sFileName;//"Documents/" + sFileName;
    document.getElementById("NordfinContentHolder_pdfViewer").click();
    document.getElementById("NordfinContentHolder_pdfViewer").href = "";

   
    
    
}
function EmailIDEnable() {
    
    var splitArray = [];

    splitArray = document.getElementById("NordfinContentHolder_hdnEmailID").value.split('|');
    var emailID = splitArray.unique();
    for (var i = 0; i < emailID.length; i++) {
        document.getElementById("NordfinContentHolder_grdInvoices_btnEmail_" + emailID[i]).setAttribute("download", "1");
    }
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


//function Email(button) {
//    debugger;
//    var custNumber = button.getAttribute("custInvoice");
//    document.getElementById("NordfinContentHolder_txtCustEmail").value = "";


//    var isDownload = button.getAttribute("download");
    
//        $.ajax({
//            type: "POST",
//            url: "frmInvoices.aspx/GetCustEmail",
//            data: '{custNumber:' + custNumber + '}',

//            contentType: "application/json; charset=utf-8",
//            dataType: "json",
//            success: function (response) {
//                debugger;

//                const invoiceNum = button.getAttribute("combineInvoice");

//                document.getElementById("NordfinContentHolder_hdnInvoiceNumber").value = invoiceNum;
//                document.getElementById("NordfinContentHolder_txtCustEmail").value = response.d;

//                $('#mdlUpdateInfo').modal({ backdrop: 'static', keyboard: false }, 'show');



//            },
//            error: function OnError(xhr) {

//            }
//        });
    
//    return false;
//}