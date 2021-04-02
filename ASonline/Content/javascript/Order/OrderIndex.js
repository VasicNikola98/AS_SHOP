$(document).ready(function () {

    var URL = "https://localhost:44333/";

    let searchInputField = document.getElementById("searchTxt");
    searchInputField.addEventListener('keyup', (e) => {
        if (e.keyCode === 13) {
            document.getElementById("searchBtn").click();
        }
    })

    $("#searchBtn").click(function () {
        var searchValue = $("#searchTxt").val();
        var status = $("#status").val();

        $.ajax({
            type: 'POST',
            url: URL + 'Order/OrderTable/',
            data: {
                searchTerm: searchValue,
                status: status
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
            url: URL + "Order/OrderTable/",
            data: {
                pageNo: $(this).attr("data-pageNo"),
                search: $("#searchTerm").val(),
                status: $("#status").val()
            }
        })
            .done(function (response) {
                $("#tableContainer").html(response);
            })
            .fail(function (XMLHttpRequest, textStatus, errorThrown) {
                alert("Fail")
            });
    });

    $(".filterStatus").click(function () {

        var statusFilter = $(this).find("input").val();

        $.ajax({
            url: URL + "Order/OrderTable/",
            data: {
                search: $("#searchTerm").val(),
                status: statusFilter
            }
        })
            .done(function (response) {
                $("#tableContainer").html(response);
            })
            .fail(function (XMLHttpRequest, textStatus, errorThrown) {
                alert("Fail")
            });

    });

    $(".arhiveBtn").click(function () {

        $.ajax({
            type: 'POST',
            url: URL + 'Order/ArhiveOrder/',
            data: {
                Id: $(this).attr("data-id")
            }
        })
            .done(function (response) {
                if (response.Success) {
                    swal("Gotovo", "Porudžbina je uspešno arhivirana!", "success").then((value) => {
                        window.location = URL + "Order/Index/";
                    });
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

                    toastr["warning"]("Porudžbinu je moguće arhivirati samo kada je njen status postavljen na 'Isporučena' ");
                }
            })
            .fail(function (XMLHttpRequest, textStatus, errorThrown) {
                alert("Fail")
            });
    });

    $(".recoverOrderBtn").click(function () {

        $.ajax({
            type: 'POST',
            url: URL + 'Order/RecoverOrder/',
            data: {
                Id: $(this).attr("data-id")
            }
        })
            .done(function (response) {
                if (response.Success) {
                    swal("Gotovo", "Narudžbina je uspešno vraćena u tabeli sa aktivnim porudžbinama!", "success").then((value) => {
                        window.location = URL + "Order/ArhiveIndex/";
                    });
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

                    toastr["error"]("Došlo je do greške pri izvršavanju ove funcije!");
                }
            })
            .fail(function (XMLHttpRequest, textStatus, errorThrown) {
                alert("Fail")
            });
    });
});