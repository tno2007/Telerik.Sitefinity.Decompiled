Type.registerNamespace("Telerik.Sitefinity.Web.UI.Fields");

Telerik.Sitefinity.Web.UI.Fields.ContentUrlField = function (element) {
    Telerik.Sitefinity.Web.UI.Fields.ContentUrlField.initializeBase(this, [element]);
}

Telerik.Sitefinity.Web.UI.Fields.ContentUrlField.prototype =
{
    initialize: function () {
        Telerik.Sitefinity.Web.UI.Fields.ContentUrlField.callBaseMethod(this, "initialize");
    },

    dispose: function () {
        Telerik.Sitefinity.Web.UI.Fields.ContentUrlField.callBaseMethod(this, "dispose");
    },

    /* IRequiresDataItemContext */

    set_dataItemContext: function (itemContext) {
        if ((itemContext.LifecycleStatus && itemContext.LifecycleStatus.HasLiveVersion) || itemContext.HasLiveVersion) {
            this._isToMirror = false;
        } else {
            this._isToMirror = true;
        }
    }

    /* IRequiresDataItemContext */
};

Telerik.Sitefinity.Web.UI.Fields.ContentUrlField.registerClass("Telerik.Sitefinity.Web.UI.Fields.ContentUrlField", Telerik.Sitefinity.Web.UI.Fields.MirrorTextField, Telerik.Sitefinity.Web.UI.Fields.IRequiresDataItemContext);