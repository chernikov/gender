function RegionList()
{
    var _this = this;
    
    this.AjaxGetRegionUrl = "/admin/Region/SubRegion";
    this.AjaxUpdateRegionOrderUrl = "/admin/Region/AjaxRegionOrder";
    this.AjaxUpdateRegionMoveUrl = "/admin/Region/AjaxRegionMove";


    this.init = function () {
        _this.initSortable($(".regionList"));

        _this.initDraggableDroppable($(".regionItem > .move"), $(".regionItem"));

        $(document).on("click", ".open", null, function () {
            _this.getRegion($(this));
        });
    }

    this.initSortable = function (item) {
        item.sortable({
            placeholder: 'region-placeholder ui-state-highlight',
            stop: function (event, ui) {
                var sortingInfo = [];
                var isNeedUpdate = false;
                $("> .regionItem", $(this)).each(function () {
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
                    url: _this.AjaxUpdateRegionOrderUrl,
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
                $(".regionItem").addClass("highlight");
            },
            stop: function (event, ui) {
                $(".regionItem").removeClass("highlight");
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
                    url: _this.AjaxUpdateRegionMoveUrl,
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

    this.getRegion = function (item) {
        var id = item.closest(".regionItem").data("id");

        var wrapper = $(".sub-wrapper", item.closest(".regionItem"));
        if ($(".sub-region", wrapper).length > 0) {
            wrapper.toggle();
        } else {
            $.ajax({
                type: "GET",
                url: _this.AjaxGetRegionUrl,
                data: { id: id },
                success: function (data) {
                    wrapper.html(data);
                    _this.initSortable($(".regionList", wrapper));
                    _this.initDraggableDroppable($(".regionItem > .move", wrapper), $(".regionItem", wrapper));
                }
            });
        }
    }
}

var regionList = null;
$().ready(function () {
    regionList = new RegionList();
    regionList.init();
});