Type.registerNamespace("Telerik.Sitefinity.Modules.Forms.Web.UI.Designers");

Telerik.Sitefinity.Modules.Forms.Web.UI.Designers.FormInstructionalTextDesigner = function (element) {
    Telerik.Sitefinity.Modules.Forms.Web.UI.Designers.FormInstructionalTextDesigner.initializeBase(this, [element]);
}

Telerik.Sitefinity.Modules.Forms.Web.UI.Designers.FormInstructionalTextDesigner.prototype = {
    /* --------------------------------- set up and tear down --------------------------------- */
    initialize: function () {
        Telerik.Sitefinity.Modules.Forms.Web.UI.Designers.FormInstructionalTextDesigner.callBaseMethod(this, 'initialize');
    },
    dispose: function () {
        Telerik.Sitefinity.Modules.Forms.Web.UI.Designers.FormInstructionalTextDesigner.callBaseMethod(this, 'dispose');
    }

    /* --------------------------------- public methods ---------------------------------- */

    /* --------------------------------- event handlers ---------------------------------- */

    /* --------------------------------- private methods --------------------------------- */

    /* --------------------------------- properties -------------------------------------- */
}

Telerik.Sitefinity.Modules.Forms.Web.UI.Designers.FormInstructionalTextDesigner.registerClass('Telerik.Sitefinity.Modules.Forms.Web.UI.Designers.FormInstructionalTextDesigner', Telerik.Sitefinity.Modules.GenericContent.Web.UI.ContentBlockDesignerBase);
