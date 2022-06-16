Type.registerNamespace("Telerik.Sitefinity.ContentLocations.Web.UI");

Telerik.Sitefinity.ContentLocations.Web.UI.ManageContentLocations = function (element) {
    Telerik.Sitefinity.ContentLocations.Web.UI.ManageContentLocations.initializeBase(this, [element]);

    this._itemTypeKey = null;
    this._providerNameKey = null;
    this._itemType = null;
    this._providerName = null;

    this._isMultilingualSite = null;
    this._isMultiSite = null;

    this._webServiceUrl = null;
    this._dataSource = null;
    this._selectors = null;

    this._grid = null;
    this._contentLanguagesDropDown = null;
    this._contentProviderSelector = null;
    this._backButton = null;
    this._messageControl = null;
    this._labelManager = null;

    this._gridDataBoundDelegate = null;
    this._documentReadyDelegate = null;
    this._beforeGetContentLocationsAjaxCallDelegate = null;
    this._languageChangedDelegate = null;
    this._providerChangedDelegate = null;
    this._backButtonClickDelegate = null;

    this._ajaxFailDelegate = null;
    this._locationPriorityMoveSuccessDelegate = null;

}

Telerik.Sitefinity.ContentLocations.Web.UI.ManageContentLocations.prototype = {

    /* ----------------------------- setup and teardown ----------------------------- */
    initialize: function () {
        Telerik.Sitefinity.ContentLocations.Web.UI.ManageContentLocations.callBaseMethod(this, 'initialize');

        this._documentReadyDelegate = Function.createDelegate(this, this._documentReadyHandler);
        this._gridDataBoundDelegate = Function.createDelegate(this, this._gridDataBoundHandler);
        this._beforeGetContentLocationsAjaxCallDelegate = Function.createDelegate(this, this._beforeGetContentLocationsAjaxCallHandler);
        this._languageChangedDelegate = Function.createDelegate(this, this._languageChangedHandler);
        this._providerChangedDelegate = Function.createDelegate(this, this._providerChangedHandler);
        this._backButtonClickDelegate = Function.createDelegate(this, this._backButtonClickHandler);

        this._ajaxFailDelegate = Function.createDelegate(this, this._ajaxFailHandler);
        this._locationPriorityMoveSuccessDelegate = Function.createDelegate(this, this._locationPriorityMoveSuccessHandler);

        this._selectors = {
            gridViewRowTemplate: "#contentLocationsGridRowTemplate"
        };

        // prevent memory leaks
        $(this).on("unload", function (e) {
            jQuery.event.remove(this);
            jQuery.removeData(this);
        });

        var queryParams = new Telerik.Sitefinity.Web.SitefinityJS.Utility.Querystring();
        this._itemType = queryParams.get(this._itemTypeKey, null);
        this._providerName = queryParams.get(this._providerNameKey, null);

        if (!this._isMultilingualSite) {
            jQuery(this.get_contentLanguagesDropDown()).hide();
        }
        else {
            this.get_contentLanguagesDropDown().add_languageChanged(this._languageChangedDelegate);
        }

        this.get_contentProviderSelector().add_onProviderSelected(this._providerChangedDelegate);
        jQuery(document).ready(this._documentReadyDelegate);
        $addHandler(this.get_backButton(), 'click', this._backButtonClickDelegate);
    },

    dispose: function () {

        if (this._documentReadyDelegate) {
            delete this._documentReadyDelegate;
        }

        if (this._gridDataBoundDelegate) {
            delete this._gridDataBoundDelegate;
        }

        if (this._ajaxFailDelegate) {
            delete this._ajaxFailDelegate;
        }

        if (this._locationPriorityMoveSuccessDelegate) {
            delete this._locationPriorityMoveSuccessDelegate;
        }

        if (this._beforeGetContentLocationsAjaxCallDelegate) {
            delete this._beforeGetContentLocationsAjaxCallDelegate;
        }

        if (this._languageChangedDelegate) {
            if (this.get_contentLanguagesDropDown()) {
                this.get_contentLanguagesDropDown().remove_languageChanged(this._languageChangedDelegate);
            }
            delete this._languageChangedDelegate;
        }

        if (this._providerChangedDelegate) {
            if (this.get_contentProviderSelector()) {
                this.get_contentProviderSelector().remove_onProviderSelected(this._providerChangedDelegate);
            }
            delete this._providerChangedDelegate;
        }

        if (this._backButtonClickDelegate) {
            if (this.get_backButton()) {
                $removeHandler(this.get_backButton(), 'click', this._backButtonClickDelegate);
            }
            delete this._backButtonClickDelegate;
        }

        Telerik.Sitefinity.ContentLocations.Web.UI.ManageContentLocations.callBaseMethod(this, 'dispose');
    },


    // ----------------------------------------- Event handlers ---------------------------------------

    _documentReadyHandler: function () {
        jQuery("body").addClass("sfHasSidebar");
        this._initializeDataSource();
        this.initializeGridView();
        this.showGridView();
    },

    _gridDataBoundHandler: function (e) {
        jQuery(".sfActionsMenu").kendoMenu({ animation: false, openOnClick: true });

        var data = jQuery(this.get_grid()).data("kendoGrid").dataSource.data();
        if (data.length > 0) {
            this._buildMenu(data);
            this.get_messageControl().hide();
            jQuery(this.get_grid()).show();
        }
        else {
            jQuery(this.get_grid()).hide();
            var neutralMessage = this.get_labelManager().getLabel("ContentLocationsResources", "NoLocationsForThisItem");
            this.get_messageControl().showNeutralMessage(neutralMessage);
        }
    },

    _buildMenu: function (data) {
        var that = this;
        for (var i = 0; i < data.length; i++) {
            var dataItem = data[i];
            var anchorTopId = that.get_id() + "_MCLGrid_Top_" + dataItem.Id;
            var anchorUpId = that.get_id() + "_MCLGrid_Up_" + dataItem.Id;
            var anchorDownId = that.get_id() + "_MCLGrid_Down_" + dataItem.Id;
            var anchorBottomId = that.get_id() + "_MCLGrid_Bottom_" + dataItem.Id;

            if (data.length != 1) {
                if (i != 0) {
                    jQuery('#' + anchorTopId).parent().show();
                    jQuery('#' + anchorUpId).parent().show();
                    that._bindAnchorClick(that, dataItem, anchorTopId, Telerik.Sitefinity.ContentLocations.Web.UI.MovingDirection.Top);
                    that._bindAnchorClick(that, dataItem, anchorUpId, Telerik.Sitefinity.ContentLocations.Web.UI.MovingDirection.Up);
                }
                else {
                    jQuery('#' + anchorTopId).parent().hide();
                    jQuery('#' + anchorUpId).parent().hide();
                }

                if (i != data.length - 1) {
                    jQuery('#' + anchorDownId).parent().show();
                    jQuery('#' + anchorBottomId).parent().show();
                    that._bindAnchorClick(that, dataItem, anchorDownId, Telerik.Sitefinity.ContentLocations.Web.UI.MovingDirection.Down);
                    that._bindAnchorClick(that, dataItem, anchorBottomId, Telerik.Sitefinity.ContentLocations.Web.UI.MovingDirection.Bottom);
                }
                else {
                    jQuery('#' + anchorDownId).parent().hide();
                    jQuery('#' + anchorBottomId).parent().hide();
                }
            }
            else {
                jQuery('#' + anchorTopId).parent().hide();
                jQuery('#' + anchorUpId).parent().hide();
                jQuery('#' + anchorDownId).parent().hide();
                jQuery('#' + anchorBottomId).parent().hide();
            }
        }
    },
    _backButtonClickHandler: function (sender, args) {
        history.back();
    },

    _beforeGetContentLocationsAjaxCallHandler: function (request) {
        if (this._isMultilingualSite) {
            var selectedIndex = this.get_contentLanguagesDropDown().get_dropDownList().selectedIndex;
            var selectedValue = this.get_contentLanguagesDropDown().get_dropDownList().options[selectedIndex].value;
            request.setRequestHeader('SF_UI_CULTURE', selectedValue);
        }
    },

    _languageChangedHandler: function (sender, args) {
        this._rebindGrid();
    },

    _providerChangedHandler: function (sender, args) {
        this._providerName = args.ProviderName;
        this._rebindGrid();
    },

    _rebindGrid: function () {
        if (jQuery(this.get_grid()).data("kendoGrid") && jQuery(this.get_grid()).data("kendoGrid").dataSource) {
            jQuery(this.get_grid()).data("kendoGrid").dataSource.read();
        }
    },

    _changeDataSourceHandler: function () {
        console.log('change ds');
    },

    _dataSourceRequestStartHandler: function () {
        console.log('start request');
    },

    _dataSourceErrorHandler: function (jqXHR) {
        console.log('error handler');
        //alert('Failed to load data');
    },

    _locationPriorityMoveSuccessHandler: function (result, args) {
        this._rebindGrid();
    },

    _ajaxFailHandler: function (jqXHR, textStatus, errorThrown) {
        //this.get_messageControl().showNegativeMessage(Telerik.Sitefinity.JSON.parse(jqXHR.responseText).Detail);
    },
    // ----------------------------------------- Private methods ---------------------------------------

    _initializeDataSource: function () {
        var that = this;
        this._dataSource = new kendo.data.DataSource({
            transport: {
                read: {
                    url: this.get_webServiceUrl(),
                    contentType: 'application/json; charset=utf-8',
                    type: "GET",
                    dataType: "json",
                    beforeSend: this._beforeGetContentLocationsAjaxCallDelegate
                },

                parameterMap: function (options) {
                    //IE FIX: If this parameter is not passed to the query string IE returns a 304,
                    //because it thinks that the page has not been modified.
                    var dummyParameter = new Date().getTime();

                    var queryObj = {
                        itemType: that._itemType,
                        provider: that._providerName,
                        time: dummyParameter
                    };

                    return queryObj;
                }
            },
            schema: {
                data: "Items",
                total: "TotalCount"
            },

            pageSize: 50,
            serverPaging: true,
            serverFiltering: true,
            error: this._dataSourceErrorHandler
        });
    },

    initializeGridView: function () {
        jQuery(this.get_grid()).kendoGrid({
            dataSource: this._dataSource,
            rowTemplate: jQuery.proxy(kendo.template(jQuery(this.getSelectors().gridViewRowTemplate).html()), this),
            scrollable: false,
            //pageable: { previousNext: false },
            autoBind: false,
            dataBound: this._gridDataBoundDelegate,
            dataBinding: this._onDataBinding
        });
    },

    showGridView: function () {
        jQuery(this.get_grid()).data("kendoGrid").dataSource.read();
        jQuery(this.get_grid()).show();
    },

    getSelectors: function () {
        return this._selectors;
    },

    _changeContentLocationPriority: function () {

    },

    _bindAnchorClick: function (manageContentLocationsItem, dataItem, id, operation) {
        var anch = jQuery("#" + id);
        if (anch) {
            anch.click(function () {
                manageContentLocationsItem._executeOperation(dataItem, operation);
            });
        }
    },

    _executeOperation: function (dataItem, operation) {
        var data = {};
        data.Item = dataItem;
        var self = this;
        //this._setLoadingDim(true);
        var request = this.get_webServiceUrl() + '?id=' + data.Item.Id + '&direction=' + operation;
        jQuery.ajax({
            type: 'PUT',
            url: request,
            contentType: "application/json",
            processData: false,
            beforeSend: this._beforeGetContentLocationsAjaxCallDelegate,
            success: this._locationPriorityMoveSuccessDelegate,
            error: this._ajaxFailDelegate
        });
    },

    _setLoadingDim: function (flag) {
        if (flag) {
            jQuery("body").addClass("sfOverflowHidden");
            //jQuery('<div class="RadAjax"><div class="raDiv">Loading</div><div class="raColor"></div></div><div class="sfWhiteBgDiv"></div>').appendTo("body");
            jQuery("#sf_moduleStatusLoading").show();
        } else {
            jQuery("body").removeClass("sfOverflowHidden");
            jQuery("#sf_moduleStatusLoading").hide();
        }
    },
    // ----------------------------------------- Properties ---------------------------------------

    get_grid: function () {
        return this._grid;
    },
    set_grid: function (value) {
        this._grid = value;
    },

    get_contentLanguagesDropDown: function () {
        return this._contentLanguagesDropDown;
    },
    set_contentLanguagesDropDown: function (value) {
        this._contentLanguagesDropDown = value;
    },

    get_contentProviderSelector: function () {
        return this._contentProviderSelector;
    },
    set_contentProviderSelector: function (value) {
        this._contentProviderSelector = value;
    },

    get_webServiceUrl: function () {
        return this._webServiceUrl;
    },
    set_webServiceUrl: function (value) {
        this._webServiceUrl = value;
    },

    get_backButton: function () {
        return this._backButton;
    },
    set_backButton: function (value) {
        this._backButton = value;
    },

    get_messageControl: function () {
        return this._messageControl;
    },
    set_messageControl: function (value) {
        this._messageControl = value;
    },

    get_labelManager: function () {
        return this._labelManager;
    },
    set_labelManager: function (value) {
        this._labelManager = value;
    }

}

Telerik.Sitefinity.ContentLocations.Web.UI.ManageContentLocations.registerClass('Telerik.Sitefinity.ContentLocations.Web.UI.ManageContentLocations', Sys.UI.Control);
if (typeof (Sys) !== 'undefined') Sys.Application.notifyScriptLoaded();

// ------------------------------------------------------------------------
// Enum MovingDirection
// ------------------------------------------------------------------------

Telerik.Sitefinity.ContentLocations.Web.UI.MovingDirection = function () {
};
Telerik.Sitefinity.ContentLocations.Web.UI.MovingDirection.prototype = {
    Bottom: 0,
    Down: 1,
    Up: 2,
    Top: 3
};
Telerik.Sitefinity.ContentLocations.Web.UI.MovingDirection.registerEnum("Telerik.Sitefinity.ContentLocations.Web.UI.MovingDirection");
