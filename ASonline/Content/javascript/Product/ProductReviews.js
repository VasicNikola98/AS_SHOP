$(document).ready(function () {

    var URL = "https://localhost:44333/";

    $(".acceptReviewBtn").click(function () {
        $.ajax({
            type: 'POST',
            url: URL + 'Product/AcceptReview/',
            data: {
                Id: $(this).attr('data-id')
            }
        })
            .done(function (response) {
                window.location = location;
            })
            .fail(function (XMLHttpRequest, textStatus, errorThrown) {
                alert("FAIL");
            });
    });
});