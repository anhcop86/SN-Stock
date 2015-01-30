function ShowStatus(status, msg) {
    $("#AdminStatus").removeClass("warning");
    $("#AdminStatus").removeClass("success");

    $("#AdminStatus").addClass(status);
    $("#AdminStatus").html(msg + '<a href="javascript:HideStatus()" style="width:20px;float:right">X</a>');
    $("#AdminStatus").fadeIn(1000, function () { });
}

function HideStatus() {
    $("#AdminStatus").fadeOut('slow', function () { });
}

function Hide(element) {
    $("'" + element + "").fadeOut('slow', function () { });
    return false;
}

function ValidatePasswordRetrieval() {
    if ($("'txtEmail").val().length == 0) {
        ShowStatus('warning', accountResources.emailIsRequired);
        return false;
    }
    if (ValidateEmail($("'txtEmail").val()) == false) {
        ShowStatus('warning', accountResources.emailIsInvalid);
        return false;
    }
    return true;
}

function ValidateLogin() {
    if ($("'UserName").val().length == 0) {
        ShowStatus('warning', accountResources.userNameIsRequired);
        return false;
    }
    if ($("'Password").val().length == 0) {
        ShowStatus('warning', accountResources.passwordIsRequried);
        return false;
    }
    return true;
}

function ValidateChangePassword() {    
    if ($("'CurrentPassword").val().length == 0) {
        ShowStatus('warning', accountResources.oldPasswordIsRequired);
        return false;
    }
    if ($("'NewPassword").val().length == 0) {
        ShowStatus('warning', accountResources.newPasswordIsRequired);
        return false;
    }
    var minReq = $("#hdnPassLength").val();
    var minPass = $("'NewPassword").val().length;

    if (minPass < minReq) {
        ShowStatus('warning', 'Minimum password length is ' + minReq + ' characters');
        return false;
    }
    if ($("'ConfirmNewPassword").val().length == 0) {
        ShowStatus('warning', accountResources.confirmPasswordIsRequired);
        return false;
    }
    if ($("'NewPassword").val() != $("'ConfirmNewPassword").val()) {
        ShowStatus('warning', accountResources.newAndConfirmPasswordMismatch);
        return false;
    }   
    return true;
}

function ValidateNewUser() {
    if ($("'UserName").val().length == 0) {
        ShowStatus('warning', accountResources.userNameIsRequired);
        return false;
    }
    if ($("'Email").val().length == 0) {
        ShowStatus('warning', accountResources.emailIsRequired);
        return false;
    }
    if (ValidateEmail($("'Email").val()) == false) {
        ShowStatus('warning', accountResources.emailIsInvalid);
        return false;
    }
    if ($("'Password").val().length == 0) {
        ShowStatus('warning', accountResources.passwordIsRequried);
        return false;
    }
    var minReq = $("#hdnPassLength").val();
    var minPass = $("'Password").val().length;

    if (minPass < minReq) {
        ShowStatus('warning', accountResources.minPassLengthInChars.replace('{0}', minReq));
        return false;
    }
    if ($("'ConfirmPassword").val().length == 0) {
        ShowStatus('warning', accountResources.confirmPasswordIsRequired);
        return false;
    }
    if ($("'Password").val() != $("'ConfirmPassword").val()) {
        ShowStatus('warning', accountResources.passwordAndConfirmPasswordIsMatch);
        return false;
    }
    return true;
}

function ValidateEmail(email) {
    var reg = /^([A-Za-z0-9_\-\.])+\@([A-Za-z0-9_\-\.])+\.([A-Za-z]{2,4})$/;
    return reg.test(email);
}