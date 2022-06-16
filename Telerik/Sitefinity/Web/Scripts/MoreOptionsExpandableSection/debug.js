Type.registerNamespace("Telerik.Sitefinity.Web.UI");

Telerik.Sitefinity.Web.UI.MoreOptionsExpandableSection = function (element) {
    this._linkTooltip = null;
    this._linkClass = null;
    this._openInNewWindow = null;
    this._moreOptionsSection = null;
    this._moreOptionsExpander = null;
    this._showOpenInNewWindow = null;

    this._moreOptionsExpanderClickDelegate = null;

    Telerik.Sitefinity.Web.UI.MoreOptionsExpandableSection.initializeBase(this, [element]);
}

Telerik.Sitefinity.Web.UI.MoreOptionsExpandableSection.prototype =
{
    /* -------------------- set up and tear down ----------- */

    initialize: function () {
        Telerik.Sitefinity.Web.UI.MoreOptionsExpandableSection.callBaseMethod(this, "initialize");

        if (this._moreOptionsExpanderClickDelegate == null) {
            this._moreOptionsExpanderClickDelegate = Function.createDelegate(this, this._moreOptionsExpanderClickHandler);
        }
        $addHandler(this._moreOptionsExpander, "click", this._moreOptionsExpanderClickDelegate);
    },

    dispose: function () {
        if (this._moreOptionsExpanderClickDelegate) {
            if (this._moreOptionsExpander) {
                $removeHandler(this._moreOptionsExpander, "click", this._moreOptionsExpanderClickDelegate);
            }
            delete this._moreOptionsExpanderClickDelegate;
        }

        Telerik.Sitefinity.Web.UI.MoreOptionsExpandableSection.callBaseMethod(this, "dispose");
    },

    /* --------------------  public methods ----------- */

    /* -------------------- events -------------------- */

    /* -------------------- event handlers ------------ */

    _moreOptionsExpanderClickHandler: function (e) {
        jQuery(this._moreOptionsSection).toggleClass("sfExpandedSection");
        dialogBase.resizeToContent();
    },

    /* -------------------- private methods ----------- */


    /* -------------------- properties ---------------- */

    get_linkTooltip: function () {
        return this._linkTooltip;
    },
    set_linkTooltip: function (value) {
        this._linkTooltip = value;
    },

    get_linkClass: function () {
        return this._linkClass;
    },
    set_linkClass: function (value) {
        this._linkClass = value;
    },

    get_openInNewWindow: function () {
        return this._openInNewWindow;
    },
    set_openInNewWindow: function (value) {
        this._openInNewWindow = value;
    },

    get_moreOptionsSection: function () {
        return this._moreOptionsSection;
    },
    set_moreOptionsSection: function (value) {
        this._moreOptionsSection = value;
    },

    get_moreOptionsExpander: function () {
        return this._moreOptionsExpander;
    },
    set_moreOptionsExpander: function (value) {
        this._moreOptionsExpander = value;
    },

    get_showOpenInNewWindow: function () {
        return this._showOpenInNewWindow;
    },
    set_showOpenInNewWindow: function (value) {
        this._showOpenInNewWindow = value;
    }
};


Telerik.Sitefinity.Web.UI.MoreOptionsExpandableSection.registerClass("Telerik.Sitefinity.Web.UI.MoreOptionsExpandableSection", Sys.UI.Control);