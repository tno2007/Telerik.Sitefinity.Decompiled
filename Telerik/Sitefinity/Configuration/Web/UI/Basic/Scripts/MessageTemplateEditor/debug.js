Type.registerNamespace("Telerik.Sitefinity.Configuration.Web.UI.Basic");

Telerik.Sitefinity.Configuration.Web.UI.Basic.MessageTemplateEditor = function (element) {
    Telerik.Sitefinity.Configuration.Web.UI.Basic.MessageTemplateEditor.initializeBase(this, [element]);
    this._window = null;
    this._windowBody = null;
    this._windowBodyClickDelegate = null;

    this._subjectTextBox = null;
    this._subjectTextBoxClickDelegate = null;
    this._subjectFocus = false;

    this._senderTextBox = null;
    this._senderNameTextBox = null;

    this._templateEditor = null;

    this._dropDownListInserts = null;

    this._insertDynamicDataButton = null;
    this._insertDynamicDataDelegate = null;

    this._backButton = null;
    this._backButtonDelegate = null;

    this._titleLabel = null;
    this._usedInLabel = null;
    this._lastModifiedLabel = null;
    this._lastModifiedByLabel = null;

    this._topCommandBar = null;
    this._bottomCommandBar = null;
    this._commandDelegate = null;

    this._serviceUrl = null;

    this._dataItem = null;
    this._successCallback = null;
    this._options = null;

    this._restoreToOriginalPromptDialog = null;
    this._restoreToOriginalPromptDelegate = null;

    this._sendTestEmailDialog = null;

    this._isRestoreOperation = false;
    this._dirtyItem = null;

    this._placeholderSeparator = "------------------";
}

Telerik.Sitefinity.Configuration.Web.UI.Basic.MessageTemplateEditor.prototype =
{
    /* --------------------  set up and tear down ----------- */
    initialize: function () {
        Telerik.Sitefinity.Configuration.Web.UI.Basic.MessageTemplateEditor.callBaseMethod(this, "initialize");

        jQuery(document).ready(this.onReady.bind(this));

        this._insertDynamicDataDelegate = Function.createDelegate(this, this._insertDynamicDataHandler);
        $addHandler(this.get_insertDynamicDataButton(), "click", this._insertDynamicDataDelegate);

        this._backButtonDelegate = Function.createDelegate(this, this._backButtonHandler);
        $addHandler(this.get_backButton(), "click", this._backButtonDelegate);
        
        this._windowBodyClickDelegate = Function.createDelegate(this, this._windowBodyClickHandler);
        $addHandler(this.get_windowBody(), "click", this._windowBodyClickDelegate);

        this._subjectTextBoxClickDelegate = Function.createDelegate(this, this._subjectTextBoxClickHandler);
        $addHandler(this.get_subjectTextBox().get_textElement(), "click", this._subjectTextBoxClickDelegate);

        this._commandDelegate = Function.createDelegate(this, this._commandHandler);
        this._topCommandBar.add_command(this._commandDelegate);
        this._bottomCommandBar.add_command(this._commandDelegate);

        this._restoreToOriginalPromptDelegate = Function.createDelegate(this, this._restoreHandler);
        this._restoreToOriginalPromptDialog.add_command(this._restoreToOriginalPromptDelegate);
    },
    dispose: function () {
        Telerik.Sitefinity.Configuration.Web.UI.Basic.MessageTemplateEditor.callBaseMethod(this, "dispose");

        if (this._insertDynamicDataDelegate) {
            $removeHandler(this.get_insertDynamicDataButton(), "click", this._insertDynamicDataDelegate);
            delete this._insertDynamicDataDelegate;
        }

        if (this._backButtonDelegate) {
            $removeHandler(this.get_backButton(), "click", this._backButtonDelegate);
            delete this._backButtonDelegate;
        }

        if (this._windowBodyClickDelegate) {
            $removeHandler(this.get_windowBody(), "click", this._windowBodyClickDelegate);
            delete this._windowBodyClickDelegate;
        }

        if (this._subjectTextBoxClickDelegate) {
            $removeHandler(this.get_subjectTextBox().get_textElement(), "click", this._subjectTextBoxClickDelegate);
            delete this._subjectTextBoxClickDelegate;
        }

        if (this._commandDelegate) {
            if (this._topCommandBar) {
                this._topCommandBar.remove_command(this._commandDelegate);
            }
            if (this._bottomCommandBar) {
                this._bottomCommandBar.remove_command(this._commandDelegate);
            }
            delete this._commandDelegate;
        }

        if (this._restoreToOriginalPromptDelegate) {
            if (this._restoreToOriginalPromptDialog) {
                this._restoreToOriginalPromptDialog.remove_command(this._restoreToOriginalPromptDelegate);
            }

            delete this._restoreToOriginalPromptDelegate;
        }
    },
    onReady: function () {
        this._window = jQuery(this.get_windowBody()).kendoWindow({
            title: false,
            visible: false,
            animation: false,
            actions: [],
            modal: true,
        })
        .data("kendoWindow")
        .maximize();

        jQuery(this.get_windowBody()).kendoValidator(
            {
                errorTemplate: '<span class="sfError">#=message#</span>'
            });
    },
    open: function (dataItem, successCallback, options) {

        this.get_subjectTextBox().clearViolationMessage();
        this.get_senderTextBox().clearViolationMessage();
        this.get_senderNameTextBox().clearViolationMessage();

        this._window.center().open();

        if(dataItem) {
            this._isRestoreOperation = false;

            this._dataItem = dataItem;
            this._successCallback = successCallback;
            this._options = options;

            this._setLabels();

            if (this._dataItem.IsModified) {
                this.get_subjectTextBox().set_value(this._dataItem.VariationSubject);
                this.get_senderTextBox().set_value(this._dataItem.VariationSenderEmailAddress);
                this.get_senderNameTextBox().set_value(this._dataItem.VariationSenderName);
                this.get_templateEditor().set_value(this._dataItem.VariationBodyHtml);
            } else {
                this.get_subjectTextBox().set_value(this._dataItem.Subject);
                this.get_senderTextBox().set_value(this._dataItem.SenderEmailAddress);
                this.get_senderNameTextBox().set_value(this._dataItem.SenderName);
                this.get_templateEditor().set_value(this._dataItem.BodyHtml);
            }

            if (this._dataItem.UsedIn) {
                this.get_usedInLabel().innerText = this._dataItem.UsedIn;
            }

            jQuery("#lastModifiedSection").addClass("sfDisplayNone");
            if (this._dataItem.LastModified && this._dataItem.LastModifiedBy) {
                this.get_lastModifiedLabel().innerText = this._dataItem.LastModified;
                this.get_lastModifiedByLabel().innerText = this._dataItem.LastModifiedBy;
                jQuery("#lastModifiedSection").removeClass("sfDisplayNone");

            }

            this._clearDropDownListPlaceholders();
            this._appendDropDownListPlaceholders(this._dataItem.DynamicPlaceholderFields);
            this._appendDropDownListPlaceholders(this._dataItem.PlaceholderFields);

            this._dirtyItemUpdate();
            this._resizeEditor();
            this._setCorrectZIndex(this._window);
        }
    },
    close: function () {
        this._window.close();
    },
    closeWithCallback: function() {
        this.close();

        if (this._successCallback) {
            this._successCallback();
        }
    },
    saveChanges: function() {
        if (!this._dirtyItemChanged() && !this._isRestoreOperation) {
            // Save operation was called and there are no changes to the field values

            this.closeWithCallback();

            return;
        }

        if (!this._validate()) {
            return;
        }

        var that = this;

        var url = this.get_serviceUrl() + "/update_variation";

        if (this._isRestoreOperation && this._options && this._options.removeVariationOnRestore && !this._dirtyItemChanged()) {
            url = this.get_serviceUrl() + "/delete_variation";
        }

        if (this._dirtyItemChanged()) {
            // Save operation was called and the field values were modified

            this._dataItem.Subject = this.get_subjectTextBox().get_value();
            this._dataItem.VariationSubject = this.get_subjectTextBox().get_value();
            this._dataItem.BodyHtml = this.get_templateEditor().get_value();
            this._dataItem.VariationBodyHtml = this.get_templateEditor().get_value();
            this._dataItem.SenderEmailAddress = this.get_senderTextBox().get_value();
            this._dataItem.VariationSenderEmailAddress = this.get_senderTextBox().get_value();
            this._dataItem.SenderName = this.get_senderNameTextBox().get_value();
            this._dataItem.VariationSenderName = this.get_senderNameTextBox().get_value();
        } 

        jQuery.ajax({
            type: "PUT",
            data: JSON.stringify(this._dataItem),
            url: url,
            contentType: "application/json; charset=utf-8",
            dataType: "json"
        })
        .done(function (result) {
            that.closeWithCallback();
        })
        .fail(function (err) {
            alert(err);
        });
    },
    sendTest: function () {

        var testEmailItem = {
            offsetTop: this._currentScrollPosition() + 80,
            ModuleName: this._dataItem.UsedIn,
            DynamicPlaceholderFields: this._dataItem.DynamicPlaceholderFields,
            PlaceholderFields: this._dataItem.PlaceholderFields,
            SenderVerified: this._dataItem.SenderVerified
        };

        if (this._dirtyItemChanged()) {
            testEmailItem.Subject = this.get_subjectTextBox().get_value();
            testEmailItem.BodyHtml = this.get_templateEditor().get_value();
            testEmailItem.SenderEmailAddress = this.get_senderTextBox().get_value();
            testEmailItem.SenderName = this.get_senderNameTextBox().get_value();

        } else {

            if (!this._isRestoreOperation && this._dataItem.IsModified) {
                testEmailItem.Subject = this._dataItem.VariationSubject;
                testEmailItem.BodyHtml = this._dataItem.VariationBodyHtml;
                testEmailItem.SenderEmailAddress = this._dataItem.VariationSenderEmailAddress;
                testEmailItem.SenderName = this._dataItem.VariationSenderName;

            } else {
                testEmailItem.Subject = this._dataItem.Subject;
                testEmailItem.BodyHtml = this._dataItem.BodyHtml;
                testEmailItem.SenderEmailAddress = this._dataItem.SenderEmailAddress;
                testEmailItem.SenderName = this._dataItem.SenderName;
            }
        }

        this.get_sendTestEmailDialog().open(testEmailItem, function() {}, function () {});
    },
    reset: function () {
        if (this._options && this._options.labels && this._options.labels.restoreDescription) {
            this.get_restoreToOriginalPromptDialog().show_prompt(null, this._options.labels.restoreDescription);

        } else {
            this.get_restoreToOriginalPromptDialog().show_prompt();
        }

    },
    _currentScrollPosition: function () {
        if (window && window.pageYOffset) {
            return window.pageYOffset;
        }
        if (document && document.documentElement && document.documentElement.scrollTop) {
            return document.documentElement.scrollTop;
        }
        if (document && document.body && document.body.scrollTop) {
            return document.body.scrollTop;
        }

        return 0;
    },
    _clearDropDownListPlaceholders : function() {
        $(this.get_dropDownListInserts()).empty();
    },
    _appendDropDownListPlaceholders: function(placeholders) {
        if (placeholders && placeholders.length > 0) {
            if ($(this.get_dropDownListInserts()).find("option").length > 0) {
                var separatorOption = new Option(this._placeholderSeparator, this._placeholderSeparator);
                separatorOption.disabled = true;
                $(this.get_dropDownListInserts()).append(separatorOption);
            }

            var that = this;
            
            $.each(placeholders, function(index, placeholder) { 
                if (placeholder.DisplayName && placeholder.FieldName) {
                    $(that.get_dropDownListInserts()).append(new Option(placeholder.DisplayName, placeholder.FieldName));
                }
            });
        }
    },
    _insertDynamicDataHandler: function(sender, args) {
        sender.stopPropagation();

        var selectedIndex = this.get_dropDownListInserts().selectedIndex;
        var selectedValue = this.get_dropDownListInserts().options[selectedIndex].value;
        
        if(!selectedValue || selectedValue === this._placeholderSeparator) {
            return;
        }

        var placeholderValue = "{|" + selectedValue + "|}";

        if(this._subjectFocus) {
            var textValue = this.get_subjectTextBox().get_value();
            var caretStart = this.get_subjectTextBox().get_textBoxElement().selectionStart;
            var caretEnd = this.get_subjectTextBox().get_textBoxElement().selectionEnd;

            this.get_subjectTextBox().set_value(textValue.substring(0, caretStart) + placeholderValue + textValue.substring(caretEnd));

            caretStart = caretStart + placeholderValue.length;
            
            this.get_subjectTextBox().get_textBoxElement().select();

            this.get_subjectTextBox().get_textBoxElement().selectionStart = caretStart;
            this.get_subjectTextBox().get_textBoxElement().selectionEnd = caretStart;

        } else {
            this.get_templateEditor().get_editControl().pasteHtml(placeholderValue);
        }
    },

    _subjectTextBoxClickHandler: function(sender, args) {
        if(sender.target.id.startsWith(this.get_subjectTextBox().get_id())) {
           this._subjectFocus = true;
        }
        sender.stopPropagation();
    },
    _windowBodyClickHandler: function(sender, args) {
        if(!sender.target.id.startsWith(this.get_subjectTextBox().get_id()) || 
            !sender.target.id.startsWith(this.get_insertDynamicDataButton().id)) {
            this._subjectFocus = false;
        }

        sender.stopPropagation();
    },
    _restoreHandler: function (sender, args) {
        if (args._commandName == "restore") {

            if(this._dataItem) {

                this._isRestoreOperation = true;

                this.get_subjectTextBox().set_value(this._dataItem.Subject);
                this.get_templateEditor().set_value(this._dataItem.BodyHtml);
                this.get_senderTextBox().set_value(this._dataItem.SenderEmailAddress);
                this.get_senderNameTextBox().set_value(this._dataItem.SenderName);

                this._dirtyItemUpdate();

            }
        }
        else if (args._commandName == "cancel") {
            return false;
        }
    },
    _commandHandler: function (sender, args) {
        switch (args.get_commandName()) {
            case "save":
                this.saveChanges();
                break;
            case "test":
                this.sendTest();
                break;
            case "cancel":
                this.close();
                break;
            case "reset":
                this.reset();
                break;
            default: 
                break;
        }
    },
    _backButtonHandler: function(sender, args) {
        this.close();
    },
    _dirtyItemUpdate: function() {
        this._dirtyItem = {
            Subject : this.get_subjectTextBox().get_value(),
            BodyHtml : this.get_templateEditor().get_value(),
            SenderEmailAddress : this.get_senderTextBox().get_value(),
            SenderName: this.get_senderNameTextBox().get_value(),
        };
    },
    _dirtyItemChanged: function() {
        return !(this._dirtyItem 
            && this._dirtyItem.BodyHtml === this.get_templateEditor().get_value() 
            && this._dirtyItem.Subject === this.get_subjectTextBox().get_value() 
            && this._dirtyItem.SenderEmailAddress === this.get_senderTextBox().get_value()
            && this._dirtyItem.SenderName === this.get_senderNameTextBox().get_value());
    },
    _setLabels: function () {
        if (this._options && this._options.labels) {

            if(this._options.labels.back && this.get_backButton()) {
                this.get_backButton().text = this._options.labels.back;
            }

            if(this._options.labels.title && this.get_titleLabel()) {
                this.get_titleLabel().innerText = this._options.labels.title;
            }
        }
    },
    _validate: function () {
        var isValid = true;
        
        if (!this.get_subjectTextBox().validate()){
            isValid = false;
        }
        if (!this.get_senderTextBox().validate()){
            isValid = false;
        }
        if (!this.get_senderNameTextBox().validate()) {
            isValid = false;
        }
        return isValid;
    },
    _resizeEditor: function () {
        var editorWidth = $("#" + this.get_templateEditor().get_id()).width() + 4;
        var editorHeight = 550;

        this.get_templateEditor().get_editControl().setSize(editorWidth, editorHeight);
    },
    _setCorrectZIndex: function (currentWindow) {
        // Solution for setting new z-index for every kendo Window: https://www.telerik.com/forums/window-zindex-problem
        // Needed because we open editor in modal window and then using Rad Editor to open another modal windows inside it
        var zIndex  = 1000;
        jQuery(".k-window").each(function(i, elem){
            jQuery(elem).css("z-index", ++zIndex);
        });

        var currentWindowZIndex = currentWindow.wrapper.css("zIndex");
        var currentWindowOverlay = currentWindow.wrapper.siblings(".k-overlay");
        
        currentWindowOverlay.css("zIndex", currentWindowZIndex);
    },
    get_windowBody: function () {
        return this._windowBody;
    },
    set_windowBody: function (value) {
        this._windowBody = value;
    },
    get_subjectTextBox: function () {
        return this._subjectTextBox;
    },
    set_subjectTextBox: function (value) {
        this._subjectTextBox = value;
    },
    get_templateEditor: function () {
        return this._templateEditor;
    },
    set_templateEditor: function (value) {
        this._templateEditor = value;
    },
    get_dropDownListInserts: function () {
        return this._dropDownListInserts;
    },
    set_dropDownListInserts: function (value) {
        this._dropDownListInserts = value;
    },
    get_insertDynamicDataButton: function () {
        return this._insertDynamicDataButton;
    },
    set_insertDynamicDataButton: function (value) {
        this._insertDynamicDataButton = value;
    },
    get_serviceUrl: function () {
        return this._serviceUrl;
    },
    set_serviceUrl: function (value) {
        this._serviceUrl = value;
    },
    get_topCommandBar: function () {
        return this._topCommandBar;
    },
    set_topCommandBar: function (value) {
        this._topCommandBar = value;
    },
    get_bottomCommandBar: function () {
        return this._bottomCommandBar;
    },
    set_bottomCommandBar: function (value) {
        this._bottomCommandBar = value;
    },
    get_restoreToOriginalPromptDialog: function () {
        return this._restoreToOriginalPromptDialog;
    },
    set_restoreToOriginalPromptDialog: function (value) {
        this._restoreToOriginalPromptDialog = value;
    },        
    get_sendTestEmailDialog: function () {
        return this._sendTestEmailDialog;
    },
    set_sendTestEmailDialog: function (value) {
        this._sendTestEmailDialog = value;
    },
    get_senderTextBox: function () {
        return this._senderTextBox;
    },
    set_senderTextBox: function (value) {
        this._senderTextBox = value;
    },
    get_senderNameTextBox: function () {
        return this._senderNameTextBox;
    },
    set_senderNameTextBox: function (value) {
        this._senderNameTextBox = value;
    },
    get_backButton: function () {
        return this._backButton;
    },
    set_backButton: function (value) {
        this._backButton = value;
    },
    get_titleLabel: function () {
        return this._titleLabel;
    },
    set_titleLabel: function (value) {
        this._titleLabel = value;
    },
    get_usedInLabel: function () {
        return this._usedInLabel;
    },
    set_usedInLabel: function (value) {
        this._usedInLabel = value;
    },
    get_lastModifiedLabel: function () {
        return this._lastModifiedLabel;
    },
    set_lastModifiedLabel: function (value) {
        this._lastModifiedLabel = value;
    },
    get_lastModifiedByLabel: function () {
        return this._lastModifiedByLabel;
    },
    set_lastModifiedByLabel: function (value) {
        this._lastModifiedByLabel = value;
    }
}

Telerik.Sitefinity.Configuration.Web.UI.Basic.MessageTemplateEditor.registerClass("Telerik.Sitefinity.Configuration.Web.UI.Basic.MessageTemplateEditor", Sys.UI.Control);