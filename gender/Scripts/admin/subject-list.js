function SubjectList()
{
    var _this = this;
    
    this.AjaxGetSubjectUrl = "/admin/Subject/SubSubject";
    this.AjaxUpdateSubjectOrderUrl = "/admin/Subject/AjaxSubjectOrder";
    this.AjaxUpdateSubjectMoveUrl = "/admin/Subject/AjaxSubjectMove";


    this.init = function () {
        _this.initSortable($(".subjectList"));

        _this.initDraggableDroppable($(".subjectItem > .move"), $(".subjectItem"));

        $(document).on("click", ".open", null, function () {
            _this.getSubject($(this));
        });
    }

    this.initSortable = function (item) {
        item.sortable({
            placeholder: 'subject-placeholder ui-state-highlight',
            stop: function (event, ui) {
                var sortingInfo = [];
                var isNeedUpdate = false;
                $("> .subjectItem", $(this)).each(function () {
                    sortingInfo.push($(this).data("id"));
                });

                var itemId = ui.item.data("id");

                var ajaxData = null;
                for (var i = sortingInfo.length; i--;)
                {
                    if (sortingInfo[i] != itemId)
                    {
                        continue;
                    }
                    ajaxData = { id: itemId, replaceTo: i + 1 };
                    isNeedUpdate = true;
                    break;
                }

                if (!isNeedUpdate) {
                    return;
                }

                $.ajax({
                    type: "POST",
                    url: _this.AjaxUpdateSubjectOrderUrl,
                    data: ajaxData,
                    success: function (data) {
                        if (data.result == "error") {
                            $(this).sortable('cancel');
                            alert(data.errors);
                        }
                    },
                    error: function () {
                        $(this).sortable('cancel');
                        alert("Внутренняя ошибка");
                    }
                });
            }
        });
    }

    this.initDraggableDroppable = function (draggable, droppable) {
        draggable.draggable({
            helper: function (event) {
                return $("<div class='ui-widget-move'>Перемещение</div>");
            },
            start: function (event, ui) {
                $(".subjectItem").addClass("highlight");
            },
            stop: function (event, ui) {
                $(".subjectItem").removeClass("highlight");
            }
        });

        droppable.droppable({
            accept: '.move',
            greedy: true,
            drop: function (event, ui) {
                var id = ui.draggable.parent().data("id");
                var moveTo = $(this).data("id");

                var ajaxData = { id: id, moveTo: moveTo };
                if (id == moveTo) {
                    return false;
                }
                $.ajax({
                    type: "POST",
                    url: _this.AjaxUpdateSubjectMoveUrl,
                    data: ajaxData,
                    success: function (data) {
                        if (data.result == "ok") {
                            window.location.reload();
                        }
                        if (data.result == "error") {
                            $(this).droppable('cancel');
                            alert(data.errors);
                        }
                    },
                    error: function () {
                        $(this).droppable('cancel');
                        alert("Внутренняя ошибка");
                    }
                });
            }
        });

        

    }

    this.getSubject = function (item) {
        var id = item.closest(".subjectItem").data("id");

        var wrapper = $(".sub-wrapper", item.closest(".subjectItem"));
        if ($(".sub-subject", wrapper).length > 0) {
            wrapper.toggle();
        } else {
            $.ajax({
                type: "GET",
                url: _this.AjaxGetSubjectUrl,
                data: { id: id },
                success: function (data) {
                    wrapper.html(data);
                    _this.initSortable($(".subjectList", wrapper));
                    _this.initDraggableDroppable($(".subjectItem > .move", wrapper), $(".subjectItem", wrapper));
                }
            });
        }
    }
}

var subjectList = null;
$().ready(function () {
    subjectList = new SubjectList();
    subjectList.init();
});