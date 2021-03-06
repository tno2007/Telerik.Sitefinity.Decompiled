Type.registerNamespace("Telerik.Sitefinity.ModuleEditor.Web");

Telerik.Sitefinity.ModuleEditor.Web.ModuleEditorDialog = function (element) {
    Telerik.Sitefinity.ModuleEditor.Web.ModuleEditorDialog.initializeBase(this, [element]);

    this._autoBind = null;
    this._customFieldsGrid = null;
    this._defaultFieldsGrid = null;
    this._commandBar = null;
    this._bottomCommandBar = null;
    this._commandBarCommandDelegate = null;
    this._emptyContext = null;
    this._serviceBaseUrl = null;
    this._defaultFieldsExpander = null;
    this._defaultFieldsWrapper = null;
    this._backButton = null;

    this._applyChangesFailureDelegate = null;
    this._applyChangesSuccessDelegate = null;
    this._fieldDeletedDelegate = null;
    this._fieldSavedDelegate = null;
    this._fieldEditingDelegate = null;
    this._toggleDefaultFieldsDelegate = null;
    this._backDelegate = null;

    this._refreshParentOnCancel = null;

    this._currentContext = null;
    this._contentType = null;
    this._isChanged = false;

    this._messageControl = null;
    this._loadingView = null;
    this._loadingViewClone = null;
    this._bottomLoadingViewClone = null;
}

Telerik.Sitefinity.ModuleEditor.Web.ModuleEditorDialog.prototype = {
    initialize: function () {
        Telerik.Sitefinity.ModuleEditor.Web.ModuleEditorDialog.callBaseMethod(this, "initialize");

        this._backDelegate = Function.createDelegate(this, this._onCancelCommand);
        $addHandler(this.get_backButton(), "click", this._backDelegate);

        this._commandBarCommandDelegate = Function.createDelegate(this, this._commandBar_Command);
        this._commandBar.add_command(this._commandBarCommandDelegate);
        this._bottomCommandBar.add_command(this._commandBarCommandDelegate);

        if (this._emptyContext) {
            this._emptyContext = Sys.Serialization.JavaScriptSerializer.deserialize(this._emptyContext);
        }

        this._applyChangesFailureDelegate = Function.createDelegate(this, this._applyChanges_Failure);
        this._applyChangesSuccessDelegate = Function.createDelegate(this, this._applyChanges_Success);
        this._fieldDeletedDelegate = Function.createDelegate(this, this._fieldDeleted);
        this._fieldSavedDelegate = Function.createDelegate(this, this._fieldSaved);
        this._fieldEditingDelegate = Function.createDelegate(this, this._fieldEditing);
        this._toggleDefaultFieldsDelegate = Function.createDelegate(this, this._toggleDefaultFields);
        $addHandler(this._defaultFieldsExpander, "click", this._toggleDefaultFieldsDelegate);

        this.get_customFieldsGrid().add_fieldSaved(this._fieldSavedDelegate);
        this.get_customFieldsGrid().add_fieldDeleted(this._fieldDeletedDelegate);
        this.get_customFieldsGrid().add_fieldEditing(this._fieldEditingDelegate);
        this.get_defaultFieldsGrid().add_fieldSaved(this._fieldSavedDelegate);
        this.get_defaultFieldsGrid().add_fieldEditing(this._fieldEditingDelegate);

        if (this._onLoadDelegate == null) {
            this._onLoadDelegate = Function.createDelegate(this, this._onLoad);
            Sys.Application.add_load(this._onLoadDelegate);
        }
    },

    dispose: function () {
        Telerik.Sitefinity.ModuleEditor.Web.ModuleEditorDialog.callBaseMethod(this, "dispose");

        if (this._backDelegate) {
            if (this.get_backButton()) {
                $removeHandler(this.get_backButton(), "click", this._backDelegate);
            }
            delete this._backDelegate;
        }

        if (this._commandBarCommandDelegate) {
            if (this._commandBar != null) {
                this._commandBar.remove_command(this._commandBarCommandDelegate);
            }
            if (this._bottomCommandBar) {
                this._bottomCommandBar.remove_command(this._commandBarCommandDelegate);
            }
            delete this._commandBarCommandDelegate;
        }
        if (this._fieldDeletedDelegate) {
            if (this.get_customFieldsGrid()) {
                this.get_customFieldsGrid().remove_fieldDeleted(this._fieldDeletedDelegate);
            }
            delete this._fieldDeletedDelegate;
        }
        if (this._applyChangesFailureDelegate) {
            delete this._applyChangesFailureDelegate;
        }
        if (this._applyChangesSuccessDelegate) {
            delete this._applyChangesSuccessDelegate;
        }
        if (this._fieldSavedDelegate) {
            if (this.get_customFieldsGrid()) {
                this.get_customFieldsGrid().remove_fieldSaved(this._fieldSavedDelegate);
            }
            if (this.get_defaultFieldsGrid()) {
                this.get_defaultFieldsGrid().remove_fieldSaved(this._fieldSavedDelegate);
            }
            delete this._fieldSavedDelegate;
        }
        if (this._fieldEditingDelegate) {
            if (this.get_customFieldsGrid()) {
                this.get_customFieldsGrid().remove_fieldEditing(this._fieldEditingDelegate);
            }
            if (this.get_defaultFieldsGrid()) {
                this.get_defaultFieldsGrid().remove_fieldEditing(this._fieldEditingDelegate);
            }
            delete this._fieldEditingDelegate;
        }
        if (this._toggleDefaultFieldsDelegate) {
            if (this._defaultFieldsExpander) {
                $removeHandler(this._defaultFieldsExpander, "click", this._toggleDefaultFieldsDelegate);
            }
            delete this._toggleDefaultFieldsDelegate;

        } if (this._defaultFieldsGridDataBoundDelegate) {
            this.get_defaultFieldsGrid().get_itemsGrid().getBinder().add_onDataBound(this._defaultFieldsGridDataBoundDelegate);
            delete this._defaultFieldsGridDataBoundDelegate;
        }

        if (this._onLoadDelegate) {
            Sys.Application.remove_load(this._onLoadDelegate);
            delete this._onLoadDelegate;
        }
    },

    _onLoad: function (sender, args) {
        this._defaultFieldsGridDataBoundDelegate = Function.createDelegate(this, this._defaultFieldsGridDataBound);
        this.get_defaultFieldsGrid().get_itemsGrid().getBinder().add_onDataBound(this._defaultFieldsGridDataBoundDelegate);
        
        if (this.get_autoBind()) {
            if (!dialogBase) {
                throw "Cannot bind editor because dialogBase is not defined.";
            }

            dialogBase.bindEditor(this.get_contentType());
        }
    },

    // callback function called if the service failed
    _applyChanges_Failure: function (error) {
        this._hideLoadingView();
        if (this._messageControl) {
            var message = error.Detail ? error.Detail : "Error saving changes";
            this._messageControl.showNegativeMessage(message);
        }
    },

    // callback function called if the service successfully complete
    _applyChanges_Success: function (caller, result) {
        this._hideLoadingView();
        caller._isChanged = true;
        caller._onCancelCommand();
    },

    _defaultFieldsGridDataBound: function (sender, args) {
        var hideGrid = sender.get_hasNoData();
        var defaultFieldGrid = jQuery(this.get_defaultFieldsGrid().get_element());
        if (hideGrid) {
            jQuery(defaultFieldGrid).parents(".sfExpandableForm").hide();
        } else {
            jQuery(defaultFieldGrid).parents(".sfExpandableForm").show();
        }
    },

    _fieldSaved: function (sender, args) {
        // TODO: make a decision here is it default or custom field
        var fieldContext = args.get_commandArgument();
        var contextUpdated = {};
        this._configureVisibleViews(fieldContext);

        this._addOrUpdateCustomField(fieldContext, contextUpdated, args.isNew);
        if (!contextUpdated.Value) {
            args.set_cancel(true);
        }
    },

    _configureVisibleViews: function (fieldContext) {
        if (fieldContext.Value.Definition.VisibleViews === "allViews") {
            fieldContext.Value.Definition.Hidden = false;
            fieldContext.Value.Definition.VisibleViews = [];
        }
        else if (fieldContext.Value.Definition.VisibleViews === "nowhere") {
            fieldContext.Value.Definition.Hidden = true;
            fieldContext.Value.Definition.VisibleViews = [];
        }
        else if (fieldContext.Value.Definition.VisibleViews) {
            fieldContext.Value.Definition.VisibleViews = Telerik.Sitefinity.fixArray(fieldContext.Value.Definition.VisibleViews);
        }
    },

    _fieldEditing: function (sender, args) {
        var argument = args.get_commandArgument();
        if (argument.fieldName) {
            var context = this.get_currentContext();

            var index = this._getFieldContextIndex(argument.fieldName, context.AddFields);
            if (index !== -1) {
                argument.definition = context.AddFields[index].Value.Definition;
                argument.context = context.AddFields[index];
            }
        }
    },

    _fieldDeleted: function (sender, args) {
        var contextUpdated = {};
        this._removeCustomField(args.get_commandArgument().Name, contextUpdated);
        if (!contextUpdated.Value) {
            args.set_cancel(true);
        }
    },

    /* public methods */
    bindEditor: function (typeName, isChanged, itemsName) {
        this._isChanged = isChanged;
        this._resetContext(typeName);
        this.set_contentType(typeName);
        //this._currentContext.ContentType = typeName;
        this.get_customFieldsGrid().set_componentType(typeName);
        this.get_defaultFieldsGrid().set_componentType(typeName);
        this.get_customFieldsGrid().set_itemsName(itemsName);
        this.get_defaultFieldsGrid().set_itemsName(itemsName);
        this.get_customFieldsGrid().dataBind();
        this.get_defaultFieldsGrid().dataBind();
        // collapse default fields by default
        //jQuery(this.get_defaultFieldsGrid().get_element()).hide();
        //        var defaultFieldGrid = jQuery(this.get_defaultFieldsGrid().get_element());
        //        var defaultFieldGridWrp = jQuery(defaultFieldGrid).parents(".sfExpandableForm").get(0);
        //        jQuery(defaultFieldGrid).parent().addClass("sfCollapsedTarget").removeClass("sfExpandedTarget");
        //        jQuery(defaultFieldGridWrp).removeClass("sfExpandedForm");
    },

    applyChanges: function () {
        var clientManager = new Telerik.Sitefinity.Data.ClientManager();
        var urlParams = [];
        var keys = [];
        //urlParams["itemType"] = "Telerik.Sitefinity.News.Model.NewsItem";
        var serviceUrl = this.get_serviceBaseUrl() + "/applyChanges"
        clientManager.InvokePut(serviceUrl, urlParams, keys, this.get_currentContext(), this._applyChangesSuccessDelegate, this._applyChangesFailureDelegate, this);
    },

    /* properties */
    get_autoBind: function () {
        return this._autoBind;
    },

    set_autoBind: function (value) {
        this._autoBind = value;
    },

    get_serviceBaseUrl: function () {
        return this._serviceBaseUrl;
    },

    set_serviceBaseUrl: function (value) {
        this._serviceBaseUrl = value;
    },

    get_backButton: function () {
        return this._backButton;
    },

    set_backButton: function (value) {
        if (this._backButton != value) {
            this._backButton = value;
        }
    },

    get_customFieldsGrid: function () {
        return this._customFieldsGrid;
    },

    set_customFieldsGrid: function (value) {
        if (this._customFieldsGrid != value) {
            this._customFieldsGrid = value;
        }
    },

    get_defaultFieldsGrid: function () {
        return this._defaultFieldsGrid;
    },

    set_defaultFieldsGrid: function (value) {
        if (this._defaultFieldsGrid != value) {
            this._defaultFieldsGrid = value;
        }
    },

    get_commandBar: function () {
        return this._commandBar;
    },
    set_commandBar: function (value) {
        this._commandBar = value;
    },

    get_bottomCommandBar: function () {
        return this._bottomCommandBar;
    },
    set_bottomCommandBar: function (value) {
        this._bottomCommandBar = value;
    },

    get_contentType: function () {
        return this._contentType;
    },

    set_contentType: function (value) {
        if (this._contentType != value) {
            this._contentType = value;
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

    get_loadingView: function () {
        return this._loadingView;
    },
    set_loadingView: function (value) {
        this._loadingView = value;
    },

    get_refreshParentOnCancel: function () {
        return this._refreshParentOnCancel;
    },
    set_refreshParentOnCancel: function (value) {
        this._refreshParentOnCancel = value;
    },

    get_currentContext: function () {
        if (this._currentContext == null) {
            this._currentContext = {
                ContentType: this.get_contentType(),
                AddFields: [],
                RemoveFields: []
            };
        }
        return this._currentContext;
    },

    set_currentContext: function (value) {
        if (this._currentContext != value) {
            this._currentContext = value;
        }
    },

    get_defaultFieldsExpander: function () {
        return this._defaultFieldsExpander;
    },
    set_defaultFieldsExpander: function (value) {
        if (this._defaultFieldsExpander != value) {
            this._defaultFieldsExpander = value;
        }
    },

    get_defaultFieldsWrapper: function () {
        return this._defaultFieldsWrapper;
    },
    set_defaultFieldsWrapper: function (value) {
        if (this._defaultFieldsWrapper != value) {
            this._defaultFieldsWrapper = value;
        }
    },

    /* private methods */
    _commandBar_Command: function (sender, args) {
        switch (args.get_commandName()) {
            case 'save':
                this._onSaveCommand();
                break;
            case 'cancel':
                this._onCancelCommand();
                break;
        }
    },

    _toggleDefaultFields: function (sender, args) {
        //this._toggle(jQuery(this.get_defaultFieldsGrid().get_element()));
        var defaultFieldGrid = jQuery(this.get_defaultFieldsGrid().get_element());
        var defaultFieldGridWrp = jQuery(defaultFieldGrid).parents(".sfExpandableForm").get(0);
        jQuery(defaultFieldGridWrp).toggleClass("sfExpandedForm");
        if (jQuery(defaultFieldGridWrp).hasClass("sfExpandedForm")) {
            jQuery(defaultFieldGrid).parent().addClass("sfExpandedTarget").removeClass("sfCollapsedTarget");
        }
        else {
            jQuery(defaultFieldGrid).parent().addClass("sfCollapsedTarget").removeClass("sfExpandedTarget");
        }
    },

    _onSaveCommand: function () {
        //alert('CURRENT CONTEXT:\n' + JSON.stringify(this.get_currentContext()));
        this._showLoadingView();
        this.applyChanges();
    },

    _onCancelCommand: function () {
        if (this.get_autoBind()) {
            return;
        }

        if (this._isChanged || this.get_refreshParentOnCancel() == true) {
            var url = window.top.location.href;
            var idx = url.indexOf("#");
            if (idx > -1) {
                url = url.substring(0, idx);
            }
            jQuery(window.top.document.body).addClass("sfLoadingTransition");
            window.top.location = url;
        }
        else {
            this.close();
        }
    },

    _showLoadingView: function () {
        this._loadingViewClone = this._cloneLoadingView(this._loadingViewClone, this._commandBar.get_element());
        this._bottomLoadingViewClone = this._cloneLoadingView(this._bottomLoadingViewClone, this._bottomCommandBar.get_element());
    },

    _cloneLoadingView: function (loadingViewClone, element) {
        if (loadingViewClone != null) {
            return loadingViewClone;
        }
        loadingViewClone = jQuery(this._loadingView).clone();
        //loading image added at command bar container div
        var $commandBar = jQuery(element);
        $commandBar.append(loadingViewClone);
        $commandBar.children().hide();
        loadingViewClone.show();
        return loadingViewClone;
    },

    _hideLoadingView: function () {
        this._removeLoadingView(this._loadingViewClone, this._commandBar.get_element());
        this._loadingViewClone = null;
        this._removeLoadingView(this._bottomLoadingViewClone, this._bottomCommandBar.get_element());
        this._bottomLoadingViewClone = null;
    },

    _removeLoadingView: function (loadingViewClone, element) {
        if (loadingViewClone) {
            loadingViewClone.remove();
            loadingViewClone = null;
            jQuery(element).children().show();
        }
    },

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

    _resetContext: function (contentType) {
        this._currentContext = this._clone(this._emptyContext);
        this._currentContext.ContentType = contentType;
    },

    _clone: function (obj) {
        var clone = jQuery.extend(true, {}, obj);
        this._fixClone(clone, obj);
        return clone;
    },

    _fixClone: function (obj, original) {
        for (var property in obj) {
            var val = original[property];
            if (val && (typeof val == 'object')) {
                if (val.constructor == Date) {
                    obj[property] = val;
                }
                else {
                    this._fixClone(obj[property], val);
                }
            }
        }
    },

    _addOrUpdateCustomField: function (fieldContext, contextUpdated, isNew) {
        var context = this.get_currentContext();
        contextUpdated.Value = true;

        if (!this._fieldIncludedForAddition(fieldContext.Key)) {
            if (this._fieldIncludedForRemoval(fieldContext.Key)) {
                // field has been added and removed without saving changes, add it again and delete it from RemoveFields collection
                var fieldIndex = this._getFieldContextIndex(fieldContext.Key, context.RemoveFields);
                context.RemoveFields.splice(fieldIndex, 1);
            }
            else {
                context.AddFields.push(fieldContext);
            }
        }
        else if (isNew) {
            contextUpdated.Value = false;
            alert("A field with the same name has already been added");
        } else {
            // Either newly added (not saved) field was edited, or saved field was edited more than once:
            // In both cases, only the definition need to be updated, especially for the case with newly added field,
            // as this keeps the correct values for things like DB mapping, which are important for successful addition.
            var index = this._getFieldContextIndex(fieldContext.Key, context.AddFields);
            context.AddFields[index].Value.Definition = fieldContext.Value.Definition;
        }
    },

    _removeCustomField: function (fieldName, contextUpdated) {
        contextUpdated.Value = false;
        if (this._fieldIncludedForAddition(fieldName)) {
            // if field has just been added and not persisted, just remove it from the context for AddFields
            var fieldIndex = this._getFieldContextIndex(fieldName, this.get_currentContext().AddFields);
            this.get_currentContext().AddFields.splice(fieldIndex, 1);
            contextUpdated.Value = true;
        }
        else if (!this._fieldIncludedForRemoval(fieldName)) {
            this.get_currentContext().RemoveFields.push(fieldName);
            contextUpdated.Value = true;
        }
    },

    _fieldIncludedForAddition: function (fieldContextKey) {
        var addedFieldsCount = this.get_currentContext().AddFields.length;
        for (var i = 0; i < addedFieldsCount; i++) {
            if (this.get_currentContext().AddFields[i].Key == fieldContextKey) {
                return true;
            }
        }
        return false;
    },

    _fieldIncludedForRemoval: function (fieldContextKey) {
        var removeFieldsCount = this.get_currentContext().RemoveFields.length;
        for (var i = 0; i < removeFieldsCount; i++) {
            if (this.get_currentContext().RemoveFields[i] == fieldContextKey) {
                return true;
            }
        }
        return false;
    },

    _getFieldContextIndex: function (fieldContextKey, contextCollection) {
        var collectionLength = contextCollection.length;
        for (var i = 0; i < collectionLength; i++) {
            if (contextCollection[i].Key == fieldContextKey) {
                return i;
            }
        }
        return -1;
    }
};

Telerik.Sitefinity.ModuleEditor.Web.ModuleEditorDialog.registerClass('Telerik.Sitefinity.ModuleEditor.Web.ModuleEditorDialog', Telerik.Sitefinity.Web.UI.AjaxDialogBase);

