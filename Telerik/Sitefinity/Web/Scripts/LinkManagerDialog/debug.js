﻿Type.registerNamespace("Telerik.Sitefinity.Web.UI");

Telerik.Sitefinity.Web.UI.LinkManagerDialog = function (element) {
    Telerik.Sitefinity.Web.UI.LinkManagerDialog.initializeBase(this, [element]);
    this._dialogModesSwitcher = null;
    this._pageSelector = null;
    this._webAddressField = null;
    this._emailField = null;
    this._insertLink = null;
    this._webAddressSection = null;
    this._pageSelectorSection = null;
    this._emailSection = null;

    this._textToDisplayFieldsClientIDs = [];
    this._moreOptionsSectionsClientIDs = [];
    this._textToDisplayFields = [];
    this._moreOptionsSections = [];

    this._linkToInsert = null;
    this._linkHasChildrenDomElements = null;
    this._currentMode = null;
    this._propertyEditorDialog = null;
    this._textToDisplayInitialValue = "";

    this._loadDelegate = null;
    this._closeDialogSaveDelegate = null;
    this._selectionChangedDelegate = null;
    this._valueChangedDelegate = null;
    this._keyUpDelegate = null;
    this._beforeDialogCloseDelegate = null;
    this._beforeLinkManagerDialogCloseDelegate = null;

    this._culture = null;
    this._uiCulture = null;
}

Telerik.Sitefinity.Web.UI.LinkManagerDialog.prototype =
{
    /* -------------------- set up and tear down ----------- */

    initialize: function () {
        Telerik.Sitefinity.Web.UI.LinkManagerDialog.callBaseMethod(this, "initialize");

        this._loadDelegate = Function.createDelegate(this, this._load);
        Sys.Application.add_load(this._loadDelegate);

        this._closeDialogSaveDelegate = Function.createDelegate(this, this._closeDialogSave);

        this._selectionChangedDelegate = Function.createDelegate(this, this._selectionChanged);
        this.get_pageSelector().add_selectionChanged(this._selectionChangedDelegate);

        this._valueChangedDelegate = Function.createDelegate(this, this._valueChanged);
        this.get_dialogModesSwitcher().add_valueChanged(this._valueChangedDelegate);

        this._keyUpDelegate = Function.createDelegate(this, this._configureButtonArea);
        $addHandler(this.get_webAddressField().get_textElement(), "keyup", this._keyUpDelegate);
        $addHandler(this.get_emailField().get_textElement(), "keyup", this._keyUpDelegate);

        var clientId = null;
        for (var i = 0; i <= this._textToDisplayFieldsClientIDs.length - 1; i++) {
            clientId = this._textToDisplayFieldsClientIDs[i];
            var textToDisplayField = $find(clientId);
            if (textToDisplayField) {
                this._textToDisplayFields.push(textToDisplayField);
                $addHandler(textToDisplayField.get_textElement(), "keyup", this._keyUpDelegate);
            }
        }

        for (var i = 0; i <= this._moreOptionsSectionsClientIDs.length - 1; i++) {
            clientId = this._moreOptionsSectionsClientIDs[i];
            var moreOptionsSection = $find(clientId);
            if (moreOptionsSection) {
                this._moreOptionsSections.push(moreOptionsSection);
            }
        }

        this._beforeDialogCloseDelegate = Function.createDelegate(this, this._beforeDialogCloseHandler);
        this._beforeLinkManagerDialogCloseDelegate = Function.createDelegate(this, this._beforeLinkManagerDialogCloseHandler);
        // TODO: this code is specific for zone editor. We should use a different approach to find property editor. 
        if (this._dialog &&
            this._dialog._parent &&
            this._dialog._parent.get_parent() &&
            this._dialog._parent.get_parent().get_parent() &&
            this._dialog._parent.get_parent().get_parent().get_parent() &&
            this._dialog._parent.get_parent().get_parent().get_parent().get_parent() &&
            this._dialog._parent.get_parent().get_parent().get_parent().get_parent().get_parent()) {
            this._propertyEditorDialog = this._dialog._parent.get_parent().get_parent().get_parent().get_parent().get_parent()._dialog;

            if (this._propertyEditorDialog != null) {
                if (this._propertyEditorDialog.get_name() == "PropertyEditorDialog") {
                    this._propertyEditorDialog.add_beforeClose(this._beforeDialogCloseDelegate);
                    this._dialog.add_beforeClose(this._beforeLinkManagerDialogCloseDelegate);
                }
            }
        }

        $addHandler(this.get_insertLink(), "click", this._closeDialogSaveDelegate);
    },

    dispose: function () {
        Telerik.Sitefinity.Web.UI.LinkManagerDialog.callBaseMethod(this, "dispose");

        if (this._loadDelegate) {
            Sys.Application.remove_load(this._loadDelegate);
            delete this._loadDelegate;
        }

        if (this.get_insertLink()) {
            $removeHandler(this.get_insertLink(), "click", this._closeDialogSaveDelegate);
        }

        if (this._closeDialogSaveDelegate) {
            delete this._closeDialogSaveDelegate;
        }

        if (this._selectionChangedDelegate) {
            if (this.get_pageSelector()) {
                this.get_pageSelector().remove_selectionChanged(this._selectionChangedDelegate);
            }
            delete this._selectionChangedDelegate;
        }

        if (this._valueChangedDelegate) {
            if (this.get_dialogModesSwitcher()) {
                this.get_dialogModesSwitcher().remove_valueChanged(this._valueChangedDelegate);
            }
            delete this._valueChangedDelegate;
        }

        if (this._keyUpDelegate) {
            if (this.get_webAddressField()) {
                $removeHandler(this.get_webAddressField().get_textElement(), "keyup", this._keyUpDelegate);
            }
            if (this.get_emailField()) {
                $removeHandler(this.get_emailField().get_textElement(), "keyup", this._keyUpDelegate);
            }
            for (var i = 0; i <= this._textToDisplayFields.length - 1; i++) {
                var textToDisplayField = this._textToDisplayFields[i];
                if (textToDisplayField) {
                    $removeHandler(textToDisplayField.get_textElement(), "keyup", this._keyUpDelegate);
                }
            }
            delete this._keyUpDelegate;
        }

        if (this._beforeDialogCloseDelegate) {
            delete this._beforeDialogCloseDelegate;
        }

        if (this._beforeLinkManagerDialogCloseDelegate) {
            this._dialog.remove_beforeClose(this._beforeLinkManagerDialogCloseDelegate);
            delete this._beforeLinkManagerDialogCloseDelegate;
        }
    },

    /* --------------------  public methods ----------- */

    /* -------------------- events -------------------- */

    /* -------------------- event handlers ------------ */

    _load: function () {
        this._linkToInsert = this.get_radWindow().ClientParameters;
        this._setCurrentMode();

        // initialize textToDisplay fields
        if (jQuery(this._linkToInsert).children().length > 0) {
            this._disableTextToDisplayFields();
        }
        else {
            this._textToDisplayInitialValue = this._linkToInsert.innerText;
            for (var i = 0; i <= this._textToDisplayFields.length - 1; i++) {
                var textToDisplayField = this._textToDisplayFields[i];
                textToDisplayField.set_value(this._linkToInsert.innerText);
            }
        }

        // initialize fields in More Options section
        for (var i = 0; i <= this._moreOptionsSections.length - 1; i++) {
            var moreOptionsSection = this._moreOptionsSections[i];
            var linkTooltip = moreOptionsSection.get_linkTooltip();
            var linkClass = moreOptionsSection.get_linkClass();
            var openInNewWindow = moreOptionsSection.get_openInNewWindow();

            linkTooltip.set_value(this._linkToInsert.title);
            linkClass.set_value(this._linkToInsert.className);
            if (openInNewWindow && this._linkToInsert.target === "_blank") {
                openInNewWindow.set_value(true);
            }
        }

        var idx = -1;
        switch (this._currentMode) {
            case "1":
                var webAddress = this._linkToInsert.getAttribute("href") ? this._linkToInsert.getAttribute("href") : "http://";
                this.get_webAddressField().set_value(webAddress);
                this._configureButtonArea();
                break;
            case "2":
                var sfref = this._linkToInsert.getAttribute("sfref");
                var href = this._linkToInsert.getAttribute("href");
                var itemNotFound = href.startsWith("Item with ID") && href.endsWith("was not found!");
                idx = sfref.indexOf("]");
                if (idx > -1 && !itemNotFound) {
                    var rootNodeId = sfref.substr(1, idx - 1);
                    if (rootNodeId != "") {
                        this.get_pageSelector().set_rootNodeIdOnEdit(rootNodeId);
                    }
                    var id = sfref.substring(idx + 1);
                    this.get_pageSelector().set_selectedItemId(id);
                    this._enableInsertButton();
                }
                break;
            case "3":
                this.get_webAddressField().set_value("http://");
                idx = this._linkToInsert.href.indexOf(":");
                if (idx > -1) {
                    var email = this._linkToInsert.href.substring(idx + 1);
                    this.get_emailField().set_value(email);
                }
                this._configureButtonArea();
                break;
            default:
                break;
        }
        jQuery("body").addClass("sfOverflowHiddenX");
        this._clearValidationErrors();
    },

    _clearValidationErrors: function () {
        jQuery(this._element).find('.sfError ').hide();
    },

    _selectionChanged: function (sender, args) {
        var selectedItems = args;
        var textToDisplayField = this._textToDisplayFields[this._currentMode - 1];

        if (selectedItems && selectedItems.length > 0) {
            if (this._textToDisplayInitialValue == "") {
                var dataItem = selectedItems[0];
                var title = null;
                if (dataItem.Title && dataItem.Title.hasOwnProperty('Value')) {
                    title = dataItem.Title.Value;
                }
                else {
                    title = dataItem.Title;
                }
                textToDisplayField.set_value(title);
            }
            this._enableInsertButton();
        }
        else if (sender.get_selectedItemId()) {
            this._enableInsertButton();
        }
        else {
            if (this._textToDisplayInitialValue == "") {
                textToDisplayField.set_value("");
            }
            this._disableInsertButton();
        }
    },

    _validateCurrentScreenInputs: function () {
        var isInputValid = false;

        var textToDisplayField = this._textToDisplayFields[this._currentMode - 1];
        var urlField = (this._currentMode === '1') ? this.get_webAddressField() : this.get_emailField();

        if (this._currentMode === '2') {
            isInputValid = this.get_pageSelector().get_selectedItem();
        } else {
            isInputValid = urlField.validate();
        }

        if (jQuery(textToDisplayField.get_textElement()).is(":visible"))
            isInputValid = isInputValid && textToDisplayField.validate();

        return isInputValid;
    },
    _closeDialogSave: function () {
        this._setLinkProperties();

        var args = new Telerik.Sitefinity.CommandEventArgs("pasteLink", this._linkToInsert);
        if (this._propertyEditorDialog) {
            this._propertyEditorDialog.remove_beforeClose(this._beforeDialogCloseDelegate);
        }

        if (this._validateCurrentScreenInputs()) {
            this.close(args);
        }
    },

    _valueChanged: function (sender, args) {
        var value = this.get_dialogModesSwitcher().get_value();
        switch (value) {
            case "1":
                this._switchToWebAddressMode();
                break;
            case "2":
                this._switchToPageSelectionMode();
                break;
            case "3":
                this._switchToEmailMode();
                break;
            default:
                break;
        }

        this._currentMode = value;
    },

    //fired before closing of the dialog
    _beforeDialogCloseHandler: function (sender, args) {
        args.set_cancel(true);
        if (this._propertyEditorDialog) {
            this._propertyEditorDialog.remove_beforeClose(this._beforeDialogCloseDelegate);
        }
        this.close();
    },

    //fired before closing of the link manager dialog
    _beforeLinkManagerDialogCloseHandler: function (sender, args) {
        if (this._propertyEditorDialog) {
            this._propertyEditorDialog.remove_beforeClose(this._beforeDialogCloseDelegate);
        }
    },

    /* -------------------- private methods ----------- */

    _getOuterHtml: function (element) {
        if (element) {
            return $telerik.getOuterHtml(element);
        }
        return "";
    },

    _setLinkProperties: function () {
        // set href and sfref attributes
        switch (this._currentMode) {
            case "1":
                this._linkToInsert.href = this.get_webAddressField().get_value();
                if (this._linkToInsert.getAttribute("sfref")) {
                    this._linkToInsert.removeAttribute("sfref");
                }

                break;
            case "2":
                var selectedItem = this.get_pageSelector().get_selectedItem();
                if (selectedItem) {
                    var href = selectedItem.FullUrl;
                    this._linkToInsert.setAttribute("href", href);
                }
                var selectedItemId = this.get_pageSelector().get_selectedItemId();
                var selectedCulture = (this.get_uiCulture() !== this.get_pageSelector().get_languageSelectorSelectedCulture()) ?
                    this.get_pageSelector().get_languageSelectorSelectedCulture() : null;
                if (selectedItemId) {
                    var key;
                    var rootNodeId = this.get_pageSelector().get_rootNodeId();
                    if (rootNodeId && rootNodeId != Telerik.Sitefinity.getEmptyGuid()) {
                        key = rootNodeId;
                    }
                    else if (selectedItem) {
                        key = selectedItem.RootId;
                    }
                    else {
                        key = "";
                    }
                    var sfref = "[" + key;
                    if (selectedCulture) {
                        sfref += "|lng%3A" + selectedCulture;
                    }
                    sfref += "]" + selectedItemId;

                    this._linkToInsert.setAttribute("sfref", sfref);
                }
                break;
            case "3":
                this._linkToInsert.href = "mailto:" + this.get_emailField().get_value();
                this._linkToInsert.removeAttribute("sfref");
                break;
        }

        // set innerHTML property
        if (!this._linkHasChildrenDomElements) {
            var textToDisplayField = this._textToDisplayFields[this._currentMode - 1];
            this._linkToInsert.innerText = textToDisplayField.get_value();
        }

        // set title, className and target properties
        var moreOptionsSection = this._moreOptionsSections[this._currentMode - 1];
        var linkTooltip = moreOptionsSection.get_linkTooltip();
        var linkClass = moreOptionsSection.get_linkClass();
        var openInNewWindow = moreOptionsSection.get_openInNewWindow();

        this._linkToInsert.title = linkTooltip.get_value();
        this._linkToInsert.className = linkClass.get_value();
        if (openInNewWindow && openInNewWindow.get_value() == "true") {
            this._linkToInsert.target = "_blank";
        }
        else {
            this._linkToInsert.target = "";
        }
    },

    _enableInsertButton: function () {
        jQuery(this.get_insertLink()).removeClass("sfDisabledLinkBtn");
    },

    _disableInsertButton: function () {
        jQuery(this.get_insertLink()).addClass("sfDisabledLinkBtn");
    },

    _configureButtonArea: function (sender, args) {
        var textToDisplayField = this._textToDisplayFields[this._currentMode - 1];
        var errorsBeforeValidation = jQuery(this._element).find(".sfError:visible").length;
       
        var isValidInput = this._validateCurrentScreenInputs();

        var errorsAfterValidation = jQuery(this._element).find(".sfError:visible").length;

        if (errorsAfterValidation > errorsBeforeValidation) {
            this.resizeToContent();
        }

        if (this._currentMode == "2") {
            if (sender)
                this._textToDisplayInitialValue = sender.target.value;
        }
        else {
            isValidInput = isValidInput && (textToDisplayField.get_value() || this._linkHasChildrenDomElements);
        }

        if (isValidInput) {
            this._enableInsertButton();
        }
        else {
            this._disableInsertButton();
        }
    },

    _switchToWebAddressMode: function () {
        jQuery(this.get_pageSelectorSection()).hide();
        jQuery(this.get_webAddressSection()).show();
        jQuery(this.get_emailSection()).hide();
        this._configureButtonArea();
        this._clearValidationErrors();
        this.resizeToContent();
    },

    _switchToPageSelectionMode: function () {
        jQuery(this.get_pageSelectorSection()).show();
        jQuery(this.get_webAddressSection()).hide();
        jQuery(this.get_emailSection()).hide();
        if (this.get_pageSelector().get_selectedItem() || this.get_pageSelector().get_selectedItemId()) {
            this._enableInsertButton();
        }
        else {
            this._disableInsertButton();
        }
        this._clearValidationErrors();
        this.resizeToContent();
    },

    _switchToEmailMode: function () {
        jQuery(this.get_pageSelectorSection()).hide();
        jQuery(this.get_webAddressSection()).hide();
        jQuery(this.get_emailSection()).show();
        this._configureButtonArea();
        this._clearValidationErrors();
        this.resizeToContent();
    },

    _disableTextToDisplayFields: function () {
        this._linkHasChildrenDomElements = true;
        for (var i = 0; i <= this._textToDisplayFields.length - 1; i++) {
            var textToDisplayField = this._textToDisplayFields[i];

            if (textToDisplayField.get_textElement())
                jQuery(textToDisplayField.get_textElement()).hide();
            if (textToDisplayField.get_titleElement())
                jQuery(textToDisplayField.get_titleElement()).hide();
            if (textToDisplayField.get_exampleElement())
                jQuery(textToDisplayField.get_exampleElement()).hide();
        }
    },

    _setCurrentMode: function () {
        var sfRef = this._linkToInsert.getAttribute("sfref");
        if (sfRef && sfRef.startsWith("[") && !sfRef.startsWith("[documents|")) {
            this._currentMode = "2";
        }
        else if (this._linkToInsert.href.indexOf("mailto:") > -1) {
            this._currentMode = "3";
        }
        else {
            this._currentMode = "1";
        }

        this.get_dialogModesSwitcher().set_value(this._currentMode);
    },

    /* -------------------- properties ---------------- */

    get_dialogModesSwitcher: function () {
        return this._dialogModesSwitcher;
    },
    set_dialogModesSwitcher: function (value) {
        this._dialogModesSwitcher = value;
    },

    get_pageSelector: function () {
        this._pageSelector.set_uiCulture(this.get_uiCulture());
        this._pageSelector.set_culture(this.get_culture());
        return this._pageSelector;
    },
    set_pageSelector: function (value) {
        this._pageSelector = value;
    },

    get_dialogModesSwitcher: function () {
        return this._dialogModesSwitcher;
    },
    set_dialogModesSwitcher: function (value) {
        this._dialogModesSwitcher = value;
    },

    get_webAddressField: function () {
        return this._webAddressField;
    },
    set_webAddressField: function (value) {
        this._webAddressField = value;
    },

    get_emailField: function () {
        return this._emailField;
    },
    set_emailField: function (value) {
        this._emailField = value;
    },

    get_insertLink: function () {
        return this._insertLink;
    },
    set_insertLink: function (value) {
        this._insertLink = value;
    },

    get_webAddressSection: function () {
        return this._webAddressSection;
    },
    set_webAddressSection: function (value) {
        this._webAddressSection = value;
    },

    get_pageSelectorSection: function () {
        return this._pageSelectorSection;
    },
    set_pageSelectorSection: function (value) {
        this._pageSelectorSection = value;
    },

    get_emailSection: function () {
        return this._emailSection;
    },
    set_emailSection: function (value) {
        this._emailSection = value;
    },

    // Specifies the culture that will be used on the server as CurrentThread when processing the request
    set_culture: function (culture) {
        this._culture = culture;
    },
    // Gets the culture that will be used on the server when processing the request
    get_culture: function () {
        return this._culture;
    },

    // Specifies the culture that will be used on the server as UICulture when processing the request
    set_uiCulture: function (culture) {
        this._uiCulture = culture;
    },
    // Gets the culture that will be used on the server as UICulture when processing the request
    get_uiCulture: function () {
        return this._uiCulture;
    }
};


Telerik.Sitefinity.Web.UI.LinkManagerDialog.registerClass("Telerik.Sitefinity.Web.UI.LinkManagerDialog", Telerik.Sitefinity.Web.UI.AjaxDialogBase);
