function InitializePasswordRecovery() {
    $("#passwordRecoveryForm").validate({
        rules: {
            userName: "required",
            temporaryPassword: "required"
        },
        messages: {
            userName: "Please enter a valid email",
            temporaryPassword: "Please enter the temporary password"
        },
        submitHandler: function (form) {
            SubmitTemporary();
        }
    });
    $("#passwordRecoveryFormparttwo").validate({
        rules: {
            password: "required",
            passwordConfirm: {
                equalTo: "#password"
            }
        },
        messages: {
            password:"required",
            passwordConfirm: {
                equalTo: "This password doesn't match the above"
            }
        },
        submitHandler: function (form) {
            SubmitNewPassword();
        }
    });
}
function SubmitTemporary() {
    var sendData = {
        userName: $("#passwordRecoveryForm").find("#userName").val(),
        temporaryPassword: $("#passwordRecoveryForm").find("#temporaryPassword").val()
    };
    var successFunction = function (result) {
        if (!result) {
            alert("The username and password combination are not correct. Please try again");
        }
        else {
            $("#passwordRecoveryForm").hide();
            $("#passwordRecoveryFormparttwo").show();
        }
    };
    ajaxCall("/ForgotPassword/TemporaryPasswordCheck", sendData, successFunction, null);
}
function SubmitNewPassword() {
    var sendData = {
        userName:$("#passwordRecoveryForm").find("#userName").val(),
        newPassword: $("#passwordRecoveryFormparttwo").find("#password").val()
    };
    var successFunction = function () {
        $("#passwordRecoveryFormparttwo").hide();
        $("#PasswordChanged").show();
    };
    ajaxCall("/ForgotPassword/NewPassword", sendData, successFunction, null);
}
//ForgotPasswordLink
function InitializeTemporaryPassword() {
    $("#temporaryPasswordForm").validate({
        rules: {
            userName:"required"
        },
        messages: {
            userName: "Please enter a vaild username/email"
        },
        submitHandler: function (form) {
            SendToUserName();
        }
    });
}
function SendToUserName() {
    var sendData = {
        userName: $("#temporaryPasswordForm").find("#userName").val()
    };
    var successFunction = function (result) {
        if (!result) {
            alert("The username you entered does not exist. Please try again")
        }
        else {
            alert("A temporary password has been sent to you.");
        }
    };
    ajaxCall("/ForgotPassword/SendTemporaryPassword", sendData, successFunction, null);
}