﻿Type.registerNamespace("Telerik.Sitefinity.Modules.Newsletters.Web.UI.Forms");

var mailingListSubscribersDialog = null;
var mailingListId = null;

Telerik.Sitefinity.Modules.Newsletters.Web.UI.Forms.MailingListSubscribersDialog = function (element) {
    this._webServiceUrl = null;
    this._subscribersGrid = null;
    this._subscribersBinder = null;
    this._subscribersSearchBox = null;
    this._titleLabel = null;
    this._titleText = null;
    this._listTitle = null;

    this._defaultSortExpression = null;
    this._defaultSearchFields = null;

    this._dynamicLists = null;

    Telerik.Sitefinity.Modules.Newsletters.Web.UI.Forms.MailingListSubscribersDialog.initializeBase(this, [element]);
}

Telerik.Sitefinity.Modules.Newsletters.Web.UI.Forms.MailingListSubscribersDialog.prototype = {

    // set up 
    initialize: function () {
        mailingListSubscribersDialog = this;        

        var that = this;

        $("#sourcesSelect").change(function () {
            // 'this' is the select element
            that._sourcesSelectionChange(this);
        });

        Telerik.Sitefinity.Modules.Newsletters.Web.UI.Forms.MailingListSubscribersDialog.callBaseMethod(this, "initialize");
    },

    // tear down
    dispose: function () {
        Telerik.Sitefinity.Modules.Newsletters.Web.UI.Forms.MailingListSubscribersDialog.callBaseMethod(this, "dispose");
    },

    /* *************************** public methods *************************** */

    // This method is used to load the dialog into the UI.
    loadDialog: function (listId, listTitle, allSubscribersCount, dynamicLists) {
        mailingListId = listId;
        this._listTitle = listTitle;

        this._titleLabel.innerHTML = String.format(this._titleText, allSubscribersCount, this._listTitle)

        // Copy the array of fields only the first time the dialog is loaded.
        if (!this._defaultSearchFields) {
            this._defaultSearchFields = this._subscribersSearchBox._binderSearch._searchFields.slice();
        }

        // Copy the sort expression only the first time the dialog is loaded.
        if (!this._defaultSortExpression) {
            this._defaultSortExpression = this._subscribersBinder.get_sortExpression();
        }

        this.reset();

        this._subscribersSearchBox.set_binder(this._subscribersBinder);
        var serviceBaseUrl = String.format(this._webServiceUrl, listId);
        this._subscribersBinder.set_serviceBaseUrl(serviceBaseUrl);

        this._dynamicLists = dynamicLists;

        this._setDefaultSearchFields();

        this._subscribersBinder.set_sortExpression(this._defaultSortExpression);

        this._subscribersBinder.DataBind();

        dialogBase.setWndHeight(630);
        dialogBase.setWndWidth(480);
    },

    reset: function () {
        this._subscribersBinder.set_urlParams({});
        this._resetFiltering();
    },

    /* *************************** private methods *************************** */    

    _sourcesSelectionChange: function (selectElement) {
        this._resetFiltering();

        var selectedValue = $(selectElement).val();

        if (selectedValue === 'static') {
            delete this._subscribersBinder.get_urlParams()['dynamicListKey'];

            this._subscribersBinder.set_sortExpression(this._defaultSortExpression);
            this._setDefaultSearchFields();
        }
        else {
            this._subscribersBinder.get_urlParams()['dynamicListKey'] = selectedValue;

            // The dynamic list type may not have the field used in the default expression.
            this._subscribersBinder.set_sortExpression('');

            var selectedDynamicList = this._getSelectedDynamicList(selectedValue);

            this._setDynamicSearchFields(selectedDynamicList);

            this._setDynamicSortExpression(selectedDynamicList);
        }

        this._subscribersBinder.DataBind();        
    },

    _setDefaultSearchFields: function () {
        var fields = this._subscribersSearchBox._binderSearch._searchFields;
        fields.length = 0;
        Array.prototype.push.apply(fields, this._defaultSearchFields);
    },

    _setDynamicSearchFields: function (selectedDynamicList) {
        var firstName = this._getDynamicFieldName(selectedDynamicList.FirstNameMappedField);
        var lastName = this._getDynamicFieldName(selectedDynamicList.LastNameMappedField);
        var email = this._getDynamicFieldName(selectedDynamicList.EmailMappedField);

        var fields = this._subscribersSearchBox._binderSearch._searchFields;
        fields.length = 0;
        fields.push(email);
        fields.push(firstName);
        fields.push(lastName);
    },

    _setDynamicSortExpression: function (selectedDynamicList) {
        var firstName = this._getDynamicFieldName(selectedDynamicList.FirstNameMappedField);
        this._subscribersBinder.set_sortExpression(firstName);
    },

    _getSelectedDynamicList: function (dynamicListKey) {
        return this._dynamicLists.filter(function (list) {
            return list.ListKey === dynamicListKey;
        })[0];
    },

    _getDynamicFieldName: function (field) {
        return field.replace(/[\{\}\|]/g, '').split('.')[1];
    },

    _resetFiltering: function () {
        this._subscribersSearchBox.get_searchBox().clearSearchBox();
        this._subscribersBinder.set_filterExpression("");
        this._subscribersBinder.set_isFiltering(false);
    },

    /* *************************** properties *************************** */
    get_webServiceUrl: function () {
        return this._webServiceUrl;
    },
    set_webServiceUrl: function (value) {
        this._webServiceUrl = value;
    },
    get_subscribersGrid: function () {
        return this._subscribersGrid;
    },
    set_subscribersGrid: function (value) {
        this._subscribersGrid = value;
    },
    get_subscribersBinder: function () {
        return this._subscribersBinder;
    },
    set_subscribersBinder: function (value) {
        this._subscribersBinder = value;
    },
    get_subscribersSearchBox: function () {
        return this._subscribersSearchBox;
    },
    set_subscribersSearchBox: function (value) {
        this._subscribersSearchBox = value;
    },
    get_titleLabel: function () {
        return this._titleLabel;
    },
    set_titleLabel: function (value) {
        this._titleLabel = value;
    }
};

Telerik.Sitefinity.Modules.Newsletters.Web.UI.Forms.MailingListSubscribersDialog.registerClass('Telerik.Sitefinity.Modules.Newsletters.Web.UI.Forms.MailingListSubscribersDialog', Telerik.Sitefinity.Web.UI.AjaxDialogBase);
