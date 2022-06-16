Type.registerNamespace("Telerik.Sitefinity.Modules.Libraries.Web.UI.Thumbnails");

thumbnailListDialog = null;

Telerik.Sitefinity.Modules.Libraries.Web.UI.Thumbnails.ThumbnailListDialog = function (element) {
    Telerik.Sitefinity.Modules.Libraries.Web.UI.Thumbnails.ThumbnailListDialog.initializeBase(this, [element]);
    this._list = null;
    this._webServiceUrl = null;
    this._clientLabelManager = null;
    this._titleLabel = null;
    this._backLink = null;
    this._embedDialog = null;

    this._selectors = null;
    this._dataSource = null;
    this._providerName = null;
    this._dataItem = null;
    this._language = null;
    this._embedDataItem = null;

    this._listDataBoundDelegate = null;
    this._thumbnailEmbedLoadDelegate = null;
    this._onLoadDelegate = null;
    this._backLinkClickDelegate = null;
}
Telerik.Sitefinity.Modules.Libraries.Web.UI.Thumbnails.ThumbnailListDialog.prototype = {

    initialize: function () {
        Telerik.Sitefinity.Modules.Libraries.Web.UI.Thumbnails.ThumbnailListDialog.callBaseMethod(this, 'initialize');

        thumbnailListDialog = this;
        
        if (this.get_backLink()) {
            this._backLinkClickDelegate = Function.createDelegate(this, this._backLinkClick);
            $addHandler(this.get_backLink(), "click", this._backLinkClickDelegate);            
        }

        this._selectors = {
            listViewRowTemplate: "#thumbnailListRowTemplate"
        };

        // prevent memory leaks
        jQuery(this).on("unload", function (e) {
            jQuery.event.remove(this);
            jQuery.removeData(this);
        });

        this._initDelegates();
    },

    dispose: function () {
        Telerik.Sitefinity.Modules.Libraries.Web.UI.Thumbnails.ThumbnailListDialog.callBaseMethod(this, 'dispose');

        if (this._backLinkClickDelegate) {
            if (this.get_backLink())
                $removeHandler(this.get_backLink(), "click", this._backLinkClickDelegate);
            delete this._backLinkClickDelegate;
        }

        this._removeHandlers();
    },

    /* Events */

    _listDataBoundHandler: function (e) {
        jQuery(".sfActionsMenu").kendoMenu({ animation: false, openOnClick: true });
    },

    _backLinkClick: function () {
        this.close(this);
    },

    /* Public methods */

    createDialog: function (commandName, dataItem, self, dialog, params, key, commandArgument) {
        if (dataItem) {
            var backKey;
            var backResourceClassId;
            var backText;

            if (params) {
                if (params.provider)
                    this._providerName = params.provider;                                                

                if (params.backLabelText && params.backLabelResourceClassId) {
                    backKey = params.backLabelText;
                    backResourceClassId = params.backLabelResourceClassId;
                }                
            }

            if (!this._providerName)
                this._providerName = this.getQueryValue("provider", true);

            if (!backKey)
                backKey = this.getQueryValue("backLabelText");

            if(!backResourceClassId)
                backResourceClassId = this.getQueryValue("backLabelResourceClassId");                            

            backText = String.format(this.get_clientLabelManager().getLabel(backResourceClassId, backKey));
            jQuery(this.get_backLink()).html(backText);

            this._dataItem = dataItem;
            this._language = commandArgument.language;
            var titleText;

            if (this._dataItem.Title.Value)
                titleText = String.format(this.get_clientLabelManager().getLabel("LibrariesResources", "AllSizesOf"), this._dataItem.Title.Value);
            else
                titleText = String.format(this.get_clientLabelManager().getLabel("LibrariesResources", "AllSizesOf"), this._dataItem.Title);            

            jQuery(this.get_titleLabel()).text(titleText);
            this.initializeDataSource();
            this.initializeListView();
        }
    },

    /* Private Methods */

    _removeHandlers: function () {
        if (this._listDataBoundDelegate) {
            delete this._listDataBoundDelegate;
        }
    },

    _initDelegates: function () {
        this._listDataBoundDelegate = Function.createDelegate(this, this._listDataBoundHandler);
    },

    fetchDataSource: function () {
        this._dataSource.fetch();
    },

    initializeDataSource: function () {
        var language = this._language;
        var mediaContentId = (this._dataItem.OriginalContentId && this._dataItem.OriginalContentId !== Telerik.Sitefinity.getEmptyGuid()) ?
                              this._dataItem.OriginalContentId :
                              this._dataItem.Id;

        this._dataSource = new kendo.data.DataSource({
            type: "json",
            transport: {
                read: {
                    url: this.get_webServiceUrl() + String.format('/?mediaContentId={0}&libraryProvider={1}', mediaContentId, this._providerName),
                    contentType: 'application/json; charset=utf-8',
                    type: "GET",
                    dataType: "json",
                    beforeSend: function (xhr) {
                        xhr.setRequestHeader("SF_CULTURE", language);
                        xhr.setRequestHeader("SF_UI_CULTURE", language);
                    }
                }
            },
            schema: {
                data: "Items"
            },
            requestStart: function (e) {
                jQuery('body').addClass('sfLoadingTransition');
            },
            change: function (e) {
                jQuery('body').removeClass('sfLoadingTransition');
            },
            error: function (jqXHR, textStatus, errorThrown) {
                jQuery('body').removeClass('sfLoadingTransition');
                var errText;
                if (jqXHR.responseText) {
                    errText = Telerik.Sitefinity.JSON.parse(jqXHR.responseText).Detail;
                }
                else {
                    errText = jqXHR.status;
                }
                alert(errText);
            }
        });
    },

    initializeListView: function () {
        var list = this.get_list();
        if (list.innerHTML) {
            list.innerHTML = "";
        }

        var that = this;
        jQuery(list).kendoListView({
            dataSource: this._dataSource,
            template: kendo.template(jQuery(this.getSelectors().listViewRowTemplate).html()),
            autoBind: true,
            dataBound: this._listDataBoundDelegate
        })
        .delegate(".sfActionsEmbed", "click", function (e) {
            var anchor = e.target;
            that._embedDataItem = that._dataSource.getByUid(jQuery(anchor).closest(".sfThumbnailRow").data("uid"));
            that._thumbnailEmbedShowDelegate = Function.createDelegate(that, that._thumbnailEmbedShowHandler);
            that.get_embedDialog().add_show(that._thumbnailEmbedShowDelegate);

            that._thumbnailEmbedLoadDelegate = Function.createDelegate(that, that._thumbnailEmbedLoadHandler);
            that.get_embedDialog().add_pageLoad(that._thumbnailEmbedLoadDelegate);

            that.get_embedDialog().show();
            Telerik.Sitefinity.centerWindowHorizontally(that.get_embedDialog());
        });
        //.delegate(".sfTurnOnItm", "click", function (e) {
        //    that._changeItemStatus(e);
        //})
        jQuery(list).show();
    },

    _thumbnailEmbedShowHandler: function (sender, args) {
        var frameHandle = sender.get_contentFrame().contentWindow;
        if (frameHandle) {
            if (frameHandle.createDialog) {
                if (!$telerik.isChrome) {
                    frameHandle.createDialog(null, this._embedDataItem);
                }
                else {
                    var that = this;
                    window.setTimeout(function () { frameHandle.createDialog(null, that._embedDataItem); }, 0);
                }
            }
        }
    },

    _thumbnailEmbedLoadHandler: function (sender, args) {
        this._thumbnailEmbedShowHandler(sender, args);
    },

    /* Properties */

    get_clientLabelManager: function () {
        return this._clientLabelManager;
    },
    set_clientLabelManager: function (value) {
        this._clientLabelManager = value;
    },

    get_titleLabel: function () {
        return this._titleLabel;
    },
    set_titleLabel: function (value) {
        this._titleLabel = value;
    },

    get_backLink: function () {
        return this._backLink;
    },
    set_backLink: function (value) {
        this._backLink = value;
    },
    get_list: function () {
        return this._list;
    },
    set_list: function (value) {
        this._list = value;
    },
    get_webServiceUrl: function () {
        return this._webServiceUrl;
    },
    set_webServiceUrl: function (value) {
        this._webServiceUrl = value;
    },
    getSelectors: function () {
        return this._selectors;
    },
    get_embedDialog: function () {
        return this._embedDialog;
    },
    set_embedDialog: function (value) {
        this._embedDialog = value;
    }
}
Telerik.Sitefinity.Modules.Libraries.Web.UI.Thumbnails.ThumbnailListDialog.registerClass("Telerik.Sitefinity.Modules.Libraries.Web.UI.Thumbnails.ThumbnailListDialog", Telerik.Sitefinity.Web.UI.AjaxDialogBase);
