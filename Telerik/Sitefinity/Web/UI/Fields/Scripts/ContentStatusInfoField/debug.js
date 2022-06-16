﻿/// <reference name="MicrosoftAjax.js"/>
/// <reference name="Telerik.Sitefinity.Resources.Scripts.jquery-1.6.3-vsdoc.js" assembly="Telerik.Sitefinity.Resources"/>
Type.registerNamespace("Telerik.Sitefinity.Web.UI.Fields");

Telerik.Sitefinity.Web.UI.Fields.ContentStatusInfoField = function (element) {
    Telerik.Sitefinity.Web.UI.Fields.ContentStatusInfoField.initializeBase(this, [element]);
    this._element = element;
    this._value = null;

    this._statusLabel = null;
    this._dateLabel = null;
    this._authorLabel = null;
    this._startDateLabel = null;
    this._stopDateLabel = null;
    this._viewResultsButton = null;
}

Telerik.Sitefinity.Web.UI.Fields.ContentStatusInfoField.prototype =
{
    initialize: function () {
        Telerik.Sitefinity.Web.UI.Fields.ContentStatusInfoField.callBaseMethod(this, "initialize");
        this.reset();
    },

    dispose: function () {
        Telerik.Sitefinity.Web.UI.Fields.ContentStatusInfoField.callBaseMethod(this, "dispose");
    },

    reset: function () {
        this.get_statusLabel().innerHTML = "";
        this.get_dateLabel().innerHTML = "";
        this.get_authorLabel().innerHTML = "";
        this.get_startDateLabel().innerHTML = "";
        this.get_stopDateLabel().innerHTML = "";
        this.get_viewResultsButton().href = "";
        this.get_startDateContainer().hide();
        this.get_stopDateContainer().hide();
        jQuery(this.get_viewResultsButton()).hide();
    },

    bind: function (){
        var value = this.get_value();
        if (value) {
            if (value.status)
                this.get_statusLabel().innerHTML = value.status;

            if (value.lastModified)
                this.get_dateLabel().innerHTML = value.lastModified.sitefinityLocaleFormat("dd MMMM, yyyy, hh:mm tt");

            if (value.owner)
                this.get_authorLabel().innerHTML = value.owner;

            if (value.startDate) {
                this.get_startDateLabel().innerHTML = value.startDate.sitefinityLocaleFormat("dd MMMM, yyyy, hh:mm tt");
                this.get_startDateContainer().show();

                if (value.reportUrl && value.status !== "NotStarted" && value.status !== "Scheduled") {
                    jQuery(this.get_viewResultsButton()).show();
                    this.get_viewResultsButton().href = value.reportUrl;
                }
            }

            if (value.stopDate && value.status === "Ended") {
                this.get_stopDateLabel().innerHTML = value.stopDate.sitefinityLocaleFormat("dd MMMM, yyyy, hh:mm tt");
                this.get_stopDateContainer().show();
            }            
        }
    },

    get_value: function(){
        return this._value;
    },
    set_value: function (value){
        this._value = value;
        this.bind();
    },

    get_startDateContainer: function(){
        return jQuery(this._element).find("#startDateContainer");
    },

    get_stopDateContainer: function () {
        return jQuery(this._element).find("#stopDateContainer");
    },

    get_statusLabel: function () {
        return this._statusLabel;
    },
    set_statusLabel: function (value) {
        this._statusLabel = value;
    },

    get_dateLabel: function () {
        return this._dateLabel;
    },
    set_dateLabel: function (value) {
        this._dateLabel = value;
    },

    get_authorLabel: function () {
        return this._authorLabel;
    },
    set_authorLabel: function (value) {
        this._authorLabel = value;
    },

    get_startDateLabel: function () {
        return this._startDateLabel;
    },
    set_startDateLabel: function (value) {
        this._startDateLabel = value;
    },

    get_stopDateLabel: function () {
        return this._stopDateLabel;
    },
    set_stopDateLabel: function (value) {
        this._stopDateLabel = value;
    },

    get_viewResultsButton: function () {
        return this._viewResultsButton;
    },
    set_viewResultsButton: function (value) {
        this._viewResultsButton = value;
    }
};

Telerik.Sitefinity.Web.UI.Fields.ContentStatusInfoField.registerClass("Telerik.Sitefinity.Web.UI.Fields.ContentStatusInfoField", Telerik.Sitefinity.Web.UI.Fields.FieldControl);