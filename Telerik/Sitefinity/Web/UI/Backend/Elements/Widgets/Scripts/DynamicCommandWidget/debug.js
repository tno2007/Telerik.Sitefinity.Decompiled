//Type._registerScript("ICommandWidget.js", ["ICommandWidget.js"]);
//Type._registerScript("IWidget.js", ["IWidget.js"]);
Type.registerNamespace("Telerik.Sitefinity.Web.UI.Backend.Elements.Widgets");

Telerik.Sitefinity.Web.UI.Backend.Elements.Widgets.DynamicCommandWidget = function (element) {
    Telerik.Sitefinity.Web.UI.Backend.Elements.Widgets.DynamicCommandWidget.initializeBase(this, [element]);
    this._element = element;
    // ------------------ IWidget members -----------------------
    this._name = null;
    this._cssClass = null;
    this._isSeparator = null;
    this._wrapperTagId = null;
    this._wrapperTagName = null;
    this._hidden = false;
    this._isBound = false;
    // ----------------------- ICommandWidget members --------------------
    this._commandName = null;
    this._commandArgument = null;
    // ----------------------- Other members --------------------------------
    this._headerTextControl = null;
    this._binder = null;
    this._binderId = null;
    this._urlParameters = null;
    this._selectedItemId = null;
    this._bindTo = null;
    this._dropDownId = null;
    // the values of the items which should fire custom commands
    this._customCommandItemValues = [];
    this._customCommandItemTexts = [];
    this._moreLinkId = null;
    this._lessLinkId = null;
    this._pageSize = null;
    this._currentNumberOfPages = 1;
    this._selectedItemCssClass = null;
    this._totalItemCount = 0;
    this._totalNumberOfPages = 1;
    this._isFilterCommand = false;
}

Telerik.Sitefinity.Web.UI.Backend.Elements.Widgets.DynamicCommandWidget.prototype = {

    initialize: function () {
        Telerik.Sitefinity.Web.UI.Backend.Elements.Widgets.DynamicCommandWidget.callBaseMethod(this, 'initialize');

        this._handlePageLoadDelegate = Function.createDelegate(this, this._loadHandler);
        this._handleDropDownChangeDelegate = Function.createDelegate(this, this._dropDownChange);
        this._handleMoreLinkClickDelegate = Function.createDelegate(this, this._moreLinkClicked);
        this._handleLessLinkClickDelegate = Function.createDelegate(this, this._lessLinkClicked);
        this._handleBinderCommandDelegate = Function.createDelegate(this, this._binderCommandHandler);
        this._handleBinderItemDataBoundDelegate = Function.createDelegate(this, this._binderItemDataBound);
        this._errorHandlerDelegate = Function.createDelegate(this, this._errorHandler);
        this._handleBinderDataBoundDelegate = Function.createDelegate(this, this._binderDataBound);

        Sys.Application.add_load(this._handlePageLoadDelegate);
        switch (this._bindTo) {
            case 0:
                // combobox
                var select = $get(this._dropDownId);
                $addHandler(select, 'change', this._handleDropDownChangeDelegate);
                break;
            case 1:
            case 2:
                // client
                // hierarchical data
                var moreLink = $get(this._moreLinkId);
                if (moreLink != null) {
                $addHandler(moreLink, 'click', this._handleMoreLinkClickDelegate);
                }
                var lessLink = $get(this._lessLinkId);
                if (lessLink != null) {
                $addHandler(lessLink, 'click', this._handleLessLinkClickDelegate);
                }
                break;
            default:
                break;
        }
    },

    dispose: function () {
        Telerik.Sitefinity.Web.UI.Backend.Elements.Widgets.DynamicCommandWidget.callBaseMethod(this, 'dispose');
        this._handlePageLoadDelegate = null;
        this._handleDropDownChangeDelegate = null;
        this._handleMoreLinkClickDelegate = null;
        this._handleLessLinkClickDelegate = null;
        this._handleBinderCommandDelegate = null;
        this._handleBinderItemDataBoundDelegate = null;
        this._errorHandlerDelegate = null;
        this._handleBinderDataBoundDelegate = null;
    },

    /* ------------------------------------ Private functions --------------------------------- */

    _loadHandler: function () {
        var binder = this.getBinder();

        switch (this._bindTo) {
            case 0:
                // combobox
                break;
            case 1:
                // client
                binder.add_onItemCommand(this._handleBinderCommandDelegate);
                binder.add_onItemDataBound(this._handleBinderItemDataBoundDelegate);
                binder.add_onDataBound(this._handleBinderDataBoundDelegate);
                break;
            case 2:
                // hierarchical
                binder.add_onItemCommand(Function.createDelegate(this, this._binderCommandHandler));
                binder.add_onDataBound(this._handleBinderDataBoundDelegate);
                binder.add_onItemDataBound(this._handleBinderItemDataBoundDelegate);
                binder.add_onError(this._errorHandlerDelegate);
                //binder.set_orginalServiceBaseUrl(String.format(binder.get_orginalServiceBaseUrl(), this._taxonomyId));
                break;
        }
    },

    _moreLinkClicked: function (sender, args) {
        this._currentNumberOfPages++;
        this._updateItemsToTake();
    },

    _lessLinkClicked: function (sender, args) {
        this._currentNumberOfPages--;
        this._updateItemsToTake();
    },

    _updateItemsToTake: function (sender, args) {
        this.dataBind();
        this.preparePager();
        
    },

    preparePager: function () {
        var lessLink = $("#" + this._lessLinkId);
        var moreLink = $("#" + this._moreLinkId);

        var lessLinkExists = lessLink.length > 0;
        if (lessLinkExists) {
        if (this._currentNumberOfPages == 1)
            lessLink.hide();
        else
            lessLink.show();
        }

        var moreLinkExists = moreLink.length > 0;
        if (moreLinkExists) {
            if (this._currentNumberOfPages == this._totalNumberOfPages) {
            moreLink.hide();
        } else {
            moreLink.show();
        }
        }
    },

    _dropDownChange: function (sender) {
        var selectedValue = $(sender.target + "option:selected").attr("value");
        if ($.inArray(selectedValue, this._customCommandItemValues) >= 0) {
            commandName = 'dynamicCustomCommand';
        }
        else {
            commandName = _commandName;
        }
        var commandEventArgs = new Telerik.Sitefinity.UI.CommandEventArgs(commandName, selectedValue);
        var h = this.get_events().getHandler('command');
        if (h) h(this, commandEventArgs);
    },

    _binderDataBound: function (sender, args) {
        var moreLink = $("#" + this._moreLinkId);
        this._totalItemCount = args.get_dataItem().TotalCount;
        this._totalNumberOfPages = Math.ceil(this._totalItemCount / this._pageSize);

        var moreLinkExists = moreLink.length > 0;
        if (this._totalItemCount <= this.get_pageSize() && moreLinkExists) {
            moreLink.hide();
        }
        if (this._totalItemCount == 0) {
            jQuery(this._headerTextControl).hide()
        }
    },

    _binderCommandHandler: function (sender, args) {
        var commandEventArgs = new Telerik.Sitefinity.UI.CommandEventArgs(this._commandName, args);
        commandEventArgs.isFilterCommand = this.get_isFilterCommand();
        this.set_selectedItemId(args.get_dataItem().Id);
        var h = this.get_events().getHandler('command');
        if (h) h(this, commandEventArgs);
    },

    _binderItemDataBound: function (sender, args) {
        var dataItem = args.get_dataItem();
        if (dataItem.Id == this._selectedItemId) {
            $(args.get_itemElement()).find("a").addClass("sfSel");
        }
        var commandEventArgs = new Telerik.Sitefinity.UI.CommandEventArgs('dynamicWidgetItemDataBound', args);
        var handler = this.get_events().getHandler('command');
        if (handler) handler(this, commandEventArgs);
    },

    _errorHandler: function (message) {
        var msg = message ? message : "";
        msg = msg.Detail ? msg.Detail : msg;
        this.getBinder()._showHideLoadingImage(false);
    },

    // rebinds the dynamic command widget
    dataBind: function () {
        if (this._hidden)
            return;
        if (this._pageSize == 0) {
            this.get_urlParameters()["take"] = this._pageSize;
            this.preparePager();

        }
        else {
            this.get_urlParameters()["take"] = this._currentNumberOfPages * this.get_pageSize();
        }
        this.getBinder().set_urlParams(this.get_urlParameters());
        if (this._bindTo === 1 || this._bindTo === 2) {
            var binder = this.getBinder();
            var context = this.get_selectedItemId();
            if (context) {
                binder.DataBind(null, context);
            }
            else {
                binder.DataBind();
            }
        }
        this._isBound = true;
    },

    chnageVisibility: function (visible) {
        this._hidden = !visible;
        if (visible) {
            if (!this._isBound) {
                this.dataBind();
            }
        }
        else {
            this.set_selectedItemId(null);

            this._isBound = false;
        }
    },

    /* ------------------------------------ Public Methods ----------------------------------- */

    add_command: function (handler) {
        this.get_events().addHandler('command', handler);
    },

    remove_command: function (handler) {
        this.get_events().removeHandler('command', handler);
    },

    getBinder: function () {
        if (this._binder == null) {
            this._binder = $find(this._binderId);
        }
        return this._binder;
    },

    /* ------------------------------------- Properties --------------------------------------- */

    get_name: function () {
        return this._name;
    },

    set_name: function (value) {
        if (this._name != value) {
            this._name = value;
            this.raisePropertyChanged('name');
        }
    },

    get_cssClass: function () {
        return this._cssClass;
    },

    set_cssClass: function (value) {
        if (this._cssClass != value) {
            this._cssClass = value;
            this.raisePropertyChanged('cssClass');
        }
    },

    get_isSeparator: function () {
        return this._isSeparator;
    },

    set_isSeparator: function (value) {
        if (this._isSeparator != value) {
            this._isSeparator = value;
            this.raisePropertyChanged('isSeparator');
        }
    },

    get_hidden: function () {
        return this._hidden;
    },

    set_hidden: function (value) {
        if (this._hidden != value) {
            this._hidden = value;
        }
    },

    get_wrapperTagId: function () {
        return this._wrapperTagId;
    },

    set_wrapperTagId: function (value) {
        if (this._wrapperTagId != value) {
            this._wrapperTagId = value;
            this.raisePropertyChanged('wrapperTagId');
        }
    },

    get_wrapperTagName: function () {
        return this._wrapperTagName;
    },

    set_wrapperTagName: function (value) {
        if (this._wrapperTagName != value) {
            this._wrapperTagName = value;
            this.raisePropertyChanged('wrapperTagName');
        }
    },

    get_urlParameters: function () {
        return this._urlParameters;
    },

    set_urlParameters: function (value) {
        if (this._urlParameters != value) {
            this._urlParameters = value;
            this.raisePropertyChanged('urlParameters');
        }
    },

    get_selectedItemId: function () {
        return this._selectedItemId;
    },

    set_selectedItemId: function (value) {
        if (this._selectedItemId != value) {
            this._selectedItemId = value;
            this.raisePropertyChanged('selectedItemId');
        }
    },

    get_pageSize: function () {
        return this._pageSize;
    },

    set_pageSize: function (value) {
        if (this._pageSize != value) {
            this._pageSize = value;
            this.raisePropertyChanged('pageSize');
        }
    },

    get_commandName: function () {
        return this._commandName;
    },

    get_element: function () {
        return this._element;
    },

    get_headerTextControl: function () {
        return this._headerTextControl;
    },
    set_headerTextControl: function (value) {
        this._headerTextControl = value;
    },

    get_customCommandItemValues: function () {
        return this._customCommandItemValues;
    },
    set_customCommandItemValues: function (value) {
        this._customCommandItemValues = value;
    },

    get_customCommandItemTexts: function () {
        return this._customCommandItemTexts;
    },
    set_customCommandItemTexts: function (value) {
        this._customCommandItemTexts = value;
    },
    get_isFilterCommand: function () {
        return this._isFilterCommand;
    },
    set_isFilterCommand: function (val) {
        if (val != this._isFilterCommand) {
            this._isFilterCommand = val;
            this.raisePropertyChanged("isFilterCommand");
        }
    }
}

Telerik.Sitefinity.Web.UI.Backend.Elements.Widgets.DynamicCommandWidget.registerClass("Telerik.Sitefinity.Web.UI.Backend.Elements.Widgets.DynamicCommandWidget", Sys.UI.Control, Telerik.Sitefinity.UI.ICommandWidget, Telerik.Sitefinity.UI.IWidget);