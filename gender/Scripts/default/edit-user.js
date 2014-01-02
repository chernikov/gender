function EditUser() {
    var _this = this;

    this.ajaxUploadFile = "/Home/UploadFile";

    this.init = function () {
        InitUpload(
            $("#ChangeImage"),
            false,
            _this.ajaxUploadFile,
            function (event, id, name, responseJSON) {
                if (responseJSON.success) {
                    $("#PreviewImage").attr("src", responseJSON.fileUrl + "?width=200");
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

        $(document).on("click", "#ChangePasswordBtn", null, function () {
            _this.changePassword();
        });

        $("#SendActivationBtn").click(function () {
            _this.sendActivation();
        });

        if (window.location.hash.length > 0)
        {
            //alert(window.location.hash);

            $(window.location.hash).collapse('toggle');
        }
    }

    this.changePassword = function ()
    {
        var ajaxData = {
            ID: $("#ChangePasswordID").val(),
            HasEmail: $("#HasEmail").val(),
            Password: $("#Password").val(),
            NewPassword: $("#NewPassword").val(),
            ConfirmPassword: $("#ConfirmPassword").val()
        };

        $.ajax({
            type: "POST",
            url: "/User/ChangePassword",
            data: ajaxData,
            success: function (data) {
                $("#ChangePasswordWrapper").html(data);
            }
        });
    }

    this.sendActivation = function () {
        var ajaxData = {
            id: $("#ID").val(),
        };
        $.ajax({
            type: "POST",
            url: "/User/SendActivation",
            data: ajaxData,
            success: function (data)
            {
                if (data.result == "ok")
                {
                    $("#SendActivationBtn").hide();
                }
            }
        });
    }
}

var editUser = null;
$().ready(function () {
    editUser = new EditUser();
    editUser.init();
});

