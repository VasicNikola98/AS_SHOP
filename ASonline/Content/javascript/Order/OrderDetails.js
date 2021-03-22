$(document).ready(function () {
    var URL = "https://localhost:44333/";

    $('#changeStatus').change(function () {
        $.ajax({
            url: URL + 'Order/ChangeStatus/',
            data: {
                status: $("#changeStatus").val(),
                id: $("#orderId").val(),
            }
        })
            .done(function (response) {
                if (response.Success) {
                    swal("Gotovo", "Status porudzbine je uspešno promenjen.", "success")
                        .then((e) => {
                          
                            window.location = URL + "Order"; 
                        });
                }
                else {
                    swal("Greška", "Ažuriranje porudzbine nije uspelo.", "warning");
                }
            })
            .fail(function (XMLHttpRequest, textStatus, errorThrown) {
                alert("Fail")
            });
    });
});