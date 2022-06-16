Type.registerNamespace("Telerik.Sitefinity.Web.UI.Fields");

Telerik.Sitefinity.Web.UI.Fields.IBulkEditFieldControl = function() { }

Telerik.Sitefinity.Web.UI.Fields.IBulkEditFieldControl.prototype = {
    dataBind: function (selectedItems) { },

    saveChanges: function (urlParams, tempData, fieldControls, successCallback, failureCallback, caller) { }
};

Telerik.Sitefinity.Web.UI.Fields.IBulkEditFieldControl.registerInterface("Telerik.Sitefinity.Web.UI.Fields.IBulkEditFieldControl");
