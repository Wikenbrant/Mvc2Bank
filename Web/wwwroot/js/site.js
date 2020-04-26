// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

$(function () {
    function AjaxGet(url, replaceId) {
        debugger;
        $.ajax(
            {
                type: "Get",
                url: url,
                success: (response) => {
                    $(replaceId).html(response);
                },
                dataType: "html"
            });
    }

    $(document).ready(function () {
        $(document).on('click', '.httpGet',
            function () {

                AjaxGet(this.href, this.dataset.ajaxUpdate);
                debugger;
                // do something…
            });
    });
});