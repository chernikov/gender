function EditDocument() {
    var _this = this;

    this.ajaxAddEvent = "/admin/Event/Add";

    this.init = function () {
        $("#EventHeader").blur(function () {
            if ($("#EventHeader").val() == "") {
                $("#EventID").val("");
            }
        });

        $("#EventHeader").autocomplete({
            source: function (request, response)
            {
                $.ajax({
                    url: "/admin/Event/SelectEvent",
                    data: {
                        term: request.term
                    },
                    success: function (data) {
                        if (data.data.length) {
                            
                            response($.map(data.data, function (item) {
                                return {
                                    label: item.name,
                                    value: item.name,
                                    id: item.id
                                }
                            }));
                        } else {
                            var result = [];
                            result.push({
                                label: "Добавить : " + data.term,
                                value: data.term,
                                id: 0
                            });
                            response(result);
                        }
                    }
                });
            },
            minLength: 2,
            select: function (event, ui) {
                if (ui.item.id > 0) {
                    $("#EventHeader").val(ui.item.label);
                    $("#EventID").val(ui.item.id);
                } else {
                    $.ajax({
                        type: "POST",
                        url: _this.ajaxAddEvent,
                        async: false,
                        data: { name: ui.item.value },
                        success: function (result) {
                            resultAjax = result;
                        }
                    });
                    if (resultAjax.result == "ok") {
                        $("#EventHeader").val(resultAjax.data.name);
                        $("#EventID").val(resultAjax.data.id);
                    }
                }
                return false;
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
var editDocument = null;

$().ready(function () {
    editDocument = new EditDocument();
    editDocument.init();
});

