// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

$(function () {
    function ajaxGet(url, replaceId) {
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

    function ajaxAppend(url, appendId, removeLoadMoreId) {
        $(removeLoadMoreId).remove();
        $.ajax(
            {
                type: "Get",
                url: url,
                success: (response) => {
                    $(appendId).append(response);
                },
                dataType: "html"
            });
    }

    $(document).ready(function () {
        $(document).on('click', '.httpGet',
            function () {

                ajaxGet(this.href, this.dataset.ajaxUpdate);
                // do something…
            });
    });

    $(document).ready(function () {
        $(document).on('click', '.append',
            function () {
                var url;
                if (this.href) {
                    url = this.href;
                } else {
                    url = this.dataset.ajaxUrl;
                }
                ajaxAppend(url, this.dataset.ajaxAppend, this.dataset.ajaxDelete);
            });
    });
});