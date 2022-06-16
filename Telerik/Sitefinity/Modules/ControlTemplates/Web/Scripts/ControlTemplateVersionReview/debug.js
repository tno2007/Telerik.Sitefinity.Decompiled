Type.registerNamespace("Telerik.Sitefinity.Modules.ControlTemplates.Web.UI");

var ControlTemplateVersionReview = null;

Telerik.Sitefinity.Modules.ControlTemplates.Web.UI.ControlTemplateVersionReview = function (element) {
    Telerik.Sitefinity.Modules.ControlTemplates.Web.UI.ControlTemplateVersionReview.initializeBase(this, [element]);
    this._moduleTitle = null;
    this._expandCommonPropertiesButton = null;
    this._expandOtherPropertiesButton = null;
    this._commonPropertiesContainer = null;
    this._otherPropertiesContainer = null;
    this._topCommandBar = null;
    this._bottomCommandBar = null;
    this._binder = null;
    this._commonPropertiesItemsList = null;
    this._otherPropertiesItemsList = null;
    this._controlTypesList = null;
    this._templateDataField = null;
    this._fieldControlIds = [];
    this._typeMappings = [];
    this._areaNameMappings = [];
    this._templateCondition = null;
    this._blankDataItem = null;
    this._providerName = null;
    this._restorePrompt = null;
    this._messageControl = null;
    this._labelManager = null;
    this._controlTypeSelector = null;
    this._itemTypesNamesSelector = null;
    this._itemTypeFullName = null;

    this._isDirty = false;
    this._isNew = false;
    this._otherPropertiesContainerExpanded = false;
    this._controlType = null;

    this._pageLoadDelegate = null;
    this._commandDelegate = null;
    this._editorSavedDelegate = null;
    this._valueChangedDelegate = null;
    this._dialogClosedDelegate = null;
    this._toggleCommonPropertiesDelegate = null;
    this._toggleOtherPropertiesDelegate = null;
    this._restoreConfirmedDelegate = null;
    this._restoreSuccessDelegate = null;
    this._restoreErrorDelegate = null;
    this._getItemTypesSuccessDelegate = null;
    this._getItemTypesFailureDelegate = null;
    this._itemTypeFullNameSelectionChangedDelegate = null;
    this._itemTypeFullNameField = null;
    this._itemTypeTitleField = null;
    this._moduleName = null;
    this._friendlyControlName = null;

    // supported commands
    this._createCommandName = null;
    this._saveCommandName = null;
    this._cancelCommandName = null;
    this._previewCommandName = null;
    this._restoreCommandName = null;
    this._codeMirror = null;

    this._params = null;
    this._key = null;
    this._versionInfo = null;
    this._historyServiceUrl = null;
    this._itemsBase = null;

     
}

Telerik.Sitefinity.Modules.ControlTemplates.Web.UI.ControlTemplateVersionReview.prototype = {

    // ------------------------------------- Initialization -------------------------------------
    initialize: function () {
        Telerik.Sitefinity.Modules.ControlTemplates.Web.UI.ControlTemplateVersionReview.callBaseMethod(this, "initialize");

        ControlTemplateVersionReview = this;
        //jQuery("#pageTitle").html(bindObject.PageTitle);
        //jQuery("#versionNumber").html(bindObject.VersionInfo.Version);
        //if (this._fieldControlIds) {
        //    this._fieldControlIds = Sys.Serialization.JavaScriptSerializer.deserialize(this._fieldControlIds);
        //}
        //if (this._typeMappings) {
        //    this._typeMappings = Sys.Serialization.JavaScriptSerializer.deserialize(this._typeMappings);
        //}
        //if (this._areaNameMappings) {
        //    this._areaNameMappings = Sys.Serialization.JavaScriptSerializer.deserialize(this._areaNameMappings);
        //}
        //if (this._blankDataItem) {
        //    this._blankDataItem = Sys.Serialization.JavaScriptSerializer.deserialize(this._blankDataItem);
        //    if (this._blankDataItem.hasOwnProperty("Id")) {
        //        delete this._blankDataItem.Id;
        //    }
        //    this._binder.setBlankDataItem(this._blankDataItem);
        //}

        this._pageLoadDelegate = Function.createDelegate(this, this._pageLoadHandler);
        Sys.Application.add_load(this._pageLoadDelegate);

        this._commandDelegate = Function.createDelegate(this, this._commandHandler);
        //this._topCommandBar.add_command(this._commandDelegate);
        this._bottomCommandBar.add_command(this._commandDelegate);

        this._editorSavedDelegate = Function.createDelegate(this, this._editorSavedHandler);
        this._binder.add_onSaved(this._editorSavedDelegate);
        
        
    },

    dispose: function () {
        if (this._pageLoadDelegate) {
            Sys.Application.remove_load(this._pageLoadDelegate);
            delete this._pageLoadDelegate;
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
        if (this._editorSavedDelegate) {
            if (this._binder) {
                this._binder.remove_onSaved(this._editorSavedDelegate);
            }
            delete this._editorSavedDelegate;
        }
        if (this._valueChangedDelegate) {
            if (this._controlTypesList && this._controlTypesList.get_choiceElement()) {
                $removeHandler(this._controlTypesList.get_choiceElement(), "change", this._valueChangedDelegate);
            }
            delete this._valueChangedDelegate;
        }
        if (this._dialogClosedDelegate) {
            if (this._commonPropertiesItemsList) {
                this._commonPropertiesItemsList.remove_dialogClosed(this._dialogClosedDelegate);
            }
            if (this._otherPropertiesItemsList) {
                this._otherPropertiesItemsList.remove_dialogClosed(this._dialogClosedDelegate);
            }
            delete this._dialogClosedDelegate;
        }
        if (this._toggleCommonPropertiesDelegate) {
            if (this._expandCommonPropertiesButton) {
                $removeHandler(this._expandCommonPropertiesButton, "click", this._toggleCommonPropertiesDelegate);
            }
            delete this._toggleCommonPropertiesDelegate;
        }
        if (this._toggleOtherPropertiesDelegate) {
            if (this._expandOtherPropertiesButton) {
                $removeHandler(this._expandOtherPropertiesButton, "click", this._toggleOtherPropertiesDelegate);
            }
            delete this._toggleOtherPropertiesDelegate;
        }
        this.get_restorePrompt().remove_command(this._restoreConfirmedDelegate);
        if (this._restoreConfirmedDelegate) {
            delete this._restoreConfirmedDelegate;
        }
        if (this._restoreSuccessDelegate) {
            delete this._restoreSuccessDelegate;
        }
        if (this._restoreErrorDelegate) {
            delete this._restoreErrorDelegate;
        }
        if (this._itemTypeFullNameSelectionChangedDelegate) {
            delete this._itemTypeFullNameSelectionChangedDelegate;
        }
        if (this._getItemTypesSuccessDelegate) {
            delete this._getItemTypesSuccessDelegate;
        }
        if (this._getItemTypesFailureDelegate) {
            delete this._getItemTypesFailureDelegate;
        }
        Telerik.Sitefinity.Modules.ControlTemplates.Web.UI.ControlTemplateVersionReview.callBaseMethod(this, "dispose");
    },

    // ------------------------------------- Public methods -------------------------------------

    createEditor: function (commandName, dataItem, self, dialog, params, key) {
       
        this.reset();

        

        if (dataItem) {
            this.dataBind(dataItem, key,params);
        }
        else if (params.TemplateId) {
            this._isNew = false;
            this.dataBind(null, { Id: params.TemplateId });
        }
        

        this._params = params;
        this._key = key;
        this._itemsBase = self;
    },

    saveChanges: function () {


        //var editCommand = "edit";
        //this._itemsBase.executeItemCommand("edit", this._binder.get_dataItem(), this._key, this._params);

        var editCommand = "edit";

        var dialogManager = window.top.GetDialogManager();
        var dialog = dialogManager.getDialogByName(editCommand);
        var context = {
            commandName: editCommand,
            dataItem: this._binder.get_dataItem(),
            itemsList: this._itemsBase,
            dialog: dialog,
            params: this._params,
            key: this._key
        };
        dialogManager.openDialog(editCommand, this, context);

           
    },

    restoreTemplate: function () {
        this.get_restorePrompt().show_prompt();
    },

    dataBind: function (dataItem, key, params) {
        var clientManager = this._binder.get_manager();
        var urlParams = [];
        if (this._providerName != null) {
            urlParams["providerName"] = this._providerName;
        }
        if (params) {

            if (params.VersionId)
                urlParams["VersionId"] = params.VersionId;
        }
        var keys = [];
        if (key) {
            for (var keyName in key) {
                if (typeof key[keyName] === "string") {
                    keys.push(key[keyName]);
                }
            }
        }
        clientManager.InvokeGet(this._binder.get_serviceBaseUrl(), urlParams, keys, this._dataBindSuccess, this._dataBindFailure, this);
        clientManager.InvokeGet(this._binder.get_serviceBaseUrl() + "/versions/", urlParams, keys, this._dataBindVersionInfoSuccess, this._dataBindFailure, this);

    },

    pasteHtml: function (jQueryElement, value) {
        // IE support
        if (document.selection) {
            jQueryElement.insertAtCaretPos(value);
        }
        else {
            var selection = jQueryElement.getSelection();
            var start = selection.start;
            var end = selection.end;

            var startExpression = jQueryElement.val().substring(0, start);
            var endExpression = jQueryElement.val().substring(end, jQueryElement.val().length);
            jQueryElement.val(startExpression + value + endExpression);
        }

        jQueryElement.setCaretPos(start + value.length + 1);
    },

    pasteInCodeMirror: function (value) {
        this.get_CodeMirror().replaceSelection(value);
    },

    reset: function () {
        //jQuery(this._commonPropertiesContainer).show();
        //jQuery(this._expandCommonPropertiesButton).removeClass("sfCollapsed");
        //jQuery(this._otherPropertiesContainer).hide();
        //jQuery(this._expandOtherPropertiesButton).addClass("sfCollapsed");
        //this._otherPropertiesContainerExpanded = false;
        //this._messageControl.hide();
    },

    // ------------------------------------- Event handlers -------------------------------------

    // fired when page has been loaded
    _pageLoadHandler: function (sender, args) {
        this._binder.set_fieldControlIds(this._fieldControlIds);

        this._codeMirror = CodeMirror.fromTextArea(this._templateDataField.get_textElement(), {
            //value: this._templateDataField.get_textElement().innerHTML,
            mode: "htmlmixed",
            lineNumbers: true,
            matchBrackets: true,
            tabMode: "classic",
            readOnly:true
            // onChange: function (n) {
            //   $('#code_002').html(editor[1].mirror.getCode());
            // } 
        });
    },

    _commandHandler: function (sender, args) {
        switch (args.get_commandName()) {
            case this._createCommandName:
                this.saveChanges();
                break;
            case this._saveCommandName:
                this.saveChanges();
                break;
            case this._cancelCommandName:
                this.close();
                break;
            case this._deleteCommandName:
                this._deleteVersion();
                break;

        }
    },

    _deleteVersion: function () {
        if (this._versionInfo.VersionInfo.IsLastPublishedVersion) {
            this.get_messageControl().showNegativeMessage(this.get_labelManager().getLabel("VersionResources", "CannotDeleteLastPublishedVersion"));
            return;
        }
        var dataItem = this._binder.get_dataItem();
        var versionId = this._params.VersionId;
        var keys = [];
        keys.push(versionId);
        var urlParams = [];
        var clientManager = new Telerik.Sitefinity.Data.ClientManager();

        clientManager.InvokeDelete(this._historyServiceUrl, urlParams, keys, this._editorSavedDelegate, this._dataBindFailure, this);

    
    },

    // fired when field controls binder has successfully saved the item
    _editorSavedHandler: function () {
        this._closeDialog();
    },

    // called when data binding was successful
    _dataBindSuccess: function (sender, result) {
        this.Caller._binder.BindItem(result);
        this.Caller._setModuleTitle(result.Item.AreaName);

        var valueToSet = result.Item.Data;
        if (valueToSet == null) {
            valueToSet = "";
        }
        this.Caller.get_CodeMirror().setValue(valueToSet);
        this.Caller._controlType = result.Item.ControlType;
        this.Caller._friendlyControlName = result.Item.FriendlyControlName;
        //this.Caller._setAppliedToTemplate();

        jQuery("#itemTitle").html(result.Item.Name);
        jQuery("#itemTitleHeading").html(result.Item.Name);
        
   },
    
    // called when data binding was successful
    _dataBindVersionInfoSuccess: function (sender, result) {
        ControlTemplateVersionReview._versionInfo = result.Item;
        noteControl.set_dataItemContext(result.Item);

       
        jQuery("#versionNumber").html(result.Item.VersionInfo.Version);
        jQuery("#modifiedTime").html(result.Item.VersionInfo.LastModified.sitefinityLocaleFormat('dd MMM, yyyy'));
        jQuery("#modifiedByUser").html(result.Item.VersionInfo.CreatedByUserName);


        if (result.Item.VersionInfo.PreviousId)
            $("#prevVersionButton, #sfSepPrevNextVersion").removeClass("sfVisibilityHidden");
        else
            $("#prevVersionButton, #sfSepPrevNextVersion").addClass("sfVisibilityHidden");
        
        if (result.Item.VersionInfo.NextId)
            $("#nextVersionButton, #sfSepPrevNextVersion").removeClass("sfVisibilityHidden");
        else
            $("#nextVersionButton, #sfSepPrevNextVersion").addClass("sfVisibilityHidden");

    },

    // called when data binding was not successful
    _dataBindFailure: function (error, caller) {
        alert(error.Detail);
    },

    _valueChangedHandler: function (sender, args) {
        this._setModuleTitle();
        this._dataBindPropertiesItemsList(this._commonPropertiesItemsList);
       
    },

    _dialogClosedHandler: function (sender, args) {
        var tag = args;
        this.pasteInCodeMirror(tag);
    },

    _toggleCommonProperties: function (sender, args) {
        this._toggle(jQuery(this._commonPropertiesContainer));
        jQuery(this._expandCommonPropertiesButton).toggleClass("sfCollapsed");
    },

    _toggleOtherProperties: function (sender, args) {
        this._toggle(jQuery(this._otherPropertiesContainer));
        jQuery(this._expandOtherPropertiesButton).toggleClass("sfCollapsed");
        if (!this._otherPropertiesContainerExpanded) {
            this._dataBindPropertiesItemsList(this._otherPropertiesItemsList);
        }
        this._otherPropertiesContainerExpanded = true;
    },

    _restoreConfirmedHandler: function (sender, args) {
        var commandName = args.get_commandName();
        if (commandName === "restore") {
            var manager = this._binder.get_manager();
            var params = this._binder.get_urlParams();
            var url = this._binder.get_serviceBaseUrl() + "/restore";
            var context = {};
            context.Item = this._binder.get_dataItem();
            params["Id"] = context.Item.Id;
            var keys = [];
            keys.push(context.Item.Id);
            manager.InvokePut(url, params, keys, context, this._restoreSuccessDelegate, this._restoreErrorDelegate, this);
        }
    },

    _restoreSuccessHandler: function (caller, result) {
        this.dataBind(null, { Id: result.Item.Id });
        var message = this.get_labelManager().getLabel("ControlTemplatesResources", "RestoreSuccessMessage");
        this.get_messageControl().showPositiveMessage(message);
    },

    _restoreErrorHandler: function (error) {
        var message = this.get_labelManager().getLabel("ControlTemplatesResources", "RestoreErrorMessage");
        this.get_messageControl().showNegativeMessage(message);
    },

    _itemTypeFullNameSelectionChangedHandler: function (sender, args) {
        this._itemTypeFullName = sender.get_value();
       
    },

    _getItemTypesSuccessHandler: function (sender, result) {
        if (result && result.Items) {
            var itemTypes = result.Items;
            for (var i = 0, itemTypesLength = itemTypes.length; i < itemTypesLength; i++) {
                this.get_itemTypesNamesSelector().addListItem(itemTypes[i][this._itemTypeFullNameField], itemTypes[i][this._itemTypeTitleField]);
            }
            if (this._itemTypeFullName) {
                this.get_itemTypesNamesSelector().set_value(this._itemTypeFullName);
            } else {
                this.get_itemTypesNamesSelector().set_value(itemTypes[0][this._itemTypeFullNameField]);
            }
        }
       
    },

    _getItemTypesFailureHandler: function (error, result) {
        alert(error.Detail);
    },

    // ------------------------------------- Private methods -------------------------------------

    // closes the dialog
    _closeDialog: function () {
        if (this._isNew && this._isDirty) {
            this.closeCreated(this.get_binder().get_lastModifiedDataItem(), this);
        }
        else if (this._isDirty) {
            //this.closeAndRebind();
            this.closeUpdated(this.get_binder().get_lastModifiedDataItem(), this);
        }
        else {
            this.close();
        }
    },

    _dataBindPropertiesItemsList: function (propertiesItemsList) {
        var itemTypeFullName = this._getItemTypeFullName();
        if (itemTypeFullName) {
            var binder = propertiesItemsList.getBinder();
            var serviceBaseUrl = binder.get_serviceBaseUrl();
            var serviceUrl = new Sys.Uri(serviceBaseUrl);
            serviceUrl.get_query().controlType = itemTypeFullName;
            binder.set_serviceBaseUrl(serviceUrl.toString());
            binder.DataBind();
            binder.set_serviceBaseUrl(serviceBaseUrl);
        }
        //TODO: decide what to do if no item type is specified.
    },

    _getItemTypeFullName: function () {
        if (!this._itemTypeFullName) {
            var selectedValue = this._controlTypesList.get_value();
            if (this._typeMappings[selectedValue] != null) {
                return this._typeMappings[selectedValue];
            } else {
                return selectedValue;
            }
        } else {
            return this._itemTypeFullName;
        }
    },

    _setModuleTitle: function (moduleName) {
        if (!this._moduleTitle)
            return;

        if (moduleName) {
            this._moduleTitle.innerHTML = moduleName;
        }
        else {
            var selectedValue = this._controlTypesList.get_value();
            this._moduleTitle.innerHTML = this._areaNameMappings[selectedValue];
        }
    },

    _getModuleTitle: function () {
        var selectedValue = this._controlTypesList.get_value();
        return this._areaNameMappings[selectedValue];
    },

    _getAppliedToTemplateName: function () {

        if (this._friendlyControlName != null) {
            return this._friendlyControlName;
        }

        if (this._moduleName != null) {
            return this._moduleName;
        }
        else if (this._moduleTitle != null) {
            return this._moduleTitle.innerHTML;
        }

        return "";
    },

    //_setAppliedToTemplate: function () {
    //    this.get_controlTypeSelector().set_value(this._controlType);

    //    if (this.get_controlTypeSelector().get_value() != this._controlType) {
    //        this.get_controlTypeSelector().addListItem(this._controlType, this._getAppliedToTemplateName());
    //        this.get_controlTypeSelector().set_value(this._controlType);
    //    }
    //},

    // TODO: remove this function when jQuery toggle function is fixed.
    _toggle: function (jQueryElement) {
        var state = jQueryElement.css("display");
        if (state == "none") {
            jQueryElement.show();
        }
        else {
            jQueryElement.hide();
        }
    },

    _configureItemTypeNamesSelector: function (serviceUrl) {
        this.get_itemTypesNamesSelector().add_valueChanged(this._itemTypeFullNameSelectionChangedDelegate);
        jQuery(this.get_itemTypesNamesSelector().get_element()).show();
        var clientManager = new Telerik.Sitefinity.Data.ClientManager();

        var urlParams = [];
        var keys = [];
        clientManager.InvokeGet(serviceUrl, urlParams, keys, this._getItemTypesSuccessDelegate, this._getItemTypesFailureDelegate, this);
    },

    _bindFields: function () {
        this._dataBindPropertiesItemsList(this._commonPropertiesItemsList);
        if (this._otherPropertiesContainerExpanded) {
            this._dataBindPropertiesItemsList(this._otherPropertiesItemsList);
        }
    },

    NavigatePrev: function () {
        if (this._versionInfo.VersionInfo.PreviousId != "" && this._versionInfo.VersionInfo.PreviousId != "undefined") {
            this._params.VersionId = this._versionInfo.VersionInfo.PreviousId;
            this.dataBind(this._binder.get_dataItem(), this._key, this._params);
        }
        
    },
    NavigateNext: function () {

        if (this._versionInfo.VersionInfo.NextId != "" && this._versionInfo.VersionInfo.NextId != "undefined") {
            this._params.VersionId = this._versionInfo.VersionInfo.NextId;
            this.dataBind(this._binder.get_dataItem(), this._key, this._params);
        }
    },

    // ------------------------------------- Properties -------------------------------------

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

    get_binder: function () {
        return this._binder;
    },
    set_binder: function (value) {
        this._binder = value;
    },

    get_fieldControlIds: function () {
        return this._fieldControlIds;
    },
    set_fieldControlIds: function (value) {
        this._fieldControlIds = value;
    },

    get_blankDataItem: function () {
        return this._blankDataItem;
    },
    set_blankDataItem: function (value) {
        this._blankDataItem = value;
    },

    get_providerName: function () {
        return this._providerName;
    },
    set_providerName: function (value) {
        this._providerName = value;
    },

    get_commonPropertiesItemsList: function () {
        return this._commonPropertiesItemsList;
    },
    set_commonPropertiesItemsList: function (value) {
        this._commonPropertiesItemsList = value;
    },

    get_otherPropertiesItemsList: function () {
        return this._otherPropertiesItemsList;
    },
    set_otherPropertiesItemsList: function (value) {
        this._otherPropertiesItemsList = value;
    },

    get_controlTypesList: function () {
        return this._controlTypesList;
    },
    set_controlTypesList: function (value) {
        this._controlTypesList = value;
    },

    get_templateDataField: function () {
        return this._templateDataField;
    },
    set_templateDataField: function (value) {
        this._templateDataField = value;
    },

    get_moduleTitle: function () {
        return this._moduleTitle;
    },
    set_moduleTitle: function (value) {
        this._moduleTitle = value;
    },

    get_expandCommonPropertiesButton: function () {
        return this._expandCommonPropertiesButton;
    },
    set_expandCommonPropertiesButton: function (value) {
        this._expandCommonPropertiesButton = value;
    },

    get_expandOtherPropertiesButton: function () {
        return this._expandOtherPropertiesButton;
    },
    set_expandOtherPropertiesButton: function (value) {
        this._expandOtherPropertiesButton = value;
    },

    get_commonPropertiesContainer: function () {
        return this._commonPropertiesContainer;
    },
    set_commonPropertiesContainer: function (value) {
        this._commonPropertiesContainer = value;
    },

    get_otherPropertiesContainer: function () {
        return this._otherPropertiesContainer;
    },
    set_otherPropertiesContainer: function (value) {
        this._otherPropertiesContainer = value;
    },

    get_restorePrompt: function () {
        return this._restorePrompt;
    },

    set_restorePrompt: function (value) {
        if (this._restorePrompt != value) {
            this._restorePrompt = value;
        }
    },

    get_messageControl: function () {
        return this._messageControl;
    },

    set_messageControl: function (value) {
        if (this._messageControl != value) {
            this._messageControl = value;
        }
    },

    get_labelManager: function () {
        return this._labelManager;
    },

    set_labelManager: function (value) {
        if (this._labelManager != value) {
            this._labelManager = value;
        }
    },

    get_controlTypeSelector: function () {
        return this._controlTypeSelector;
    },

    set_controlTypeSelector: function (value) {
        if (this._controlTypeSelector != value) {
            this._controlTypeSelector = value;
        }
    },

    get_itemTypesNamesSelector: function () {
        return this._itemTypesNamesSelector;
    },

    set_itemTypesNamesSelector: function (value) {
        if (this._itemTypesNamesSelector != value) {
            this._itemTypesNamesSelector = value;
        }
    },

    get_CodeMirror: function () {
        return this._codeMirror;
    },

    set_CodeMirror: function (value) {
        this._codeMirror = value;
    }
};

Telerik.Sitefinity.Modules.ControlTemplates.Web.UI.ControlTemplateVersionReview.registerClass("Telerik.Sitefinity.Modules.ControlTemplates.Web.UI.ControlTemplateVersionReview", Telerik.Sitefinity.Web.UI.AjaxDialogBase);
