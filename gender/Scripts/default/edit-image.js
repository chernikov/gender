function EditImage() {
    _this = this;
    this.ajaxUploadFile = "/admin/Home/UploadFile";

    this.init = function () {
        InitUpload(
            $("#ChangeImage"),
            false,
            _this.ajaxUploadFile,
            function (event, id, name, responseJSON) {
                if (responseJSON.success) {
                    $("#PreviewImage").attr("src", responseJSON.fileUrl + "?width=600");
                    $("#PreviewImage").removeClass("hidden");
                    $("#Path").val(responseJSON.fileUrl);
                }
            },
            [],
            $("#ChangeImage").text()
            );

        $("#DeleteImage").click(function () {
            $("#PreviewImage").addClass("hidden");
            $("#Path").val("");
        });


        $("#SourceLink").blur(function () {
            if ($(this).val().indexOf("http") != 0) {
                $(this).val() = "http://" + $(this).val();
            }
        });
    }
}


var editImage = null;
$().ready(function () {
    editImage = new EditImage();
    editImage.init();
});

