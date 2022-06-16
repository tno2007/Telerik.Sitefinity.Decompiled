﻿Type.registerNamespace("Telerik.Sitefinity.Multisite.Web.UI");

Telerik.Sitefinity.Multisite.Web.UI.DataSourceProviderSelectorDialog = function (element) {
    Telerik.Sitefinity.Multisite.Web.UI.DataSourceProviderSelectorDialog.initializeBase(this, [element]);

    this._siteDataSourceConfig = null;
    this._site = null;

    this._webServiceUrl = null;
    this._dialogTitle = null;
    this._clientLabelManager = null;

    this._cancelButton = null;
    this._cancelButtonClickDelegate = null;

    this._doneButton = null;
    this._doneButtonClickDelegate = null;

    this._addProviderButton = null;
    this._addProviderButtonClickDelegate = null;

    this._dataSourceCreateProviderDialog = null;
    this._dataSourceCreateProviderDialogCloseDelegate = null;

    this._sourcesGrid = null;
    this._sourcesGridDataBoundDelegate = null;
    this._dataSource = null;
    this._dataSourceChangeDelegate = null;
    this._dataSourceChanging = false;

    this._errorMessageWrapper = null;
    this._errorMessageLabel = null;

    this._searchBox = null;
    this._searchBoxSearchDelegate = null;

    this._selectAllSourcesCheckbox = null;
    this._selectAllSourcesCheckboxClickDeelgate = null;

    this._disableProviderConfirmDialog = null;
    this._allowMultipleProviders = null;

    this.usersDataSourceMode = false;
};

Telerik.Sitefinity.Multisite.Web.UI.DataSourceProviderSelectorDialog.prototype = {
    initialize: function () {
        Telerik.Sitefinity.Multisite.Web.UI.DataSourceProviderSelectorDialog.callBaseMethod(this, "initialize");

        this._cancelButtonClickDelegate = Function.createDelegate(this, this._cancelButtonClickHandler);
        $addHandler(this.get_cancelButton(), "click", this._cancelButtonClickDelegate);

        this._doneButtonClickDelegate = Function.createDelegate(this, this._doneButtonClickHandler);
        $addHandler(this.get_doneButton(), "click", this._doneButtonClickDelegate);

        this._addProviderButtonClickDelegate = Function.createDelegate(this, this._addProviderButtonClickHandler);
        $addHandler(this.get_addProviderButton(), "click", this._addProviderButtonClickDelegate);

        this._dataSourceChangeDelegate = Function.createDelegate(this, this._dataSourceChangeHandler);

        this._searchBoxSearchDelegate = Function.createDelegate(this, this._searchBoxSearchHandler);
        this.get_searchBox().add_search(this._searchBoxSearchDelegate);

        this._sourcesGridDataBoundDelegate = Function.createDelegate(this, this._sourcesGridDataBoundHandler);

        this._selectAllSourcesCheckboxClickDelegate = Function.createDelegate(this, this._selectAllSourcesCheckboxClickHandler);
        $addHandler(this.get_selectAllSourcesCheckbox(), "click", this._selectAllSourcesCheckboxClickDelegate);

        this._dataSourceCreateProviderDialogCloseDelegate = Function.createDelegate(this, this._dataSourceCreateProviderDialogClosed);
        this.get_dataSourceCreateProviderDialog().add_close(this._dataSourceCreateProviderDialogCloseDelegate);

        this._initializeGrid();
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

        if (this._addProviderButtonClickDelegate) {
            if (this.get_addProviderButton()) {
                $removeHandler(this.get_addProviderButton(), "click", this._addProviderButtonClickDelegate);
            }
            delete this._addProviderButtonClickDelegate;
        }

        if (this._dataSourceChangeDelegate) {
            delete this._dataSourceChangeDelegate;
        }

        if (this._searchBoxSearchDelegate) {
            if (this.get_searchBox()) {
                this.get_searchBox().remove_search(this._searchBoxSearchDelegate);
            }
            delete this._searchBoxSearchDelegate;
        }

        if (this._sourcesGridDataBoundDelegate) {
            delete this._sourcesGridDataBoundDelegate;
        }

        if (this._selectAllSourcesCheckboxClickDelegate) {
            if (this.get_selectAllSourcesCheckbox()) {
                $removeHandler(this.get_selectAllSourcesCheckbox(), "click", this._selectAllSourcesCheckboxClickDelegate);
            }
            delete this._selectAllSourcesCheckboxClickDelegate;
        }


        if (this._dataSourceCreateProviderDialogCloseDelegate) {
            if (this.get_dataSourceCreateProviderDialog()) {
                this.get_dataSourceCreateProviderDialog().remove_close(this._dataSourceCreateProviderDialogCloseDelegate);
            }
            delete this._dataSourceCreateProviderDialogCloseDelegate;
        }

        Telerik.Sitefinity.Multisite.Web.UI.DataSourceProviderSelectorDialog.callBaseMethod(this, "dispose");
    },

    /* *************************** public methods *************************** */

    reset: function () {
        this._siteDataSourceConfig = null;
        this._site = null;

        this._dataSourceChanging = false;
        this._dataSource.data([]);
        this.get_searchBox().clearSearchBox();

        this._hideError();
    },

    show: function (site, siteDataSourceConfig) {
        this.reset();

        this.usersDataSourceMode = siteDataSourceConfig.Title == "Users";

        this._siteDataSourceConfig = siteDataSourceConfig;
        this._site = site;
        this._dataSource.transport.options.read.url = this.get_webServiceUrl() + this._site.Id + "/" + encodeURIComponent(this._siteDataSourceConfig.Name) + "/availablelinks/";
        this._dataSource.read();

        this._updateLabelsAndMessages();

        this._allowMultipleProviders = siteDataSourceConfig.AllowMultipleProviders;

        jQuery(this.get_kendoWindow().wrapper).width(510);

        Telerik.Sitefinity.Multisite.Web.UI.DataSourceProviderSelectorDialog.callBaseMethod(this, "show");
    },

    /* *************************** private methods *************************** */

    _updateLabelsAndMessages: function () {
        var titleText = String.format(this.get_clientLabelManager().getLabel("MultisiteResources", "ThisSiteCanUseModuleFrom"), this._siteDataSourceConfig.Title);
        var providerButtonText = this.get_clientLabelManager().getLabel("MultisiteResources", "CreateNewSource");
        var sourceGridColumn1Title = this.get_clientLabelManager().getLabel("MultisiteResources", "Name");
        var sourceGridColumn3Title = this.get_clientLabelManager().getLabel("MultisiteResources", "UsedAlsoBy");
        if (this.usersDataSourceMode) {
            titleText = this.get_clientLabelManager().getLabel("MultisiteResources", "SelectUserGroups");
            providerButtonText = this.get_clientLabelManager().getLabel("MultisiteResources", "CreateNewUserGroup");
            sourceGridColumn1Title = this.get_clientLabelManager().getLabel("MultisiteResources", "UserGroup");
            sourceGridColumn3Title = this.get_clientLabelManager().getLabel("MultisiteResources", "AccessTo");
        }

        jQuery(this.get_dialogTitle()).html(titleText);
        jQuery(this.get_sourcesGrid()).find("th[data-index='1']").text(sourceGridColumn1Title);
        jQuery(this.get_sourcesGrid()).find("th[data-index='3']").text(sourceGridColumn3Title);
        this.get_addProviderButton().text = providerButtonText;
    },

    _ensureDefaultProvider: function () {
        var data = this._dataSource.data();

        if (data.length === 0)
            return;

        for (var i = 0; i < data.length; i++) {
            if (data[i].Link.IsDefault) {
                return;
            }
        }

        for (var j = 0; j < data.length; j++) {
            var jCheckbox = jQuery("#" + this._providerCheckboxId(data[j].Link.ProviderName));
            if (jCheckbox.is(":checked")) {
                data[j].Link.IsDefault = true;
                return;
            }
        }
    },

    _updateDefaultProviderDisplay: function () {
        var data = this._dataSource.data();

        for (var i = 0; i < data.length; i++) {
            var defaultLabel = jQuery("#" + this._providerDefaultLabelId(data[i].Link.ProviderName));
            var mkDefaultLink = jQuery("#" + this._providerMakeDefaultLinkId(data[i].Link.ProviderName));

            if (data[i].Link.IsDefault) {
                defaultLabel.show();
                mkDefaultLink.hide();
                if (this._allowMultipleProviders === false) {
                    jQuery("#" + this._providerRowId(data[i].Link.ProviderName)).removeClass('sfProviderSelected').addClass('sfProviderSelected');
                }
            }
            else {
                defaultLabel.hide();
                var jCheckbox = jQuery("#" + this._providerCheckboxId(data[i].Link.ProviderName));
                if (this._allowMultipleProviders === true) {
                    if (jCheckbox.is(":checked")) {
                        mkDefaultLink.show();
                    }
                    else {
                        if (data[i].LinkSelected) {
                            mkDefaultLink.show();
                        }
                        else {
                            mkDefaultLink.hide();
                        }
                    }

                    data[i].LinkSelected = jCheckbox.is(":checked");
                }
                else {
                    mkDefaultLink.hide();
                }

                if (this._allowMultipleProviders === false) {
                    jQuery("#" + this._providerRowId(data[i].Link.ProviderName)).removeClass('sfProviderSelected');
                }
            }
        }
    },

    _selectAllSourcesCheckboxClickHandler: function (sender, args) {
        jQuery("input[id^='" + this.get_id() + "_providerChkBox_']").attr('checked', this.get_selectAllSourcesCheckbox().checked);

        if (!this.get_selectAllSourcesCheckbox().checked) {
            var data = this._dataSource.data();
            for (var i = 0; i < data.length; i++) {
                data[i].Link.IsDefault = false;
            }
        }

        this._ensureDefaultProvider();
        this._updateDefaultProviderDisplay();
    },

    _sourcesGridDataBoundHandler: function (e) {
        var data = this._dataSource.data();

        if (data) {
            for (var i = 0; i < data.length; i++) {
                var that = this;

                var checkbox = jQuery("#" + this._providerCheckboxId(data[i].Link.ProviderName));
                var mkDefaultLink = jQuery("#" + this._providerMakeDefaultLinkId(data[i].Link.ProviderName));

                if (this._allowMultipleProviders === true) {
                    jQuery(this.get_selectAllSourcesCheckbox()).show();
                    // unbind click that is defined when _allowMultipleProviders is set to false 
                    jQuery('.sfSelectorGridWrapper table tr').unbind('click');

                    var checkboxClick = function (senderChkBox, dataIdx) {
                        return function () {
                            that.get_selectAllSourcesCheckbox().checked = jQuery("input[id^='" + that.get_id() + "_providerChkBox_']:checked").length === data.length;

                            if (!senderChkBox.is(":checked")) {
                                data[dataIdx].Link.IsDefault = false;
                            }

                            if (data[dataIdx].LinkSelected) {
                                data[dataIdx].LinkSelected = senderChkBox.is(":checked");
                            }

                            var selectedLinks = that._selectedLinks();
                            if (selectedLinks.length) {
                                that._hideError();
                            }

                            that._ensureDefaultProvider();
                            that._updateDefaultProviderDisplay();
                        };
                    }(checkbox, i);
                    checkbox.click(checkboxClick);


                    var mkDefaultLinkClick = function (dataIdx) {
                        return function () {
                            for (var j = 0; j < data.length; j++) {
                                data[j].Link.IsDefault = false;
                            }
                            data[dataIdx].Link.IsDefault = true;
                            that._updateDefaultProviderDisplay();
                        };
                    }(i);
                    mkDefaultLink.click(mkDefaultLinkClick);

                }
                else {
                    jQuery(this.get_selectAllSourcesCheckbox()).hide();
                    checkbox.hide();
                    mkDefaultLink.hide();

                    $('.sfSelectorGridWrapper table tr').unbind('click').click(function (event) {
                        if (event.currentTarget.tagName !== "TR") { return; }
                        event.stopPropagation();
                        event.preventDefault();
                        for (var k = 0; k < data.length; k++) {
                            data[k].Link.IsDefault = false;
                        }

                        var element = event.target;
                        while (element.tagName !== 'TR' && element.parentNode) {
                            element = element.parentNode;
                        }

                        data[element.rowIndex - 1].Link.IsDefault = true;

                        that._updateDefaultProviderDisplay();
                    });
                }

                var usedAlsoByItems = [];
                if (data[i].Link.IsGlobalProvider) {
                    usedAlsoByItems.push(this.get_clientLabelManager().getLabel("MultisiteResources", "SitesAll"));
                } else {
                    for (var t = 0; t < data[i].UsedAlsoBy.length; t++) {
                        usedAlsoByItems.push(data[i].UsedAlsoBy[t].htmlEncode());
                    }
                } 

                jQuery("#" + this._providerUsageCellId(data[i].Link.ProviderName)).expandableLabel({
                    items: usedAlsoByItems,
                    maxCharSize: 22,
                    moreText: this.get_clientLabelManager().getLabel("MultisiteResources", "MoreText"),
                    lessText: this.get_clientLabelManager().getLabel("MultisiteResources", "LessText")
                });

                var deleteLink = jQuery("#" + this._providerDisableLinkId(data[i].Link.ProviderName));
                var providerRow = deleteLink.parents("tr:first");
                if (data[i].IsDeletable) {
                    deleteLink.show();

                    var deleteLinkClick = function (data, dataIdx, row) {
                        return function () {
                            that._disableProvider(data, data[dataIdx], row);
                        };
                    }(data, i, providerRow);

                    deleteLink.click(deleteLinkClick);
                }
                else
                    deleteLink.hide();
            }

            if (this._allowMultipleProviders === true) {
                this.get_selectAllSourcesCheckbox().checked = jQuery("input[id^='" + this.get_id() + "_providerChkBox_']:checked").length === data.length;
            }
            this._updateDefaultProviderDisplay();
        }
    },

    _disableProvider: function (data, providerData, providerRow) {
        if (providerData.Draft === true) {
            this._removeProviderRow(data, providerData, providerRow);
            return;
        }

        var that = this;
        var dataSources = data;
        var serviceUrl = this.get_webServiceUrl();
        if (serviceUrl[serviceUrl.length - 1] !== "/")
            serviceUrl += "/";
        var managerName = this._siteDataSourceConfig.Name;
        var promptCallback = function (sender, args) {
            if (args.get_commandName() === "disableProvider") {
                var data = {};
                $.ajax({
                    type: 'PUT',
                    url: serviceUrl + "disableprovider/?managerName=" + managerName + "&providerName=" + providerData.Link.ProviderName,
                    converters: {
                        "text json": function (data) {
                            return Sys.Serialization.JavaScriptSerializer.deserialize(data);
                        }
                    },
                    contentType: "application/json",
                    processData: false,
                    data: Telerik.Sitefinity.JSON.stringify(data),
                    success: function (result, args) {
                        if (result) {
                            that._removeProviderRow(dataSources, providerData, providerRow);
                        }
                    },
                    error: function (jqXHR, textStatus, errorThrown) {
                        alert(Telerik.Sitefinity.JSON.parse(jqXHR.responseText).Detail);
                    }
                });
            }
        };
        this.get_disableProviderConfirmDialog().show_prompt(null, null, promptCallback);
    },

    _searchBoxSearchHandler: function (sender, args) {
        var data = this._dataSource.data();

        if (data) {
            var query = args.get_query();
            if (query) {
                query = query.toLowerCase();
            }

            for (var i = 0; i < data.length; i++) {
                var jRow = jQuery("#" + this._providerRowId(data[i].Link.ProviderName));
                if (query && data[i].DisplayName.toLowerCase().indexOf(query) === -1) {
                    jRow.hide();
                }
                else {
                    jRow.show();
                }
            }
        }
    },

    _cancelButtonClickHandler: function (sender, args) {
        this.close();
    },

    _doneButtonClickHandler: function (sender, args) {
        var selectedLinks = this._selectedLinks();

        if (selectedLinks.length) {
            this.close({ dataSource: this._siteDataSourceConfig, links: selectedLinks });
        } else {
            this._showError(this.get_clientLabelManager().getLabel("MultisiteResources", "SourcesAreNotSelectedError"));
        }
    },

    _addProviderButtonClickHandler: function (sender, args) {
        jQuery(this.get_kendoWindow().wrapper).hide();
        this.get_dataSourceCreateProviderDialog().show(this._dataSource, this._siteDataSourceConfig);
    },

    _dataSourceCreateProviderDialogClosed: function (sender, args) {
        if (args._data && args._data.providerName) {
            var providerTitle = args._data.providerName;
            var dataSource = this._dataSource;
            var data = dataSource.data();

            var isDefault = true;
            for (var i = 0; i < data.length; i++) {
                if (data[i].Link.IsDefault) {
                    isDefault = false;
                    break;
                }
            }

            var newSourceLink = Telerik.Sitefinity.cloneObject(this._siteDataSourceConfig.SampleLink);
            newSourceLink.ProviderName = "new-sf-provider-" + new Date().getTime();
            newSourceLink.ProviderTitle = providerTitle;
            newSourceLink.IsDefault = isDefault;

            data.splice(0, 0, {
                IsDeletable: true,
                Link: newSourceLink,
                UsedAlsoBy: [],
                DisplayName: providerTitle,
                LinkSelected: true,
                Draft: true
            });
        }

        jQuery(this.get_kendoWindow().wrapper).show();
    },

    _selectedLinks: function () {
        var data = this._dataSource.data();
        var selectedLinks = [];
        for (var i = 0; i < data.length; i++) {
            if (this._allowMultipleProviders) {
                var jCheckbox = jQuery("#" + this._providerCheckboxId(data[i].Link.ProviderName));
                if (jCheckbox.is(":checked")) {
                    selectedLinks.push(data[i].Link);
                }
            } else {
                if (data[i].Link.IsDefault === true) {
                    selectedLinks.push(data[i].Link);
                }
            }
        }

        return selectedLinks;
    },

    _initializeGrid: function () {
        this._dataSource = this._getDataSource();
        this._bindSourceGrid(this._dataSource);
    },

    _getDataSource: function () {
        return new kendo.data.DataSource({
            type: "json",
            transport: {
                read: {
                    contentType: 'application/json; charset=utf-8',
                    type: "GET",
                    dataType: "json"
                }
            },
            schema: {
                data: "Items"
            },
            change: this._dataSourceChangeDelegate
        });
    },

    _bindSourceGrid: function (dataSource) {
        jQuery(this.get_sourcesGrid()).kendoGrid({
            dataSource: dataSource,
            rowTemplate: jQuery.proxy(kendo.template(jQuery("#sourcesGridRowTemplate").html()), this),
            autoBind: false,
            dataBound: this._sourcesGridDataBoundDelegate,
            scrollable: false
        });
    },

    _removeProviderRow: function (data, providerData, providerRow) {
        this._removeByAttr(data, "DisplayName", providerData.DisplayName);
        providerRow.remove();

        this._ensureDefaultProvider();
        this._updateDefaultProviderDisplay();
    },

    _providerCheckboxId: function (providerName) {
        return this.get_id() + "_providerChkBox_" + providerName.replace(/(\.|\s)/g, "_");
    },

    _providerRowId: function (providerName) {
        return this.get_id() + "_providerRow_" + providerName.replace(/(\.|\s)/g, "_");
    },

    _providerUsageCellId: function (providerName) {
        return this.get_id() + "_providerUsageCell_" + providerName.replace(/(\.|\s)/g, "_");
    },

    _providerDisableLinkId: function (providerName) {
        return this.get_id() + "_providerDeleteCell_" + providerName.replace(/(\.|\s)/g, "_");
    },

    _providerDefaultLabelId: function (providerName) {
        return this.get_id() + "_providerDefLbl_" + providerName.replace(/(\.|\s)/g, "_");
    },

    _providerMakeDefaultLinkId: function (providerName) {
        return this.get_id() + "_providerMkDefLnk_" + providerName.replace(/(\.|\s)/g, "_");
    },

    _dataSourceChangeHandler: function (e) {
        if (this._dataSourceChanging === true || !this._site)
            return;
        this._dataSourceChanging = true;

        var data = e.sender.data().slice(0);

        for (var t = 0; t < this._siteDataSourceConfig.Links.length; t++) {
            var availableLink = this._getAvailableDataSourceLink(data, this._siteDataSourceConfig.Links[t].ProviderName);
            if (!availableLink) {
                data.splice(0, 0, { Link: this._siteDataSourceConfig.Links[t], UsedAlsoBy: [] });
            }
        }

        var selectedLinksCount = 0;
        for (var i = 0; i < data.length; i++) {
            data[i].DisplayName = data[i].Link.ProviderTitle ? data[i].Link.ProviderTitle : data[i].Link.ProviderName;
            data[i].IsGlobalProvider = data[i].Link.IsGlobalProvider;

            if (typeof data[i].LinkSelected === 'undefined' || data[i].Draft === false) {
                var existingLink = this._getDataSourceLink(data[i].Link.ProviderName);
                if (existingLink) {
                    data[i].LinkSelected = true;
                    if (existingLink.IsDefault) {
                        for (var j = 0; j < data.length; j++) {
                            data[j].Link.IsDefault = i === j;
                        }
                    }
                }
                else {
                    data[i].LinkSelected = false;
                }
            }

            if (data[i].LinkSelected) {
                selectedLinksCount++;
            }
        }

        if (selectedLinksCount === 0 && this._siteDataSourceConfig.GhostLink) {
            var ghost = Telerik.Sitefinity.cloneObject(this._siteDataSourceConfig.GhostLink);
            ghost.IsDefault = false;
            data.splice(0, 0, {
                LinkSelected: false,
                DisplayName: ghost.ProviderTitle ? ghost.ProviderTitle : ghost.ProviderName,
                Link: ghost,
                UsedAlsoBy: []
            });
        }

        data.sort(function (a, b) {
            return a.DisplayName.toLowerCase().localeCompare(b.DisplayName.toLowerCase());
        });

        e.sender.data(data);
        this._dataSourceChanging = false;
    },

    _getAvailableDataSourceLink: function (data, providerName) {
        for (var i = 0; i < data.length; i++) {
            var current = data[i].Link;
            if (current.ProviderName === providerName) {
                return current;
            }
        }
        return null;
    },

    _getDataSourceLink: function (providerName) {
        for (var i = 0; i < this._siteDataSourceConfig.Links.length; i++) {
            var current = this._siteDataSourceConfig.Links[i];
            if (current.ProviderName === providerName) {
                return current;
            }
        }
        return null;
    },

    _removeByAttr: function (arr, attr, value) {
        var i = arr.length;
        while (i--) {
            if (arr[i]
                && arr[i].hasOwnProperty(attr)
                && (arguments.length > 2 && arr[i][attr] === value)) {

                arr.splice(i, 1);

            }
        }
        return arr;
    },

    _showError: function (error) {
        $(this.get_errorMessageWrapper()).show();
        $(this.get_errorMessageLabel()).text(error);
        this._resizeToContent();
    },

    _hideError: function () {
        $(this.get_errorMessageWrapper()).hide();
        $(this.get_errorMessageLabel()).text("");
        this._resizeToContent();
    },

    _resizeToContent: function () {
        if (window.dialogBase) {
            dialogBase.resizeToContent();
        }
    },

    /* *************************** properties *************************** */

    get_dialogTitle: function () {
        return this._dialogTitle;
    },
    set_dialogTitle: function (value) {
        this._dialogTitle = value;
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
    get_addProviderButton: function () {
        return this._addProviderButton;
    },
    set_addProviderButton: function (value) {
        this._addProviderButton = value;
    },
    get_clientLabelManager: function () {
        return this._clientLabelManager;
    },
    set_clientLabelManager: function (value) {
        this._clientLabelManager = value;
    },
    get_sourcesGrid: function () {
        return this._sourcesGrid;
    },
    set_sourcesGrid: function (value) {
        this._sourcesGrid = value;
    },
    get_webServiceUrl: function () {
        return this._webServiceUrl;
    },
    set_webServiceUrl: function (value) {
        this._webServiceUrl = value;
    },
    get_searchBox: function () {
        return this._searchBox;
    },
    set_searchBox: function (value) {
        this._searchBox = value;
    },
    get_selectAllSourcesCheckbox: function () {
        return this._selectAllSourcesCheckbox;
    },
    set_selectAllSourcesCheckbox: function (value) {
        this._selectAllSourcesCheckbox = value;
    },

    get_disableProviderConfirmDialog: function () {
        return this._disableProviderConfirmDialog;
    },
    set_disableProviderConfirmDialog: function (value) {
        this._disableProviderConfirmDialog = value;
    },

    get_dataSourceCreateProviderDialog: function () {
        return this._dataSourceCreateProviderDialog;
    },
    set_dataSourceCreateProviderDialog: function (value) {
        this._dataSourceCreateProviderDialog = value;
    },

    get_errorMessageLabel: function () {
        return this._errorMessageLabel;
    },
    set_errorMessageLabel: function (value) {
        this._errorMessageLabel = value;
    },

    get_errorMessageWrapper: function () {
        return this._errorMessageWrapper;
    },
    set_errorMessageWrapper: function (value) {
        this._errorMessageWrapper = value;
    }
};

Telerik.Sitefinity.Multisite.Web.UI.DataSourceProviderSelectorDialog.registerClass('Telerik.Sitefinity.Multisite.Web.UI.DataSourceProviderSelectorDialog', Telerik.Sitefinity.Web.UI.Kendo.KendoWindow);