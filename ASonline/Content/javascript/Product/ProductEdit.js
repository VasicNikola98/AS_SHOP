$('#editProduct').validate({
    rules: {
        Name: {
            required: true,
            minlength: 3,
            maxlength: 50
        },
        Description: {
            maxlength: 1000
        },
        Price: {
            required: true,
            range: [1, 1000000000000000]
        },
        PriceUnderline: {
            range: [1, 1000000000000000]
        }
    },
    messages: {
        Name: {
            required: 'Naziv proizvoda je obavezno polje!',
            minlength: 'Naziv proizvoda mora biti veći od 3 karaktera!',
            maxlength: 'Naziv proizvoda ne može da sadrži više od 50 karaktera!'
        },
        Description: {
            maxlength: 'Opis proizvoda ne može da sadrži više od 1000 karaktera!'
        },
        Price: {
            required: 'Cena proizvoda je obavezno polje!',
            range: 'Cena mora biti u opsegu od 1 do 1000000000000000!'
        },
        PriceUnderline: {
            range: 'Cena mora biti u opsegu od 1 do 1000000000000000!'
        }
    }
});

$("#updateBtn").click(function () {
    if ($("#editProduct").valid()) {
        $.ajax({
            type: 'POST',
            url: '/Product/Edit/', //'@Url.Action("Edit","Product")',
            data: $("#editProduct").serialize()
        })
            .done(function (response) {
                $("#tableContainer").html(response);
                $("#actionContainer").html("");
                toastr["success"]("Proizvod je uspešno ažuriran!");
                focusAction("tableContainer");

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
});

$("#cancelBtn").click(function () {
    $("#actionContainer").html("");
    focusAction("tableContainer");
});

$("#imageUpload").change(function () {


    var element = this;
    var formData = new FormData();
    var totalFiles = element.files.length;

    for (var i = 0; i < totalFiles; i++) {
        var file = element.files[i];
        formData.append("Photo", file);
    }

    $.ajax({
        type: 'POST',
        url: '/Shared/UploadImage/', //'@Url.Action("UploadImage", "Shared")',
        dataType: 'json',
        data: formData,
        contentType: false,
        processData: false
    })
        .done(function (response) {
            console.log(response);
            if (response.Success) {
                $("#productImage").attr("src", response.ImageUrl);
                $("#ImageUrl").val(response.ImageUrl);
            }
        })
        .fail(function (XMLHttpRequest, textStatus, errorThrown) {
            alert("Fail")
        });
});