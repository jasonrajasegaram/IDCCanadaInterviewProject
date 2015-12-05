function InitializeAdminBook() {
    FillInAdminBook();
    InitializeAdminBookSettings();
}
function FillInAdminBook() {

    var successFunction = function (adminData) {
        $("#AdminBookPage").find("#adminIntro").html("Welcome " + adminData.userData.firstName + " " + adminData.userData.lastName + " to your Admin Book!");
        var adminList = adminData.userList;
        DrawAdminTable(adminList)
    }
    var data = {
    };
    ajaxCall("/Admin/GetCurrentAdmin", data, successFunction, null);

}
function InitializeAdminBookSettings() {
    $("#createnewuserform").validate({
        rules: {
            firstName: "required",
            lastName: "required",
            userName: "required"
        },
        messages: {
            firstName: "Please enter the first name",
            lastName: "Please enter the last name",
            userName: "Please enter a valid username/email"
        },
        submitHandler: function (form) {
            $("#userFormDialog").dialog("close");
            SubmitNewUser();
        }
    });
    $("#editUserform").validate({
        rules: {
            firstName: "required",
            lastName: "required",
            userName: "required"
        },
        messages: {
            firstName: "Please enter the first name",
            lastName: "Please enter the last name",
            userName: "Please enter a valid username/email"
        },
        submitHandler: function (form) {
            $("#editUserFormDialog").dialog("close");
            EditUser();
        }
    });
}
function DrawAdminTable(adminList) {
    var HTMLString = "<table border='1'>";
    for (var i = 0; i < adminList.length; i++) {
        var oneUser = adminList[i];
        HTMLString += "<tr class='adminBookItem' id='" + oneUser.ID + "'>";
        HTMLString += "<td>" + oneUser.firstName + " </td>";
        HTMLString += "<td>" + oneUser.lastName + " </td>";
        HTMLString += "<td>" + oneUser.userName+ " </td>";
        HTMLString += "<td><button id='EditButton_" + oneUser.ID + "_" + i + "' class='editUserButton'>Edit</button><button id='DeleteButton_" + oneUser.ID + "_" + i + "' class='deleteUserButton'>Delete</button></td>"
        HTMLString += "</tr>"
    }
    HTMLString += "</table>";
    $("#AdminBookPage").find("#AdminTable").html(HTMLString);
    $("#AdminTable").find(".deleteUserButton").each(function (index, ele) {
        $(ele).click(function () {
            var userNumber = $(ele).attr('id').split("_")[1];
            var deleteUser = confirm("Are you sure you want to delete this user?")
            if (deleteUser) {
                RemoveUser(userNumber);
            }
        })
    });
    $("#AdminTable").find(".editUserButton").each(function (index, ele) {
        $(ele).click(function () {
            var userID = $(ele).attr('id').split("_")[1];
            var userIndex = $(ele).attr('id').split("_")[2];
            $("#editUserform").find(".userID").attr('id', userID);
            $("#editUserform").find("#firstName").val(adminList[userIndex].firstName);
            $("#editUserform").find("#lastName").val(adminList[userIndex].lastName);
            $("#editUserform").find("#userName").val(adminList[userIndex].userName);
            if (adminList[userIndex].isAdmin) {
                $("#editUserform").find("#isAdmin").prop("checked",true);
            }
            else {
                $("#editUserform").find("#isAdmin").prop("checked", false);
            }
            $("#editUserform").show();
            $("#editUserFormDialog").dialog({
                autoOpen: false,
                height: 400,
                width: 800,
                modal: true,
            });
            $("#editUserFormDialog").dialog("open");
        })
    });
}
function SubmitNewUser() {
    var firstName = $("#createnewuserform").find("#firstName").val();
    var lastName = $("#createnewuserform").find("#lastName").val();
    var userName = $("#createnewuserform").find("#userName").val();
    var isAdmin = $("#createnewuserform").find("#isAdmin").is(":checked");

    var sendData = {
        firstName: firstName,
        lastName: lastName,
        userName: userName,
        isAdmin: isAdmin
    };
    var successFunction = function () {
        FillInAdminBook();
        alert("Successfully added new user!")
    }
    ajaxCall("/User/AddNewUser", sendData, successFunction, null);

}
function OpenCreateNewUserForm() {
    $("#createnewuserform").show();
    $("#userFormDialog").dialog({
        autoOpen: false,
        height: 400,
        width: 800,
        modal: true
    });
    $("#userFormDialog").dialog("open");
}
function RemoveUser(userNumber) {
    var sendData = {
        userNumber: userNumber
    };
    var successFunction = function () {
        FillInAdminBook();
    }
    ajaxCall("/User/DeleteUser", sendData, successFunction, null);
}
function EditUser() {
    var userID = $("#editUserform").find(".userID").attr('id');
    var firstName = $("#editUserform").find("#firstName").val();
    var lastName = $("#editUserform").find("#lastName").val();
    var userName = $("#editUserform").find("#userName").val();
    var isAdmin = $("#editUserform").find("#isAdmin").is(":checked");
    var sendData = {
        userID: userID,
        firstName: firstName,
        lastName: lastName,
        userName: userName,
        isAdmin: isAdmin
    }
    var successFunction = function () {
        FillInAdminBook();
    }
    ajaxCall("/User/EditUser", sendData, successFunction, null);
}