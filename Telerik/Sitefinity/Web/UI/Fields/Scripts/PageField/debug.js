Type.registerNamespace("Telerik.Sitefinity.Web.UI.Fields");

Telerik.Sitefinity.Web.UI.Fields.PageField = function (element) {
    Telerik.Sitefinity.Web.UI.Fields.PageField.initializeBase(this, [element]);

    this._element = element;
    this._commandBar = null;
    this._commandBarCommandDelegate = null;
    this._openSelector = null;
    this._openSelectorCommandDelegate = null;
    this._selectorWindow = null;
    this._selectorWindowDialog = null;
    this._pageSelector = null;
    this._pageUrl = null;
    this._pageTitle = null;
    this._pageStatus = null;
    this._pageLiveUrl = null;
    this._pageCulture = null;
    this._pageDetailsContainer = null;
    this._providerName = null;
    this._webServiceUrl = null;
    this._showSelectedPageSuccessDelegate = null;
    this._selectedPage = null;
    this._originalValue = null;
    this._originalValueSet = 0;
    this._sortExpression = null;
    this._hiddenContent = null;

    this._selectorDialogZIndex = 10501;
}
Telerik.Sitefinity.Web.UI.Fields.PageField.prototype =
{
    /* -------------------- set up and tear down ----------- */
    initialize: function () {
        Telerik.Sitefinity.Web.UI.Fields.PageField.callBaseMethod(this, "initialize");

        // delegates
        if (this._commandBarCommandDelegate == null)
            this._commandBarCommandDelegate = Function.createDelegate(this, this.commandBar_Command);
        if (this._showSelectedPageSuccessDelegate == null)
            this._showSelectedPageSuccessDelegate = Function.createDelegate(this, this._showSelectedPageSuccess);

        if (this._commandBar != null)
            this._commandBar.add_command(this._commandBarCommandDelegate);
        if (this._openSelector) {
            if (this._openSelectorCommandDelegate == null)
                this._openSelectorCommandDelegate = Function.createDelegate(this, this.openSelectorCommand);
            $addHandler(this._openSelector, "click", this._openSelectorCommandDelegate);
        }
        var that = this;
        this._selectorWindowDialog = jQuery(this._selectorWindow).dialog({
            autoOpen: false,
            modal: true,
            width: 355,
            height: "auto",
            closeOnEscape: true,
            resizable: false,
            draggable: false,
            classes: {
                "ui-dialog": "sfSelectorDialog sfZIndexXL"
            }
        });
    },
    dispose: function () {
        if (this._commandBarCommandDelegate != null) {
            this._commandBar.remove_command(this._commandBarCommandDelegate);
            delete this._commandBarCommandDelegate;
        }
        if (this._openSelectorCommandDelegate != null)
            delete this._openSelectorCommandDelegate;

        if (this._openSelector)
            $clearHandlers(this._openSelector);

        this._commandBar = null;

        Telerik.Sitefinity.Web.UI.Fields.PageField.callBaseMethod(this, "dispose");
    },
    /* --------- Public Methods ------------ */
    reset: function () {
        if (this.get_pageUrl()) {
            this.get_pageUrl().innerHTML = "";
            jQuery(this.get_pageUrl()).addClass("sfDisplayNone");
        } else if (this.get_pageDetailsContainer()) {
            jQuery(this.get_pageDetailsContainer()).addClass("sfDisplayNone");
            var emptyString = "";
            this._setPageDetails(emptyString, emptyString, emptyString, emptyString, emptyString);
        }

        this._selectedPage = null;
        this._originalValueSet = 0;
        this.set_value(null);

        Telerik.Sitefinity.Web.UI.Fields.PageField.callBaseMethod(this, "reset");
    },
    bindPageSelector: function () {
        //binds the page selector only if it is not already bound when selector is load 
        if (this.get_sortExpression())
            this.get_pageSelector().get_treeBinder().set_sortExpression(this.get_sortExpression());

        if (!this._pageSelector.get_bindOnLoad()) {
            if (this._culture) {
                var cultureIsChanged = this._pageSelector.get_culture() != this._culture;
                if (cultureIsChanged) {
                    this._pageSelector.set_culture(this._culture);
                    this._pageSelector.set_uiCulture(this._culture);
                    var languageSelector = this._pageSelector.get_languageSelector();
                    if (languageSelector && languageSelector._dropDownList) {
                        languageSelector._dropDownList.value = this._culture;
                    }
                }
            }
            else {
                this._pageSelector.dataBind();
            }
        }
    },
    /* --------- Event Handlers ------------ */
    commandBar_Command: function (sender, args) {
        switch (args.get_commandName()) {
            case "save":
                this._saveChanges();
                this._pageSelector.clearSelection();
                this._close();
                break;
            case "cancel":
                this._pageSelector.clearSelection();
                this._close();
                break;
        }
    },
    openSelectorCommand: function () {
        if (this._value && this._value != Telerik.Sitefinity.getEmptyGuid()) {
            this._setSelectorValue(this._value);
        }
        this.bindPageSelector();
        //        var oWnd = this._selectorWindow;
        //        oWnd.show();
        //        oWnd.set_visibleTitlebar(true);
        //        Telerik.Sitefinity.centerWindowHorizontally(oWnd);

        // Bugfix http://teampulse.telerik.com/view#item/248229
        // If we are inside a simple widget designer, we hide all of the current 
        // content when showing the selector and revert after we close the selector.
        var body = jQuery("body");
        if (body.hasClass("sfDesignerSimpleMode") || (body.hasClass("sfSelectorDialog") && !body.hasClass("sfShowModalInSelector")))
            this._hiddenContent = jQuery("body>div:visible:not(.RadAjax)");
        else
            this._hiddenContent = jQuery();

        this._selectorWindowDialog.dialog("open");
        jQuery(this._selectorWindowDialog).parent().css({ "top": this._dialogScrollTop() });
        this._hiddenContent = this._hiddenContent.add(".ui-dialog-titlebar:visible");
        if (jQuery("body").hasClass("sfSelectorDialog"))
            this._hiddenContent = this._hiddenContent.add("form:visible");

        this._hiddenContent.hide();

        this._selectorOpenedHandler();
    },
    /* ------------ Private Methods -------------- */
    _saveChanges: function () {
        var item = this._pageSelector.get_selectedItem();
        this._selectedPage = item;
        if (item) {
            this.set_value(item.Id);
        }
        else {
            this.set_value(null);
        }
    },
    /* calculates top position of selector dialog */
    _dialogScrollTop: function () {
        var scrollTopHtml = jQuery("html").eq(0).scrollTop();
        var scrollTopBody = jQuery("body").eq(0).scrollTop();
        var scrollTop = ((scrollTopHtml > scrollTopBody) ? scrollTopHtml : scrollTopBody) + 50;
        return scrollTop;
    },
    _valueChangedHandler: function () {
        if (this._value && this._value != Telerik.Sitefinity.getEmptyGuid()) {
            this._setOpenSelectorText(true);
            if (this._selectedPage)
                this._setPageProps(this._selectedPage);
            else
                this._showSelectedPage();
        }
        else {
            this._setOpenSelectorText(false);
            this._clearPageProps();
        }
        Telerik.Sitefinity.Web.UI.Fields.PageField.callBaseMethod(this, "_valueChangedHandler");
    },
    _close: function (context) {
        //this._selectorWindow.close(context);
        this._selectorWindowDialog.dialog("close");
        if (this._hiddenContent)
            this._hiddenContent.show();
        this._selectorClosedHandler();
    },
    _showSelectedPage: function () {
        var pageId = this._value;
        var clientManager = new Telerik.Sitefinity.Data.ClientManager();
        var urlParams = {};
        urlParams["filter"] = String.format("Id == ({0})", pageId);
        urlParams["root"] = this.get_pageSelector().get_rootNodeId();
        urlParams["provider"] = this.get_providerName();
        var keys = {};
        if (this._culture) {
            clientManager.set_culture(this._culture);
            clientManager.set_uiCulture(this._culture);
        }
        clientManager.InvokeGet(this._webServiceUrl, urlParams, keys, this._showSelectedPageSuccessDelegate, this._showSelectedPageFailure, this);
    },
    _showSelectedPageSuccess: function (sender, result) {
        if (result && result.Items.length > 0) {
            var item = result.Items[0];
            this._selectedPage = item;
            this._setPageProps(item);
        }
        else {
            this._value = null;
        }
    },
    _showSelectedPageFailure: function (sender, result) {
        alert(sender.Detail);
    },
    _setSelectorValue: function (value) {
        if (value) {
            this._pageSelector.set_selectedItems([{ Id: value }]);
            this._pageSelector._onload();
        }
    },
    _setPageProps: function (item) {
        var title = "";
        if (item.TitlesPath) {
            title = item.TitlesPath;
        }
        else {
            title = item.Title.Value ? item.Title.Value : item.Title;
        }
        if (this.get_pageUrl()) {
            jQuery(this._pageUrl).text(title).removeClass("sfDisplayNone");
        } else if (this.get_pageDetailsContainer()) {
            jQuery(this.get_pageDetailsContainer()).removeClass("sfDisplayNone");
            var culture = this.get_culture() ? this.get_culture() : "";
            this._setPageDetails(title, culture, item.StatusText, item.Status, item.PageLiveUrl);
        }
    },
    _clearPageProps: function () {
        if (this.get_pageUrl()) {
            jQuery(this._pageUrl).html("").attr("href", "").addClass("sfDisplayNone");
        } else if (this.get_pageDetailsContainer()) {
            jQuery(this.get_pageDetailsContainer()).addClass("sfDisplayNone");
            var emptyString = "";
            this._setPageDetails(emptyString, emptyString, emptyString, emptyString, emptyString);
        }
    },
    _setPageDetails: function (title, culture, statusText, status, liveUrl) {
        this.get_pageTitle().innerText = title;
        
        if (culture) {
            this.get_pageCulture().innerHTML = culture;
            jQuery(this.get_pageCulture()).removeClass("sfDisplayNone");
        }

        this.get_pageStatus().innerHTML = statusText;
        this.get_pageLiveUrl().href = liveUrl;
        var titleParent = jQuery(this.get_pageTitle()).parent();
        if (titleParent) {
            titleParent.attr('class', '');
            titleParent.addClass("sfItemTitle");
            if (status) {
                titleParent.addClass("sf" + status.toLowerCase());
            }
        }
    },
    _setOpenSelectorText: function (valueSelected) {
        var text = valueSelected ? this._resources.Change : this._resources.SelectPage;
        jQuery(this._openSelector).find(".sfLinkBtnIn").text(text);
    },
    _selectorOpenedHandler: function () {
        if (typeof this.get_events == 'function') {
            var h = this.get_events().getHandler('selectorOpened');
            if (h) h(this, Sys.EventArgs.Empty);
            return Sys.EventArgs.Empty;
        }
    },
    _selectorClosedHandler: function () {
        if (typeof this.get_events == 'function') {
            var h = this.get_events().getHandler('selectorClosed');
            if (h) h(this, Sys.EventArgs.Empty);
            return Sys.EventArgs.Empty;
        }
    },
    /* ---------- Properties ------- */
    get_element: function () {
        return this._element;
    },

    get_selectedPage: function(){
        return this._selectedPage;
    },

    // Gets the value of the field control.
    get_value: function () {
        // if the current value is null, i.e. cleared, and the original value is not null, i.e. a guid
        // this prevents unnecessary setting empty guids to all content
        if (!this._value && this._originalValue) {
            return Telerik.Sitefinity.getEmptyGuid();
        }
        return this._value;
    },
    // Sets the value of the field control.
    set_value: function (value) {
        if (this._originalValueSet == 1) { // skips the call from set_properties() of asp.net framework //TODO rework this
            this._originalValue = value;
        }
        this._originalValueSet++;
        this._value = value;
        this._valueChangedHandler();
    },
    get_commandBar: function () {
        return this._commandBar;
    },
    set_commandBar: function (value) {
        this._commandBar = value;
    },
    get_openSelector: function () {
        return this._openSelector;
    },
    set_openSelector: function (value) {
        this._openSelector = value;
    },
    get_selectorWindow: function () {
        return this._selectorWindow;
    },
    set_selectorWindow: function (value) {
        this._selectorWindow = value;
    },
    get_pageSelector: function () {
        return this._pageSelector;
    },
    set_pageSelector: function (value) {
        this._pageSelector = value;
    },
    get_pageUrl: function () {
        return this._pageUrl;
    },
    set_pageUrl: function (value) {
        this._pageUrl = value;
    },
    get_pageTitle: function () {
        return this._pageTitle;
    },
    set_pageTitle: function (value) {
        this._pageTitle = value;
    },
    get_pageStatus: function () {
        return this._pageStatus;
    },
    set_pageStatus: function (value) {
        this._pageStatus = value;
    },
    get_pageLiveUrl: function () {
        return this._pageLiveUrl;
    },
    set_pageLiveUrl: function (value) {
        this._pageLiveUrl = value;
    },
    get_pageCulture: function () {
        return this._pageCulture;
    },
    set_pageCulture: function (value) {
        this._pageCulture = value;
    },
    get_pageDetailsContainer: function () {
        return this._pageDetailsContainer;
    },
    set_pageDetailsContainer: function (value) {
        this._pageDetailsContainer = value;
    },
    get_providerName: function () {
        return this._providerName;
    },
    set_providerName: function (value) {
        this._providerName = value;
    },
    get_webServiceUrl: function () {
        return this._webServiceUrl;
    },
    set_webServiceUrl: function (value) {
        this._webServiceUrl = value;
    },
    get_resources: function () {
        return this._resources;
    },
    set_resources: function (value) {
        this._resources = value;
    },

    get_selectorWindowDialog: function () {
        return this._selectorWindowDialog;
    },

    add_selectorOpened: function (delegate) {
        this.get_events().addHandler('selectorOpened', delegate);
    },

    remove_selectorOpened: function (delegate) {
        this.get_events().removeHandler('selectorOpened', delegate);
    },

    add_selectorClosed: function (delegate) {
        this.get_events().addHandler('selectorClosed', delegate);
    },

    remove_selectorClosed: function (delegate) {
        this.get_events().removeHandler('selectorClosed', delegate);
    },

    get_sortExpression: function () {
        return this._sortExpression;
    },

    set_sortExpression: function (value) {
        this._sortExpression = value;
    },

    get_selectorDialogZIndex: function () {
        return this._selectorDialogZIndex;
    },
    set_selectorDialogZIndex: function (value) {
        this._selectorDialogZIndex = value;
    }
};
Telerik.Sitefinity.Web.UI.Fields.PageField.registerClass("Telerik.Sitefinity.Web.UI.Fields.PageField", Telerik.Sitefinity.Web.UI.Fields.FieldControl);
