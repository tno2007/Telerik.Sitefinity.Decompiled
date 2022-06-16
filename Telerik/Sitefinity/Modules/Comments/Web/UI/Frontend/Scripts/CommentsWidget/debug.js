﻿Type.registerNamespace("Telerik.Sitefinity.Modules.Comments.Web.UI.Frontend");

Telerik.Sitefinity.Modules.Comments.Web.UI.Frontend.CommentsWidget = function (element) {
    Telerik.Sitefinity.Modules.Comments.Web.UI.Frontend.CommentsWidget.initializeBase(this, [element]);

    this._submitCommentForm = null;
    this._commentsListViewForm = null;
    this._serviceUrl = null;
    this._loginServiceUrl = null;
    this._leaveCommentAnchor = null;
    this._clientLabelManager = null;
    this._threadKey = null;
    this._threadIsClosed = null;
    this._commentsRestClient = null;
    this._countAll = 0;
    this._language = null;
    this._onLoadDelegate = null;
    this.commentsRestClientCommon;
    this._requiresApprovalSetting = null;
    this._requiredApprovalMessageLabel = null;
    this._reviewSubmittedSuccessfullyMessageLabel = null;
    this._commentsNotificationSubscriptionControl = null;
    this._requiresCaptchaSetting = null;
    this._commentsAverageRatingControlBinder = null;
    this._enableRatings = null;
}

Telerik.Sitefinity.Modules.Comments.Web.UI.Frontend.CommentsWidget.prototype = {
    initialize: function () {
        Telerik.Sitefinity.Modules.Comments.Web.UI.Frontend.CommentsWidget.callBaseMethod(this, 'initialize');

        this.commentsRestClientCommon = new Telerik.Sitefinity.Modules.Comments.Web.UI.Frontend.CommentsRestClient(this.get_serviceUrl());
        this.set_commentsRestClient(this.commentsRestClientCommon);
        this.get_commentsListViewForm().set_commentsRestClient(this.commentsRestClientCommon);

        if (!this.get_threadIsClosed()) {
            this._leaveCommentAnchor = this.get_commentsListViewForm().get_leaveCommentAnchor();
            this._leaveCommentClickDelegate = Function.createDelegate(this, this._scrollToItem);
            $addHandler(this._leaveCommentAnchor, "click", this._leaveCommentClickDelegate);
        }
        else {
            this._leaveCommentAnchor = this.get_commentsListViewForm().get_leaveCommentAnchor();
            jQuery(this._leaveCommentAnchor).hide();
        }

        this._onLoadDelegate = Function.createDelegate(this, this._onLoadHandler);
        Sys.Application.add_load(this._onLoadDelegate);

        this.set_countAll(this.get_commentsListViewForm().get_initalCommentsCount());

        //if paging is enabled
        if (this.get_commentsListViewForm().get_loadMoreCommentsAnchor() !== null)
            this.get_commentsListViewForm().add_updateLabels(jQuery.proxy(this._updateLoadMoreCommentsLinksVisibility, this));

        this._updateLeaveCommentsLinkVisibility();
        this._updateSortLinkVisibility();

        this.changeFormsVisibility();


    },

    dispose: function () {
        //if thread is nto closed
        if (this.get_submitCommentForm()) {
            if (this.get_submitCommentForm().get_submitAuthenticatedCommentForm()) {
                this.get_submitCommentForm().get_submitAuthenticatedCommentForm().remove_submitSuccess(this._refreshCommentsAll);
            }

            if (this.get_submitCommentForm().get_submitUnAuthenticatedCommentForm()) {
                this.get_submitCommentForm().get_submitUnAuthenticatedCommentForm().remove_submitSuccess(this._refreshCommentsAll);
            }

            if (this._leaveCommentClickDelegate) {
                if (this._leaveCommentAnchor) {
                    $removeHandler(this._leaveCommentAnchor, "click", this._leaveCommentClickDelegate);
                }
                delete this._leaveCommentClickDelegate;
            }
        }

        if (this._onLoadDelegate) {
            Sys.Application.remove_load(this._onLoadDelegate);
            delete this._onLoadDelegate;
        }

        //if paging is enabled
        if (this.get_commentsListViewForm().get_loadMoreCommentsAnchor() !== null)
            this.get_commentsListViewForm().remove_updateLabels(this._updateLoadMoreCommentsLinksVisibility);

        Telerik.Sitefinity.Modules.Comments.Web.UI.Frontend.CommentsWidget.callBaseMethod(this, 'dispose');
    },

    changeFormsVisibility: function () {
        var that = this;
        var submitForm = this.get_submitCommentForm();

        var onSuccess = function (data) {            
            if (submitForm) {
                jQuery.proxy(submitForm.setFormViewMode(data.IsAuthenticated), submitForm);
                that.setCaptchaVisibility(data.IsAuthenticated);
            }

            if (that.get_commentsNotificationSubscriptionControl()) {
                if (!data.IsAuthenticated) {
                    jQuery(that.get_commentsNotificationSubscriptionControl().get_element()).hide();
                }
                else {
                    that.get_commentsNotificationSubscriptionControl().init();
                }
            }
        };

        var onError = function () {
            that.showAuthenticatedForm = false;
            that.showUnAuthenticatedForm = false;

            if (submitForm) {
                jQuery(submitForm.get_submitCommentForm()).hide();
                jQuery(submitForm.get_loginLink()).hide();
            }
        };

        var requestUrl = (this.get_loginServiceUrl() + String.format("/is-authenticated"));

        jQuery.ajax({
            type: 'GET',
            url: requestUrl,
            contentType: "application/json",
            accepts: {
                text: "application/json"
            },
            cache: false,
            success: onSuccess,
            error: onError
        });
    },

    setCaptchaVisibility: function (isAuthenticated) {
        var submitForm = this.get_submitCommentForm();

        if (isAuthenticated == true) {
            jQuery.proxy(submitForm.setCaptchaViewMode(false));
        }
        else {
            submitForm.setCaptchaViewMode(this._requiresCaptchaSetting);
        }
    },

    _onLoadHandler: function () {
        if (this.get_submitCommentForm()) {
            this.get_submitCommentForm().add_submitSuccess(jQuery.proxy(this._refreshCommentsAll, this));
        }
    },

    //refresh the kendo comments list by adding the item only client side
    //refresh the comments count
    _refreshCommentsAll: function (sender, args) {
        if (this.get_requiresApprovalSetting())
            jQuery(this.get_requiredApprovalMessageLabel()).show();
        else if (this.get_enableRatings())
            jQuery(this.get_reviewSubmittedSuccessfullyMessageLabel()).show();

        this._refreshCommentsList(sender, args);

        //get count
        this.get_commentsRestClient().getCommentsCount([this.get_threadKey()], null, jQuery.proxy(this._updateCommentsCount, this));
    },

    _refreshCommentsList: function (sender, args) {
        this.get_commentsListViewForm().addNewComment(args);
    },


    //scroll to the comment form 
    _scrollToItem: function (sender, args) {
        jQuery('html,body').animate({
            scrollTop: jQuery(this.get_submitCommentForm().get_element()).offset().top
        }, 300);

    },

    //updates the comments count label
    _updateCommentsCount: function (data, textStatus, jqXHR) {
        this.set_countAll(data.Items[0].Count);

        //if paging is enabled
        if (this.get_commentsListViewForm().get_loadMoreCommentsAnchor() !== null)
            this._updateLoadMoreCommentsLinksVisibility();

        if (data.Items[0].Count == 0) {
            return;
        }

        this._updateLeaveCommentsLinkVisibility();
        this._updateSortLinkVisibility();

        //update count labels
        var counterFields = jQuery(
            ".sfcommentsCounterWrp[threadKey = '" + this.get_threadKey() + "']," +
            ".sfcommentsTitle[threadKey = '" + this.get_threadKey() + "']");
        var commentsCountLabelText;
        var singleCommentCountLabelFormatText = this.get_clientLabelManager().getLabel("CommentsResources", "CommentCount");
        var commentsCountLabelFormatText = this.get_clientLabelManager().getLabel("CommentsResources", "CommentsCount");
        if (this.get_enableRatings() == true) {
            singleCommentCountLabelFormatText = this.get_clientLabelManager().getLabel("CommentsResources", "ReviewCount");
            commentsCountLabelFormatText = this.get_clientLabelManager().getLabel("CommentsResources", "ReviewsCount");
        }

        for (var i = 0; i < counterFields.length; i++) {
            if ($(counterFields[i]).is('.sfcommentsIconOnly, .sfcommentsShort')) {
                commentsCountLabelText = data.Items[0].Count;
            }
            else if (data.Items[0].Count == 1) {
                commentsCountLabelText = String.format(singleCommentCountLabelFormatText, data.Items[0].Count);
            }
            else if (data.Items[0].Count > 1) {
                commentsCountLabelText = String.format(commentsCountLabelFormatText, data.Items[0].Count);
            }
            $(counterFields[i]).html(commentsCountLabelText);
        }

        if (this.get_commentsAverageRatingControlBinder()) {
            this.get_commentsAverageRatingControlBinder().getCommentsThreadRatings();
        }
    },

    //hide/show leave comment link
    _updateLeaveCommentsLinkVisibility: function () {
        if (this.get_countAll() == 0 ||
            this.get_threadIsClosed() ||
            this.get_submitCommentForm() == null ||
            !this.get_submitCommentForm()._isVisible()) {
            jQuery(this._leaveCommentAnchor).hide();
        }
        else {
            jQuery(this._leaveCommentAnchor).show();
        }
    },

    //update sort links visiblility
    _updateSortLinkVisibility: function () {
        if (this.get_countAll() <= 1) {
            jQuery(this.get_element()).find(".sfcommentsOrderFilterWrp").hide();
        }
        else {
            jQuery(this.get_element()).find(".sfcommentsOrderFilterWrp").show();
        }
    },

    //update the visibility of load more link
    _updateLoadMoreCommentsLinksVisibility: function () {
        var count = this.get_countAll();

        //show/hide load more link
        if (count > this.get_commentsListViewForm().get_currentCommentsCount())
            jQuery(this.get_commentsListViewForm().get_loadMoreCommentsAnchor()).show();
        else
            jQuery(this.get_commentsListViewForm().get_loadMoreCommentsAnchor()).hide();
    },

    get_submitCommentForm: function () {
        return this._submitCommentForm;
    },
    set_submitCommentForm: function (value) {
        this._submitCommentForm = value;
    },

    get_commentsListViewForm: function () {
        return this._commentsListViewForm;
    },
    set_commentsListViewForm: function (value) {
        this._commentsListViewForm = value;
    },

    get_serviceUrl: function () {
        return this._serviceUrl;
    },
    set_serviceUrl: function (value) {
        this._serviceUrl = value;
    },

    get_loginServiceUrl: function () {
        return this._loginServiceUrl;
    },
    set_loginServiceUrl: function (value) {
        this._loginServiceUrl = value;
    },

    get_threadKey: function () {
        return encodeURIComponent(this._threadKey);
    },
    set_threadKey: function (value) {
        this._threadKey = value;
    },

    get_threadIsClosed: function () {
        return this._threadIsClosed;
    },
    set_threadIsClosed: function (value) {
        this._threadIsClosed = value;
    },

    get_clientLabelManager: function () {
        return this._clientLabelManager;
    },
    set_clientLabelManager: function (value) {
        this._clientLabelManager = value;
    },

    get_commentsRestClient: function () {
        return this._commentsRestClient;
    },
    set_commentsRestClient: function (value) {
        this._commentsRestClient = value;
    },

    get_countAll: function () {
        return this._countAll;
    },
    set_countAll: function (value) {
        this._countAll = value;
    },

    get_language: function () {
        return this._language;
    },
    set_language: function (value) {
        this._language = value;
    },

    get_requiredApprovalMessageLabel: function () {
        return this._requiredApprovalMessageLabel;
    },
    set_requiredApprovalMessageLabel: function (value) {
        this._requiredApprovalMessageLabel = value;
    },

    get_reviewSubmittedSuccessfullyMessageLabel: function () {
        return this._reviewSubmittedSuccessfullyMessageLabel;
    },
    set_reviewSubmittedSuccessfullyMessageLabel: function (value) {
        this._reviewSubmittedSuccessfullyMessageLabel = value;
    },

    get_requiresApprovalSetting: function () {
        return this._requiresApprovalSetting;
    },
    set_requiresApprovalSetting: function (value) {
        this._requiresApprovalSetting = value;
    },

    get_commentsNotificationSubscriptionControl: function () {
        return this._commentsNotificationSubscriptionControl;
    },
    set_commentsNotificationSubscriptionControl: function (value) {
        this._commentsNotificationSubscriptionControl = value;
    },

    get_requiresCaptchaSetting: function () {
        return this._requiresCaptchaSetting;
    },
    set_requiresCaptchaSetting: function (value) {
        this._requiresCaptchaSetting = value;
    },

    get_commentsAverageRatingControlBinder: function () {
        return this._commentsAverageRatingControlBinder;
    },
    set_commentsAverageRatingControlBinder: function (value) {
        this._commentsAverageRatingControlBinder = value;
    },

    get_enableRatings: function () {
        return this._enableRatings;
    },
    set_enableRatings: function (value) {
        this._enableRatings = value;
    }
};

Telerik.Sitefinity.Modules.Comments.Web.UI.Frontend.CommentsWidget.registerClass("Telerik.Sitefinity.Modules.Comments.Web.UI.Frontend.CommentsWidget", Sys.UI.Control);