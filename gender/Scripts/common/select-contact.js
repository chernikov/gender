function SelectContact()
{
    var _this = this;

    this.init = function ()
    {
        $("#AddContact").click(function () {
            $.ajax({
                type: "GET",
                url: "/Contact/AddContact",
                success: function (data) {
                    $("#ContactListWrapper").append(data);
                }
            })
        });

        $(document).on("click", ".remove-line-contact", function () {
            $(this).closest(".ContactWrapper").remove();
        });

    }
}

var selectContact = null;

$().ready(function () {
    selectContact = new SelectContact();
    selectContact.init();
});