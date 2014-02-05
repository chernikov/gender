function BlogPostTags() {
    var _this = this;

    this.init = function ()
    {
        $(".SubjectList").each(function (i, item) {
            $(this).ajaxChosen({
                method: 'GET',
                url: "/Select/SelectSubject",
                dataType: 'json',
                minTermLength: 2,
                afterTypeDelay: 300,
                keepTypingMsg: "Продолжайте печатать...",
                lookingForMsg: "Ищу...",
            }, function (data) {
                var terms = {};
                if (data.result == "ok") {
                    $.each(data.data, function (i, val) {
                        terms[val.id] = val.name;
                    });
                }
                return terms;
            });
        });

        $(".RegionList").each(function (i, item) {
            $(this).ajaxChosen({
                method: 'GET',
                url: "/Select/SelectRegion",
                dataType: 'json',
                minTermLength: 2,
                afterTypeDelay: 300,
                keepTypingMsg: "Продолжайте печатать...",
                lookingForMsg: "Ищу...",
            }, function (data) {
                var terms = {};
                if (data.result == "ok") {
                    $.each(data.data, function (i, val) {
                        terms[val.id] = val.name;
                    });
                }
                return terms;
            });
        });

        $(".SubmitBlogTagForm").click(function () {

            var button = $(this);
            var form = $(this).closest("form");
            $.ajax({
                type: "POST",
                url: "/admin/Blog/BlogTags",
                data: form.serialize(),
                beforeSent : function() {
                    button.attr("disabled", true);
                },
                success: function (data)
                {
                    if (data.result == "ok") {
                        button.removeAttr("disabled");
                        var obj = $("<span class='label label-info'>Сохранено</span>");
                        button.after(obj);
                        obj.fadeOut(1000);

                    }
                }
            });

            return false;
        })
    }
}

var blogPostTags = null;
$().ready(function () {
    blogPostTags = new BlogPostTags();
    blogPostTags.init();
});

