$(document).ready(function () {
    try {
        if ($.fn.jquery == "1.10.2" && jq3 != undefined) {
            $ = jq3;
        }
    }
    catch(err){
    }

   
    $('.topMenuOpenDropdown').click(function(){ 
        $('.dropdownMenu').toggleClass('hidden');
        $('.topMenuOpenDropdown').toggleClass('dropdownOpen');
        $('.topMenuSearchContainer').toggleClass('dropdownOpen');
        
    });


    $('.sideContractMenuButtonStatistics button').click(function (e) {
     
        e.preventDefault();
       
        $('.sideMenuStatistics').addClass('hidden');
        
        $('.sideContractMenuStatistics').toggleClass('hidden');
        $('.sideContractMenuButtonStatistics').toggleClass('activated');
        if (!$('.sideContractMenuButtonStatistics').hasClass('activated')) {
            $('.imgContracts').attr('src', 'Images/Contracts.svg');
        }
        else {
            $('.imgContracts').attr('src', 'Images/Contracts_Orange.svg');
        }
    });

    $('.sideMenuButtonStatistics button').click(function (e) {
    
        e.preventDefault();
       
        $('.sideContractMenuStatistics').addClass('hidden');
       
        $('.sideMenuStatistics').toggleClass('hidden');
        $('.sideMenuButtonStatistics').toggleClass('activated');
        if (!$('.sideMenuButtonStatistics').hasClass('activated')) {
          $('.imgStatistics').attr('src','Images/NFC_batches.svg');
        }
        else {
          $('.imgStatistics').attr('src','Images/NFC_batches_orange.svg');
        }
    });

    $('.topMenuSearchAdvanced').click(function () {
        $('.dropdownAdvanceSearch').toggleClass('hidden');

        $('#txtSearch').val("");
    });
    
    $('#pnlNotification').click(function (e) { 
        
        e.preventDefault;
        e.stopImmediatePropagation();
        $('.dropdownNotification').toggleClass('hidden');
        $('#dropdownNotifiedText').html('');
        $('.topMenuNotification').toggleClass('dropdownNotificationcolor');
        $.ajax({
            type: "POST",
            url: "frmDashboard.aspx/Notification",
            data: '{}',

            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (response) {
             
              
                const notificationList = JSON.parse(response.d);

                const NotificationList = notificationList.NotificationList;
                for (var i = 0; i < NotificationList.length; i++) {
                    //if (i == 0) {
                    //    $('#dropdownNotifiedText').append('<table>');
                    //}
                    //$('#grdNotification').append('<div >' + NotificationList[i].NotesUser + '</div> ' + '<div >' + NotificationList[i].NotesText + '</div> <hr/>' );
                    //< tr > <td class='Notesalign'>" + NotificationList[i].NotesUser + "</td>" + " < td class='Notesalign' > " + NotificationList[i].NotesText+ "</td > " +
                    $("#dropdownNotifiedText").append("<div class='Notification' onclick=notificationClick('" + NotificationList[i].NotesInvoice + "') > <div class='InnerNotification'> " + NotificationList[i].NotesUser + " </div> <div> " + NotificationList[i].NotesText + " </div> </div> <hr />"

                    );
                 
                }
               
                //$('#dropdownNotifiedText').append('</table>');
                document.getElementById("iNotification").style.color = "#FFB100";
                if (sessionStorage.getItem("notifiedColor") != undefined)
                    sessionStorage.removeItem("notifiedColor");


             

              
            },
            error: function OnError(xhr) {

            }
        });

    }); 

  
    

    $('.featureNotAvailableMsgBG').click(function () {

        $('.featureNotAvailableMsgBG').toggleClass('hidden');
    });

    $('.featureNotAvailableTrigger').click(function (e) {
      
        e.preventDefault;
        e.stopImmediatePropagation();
        $('.featureNotAvailableBG').toggleClass('hidden');
    }); 
  
    $('.featureNotAvailableBG').click(function () { 
        
        $('.featureNotAvailableBG').toggleClass('hidden');
    });
    
    
    
    // Table vertical Zebtra stripes
    $('.table td').hover(function () {
       
    var t = parseInt($(this).index()) + 1;
    $(this).parents('table').find('td:nth-child(' + t + ')').addClass('highlighted');
    },
    function() {
        var t = parseInt($(this).index()) + 1;
        $(this).parents('table').find('td:nth-child(' + t + ')').removeClass('highlighted');
    });
    


  


});


function StatisticsClick() {
    if (document.getElementById("lstStatistics").style.display == "none")
      document.getElementById("lstStatistics").style.display = "";
    else
      document.getElementById("lstStatistics").style.display = "none";
}



function save() {
    var dt = new Date();

    dt.toLocaleDateString('en-US', { weekday: 'long' });
    document.getElementById("spnCalendar").innerHTML = dt.toLocaleDateString('en-US', { weekday: 'long' });
    document.getElementById("spnDate").innerHTML = dt.toString().split(' ')[1] + " " + dt.toString().split(' ')[2];
}

function notificationClick(invoiceNum) {

    $.ajax({
        type: "POST",
        url: "frmDashboard.aspx/NotificationInvoice",
        data: '{InvoiceNum: "' + invoiceNum + '"}',

        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
           
            document.getElementById("btnInvoiceSearch").click();

        },
        error: function OnError(xhr) {

        }
    });
}


function logout() {
    document.getElementById("btnlogout").click();
}

$(document).ready(function () {
    $(document).on("click", "#grdClientName tbody tr", function (e) {
       
        if (e.currentTarget.cells[0].textContent != "0") {
            document.getElementById("hdnClientName").value = e.currentTarget.cells[1].textContent;
            document.getElementById("hdnClientID").value = e.currentTarget.cells[0].textContent;

            document.getElementById("btnClientName").click();
        }

    });


    $('#divDropdown').click(function (e) {
        e.preventDefault();
        if (document.getElementById("dropdownClientName").className == "dropdownMenu") {
            $('#grdClientName tbody tr').each(function (e, element) {
                if (document.getElementById("txtClientName").getAttribute("clientid") == element.cells[0].textContent) {
                    element.style.color = "#FFB100";
                    element.style.background = "rgba(255, 255, 255, 0.15)";
                }


            });
        }
    });
});


function AdvanceSearch() {
    if (document.getElementById("dropdownAdvanceSearch").className.indexOf("hidden") == -1) {
        if (document.getElementById("txtCustomerName").value == "" && document.getElementById("txtEmail").value == "" && document.getElementById("txtPersonalNumber").value == "")
            return false;
        else if (document.getElementById("txtCustomerName").value != "" && document.getElementById("txtCustomerName").value.length < 3) {
            return false;
        }
        else
            document.getElementById("hdnAdvanceSearch").value = "1";

    }
    else {
        document.getElementById("hdnAdvanceSearch").value = "0";
        return true;
    }
        
}




$(function () {
    if (document.getElementById("iNotification") != null) {
        var chat = $.connection.notificationHub;

        chat.client.broadcastMessage = function (name, message) {
            document.getElementById("iNotification").style.color = "red";
            sessionStorage.setItem("notifiedColor", "red");
        };

        $.connection.hub.start(function () {
            chat.server.join($("#hdnClientID").val());
            if (sessionStorage.getItem("notifiedColor") != undefined)
                document.getElementById("iNotification").style.color = sessionStorage.getItem("notifiedColor");
            else
                document.getElementById("iNotification").style.color = "#FFB100";
            document.getElementById("iNotification").style.display = "";
        });

        $.connection.hub.start().done(function () {
            $('#btnNotification').click(function () {

                chat.server.send($("#hdnClientID").val());
            });
        });
    }
});


function PanelClick() {
    window.location.href = "frmDashboard.aspx";
}


function funDoWork() {
    alert("BackGround");

}


function EmailAlert() {
    alert("EmailAlert");
}