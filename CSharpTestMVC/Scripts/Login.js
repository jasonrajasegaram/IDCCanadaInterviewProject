function InitializeLogin() {
    $("#loginForm").validate({
        rules: {
            loginUsername: "required",
            loginPassword: "required"
        },
        messages: {
            loginUsername: "Please enter your username/email",
            loginPassword:"Please enter your password"
        },
        submitHandler: function(form) {
            SubmitLoginInfo();
        }
    });

}
function SubmitLoginInfo() {
    var username = $("#loginForm").find("#loginUsername").val();
    var password = $("#loginForm").find("#loginPassword").val();
    var sendData = {
        username: username,
        password: password
    };
    var successFunction = function (data) {
        if (data.userFound) {
            if (data.correctPassword) {
                window.location = "AddressBook/Index";
            }
            else {
                $("#spinner").hide();
                alert("Invalid Credentials");
            }
        }
        else {
            $("#spinner").hide();
            alert("Invalid Credentials");
        }

    }
    $("#spinner").show();
    ajaxCall("/Home/Login", sendData, successFunction, null);
}