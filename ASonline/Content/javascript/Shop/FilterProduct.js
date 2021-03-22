$(document).ready(function () {
    var products;
    $(".productAddToCart").click(function () {

        var existingCookieData = $.cookie('CartProducts');

        if (existingCookieData != undefined && existingCookieData != "" && existingCookieData != null) {

            products = existingCookieData.split('-');

        }
        else {
            products = [];
        }
        var productId = $(this).attr('data-id');

        products.push(productId);
        $.cookie('CartProducts', products.join('-'), { path: '/' });

        updateCartProducts();
        swal("Done", "Product Added to Cart!", "success");
    });
});
