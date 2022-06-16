﻿Type.registerNamespace("Telerik.Sitefinity.Modules.Newsletters.Web.UI.Reports");

Telerik.Sitefinity.Modules.Newsletters.Web.UI.Reports.AbTestAnalysis = function (element) {
    Telerik.Sitefinity.Modules.Newsletters.Web.UI.Reports.AbTestAnalysis.initializeBase(this, [element]);

    this._rootUrl = null;
    this._providerName = null;
    this._abTestId = null;
    this._manager = null;

    this._whatWasTestedReadPanel = null;
    this._whatWasTestedWritePanel = null;
    this._whatWasTestedLabel = null;
    this._whatWasTestedTextBox = null;

    this._whatWasTestedEditButtonClickDelegate = null;
    this._whatWasTestedEditButton = null;

    this._whatWasTestedSaveButtonClickDelegate = null;
    this._whatWasTestedSaveButton = null;

    this._conclusionReadPanel = null;
    this._conclusionWritePanel = null;
    this._conclusionLabel = null;
    this._conclusionTextBox = null;

    this._conclusionEditButtonClickDelegate = null;
    this._conclusionEditButton = null;

    this._conclusionSaveButtonClickDelegate = null;
    this._conclusionSaveButton = null;
}

Telerik.Sitefinity.Modules.Newsletters.Web.UI.Reports.AbTestAnalysis.prototype = {
    initialize: function () {
        Telerik.Sitefinity.Modules.Newsletters.Web.UI.Reports.AbTestAnalysis.callBaseMethod(this, "initialize");

        this._manager = new Telerik.Sitefinity.Modules.Newsletters.Web.UI.NewslettersClientManager(this._rootUrl, this._providerName);

        this._whatWasTestedEditButtonClickDelegate = Function.createDelegate(this, this._whatWasTestedEditButtonClickHandler);
        $addHandler(this.get_whatWasTestedEditButton(), "click", this._whatWasTestedEditButtonClickDelegate);

        this._whatWasTestedSaveButtonClickDelegate = Function.createDelegate(this, this._whatWasTestedSaveButtonClickHandler);
        $addHandler(this.get_whatWasTestedSaveButton(), "click", this._whatWasTestedSaveButtonClickDelegate);

        this._conclusionEditButtonClickDelegate = Function.createDelegate(this, this._conclusionEditButtonClickHandler);
        $addHandler(this.get_conclusionEditButton(), "click", this._conclusionEditButtonClickDelegate);

        this._conclusionSaveButtonClickDelegate = Function.createDelegate(this, this._conclusionSaveButtonClickHandler);
        $addHandler(this.get_conclusionSaveButton(), "click", this._conclusionSaveButtonClickDelegate);

        if (!jQuery(this.get_whatWasTestedLabel()).html()) {
            this._whatWasTestedSwitchToWriteMode();
        }

        if (!jQuery(this.get_conclusionLabel()).html()) {
            this._conclusionSwitchToWriteMode();
        }
    },

    dispose: function () {
        if (this._whatWasTestedEditButtonClickDelegate) {
            if (this.get_whatWasTestedEditButton()) {
                $removeHandler(this.get_whatWasTestedEditButton(), "click", this._whatWasTestedEditButtonClickDelegate);
            }
            delete this._whatWasTestedEditButtonClickDelegate;
        }

        if (this._whatWasTestedSaveButtonClickDelegate) {
            if (this.get_whatWasTestedSaveButton()) {
                $removeHandler(this.get_whatWasTestedSaveButton(), "click", this._whatWasTestedSaveButtonClickDelegate);
            }
            delete this._whatWasTestedSaveButtonClickDelegate;
        }

        if (this._conclusionEditButtonClickDelegate) {
            if (this.get_conclusionEditButton()) {
                $removeHandler(this.get_conclusionEditButton(), "click", this._conclusionEditButtonClickDelegate);
            }
            delete this._conclusionEditButtonClickDelegate;
        }

        if (this._conclusionSaveButtonClickDelegate) {
            if (this.get_conclusionSaveButton()) {
                $removeHandler(this.get_conclusionSaveButton(), "click", this._conclusionSaveButtonClickDelegate);
            }
            delete this._conclusionSaveButtonClickDelegate;
        }

        this._manager.dispose();

        Telerik.Sitefinity.Modules.Newsletters.Web.UI.Reports.AbTestAnalysis.callBaseMethod(this, "dispose");
    },

    _whatWasTestedEditButtonClickHandler: function (sender, args) {
        this._whatWasTestedSwitchToWriteMode();
    },

    _whatWasTestedSaveButtonClickHandler: function (sender, args) {
        var value = jQuery(this.get_whatWasTestedTextBox()).val();
        this._manager.setAbTestTestingNote(this._abTestId, value);
        jQuery(this.get_whatWasTestedLabel()).html(value);
        if (value) {
            jQuery(this.get_whatWasTestedReadPanel()).show();
            jQuery(this.get_whatWasTestedWritePanel()).hide();
        }
    },

    _conclusionEditButtonClickHandler: function (sender, args) {
        this._conclusionSwitchToWriteMode();
    },

    _conclusionSaveButtonClickHandler: function (sender, args) {
        var value = jQuery(this.get_conclusionTextBox()).val();
        this._manager.setAbTestConclusion(this._abTestId, value);
        jQuery(this.get_conclusionLabel()).html(value);
        if (value) {
            jQuery(this.get_conclusionReadPanel()).show();
            jQuery(this.get_conclusionWritePanel()).hide();
        }
    },

    _whatWasTestedSwitchToWriteMode: function () {
        jQuery(this.get_whatWasTestedTextBox()).val(jQuery(this.get_whatWasTestedLabel()).html());
        jQuery(this.get_whatWasTestedWritePanel()).show();
        jQuery(this.get_whatWasTestedReadPanel()).hide();
    },

    _conclusionSwitchToWriteMode: function () {
        jQuery(this.get_conclusionTextBox()).val(jQuery(this.get_conclusionLabel()).html());
        jQuery(this.get_conclusionWritePanel()).show();
        jQuery(this.get_conclusionReadPanel()).hide();
    },

    get_whatWasTestedReadPanel: function () {
        return this._whatWasTestedReadPanel;
    },
    set_whatWasTestedReadPanel: function (value) {
        this._whatWasTestedReadPanel = value;
    },
    get_whatWasTestedWritePanel: function () {
        return this._whatWasTestedWritePanel;
    },
    set_whatWasTestedWritePanel: function (value) {
        this._whatWasTestedWritePanel = value;
    },
    get_whatWasTestedLabel: function () {
        return this._whatWasTestedLabel;
    },
    set_whatWasTestedLabel: function (value) {
        this._whatWasTestedLabel = value;
    },
    get_whatWasTestedTextBox: function () {
        return this._whatWasTestedTextBox;
    },
    set_whatWasTestedTextBox: function (value) {
        this._whatWasTestedTextBox = value;
    },
    get_whatWasTestedEditButton: function () {
        return this._whatWasTestedEditButton;
    },
    set_whatWasTestedEditButton: function (value) {
        this._whatWasTestedEditButton = value;
    },
    get_whatWasTestedSaveButton: function () {
        return this._whatWasTestedSaveButton;
    },
    set_whatWasTestedSaveButton: function (value) {
        this._whatWasTestedSaveButton = value;
    },
    get_conclusionReadPanel: function () {
        return this._conclusionReadPanel;
    },
    set_conclusionReadPanel: function (value) {
        this._conclusionReadPanel = value;
    },
    get_conclusionWritePanel: function () {
        return this._conclusionWritePanel;
    },
    set_conclusionWritePanel: function (value) {
        this._conclusionWritePanel = value;
    },
    get_conclusionLabel: function () {
        return this._conclusionLabel;
    },
    set_conclusionLabel: function (value) {
        this._conclusionLabel = value;
    },
    get_conclusionTextBox: function () {
        return this._conclusionTextBox;
    },
    set_conclusionTextBox: function (value) {
        this._conclusionTextBox = value;
    },
    get_conclusionEditButton: function () {
        return this._conclusionEditButton;
    },
    set_conclusionEditButton: function (value) {
        this._conclusionEditButton = value;
    },
    get_conclusionSaveButton: function () {
        return this._conclusionSaveButton;
    },
    set_conclusionSaveButton: function (value) {
        this._conclusionSaveButton = value;
    }
};

Telerik.Sitefinity.Modules.Newsletters.Web.UI.Reports.AbTestAnalysis.registerClass('Telerik.Sitefinity.Modules.Newsletters.Web.UI.Reports.AbTestAnalysis', Sys.UI.Control);

