function ImportBlog()
{
    var _this = this;

    this.init = function ()
    {
        $("#ImportPostBtn").click(function ()
        {
            if ($("#ImportBlogUrls").val() != "")
            {
                $.ajax({
                    type: "POST",
                    url: "/user/ImportPosts",
                    data: { urls: $("#ImportBlogUrls").val() },
                    success: function (data) {
                        if (data.result == "ok") {

                            var ul = $("<ol>").addClass("edit-blog");
                            $.each(data.data, function(i, item) 
                            {
                                var li = $("<li>").html(item);
                                ul.append(li);
                            });
                            $("#ModalBody").html(ul);
                            $("#importResultWrapper").modal();
                        }
                    }
                });
            }
            return false;
        });

        $("#ParseBlogBtn").click(function ()
        {
            if ($("#ParseBlog").val() != "") {
                $.ajax({
                    type: "POST",
                    url: "/user/AddParser",
                    data: { url: $("#ParseBlog").val() },
                    success: function (data) {
                        if (data.result == "ok")
                        {
                            $.ajax({
                                url: "/user/Parsers",
                                success: function (content)
                                {
                                    $("#ImportedParsersWrapper").html(content);
                                }
                            });
                            $("#ModalBody").html(data.data);
                            $("#importResultWrapper").modal();
                        }
                    }
                });
            }
            return false;
        });


    }
}

var importBlog = null;

$().ready(function () {
    importBlog = new ImportBlog();
    importBlog.init();
});