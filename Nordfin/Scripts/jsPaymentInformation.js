
var jq13 = jQuery.noConflict();
jQuery(document).ready(function () {
    
    $(function () {

        var jq142 = jQuery.noConflict();


        Date.prototype.addDays = function (days) {

            var date = new Date(this.valueOf());
            date.setDate(date.getDate() + days);
            var iMonth = date.getMonth() + 1;
            var month = (iMonth < 10) ? '0' + iMonth : iMonth;
            var day = (date.getDate() < 10) ? '0' + date.getDate() : date.getDate();
            return date.getFullYear() + '-' + month + '-' + day;
        }

        function enableDueDays(date) {



            var sdate = jq142.datepicker.formatDate('yy-mm-dd', date)
            if (jq142.inArray(sdate, DueArray) != -1) {
                return [true];
            }
            return [false];



        }
        jq142("#txtDueDate").datepicker({

            dateFormat: 'yy-mm-dd',
            beforeShowDay: enableDueDays,
            defaultDate: today,

            onClose: function (date, datepicker) {
                collectionStopDate();
            }
        });
       
        var DueArray = [];
        var today = new Date(jq142("#txtDueDate").val());

        var iPurchased = document.getElementById("hdnPurchased").value;

        let dueData = 0;
        if (iPurchased == "0" && jq142("#txtCollectionStatus").val().trim() == "") {
            dueData = 60;
        }
        else {
            jq142("#txtDueDate").attr("disabled", "disabled");
        }
        for (var i = 0; i < dueData; i++) {
            DueArray.push(today.addDays(i));
        }

        var DateArray = [];
        collectionStopDate();
        function collectionStopDate() {
           

            DateArray = [];
            var today = new Date(jq142("#txtDueDate").val());



            for (var i = 0; i < 90; i++) {
                DateArray.push(today.addDays(i));
            }
        }
        function enableAllTheseDays(date) {

           

                var sdate = jq142.datepicker.formatDate('yy-mm-dd', date)
            if (jq142.inArray(sdate, DateArray) != -1) {
                    return [true];
                }
                return [false];

          

        }

        jq142("#txtCollectionStopUntil").on("cut copy paste", function (e) {
            e.preventDefault();
        });
        jq142("#txtCollectionStopUntil").datepicker({

            dateFormat: 'yy-mm-dd',
            beforeShowDay: enableAllTheseDays,
            defaultDate: today,
           
            onClose: function (date, datepicker) {
                if (jq142("#txtCollectionStopUntil").val() == "") {
                    jq142('select option:eq(0)').prop('selected', true);
                }
                if (jq142("#cboCollectionStop").val() == "0") {


                    jq142("#txtCollectionStopUntil").attr("disabled", "disabled");
                }
            }
        });

       
        jq142("#txtCollectionStopUntil").keypress(function () {
            return false;
        });
        document.addEventListener('keydown', logKey);


        jq142("#cboContested").change(function () {
           
            if (jq142("#cboContested").val() == "1") {
                jq142("#cboCollectionStop").val('1');
                jq142("#cboCollectionStop").attr("disabled", "disabled");
                var currentDate = new Date();
                var day = currentDate.getDate();
                var imonth = currentDate.getMonth() + 1;
                var month = (imonth < 10) ? '0' + imonth : imonth;
                var year = currentDate.getFullYear();
                var dateFormat = year + "-" + month + "-" + day;
                jq142("#txtContestedDate").val(dateFormat);
                jq142("#txtContestedDate").attr("disabled", "disabled");
                jq142("#txtCollectionStopUntil").val('');
                
            }
            else {
                document.getElementById("spnBody").innerText = "Are you sure you want to remove ?"
                jq13("#btnYes").unbind();
                angular.element(document.getElementById('AngularDiv')).scope().showModal(function (confirm) {
                  
                    if (confirm) {
                        jq142("#cboCollectionStop").val('0');
                        jq142("#cboCollectionStop").removeAttr("disabled");
                        jq142("#txtContestedDate").val('');
                        jq142("#txtCollectionStopUntil").val('');
                    }
                    else {
                        jq142("#cboContested").val('1');
                    }
                }
                );
            }
            
        });

        function getDate() {
           
           
        }

        jq142("#cboCollectionStop").change(function () {
          
           
            if (jq142("#cboCollectionStop").val() == "0") {


                jq142("#txtCollectionStopUntil").attr("disabled", "disabled");
            }

            else {
                jq142("#txtCollectionStopUntil").removeAttr("disabled");
            }
            jq142("#txtCollectionStopUntil").val("");

            jq142("#txtCollectionStopUntil").focus();
        });

        if (jq142("#cboCollectionStop").val() == "0") {


            jq142("#txtCollectionStopUntil").attr("disabled", "disabled");
        }

    });



});

function closex(test) {

    var mpu = window.parent.$("#NordfinContentHolder_closeButton");
    mpu.click();
    return false;
}

function logKey(e) {
    if (e && e.code == "Escape") {
        var mpu = window.parent.$("#NordfinContentHolder_closeButton");
        mpu.click();
       
    }
}
var app = angular.module("myApp", []);

app.controller("myCtrl", function ($scope, $http) {

   
    $http({
        url: "frmPaymentInformation.aspx/LoadPaymentTable",
        dataType: 'json',
        method: 'POST',
        data: '{}',
        headers: {
            "Content-Type": "application/json; charset=utf-8",
            "X-Requested-With": "XMLHttpRequest"
        }
    }).then(function (response) {
          
        const resData = response.data.d;
        const payList = JSON.parse(resData);

        $scope.PaymentsList = payList.PaymentsList;
        $scope.FeesList = payList.FeesList;
        $scope.InterestList = payList.InterestList;
        $scope.TransactionList = payList.TransactionList;
        if (payList.PayoutList != null) {

            for (var i = 0; i < payList.PayoutList.length; i++) {
               
                if (payList.PayoutList[i].OverpayCreditID == -1) {
                    if (document.getElementById("hdnPaymentCheck").value.indexOf("credit") == -1)
                        document.getElementById("btnCreditPaymentPayout").style.display = "none";
                    if (document.getElementById("hdnPaymentCheck").value.indexOf("overpayment") == -1)
                        document.getElementById("btnOverPaymentPayout").style.display = "none";
                    return;
                }
                if (payList.PayoutList[i].OverpayCreditID == 0) {
                    document.getElementById("btnCreditPaymentPayout").innerText = "Marked for payout";
                    document.getElementById("txtCreditPaymentBankAccount").value = payList.PayoutList[i].BankAccount;
                    document.getElementById("btnCreditPaymentPayout").setAttribute("payoutid", payList.PayoutList[i].PayoutID);
                }
                else {
                    document.getElementById("btnOverPaymentPayout").innerText = "Marked for payout";
                    document.getElementById("txtOverPaymentBankAccount").value = payList.PayoutList[i].BankAccount;
                    document.getElementById("btnOverPaymentPayout").setAttribute("payoutid", payList.PayoutList[i].PayoutID);
                }
            }
        }
      

    }, function (error) {

     
        });



    $scope.feeRemove = function ($event, feeRemove) {
        jq13("#btnYes").unbind();
        document.getElementById("spnBody").innerText = "Are you sure you want to remove ?";
        $scope.showModal(function (confirm) {
            if (confirm) {
                var feeData = {
                    FeeID: feeRemove.FeeID,
                    FeeRemainingAmount: feeRemove.FeeRemainingAmount,
                    FeesCurrency: feeRemove.FeeCurrency
                };
                $http({
                    url: "frmPaymentInformation.aspx/updateFeeList",
                    dataType: 'json',
                    method: 'POST',
                    data: JSON.stringify(feeData),

                    headers: {
                        "Content-Type": "application/json; charset=utf-8",
                        "X-Requested-With": "XMLHttpRequest"
                    }
                }).then(function (response) {
                    
                    const resData = response.data.d;
                    const payList = JSON.parse(resData);
                    $scope.FeesList = payList.FeeResult;

                }, function (error) {
                });
            } else {

            }
        });
        
       

        $event.preventDefault();
    }
   

    $scope.InsertPayout = function (payoutctrlID, $event, OverpayCreditID, buttonID) {
       
        if (document.getElementById(buttonID).innerText.toUpperCase() == "PAYOUT") {

            if (document.getElementById(payoutctrlID).value.trim() == "") {


                $scope.informModal(function (confirm) {


                });


            }
            else {
                jq13("#btnYes").unbind();
                var sMessage = 'Are you sure you want to book this payout to account ' + document.getElementById(payoutctrlID).value + '?';

                document.getElementById("spnBody").innerText = sMessage;

                $scope.showModal(function (confirm) {
                    if (confirm) {

                        var payoutData = {
                            sBankAccount: document.getElementById(payoutctrlID).value,
                            sOverpayCreditID: OverpayCreditID
                        };
                      
                        if (payoutData.sBankAccount != "" && document.getElementById(buttonID).innerText.toUpperCase() == "PAYOUT") {
                            $http({
                                url: "frmPaymentInformation.aspx/insertPayout",
                                dataType: 'json',
                                method: 'POST',
                                data: JSON.stringify(payoutData),

                                headers: {
                                    "Content-Type": "application/json; charset=utf-8",
                                    "X-Requested-With": "XMLHttpRequest"
                                }
                            }).then(function (response) {

                                const resData = response.data.d;
                                const payoutResult = JSON.parse(resData);
                            
                                document.getElementById(buttonID).innerText = "Marked for payout";
                                document.getElementById(buttonID).setAttribute("payoutid", payoutResult.Result);
                                

                            }, function (error) {

                            });
                        }
                       

                    } else {

                    }
                });


            }
        }
        else if (document.getElementById(payoutctrlID).value != "" && document.getElementById(buttonID).innerText.toUpperCase() == "MARKED FOR PAYOUT") {
           
            document.getElementById("btnRemove").setAttribute("buttonid", buttonID);
            if (document.getElementById(buttonID).getAttribute("payoutid") != null) {
                const payoutid = document.getElementById(buttonID).getAttribute("payoutid");
                document.getElementById("btnRemove").setAttribute("payoutid", payoutid);
               
            }

            if (buttonID == "btnCreditPaymentPayout") {
                document.getElementById("lblPayment").innerText = "Credit Payment";
                document.getElementById("txtPayment").value = document.getElementById("txtPayoutCreditPayment").value;
                document.getElementById("txtAccount").value = document.getElementById("txtCreditPaymentBankAccount").value;
            }
            else {
                document.getElementById("lblPayment").innerText = "Over Payment";
                document.getElementById("txtPayment").value =  document.getElementById("txtPayoutOverPayment").value;
                document.getElementById("txtAccount").value = document.getElementById("txtOverPaymentBankAccount").value;
            }
            jq13('#mdlPayout').modal({ backdrop: 'static', keyboard: false }, 'show');


        }
        $event.preventDefault();
    }


    $scope.updateInterest = function ($event,interestRemove) {
        jq13("#btnYes").unbind();
        document.getElementById("spnBody").innerText = "Are you sure you want to remove ?";
        $scope.showModal(function (confirm) {
            if (confirm) {
                const InterestID = interestRemove.InterestID;
                $http({
                    url: "frmPaymentInformation.aspx/updateInterest",
                    dataType: 'json',
                    method: 'POST',
                    data: JSON.stringify({ InterestID }),

                    headers: {
                        "Content-Type": "application/json; charset=utf-8",
                        "X-Requested-With": "XMLHttpRequest"
                    }
                }).then(function (response) {
                    const resData = response.data.d;
                    const payList = JSON.parse(resData);
                    $scope.InterestList = payList.InterestList;
                }, function (error) {
                });
            } else {
              
            }
        });


        
        $event.preventDefault();
    }

    var bCheck = false;
    $scope.showModal = function (callback) {
      
       
        jq13('#mdlDeleteConfirm').modal({ backdrop: 'static', keyboard: false },'show');

        jq13("#btnYes").on("click", function () {
            callback(true);
            jq13("#mdlDeleteConfirm").modal('hide');
           
        });
        jq13("#btnNo").on("click", function () {
            callback(false);
            

        });
        
      
    }
  
    $scope.informModal = function (callback) {

      

        jq13('#mdlInformConfirm').modal({ backdrop: 'static', keyboard: false }, 'show');

       

      

    }
    $scope.btnDownload = function ($event) {
        const FileDetails = {
            ClientName: document.getElementById("hdnClientName").value,
            FileName: document.getElementById("hdnFileName").value
           
        

        };
        $http({
            url: "frmPaymentInformation.aspx/PDFDownload",
            dataType: 'json',
            method: 'POST',
            data: JSON.stringify(FileDetails),

            headers: {
                "Content-Type": "application/json; charset=utf-8",
                "X-Requested-With": "XMLHttpRequest"
            }
        }).then(function (response) {
            const resData = response.data.d;
            const fileList = JSON.parse(resData);

              
            if (fileList.BoolResult == false)
                document.getElementById("pdfViewer").href = window.parent.document.getElementById('NordfinContentHolder_hdnArchiveLink').value + fileList.ViewerLink;
            else
                document.getElementById("pdfViewer").href = "Documents/" + fileList.SessionID + "/" + fileList.FileName;



            document.getElementById("pdfViewer").click();
            document.getElementById("pdfViewer").href = "";

        }, function (error) {
            });

        $event.preventDefault();

    }
    $scope.RemovePayout = function () {

        let payoutID = "0";
        if (document.getElementById("btnRemove").getAttribute("payoutid") != null) {
            payoutID = document.getElementById("btnRemove").getAttribute("payoutid");
        }
        if (payoutID != "0") {
            $http({
                url: "frmPaymentInformation.aspx/RemovePayout",
                dataType: 'json',
                method: 'POST',
                data: JSON.stringify({ PayoutID: payoutID }),

                headers: {
                    "Content-Type": "application/json; charset=utf-8",
                    "X-Requested-With": "XMLHttpRequest"
                }
            }).then(function (response) {
               
                const cbuttonID = document.getElementById("btnRemove").getAttribute("buttonid");
                document.getElementById(cbuttonID).innerText = "Payout";
                if (cbuttonID == "btnCreditPaymentPayout") {
                    document.getElementById("txtCreditPaymentBankAccount").value = "";
                }
                else {
                    document.getElementById("txtOverPaymentBankAccount").value = "";
                }
                

            }, function (error) {
            });
        }
       
    }
    
    $scope.notesUpdate = function ($event) {
       
        var sDueDate = "";
        var sCollectionStopUntil = "";
        var sCollectionStop = "";
        if (document.getElementById("hdnDueDate").value != document.getElementById("txtDueDate").value) {

            sDueDate = document.getElementById("txtDueDate").value;
        }
        //if (document.getElementById("hdnCollectionStopUntil").value != document.getElementById("txtCollectionStopUntil").value) {

            sCollectionStopUntil = document.getElementById("txtCollectionStopUntil").value;
        //}
        if (document.getElementById("hdnCollectionStop").value != document.getElementById("cboCollectionStop").value) {

            sCollectionStop = document.getElementById("cboCollectionStop").value;
        }

        document.getElementById("spnBody").innerText = "Are you sure you want to Update?"
        jq13("#btnYes").unbind();
        $scope.showModal(function (confirm) {
            if (confirm) {
              
                const notesData = [];
                const notes = {
                    DueDateNewValue: sDueDate,
                    CollectionStopUntilNewValue: sCollectionStopUntil,
                    CollectionStopNewValue: sCollectionStop,
                    DueDateOldValue: document.getElementById("hdnDueDate").value,
                    CollectionStopUntilOldValue: document.getElementById("hdnCollectionStopUntil").value,
                    CollectionStopOldValue: document.getElementById("hdnCollectionStop").value,
                    NoteText: document.getElementById("txtNotes").value,
                    Contested: jq13("#cboContested option:selected").text(),
                    ContestedDate: document.getElementById("txtContestedDate").value,
                };

                notesData.push(notes);
                $http({
                    url: "frmPaymentInformation.aspx/insertNotes",
                    dataType: 'json',
                    method: 'POST',
                    data: JSON.stringify({ name: notes }),

                    headers: {
                        "Content-Type": "application/json; charset=utf-8",
                        "X-Requested-With": "XMLHttpRequest"
                    }
                }).then(function (response) {
                    const resData = response.data.d;
                    const payList = JSON.parse(resData);
                    
                    document.getElementById("hdnDueDate").value = payList.NotesInfo.DueDateNewValue;
                    document.getElementById("hdnCollectionStopUntil").value = payList.NotesInfo.CollectionStopUntilNewValue;
                    document.getElementById("hdnCollectionStop").value = payList.NotesInfo.CollectionStopNewValue;
                    document.getElementById("txtNotes").value = '';

                    const NotesList = payList.NotesList;
                    if (jq13("#grdNotes tr td")[0].colSpan==5)
                        jq13("#grdNotes tr:eq(1)").remove(); 
                  
                    for (var i = 0; i < NotesList.length; i++) {
                        jq13("#grdNotes").append("<tr><td class='Notesalign'>" + NotesList[i].InvoiceNumber + "</td>" + "<td class='Notesalign'>" + NotesList[i].NoteType + "</td> " +
                            + "</td>" + "<td class='Notesalign'>" + NotesList[i].NoteDate + "</td> " + "</td>" + "<td class='Notesalign'>" + NotesList[i].UserName + "</td> " +
                            "</td>" + "<td class='Notesalign'>" + NotesList[i].NoteText + "</td> " + "</tr>");

                        if (window.parent.$("#NordfinContentHolder_grdNotes tr td")[0] != undefined && window.parent.$("#NordfinContentHolder_grdNotes tr td")[0].colSpan == 5)
                            window.parent.$("#NordfinContentHolder_grdNotes tr:eq(1)").remove();
                        window.parent.$("#NordfinContentHolder_grdNotes tr:first").after("<tr><td class='Notesalign'>" + NotesList[i].InvoiceNumber + "</td>" + "<td class='Notesalign'>" + NotesList[i].NoteType + "</td> " +
                            + "</td>" + "<td class='Notesalign'>" + NotesList[i].NoteDate + "</td> " + "</td>" + "<td class='Notesalign'>" + NotesList[i].UserName + "</td> " +
                            "</td>" + "<td class='Notesalign'>" + NotesList[i].NoteText + "</td> " + "</tr>");
                    }

                    window.parent.$("#btnNotification").click();
                    

                }, function (error) {
                        
                });
            } else {

            }
        });


      
        $event.preventDefault();

    }

 
});



function PDFViewer(sFileName, sPDFViewerLink, sSessionId, bResult) {

    
}