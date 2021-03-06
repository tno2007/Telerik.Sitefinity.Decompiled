Type.registerNamespace("Telerik.Sitefinity.Web.UI");

/* Pager class */

Telerik.Sitefinity.Web.UI.NavigateToPage = function (pagerId, pageIndex) {
    var pager = $find(pagerId);
    if (pager) {
        pager.changePage(pageIndex);
    }
};


Telerik.Sitefinity.Web.UI.Pager = function (element) {
    Telerik.Sitefinity.Web.UI.Pager.initializeBase(this, [element]);
    this._virtualItemCount = 0;
    this._displayCount = 10;
    this._pageSize = 10;
    this._currentPage = 0;
    this._pageCount = 0;
    this._navigationMode = Telerik.Sitefinity.Web.UI.PagerNavigationModes.ClientSide;

    this._numericDivId = null;
    this._numericFormat = null;
    this._pageNumberCssClass = null;
    this._currentPageNumberCssClass = null;
    this._prevGroupText = null;
    this._prevGroupCssClass = null;
    this._nextGroupText = null;
    this._nextGroupCssClass = null;

    this._pageLinkClickedDelegate = null;
    this._pageLoadDelegate = null;
}

Telerik.Sitefinity.Web.UI.Pager.prototype = {

    // set up 
    initialize: function () {
        Telerik.Sitefinity.Web.UI.Pager.callBaseMethod(this, "initialize");
        this._pageChangedDelegate = Function.createDelegate(this, this._raisePageChanged);
    },

    // tear down
    dispose: function () {
        if (this._pageLinkClickedDelegate) {
            delete this._pageLinkClickedDelegate;
        }
        if (this._pageLoadDelegate) {
            delete this._pageLoadDelegate;
        }
        Telerik.Sitefinity.Web.UI.Pager.callBaseMethod(this, "dispose");
    },

    init: function (virtualItemCount, pageSize) {
        this._virtualItemCount = virtualItemCount;
        this._pageSize = pageSize;

        if (this._virtualItemCount > this._pageSize) {
            $('#' + this._numericDivId).show();
            this.invalidate();
        }
        else {
            $('#' + this._numericDivId).hide();
        }
    },

    changePage: function (command) {
        var newPage = this.get_currentPage();
        if (command == "Next") {
            newPage++;
        }
        else if (command == "Prev") {
            newPage--;
        }
        else if (command == "First") {
            newPage = 1;
        }
        else if (command == "Last") {
            newPage = this.get_pageCount();
        }
        else {
            newPage = parseInt(command);
        }

        if (newPage < 1 || newPage > this.get_pageCount())
            return false;

        this._currentPage = newPage;
        this.invalidate();
        this._raisePageChanged(newPage);
    },


    /* ----------------------- private ----------------------- */

    invalidate: function () {
        this._pageCount = Math.ceil(this._virtualItemCount / this._pageSize);

        var numericDiv = $get(this._numericDivId);
        if (numericDiv) {
            this._populateNumeric(this, numericDiv);
        }
    },

    _populateNumeric: function (pager, numericDiv) {
        numericDiv.innerHTML = "";

        var startIndex = 1;

        if (this.get_currentPage() > this._displayCount) {
            startIndex = (Math.floor((this.get_currentPage() - 1) / this._displayCount) * this._displayCount) + 1;
        }

        var end = Math.min(this._pageCount, (startIndex + this._displayCount) - 1);

        var html = new Sys.StringBuilder();

        if (startIndex > this._displayCount) {
            this._appendLink(html, this._prevGroupText, this._prevGroupCssClass, Math.max(startIndex - 1, 1));
        }

        for (var i = startIndex, len = end; i <= len; i++) {
            var number = String.format(this._numericFormat, i);
            if (i == this._currentPage) {
                this._appendLink(html, number, this._currentPageNumberCssClass, false);
            } else {
                this._appendLink(html, number, this._pageNumberCssClass, i);
            }
        }

        if (end < this._pageCount) {
            this._appendLink(html, this._nextGroupText, this._nextGroupCssClass, end + 1);
        }

        numericDiv.innerHTML = html.toString();
    },

    _appendLink: function (html, text, className, pageNumber) {
        var onclick = (typeof pageNumber !== 'number') ?
            "return false;" :
            String.format("Telerik.Sitefinity.Web.UI.NavigateToPage('{0}',{1}); return false;", this.get_id(), pageNumber);

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

    /* ----------------------- events ----------------------- */
    add_pageChanged: function (delegate) {
        this.get_events().addHandler("pageChanged", delegate);
    },

    remove_pageChanged: function (delegate) {
        this.get_events().removeHandler("pageChanged", delegate);
    },

    /* ----------------------- event raising ----------------------- */
    _raisePageChanged: function (pageIndex) {
        var handler = this.get_events().getHandler("pageChanged");
        if (handler) {
            var skip = (pageIndex - 1) * this._pageSize;
            var take = this._pageSize;
            var args = new Telerik.Sitefinity.Web.UI.PagingEventArgs(pageIndex, skip, take);
            handler(this, args);
        }
        return true;
    },

    /* ----------------------- properties ----------------------- */
    get_virtualItemCount: function () {
        return this._virtualItemCount;
    },

    get_pageSize: function () {
        return this._pageSize;
    },

    get_currentPage: function () {
        return this._currentPage;
    },
    //this method does not raise any events or cause rebinding - use changePage() - to notify pageChanged event subscribers and cause rebinding
    set_currentPage: function (pageNumber) {
        this._currentPage = pageNumber;
    },
    get_pageCount: function () {
        return this._pageCount;
    }


};
Telerik.Sitefinity.Web.UI.Pager.registerClass('Telerik.Sitefinity.Web.UI.Pager', Sys.UI.Control);

Telerik.Sitefinity.Web.UI.PagerNavigationModes = function () {
};
Telerik.Sitefinity.Web.UI.PagerNavigationModes.prototype = {
    Postback: 0,
    Links: 1,
    ClientSide: 2,
    Evaluator: 4,
    Auto: 3
};

Telerik.Sitefinity.Web.UI.PagerNavigationModes.registerEnum("Telerik.Sitefinity.Web.UI.PagerNavigationModes");



//-----------------------------------------------------------------------------
// Event arguments
//-----------------------------------------------------------------------------

Telerik.Sitefinity.Web.UI.PagingEventArgs = function (newPage, skip, take) {
    Telerik.Sitefinity.Web.UI.PagingEventArgs.initializeBase(this);
    this._newPage = newPage;
    this._skip = skip;
    this._take = take;
}
Telerik.Sitefinity.Web.UI.PagingEventArgs.prototype = {
    get_newPage: function () {
        return this._newPage;
    },
    get_skip: function () {
        return this._skip;
    },
    get_take: function () {
        return this._take;
    }
};
Telerik.Sitefinity.Web.UI.PagingEventArgs.registerClass("Telerik.Sitefinity.Web.UI.PagingEventArgs", Sys.EventArgs);

