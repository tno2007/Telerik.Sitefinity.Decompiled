Type.registerNamespace("Telerik.Sitefinity.Web.UI.Fields");

Telerik.Sitefinity.Web.UI.Fields.BlockableIpField = function (element) {
    Telerik.Sitefinity.Web.UI.Fields.BlockableIpField.initializeBase(this, [element]);

    this._serviceUrl = null;
    this._ipTextField = null;
    this._blockIpLink = null;
    this._unblockIpLink = null;
    this._clientLabelManager = null;

    this._blockIpDelegate = null;
    this._unblockIpDelegate = null;
    this._onLoadDelegate = null;
}

Telerik.Sitefinity.Web.UI.Fields.BlockableIpField.prototype = {
    initialize: function () {
        Telerik.Sitefinity.Web.UI.Fields.BlockableIpField.callBaseMethod(this, 'initialize');

        this._blockIpDelegate = Function.createDelegate(this, this.blockIp);
        $addHandler(this._blockIpLink, "click", this._blockIpDelegate, true);

        this._unblockIpDelegate = Function.createDelegate(this, this.unblockIp);
        $addHandler(this._unblockIpLink, "click", this._unblockIpDelegate, true);

        this._onLoadDelegate = Function.createDelegate(this, this._onLoad);
        Sys.Application.add_load(this._onLoadDelegate);
        
        //hiding block ip link because it is not implemented
        jQuery(this._blockIpLink).hide();
        jQuery(this._unblockIpLink).hide();
    },
    dispose: function () {
        Telerik.Sitefinity.Web.UI.Fields.BlockableIpField.callBaseMethod(this, 'dispose');

        if (this._blockIpDelegate) {
            delete this._blockIpDelegate;
        }

        if (this._unblockIpDelegate) {
            delete this._unblockIpDelegate;
        }

        if (this._onLoadDelegate) {
            Sys.Application.remove_load(this._onLoadDelegate);
            delete this._onLoadDelegate;
        }
    },

    /* ------------------ Events --------------*/
    _onLoad: function () {


    },

    /* --------------------  public methods ----------- */
    blockIp: function () {
        this._blockIp_Success();
        //Actual blocking not yet working
        //clientManager = new Telerik.Sitefinity.Data.ClientManager();
        //clientManager.InvokePut(serviceUrl, urlParams, keys, _blockIp_Success, _serviceCall_Failure);
    },

    unblockIp: function () {
        this._unblockIp_Success();
        //Actual blocking not yet working
        //clientManager = new Telerik.Sitefinity.Data.ClientManager();
        //clientManager.InvokePut(serviceUrl, urlParams, keys, _unblockIp_Success, _serviceCall_Failure);
    },

    /* ------------------ Private methods --------------*/

    _blockIp_Success: function (result) {
        jQuery(this._blockIpLink).hide();
        jQuery(this._unblockIpLink).show();
        var blockedResource = this._clientLabelManager.control.getLabel("ContentResources", "Blocked");
        jQuery(this._ipTextField).addClass("sfBlocked").find(".sfTxtContent").append("<span class='sfBlockLbl'>" + blockedResource + "</span>");
    },

    _unblockIp_Success: function (result) {
        jQuery(this._blockIpLink).show();
        jQuery(this._unblockIpLink).hide();
        jQuery(this._ipTextField).removeClass("sfBlocked").find(".sfTxtContent .sfBlockLbl").remove();
    },

    /* ------------------ Properies --------------*/
    get_ipTextField: function () {
        return this._ipTextField;
    },

    set_ipTextField: function (value) {
        this._ipTextField = value;
    }
    ,
    get_blockIpLink: function () {
        return this._blockIpLink;
    },

    set_blockIpLink: function (value) {
        this._blockIpLink = value;
    }
    ,
    get_unblockIpLink: function () {
        return this._unblockIpLink;
    },

    set_unblockIpLink: function (value) {
        this._unblockIpLink = value;
    }
    ,
    get_clientLabelManager: function () {
        return this._clientLabelManager;
    },

    set_clientLabelManager: function (value) {
        this._clientLabelManager = value;
    }
}

Telerik.Sitefinity.Web.UI.Fields.BlockableIpField.registerClass('Telerik.Sitefinity.Web.UI.Fields.BlockableIpField', Sys.UI.Control);
