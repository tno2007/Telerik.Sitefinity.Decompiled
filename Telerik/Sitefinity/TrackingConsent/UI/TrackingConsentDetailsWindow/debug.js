Type.registerNamespace("Telerik.Sitefinity.TrackingConsent.UI");

Telerik.Sitefinity.TrackingConsent.UI.TrackingConsentDetailsWindow = function (element) {
    Telerik.Sitefinity.TrackingConsent.UI.TrackingConsentDetailsWindow.initializeBase(this, [element]);
}

Telerik.Sitefinity.TrackingConsent.UI.TrackingConsentDetailsWindow.prototype =
 {
     initialize: function () {
         Telerik.Sitefinity.TrackingConsent.UI.TrackingConsentDetailsWindow.callBaseMethod(this, "initialize");

         jQuery(document).ready(this.onReady.bind(this));

         this._doneButtonClickDelegate = Function.createDelegate(this, this._doneButtonClicked);
         $addHandler(this.get_doneButton(), "click", this._doneButtonClickDelegate);

         this._cancelButtonClickDelegate = Function.createDelegate(this, this._cancelButtonClicked);
         $addHandler(this.get_cancelButton(), "click", this._cancelButtonClickDelegate);

         this._changeButtonClickDelegate = Function.createDelegate(this, this._changeClicked);
         $addHandler(this.get_changeButton(), "click", this._changeButtonClickDelegate);

         this._fileExplorerLoadDelegate = Function.createDelegate(this, this._fileExplorerLoad);
         this._fileExplorer.add_load(this._fileExplorerLoadDelegate);
         this._fileExplorer.add_ajaxRequestEnd(this._fileExplorerLoadDelegate);

         this._availableFiles = [];
     },

     dispose: function () {
         if (this._cancelButtonClickDelegate) {
             $removeHandler(this.get_cancelButton(), "click", this._cancelButtonClickDelegate);
             delete this._cancelButtonClickDelegate;
         }

         if (this._doneButtonClickDelegate) {
             $removeHandler(this.get_doneButton(), "click", this._doneButtonClickDelegate);
             delete this._doneButtonClickDelegate;
         }

         if (this._changeButtonClickDelegate) {
             $removeHandler(this.get_changeButton(), "click", this._changeButtonClickDelegate);
             delete this._changeButtonClickDelegate;
         }

         if (this._fileExplorerLoadDelegate) {
             this._fileExplorer.remove_ajaxRequestEnd(this._fileExplorerLoadDelegate);
             this._fileExplorer.remove_load(this._fileExplorerLoadDelegate);
             delete this._fileExplorerLoadDelegate;
         }

         Telerik.Sitefinity.TrackingConsent.UI.TrackingConsentDetailsWindow.callBaseMethod(this, "dispose");
     },

     get_windowBody: function () {
         return this._windowBody;
     },
     set_windowBody: function (value) {
         this._windowBody = value;
     },
     get_doneButton: function () {
         return this._doneButton;
     },
     set_doneButton: function (value) {
         this._doneButton = value;
     },
     get_cancelButton: function () {
         return this._cancelButton;
     },
     set_cancelButton: function (value) {
         this._cancelButton = value;
     },
     get_changeButton: function () {
         return this._changeButton;
     },
     set_changeButton: function (value) {
         this._changeButton = value;
     },
     get_fileExplorer: function () {
         return this._fileExplorer;
     },
     set_fileExplorer: function (value) {
         this._fileExplorer = value;
     },

     onReady: function () {
         this._jWin = jQuery(this.get_windowBody());

         this._window = this._jWin.kendoWindow({
             title: false,
             visible: false,
             animation: false,
             actions: [],
             modal: true,
             minWidth: 550,
             appendTo: "form#aspnetForm"
         }).data("kendoWindow");

         this._templateSelector = this._jWin.find("#templateSelector").kendoWindow({
             title: false,
             visible: false,
             animation: false,
             actions: [],
             modal: true,
             appendTo: "form#aspnetForm"
         }).data("kendoWindow");

         $("#templateSelectorDone").click(this._templateSelectorDoneClicked.bind(this));
         $("#templateSelectorCancel").click(this._templateSelectorCancelClicked.bind(this));
     },

     open: function (createMode, consent, domainValidator, callback) {
         this._callback = callback;
         this._consent = consent;
         this._domainValidator = domainValidator;
         this._createMode = createMode;

         this._jWin.find('#addConsentTitle').toggle(createMode);
         this._jWin.find('#editConsentTitle').toggle(!createMode);
         this._jWin.find('#domainLabelContainer').toggle(consent.IsMaster);
         this._jWin.find('#domainInputContainer').toggle(!consent.IsMaster);

         this._jWin.find('#errorMessage').hide();

         this._dataItem = kendo.observable(jQuery.extend(true, {}, consent));
         kendo.bind(this._jWin, this._dataItem);

         this._validateDialog();

         this._window.center().open();
     },

     _validateDomain: function () {
         this._hasValidDomain = true;

         if (typeof (this._domainValidator) == "function") {
             var error = this._domainValidator(this._dataItem, this._consent);
             if (error) {
                 this._jWin.find('#errorMessage').text(error);
                 this._jWin.find('#errorMessage').show();
                 this._hasValidDomain = false;
             }
         }
     },

     _validateDialog: function () {
         var fileError = this._jWin.find('#fileError');
         fileError.hide();
         this._hasValidDialog = true;

         if (this._dataItem.ConsentDialog &&
             this._availableFiles.indexOf(this._dataItem.ConsentDialog) < 0) {
             fileError.show();
             this._hasValidDialog = false;
         }
     },

     _templateSelectorDoneClicked: function () {
         var selected = this._fileExplorer.get_selectedItem();
         if (selected && (selected.get_type() == 0)) {
             this._dataItem.set("ConsentDialog", "~" + selected.get_path());
         }

         this._validateDialog();
         this._templateSelector.close();
     },

     _templateSelectorCancelClicked: function () {
         this._templateSelector.close();
     },

     _fileExplorerLoad: function () {
         // update available files
         this._availableFiles = [];

         var tree = this._fileExplorer.get_tree();
         if (tree._nodeData &&
             tree._nodeData.length > 0 &&
             tree._nodeData[0].items) {
             for (var idx = 0; idx < tree._nodeData[0].items.length; idx++) {
                 this._availableFiles.push("~" + tree._nodeData[0].items[idx].value);
             }
         }

         if (!this._dataItem) return;

         // update tree view selection
         tree.unselectAllNodes();
        
         var allNodes = tree.get_allNodes();
         for (var idx = 0; idx < allNodes.length; idx++) {
             var node = allNodes[idx];
             if (this._dataItem.ConsentDialog == "~" + node.get_value()) {
                 node.select();
                 break;
             }
         }
                 
         // validate 
         this._validateDialog();
     },

     _normalizeInput: function () {
         if (this._createMode && this._dataItem.Domain) {
             this._dataItem.Domain = this._dataItem.Domain.trim();
         }
     },

     _doneButtonClicked: function () {

         this._normalizeInput();
         this._validateDomain();

         if (!this._hasValidDomain || !this._hasValidDialog) {
             return;
         }

         for (var prop in this._consent) {
             this._consent[prop] = this._dataItem[prop];
         }

         this._window.close();

         if (this._callback) {
             this._callback(this._dataItem);
         }
     },

     _cancelButtonClicked: function () {
         this._window.close();
     },

     _changeClicked: function () {
         this._fileExplorer.refresh();
         this._templateSelector.center().open();
     },
 };

Telerik.Sitefinity.TrackingConsent.UI.TrackingConsentDetailsWindow.registerClass("Telerik.Sitefinity.TrackingConsent.UI.TrackingConsentDetailsWindow", Sys.UI.Control);