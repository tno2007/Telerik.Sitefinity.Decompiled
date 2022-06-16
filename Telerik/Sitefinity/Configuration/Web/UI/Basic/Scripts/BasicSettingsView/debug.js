﻿/// <reference name="MicrosoftAjax.js"/>
/// <reference name="Telerik.Sitefinity.Configuration.Web.UI.Basic.Scripts.FieldDisplayMode.js" assembly="Telerik.Sitefinity"/>

// global basic settings view variable
if (typeof basicSettingsView === "undefined") {
    basicSettingsView = null;
}


Type.registerNamespace("Telerik.Sitefinity.Configuration.Web.UI.Basic");

Telerik.Sitefinity.Configuration.Web.UI.Basic.BasicSettingsView = function (element) {
    this._binder = null;
    this._fieldControlIds = null;
    this._saveButton = null;
    this._dataItem = null;
    this._itemContext = null;
    this._message = null;
    this._loadingView = null;
    this._loadingViewClone = null;
    this._buttonsArea = null;
    this._clientLabelManager = null;
    this._basicSettingsSitePanel = null;
    this._siteId = null;
    this._action = null;

    this._loadDelegate = null;
    this._saveChangesDelegate = null;
    this._dataBindSuccessDelegate = null;
    this._binderSavedDelegate = null;
    this._binderErrorDelegate = null;
    this._binderEndProcessingDelegate = null;
    this._changeInheritanceDelegate = null;

    Telerik.Sitefinity.Configuration.Web.UI.Basic.BasicSettingsView.initializeBase(this, [element]);
}

Telerik.Sitefinity.Configuration.Web.UI.Basic.BasicSettingsView.prototype =
 {
     /* --------------------  set up and tear down ----------- */

     initialize: function () {
         Telerik.Sitefinity.Configuration.Web.UI.Basic.BasicSettingsView.callBaseMethod(this, "initialize");

         basicSettingsView = this;

         if (this._binder) {
             if (this._fieldControlIds) {
                 this._fieldControlIds = Sys.Serialization.JavaScriptSerializer.deserialize(this._fieldControlIds);
             }

             this._loadDelegate = Function.createDelegate(this, this._load);
             Sys.Application.add_load(this._loadDelegate);
             this._dataBindSuccessDelegate = Function.createDelegate(this, this._dataBindSuccess);

             this._binderSavedDelegate = Function.createDelegate(this, this._binderSaved);
             this._binder.add_onSaved(this._binderSavedDelegate);

             this._binderErrorDelegate = Function.createDelegate(this, this._binderError);
             this._binder.add_onError(this._binderErrorDelegate);

             this._binderEndProcessingDelegate = Function.createDelegate(this, this.binderEndProcessing);
             this._binder.add_onEndProcessing(this._binderEndProcessingDelegate);

             this._saveChangesDelegate = Function.createDelegate(this, this.saveChanges);
             $addHandler(this._saveButton, "click", this._saveChangesDelegate);

             if (this.get_basicSettingsSitePanel()) {
                 if (!this._changeInheritanceDelegate) {
                     this._changeInheritanceDelegate = Function.createDelegate(this, this._onChangeInheritance);
                 }
                 this.get_basicSettingsSitePanel().add_command(this._changeInheritanceDelegate);
             }
         }
     },

     dispose: function () {
         if (this._binder) {
             if (this._loadDelegate) {
                 Sys.Application.remove_load(this._loadDelegate);
                 delete this._loadDelegate;
             }
             if (this._saveChangesDelegate) {
                 $removeHandler(this._saveButton, "click", this._saveChangesDelegate);
                 delete this._saveChangesDelegate;
             }
             if (this._changeInheritanceDelegate) {
                 this.get_basicSettingsSitePanel().remove_command(this._changeInheritanceDelegate)
                 delete this._changeInheritanceDelegate;
             }

             this._binder.remove_onSaved(this._binderSavedDelegate);
             delete this._binderSavedDelegate;
             this._binder.remove_onError(this._binderErrorDelegate);
             delete this._binderErrorDelegate;
             this._binder.remove_onEndProcessing(this._binderEndProcessingDelegate);
             delete this._binderEndProcessingDelegate;
             delete this._dataBindSuccessDelegate;
         }
         Telerik.Sitefinity.Configuration.Web.UI.Basic.BasicSettingsView.callBaseMethod(this, "dispose");
     },

     /* --------------------  public methods ----------- */

     dataBind: function (global) {
         var clientManager = this._binder.get_manager();
         var serviceUrl = this._binder.get_serviceBaseUrl();
         var urlParams = [];
         var keys = [];
         if (!global && this.get_siteId())
             urlParams["siteId"] = this.get_siteId();
         clientManager.InvokeGet(serviceUrl, urlParams, keys, this._dataBindSuccessDelegate, this._dataBindFailure, this);
     },

     saveChanges: function () {
         if (!this.validate())
             return;
         this._binderStartProcessing();
         if (this.get_siteId()) {
             this._binder.get_urlParams()["siteId"] = this.get_siteId();
             if (this._action) {
                 this._binder.get_urlParams()["inheritanceState"] = this._action;
             }
         }
         this._binder.SaveChanges();
         this._scrollTop();
         this._action = null;
     },

     validate: function () {
         return true;
     },

     /* --------------------  events handlers ----------- */

     _load: function (sender, args) {
        this._binder.set_fieldControlIds(this._fieldControlIds);
        this.dataBind();
     },

     // called when data binding was successful
     _dataBindSuccess: function (sender, result) {
         var dataItem;
         if (result.hasOwnProperty("Item")) {
             this._itemContext = result;
             this._dataItem = result.Item;
             dataItem = result;

             if (this.get_basicSettingsSitePanel()) {
                 this.get_basicSettingsSitePanel().set_inherits(result.Inherit);
                 this.get_basicSettingsSitePanel().refresh();
                 if (result.Inherit && this._action == null) {
                     jQuery(".sfBasicSettingsWrp .sfButtonArea .sfLinkBtn").hide();
                 }
                 else {
                     jQuery(".sfBasicSettingsWrp .sfButtonArea .sfLinkBtn").show();
                 }
             }
         }
         else {
             this._dataItem = result;
             dataItem = { Item: result };
         }
         this._binder.BindItem(dataItem);
     },

     // called when data binding was not successful
     _dataBindFailure: function (error, caller) {
         alert(error.Detail);
     },

     // called when the binder saved changes successfully
     _binderSaved: function () {
         this._message.showPositiveMessage(this._clientLabelManager.getLabel('Labels', 'ChangesSuccessfullySaved'));
     },

     // called when the binder experience error
     _binderError: function (sender, args) {
         this._message.showNegativeMessage(args.get_error());
     },

     // called when the service is called
     _binderStartProcessing: function (sender, args) {
         if (this._loadingViewClone != null) {
             return;
         }
         this._loadingViewClone = this._loadingView.clone();
         //loading image added at toolbar container div
         var jba = $(this._buttonsArea);
         jba.append(this._loadingViewClone);
         jba.children().hide();
         this._loadingViewClone.show();
     },

     // called when the service returns result
     binderEndProcessing: function (sender, args) {
         if (this._loadingViewClone) {
             this._loadingViewClone.remove();
             this._loadingViewClone = null;
             $(this._buttonsArea).children().show();
         }
     },

     _onChangeInheritance: function (sender, args) {
         if (args._commandName == "changeInheritance") {
             if (args._commandArgument == "break") {
                 //TODO: enable edit controls

                 this.get_basicSettingsSitePanel().set_inherits(false);
                 jQuery(".sfBasicSettingsWrp .sfButtonArea .sfLinkBtn").show();
                 this._action = "break";
             }
             else if (args._commandArgument == "inherit") {
                 //reload the information from server with no site parameter
                 if (this._action == "break") {
                     this._action = null;
                 }
                 else {
                     this._action = "inherit";
                 }
                 this.dataBind(true);
                 this.get_basicSettingsSitePanel().set_inherits(true);
             }
             //refresh ui
             this.get_basicSettingsSitePanel().refresh();
         }
     },

     /* -------------------- events -------------------- */

     /* -------------------- private methods -------------------- */
     _scrollTop: function () {
         $('html, body').animate({ scrollTop: 0 }, 'slow');
     },

     /* -------------------- properties ---------------- */

     get_binder: function () {
         return this._binder;
     },
     set_binder: function (value) {
         this._binder = value;
     },

     get_fieldControlIds: function () {
         return this._fieldControlIds;
     },
     set_fieldControlIds: function (value) {
         this._fieldControlIds = value;
     },

     get_saveButton: function () {
         return this._saveButton;
     },
     set_saveButton: function (value) {
         this._saveButton = value;
     },

     get_message: function () {
         return this._message;
     },
     set_message: function (value) {
         this._message = value;
     },

     get_buttonsArea: function () {
         return this._buttonsArea;
     },
     set_buttonsArea: function (value) {
         this._buttonsArea = value;
     },

     get_loadingView: function () {
         return this._loadingView;
     },
     set_loadingView: function (value) {
         this._loadingView = $(value);
     },

     get_clientLabelManager: function () {
         return this._clientLabelManager;
     },
     set_clientLabelManager: function (value) {
         this._clientLabelManager = value;
     },

     get_basicSettingsSitePanel: function () {
         return this._basicSettingsSitePanel;
     },
     set_basicSettingsSitePanel: function (value) {
         this._basicSettingsSitePanel = value;
     },

     get_siteId: function () {
         return this._siteId;
     },
     set_siteId: function (value) {
         this._siteId = value;
     }
 };

Telerik.Sitefinity.Configuration.Web.UI.Basic.BasicSettingsView.registerClass("Telerik.Sitefinity.Configuration.Web.UI.Basic.BasicSettingsView", Sys.UI.Control);