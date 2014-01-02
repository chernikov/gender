function Like()
{
    var _this = this;

    this.init = function ()
    {
        _this.initComment();
        _this.initDocument();
        _this.initEvent();
        _this.initImage();
        _this.initOrganization();
        _this.initPublication();
        _this.initStudyMaterial();
        _this.initWebLink();
        _this.initBlogPostShort();
        _this.initBlogPost();
    }

    this.initComment = function ()
    {
        $(document).on("click", ".comment-like .plus.icon-plus", null, function () {
            var id = $(this).closest(".comment-like").data("id");
            _this.LikeComment(id, $(this), true);
        });
        
        $(document).on("click", ".comment-like .minus.icon-minus", null, function () {
            var id = $(this).closest(".comment-like").data("id");
            _this.LikeComment(id, $(this), false);
        });
    }

    this.LikeComment = function (id, item, value)
    {
        var wrapper = item.closest(".comment-like-wrapper");

        $.ajax({
            type: "POST",
            url: "/Like/CommentLike",
            data: { id: id, value: value },
            success: function (data)
            {
                wrapper.html(data);
            }
        });
    }

    this.initDocument = function () {
        $(document).on("click", ".document-like .plus.icon-plus", null, function () {
            var id = $(this).closest(".document-like").data("id");
            _this.LikeDocument(id, $(this), true);
        });

        $(document).on("click", ".document-like .minus.icon-minus", null, function () {
            var id = $(this).closest(".document-like").data("id");
            _this.LikeDocument(id, $(this), false);
        });
    }

    this.LikeDocument = function (id, item, value) {
        var wrapper = item.closest(".document-like-wrapper");

        $.ajax({
            type: "POST",
            url: "/Like/DocumentLike",
            data: { id: id, value: value },
            success: function (data) {
                _this.UpdateDocumentLike(id);
            }
        });
    }

    this.UpdateDocumentLike = function (id)
    {
        $.ajax({
            type: "GET",
            url: "/Like/DocumentLike",
            data: { id: id},
            success: function (data) {
                $(".document-like-wrapper").html(data);
            }
        });
    }

    this.initEvent = function () {
        $(document).on("click", ".event-like .plus.icon-plus", null, function () {
            var id = $(this).closest(".event-like").data("id");
            _this.LikeEvent(id, $(this), true);
        });

        $(document).on("click", ".event-like .minus.icon-minus", null, function () {
            var id = $(this).closest(".event-like").data("id");
            _this.LikeEvent(id, $(this), false);
        });
    }

    this.LikeEvent = function (id, item, value) {
        var wrapper = item.closest(".event-like-wrapper");

        $.ajax({
            type: "POST",
            url: "/Like/EventLike",
            data: { id: id, value: value },
            success: function (data) {
                _this.UpdateEventLike(id);
            }
        });
    }

    this.UpdateEventLike = function (id) {
        $.ajax({
            type: "GET",
            url: "/Like/EventLike",
            data: { id: id },
            success: function (data) {
                $(".event-like-wrapper").html(data);
            }
        });
    }

    this.initImage = function () {
        $(document).on("click", ".image-like .plus.icon-plus", null, function () {
            var id = $(this).closest(".image-like").data("id");
            _this.LikeImage(id, $(this), true);
        });

        $(document).on("click", ".image-like .minus.icon-minus", null, function () {
            var id = $(this).closest(".image-like").data("id");
            _this.LikeImage(id, $(this), false);
        });
    }

    this.LikeImage = function (id, item, value) {
        var wrapper = item.closest(".image-like-wrapper");

        $.ajax({
            type: "POST",
            url: "/Like/ImageLike",
            data: { id: id, value: value },
            success: function (data) {
                _this.UpdateImageLike(id);
            }
        });
    }

    this.UpdateImageLike = function (id) {
        $.ajax({
            type: "GET",
            url: "/Like/ImageLike",
            data: { id: id },
            success: function (data) {
                $(".image-like-wrapper").html(data);
            }
        });
    }

    this.initOrganization = function () {
        $(document).on("click", ".organization-like .plus.icon-plus", null, function () {
            var id = $(this).closest(".organization-like").data("id");
            _this.LikeOrganization(id, $(this), true);
        });

        $(document).on("click", ".organization-like .minus.icon-minus", null, function () {
            var id = $(this).closest(".organization-like").data("id");
            _this.LikeOrganization(id, $(this), false);
        });
    }

    this.LikeOrganization = function (id, item, value) {
        var wrapper = item.closest(".organization-like-wrapper");

        $.ajax({
            type: "POST",
            url: "/Like/OrganizationLike",
            data: { id: id, value: value },
            success: function (data) {
                _this.UpdateOrganizationLike(id);
            }
        });
    }

    this.UpdateOrganizationLike = function (id) {
        $.ajax({
            type: "GET",
            url: "/Like/OrganizationLike",
            data: { id: id },
            success: function (data) {
                $(".organization-like-wrapper").html(data);
            }
        });
    }

    this.initPublication = function () {
        $(document).on("click", ".publication-like .plus.icon-plus", null, function () {
            var id = $(this).closest(".publication-like").data("id");
            _this.LikePublication(id, $(this), true);
        });

        $(document).on("click", ".publication-like .minus.icon-minus", null, function () {
            var id = $(this).closest(".publication-like").data("id");
            _this.LikePublication(id, $(this), false);
        });
    }

    this.LikePublication = function (id, item, value) {
        var wrapper = item.closest(".publication-like-wrapper");

        $.ajax({
            type: "POST",
            url: "/Like/PublicationLike",
            data: { id: id, value: value },
            success: function (data) {
                _this.UpdatePublicationLike(id);
            }
        });
    }

    this.UpdatePublicationLike = function (id) {
        $.ajax({
            type: "GET",
            url: "/Like/PublicationLike",
            data: { id: id },
            success: function (data) {
                $(".publication-like-wrapper").html(data);
            }
        });
    }

    this.initStudyMaterial = function () {
        $(document).on("click", ".study-material-like .plus.icon-plus", null, function () {
            var id = $(this).closest(".study-material-like").data("id");
            _this.LikeStudyMaterial(id, $(this), true);
        });

        $(document).on("click", ".study-material-like .minus.icon-minus", null, function () {
            var id = $(this).closest(".study-material-like").data("id");
            _this.LikeStudyMaterial(id, $(this), false);
        });
    }

    this.LikeStudyMaterial = function (id, item, value) {
        var wrapper = item.closest(".study-material-like-wrapper");

        $.ajax({
            type: "POST",
            url: "/Like/StudyMaterialLike",
            data: { id: id, value: value },
            success: function (data) {
                _this.UpdateStudyMaterialLike(id);
            }
        });
    }

    this.UpdateStudyMaterialLike = function (id) {
        $.ajax({
            type: "GET",
            url: "/Like/StudyMaterialLike",
            data: { id: id },
            success: function (data) {
                $(".study-material-like-wrapper").html(data);
            }
        });
    }

    this.initWebLink = function () {
        $(document).on("click", ".web-link-like .plus.icon-plus", null, function () {
            var id = $(this).closest(".web-link-like").data("id");
            _this.LikeWebLink(id, $(this), true);
        });

        $(document).on("click", ".web-link-like .minus.icon-minus", null, function () {
            var id = $(this).closest(".web-link-like").data("id");
            _this.LikeWebLink(id, $(this), false);
        });
    }

    this.LikeWebLink = function (id, item, value) {
        var wrapper = item.closest(".web-link-like-wrapper");

        $.ajax({
            type: "POST",
            url: "/Like/WebLinkLike",
            data: { id: id, value: value },
            success: function (data) {
                _this.UpdateWebLinkLike(id);
            }
        });
    }

    this.UpdateWebLinkLike = function (id) {
        $.ajax({
            type: "GET",
            url: "/Like/WebLinkLike",
            data: { id: id },
            success: function (data) {
                $(".web-link-like-wrapper").html(data);
            }
        });
    }

    this.initBlogPostShort = function () {
        $(document).on("click", ".blog-post-short-like .plus.icon-plus", null, function () {
            var id = $(this).closest(".blog-post-short-like").data("id");
            _this.LikeBlogPostShort(id, $(this), true);
        });

        $(document).on("click", ".blog-post-short-like .minus.icon-minus", null, function () {
            var id = $(this).closest(".blog-post-short-like").data("id");
            _this.LikeBlogPostShort(id, $(this), false);
        });
    }

    this.LikeBlogPostShort = function (id, item, value) {
        var wrapper = item.closest(".blog-post-short-like");

        $.ajax({
            type: "POST",
            url: "/Like/BlogPostShortLike",
            data: { id: id, value: value },
            success: function (data) {
                _this.UpdateBlogPostShortLike(id);
            }
        });
    }


    this.UpdateBlogPostShortLike = function (id) {

        $.ajax({
            type: "GET",
            url: "/Like/BlogPostShortLike",
            data: { id: id },
            success: function (data) {
                $(".blog-post-short-like[data-id='"+id+"']").html(data);
            }
        });
    }

    this.initBlogPost = function () {
        $(document).on("click", ".blog-post-like .plus.icon-plus", null, function () {
            var id = $(this).closest(".blog-post-like").data("id");
            _this.LikeBlogPost(id, $(this), true);
        });

        $(document).on("click", ".blog-post-like .minus.icon-minus", null, function () {
            var id = $(this).closest(".blog-post-like").data("id");
            _this.LikeBlogPost(id, $(this), false);
        });
    }

    this.LikeBlogPost = function (id, item, value) {
        var wrapper = item.closest(".blog-post-like");

        $.ajax({
            type: "POST",
            url: "/Like/BlogPostLike",
            data: { id: id, value: value },
            success: function (data) {
                _this.UpdateBlogPostLike(id);
            }
        });
    }


    this.UpdateBlogPostLike = function (id) {

        $.ajax({
            type: "GET",
            url: "/Like/BlogPostLike",
            data: { id: id },
            success: function (data) {
                $(".blog-post-like").html(data);
            }
        });
    }
}

var like = null;

$().ready(function ()
{
    like = new Like();
    like.init();
});


