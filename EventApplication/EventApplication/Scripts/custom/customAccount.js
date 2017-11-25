
if (nr == 1)
    $('.register_box').addClass('active')
if (nr == 2)
    $('.login_box').addClass('active')

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

    $(".field_box").on("click", function () {
        $(".field_box").removeClass("active");
        $(".field_box").find("i").removeClass("active");
        $(this).addClass("active");
        $(this).find("i").addClass("active");
    });
})();