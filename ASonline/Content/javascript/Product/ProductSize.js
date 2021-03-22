$(document).ready(function () {
    $('#stockProduct').validate({
        rules: {
            Size: {
                required: true,
            },
            Quantity: {
                required: true
            }
        },
        messages: {
            Size: {
                required: 'Ovo polje je obavezno!',
            },
            Quantity: {
                required: 'Ovo polje je obavezno!'
            }
        }
    });

    $("#saveSizeBtn").click(function () {
        if ($("#stockProduct").valid()) {
            $.ajax({
                type: 'POST',
                url: '/Product/Size/',
                data: $("#stockProduct").serialize()
            })
                .done(function (response) {
                    toastr["success"]("Veličina je uspešno dodata!");
                })
                .fail(function (XMLHttpRequest, textStatus, errorThrown) {
                    alert("FAIL");
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

    $("#cancelBtn").click(function () {
        $("#actionContainer").html("");
        focusAction("tableContainer");
    });
});