function RegionEdit() {
    var _this = this;

    this.init = function() 
    {
    }
}

var regionEdit = null;
$().ready(function () {
    regionEdit = new RegionEdit();
    regionEdit.init();
});