Type.registerNamespace("Telerik.Sitefinity.Modules.Forms.Web.UI.Fields");

Telerik.Sitefinity.Modules.Forms.Web.UI.Fields.FormDropDownList = function (element) {
    Telerik.Sitefinity.Modules.Forms.Web.UI.Fields.FormDropDownList.initializeBase(this, [element]);
}

Telerik.Sitefinity.Modules.Forms.Web.UI.Fields.FormDropDownList.prototype = {
    /* --------------------------------- set up and tear down --------------------------------- */
    initialize: function () {
        Telerik.Sitefinity.Modules.Forms.Web.UI.Fields.FormDropDownList.callBaseMethod(this, 'initialize');
    },
    dispose: function () {
        Telerik.Sitefinity.Modules.Forms.Web.UI.Fields.FormDropDownList.callBaseMethod(this, 'dispose');
    }

    /* --------------------------------- public methods ---------------------------------- */

    /* --------------------------------- event handlers ---------------------------------- */

    /* --------------------------------- private methods --------------------------------- */

    /* --------------------------------- properties -------------------------------------- */
}

Telerik.Sitefinity.Modules.Forms.Web.UI.Fields.FormDropDownList.registerClass('Telerik.Sitefinity.Modules.Forms.Web.UI.Fields.FormDropDownList', Telerik.Sitefinity.Web.UI.Fields.ChoiceField);
