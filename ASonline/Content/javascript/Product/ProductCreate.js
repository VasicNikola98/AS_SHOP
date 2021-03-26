$(document).ready(function () {

    var URL = "https://localhost:44333/"

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

    var ProductImage = new Array();
    var ProductStock = new Array();

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
            url: URL + "Shared/UploadImage/",
            dataType: 'json',
            data: formData,
            contentType: false,
            processData: false
        })
            .done(function (response) {
                if (response.Success) {
                    ProductImage.push(response.ImageUrl);

                    var mainImageDiv = document.getElementById("product-images");
                    var imageDiv = document.createElement("div");
                    imageDiv.style.display = "inline-block";
                    var productImg = document.createElement("img");
                    productImg.src = response.ImageUrl;
                    productImg.alt = "Image";

                    if (ProductImage.length == 1) {
                        productImg.classList = "productCreateImage-thumb";
                    }
                    else {
                        productImg.classList = "productCreateImage-other";
                    }

                    imageDiv.appendChild(productImg);
                    mainImageDiv.appendChild(imageDiv);

                }
            })
            .fail(function (XMLHttpRequest, textStatus, errorThrown) {
                alert("Fail")
            });
    });

    $("#addSizeBtn").click(function () {
      
        var size = $("#SizeId").val();
        var quantity = parseInt($("#QuantityId").val());
        var weight = "";

        switch (size) {
            case "XS":
                weight = 0;
                break;
            case "S":
                weight = 1;
                break;
            case "M":
                weight = 2;
                break;
            case "L":
                weight = 3;
                break;
            case "XL":
                weight = 4;
                break;
            case "XXL":
                weight = 5;
                break;
            case "XXXL":
                weight = 6;
                break;
            default:
                break;
        }

  

        if (quantity > 0) {

            var existingSize = ProductStock.find(({ Size }) => Size === size);

            if (existingSize != undefined) {

                ProductStock.forEach((x) => {
                    if (x.Size === size) {
                        x.Quantity = x.Quantity + parseInt(quantity);
                    }
                })

                toastr.options = {
                    "debug": false,
                    "positionClass": "toast-bottom-right",
                    "onclick": null,
                    "fadeIn": 300,
                    "fadeOut": 1000,
                    "timeOut": 5000,
                    "extendedTimeOut": 1000
                }

                toastr["success"]("Veličina je uspešno dodata!");

            }
            else {

                var productStock = {
                    Size: size,
                    Quantity: quantity,
                    DefaultWeight: weight
                }

                ProductStock.push(productStock);

                toastr.options = {
                    "debug": false,
                    "positionClass": "toast-bottom-right",
                    "onclick": null,
                    "fadeIn": 300,
                    "fadeOut": 1000,
                    "timeOut": 5000,
                    "extendedTimeOut": 1000
                }

                toastr["success"]("Veličina je uspešno dodata!");

            }
            
        }
        else {
            toastr.options = {
                "debug": false,
                "positionClass": "toast-bottom-right",
                "onclick": null,
                "fadeIn": 300,
                "fadeOut": 1000,
                "timeOut": 5000,
                "extendedTimeOut": 1000
            }

            toastr["error"]("Količina mora biti veća od nule!");
        }
       
    });

    $("#saveBtn").click(function () {
        if ($("#createProduct").valid()) {

            $.ajax({
                type: 'POST',
                url: URL + "Product/Create/",
                data: {
                    Name: $("#Name").val(),
                    Description: $("#Description").val(),
                    PriceUnderline: $("#PriceUnderline").val(),
                    Price: $("#Price").val(),
                    CategoryId: $("#CategoryId").val(),
                    ImageUrl: ProductImage,
                    Stock: ProductStock
                }
            })
                .done(function (response) {
                   
                    swal("Gotovo", "Proizvod je uspešno dodat!", "success").then((value) => {
                        window.location = URL + "Product/Index/";
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
                buttons: true,
                dangerMode: true,
            })
        }
    });
  
});