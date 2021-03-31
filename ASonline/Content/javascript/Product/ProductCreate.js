$(document).ready(function () {

    var URL = "https://localhost:44333/"

    $('#createProduct').validate({
        rules: {
            Name: {
                required: true,
            },
            Price: {
                required: true,
               
            },
            PriceUnderline: {
                range: [0, 100]
            }
        },
        messages: {
            Name: {
                required: 'Naziv proizvoda je obavezno polje!',
            },
            Price: {
                required: 'Cena proizvoda je obavezno polje!',
            },
            PriceUnderline: {
                range: 'Sniženje mora biti u rangu od 0 do 100 %'
            }
        }
    });

    var ProductImage = new Array();
    var ProductStock = new Array();
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
            url: URL + "Shared/UploadImage/",
            dataType: 'json',
            data: formData,
            contentType: false,
            processData: false
        })
            .done(function (response) {
                if (response.Success) {
                    var product = {
                        Id: id,
                        ImageUrl: response.ImageUrl
                    }
                    ProductImage.push(product);

                    
                    var mainImageDiv = document.getElementById("product-images");
                    var imageDiv = document.createElement("div");
                    imageDiv.style.display = "inline-block";
                    var productImg = document.createElement("img");
                    productImg.src = response.ImageUrl;
                    productImg.alt = "Image";

                    var removeImageBtn = document.createElement("span");
                    removeImageBtn.innerHTML = "X";
                    removeImageBtn.id = id;

                    ++id;
                    removeImageBtn.onclick = function () {
                        removeImage(this);
                    };

                    productImg.classList = "productCreateImage-other";
                    removeImageBtn.classList = "removeOtherImage";
 
                    imageDiv.appendChild(productImg);
                    imageDiv.appendChild(removeImageBtn);
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
       
        while (i < ProductImage.length) {
            if (ProductImage[i].Id === parseInt(imageId)) {
                ProductImage.splice(i, 1);
            } else {
                ++i;
            }
        }

        e.parentElement.innerHTML = "";
    }

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

            if (ProductStock.length > 0) {
                var mainSizeDiv = document.getElementById("addedSizesId");
                mainSizeDiv.innerHTML = "";

                ProductStock.forEach((x) => {
                    var badgeDiv = document.createElement("div");
                    badgeDiv.classList.add("badge");
                    badgeDiv.classList.add("badge-secondary");
                    badgeDiv.classList.add("p-2");
                    badgeDiv.style.margin = "10px";

                    var sizeInfo = document.createElement("span");
                    sizeInfo.style.fontSize = "16px";
                    sizeInfo.innerHTML = `${x.Size} - ${x.Quantity}`;

                    var removeSizeBtn = document.createElement("span");
                    removeSizeBtn.classList = "removeSizeBtn";
                    removeSizeBtn.innerHTML = "X";
                    removeSizeBtn.id = x.DefaultWeight;
                    removeSizeBtn.onclick = function () {
                        removeSize(x);
                    }
                 

                    badgeDiv.appendChild(sizeInfo);
                    badgeDiv.appendChild(removeSizeBtn);
                    mainSizeDiv.appendChild(badgeDiv);
                });
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

    function removeSize(size) {
        
        var i = 0;
        while (i < ProductStock.length) {
            if (ProductStock[i].DefaultWeight === size.DefaultWeight) {
                ProductStock.splice(i, 1);
            } else {
                ++i;
            }
        }

        if (ProductStock.length >= 0) {
            var mainSizeDiv = document.getElementById("addedSizesId");
            mainSizeDiv.innerHTML = "";

            ProductStock.forEach((x) => {
                var badgeDiv = document.createElement("div");
                badgeDiv.classList.add("badge");
                badgeDiv.classList.add("badge-secondary");
                badgeDiv.classList.add("p-2");
                badgeDiv.style.margin = "10px";

                var sizeInfo = document.createElement("span");
                sizeInfo.style.fontSize = "16px";
                sizeInfo.innerHTML = `${x.Size} - ${x.Quantity}`;

                var removeSizeBtn = document.createElement("span");
                removeSizeBtn.classList = "removeSizeBtn";
  
                removeSizeBtn.innerHTML = "X";
                removeSizeBtn.id = x.DefaultWeight;
                removeSizeBtn.onclick = function () {
                    removeSize(x);
                }


                badgeDiv.appendChild(sizeInfo);
                badgeDiv.appendChild(removeSizeBtn);
                mainSizeDiv.appendChild(badgeDiv);
            });
        }

    }

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
                    ProductImage: ProductImage,
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
                dangerMode: true,
            })
        }
    });
  
});