$.fn.extend({
    insertAtCaret: function (myValue) {
        if (document.selection) {
            this.focus();
            sel = document.selection.createRange();
            sel.text = myValue;
            this.focus();
        }
        else if (this.selectionStart || this.selectionStart == '0') {
            var startPos = this.selectionStart;
            var endPos = this.selectionEnd;
            var scrollTop = this.scrollTop;
            this.val(this.val().substring(0, startPos) + myValue + this.val().substring(endPos, this.val().length));
            this.focus();
            this.selectionStart = startPos + myValue.length;
            this.selectionEnd = startPos + myValue.length;
            this.scrollTop = scrollTop;
        } else {
            this.val(this.val() + myValue);
            this.focus();
        }
    }
})

Type.registerNamespace("Telerik.Sitefinity.Modules.Newsletters.Web.UI.Wizards.Steps");

Telerik.Sitefinity.Modules.Newsletters.Web.UI.Wizards.Steps.CampaignMessageStep = function (element) {
    this._plainTextPanel = null;
    this._htmlTextPanel = null;
    this._internalPagePanel = null;
    this._externalPagePanel = null;
    this._plainTextControl = null;
    this._htmlTextControl = null;
    this._existingPagesSelector = null;
    this._editInternalPageLink = null;
    this._mergeTagSelector = null;
    this._htmlMergeTagSelector = null;
    this._plainTextGenerationHtml = null;
    this._plainTextVersionHtmlPanel = null;
    this._plainTextVersionHtml = null;
    this._plainTextGenerationPage = null;
    this._plainTextVersionPagePanel = null;
    this._plainTextVersionPage = null;
    this._messageBodyId = null;
    this._messageMode = null;
    this._editorUrl = null;
    this._PLAINTEXT = 0;
    this._HTML = 1;
    this._STANDARDINTERNAL = 2;
    this._STANDARDEXTERNAL = 3;

    this._editInternalPageDelegate = null;
    this._mergeTagSelectedDelegate = null;
    this._htmlMergeTagSelectedDelegate = null;
    this._plainTextGenerationChangedDelegate = null;

    Telerik.Sitefinity.Modules.Newsletters.Web.UI.Wizards.Steps.CampaignMessageStep.initializeBase(this, [element]);
}

Telerik.Sitefinity.Modules.Newsletters.Web.UI.Wizards.Steps.CampaignMessageStep.prototype = {

    // set up 
    initialize: function () {

        if (this._editInternalPageDelegate === null) {
            this._editInternalPageDelegate = Function.createDelegate(this, this._openPageEditor);
        }
        if (this._editInternalPageLink != null) {
            $addHandler(this._editInternalPageLink, 'click', this._editInternalPageDelegate);
        }

        if (this._mergeTagSelectedDelegate === null) {
            this._mergeTagSelectedDelegate = Function.createDelegate(this, this._mergeTagSelectedHandler);
        }
        if (this._mergeTagSelector != null) {
            this._mergeTagSelector.add_tagSelected(this._mergeTagSelectedDelegate);
        }

        if (this._htmlMergeTagSelectedDelegate === null) {
            this._htmlMergeTagSelectedDelegate = Function.createDelegate(this, this._htmlMergeTagSelectedHandler);
        }
        if (this._htmlMergeTagSelector != null) {
            this._htmlMergeTagSelector.add_tagSelected(this._htmlMergeTagSelectedDelegate);
        }

        if (this._plainTextGenerationChangedDelegate === null) {
            this._plainTextGenerationChangedDelegate = Function.createDelegate(this, this._plainTextGenerationChangedHandler);
        }
        if (this._plainTextGenerationHtml != null) {
            this._plainTextGenerationHtml.add_valueChanged(this._plainTextGenerationChangedDelegate);
        }
        if (this._plainTextGenerationPage != null) {
            this._plainTextGenerationPage.add_valueChanged(this._plainTextGenerationChangedDelegate);
        }

        Telerik.Sitefinity.Modules.Newsletters.Web.UI.Wizards.Steps.CampaignMessageStep.callBaseMethod(this, "initialize");
    },

    // tear down
    dispose: function () {

        if (this._editInternalPageDelegate) {
            delete this._editInternalPageDelegate;
        }

        if (this._mergeTagSelectedDelegate) {
            this._mergeTagSelector.remove_tagSelected(this._mergeTagSelectedDelegate);
            delete this._mergeTagSelectedDelegate;
        }

        if (this._htmlMergeTagSelectedDelegate) {
            this._htmlMergeTagSelector.remove_tagSelected(this._htmlMergeTagSelectedDelegate);
            delete this._htmlMergeTagSelectedDelegate;
        }

        if (this._plainTextGenerationChangedDelegate) {
            this._plainTextGenerationHtml.remove_valueChanged(this._plainTextGenerationChangedDelegate);
            this._plainTextGenerationPage.remove_valueChanged(this._plainTextGenerationChangedDelegate)
            delete this._plainTextGenerationChangedDelegate;
        }

        Telerik.Sitefinity.Modules.Newsletters.Web.UI.Wizards.Steps.CampaignMessageStep.callBaseMethod(this, "dispose");
    },

    /* *************************** public methods *************************** */

    isValid: function () {

        if (this.get_autoSaved()) {

            if (this._messageMode == this._STANDARDINTERNAL) {
                return this._messageBodyId != null;
            } else if (this._messageMode == this._STANDARDEXTERNAL) {
                var selectedPages = this.get_existingPagesSelector().getSelectedItems();
                return selectedPages.length > 0;
            } else {
                return this.get_bodyText().length > 0;
            }
        }

        return false;
    },

    loadMessageBody: function (messageBody) {
        this.set_messageBodyId(messageBody.Id);
        switch (messageBody.MessageBodyType) {
            case this._PLAINTEXT:
                this.switchToPlainTextMode();
                if (messageBody.BodyText != null) {
                    $(this.get_plainTextControl()).val(messageBody.BodyText);
                }
                break;
            case this._HTML:
                this.switchToHtmlMode();
                if (messageBody.BodyText != null) {
                    this.get_htmlTextControl().set_value(messageBody.BodyText);
                }
                this.set_htmlPlainTextVersion(messageBody.PlainTextVersion);
                break;
            case this._STANDARDINTERNAL:
                this.switchToStandardInternalMode();
                this.set_pagePlainTextVersion(messageBody.PlainTextVersion);
                break;
            case this._STANDARDEXTERNAL:
                alert('Not implemented!');
                break;
        }
    },

    show: function () {
        if (this._messageMode == this._STANDARDINTERNAL) {
            this._openPageEditor();
        } else if (this._messageMode == this._STANDARDEXTERNAL) {
            // do nothing
        }
    },

    reset: function () {
        this.switchToHtmlMode();
        this._messageMode = this._HTML;
        this._messageBodyId = null;
        jQuery(this.get_plainTextControl()).val('');
        this.get_htmlTextControl().set_value('');
    },

    switchToPlainTextMode: function () {
        $(this.get_plainTextPanel()).show();
        $(this.get_htmlTextPanel()).hide();
        $(this.get_internalPagePanel()).hide();
        $(this.get_externalPagePanel()).hide();
        this._messageMode = this._PLAINTEXT;
    },

    switchToHtmlMode: function () {
        $(this.get_plainTextPanel()).hide();
        $(this.get_htmlTextPanel()).show();
        $(this.get_internalPagePanel()).hide();
        $(this.get_externalPagePanel()).hide();
        this._messageMode = this._HTML;
    },

    switchToStandardInternalMode: function () {
        $(this.get_plainTextPanel()).hide();
        $(this.get_htmlTextPanel()).hide();
        $(this.get_internalPagePanel()).show();
        $(this.get_externalPagePanel()).hide();
        this._messageMode = this._STANDARDINTERNAL;
    },

    switchToStandardExternalMode: function () {
        $(this.get_plainTextPanel()).hide();
        $(this.get_htmlTextPanel()).hide();
        $(this.get_internalPagePanel()).hide();
        $(this.get_externalPagePanel()).show();
        this._messageMode = this._STANDARDEXTERNAL;
    },

    setMergeTags: function (mergeTags) {
        this.get_mergeTagSelector().setMergeTags(mergeTags);
        this.get_htmlMergeTagSelector().setMergeTags(mergeTags);
    },

    /* *************************** private methods *************************** */

    _openPageEditor: function () {
        var currentWindow = this._getRadWindow();
        var windowManager = currentWindow.get_windowManager();
        var editorWindow = windowManager.getWindowByName("standardCampaignEditor");
        var url = this._editorUrl + this.get_messageBodyId();
        if (editorWindow.get_navigateUrl() != url) {
            editorWindow.set_navigateUrl(url);
        }
        editorWindow.show();
        editorWindow.maximize();
        currentWindow.minimize();
    },

    _mergeTagSelectedHandler: function (sender, args) {
        $(this.get_plainTextControl()).insertAtCaret(args.MergeTag);
    },

    _htmlMergeTagSelectedHandler: function (sender, args) {
        this.get_htmlTextControl()._editControl.pasteHtml(args.MergeTag);
    },

    _getRadWindow: function () {
        var oWindow = null;
        if (window.radWindow)
            oWindow = window.radWindow;
        else if (window.frameElement.radWindow)
            oWindow = window.frameElement.radWindow;
        return oWindow;
    },

    _plainTextGenerationChangedHandler: function (sender, args) {
        var selectedValue = sender.get_value();
        switch (selectedValue) {
            case "AutomaticallyHtml":
                jQuery(this._plainTextVersionHtmlPanel).hide();
                break;
            case "ManuallyHtml":
                jQuery(this._plainTextVersionHtmlPanel).show();
                break;
            case "AutomaticallyPage":
                jQuery(this._plainTextVersionPagePanel).hide();
                break;
            case "ManuallyPage":
                jQuery(this._plainTextVersionPagePanel).show();
                break;
            default:
                break;
        }
    },

    /* *************************** properties *************************** */
    get_plainTextPanel: function () {
        return this._plainTextPanel;
    },
    set_plainTextPanel: function (value) {
        this._plainTextPanel = value;
    },
    get_htmlTextPanel: function () {
        return this._htmlTextPanel;
    },
    set_htmlTextPanel: function (value) {
        this._htmlTextPanel = value;
    },
    get_internalPagePanel: function () {
        return this._internalPagePanel;
    },
    set_internalPagePanel: function (value) {
        this._internalPagePanel = value;
    },
    get_externalPagePanel: function () {
        return this._externalPagePanel;
    },
    set_externalPagePanel: function (value) {
        this._externalPagePanel = value;
    },
    get_plainTextControl: function () {
        return this._plainTextControl;
    },
    set_plainTextControl: function (value) {
        this._plainTextControl = value;
    },
    get_htmlTextControl: function () {
        return this._htmlTextControl;
    },
    set_htmlTextControl: function (value) {
        this._htmlTextControl = value;
    },
    get_existingPagesSelector: function () {
        return this._existingPagesSelector;
    },
    set_existingPagesSelector: function (value) {
        this._existingPagesSelector = value;
    },
    get_editInternalPageLink: function () {
        return this._editInternalPageLink;
    },
    set_editInternalPageLink: function (value) {
        this._editInternalPageLink = value;
    },
    get_messageBodyId: function () {
        return this._messageBodyId;
    },
    set_messageBodyId: function (value) {
        this._messageBodyId = value;
    },
    get_bodyText: function () {
        if (this._messageMode == this._PLAINTEXT) {
            return this.get_plainTextControl().value;
        } else if (this._messageMode == this._HTML) {
            var htmlVal = this.get_htmlTextControl().get_value();
            if (htmlVal != null && htmlVal != '<br>') {
                return htmlVal;
            }
            return '';
        }
        else {
            return '';
        }
    },
    set_bodyText: function (value) {
        if (this._messageMode == this._PLAINTEXT) {
            this.get_plainTextControl().value = value;
        } else if (this._messageMode == this._HTML) {
            this.get_htmlTextControl().set_value(value);
        } else {
            alert('This campaign type is not supported!');
        }
    },
    get_mergeTagSelector: function () {
        return this._mergeTagSelector;
    },
    set_mergeTagSelector: function (value) {
        this._mergeTagSelector = value;
    },
    get_htmlMergeTagSelector: function () {
        return this._htmlMergeTagSelector;
    },
    set_htmlMergeTagSelector: function (value) {
        this._htmlMergeTagSelector = value;
    },

    get_plainTextGenerationHtml: function () {
        return this._plainTextGenerationHtml;
    },
    set_plainTextGenerationHtml: function (value) {
        this._plainTextGenerationHtml = value;
    },

    get_plainTextVersionHtmlPanel: function () {
        return this._plainTextVersionHtmlPanel;
    },
    set_plainTextVersionHtmlPanel: function (value) {
        this._plainTextVersionHtmlPanel = value;
    },

    get_plainTextVersionHtml: function () {
        return this._plainTextVersionHtml;
    },
    set_plainTextVersionHtml: function (value) {
        this._plainTextVersionHtml = value;
    },

    get_htmlPlainTextVersion: function () {
        if (this._messageMode == this._HTML && this._plainTextGenerationHtml.get_value() == "ManuallyHtml") {
            return this._plainTextVersionHtml.value;
        }
        return '';
    },
    set_htmlPlainTextVersion: function (value) {
        if (this._messageMode == this._HTML) {
            this._plainTextVersionHtml.value = value;
            if (value != "") {
                this._plainTextGenerationHtml.set_value("ManuallyHtml");
            }
        }
    },

    get_plainTextGenerationPage: function () {
        return this._plainTextGenerationPage;
    },
    set_plainTextGenerationPage: function (value) {
        this._plainTextGenerationPage = value;
    },

    get_plainTextVersionPagePanel: function () {
        return this._plainTextVersionPagePanel;
    },
    set_plainTextVersionPagePanel: function (value) {
        this._plainTextVersionPagePanel = value;
    },

    get_plainTextVersionPage: function () {
        return this._plainTextVersionPage;
    },
    set_plainTextVersionPage: function (value) {
        this._plainTextVersionPage = value;
    },

    get_pagePlainTextVersion: function () {
        if (this._messageMode == this._STANDARDINTERNAL && this._plainTextGenerationPage.get_value() == "ManuallyPage") {
            return this._plainTextVersionPage.value;
        }
        return '';
    },
    set_pagePlainTextVersion: function (value) {
        if (this._messageMode == this._STANDARDINTERNAL) {
            this._plainTextVersionPage.value = value;
            if (value != "") {
                this._plainTextGenerationPage.set_value("ManuallyPage");
            }
        }
    }
};

Telerik.Sitefinity.Modules.Newsletters.Web.UI.Wizards.Steps.CampaignMessageStep.registerClass('Telerik.Sitefinity.Modules.Newsletters.Web.UI.Wizards.Steps.CampaignMessageStep', Telerik.Sitefinity.Modules.Newsletters.Web.UI.WizardFramework.SitefinityWizardStepControl);