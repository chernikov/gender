function SelectSubject()
{
    _this = this;
    this.ajaxSelectSubject = "/Select/SelectSubject";

    this.init = function ()
    {
        $("#SubjectList").each(function (i, item) {
            $(this).ajaxChosen({
                method: 'GET',
                url: _this.ajaxSelectSubject,
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

var selectSubject = null;

$().ready(function () {
    selectSubject = new SelectSubject();
    selectSubject.init();
});