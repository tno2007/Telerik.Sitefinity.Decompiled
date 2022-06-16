Type.registerNamespace("Telerik.Sitefinity.ModuleEditor.Web.UI");

Telerik.Sitefinity.ModuleEditor.Web.UI.FieldsGrid = function (element) {
    Telerik.Sitefinity.ModuleEditor.Web.UI.FieldsGrid.initializeBase(this, [element]);
    this._radWindowManager = null;
    this._createCustomFieldWindow = null;
    this._addCustomFieldLink = null;
    this._componentType = null;
    this._itemsGrid = null;
    this._noCustomFieldsLabel = null;

    this._fieldEditorDialogUrl = null;
    this._createFieldDialogUrl = null;
    this._fieldDeletedDelegate = null;
    this._dialogClosedDelegate = null;

    this._addCustomFieldLinkClickDelegate = null;
    this._onLoadDelegate = null;
    this._itemsGridItemCommandDelegate = null; 
    this._closeDialogExtensionDelegate = null;
    this._loadDialogExtensionDelegate = null;
    this._showDialogExtensionDelegate = null;
    this._binderDataBoundDelegate = null;
    this._binderItemDataBoundDelegate = null;
    this._binderItemDataBindingDelegate = null;
    this._fieldEditorCloseDelegate = null;

    this._allowContentLinks = true;

    this._gridItems = [];
    this._totalCount = 0;
    this._newlyAddedCssClass = "sfNewlyAddedCustomField";
    this._itemsName = null;
}

Telerik.Sitefinity.ModuleEditor.Web.UI.FieldsGrid.prototype = {

    initialize: function () {
        Telerik.Sitefinity.ModuleEditor.Web.UI.FieldsGrid.callBaseMethod(this, "initialize");

        requirejs.config({
            baseUrl: this._siteBaseUrl + "Res",
            paths: {
                FieldEditor: 'Telerik.Sitefinity.ModuleEditor.Web.Scripts.FieldEditor',
            },
            waitSeconds: 0
        });
        var fieldsGrid = this;
        require(["FieldEditor"], function (FieldEditor) {
            fieldsGrid.fieldEditor = new FieldEditor();
            fieldsGrid.fieldEditor.initialize(fieldsGrid._siteBaseUrl);
        });

        if (this._addCustomFieldLinkDelegate == null) {
            this._addCustomFieldLinkDelegate = Function.createDelegate(this, this._createCustomFieldLinkClicked);
        }
        if (this.get_addCustomFieldLink()) {
            $addHandler(this.get_addCustomFieldLink(), "click", this._addCustomFieldLinkDelegate);
        }
        if (this._onLoadDelegate == null) {
            this._onLoadDelegate = Function.createDelegate(this, this._onLoad);
        }
        Sys.Application.add_load(this._onLoadDelegate);
        if (this._itemsGridItemCommandDelegate == null) {
            this._itemsGridItemCommandDelegate = Function.createDelegate(this, this._itemsGridItemCommand);
        }
        this.get_itemsGrid().add_itemCommand(this._itemsGridItemCommandDelegate);
        if (this._fieldDeletedDelegate == null) {
            this._fieldDeletedDelegate = Function.createDelegate(this, this._fieldDeletedHandler);
        }
        if (this._dialogClosedDelegate == null) {
            this._dialogClosedDelegate = Function.createDelegate(this, this._dialogClosedHandler);
        }
        if (this._closeDialogExtensionDelegate == null) {
            this._closeDialogExtensionDelegate = Function.createDelegate(this, this._closeDialogExtension);
        }
        if (this._loadDialogExtensionDelegate == null) {
            this._loadDialogExtensionDelegate = Function.createDelegate(this, this._loadDialogExtension);
        }
        if (this._showDialogExtensionDelegate == null) {
            this._showDialogExtensionDelegate = Function.createDelegate(this, this._showDialogExtension);
        }
        if (this._fieldEditorCloseDelegate == null) {
            this._fieldEditorCloseDelegate = Function.createDelegate(this, this._fieldEditorCloseHandler);
        }
        this._binderDataBoundDelegate = Function.createDelegate(this, this._binderDataBound);
        this.get_itemsGrid().getBinder().add_onDataBound(this._binderDataBoundDelegate);

        this._binderItemDataBoundDelegate = Function.createDelegate(this, this._binderItemDataBound);
        this.get_itemsGrid().getBinder().add_onItemDataBound(this._binderItemDataBoundDelegate);

        this._binderItemDataBindingDelegate = Function.createDelegate(this, this._binderItemDataBinding);
        this.get_itemsGrid().getBinder().add_onItemDataBinding(this._binderItemDataBindingDelegate);
    },

    dispose: function () {
        Telerik.Sitefinity.ModuleEditor.Web.UI.FieldsGrid.callBaseMethod(this, "dispose");

        if (this._addCustomFieldLinkDelegate) {
            if (this.get_addCustomFieldLink()) {
                $removeHandler(this.get_addCustomFieldLink(), "click", this._addCustomFieldLinkDelegate);
            }
            delete this._addCustomFieldLinkDelegate;
        }
        if (this._onLoadDelegate) {
            Sys.Application.remove_load(this._onLoadDelegate);
            delete this._onLoadDelegate;
        }
        if (this._itemsGridItemCommandDelegate) {
            if (this.get_itemsGrid()) {
                this.get_itemsGrid().remove_itemCommand(this._itemsGridItemCommandDelegate);
            }
            delete this._itemsGridItemCommandDelegate;
        }
        if (this._fieldDeletedDelegate) {
            delete this._fieldDeletedDelegate;
        }
        if (this._dialogClosedDelegate) {
            delete this._dialogClosedDelegate;
        }
        if (this._closeDialogExtensionDelegate) {
            delete this._closeDialogExtensionDelegate;
        }
        if (this._loadDialogExtensionDelegate) {
            delete this._loadDialogExtensionDelegate;
        }
        if (this._showDialogExtensionDelegate) {
            delete this._showDialogExtensionDelegate;
        }
        if (this._fieldEditorCloseDelegate) {
            delete this._fieldEditorCloseDelegate;
        }
        if (this._binderDataBoundDelegate) {
            if (this.get_itemsGrid() && this.get_itemsGrid().getBinder()) {
                this.get_itemsGrid().getBinder().remove_onDataBound(this._binderDataBoundDelegate);
            }
            delete this._binderDataBoundDelegate;
        }
        if (this._binderItemDataBoundDelegate) {
            if (this.get_itemsGrid() && this.get_itemsGrid().getBinder()) {
                this.get_itemsGrid().getBinder().remove_onItemDataBound(this._binderItemDataBoundDelegate);
            }
            delete this._binderItemDataBoundDelegate;
        }
        if (this._binderItemDataBindingDelegate) {
            delete this._binderItemDataBindingDelegate;
        }
    },

    _onLoad: function (sender, args) {
        if (typeof GetDialogManager == "function") {
            var dialogManager = GetDialogManager();
            dialogManager.blacklistWindowManager(this._radWindowManager);
        }
    },

    _fieldEditorCloseHandler: function (e) {
        if (this.fieldEditor && this.fieldEditor.model.commandName == "saveCustomField") {
            var definition = this.fieldEditor.model.field;
            var argument = new Telerik.Sitefinity.CommandEventArgs("fieldSaved", definition);
            argument.isNew = false;
            if (definition) {
                if (this._findGridItemIndexByName(definition.Key) == -1) {
                    this._dialogClosedDelegate(this, argument);

                    if (!argument.get_cancel()) {
                        this._addFieldToGrid(args);
                    }
                }
                else {
                    if (argument.isNew)
                        alert("A field with the same name has already been added.");
                    else {
                        this._dialogClosedDelegate(this, argument);
                    }
                }
            }
        }
    },

    _itemsGridItemCommand: function (sender, args) {
        var fieldName = args.get_commandArgument().Name;

        switch (args.get_commandName()) {
            case "editDefaultField":
            case "editCustomField":
                var isCustom = (args.get_commandName() == "editDefaultField") ? false : true;
                var contentType = args.get_commandArgument().ContentType;
                var fieldTypeName = args.get_commandArgument().FieldTypeName;
                var dialogUrl = this._fieldEditorDialogUrl;

                var commandArgument = { fieldName: fieldName };
                this._raise_fieldEditing(new Telerik.Sitefinity.CommandEventArgs("fieldEditing", commandArgument));

                if (fieldTypeName === "RelatedMedia" || fieldTypeName === "RelatedData") {
                    var fieldEditorWindow = $('<div></div>').kendoWindow({
                        actions: [],
                        title: "Settings",
                        resizable: false,
                        modal: true,
                        animation: false,
                        close: this._fieldEditorCloseDelegate
                    });
                    fieldEditorWindow.addClass("sfSelectorDialog");
                    fieldEditorWindow.data("kendoWindow").content('<h1>Settings</h1><div id="windowContainer" class="sfBasicDim"></div>').center().open();

                    // if we are not editing newly created field, we should get the field definitions from the arguments
                    if (!commandArgument.context) {
                        commandArgument.context = 
                            { 
                                Key: fieldName, 
                                Value: 
                                    { 
                                        ContentType: contentType,
                                        FieldTypeKey: fieldTypeName,
                                        IsCustom: isCustom,
                                        Name: fieldName, 
                                        Definition: args.get_commandArgument().RelatedFieldDefinition
                                    }
                            };
                    }

                    var isNotOpenGraphMedia = true;
                    if (fieldName === "OpenGraphImage" || fieldName === "OpenGraphVideo")
                        isNotOpenGraphMedia = false;

                    var model = {
                        field: commandArgument.context,
                        mainFormSelector: "div.sfFormStepOne", //customFieldPropertyEditor
                        container: "#windowContainer",
                        fieldType: fieldTypeName,
                        kendoWindow: fieldEditorWindow,
                        commandName: args.get_commandName(),
                        isNotOpenGraphMedia: isNotOpenGraphMedia
                    };
                    
                    // open field editor on EDIT of custom field
                    this.fieldEditor.open(model, true);
                    fieldEditorWindow.css({ "visibility": "visible" }).parent().css({ "top": "50px", "left": "50%", "margin-left": "-212px" });
                    break;
                } else {
                    dialogUrl += String.format("?mode=edit&fieldTypeName={0}&fieldName={1}&componentType={2}&command={3}&itemsName={4}&isCustom={5}", // &edit=true
                                    fieldTypeName,
                                    fieldName,
                                    this.get_componentType(),
                                    args.get_commandName(),
                                    this._itemsName,
                                    isCustom);
                }

                this._openDialog("editField", dialogUrl, "editField", commandArgument.definition);
                break;
            case "deleteField":
                fieldName = args.get_commandArgument().Name;

                var kendoWindow = $("<div />").kendoWindow({
                    actions: [],
                    title: "Delete field",
                    resizable: false,
                    modal: true,
                    animation: false
                });
                kendoWindow.addClass("sfSelectorDialog");

                var contentSelector = "#buit-in-fields-alert";
                var isBuiltIn = args.get_commandArgument().IsBuiltIn;
                if (!isBuiltIn) {
                    contentSelector = "#delete-field-confirmation";
                }

                //format message
                var fieldNameHtml = '<span class="sfEmphazie">' + fieldName + '</span>';
                var content = String.format($(contentSelector).html(), fieldNameHtml);
                kendoWindow.data("kendoWindow").content(content).center().open();

                var that = this;
                kendoWindow
                       .find(".delete-confirm,.delete-cancel")
                           .click(function () {

                               if ($(this).hasClass("delete-confirm")) {
                                   that._fieldDeletedDelegate(args);
                               }

                               kendoWindow.data("kendoWindow").close();
                           })
                           .end();

                kendoWindow.css({ "visibility": "visible" }).parent().css({ "top": "50px", "left": "50%", "margin-left": "-212px" });
                break;
        }
    },

    dataBind: function () {
        var originalUrlParams = this.get_itemsGrid()._binder.get_urlParams();
        originalUrlParams["contentType"] = this.get_componentType();
        this.get_itemsGrid()._binder.set_urlParams(originalUrlParams);
        this.get_itemsGrid().dataBind();
    },

    _binderDataBound: function (sender, args) {
        this._gridItems = args.get_dataItem().Items;
        this._totalCount = args.get_dataItem().TotalCount;

        if (this.get_noCustomFieldsLabel()) {
            var hideGrid = sender.get_hasNoData();
            this._setControlsVisibility(hideGrid);
        }
    },

    _binderItemDataBound: function (sender, args) {
        var itemElement = args.get_itemElement();
        var dataItem = args.get_dataItem();

        if (itemElement) {
            if (dataItem && dataItem.NewlyAddedCssClass) {
                jQuery(itemElement).addClass(dataItem.NewlyAddedCssClass);
            }
            else {
                jQuery(itemElement).removeClass(this._newlyAddedCssClass);
            }
        }
    },

    _binderItemDataBinding: function (sender, args) {
        if (args.Name && args.ContentType) {
            switch (args.Name) {
                case "MetaTitle":
                case "MetaDescription":
                    args.ContentType = "Search engine optimization";
                    break;
                case "OpenGraphTitle":
                case "OpenGraphDescription":
                case "OpenGraphImage":
                case "OpenGraphVideo":
                    args.ContentType = "Social media (OpenGraph)";
                    break;
            }
        }
    },

    _createCustomFieldLinkClicked: function (sender, args) {
        var dialogUrl = String.format(this._createFieldDialogUrl + "?componentType={0}&itemsName={1}", this.get_componentType(), this._itemsName);
        if (this.get_allowContentLinks() == true) dialogUrl += "&AllowContentLinks=true";        
        this._openDialog("createCustomField", dialogUrl, "createCustomField");
    },

    _openDialog: function (dialogName, dialogUrl, commandName, definition) {
        var dialog = this.get_radWindowManager().getWindowByName(dialogName);
        if (dialog) {
            dialog.set_navigateUrl(dialogUrl);

            if (typeof (dialog._sfArgs) != "undefined") {
                delete dialog._sfArgs;
            }

            dialog._sfArgs = { CommandName: commandName, Definition: definition };

            if (dialogName === "createCustomField") {
                dialog._sfDefaultFields = dialogBase.get_defaultFieldsGrid()._gridItems;
            }

            if (!dialog._sfShowDialogExtension) {
                dialog._sfShowDialogExtension = this._showDialogExtensionDelegate;
                dialog.add_show(dialog._sfShowDialogExtension);
            }

            if (!dialog._sfCloseDialogExtension) {
                dialog._sfCloseDialogExtension = this._closeDialogExtensionDelegate;
                dialog.add_close(dialog._sfCloseDialogExtension);
            }

            //check if the the dialog is set to reload on each showing.
            //If that's the case - the loadDialogExtension handler should be reattached.
            if (!dialog._sfLoadDialogExtension || dialog.get_reloadOnShow()) {
                dialog._sfLoadDialogExtension = this._loadDialogExtensionDelegate;
                dialog.add_pageLoad(dialog._sfLoadDialogExtension);
            }

            dialog.show();

            Telerik.Sitefinity.centerWindowHorizontally(dialog);
        }
    },

    _showDialogExtension: function (sender, args, isLoad) {
        var popupElement = sender.get_popupElement();
        jQuery(popupElement).addClass("sfDisplayBlockImportant");

        var frameHandle = sender.get_contentFrame().contentWindow;
        if (frameHandle) {
            if (frameHandle.createDialog && (!sender.get_reloadOnShow() || isLoad)) {
                var sfArgs = sender._sfArgs;
                frameHandle.createDialog(sfArgs.CommandName, sfArgs.Definition);
            }
            if (frameHandle.reset && (!sender.get_reloadOnShow() || isLoad)) {
                frameHandle.reset();
            }
        }
    },

    _loadDialogExtension: function (sender, e) {
        var args = sender._sfArgs;

        sender.remove_pageLoad(sender._sfLoadDialogExtension);
        sender._sfShowDialogExtension(sender, args, true);
    },

    _closeDialogExtension: function (sender, args) {
        var popupElement = sender.get_popupElement();
        jQuery(popupElement).removeClass("sfDisplayBlockImportant");

        var definition = args.get_argument();
        var argument = new Telerik.Sitefinity.CommandEventArgs("fieldSaved", definition);
        var commandName = sender._sfArgs.CommandName;
        argument.isNew = commandName === "createCustomField";

        if (definition) {
            if (this._findGridItemIndexByName(definition.Key) == -1) {
                this._dialogClosedDelegate(this, argument);

                if (!argument.get_cancel()) {
                    this._addFieldToGrid(args);
                }
            }
            else {
                if (argument.isNew)
                    alert("A field with the same name has already been added.");
                else {
                    this._dialogClosedDelegate(this, argument);
                }
            }
        }
    },

    _addFieldToGrid: function (args) {
        var item = args.get_argument();
        this._gridItems.push(this._getGridDataItem(item));
        var totalCount = this._totalCount + 1;
        var data = { Items: this._gridItems, TotalCount: totalCount };
        this.get_itemsGrid().getBinder().BindCollection(data);
        this._setControlsVisibility(false);
    },

    _removeFieldFromGrid: function (args) {
        var item = args.get_commandArgument();
        var indexToRemove = this._findGridItemIndexByName(item.Name);
        this._gridItems.splice(indexToRemove, 1);
        var totalCount = this._totalCount - 1;
        var data = { Items: this._gridItems, TotalCount: totalCount };
        this.get_itemsGrid().getBinder().BindCollection(data);
        var hideGrid = (totalCount == 0);
        this._setControlsVisibility(hideGrid);
    },

    _getGridDataItem: function (definition) {
        var newItem = {};
        newItem.Id = "";
        newItem.IsCustom = true;
        newItem.ContentType = definition.Value.ContentType;
        newItem.FieldTypeName = definition.Value.FieldTypeKey;
        newItem.Name = definition.Value.Name;
        newItem.NewlyAddedCssClass = this._newlyAddedCssClass;
        return newItem;
    },

    _findGridItemIndexByName: function (itemName) {
        var itemCount = this._gridItems.length;
        for (var index = 0; index < itemCount; index++) {
            if (this._gridItems[index].Name == itemName) {
                return index;
            }
        }
        return -1;
    },

    _setControlsVisibility: function (hideGrid) {
        if (hideGrid) {
            jQuery(this.get_noCustomFieldsLabel()).show();
            jQuery(this.get_itemsGrid().get_element()).hide();
        }
        else {
            jQuery(this.get_noCustomFieldsLabel()).hide();
            jQuery(this.get_itemsGrid().get_element()).show();
        }
    },

    /* --------------------- Events --------------------------------- */

    _fieldDeletedHandler: function (args) {
        //this._removeFieldFromGrid(args);
        var handler = this.get_events().getHandler("fieldDeleted");
        if (handler) handler(this, args);
        if (!args.get_cancel()) {
            this._removeFieldFromGrid(args);
        }
    },

    add_fieldDeleted: function (handler) {
        this.get_events().addHandler("fieldDeleted", handler);
    },

    remove_fieldDeleted: function (handler) {
        this.get_events().removeHandler("fieldDeleted", handler);
    },

    _dialogClosedHandler: function (sender, args) {
        var handler = this.get_events().getHandler("fieldSaved");
        if (handler) handler(this, args);
    },

    add_fieldSaved: function (handler) {
        this.get_events().addHandler("fieldSaved", handler);
    },

    remove_fieldSaved: function (handler) {
        this.get_events().removeHandler("fieldSaved", handler);
    },

    add_fieldEditing: function (handler) {
        this.get_events().addHandler("fieldEditing", handler);
    },

    remove_fieldEditing: function (handler) {
        this.get_events().removeHandler("fieldEditing", handler);
    },

    _raise_fieldEditing: function (args) {
        var handler = this.get_events().getHandler("fieldEditing");
        if (handler) handler(this, args);
    },

    /* --------------------- Properties ----------------------------- */

    get_allowContentLinks: function () {
        return this._allowContentLinks;
    },

    set_allowContentLinks: function (value) {
        if (this._allowContentLinks != value) {
            this._allowContentLinks = value;
        }
    },

    get_radWindowManager: function () {
        return this._radWindowManager;
    },

    set_radWindowManager: function (value) {
        if (this._radWindowManager != value) {
            this._radWindowManager = value;
        }
    },

    get_addCustomFieldLink: function () {
        return this._addCustomFieldLink;
    },

    set_addCustomFieldLink: function (value) {
        if (this._addCustomFieldLink != value) {
            this._addCustomFieldLink = value;
        }
    },

    get_componentType: function () {
        return this._componentType;
    },

    set_componentType: function (value) {
        if (this._componentType != value) {
            this._componentType = value;
        }
    },

    get_itemsGrid: function () {
        return this._itemsGrid;
    },

    set_itemsGrid: function (value) {
        if (this._itemsGrid != value) {
            this._itemsGrid = value;
        }
    },

    get_noCustomFieldsLabel: function () {
        return this._noCustomFieldsLabel;
    },
    set_noCustomFieldsLabel: function (value) {
        if (this._noCustomFieldsLabel != value) {
            this._noCustomFieldsLabel = value;
        }
    },

    get_itemsName: function () {
        return this._itemsName;
    },

    set_itemsName: function (value) {
        if (this._itemsName != value) {
            this._itemsName = value;
        }
    }
};

Telerik.Sitefinity.ModuleEditor.Web.UI.FieldsGrid.registerClass('Telerik.Sitefinity.ModuleEditor.Web.UI.FieldsGrid', Sys.UI.Control);

