Type.registerNamespace("Telerik.Sitefinity.Modules.Comments.Web.UI.Backend");

var detailsView;

Telerik.Sitefinity.Modules.Comments.Web.UI.Backend.CommentsDetailView = function (element) {
    Telerik.Sitefinity.Modules.Comments.Web.UI.Backend.CommentsDetailView.initializeBase(this, [element]);

    this._commentsRestClient = null;
    this._serviceUrl = null;
    this._authorSidebar = null;
    this._isUpdated = false;
    this._deleteConfirmationDialog = null;
}

Telerik.Sitefinity.Modules.Comments.Web.UI.Backend.CommentsDetailView.prototype =
 {
     initialize: function () {
         Telerik.Sitefinity.Modules.Comments.Web.UI.Backend.CommentsDetailView.callBaseMethod(this, "initialize");

         var commentsRestClientCommon = new Telerik.Sitefinity.Modules.Comments.Web.UI.Frontend.CommentsRestClient(this.get_serviceUrl());
         this.set_commentsRestClient(commentsRestClientCommon);

         detailsView = this;

         this._ajaxCompleteDelegate = Function.createDelegate(this, this._ajaxCompleteHandler);
         this._ajaxFailDelegate = Function.createDelegate(this, this._ajaxFailHandler);

     },

     dispose: function () {
         if (this._ajaxCompleteDelegate) {
             delete this._ajaxCompleteDelegate;
         }

         if (this._ajaxFailDelegate) {
             delete this._ajaxFailDelegate;
         }

         Telerik.Sitefinity.Modules.Comments.Web.UI.Backend.CommentsDetailView.callBaseMethod(this, "dispose");
     },

     //invoked every time the form loads
     loadForm: function (itemId) {
         this.reset();
         if (this.get_authorSidebar())
             this.get_authorSidebar().reset();
         if (itemId) {
             this.get_commentsRestClient().getComment(itemId, jQuery.proxy(this.setDataItem, this));
         }
         jQuery("body").addClass("sfFormDialog");
     },

     //change the visibility of the options in the MoreActions button
     _setMoreActionsOptions: function (status) {
         for (var i = 0; i < this.get_moreActonsWidgets().length; i++) {
             var moreActionsWidget = this.get_moreActonsWidgets()[i];
             moreActionsWidget.showWidgetByName("publish");
             moreActionsWidget.showWidgetByName("markSpam");
             moreActionsWidget.showWidgetByName("hide");
             moreActionsWidget.showWidgetByName("delete");
             if (status === "Published") {
                 moreActionsWidget.hideWidgetByName("publish");
             }
             else if (status === "Spam") {
                 moreActionsWidget.hideWidgetByName("markSpam");
             }
             else if (status === "Hidden") {
                 moreActionsWidget.hideWidgetByName("hide");
             }
         }
     },

     //event handler for the click events from the buttons in the toolbar
     _widgetCommandHandler: function (sender, args) {
         var that = this;
         switch (args.get_commandName()) {
             case "save":
                 if (this.validate())
                     this.saveDataItem();
                 break;
             case "delete":
                 var promptCallback = function (sender, promptArgs) {
                     if (promptArgs.get_commandName() === "delete") {
                         that.deleteComment();
                     }
                 };

                 this.get_deleteConfirmationDialog().show_prompt(null, null, promptCallback);
                 break;
             case "markSpam":
                 this.markAsSpam();
                 this._isUpdated = true;
                 break;
             case "hide":
                 this.hide();
                 this._isUpdated = true;
                 break;
             case "publish":
                 this.publish();
                 this._isUpdated = true;
                 break;

         }

         Telerik.Sitefinity.Modules.Comments.Web.UI.Backend.CommentsDetailView.callBaseMethod(this, "_widgetCommandHandler", [sender, args]);
     },

     //saves current data item
     saveDataItem: function () {
         var item = this.get_dataItem();
         this._setLoadingViewVisible(true);
         var onSuccess = function () {
             dialogBase.closeAndRebind();
         };
         this.get_commentsRestClient().updateComment(item, onSuccess, jQuery.proxy(this._ajaxFailHandler, this), jQuery.proxy(this._ajaxCompleteHandler, this));
     },

     //marks current item as spam
     markAsSpam: function () {
         var item = this.get_dataItem();
         this._setLoadingViewVisible(true);
         var that = this;
         var onSuccess = function () {
             that.loadForm(item.Key);
         };
         item.Status = "Spam";
         this.get_commentsRestClient().updateComment(item, onSuccess, jQuery.proxy(this._ajaxFailDelegate, this), jQuery.proxy(this._ajaxCompleteDelegate, this));
     },

     //hide current item
     hide: function () {
         var item = this.get_dataItem();
         this._setLoadingViewVisible(true);
         var that = this;
         var onSuccess = function () {
             that.loadForm(item.Key);
         };
         item.Status = "Hidden";
         this.get_commentsRestClient().updateComment(item, onSuccess, jQuery.proxy(this._ajaxFailDelegate, this), jQuery.proxy(this._ajaxCompleteDelegate, this));
     },

     //publish current item
     publish: function () {
         var item = this.get_dataItem();
         this._setLoadingViewVisible(true);
         var that = this;
         var onSuccess = function () {
             that.loadForm(item.Key);
         };
         item.Status = "Published";
         this.get_commentsRestClient().updateComment(item, onSuccess, jQuery.proxy(this._ajaxFailDelegate, this), jQuery.proxy(this._ajaxCompleteDelegate, this));
     },

     deleteComment: function () {
         var item = this.get_dataItem();
         this._setLoadingViewVisible(true);
         var that = this;
         var onSuccess = function () {
             dialogBase.closeAndRebind();
         };
         this.get_commentsRestClient().deleteComment(item.Key, onSuccess, this._ajaxFailDelegate, this._ajaxCompleteDelegate);
     },

     //populate all fields
     setDataItem: function (data) {
         data["DateCreated"] = data["DateCreated"].format("f");
         this.set_dataItem(data);

         if (this.get_authorSidebar()) {
             this.get_authorSidebar().set_dataItem(data);
             this.get_authorSidebar().fillElements();
         }
         this._setMoreActionsOptions(data.status);
     },

     set_disableField: function (fieldName, shouldDisable) {
         var control = this.getFieldControlByName(fieldName);
         control.get_textElement().disabled = shouldDisable;
     },

     //validate fields
     validate: function () {
         var fieldControlsCount = this.get_fieldControls().length;
         var isValid = true;

         //Resets this flag in order to give focus to the first element that has a validation error
         Telerik.Sitefinity.Web.UI.Fields.FieldControl.isValidationMessagedFocused = false;

         while (fieldControlsCount--) {
             var fieldControl = this.get_fieldControls()[fieldControlsCount];
             var isCurrentValid;
             if (!(fieldControl.get_value() == "" && fieldControl.get_dataFieldName() == "Email"))
                 isCurrentValid = fieldControl.validate();
             isValid = isValid && isCurrentValid;
         }
         return isValid;
     },

     _ajaxCompleteHandler: function (jqXHR, textStatus) {
         this._setLoadingViewVisible(false);
     },

     _ajaxFailHandler: function (jqXHR, textStatus, errorThrown) {
         this.get_messageControl().showNegativeMessage(Telerik.Sitefinity.JSON.parse(jqXHR.responseText).Detail);
     },

     //shows loading message
     _setLoadingViewVisible: function (loading) {
         if (loading) {
             jQuery(this.get_loadingView()).show();
         }
         else
             jQuery(this.get_loadingView()).hide();
         this.showWidgetBars(!loading);
     },

     //closes the dialog
     close: function () {
         if (this._isUpdated)
             dialogBase.closeAndRebind();
         else
             dialogBase.close();
     },

     /* -------------------- properties ----------- */

     get_serviceUrl: function () {
         return this._serviceUrl;
     },
     set_serviceUrl: function (value) {
         this._serviceUrl = value;
     },

     get_clientLabelManager: function () {
         return this._clientLabelManager;
     },
     set_clientLabelManager: function (value) {
         this._clientLabelManager = value;
     },

     get_commentsRestClient: function () {
         return this._commentsRestClient;
     },
     set_commentsRestClient: function (value) {
         this._commentsRestClient = value;
     },

     get_authorSidebar: function () {
         return this._authorSidebar;
     },
     set_authorSidebar: function (value) {
         this._authorSidebar = value;
     },

     get_deleteConfirmationDialog: function () {
         return this._deleteConfirmationDialog;
     },
     set_deleteConfirmationDialog: function (value) {
         this._deleteConfirmationDialog = value;
     }
 };

Telerik.Sitefinity.Modules.Comments.Web.UI.Backend.CommentsDetailView.registerClass("Telerik.Sitefinity.Modules.Comments.Web.UI.Backend.CommentsDetailView", Telerik.Sitefinity.Web.UI.Backend.Elements.DetailViewControl);