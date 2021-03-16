$('#createProduct').validate({
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
        url: 'Shared/UploadImage/', 
        dataType: 'json',
        data: formData,
        contentType: false,
        processData: false
    })
        .done(function (response) {
            if (response.Success) {
                var ImgUrl = [];
                for (var i = 0; i < response.ImageUrl.length; i++) {
                    ImgUrl.push(response.ImageUrl[i]);
                }
                $("#ImageUrl").val(ImgUrl);

                var thumbDiv = document.getElementById("thumbId");
               
                var thumb = document.createElement("div");
                thumb.className = "thumb";
                var img = document.createElement("img");
                img.src = ImgUrl[0];
                img.alt = "image";
                thumb.appendChild(img);
                thumbDiv.appendChild(thumb);


                for (var i = 1; i < ImgUrl.length; i++) {
                    var thumb = document.createElement("div");
                    thumb.className = "productImage";
                    var img = document.createElement("img");
                    img.src = ImgUrl[i];
                    img.alt = "image";
                    img.className = "thumbPhoto";
                    
                    thumb.appendChild(img);
                    thumbDiv.appendChild(thumb);
                }

            }
        })
        .fail(function (XMLHttpRequest, textStatus, errorThrown) {
            alert("Fail")
        });
});

$("#saveBtn").click(function () {
    if ($("#createProduct").valid()) {
        $.ajax({
            type: 'POST',
            url: 'Product/Create/',
            data: $("#createProduct").serialize()
        })
            .done(function (response) {
                $("#tableContainer").html(response);
                $("#actionContainer").html("");
                toastr["success"]("Proizvod je uspešno dodat!");
                focusAction("tableContainer");
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