/// <reference name="MicrosoftAjax.js"/>
/// <reference name="Telerik.Sitefinity.Resources.Scripts.jquery-1.6.3-vsdoc.js" assembly="Telerik.Sitefinity.Resources"/>

Type.registerNamespace("Telerik.Sitefinity.Modules.Pages.Web.UI.Fields");

Telerik.Sitefinity.Modules.Pages.Web.UI.Fields.ResponsiveLayoutField = function (element) {
    Telerik.Sitefinity.Modules.Pages.Web.UI.Fields.ResponsiveLayoutField.initializeBase(this, [element]);
    this._element = element;
    this._selectMediaQueryDialog = null;
    this._selectButtonMediaQuery = null;
    this._selectButtonMediaQueryClickDelegate = null;
    this._lnkDoneMediaQuery = null;
    this._lnkCancelMediaQuery = null;
    this._doneSelectingMediaQueryDelegate = null;
    this._MediaQueryDoneSelectingDelegate = null;
    this._MediaQueryCancelDelegate = null;
    this._cancelMediaQueryDelegate = null;
    this._MediaQueryItemSelector = null;
    this._MediaQuerySelectedKeys = null;
    this._MediaQuerySelectedItems = null;
    this._MediaQueryBinderBound = false;
    this._itemId = null;
    this._itemType = null;
    this._serviceUrl = null;
}

Telerik.Sitefinity.Modules.Pages.Web.UI.Fields.ResponsiveLayoutField.prototype = {
    initialize: function () {
        /* Here you can attach to events or do other initialization */
        Telerik.Sitefinity.Modules.Pages.Web.UI.Fields.ResponsiveLayoutField.callBaseMethod(this, "initialize");

        if (this._selectButtonMediaQuery) {
            this._selectButtonMediaQueryClickDelegate = Function.createDelegate(this, this._selectButtonMediaQueryClicked);
            $addHandler(this._selectButtonMediaQuery, "click", this._selectButtonMediaQueryClickDelegate);
        }

        if (this._lnkDoneMediaQuery) {
            this._MediaQueryDoneSelectingDelegate = Function.createDelegate(this, this._MediaQueryDoneSelecting);
            $addHandler(this._lnkDoneMediaQuery, "click", this._MediaQueryDoneSelectingDelegate);
        }

        if (this._lnkCancelMediaQuery) {
            this._MediaQueryCancelDelegate = Function.createDelegate(this, this._MediaQueryItemSelectorCloseHandler);
            $addHandler(this._lnkCancelMediaQuery, "click", this._MediaQueryCancelDelegate);
        }

        $('input:radio[name="MediaQueryRules"]').change(this._mediaQueryRulesButtonsChanged);
        this._resetUI();
        this.get_MediaQueryItemSelector().set_selectedKeys(this.get_selectedKeys());

        this._selectMediaQueryDialog = jQuery("#MediaQuerySelector").dialog({
            autoOpen: false,
            modal: true,
            width: 425,
            height: "auto",
            closeOnEscape: true,
            resizable: false,
            draggable: false,
            classes: {
                "ui-dialog": "sfSelectorDialog sfZIndexL"
            }
        });
    },

    dispose: function () {

        if (this._selectButtonMediaQueryClickDelegate) {
            if (this._selectButtonMediaQuery) {
                $removeHandler(this._selectButtonMediaQuery, "click", this._selectButtonMediaQueryClickDelegate);
            }
            delete this._selectButtonMediaQueryClickDelegate;
        }

        if (this._MediaQueryDoneSelectingDelegate) {
            if (this._lnkDoneMediaQuery) {
                $removeHandler(this._lnkDoneMediaQuery, "click", this._MediaQueryDoneSelectingDelegate);
            }
            delete this._MediaQueryDoneSelectingDelegate;
        }

        if (this._MediaQueryCancelDelegate) {
            if (this._lnkCancelMediaQuery) {
                $removeHandler(this._lnkCancelMediaQuery, "click", this._MediaQueryCancelDelegate);
            }
            delete this._MediaQueryCancelDelegate;
        }

        /*  this is the place to unbind/dispose the event handlers created in the initialize method */
        Telerik.Sitefinity.Modules.Pages.Web.UI.Fields.ResponsiveLayoutField.callBaseMethod(this, "dispose");
    },

    /* --------------------------------- public methods ---------------------------------- */

    /* --------------------------------- event handlers ---------------------------------- */

    _selectButtonMediaQueryClicked: function (sender, args) {
        this._selectMediaQueryDialog.dialog("open");
        this._dialogScrollToTop(this._selectMediaQueryDialog);
        return false;
    },

    _MediaQueryItemSelectorCloseHandler: function (sender, args) {
        this._selectMediaQueryDialog.dialog("close");
        this._resetUI();
        return false;
    },

    _MediaQueryDoneSelecting: function (sender, args) {
        var selectedMediaQueryOption = $('input[name=MediaQueryRules]:radio:checked').val();

        switch (selectedMediaQueryOption) {
            case "Inherit":
            case "None":
                this._setSelectedQueries.call(this, [], selectedMediaQueryOption);
                break;
            case "All":
            case "Selected":
                this._setSelectedQueries.call(this, this.get_MediaQuerySelectedItems(), selectedMediaQueryOption);
                break;
            default:
        }

    },

    _mediaQueryRulesButtonsChanged: function () {
        if ($(this).val() == "Selected") {
            $("#mediaQueryItemSelectorContainer").show();
        }
        else {
            $("#mediaQueryItemSelectorContainer").hide();
        }
    },

    /* --------------------------------- private methods --------------------------------- */
    _setSelectedQueries: function (queries, type) {
        var self = this;
        var data = {
            ItemId: this.get_itemId(),
            ItemType: this.get_itemType(),
            MediaQueries: queries,
            LinkType: type
        };

        $.ajax({
            url: this.get_serviceUrl() + "/mql/?provider=",
            type: "PUT",
            data: JSON.stringify({ "Item": data }),
            dataType: "json",
            contentType: 'application/json; charset=utf-8',
            success: function () {
                self._selectMediaQueryDialog.dialog("close");
                self._refreshSelectedLinkTypeLabel(self.get_itemType(), type);
                return false;
            }
        });
    },

    _dialogScrollToTop: function (dlg) {
        var scrollTopHtml = jQuery("html").eq(0).scrollTop();
        var scrollTopBody = jQuery("body").eq(0).scrollTop();
        var scrollTop = ((scrollTopHtml > scrollTopBody) ? scrollTopHtml : scrollTopBody) + 50;
        jQuery(dlg).parent().css({ "top": scrollTop, "position": "absolute" });
    },

    _resetUI: function () {
        var selectedLinkType = this.get_mediaQueryLinkType();
        if (selectedLinkType == "Selected") {
            $("#mediaQueryItemSelectorContainer").show();
        }
        var itemType = this.get_itemType();
        if (itemType == "Template") {
            //$('#selectedLinkTypeContainer').text("As set in base template");
            $("#pageInheritContainer").hide();
            $("#templateInheritContainer").show();
            if (selectedLinkType == "Inherit") {
                $("input:radio[id='templateInherit']").attr("checked", "checked");
            }
            else {
                $("input:radio[value='" + selectedLinkType + "']").attr("checked", "checked");
            }
        }
        else if (itemType == "Page") {
            //$('#selectedLinkTypeContainer').text("As set in template");
            $("#templateInheritContainer").hide();
            $("#pageInheritContainer").show();
            if (selectedLinkType == "Inherit") {
                $("input:radio[id='pageInherit']").attr("checked", "checked");
            }
            else {
                $("input:radio[value='" + selectedLinkType + "']").attr("checked", "checked");
            }
        }
    },
    _refreshSelectedLinkTypeLabel: function (itemType, type) {
        switch (type) {
            case "Inherit":
                if (itemType == "Template") {
                    $('#selectedLinkTypeContainer').text("As set in base template");
                }
                else if (itemType == "Page") {
                    $('#selectedLinkTypeContainer').text("As set in template");
                }
                break;
            case "None":
                $('#selectedLinkTypeContainer').text("No groups of rules are applied");
                break;
            case "All":
                $('#selectedLinkTypeContainer').text("All groups of rules are applied");
                break;
            case "Selected":
                $('#selectedLinkTypeContainer').text("Only selected groups of rules are applied");
                break;
            default:
        }
    },

    /* --------------------------------- properties -------------------------------------- */
    get_value: function () {
        return null;
    },

    set_value: function (value) {
        this._value = value;
    },

    get_selectButtonMediaQuery: function () {
        return this._selectButtonMediaQuery;
    },

    set_selectButtonMediaQuery: function (value) {
        this._selectButtonMediaQuery = value;
    },

    get_MediaQueryItemSelector: function () {
        return this._MediaQueryItemSelector;
    },

    set_MediaQueryItemSelector: function (value) {
        this._MediaQueryItemSelector = value;
    },

    get_MediaQueryBinder: function () {
        return this._MediaQueryItemSelector.get_binder();
    },

    get_lnkDoneMediaQuery: function () {
        return this._lnkDoneMediaQuery;
    },
    set_lnkDoneMediaQuery: function (value) {
        this._lnkDoneMediaQuery = value;
    },
    get_lnkCancelMediaQuery: function () {
        return this._lnkCancelMediaQuery;
    },
    set_lnkCancelMediaQuery: function (value) {
        this._lnkCancelMediaQuery = value;
    },

    get_MediaQueryItemSelector: function () {
        return this._MediaQueryItemSelector;
    },

    set_MediaQueryItemSelector: function (value) {
        this._MediaQueryItemSelector = value;
    },

    get_MediaQueryBinder: function () {
        return this._MediaQueryItemSelector.get_binder();
    },

    get_MediaQuerySelectedKeys: function () {
        return this._MediaQueryItemSelector.get_selectedKeys();
    },
    set_MediaQuerySelectedKeys: function (keys) {
        this._selectedKeys = keys;
    },

    get_MediaQuerySelectedItems: function () {
        return this._MediaQueryItemSelector.getSelectedItems();
    },
    set_MediaQuerySelectedItems: function (items) {
        this._MediaQuerySelectedItems = items;
        if (this._MediaQueryBinderBound) {
            this._MediaQueryItemSelector.bindSelector();
        }
    },
    get_itemId: function () {
        return this._itemId;
    },
    set_itemId: function (value) {
        this._itemId = value;
    },
    get_itemType: function () {
        return this._itemType;
    },
    set_itemType: function (value) {
        this._itemType = value;
    },
    get_serviceUrl: function () {
        return this._serviceUrl;
    },
    set_serviceUrl: function (value) {
        this._serviceUrl = value;
    },
    get_selectedKeys: function () {
        return this._selectedKeys;
    },
    set_selectedKeys: function (value) {
        this._selectedKeys = value;
    },
    get_mediaQueryLinkType: function () {
        return this._mediaQueryLinkType;
    },
    set_mediaQueryLinkType: function (value) {
        this._mediaQueryLinkType = value;
    }
};

Telerik.Sitefinity.Modules.Pages.Web.UI.Fields.ResponsiveLayoutField.registerClass("Telerik.Sitefinity.Modules.Pages.Web.UI.Fields.ResponsiveLayoutField", Telerik.Sitefinity.Web.UI.Fields.FieldControl);