/// <reference name="MicrosoftAjax.js"/>
/// <reference name="Telerik.Sitefinity.Resources.Scripts.jquery-1.6.3-vsdoc.js" assembly="Telerik.Sitefinity.Resources"/>
//Type._registerScript("TaxonomyControlDesignerView.js", ["IDesignerViewControl.js"]);
//Type.registerNamespace("Telerik.Sitefinity.Web.UI.ControlDesign");
Type.registerNamespace("Telerik.Sitefinity.Web.UI.PublicControls.Designers");

Telerik.Sitefinity.Web.UI.PublicControls.Designers.TaxonomyControlDesigner = function (element) {
    this._autoProperty = function (name) {
        var underscored = "_" + name;
        this[underscored] = null;
        this["get" + underscored] = function () { return this[underscored]; };
        this["set" + underscored] = function (value) { this[underscored] = value; };
    };

    this._onLoadDelegate = null;
    this._onUnloadDelegate = null;

    this._autoProperty("parentDesigner");
    this._autoProperty("propertyEditor");
    this._autoProperty("showItemCountCheckBox");
    this._autoProperty("showEmptyItemsCheckBox");

    Telerik.Sitefinity.Web.UI.PublicControls.Designers.TaxonomyControlDesigner.initializeBase(this, [element]);
};

Telerik.Sitefinity.Web.UI.PublicControls.Designers.TaxonomyControlDesigner.prototype = {
    /* --------------------------------- set up and tear down --------------------------------- */
    initialize: function () {
        if (!this._onLoadDelegate) {
            this._onLoadDelegate = Function.createDelegate(this, this._onLoad);
        }
        if (!this._onUnloadDelegate) {
            this._onUnloadDelegate = Function.createDelegate(this, this._onUnload);
        }

        this._valueUpdatedDelegate = this._valueUpdatedDelegate || Function.createDelegate(this, this._onControlValueUpdated);

        Sys.Application.add_load(this._onLoadDelegate);
        Sys.Application.add_unload(this._onUnloadDelegate);

        Telerik.Sitefinity.Web.UI.PublicControls.Designers.TaxonomyControlDesigner.callBaseMethod(this, 'initialize');
    },

    dispose: function () {
        Telerik.Sitefinity.Web.UI.PublicControls.Designers.TaxonomyControlDesigner.callBaseMethod(this, 'dispose');

        if (this._onLoadDelegate) {
            delete this._onLoadDelegate;
        }
        if (this._onUnloadDelegate) {
            delete this._onUnloadDelegate;
        }
    },

    /* --------------------------------- public methods --------------------------------- */
    // implementation of IDesignerViewControl: Forces the control to refersh from the control Data
    refreshUI: function () {
        var controlData = this.get_controlData();

        this.get_showItemCountCheckBox().checked = controlData.ShowItemCount;
        this.get_showEmptyItemsCheckBox().checked = controlData.ShowEmptyItems;

        jQuery("input[name='NavigationMode']").filter(String.format("[value='{0}']", controlData.RenderAs)).click();
        jQuery("input[name='SortOrder']").filter(String.format("[value='{0}']", controlData.SortOrder)).click();
        jQuery("input[name='ListExpansion']").filter(String.format("[value='{0}']", controlData.HierarchicalExpansion)).click();

        if (!controlData.IsHierarchicalTaxonomy) {
            jQuery(".sfHierarchicalOnly").remove();
        }

        this._refreshing = false;
    },

    // implementation of IDesignerViewControl: forces the designer view to apply the changes on UI to the control Data
    applyChanges: function () {
        var controlData = this.get_controlData();

        controlData.ShowItemCount = this.get_showItemCountCheckBox().checked;
        controlData.ShowEmptyItems = this.get_showEmptyItemsCheckBox().checked;

        controlData.RenderAs = jQuery("input[name='NavigationMode']:checked").val();
        controlData.SortOrder = jQuery("input[name='SortOrder']:checked").val();
        controlData.HierarchicalExpansion = jQuery("input[name='ListExpansion']:checked").val();

        // TODO: implement
    },

    // gets the javascript control object that is being designed
    get_controlData: function () {
        return this.get_propertyEditor().get_control();
    },

    /* --------------------------------- event handlers --------------------------------- */
    // this method is executed when the page is ready and all client components have been initialized
    _onLoad: function () {
        this.refreshUI();
    },

    _onUnload: function () {

    },

    _onControlValueUpdated: function (sender, args) {

    }

    /* --------------------------------- private methods --------------------------------- */

    /* --------------------------------- properties --------------------------------- */
};

Telerik.Sitefinity.Web.UI.PublicControls.Designers.TaxonomyControlDesigner.registerClass('Telerik.Sitefinity.Web.UI.PublicControls.Designers.TaxonomyControlDesigner', Telerik.Sitefinity.Web.UI.ControlDesign.ControlDesignerBase);

if (typeof (Sys) !== 'undefined') Sys.Application.notifyScriptLoaded();