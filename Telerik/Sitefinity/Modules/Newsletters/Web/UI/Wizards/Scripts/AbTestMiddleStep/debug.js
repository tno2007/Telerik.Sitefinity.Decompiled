﻿Type.registerNamespace("Telerik.Sitefinity.Modules.Newsletters.Web.UI.Wizards");

Telerik.Sitefinity.Modules.Newsletters.Web.UI.Wizards.AbTestMiddleStep = function (element) {
    Telerik.Sitefinity.Modules.Newsletters.Web.UI.Wizards.AbTestMiddleStep.initializeBase(this, [element]);

    this._middleStepWrapper = null;
    this._clientLabelManager = null;
    this._backLink = null;

    this._copyContentLink = null;
    this._copyContentLinkClickDelegate = null;

    this._startFromScratchLink = null;
    this._startFromScratchLinkDelegate = null;

    this._issueAName = null;
    this._rootCampaignName = null;
    this._origin = null;

    this.HIDE_RESULT = {
        COPY_CONTENT: "copy-content",
        START_FROM_SCRATCH: "start-from-scratch"
    };
}

Telerik.Sitefinity.Modules.Newsletters.Web.UI.Wizards.AbTestMiddleStep.prototype = {
    initialize: function () {
        Telerik.Sitefinity.Modules.Newsletters.Web.UI.Wizards.AbTestMiddleStep.callBaseMethod(this, "initialize");

        this._copyContentLinkClickDelegate = Function.createDelegate(this, this._copyContentLinkClick);
        $addHandler(this.get_copyContentLink(), "click", this._copyContentLinkClickDelegate);

        this._startFromScratchLinkClickDelegate = Function.createDelegate(this, this._startFromScratchLinkClick);
        $addHandler(this.get_startFromScratchLink(), "click", this._startFromScratchLinkClickDelegate);
    },

    dispose: function () {
        if (this._copyContentLinkClickDelegate) {
            if (this.get_copyContentLink()) {
                $removeHandler(this.get_copyContentLink(), "click", this._copyContentLinkClickDelegate);
            }
            delete this._copyContentLinkClickDelegate;
        }

        if (this._startFromScratchLinkClickDelegate) {
            if (this.get_startFromScratchLink()) {
                $removeHandler(this.get_startFromScratchLink(), "click", this._startFromScratchLinkClickDelegate);
            }
            delete this._startFromScratchLinkClickDelegate;
        }

        Telerik.Sitefinity.Modules.Newsletters.Web.UI.Wizards.AbTestMiddleStep.callBaseMethod(this, "dispose");
    },

    show: function (viewParams) {
        this._issueAName = viewParams.issueAName;
        this._rootCampaignName = viewParams.rootCampaignName;
        this._origin = viewParams.origin;

        this._updateUi();

        jQuery(this.get_middleStepWrapper()).show();
    },

    hide: function (result) {
        var args = this._raise_hide(result);

        if (args.get_cancel() == false) {
            jQuery(this.get_middleStepWrapper()).hide();
        }
    },

    add_hide: function (delegate) {
        this.get_events().addHandler('hide', delegate);
    },

    remove_hide: function (delegate) {
        this.get_events().removeHandler('hide', delegate);
    },

    _raise_hide: function (result) {
        if (typeof this.get_events == 'function') {
            var h = this.get_events().getHandler('hide');
            var args = new Telerik.Sitefinity.Modules.Newsletters.Web.UI.Wizards.AbTestViewHideEventArgs(result);
            if (h) h(this, args);
            return args;
        }
    },

    _updateUi: function () {
        var backText;
        switch (this._origin) {
            case "campaigns":
                backText = this.get_clientLabelManager().getLabel("NewslettersResources", "BackToCampaigns");
                break;
            case "overview":
                backText = String.format(this.get_clientLabelManager().getLabel("NewslettersResources", "BackToFormat"), this._rootCampaignName);
                break;
        }
        jQuery(this.get_backLink()).html(backText);
        jQuery(this.get_copyContentLink()).html(String.format(this.get_clientLabelManager().getLabel("NewslettersResources", "CopyContentFromFormat"),
                    this._issueAName));
    },

    _copyContentLinkClick: function () {
        this.hide(this.HIDE_RESULT.COPY_CONTENT);
    },

    _startFromScratchLinkClick: function () {
        this.hide(this.HIDE_RESULT.START_FROM_SCRATCH);
    },

    get_clientLabelManager: function () {
        return this._clientLabelManager;
    },
    set_clientLabelManager: function (value) {
        this._clientLabelManager = value;
    },
    get_backLink: function () {
        return this._backLink;
    },
    set_backLink: function (value) {
        this._backLink = value;
    },
    get_middleStepWrapper: function () {
        return this._middleStepWrapper;
    },
    set_middleStepWrapper: function (value) {
        this._middleStepWrapper = value;
    },
    get_copyContentLink: function () {
        return this._copyContentLink;
    },
    set_copyContentLink: function (value) {
        this._copyContentLink = value;
    },
    get_startFromScratchLink: function () {
        return this._startFromScratchLink;
    },
    set_startFromScratchLink: function (value) {
        this._startFromScratchLink = value;
    }
};

Telerik.Sitefinity.Modules.Newsletters.Web.UI.Wizards.AbTestMiddleStep.registerClass('Telerik.Sitefinity.Modules.Newsletters.Web.UI.Wizards.AbTestMiddleStep', Sys.UI.Control);
