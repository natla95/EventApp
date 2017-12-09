﻿var EventNameReq = /^[A-Z-a-z0-9ąóęćłśńżźĄÓĘĆŁŚŃŻŹ\s\-\ \.\,_\!]{5,100}$/;
var OrganizatorNameReq = /^[A-Z-a-ząóęćłśńżźĄÓĘĆŁŚŃŻŹ\s\-]{2,100}$/;
var AddressEventReq = /^[A-Z-a-z0-9ąóęćłśńżźĄÓĘĆŁŚŃŻŹ\s\-\.\/\,]{5,150}$/;
var GuestName = /^[A-Z-a-ząóęćłśńżźĄÓĘĆŁŚŃŻŹ\s\-]{2,50}$/;

function ValidateEventForm() {
    var error = false;
    var eventName = $('#eventName').val();
    var organizer1Name = $('#organizer1Name').val();
    var organizer2Name = $('#organizer2Name').val();
    var churchAddress = $('#churchAddress').val();
    var weddingAddress = $('#weddingAddress').val();

    if (eventName != "") {
        if (eventName.match(EventNameReq) != null) {
            error = false;
            $('#eventName').removeClass("error");
            $('#eventName_e').html("");

        }
        else {
            error = true;
            $('#eventName').addClass("error");
            $('#eventName_e').html("Niepoprawny format");
        }
    }
    else {
        error = true;
        $('#eventName').addClass("error");
        $('#eventName_e').html("Pole wymagane");
    }
    
    if (organizer1Name != "") {
        if (organizer1Name.match(OrganizatorNameReq) != null) {
            error = false;
            $('#organizer1Name').removeClass("error");
            $('#organizer1Name_e').html("");

        }
        else {
            error = true;
            $('#organizer1Name').addClass("error");
            $('#organizer1Name_e').html("Niepoprawny format");
        }
    }
    else {
        error = true;
        $('#organizer1Name').addClass("error");
        $('#organizer1Name_e').html("Pole wymagane");
    }
    
    if (organizer2Name != "") {
        if (organizer2Name.match(OrganizatorNameReq) != null) {
            error = false;
            $('#organizer2Name').removeClass("error");
            $('#organizer2Name_e').html("");
        }
        else {
            error = true;
            $('#organizer2Name').addClass("error");
            $('#organizer2Name_e').html("Niepoprawny format");
        }
    }
    else {
        error = true;
        $('#organizer2Name').addClass("error");
        $('#organizer2Name_e').html("Pole wymagane");
    }
    

    if (churchAddress.match(AddressEventReq) != null) {
        error = false;
        $('#churchAddress').removeClass("error");
        $('#churchAddress_e').html("");   
    }
    else {
        error = true;
        $('#churchAddress').addClass("error");
        $('#churchAddress_e').html("Niepoprawny format adresu");
    }

    if (weddingAddress.match(AddressEventReq) != null) {
        error = false;
        $('#weddingAddress').removeClass("error");
        $('#weddingAddress_e').html("");
    }
    else {
        error = true;
        $('#weddingAddress').addClass("error");
        $('#weddingAddress_e').html("Niepoprawny format adresu");
    }

    return !error;
}

function ValidateGuestForm() {
    var error = false;
    var firstName = $('#guestFirstName').val();
    var lastName = $('#guestLastName').val();

    if (firstName != "") {
        if (firstName.match(GuestName) != null) {
            error = false;
            $('#guestFirstName').removeClass("error");
            $('#guestFirstName_e').html("");
        }
        else {
            error = true;
            $('#guestFirstName').addClass("error");
            $('#guestFirstName_e').html("Niepoprawny format");
        }
    }
    else {
        error = true;
        $('#guestFirstName').addClass("error");
        $('#guestFirstName_e').html("Pole wymagane");
    }

    if (lastName != "") {
        if (lastName.match(GuestName) != null) {
            error = false;
            $('#guestLastName').removeClass("error");
            $('#guestLastName_e').html("");

        }
        else {
            error = true;
            $('#guestLastName').addClass("error");
            $('#guestLastName_e').html("Niepoprawny format");
        }
    }
    else {
        error = true;
        $('#guestLastName').addClass("error");
        $('#guestLastName_e').html("Pole wymagane");
    }

    return !error;
}


(function () {

    // ---- EVENT form ----
    $('#eventName').on("change", function () {
        var obj = $(this);
        var val = obj.val();
        if (val != "") {
            if (val.match(EventNameReq) != null) {
                obj.removeClass("error");
                $('#eventName_e').html(" ");
            }
            else {
                obj.addClass("error");
                $('#eventName_e').html("Niepoprawny format");
            }
        }
        else {
            obj.addClass("error");
            $('#eventName_e').html("Pole wymagane");
        }   
    })
    $('#organizer1Name').on("change", function () {
        var obj = $(this);
        var val = obj.val();
        if (val != "") {
            if (val.match(OrganizatorNameReq) != null) {
                obj.removeClass("error");
                $('#organizer1Name_e').html(" ");
            }
            else {
                obj.addClass("error");
                $('#organizer1Name_e').html("Niepoprawny format");
            }
        }
        else {
            obj.addClass("error");
            $('#organizer1Name_e').html("Pole wymagane");
        }     
    })
    $('#organizer2Name').on("change", function () {
        var obj = $(this);
        var val = obj.val();
        if (val != "") {
            if (val.match(OrganizatorNameReq) != null) {
                obj.removeClass("error");
                $('#organizer2Name_e').html(" ");
            }
            else {
                obj.addClass("error");
                $('#organizer2Name_e').html("Niepoprawny format");
            }
        }
        else {
            obj.addClass("error");
            $('#organizer2Name_e').html("Pole wymagane");
        }
        
    })
    $('#churchAddress').on("change", function () {
        var obj = $(this);
        var val = obj.val();
        if (val != "") {
            if (val.match(AddressEventReq) != null) {
                obj.removeClass("error");
                $('#churchAddress_e').html(" ");
            }
            else {
                obj.addClass("error");
                $('#churchAddress_e').html("Niepoprawny format adresu");
            }
        }
        else {
            obj.removeClass("error");
            $('#churchAddress_e').html(" ");
        }
        
    })
    $('#weddingAddress').on("change", function () {
        var obj = $(this);
        var val = obj.val();
        if (val != "") {
            if (val.match(AddressEventReq) != null) {
                obj.removeClass("error");
                $('#weddingAddress_e').html(" ");
            }
            else {
                obj.addClass("error");
                $('#weddingAddress_e').html("Niepoprawny format adresu");
            }
        }
        else {
            obj.removeClass("error");
            $('#weddingAddress_e').html(" ");
        }         
    })

    $('#addEventButton').on("click", function () {
        if (ValidateEventForm()) {
            $('#addEventForm').submit();
        }
    })
    $('#saveEventEdition').on("click", function () {
        if (ValidateEventForm) {
            $('#editEventForm').submit();
        }
    })
        // ---- end EVENT form ------ 

    // -- GUEST form ---
    $('#guestFirstName').on("change", function () {
        var obj = $(this);
        var val = obj.val();
        if (val != "") {
            if (val.match(GuestName) != null) {
                obj.removeClass("error");
                $('#guestFirstName_e').html(" ");
            }
            else {
                obj.addClass("error");
                $('#guestFirstName_e').html("Niepoprawny format");
            }
        }
        else {
            obj.addClass("error");
            $('#guestFirstName_e').html("Pole wymagane");
        }
    })

    $('#guestLastName').on("change", function () {
        var obj = $(this);
        var val = obj.val();
        if (val != "") {
            if (val.match(GuestName) != null) {
                obj.removeClass("error");
                $('#guestLastName_e').html(" ");
            }
            else {
                obj.addClass("error");
                $('#guestLastName_e').html("Niepoprawny format");
            }
        }
        else {
            obj.addClass("error");
            $('#guestLastName_e').html("Pole wymagane");
        }
    })

    $('#addNewGuest').on("click", function () {
        if (ValidateGuestForm()) {
            $('#addGuestForm').submit();
        }
    })
})();