Type.registerNamespace("Telerik.Sitefinity.Modules.Forms.Web.UI");
//HACK this is to aviod the dialogBase is undefined in the DetailsView error.
dialogBase = null;

Telerik.Sitefinity.Modules.Forms.Web.UI.FormsMasterDetailView = function (element) {
    Telerik.Sitefinity.Modules.Forms.Web.UI.FormsMasterDetailView.initializeBase(this, [element]);
    this._splitterBar = null;
    this._detailDialog = null;
    this._detailViewPane = null;
    this._exportServiceUrl = null;
    this._entryPageIndexServiceUrl = null;
    this._formName = null;
    this._itemType = null;
    this._isPublished = null;
    this._isShared = false;
    this._flatSiteSelector = null;

    this._lastUpdatedEntry = null

    this._originalDialogParamaters = null;
    this._selectedSourceSite = null;

    this._jTransportForm = null;
    this._jTransportInput = null;

    this._detailsChangedDelegate = null;
    this._detailViewCommandDelegate = null;
    this._dialogClosedDelegate = null;
    this._entryIdLoadDelegate = null;
    this._masterUpdatedDataBoundDelegate = null;
    this._masterItemsListCommandDelegate = null;
    this._siteSelectorSelectedDelegate = null;
    this._siteSelectorLoadDelegate = null;
}

Telerik.Sitefinity.Modules.Forms.Web.UI.FormsMasterDetailView.prototype =
    {
        initialize: function () {
            this._dialogClosedDelegate = Function.createDelegate(this, this._dialogClosedHandler);
            this._masterViewCommandDelegate = Function.createDelegate(this, this._masterViewCommandHandler);

            this._entryId = this._getQueryStringParam("entryId");
            this._entryIdLoadDelegate = Function.createDelegate(this, this._entryIdLoadHandler);

            if (!this._siteSelectorSelectedDelegate) {
                this._siteSelectorSelectedDelegate = Function.createDelegate(this, this._siteSelectorSelectedHandler);
                this.get_flatSiteSelector().add_onSiteSelected(this._siteSelectorSelectedDelegate);
            }

            if (!this._siteSelectorLoadDelegate) {
                this._siteSelectorLoadDelegate = Function.createDelegate(this, this._siteSelectorLoadHandler);
                this.get_flatSiteSelector().add_onLoad(this._siteSelectorLoadDelegate);
            }

            Telerik.Sitefinity.Modules.Forms.Web.UI.FormsMasterDetailView.callBaseMethod(this, "initialize");
        },

        dispose: function () {
            Telerik.Sitefinity.Modules.Forms.Web.UI.FormsMasterDetailView.callBaseMethod(this, "dispose");
            if (this._detailsViewDetailsChanged) {
                delete this._detailsViewDetailsChanged;
            }
            if (this._dialogClosedDelegate) {
                delete this._dialogClosedDelegate;
            }
            if (this._masterViewCommandDelegate) {
                delete this._masterViewCommandDelegate;
            }
            if (this._entryIdLoadDelegate) {
                delete this._entryIdLoadDelegate;
            }

            if (this._siteSelectorSelectedDelegate) {
                this.get_flatSiteSelector().remove_onSiteSelected(this._siteSelectorSelectedDelegate);
                delete this._siteSelectorSelectedDelegate;
            }

            if (this._siteSelectorLoadDelegate) {
                this.get_flatSiteSelector().remove_onLoad(this._siteSelectorLoadDelegate);
                delete this._siteSelectorLoadDelegate;
            }
        },

        /* --------------------  public methods --------------------  */


        /* -------------------- events -------------------- */

        /* -------------------- event handlers -------------------- */
        _handlePageLoad: function (sender, args) {
            this._detailsChangedDelegate = Function.createDelegate(this, this._detailsViewDetailsChanged);
            this.get_detailView().add_detailsChanged(this._detailsChangedDelegate);
            this._detailViewCommandDelegate = Function.createDelegate(this, this._detailViewCommand);
            this.get_detailView().add_detailViewCommand(this._detailViewCommandDelegate);

            this._itemsGrid = this.get_masterView().get_itemsGrid();

            if (this._itemsGrid) {
                this._itemsGrid.add_dialogClosed(this._dialogClosedDelegate);
                this._itemsGrid.add_command(this._masterViewCommandDelegate);
                this._originialDialogParameters = {
                    create: this._itemsGrid._dialogParameters["create"]
                };
                this._itemsGrid.add_dataBound(this._entryIdLoadDelegate);
            }

            if (this.get_isPublished() == false) {
                var toolbar = this.get_masterView().get_toolbar();
                var createPageWidget = toolbar.getWidgetByName("CreateEntryWidget");
                createPageWidget.set_visible(false);
            }

            Telerik.Sitefinity.Modules.Forms.Web.UI.FormsMasterDetailView.callBaseMethod(this, "_handlePageLoad");
        },

        _masterViewSelectionChanged: function (sender, eventArgs) {
            var selectedItems = this._masterView.get_selectedItems();
            if (selectedItems && selectedItems.length > 0 && this.get_detailViewPane().get_collapsed()) {
                this.get_detailViewPane().expand();
                if (this._lastUpdatedEntry != null) {
                    //TODO localize this and make it with response number;
                    this.get_detailView().get_messageControl().showPositiveMessage('Response edited successfully.');
                    this._lastUpdatedEntry = null;
                }
                else {
                    this.get_detailView().get_messageControl().hide();
                }
            }
            else if ((!selectedItems || selectedItems.length < 1) && !this.get_detailViewPane().get_collapsed()) {
                this.get_detailViewPane().collapse();
            }
            Telerik.Sitefinity.Modules.Forms.Web.UI.FormsMasterDetailView.callBaseMethod(this, "_masterViewSelectionChanged");

        },

        _detailsViewDetailsChanged: function (sender) {
            var masterView = this.get_masterView();
            masterView.get_binder().DataBind();
        },

        _detailViewCommand: function (sender, args) {
            var commandName = args.get_commandName();
            var argument = args.get_commandArgument();
            switch (commandName) {
                case 'edit':
                    var dataItem = argument;
                    this.get_masterView().get_currentItemsList().executeItemCommand('edit', dataItem, dataItem.Id);
                    break;
                default:
                    break;
            }
        },

        _masterViewCommandHandler: function (sender, args) {
            var commandName = args.get_commandName();
            var argument = args.get_commandArgument();
            switch (commandName) {
                case 'export':
                    this._export();
                    break;
                case 'sort':
                    {
                        var sortExpression = args.get_commandArgument();
                        this._masterView.set_currentItemsList(this._masterView.get_itemsGrid());
                        var binder = this._masterView.get_currentItemsList().getBinder();
                        binder.set_sortExpression(sortExpression);
                        binder.DataBind();
                    }
                    break;
                default:
                    break;
            }
        },
        _entryIdLoadHandler: function (sender, args) {
            if (this._entryId && !this._entryIdCalled) {
                this._entryIdCalled = true;
                var providerName = this.get_detailView()._binder._providerName;
                var itemType = this._itemType;
                var sortExpression = this.get_masterView()._binder.get_sortExpression();
                var filterExpression = this.get_masterView()._binder.get_filterExpression();
                var pageSize = this._itemsGrid._binder.GetTableView().get_pageSize();
                var serviceUrl = this._entryPageIndexServiceUrl + "/" + this._formName + "/" + this._entryId;

                var urlParams = {
                    "providerName": providerName,
                    "itemType": itemType,
                    "sortExpression": sortExpression,
                    "filter": filterExpression,
                    "pageSize": pageSize
                };

                this._itemsGrid._binder.get_manager().InvokeGet(serviceUrl, urlParams, [], this._entryIdLoadSuccess, this._entryIdLoadFailure, this);
            }

            if (this._entryId && this._entryIdFound) {
                this._itemsGrid.selectByIds([this._entryId]);
                this._entryId = null;
                this._entryIdFound = false;
            }
        },
        _getQueryStringParam: function (param) {
            return unescape(window.location.search.replace(new RegExp("^(?:.*[&\\?]" + escape(param).replace(/[\.\+\*]/g, "\\$&") + "(?:\\=([^&]*))?)?.*$", "i"), "$1"))
        },
        _entryIdLoadSuccess: function (sender, result) {
            var pageIndex = parseInt(result);
            // If the page index is -1 the entry response has not been found
            if (pageIndex > -1) {
                // There is no need to set the page index when its value is 0 
                // since it is set by default during the initial data bound event
                if (pageIndex > 0) {
                    this.Caller._itemsGrid._binder.GetTableView().set_currentPageIndex(pageIndex);
                    this.Caller._entryIdFound = true;

                } else {
                    this.Caller._itemsGrid.selectByIds([this.Caller._entryId]);
                    this.Caller._entryId = null;
                }
            } else {
                alert("Item not found");
            }
        },
        _entryIdLoadFailure: function (result) {
            alert(result);
        },
        _export: function () {
            var itemsGrid = this._masterView.get_currentItemsList();
            var binder = itemsGrid.getBinder();
            var selectedItems = binder.get_selectedItems();
            var selectedItemsLenght = selectedItems.length;
            var idsToExport = [];
            while (selectedItemsLenght--) {
                idsToExport.push(selectedItems[selectedItemsLenght].Id);
            }
            var url = this._exportServiceUrl + '?exportAs=xlsx&formName=' + this._formName + '&itemType=' + this._itemType;

            this._invokeDownloadFormSubmit(url, idsToExport);
        },

        _invokeDownloadFormSubmit: function (url, idsToExport) {
            var dataToExport = idsToExport.join(",");
            this._getConfiguredJTrasportForm(dataToExport, url).submit();
        },

        _getConfiguredJTrasportForm: function (valueToSubmit, url) {
            if (this._jTransportForm == null) {
                this._jTransportForm = this._createTransportForm(url);
                this._jTransportInput = jQuery("<input type='text' name='entriesIds'/>");
                this._jTransportInput.appendTo(this._jTransportForm);
            }

            this._jTransportInput.val(valueToSubmit);
            return this._jTransportForm;
        },

        _createTransportForm: function (url) {
            var transportForm = jQuery("<form method='POST' action ='" + url + "'/>");
            // IE has issues with the iframe and form.
            // the IFrame is created with attributes inside the html string because of a IE bug with iframe's name and/or id attribute.
            //var iframe = jQuery("<iframe src='javascript:false;' id='transportIframe' name='transportIframe'/>");

            //transportForm.appendTo(iframe);
            transportForm.hide().appendTo('body');
            return transportForm;
        },

        _masterUpdatedDataBoundHandler: function (sender, args) {
            this.get_masterView().get_currentItemsList().remove_dataBound(this._masterUpdatedDataBoundDelegate);
            delete this._masterUpdatedDataBoundDelegate;
            if (this._lastUpdatedEntry) {
                this.get_masterView().get_currentItemsList().selectByIds([this._lastUpdatedEntry.Id]);
            }
        },

        _siteSelectorSelectedHandler: function (sender, site) {
            this._selectedSourceSite = site;
            if (this._itemsGrid && this._originialDialogParameters) {
                if (this._selectedSourceSite) {
                    this._itemsGrid._dialogParameters["create"] = this._originialDialogParameters["create"] + "&siteId=" + this._selectedSourceSite.Id + "&siteName=" + this._selectedSourceSite.Name;
                }
                else {
                    this._itemsGrid._dialogParameters["create"] = this._originialDialogParameters["create"]
                }
            }

            var gridBinder = this._masterView.get_itemsGrid().getBinder();
            if (gridBinder.get_serviceBaseUrl().indexOf("FormsService.svc/entries") > -1) {
                var urlParams = [];
                if (this._selectedSourceSite) {
                    urlParams["siteId"] = this._selectedSourceSite.Id;
                }
                gridBinder.set_urlParams(urlParams);
                gridBinder.DataBind();
            }
        },

        _siteSelectorLoadHandler: function (sender) {
            if (!this._isShared) {
                jQuery("body").removeClass("sfHasSiteSelector");
            }
        },

        /* -------------------- private methods -------------------- */

        _dialogClosedHandler: function (sender, args) {
            this._lastUpdatedEntry = null;
            if (args.get_isUpdated()) {
                this._lastUpdatedEntry = args.get_dataItem();
                this._masterUpdatedDataBoundDelegate = Function.createDelegate(this, this._masterUpdatedDataBoundHandler);
                this.get_masterView().get_currentItemsList().add_dataBound(this._masterUpdatedDataBoundDelegate);
            }
        },

        /* -------------------- properties -------------------- */

        get_isPublished: function () {
            return this._isPublished;
        },

        set_isPublished: function (value) {
            if (value !== this._isPublished) {
                this._isPublished = value;
            }
        },

        get_splitterBar: function () {
            return this._splitterBar;
        },

        set_splitterBar: function (value) {
            if (value !== this._splitterBar) {
                this._splitterBar = value;
            }
        },
        get_detailViewPane: function () {
            return this._detailViewPane;
        },

        set_detailViewPane: function (value) {
            if (value !== this._detailViewPane) {
                this._detailViewPane = value;
            }
        },

        get_detailDialog: function () {
            return this._detailDialog;
        },

        set_detailDialog: function (value) {
            if (value !== this._detailViewPane) {
                this._detailDialog = value;
            }
        },

        get_flatSiteSelector: function () {
            return this._flatSiteSelector;
        },

        set_flatSiteSelector: function (value) {
            this._flatSiteSelector = value;
        }
    };

Telerik.Sitefinity.Modules.Forms.Web.UI.FormsMasterDetailView.registerClass("Telerik.Sitefinity.Modules.Forms.Web.UI.FormsMasterDetailView", Telerik.Sitefinity.Web.UI.ContentUI.Views.Backend.MasterDetailView);

