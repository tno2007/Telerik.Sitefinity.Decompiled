/// <reference name="MicrosoftAjax.js"/>
/// <reference name="Telerik.Sitefinity.Resources.Scripts.jquery-1.6.3-vsdoc.js" assembly="Telerik.Sitefinity.Resources"/>
Type.registerNamespace("Telerik.Sitefinity.Security.Web.UI");

// ------------------------------------------------------------------------
// UserProfileDetailWriteView class
// ------------------------------------------------------------------------

Telerik.Sitefinity.Security.Web.UI.UserProfileDetailWriteView = function (element) {
    Telerik.Sitefinity.Security.Web.UI.UserProfileDetailWriteView.initializeBase(this, [element]);

    this._originalEmail = null;
    this._isExternalUser = null;
    this._isInPasswordMode = false;
    this._test = null;
    this._saveChangesControl = null;
    this._passwordRequirePanel = null;
    this._linkBtnHref = null
    this._saveChangesButtonClickDeledate = null;
};

Telerik.Sitefinity.Security.Web.UI.UserProfileDetailWriteView.prototype = {

    /* --------------------------------- set up and tear down --------------------------------- */

    initialize: function () {
        Telerik.Sitefinity.Security.Web.UI.UserProfileDetailWriteView.callBaseMethod(this, 'initialize');

        if (!this._isExternalUser) {
            var saveChangesControl = this.get_saveChangesControl();
            if (saveChangesControl != null) {
                this._saveChangesButtonClickDeledate = Function.createDelegate(this, this._saveChangesButtonClickHandler);
                saveChangesControl.onclick = this._saveChangesButtonClickDeledate;
            }
        }
    },

    dispose: function () {
        if (this._saveChangesButtonClickHandler) {
            delete this._saveChangesButtonClickHandler;
        }

        Telerik.Sitefinity.Security.Web.UI.UserProfileDetailWriteView.callBaseMethod(this, 'dispose');
    },

    /* --------------------------------- public methods --------------------------------- */

    

    /* --------------------------------- event handlers --------------------------------- */

    _saveChangesButtonClickHandler: function (sender, args) {
        var linkBtn = this.get_saveChangesControl();
        var emailField = $(".sfProfileEmailMarker :input")[0];
        var email = emailField ? emailField.value : null;
        if (!this._isInPasswordMode && this._originalEmail != email && this._passwordRequirePanel != null) {
            this._isInPasswordMode = true;

            $(".sfProfileFieldsListMarker").hide();
            $(this._passwordRequirePanel).show();
            
            this._linkBtnHref = linkBtn.href;
            linkBtn.href = "#";

            return false;
        } else {
            if (this._linkBtnHref != null)
                linkBtn.href = this._linkBtnHref;

            return true;
        }

        return false;
    },

    /* --------------------------------- private methods --------------------------------- */

    

    /* --------------------------------- properties --------------------------------- */
    get_test: function () {
        return this._userSelector;
    },
    set_test: function (value) {
        this._test = value;
    },

    get_saveChangesControl: function () {
        return this._saveChangesControl;
    },
    set_saveChangesControl: function (value) {
        this._saveChangesControl = value;
    },

    get_passwordRequirePanel: function () {
        return this._passwordRequirePanel;
    },
    set_passwordRequirePanel: function (value) {
        this._passwordRequirePanel = value;
    }
}

Telerik.Sitefinity.Security.Web.UI.UserProfileDetailWriteView.registerClass('Telerik.Sitefinity.Security.Web.UI.UserProfileDetailWriteView', Telerik.Sitefinity.Web.UI.ContentUI.Views.Backend.ViewBase);