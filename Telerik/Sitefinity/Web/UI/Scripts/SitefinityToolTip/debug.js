Type.registerNamespace("Telerik.Sitefinity.Web.UI");

Telerik.Sitefinity.Web.UI.SitefinityToolTip = function (element) {
    Telerik.Sitefinity.Web.UI.SitefinityToolTip.initializeBase(this, [element]);

    this._wrpToolTip = null;
    this._btnToolTipTarget = null;

    this._toolTipTargetClickDelegate = null;
}

Telerik.Sitefinity.Web.UI.SitefinityToolTip.prototype =
{
    /* -------------------- set up and tear down ----------- */

    initialize: function () {
        Telerik.Sitefinity.Web.UI.SitefinityToolTip.callBaseMethod(this, "initialize");

        this._toolTipTargetClickDelegate = Function.createDelegate(this, this._toolTipTargetClicked);
        $addHandler(this.get_btnToolTipTarget(), 'click', this._toolTipTargetClickDelegate);

    },

    dispose: function () {
        Telerik.Sitefinity.Web.UI.SitefinityToolTip.callBaseMethod(this, "dispose");
    },

    hide: function () {
        jQuery(this.get_wrpToolTip()).hide();
    },

    _toolTipTargetClicked: function(){
        jQuery(this.get_wrpToolTip()).toggle();
        var evt = new CustomEvent("tooltipToggled");
        document.dispatchEvent(evt);
    },

    /* -------------------- getters & setters ---------------- */

    get_wrpToolTip: function () {
        return this._wrpToolTip;
    },

    set_wrpToolTip: function (value) {
        this._wrpToolTip = value;
    },

    get_btnToolTipTarget: function () {
        return this._btnToolTipTarget;
    },

    set_btnToolTipTarget: function (value) {
        this._btnToolTipTarget = value;
    }
};

Telerik.Sitefinity.Web.UI.SitefinityToolTip.registerClass("Telerik.Sitefinity.Web.UI.SitefinityToolTip", Sys.UI.Control);
