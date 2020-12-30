$('#createOrderDetails').validate({
    rules: {
        FirstName: {
            required: true,
        },
        LastName: {
            required: true,
        },
        Address: {
            required: true,
        },
        Nummber: {
            required: true,
        },
        City: {
            required: true,
        },
        PostCode: {
            required: true,
        },

    },
    messages: {
        FirstName: {
            required: 'Ime je obavezno polje!',
        },
        LastName: {
            required: 'Prezime je obavezno polje!',
        },
        Address: {
            required: 'Adresa je obavezno polje!',
        },
        Nummber: {
            required: 'Broj telefona je obavezno polje!',
        },
        City: {
            required: 'Grad je obavezno polje!',
        },
        PostCode: {
            required: 'Poštanski broj je obavezno polje!',
        }
    }
});

$("#placeOrderBtn").click(function () {
    if ($("#createOrderDetails").valid()) {
        $(".loading-icon").removeClass("hide-spinner");

        let orderDetails = {
            FirstName: $("#FirstName").val(),
            LastName: $("#LastName").val(),
            Email: $("#Email").val(),
            Address: $("#Address").val(),
            Nummber: $("#Nummber").val(),
            Country: $("#Country").val(),
            City: $("#City").val(),
            PostCode: $("#PostCode").val()
        };

        $.ajax({
            type: 'POST',
            url: '/Shop/PlaceOrder/',//'@Url.Action("PlaceOrder","Shop")',
            dataType: "json",
            data: orderDetails
        })
            .done(function (response) {
                if (response.Success) {
                    getCartCounter();
                    $(".loading-icon").addClass("hide-spinner");
                    let url = "https://localhost:44333/Shop/";
                    swal("Gotovo", "Vaša narudzbina je prihvaćena.", "success").then((value) => {
                        window.location = url;//'@Url.Action("Index","Shop")';
                    });

                }
                else {
                    swal("Greška", "Nije moguće izvršiti narudzbinu.", "warning");
                }
            })
            .fail(function (XMLHttpRequest, textStatus, errorThrown) {
                alert("FAIL")
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
});
