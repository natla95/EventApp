console.log(iconNr)
if (iconNr == 1)
    $('#menu1').addClass('active');
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
