function UploadAttach()
{
    var _this = this;

    this.ajaxUploadAttach = "/File/Upload";
    this.ajaxFileItem = "/File/Item";

    this.init = function ()
    {
        InitUpload(
               $("#UploadAttach"),
               true,
               _this.ajaxUploadAttach,
               function (event, id, name, responseJSON)
               {
                   if (responseJSON.success) {
                       _this.createFile(responseJSON);
                   } else {
                       alert(responseJSON.error);
                   }
                   $(".qq-upload-success").remove();
               },
               [],
               $("#UploadAttach").text());

        $(document).on("click", ".remove-file", function () {
            $(this).closest(".span2").remove();
        });
    }

    this.createFile = function (responseJSON)
    {
        var id = responseJSON.file.ID;
        $.ajax({
            type: "GET",
            url: _this.ajaxFileItem,
            data : {id : id},
            success: function (data) {
                $("#FilesListWrapper").append(data);
            }
        });
    }
}

var uploadAttach = null;
$().ready(function ()
{
    uploadAttach = new UploadAttach();
    uploadAttach.init();
});