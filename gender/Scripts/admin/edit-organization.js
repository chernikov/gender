function EditOrganization() {
    _this = this;
    this.ajaxUploadFile = "/admin/Home/UploadFile";

    this.init = function () {
        InitUpload(
            $("#ChangeImage"),
            false,
            _this.ajaxUploadFile,
            function (event, id, name, responseJSON) {
                if (responseJSON.success) {
                    $("#PreviewImage").attr("src", responseJSON.fileUrl + "?width=200");
                    $("#PreviewImage").removeClass("hidden");
                    $("#Logo").val(responseJSON.fileUrl);
                }
            },
            [],
            $("#ChangeImage").text()
            );

        $("#DeleteImage").click(function () {
            $("#PreviewImage").addClass("hidden");
            $("#Logo").val("");
        });
    }
}

var editOrganization = null;
$().ready(function () {
    editOrganization = new EditOrganization();
    editOrganization.init();
});

