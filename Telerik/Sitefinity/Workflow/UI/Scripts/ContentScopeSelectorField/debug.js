Type.registerNamespace("Telerik.Sitefinity.Workflow.UI");

Telerik.Sitefinity.Workflow.UI.ContentScopeSelectorField = function (element) {
    Telerik.Sitefinity.Workflow.UI.ContentScopeSelectorField.initializeBase(this, [element]);

    this._element = element;
}

Telerik.Sitefinity.Workflow.UI.ContentScopeSelectorField.prototype = {

    _updateValueNamesUI: function () {
        var names = [];
        if (this._value != null && this._value.length > 0) {
            for (var i = 0; i < this._value.length; i++) {
                names.push(this._value[i].Title);
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
             
            var items = [];
            if (this._value != null)
                items = this._value;
            var keys = [];
            for (var i = 0; i < items.length; i++) {
                keys.push(items[i]["ContentType"]);
            }
            dialog.set_intialSelectedItems(keys);
            dialog.resizeToContent();

            if (this._isFirstOpen) {
                this._isFirstOpen = false;
                var selector = dialog.get_itemSelector();
                var binder = selector.get_binder();
                this._configureBinder(binder);
                binder.DataBind();
            }
        }
    },

    _configureBinder: function(binder) {
        var params = binder.get_urlParams();
        params['sf_date_param'] = new Date().getTime();
        binder.set_unescapeHtml(true);
    }
};

Telerik.Sitefinity.Workflow.UI.ContentScopeSelectorField.registerClass("Telerik.Sitefinity.Workflow.UI.ContentScopeSelectorField", Telerik.Sitefinity.Web.UI.Fields.SelectorFieldBase);