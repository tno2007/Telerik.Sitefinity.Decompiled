Type.registerNamespace("Telerik.Sitefinity.Modules.Libraries.Web.UI");

Telerik.Sitefinity.Modules.Libraries.Web.UI.LibrarySelectorField = function (element) {

    Telerik.Sitefinity.Modules.Libraries.Web.UI.LibrarySelectorField.initializeBase(this, [element]);
    this._element = element;
    this._labelElement = null;
    this._selectElement = null;
    this._expandableExtenderId = null;
    this._expandableExtenderBehavior = null;
    this._binder = null;
    this._serviceBaseUrl = null;
    this._libraryType = null;
    this._selectedOptionText = null;
    this._binderDataBoundDelegate = null;
    this._changed = false;
    this._selectedLibraryId = null;
    this._handleControlFocusDelegate = null;
    this._handleControlBlurDelegate = null;
    this._handleSelectedChangeDelegate = null;
    this._provider = "";
}

Telerik.Sitefinity.Modules.Libraries.Web.UI.LibrarySelectorField.prototype =
{
    initialize: function () {
        if (this._handleControlFocusDelegate == null) {
            this._handleControlFocusDelegate = Function.createDelegate(this, this._focusHandler);
        }
        if (this._handleSelectedChangeDelegate == null) {
            this._handleSelectedChangeDelegate = Function.createDelegate(this, this._changeHandler);
        }
        if (this._handleControlBlurDelegate == null) {
            this._handleControlBlurDelegate = Function.createDelegate(this, this._blurHandler);
        }
        if (this._binderDataBoundDelegate == null) {
            this._binderDataBoundDelegate = Function.createDelegate(this, this._binderDataBound);
        }

        this.dataBind();

        $addHandler(this._element, "focus", this._handleControlFocusDelegate);
        $addHandler(this.get_selectElement(), "change", this._handleSelectedChangeDelegate);
        $addHandler(this.get_selectElement(), "blur", this._handleControlBlurDelegate);

        Telerik.Sitefinity.Modules.Libraries.Web.UI.LibrarySelectorField.callBaseMethod(this, "initialize");
    },

    dispose: function () {
        if (this._handleControlFocusDelegate) {
            if (this._element) {
                $removeHandler(this._element, "focus", this._handleControlFocusDelegate);
            }
            delete this._handleControlFocusDelegate;
        }

        if (this._handleSelectedChangeDelegate) {
            if (this.get_selectElement()) {
                $removeHandler(this.get_selectElement(), "change", this._handleSelectedChangeDelegate);
            }
            delete this._handleSelectedChangeDelegate;
        }

        this._labelElement = null;
        this._selectElement = null;

        Telerik.Sitefinity.Modules.Libraries.Web.UI.LibrarySelectorField.callBaseMethod(this, "dispose");
    },

    /* --------------------  public methods ----------- */

    dataBind: function () {
        if (this._binder != null) {
            this._binder.set_serviceBaseUrl(this._serviceBaseUrl);
            this._binder.get_urlParams()["itemType"] = this._libraryType;
            this._binder.get_urlParams()["provider"] = this._provider;
            this._binder.add_onDataBound(this._binderDataBoundDelegate);
            this._binder.DataBind();
        }
    },

    saveChanges: function (dataItem, successCallback, failureCallback, caller) {
        var clientManager = this._binder.get_manager();
        var serviceUrl = this._serviceBaseUrl + "parent/";
        var urlParams = [];
        urlParams['itemType'] = dataItem.ItemType;
        urlParams['parentItemType'] = this._libraryType;
        if (this._providerName != null) {
            urlParams['provider'] = this._binder.get_providerName;
        }

        // add global data keys, if any
        var keys = [];
        {
            keys.push(this._selectedLibraryId);
            keys.push(dataItem.Item.Id);
        }

        clientManager.InvokePut(serviceUrl, urlParams, keys, dataItem, successCallback, successCallback, caller);

        keys.pop();
    },

    isChanged: function () {
        return this._changed;
    },

    getSelectedParentId: function () {
        return this._selectedLibraryId;
    },

    reset: function () {
        this.set_value(null);
        this._changed = false;
        Telerik.Sitefinity.Modules.Libraries.Web.UI.LibrarySelectorField.callBaseMethod(this, "reset");
    },

    // Gets the value of the field control.
    get_value: function () {
        return this._value;
    },

    // Sets the value of the text field control depending on DisplayMode.
    set_value: function (value) {
        this._value = value;
        if (this._value != null && this._value != "") {
            jQuery(this._labelElement).text(value.Title.Value);
            this.get_selectElement().value = value.Id;
        }
        else {
            this._clearLabel();
        }
    },

    /* -------------------- events -------------------- */

    /* -------------------- event handlers ------------ */
    _focusHandler: function (e) {
        e.stopPropagation();

        var behavior = this._get_ExpandableExtenderBehavior();
        if (behavior != null)
            behavior.expand();
        this.get_selectElement().focus();
    },

    _changeHandler: function (e) {
        e.stopPropagation();

        var val = jQuery("#" + this.get_selectElement().id + " option:selected");
        this._selectedLibraryId = val.val();
        jQuery(this._labelElement).text(val.text());
        this._changed = true;

        //this.get_selectElement().selectedIndex = 0;
        this.get_selectElement().blur();
    },

    _blurHandler: function (e) {
        e.stopPropagation();

        var behavior = this._get_ExpandableExtenderBehavior();
        if (behavior != null && !behavior.get_originalExpandedState()) {
            behavior.collapse();
        }
    },

    _binderDataBound: function (sender, eventArgs) {

        var select = sender.get_target();
        var content = String.format('<option value="0" selected="selected">{0}</option>', this._selectedOptionText);
        var hasAddedDefaultItem = $("option:contains('" + this._selectedOptionText + "')", $(select)) != null;

        if (!hasAddedDefaultItem)
            $(select).prepend(content);
        select.selectedIndex = 0;
    },

    /* -------------------- private methods ----------- */

    _clearLabel: function () {
        if (this._labelElement != null) {
            this._labelElement.innerHTML = "";
        }
    },

    _get_ExpandableExtenderBehavior: function () {
        if (this._expandableExtenderBehavior) {
            return this._expandableExtenderBehavior;
        }
        this._expandableExtenderBehavior = Sys.UI.Behavior.getBehaviorByName(this._element, "ExpandableExtender");
        return this._expandableExtenderBehavior;
    },

    /* -------------------- properties ---------------- */

    // Gets the reference to the DOM element used to display the text box of the text field control.
    get_selectElement: function () {
        return this._selectElement;
    },

    // Sets the reference to the DOM element used to display the text box of the text field control.
    set_selectElement: function (value) {
        this._selectElement = value;
    },

    // Gets the reference to the DOM element used to display the label box of the text field control.
    get_labelElement: function () {
        return this._labelElement;
    },

    // Sets the reference to the DOM element used to display the label of the text field control.
    set_labelElement: function (value) {
        this._labelElement = value;
    },

    // gts the client binder
    get_binder: function () {
        return this._binder;
    },

    // sets the client binder
    set_binder: function (value) {
        this._binder = value;
    },

    // gets service Url
    get_serviceBaseUrl: function () {
        return this._serviceBaseUrl;
    },

    // sets service Url
    set_serviceBaseUrl: function (value) {
        this._serviceBaseUrl = value;
    },

    // gets library type
    get_libraryType: function () {
        return this._libraryType;
    },

    //  sets library type
    set_libraryType: function (value) {
        this._libraryType = value;
    },

    // Sets the position in the tabbing order
    // Overridden from field control
    set_tabIndex: function (value) {
        this._tabIndex = value;
        jQuery(this.get_selectElement()).attr("tabindex", value);
    },
    // Gets the provider name for the items to retrieve/edit.
    get_provider: function () {
        return this._provider;
    },

    // Sets the provider name for the items to retrieve/edit.
    set_provider: function (value) {
        this._provider = value;
    }
};
Telerik.Sitefinity.Modules.Libraries.Web.UI.LibrarySelectorField.registerClass("Telerik.Sitefinity.Modules.Libraries.Web.UI.LibrarySelectorField", Telerik.Sitefinity.Web.UI.Fields.FieldControl, Telerik.Sitefinity.Web.UI.IParentSelectorField);
