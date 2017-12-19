
if (iconNr == 1) {
    $('#menu1').addClass('active');
    console.log(haveEvent)
    if (haveEvent == "yes") {
        $('#addEvent').css("display", "none");
        $('#editEvent').css("display", "inline-block");
        $('#deleteEvent').css("display", "inline-block");
    }
    else if (haveEvent == "no") {
        $('#addEvent').css("display", "block");
        $('#editEvent').css("display", "none");
        $('#deleteEvent').css("display", "none");
    }
} 
if (iconNr == 2)
    $('#menu2').addClass('active');
if (iconNr == 3)
    $('#menu3').addClass('active');
if (iconNr == 4)
    $('#menu4').addClass('active');
if (iconNr == 5)
    $('#menu5').addClass('active');
if (iconNr == 6)
    $('#menu6').addClass('active');



(function () {

    $('#userIcon').on("click", function () {
        var objLog = $('#logOut');
        if (objLog.hasClass('not_active')) {
            objLog.removeClass('not_active');
            objLog.addClass('active');
            $('.log_out').slideDown(600);
        }
        else {
            objLog.removeClass('active');
            objLog.addClass('not_active');
        }
    });

})();
