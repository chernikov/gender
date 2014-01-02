function EditUser()
{
    var _this = this;

    this.ajaxUploadFile = "/admin/Home/UploadFile";

    this.init = function ()
    {
        InitUpload(
            $("#ChangeImage"),
            false,
            _this.ajaxUploadFile,
            function (event, id, name, responseJSON) {
                if (responseJSON.success) {
                    $("#PreviewImage").attr("src", responseJSON.fileUrl + "?width=100");
                    $("#PreviewImage").removeClass("hidden");
                    $("#DeleteImage").removeClass("hidden");
                    $("#Photo").val(responseJSON.fileUrl);
                }
            },
            [],
            $("#ChangeImage").text()
            );

        $("#DeleteImage").click(function () {
            $("#PreviewImage").addClass("hidden");
            $("#DeleteImage").addClass("hidden");
            $("#Photo").val("");
        });
    }
}

var editUser = null;
$().ready(function () {
    editUser = new EditUser();
    editUser.init();
});

