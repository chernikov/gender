function EditPublication() {
    _this = this;

    this.init = function ()
    {
        InitUpload(
            $("#ChangeImage"),
            false,
            "/admin/Home/UploadFile",
            function (event, id, name, responseJSON) {
                if (responseJSON.success) {
                    $("#PreviewImage").attr("src", responseJSON.fileUrl + "?width=200");
                    $("#PreviewImage").removeClass("hidden");
                    $("#Cover").val(responseJSON.fileUrl);
                }
            },
            [],
            $("#ChangeImage").text());

        $("#DeleteImage").click(function ()
        {
            $("#PreviewImage").addClass("hidden");
            $("#Cover").val("");
        });

        $("#ParentName").blur(function () {
            if ($("#ParentName").val() == "")
            {
                $("#ParentID").val("");
            }
        });

        $("#ParentName").autocomplete({
            source: function (request, response) {
                
                $.ajax({
                    url: "/Select/SelectPublication",
                    data: {
                        term : request.term
                    },
                    success: function (data) {
                        response($.map(data.data, function (item) {
                            return {
                                label: item.name,
                                value: item.name,
                                id: item.id
                            }
                        }));
                    }
                });
            },
            minLength: 2,
            select: function (event, ui) {
                $("#ParentName").val(ui.item.label);
                $("#ParentID").val(ui.item.id);
            },
            open: function () {
                $(this).removeClass("ui-corner-all").addClass("ui-corner-top");
            },
            close: function () {
                $(this).removeClass("ui-corner-top").addClass("ui-corner-all");
            }
        });
    }
}

var editPublication = null;
$().ready(function () {
    editPublication = new EditPublication();
    editPublication.init();
});

