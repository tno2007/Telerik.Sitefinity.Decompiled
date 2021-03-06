Type.registerNamespace("Telerik.Sitefinity.Modules.ResponsiveDesign.BasicSettings");
Telerik.Sitefinity.Modules.ResponsiveDesign.BasicSettings.ResponsiveDesignTransformationsBasicSettingsView = function (element) {
    Telerik.Sitefinity.Modules.ResponsiveDesign.BasicSettings.ResponsiveDesignTransformationsBasicSettingsView.initializeBase(this, [element]);
    this._grid = null;
    this._webServiceUrl = null;
    this._clientLabelManager = null;

    this._selectors = null;
    this._dataSource = null;
    this._lastQuery = null;
    this._pageSize = 10;

    this._gridDataBoundDelegate = null;
    this._onLoadDelegate = null;
    this._showEditWidnowButton = null;
    this._navTransformationsEditDialog = null;
    this._navTransformationsEditDialogCloseDelegate = null;
    this._confirmationDialog = null;
    this._deleteConfirmationDialog = null;
}

Telerik.Sitefinity.Modules.ResponsiveDesign.BasicSettings.ResponsiveDesignTransformationsBasicSettingsView.prototype =
 {
     /* --------------------  set up and tear down ----------- */

     initialize: function () {
         Telerik.Sitefinity.Modules.ResponsiveDesign.BasicSettings.ResponsiveDesignTransformationsBasicSettingsView.callBaseMethod(this, "initialize");
         this._selectors = {
             gridViewRowTemplate: "#transformationsGridRowTemplate"
         };

         // prevent memory leaks
         jQuery(this).on("unload", function (e) {
             jQuery.event.remove(this);
             jQuery.removeData(this);
         });

         this._gridDataBoundDelegate = Function.createDelegate(this, this._gridDataBoundHandler);

         this._onLoadDelegate = Function.createDelegate(this, this._onLoadHandler);
         Sys.Application.add_load(this._onLoadDelegate);

         if (this.get_showEditWidnowButton()) {
             this._showEditWindowClickDelegate = Function.createDelegate(this, this._showEditWindowClickHandler);
             $addHandler(this.get_showEditWidnowButton(), "click", this._showEditWindowClickDelegate);
         }

         this._navTransformationsEditDialogCloseDelegate = Function.createDelegate(this, this._navTransformationsEditDialogCloseHandler);
         this.get_navTransformationsEditDialog().add_close(this._navTransformationsEditDialogCloseDelegate);
     },

     dispose: function () {
         if (this.get_showEditWidnowButton()) {
             $removeHandler(this.get_showEditWidnowButton(), "click", this._showEditWindowClickDelegate);
         }
         if (this._showEditWindowClickDelegate) {
             delete this._showEditWindowClickDelegate;
         }

         if (this._gridDataBoundDelegate) {
             delete this._gridDataBoundDelegate;
         }

         if (this._onLoadDelegate) {
             Sys.Application.remove_load(this._onLoadDelegate);
             delete this._onLoadDelegate;
         }

         if (this._navTransformationsEditDialogCloseDelegate) {
             if (this.get_navTransformationsEditDialog()) {
                 this.get_navTransformationsEditDialog().remove_close(this._navTransformationsEditDialogCloseDelegate);
             }
             delete this._navTransformationsEditDialogCloseDelegate;
         }

         Telerik.Sitefinity.Modules.ResponsiveDesign.BasicSettings.ResponsiveDesignTransformationsBasicSettingsView.callBaseMethod(this, "dispose");
     },

     /* --------------------  public methods ----------- */

     initializeDataSource: function () {                                    
         this._dataSource = new kendo.data.DataSource({
             type: "json",
             transport: {
                 read: {
                     url: this.get_webServiceUrl() + '/nav_transformations/',
                     contentType: 'application/json; charset=utf-8',
                     type: "GET",
                     cache: false,
                     dataType: "json"
                 }
             },
             requestStart: function (e) {
                 jQuery('body').addClass('sfLoadingTransition');
             },
             change: function (e) {
                 jQuery('body').removeClass('sfLoadingTransition');
             },
             error: function (jqXHR, textStatus, errorThrown) {
                 jQuery('body').removeClass('sfLoadingTransition');
                 var errText;
                 if (jqXHR.responseText) {
                     errText = Telerik.Sitefinity.JSON.parse(jqXHR.responseText).Detail;
                 }
                 else {
                     errText = jqXHR.status;
                 }
                 alert(errText);
             }                          
         });
     },

     initializeGridView: function () {
         var that = this;
         jQuery(this.get_grid()).kendoGrid({
             dataSource: this._dataSource,
             rowTemplate: kendo.template(jQuery(this.getSelectors().gridViewRowTemplate).html()),
             scrollable: false,
             pageable: false,
             autoBind: true,
             dataBound: this._gridDataBoundDelegate
         }).delegate(".sfUnpublishItm", "click", function (e) {
             that._showPromptMessage(e, that._changeItemStatus, 'DeactivateNavTransformationSettingTitle', 'DeactivateNavTransformationSettingMessage');
         }).delegate(".sfPublishItm", "click", function (e) {
             that._changeItemStatus(e);
         }).delegate(".sfItemTitle", "click", function (e) {
             that._openNavTransformationDetails(e);
         }).delegate(".sfDeleteItm", "click", function (e) {
             that._showPromptMessage(e, that._deleteTransformation, 'DeleteNavTransformationSettingTitle', 'DeleteNavTransformationSettingMessage');
            
         });
         jQuery(this.get_grid()).show();
         jQuery("body").addClass("sfBlobStorageBasicSettings");
     },

     getSelectors: function () {
         return this._selectors;
     },

     fetchDataSource: function () {
         this._dataSource.read();
     },

     /* --------------------  events handlers ----------- */

     _onLoadHandler: function () {
         this.initializeDataSource();
         this.initializeGridView();
     },

     _gridDataBoundHandler: function (e) {         
         jQuery(".sfActionsMenu").kendoMenu({ animation: false, openOnClick: true });         
     },

     _deleteTransformation: function (e) {
         var anchor = e.target;
         var kendoDataItem = jQuery(this.get_grid()).data("kendoGrid").dataItem(jQuery(anchor).closest("tr"));

         var that = this;
         jQuery.ajax({
             type: 'DELETE',
             url: this.get_webServiceUrl() + String.format('/nav_transformations/{0}/', encodeURIComponent(kendoDataItem.Name)),
             contentType: "application/json",
             processData: false,
             success: function () { that.fetchDataSource(); }
         });
     },

     _showEditWindowClickHandler: function (e) {
         this.get_navTransformationsEditDialog().show();
     },     

     _navTransformationsEditDialogCloseHandler: function (sender, args) {
         var data = args.get_data();
         if (data) {
             this.fetchDataSource();
         }
     },

     /* --------------------  private methods ----------- */

     _showPromptMessage:function(e, actionToExecute, title, message) {
         var anchor = e.target;
         var kendoDataItem = jQuery(this.get_grid()).data("kendoGrid").dataItem(jQuery(anchor).closest("tr"));
         var usedInCount = kendoDataItem.UsedIn.length;
         if (usedInCount === 0) {
             if (title === "DeleteNavTransformationSettingTitle") //in case of delete
             {
                 var that = this;
                 that.get_deleteConfirmationDialog().set_message(that.get_clientLabelManager().getLabel('Labels', 'QuestionBeforeDeletingItem'));
                 var promptCallback = function (sender, args) {
                     if (args.get_commandName() === "delete") {
                         actionToExecute.call(that, e);
                     }
                 };

                 that.get_deleteConfirmationDialog().show_prompt(null, null, promptCallback);
             }
             else //in case of deactivate
                actionToExecute.call(this, e);
         }
         else {

             this.get_confirmationDialog().set_title(this.get_clientLabelManager().getLabel('Labels', title));
             this.get_confirmationDialog().set_message(String.format(this.get_clientLabelManager().getLabel('Labels', message), usedInCount));
             this.get_confirmationDialog().show_prompt();
         }
     },

     _changeItemStatus: function (e) {
         var anchor = e.target;
         var kendoDataItem = jQuery(this.get_grid()).data("kendoGrid").dataItem(jQuery(anchor).closest("tr"));

         var that = this;
         jQuery.ajax({
             type: 'PUT',
             url: this.get_webServiceUrl() + String.format('/nav_transformations/changeStatus/{0}/', encodeURIComponent(kendoDataItem.Name)),
             contentType: "application/json",
             processData: false,
             success: function () { that.fetchDataSource(); }
         });
     },

     _openNavTransformationDetails: function (e) {
         var anchor = e.target;
         var kendoDataItem = jQuery(this.get_grid()).data("kendoGrid").dataItem(jQuery(anchor).closest("tr"));
         var dataItem = this._getDataItem(kendoDataItem);

         //open the dialog here with dataItem
         this.get_navTransformationsEditDialog().show(dataItem);
     },

     _getDataItem: function (kendoDataItem) {
         var dataItem = {
             Name: kendoDataItem.Name,
             Css: kendoDataItem.Css,
             IsActive: kendoDataItem.IsActive,
             Title: kendoDataItem.Title
         };

         return dataItem;
     },

     /* -------------------- properties ---------------- */
     get_grid: function () {
         return this._grid;
     },
     set_grid: function (value) {
         this._grid = value;
     },
     get_webServiceUrl: function () {
         return this._webServiceUrl;
     },
     set_webServiceUrl: function (value) {
         this._webServiceUrl = value;
     },
     get_clientLabelManager: function () {
         return this._clientLabelManager;
     },
     set_clientLabelManager: function (value) {
         this._clientLabelManager = value;
     },
     get_showEditWidnowButton: function () {
         return this._showEditWidnowButton;
     },
     set_showEditWidnowButton: function (value) {
         this._showEditWidnowButton = value;
     },
     get_navTransformationsEditDialog: function () {
         return this._navTransformationsEditDialog;
     },
     set_navTransformationsEditDialog: function (value) {
         this._navTransformationsEditDialog = value;
     },
     get_confirmationDialog: function () {
         return this._confirmationDialog;
     },
     set_confirmationDialog: function (value) {
         this._confirmationDialog = value;
     },
     get_deleteConfirmationDialog: function () {
         return this._deleteConfirmationDialog;
     },
     set_deleteConfirmationDialog: function (value) {
         this._deleteConfirmationDialog = value;
     }
 };

Telerik.Sitefinity.Modules.ResponsiveDesign.BasicSettings.ResponsiveDesignTransformationsBasicSettingsView.registerClass("Telerik.Sitefinity.Modules.ResponsiveDesign.BasicSettings.ResponsiveDesignTransformationsBasicSettingsView", Sys.UI.Control);