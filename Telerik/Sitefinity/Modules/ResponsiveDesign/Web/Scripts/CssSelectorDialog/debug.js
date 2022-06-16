/// <reference name="MicrosoftAjax.js"/>
/// <reference name="Telerik.Sitefinity.Resources.Scripts.jquery-1.6.3-vsdoc.js" assembly="Telerik.Sitefinity.Resources"/>
Type.registerNamespace("Telerik.Sitefinity.Modules.ResponsiveDesign.Web.FieldControls");

Telerik.Sitefinity.Modules.ResponsiveDesign.Web.FieldControls.CssSelectorDialog = function (element) {

    Telerik.Sitefinity.Modules.ResponsiveDesign.Web.FieldControls.CssSelectorDialog.initializeBase(this, [element]);

    this._loadingView = null;
}

Telerik.Sitefinity.Modules.ResponsiveDesign.Web.FieldControls.CssSelectorDialog.prototype =
{
    initialize: function () {
        Telerik.Sitefinity.Modules.ResponsiveDesign.Web.FieldControls.CssSelectorDialog.callBaseMethod(this, "initialize");
        var that = this;

        // hook up the done button
        jQuery(this._selectors.doneButton).click(function () {
            var treeview = jQuery(that._selectors.fileSystemTree).data("kendoTreeView");
            var selectedFile = {};
            selectedFile.Path = jQuery(treeview.select()).attr("data-sf-path");
            that.closeDialog(selectedFile);
        });

        // hook up the cancel button
        jQuery(this._selectors.cancelButton).click(function () {
            that.closeDialog();
        });

        jQuery("body").click(function (e) {
            var treeview = jQuery(that._selectors.fileSystemTree).data("kendoTreeView");
            var selectedNode = treeview.select();

            if (jQuery(e.target).hasClass("k-in") && selectedNode && selectedNode.find(".k-in")[0] == e.target) {
                treeview.select(jQuery());
            }
            else if (jQuery(e.target).hasClass("k-in")) {
                var node = jQuery(e.target).parents(".k-item").first();
                treeview.select(node);
            }
        });

        that.resizeToContent();

        // initialize the kendo tree for the file selector
        jQuery(this._selectors.fileSystemTree).kendoTreeView();
        var treeview = jQuery(that._selectors.fileSystemTree).data("kendoTreeView");

        treeview.bind("select", function (e) {
            e.preventDefault();
        });

        var currentValue = jQuery(this._selectors.selectedFileLabel, parent.document);
        this.set_value(currentValue)
    },

    /* --------------------  public methods ----------- */

    reset: function () {
    },

    set_value: function (value) {
        if (value) {
            var treeview = jQuery(this._selectors.fileSystemTree).data("kendoTreeView");
            var dataItem = treeview.dataItem('[data-sf-path="' + value.html() + '"]');
            if (dataItem) {
                var selectedNode = treeview.findByUid(dataItem.uid);
                jQuery(selectedNode).parentsUntil('.k-treeview').filter('.k-item').each(
                    function (index, element) {
                        treeview.expand(jQuery(this));
                    });
                treeview.select(selectedNode);
            }
        }
    },

    getRadWindow: function () {
        var oWindow = null;
        if (window.radWindow)
            oWindow = window.radWindow;
        else if (window.frameElement.radWindow)
            oWindow = window.frameElement.radWindow;
        return oWindow;
    },

    closeDialog: function (argument) {
        var dialog = this.getRadWindow();
        if (argument) {
            dialog.close(argument);
        }
        else {
            dialog.close();
        }
    },

    _selectors: {
        selectorDialog: "#css-selector-file-explorer-dialog",
        selectButton: "#css-selector-open-file-explorer-button",
        doneButton: "#css-selector-done-button",
        cancelButton: "#css-selector-cancel-button",
        fileSystemTree: "#css-selector-file-system-tree",
        selectedFileLabel: "#css-selector-selected-file"
    },

    get_loadingView: function () {
        return this._loadingView;
    },
    set_loadingView: function (value) {
        this._loadingView = value;
    }
};

Telerik.Sitefinity.Modules.ResponsiveDesign.Web.FieldControls.CssSelectorDialog.registerClass("Telerik.Sitefinity.Modules.ResponsiveDesign.Web.FieldControls.CssSelectorDialog", Telerik.Sitefinity.Web.UI.AjaxDialogBase);
