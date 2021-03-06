Type.registerNamespace("Telerik.Sitefinity.Taxonomies.Web.UI");
Telerik.Sitefinity.Taxonomies.Web.UI.TaxonDetailFormView = function (element) {
    Telerik.Sitefinity.Taxonomies.Web.UI.TaxonDetailFormView.initializeBase(this, [element]);

    this._formCreatedDelegate = null;
}

Telerik.Sitefinity.Taxonomies.Web.UI.TaxonDetailFormView.prototype = {

    initialize: function() { 
        Telerik.Sitefinity.Taxonomies.Web.UI.TaxonDetailFormView.callBaseMethod(this, "initialize");

        this._formCreatedDelegate = Function.createDelegate(this, this._formCreatedHandler);
        this.add_formCreated(this._formCreatedDelegate);
    },

    _formCreatedHandler: function (sender, args) {
        var fieldControlsCount = this._fieldControlIds.length;
        if (fieldControlsCount > 0) {
            while (fieldControlsCount--) {
                var fieldControl = $find(this._fieldControlIds[fieldControlsCount]);
                if (fieldControl.get_fieldName() == 'ParentTaxonId') {
                    if (args.get_dataItem() != null && args.get_dataItem().ParentTaxonId && args.get_commandArgument().languageMode == 'create') {
                        jQuery(fieldControl.get_element()).hide();
                    } else {
                        jQuery(fieldControl.get_element()).show();
                    }
                }
            }
        }
    },

    _closeDialog: function () {
        if (this._isOpenedInEditingWindow) {
            this._checkForChanges = false;
            var args = { deleteTemp: false, workflowOperationWasExecuted: true }
            if (this._isNew && this._isDirty) {
                args.isNew = true;
                args.lastModifiedDataItem = this.get_binder().get_lastModifiedDataItem();
            }
            this._raiseCloseInlineEditingWindow(args);
            return;
        }
        var args = this._raiseFormClosing(this._isNew, this._isDirty, this.get_binder().get_lastModifiedDataItem(), null, this._commandArgument);
        if (args.get_cancel())
            return;
        if (this._isNew && this._isDirty) {
            dialogBase.closeCreated(this.get_binder().get_lastModifiedDataItem(), this);
        }
        else if (this._isDirty) {
            if (this._callParams.NotifyUrlChanged) {
                var dataItem = this.get_binder().get_lastModifiedDataItem();
                var urlName = "";
                if (dataItem) {
                    urlName = dataItem.UrlName.Value;
                }
                dialogBase.close("redirect:" + urlName);
            }
            else if (this._multipleSaving === false) {
                dialogBase.closeUpdated(this.get_binder().get_lastModifiedDataItem(), this);
            }
        }
        else {
            dialogBase.close();
        }
    }
}

Telerik.Sitefinity.Taxonomies.Web.UI.TaxonDetailFormView.registerClass('Telerik.Sitefinity.Taxonomies.Web.UI.TaxonDetailFormView', Telerik.Sitefinity.Web.UI.ContentUI.Views.Backend.Detail.DetailFormView);