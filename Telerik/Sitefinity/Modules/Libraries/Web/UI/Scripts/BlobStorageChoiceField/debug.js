
Type.registerNamespace("Telerik.Sitefinity.Modules.Libraries.Web.UI"); 

Telerik.Sitefinity.Modules.Libraries.Web.UI.BlobStorageChoiceField = function (element) {
    Telerik.Sitefinity.Modules.Libraries.Web.UI.BlobStorageChoiceField.initializeBase(this, [element]);
}

Telerik.Sitefinity.Modules.Libraries.Web.UI.BlobStorageChoiceField.prototype =
{
    initialize: function () {
        Telerik.Sitefinity.Modules.Libraries.Web.UI.BlobStorageChoiceField.callBaseMethod(this, "initialize");
    },

    dispose: function () {
        Telerik.Sitefinity.Modules.Libraries.Web.UI.BlobStorageChoiceField.callBaseMethod(this, "dispose");
    },

    set_value: function (value) {
        if (value) {
            Telerik.Sitefinity.Modules.Libraries.Web.UI.BlobStorageChoiceField.callBaseMethod(this, "set_value", [value]);
        } else {
            Telerik.Sitefinity.Modules.Libraries.Web.UI.BlobStorageChoiceField.callBaseMethod(this, "set_value", [this.get_defaultValue()]);
        }
        // Fix: ChoiceField in Read mode sets the key instead of the text of the selected choice
        if (value && this.get_displayMode() == Telerik.Sitefinity.Web.UI.Fields.FieldDisplayMode.Read) {
            for (var i = 0; i < this.get_choices().length; i++) {
                if (this.get_choices()[i].Value === value && this.get_choices()[i].Enabled) {
                    jQuery(this.get_readModeLabel()).text(this.get_choices()[i].Text);
                    break;
                }
            }
        }
    }
};

Telerik.Sitefinity.Modules.Libraries.Web.UI.BlobStorageChoiceField.registerClass("Telerik.Sitefinity.Modules.Libraries.Web.UI.BlobStorageChoiceField", Telerik.Sitefinity.Web.UI.Fields.ChoiceField);