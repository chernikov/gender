function IndexBlogPost() {
    _this = this;
    this.ajaxRedirects = "/admin/BlogPost/Redirects";
    this.ajaxAddRedirect = "/admin/BlogPost/AddRedirect";
    this.ajaxRemoveRedirect = "/admin/RecordRedirect/Remove";

    this.init = function () {

        $(".redirect").click(function () {
            _this.showRedirectDialog($(this).data("id"));
            return false;
        });
    }

    this.showRedirectDialog = function (id) {
        _this.selectedID = id;
        $.ajax({
            type: "GET",
            url: _this.ajaxRedirects,
            data: { id: id },
            success: function (data) {
                $("#redirectWrapper").modal();
                $("#redirectWrapper").html(data);
                _this.initRedirect();
            }
        });
    };

    this.initRedirect = function () {
        $("#AddRedirect").click(function () {
            var ajaxData = $("#RedirectForm").serialize();
            $.ajax({
                type: "POST",
                url: _this.ajaxAddRedirect,
                data: ajaxData,
                success: function (data) {
                    if (data.result == "ok") {
                        _this.showRedirectDialog(_this.selectedID);
                    }
                }
            });
            return false;
        });

        $(".remove-redirect").click(function () {
            var id = $(this).data("id");
            var item = $(this).closest(".redirect-line");
            $.ajax({
                type: "POST",
                url: _this.ajaxRemoveRedirect,
                data: { id: id },
                success: function (data) {
                    if (data.result == "ok") {
                        item.remove();
                    }
                }
            });
            return false;
        });

        $("#BlogPostID").val(_this.selectedID);
    }

}

var indexBlogPost = null;
$().ready(function () {
    indexBlogPost = new IndexBlogPost();
    indexDocument.init();
});

