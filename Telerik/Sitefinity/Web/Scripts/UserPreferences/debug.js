﻿Type.registerNamespace("Telerik.Sitefinity.Web.UI");
// Constructor

var _userPreferences = null;
function GetUserPreferences() {
    return _userPreferences;
}

Telerik.Sitefinity.Web.UI.UserPreferences = function () {

    Telerik.Sitefinity.Web.UI.UserPreferences.initializeBase(this);
    this._timeOffset = 0;
    this._timeZoneDisplayName = null;
    this._timeZoneId = null;
    this._userBrowserSettingsForCalculatingDates = true;
}

Telerik.Sitefinity.Web.UI.UserPreferences.prototype = {
    initialize: function () {
        Telerik.Sitefinity.Web.UI.UserPreferences.callBaseMethod(this, 'initialize');
        _userPreferences = this;
    },

    // Release resources before control is disposed.
    dispose: function () {
        Telerik.Sitefinity.Web.UI.UserPreferences.callBaseMethod(this, 'dispose');
    },

    sitefinityLocaleFormat: function (date, format) {
        if (this._userBrowserSettingsForCalculatingDates) {
            return date.localeFormat(format);
        }
        else
            return this.sitefinityToLocalDate(date).format(format);
    },




    sitefinityToLocalDate: function (date) {

        if (date == null) return date;

        if (this._userBrowserSettingsForCalculatingDates) {
            return date;
        }
        else {
            var ticks = parseInt(date.getTime()) + parseInt(this._timeOffset) + parseInt(date.getTimezoneOffset() * 60 * 1000);
            var newDate = new Date(ticks);
            return newDate;
        }
    },
    sitefinityToUniversalDate: function (date) {

        if (date == null) return date;

        if (this._userBrowserSettingsForCalculatingDates) {
            return date;
        }
        else {
            var ticks = parseInt(date.getTime()) - parseInt(this._timeOffset) - parseInt(date.getTimezoneOffset() * 60 * 1000);
            var newDate = new Date(ticks);
            return newDate;
        }
    },

    get_timeOffset: function () {
        return this._timeOffset;
    },
    set_timeOffset: function (value) {
        this._timeOffset = value;
    },
    get_timeZoneDisplayName: function () {
        return this._timeZoneDisplayName;
    },
    set_timeZoneDisplayName: function (value) {
        this._timeZoneDisplayName = value;
    },
    get_timeZoneId: function () {
        return this._timeZoneId;
    },
    set_timeZoneId: function (value) {
        this._timeZoneId = value;
    },
    get_userBrowserSettingsForCalculatingDates: function () {
        return this._userBrowserSettingsForCalculatingDates;
    },
    set_userBrowserSettingsForCalculatingDates: function (value) {
        this._userBrowserSettingsForCalculatingDates = value;
    }

}

Telerik.Sitefinity.Web.UI.UserPreferences.registerClass('Telerik.Sitefinity.Web.UI.UserPreferences', Sys.Component);

Date.prototype.sitefinityLocaleFormat = function (value) {
    return GetUserPreferences().sitefinityLocaleFormat(this, value);
};