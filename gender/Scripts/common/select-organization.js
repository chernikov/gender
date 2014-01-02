function SelectOrganization() {
    _this = this;
    this.ajaxSelectOrganization = "/Select/SelectOrganization";
    this.ajaxAddOrganization = "/Organization/Add";
    this.init = function () {
        $("#OrganizationList").ajaxChosen({
            method: 'GET',
            url: _this.ajaxSelectOrganization,
            dataType: 'json',
            minTermLength: 2,
            afterTypeDelay: 300,
            keepTypingMsg: "Продолжайте печатать...",
            lookingForMsg: "Не найдено (Enter чтобы добавить)...",
            addCallback: function (data)
            {
                var resultAjax = null;
                $.ajax({
                    type: "POST",
                    url: _this.ajaxAddOrganization,
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

        //var wrapper = $("#OrganizationList").closest(".controls");

        //$(".search-field input", wrapper).keyup(function (e) {
        //    var code = (e.keyCode ? e.keyCode : e.which);
        //    if (code == 13) {
        //        if (confirm("Добавить организацию?")) {
        //            alert("Добавляю " + $(this).val());
        //            /*$("#OrganizationList").chosen().change();*/
        //        };
        //        return false;
        //    }
        //});
    }
}

var selectOrganization = null;

$().ready(function () {
    selectOrganization = new SelectOrganization();
    selectOrganization.init();
});