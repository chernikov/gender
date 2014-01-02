function SubjectSubscription()
{
    var _this = this;

    this.init = function ()
    {
        if ($("#SubscribeBtn").length > 0) {
            $("#SubscribeBtn").click(function () {
                
                var id = $(this).data("id");

                var item = $(this);
                
                $.ajax({
                    type: "POST",
                    url: "/subject/ToggleSubscription",
                    data: { id: id },
                    success: function (data)
                    {
                        if (data.result == "ok") {
                            if (item.hasClass("active"))
                            {
                                item.removeClass("active");
                                item.text("Подписаться на обновления по этой теме");
                            } else {
                                item.addClass("active");
                                item.text("Отписаться от обновлений по этой теме");
                            }
                        }
                    }
                });
            });
        }
    }
}


var subjectSubscription = null;

$().ready(function () {
    subjectSubscription = new SubjectSubscription();
    subjectSubscription.init();

});