$(document).ready(function () {
    $('input[type="radio"]').on('click change', function (e) {

        let inputId = $(this).val();
        var quantity = document.getElementById(inputId).value;

        $("#qty").html(quantity + " na zalihama");

        $("#quantityCounter").attr({
            "max": quantity
        });

        let quantityAlert = document.getElementById("quantity-alert");

        quantityAlert.classList.add("alert-success");
    });

    $(".productAddToCart").click(function () {


        let size = "";
        size = $('input[name="Size"]:checked').val();
        let quantity = $("#quantityCounter").val();

        if (size != "" && size != undefined && size != null) {
            $(".loading-icon").removeClass("hide-spinner");
            let prId = $(this).attr('data-id');

            if (localStorage.getItem('cartHashUserId') == null) {
                let hashUserId = generateUUID();
                localStorage.setItem('cartHashUserId', hashUserId);
                $.cookie('cartItemHashedUserId', hashUserId, { path: '/' });
            }

            let url = 'https://localhost:44333/Shop/PlaceCartItem/'

            var cartItem = {
                productId: prId,
                productSize: size,
                productQuantity: quantity
            };

            $.ajax({
                method: 'POST',
                dataType: 'json',
                url: url,
                data: cartItem
            })
                .done(function (response) {
                    if (response.Success) {
                        getCartCounter();
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

                        toastr["success"]("Proizvod je dodat u korpu!");
                    }
                    else {
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

                        toastr["warning"]("Ovu količinu nije moguće dodati u korpu!");
                    }
                });

        }
        else {
            swal({
                title: "Ups..",
                text: "Morate izabrati veličinu pre dodavanja proizvoda u korpu!",
                icon: "warning",

                dangerMode: true,
            })
        }
    });

    $('#commentform').validate({
        rules: {
            Name: {
                required: true,
                minlength: 2,
                maxlength: 50
            },
            Email: {
                required: true
            },
            Comment: {
                required: true
            }
        },
        messages: {
            Name: {
                required: 'Ime je obavezno polje!',
                minlength: 'Ime mora biti veći od 2 karaktera!',
                maxlength: 'Ime ne može da sadrži više od 50 karaktera!'
            },
            Email: {
                required: 'Email je obavezno polje!'
            },
            Comment: {
                required: 'Komentar je obavezno polje!'
            }
        }
    });

    $(".comment-submit").click(function () {

        var productId = parseInt($("#productId").val());
        var Rating = "";
        Rating = $('input[name="star"]:checked').val();

        if (Rating != "" && Rating != undefined && Rating != null) {
            if ($("#commentform").valid()) {

                var review = {
                    productId: productId,
                    Rating: parseInt(Rating),
                    Name: $("#Name").val(),
                    Email: $("#Email").val(),
                    Comment: $("#Comment").val()
                };

                let url = 'https://localhost:44333/Product/AddReviews/'
                $.ajax({
                    method: 'POST',
                    url: url,//'@Url.Action("AddReviews", "Product")',
                    dataType: "json",
                    data: review
                })
                    .done(function (response) {
                        if (response.Success) {
                            swal("Hvala na oceni", "Vaša recenzija je poslata. Biće odobrena u najkraćem roku od strane administratora.", "success").then((value) => {
                                window.location = location;
                            });
                        }
                    })
                    .fail(function (XMLHttpRequest, textStatus, errorThrown) {
                        alert("Fail")
                    });
            }
            else {
                swal({
                    title: "Ups...",
                    text: "Popunite sva polja koja su označena * ispravnim podacima!",
                    icon: "warning",
                    buttons: true,
                    dangerMode: true,
                })
            }
        }
        else {
            swal({
                title: "Ups...",
                text: "Ocenite proizvod!",
                icon: "warning",
                buttons: true,
                dangerMode: true,
            })
        }
    });
});
