Type.registerNamespace("Telerik.Sitefinity.Modules.Forms.Web.UI.Fields");

Telerik.Sitefinity.Modules.Forms.Web.UI.Fields.FormEmailTextBox = function (element) {
    Telerik.Sitefinity.Modules.Forms.Web.UI.Fields.FormEmailTextBox.initializeBase(this, [element]);
}

Telerik.Sitefinity.Modules.Forms.Web.UI.Fields.FormEmailTextBox.prototype = {
    /* --------------------------------- set up and tear down --------------------------------- */
    initialize: function () {
        Telerik.Sitefinity.Modules.Forms.Web.UI.Fields.FormEmailTextBox.callBaseMethod(this, 'initialize');
    },
    dispose: function () {
        Telerik.Sitefinity.Modules.Forms.Web.UI.Fields.FormEmailTextBox.callBaseMethod(this, 'dispose');
    }

    /* --------------------------------- public methods ---------------------------------- */

    /* --------------------------------- event handlers ---------------------------------- */

    /* --------------------------------- private methods --------------------------------- */

    /* --------------------------------- properties -------------------------------------- */
}

Telerik.Sitefinity.Modules.Forms.Web.UI.Fields.FormEmailTextBox.registerClass('Telerik.Sitefinity.Modules.Forms.Web.UI.Fields.FormEmailTextBox', Telerik.Sitefinity.Web.UI.Fields.EmailTextField);
