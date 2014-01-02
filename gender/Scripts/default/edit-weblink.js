function EditWebLink() {
    var _this = this;

    this.ajaxUploadFile = "/admin/Home/UploadFile";
    this.ajaxWebLinkGetScreenshot = "/admin/WebLink/GetScreenshot";

    this.init = function () {

        $("#Url").blur(function () {
            if ($("#Screenshot").val() == "")
            {
                _this.getScreenshot($(this).val());
            }
        });

        InitUpload(
            $("#ChangeScreenshot"),
            false,
            _this.ajaxUploadFile,
            function (event, id, name, responseJSON) {
                if (responseJSON.success) {
                    $("#PreviewScreenshot").attr("src", responseJSON.fileUrl + "?width=600");
                    $("#PreviewScreenshot").removeClass("hidden");
                    $("#Screenshot").val(responseJSON.fileUrl);
                }
            },
            [],
            $("#ChangeScreenshot").text()
            );

        $("#DeleteScreenshot").click(function () {
            $("#PreviewScreenshot").addClass("hidden");
            $("#Screenshot").val("");
        });
    }

    this.getScreenshot = function (url)
    {
        $.ajax({
            type: "GET",
            url: _this.ajaxWebLinkGetScreenshot,
            data: { url: url },
            success: function (data)
            {
                if (data.result == "ok")
                {
                    $("#PreviewScreenshot").attr("src", data.path + "?width=600");
                    $("#Screenshot").val(data.path);
                    $("#PreviewScreenshot").removeClass("hidden");
                }
            }
        });
    }
}

var editWebLink = null;
$().ready(function () {
    editWebLink = new EditWebLink();
    editWebLink.init();
});

