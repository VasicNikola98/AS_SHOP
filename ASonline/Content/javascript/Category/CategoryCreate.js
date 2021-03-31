$(document).ready(function () {

    var URL = "https://localhost:44333/";

    $('#createCategory').validate({
        rules: {
            Name: {
                required: true,
                minlength: 3,
                maxlength: 50
            },
            Description: {
                maxlength: 1000
            },
            IsFeatured: {
                required: true,
            }
        },
        messages: {
            Name: {
                required: 'Naziv kategorije je obavezno polje!',
                minlength: 'Naziv kategorije mora biti veći od 3 karaktera!',
                maxlength: 'Naziv kategorije ne može da sadrži više od 50 karaktera!'
            },
            Description: {
                maxlength: 'Opis kategorije ne može da sadrži više od 1000 karaktera!'
            },
            IsFeatured: {
                required: 'Izaberite jedno od ponudjenih polja.',
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
            url: URL + 'Shared/UploadImage/',
            dataType: 'json',
            data: formData,
            contentType: false,
            processData: false
        })
            .done(function (response) {
                console.log(response);
                if (response.Success) {
                    $("#categoryImage").attr("src", response.ImageUrl);
                    $("#ImageUrl").val(response.ImageUrl);
                }
            })
            .fail(function (XMLHttpRequest, textStatus, errorThrown) {
                alert("Fail")
            });
    });

    $("#saveCategoryBtn").click(function () {
        if ($("#createCategory").valid()) {
            $.ajax({
                type: 'POST',
                url: URL + 'Category/Create/',
                data: $("#createCategory").serialize()
            })
                .done(function (response) {
                    swal("Gotovo", "Kategorija je uspešno dodata!", "success").then((value) => {
                        window.location = URL + "Category/Index/";
                    });
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
                dangerMode: true,
            });
        }
    });

});