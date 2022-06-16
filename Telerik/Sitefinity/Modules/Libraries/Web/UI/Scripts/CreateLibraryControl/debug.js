/// <reference name="MicrosoftAjax.js"/>
/// <reference name="Telerik.Sitefinity.Resources.Scripts.jquery-1.6.2-vsdoc.js" assembly="Telerik.Sitefinity.Resources"/>
Type.registerNamespace("Telerik.Sitefinity.Modules.Libraries.Web.UI.Designers");

Telerik.Sitefinity.Modules.Libraries.Web.UI.Designers.CreateLibraryControl = function (element) {
	this._cancelButton = null;
	this._createButton = null;
	this._createLibraryTxt = null;

	this._createLibraryWebServiceUrl = null;
	this._blankLibraryDataItem = null;
	this._libraryType = null;
	this._provider = null;
	this._albumNameExpression = null;

	this._noParent = null;
	this._selectParent = null;
	this._parentLibrarySelector = null;

	this._radioButtonsClickDelegate = null;
	this._radWindow = null;

	Telerik.Sitefinity.Modules.Libraries.Web.UI.Designers.CreateLibraryControl.initializeBase(this, [element]);
};
Telerik.Sitefinity.Modules.Libraries.Web.UI.Designers.CreateLibraryControl.prototype =
{
    initialize: function () {
        Telerik.Sitefinity.Modules.Libraries.Web.UI.Designers.CreateLibraryControl.callBaseMethod(this, "initialize");

        if (this._blankLibraryDataItem)
            this._blankLibraryDataItem = this._deserializeBlankDataItem(this._blankLibraryDataItem);

        this.attachEventHandlers();                
    },

    createLibrary: function (name) {
        this._createAlbumSimple(name);
    },

    dispose: function () {
        this.detachEventHandlers();
    },

    /* ---=== event handling ===---*/
    attachEventHandlers: function () {
        this.cancelClickDelegate = Function.createDelegate(this, this.cancelClickHandler);
        $addHandler(this._cancelButton, "click", this.cancelClickDelegate);

        this.createClickDelegate = Function.createDelegate(this, this.createClickHandler);
        $addHandler(this._createButton, "click", this.createClickDelegate);

        this._radioButtonsClickDelegate = Function.createDelegate(this, this.radioButtonsClick);
        $addHandler(this.get_noParent(), "click", this._radioButtonsClickDelegate);
        $addHandler(this.get_selectParent(), "click", this._radioButtonsClickDelegate);
    },
    detachEventHandlers: function () {
        $removeHandler(this._cancelButton, "click", this.cancelClickDelegate);
        delete this.cancelClickDelegate;

        $removeHandler(this._createButton, "click", this.createClickDelegate);
        delete this.createClickDelegate;

        if (!this._radioButtonsClickDelegate) {
            if (this.get_noParent()) {
                $removeHandler(this.get_noParent(), "click", this._radioButtonsClickDelegate);
            }

            if (this.get_selectParent()) {
                $removeHandler(this.get_selectParent(), "click", this._radioButtonsClickDelegate);
            }

            delete this._radioButtonsClickDelegate;
        }
    },

    rebind: function(providerName)
    {
        this.get_parentLibrarySelector().set_providerName(providerName);
        this.get_parentLibrarySelector().dataBind();
    },

    createClickHandler: function () {
        if (!this.validate()) {
            return;
        }

        var eventArgs = this._raiseEvent("commandExecuting", this._createEventArgs("createLibrary", this.get_libraryName()));

        if (eventArgs.get_cancel())
            return;

        this.createLibrary(this.get_libraryName());
    },

    validate: function () {
        return this.get_createLibraryTxt().validate();
    },


    cancelClickHandler: function () {
        this._handleCommandExecution("cancel");
        this.set_libraryName("");
    },

    radioButtonsClick: function () {
        jQuery(this.get_parentLibrarySelector().get_element()).toggle(this.get_selectParent().checked);
        if ((this.get_selectParent()).checked) {
            this.get_parentLibrarySelector().set_providerName(this.get_provider());
            this.get_parentLibrarySelector().dataBind();
        }
        if (this.get_radWindow()) {
            this.get_radWindow().AjaxDialog.resizeToContent();
        }
    },

    reset: function()
    {
        this.get_noParent().checked = true;
        this.get_noParent().click();        
    },

    /* ---=== properties ===--- */
    get_element: function () { return this._element; },
    set_element: function (value) { this._element = value; },
    get_createLibraryTxt: function () { return this._createLibraryTxt; },
    set_createLibraryTxt: function (value) { this._createLibraryTxt = value; },
    get_cancelButton: function () { return this._cancelButton; },
    set_cancelButton: function (value) { this._cancelButton = value; },
    get_createButton: function () { return this._createButton; },
    set_createButton: function (value) { this._createButton = value; },
    get_libraryName: function () { return this.get_createLibraryTxt().get_value(); },
    set_libraryName: function () { this.get_createLibraryTxt().set_value(); },

    get_noParent: function () {
        return this._noParent;
    },
    set_noParent: function (value) {
        this._noParent = value;
    },

    get_selectParent: function () {
        return this._selectParent;
    },
    set_selectParent: function (value) {
        this._selectParent = value;
    },

    get_parentLibrarySelector: function () {
        return this._parentLibrarySelector;
    },
    set_parentLibrarySelector: function (value) {
        this._parentLibrarySelector = value;
    },

    /* ---=== this event handlers ===--- */
    add_commandExecuted: function (handler) { this.get_events().addHandler("commandExecuted", handler); },
    remove_commandExecuted: function (handler) { this.get_events().removeHandler("commandExecuted", handler); },
    add_commandExecuting: function (handler) { this.get_events().addHandler("commandExecuting", handler); },
    remove_commandExecuting: function (handler) { this.get_events().removeHandler("commandExecuting", handler); },

    /* ---=== private members ===--- */
    _raiseEvent: function (eventName, eventArgs) {
        var handler = this.get_events().getHandler(eventName);
        if (handler) handler(this, eventArgs);
        return eventArgs;
    },
    _createEventArgs: function (commandName, args) {
        return new Sys.CommandEventArgs(commandName, args, this);
    },
    _handleCommandExecution: function (commandName, commandArgs, executeCustomCode) {
        var eventArgs = this._raiseEvent("commandExecuting", this._createEventArgs(commandName, commandArgs));

        if (eventArgs.get_cancel())
            return;

        if (typeof (executeCustomCode) === "function")
            executeCustomCode();

        this._raiseEvent("commandExecuted", this._createEventArgs(commandName, commandArgs));
    },

    _createAlbumSimple: function (albumName) {
        var dataObject = this._clone(this._blankLibraryDataItem);

        dataObject.Name = albumName;
        dataObject.Title = { PersistedValue: albumName };
        var urlName = albumName.replace(new XRegExp(this._albumNameExpression, "g"), "-");
        dataObject.UrlName = { PersistedValue: urlName };

        // ensure that you have last modified set
        var currentDate = new Date();
        if (dataObject['LastModified'] == null)
            dataObject['LastModified'] = currentDate;
        if (dataObject['DateCreated'] == null)
            dataObject['DateCreated'] = currentDate;
        if (dataObject['PublicationDate'] == null)
            dataObject['PublicationDate'] = currentDate;
        if (dataObject['ParentId'] == null) {
            if ((this.get_selectParent()).checked && this.get_parentLibrarySelector().get_selectedItem())
                dataObject['ParentId'] = this.get_parentLibrarySelector().get_selectedItem().Id;
        }

        var itemContext = {
            'Item': dataObject,
            'ItemType': this._libraryType
        };

        var clientManager = new Telerik.Sitefinity.Data.ClientManager();
        var serviceUrl = this._createLibraryWebServiceUrl;
        var urlParams = [];
        urlParams['provider'] = this._provider;        
        var key = clientManager.GetEmptyGuid();
        clientManager.InvokePut(serviceUrl, urlParams, [key], itemContext, this._createAlbumSuccess, this._createAlbumFailure, this);
    },

    _createAlbumSuccess: function (target, data, webRequest) {
        target._raiseEvent("commandExecuted", target._createEventArgs("createLibrary", data.Item));
        target.set_libraryName("");
    },

    _createAlbumFailure: function (result) {
        alert(result.Detail);
    },

    _deserializeBlankDataItem: function (blankDataItem) {
        blankDataItem = Sys.Serialization.JavaScriptSerializer.deserialize(blankDataItem);
        if (blankDataItem.hasOwnProperty('Id')) {
            delete blankDataItem.Id;
        }
        return blankDataItem;
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

    get_provider: function () {
        return this._provider;
    },
    set_provider: function (value) {
        this._provider = value;
    },

    get_radWindow: function () {
        if (!this._radWindow) {
            if (typeof window.radWindow !== "undefined") {
                this._radWindow = window.radWindow;
            }
            else if (window.frameElement != null && typeof window.frameElement.radWindow !== "undefined") {
                this._radWindow = window.frameElement.radWindow;
            }
        }
        return this._radWindow;
    }
};

Telerik.Sitefinity.Modules.Libraries.Web.UI.Designers.CreateLibraryControl.registerClass("Telerik.Sitefinity.Modules.Libraries.Web.UI.Designers.CreateLibraryControl", Sys.UI.Control);