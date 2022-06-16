Type.registerNamespace("Telerik.Sitefinity.Web.UI.Backend");

Telerik.Sitefinity.Web.UI.Backend.ClientPager = function (element) {
    Telerik.Sitefinity.Web.UI.Backend.ClientPager.initializeBase(this, [element]);

    this._handlePageLoadDelegate = null;
    this._infoDiv = null;
    this._cmdPrev = null;
    this._cmdNext = null;
    this._gridBinder = null;
    this._tableView = null;
    this._maxDisplayPagesCount = 10;
    this._displayInTheMiddleCount = 9;
    this._currentPageNumberCssClass = "rgCurrentPage";
    this._itemsGridPagerTextFormat = null;
}

Telerik.Sitefinity.Web.UI.Backend.ClientPager.prototype = {
    // ------------------------------------------------------------------------
    // initialization and clean-up
    // ------------------------------------------------------------------------
    initialize: function () {
        Telerik.Sitefinity.Web.UI.Backend.ClientPager.callBaseMethod(this, 'initialize');

        this._binderGridBoundDelegate = Function.createDelegate(this, this._handleGridDataBound);

        this._tableView = this._gridBinder.GetTableView();
        var grid = this._tableView._owner;

        grid.add_dataBound(this._binderGridBoundDelegate);
    },

    dispose: function () {
        // clean-up
        Telerik.Sitefinity.Web.UI.Backend.ClientPager.callBaseMethod(this, 'dispose');
    },

    get_infoDiv: function () {
        return this._infoDiv;
    },
    set_infoDiv: function (value) {
        this._infoDiv = value;
    },

    get_cmdNext: function () {
        return this._cmdNext;
    },
    set_cmdNext: function (value) {
        this._cmdNext = value;
    },

    get_cmdPrev: function () {
        return this._cmdPrev;
    },
    set_cmdPrev: function (value) {
        this._cmdPrev = value;
    },

    get_gridBinder: function () {
        return this._gridBinder;
    },
    set_gridBinder: function (value) {
        this._gridBinder = value;
    },

    get_itemsGridPagerTextFormat: function () {
        return this._itemsGridPagerTextFormat;
    },
    set_itemsGridPagerTextFormat: function (value) {
        this._itemsGridPagerTextFormat = value;
    },

    _handleGridDataBound: function (sender, args) {
        var tableView = this._gridBinder.GetTableView();

        this._setNavigationCommand(this.get_cmdPrev(), tableView, "'Prev'");
        this._setNavigationCommand(this.get_cmdNext(), tableView, "'Next'");
        this._setNavigationControlsVisibility(tableView);

        this._populateNumeric(tableView);
        this._populateInfoPanel(tableView);
    },

    _populateNumeric: function (tableView) {
        var numericDiv = $get(this._numericDivId);
        numericDiv.innerHTML = "";
        var sfPagerEllipsisClass = "sfPagerEllipsis";

        var currentPage = tableView.get_currentPageIndex() + 1;

        var pageSize = tableView.get_pageSize();

        this._pageCount = Math.ceil(tableView.get_virtualItemCount() / pageSize);

        var html = new Sys.StringBuilder();

        if (this._pageCount > this._maxDisplayPagesCount) {

            if (1 == currentPage) {
                this._appendLink(html, 1, this._currentPageNumberCssClass, 1);
            }
            else {
                this._appendLink(html, 1, null, 1);
            }

            var itemsOffset = Math.floor(this._displayInTheMiddleCount / 2);

            var showLeftDots = (currentPage - itemsOffset) > 2;
            var showRightDots = (currentPage + itemsOffset) <= this._pageCount - 2;

            if (showLeftDots)
                this._appendLink(html, "...", sfPagerEllipsisClass, false);

            // 1,2,3,4,5
            if ((!showLeftDots) && (!showRightDots)) {
                var middleStart = 2;
                var middleEnd = this._pageCount - 1;
            }
                // 1,2,3...10
            else if (((!showLeftDots) && (showRightDots))) {
                var middleStart = 2;
                var middleEnd = this._displayInTheMiddleCount;
            }
                // 1...5,6,7...10
            else if (((showLeftDots) && (showRightDots))) {
                var middleStart = currentPage - itemsOffset;
                var middleEnd = currentPage + itemsOffset;
            }
                // 1...7,8,9,10
            else if (((showLeftDots) && (!showRightDots))) {
                var middleStart = this._pageCount - this._displayInTheMiddleCount;
                var middleEnd = this._pageCount - 1;
            }

            for (var i = middleStart, len = middleEnd; i <= len; i++) {
                if (i == currentPage) {
                    this._appendLink(html, i, this._currentPageNumberCssClass, false);
                }
                else {
                    this._appendLink(html, i, null, i);
                }
            }

            if (showRightDots)
                this._appendLink(html, "...", sfPagerEllipsisClass, false);

            if (this._pageCount == currentPage) {
                this._appendLink(html, this._pageCount, this._currentPageNumberCssClass, this._pageCount);
            }
            else {
                this._appendLink(html, this._pageCount, null, this._pageCount);
            }
        }
        else {

            for (var i = 1, len = this._pageCount; i <= len; i++) {
                if (i == currentPage) {
                    this._appendLink(html, i, this._currentPageNumberCssClass, false);
                }
                else {
                    this._appendLink(html, i, null, i);
                }
            }
        }

        numericDiv.innerHTML = html.toString();
    },

    _appendLink: function (html, text, className, pageNumber) {
        var onclick = (typeof pageNumber !== 'number') ?
            "return false;" :
            String.format("Telerik.Web.UI.Grid.NavigateToPage('{0}',{1}); return false;", this._tableView.get_id(), pageNumber);

        html.append("<a href=\"#\"");
        if (className) {
            html.append(" class=\"");
            html.append(className);
            html.append("\"");
        }
        html.append(" onclick=\"");
        html.append(onclick);
        html.append("\">");
        html.append(text);
        html.append("</a>");
    },

    _setNavigationCommand: function (ctrl, tableView, command) {
        ctrl.href = "#";

        var page = tableView.get_currentPageIndex() + 1;

        switch (command) {
            case "'Prev'":
                page -= 1;
                break;

            case "'Next'":
                page += 1;
                break;
        }
        var att = document.createAttribute("onclick");
        att.value = String.format("Telerik.Web.UI.Grid.NavigateToPage('{0}',{1}); return false;", tableView.get_id(), page);
        ctrl.setAttributeNode(att);
    },

    _setNavigationControlsVisibility: function (tableView) {

        var currentPage = tableView.get_currentPageIndex() + 1;

        var jCmdPrev = jQuery("#" + this.get_cmdPrev().id);
        var jCmdNext = jQuery("#" + this.get_cmdNext().id);

        jCmdPrev.show();
        jCmdNext.show();

        if (currentPage == 1) {
            jCmdPrev.hide();
        }

        if (currentPage == this._pageCount) {
            jCmdNext.hide();
        }
    },

    _populateInfoPanel: function (tableView) {
        var infoDiv = this.get_infoDiv();

        var html = new Sys.StringBuilder();

        var pageSize = tableView.get_pageSize();

        var from = (tableView.get_currentPageIndex() * pageSize) + 1;
        var to = Math.min(from + pageSize - 1, tableView.get_virtualItemCount());

        html.append(String.format(this.get_itemsGridPagerTextFormat(), from, to, tableView.get_virtualItemCount()));

        infoDiv.innerHTML = html.toString();
    }
}

Telerik.Sitefinity.Web.UI.Backend.ClientPager.registerClass('Telerik.Sitefinity.Web.UI.Backend.ClientPager', Sys.UI.Control);