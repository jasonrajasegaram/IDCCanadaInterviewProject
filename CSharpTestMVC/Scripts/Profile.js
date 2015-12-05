function InitializeProfile() {
    FillInProfile();
    InitializeProfileSettings();
}
function FillInProfile() {

    var successFunction = function (userData) {
        $("#Profile").find(".userID").attr('id', userData.ID);
        $("#Profile").find("#userName").html("Your username is: " + userData.userName);
        $("#Profile").find("#firstName").html("Your first name is: " + userData.firstName);
        $("#Profile").find("#lastName").html("Your last name is: " + userData.lastName);
        $("#editProfileform").find("#firstName").val(userData.firstName);
        $("#editProfileform").find("#lastName").val(userData.lastName);
        $("#editProfileform").find("#userName").val(userData.userName);
    }
    var data = {
    };
    ajaxCall("/User/GetCurrentUser", data, successFunction, null);

}
function InitializeProfileSettings() {
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