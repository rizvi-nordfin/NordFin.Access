var $ = jQuery.noConflict();
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
    $("#NordfinContentHolder_grdCustomer tr:first").before("<tr id='trHead' class='trHead' ><th class='labelcolor itemalign trHead'> " + "" + "</th>" + "<th class='itemalign trHead'>" + "" + "</th> " +
        "<th class='itemalign trHead' >" + "" + "</th> " + "<th class='itemalign trHead'>" + '' + "</th>" + "<th class='itemalign trHead'>" + "" + "</th> " +
        "<th class='itemalign trHead'>" + "" + "</th> " + "<th class='itemalign trHead'>" + '' + "</th> " + "<th class='itemalign trHead'>" + '' + "</th> " + + "<th class='itemalign trHead'>"
        + '' + "</th> " + "<th class='itemalign trHead'>" + "" + "</th> " + "<th class='itemalign trHead'>" + "" + "</th> " + "<th id='tdMatch' class='itemalign trHead'>" + '' + "</th> "
        + "<th id='tdExport' class='itemalign trHead'>" + '' + "</th>" +

        + "<th id='tdCheck' class='itemaligntrHead'>" + '' + "</th> " + "</tr>");

    $("#NordfinContentHolder_grdCustomer").append(" <tfoot><tr><td class='labelcolor itemalign'> " + "Summary" + "</td>" + "<td class='itemalign'>" + "" + "</td> " +
        "<td class='itemalign'>" + document.getElementById("NordfinContentHolder_lblSumAmount").textContent + "</td> " + "<td class='itemalign'>" + document.getElementById("NordfinContentHolder_lblFeesAmount").textContent + "</td>" + "<td class='itemalign'>" + "" + "</td> " +
        "<td class='itemalign'>" + "" + "</td> " + "<td class='itemalign'>" + document.getElementById("NordfinContentHolder_lblRemain").textContent + "</td> " + "<td class='itemalign'>" + document.getElementById("NordfinContentHolder_lblTotalRemain").textContent
        + "</td> " + + "<td class='itemalign'>" + "" + "</td> " + "<td class='itemalign'>" + "" + "</td> " + "<td class='itemalign'>" + "" + "</td> " + "<td class='itemalign'>" + document.getElementById("NordfinContentHolder_lblOverPaid").textContent + "</td> " + "</tr></tfoot>");

    addButton();


}


function addButton() {
    
    var element = document.createElement("input");
    element.type = 'button';
    element.value = 'Download';
    element.name = 'btncheck';
    element.onclick = function () {
            document.getElementById("NordfinContentHolder_btnExportreport").OnClientClick = ExportExcel();
    };
    element.classList = 'button button-table downloadButton';
    var foo = document.getElementById("tdExport");
    foo.appendChild(element);
} 
window.onload = function () {
    $addHandler(document, "keydown", onKeyDown);


    $('#divCollapse').click(function () {
        if ($('#divCollapse').hasClass("divcollapse"))
            $('#divCollapse').removeClass("divcollapse");
        else
            $('#divCollapse').addClass("divcollapse");

        if ($('#spnPlus').hasClass("plusIcon")) {


        }
        $('#spnPlus').toggleClass("minusIcon");
        $('#spnMinus').toggleClass("minusIcon");


    });

    var checkID = '';
    $('.checkbox').change(function () {

        if (checkID == '') {
            checkID = this.firstChild.id;
        }
        else if (checkID != this.firstChild.id) {
            $("#" + checkID)[0].checked = false;
            checkID = this.firstChild.id;
        }

    });

  

    


   
    if (this.document.getElementById("NordfinContentHolder_hdnMatch").value == "true") {
        //addInButton();
        $('#divManualInvoiceRow').show(); 
        $('#divMatch').show();
        function addInButton() {
            var element = document.createElement("input");
            element.type = 'button';
            element.value = 'Match Credit';
            element.name = 'btnMatchCredit';
            element.onclick = function () {
                document.getElementById("NordfinContentHolder_btnInvoice").click();
            };
            element.classList = 'button button-table downloadButton matchCredit';
            var foo = document.getElementById("tdMatch");
            foo.appendChild(element);
        }
    }

};


function LinkClick(linkValues) {







    const paramValues = document.getElementById(linkValues.id).getAttribute("invoiceData") + "|" + linkValues.text;

    const overpaidAmt = document.getElementById(linkValues.id).getAttribute("overpaymentData").replace(/\s/g, '');
    const remainAmt = document.getElementById(linkValues.id).getAttribute("remainData").replace(/\s/g, '');


    const sFileName = document.getElementById("NordfinContentHolder_hdnFileName").value + "_" + linkValues.text + "_" + "inv" + ".";

    const sClientName = document.getElementById("NordfinContentHolder_hdnClientName").value;

    var customerData = {
        CustomerNumber: $('#NordfinContentHolder_lblCustomerNumber').text(),
        Name: $('#NordfinContentHolder_lblName').text(),
        Address1: $('#NordfinContentHolder_lblAddress').text(),
        Address2: $('#NordfinContentHolder_lblAddress1').text(),
        PostalCode: $('#NordfinContentHolder_lblPostalCode').text(),
        City: $('#NordfinContentHolder_lblCity').text(),
    }

    var customerJson = JSON.stringify(customerData);
    customerJson = encodeURIComponent(customerJson);
    document.getElementById("NordfinContentHolder_btnOpenModal").click();

    document.getElementById("NordfinContentHolder_iframeModal").src = "frmPaymentInformation.aspx?InvoiceData=" + paramValues + "&Remain=" + remainAmt + "&OverPaid=" + overpaidAmt + "&FileName=" + sFileName + "&ClientName=" + sClientName + "&Customer=" + customerJson + " ";





    $(window).scrollTop(0);
    return false;

}
function onKeyDown(e) {
    if (e && e.keyCode == Sys.UI.Key.esc) {
        var mpu = $("#NordfinContentHolder_closeButton");
        mpu.click();
    }
}

//function PDFViewer(sFileName, sPDFViewerLink) {
//    if (sFileName == "")
//        document.getElementById("NordfinContentHolder_pdfViewer").href = sPDFViewerLink;
//    else
//        document.getElementById("NordfinContentHolder_pdfViewer").href = "Documents/" + sFileName;
//    document.getElementById("NordfinContentHolder_pdfViewer").click();
//    document.getElementById("NordfinContentHolder_pdfViewer").href = "";
//}
function PDFViewer(sFileName, sPDFViewerLink, sSessionId, bResult, buttonID) {
    if (bResult == "False")
        document.getElementById("NordfinContentHolder_pdfViewer").href = sPDFViewerLink;
    else
        document.getElementById("NordfinContentHolder_pdfViewer").href = "Documents/" + sSessionId + "/" + sFileName;
    document.getElementById("NordfinContentHolder_pdfViewer").click();
    document.getElementById("NordfinContentHolder_pdfViewer").href = "";



}


function RestoreMypage() {

    $('#mdlDeleteConfirm').modal({ backdrop: 'static', keyboard: false }, 'show');

    return false;
}


function UpdateInfo() {

    $("#NordfinContentHolder_txtCustomerName").val($('#NordfinContentHolder_lblName').text());

    $("#NordfinContentHolder_txtAddress1").val($('#NordfinContentHolder_lblAddress').text());

    $("#NordfinContentHolder_txtAddress2").val($('#NordfinContentHolder_lblAddress1').text());

    $("#NordfinContentHolder_txtPostalCode").val($('#NordfinContentHolder_lblPostalCode').text());

    $("#NordfinContentHolder_txtCity").val($('#NordfinContentHolder_lblCity').text());

    $("#NordfinContentHolder_txtCountry").val($('#NordfinContentHolder_lblCountry').text());

    $("#NordfinContentHolder_txtEmail").val($('#NordfinContentHolder_lblEmail').text());

    $("#NordfinContentHolder_txtPhoneNumber").val($('#NordfinContentHolder_lblPhone').text());

    $('#mdlAccessInfo').modal({ backdrop: 'static', keyboard: false }, 'show');
    return false;
}


function AllowNumbersPlus(evt) {

    var charCode = (evt.which) ? evt.which : event.keyCode
    if (charCode != 43 && charCode > 31 && (charCode < 48 || charCode > 57))
        return false;
    return true;
}

function ExportExcel() {
    $('#mdlExport').modal({ backdrop: 'static', keyboard: false }, 'show');
    return false;
}
function InvoiceInfo() {

    Gridviewunclick();
    PanelUnClick();
    $('#mdlUpdateConfirm').modal({ backdrop: 'static', keyboard: false }, 'show');
    Gridview();
    PanelClick();
    return false;
}

var positiveRow;
var negativeRow;
function Gridview() {
    $("#NordfinContentHolder_grdInvoiceRemaining tbody tr").click(function (e) {


        const Values = $(this).find(':last-child').text();
        if ((negativeRow != null && negativeRow != undefined) && Values.substring(0, 1) == "-") {
            negativeRow.style.background = "#323E53";

        }
        if ((positiveRow != null && positiveRow != undefined) && Values.substring(0, 1) != "-") {
            positiveRow.style.background = "#323E53";

        }
        if (Values.substring(0, 1) == "-") {
            negativeRow = e.currentTarget;
        }
        else {
            positiveRow = e.currentTarget;
        }
        e.currentTarget.style.background = "#FFB100";
    });
}

function Gridviewunclick() {
    $("#NordfinContentHolder_grdInvoiceRemaining tbody tr").unbind('click');
}
function PanelUnClick() {
    $("#pnlClose").unbind('click');

}

function PanelClick() {

    $('#pnlClose').click(function () {

        $('#mdlUpdateConfirm').modal('toggle');
        location.reload();
    });

    window.location.href = "frmDashboard.aspx";
}

function ShowPopup() {

    if (negativeRow != undefined && positiveRow != undefined) {
        $("#spntext").text($("#spntext").text().replace("XX", $(negativeRow).find(':nth-child(2)').text()));
        $("#spntext").text($("#spntext").text().replace("YY", $(positiveRow).find(':nth-child(2)').text()));
        $('#mdlPopConfirm').modal({ backdrop: 'static', keyboard: false }, 'show');
    }

    return false;
}
function Match() {
    $.ajax({
        type: "POST",
        url: "frmCustomer.aspx/GetMatchInvoices",
        data: JSON.stringify({
            NegativeinvAmount: $(negativeRow).find(':first-child').text() + "|" + $(negativeRow).find(':last-child').text(),
            PositiveinvAmount: $(positiveRow).find(':first-child').text() + "|" + $(positiveRow).find(':last-child').text()
        })


        ,

        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {

            $('#mdlPopConfirm').modal('toggle');
            const resData = response.d;
            const MatchInvoice = JSON.parse(resData).MatchInvoice;

            for (var i = 0; i < MatchInvoice.length; i++) {

                if (MatchInvoice[i].InvoiceID == $(negativeRow).find(':first-child').text().trim()) {
                    $(negativeRow).find(':last-child').text(MatchInvoice[i].Remainingamount);
                    negativeRow.style.background = "#323E53";
                }
                else if (MatchInvoice[i].InvoiceID == $(positiveRow).find(':first-child').text().trim()) {
                    $(positiveRow).find(':last-child').text(MatchInvoice[i].Remainingamount);
                    positiveRow.style.background = "#323E53";
                }

            }



        },
        error: function OnError(xhr) {

        }
    });

    return false;
}

function close() {



    return false;
}

function CreditCheck() {
    $('.featureNotAvailableMsgBG').toggleClass('hidden');
    return false;
}

function showManualInvoice() {
    $('#NordfinContentHolder_ucManualInvoice_txtCustNum').val($('#NordfinContentHolder_lblCustomerNumber').text());
    $('#NordfinContentHolder_ucManualInvoice_txtCustName').val($('#NordfinContentHolder_lblName').text());
    $('#NordfinContentHolder_ucManualInvoice_txtCustContact').val($('#NordfinContentHolder_lblAddress').text());
    $('#NordfinContentHolder_ucManualInvoice_txtCustAddress').val($('#NordfinContentHolder_lblAddress1').text());
    $('#NordfinContentHolder_ucManualInvoice_txtCustPostCode').val($('#NordfinContentHolder_lblPostalCode').text());
    $('#NordfinContentHolder_ucManualInvoice_txtCustCity').val($('#NordfinContentHolder_lblCity').text());
    $('#NordfinContentHolder_ucManualInvoice_hdnTitle').val("Manual Invoice");
    $('#NordfinContentHolder_ucManualInvoice_spnTitle').text("Manual Invoice");
    $('#NordfinContentHolder_mdlManualInvoice').modal({ backdrop: 'static', keyboard: false }, 'show');
    return false;
};


function showManualInvoiceSuccess() {
    $(function () {
        $('#NordfinContentHolder_mdlDeleteConfirm').modal({ backdrop: 'static', keyboard: false }, 'show');
    });
    return false;
};

//function Email(button) {

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