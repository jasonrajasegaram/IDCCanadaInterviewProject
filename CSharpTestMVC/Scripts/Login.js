function InitializeLogin() {
    $("#loginForm").validate({
        rules: {
            loginUsername: "required",
            loginPassword: "required"
        },
        messages: {
            loginUsername: "Please enter your username",
            loginPassword:"Please enter your password"
        }
    });
}