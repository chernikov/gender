function Comment()
{
    var _this = this;

    this.init = function ()
    {
        $("#CreateCommentBtn").click(function () {
            _this.createComment();
            return false;
        });

        $("#AddComment").click(function () {
            $("#ParentID").val("");
            $("#HeaderLegend").text("Добавить комментарий");
        });

        $(".reply-comment").click(function () {
            $("#ParentID").val($(this).data("id"));
            var text = $(".comment-content", $(this).parent()).text();
            $("#HeaderLegend").html("Ответить на комментарий: <small>" + text + "</small>");
        });

        $("#Text").focus(function () {
            $("#Text").closest(".control-group").removeClass("error");
        });

        $(".remove-comment").click(function () {
            if (confirm("Удалить этот комментарий?"))
            {
                $.ajax({
                    type : "POST",
                    url: "/Comment/RemoveComment",
                    data: {
                        id: $(this).data("id")
                    },
                    success: function (data) {
                        if (data.result == "ok") {
                            window.location.reload();
                        }
                    }
                });
            }
        });

        $("#SubscribeCommentBtn").click(function () {
            
            _this.toggleSubscription($(this));
        });
    }

    this.createComment = function ()
    {
        var ajaxData = $("#form-add-comment").serialize();
        $.ajax({
            type: "POST",
            url: _this.ajaxComment,
            data: ajaxData,
            beforeSend: function () {
                $("#Text").closest(".control-group").removeClass("error");
            },
            success: function (data)
            {
                if (data.result == "ok") {
                    window.location.reload();
                } else {
                    $("#Text").closest(".control-group").addClass("error");
                }
            },
            error: function () {
                $("#Text").closest(".control-group").addClass("error");
            }
        });
    }


    this.toggleSubscription = function (item)
    {
        var id = item.data("id");
        var type = item.data("type");
        $.ajax({
            type: "GET",
            url: "/Comment/ToggleSubscription" + type + "/",
            data: { id: id },
            success: function (data) {
                if (data.result == "ok") {
                    if (item.hasClass("active"))
                    {
                        item.removeClass("active");
                        item.text("Подписаться на комментарии");
                    } else {
                        item.addClass("active");
                        item.text("Отписаться от комментариев");
                    }
                }
            }
        });
    }
}

var comment = null;
$().ready(function () {
    comment = new Comment();
    comment.init();
});