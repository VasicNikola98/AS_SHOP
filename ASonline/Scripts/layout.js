$(document).ready(function () {

    var URL = "https://localhost:44333/";

    $('#subscribe-form').validate({
        rules: {
            newsletterEmail: {
                required: true,
            }
        },
        messages: {
            newsletterEmail: {
                required: 'Ovo polje je obavezno!',
            }
        }
    });

    $("#saveNewsletter").click(function () {
        if ($("#subscribe-form").valid()) {
            $(".loading-icon").removeClass("hide-spinner");
         
            $.ajax({
                type: 'POST',
                url: URL + "Shop/AddNewsletter/",
                data: {
                    NewsletterEmail: $("#newsletterEmail").val()
                }
            })
                .done(function (response) {
                    $(".loading-icon").addClass("hide-spinner");
                    toastr.options = {
                        "debug": false,
                        "positionClass": "toast-bottom-right",
                        "onclick": null,
                        "fadeIn": 300,
                        "fadeOut": 1000,
                        "timeOut": 5000,
                        "extendedTimeOut": 1000
                    }

                    toastr["success"]("Uspešno ste se pretplatili na mailing listu.");
                    $("#newsletterEmail").val('');
                })
                .fail(function (XMLHttpRequest, textStatus, errorThrown) {
                    alert("FAIL");
                });
        }
        else {
            swal({
                title: "Ups...",
                text: "Popunite polje sa emailom pa pokušajte ponovo!",
                icon: "warning",
                dangerMode: true,
            })
        }
    });

    function submitFormFooter() {
        $("#logoutFormFooter").submit();
    }

    const cookieContainer = document.querySelector(".cookie-container");
    const cookieBtn = document.querySelector(".cookieBtn");

    cookieBtn.addEventListener('click', () => {
        cookieContainer.classList.remove("cookie-container-active");
        localStorage.setItem("cookieBannerDisplayed", "true");
    });

    setTimeout(() => {
        if (!localStorage.getItem("cookieBannerDisplayed"))
            cookieContainer.classList.add("cookie-container-active");
    }, 2000);


});

function submitform() {
    $("#logoutForm").submit();
}
