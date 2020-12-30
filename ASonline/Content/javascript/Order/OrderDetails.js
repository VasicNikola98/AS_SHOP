$('#changeStatus').change(function () {
    $.ajax({
        url: '/Order/ChangeStatus/',//'@Url.Action("ChangeStatus","Order")',
        data: {
            status: $("#changeStatus").val(),
            id: $("#orderId").val(),//'@Model.Order.Id'
        }
    })
        .done(function (response) {
            if (response.Success) {
                swal("Gotovo", "Status porudzbine je uspešno promenjen.", "success")
                    .then((e) => {
                        var url = "https://localhost:44333/Order"
                        window.location = url; //'@Url.Action("Index","Order")';
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