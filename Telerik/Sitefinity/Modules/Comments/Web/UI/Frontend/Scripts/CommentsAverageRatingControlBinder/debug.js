Type.registerNamespace("Telerik.Sitefinity.Modules.Comments.Web.UI.Frontend");

Telerik.Sitefinity.Modules.Comments.Web.UI.Frontend.CommentsAverageRatingControlBinder = function (element) {

    Telerik.Sitefinity.Modules.Comments.Web.UI.Frontend.CommentsAverageRatingControlBinder.initializeBase(this, [element]);
    this.threadKeys = new Array();
    this._serviceUrl = null;
    this._writeAReviewText = null;
    this._averageRatingText = null;
    this._reviewText = null;
    this._reviewsText = null;
}

Telerik.Sitefinity.Modules.Comments.Web.UI.Frontend.CommentsAverageRatingControlBinder.prototype = {
    initialize: function () {
        Telerik.Sitefinity.Modules.Comments.Web.UI.Frontend.CommentsAverageRatingControlBinder.callBaseMethod(this, 'initialize');
        this.collectThreadIds();
        this.getCommentsThreadRatings();
    },

    // Release resources before control is disposed.
    dispose: function () {
        Telerik.Sitefinity.Modules.Comments.Web.UI.Frontend.CommentsAverageRatingControlBinder.callBaseMethod(this, 'dispose');
    },

    collectThreadIds: function () {
        var CommentsAverageRatingControl = jQuery(".sfcommentsThreadRatingWrp");
        for (var i = 0; i < CommentsAverageRatingControl.length; i++) {
            var threadKey = jQuery(CommentsAverageRatingControl[i]).attr("threadKey");
            if (threadKey && !this.threadKeys.hasOwnProperty(threadKey)) {
                this.threadKeys.push(threadKey);
            }
        }
    },

    setData: function (data, textStatus, jqXHR) {
        for (var i = 0; i < data.length; i++) {
            this.setAverageRating(data[i]);
        }
    },
    /**
     * Sets the current average rating to the rating control. 
     * @param currentThread a thread item used for setting the rating based on
     * @return true if rating found and was set, false otherwise.
     */
    setAverageRating: function (currentThread) {
        var that = this;

        function getReviewsText(commentCount) {
            if (commentCount == 1)
                return " " + that.get_reviewText();
            else if (commentCount > 1)
                return " " + that.get_reviewsText();
            else
                return "";
        }

        var currentThreadKey = currentThread.Key;
        var commentsAverageRatingControls = jQuery(".sfcommentsThreadRatingWrp[threadKey='" + currentThreadKey + "']");
        
        jQuery.each(commentsAverageRatingControls, function (index, commentsAverageRatingControl) {
            var displayMode = jQuery(commentsAverageRatingControl).attr("displayMode");
            var navigateUrl = jQuery(commentsAverageRatingControl).attr("navigateUrl");

            if ((typeof currentThread.AverageRating === 'undefined') &&
               (displayMode === "FullText" || displayMode === "MediumText" || displayMode === "ShortText")) { // No average rating to display
                if (navigateUrl) { // If we have a navigation Url, we display the "Send a review" link
                    jQuery(commentsAverageRatingControl).rating({
                        value: null,
                        closingText: that.get_writeAReviewText(),
                        closingTextNavigateUrl: navigateUrl,
                        readOnly: true,
                        displayMode: displayMode
                    });
                }
            }
            else if (displayMode === "FullText") {
                jQuery(commentsAverageRatingControl).rating({
                    value: currentThread.AverageRating,
                    label: that.get_averageRatingText(),
                    reviewCount: currentThread.Count,
                    reviewCountNavigateUrl: navigateUrl,
                    closingText: getReviewsText(currentThread.Count),
                    closingTextNavigateUrl: navigateUrl,
                    readOnly: true,
                    displayMode: "FullText"
                });
            } else if (displayMode === "MediumText") {
                jQuery(commentsAverageRatingControl).rating({
                    value: currentThread.AverageRating,
                    reviewCount: currentThread.Count,
                    reviewCountNavigateUrl: navigateUrl,
                    closingText: getReviewsText(currentThread.Count),
                    closingTextNavigateUrl: navigateUrl,
                    readOnly: true,
                    displayMode: "MediumText"
                });
            } else if (displayMode === "ShortText") {
                jQuery(commentsAverageRatingControl).rating({
                    value: currentThread.AverageRating,
                    readOnly: true,
                    displayMode: "ShortText"
                });
            } else { //displayMode === "IconOnly"
                jQuery(commentsAverageRatingControl).rating({
                    value: currentThread.AverageRating,
                    readOnly: true
                });
            }
        });

        return true;
    },

    getCommentsThreadRatings: function () {
        var getThreadDataUrl = String.format(this.get_serviceUrl() + "/comments/reviews_statistics?ThreadKey={0}", this.threadKeys);
        jQuery.ajax({
            type: 'GET',
            url: getThreadDataUrl,
            contentType: "application/json",
            cache: false,
            accepts: {
                text: "application/json"
            },
            processData: false,
            success: jQuery.proxy(this.setData, this)
        });
    },

    get_serviceUrl: function () {
        return this._serviceUrl;
    },
    set_serviceUrl: function (value) {
        this._serviceUrl = value;
    },

    get_writeAReviewText: function () {
        return this._writeAReviewText;
    },
    set_writeAReviewText: function (value) {
        this._writeAReviewText = value;
    },

    get_averageRatingText: function () {
        return this._averageRatingText;
    },
    set_averageRatingText: function (value) {
        this._averageRatingText = value;
    },

    get_reviewText: function () {
        return this._reviewText;
    },
    set_reviewText: function (value) {
        this._reviewText = value;
    },

    get_reviewsText: function () {
        return this._reviewsText;
    },
    set_reviewsText: function (value) {
        this._reviewsText = value;
    }

}

Telerik.Sitefinity.Modules.Comments.Web.UI.Frontend.CommentsAverageRatingControlBinder.registerClass('Telerik.Sitefinity.Modules.Comments.Web.UI.Frontend.CommentsAverageRatingControlBinder', Sys.UI.Control);