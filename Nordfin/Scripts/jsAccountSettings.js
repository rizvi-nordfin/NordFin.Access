function changePassword() {

    clearControls();
    $('#mdlReport').modal({ backdrop: 'static', keyboard: false }, 'show');
}
function showUpdaeInfo(heading,value) {
    $("#updateInfoModalLabel").text(heading);
    document.getElementById("NordfinContentHolder_txtNewNumber").value = "";
    document.getElementById("NordfinContentHolder_txtNewEmail").value = "";
    if (value == 0) {
        $("#pnlEmail").show();
        $("#pnlPhoneNumber").hide();
    }

    if (value == 1) {
        $("#pnlEmail").hide();
        $("#pnlPhoneNumber").show();
    }
    $('#mdlUpdateInfo').modal({ backdrop: 'static', keyboard: false }, 'show');

}
function clearControls() {
    $('#NordfinContentHolder_lblstrPassword').addClass('hide');
    $('#NordfinContentHolder_lblWeak').addClass('hide');
    $('#NordfinContentHolder_lblStrong').addClass('hide');
    $('#NordfinContentHolder_lblGood').addClass('hide');
    $('#NordfinContentHolder_lblPasswordNotMatch').addClass('hide');
    $('#NordfinContentHolder_lblPasswordMatch').addClass('hide');
    $('#spnPasswordnotmatch').addClass('hide');
    document.getElementById("NordfinContentHolder_txtOldPassword").value = "";
    document.getElementById("NordfinContentHolder_txtPassword").value = "";
    document.getElementById("NordfinContentHolder_txtConfirmPassword").value = "";
    

}
function checkPassStr($this) {

   
    $('#NordfinContentHolder_lblstrPassword').addClass('hide');
    $('#NordfinContentHolder_lblWeak').addClass('hide');
    $('#NordfinContentHolder_lblStrong').addClass('hide');
    $('#NordfinContentHolder_lblGood').addClass('hide');
   
    checkStrength($this.value);
}


function checkStrength(password) {
   
    var strength = 0
    if (password.length < 3) {
      
        $('#NordfinContentHolder_lblstrPassword').removeClass();

        document.getElementById("NordfinContentHolder_lblstrPassword").style.display = "";

        return;
    } 
    if (password.length > 7) strength += 1
    if (password.match(/([a-z].*[A-Z])|([A-Z].*[a-z])/)) strength += 1
    if (password.match(/([a-zA-Z])/) && password.match(/([0-9])/)) strength += 1
    if (password.match(/([!,%,&,@,#,$,^,*,?,_,~])/)) strength += 1
    if (password.match(/(.*[!,%,&,@,#,$,^,*,?,_,~].*[!,%,&,@,#,$,^,*,?,_,~])/)) strength += 1
    if (strength < 2) {
        $('#NordfinContentHolder_lblWeak').removeClass();
        //$('#strength_message').addClass('weak')
        document.getElementById("NordfinContentHolder_lblWeak").style.display = "";
        return;

    } else if (strength == 2) {
        $('#NordfinContentHolder_lblGood').removeClass();
        document.getElementById("NordfinContentHolder_lblGood").style.display = "";
        return;

    } else {
        $('#NordfinContentHolder_lblStrong').removeClass();
        document.getElementById("NordfinContentHolder_lblStrong").style.display = "";
        return;

    }
}

function checkOldPassword($this) {
    $('#spnPasswordnotmatch').addClass('hide');

}
function checkPassMatch($this) {
    var password = document.getElementById("NordfinContentHolder_txtPassword").value;
   
    if ($this.value != password) {
       
        $('#NordfinContentHolder_lblPasswordNotMatch').removeClass();
        $('#NordfinContentHolder_lblPasswordMatch').addClass('hide');
        $('#passNotMatch').addClass('short');
        //document.getElementById("NordfinContentHolder_lblStrong").style.display = "";


        return false;
    } else {
     

        $('#passNotMatch').removeClass();
        $('#NordfinContentHolder_lblPasswordNotMatch').addClass('hide');
        $('#NordfinContentHolder_lblPasswordMatch').removeClass();
        return true;
    }
}

function checkEmail($this) {
    $('#spnEmail').addClass('hide');
}
function validateEmail(email) {
    var re = /\S+@\S+\.\S+/;
    return re.test(email);
}
function confirmUpdate() {
    if (document.getElementById("NordfinContentHolder_txtNewEmail").value != "" && !validateEmail(document.getElementById("NordfinContentHolder_txtNewEmail").value)) {
        $('#EmailValid').removeClass();
        $('#spnEmail').removeClass();
        return false;
    }

    if (document.getElementById("NordfinContentHolder_txtNewEmail").value != "") {
        $('#mdlUpdateInfo').modal('hide');
        $('#mdlUpdateConfirm').modal({ backdrop: 'static', keyboard: false }, 'show');
        return false;
    }
    else {
        document.getElementById("NordfinContentHolder_btnYes").click();
        return true;
    }
}
$(document).ready(function () {
    $('.featureNotAvailableBGpnl').click(function () {
        $('.featureNotAvailableBGpnl').toggleClass('hidden');
    });

    $('.featureNotAvailableBGpnlPass').click(function () {
        $('.featureNotAvailableBGpnlPass').toggleClass('hidden');
    });

  
});
var app = angular.module("myApp", []);

app.controller("myCtrl", function ($scope, $http) {

    $scope.submitClick = function ($event) {

        if (document.getElementById("NordfinContentHolder_txtOldPassword").value == "" || document.getElementById("NordfinContentHolder_txtConfirmPassword").value == "") {
         
        }
      
        else if (document.getElementById("strength_message").innerText == "Weak" || document.getElementById("strength_message").innerText == "Minimum 8 chracter") {
          
        }
        else if (document.getElementById("NordfinContentHolder_txtPassword").value != document.getElementById("NordfinContentHolder_txtConfirmPassword").value) {
           
        }
     
        else {
            $http({
                url: "frmAccountSettings.aspx/OldPasswordCheck",
                dataType: 'json',
                method: 'POST',
                data: '{sPassword: "' + document.getElementById("NordfinContentHolder_txtOldPassword").value + '" }',

                headers: {
                    "Content-Type": "application/json; charset=utf-8",
                    "X-Requested-With": "XMLHttpRequest"
                }
            }).then(function (response) {

                const passwordCount = JSON.parse(response.data.d);
                if (passwordCount.PasswordExsists > 0) {
                    document.getElementById("NordfinContentHolder_submit").click();

                }
                else {
                    $('#spnPasswordnotmatch').removeClass();

                }





            }, function (error) {
            });
        }
        $event.preventDefault();
    }
});
