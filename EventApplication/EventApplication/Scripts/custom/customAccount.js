if (nr == 1)
    $('.register_box').addClass('active')
if (nr == 2)
    $('.login_box').addClass('active')

//req 
var firstNameReq = /^[A-Z-a-ząóęćłśńżźĄÓĘĆŁŚŃŻŹ]{2,50}$/;
var lastNameReq = /^[A-Z-a-ząóęćłśńżźĄÓĘĆŁŚŃŻŹ]{2,50}$/;
var emailReq = /^[A-Za-z0-9_\-\.]{1,}@[a-z0-9_\.]{1,}\.[a-z]{2,5}$/;

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

    //validation for forms' fields
    $('#FirstName').on("change", function () {
        var obj = $(this).val();
        if (obj.match(firstNameReq) != null) {
            $(this).closest(".field_box").removeClass("error");
            $(this).siblings().removeClass("field-validation-error");
        }
        else {
            $(this).closest(".field_box").addClass("error");
        }
        
    });
    $('#LastName').on("change", function () {
        var obj = $(this).val();
        if (obj.match(lastNameReq) != null) {
            $(this).closest(".field_box").removeClass("error");
            $(this).siblings().removeClass("field-validation-error");
        }
        else {
            $(this).closest(".field_box").addClass("error");
            $(this).siblings().removeClass("field-validation-error");
        }
    });
    $('#Email').on("change", function () {
        var obj = $(this).val();
        if (obj.match(emailReq) != null) {
            $(this).closest(".field_box").removeClass("error");
            $(this).siblings().removeClass("field-validation-error");
        }
        else {
            $(this).closest(".field_box").addClass("error");
        }
        
    });
    $('#Password').on("change", function () {
        var obj = $(this);
        if (obj.length <= 8) {
            $(this).closest(".field_box").addClass("error");
            $(this).siblings().removeClass("field-validation-error");
        }
        else {
            $(this).closest(".field_box").removeClass("error");
        }

    });

})();