﻿Type.registerNamespace("Telerik.Sitefinity.Modules.Newsletters.Web.UI.Reports");

Telerik.Sitefinity.Modules.Newsletters.Web.UI.Reports.SubscribersReportSidebar = function (element) {
    Telerik.Sitefinity.Modules.Newsletters.Web.UI.Reports.SubscribersReportSidebar.initializeBase(this, [element]);

    this._allSubscribers = null;
    this._allSubscribersClickDelegate = null;

    this._openedSubscribers = null;
    this._openedSubscribersClickDelegate = null;

    this._clickedSubscribers = null;
    this._clickedSubscribersClickDelegate = null;

    this._notDeliveredSubscribers = null;
    this._notDeliveredSubscribersClickDelegate = null;

    this._invalidEmailSubscribers = null;
    this._invalidEmailSubscribersClickDelegate = null;

    this._unsubscribedSubscribers = null;
    this._unsubscribedSubscribersClickDelegate = null;

    this._byClickedLinks = null;
    this._byClickedLinksClickDelegate = null;

    this._filtersContainer = null;
    this._clickedLinksContainer = null;

    this._clickedLinks = null;
    this._clickedLinksSelectedDelegate = null;

    this._backFromClickedLinks = null;
    this._backFromClickedLinksClickDelegate = null;
}

Telerik.Sitefinity.Modules.Newsletters.Web.UI.Reports.SubscribersReportSidebar.prototype = {
    initialize: function () {
        Telerik.Sitefinity.Modules.Newsletters.Web.UI.Reports.SubscribersReportSidebar.callBaseMethod(this, "initialize");

        this._allSubscribersClickDelegate = Function.createDelegate(this, this._allSubscribersClick);
        $addHandler(this.get_allSubscribers(), "click", this._allSubscribersClickDelegate);

        this._openedSubscribersClickDelegate = Function.createDelegate(this, this._openedSubscribersClick);
        $addHandler(this.get_openedSubscribers(), "click", this._openedSubscribersClickDelegate);

        this._clickedSubscribersClickDelegate = Function.createDelegate(this, this._clickedSubscribersClick);
        $addHandler(this.get_clickedSubscribers(), "click", this._clickedSubscribersClickDelegate);

        this._notDeliveredSubscribersClickDelegate = Function.createDelegate(this, this._notDeliveredSubscribersClick);
        $addHandler(this.get_notDeliveredSubscribers(), "click", this._notDeliveredSubscribersClickDelegate);

        this._invalidEmailSubscribersClickDelegate = Function.createDelegate(this, this._invalidEmailSubscribersClick);
        $addHandler(this.get_invalidEmailSubscribers(), "click", this._invalidEmailSubscribersClickDelegate);

        this._unsubscribedSubscribersClickDelegate = Function.createDelegate(this, this._unsubscribedSubscribersClick);
        $addHandler(this.get_unsubscribedSubscribers(), "click", this._unsubscribedSubscribersClickDelegate);

        this._byClickedLinksClickDelegate = Function.createDelegate(this, this._byClickedLinksClick);
        $addHandler(this.get_byClickedLinks(), "click", this._byClickedLinksClickDelegate);

        this._clickedLinksSelectedDelegate = Function.createDelegate(this, this._clickedLinksSelected);
        this.get_clickedLinks().add_selected(this._clickedLinksSelectedDelegate);

        this._backFromClickedLinksClickDelegate = Function.createDelegate(this, this._backFromClickedLinksClick);
        $addHandler(this.get_backFromClickedLinks(), "click", this._backFromClickedLinksClickDelegate);
    },

    dispose: function () {
        if (this._allSubscribersClickDelegate) {
            if (this.get_allSubscribers()) {
                $removeHandler(this.get_allSubscribers(), "click", this._allSubscribersClickDelegate);
            }
            delete this._allSubscribersClickDelegate;
        }

        if (this._openedSubscribersClickDelegate) {
            if (this.get_openedSubscribers()) {
                $removeHandler(this.get_openedSubscribers(), "click", this._openedSubscribersClickDelegate);
            }
            delete this._openedSubscribersClickDelegate;
        }

        if (this._clickedSubscribersClickDelegate) {
            if (this.get_clickedSubscribers()) {
                $removeHandler(this.get_clickedSubscribers(), "click", this._clickedSubscribersClickDelegate);
            }
            delete this._clickedSubscribersClickDelegate;
        }

        if (this._notDeliveredSubscribersClickDelegate) {
            if (this.get_notDeliveredSubscribers()) {
                $removeHandler(this.get_notDeliveredSubscribers(), "click", this._notDeliveredSubscribersClickDelegate);
            }
            delete this._notDeliveredSubscribersClickDelegate;
        }

        if (this._invalidEmailSubscribersClickDelegate) {
            if (this.get_invalidEmailSubscribers()) {
                $removeHandler(this.get_invalidEmailSubscribers(), "click", this._invalidEmailSubscribersClickDelegate);
            }
            delete this._invalidEmailSubscribersClickDelegate;
        }

        if (this._unsubscribedSubscribersClickDelegate) {
            if (this.get_unsubscribedSubscribers()) {
                $removeHandler(this.get_unsubscribedSubscribers(), "click", this._unsubscribedSubscribersClickDelegate);
            }
            delete this._unsubscribedSubscribersClickDelegate;
        }

        if (this._byClickedLinksClickDelegate) {
            if (this.get_byClickedLinks()) {
                $removeHandler(this.get_byClickedLinks(), "click", this._byClickedLinksClickDelegate);
            }
            delete this._byClickedLinksClickDelegate;
        }

        if (this._clickedLinksSelectedDelegate) {
            if (this.get_clickedLinks()) {
                this.get_clickedLinks().remove_selected(this._clickedLinksSelectedDelegate);
            }
            delete this._clickedLinksSelectedDelegate;
        }

        if (this._backFromClickedLinksClickDelegate) {
            if (this.get_backFromClickedLinks()) {
                $removeHandler(this.get_backFromClickedLinks(), "click", this._backFromClickedLinksClickDelegate);
            }
            delete this._backFromClickedLinksClickDelegate;
        }

        Telerik.Sitefinity.Modules.Newsletters.Web.UI.Reports.SubscribersReportSidebar.callBaseMethod(this, "dispose");
    },

    _allSubscribersClick: function (sender, args) {
        this._filterSubscribersReport("");
    },

    _openedSubscribersClick: function (sender, args) {
        this._filterSubscribersReport("?filter=NULL+!%3d+DateOpened");
    },

    _clickedSubscribersClick: function (sender, args) {
        this._filterSubscribersReport("?filter=HasClicked+%3d%3d+true");
    },

    _notDeliveredSubscribersClick: function (sender, args) {
        this._filterSubscribersReport("?filter=" + encodeURIComponent("(MessageStatus != Normal && MessageStatus != EmailAddressDoesNotExist) || DeliveryStatus == Failure"));
    },

    _invalidEmailSubscribersClick: function (sender, args) {
        this._filterSubscribersReport("?filter=MessageStatus+%3d%3d+EmailAddressDoesNotExist");
    },

    _unsubscribedSubscribersClick: function (sender, args) {
        this._filterSubscribersReport("?filter=HasUnsubscribed+%3d%3d+true");
    },

    _byClickedLinksClick: function (sender, args) {
        jQuery(this.get_filtersContainer()).hide();
        this.get_clickedLinks().clear();
        jQuery(this.get_clickedLinksContainer()).show();
    },

    _filterSubscribersReport: function (query) {
        //a function that is defined in SubscribersReportView.ascx
        if (filterSubscribersReport) {
            filterSubscribersReport(query);
        }
    },

    _backFromClickedLinksClick: function (sender, args) {
        jQuery(this.get_clickedLinksContainer()).hide();
        jQuery(this.get_filtersContainer()).show();
    },

    _clickedLinksSelected: function (sender, args) {
        this._filterSubscribersReport("?byClickedLink=" + escape(args));
    },

    get_allSubscribers: function () {
        return this._allSubscribers;
    },
    set_allSubscribers: function (value) {
        this._allSubscribers = value;
    },
    get_openedSubscribers: function () {
        return this._openedSubscribers;
    },
    set_openedSubscribers: function (value) {
        this._openedSubscribers = value;
    },
    get_clickedSubscribers: function () {
        return this._clickedSubscribers;
    },
    set_clickedSubscribers: function (value) {
        this._clickedSubscribers = value;
    },
    get_notDeliveredSubscribers: function () {
        return this._notDeliveredSubscribers;
    },
    set_notDeliveredSubscribers: function (value) {
        this._notDeliveredSubscribers = value;
    },
    get_invalidEmailSubscribers: function () {
        return this._invalidEmailSubscribers;
    },
    set_invalidEmailSubscribers: function (value) {
        this._invalidEmailSubscribers = value;
    },
    get_unsubscribedSubscribers: function () {
        return this._unsubscribedSubscribers;
    },
    set_unsubscribedSubscribers: function (value) {
        this._unsubscribedSubscribers = value;
    },
    get_byClickedLinks: function () {
        return this._byClickedLinks;
    },
    set_byClickedLinks: function (value) {
        this._byClickedLinks = value;
    },
    get_filtersContainer: function () {
        return this._filtersContainer;
    },
    set_filtersContainer: function (value) {
        this._filtersContainer = value;
    },
    get_clickedLinksContainer: function () {
        return this._clickedLinksContainer;
    },
    set_clickedLinksContainer: function (value) {
        this._clickedLinksContainer = value;
    },
    get_clickedLinks: function () {
        return this._clickedLinks;
    },
    set_clickedLinks: function (value) {
        this._clickedLinks = value;
    },
    get_backFromClickedLinks: function () {
        return this._backFromClickedLinks;
    },
    set_backFromClickedLinks: function (value) {
        this._backFromClickedLinks = value;
    }
};

Telerik.Sitefinity.Modules.Newsletters.Web.UI.Reports.SubscribersReportSidebar.registerClass('Telerik.Sitefinity.Modules.Newsletters.Web.UI.Reports.SubscribersReportSidebar', Sys.UI.Control);
