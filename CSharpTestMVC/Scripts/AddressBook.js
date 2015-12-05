function InitializeAddressBook() {
    FillInAddressBook();
    InitializeAddressBookSettings();
}
function FillInAddressBook() {

    var successFunction = function (userData) {
        $("#AddressBookPage").find("#userIntro").html("Welcome " + userData.firstName + " " + userData.lastName + " to your Address Book!");
        if (userData.isAdmin) {
            $("#adminLink").show();
        }
        var userContacts = userData.userContacts;
        DrawAddressTable(userContacts)
    }
    var data = {
    };
    ajaxCall("/User/GetCurrentUser", data, successFunction, null);
    
}
function InitializeAddressBookSettings() {
    $("#createnewcontactform").validate({
        rules: {
            firstName: "required",
            lastName: "required",
            phoneNumber: "required",
            streetName: "required",
            city: "required",
            province: "required",
            postalCode: "required",
            country: "required"
        },
        messages: {
            firstName: "Please enter the first name",
            lastName: "Please enter the last name",
            phoneNumber: "Please enter the phone number",
            streetName: "Please enter the street name",
            city: "Please enter the city",
            province: "Please enter the province",
            postalCode: "Please enter the postal code",
            country: "Please enter the country"
        },
        submitHandler: function (form) {
            $("#contactFormDialog").dialog("close");
            SubmitNewContact();
        }
    });
    $("#editContactform").validate({
        rules: {
            firstName: "required",
            lastName: "required",
            phoneNumber: "required",
            streetName: "required",
            city: "required",
            province: "required",
            postalCode: "required",
            country: "required"
        },
        messages: {
            firstName: "Please enter the first name",
            lastName: "Please enter the last name",
            phoneNumber: "Please enter the phone number",
            streetName: "Please enter the street name",
            city: "Please enter the city",
            province: "Please enter the province",
            postalCode: "Please enter the postal code",
            country: "Please enter the country"
        },
        submitHandler: function (form) {
            $("#editContactFormDialog").dialog("close");
            EditContact();
        }
    });
}
function DrawAddressTable(userContacts){
    var HTMLString = "<table border='1'>";
    for (var i = 0; i < userContacts.length; i++) {
        var oneContact =userContacts[i];
        HTMLString +="<tr class='addressBookItem' id='" + oneContact.ID +"'>";
        HTMLString += "<td>" + oneContact.firstName + " </td>";
        HTMLString += "<td>" + oneContact.lastName + " </td>";
        HTMLString += "<td>" + oneContact.phoneNumber + " </td>";
        HTMLString += "<td>" + oneContact.streetName + " </td>";
        HTMLString += "<td>" + oneContact.city + " </td>";
        HTMLString += "<td>" + oneContact.province + " </td>";
        HTMLString += "<td>" + oneContact.postalCode + " </td>";
        HTMLString += "<td>" + oneContact.country + " </td>";
        HTMLString += "<td><button id='EditButton_" + oneContact.ID + "_" + i + "' class='editContactButton'>Edit</button><button id='DeleteButton_" + oneContact.ID + "_" + i + "' class='deleteContactButton'>Delete</button></td>"
        HTMLString += "</tr>"
    }
    HTMLString += "</table>";
    $("#AddressBookPage").find("#AddressTable").html(HTMLString);
    $("#AddressTable").find(".deleteContactButton").each(function (index, ele) {
        $(ele).click(function(){
            var contactNumber = $(ele).attr('id').split("_")[1];
            var deleteContact = confirm("Are you sure you want to delete this contact?")
            if (deleteContact) {
                RemoveContact(contactNumber);
            }
        })
    });
    $("#AddressTable").find(".editContactButton").each(function (index, ele) {
        $(ele).click(function () {
            var contactID = $(ele).attr('id').split("_")[1];
            var contactIndex = $(ele).attr('id').split("_")[2];
            $("#editContactform").find(".contactID").attr('id', contactID);
            $("#editContactform").find("#firstName").val(userContacts[contactIndex].firstName);
            $("#editContactform").find("#lastName").val(userContacts[contactIndex].lastName);
            $("#editContactform").find("#phoneNumber").val(userContacts[contactIndex].phoneNumber);
            $("#editContactform").find("#streetName").val(userContacts[contactIndex].streetName);
            $("#editContactform").find("#city").val(userContacts[contactIndex].city);
            $("#editContactform").find("#province").val(userContacts[contactIndex].province);
            $("#editContactform").find("#postalCode").val(userContacts[contactIndex].postalCode);
            $("#editContactform").find("#country").val(userContacts[contactIndex].country);
            $("#editContactform").show();
            $("#editContactFormDialog").dialog({
                autoOpen: false,
                height: 400,
                width: 800,
                modal: true
            });
            $("#editContactFormDialog").dialog("open");
        })
    });
}
function SubmitNewContact() {
    var firstName= $("#createnewcontactform").find("#firstName").val();
    var lastName= $("#createnewcontactform").find("#lastName").val();
    var phoneNumber = $("#createnewcontactform").find("#phoneNumber").val();
    var streetName= $("#createnewcontactform").find("#streetName").val();
    var city= $("#createnewcontactform").find("#city").val();
    var province= $("#createnewcontactform").find("#province").val();
    var postalCode= $("#createnewcontactform").find("#postalCode").val();
    var country= $("#createnewcontactform").find("#country").val();


    var sendData = {
        firstName: firstName,
        lastName: lastName,
        phoneNumber: phoneNumber,
        streetName: streetName,
        city: city,
        province: province,
        postalCode: postalCode,
        country: country
    };
    var successFunction = function () {
        FillInAddressBook();
        alert("Successfully added new contact!")
    }
    ajaxCall("/Contact/AddNewContact", sendData, successFunction, null);

}
function OpenCreateNewContactForm() {
    $("#createnewcontactform").show();
    $("#contactFormDialog").dialog({
        autoOpen: false,
        height: 400,
        width:800,
        modal: true
    });
    $("#contactFormDialog").dialog("open");
}
function RemoveContact(contactNumber) {
    var sendData = {
        contactNumber: contactNumber
    };
    var successFunction = function(){
        FillInAddressBook();
    }
    ajaxCall("/Contact/DeleteContact", sendData, successFunction, null);
}
function EditContact() {
    var contactID=$("#editContactform").find(".contactID").attr('id');
    var firstName=$("#editContactform").find("#firstName").val();
    var lastName=$("#editContactform").find("#lastName").val();
    var phoneNumber=$("#editContactform").find("#phoneNumber").val();
    var streetName = $("#editContactform").find("#streetName").val();
    var city = $("#editContactform").find("#city").val();
    var province = $("#editContactform").find("#province").val();
    var postalCode = $("#editContactform").find("#postalCode").val();
    var country = $("#editContactform").find("#country").val();
    var sendData = {
        contactID: contactID,
        firstName: firstName,
        lastName: lastName,
        phoneNumber: phoneNumber,
        streetName: streetName,
        city: city,
        province: province,
        postalCode: postalCode,
        country: country
    }
    var successFunction = function(){
        FillInAddressBook();
    }
    ajaxCall("/Contact/EditContact", sendData, successFunction, null);
}