Type.registerNamespace("Telerik.Sitefinity.Modules.Comments.Web.UI.Frontend");

Telerik.Sitefinity.Modules.Comments.Web.UI.Frontend.CommentsCountControlBinder = function () {
    Telerik.Sitefinity.Modules.Comments.Web.UI.Frontend.CommentsCountControlBinder.initializeBase(this);

    this._serviceUrl = null;
    this._ninetyNinePlusText = null;
    this._commentsText = null;
    this._commentText = null;
    this._leaveCommentText = null;
}

Telerik.Sitefinity.Modules.Comments.Web.UI.Frontend.CommentsCountControlBinder.prototype = {
    initialize: function () {
        Telerik.Sitefinity.Modules.Comments.Web.UI.Frontend.CommentsCountControlBinder.callBaseMethod(this, 'initialize');

        this._getCommentsCounts();
    },

    // Release resources before control is disposed.
    dispose: function () {

        Telerik.Sitefinity.Modules.Comments.Web.UI.Frontend.CommentsCountControlBinder.callBaseMethod(this, 'dispose');
    },

    _collectThreadIds: function () {
        var commmentsCounterControls = jQuery(".sfcommentsCounterWrp");
        var uniqueKeys = {};
        for (var i = 0; i < commmentsCounterControls.length; i++) {
            uniqueKeys[jQuery(commmentsCounterControls[i]).attr("threadKey")] = true;
        }
        var threadKeys = new Array();
        jQuery.each(uniqueKeys, function (key, value) {
            threadKeys.push(key);
        });
        return threadKeys;
    },

    _setCommentsCounts: function (data, textStatus, jqXHR) {
        var threadCountList = data;
        for (var i = 0; i < threadCountList.Items.length; i++) {
            var currentThreadKey = threadCountList.Items[i].Key;
            var commmentsCounterControls = jQuery(".sfcommentsCounterWrp[threadKey='" + currentThreadKey + "']");
            var currentCount = threadCountList.Items[i].Count;

            //format count
            if (currentCount == -1)
                continue;

            var that = this;
            jQuery.each(commmentsCounterControls, function (index, commentsCounterControl) {
                var currentCountFormatted = "";
                var displayMode = jQuery(commentsCounterControl).attr("displayMode");

            if ((!currentCount || currentCount == 0) && !(displayMode === "IconOnly")) {
                    currentCountFormatted = that.get_leaveCommentText();
            }
            if (currentCount > 0 && currentCount < 100)
                currentCountFormatted = currentCount;
            else if (currentCount >= 100)
                    currentCountFormatted = that.get_ninetyNinePlusText();
            //add comments text
            if (displayMode === "FullText" && currentCount == 1)
                    currentCountFormatted += " " + that.get_commentText().toLowerCase();
            else if (displayMode === "FullText" && currentCount > 1)
                    currentCountFormatted += " " + that.get_commentsText().toLowerCase();

                //set the comments count text in the counter control
                jQuery(commentsCounterControl).html(currentCountFormatted);
            //set up the visible images
            if (displayMode === "IconOnly") {
                    that._setImages(currentCount, commentsCounterControl);
            }
            });
        }
    },

    _setImages: function (count, counterControl) {
        if (counterControl.length === 0)
            return;
        else if (count && count != 0) {
            jQuery(counterControl).removeClass("sfcommentsEmpty");
        }
        else {
            jQuery(counterControl).addClass("sfcommentsEmpty");
        }
    }, 

    _getCommentsCounts: function () {
        var threadKeys = this._collectThreadIds();
        var getCountUrl = String.format(this.get_serviceUrl() + "/comments/count?ThreadKey={0}", threadKeys);
        jQuery.ajax({
            type: 'GET',
            url: getCountUrl,
            contentType: "application/json",
            cache: false,
            accepts: {
                text: "application/json"
            },
            processData: false,
            success: jQuery.proxy(this._setCommentsCounts, this)
        });
    },

    get_serviceUrl: function () {
        return this._serviceUrl;
    },
    set_serviceUrl: function (value) {
        this._serviceUrl = value;
    },

    get_ninetyNinePlusText: function () {
        return this._ninetyNinePlusText;
    },
    set_ninetyNinePlusText: function (value) {
        this._ninetyNinePlusText = value;
    },

    get_commentsText: function () {
        return this._commentsText;
    },
    set_commentsText: function (value) {
        this._commentsText = value;
    },

    get_commentText: function () {
        return this._commentText;
    },
    set_commentText: function (value) {
        this._commentText = value;
    },

    get_leaveCommentText: function () {
        return this._leaveCommentText;
    },
    set_leaveCommentText: function (value) {
        this._leaveCommentText = value;
    }
}

Telerik.Sitefinity.Modules.Comments.Web.UI.Frontend.CommentsCountControlBinder.registerClass('Telerik.Sitefinity.Modules.Comments.Web.UI.Frontend.CommentsCountControlBinder', Sys.Component);