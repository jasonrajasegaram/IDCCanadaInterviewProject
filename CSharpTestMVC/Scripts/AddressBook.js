function InitializeAddressBook() {
    $(".navigationBar").show();
    FillInAddressBook();
    InitializeAddressBookSettings();
}
function FillInAddressBook() {

    var successFunction = function (userData) {
        $("#AddressBookPage").find("#userIntro").html("<h1>Welcome " + userData.firstName + " " + userData.lastName + " to your Address Book!</h1><span><b>Click a contact to view their details</b></span>");
        if (userData.isAdmin) {
            $(".adminBookLink").show();
        }
        else {
            $(".adminBookLink").hide();
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
    var HTMLStringTwo = ""; 
    for (var i = 0; i < userContacts.length; i++) {
        var oneContact = userContacts[i];
        HTMLStringTwo += "<div class='addressBookItem' id='" + oneContact.ID + "'>";
        HTMLStringTwo += "<div class='addressBookBody'><span class='addressBookItemName'>" + oneContact.firstName + " " + oneContact.lastName + "</span>";
        HTMLStringTwo += "<div class='addressBookBodyInfo'hidden><br/><span><b>Phone Number:</b> " + oneContact.phoneNumber + "</span><br/>";
        HTMLStringTwo += "<span><b>StreetName:</b> " + oneContact.streetName + "</span><br/>";
        HTMLStringTwo += "<span><b>City:</b> " + oneContact.city + "</span><br/>";
        HTMLStringTwo += "<span><b>Province:</b> " + oneContact.province + "</span><br/>";
        HTMLStringTwo += "<span><b>Postal Code:</b> " + oneContact.postalCode + "</span><br/>";
        HTMLStringTwo += "<span><b>Country:</b> " + oneContact.country + "</span><br/>";
        var notes = oneContact.notes;
        if (notes == null) {
            notes = "";
        }
        HTMLStringTwo += "<span><b>Notes:</b> " + notes + "</span><br/><br/>";
        HTMLStringTwo += "<button id='EditButton_" + oneContact.ID + "_" + i + "' class='editContactButton'>Edit</button><button id='DeleteButton_" + oneContact.ID + "_" + i + "' class='deleteContactButton'>Delete</button><br/>";
        HTMLStringTwo += "</div>";
        HTMLStringTwo += "</div>";
        HTMLStringTwo += "</div>";
    }
    $("#AddressBookPage").find("#AddressDropDown").html(HTMLStringTwo);
    $("#AddressDropDown").find(".addressBookBody").each(function (index, ele) {
        $(ele).find(".addressBookItemName").click(function () {
            if ($(ele).find(".addressBookBodyInfo").is(':visible')){
                $(ele).find(".addressBookBodyInfo").hide();
            }
            else {
                $(ele).find(".addressBookBodyInfo").show();
            }
            
        });
        $(ele).find(".addressBookItemName").hover(function () {
            $(ele).find(".addressBookItemName").addClass('addressBookHover');
        }, function () {
            $(ele).find(".addressBookItemName").removeClass('addressBookHover');
        });
    });
    $("#AddressDropDown").find(".deleteContactButton").each(function (index, ele) {
        $(ele).click(function(){
            var contactNumber = $(ele).attr('id').split("_")[1];
            var deleteContact = confirm("Are you sure you want to delete this contact?")
            if (deleteContact) {
                RemoveContact(contactNumber);
            }
        })
    });
    $("#AddressDropDown").find(".editContactButton").each(function (index, ele) {
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
                height: 600,
                width: 800,
                modal: true,
                open: function () {
                    CKEDITOR.replace('editNotes', {
                        height: '200px',
                        width: '400px'
                    });
                    var editNotes = userContacts[contactIndex].notes;
                    if (editNotes == null) {
                        editNotes = "";
                    }
                    CKEDITOR.instances.editNotes.setData(editNotes);
                },
                close: function () {
                    $("#editContactform").find("#editNotes").val(CKEDITOR.instances.editNotes.getData());
                    CKEDITOR.instances.editNotes.destroy(false);
                }
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
    var country = $("#createnewcontactform").find("#country").val();
    var notes = $("#createnewcontactform").find("#notes").val();


    var sendData = {
        firstName: firstName,
        lastName: lastName,
        phoneNumber: phoneNumber,
        streetName: streetName,
        city: city,
        province: province,
        postalCode: postalCode,
        country: country,
        notes: notes
    };
    var successFunction = function () {
        FillInAddressBook();
        alert("Successfully added new contact!");
        $("#createnewcontactform").find("#firstName").val("");
        $("#createnewcontactform").find("#lastName").val("");
        $("#createnewcontactform").find("#phoneNumber").val("");
        $("#createnewcontactform").find("#streetName").val("");
        $("#createnewcontactform").find("#city").val("");
        $("#createnewcontactform").find("#province").val("");
        $("#createnewcontactform").find("#postalCode").val("");
        $("#createnewcontactform").find("#country").val("");
        $("#createnewcontactform").find("#notes").val("");

    }
    ajaxCall("/Contact/AddNewContact", sendData, successFunction, null);

}
function OpenCreateNewContactForm() {
    $("#createnewcontactform").show();
    $("#contactFormDialog").dialog({
        autoOpen: false,
        height: 700,
        width:700,
        modal: true,
        dialogClass: "dialogFormatClass",
        open: function () {
            CKEDITOR.replace('notes', {
                height: '200px',
                width: '400px'
            });
        },
        close: function () {
            $("#createnewcontactform").find("#notes").val(CKEDITOR.instances.notes.getData());
            CKEDITOR.instances.notes.destroy(false);
        }
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
    var notes = $("#editContactform").find("#editNotes").val();
    var sendData = {
        contactID: contactID,
        firstName: firstName,
        lastName: lastName,
        phoneNumber: phoneNumber,
        streetName: streetName,
        city: city,
        province: province,
        postalCode: postalCode,
        country: country,
        notes: notes
    }
    var successFunction = function () {
        $("#editContactform").find("#editNotes").val("");
        FillInAddressBook();
    }
    ajaxCall("/Contact/EditContact", sendData, successFunction, null);
}