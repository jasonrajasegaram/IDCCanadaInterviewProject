function InitializeProfile() {
    $(".navigationBar").show();
    FillInProfile();
    $("#editProfileform").validate({
        rules: {
            firstName: "required",
            lastName: "required",
            userName: "required"
        },
        messages: {
            firstName: "Please enter the first name",
            lastName: "Please enter the last name",
            userName: "Please enter the username"
        },
        submitHandler: function (form) {
            EditProfile();
            $("#editProfileFormDialog").dialog("close");

        }
    });
    $("#changePasswordform").validate({
        rules: {
            oldPassword: "required",
            newPassword: "required",
            newPasswordConfirm: {
                equalTo: "#newPassword"
            }
        },
        messages: {
            oldPassword: "Please enter your password",
            newPassword: "Please enter a new password",
            newPasswordConfirm: {
                equalTo: "This password does not match the above"
            }
        },
        submitHandler: function (form) {
            ChangePassword();
            $("#changePasswordFormDialog").dialog("close");

        }
    });
}
function FillInProfile() {

    var successFunction = function (userData) {
        if (userData.isAdmin) {
            $(".adminBookLink").show();
        }
        else {
            $(".adminBookLink").hide();
        }
        $("#Profile").find(".userID").attr('id', userData.ID);
        $("#Profile").find("#userName").html("<b>Username:</b> " + userData.userName);
        $("#Profile").find("#firstName").html("<b>First name:</b> " + userData.firstName);
        $("#Profile").find("#lastName").html("<b>Last name:</b> " + userData.lastName);
        $("#editProfileform").find("#firstName").val(userData.firstName);
        $("#editProfileform").find("#lastName").val(userData.lastName);
        $("#editProfileform").find("#userName").val(userData.userName);
    }
    var data = {
    };
    ajaxCall("/User/GetCurrentUser", data, successFunction, null);

}
function OpenEditProfileForm() {
    $("#editProfileform").show();
    $("#editProfileFormDialog").dialog({
        autoOpen: false,
        height: 400,
        width: 800,
        modal: true
    });
    $("#editProfileFormDialog").dialog("open");
}
function EditProfile() {
    var sendData = {
        userID: $("#Profile").find(".userID").attr('id'),
        firstName: $("#editProfileform").find("#firstName").val(),
        lastName: $("#editProfileform").find("#lastName").val(),
        userName: $("#editProfileform").find("#userName").val()
    };
    var successfunction = function () {
        FillInProfile();
    }
    ajaxCall("/User/EditUser", sendData, successfunction, null);
}
function OpenChangePasswordForm() {
    $("#changePasswordform").show();
    $("#changePasswordFormDialog").dialog({
        autoOpen: false,
        height: 400,
        width: 800,
        modal: true
    });
    $("#changePasswordFormDialog").dialog("open");
}
function ChangePassword() {
    var sendData = {
        oldPassword: $("#changePasswordform").find("#oldPassword").val(),
        newPassword: $("#changePasswordform").find("#newPassword").val(),
        userID: $("#Profile").find(".userID").attr('id')
    };
    var successFunction = function (result) {
        if (result) {
            alert("Your password has been changed!");
            FillInProfile();
        }
        else {
            alert("The password you entered is incorrect. Please try again");
        }
    }
    ajaxCall("/User/ChangePassword", sendData, successFunction, null);
}