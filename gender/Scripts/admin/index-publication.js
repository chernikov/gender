function IndexPublication() {
    var _this = this;

    this.ajaxAccess = "/admin/Publication/Access";
    this.ajaxAddAccess = "/admin/Publication/AddAccess";
    this.ajaxRemoveAccess = "/admin/Publication/RemoveAccess";

    this.ajaxRedirects = "/admin/Publication/Redirects";
    this.ajaxAddRedirect = "/admin/Publication/AddRedirect";
    this.ajaxRemoveRedirect = "/admin/RecordRedirect/Remove";

    this.selectedID = null;
    this.init = function () {
        $(".access").click(function () {
            _this.showAccessDialog($(this).data("id"));
            return false;
        });

        $(".redirect").click(function () {
            _this.showRedirectDialog($(this).data("id"));
            return false;
        });
    }

    this.showAccessDialog = function (id) {
        _this.selectedID = id;
        $.ajax({
            type: "GET",
            url: _this.ajaxAccess,
            data: { id: id },
            success: function (data) {
                $("#accessWrapper").modal();
                $("#accessWrapper").html(data);
                selectUser.initAccessName();
                _this.initAccess();
            }
        });
    };

    this.initAccess = function () {
        $("#AddAccess").click(function () {
            var ajaxData =
            {
                PublicationID: _this.selectedID,
                UserID: $("#AccessID").val()
            };

            $.ajax({
                type: "POST",
                url: _this.ajaxAddAccess,
                data: ajaxData,
                success: function (data) {
                    if (data.result == "ok") {
                        _this.showAccessDialog(_this.selectedID);
                    }
                }
            });
            return false;
        });

        $(".remove-access").click(function () {
            var id = $(this).data("id");
            var item = $(this).closest(".access-line");
            $.ajax({
                type: "POST",
                url: _this.ajaxRemoveAccess,
                data: { id: id },
                success: function (data) {
                    if (data.result == "ok") {
                        item.remove();
                    }
                }
            });
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

        $("#PublicationID").val(_this.selectedID);
    }
}

var indexPublication = null;

$().ready(function () {
    indexPublication = new IndexPublication();
    indexPublication.init();
});

