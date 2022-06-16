Type.registerNamespace("Telerik.Sitefinity.Web.UI.Fields");

Telerik.Sitefinity.Web.UI.Fields.BlockableEmailField = function (element) {
    Telerik.Sitefinity.Web.UI.Fields.BlockableEmailField.initializeBase(this, [element]);

    this._serviceUrl = null;
    this._emailControl = null;
    this._blockEmailLink = null;
    this._unblockEmailLink = null;
    this._clientLabelManager = null;

    this._blockEmailDelegate = null;
    this._unblockEmailDelegate = null;
    this._onLoadDelegate = null;
}

Telerik.Sitefinity.Web.UI.Fields.BlockableEmailField.prototype = {
    initialize: function () {
        Telerik.Sitefinity.Web.UI.Fields.BlockableEmailField.callBaseMethod(this, 'initialize');

            this._blockEmailDelegate = Function.createDelegate(this, this.blockEmail);
            $addHandler(this._blockEmailLink, "click", this._blockEmailDelegate, true);

            this._unblockEmailDelegate = Function.createDelegate(this, this.unblockEmail);
            $addHandler(this._unblockEmailLink, "click", this._unblockEmailDelegate, true);

            this._onLoadDelegate = Function.createDelegate(this, this._onLoad);
            Sys.Application.add_load(this._onLoadDelegate);

            //hiding block ip link because it is not implemented
            jQuery(this._blockEmailLink).hide();
            jQuery(this._unblockEmailLink).hide();
    },
    dispose: function () {
        Telerik.Sitefinity.Web.UI.Fields.BlockableEmailField.callBaseMethod(this, 'dispose');

        if (this._blockEmailDelegate) {
            delete this._blockEmailDelegate;
        }

        if (this._unblockEmailDelegate) {
            delete this._unblockEmailDelegate;
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
    blockEmail: function () {
        this._blockEmail_Success();
        //Actual blocking not yet working
        //clientManager = new Telerik.Sitefinity.Data.ClientManager();
        //clientManager.InvokePut(serviceUrl, urlParams, keys, _blockEmail_Success, _serviceCall_Failure);

    },

    unblockEmail: function () {
        this._unblockEmail_Success();
        //Actual blocking not yet working
        //clientManager = new Telerik.Sitefinity.Data.ClientManager();
        //clientManager.InvokePut(serviceUrl, urlParams, keys, _unblockEmail_Success, _serviceCall_Failure);

    },

    /* ------------------ Private methods --------------*/

    _blockEmail_Success: function (result) {
        jQuery(this._blockEmailLink).hide();
        jQuery(this._unblockEmailLink).show();
        var blockedResource = this._clientLabelManager.control.getLabel("ContentResources", "Blocked");
        jQuery(this._emailControl).addClass("sfBlocked").find(".sfFieldWrp").append("<span class='sfBlockLbl'>" + blockedResource + "</span>");
    },

    _unblockEmail_Success: function (result) {
        jQuery(this._blockEmailLink).show();
        jQuery(this._unblockEmailLink).hide();
        jQuery(this._emailControl).removeClass("sfBlocked").find(".sfFieldWrp .sfBlockLbl").remove();
    },

    _serviceCall_Failure: function (result) {
        alert(result.Detail);
    },
    /* ------------------ Properies --------------*/
    get_emailControl: function () {
        return this._emailControl;
    },

    set_emailControl: function (value) {
        this._emailControl = value;
    }
    ,
    get_blockEmailLink: function () {
        return this._blockEmailLink;
    },

    set_blockEmailLink: function (value) {
        this._blockEmailLink = value;
    }
    ,
    get_unblockEmailLink: function () {
        return this._unblockEmailLink;
    },

    set_unblockEmailLink: function (value) {
        this._unblockEmailLink = value;
    }
    ,
    get_clientLabelManager: function () {
        return this._clientLabelManager;
    },

    set_clientLabelManager: function (value) {
        this._clientLabelManager = value;
    }
}

Telerik.Sitefinity.Web.UI.Fields.BlockableEmailField.registerClass('Telerik.Sitefinity.Web.UI.Fields.BlockableEmailField', Sys.UI.Control);
