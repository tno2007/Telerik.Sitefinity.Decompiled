Type.registerNamespace("Telerik.Sitefinity.Workflow.UI");

Telerik.Sitefinity.Workflow.UI.LanguageSelectorField = function (element) {
    Telerik.Sitefinity.Workflow.UI.LanguageSelectorField.initializeBase(this, [element]);

    this._element = element;
    this._siteIds = [];
}

Telerik.Sitefinity.Workflow.UI.LanguageSelectorField.prototype = {

    _updateValueNamesUI: function () {
        var names = [];
        if (this._value != null && this._value.length > 0) {
            for (var i = 0; i < this._value.length; i++) {
                names.push(this._value[i].DisplayName);
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
            if (this._isFirstOpen) {
                this._isFirstOpen = false;
                this._rebindSelector(this._siteIds);
            }
              
            var items = [];
            if (this._value != null)
                items = this._value;
            var keys = [];
            for (var i = 0; i < items.length; i++) {
                keys.push(items[i]["ShortName"]);
            }
            dialog.set_intialSelectedItems(keys);
            dialog.resizeToContent();
        }
    },

    set_siteIds: function (value) {
         
        while (this._siteIds.length > 0) {
            this._siteIds.pop();
        }
        for (var i = 0; i < value.length; i++) {
            this._siteIds.push(value[i]);
        }
        this._rebindSelector(this._siteIds);
        
    },

    reset: function () {
        this.set_siteIds([]);
        Telerik.Sitefinity.Workflow.UI.LanguageSelectorField.callBaseMethod(this, "reset");
    },

    _rebindSelector: function(value) {
        var dialog = this.get_selectorDialog().AjaxDialog;
        if (dialog) {
            var selector = dialog.get_itemSelector();
            var binder = selector.get_binder();
            var manager = binder.get_manager();
            manager.set_serializePostData(false);
            binder.set_postData(JSON.stringify(value));
            binder.DataBind();
            manager.set_serializePostData(true);
        }
    }
};

Telerik.Sitefinity.Workflow.UI.LanguageSelectorField.registerClass("Telerik.Sitefinity.Workflow.UI.LanguageSelectorField", Telerik.Sitefinity.Web.UI.Fields.SelectorFieldBase);