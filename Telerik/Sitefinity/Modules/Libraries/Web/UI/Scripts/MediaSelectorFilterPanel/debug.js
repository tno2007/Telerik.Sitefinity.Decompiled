Type.registerNamespace("Telerik.Sitefinity.Modules.Libraries.Web.UI.Designers");

Telerik.Sitefinity.Modules.Libraries.Web.UI.Designers.MediaSelectorFilterPanel = function (element) {
    Telerik.Sitefinity.Modules.Libraries.Web.UI.Designers.MediaSelectorFilterPanel.initializeBase(this, [element]);

    this._parentType = null;
    this._contentType = null;
    this._provider;
    this._loadDelegate = null;
    this._bindOnLoad = null;
    this._librarySelector = null;
    this._commandBar = null;
    this._commandBarCommandDelegate = null;
    this._selectionChangedDelegate = null;
    this._showLibFilterWrp = null;
    this._itemsCount = null;
}

Telerik.Sitefinity.Modules.Libraries.Web.UI.Designers.MediaSelectorFilterPanel.prototype = {
    initialize: function () {
        Telerik.Sitefinity.Modules.Libraries.Web.UI.Designers.MediaSelectorFilterPanel.callBaseMethod(this, "initialize");
   
        this._loadDelegate = Function.createDelegate(this, this._load);
        Sys.Application.add_load(this._loadDelegate);

        if (this._librarySelector) {
            this._selectionChangedDelegate = Function.createDelegate(this, this._selectionChanged);
            this.get_librarySelector().add_selectionChanged(this._selectionChangedDelegate);
        }

        if (this._commandBar) {
            this._commandBarCommandDelegate = Function.createDelegate(this, this._commandBarCommandHandler);
            this.get_commandBar().add_command(this._commandBarCommandDelegate);
        }

        if (!this._showLibFilterWrp) {
            $('.sfPercentageColsDim').width(410);
        }
    },

    dispose: function () {
        Telerik.Sitefinity.Modules.Libraries.Web.UI.Designers.MediaSelectorFilterPanel.callBaseMethod(this, "dispose");
      
        if (this._loadDelegate) {
            Sys.Application.remove_load(this._loadDelegate);
            delete this._loadDelegate;
        }

        if (this._selectionChangedDelegate) {
            if (this.get_librarySelector()) {
                this.get_librarySelector().remove_selectionChanged(this._selectionChangedDelegate);
            }
            delete this._selectionChangedDelegate;
        }
        if (this._commandBarCommandDelegate) {
            if (this.get_commandBar()) {
                this.get_commandBar().remove_command(this._commandBarCommandDelegate);
            }
            delete this._commandBarCommandDelegate;
        }
    },

    rebind: function () {
        this._initializeLists();
    },

    toggleUploadOption: function (display) {
        var uploadButton = jQuery($get(this._commandBar.findItemByCommandName("upload").ButtonClientId));
        uploadButton.toggle(display);
    },

    // ----------------------------------------------- Public functions -----------------------------------------------

    /* -------------------- events -------------------- */

    add_onFilterSelectedCommand: function (delegate) {
        this.get_events().addHandler('onFilterSelected', delegate);
    },
    remove_onFilterSelectedCommand: function (delegate) {
        this.get_events().removeHandler('onFilterSelected', delegate);
    },

    /* -------------------- event handlers -------------------- */

    _onFilterSelectedHandler: function (sender, args) {
        var h = this.get_events().getHandler('onFilterSelected');
        if (h) h(sender, args);
    },

    _load: function () {
        if (this._bindOnLoad) {
            this._initializeLists();
        }
    },

    _selectionChanged: function (sender, args) {
        jQuery($get(this.get_commandBar().get_id())).children().removeClass("sfSelected");
        this._onFilterSelectedHandler(sender, args);
    },

    _commandBarCommandHandler: function (sender, args) {

        this.get_librarySelector().clearSelection();
        jQuery($get(sender.get_id())).children().removeClass("sfSelected");
        jQuery($get(sender.findItemByCommandName(args.get_commandName()).ButtonClientId)).addClass("sfSelected");
 
        this._onFilterSelectedHandler(sender, args);
    },

    /* -------------------- private methods -------------------- */

    _initializeLists: function () {
        if (this._librarySelector) {
            this.get_librarySelector().get_treeBinder().set_enableInitialExpanding(false);
            var urlParams = this.get_librarySelector().get_treeBinder().get_urlParams();
            if (urlParams['take'] === undefined || urlParams['take'] === null) {
                urlParams['take'] = this.get_itemsCount()
                this.get_librarySelector().get_treeBinder().set_urlParams(urlParams);
            }
          
            this.get_librarySelector().set_provider(this._provider);
            this.get_librarySelector().dataBind();
        }
    },

    // ------------------------------------------------- Properties ----------------------------------------------------    

    get_parentType: function () {
        return this._parentType;
    },
    set_parentType: function (value) {
        this._parentType = value;
    },

    get_contentType: function () {
        return this._contentType;
    },
    set_contentType: function (value) {
        this._contentType = value;
    },

    get_itemsCount: function () {
        return this._itemsCount;
    },
    set_itemsCount: function (value) {
        this._itemsCount = value;
    },

    get_bindOnLoad: function () {
        return this._bindOnLoad;
    },
    set_bindOnLoad: function (value) {
        this._bindOnLoad = value;
    },

    get_librarySelector: function () {
        return this._librarySelector;
    },
    set_librarySelector: function (value) {
        this._librarySelector = value;
    },

    get_commandBar: function () {
        return this._commandBar;
    },
    set_commandBar: function (value) {
        this._commandBar = value;
    }
}

Telerik.Sitefinity.Modules.Libraries.Web.UI.Designers.MediaSelectorFilterPanel.registerClass("Telerik.Sitefinity.Modules.Libraries.Web.UI.Designers.MediaSelectorFilterPanel", Sys.UI.Control);
