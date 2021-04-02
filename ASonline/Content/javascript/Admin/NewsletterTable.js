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
            url: URL + "/Admin/NewsletterTable/",
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
            url: URL + "/Admin/NewsletterTable/",
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
});