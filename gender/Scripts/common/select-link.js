function SelectLink()
{
    var _this = this;

    this.prefix = "";
    this.type = "title";
    this.init = function ()
    {
        $("#AddLink").click(function () {
            $.ajax({
                type: "GET",
                url: "/Link/AddLink",
                success: function (data) {
                    $("#LinkListWrapper").append(data);
                }
            })
        });

        $(document).on("click", ".remove-line-link", function () {
            $(this).closest(".LinkWrapper").remove();
        });

        $(document).on("blur", ".linkUrl", function () {
            if (_this.type == "title") {
                _this.processUrl($(this));
            } else {
                _this.processSimpleUrl($(this));
            }
        });

        if ($("#AddLinkShop").length > 0) {
            $("#AddLinkShop").click(function () {
                $.ajax({
                    type: "GET",
                    url: "/Link/AddLink?type=2",
                    success: function (data) {
                        $("#LinkShopListWrapper").append(data);
                    }
                })
            });
        }
    }

    this.processUrl = function (item)
    {
        var url = $("input", item).val();
        var row = item.closest(".row");
        if (url.indexOf("http") != 0) {
            url = "http://" + url;
            $("input", item).val(url);
        }
        $.ajax({
            type: "GET",
            url: "/Link/ProcessUrl",
            data: { url: url },
            beforeSend: function () {
                $(".icon img", row).addClass("hidden");
                $(".title", row).text("");
            },
            success: function (data) {
                if (data.result == "ok") {
                    $(".icon img", row).removeClass("hidden");
                    $(".icon img", row).attr("src", data.data.filePath);
                    var link = $("<a href='" + url + "' target='_blank'>" + data.data.title + "</a>");
                    $(".title", row).html(link);
                    $(".iconHidden", row).val(data.data.filePath);
                    $(".titleHidden", row).val(data.data.title);
                } else {
                    $(".icon img", row).addClass("hidden");
                    $(".title", row).text("");
                    $(".iconHidden", row).val("");
                    $(".titleHidden", row).val("");
                }
            }
        });
    }

    this.processSimpleUrl = function (item) {
        var url = $("input", item).val();
        var row = item.closest(".row");
        if (url.indexOf("http") != 0) {
            url = "http://" + url;
            $("input", item).val(url);
        }
        $.ajax({
            type: "GET",
            url: "/Link/ProcessSimpleUrl",
            data: { url: url },
            beforeSend: function () {
                $(".icon img", row).addClass("hidden");
                $(".title", row).text("");
            },
            success: function (data) {
                if (data.result == "ok")
                {
                    $(".icon img", row).removeClass("hidden");
                    $(".icon img", row).attr("src", data.data.filePath);
                    var link = $("<a href='" + url + "' target='_blank'>" + _this.prefix + " " + data.data.result + "</a>");
                    $(".title", row).html(link);
                    $(".iconHidden", row).val(data.data.filePath);
                    $(".titleHidden", row).val(_this.prefix + " " + data.data.result);
                } else {
                    $(".icon img", row).addClass("hidden");
                    $(".title", row).text("");
                    $(".iconHidden", row).val("");
                    $(".titleHidden", row).val("");
                }
            }
        });
    }


}

var selectLink = null;

$().ready(function ()
{
    selectLink = new SelectLink();
    selectLink.init();
});