Type.registerNamespace("Telerik.Sitefinity.Folders.Web.UI");

Telerik.Sitefinity.Folders.Web.UI.MediaContentBreadcrumb = function (element) {
    Telerik.Sitefinity.Folders.Web.UI.MediaContentBreadcrumb.initializeBase(this, [element]);

    this._element = element;
    this._excludeNeighbours = null;
    this._baseServiceUrl = null;
    this._methodServiceUrl = null;

    this._itemId = null;
    this._providerName = null;
    this._rootFolderName = null;

    this._wrapper = null;
    this._clientManager = null;

    this._itemClickDelegate = null;
    this._rootItemClickDeledate = null;

    this._folderData = null;

    this._rootFolderTag = null;    
}

Telerik.Sitefinity.Folders.Web.UI.MediaContentBreadcrumb.prototype = {

    initialize: function () {
        Telerik.Sitefinity.Folders.Web.UI.MediaContentBreadcrumb.callBaseMethod(this, "initialize");

        this._itemClickDelegate = Function.createDelegate(this, this._itemClickHandler);
        this._rootItemClickDeledate = Function.createDelegate(this, this._rootItemClickHandler);

    },
    dispose: function () {

        if (this._itemClickDelegate) {
            var folderData = this.get_folderData();
            if (folderData) {
                for (var i = 0; i < folderData.length; i++) {
                    var currentDataItemId = folderData[i].Id;
                    var folderLink = $get(currentDataItemId);
                    $removeHandler(folderLink, "click", this._itemClickDelegate);
                }
            }
            delete this._itemClickDelegate;
        }

        if (this._rootItemClickDeledate) {
            if (this._rootFolderTag) {
                $removeHandler(this._rootFolderTag, "click", this._rootItemClickDeledate);
            }
            delete this._rootItemClickDeledate;
        }

        Telerik.Sitefinity.Folders.Web.UI.MediaContentBreadcrumb.callBaseMethod(this, "dispose");

    },

    // ---------------------------------------------------- Events ---------------------------------------------------------

    add_onItemSelectCommand: function (delegate) {
        this.get_events().addHandler('onItemSelectCommand', delegate);
    },

    remove_onItemSelectCommand: function (delegate) {
        this.get_events().removeHandler('onItemSelectCommand', delegate);
    },

    add_onRootItemSelectCommand: function (delegate) {
        this.get_events().addHandler('onRootItemSelectCommand', delegate);
    },

    remove_onRootItemSelectCommand: function (delegate) {
        this.get_events().removeHandler('onRootItemSelectCommand', delegate);
    },

    // ------------------------------------------------- Public Methods ----------------------------------------------------

    dataBind: function (id) {
        if (id == null) {
            jQuery(this.get_element()).hide();
        }
        else {
            jQuery(this.get_element()).show();
            this._dataBindInternal(id);
        }
    },

    // ------------------------------------------------- Event Handlers -----------------------------------------------------

    _itemClickHandler: function (sender, args) {
        var itemId = sender.target.getAttribute("id");
        var dataItem = this._findDataItem(itemId, this.get_folderData());
        if (dataItem) {
            this._raiseOnItemSelectCommand(dataItem);
        }
    },

    _rootItemClickHandler: function (sender, args) {
        this._raiseOnRootItemSelectCommand(null);
    },

    // ------------------------------------------------- Private Methods ----------------------------------------------------    
    _raiseOnItemSelectCommand: function (dataItem) {
        var itemEventArgs = dataItem;
        var h = this.get_events().getHandler('onItemSelectCommand');
        if (h) h(this, itemEventArgs);
    },

    _raiseOnRootItemSelectCommand: function (dataItem) {
        var itemEventArgs = dataItem;
        var h = this.get_events().getHandler('onRootItemSelectCommand');
        if (h) h(this, itemEventArgs);
    },

    _dataBindInternal: function (id) {
        var urlParams = {
            provider: this.get_providerName(),
            excludeNeighbours: this.get_excludeNeighbours()
        };
        var manager = this.get_clientManager();
        var serviceUrl = this._baseServiceUrl + this._methodServiceUrl;
       
        if (id == null || typeof id === 'undefined') {
            id = this.get_itemId();
        }

        manager.InvokeGet(serviceUrl, urlParams, [id], this._dataBindSuccess, this._dataBindFailure, this);
    },

    _dataBindSuccess: function (sender, result) {
        var folders = result.Items;
        sender._buildBreadcrumb(sender, folders);
    },

    _dataBindFailure: function (error, caller) {
        alert(error.Detail);
    },

    _buildBreadcrumb: function (sender, folders) {
        //reset the array
        this.get_folderData().length = 0;
        //remove child nodes from the wrapper tag
        while (sender._wrapper.hasChildNodes()) {
            sender._wrapper.removeChild(sender._wrapper.lastChild);
        }
        var folderTag = document.createElement("a");
        jQuery(folderTag).html(this.get_rootFolderName());

        $addHandler(folderTag, "click", this._rootItemClickDeledate);
        this._rootFolderTag = folderTag;

        var arrowTag = document.createElement("i");
        jQuery(arrowTag).html("&rarr;");
        jQuery(arrowTag).addClass("sfSep");

        sender._wrapper.appendChild(folderTag);
        sender._wrapper.appendChild(arrowTag);

        for (var i = 0; i < folders.length - 1; i++) {

            var currentDataItem = folders[i];
            
            folderTag = document.createElement("a");
            folderTag.setAttribute("id", currentDataItem.Id);
            $addHandler(folderTag, "click", this._itemClickDelegate);

            arrowTag = document.createElement("i");
            jQuery(arrowTag).html("&rarr;");
            jQuery(arrowTag).addClass("sfSep");

            this.get_folderData().push(currentDataItem);

            jQuery(folderTag).text(currentDataItem.Title);

            sender._wrapper.appendChild(folderTag);
            sender._wrapper.appendChild(arrowTag);
        }

        var lastDataItem = folders[folders.length - 1];
        folderTag = document.createElement("span");
        
        jQuery(folderTag).text(lastDataItem.Title);

        sender._wrapper.appendChild(folderTag);
    },

    _findDataItem: function (id, folderData) {

        for (var i = 0; i < folderData.length; i++) {
            var currentDataItem = folderData[i];
            if (currentDataItem.Id == id) {
                return currentDataItem;
            }
        }
        return null;
    },
    // ------------------------------------------------- Properties ----------------------------------------------------    

    get_element: function () {
        return this._element;
    },

    set_element: function (value) {
        this._element = value;
    },

    set_baseServiceUrl: function (value) {
        this._baseServiceUrl = value;
    },

    get_baseServiceUrl: function () {
        return this._baseServiceUrl;
    },

    set_methodServiceUrl: function (value) {
        this._methodServiceUrl = value;
    },

    get_methodServiceUrl: function () {
        return this._methodServiceUrl;
    },

    get_clientManager: function () {
        if (this._clientManager == null) this._clientManager = new Telerik.Sitefinity.Data.ClientManager();
        return this._clientManager;
    },

    get_providerName: function () {
        return this._providerName;
    },

    set_providerName: function (value) {
        this._providerName = value;
    },

    get_itemId: function () {
        return this._itemId;
    },

    set_itemId: function (value) {
        this._itemId = value;
    },

    get_wrapper: function () {
        return this._wrapper;
    },

    set_wrapper: function (value) {
        this._wrapper = value;
    },

    get_excludeNeighbours: function () {
        return this._excludeNeighbours;
    },

    set_excludeNeighbours: function (value) {
        this._excludeNeighbours = value;
    },
    
    get_folderData: function () {
        if (!this._folderData) {
            this._folderData = new Array();
        }
        return this._folderData;
    },

    get_rootFolderName: function () {
        return this._rootFolderName;
    },

    set_rootFolderName: function (value) {
        this._rootFolderName = value;
    }
}

Telerik.Sitefinity.Folders.Web.UI.MediaContentBreadcrumb.registerClass("Telerik.Sitefinity.Folders.Web.UI.MediaContentBreadcrumb", Sys.UI.Control);