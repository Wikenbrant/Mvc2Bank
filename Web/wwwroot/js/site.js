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
                    if (response.redirect) {
                        // data.redirect contains the string URL to redirect to
                        window.location.href = response.redirect;
                    } else {
                        // data.form contains the HTML for the replacement form
                        $(replaceId).html(response);
                    }
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
                    if (response.redirect) {
                        // data.redirect contains the string URL to redirect to
                        window.location.href = response.redirect;
                    } else {
                        // data.form contains the HTML for the replacement form
                        $(appendId).append(response);
                    }
                },
                dataType: "html"
            });
    }

    $(document).ready(function () {
        $(document).on('click', '.httpGet',
            function () {
                
                ajaxGet(this.href, this.dataset.ajaxUpdate);
            });

        $(document).on('change', '.httpSearch',
            function (e) {
                e.preventDefault();
                $.ajax(
                    {
                        type: "Get",
                        url: this.dataset.url,
                        data: {
                            searchText: $("#search").val()
                        },
                        success: (response) => {
                            if (response.redirect) {
                                // data.redirect contains the string URL to redirect to
                                window.location.href = response.redirect;
                            } else {
                                // data.form contains the HTML for the replacement form
                                $(this.dataset.replaceid).html(response);
                            }
                            
                        },
                        dataType: "html"
                    });
            });
        $(document).on('click', '.httpOther',
            function (e) {
                e.preventDefault();

                $.ajax(
                    {
                        type: "Get",
                        url: this.href,
                        data: {
                            searchText: $("#search").val()
                        },
                        success: (response) => {
                            
                            if (response.redirect) {
                                // data.redirect contains the string URL to redirect to
                                window.location.href = response.redirect;
                            } else {
                                // data.form contains the HTML for the replacement form
                                $(this.dataset.replaceid).html(response);
                            }
                        },
                        dataType: "html"
                    });
            });

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

        $(document).on('click', '.customer-tab-pane',
            function () {
                $("#customer" + this.dataset.customerid + "Tabs").find(".active").toggleClass("active");
                $("#customer" + this.dataset.customerid + "NavTabs").find(".active").toggleClass("active");
                $(this.dataset.open).toggleClass("active");
                $(this).toggleClass("active");
            });

        $(document).on('submit', '.createTransactionForm',
            function (e) {
                e.preventDefault();
                var form = this;
                $.ajax({
                    type: "POST",
                    url: this.action,
                    data: $(this).serialize(),
                    success: function (data) {
                        if (data.redirect) {
                            // data.redirect contains the string URL to redirect to
                            window.location.href = data.redirect;
                        } else {
                            // data.form contains the HTML for the replacement form
                            if (data.length > 0) {
                                for (var i = 0; i < data.length; i++) {
                                    var error = data[i];
                                    var errorLi = $("<li></li>").text(error);
                                    $(form).find(".errors").append(errorLi);
                                }
                            }
                        }
                        
                    }
                });
            });
    });
});