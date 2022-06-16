﻿Type.registerNamespace("Telerik.Sitefinity.Web.UI.Fields");

Telerik.Sitefinity.Web.UI.Fields.SitesChoiceField = function (element) {
    Telerik.Sitefinity.Web.UI.Fields.SitesChoiceField.initializeBase(this, [element]);
}

Telerik.Sitefinity.Web.UI.Fields.SitesChoiceField.prototype = {

    _updateValueNamesUI: function () {
        var names = [];
        if (this._value != null && this._value.length > 0) {
            for (var i = 0; i < this._value.length; i++) {
                names.push(this._value[i].Name);
            }

            jQuery(this.get_selectedValuesContainer()).html(names.join(', '));
            jQuery(this.get_selectedValuesContainer()).show();
        } else {
            jQuery(this.get_selectedValuesContainer()).hide();
        }
    },
   
    _selectorDialogShowHandler: function (sender, args) {
        var dialog = this.get_selectorDialog().AjaxDialog;
        if (dialog) {
            var selector = dialog.get_itemSelector();
            var binder = selector.get_binder();
            if (this._isFirstOpen) {
                selector.bindSelector();
            }

            var items = [];
            if (this._value != null)
                items = this._value;
            var keys = [];
            for (var i = 0; i < items.length; i++) {
                keys.push(items[i]["SiteId"]);
            }
            dialog.set_intialSelectedItems(keys);
            dialog.resizeToContent();
        }
    },

    _selectorDialogCloseHandler: function (sender, args) {
        var items = args.get_argument();
        if (items != null) {
            var trimmed = [];
            for (var i = 0; i < items.length; i++) {
                trimmed.push({
                    SiteId: items[i].Id,
                    Name: items[i].Name,
                    Cultures: [].concat(items[i].Cultures)
                });
            }
            this.set_value(trimmed);
        }
    }
};

Telerik.Sitefinity.Web.UI.Fields.SitesChoiceField.registerClass("Telerik.Sitefinity.Web.UI.Fields.SitesChoiceField", Telerik.Sitefinity.Web.UI.Fields.SelectorFieldBase);