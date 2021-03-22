$(document).ready(function () {
    let searchInputField = document.getElementById("searchTxt");
    searchInputField.addEventListener('keyup', (e) => {
        if (e.keyCode === 13) {
            document.getElementById("searchBtn").click();
        }
    })

    var URL = "https://localhost:44333";

    $("#searchBtn").click(function () {
        var searchValue = $("#searchTxt").val();

        $.ajax({
            type: 'POST',
            url: URL + "/Product/ProductTable/",
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

    $(".pageButtons").click(function () {

        $.ajax({
            url: URL + "/Product/ProductTable/",
            data: {
                pageNo: $(this).attr("data-pageNo"),
                search: $("#searchTerm").val(),
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
            url: URL + '/Product/Create/',
        })
            .done(function (response) {
               
            })
            .fail(function (XMLHttpRequest, textStatus, errorThrown) {
                alert("Fail")
            });
    });

    $(".editBtn").click(function () {

        $.ajax({
            url: URL + "/Product/Edit/",
            data: {
                Id: $(this).attr('data-id'),
            }
        })
            .done(function (response) {
               
            })
            .fail(function (XMLHttpRequest, textStatus, errorThrown) {
                alert("Fail")
            });
    });

    $(".sizeBtn").click(function () {
        $.ajax({
            url: URL + "/Product/Size/",
            data: {
                Id: $(this).attr('data-id'),
            }
        })
            .done(function (response) {
                
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

                    $.ajax({
                        type: 'POST',
                        url: URL + "/Product/Delete/",
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
});

