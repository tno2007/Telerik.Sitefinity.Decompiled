Type.registerNamespace("Telerik.Sitefinity.Web.UI");
/* ClientLabelManager class */

Telerik.Sitefinity.Web.UI.ClientLabelManager = function(element) {
    this._dictionary = null;
	Telerik.Sitefinity.Web.UI.ClientLabelManager.initializeBase(this, [element]);
}
Telerik.Sitefinity.Web.UI.ClientLabelManager.prototype = {
    // set up and teardown
    initialize: function () {
        this._dictionary = Sys.Serialization.JavaScriptSerializer.deserialize(this._dictionary);
        Telerik.Sitefinity.Web.UI.ClientLabelManager.callBaseMethod(this, "initialize");
    },
    dispose: function () {
        Telerik.Sitefinity.Web.UI.ClientLabelManager.callBaseMethod(this, "dispose");
    },
    getLabel: function (classId, key) {
        if (classId == null || classId == "") {
            return key;
        }
        return this._dictionary[classId + key];
    },
    getConstant: function (name) {
        if (name == null || name == "") {
            return "";
        }
        return this._dictionary[name];
    }
};
Telerik.Sitefinity.Web.UI.ClientLabelManager.registerClass('Telerik.Sitefinity.Web.UI.ClientLabelManager', Sys.UI.Control);