function SelectEvent()
{
    _this = this;
    this.ajaxSelectEvent = "/Select/SelectEvent";
    this.ajaxAddEvent = "/Event/Add";

    this.init = function ()
    {
        $("#EventList").each(function (i, item) {
            $(this).ajaxChosen({
                method: 'GET',
                url: _this.ajaxSelectEvent,
                dataType: 'json',
                minTermLength: 2,
                afterTypeDelay: 300,
                keepTypingMsg: "Продолжайте печатать...",
                lookingForMsg: "Не найдено (Enter чтобы добавить)...",
                addCallback: function (data) {
                    var resultAjax = null;
                    $.ajax({
                        type: "POST",
                        url: _this.ajaxAddEvent,
                        async: false,
                        data: { name: data },
                        success: function (result) {
                            resultAjax = result;
                        }
                    });

                    if (resultAjax.result == "ok") {
                        return {
                            value: resultAjax.data.id,
                            text: resultAjax.data.name
                        };
                    }
                    return null;
                }
            }, function (data) {
                var terms = {};
                if (data.result == "ok") {
                    $.each(data.data, function (i, val) {
                        terms[val.id] = val.name;
                    });
                }
                return terms;
            });
        });
    }
}

var selectEvent = null;
$().ready(function () {
    selectEvent = new SelectEvent();
    selectEvent.init();
});