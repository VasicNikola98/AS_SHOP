
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
        url:  'Category/_CategoryTable/',//'@Url.Action("_CategoryTable","Category")',
        data: {
            search: searchValue
        }
    })
        .done(function (response) {
            $("#tableCategoryContainer").html(response);
        })
        .fail(function (XMLHttpRequest, textStatus, errorThrown) {
            alert("Fail")
        });
});

$("#newBtn").click(function () {
    $.ajax({
        url: '/Category/Create',
    })
        .done(function (response) {
            $("#actionCategoryContainer").html(response);
            focusAction("actionCategoryContainer");
        })
        .fail(function (XMLHttpRequest, textStatus, errorThrown) {
            alert("Fail")
        });
});

$(".editBtn").click(function () {
    $.ajax({
        url: 'Category/Edit/',//'@Url.Action("Edit","Category")',
        data: {
            Id: $(this).attr('data-id'),
        }
    })
        .done(function (response) {
            $("#actionCategoryContainer").html(response);

            focusAction("actionCategoryContainer");
        })
        .fail(function (XMLHttpRequest, textStatus, errorThrown) {
            alert("Fail")
        });
});

$(".pageButtons").click(function () {
    $.ajax({
        url: 'Category/_CategoryTable/',//'@Url.Action("_CategoryTable", "Category")',
        data: {
            pageNo: $(this).attr("data-pageNo"),
            search: $("#SearchTerm").val()
        }
    })
        .done(function (response) {
            $("#tableCategoryContainer").html(response);
        })
        .fail(function (XMLHttpRequest, textStatus, errorThrown) {
            alert("Fail")
        });
});

$(".deleteBtn").click(function () {
    swal({
        text: "Da li ste sigurni da želite da izbrišete ovu kategoriju?",
        icon: "warning",
        buttons: true,
        dangerMode: true,
    })
        .then((willDelete) => {
            if (willDelete) {
                $.ajax({
                    type: 'POST',
                    url: 'Category/Delete/',//'@Url.Action("Delete","Category")',
                    data: {
                        Id: $(this).attr('data-id'),
                    }
                })
                    .done(function (response) {
                        $("#tableCategoryContainer").html(response);
                        swal("Kategorija je uspešno izbrisana!", {
                            icon: "success",
                        });
                    })
                    .fail(function (XMLHttpRequest, textStatus, errorThrown) {
                        alert("Fail")
                    });
            }
        });
});