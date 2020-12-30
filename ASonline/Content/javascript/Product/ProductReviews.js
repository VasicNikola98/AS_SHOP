$(".acceptReviewBtn").click(function () {
    $.ajax({
        type: 'POST',
        url: '/Product/AcceptReview/',//'@Url.Action("AcceptReview", "Product")',
        data: {
            Id: $(this).attr('data-id')
        }
    })
        .done(function (response) {
            window.location = location;
            toastr["success"]("Komentar je odobren!");
        })
        .fail(function (XMLHttpRequest, textStatus, errorThrown) {
            alert("FAIL");
        });
});