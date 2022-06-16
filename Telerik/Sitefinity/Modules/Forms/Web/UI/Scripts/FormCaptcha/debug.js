Type.registerNamespace("Telerik.Sitefinity.Modules.Forms.Web.UI.Fields");

Telerik.Sitefinity.Modules.Forms.Web.UI.Fields.FormCaptcha = function (element) {
    Telerik.Sitefinity.Modules.Forms.Web.UI.Fields.FormCaptcha.initializeBase(this, [element]);

    this._radFormCaptcha = null;
}

Telerik.Sitefinity.Modules.Forms.Web.UI.Fields.FormCaptcha.prototype = {
    /* --------------------------------- set up and tear down --------------------------------- */
    initialize: function () {
        Telerik.Sitefinity.Modules.Forms.Web.UI.Fields.FormCaptcha.callBaseMethod(this, 'initialize');
    },
    dispose: function () {
        Telerik.Sitefinity.Modules.Forms.Web.UI.Fields.FormCaptcha.callBaseMethod(this, 'dispose');
    },

    /* --------------------------------- public methods ---------------------------------- */
    reset: function () {
    },
    /* --------------------------------- event handlers ---------------------------------- */

    /* --------------------------------- private methods --------------------------------- */

    /* --------------------------------- properties -------------------------------------- */
    get_radFormCaptcha: function () {
        return this._radFormCaptcha;
    },
    set_radFormCaptcha: function (value) {
        this._radFormCaptcha = value;
    }
}

Telerik.Sitefinity.Modules.Forms.Web.UI.Fields.FormCaptcha.registerClass('Telerik.Sitefinity.Modules.Forms.Web.UI.Fields.FormCaptcha', Telerik.Sitefinity.Web.UI.Fields.FieldControl);