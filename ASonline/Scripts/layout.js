
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
            url: 'Shop/AddNewsletter/',
            data: {
                NewsletterEmail: $("#newsletterEmail").val()
            }
        })
            .done(function (response) {
                $(".loading-icon").addClass("hide-spinner");
                toastr["success"]("Uspešno ste se pretplatili.");
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
            buttons: true,
            dangerMode: true,
        })
    }
});

function submitform() {
    $("#logoutForm").submit();
}

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

