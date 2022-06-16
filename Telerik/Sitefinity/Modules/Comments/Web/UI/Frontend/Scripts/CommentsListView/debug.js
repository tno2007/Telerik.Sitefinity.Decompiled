Type.registerNamespace("Telerik.Sitefinity.Modules.Comments.Web.UI.Frontend");

Telerik.Sitefinity.Modules.Comments.Web.UI.Frontend.CommentsListView = function (element) {
    Telerik.Sitefinity.Modules.Comments.Web.UI.Frontend.CommentsListView.initializeBase(this, [element]);
    this._serviceUrl = null;
    this._loadMoreCommentsAnchor = null;
    this._currentCommentsCount = null;
    this._initalCommentsCount = null;
    this._loadMoreCommentsSize = null;
    this._newestOnTopAnchor = null;
    this._oldestOnTopAnchor = null;
    this._commentsCountLabel = null;
    this._leaveCommentAnchor = null;
    this._sortAscDate = null;
    this._latestDate = null;
    this._oldestDate = null;
    this.sortDescending = true;
    this._clientLabelManager = null;
    this._threadKey = null;
    this._currentLanguage = null;
    this._visibleCommentsStatuses = null;
    this._isLoading = false;
    this._alwaysUseUtc = false;
}

Telerik.Sitefinity.Modules.Comments.Web.UI.Frontend.CommentsListView.prototype = {
    initialize: function () {
        Telerik.Sitefinity.Modules.Comments.Web.UI.Frontend.CommentsListView.callBaseMethod(this, 'initialize');

        if (this.get_sortAscDate()) {
            this.sortDescending = false;
            jQuery(this.get_newestOnTopAnchor()).show();
            jQuery(this.get_oldestOnTopAnchor()).hide();
        }
        else {
            jQuery(this.get_newestOnTopAnchor()).hide();
        }

        this._initializeKendo();
        var safeThreadAttrName = unescape(this.get_threadKey());
        jQuery(this.get_commentsCountLabel()).attr("threadKey", safeThreadAttrName);

        this._initializeRating();

        //if paging is enabled 
        if (this.get_loadMoreCommentsAnchor() !== null) {
            //show/hide load more link
            if (this.get_initalCommentsCount() > this.get_currentCommentsCount())
                jQuery(this.get_loadMoreCommentsAnchor()).show();
            else
                jQuery(this.get_loadMoreCommentsAnchor()).hide();

            this._loadMoreClickDelegate = Function.createDelegate(this, this._loadMoreClickHandler);
            $addHandler(this.get_loadMoreCommentsAnchor(), "click", this._loadMoreClickDelegate);
        }

        this._newestOnTopClickDelegate = Function.createDelegate(this, this._changeSortOrderClickHandler);
        $addHandler(this.get_newestOnTopAnchor(), "click", this._newestOnTopClickDelegate);

        this._oldestOnTopClickDelegate = Function.createDelegate(this, this._changeSortOrderClickHandler);
        $addHandler(this.get_oldestOnTopAnchor(), "click", this._oldestOnTopClickDelegate);
    },

    dispose: function () {
        if (this._newestOnTopClickDelegate) {
            if (this.get_newestOnTopAnchor()) {
                $removeHandler(this.get_newestOnTopAnchor(), "click", this._newestOnTopClickDelegate);
            }
            delete this._newestOnTopClickDelegate;
        }

        if (this._oldestOnTopClickDelegate) {
            if (this.get_oldestOnTopAnchor()) {
                $removeHandler(this.get_oldestOnTopAnchor(), "click", this._oldestOnTopClickDelegate);
            }
            delete this._oldestOnTopClickDelegate;
        }

        if (this._loadMoreClickDelegate) {
            if (this.get_loadMoreCommentsAnchor()) {
                $removeHandler(this.get_loadMoreCommentsAnchor(), "click", this._loadMoreClickDelegate);
            }
            delete this._loadMoreClickDelegate;
        }

        Telerik.Sitefinity.Modules.Comments.Web.UI.Frontend.CommentsListView.callBaseMethod(this, 'dispose');
    },

    _initializeKendo: function () {
        //bind the kendo ListView
        jQuery(this.get_commentsKendoListView()).kendoListView({
            template: jQuery.proxy(kendo.template(jQuery("#commentsKendoListViewTemplate").html()), this)
        });
    },
    _initializeRating : function() {
        jQuery(this._element).find(".sfRating").each(function () {
            var currentRating = jQuery(this).html();
            currentRating = currentRating.replace(/,/g, '.'); // we fix the decimal separator for languages which use comma instead of dot (for example BG)
            if (jQuery.isNumeric(currentRating)) // check if the rating is a valid number
            {
                jQuery(this).rating({ value: currentRating, readOnly: true });
            }
        });
       
    },

    //fires on the kendo data source change
    _dataSourceChanged: function (e) {
        this._updateTimeInterval();
    },

    //update the time interval of all comments
    _updateTimeInterval: function () {
        var currentData = jQuery(jQuery(this.get_commentsKendoListView()).data().kendoListView.dataSource.data());
        if (currentData.length == 0)
            return;
        if (this.get_sortAscDate()) {
            this._latestDate = currentData.last()[0].DateCreated;
            this._oldestDate = currentData.first()[0].DateCreated;
        }
        else {
            this._latestDate = currentData.first()[0].DateCreated;
            this._oldestDate = currentData.last()[0].DateCreated;
        }
    },

    add_updateLabels: function (delegate) {
        this.get_events().addHandler('updateLabels', delegate);
    },
    remove_updateLabels: function (delegate) {
        this.get_events().removeHandler('updateLabels', delegate);
    },

    raise_updateLabels: function () {
        if (typeof this.get_events == 'function') {
            var h = this.get_events().getHandler('updateLabels');
            if (h) h(this, Sys.EventArgs.Empty);
            return Sys.EventArgs.Empty;
        }
    },

    addNewComment: function (comment) {
        if (jQuery.inArray(comment.Status, this.get_visibleCommentsStatuses()) == -1)
            return;
        var currentDataSource = jQuery(this.get_commentsKendoListView()).data().kendoListView.dataSource;
        if (currentDataSource.transport.options || this.get_sortAscDate()) {
            //update total count of the elements 
            currentDataSource.total(currentDataSource.total() + 1);

            //add new comment locally
            if (currentDataSource.data().length < this.get_currentCommentsCount()) {
                if (!this.get_sortAscDate()) {
                    currentDataSource.insert(0, comment);
                }
                else {
                    currentDataSource.add(comment);
                }
            }
            else {// add the new comment only on the server 
                if (!this.get_sortAscDate()) {
                    currentDataSource.insert(0, comment);
                    //if paging is enabled
                    if (this.get_loadMoreCommentsAnchor() !== null) {
                        currentDataSource.remove(jQuery(currentDataSource.data()).last()[0]);
                    }
                }
                else {
                    if (this.get_loadMoreCommentsAnchor() === null) {
                        currentDataSource.add(comment);
                    }
                }
            }
        }
        else {
            jQuery(this.get_commentsServerListView()).hide();
            jQuery(this.get_commentsKendoListView()).data().kendoListView.setDataSource(this._getNewDataSource(null, null, this.get_currentCommentsCount()));
        }
    },

    //performs the sorting of the data
    _changeSortOrderClickHandler: function () {
        jQuery(this.get_commentsServerListView()).hide();

        this.set_sortAscDate(!this.get_sortAscDate());
        if (this.get_sortAscDate()) {//oldest on top
            jQuery(this.get_newestOnTopAnchor()).show();
            jQuery(this.get_oldestOnTopAnchor()).hide();
            this.sortDescending = false;
        }
        else {//newest on top
            jQuery(this.get_newestOnTopAnchor()).hide();
            jQuery(this.get_oldestOnTopAnchor()).show();
            this.sortDescending = true;
        }

        var dataSource = this._getNewDataSource(null, null, this.get_currentCommentsCount());
        jQuery(this.get_commentsKendoListView()).data().kendoListView.setDataSource(dataSource);
    },

    _getNewDataSource: function (olderThan, newerThan, take) {
        var params = this.get_commentsRestClient()._getQueryString(this.get_threadKey(), this.get_currentLanguage(), olderThan, newerThan, this.sortDescending, take);
        
        return new kendo.data.DataSource({
            transport: {
                read: {
                    cache: false, //Fixes a bug with the IE caching Ajax responses
                    url: this.get_serviceUrl() + "/comments/" + params,
                    contentType: "application/json; charset=utf-8",
                    type: "GET",
                    accepts: {
                        text: "application/json"
                    },
                    dataType: "text",
                    dataFilter: function (data) {
                        return Sys.Serialization.JavaScriptSerializer.deserialize(data);
                    }
                }
            },
            schema: {
                data: "Items",
                total: "TotalCount"
            },
            change: jQuery.proxy(this._dataSourceChanged, this)
        });
    },

    //handles "load more comments" link click
    _loadMoreClickHandler: function () {
        if (this._isLoading) {
            return;
        }
        this._isLoading = true;

        var olderThan;
        var newerThan;
        var olderThanFormated;
        var newerThanFormated;
        var date = new Date();
        if (this.get_sortAscDate()) {
            olderThan = null;
            newerThan = new Date(this._latestDate.getTime());
            if (!this.get_alwaysUseUtc()) {
                newerThan.setMinutes(newerThan.getMinutes() + date.getTimezoneOffset());
            }
            newerThanFormated = newerThan.format("dd MMM yyyy HH:mm:ss.fff");
        }
        else {
            newerThan = null;
            olderThan = new Date(this._oldestDate.getTime());
            if (!this.get_alwaysUseUtc()) {
                olderThan.setMinutes(olderThan.getMinutes() + date.getTimezoneOffset());
            }
            olderThanFormated = olderThan.format("dd MMM yyyy HH:mm:ss.fff");
        }
        var that = this;
        //sends ajax request to get more items
        this.get_commentsRestClient().getComments(this.get_threadKey(), this.get_currentLanguage(), olderThanFormated,
            newerThanFormated, this.sortDescending, this.get_loadMoreCommentsSize(),
            jQuery.proxy(this._addMoreItemsToKendo, this), null, function () {
                that._isLoading = false;
            });
    },

    //add new items directly into the kendo tree when the user request more comments
    _addMoreItemsToKendo: function (data, textStatus, jqXHR) {
        var currentDataSource = jQuery(this.get_commentsKendoListView()).data().kendoListView.dataSource;
        for (var i = 0; i < data.Items.length; i++)
            currentDataSource.add(data.Items[i]);

        this._updateTimeInterval();

        this.set_currentCommentsCount(this.get_currentCommentsCount() + this.get_loadMoreCommentsSize());
        currentDataSource.total(currentDataSource.total() + data.length);

        //update load more comments labels visibility
        this.raise_updateLabels();
    },

    get_commentsKendoListView: function () {
        return jQuery(this.get_element()).find(".sfcommentsList.sfkendoList")[0];
    },
    get_commentsServerListView: function () {
        return jQuery(this.get_element()).find(".sfcommentsList.sfserverList")[0];
    },

    get_loadMoreCommentsAnchor: function () {
        return this._loadMoreCommentsAnchor;
    },
    set_loadMoreCommentsAnchor: function (value) {
        this._loadMoreCommentsAnchor = value;
    },

    get_newestOnTopAnchor: function () {
        return this._newestOnTopAnchor;
    },
    set_newestOnTopAnchor: function (value) {
        this._newestOnTopAnchor = value;
    },

    get_leaveCommentAnchor: function () {
        return this._leaveCommentAnchor;
    },
    set_leaveCommentAnchor: function (value) {
        this._leaveCommentAnchor = value;
    },

    get_oldestOnTopAnchor: function () {
        return this._oldestOnTopAnchor;
    },
    set_oldestOnTopAnchor: function (value) {
        this._oldestOnTopAnchor = value;
    },

    get_initalCommentsCount: function () {
        return this._initalCommentsCount;
    },
    set_initalCommentsCount: function (value) {
        this._initalCommentsCount = value;
    },

    get_commentsCountLabel: function () {
        return this._commentsCountLabel;
    },
    set_commentsCountLabel: function (value) {
        this._commentsCountLabel = value;
    },

    get_loadMoreCommentsSize: function () {
        return this._loadMoreCommentsSize;
    },
    set_loadMoreCommentsSize: function (value) {
        this._loadMoreCommentsSize = value;
    },

    get_currentCommentsCount: function () {
        return this._currentCommentsCount;
    },
    set_currentCommentsCount: function (value) {
        this._currentCommentsCount = value;
    },

    get_serviceUrl: function () {
        return this._serviceUrl;
    },
    set_serviceUrl: function (value) {
        this._serviceUrl = value;
    },

    get_currentLanguage: function () {
        return this._currentLanguage;
    },
    set_currentLanguage: function (value) {
        this._currentLanguage = value;
    },

    get_sortAscDate: function () {
        return this._sortAscDate;
    },
    set_sortAscDate: function (value) {
        this._sortAscDate = value;
    },

    get_threadKey: function () {
        return encodeURIComponent(this._threadKey);
    },
    set_threadKey: function (value) {
        this._threadKey = value;
    },

    get_clientLabelManager: function () {
        return this._clientLabelManager;
    },
    set_clientLabelManager: function (value) {
        this._clientLabelManager = value;
    },

    get_commentsRestClient: function () {
        return this._commentsRestClient;
    },
    set_commentsRestClient: function (value) {
        this._commentsRestClient = value;
    },

    get_oldestDate: function () {
        return this._oldestDate;
    },
    set_oldestDate: function (value) {
        this._oldestDate = value;
    },

    get_latestDate: function () {
        return this._latestDate;
    },
    set_latestDate: function (value) {
        this._latestDate = value;
    },

    get_visibleCommentsStatuses: function () {
        return this._visibleCommentsStatuses;
    },
    set_visibleCommentsStatuses: function (value) {
        this._visibleCommentsStatuses = value;
    },

    get_alwaysUseUtc: function () {
        return this._alwaysUseUtc;
    },
    set_alwaysUseUtc: function (value) {
        this._alwaysUseUtc = value;
    }
};

Telerik.Sitefinity.Modules.Comments.Web.UI.Frontend.CommentsListView.registerClass("Telerik.Sitefinity.Modules.Comments.Web.UI.Frontend.CommentsListView", Sys.UI.Control);