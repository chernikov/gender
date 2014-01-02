function SlideSubject()
{
    var _this = this;
    

    this.init = function ()
    {
        $(document).on("click", "#NextSubject", null, function () {
            var type = $("#AlsoSubjectWrapper").data("type");
            var id = $("#AlsoSubjectWrapper").data("id");
            var subjectID = $(this).data("id");
            $.ajax({
                type: "GET",
                url: "/" + type + "/AlsoSubject",
                data: { id: id, idSubject: subjectID },
                success: function (data) {
                    $("#AlsoSubjectWrapper").html(data);
                }
            });
        });
    
    }
}


var slideSubject = null;

$().ready(function () {
    slideSubject = new SlideSubject();
    slideSubject.init();
});
