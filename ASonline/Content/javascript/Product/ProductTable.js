let searchInputField = document.getElementById("searchTxt");
searchInputField.addEventListener('keyup', (e) => {
    if (e.keyCode === 13) {
        document.getElementById("searchBtn").click();
    }
})

$("#searchBtn").click(function () {
    var searchValue = $("#searchTxt").val();

    $.ajax({
        type: 'POST',
        url: '/Product/ProductTable/',//'@Url.Action("ProductTable","Product")',
        data: {
            search: searchValue
        }
    })
        .done(function (response) {
            $("#tableContainer").html(response);
        })
        .fail(function (XMLHttpRequest, textStatus, errorThrown) {
            alert("Fail")
        });
});

$("#newBtn").click(function () {
    $.ajax({
        url: '/Product/Create/',
    })
        .done(function (response) {
            $("#actionContainer").html(response);
            focusAction("actionContainer");
        })
        .fail(function (XMLHttpRequest, textStatus, errorThrown) {
            alert("Fail")
        });
});

$(".pageButtons").click(function () {
    $.ajax({
        url: '/Product/ProductTable/',//'@Url.Action("ProductTable","Product")',
        data: {
            pageNo: $(this).attr("data-pageNo"),
            search: $("#searchTerm").val(),//'@Model.SearchTerm'
        }
    })
        .done(function (response) {
            $("#tableContainer").html(response);
        })
        .fail(function (XMLHttpRequest, textStatus, errorThrown) {
            alert("Fail")
        });
});

$(".editBtn").click(function () {
    $.ajax({
        url: "/Product/Edit/",//'@Url.Action("Edit","Product")',
        data: {
            Id: $(this).attr('data-id'),
        }
    })
        .done(function (response) {
            $("#actionContainer").html(response);

            focusAction("actionContainer");
        })
        .fail(function (XMLHttpRequest, textStatus, errorThrown) {
            alert("Fail")
        });
});

$(".sizeBtn").click(function () {
    $.ajax({
        url: "/Product/Size/",//'@Url.Action("Size","Product")',
        data: {
            Id: $(this).attr('data-id'),
        }
    })
        .done(function (response) {
            $("#actionContainer").html(response);
            focusAction("actionContainer");
        })
        .fail(function (XMLHttpRequest, textStatus, errorThrown) {
            alert("Fail")
        });
});

$(".deleteBtn").click(function () {
    swal({
        text: "Da li ste sigurni da želite da izbrišete ovaj proizvod?",
        icon: "warning",
        buttons: true,
        dangerMode: true,
    })
        .then((willDelete) => {
            if (willDelete) {
                var url = "https://localhost:44333/Product/Delete/"
                $.ajax({
                    type: 'POST',
                    url: url,//"/Product/Delete/",//'@Url.Action("Delete","Product")',
                    data: {
                        Id: $(this).attr('data-id'),
                    }
                })
                    .done(function (response) {
                        $("#tableContainer").html(response);
                        swal("Proizvod je uspešno izbrisan!", {
                            icon: "success",
                        });
                    })
                    .fail(function (XMLHttpRequest, textStatus, errorThrown) {
                        alert("Fail")
                    });
            }
        });
});
