/* FormBinder class */

/* Concrete implementation of the ClientBinder for the static form (form that defines static fields in its
 * client template). This binder is generally used for creating new item or editing existing item.
 */
Telerik.Sitefinity.Web.UI.FormBinder = function() {
    Telerik.Sitefinity.Web.UI.FormBinder.initializeBase(this);
}
Telerik.Sitefinity.Web.UI.FormBinder.prototype = {
    // set up and tear down
    initialize: function () {
        Telerik.Sitefinity.Web.UI.FormBinder.callBaseMethod(this, 'initialize');
        this._serviceCallback = this.BindItem;
    },
    dispose: function () {
        Telerik.Sitefinity.Web.UI.FormBinder.callBaseMethod(this, 'dispose');
    },
    DataBind: function (dataItem) {
        Telerik.Sitefinity.Web.UI.FormBinder.callBaseMethod(this, "DataBind");
        this.ClearTarget();
        if (dataItem) {
            this.BindItem(dataItem);
        }
        else {
            var clientManager = this.get_manager();
            clientManager.set_urlParams(this.get_urlParams());
            clientManager.GetItem(this);
        }
    },
    New: function () {
        var mockData = [];
        this.Reset();
        this.BindItem(mockData);
    },
    SaveChanges: function () {
        if ($(document.forms[0]).validate().form() == false) {
            return false;
        }
        this._savingHandler();
        binder = this;
        var clientManager = this.get_manager();
        clientManager.set_urlParams(this.get_urlParams());
        $(this.GetTarget()).find('.sys-container').each(function (i, element) {
            var dataItem = binder.GetData(element);
            binder._itemSavingHandler(binder.GetItemKey(i), dataItem, i, element);
            clientManager.SaveItem(binder, null, dataItem, null);
        });
        return true;
    },
    // Overridden method from the ClientBinder. The ClientManager will call this method after it receives data
    // (collection of items) from the service and pass the CollectionContext (defined on the server) to it.
    BindCollection: function (data) {
        alert('Bind function is not supported by FormBinder. Please use BindItem instead');
    },
    // Overridden method from the ClientBinder. The ClientManager will call this method afer it receives data
    // (single item) from the service and pass the deserialized item to it.
    BindItem: function (data) {
        var template = new Sys.UI.Template($get(this._clientTemplates[0]));
        var isGeneric = false;
        if (data != null && data.IsGeneric != null) {
            isGeneric = data.IsGeneric;
        }
        var target = this.GetTarget();
        this.LoadItem(target, template, data, isGeneric, 0);

        // ensure unique IDs
        $(target).find('[id]').each(function (i, element) {
            // new ajax framework always adds index to id parameters, so remove it
            var attr = $(element).attr('id');
            var attrUnique = attr.substring(0, attr.length - 1) + 0;
            $(element).attr('id', attrUnique);
        });

        $(target).find('[for]').each(function (i, element) {
            var attrUnique = $(element).attr('for') + 0;
            $(element).attr('for', attrUnique);
        });

        this.BuildInputWrappers(0, target, data);

        this.LoadDataItemKey(data);
        var key = this.GetItemKey(0);
        this.AssignCommands(target, data, key, 0);
        this.ApplyValidationRules(target, 0);
        this._itemDataBoundHandler(key, data, 0, target);
        this._dataBoundHandler();
    }
}
Telerik.Sitefinity.Web.UI.FormBinder.registerClass('Telerik.Sitefinity.Web.UI.FormBinder', Telerik.Sitefinity.Web.UI.ClientBinder);