﻿/// <reference name="MicrosoftAjax.js"/>
/// <reference name="Telerik.Sitefinity.Resources.Scripts.jquery-1.6.3-vsdoc.js" assembly="Telerik.Sitefinity.Resources"/>
Type.registerNamespace("Telerik.Sitefinity.Modules.ResponsiveDesign.Web.FieldControls");

Telerik.Sitefinity.Modules.ResponsiveDesign.Web.FieldControls.NavigationTransformationsField = function (element) {
    Telerik.Sitefinity.Modules.ResponsiveDesign.Web.FieldControls.NavigationTransformationsField.initializeBase(this, [element]);

    this._dataSource = null;
    this._addTransformationButton = null;
    this._navigationTransformationRowTemplate = null;
    this._navigationTransformationsListElement = null;
    this._navigationTransformationsList = null;
    this._transformations = null;
    this._clientLabelManager = null;
    this._addTransformationButtonClickDelegate = null;
}

Telerik.Sitefinity.Modules.ResponsiveDesign.Web.FieldControls.NavigationTransformationsField.prototype =
{
    initialize: function () {       
        Telerik.Sitefinity.Modules.ResponsiveDesign.Web.FieldControls.NavigationTransformationsField.callBaseMethod(this, "initialize");
       
        this._addTransformationButtonClickDelegate = Function.createDelegate(this, this._addTransformationButtonClickHandler);
        $addHandler(this.get_addTransformationButton(), "click", this._addTransformationButtonClickDelegate);
     
        this._dataSource = new kendo.data.DataSource();
        var that = this;
        this._navigationTransformationsList = jQuery(this.get_navigationTransformationsListElement()).kendoListView({
            dataSource: this._dataSource,
            template: jQuery.proxy(kendo.template(this.get_navigationTransformationRowTemplate()), this)
        })
        .delegate(".sfNavClassesSelect", "change", function (e) {
            jQuery(this).parent().find(".sfNavClassesInput").toggle(jQuery(this).val() != "all");
            if (jQuery(this).parent().find(".sfNavClassesInput").length !== 0) {
                jQuery(this).parent().find(".sfNavClassesInput").watermarkField({
                    defaultText: that.get_clientLabelManager().getLabel('Labels', 'DefaultStyleText'),
                    isInline: true
                });
            }
        })
        .delegate(".sfNavDeleteButton", "click", function (e) {
            jQuery(this).parent().remove();
            var data = that.get_value();
            that.set_value(data);
        })
        .data("kendoListView");
        this.set_defaultValue([]);       
    },

    dispose: function () {
        if (this._addTransformationButtonClickDelegate) {
            if (this.get_addTransformationButton()) {
                $removeHandler(this.get_addTransformationButton(), "click", this._addTransformationButtonClickDelegate);
            }
            delete this._addTransformationButtonClickDelegate;
        }

        Telerik.Sitefinity.Modules.ResponsiveDesign.Web.FieldControls.NavigationTransformationsField.callBaseMethod(this, "dispose");
    },

    /* --------------------  public methods ----------- */

    reset: function () {
        if (this._dataSource) {
            this._dataSource.data([]);
        }
    },

    get_value: function () {
        return this._getEnteredData();
    },

    set_value: function (value) {
        if (this._dataSource) {
            this._dataSource.data(value);
        }
    },

    /* ------------------ private methods -------------- */

    _renderTransformationOptions: function (selectedName) {
        var result = "";
        for (var t in this.get_transformations()) {
            var text = this.get_transformations()[t];
            result += String.format("<option {0} value='{1}'>{2}</option>", selectedName == t ? "selected" : "", t, text);
        }
        return result;
    },

    _getEnteredData: function () {
        var data = jQuery(this.get_element()).find(".sfNavTransformationRow").map(function () {
            return {
                CssClasses: jQuery(this).find(".sfNavClassesSelect").val() == "all" ? "" : jQuery(this).find(".sfNavClassesInput").val(),
                TransformationName: jQuery(this).find(".sfTransformationSelect").val()
            };
        }).get();
        return data;
    },

    /* ------------------- event handlers -------------*/

    _addTransformationButtonClickHandler: function (sender, args) {
        var enteredData = this._getEnteredData();
        enteredData.push({
            CssClasses: "",
            TransformationName: "ToDropDown"
        });
        this._dataSource.data(enteredData);      
    },

    /* -------------------- properties ---------------- */

    get_navigationTransformationRowTemplate: function () {
        return jQuery("#navigationTransformationRowTemplate").html();
    },
    get_navigationTransformationsList: function () {
        return this._navigationTransformationsList;
    },
    get_navigationTransformationsListElement: function () {
        return this._navigationTransformationsListElement;
    },
    set_navigationTransformationsListElement: function (value) {
        this._navigationTransformationsListElement = value;
    },
    get_addTransformationButton: function () {
        return this._addTransformationButton;
    },
    set_addTransformationButton: function (value) {
        this._addTransformationButton = value;
    },
    get_transformations: function () {
        return this._transformations;
    },
    set_transformations: function (value) {
        this._transformations = value;
    },
    get_clientLabelManager: function () {
        return this._clientLabelManager;
    },
    set_clientLabelManager: function (value) {
        this._clientLabelManager = value;
    }
};

Telerik.Sitefinity.Modules.ResponsiveDesign.Web.FieldControls.NavigationTransformationsField.registerClass("Telerik.Sitefinity.Modules.ResponsiveDesign.Web.FieldControls.NavigationTransformationsField", Telerik.Sitefinity.Web.UI.Fields.FieldControl);
