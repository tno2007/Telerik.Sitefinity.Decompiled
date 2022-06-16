Type.registerNamespace("Telerik.Sitefinity.Multisite.Web.UI");

Telerik.Sitefinity.Multisite.Web.UI.ShareItemDialog = function (element) {
    Telerik.Sitefinity.Multisite.Web.UI.ShareItemDialog.initializeBase(this, [element]);

    this._cancelButton = null;
    this._cancelButtonClickDelegate = null;

    this._doneButton = null;
    this._doneButtonClickDelegate = null;

    this._getSharedSitesUrl = null;
    this._setSharedSitesUrl = null;

    this._sitesGrid = null;
    this._data = null;

    this._sitesGridDataBoundDelegate = null;

    this._loadingCounter = 0;

    this._buttonsPanel = null;
    this._loadingView = null;
    this._messageControl = null;

    this._ajaxCompletedDelegate = null;
    this._ajaxFailDelegate = null;

    this._searchBox = null;
    this._searchBoxSearchDelegate = null;
}

Telerik.Sitefinity.Multisite.Web.UI.ShareItemDialog.prototype = {
    initialize: function () {
        Telerik.Sitefinity.Multisite.Web.UI.ShareItemDialog.callBaseMethod(this, "initialize");

        this._cancelButtonClickDelegate = Function.createDelegate(this, this._cancelButtonClickHandler);
        $addHandler(this.get_cancelButton(), "click", this._cancelButtonClickDelegate);

        this._doneButtonClickDelegate = Function.createDelegate(this, this._doneButtonClickHandler);
        $addHandler(this.get_doneButton(), "click", this._doneButtonClickDelegate);

        this._sitesGridDataBoundDelegate = Function.createDelegate(this, this._sitesGridDataBoundHandler);
        this._ajaxCompleteDelegate = Function.createDelegate(this, this._ajaxCompleteHandler);
        this._ajaxFailDelegate = Function.createDelegate(this, this._ajaxFailHandler);

        this._searchBoxSearchDelegate = Function.createDelegate(this, this._searchBoxSearchHandler);
        this.get_searchBox().add_search(this._searchBoxSearchDelegate);

        this._loadSites();
    },

    dispose: function () {
        if (this._cancelButtonClickDelegate) {
            if (this.get_cancelButton()) {
                $removeHandler(this.get_cancelButton(), "click", this._cancelButtonClickDelegate);
            }
            delete this._cancelButtonClickDelegate;
        }

        if (this._doneButtonClickDelegate) {
            if (this.get_doneButton()) {
                $removeHandler(this.get_doneButton(), "click", this._doneButtonClickDelegate);
            }
            delete this._doneButtonClickDelegate;
        }

        if (this._sitesGridDataBoundDelegate) {
            delete this._sitesGridDataBoundDelegate;
        }
        if (this._ajaxCompleteDelegate) {
            delete this._ajaxCompleteDelegate;
        }
        if (this._ajaxFailDelegate) {
            delete this._ajaxFailDelegate;
        }

        if (this._searchBoxSearchDelegate) {
            if (this.get_searchBox()) {
                this.get_searchBox().remove_search(this._searchBoxSearchDelegate);
            }
            delete this._searchBoxSearchDelegate;
        }

        Telerik.Sitefinity.Multisite.Web.UI.ShareItemDialog.callBaseMethod(this, "dispose");
    },

    _cancelButtonClickHandler: function (sender, args) {
        this.close();
    },

    _doneButtonClickHandler: function (sender, args) {
        var that = this;
        var doneSuccess = function (data, textStatus, jqXHR) {
            that.closeUpdated();
        };

        this._setLoadingViewVisible(true);
        jQuery.ajax({
            type: 'PUT',
            url: this._setSharedSitesUrl,
            contentType: "application/json",
            processData: false,
            data: Sys.Serialization.JavaScriptSerializer.serialize(this._getSharedSiteIds()),
            success: doneSuccess,
            error: this._ajaxFailDelegate,
            complete: this._ajaxCompleteDelegate
        });
    },

    _getSharedSiteIds: function () {
        var result = [];
        for (var i = 0; i < this._data.length; i++) {
            if (this._data[i].IsShared) {
                result.push(this._data[i].SiteId);
            }
        }
        return result;
    },

    _sitesGridDataBoundHandler: function (sender, args) {
        if (this._data) {
            for (var i = 0; i < this._data.length; i++) {
                var jCheckbox = jQuery("#" + this._siteCheckboxId(this._data[i].SiteId));
                var that = this;
                var checkboxClick = function (checkbox, idx) {
                    return function () {
                        that._data[idx].IsShared = checkbox.is(":checked");
                    };
                } (jCheckbox, i);

                jCheckbox.click(checkboxClick);
            }
        }
    },

    _loadSites: function () {
        var that = this;
        var loadSitesSuccess = function (data, textStatus, jqXHR) {
            if (data.Items) {
                for (var i = 0; i < data.Items.length; i++) {
                    data.Items[i].SiteName = data.Items[i].SiteName.htmlEncode();
                }
            }

            that._data = data.Items;
            that._initializeGrid();
        };

        this._setLoadingViewVisible(true);
        jQuery.ajax({
            type: 'GET',
            url: this._getSharedSitesUrl,
            contentType: "application/json",
            processData: false,
            success: loadSitesSuccess,
            error: this._ajaxFailDelegate,
            complete: this._ajaxCompleteDelegate
        });
    },

    _initializeGrid: function () {
        var dataSource = new kendo.data.DataSource({
            data: this._data
        });

        jQuery(this.get_sitesGrid()).kendoGrid({
            dataSource: dataSource,
            rowTemplate: jQuery.proxy(kendo.template(jQuery("#sitesGridRowTemplate").html()), this),
            autoBind: true,
            dataBound: this._sitesGridDataBoundDelegate,
            scrollable: false
        });

        this.resizeToContent();
    },

    _siteCheckboxId: function (siteId) {
        return this.get_id() + "_siteCheckbox_" + siteId;
    },

    _siteRowId: function (siteId) {
        return this.get_id() + "_siteRow_" + siteId;
    },

    _setLoadingViewVisible: function (loading) {
        if (loading) {
            this._loadingCounter++;
        }
        else {
            if (this._loadingCounter > 0) {
                this._loadingCounter--;
            }
        }
        if (this._loadingCounter > 0) {
            jQuery(this.get_buttonsPanel()).hide();
            jQuery(this.get_loadingView()).show();
        }
        else {
            jQuery(this.get_loadingView()).hide();
            jQuery(this.get_buttonsPanel()).show();
        }
    },

    _ajaxCompleteHandler: function (jqXHR, textStatus) {
        this._setLoadingViewVisible(false);
        dialogBase.resizeToContent();
    },

    _ajaxFailHandler: function (jqXHR, textStatus, errorThrown) {
        this.get_messageControl().showNegativeMessage(Telerik.Sitefinity.JSON.parse(jqXHR.responseText).Detail);
    },

    _searchBoxSearchHandler: function (sender, args) {
        if (this._data) {
            var query = args.get_query();
            if (query) {
                query = query.toLowerCase();
            }

            for (var i = 0; i < this._data.length; i++) {
                var jRow = jQuery("#" + this._siteRowId(this._data[i].SiteId));
                if (query && this._data[i].SiteName.toLowerCase().indexOf(query) === -1) {
                    jRow.hide();
                }
                else {
                    jRow.show();
                }
            }
        }
    },

    get_cancelButton: function () {
        return this._cancelButton;
    },
    set_cancelButton: function (value) {
        this._cancelButton = value;
    },
    get_doneButton: function () {
        return this._doneButton;
    },
    set_doneButton: function (value) {
        this._doneButton = value;
    },
    get_sitesGrid: function () {
        return this._sitesGrid;
    },
    set_sitesGrid: function (value) {
        this._sitesGrid = value;
    },
    get_buttonsPanel: function () {
        return this._buttonsPanel;
    },
    set_buttonsPanel: function (value) {
        this._buttonsPanel = value;
    },
    get_loadingView: function () {
        return this._loadingView;
    },
    set_loadingView: function (value) {
        this._loadingView = value;
    },
    get_messageControl: function () {
        return this._messageControl;
    },
    set_messageControl: function (value) {
        this._messageControl = value;
    },
    get_searchBox: function () {
        return this._searchBox;
    },
    set_searchBox: function (value) {
        this._searchBox = value;
    }
}

Telerik.Sitefinity.Multisite.Web.UI.ShareItemDialog.registerClass("Telerik.Sitefinity.Multisite.Web.UI.ShareItemDialog", Telerik.Sitefinity.Web.UI.AjaxDialogBase);

if (typeof (Sys) !== 'undefined') Sys.Application.notifyScriptLoaded();


