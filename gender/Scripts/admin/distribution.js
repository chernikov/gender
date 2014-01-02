function Distribution() {

    var _this = this;

    this.init = function () {
    };
}

var distribution = null;
$().ready(function () {
    distribution = new Distribution();
    distribution.init();
});