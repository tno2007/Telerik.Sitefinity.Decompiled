﻿/// <reference name="MicrosoftAjax.js"/>
/// <reference name="Telerik.Sitefinity.Resources.Scripts.jquery-1.6.3-vsdoc.js" assembly="Telerik.Sitefinity.Resources"/>
Type.registerNamespace("Telerik.Sitefinity.Web.UI.Fields");

Telerik.Sitefinity.Web.UI.Fields.RelatingDataField = function (element) {
    Telerik.Sitefinity.Web.UI.Fields.RelatingDataField.initializeBase(this, [element]);

    this._siteBaseUrl = null;
    this._relatedDataService = null;
    this._genericDataService = null;

    this._element = element;
    this._container = null;
    this._providerName = null;
    this._itemId = null;
    this._loadingContainer = null;
    this._expanderButton = null;
    this._openDialogButton = null;
    this._itemType = null;
    this._isMultilingual = null;
    this._clientManager = null;
    this._emptyGuid = '00000000-0000-0000-0000-000000000000';
}

Telerik.Sitefinity.Web.UI.Fields.RelatingDataField.prototype =
{
    initialize: function () {
        Telerik.Sitefinity.Web.UI.Fields.RelatingDataField.callBaseMethod(this, "initialize");

        requirejs.config({
            baseUrl: this._siteBaseUrl + "Res",
            paths: {
                RelatingDataGrid: 'Telerik.Sitefinity.RelatedData.Web.UI.Scripts.RelatingDataGrid',
                RelatingDataGridTemplate: 'Telerik.Sitefinity.RelatedData.Web.UI.Templates.RelatingDataGrid.sfhtml',

                ContentSelectorBase: 'Telerik.Sitefinity.RelatedData.Web.UI.Scripts.ContentSelectorBase',
                FlatContentSelector: 'Telerik.Sitefinity.RelatedData.Web.UI.Scripts.FlatContentSelector',
                FlatContentSelectorTemplate: 'Telerik.Sitefinity.RelatedData.Web.UI.Templates.FlatContentSelector.sfhtml',

                DetailViewEditingWindow: 'Telerik.Sitefinity.Web.RequireJSModules.Scripts.DetailViewEditingWindow',
                ContentRepository: 'Telerik.Sitefinity.Web.UI.PublicControls.InlineEditing.Scripts.ContentRepository'
            },
            map: {
                '*': {
                    text: this._siteBaseUrl + "ExtRes/Telerik.Sitefinity.Resources.Scripts.RequireJS.text.js",
                }
            },
            waitSeconds: 0
        });

        this._onRequestEndDelegate = Function.createDelegate(this, this.onRequestEndHandler);

        var that = this;
        this._viewModel = kendo.observable({
            closeDialog: function (e) {
                e.preventDefault();
                that._relatingDataWindow.close();
            },
            toggle: function (e) {
                e.preventDefault();
                that._toggleRelatingDataInfo();
            },
            openDialog: function (e) {
                e.preventDefault();
                that._openRelatingDataWindow();
            }
        });

        var wrapper = $(this._container);
        kendo.bind(wrapper, this._viewModel);
    },

    dispose: function () {
        Telerik.Sitefinity.Web.UI.Fields.RelatingDataField.callBaseMethod(this, "dispose");

        if (this._onRequestEndDelegate) {
            delete this._onRequestEndDelegate;
        }
    },

    reset: function () {
        Telerik.Sitefinity.Web.UI.Fields.RelatingDataField.callBaseMethod(this, "reset");

        //Hide expandable dialog and it's child elements
        jQuery(this.get_openDialogButton()).hide();
        jQuery(this._element).find("#noItemsMessage").hide();
        jQuery(this._element).find("#expandableContainer").hide();
        jQuery(this.get_container()).removeClass("sfExpandedForm");
        this.RelatingDataGrid = null;
    },

    getParameterMap: function () {
        return {
            ChildItemId: this._itemId,
            ChildItemType: this._itemType,
            ChildProviderName: this._providerName
        }
    },

    onRequestEndHandler: function (e) {
        //Hide loading container
        jQuery(this.get_loadingContainer()).hide();

        this._itemsCountChanged(e.response.TotalCount);
    },

    dataBind: function (dataItem, itemType, provider) {
        if (dataItem.Id && dataItem.Id !== this._emptyGuid) {
            jQuery(this._element).show();
            this._itemId = dataItem.Id;
            if (dataItem.OriginalContentId && dataItem.OriginalContentId != this._emptyGuid) {
                this._itemId = dataItem.OriginalContentId;
            }
        }
        else {
            jQuery(this._element).hide();
        }

        this._itemType = itemType;
        this._providerName = provider;

        //Reset the field when the data context has changed
        this.reset();
    },

    _itemsCountChanged: function (count) {
        if (count > 0) {
            this._relatingDataWindow.element.find("#relatingDataTitle").text(String.format(this.get_clientLabelManager().getLabel('ModuleEditorResources', 'RelatingItemsDialogTitle'), count));
            jQuery(this.get_openDialogButton()).text(String.format(this.get_clientLabelManager().getLabel('ModuleEditorResources', 'ViewItemsLinkText'), count));
            jQuery(this.get_openDialogButton()).show();
            jQuery(this._element).find("#noItemsMessage").hide();
        } else {
            jQuery(this.get_openDialogButton()).hide();
            jQuery(this._element).find("#noItemsMessage").show();
        }
    },

    _toggleRelatingDataInfo: function () {
        var jQueryElement = jQuery(this._element).find("#expandableContainer");
        var state = jQueryElement.css("display");
        if (state == "none") {
            jQueryElement.show();
            jQuery(this.get_container()).addClass("sfExpandedForm");
            this._bindRelatingDataGrid();
        }
        else {
            jQueryElement.hide();
            jQuery(this.get_container()).removeClass("sfExpandedForm");
        }
    },

    _bindRelatingDataGrid: function () {
        var that = this;
        if (that._relatingDataWindow == null) {
            that._relatingDataWindow = $(that._element).find("#relatingDataWindow").kendoWindow({
                actions: [],
                resizable: false,
                modal: true,
                animation: false
            }).addClass("sfSelectorDialog").data("kendoWindow");
            that._relatingDataWindow.center();
        }

        if (!that.RelatingDataGrid) {
            //Show loading container
            jQuery(this.get_loadingContainer()).show();

            //Hide expandable container elements
            jQuery(this.get_openDialogButton()).hide();
            jQuery(this._element).find("#noItemsMessage").hide();

            require(["RelatingDataGrid"], function (RelatingDataGrid) {
                var parameterMap = that.getParameterMap();
                var settings = {
                    parentElement: that._relatingDataWindow.element.find("#relatingItemsContainer"),
                    parameterMap: parameterMap,
                    serviceUrl: that._relatedDataService + "/parent-items",
                    removeTempItemServiceUrl: that._genericDataService,
                    enableSearch: false,
                    enableMultilingualSearch: false,
                    dataSelectable: false,
                    hideGridHeaderRow: true,
                    isMultilingual: that._isMultilingual,
                    culture: that._uiCulture,
                    allowMultipleSelection: false,
                    pagerConfig: { pageable: false },
                    onRequestEnd: that._onRequestEndDelegate,
                    kendoWindow: that._relatingDataWindow
                };
                that.RelatingDataGrid = new RelatingDataGrid(settings);
                that.RelatingDataGrid.siteBaseUrl = that._siteBaseUrl;
                that.RelatingDataGrid.init();
            });
        }
    },

    _openRelatingDataWindow: function () {
        this.RelatingDataGrid.open();
    },

    /* --------------------  Getters and setters ----------- */

    get_container: function () {
        return this._container;
    },

    set_container: function (value) {
        this._container = value;
    },

    get_loadingContainer: function () {
        return this._loadingContainer;
    },

    set_loadingContainer: function (value) {
        this._loadingContainer = value;
    },

    get_openDialogButton: function () {
        return this._openDialogButton;
    },

    set_openDialogButton: function (value) {
        this._openDialogButton = value;
    },

    get_expanderButton: function () {
        return this._expanderButton;
    },

    set_expanderButton: function (value) {
        this._expanderButton = value;
    },

    get_clientLabelManager: function () {
        return this._clientLabelManager;
    },

    set_clientLabelManager: function (value) {
        this._clientLabelManager = value;
    }
}

Telerik.Sitefinity.Web.UI.Fields.RelatingDataField.registerClass("Telerik.Sitefinity.Web.UI.Fields.RelatingDataField", Telerik.Sitefinity.Web.UI.Fields.FieldControl,
    Telerik.Sitefinity.Web.UI.Fields.IRelatingDataField, Telerik.Sitefinity.Web.UI.Fields.ILocalizableFieldControl);