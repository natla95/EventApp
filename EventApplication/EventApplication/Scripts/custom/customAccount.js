
function CurrentSlide(n) {
    ShowSlide(slideIndex = n);
}

function ShowSlide(n) {
    var i;
    var slides = document.getElementsByClassName("app_desc");
    var dots = document.getElementsByClassName("dot");
    if (n > slides.length) { slideIndex = 1 }
    if (n < 1) { slideIndex = slides.length }
    for (i = 0; i < slides.length; i++) {
        slides[i].style.display = "none";
    }
    for (i = 0; i < dots.length; i++) {
        dots[i].className = dots[i].className.replace("activeDot", "");
    }
    slides[slideIndex - 1].style.display = "block";
    dots[slideIndex - 1].className += " activeDot";
} 

(function () {

    $('#slide2').css("display", "block");
    $('#slideDot2').addClass("activeDot");

})();