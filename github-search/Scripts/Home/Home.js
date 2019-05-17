$(function () {
    RefreshProductList();
});

function RefreshProductList(url) {
    //post the URL and get the data for the page.
    $(document).off().on("click", "#Search", function (event) {

        var data = { Search = $(this).text() };

        $.post({
            url: url,
            data: data
        }).done(function (data) {
            $(".faceted-list").html(data);
        });
    }
}