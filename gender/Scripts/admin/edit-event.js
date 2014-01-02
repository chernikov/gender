function EditEvent() {
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
                    $("#Image").val(responseJSON.fileUrl);
                }
            },
            [],
            $("#ChangeImage").text()
            );

        $("#DeleteImage").click(function () {
            $("#PreviewImage").addClass("hidden");
            $("#Image").val("");
        });
    }
}
var editEvent = null;

$().ready(function () {
    editEvent = new EditEvent();
    editEvent.init();
});

