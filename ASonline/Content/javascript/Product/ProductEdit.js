$(document).ready(function () {

    var URL = "https://localhost:44333/";

    $('#editProduct').validate({
        rules: {
            Name: {
                required: true
            },
            Price: {
                required: true,
            } 
        },
        messages: {
            Name: {
                required: 'Naziv proizvoda je obavezno polje!',
            },
            Price: {
                required: 'Cena je obavezno polje!'
            }
        }
    });

    $("#updateBtn").click(function () {
        if ($("#editProduct").valid()) {
            $.ajax({
                type: 'POST',
                url: URL + 'Product/Edit/', 
                data: $("#editProduct").serialize()
            })
                .done(function (response) {
                    if (response.Success) {
                        swal("Gotovo", "Proizvod je uspešno ažuriran!", "success").then((value) => {
                            window.location = URL + "Product/Index/";
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
                dangerMode: true,
            })
        }
    });


    var EditProductImages = new Array();
    var id = 0;
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
               
                if (response.Success) {

                    var image = {
                        Id: id,
                        ImageUrl: response.ImageUrl
                    }

                    EditProductImages.push(image);

                    var mainImageDiv = document.getElementById("image-wrapper");
                    var imageDiv = document.createElement("div");
                    imageDiv.style.display = "inline-block";

                    var image = document.createElement("img");
                    image.src = response.ImageUrl;
                    image.alt = "Image";
                    image.className = "productCreateImage-other";

                    var removeBtn = document.createElement("span");
                    removeBtn.innerHTML = "X";
                    removeBtn.className = "removeImageDBtn";
                    removeBtn.id = id;
                    ++id;

                    removeBtn.onclick = function () {
                        removeImage(this);
                    }

                    imageDiv.appendChild(image);
                    imageDiv.appendChild(removeBtn);
                    mainImageDiv.appendChild(imageDiv);
                }
            })
            .fail(function (XMLHttpRequest, textStatus, errorThrown) {
                alert("Fail")
            });
    });

    function removeImage(e) {

        var i = 0;
        var imageId = e.id;

        while (i < EditProductImages.length) {
            if (EditProductImages[i].Id === parseInt(imageId)) {
                EditProductImages.splice(i, 1);
            } else {
                ++i;
            }
        }

        e.parentElement.innerHTML = "";

    }

    $(".removeAvailableSize").click(function (e) {

        $.ajax({
            type: 'POST',
            url: URL + 'Product/DeleteSize/',
            data: {
                Id: $(this).attr('data-id')
            }
        })
            .done(function (response) {
                if (response.Success) {
                    e.target.parentElement.style.display = 'none';
                }
            })
            .fail(function (XMLHttpRequest, textStatus, errorThrown) {
                alert("Fail")
            });

    })

    $("#sizeForm").validate({
        rules: {
            Quantity: {
                required: true,
            },
        },
        messages: {
            Quantity: {
                required: 'Količina je obavezno polje!',
            }
        }
    });

    var sizeList = new Array();
    $(".addSizeBtn").click(function () {

        var size = $("#SizeId").val();
        var quantity = parseInt($("#QuantityId").val());
        var weight = "";
        var productId = $("#Id").val();

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
            if ($("#sizeForm").valid()) {
                $.ajax({
                    type: 'POST',
                    url: URL + 'Product/AddSize/',
                    data: {
                        ProductId: productId,
                        Size: size,
                        Quantity: quantity,
                        DefaultWeight: weight
                    }
                })
                    .done(function (response) {
                        if (response.Success) {

                            var existingSize = sizeList.find(({ Size }) => Size === size);

                            if (existingSize != undefined) {

                                sizeList.forEach((x) => {
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

                                sizeList.push(productStock);

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

                            if (sizeList.length > 0) {
                                var mainSizeDiv = document.getElementById("sizeDiv");
                                mainSizeDiv.innerHTML = "";

                                sizeList.forEach((x) => {
                                    var badgeDiv = document.createElement("div");
                                    badgeDiv.classList.add("badge");
                                    badgeDiv.classList.add("badge-secondary");
                                    badgeDiv.classList.add("p-2");
                                    badgeDiv.style.margin = "10px";

                                    var sizeInfo = document.createElement("span");
                                    sizeInfo.style.fontSize = "16px";
                                    sizeInfo.innerHTML = `${x.Size} - ${x.Quantity}`;

                                    badgeDiv.appendChild(sizeInfo);
                                    mainSizeDiv.appendChild(badgeDiv);
                                });
                            }
                        }
                    })
                    .fail(function (XMLHttpRequest, textStatus, errorThrown) {
                        alert("Fail")
                    });
            }
            else {
                swal({
                    title: "Ups...",
                    text: "Popunite sva polja ispravnim podacima!",
                    icon: "warning",
                    dangerMode: true,
                })
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

    $(".removeImageBtn").click(function (e) {
        
        $.ajax({
            type: 'POST',
            url: URL + 'Shared/DeleteImage/',
            data: {
                Id: $(this).attr("data-id")
            }
        })
            .done(function (response) {
                if (response.Success) {
                    e.target.parentElement.innerHTML = "";
                }
            })
            .fail(function (XMLHttpRequest, textStatus, errorThrown) {
                alert("Fail")
            });
    });
});