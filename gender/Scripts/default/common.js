
function LoadCssDynamically(fileName) {
    var fileref = $('<link>');
    fileref.attr("rel", "stylesheet");
    fileref.attr("type", "text/css");
    fileref.attr("href", fileName);
    $("head").append(fileref);
}

function LoadJsDynamically(fileName) {
    var fileref = $('<script>');
    fileref.attr("type", "text/javascript");
    fileref.attr("src", fileName);

    $("head").append(fileref);
}


function InitUpload(item, multiple, url, oncomplete, extensions, title) {
    if (extensions == null) {
        extensions = [];
    }
    if (typeof (qq) == 'undefined') {
        LoadCssDynamically("/Content/css/fineuploader-3.5.0.css");
        LoadJsDynamically("/Scripts/jquery.fineuploader-3.5.0.js");
    }

    var uploader = new qq.FineUploaderBasic({
        button: item[0],
        request:
        {
            endpoint: url
        },
        autoUpload: true,
        multiple: multiple,
        text: {
            uploadButton: title
        },
        sizeLimit: 4000000000,
        allowedExtensions: extensions,
        failedUploadTextDisplay: {
            mode: 'custom',
            maxChars: 400,
            responseProperty: 'error',
            enableTooltip: true
        },
        callbacks: {
            onComplete: function (id, fileName, responseJSON) {
                $.unblockUI();
                oncomplete("event", id, fileName, responseJSON);
            },
            onUpload: function () {
                $.blockUI({ message: '<h1>Подождите, пожалуйста...</h1>' });
            },
            onError: function () {
                $.unblockUI();
            }
        }
    });
}

function Common() {
    var _this = this;

    this.init = function () {
        $('.dropdown-toggle').dropdown();

        $(document).on("click", ".stop-action", null, function () {
            return confirm("Вы действительно хотите это сделать?");
        });

        $(document).on("click", ".delete-action, .btn-danger", null, function () {
            if ($(this).hasClass("no-submit")) {
                return true;
            }
            return confirm("Вы действительно хотите удалить?");
        });

        $('.datetimepicker').datepicker({
            weekStart: 1,
            format: "dd.mm.yyyy",
            startDate: new Date(1753, 0, 2, 0, 0),
        });
    };
}

var common = null;
$().ready(function () {
    common = new Common();
    common.init();
});