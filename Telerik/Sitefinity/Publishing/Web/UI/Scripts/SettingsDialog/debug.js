Type.registerNamespace("Telerik.Sitefinity.Publishing.Web.UI");
// ------------------------------------------------------------------------
// Class SettingsDialog
// ------------------------------------------------------------------------
Telerik.Sitefinity.Publishing.Web.UI.SettingsDialog = function (element) {
    Telerik.Sitefinity.Publishing.Web.UI.SettingsDialog.initializeBase(this, [element]);

    //storage
    this._multiPage = null;
    this._pipeSelector = null;

    this._designersMap = null;
    this._currentDesigner = null;
    this._editMode = false;
    this._selectorArea = null;
    this._btnCancelMappingSettings = null;
    this._btnAcceptMappingSettings = null;
    this._mappingArea = null;
    this._mappingControl = null;
    this._newPublishingPointDefaultTitle = null;

    this._designerInitialized = false;
    this._settingsContextKey = null;
    this._settingsOriginalData = null;
    this._doneEditingHandler = null;
    this._windowLoaded = false;
    this._commandBar = null;
    this._loadDelegate = null;

    Telerik.Sitefinity.Publishing.Web.UI.SettingsDialog.instance = this;
}

Telerik.Sitefinity.Publishing.Web.UI.SettingsDialog.prototype = {
    initialize: function () {
        Telerik.Sitefinity.Publishing.Web.UI.SettingsDialog.callBaseMethod(this, "initialize");
        // delegates
        this._loadDelegate = Function.createDelegate(this, this._load);
        Sys.Application.add_load(this._loadDelegate);

        this._commandBarCommandDelegate = Function.createDelegate(this, this._handleCommandBarCommand);
        this._pipeSelectorValueChangedDelegate = Function.createDelegate(this, this._pipeSelectorValueChanged);
        this._openMappingSettingsButtonClickedDelegate = Function.createDelegate(this, this._openMappingSettingsButtonClicked);
        this._acceptMappingSettingsDelegate = Function.createDelegate(this, this._acceptMappingSettings);
        this._cancelMappingSettingsDelegate = Function.createDelegate(this, this._cancelMappingSettings);

        // events
        if (this._commandBar != null)
            this._commandBar.add_command(this._commandBarCommandDelegate);
        $addHandler(this._btnAcceptMappingSettings, "click", this._acceptMappingSettingsDelegate);
        $addHandler(this._btnCancelMappingSettings, "click", this._cancelMappingSettingsDelegate);
        this._pipeSelector.add_valueChanged(this._pipeSelectorValueChangedDelegate);

        // initialization
        this._setCurrentDesigner(this._getDesignerByIndex(0));
        if (this._editMode) {
            this._hideElement(this.get_selectorArea());
        }
        else {
            // uncomment if first should be displayed by default
            this.get_pipeSelector().set_selectedChoicesIndex(0);
            this.get_multiPage().set_selectedIndex(1);
            this.get_multiPage().addCssClass("sfHideTitle");
        }
        window.settingsDialog = this;
    },

    dispose: function () {
        if (this._loadDelegate) {
            Sys.Application.remove_load(this._loadDelegate);
            delete this._loadDelegate;
        }

        var element = this.get_element();

        // unsubscribe from events
        $clearHandlers(element);
        $clearHandlers(this._btnCancelMappingSettings);
        $clearHandlers(this._btnAcceptMappingSettings);

        // delete delegates
        this._clearCurrentDesignerEvents();
        if (this._commandBarCommandDelegate != null)
            this._commandBar.remove_command(this._commandBarCommandDelegate);
        this._acceptMappingSettingsDelegate = null;
        this._cancelMappingSettingsDelegate = null;

        // remove references to html elements
        this._commandBar = null;
        this._commandBarCommandDelegate = null;
        this._currentDesigner = null;
        this._btnCancelMappingSettings = null;
        this._btnAcceptMappingSettings = null;
        Telerik.Sitefinity.Publishing.Web.UI.SettingsDialog.callBaseMethod(this, "dispose");
    },

    /* -------------------- Public methods ------------ */

    saveChanges: function () {
        var designer = this._getCurrentDesigner();
        var isValid = designer.validate();
        if (!isValid) {
            this.resizeToContent();
            return false;
        }
        designer.applyChanges();
        var data = designer.get_controlData();
        // because of IE iframe bugs we don't want to pass functions 
        // when this iframe's content is reloaded, all objects created here are "freed"
        // which means that their functions throw exceptions when invoked

        //var serialized = Sys.Serialization.JavaScriptSerializer.serialize(data);
        var serialized = Telerik.Sitefinity.JSON.stringify(data);
        if (this._doneEditingHandler) {
            this._doneEditingHandler(serialized, this._settingsContextKey);
        }
        return true;
    },

    showSettings: function (settingsData, doneHandler, settingsContext) {
        this._settingsContextKey = settingsContext;
        this._doneEditingHandler = doneHandler;
        this._settingsOriginalData = settingsData;
        if (this._editMode) {
            this._initializeDesigner(settingsData);
        }
        else {
            this._getCurrentDesigner().refreshUI();
        }
        this.resizeToContent();
    },

    /* -------------------- Event handlers ------------ */

    _handleCommandBarCommand: function (/*sender, args*/) {
        var args = arguments[1];
        switch (args.get_commandName()) {
            case "save":
                if (this.saveChanges())
                    this.close();
                break;
            case "cancel":
                this.close();
                break;
            default:
                break;
        }
    },

    _load: function () {
        dialogBase.resizeToContent();
        if ((jQuery.browser.safari || jQuery.browser.chrome) && !dialogBase._dialog.isMaximized()) {
            jQuery("body").addClass("sfOverflowHiddenX");
        }
    },

    _pipeSelectorValueChanged: function (/*pipeSelector, emptyArgs*/) {
        var multiPageOffset = this._editMode ? 0 : 1;
        var index = this.get_pipeSelector().get_selectedChoicesIndex()[0];
        this.get_multiPage().set_selectedIndex(index + multiPageOffset);
        this._setCurrentDesigner(this._getDesignerByIndex(index));
        this._getCurrentDesigner().refreshUI();
        this.resizeToContent();
    },

    _openMappingSettingsButtonClicked: function (/*domEvent*/) {
        var data = this._getCurrentDesigner().get_controlData();
        var mappingControl = this.get_mappingControl();

        var pipe = data.pipe;
        var title;
        var destFields;
        var sourceFields;
        var publishingPointName = pipe.PublishingPointName;
        var publishingPointDefinition = this._settingsOriginalData.getDefinitionFunc();
        if (typeof publishingPointName != "string") {
            publishingPointName = this._newPublishingPointDefaultTitle;
        }

        if (pipe.IsInbound) {
            // pipe -> publishing point
            title = publishingPointName + " <- " + pipe.PipeName;
            destFields = publishingPointDefinition;
            sourceFields = pipe.Definition;
        }
        else {
            // publishing point -> pipe
            title = pipe.PipeName + " <- " + publishingPointName;
            destFields = pipe.Definition;
            sourceFields = publishingPointDefinition;
        }

        mappingControl.set_title(title);
        mappingControl.dataBind(destFields, sourceFields, data.pipe.MappingSettings);

        this._showMappingArea();
        this._hideElement(this._getCurrentDesigner().get_openMappingSettingsButton());
        this.resizeToContent();
    },

    _acceptMappingSettings: function (/*domEvent*/) {
        var data = this._getCurrentDesigner().get_controlData();
        var mappingControl = this.get_mappingControl();
        data.pipe.MappingSettings = mappingControl.get_mappingSettings();
        this._getCurrentDesigner().set_controlData(data);

        this._hideMappingArea();
        this._showElement(this._getCurrentDesigner().get_openMappingSettingsButton());
        this.resizeToContent();
    },

    _cancelMappingSettings: function (/*domEvent*/) {
        var data = this._getCurrentDesigner().get_controlData();
        var mappingControl = this.get_mappingControl();
        data.pipe.MappingSettings = mappingControl.get_originalMappingSettings();
        this._hideMappingArea();
        this._showElement(this._getCurrentDesigner().get_openMappingSettingsButton());
        this.resizeToContent();
    },

    /* -------------------------- Private methods ----------------------------------- */

    _initializeDesigner: function (data) {
        if (!this._designerInitialized) {
            this._getCurrentDesigner().set_controlData(data);
            this._designerInitialized = true;
        }
        this.resizeToContent();
    },

    _getDesignerByIndex: function (indexToGet) {
        var counter = 0;
        for (var propertyName in this._designersMap) {
            if (counter == indexToGet) return $find(this._designersMap[propertyName]);
            counter++;
        }
        return null;
    },

    _setCurrentDesigner: function (designer) {
        this._clearCurrentDesignerEvents();
        var btn = designer.get_openMappingSettingsButton();
        //        if (this._editMode) {
        //            this._showElement(btn);
        //        }
        //        else {
        //            this._hideElement(btn);
        //        }
        $addHandler(btn, "click", this._openMappingSettingsButtonClickedDelegate);
        this._currentDesigner = designer;
    },

    _clearCurrentDesignerEvents: function () {
        var current = this._getCurrentDesigner();
        if (typeof current !== "undefined" && current != null && typeof current.get_openMappingSettingsButton === "function") {
            var btn = current.get_openMappingSettingsButton();
            $removeHandler(btn, "click", this._openMappingSettingsButtonClickedDelegate);
        }
    },

    _getCurrentDesigner: function () {
        return this._currentDesigner;
    },

    _hideMappingArea: function () {
        var mappingElement = this.get_mappingArea();
        this._hideElement(mappingElement);
    },

    _showMappingArea: function () {
        var mappingElement = this.get_mappingArea();
        this._showElement(mappingElement);

        if (jQuery.browser.safari) {
            jQuery("body").removeClass("sfOverflowHiddenX");
        }
    },

    _showElement: function (element) {
        if (Sys.UI.DomElement.containsCssClass(element, "sfDisplayNone")) {
            Sys.UI.DomElement.removeCssClass(element, "sfDisplayNone");
        }
    },

    _hideElement: function (element) {
        if (!Sys.UI.DomElement.containsCssClass(element, "sfDisplayNone")) {
            Sys.UI.DomElement.addCssClass(element, "sfDisplayNone");
        }
    },

    /* -------------------------- Properties ---------------------------------------- */
    get_commandBar: function () {
        return this._commandBar;
    },
    set_commandBar: function (value) {
        this._commandBar = value;
    },

    get_multiPage: function () {
        return this._multiPage;
    },
    set_multiPage: function (value) {
        this._multiPage = value;
    },

    get_pipeSelector: function () { return this._pipeSelector; },
    set_pipeSelector: function (value) { this._pipeSelector = value; },


    get_designersMap: function () { return this._designersMap; },
    set_designersMap: function (value) { this._designersMap = value; },

    get_selectorArea: function () { return this._selectorArea; },
    set_selectorArea: function (val) { this._selectorArea = val; },
    get_mappingControl: function () { return this._mappingControl; },
    set_mappingControl: function (val) { this._mappingControl = val; },
    get_mappingArea: function () { return this._mappingArea; },
    set_mappingArea: function (val) { this._mappingArea = val; },
    get_btnCancelMappingSettings: function () { return this._btnCancelMappingSettings; },
    set_btnCancelMappingSettings: function (val) { this._btnCancelMappingSettings = val; },
    get_btnAcceptMappingSettings: function () { return this._btnAcceptMappingSettings; },
    set_btnAcceptMappingSettings: function (val) { this._btnAcceptMappingSettings = val; }
}

Telerik.Sitefinity.Publishing.Web.UI.SettingsDialog.registerClass("Telerik.Sitefinity.Publishing.Web.UI.SettingsDialog", Telerik.Sitefinity.Web.UI.AjaxDialogBase);
if (typeof (Sys) !== 'undefined') Sys.Application.notifyScriptLoaded();


