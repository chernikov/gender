function SelectUserEmail()
{
    var _this = this;

    this.init = function ()
    {
        $("#AddUserEmail").click(function () {
            $.ajax({
                type: "GET",
                url: "/UserEmail/AddUserEmail",
                success: function (data) {
                    $("#UserEmailListWrapper").append(data);
                }
            })
        });

        $(document).on("click", ".remove-line-user-email", function () {
            $(this).closest(".UserEmailWrapper").remove();
        });

        $(document).on("click", ".primary-email", function () {
            $("input", $(".primary-email")).removeAttr("checked");
            $("input", $(this)).attr("checked", "checked");
        });
    }
}

var selectUserEmail = null;
$().ready(function () {
    selectUserEmail = new SelectUserEmail();
    selectUserEmail.init();
});