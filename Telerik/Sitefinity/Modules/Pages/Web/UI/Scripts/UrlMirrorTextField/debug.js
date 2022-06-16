Type.registerNamespace("Telerik.Sitefinity.Modules.Pages.Web.UI.Fields");

Telerik.Sitefinity.Modules.Pages.Web.UI.Fields.UrlMirrorTextField = function(element) {
    Telerik.Sitefinity.Modules.Pages.Web.UI.Fields.UrlMirrorTextField.initializeBase(this, [element]);

    this._extension = null;
    this._urlControlId = null;
    this._urlControl = null;
    this._urlLabelControl = null;
    this._urlLabelControlId = null;
}

Telerik.Sitefinity.Modules.Pages.Web.UI.Fields.UrlMirrorTextField.prototype =
{
    initialize: function () {
        Telerik.Sitefinity.Modules.Pages.Web.UI.Fields.UrlMirrorTextField.callBaseMethod(this, "initialize");
        this._pageLoadDelegate = Function.createDelegate(this, this.onLoadURL);
        Sys.Application.add_load(this._pageLoadDelegate);
    },

    dispose: function () {
        var urlControl = this.get_urlControl();
        if (urlControl != null && this._parentChangedDelegate != null) {
            urlControl.remove_valueChanged(this._parentChangedDelegate);
            delete this._parentChangedDelegate;
        }

        var mirroredControl = this.get_mirroredControl();
        if (mirroredControl != null && this._mirroredValueChangedDelegate != null) {
            mirroredControl.remove_valueChanged(this._mirroredValueChangedDelegate);
            delete this._mirroredValueChangedDelegate;
        }

        if (this._pageLoadDelegate)
            delete this._pageLoadDelegate;

        Telerik.Sitefinity.Modules.Pages.Web.UI.Fields.UrlMirrorTextField.callBaseMethod(this, "dispose");
    },

    onLoadURL: function () {
        //Subscribe for change parent events
        var urlControl = this.get_urlControl();
        if (this._parentChangedDelegate == null) {
            this._parentChangedDelegate = Function.createDelegate(this, this._parentChanged);
        }
        urlControl.add_valueChanged(this._parentChangedDelegate);

        //Subscribe for name changed (only raises when value set from system, not UI)
        var mirroredControl = this.get_mirroredControl();
        if (this._mirroredValueChangedDelegate == null) {
            this._mirroredValueChangedDelegate = Function.createDelegate(this, this._mirroredValueChanged);
        }
        mirroredControl.add_valueChanged(this._mirroredValueChangedDelegate);

        //Insert DOM element for the URL
        this._urlLabelControlId = "urlLabelElement";
        var textElmId = this.get_textElement().id;
        var urlLabelElm = $("<span></span>").attr('id', this.get_urlLabelControlId()); //.click(somefunction); 
        urlLabelElm.addClass("sfUrlPath");
        $('#' + textElmId).before(urlLabelElm);
        this._urlLabelControl = urlLabelElm.get(0);

        //Refresh the URL
        this.refreshURL();

        //Hide button if empty name
        this._refreshChangeButtonState();
    },


    _mirroredValueChanged: function (sender, args) {
        this._refreshChangeButtonState();
    },
    _parentChanged: function (sender, args) {
        this.refreshURL();
    },


    /* --------------------  public methods ----------- */

    //Overrides the mirror method to hide/show the "Change" button
    mirror: function () {
        Telerik.Sitefinity.Modules.Pages.Web.UI.Fields.UrlMirrorTextField.callBaseMethod(this, "mirror");
        this._refreshChangeButtonState();
        if (this.get_isToMirror()) {
            this._refreshUrlExtension();
        }
    },
    //Refreshes the value of the parent URL
    refreshURL: function () {
        var url = this.getCurrentParentURL();
        var urlLabelElm = this.get_urlLabelControl();
        urlLabelElm.innerHTML = url;
    },
    //Returns the parent URL corresponding to the current parent selection.
    getCurrentParentURL: function () {
        var selectedNode = this.get_urlControl().get_value();

        var urlString = this._getURLPrefix();
        if (selectedNode != null) {
            var path = selectedNode.Path;
            path = path.replace(/ > /gi, "/");
            if (path.length > 0) {
                path = "/" + path;
            }
            urlString += path;
        }
        urlString += this._getURLSuffix();
        return urlString;
    },

    //overrides the checkIfIsToMirror to add extension to the check value
    checkIfIsToMirror: function () {
        this._ckeckConditionalMirroring = false;
        var filteredMirroredControlValue = this._getFilteredMirroredControlValue().toLowerCase();
        if (filteredMirroredControlValue && this.get_extension()) {
            filteredMirroredControlValue += this.get_extension();
        }
        if (!this.get_prefixText()) {
            if (filteredMirroredControlValue == null || filteredMirroredControlValue == undefined || this.get_value().toLowerCase() !== filteredMirroredControlValue)
                this._isToMirror = false;
        }
        else {
            if (filteredMirroredControlValue == null || filteredMirroredControlValue == undefined || (this.get_value().toLowerCase() !== filteredMirroredControlValue && (this.get_prefixText() + this.get_value()).toLowerCase() !== filteredMirroredControlValue))
                this._isToMirror = false;
        }
    },

    /* -------------------- private methods ----------- */

    _refreshChangeButtonState: function () {
        var filteredMirroredControlValue = this._getFilteredMirroredControlValue();
        if (filteredMirroredControlValue.length == 0) {
            this.get_changeControl().style.display = "none";
        } else {
            this.get_changeControl().style.display = "";
        }
    },
    _getURLSuffix: function () {
        return "/";
    },
    _getURLPrefix: function () {
        return "";
    },
    _refreshUrlExtension: function () {
        var filteredMirroredControlValue = this._getFilteredMirroredControlValue();
        if (filteredMirroredControlValue.length != 0) {
            if (this.get_extension() != "") {
                var mirrorValue = this.get_mirroredValueLabel().innerHTML;
                this.get_mirroredValueLabel().innerHTML = mirrorValue + this.get_extension();
            }
        }
    },

    /* -------------------- properties ---------------- */
    get_urlControl: function () {
        if (!this._urlControl) {
            if (this.get_urlControlId()) {
                if (!$get_clientId)
                    throw "The page does not have the required FormManager or you are trying to use it before it is initialized.";
                var urlControlClientId = $get_clientId(this.get_urlControlId());
                if (!urlControlClientId)
                    throw "No control with ID:'" + this.get_urlControlId() + "' have been registered to the FormManager. Please make sure that the control that you want to mirror calls FormManager.GetCurrent().Register(this);";
                //TODO make this work for other controls
                this._urlControl = $find(urlControlClientId);
            }
            else {
                throw "The UrlControlId is not set.";
            }
        }
        return this._urlControl;
    },
    get_urlControlId: function () {
        return this._urlControlId;
    },
    set_urlControlId: function (value) {
        this._urlControlId = value;
    },
    get_urlLabelControl: function () {
        if (!this._urlLabelControl) {
            if (this.get_urlLabelControlId()) {
                this._urlLabelControl = $('#' + this.get_urlLabelControlId()).get(0);
            } else {
                throw "The UrlLabelControlId is not set.";
            }
        }
        return this._urlLabelControl;
    },
    get_urlLabelControlId: function () {
        return this._urlLabelControlId;
    },
    get_extension: function () {
        return this._extension;
    },
    set_extension: function (value) {
        this._extension = value;
    }
};

Telerik.Sitefinity.Modules.Pages.Web.UI.Fields.UrlMirrorTextField.registerClass("Telerik.Sitefinity.Modules.Pages.Web.UI.Fields.UrlMirrorTextField", Telerik.Sitefinity.Web.UI.Fields.MirrorTextField);