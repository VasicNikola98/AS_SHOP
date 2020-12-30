$("#saveSizeBtn").click(function () {
    // if ($("#stockProduct").valid()) {
    $.ajax({
        type: 'POST',
        url: '/Product/Size/',//'@Url.Action("Size","Product")',
        data: $("#stockProduct").serialize()
    })
        .done(function (response) {
            $("#tableContainer").html(response);
            $("#actionContainer").html("");
            toastr["success"]("Velicina je uspešno dodata!");
            focusAction("tableContainer");
        })
        .fail(function (XMLHttpRequest, textStatus, errorThrown) {
            alert("FAIL");
        });
    //}
    //else {
    //swal({
    //title: "Ups...",
    //text: "Popunite sva polja koja su označena * ispravnim podacima!",
    //icon: "warning",
    //buttons: true,
    //dangerMode: true,
    //})
    //}
});

$("#cancelBtn").click(function () {
    $("#actionContainer").html("");
    focusAction("tableContainer");
});