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

Type.registerNamespace("Telerik.Sitefinity.Modules.Newsletters.Web.UI.Forms");

/* TemplateForm class */

var templateForm = null;

Telerik.Sitefinity.Modules.Newsletters.Web.UI.Forms.TemplateForm = function (element) {

    this._webServiceUrl = null;
    this._templateNameField = null;
    this._saveChangesButton = null;
    this._saveChangesLabel = null;
    this._saveChangesDelegate = null;

    this._formTitleControl = null;
    this._createTemplateTitle = null;
    this._createAndAddContentText = null;
    this._editTemplateTitle = null;
    this._saveChangesText = null;
    this._saveText = null;

    this._htmlTemplateRadio = null;
    this._htmlTextPanel = null;
    this._htmlTextControl = null;

    this._plainTextTemplateRadio = null;
    this._plainTextPanel = null;
    this._plainTextControl = null;

    this._standardTemplateRadio = null;

    this._editedTemplate = null;
    this._mergeTagSelector = null;
    this._htmlMergeTagSelector = null;

    this._templateNamePanel = null;
    this._templateTypePanel = null;
    this._templateContentPanel = null;

    this._mergeTagSelectedDelegate = null;
    this._templateTypeRadioClickedDelegate = null;
    this._getTemplateSuccessDelegate = null;

    this._isTempSave = false;
    this._editorUrl = null;
    this._invariantCulture = null;
    this._createTemplateDisplayMode = null;
    this._addContentDisplayMode = null;
    this._editTemplateDisplayMode = null;
    this._editContentDisplayMode = null;
    this._displayMode = null;

    Telerik.Sitefinity.Modules.Newsletters.Web.UI.Forms.TemplateForm.initializeBase(this, [element]);
}

Telerik.Sitefinity.Modules.Newsletters.Web.UI.Forms.TemplateForm.prototype = {

    // set up 
    initialize: function () {
        templateForm = this;

        if (this._saveChangesDelegate === null) {
            this._saveChangesDelegate = Function.createDelegate(this, this._saveChanges);
        }
        $addHandler(this._saveChangesButton, 'click', this._saveChangesDelegate);

        if (this._templateTypeRadioClickedDelegate === null) {
            this._templateTypeRadioClickedDelegate = Function.createDelegate(this, this._templateTypeRadioClickedHandler);
        }
        $addHandler(this._htmlTemplateRadio, 'click', this._templateTypeRadioClickedDelegate);
        $addHandler(this._plainTextTemplateRadio, 'click', this._templateTypeRadioClickedDelegate);
        $addHandler(this._standardTemplateRadio, 'click', this._templateTypeRadioClickedDelegate);

        if (this._mergeTagSelectedDelegate === null) {
            this._mergeTagSelectedDelegate = Function.createDelegate(this, this._mergeTagSelectedHandler);
        }
        this._mergeTagSelector.add_tagSelected(this._mergeTagSelectedDelegate);
        this._htmlMergeTagSelector.add_tagSelected(this._mergeTagSelectedDelegate);

        this._getTemplateSuccessDelegate = Function.createDelegate(this, this._getTemplateSuccessHandler);

        Telerik.Sitefinity.Modules.Newsletters.Web.UI.Forms.TemplateForm.callBaseMethod(this, "initialize");
    },

    // tear down
    dispose: function () {
        if (this._saveChangesDelegate) {
            delete this._saveChangesDelegate;
        }

        if (this._templateTypeRadioClickedDelegate) {
            delete this._templateTypeRadioClickedDelegate;
        }

        if (this._mergeTagSelectedDelegate) {
            this._mergeTagSelector.remove_tagSelected(this._mergeTagSelectedDelegate);
            this._htmlMergeTagSelector.remove_tagSelected(this._mergeTagSelectedDelegate);
            delete this._mergeTagSelectedDelegate;
        }

        if (this._getTemplateSuccessDelegate) {
            delete this._getTemplateSuccessDelegate;
        }

        Telerik.Sitefinity.Modules.Newsletters.Web.UI.Forms.TemplateForm.callBaseMethod(this, "dispose");
    },

    /* *************************** public methods *************************** */

    // This method is used to load the existing template into the UI; function is used for editing templates.
    loadTemplate: function (template, templateFormDisplayMode) {
        this._displayMode = templateFormDisplayMode;

        if (template == null) {
            this.get_formTitleControl().innerHTML = this.get_createTemplateTitle();
            this.get_saveChangesLabel().innerHTML = this._createAndAddContentText;
            this.clearForm();
        } else {
            if (templateFormDisplayMode == this._editTemplateDisplayMode) {
                this._switchToEditTemplateDisplayMode();
            }
            else {
                this._switchToContentDisplayMode();
            }

            this.get_formTitleControl().innerHTML = this.get_editTemplateTitle();
            this.get_saveChangesLabel().innerHTML = this._saveChangesText;
            this._getTemplate(template.Id);
        }
    },

    getBodyText: function () {
        if ($(this._htmlTemplateRadio).is(':checked')) {
            return this.get_htmlTextControl().get_value();
        } else if ($(this._plainTextTemplateRadio).is(':checked')) {
            return this.get_plainTextControl().value;
        } else if ($(this._standardTemplateRadio).is(':checked')) {
            // there is no body text
            return null;
        }
    },

    getMessageBodyType: function () {
        if ($(this._htmlTemplateRadio).is(':checked')) {
            return 1;
        } else if ($(this._plainTextTemplateRadio).is(':checked')) {
            return 0;
        } else if ($(this._standardTemplateRadio).is(':checked')) {
            return 2;
        }
    },

    clearForm: function () {
        this.get_templateNameField().reset();
        this.get_htmlTextControl().reset();
        this.get_plainTextControl().value = '';
        $(this._standardTemplateRadio).attr('checked', 'checked');
        this._switchToCreateTemplateDisplayMode();
        this._switchToHtmlTemplate();
        this._editedTemplate = null;
    },

    /* *************************** private methods *************************** */
    _saveChanges: function () {
        if (this._isFormValid()) {
            if (this._displayMode == this._createTemplateDisplayMode) {
                if (this.getMessageBodyType() == 2) {
                    this._showPageEditor();
                }
                else {
                    this._displayMode = this._addContentDisplayMode;
                    this._switchToContentDisplayMode();
                    this.get_saveChangesLabel().innerHTML = this._saveText;
                }
            }
            else {
                this._saveTemplate();
            }
        }
    },

    _saveChanges_Success: function (sender, args) {
        if (sender._isTempSave) {
            sender._isTempSave = false;
            sender._editedTemplate = args;
            sender._openPageEditor(args.Id);
        } else {
            var editedTemplate = sender._editedTemplate;

            sender.clearForm();
            if (editedTemplate == null) {
                dialogBase.closeCreated();
            } else {
                dialogBase.closeUpdated();
            }
        }
    },

    _saveChanges_Failure: function (sender, args) {
        alert('Failure!');
    },

    _isFormValid: function () {
        return this.get_templateNameField().validate();
    },

    _saveTemplate: function () {
        var templateObject = this._getTemplateObject();
        var clientManager = new Telerik.Sitefinity.Data.ClientManager();
        var urlParams = [];
        urlParams['isPageBased'] = $(this._standardTemplateRadio).is(':checked');

        var id = (this._editedTemplate == null) ? clientManager.GetEmptyGuid() : this._editedTemplate.Id;
        var keys = [id];
        //hack: the name of the invariant culture is empty string which can not be set as a value to a header
        //because empty string the default culture, that is why a constant for the invariant culture name used
        clientManager.set_uiCulture(this._invariantCulture);
        clientManager.set_culture(this._invariantCulture);
        clientManager.InvokePut(this.get_webServiceUrl(), urlParams, keys, templateObject, this._saveChanges_Success, this._saveChanges_Failure, this);
    },

    _getTemplateObject: function () {
        var template = null;
        if (this._isTempSave) {
            template = {
                'Name': this.get_templateNameField().get_value(),
                'MessageBodyType': this.getMessageBodyType()
            };
        } else {
            template = {
                'Name': this.get_templateNameField().get_value(),
                'BodyText': this.getBodyText(),
                'MessageBodyType': this.getMessageBodyType()
            };
        }
        return template;
    },

    _templateTypeRadioClickedHandler: function () {
        if ($(this._htmlTemplateRadio).is(':checked')) {
            this._switchToHtmlTemplate();
        } else if ($(this._plainTextTemplateRadio).is(':checked')) {
            this._switchToPlainTextTemplate();
        }
    },

    _switchToHtmlTemplate: function () {
        $(this._htmlTextPanel).show();
        $(this._plainTextPanel).hide();
    },

    _switchToPlainTextTemplate: function () {
        $(this._htmlTextPanel).hide();
        $(this._plainTextPanel).show();
    },

    _switchToCreateTemplateDisplayMode: function () {
        jQuery(this._templateNamePanel).show();
        jQuery(this._templateTypePanel).show();
        jQuery(this._templateContentPanel).hide();
        jQuery("body").removeClass("sfWidgetTmpDialog");
    },

    _switchToEditTemplateDisplayMode: function () {
        jQuery(this._templateNamePanel).show();
        jQuery(this._templateTypePanel).hide();
        jQuery(this._templateContentPanel).hide();
        jQuery("body").removeClass("sfWidgetTmpDialog");
    },

    _switchToContentDisplayMode: function () {
        jQuery(this._templateNamePanel).hide();
        jQuery(this._templateTypePanel).hide();
        jQuery(this._templateContentPanel).show();
        //fixes an issue with #259996 in TWU 2014 Q1 SP1 
        this.get_htmlTextControl().get_editControl().repaint();
        jQuery("body").addClass("sfWidgetTmpDialog");
    },

    _mergeTagSelectedHandler: function (sender, args) {
        if (sender == this.get_mergeTagSelector()) {
            $(this.get_plainTextControl()).insertAtCaret(args.MergeTag);
        } else {
            this.get_htmlTextControl()._editControl.pasteHtml(args.MergeTag);
        }
    },

    _showPageEditor: function () {
        this._isTempSave = true;
        this._saveTemplate();
    },

    _openPageEditor: function (messageBodyId) {
        jQuery(window.top.document.body).addClass("sfLoadingTransition");

        var url = this._editorUrl + messageBodyId;
        url = url + "?ReturnUrl=" + window.top.location.pathname + window.top.location.search;
        window.top.location = url;
    },

    _getTemplate: function (templateId) {
        jQuery.ajax({
            type: 'GET',
            url: this.get_webServiceUrl() + '/' + templateId + '/',
            contentType: "application/json",
            processData: false,
            success: this._getTemplateSuccessDelegate
        });
    },

    _getTemplateSuccessHandler: function (result, args) {
        this._editedTemplate = result;

        this.get_templateNameField().set_value(this._editedTemplate.Name);
        if (this._editedTemplate.MessageBodyType == 0) {
            this.get_plainTextControl().value = this._editedTemplate.BodyText;
            $(this._plainTextTemplateRadio).attr('checked', 'checked');
            this._switchToPlainTextTemplate();
        } else if (this._editedTemplate.MessageBodyType == 1) {
            this.get_htmlTextControl().set_value(this._editedTemplate.BodyText);
            $(this._htmlTemplateRadio).attr('checked', 'checked');
            this._switchToHtmlTemplate();
        } else if (this._editedTemplate.MessageBodyType == 2) {
            $(this._standardTemplateRadio).attr('checked', 'checked');
        } else {
            alert('Not supported!');
        }
    },

    /* *************************** properties *************************** */
    get_formTitleControl: function () {
        return this._formTitleControl;
    },
    set_formTitleControl: function (value) {
        this._formTitleControl = value;
    },
    get_createTemplateTitle: function () {
        return this._createTemplateTitle;
    },
    set_createTemplateTitle: function (value) {
        this._createTemplateTitle = value;
    },
    get_editTemplateTitle: function () {
        return this._editTemplateTitle;
    },
    set_editTemplateTitle: function (value) {
        this._editTemplateTitle = value;
    },
    get_webServiceUrl: function () {
        return this._webServiceUrl;
    },
    set_webServiceUrl: function (value) {
        this._webServiceUrl = value;
    },
    get_templateNameField: function () {
        return this._templateNameField;
    },
    set_templateNameField: function (value) {
        this._templateNameField = value;
    },
    get_saveChangesButton: function () {
        return this._saveChangesButton;
    },
    set_saveChangesButton: function (value) {
        this._saveChangesButton = value;
    },
    get_saveChangesLabel: function () {
        return this._saveChangesLabel;
    },
    set_saveChangesLabel: function (value) {
        this._saveChangesLabel = value;
    },
    get_htmlTemplateRadio: function () {
        return this._htmlTemplateRadio;
    },
    set_htmlTemplateRadio: function (value) {
        this._htmlTemplateRadio = value;
    },
    get_htmlTextPanel: function () {
        return this._htmlTextPanel;
    },
    set_htmlTextPanel: function (value) {
        this._htmlTextPanel = value;
    },
    get_htmlTextControl: function () {
        return this._htmlTextControl;
    },
    set_htmlTextControl: function (value) {
        this._htmlTextControl = value;
    },
    get_plainTextTemplateRadio: function () {
        return this._plainTextTemplateRadio;
    },
    set_plainTextTemplateRadio: function (value) {
        this._plainTextTemplateRadio = value;
    },
    get_plainTextPanel: function () {
        return this._plainTextPanel;
    },
    set_plainTextPanel: function (value) {
        this._plainTextPanel = value;
    },
    get_plainTextControl: function () {
        return this._plainTextControl;
    },
    set_plainTextControl: function (value) {
        this._plainTextControl = value;
    },
    get_standardTemplateRadio: function () {
        return this._standardTemplateRadio;
    },
    set_standardTemplateRadio: function (value) {
        this._standardTemplateRadio = value;
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
    get_templateNamePanel: function () {
        return this._templateNamePanel;
    },
    set_templateNamePanel: function (value) {
        this._templateNamePanel = value;
    },
    get_templateTypePanel: function () {
        return this._templateTypePanel;
    },
    set_templateTypePanel: function (value) {
        this._templateTypePanel = value;
    },
    get_templateContentPanel: function () {
        return this._templateContentPanel;
    },
    set_templateContentPanel: function (value) {
        this._templateContentPanel = value;
    }
};

Telerik.Sitefinity.Modules.Newsletters.Web.UI.Forms.TemplateForm.registerClass('Telerik.Sitefinity.Modules.Newsletters.Web.UI.Forms.TemplateForm', Telerik.Sitefinity.Web.UI.AjaxDialogBase);
