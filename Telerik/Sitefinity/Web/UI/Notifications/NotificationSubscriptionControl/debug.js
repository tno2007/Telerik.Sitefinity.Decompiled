Type.registerNamespace("Telerik.Sitefinity.Web.UI.Notifications");

Telerik.Sitefinity.Web.UI.Notifications.NotificationSubscriptionControl = function (element) {
    Telerik.Sitefinity.Web.UI.Notifications.NotificationSubscriptionControl.initializeBase(this, [element]);

    this._subscribeUrl = null;
    this._unsubscribeUrl = null;
    this._checkSubscriptionStatus = null;
    this._subscriptionItemKey = null;

    this._subscribeWrp = null;
    this._unsubscribeWrp = null;
    this._successfullySubscribedWrp = null;
    this._successfullyUnsubscribedWrp = null;

    this.subscribeAnchor = null;
    this.unsubscribeAnchor = null;
    this.successfullySubscribedAnchor = null;
    this.successfullyUnsubscribedAnchor = null;

    this._subscribeLinkDelegate = null;
    this._unsubscribeLinkDelegate = null;
}

Telerik.Sitefinity.Web.UI.Notifications.NotificationSubscriptionControl.prototype = {
    initialize: function () {
        Telerik.Sitefinity.Web.UI.Notifications.NotificationSubscriptionControl.callBaseMethod(this, 'initialize');
          
        this.subscribeAnchor = jQuery(this.get_subscribeWrp()).find("a")[0];
        this.unsubscribeAnchor = jQuery(this.get_unsubscribeWrp()).find("a")[0];
        this.successfullySubscribedAnchor = jQuery(this.get_successfullySubscribedWrp()).find("a")[0];
        this.successfullyUnsubscribedAnchor = jQuery(this.get_successfullyUnsubscribedWrp()).find("a")[0];

        this._subscribeLinkDelegate = Function.createDelegate(this, this._subscribeLinkHandler);
        $addHandler(this.subscribeAnchor, "click", this._subscribeLinkDelegate);
        $addHandler(this.successfullyUnsubscribedAnchor, "click", this._subscribeLinkDelegate);

        this._unsubscribeLinkDelegate = Function.createDelegate(this, this._unsubscribeLinkHandler);
        $addHandler(this.unsubscribeAnchor, "click", this._unsubscribeLinkDelegate);
        $addHandler(this.successfullySubscribedAnchor, "click", this._unsubscribeLinkDelegate);
    },

    dispose: function () {
        if (this._subscribeLinkDelegate) {
            if (this.subscribeAnchor) {
                $removeHandler(this.subscribeAnchor, "click", this._subscribeLinkDelegate);
            }
            if (this.successfullyUnsubscribedAnchor) {
                $removeHandler(this.successfullyUnsubscribedAnchor, "click", this._subscribeLinkDelegate);
            }
            delete this._subscribeLinkDelegate;
        }

        if (this._unsubscribeLinkDelegate) {
            if (this.unsubscribeAnchor) {
                $removeHandler(this.unsubscribeAnchor, "click", this._unsubscribeLinkDelegate);
            }
            if (this.successfullySubscribedAnchor) {
                $removeHandler(this.successfullySubscribedAnchor, "click", this._unsubscribeLinkDelegate);
            }
            delete this._unsubscribeLinkDelegate;
        }

        Telerik.Sitefinity.Web.UI.Notifications.NotificationSubscriptionControl.callBaseMethod(this, 'dispose');
    },

    init: function () {
        url = this.get_checkSubscriptionStatus();
        url = url + '?threadKey=' + escape(this.get_subscriptionItemKey());

        jQuery.ajax({
            type: "GET",
            url: url,
            cache: false,
            success: jQuery.proxy(this.onCheckSubscriptionStatusSuccess, this),
            dataType: "json"
        });
    },

    onCheckSubscriptionStatusSuccess: function (data) {
        if (data.IsSubscribed == true)
            jQuery(this.get_unsubscribeWrp()).show();
        else
            jQuery(this.get_subscribeWrp()).show();
    },

    _subscribeLinkHandler: function (sender) {
        url = this.get_subscribeUrl();
        url = url + '?threadKey=' + escape(this.get_subscriptionItemKey());

        jQuery.ajax({
            type: "POST",
            url: url,
            cache:false,
            success: jQuery.proxy(this.onSubscribeSuccess, this),
            dataType: "json"
        });
    },

    onSubscribeSuccess:function(){
        jQuery(this.get_subscribeWrp()).hide();
        jQuery(this.get_successfullyUnsubscribedWrp()).hide();
        jQuery(this.get_successfullySubscribedWrp()).show();
    },

    _unsubscribeLinkHandler: function (sender) {
        url = this.get_unsubscribeUrl();
        url = url + '?threadKey=' + escape(this.get_subscriptionItemKey());

        jQuery.ajax({
            type: "POST",
            url: url,
            cache: false,
            success: jQuery.proxy(this.onUnsubscribeSuccess, this),
            dataType: "json"
        });
    },

    onUnsubscribeSuccess:function(){
        jQuery(this.get_unsubscribeWrp()).hide();
        jQuery(this.get_successfullySubscribedWrp()).hide();
        jQuery(this.get_successfullyUnsubscribedWrp()).show();
    },

    get_subscribeUrl: function () {
        return this._subscribeUrl;
    },
    set_subscribeUrl: function (value) {
        this._subscribeUrl = value;
    },

    get_unsubscribeUrl: function () {
        return this._unsubscribeUrl;
    },
    set_unsubscribeUrl: function (value) {
        this._unsubscribeUrl = value;
    },

    get_checkSubscriptionStatus: function () {
        return this._checkSubscriptionStatus;
    },
    set_checkSubscriptionStatus: function (value) {
        this._checkSubscriptionStatus = value;
    },

    get_subscriptionItemKey: function () {
        return this._subscriptionItemKey;
    },
    set_subscriptionItemKey: function (value) {
        this._subscriptionItemKey = value;
    },

    get_subscribeWrp: function () {
        return this._subscribeWrp;
    },
    set_subscribeWrp: function (value) {
        this._subscribeWrp = value;
    },

    get_unsubscribeWrp: function () {
        return this._unsubscribeWrp;
    },
    set_unsubscribeWrp: function (value) {
        this._unsubscribeWrp = value;
    },

    get_successfullySubscribedWrp: function () {
        return this._successfullySubscribedWrp;
    },
    set_successfullySubscribedWrp: function (value) {
        this._successfullySubscribedWrp = value;
    },

    get_successfullyUnsubscribedWrp: function () {
        return this._successfullyUnsubscribedWrp;
    },
    set_successfullyUnsubscribedWrp: function (value) {
        this._successfullyUnsubscribedWrp = value;
    }
}

Telerik.Sitefinity.Web.UI.Notifications.NotificationSubscriptionControl.registerClass("Telerik.Sitefinity.Web.UI.Notifications.NotificationSubscriptionControl", Sys.UI.Control);