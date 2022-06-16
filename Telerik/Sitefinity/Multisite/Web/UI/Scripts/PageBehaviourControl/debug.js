﻿Type.registerNamespace("Telerik.Sitefinity.Multisite.Web.UI");

Telerik.Sitefinity.Multisite.Web.UI.PageBehaviourControl = function (element) {
    Telerik.Sitefinity.Multisite.Web.UI.PageBehaviourControl.initializeBase(this, [element]);

    this._element = element;
    this._value = null;

    this._showMessageRadio = null;
    this._messageField = null;
    this._redirectRadio = null;
    this._pageField = null;
    this._clientLabelManager = null;

    this._messageRadioClickDelegate = null;
    this._redirectRadioClickDelegate = null;

    this.EMPTY_GUID = "00000000-0000-0000-0000-000000000000";
}

Telerik.Sitefinity.Multisite.Web.UI.PageBehaviourControl.prototype = {
    initialize: function () {
        Telerik.Sitefinity.Multisite.Web.UI.PageBehaviourControl.callBaseMethod(this, "initialize");

        this._messageRadioClickDelegate = Function.createDelegate(this, this._messageRadioClick);
        $addHandler(this.get_showMessageRadio(), "click", this._messageRadioClickDelegate);

        this._redirectRadioClickDelegate = Function.createDelegate(this, this._redirectRadioClick);
        $addHandler(this.get_redirectRadio(), "click", this._redirectRadioClickDelegate);
    },

    dispose: function () {
        if (this._messageRadioClickDelegate) {
            if (this.get_showMessageRadio()) {
                $removeHandler(this.get_showMessageRadio(), "click", this._messageRadioClickDelegate);
            }
            delete this._messageRadioClickDelegate;
        }

        if (this._redirectRadioClickDelegate) {
            if (this.get_redirectRadio()) {
                $removeHandler(this.get_redirectRadio(), "click", this._redirectRadioClickDelegate);
            }
            delete this._redirectRadioClickDelegate;
        }

        Telerik.Sitefinity.Multisite.Web.UI.PageBehaviourControl.callBaseMethod(this, "dispose");
    },

    /* *************************** public methods *************************** */

    reset: function (rootNodeId) {
        this.get_messageField().set_value(this.get_clientLabelManager().getLabel("MultisiteResources", "ThisPageIsNotAccessible"));
        jQuery(this.get_showMessageRadio()).click();
        this.get_pageField().reset();
        this._setRootNodeId(rootNodeId);
    },

    /* *************************** event handlers *************************** */

    _messageRadioClick: function (sender, args) {
        this._showMessageField();
    },

    _redirectRadioClick: function (sender, args) {
        this._showPageSelector();
    },

    /* *************************** private methods *************************** */

    _showMessageField: function () {
        jQuery(this.get_messageField().get_element()).show();
        jQuery(this.get_pageField().get_element()).hide();
    },

    _showPageSelector: function () {
        jQuery(this.get_pageField().get_element()).show();
        jQuery(this.get_messageField().get_element()).hide();
    },

    
    _setRootNodeId: function (rootNodeId) {
        if(rootNodeId) {
            this.get_pageField().get_pageSelector().set_rootNodeId(rootNodeId);
        }
    },

    /* *************************** properties *************************** */

    get_element: function () {
        return this._element;
    },
    get_value: function () {
        if (this.get_showMessageRadio().checked) {
            this._value = { "Message": this.get_messageField().get_value() };
        }
        else if (this.get_redirectRadio().checked) {
            var pageId = this.get_pageField().get_value() ? this.get_pageField().get_value() : this.EMPTY_GUID;
            this._value = { "PageId": pageId };
        }

        return this._value;
    },
    set_value: function (value) {
        this._value = value;
        this._setRootNodeId(value.SiteMapRootNodeId);

        if (value.OfflineSiteMessage) {
            this.get_messageField().set_value(value.OfflineSiteMessage);
            jQuery(this.get_showMessageRadio()).click();
        }
        else if (value.OfflinePageToRedirect && value.OfflinePageToRedirect != this.EMPTY_GUID) {
            this.get_pageField().set_value(value.OfflinePageToRedirect)
            jQuery(this.get_redirectRadio()).click();
        }
    },
    get_showMessageRadio: function () {
        return this._showMessageRadio;
    },
    set_showMessageRadio: function (value) {
        this._showMessageRadio = value;
    },
    get_messageField: function () {
        return this._messageField;
    },
    set_messageField: function (value) {
        this._messageField = value;
    },
    get_redirectRadio: function () {
        return this._redirectRadio;
    },
    set_redirectRadio: function (value) {
        this._redirectRadio = value;
    },
    get_pageField: function () {
        return this._pageField;
    },
    set_pageField: function (value) {
        this._pageField = value;
    },
    get_clientLabelManager: function () {
        return this._clientLabelManager;
    },
    set_clientLabelManager: function (value) {
        this._clientLabelManager = value;
    }
};

Telerik.Sitefinity.Multisite.Web.UI.PageBehaviourControl.registerClass('Telerik.Sitefinity.Multisite.Web.UI.PageBehaviourControl', Telerik.Sitefinity.Web.UI.Kendo.KendoWindow);
