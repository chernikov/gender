function SelectUser() {
    _this = this;

    this.init = function () {
       
    }

    this.initAccessName = function ()
    {
        $("#AccessName").autocomplete({
            source: function (request, response) {
                $.ajax({
                    url: "/Select/SelectUser",
                    data: {
                        term: request.term
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
                $("#AccessName").val(ui.item.label);
                $("#AccessID").val(ui.item.id);
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

var selectUser = null;
$().ready(function () {
    selectUser = new SelectUser();
    selectUser.init();
});