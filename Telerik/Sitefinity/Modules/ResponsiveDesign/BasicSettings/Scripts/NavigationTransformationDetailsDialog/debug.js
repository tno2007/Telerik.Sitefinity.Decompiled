﻿Type.registerNamespace("Telerik.Sitefinity.Modules.ResponsiveDesign.BasicSettings");
Telerik.Sitefinity.Modules.ResponsiveDesign.BasicSettings.NavigationTransformationDetailsDialog = function (element) {
    Telerik.Sitefinity.Modules.ResponsiveDesign.BasicSettings.NavigationTransformationDetailsDialog.initializeBase(this, [element]);
    this._saveButton = null;
    this._cancelLink = null;
    this._titleTextField = null;
    this._transformationCssTextField = null;
    this._nameInCodeTextField = null;
    this._activeTransformationChoiceField = null;
    this._webServiceUrl = null;
    this._item = null;
    this._messageControl = null;
    this._loadingCounter = null;
    this._loadingView = null;
    this._ajaxFailDelegate = null;
    this._ajaxCompletedDelegate = null;
    this._originalName = null;
    this._dialogTitleLabel = null;
    this._clientLabelManager = null;
    this._proceedEditConfirmationDialog = null;
    this._codeMirror = null;
    this._confirmationDialog = null;
    this._buttonsPanel = null;
}

Telerik.Sitefinity.Modules.ResponsiveDesign.BasicSettings.NavigationTransformationDetailsDialog.prototype =
 {
     /* --------------------  set up and tear down ----------- */

     initialize: function () {
         Telerik.Sitefinity.Modules.ResponsiveDesign.BasicSettings.NavigationTransformationDetailsDialog.callBaseMethod(this, "initialize");

         this._cancelDelegate = Function.createDelegate(this, this._cancel);
         $addHandler(this.get_cancelLink(), "click", this._cancelDelegate);

         this._saveDelegate = Function.createDelegate(this, this._save);
         $addHandler(this.get_saveButton(), "click", this._saveDelegate);

         this._ajaxCompleteDelegate = Function.createDelegate(this, this._ajaxCompleteHandler);
         this._ajaxFailDelegate = Function.createDelegate(this, this._ajaxFailHandler);
      
         this._codeMirror = CodeMirror.fromTextArea(this.get_transformationCssTextField().get_textElement(), {
             mode: "css",
             lineNumbers: true,
             matchBrackets: true
         });
     },

     dispose: function () {
         Telerik.Sitefinity.Modules.ResponsiveDesign.BasicSettings.NavigationTransformationDetailsDialog.callBaseMethod(this, "dispose");

         if (this._cancelDelegate) {
             if (this.get_cancelLink()) {
                 $removeHandler(this.get_cancelLink(), "click", this._cancelDelegate);
             }
             delete this._cancelDelegate;
         }

         if (this._saveDelegate) {
             if (this.get_saveButton()) {
                 $removeHandler(this.get_saveButton(), "click", this._saveDelegate);
             }
             delete this._saveDelegate;
         }

         if (this._ajaxCompleteDelegate) {
             delete this._ajaxCompleteDelegate;
         }

         if (this._ajaxFailDelegate) {
             delete this._ajaxFailDelegate;
         }
     },

     /* --------------------  public methods ----------- */

     show: function (item) {
         Telerik.Sitefinity.Modules.ResponsiveDesign.BasicSettings.NavigationTransformationDetailsDialog.callBaseMethod(this, "show");
         this.reset();
         var that = this;
         var onSuccess = function (data, textStatus, jqXHR) {
            that._item = data;
            that._originalName = that._item.Name;
            that._updateUi();
         }

         //is edit mode
         if (item) {
             this.get_dialogTitleLabel().innerHTML = String.format(this.get_clientLabelManager().getLabel("Labels", "EditResponsiveDesignSetting"), item.Title);
             this._getItem(item.Name, onSuccess);
         }
         //is create mode
         else {
             this.get_dialogTitleLabel().innerHTML = this.get_clientLabelManager().getLabel("Labels", "AddResponsiveDesignSetting");
         }
     },

     reset: function () {
         this._item = null;
         this._originalName = null;
         this.get_titleTextField().reset();
         this.get_nameInCodeTextField().reset();
         this.get_codeMirror().setValue("");
         this.get_activeTransformationChoiceField().checked = true;
         this.get_messageControl().hide();
     },
     
     /* --------------------  events handlers ----------- */
     _save: function (sender, args) {
         var currentName = this.get_nameInCodeTextField().get_value();
         var currentIsActive = this.get_activeTransformationChoiceField().checked;
         if (this._item == null)
             this._proceedSaving();
         else {
             var usedInCount;
             if (this._item.UsedIn) {
                 usedInCount = this._item.UsedIn.length;
             }
             else {
                 usedInCount = 0;
             }

             if (currentName != this._originalName && usedInCount !== 0) {
                 this.get_proceedEditConfirmationDialog().set_message(String.format(this.get_clientLabelManager().getLabel('Labels', 'ProceedEditConfirmationDialogMessage'), usedInCount));
                 var that = this;
                 var promptCallback = function (sender, args) {
                     if (args.get_commandName() == "cancel") {
                         return;
                     }
                     else
                         that._proceedSaving();
                 };

                 this.get_proceedEditConfirmationDialog().show_prompt(null, null, promptCallback);
             }
             else if (usedInCount !== 0 && !currentIsActive) {
                 this._showDeactivateMessage(usedInCount, 'DeactivateNavTransformationSettingTitle', 'DeactivateNavTransformationSettingMessage');
             }
             else
                 this._proceedSaving();
         }

     },

     _cancel: function (sender, args) {
         this.close();
     },

     _ajaxCompleteHandler: function (jqXHR, textStatus) {
         this._setLoadingViewVisible(false);
     },

     _ajaxFailHandler: function (jqXHR, textStatus, errorThrown) {
         this.get_messageControl().showNegativeMessage(Telerik.Sitefinity.JSON.parse(jqXHR.responseText).Detail);
     },

     /* --------------------  private methods ----------- */
     _showDeactivateMessage: function (usedInCount, title, message) {
         this.get_confirmationDialog().set_title(this.get_clientLabelManager().getLabel('Labels', title));
         this.get_confirmationDialog().set_message(String.format(this.get_clientLabelManager().getLabel('Labels', message), usedInCount));
         this.get_confirmationDialog().show_prompt();
     },

     _isValid: function () {
         var isValid = true;
         if (this.get_titleTextField().validate() == false) {
             isValid = false;
         }

         if (this.get_nameInCodeTextField().validate() == false) {
             isValid = false;
         }
         
         return isValid;
     },

     _proceedSaving: function ()
     {
         if (this._isValid()) {
             var that = this;
             var onSuccess = function (data, textStatus, jqXHR) {
                 that.close(data);
             };
             this._updateCurrentItem();
             this._saveItem(onSuccess);
         }
     },

     _saveItem: function (onSuccess) {
         var name = this._originalName ? this._originalName : this._item.Name;
         var isNew = (this._originalName == null) ? "true" : "false";

         this._setLoadingViewVisible(true);
         jQuery.ajax({
             type: 'PUT',
             url: this.get_webServiceUrl() + String.format('{0}/?isNew={1}', encodeURIComponent(name), isNew),
             contentType: "application/json",
             data: Telerik.Sitefinity.JSON.stringify(this._item),
             cache: false,
             processData: false,
             success: onSuccess,
             error: this._ajaxFailDelegate,
             complete: this._ajaxCompleteDelegate
         });
     },

     _updateUi: function () {
         if (this._item) {
             this.get_titleTextField().set_value(this._item.Title);
             var itemCss = this._item.Css ? this._item.Css : "";
             this.get_codeMirror().setValue(itemCss);
             this.get_nameInCodeTextField().set_value(this._item.Name);
             this.get_activeTransformationChoiceField().checked = this._item.IsActive;
         }
     },

     _updateCurrentItem: function () {
         if (this._item == null) {
             this._item = {};
         }
        
         this._item.Name = this.get_nameInCodeTextField().get_value();
         this._item.Title = this.get_titleTextField().get_value();        
         this._item.Css = this.get_codeMirror().getValue();
         this._item.IsActive = this.get_activeTransformationChoiceField().checked;
         
     },

     _getItem: function (name, onSuccess) {

         this._setLoadingViewVisible(true);
         jQuery.ajax({
             type: 'GET',
             url: this.get_webServiceUrl() + String.format('{0}/', encodeURIComponent(name)),
             contentType: "application/json",
             processData: false,
             cache: false,
             success: onSuccess,
             error: this._ajaxFailDelegate,
             complete: this._ajaxCompleteDelegate
         });
     },


     _setLoadingViewVisible: function (loading) {
         if (loading) {
             this._loadingCounter++;
         }
         else {
             if (this._loadingCounter > 0) {
                 this._loadingCounter--;
             }
         }
         if (this._loadingCounter > 0) {
             jQuery(this.get_buttonsPanel()).hide();
             jQuery(this.get_loadingView()).show();
         }
         else {
             jQuery(this.get_loadingView()).hide();
             jQuery(this.get_buttonsPanel()).show();
         }
     },

     /* -------------------- properties ---------------- */
     get_saveButton: function () {
         return this._saveButton;
     },
     set_saveButton: function (value) {
         this._saveButton = value;
     },
     get_cancelLink: function () {
         return this._cancelLink;
     },
     set_cancelLink: function (value) {
         this._cancelLink = value;
     },
     get_titleTextField: function () {
         return this._titleTextField;
     },
     set_titleTextField: function (value) {
         this._titleTextField = value;
     },
     get_transformationCssTextField: function () {
         return this._transformationCssTextField;
     },
     set_transformationCssTextField: function (value) {
         this._transformationCssTextField = value;
     },
     get_nameInCodeTextField: function () {
         return this._nameInCodeTextField;
     },
     set_nameInCodeTextField: function (value) {
         this._nameInCodeTextField = value;
     },
     get_activeTransformationChoiceField: function () {
         return this._activeTransformationChoiceField;
     },
     set_activeTransformationChoiceField: function (value) {
         this._activeTransformationChoiceField = value;
     },
     get_webServiceUrl: function () {
         return this._webServiceUrl;
     },
     set_webServiceUrl: function (value) {
         this._webServiceUrl = value;
     },
     get_loadingView: function () {
         return this._loadingView;
     },
     set_loadingView: function (value) {
         this._loadingView = value;
     },
     get_messageControl: function () {
         return this._messageControl;
     },
     set_messageControl: function (value) {
         this._messageControl = value;
     },
     get_dialogTitleLabel: function () {
         return this._dialogTitleLabel;
     },
     set_dialogTitleLabel: function (value) {
         this._dialogTitleLabel = value;
     },
     get_clientLabelManager: function () {
         return this._clientLabelManager;
     },
     set_clientLabelManager: function (value) {
         this._clientLabelManager = value;
     },
     get_proceedEditConfirmationDialog: function () {
         return this._proceedEditConfirmationDialog;
     },
     set_proceedEditConfirmationDialog: function (value) {
         this._proceedEditConfirmationDialog = value;
     },
     get_codeMirror: function () {
         return this._codeMirror;
     },
     set_codeMirror: function (value) {
         this._codeMirror = value;
     },
     get_confirmationDialog: function () {
         return this._confirmationDialog;
     },
     set_confirmationDialog: function (value) {
         this._confirmationDialog = value;
     },
     get_buttonsPanel: function () {
         return this._buttonsPanel;
     },
     set_buttonsPanel: function (value) {
         this._buttonsPanel = value;
     }
 };

Telerik.Sitefinity.Modules.ResponsiveDesign.BasicSettings.NavigationTransformationDetailsDialog.registerClass("Telerik.Sitefinity.Modules.ResponsiveDesign.BasicSettings.NavigationTransformationDetailsDialog", Telerik.Sitefinity.Web.UI.Kendo.KendoWindow);