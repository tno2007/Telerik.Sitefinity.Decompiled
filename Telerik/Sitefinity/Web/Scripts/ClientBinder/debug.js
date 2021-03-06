Type.registerNamespace("Telerik.Sitefinity.Web.UI");
Type.registerNamespace("Telerik.Sitefinity.Web.UI.ClientBinders");

/* ClientBinder base class */

/* This is a base class for all javascript client binder classes. Client binder classes have the purpose
* of acting as an itermediary between the user interface elements used to display data on one side and
* ClientManager class on the other side which is a client side data layer. The ClientManager provides all
* the necessary abstractions for communicating with the REST WCF services, and it is the job of the ClientBinder
* to interpret that data and display it on the client. In general, client binders communicate with the
* ClientManager by invoking a proper method on them, and passing themself as an argument, since they hold
* the information necessary for the proper work with the data (such as filters, skip, take...)
*/
Telerik.Sitefinity.Web.UI.ClientBinder = function () {
    // private fields
    this._target = null;
    this._targetId = null;
    this._serviceBaseUrl = null;
    this._clientTemplates = null;
    this._sortExpression = null;
    this._skip = null;
    this._take = null;
    this._filterExpression = null;
    this._bindOnLoad = null;
    this._serviceCallback = null;
    this._allowAutomaticDelete = null;
    this._parentItemId = null;
    this._managerType = null;

    this._saveInvokerId = null;

    this._provider = null;

    this._dataMembers = null;
    this._dataKeyNames = null;
    this._dataKeys = null;
    this._globalDataKeys = null;
    this._urlParams = null;
    this._culture = null;
    this._uiCulture = null;
    this._isMultilingual = null;
    this._dataType = null;

    this._validationRules = null;

    this._loadingPanelID = null;
    this._loadingPanelsToHide = [];
    this._isBinding = false;

    this._manager = null;

    // event delegates - specified through the server representation of the ClientBinder implementation.
    // These events allow the developers, to hook up into the lifecycle of the ClientBinder and execute
    // arbitrary logic at designated stages.
    this._binderInitializedDelegate = null;
    this._dataBoundDelegate = null;
    this._dataBindingDelegate = null;
    this._internalDataBoundDelegate = null;
    this._itemDataBindingDelegate = null;
    this._itemDataBoundDelegate = null;
    this._targetCommandDelegate = null;
    this._itemEditCommandDelegate = null;
    this._itemDeleteCommandDelegate = null;
    this._itemSelectCommandDelegate = null;
    this._itemCancelCommandDelegate = null;
    this._itemSaveCommandDelegate = null;
    this._itemCommandDelegate = null;
    this._itemSavingDelegate = null;
    this._savingDelegate = null;
    this._savedDelegate = null;
    this._editDelegate = null;
    this._deletingDelegate = null;
    this._deletedDelegate = null;
    this._errorDelegate = null;
    this._startProcessingDelegate = null;
    this._endProcessingDelegate = null;

    //command assigning closures
    this._fnAssignDeleteCommand = null;
    this._fnAssignEditCommand = null;
    this._fnAssignSelectCommand = null;
    this._fnAssignCancelCommand = null;
    this._fnAssignSaveCommand = null;
    this._fnAssignCustomCommand = null;
    this._fnApplyLocalizationTransformations = null;

    this._unescapeHtml = null;
    this._unescapableTags = ["object", "script", "embed"];

    this._checkboxClass = null;
    this._dropdownClass = null;
    this._displayedItemsCount = 0;
    this._isFiltering = false;
    this._workflowOperation = null;
    this._contextBag = null;

    Telerik.Sitefinity.Web.UI.ClientBinder.initializeBase(this);
}

Telerik.Sitefinity.Web.UI.ClientBinder.prototype = {
    // set up and teardown
    initialize: function () {
        // Register events
        if (this._binderInitializedDelegate === null) {
            this._binderInitializedDelegate = Function.createDelegate(this, this._binderInitializedHandler);
        }
        if (this._dataBindingDelegate === null) {
            this._dataBindingDelegate = Function.createDelegate(this, this._dataBindingHandler);
        }
        if (this._dataBoundDelegate === null) {
            this._dataBoundDelegate = Function.createDelegate(this, this._dataBoundHandler);
        }
        if (this._itemDataBindingDelegate == null) {
            this._itemDataBindingDelegate = Function.createDelegate(this, this._itemDataBindingHandler);
        }
        if (this._itemDataBoundDelegate === null) {
            this._itemDataBoundDelegate = Function.createDelegate(this, this._itemDataBoundHandler);
        }
        if (this._targetCommandDelegate === null) {
            this._targetCommandDelegate = Function.createDelegate(this, this._targetCommandHandler);
        }
        if (this._itemEditCommandDelegate === null) {
            this._itemEditCommandDelegate = Function.createDelegate(this, this._itemEditCommandHandler);
        }
        if (this._itemDeleteCommandDelegate === null) {
            this._itemDeleteCommandDelegate = Function.createDelegate(this, this._itemDeleteCommandHandler);
        }
        if (this._itemSelectCommandDelegate === null) {
            this._itemSelectCommandDelegate = Function.createDelegate(this, this._itemSelectCommandHandler);
        }
        if (this._itemCancelCommandDelegate == null) {
            this._itemCancelCommandDelegate = Function.createDelegate(this, this._itemCancelCommandHandler);
        }
        if (this._itemSaveCommandDelegate == null) {
            this._itemSaveCommandDelegate = Function.createDelegate(this, this._itemSaveCommandHandler);
        }
        if (this._itemCommandDelegate == null) {
            this._itemCommandDelegate = Function.createDelegate(this, this._itemCommandHandler);
        }
        if (this._itemSavingDelegate === null) {
            this._itemSavingDelegate = Function.createDelegate(this, this._itemSavingHandler);
        }
        if (this._savingDelegate === null) {
            this._savingDelegate = Function.createDelegate(this, this._savingHandler);
        }
        if (this._savedDelegate === null) {
            this._savedDelegate = Function.createDelegate(this, this._savedHandler);
        }
        if (this._editDelegate == null) {
            this._editDelegate = Function.createDelegate(this, this._editHandler);
        }
        if (this._deletingDelegate === null) {
            this._deletingDelegate = Function.createDelegate(this, this._deletingHandler);
        }
        if (this._deletedDelegate === null) {
            this._deletedDelegate = Function.createDelegate(this, this._deletedHandler);
        }
        if (this._errorDelegate === null) {
            this._errorDelegate = Function.createDelegate(this, this._errorHandler);
        }
        if (this._startProcessingDelegate === null) {
            this._startProcessingDelegate = Function.createDelegate(this, this._startProcessingHandler);
        }
        if (this._endProcessingDelegate === null) {
            this._endProcessingDelegate = Function.createDelegate(this, this._endProcessingHandler);
        }
        this._showLoadingPanelDelegate = Function.createDelegate(this, this.showLoadingPanel);
        this._hideLoadingPanelsDelegate = Function.createDelegate(this, this.hideLoadingPanels);
        this._saveChangesDelegate = Function.createDelegate(this, this.SaveChanges);

        if (this._loadingPanelID && $get(this._loadingPanelID) != null) {
            this.add_onStartProcessing(this._showLoadingPanelDelegate);
            this.add_onEndProcessing(this._hideLoadingPanelsDelegate);
        }

        if (this._bindOnLoad) {
            this.DataBind();
        }

        if (this._saveInvokerId != null) {
            var saveInvokerElement = $get(this._saveInvokerId);
            if (saveInvokerElement)
                $addHandler(saveInvokerElement, 'click', this._saveChangesDelegate);
            else
                this.ThrowBinderException('The element with id"' + this._saveInvokerId + '" specified as the SaveInvoker cannot be found.');

        }

        Telerik.Sitefinity.Web.UI.ClientBinder.callBaseMethod(this, "initialize");

        // add regular expression validation rule
        jQuery.validator.addMethod("regex", function (value, element, params) {
            var regex = new RegExp(params);
            return this.optional(element) || regex.test(value);
        },
                                   jQuery.format("The given value does not match the regular expression rule.")
        );

        jQuery(document.forms[0]).validate();

        var binder = this;

        this._documentReadyJQueryCallback = function () {
            binder._binderInitializedHandler(this, Sys.EventArgs.Empty);
        };

        $(document).ready(this._documentReadyJQueryCallback);
    },
    dispose: function () {
        // Clean up events
        if (this._binderInitializedDelegate) {
            delete this._binderInitializedDelegate;
        }
        if (this._dataBindingDelegate) {
            delete this._dataBindingDelegate;
        }
        if (this._dataBoundDelegate) {
            delete this._dataBoundDelegate;
        }
        if (this._itemDataBindingDelegate) {
            delete this._itemDataBindingDelegate;
        }
        if (this._itemDataBoundDelegate) {
            delete this._itemDataBoundDelegate;
        }
        if (this._internalDataBoundDelegate) {
            delete this._internalDataBoundDelegate;
        }
        if (this._targetCommandDelegate) {
            delete this._targetCommandDelegate;
        }
        if (this._itemEditCommandDelegate) {
            delete this._itemEditCommandDelegate;
        }
        if (this._itemDeleteCommandDelegate) {
            delete this._itemDeleteCommandDelegate;
        }
        if (this._itemSelectCommandDelegate) {
            delete this._itemSelectCommandDelegate;
        }
        if (this._itemCancelCommandDelegate) {
            delete this._itemCancelCommandDelegate;
        }
        if (this._itemSaveCommandDelegate) {
            delete this._itemSaveCommandDelegate;
        }
        if (this._itemCommandDelegate) {
            delete this._itemCommandDelegate;
        }
        if (this._itemSavingDelegate) {
            delete this._itemSavingDelegate;
        }
        if (this._savingDelegate) {
            delete this._savingDelegate;
        }
        if (this._savedDelegate) {
            delete this._savedDelegate;
        }
        if (this._editDelegate) {
            delete this._editDelegate;
        }
        if (this._deletingDelegate) {
            delete this._deletingDelegate;
        }
        if (this._deletedDelegate) {
            delete this._deletedDelegate;
        }
        if (this._errorDelegate) {
            delete this._errorDelegate;
        }
        if (this._startProcessingHandler) {
            delete this._startProcessingHandler;
        }
        if (this._endProcessingHandler) {
            delete this._endProcessingHandler;
        }
        if (this._documentReadyJQueryCallback) {
            $(document).unbind("ready", this._documentReadyJQueryCallback);
        }
        this._showLoadingPanelDelegate = null;
        this._hideLoadingPanelsDelegate = null;
        this._saveChangesDelegate = null;

        // cllosures deletion
        if (this._fnAssignDeleteCommand) {
            delete this._fnAssignDeleteCommand;
        }
        if (this._fnAssignEditCommand) {
            delete this._fnAssignEditCommand;
        }
        if (this._fnAssignSelectCommand) {
            delete this._fnAssignSelectCommand;
        }
        if (this._fnAssignCancelCommand) {
            delete this._fnAssignCancelCommand;
        }
        if (this._fnAssignSaveCommand) {
            delete this._fnAssignSaveCommand;
        }
        if (this._fnAssignCustomCommand) {
            delete this._fnAssignCustomCommand;
        }
        if (this._fnApplyLocalizationTransformations) {
            delete this._fnApplyLocalizationTransformations;
        }

        Telerik.Sitefinity.Web.UI.ClientBinder.callBaseMethod(this, "dispose");
    },

    // EVENTS BINDING AND UNBINDING
    add_onBinderInitialized: function (delegate) {
        this.get_events().addHandler('onBinderInitialized', delegate);
    },
    remove_onBinderInitialized: function (delegate) {
        this.get_events().removeHandler('onBinderInitialized', delegate);
    },
    add_onDataBinding: function (delegate) {
        this.get_events().addHandler('onDataBinding', delegate);
    },
    remove_onDataBinding: function (delegate) {
        this.get_events().removeHandler('onDataBinding', delegate);
    },
    add_onDataBound: function (delegate) {
        this.get_events().addHandler('onDataBound', delegate);
    },
    remove_onDataBound: function (delegate) {
        this.get_events().removeHandler('onDataBound', delegate);
    },
    add_onItemDataBinding: function (delegate) {
        this.get_events().addHandler('onItemDataBinding', delegate);
    },
    remove_onItemDataBinding: function (delegate) {
        this.get_events().removeHandler('onItemDataBinding', delegate);
    },
    add_onItemDataBound: function (delegate) {
        this.get_events().addHandler('onItemDataBound', delegate);
    },
    remove_onItemDataBound: function (delegate) {
        this.get_events().removeHandler('onItemDataBound', delegate);
    },
    add_onItemDomCreated: function (delegate) {
        this.get_events().addHandler('onItemDomCreated', delegate);
    },
    remove_onItemDomCreated: function (delegate) {
        this.get_events().removeHandler('onItemDomCreated', delegate);
    },
    add_onTargetCommand: function (delegate) {
        this.get_events().addHandler('onTargetCommand', delegate);
    },
    remove_onTargetCommand: function (delegate) {
        this.get_events().removeHandler('onTargetCommand', delegate);
    },
    add_onItemEditCommand: function (delegate) {
        this.get_events().addHandler('onItemEditCommand', delegate);
    },
    remove_onEditCommand: function (delegate) {
        this.get_events().removeHandler('onItemEditCommand', delegate);
    },
    add_onItemDeleteCommand: function (delegate) {
        this.get_events().addHandler('onItemDeleteCommand', delegate);
    },
    remove_onItemDeleteCommand: function (delegate) {
        this.get_events().removeHandler('onItemDeleteCommand', delegate);
    },
    add_onItemSelectCommand: function (delegate) {
        this.get_events().addHandler('onItemSelectCommand', delegate);
    },
    remove_onItemSelectCommand: function (delegate) {
        this.get_events().removeHandler('onItemSelectCommand', delegate);
    },
    add_onItemCancelCommand: function (delegate) {
        this.get_events().addHandler('onItemCancelCommand', delegate);
    },
    remove_onItemCancelCommand: function (delegate) {
        this.get_events().removeHandler('onItemCancelCommand', delegate);
    },
    add_onItemSaveCommand: function (delegate) {
        this.get_events().addHandler('onItemSaveCommand', delegate);
    },
    remove_onItemSaveCommand: function (delegate) {
        this.get_events().removeHandler('onItemSaveCommand', delegate);
    },
    add_onItemCommand: function (delegate) {
        this.get_events().addHandler('onItemCommand', delegate);
    },
    remove_onItemCommand: function (delegate) {
        this.get_events().removeHandler('onItemCommand', delegate);
    },
    add_onItemSaving: function (delegate) {
        this.get_events().addHandler('onItemSaving', delegate);
    },
    remove_onItemSaving: function (delegate) {
        this.get_events().removeHandler('onItemSaving', delegate);
    },
    add_onSaving: function (delegate) {
        this.get_events().addHandler('onSaving', delegate);
    },
    remove_onSaving: function (delegate) {
        this.get_events().addHandler('onSaving', delegate);
    },
    add_onSaved: function (delegate) {
        this.get_events().addHandler('onSaved', delegate);
    },
    remove_onSaved: function (delegate) {
        this.get_events().removeHandler('onSaved', delegate);
    },
    add_onDeleting: function (delegate) {
        this.get_events().addHandler('onDeleting', delegate);
    },
    remove_onDeleting: function (delegate) {
        this.get_events().removeHandler('onDeleting', delegate);
    },
    add_onDeleted: function (delegate) {
        this.get_events().addHandler('onDeleted', delegate);
    },
    remove_onDeleted: function (delegate) {
        this.get_events().removeHandler('onDeleted', delegate);
    },
    add_onError: function (delegate) {
        this.get_events().addHandler('onError', delegate);
    },
    remove_onError: function (delegate) {
        this.get_events().removeHandler('onError', delegate);
    },
    add_onStartProcessing: function (delegate) {
        this.get_events().addHandler('onStartProcessing', delegate);
    },
    remove_onStartProcessing: function (delegate) {
        this.get_events().removeHandler('onStartProcessing', delegate);
    },
    add_onEndProcessing: function (delegate) {
        this.get_events().addHandler('onEndProcessing', delegate);
    },
    remove_onEndProcessing: function (delegate) {
        this.get_events().removeHandler('onEndProcessing', delegate);
    },

    /* EVENT HANDLERS  */
    _binderInitializedHandler: function () {
        var eventArgs = this._getEventArgs();
        var h = this.get_events().getHandler('onBinderInitialized');
        if (h) h(this, eventArgs);
        return eventArgs;
    },
    _dataBoundHandler: function (data) {
        var eventArgs = this._getEventArgs(null, data);
        var h = this.get_events().getHandler('onDataBound');
        if (h) h(this, eventArgs);
        return eventArgs;
    },
    _dataBindingHandler: function (data) {
        var eventArgs = this._getEventArgs(null, data);
        var h = this.get_events().getHandler('onDataBinding');
        if (h) h(this, eventArgs);
        return eventArgs;
    },
    _targetCommandHandler: function (commandName, commandArgument) {
        var eventArgs = { 'CommandName': commandName, 'CommandArgument': commandArgument };
        var h = this.get_events().getHandler('onTargetCommand');
        if (h) h(this, eventArgs);
        return eventArgs;
    },
    _itemDataBindingHandler: function (dataItem) {
        var itemEventArgs = dataItem;
        var h = this.get_events().getHandler('onItemDataBinding');
        if (h) h(this, itemEventArgs);
        return itemEventArgs;
    },
    _itemDataBoundHandler: function (key, dataItem, itemIndex, itemElement) {
        var itemEventArgs = this._getItemEventArgs(key, dataItem, itemIndex, itemElement);
        var h = this.get_events().getHandler('onItemDataBound');
        if (h) h(this, itemEventArgs);
        return itemEventArgs;
    },
    _raiseItemDomCreated: function (key, dataItem, itemIndex, itemElement) {
        var itemEventArgs = this._getItemEventArgs(key, dataItem, itemIndex, itemElement);
        var h = this.get_events().getHandler('onItemDomCreated');
        if (h) h(this, itemEventArgs);
        return itemEventArgs;
    },
    _itemEditCommandHandler: function (key, dataItem, itemIndex, itemElement) {
        var itemEventArgs = this._getItemEventArgs(key, dataItem, itemIndex, itemElement, null, 'edit');
        var h = this.get_events().getHandler('onItemEditCommand');
        if (h) h(this, itemEventArgs);
    },
    _itemDeleteCommandHandler: function (key, dataItem, itemIndex, itemElement) {
        var itemEventArgs = this._getItemEventArgs(key, dataItem, itemIndex, itemElement, null, 'delete');
        var h = this.get_events().getHandler('onItemDeleteCommand');
        if (h) h(this, itemEventArgs);
    },
    _itemSelectCommandHandler: function (key, dataItem, itemIndex, itemElement) {
        this._onItemSelecting(key, dataItem, itemIndex, itemElement);
        var itemEventArgs = this._getItemEventArgs(key, dataItem, itemIndex, itemElement, null, 'select');
        var h = this.get_events().getHandler('onItemSelectCommand');
        if (h) h(this, itemEventArgs);
    },
    _itemCancelCommandHandler: function (key, dataItem, itemIndex, itemElement) {
        var itemEventArgs = this._getItemEventArgs(key, dataItem, itemIndex, itemElement, null, 'cancel');
        var h = this.get_events().getHandler('onItemCancelCommand');
        if (h) h(this, itemEventArgs);
    },
    _itemSaveCommandHandler: function (key, dataItem, itemIndex, itemElement) {
        var itemEventArgs = this._getItemEventArgs(key, dataItem, itemIndex, itemElement, null, 'save');
        var h = this.get_events().getHandler('onItemSaveCommand');
        if (h) h(this, itemEventArgs);
    },
    _itemCommandHandler: function (key, dataItem, itemIndex, itemElement, commandName, commandArgument) {
        var itemEventArgs = this._getItemEventArgs(key, dataItem, itemIndex, itemElement, null, commandName, commandArgument);
        var h = this.get_events().getHandler('onItemCommand');
        if (h) h(this, itemEventArgs);
    },
    _itemSavingHandler: function (key, dataItem, itemIndex, itemElement, propertyBag) {
        var itemEventArgs = this._getItemEventArgs(key, dataItem, itemIndex, itemElement, propertyBag);
        var h = this.get_events().getHandler('onItemSaving');
        if (h) h(this, itemEventArgs);
        return itemEventArgs;
    },
    _savingHandler: function () {
        var eventArgs = this._getEventArgs();
        var h = this.get_events().getHandler('onSaving');
        if (h) h(this, eventArgs);
        return eventArgs;
    },
    _savedHandler: function () {
        var eventArgs = this._getEventArgs();
        var h = this.get_events().getHandler('onSaved');
        if (h) h(this, eventArgs);
        return eventArgs;
    },
    _editHandler: function (key) {
        var eventArgs = this._getEventArgs(key);
        var h = this.get_events().getHandler('onEdit');
        if (h) h(this, eventArgs);
        return eventArgs;
    },
    _deletingHandler: function (key) {
        var eventArgs = this._getEventArgs(key);
        var h = this.get_events().getHandler('onDeleting');
        if (h) h(this, eventArgs);
        return eventArgs;
    },
    _deletedHandler: function (key) {
        this.RemoveItemKey(key);
        var eventArgs = this._getEventArgs(key);
        var h = this.get_events().getHandler('onDeleted');
        if (h) h(this, eventArgs);
        return eventArgs;
    },
    _startProcessingHandler: function () {
        this._isBinding = true;
        var eventArgs = this._getEventArgs();
        var h = this.get_events().getHandler('onStartProcessing');
        if (h) h(this, eventArgs);
        return eventArgs;
    },
    _endProcessingHandler: function () {
        this._isBinding = false;
        var eventArgs = this._getEventArgs();
        var h = this.get_events().getHandler('onEndProcessing');
        if (h) h(this, eventArgs);
        return eventArgs;
    },
    _getEventArgs: function (key, dataItem) {
        var eventArgs = new Telerik.Sitefinity.Web.UI.ClientBinderEventArgs(this.GetTarget(), key, dataItem);
        return eventArgs;
    },
    _getItemEventArgs: function (key, dataItem, itemIndex, itemElement, propertyBag, commandName, commandArgument) {
        var itemEventArgs = new Telerik.Sitefinity.Web.UI.ItemEventArgs(key, dataItem, itemIndex, itemElement, propertyBag, commandName, commandArgument);
        return itemEventArgs;
    },
    _errorHandler: function (error) {
        var eventArgs = new Telerik.Sitefinity.Web.UI.ErrorEventArgs(error);
        var h = this.get_events().getHandler('onError');
        if (h) h(this, eventArgs);
        return eventArgs;
    },
    _onItemSelecting: function (key, dataItem, itemIndex, itemElement) {
    },

    /* METHODS */

    // Resets paging
    ClearPager: function () {

    },

    // Abstract function for data binding, which needs to be implemented by all classes inheriting
    // from ClientBinder class.
    // If you provide the data parameter and the _serviceCallback has been set the binder will bind
    // to this data without a service call (offline DataBinding).
    DataBind: function (data, context) {
        this._dataKeys = null;

        if (data != null && this._serviceCallback == null) {
            alert('You have to specify the serviceCallback in the inherited binder if you want to bind offline');
        }
        else if (data != null) {
            this._serviceCallback(data, context);
        }
    },

    // Abstract function for saving changes, which needs to be implemented by all classes inheriting 
    // from ClientBinder class.
    SaveChanges: function () {
        var message = 'Call to abstract function. SaveChanges function has to be implemented on the concrete ' +
                      'binder. ClientBinder is a base implementation for all client binders.';
        alert(message);

        // TODO: merge data here from all the client binders.

    },
    // Abstract function for binding a collection of items, which needs to be implemented by all classes inheriting 
    // from ClientBinder class. This is the function will be called on a callback from the ClientManager with
    // the CollectionContext (defined on the server) passed as the data argument.
    BindCollection: function (data) {
        var message = 'Call to abstract function. BindCollection function has to be implemented on the concrete ' +
                      'binder. ClientBinder is a base implementation for all client binders.';
        alert(message);
    },
    // Abstract function for binding a single item, which needs to be implemented by all classes inheriting 
    // from ClientBinder class. This is the function will be called on a callback from the ClientManager with
    // the requested object passed as data argument.
    BindItem: function (data) {
        var message = 'Call to abstract function. BindItem function has to be implemented on the concrete ' +
                      'binder. ClientBinder is a base implementation for all client binders.';
        alert(message);
    },
    // Abstract function for updating a set of items, which needs to be implemented by all classes inheriting 
    // from ClientBinder class.
    UpdateItems: function (data) {
        var message = 'Call to abstract function. UpdateItems function has to be implemented on the concrete ' +
                      'binder. ClientBinder is a base implementation for all client binders.';
        alert(message);
    },
    DeleteItem: function (key, language) {
        var clientManager = this.get_manager();
        var args = this._deletingHandler(key);
        if (args.get_cancel() === false) {
            clientManager.DeleteItem(this, key, language);
            //fix for #57677: The explicit call to _deletedHandler() was commented out should not be invoked here.
            //Instead, the clientmanager will raise the event when it's done with the deletion.
            //this._deletedHandler(key);
        }
    },
    deleteItems: function (keys, callback, caller, language, checkRelatingData) {
        var clientManager = this.get_manager();
        var args = this._deletingHandler(keys);
        if (args.get_cancel() === false) {
            clientManager.deleteItems(this, keys, callback, caller, language, checkRelatingData);
        }
    },
    // Gets the property bag of the properties to be persisted inside of a given container. The function will
    // return a jagged array, where the first dimension represents the property, and second dimension has two
    // items : Property Name (0) and Property Value (1). It is the responsibility of the WCF classes to
    // interpret this data and instantiate objects from them (so that the Factory pattern is followed).
    GetData: function (container, itemIndex) {
        if (!container) {
            container = this.GetTarget();
        }
        if (!itemIndex) {
            itemIndex = 0;
        }
        var objectProps = [];
        var searchPattern = '';
        if (this._dataMembers == null)
            this.ThrowBinderException('GetData function must not be called, if the the DataMembers array has not been declared.');

        this._dataMembers.concat(this._dataKeyNames);

        for (dataMemberIndex = 0, dataMembersLen = this._dataMembers.length; dataMemberIndex < dataMembersLen; dataMemberIndex++) {
            searchPattern += "[id='" + this._dataMembers[dataMemberIndex] + itemIndex + "'],";
        }
        searchPattern = searchPattern.substr(0, searchPattern.length - 1);
        var dataElements = $(container).find(searchPattern);
        for (dataElementIndex = 0, dataMembersLen = this._dataMembers.length; dataElementIndex < dataMembersLen; dataElementIndex++) {
            var element = dataElements[dataElementIndex];
            var propNameValue = [2];
            propNameValue[0] = element.id.substring(0, element.id.length - itemIndex.toString().length);

            if ($(element).hasClass('radioList')) {
                $(element).find('input:checked').each(function (i, element) {
                    propNameValue[1] = $(element).val();
                });

            }
            else if ($(element).hasClass('checkboxList')) {
                $(element).find('input:checked').each(function (i, element) {
                    propNameValue[1] = $(element).val();
                });
            }
            else if ($(element).hasClass('optionList')) {
                $(element).find('option:selected').each(function (i, element) {
                    propNameValue[1] = $(element).val();
                });
            }
            else if ($(element).prop('type').toLowerCase() == 'checkbox') {
                propNameValue[1] = element.checked;
            }
            else {
                propNameValue[1] = element.value;
            }
            objectProps.push(propNameValue);
        }
        return objectProps;
    },

    // This function merges the property bag of the binder with the property bag
    // that was passed to it and returns the merged property bag.
    // The properties of the binder always take priority over the properties passed as
    // the argument.
    MergePropertyBags: function (propertyBag) {
        if (!dataItemIndex) {
            dataItemIndex = 0;
        }
        var binderPropertyBag = this.GetData(this.GetTarget());
        var mergedBag = propertyBag.concat(binderPropertyBag);
        return mergedBag;
    },

    EnsureDataItemIntegrity: function (dataItem) {
        if (!dataItem) {
            dataItem = [];
        }

        if (this._dataKeyNames) {
            for (dataKeyNameIndex = 0, keyNamesLen = this._dataKeyNames.length; dataKeyNameIndex < keyNamesLen; dataKeyNameIndex++) {
                var keyName = this._dataKeyNames[dataKeyNameIndex];
                if (dataItem[keyName] === undefined) {
                    dataItem[keyName] = '';
                }
            }
        }
        if (this._dataMembers) {
            for (dataMemberIndex = 0, dataMembersLen = this._dataMembers.length; dataMemberIndex < dataMembersLen; dataMemberIndex++) {
                var dataMemberName = this._dataMembers[dataMemberIndex];
                if (dataItem[dataMemberName] === undefined) {
                    dataItem[dataMemberName] = '';
                }
            }
        }
        return dataItem;
    },

    GetTarget: function () {
        if (this._target)
            return this._target;
        if (this._targetId.length == 0)
            return null;
        this._target = $get(this._targetId);
        return this._target;
    },
    // Clears all the markup from the target control.
    ClearTarget: function () {
        var target = this.GetTarget();
        if (target != null) {
            if (target.hasChildNodes()) {
                while (target.childNodes.length >= 1) {
                    target.removeChild(target.firstChild);
                }
            }
            //target.innerHTML = '';
        }
    },
    ThrowBinderException: function (message) {
        var exception = message;
        exception += '\n';
        exception += "Binder instance: " + Object.getType(this).getName();
        exception += '\n';
        exception += "Binder target id: " + this._targetId;
        alert(exception);
    },

    LoadItem: function (itemContainer, itemTemplate, dataItem, isGeneric, itemIndex) {
        // instantiate the template
        var dataItem = this.EnsureDataItemIntegrity(dataItem);
        if (isGeneric) {
            if (this._dataMembers == null)
                this.ThrowBinderException('DataMembers array has not been set.');
            if (this._dataMembers.length > 1)
                this.ThrowBinderException('You cannot use generic collections with more than one DataMember.');
            var defaultDataItem = [];
            defaultDataItem[this._dataMembers[0]] = dataItem;
            dataItem = defaultDataItem;
        }

        itemTemplate.instantiateIn(itemContainer, null, dataItem, itemIndex);

        if (this._unescapeHtml) {
            var tags = this.get_unescapableTags();
            var len = tags.length;
            $(itemContainer).find('.sys-container:last, *:last').each(function () {
                var unescapedHtml = $(this).html().replace(/&lt;/g, '<').replace(/&gt;/g, '>').replace(/&amp;/g, '&');
                for (var i = 0; i < len; i++) {
                    var tag = tags[i];
                    unescapedHtml = unescapedHtml.replace("<" + tag, "<input type='hidden'").replace("</" + tag + ">", "</input");
                }
                $(this).html(unescapedHtml);
            });
        }

        //Fix: Make sure that checkboxes are checked according to their input value.
        $(itemContainer).find("input[type='checkbox']").each(function () {
            var valueSet = this.getAttribute("value");
            if ((valueSet != null) && ((valueSet.toUpperCase() == "TRUE") || (valueSet.toUpperCase() == "FALSE"))) {
                this.checked = valueSet;
            }
        });
    },

    EnableActionMenus: function (itemContainer) {
        // set the action menu on the item
        var items = jQuery(itemContainer).find('.actionsMenu').not(jQuery(this).parents(".actionsMenu"));
        var processed = [];

        for (var i = 0, length = items.length; i < length; i++) {
            var element = items[i];
            var item = jQuery(element);
            var iter = processed.length;
            var skip = false;
            // checking if some of the parents of the current element has already a actions menu and if fond we skip it
            // because for trees the function is called on each of the nested elements
            while (iter--) {
                if (processed[iter].find(item)) {
                    skip = true;
                    break;
                }
            }
            processed.push(item);
            if (!skip) {
                item.clickMenu();
                item.find(".main:first-child").addClass("sfFirst");
                item.find(".main:last-child").addClass("sfLast");
            }
        }
    },

    // replaces the textboxes with a more appropriate user interface elements, based on the class
    BuildInputWrappers: function (container) {
        var wrapperSuffix = '-wrapper';

        this._configureBooleanProperties(container, wrapperSuffix);
        this._configureEnumProperties(container, wrapperSuffix);
    },

    _configureBooleanProperties: function (container, wrapperSuffix) {
        // input wrapper for boolean properties
        var checkboxSelector = '.' + this._checkboxClass + ' input[type="text"]';
        $(container).find(checkboxSelector).each(function () {
            var txt = this;
            // hide the textbox
            $(txt).hide();
            // append the checkbox
            var wrapperId = $(txt).attr('id') + wrapperSuffix;
            var checkBox = document.createElement('input');
            checkBox.setAttribute('type', 'checkbox');
            checkBox.setAttribute('id', wrapperId);
            var value = $(txt).val();
            if (value.toLowerCase() == 'true') {
                if ($telerik.isIE) {
                    checkBox.setAttribute('defaultChecked', 'defaultChecked');
                }
                $(checkBox).attr('checked', true);
            }
            $(checkBox).insertAfter(txt);
            // ensure control/wrapper synchronization
            $(checkBox).each(function () {
                $(this).click(function () {
                    // change the value of textbox
                    if ($(this).is(':checked')) {
                        $(txt).val('true');
                    }
                    else {
                        $(txt).val('false');
                    }
                    $(txt).trigger('change');
                });
                // prevent memory leaks
                $(this).on("unload", function (e) {
                    jQuery.event.remove(this);
                    jQuery.removeData(this);
                });
            });
        });

        // change the "for" attribute for the labels associated with wrapped checkboxes
        $(container).find('.' + this._checkboxClass + ' label').each(function () {
            var forAttr = $(this).attr('for') + wrapperSuffix;
            $(this).attr('for', forAttr);
        });
    },

    _configureEnumProperties: function (container, wrapperSuffix) {
        // input wrapper for enum properties
        var dropdownSelector = '.' + this._dropdownClass + ' input[type="text"]';
        $(container).find(dropdownSelector).each(function () {
            var txt = this;
            var originalValue = $(txt).val();
            if (originalValue && originalValue.length > 0) {
                // hide the textbox
                $(txt).hide();
                // append the dropdown
                var wrapperId = $(txt).attr('id') + wrapperSuffix;
                var dropdown = document.createElement('select');
                dropdown.setAttribute('name', wrapperId);
                dropdown.setAttribute('id', wrapperId);

                var jsonValue = JSON.parse(originalValue);
                var selectedValue = jsonValue.Value;
                var values = jsonValue.Options;

                // Update the textbox with the current value
                $(txt).val(selectedValue);

                var selectedValueIcon = null;
                for (var i = 0; i < values.length; i++) {
                    var optionData = values[i];
                    var optionValue = optionData;
                    var option = document.createElement("option");
                    if (optionData.hasOwnProperty("Value")) {
                        option.value = optionData.Value;
                        option.text = optionData.Text;

                        if (optionData.hasOwnProperty("Icon")) {
                            option.setAttribute('data-icon', optionData.Icon);
                            if (selectedValueIcon == null) {
                                selectedValueIcon = document.createElement('span');
                            }
                        }

                        optionValue = optionData.Value;
                    }
                    else {

                        option.value = optionValue;
                        option.text = optionValue;
                    }

                    if (optionValue == selectedValue) {
                        option.setAttribute('selected', 'selected');
                        if (selectedValueIcon) {
                            selectedValueIcon.title = option.text;
                            selectedValueIcon.className = option.getAttribute('data-icon');
                        }

                        if (optionData.hasOwnProperty("IsReadOnly")) {
                            if (optionData.IsReadOnly == true) {
                                dropdown.setAttribute('disabled', true);                                
                            }                        
                        }
                    }

                    dropdown.options.add(option);
                }

                $(dropdown).insertAfter(txt);
                if (selectedValueIcon)
                    $(selectedValueIcon).insertAfter(dropdown);

                // ensure control/wrapper synchronization
                $(dropdown).each(function () {
                    $(this).change(function () {
                        // change the value of the dropdown
                        $(txt).val($(dropdown).val());
                        var icon = $(selectedValueIcon);
                        if (icon) {
                            $(icon).attr('title', $(dropdown).find('option:selected').text());
                            $(icon).attr('class', $(dropdown).find('option:selected').attr('data-icon'));
                        }

                        $(txt).trigger('change');
                    });
                    // prevent memory leaks
                    $(this).on("unload", function (e) {
                        jQuery.event.remove(this);
                        jQuery.removeData(this);
                    });
                });

            }            
        });
    },

    ApplyValidationRules: function (itemElement, itemIndex) {
        if (!this._validationRules) {
            return;
        }
        for (validationRuleIndex = 0, validationRulesLen = this._validationRules.length; validationRuleIndex < validationRulesLen; validationRuleIndex++) {
            var validationRule = this._validationRules[validationRuleIndex];
            var elementSelector = '#' + validationRule.GetUniqueId(itemIndex);
            $(itemElement).find(elementSelector).each(function (i, element) {
                // required
                if (validationRule.get_ruleName() == 'required') {
                    $(element).rules("add", {
                        required: true,
                        messages: {
                            required: validationRule.get_message()
                        }
                    });
                }
                // minlength
                if (validationRule.get_ruleName() == 'minlength') {
                    $(element).rules("add", {
                        minlength: validationRule.get_ruleArgument(),
                        messages: {
                            minlength: validationRule.get_message()
                        }
                    });
                }
                // maxlength
                if (validationRule.get_ruleName() == 'maxlength') {
                    $(element).rules("add", {
                        maxlength: validationRule.get_ruleArgument(),
                        messages: {
                            maxlength: validationRule.get_message()
                        }
                    });
                }
                // rangelength
                if (validationRule.get_ruleName() == 'rangelength') {
                    $(element).rules("add", {
                        rangelength: validationRule.get_ruleArgument(),
                        messages: {
                            rangelength: validationRule.get_message()
                        }
                    });
                }
                // min
                if (validationRule.get_ruleName() == 'min') {
                    $(element).rules("add", {
                        min: validationRule.get_ruleArgument(),
                        messages: {
                            min: validationRule.get_message()
                        }
                    });
                }
                // max
                if (validationRule.get_ruleName() == 'max') {
                    $(element).rules("add", {
                        max: validationRule.get_ruleArgument(),
                        messages: {
                            max: validationRule.get_message()
                        }
                    });
                }
                // range
                if (validationRule.get_ruleName() == 'range') {
                    $(element).rules("add", {
                        range: validationRule.get_ruleArgument(),
                        messages: {
                            range: validationRule.get_message()
                        }
                    });
                }
                // email
                if (validationRule.get_ruleName() == 'email') {
                    $(element).rules("add", {
                        email: true,
                        messages: {
                            email: validationRule.get_message()
                        }
                    });
                }
                // url
                if (validationRule.get_ruleName() == 'url') {
                    $(element).rules("add", {
                        url: true,
                        messages: {
                            url: validationRule.get_message()
                        }
                    });
                }
                // date
                if (validationRule.get_ruleName() == 'date') {
                    $(element).rules("add", {
                        date: true,
                        messages: {
                            date: validationRule.get_message()
                        }
                    });
                }
                // dateISO
                if (validationRule.get_ruleName() == 'dateISO') {
                    $(element).rules("add", {
                        dateISO: true,
                        messages: {
                            dateISO: validationRule.get_message()
                        }
                    });
                }
                // number
                if (validationRule.get_ruleName() == 'number') {
                    $(element).rules("add", {
                        number: true,
                        messages: {
                            number: validationRule.get_message()
                        }
                    });
                }
                // digits
                if (validationRule.get_ruleName() == 'digits') {
                    $(element).rules("add", {
                        digits: true,
                        messages: {
                            digits: validationRule.get_message()
                        }
                    });
                }
                // creditcard
                if (validationRule.get_ruleName() == 'creditcart') {
                    $(element).rules("add", {
                        creditcard: true,
                        messages: {
                            creditcard: validationRule.get_message()
                        }
                    });
                }
                // accept
                if (validationRule.get_ruleName() == 'accept') {
                    $(element).rules("add", {
                        accept: validationRule.get_ruleArgument(),
                        messages: {
                            accept: validationRule.get_message()
                        }
                    });
                }
                // equalTo
                if (validationRule.get_ruleName() == 'equalTo') {
                    $(element).rules("add", {
                        equalTo: '#' + validationRule.get_ruleArgument().toString() + itemIndex.toString(),
                        messages: {
                            equalTo: validationRule.get_message()
                        }
                    });
                }
                // regex
                if (validationRule.get_ruleName() == 'regex') {
                    $(element).rules("add", {
                        regex: validationRule.get_ruleArgument(),
                        messages: {
                            regex: validationRule.get_message()
                        }
                    });
                }
            });
        }
    },

    IsValid: function () {
        return $(document.forms[0]).validate().form();
    },

    ResetFormValidation: function () {
        $(document.forms[0]).validate().resetForm();
    },

    LoadDataItemKey: function (dataItem) {
        // set the data keys
        var key = [];
        if (this._dataKeyNames) {
            if (!this._dataKeys) {
                this._dataKeys = new Array();
            }
            for (dataKeyIndex = 0, dataKeysLen = this._dataKeyNames.length; dataKeyIndex < dataKeysLen; dataKeyIndex++) {
                var dataKeyName = this._dataKeyNames[dataKeyIndex];
                var dataKeyValue = dataItem[dataKeyName];
                if (dataKeyValue == null) {
                    continue;
                }
                if (dataKeyValue.length == 0) {
                    continue;
                }
                key[dataKeyName] = dataKeyValue;
                if (!key) {
                    return;
                }
            }
            this._dataKeys.push(key);
        }
    },

    RemoveItemKey: function (key) {
        // remove from the data keys collection
        if (this._dataKeys) {
            for (dataKeysIndex = 0, dataLeysLen = this._dataKeys.length; dataKeysIndex < dataLeysLen; dataKeysIndex++) {
                if (this.ObjectsAreSame(key, this._dataKeys[dataKeysIndex])) {
                    this._dataKeys = this._dataKeys.splice(dataKeysIndex, 1);
                    break;
                }
            }
        }
    },

    ObjectsAreSame: function (x, y) {
        var objectsAreSame = true;
        for (var propertyName in x) {
            if (x[propertyName] !== y[propertyName]) {
                objectsAreSame = false;
                break;
            }
        }
        return objectsAreSame;
    },

    EnsureTextLength: function (itemElement, maxLength) {
        $(itemElement).find('.sfEnsureTextLength').each(function (i, element) {
            var jElement = $(element);
            var val = jElement.text();
            if (val.length > maxLength) {
                val = val.substring(0, maxLength - 3);
                jElement.text(val + "...");
            }
            jElement.removeClass('sfEnsureTextLength');
        });
    },

    selectByIds: function (ids) {
        throw "selectByIds not implemented for the 'abstract' ClientBinder.";
    },

    AssignCommands: function (itemElement, dataItem, key, itemIndex) {
        var binder = this;
        var jItemElement = $(itemElement);

        // delete command
        this._assignDeleteCommand(jItemElement, binder, itemElement, dataItem, key, itemIndex);

        // edit command
        this._assignEditCommand(jItemElement, binder, itemElement, dataItem, key, itemIndex);

        // select command
        this._assignSelectCommand(jItemElement, binder, itemElement, dataItem, key, itemIndex);

        // cancel command
        this._assignCancelCommand(jItemElement, binder, itemElement, dataItem, key, itemIndex);

        // save command
        this._assignSaveCommand(jItemElement, binder, itemElement, dataItem, key, itemIndex);

        // custom commands
        this._assignCustomCommand(jItemElement, binder, itemElement, dataItem, key, itemIndex);

        // makes localization dependent template transformations
        this._applyLocalizationTransformations(jItemElement, binder, itemElement, dataItem, key, itemIndex);

        delete binder;
    },

    _assignDeleteCommand: function (jItemElement, binder, itemElement, dataItem, key, itemIndex) {
        if (this._fnAssignDeleteCommand) {
            delete this._fnAssignDeleteCommand;
        }
        // Closure
        this._fnAssignDeleteCommand = function (i, element) {
            var jElement = $(element);
            jElement.click(function (jEvent) {
                jEvent.preventDefault();
                binder._itemDeleteCommandHandler(key, dataItem, itemIndex, itemElement);
                if (binder._allowAutomaticDelete) {
                    var binderArgs = binder._deletingHandler(key);
                    if (binderArgs.get_cancel() == false) {
                        binder.DeleteItem(key);
                        binder._deletedHandler(key);
                        binder.DataBind();
                    }
                }
                return false;
            });
            // TODO: The unload event on the element is never fired which is a potential memory leak. Investigate the problem.
            // prevent memory leaks
            jElement.on("unload", function (e) {
                jQuery.event.remove(element);
                jQuery.removeData(element);
            });
        };
        jItemElement.find('.deleteCommand').each(this._fnAssignDeleteCommand);
    },

    _assignEditCommand: function (jItemElement, binder, itemElement, dataItem, key, itemIndex) {
        if (this._fnAssignEditCommand) {
            delete this._fnAssignEditCommand;
        }
        // Closure
        this._fnAssignEditCommand = function (i, element) {
            var jElement = $(element);
            jElement.click(function (jEvent) {
                jEvent.preventDefault();
                binder._itemEditCommandHandler(key, dataItem, itemIndex, itemElement);
                return false;
            });
            // TODO: The unload event on the element is never fired which is a potential memory leak. Investigate the problem.
            // prevent memory leaks
            jElement.on("unload", function (e) {
                jQuery.event.remove(element);
                jQuery.removeData(element);
            });
        }
        jItemElement.find('.editCommand').each(this._fnAssignEditCommand);
    },

    _assignSelectCommand: function (jItemElement, binder, itemElement, dataItem, key, itemIndex) {
        if (this._fnAssignSelectCommand) {
            delete this._fnAssignSelectCommand;
        }
        // Closure
        this._fnAssignSelectCommand = function (i, element) {
            var jElement = $(element);
            jElement.click(function (jEvent) {
                if (jElement.is('input:checkbox') || jElement.is('input:radio')) {
                    binder._itemSelectCommandHandler(key, dataItem, itemIndex, itemElement);
                }
                else {
                    jEvent.preventDefault();
                    binder._itemSelectCommandHandler(key, dataItem, itemIndex, itemElement);
                    return false;
                }
            });
            // TODO: The unload event on the element is never fired which is a potential memory leak. Investigate the problem.
            // prevent memory leaks
            jElement.on("unload", function (e) {
                jQuery.event.remove(element);
                jQuery.removeData(element);
            });
        }
        jItemElement.find('.selectCommand').each(this._fnAssignSelectCommand);
    },

    _assignCancelCommand: function (jItemElement, binder, itemElement, dataItem, key, itemIndex) {
        if (this._fnAssignCancelCommand) {
            delete this._fnAssignCancelCommand;
        }
        this._fnAssignCancelCommand = function (i, element) {
            var jElement = $(element);
            jElement.click(function (jEvent) {
                jEvent.preventDefault();
                binder._itemCancelCommandHandler(key, dataItem, itemIndex, itemElement);
                return false;
            });
            // TODO: The unload event on the element is never fired which is a potential memory leak. Investigate the problem.
            // prevent memory leaks
            jElement.on("unload", function (e) {
                jQuery.event.remove(element);
                jQuery.removeData(element);
            });
        }
        jItemElement.find('.cancelCommand').each(this._fnAssignCancelCommand);
    },

    _assignSaveCommand: function (jItemElement, binder, itemElement, dataItem, key, itemIndex) {
        if (this._fnAssignSaveCommand) {
            delete this._fnAssignSaveCommand;
        }
        this._fnAssignSaveCommand = function (i, element) {
            var jElement = $(element);
            jElement.click(function (jEvent) {
                jEvent.preventDefault();
                binder._itemSaveCommandHandler(key, dataItem, itemIndex, itemElement);
                return false;
            });
            // TODO: The unload event on the element is never fired which is a potential memory leak. Investigate the problem.
            // prevent memory leaks
            jElement.on("unload", function (e) {
                jQuery.event.remove(element);
                jQuery.removeData(element);
            });
        }
        jItemElement.find('.saveCommand').each(this._fnAssignSaveCommand);
    },

    _assignCustomCommand: function (jItemElement, binder, itemElement, dataItem, key, itemIndex) {
        if (this._fnAssignCustomCommand) {
            delete this._fnAssignCustomCommand;
        }

        this._fnAssignCustomCommand = function (i, element) {
            var jElement = $(element);
            var className = jElement.attr("class");
            var commandName = "";

            var matchResult = className.match(/sf_binderCommand_(\S*)/i);

            if (matchResult && matchResult[1]) {
                commandName = matchResult[1];
            }

            shouldAssignCommand = !jElement.hasClass('sfDisabled');
            if (shouldAssignCommand) {
                jElement.click(function (jEvent) {
                    var doPreventDefault = true;
                    //enable checkboxes to get checked/unchecked
                    if (this.type == "checkbox") doPreventDefault = false;
                    if (doPreventDefault) {
                        jEvent.preventDefault();
                    }
                    var commandNameObj = { commandName: commandName };
                    var commandArgument = binder._replaceArgumentValues(dataItem, commandNameObj);
                    binder._itemCommandHandler(key, dataItem, itemIndex, itemElement, commandNameObj.commandName, commandArgument);
                    jItemElement.find('.outerbox').each(function () {
                        $(this).hide();
                    });

                    return !doPreventDefault;
                });
            }
            else if (commandName == "void") {
                jElement.click(function (jEvent) {
                    return false;
                });
            }
            // the unload event is fired only for the window object.
            // prevent memory leaks
            $(element).on("unload", function (e) {
                jQuery.event.remove(element);
                jQuery.removeData(element);
            });
        }
        jItemElement.find("[class*='sf_binderCommand']").each(this._fnAssignCustomCommand);
    },

    _applyLocalizationTransformations: function (jItemElement, binder, itemElement, dataItem, key, itemIndex) {
        if (this._fnApplyLocalizationTransformations) {
            delete this._fnApplyLocalizationTransformations;
        }

        this._fnApplyLocalizationTransformations = function (i, element) {
            var jElement = jQuery(element);
            var className = jElement.attr("class");

            var matchResult = className.match(/sf_binderLocalization_(\S*)/i);
            if (matchResult && matchResult.length) {
                for (var i = 0, resultLength = matchResult.length; i < resultLength; i++) {
                    switch (matchResult[i]) {
                        case "showIfLanguageUnavailable":
                            binder._applyLocalizationTransformation_showIfLanguageUnavailable(jElement, binder, itemElement, dataItem, key, itemIndex);
                            break;
                        default:
                            break;
                    }
                }
            }
        }

        jItemElement.find("*[class^='sf_binderLocalization']").each(this._fnApplyLocalizationTransformations);
    },

    _applyLocalizationTransformation_showIfLanguageUnavailable: function (jElement, binder, itemElement, dataItem, key, itemIndex) {
        var showElement = false;
        var availableLanguages = dataItem.AvailableLanguages;

        //TODO decide on this check if the item is not ILocalizable (does not have AvailableLanguages) and what to do.
        if (this.get_isMultilingual() && availableLanguages && jQuery.isArray(availableLanguages)) {
            // The binder is binding a specific culture and it is unavailable for the current dataItem
            if (this.get_uiCulture() && jQuery.inArray(this.get_uiCulture(), availableLanguages) < 0) {
                showElement = true;
            }
        }

        if (showElement) {
            jElement.show();
        }
        else {
            jElement.remove();
        }
    },

    _replaceArgumentValues: function (dataItem, commandNameObj) {
        var argument = null;
        if (commandNameObj.commandName && dataItem) {
            var matches = commandNameObj.commandName.match(/\[\[(.+)\]\]/im);
            if (matches) {
                argument = matches[1];
                commandNameObj.commandName = commandNameObj.commandName.replace(matches[0], '');
            }
        }
        return argument;
    },

    _getPropertyValue: function (dataItem, propertyName) {
        var dotIndex = propertyName.indexOf(".");
        var property = dataItem[propertyName];
        if (property && dotIndex != -1) {
            var itemName = propertyName.slice(0, dotIndex);
            var itemValueName = propertyName.substring(dotIndex);
            if (itemName) {
                var item = dataItem[itemName];
                return this._getPropertyValue(item, itemValueName);
            }
        }
        return property;
    },

    AddValidationRule: function (id, ruleName, message, ruleArgument) {
        if (!this._validationRules) {
            this._validationRules = [];
        }

        var supportedRules = ['required', 'minlength', 'maxlength', 'rangelength', 'min', 'max', 'range', 'email', 'url', 'date', 'dateISO', 'number', 'digits', 'creditcard', 'accept', 'equalTo', 'regex'];
        if (supportedRules.indexOf(ruleName) == -1) {
            var supportedRulesString = '';
            for (sr = 0, supportedRulesLen = supportedRules.length; sr < supportedRulesLen; sr++) {
                supportedRulesString += supportedRules[sr] + '\n';
            }
            alert('Specified rule name "' + ruleName + '" is not supported. Following validation rules are supported:\n' + supportedRulesString);
            return;
        }

        var rulesThatRequireArgument = ['minlength', 'maxlength', 'rangelength', 'min', 'max', 'range', 'accept', 'equalTo'];

        if (rulesThatRequireArgument.indexOf(ruleName) > -1 && ruleArgument == null) {
            alert('Specified rule name "' + ruleName + '" requires a rule argument to be passed to AddValidationRule as fourth argument.');
            return;
        }

        var index = this._getValidationRuleIndex(id, ruleName);
        if (index === -1) {
            var validationRule = new Telerik.Sitefinity.Web.UI.ValidationRule(id, ruleName, message, ruleArgument);
            this._validationRules.push(validationRule);
        }
    },

    RemoveValidationRule: function (id, ruleName) {
        if (!this._validationRules) {
            return;
        }

        var index = this._getValidationRuleIndex(id, ruleName);
        if (index > -1) {
            var validationRule = this._validationRules[index];
            var elementSelector = '#' + validationRule.GetUniqueId(index);
            $(this.GetTarget()).find(elementSelector).each(function (i, element) {
                $(element).rules('remove', validationRule.get_ruleName());
            });

            this._validationRules.splice(index, 1);
        }
    },

    _getValidationRuleIndex: function (id, ruleName) {
        if (!this._validationRules) {
            return -1;
        }

        for (var i = 0; i < this._validationRules.length; i++) {
            var rule = this._validationRules[i];
            if (rule.get_Id() === id && rule.get_ruleName() === ruleName) {
                return i;
            }
        }

        return -1;
    },

    DataKeyAdded: function (key) {
        for (dataKeyCount = 0, dataKeysLen = this._dataKeys.length; dataKeyCount < dataKeysLen; dataKeyCount++) {
            var dataKey = this._dataKeys[dataKeyCount];
            if (key == dataKey) {
                return true;
            }
        }
        return false;
    },

    Clone: function (newInstance) {
        if (newInstance == null)
            this.ThrowBinderException('When calling the Clone function, you must pass the new instance of ClientBinder type to which the properties should be cloned.');
        newInstance.set_dataMembers(this.get_dataMembers());
        newInstance.set_clientTemplates(this.get_clientTemplates());
        newInstance.set_serviceBaseUrl(this.get_serviceBaseUrl());
        // TODO: add other properties here that should be cloned
    },

    Reset: function () {
        // clear the data key values
        this.ClearTarget();
        this._dataKeys = null;
    },

    GetItemKey: function (itemIndex) {
        if (!this._dataKeyNames) {
            return;
        }
        var key = [];
        var keyName;
        var keyValue;
        // add global keys
        if (this._globalDataKeys) {
            for (globalKeyIndex = 0, globalKeysLen = this._dataKeyNames.length; globalKeyIndex < globalKeysLen; globalKeyIndex++) {
                keyName = this._dataKeyNames[globalKeyIndex];
                keyValue = this._globalDataKeys[keyName];
                key[keyName] = keyValue;
            }
        }

        // add actual keys
        if (this._dataKeys) {
            var dataKey = this._dataKeys[itemIndex];
            // make additional checks to see if index is in range (fixes bug #65936)
            if (typeof (dataKey) == "undefined")
                return;
            for (dataKeyNamesIndex = 0, dataKeyNamesLen = this._dataKeyNames.length; dataKeyNamesIndex < dataKeyNamesLen; dataKeyNamesIndex++) {
                keyName = this._dataKeyNames[dataKeyNamesIndex];
                keyValue = dataKey[keyName];
                if (keyValue) {
                    key[keyName] = keyValue;
                }
            }
        }
        return key;
    },

    GetEmptyGuid: function () {
        return '00000000-0000-0000-0000-000000000000';
    },

    ClearUrlParams: function () {
        this._urlParams = null;
    },

    get_manager: function () {
        if (this._manager === null) {
            this._manager = new Telerik.Sitefinity.Data.ClientManager();
            this._manager.set_urlParams(this._urlParams);
            this._manager.set_culture(this._culture);
            this._manager.set_uiCulture(this._uiCulture);
            this._manager.set_fallbackMode(this._fallbackMode);
        }
        return this._manager;
    },

    DeserializeData: function (data) {
        if (data['SerializedCollection'] == null) {
            return data;
        }
        var items = Sys.Serialization.JavaScriptSerializer.deserialize(data.SerializedCollection);
        return { 'Items': items, 'TotalCount': data.TotalCount };
    },

    showLoadingPanel: function () {
        var panel = $find(this._loadingPanelID);
        if (panel != null) {
            panel._manager = this.get_manager();

            if (panel.show(this.GetTarget().id)) {
                var obj = { "Panel": panel, "ControlID": this.GetTarget().id }

                if (!Array.contains(this._loadingPanelsToHide, obj)) {
                    this._loadingPanelsToHide[this._loadingPanelsToHide.length] = obj;
                }
            }
        }
    },

    hideLoadingPanels: function () {
        for (var i = 0; i < this._loadingPanelsToHide.length; i++) {
            var panel = this._loadingPanelsToHide[i].Panel;
            var controlID = this._loadingPanelsToHide[i].ControlID;
            if (panel != null) {
                panel.hide(controlID);
                Array.remove(this._loadingPanelsToHide, this._loadingPanelsToHide[i]);
                i--;
            }
        }
    },

    // Clears all selected items; both in user interface and data.
    clearSelection: function () {

    },

    /* PROPERTIES */
    get_selectedItemsCount: function () {
        var typeName = Object.getType(this).getName();
        alert(typeName + ".get_selectedItemsCount() is not implemented");
        return null;
    },

    get_selectedItems: function () {
        var typeName = Object.getType(this).getName();
        alert(typeName + ".get_selectedItems() is not implemented");
        return null;
    },

    // Gets or sets the id of the binder's target control.
    get_targetId: function () {
        return this._targetId;
    },
    set_targetId: function (value) {
        if (this._targetId != value) {
            this._targetId = value;
            this.raisePropertyChanged('targetId');
        }
    },
    get_target: function () {
        return this._target;
    },
    set_target: function (value) {
        if (this._target != value) {
            this._target = value;
            this.raisePropertyChanged('target');
        }
    },
    // Gets or sets the base WCF service url.
    get_serviceBaseUrl: function () {
        return this._serviceBaseUrl;
    },
    set_serviceBaseUrl: function (value) {
        if (this._serviceBaseUrl != value) {
            this._serviceBaseUrl = value;
            this.raisePropertyChanged('serviceBaseUrl');
        }
    },
    // Gets or sets the array with the names of client side templates.
    get_clientTemplates: function () {
        return this._clientTemplates;
    },
    set_clientTemplates: function (value) {
        if (this._clientTemplates != value) {
            this._clientTemplates = value;
            this.raisePropertyChanged('clientTemplates');
        }
    },
    // Gets or sets the sort expression to be used while bindinig.
    get_sortExpression: function () {
        return this._sortExpression;
    },
    set_sortExpression: function (value) {
        if (this._sortExpression != value) {
            this._sortExpression = value;
            this.raisePropertyChanged('sortExpression');
        }
    },
    // Gets or sets the number of items to skip while retrieving data.
    get_skip: function () {
        return this._skip;
    },
    set_skip: function (value) {
        if (this._skip != value) {
            this._skip = value;
            this.raisePropertyChanged('skip');
        }
    },
    // Gets or sets the maximum number of items to take while retriving data.
    get_take: function () {
        return this._take;
    },
    set_take: function (value) {
        if (this._take != value) {
            this._take = value;
            this.raisePropertyChanged('take');
        }
    },
    // Gets or sets the filter expression used to filter items. Filter expression
    // uses dynamic LINQ as syntax.
    get_filterExpression: function () {
        return this._filterExpression;
    },
    set_filterExpression: function (value) {
        if (this._filterExpression != value) {
            this._filterExpression = value;
            this.raisePropertyChanged('filterExpression');
        }
    },
    // Gets or sets the array of names that define the key of the object binder is working with.
    // Supports multiple keys.
    get_dataKeyNames: function () {
        return this._dataKeyNames;
    },
    set_dataKeyNames: function (value) {
        if (this._dataKeyNames != value) {
            this._dataKeyNames = value;
            this.raisePropertyChanged('dataKeyNames');
        }
    },
    get_dataType: function () {
        return this._dataType;
    },
    set_dataType: function (value) {
        if (this._dataType != value) {
            this._dataType = value;
            this.raisePropertyChanged('dataType');
        }
    },
    get_dataKeys: function () {
        if (!this._dataKeys) {
            this._dataKeys = [];
        }
        return this._dataKeys;
    },
    set_dataKeys: function (value) {
        // TODO: check if the input is valid (has two be a jagged array, with two dimensions in the second array
        if (this._dataKeys != value) {
            this._dataKeys = value;
            this.raisePropertyChanged('dataKeys');
        }
    },
    get_globalDataKeys: function () {
        if (!this._globalDataKeys) {
            this._globalDataKeys = [];
        }
        return this._globalDataKeys;
    },
    set_globalDataKeys: function (value) {
        if (this._globalDataKeys != value) {
            this._globalDataKeys = value;
            this.raisePropertyChanged('globalDataKeys');
        }
    },
    // Gets or sets the value determining wheter data should be loaded when the object is initialized.	
    get_bindOnLoad: function () {
        return this._bindOnLoad;
    },
    set_bindOnLoad: function (value) {
        if (this._bindOnLoad != value) {
            this._bindOnLoad = value;
            this.raisePropertyChanged('bindOnLoad');
        }
    },
    get_saveInvokerId: function () {
        return this._saveInvokerId;
    },
    set_saveInvokerId: function (value) {
        if (this._saveInvokerId != value) {
            this._saveInvokerId = value;
            this.raisePropertyChanged('saveInvokerId');
        }
    },
    get_dataMembers: function () {
        return this._dataMembers;
    },
    set_dataMembers: function (value) {
        if (this._dataMembers != value) {
            this._dataMembers = value;
            this.raisePropertyChanged('dataMembers');
        }
    },
    get_provider: function () {
        return this._provider;
    },
    set_provider: function (value) {
        if (this._provider != value) {
            this._provider = value;
            this.raisePropertyChanged('provider');
        }
    },
    get_managerType: function () {
        return this._managerType;
    },
    set_managerType: function (value) {
        if (this._managerType != value) {
            this._managerType = value;
            this.raisePropertyChanged('managerType');
        }
    },
    get_allowAutomaticDelete: function () {
        return this._allowAutomaticDelete;
    },
    set_allowAutomaticDelete: function (value) {
        if (this._allowAutomaticDelete != value) {
            this._allowAutomaticDelete = value;
            this.raisePropertyChanged('allowAutomaticDelete');
        }
    },
    get_urlParams: function () {
        if (this._urlParams == null) {
            this._urlParams = new Object();
        }
        return this._urlParams;
    },
    set_urlParams: function (value) {
        if (this._urlParams != value) {
            this._urlParams = value;
            this.raisePropertyChanged('urlParams');
        }
    },
    get_unescapeHtml: function () {
        return this._unescapeHtml;
    },
    set_unescapeHtml: function (value) {
        if (this._unescapeHtml != value) {
            this._unescapeHtml = value;
            this.raisePropertyChanged('unescapeHtml');
        }
    },
    get_unescapableTags: function () {
        if (!this._unescapableTags) {
            this._unescapableTags = ["object", "script", "embed"];
        }
        return this._unescapableTags;
    },
    set_unescapableTags: function (value) {
        if (this._unescapableTags != value) {
            this._unescapableTags = value;
            this.raisePropertyChanged('unescapableTags');
        }
    },
    get_parentItemId: function () {
        return this._parentItemId;
    },
    set_parentItemId: function (value) {
        if (this._parentItemId != value) {
            this._parentItemId = value;
            this.raisePropertyChanged('parentItemId');
        }
    },
    get_hasNoData: function () {
        return this._displayedItemsCount == 0;
    },

    get_displayedItemsCount: function () {
        return this._displayedItemsCount;
    },

    get_isFiltering: function () {
        return this._isFiltering;
    },
    set_isFiltering: function (value) {
        this._isFiltering = value;
    },

    get_isBinding: function () {
        return this._isBinding;
    },
    set_isBinding: function (value) {
        this._isBinding = value;
    },

    get_isMultilingual: function () {
        return this._isMultilingual;
    },
    set_isMultilingual: function (value) {
        this._isMultilingual = value;
    },

    // Specifies the culture that will be used on the server as CurrentThread when processing the request
    set_culture: function (culture) {
        this._culture = culture;
        if (this._manager != null)
            this._manager.set_culture(this._culture);
    },
    // Gets the culture that will be used on the server when processing the request
    get_culture: function () {
        return this._culture;
    },

    // Specifies the culture that will be used on the server as UICulture when processing the request
    set_uiCulture: function (culture) {
        this._uiCulture = culture;
        if (this._manager != null)
            this._manager.set_uiCulture(this._uiCulture);
    },
    // Gets the culture that will be used on the server as UICulture when processing the request
    get_uiCulture: function () {
        return this._uiCulture;
    },

    // Sets the culture fallback mode for the requests.
    set_fallbackMode: function (fallbackMode) {
        this._fallbackMode = fallbackMode;
        if (this._manager != null)
            this._manager.set_fallbackMode(this._fallbackMode);
    },
    // Gets the culture fallback mode for the requests.
    get_fallbackMode: function () {
        return this._fallbackMode;
    },
    get_workflowOperation: function () {
        return this._workflowOperation;
    },
    set_workflowOperation: function (value) {
        this._workflowOperation = value;
    },
    get_contextBag: function () {
        return this._contextBag;
    },
    set_contextBag: function (value) {
        this._contextBag = value;
    }
};
Telerik.Sitefinity.Web.UI.ClientBinder.registerClass("Telerik.Sitefinity.Web.UI.ClientBinder", Sys.Component);

/* Client binder event args class */

Telerik.Sitefinity.Web.UI.ClientBinderEventArgs = function (target, key, dataItem) {
    // private fields
    this._target = target;
    this._key = key;
    this._cancel = false;
    this._dataItem = typeof (dataItem) != "undefined" ? dataItem : null;
    Telerik.Sitefinity.Web.UI.ClientBinderEventArgs.initializeBase(this);
};

Telerik.Sitefinity.Web.UI.ClientBinderEventArgs.prototype = {
    // set up and tear down
    initialize: function () {
        Telerik.Sitefinity.Web.UI.ClientBinderEventArgs.callBaseMethod(this, 'initialize');
    },
    dispose: function () {
        Telerik.Sitefinity.Web.UI.ClientBinderEventArgs.callBaseMethod(this, 'dispose');
    },
    // properties
    get_target: function () {
        return this._target;
    },
    get_key: function () {
        return this._key;
    },
    get_cancel: function () {
        return this._cancel;
    },
    set_cancel: function (value) {
        if (this._cancel != value) {
            this._cancel = value;
            this.raisePropertyChanged('cancel');
        }
    },
    get_dataItem: function () {
        return this._dataItem;
    }
};

Telerik.Sitefinity.Web.UI.ClientBinderEventArgs.registerClass("Telerik.Sitefinity.Web.UI.ClientBinderEventArgs", Sys.Component);

/* Client binder item event args class */
Telerik.Sitefinity.Web.UI.ItemEventArgs = function (key, dataItem, itemIndex, itemElement, propertyBag, commandName, commandArgument) {
    this._key = key;
    this._dataItem = dataItem;
    this._itemIndex = itemIndex;
    this._itemElement = itemElement;
    this._cancel = false;
    this._propertyBag = propertyBag;
    this._commandName = commandName;
    this._commandArgument = commandArgument;
    Telerik.Sitefinity.Web.UI.ItemEventArgs.initializeBase(this);
};

Telerik.Sitefinity.Web.UI.ItemEventArgs.prototype = {
    // set up and tear down
    initialize: function () {
        Telerik.Sitefinity.Web.UI.ItemEventArgs.callBaseMethod(this, 'initialize');
    },
    dispose: function () {
        Telerik.Sitefinity.Web.UI.ItemEventArgs.callBaseMethod(this, 'dispose');
    },
    get_key: function () {
        return this._key;
    },
    get_dataItem: function () {
        return this._dataItem;
    },
    get_itemIndex: function () {
        return this._itemIndex;
    },
    get_itemElement: function () {
        return this._itemElement;
    },
    get_cancel: function () {
        return this._cancel;
    },
    set_cancel: function (value) {
        if (this._cancel != value) {
            this._cancel = value;
            this.raisePropertyChanged('cancel');
        }
    },
    get_propertyBag: function () {
        return this._propertyBag;
    },
    set_propertyBag: function (value) {
        if (this._propertyBag != value) {
            this._propertyBag = value;
            this.raisePropertyChanged('propertyBag');
        }
    },
    get_commandName: function () {
        return this._commandName;
    },
    set_commandName: function (value) {
        if (this._commandName != value) {
            this._commandName = value;
            this.raisePropertyChanged('commandName');
        }
    },
    get_commandArgument: function () {
        return this._commandArgument;
    },
    set_commandArgument: function (value) {
        if (this._commandArgument != value) {
            this._commandArgument = value;
            this.raisePropertyChanged('commandArgument');
        }
    },
    GetUniqueId: function (id) {
        return id + this._itemIndex;
    },
    FindControl: function (id) {
        var searchPattern = '#' + this.GetUniqueId(id);
        var control;
        $(this._itemElement).find(searchPattern).each(function (i, element) {
                                                          control = element;
                                                      });
        return control;
    },
    AppendData: function (propertyBag) {
        this._propertyBag = this._propertyBag.concat(propertyBag);
    }
};

Telerik.Sitefinity.Web.UI.ItemEventArgs.registerClass('Telerik.Sitefinity.Web.UI.ItemEventArgs', Sys.Component);


/* Client binder item event args class */
Telerik.Sitefinity.Web.UI.ErrorEventArgs = function (error) {
    if (error != null && error.hasOwnProperty("Detail")) {
        this._error = error.Detail;
    }
    else {
        this._error = error;
    }
    Telerik.Sitefinity.Web.UI.ErrorEventArgs.initializeBase(this);
};
Telerik.Sitefinity.Web.UI.ErrorEventArgs.prototype = {
    // set up and tear down
    initialize: function () {
        Telerik.Sitefinity.Web.UI.ErrorEventArgs.callBaseMethod(this, 'initialize');
    },
    dispose: function () {
        Telerik.Sitefinity.Web.UI.ErrorEventArgs.callBaseMethod(this, 'dispose');
    },
    get_error: function () {
        return this._error;
    }
};
Telerik.Sitefinity.Web.UI.ErrorEventArgs.registerClass('Telerik.Sitefinity.Web.UI.ErrorEventArgs', Sys.Component);

Telerik.Sitefinity._ClientBindingManager = function Telerik$Sitefinity$_ClientBindingManager() {
    this._clientBinders = [];
    this._clientBindersLoaded = false;
};
function Telerik$Sitefinity$_ClientBindingManager$dispose() {
    this._clientBinders = null;
}
function Telerik$Sitefinity$_ClientBindingManager$get_incompleteBindingsCount() {
    if (!this._clientBindersLoaded)
        this._loadClientBinders();
    return this._getIncompleteBindingsCount();
}
function Telerik$Sitefinity$_ClientBindingManager$_loadClientBinders() {
    if (!Sys.Application._loaded)
        throw "The check for bindings was invoked before the page was loaded.";

    var components = Sys.Application.getComponents();
    for (var i = 0, len = components.length; i < len; i++) {
        var component = components[i];
        if (component && component.get_isBinding && Telerik.Sitefinity.Web.UI.ClientBinder.isInstanceOfType(component)) {
            this._clientBinders.push(component);
        }
    }
    this._clientBindersLoaded = true;
}

function Telerik$Sitefinity$_ClientBindingManager$_getIncompleteBindingsCount() {
    var incompleteBindingsCount = 0;
    for (var i = 0, len = this._clientBinders.length; i < len; i++) {
        binder = this._clientBinders[i];
        if (binder && binder.get_isBinding()) {
            incompleteBindingsCount++;
        }
    }
    return incompleteBindingsCount;
}

Telerik.Sitefinity._ClientBindingManager.prototype = {
    dispose: Telerik$Sitefinity$_ClientBindingManager$dispose,
    get_incompleteBindingsCount: Telerik$Sitefinity$_ClientBindingManager$get_incompleteBindingsCount,
    _loadClientBinders: Telerik$Sitefinity$_ClientBindingManager$_loadClientBinders,
    _getIncompleteBindingsCount: Telerik$Sitefinity$_ClientBindingManager$_getIncompleteBindingsCount
};

Telerik.Sitefinity._ClientBindingManager.registerClass('Telerik.Sitefinity._ClientBindingManager', null, Sys.IDisposable);

if (typeof (Telerik.Sitefinity.ClientBindingManager) === "undefined") {
    Telerik.Sitefinity.ClientBindingManager = new Telerik.Sitefinity._ClientBindingManager();
}

/* Validation rule class */
Telerik.Sitefinity.Web.UI.ValidationRule = function (id, ruleName, message, ruleArgument) {
    this._id = id;
    this._ruleName = ruleName;
    this._message = message;
    this._ruleArgument = ruleArgument;
    Telerik.Sitefinity.Web.UI.ValidationRule.initializeBase(this);
};

Telerik.Sitefinity.Web.UI.ValidationRule.prototype = {
    initialize: function () {
        Telerik.Sitefinity.Web.UI.ValidationRule.callBaseMethod(this, 'initialize');
    },
    dispose: function () {
        Telerik.Sitefinity.Web.UI.ValidationRule.callBaseMethod(this, 'dispose');
    },
    get_Id: function () {
        return this._id;
    },
    get_ruleName: function () {
        return this._ruleName;
    },
    get_message: function () {
        return this._message;
    },
    get_ruleArgument: function () {
        return this._ruleArgument;
    },
    GetUniqueId: function (itemIndex) {
        return this._id + itemIndex;
    }
};

Telerik.Sitefinity.Web.UI.ValidationRule.registerClass('Telerik.Sitefinity.Web.UI.ValidationRule', Sys.Component);

/* js extensions */
if (!Array.prototype.indexOf) {
    Array.prototype.indexOf = function (elt /*, from*/) {
        var len = this.length;

        var from = Number(arguments[1]) || 0;
        from = (from < 0) ? Math.ceil(from) : Math.floor(from);
        if (from < 0)
            from += len;

        for (; from < len; from++) {
            if (from in this &&
                        this[from] === elt)
                return from;
        }
        return -1;
    };
}
