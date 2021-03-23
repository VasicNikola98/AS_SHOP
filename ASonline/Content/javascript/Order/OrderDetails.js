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

    $('#changeStatus').change(function () {
        $.ajax({
            url: URL + 'Order/ChangeStatus/',
            data: {
                status: $("#changeStatus").val(),
                id: $("#orderId").val(),
            }
        })
            .done(function (response) {
                if (response.Success) {
                    swal("Gotovo", "Status porudzbine je uspešno promenjen.", "success")
                        .then((e) => {
                          
                            window.location = URL + "Order"; 
                        });
                }
                else {
                    swal("Greška", "Ažuriranje porudzbine nije uspelo.", "warning");
                }
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
});