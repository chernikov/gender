function SelectRegion()
{
    _this = this;
    this.ajaxSelectRegion = "/Select/SelectRegion";

    this.init = function ()
    {
        $("#RegionList").each(function (i, item) {
            $(this).ajaxChosen({
                method: 'GET',
                url: _this.ajaxSelectRegion,
                dataType: 'json',
                minTermLength: 2,
                afterTypeDelay: 300,
                keepTypingMsg: "Продолжайте печатать...",
                lookingForMsg: "Ищу...",
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

var selectRegion = null;

$().ready(function () {
    selectRegion = new SelectRegion();
    selectRegion.init();
});