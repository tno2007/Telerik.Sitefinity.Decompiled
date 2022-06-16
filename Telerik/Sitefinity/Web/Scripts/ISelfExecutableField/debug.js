Type.registerNamespace("Telerik.Sitefinity.Web.UI");

Telerik.Sitefinity.Web.UI.ISelfExecutableField = function() { }

Telerik.Sitefinity.Web.UI.ISelfExecutableField.prototype = {

    saveChanges: function(dataItem, successCallback, failureCallback, caller) { },
    
    isChanged: function() {}
};

Telerik.Sitefinity.Web.UI.ISelfExecutableField.registerInterface("Telerik.Sitefinity.Web.UI.ISelfExecutableField");
