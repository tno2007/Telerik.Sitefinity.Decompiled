/// <reference name="MicrosoftAjax.js"/>
/// <reference name="Telerik.Sitefinity.Resources.Scripts.jquery-1.6.3-vsdoc.js" assembly="Telerik.Sitefinity.Resources"/>
Type.registerNamespace("Telerik.Sitefinity.Web.UI.Fields");

Telerik.Sitefinity.Web.UI.Fields.ContentStatisticsField = function (element) {
    Telerik.Sitefinity.Web.UI.Fields.ContentStatisticsField.initializeBase(this, [element]);
    this._element = element;
    this._serviceBaseUrl = null;
    this._itemId = null;
    this._itemType = null;
    this._statsLink = null;
    this._statsLinkDelegate = null;
}

Telerik.Sitefinity.Web.UI.Fields.ContentStatisticsField.prototype =
{
    /* --------------------  set up and tear down ----------- */

    initialize: function () {
        Telerik.Sitefinity.Web.UI.Fields.ContentStatisticsField.callBaseMethod(this, "initialize");
        if (this._statsLink) {

        }
    },

    dispose: function () {
        Telerik.Sitefinity.Web.UI.Fields.ContentStatisticsField.callBaseMethod(this, "dispose");
    },

    /* --------------------  public methods ----------- */

    reset: function () {

    },

    /* -------------------- events -------------------- */

    add_command: function (handler) {
    },

    remove_command: function (handler) {
    },

    /* -------------------- event handlers ------------ */

    _toggle: function (sender, args) {

    },

    _openRevisionHistory: function (sender, args) {
    },

    _redirectToStatistics: function (sender, args) {
        var serviceBaseUrl = this.get_serviceBaseUrl();
        window.location.href = serviceBaseUrl + "/:" + this._itemId + "/:" + this._itemType;
    },

    onCommand: function (sender, args) {
    },

    /* -------------------- private methods ----------- */

    /* -------------------- properties ---------------- */

    get_serviceBaseUrl: function () {
        return this._serviceBaseUrl;
    },
    set_serviceBaseUrl: function (value) {
        this._serviceBaseUrl = value;
    },

    get_statsLink: function () {
        return this._statsLink;
    },
    set_statsLink: function (value) {
        this._statsLink = value;
    },

    // inherited from IRequiresDataItemContext
    set_dataItemContext: function (value) {
        var itemLifecycle = value.LifecycleStatus || value.Item.Lifecycle;
        if (itemLifecycle) {
            this._isBoundToItem = true;
            jQuery(this._element).show();
            
            var hasLiveVersion = value.HasLiveVersion || itemLifecycle.HasLiveVersion || value.Item.ApprovalWorkflowState.Value === "Unpublished";

            if (itemLifecycle.IsPublished || hasLiveVersion) {
                this._itemId = value.Item.OriginalContentId;
                this._itemType = value.ItemType;
                var statisticsUrl = "";
                var itemTitle = value.Item && value.Item.Title ? escape(value.Item.Title.Value) : escape(value.Item.SystemUrl);
                statisticsUrl = $(this._serviceBaseUrl).val() + "/:[" + escape(itemTitle) + "]/:" + value.Item.OriginalContentId + "/:" + value.ItemType + "/:" + value.Item.ProviderName;
                
                $(this._statsLink).attr("href", statisticsUrl);
            }
            else {
                $(this._element).hide();
            }
        }
        else {
            $(this._element).hide();
        }
    },
};

Telerik.Sitefinity.Web.UI.Fields.ContentStatisticsField.registerClass("Telerik.Sitefinity.Web.UI.Fields.ContentStatisticsField", Telerik.Sitefinity.Web.UI.Fields.FieldControl, Telerik.Sitefinity.Web.UI.Fields.IRequiresDataItemContext, Telerik.Sitefinity.Web.UI.Fields.ICommandField);