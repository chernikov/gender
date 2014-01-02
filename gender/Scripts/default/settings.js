function Settings()
{
    var _this = this;
    this.init = function ()
    {
        $("#EditNotifyBtn").click(function ()
        {
            $.ajax({
                type: "GET",
                url: "/User/EditUserNotify",
                data: {
                    ID: $("#ID").val(),
                    NoticeUpdatePeriod: $("#NoticeUpdatePeriod").val(),
                    NoticeCommentPeriod : $("#NoticeCommentPeriod").val()
                },
                success: function (data) {
                    if (data.result == "ok")
                    {
                        $("#messageNotifyWrapper").html("<div class=\"alert alert-info\"><button type=\"button\" class=\"close\" data-dismiss=\"alert\">&times;</button>" + data.message + "</div>");
                    }
                    if (data.result == "error")
                    {
                        $("#messageNotifyWrapper").html("<div class=\"alert alert-error\"><button type=\"button\" class=\"close\" data-dismiss=\"alert\">&times;</button>" + data.message + "</div>");
                    }
                }
            });

            return false;
        });

        $("#SubjectSubscriptionBtn").click(function () {
            var formData = "ID=" + $("#ID").val() + "&"+ $("#SubjectSubscriptionForm").serialize();
            $.ajax({
                type: "GET",
                traditional : true,
                url: "/User/EditSubjectSubscription",
                data: formData,
                success: function (data) {
                    if (data.result == "ok") {
                        $("#messageSubjectSubscriptionWrapper").html("<div class=\"alert alert-info\"><button type=\"button\" class=\"close\" data-dismiss=\"alert\">&times;</button>" + data.message + "</div>");
                    }
                    if (data.result == "error") {
                        $("#messageSubjectSubscriptionWrapper").html("<div class=\"alert alert-error\"><button type=\"button\" class=\"close\" data-dismiss=\"alert\">&times;</button>" + data.message + "</div>");
                    }
                }
            });

            return false;
        });

        $("#CommentSubscriptionBtn").click(function () {
            var formData = "ID=" + $("#ID").val() + "&" + $("#CommentSubscriptionForm").serialize();
            $.ajax({
                type: "GET",
                traditional: true,
                url: "/User/EditCommentSubscription",
                data: formData,
                success: function (data) {
                    if (data.result == "ok")
                    {
                        $.ajax({
                            type: "GET",
                            url: "/User/CommentSubscription",
                            data: { id: $("#ID").val() },
                            success: function (data) {
                                $("#CommentSubscriptionWrapper").html(data);
                            }
                        });
                    }
                   
                }
            });

            return false;
        });
    };
}

var settings = null;
$().ready(function ()
{
    settings = new Settings();
    settings.init();
});