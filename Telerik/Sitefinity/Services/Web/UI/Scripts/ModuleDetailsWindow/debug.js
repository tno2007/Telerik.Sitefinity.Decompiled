Type.registerNamespace("Telerik.Sitefinity.Services.Web.UI");

Telerik.Sitefinity.Services.Web.UI.ModuleDetailsWindow = function (element) {
    Telerik.Sitefinity.Services.Web.UI.ModuleDetailsWindow.initializeBase(this, [element]);

    this._moduleName = null;
    this._moduleDescription = null;
    this._moduleType = null;
    this._selectors = null;
    this._modulesManager = null;
    this._operation = null;
    this._dataItem = null;
    this._documentReadyDelegateModuleWindow = null;
    this._saveModuleDelegate = null;
}

Telerik.Sitefinity.Services.Web.UI.ModuleDetailsWindow.prototype = {

    /* ----------------------------- setup and teardown ----------------------------- */
    initialize: function () {
        Telerik.Sitefinity.Services.Web.UI.ModuleDetailsWindow.callBaseMethod(this, 'initialize');

        this._selectors = {
            moduleDetailsWindow: "#moduleDetailsWindow",
            backToModulesLink: "#backToModulesLink",
            moduleInstallTitle: "#moduleInstallTitleDiv",
            moduleEditTitle: "#moduleEditTitleDiv",
            startModuleOnFirstCall: "#startModuleOnFirstCall",
            startModuleOnApplicationStart: "#startModuleOnApplicationStart",
            doNotStartModule: "#doNotStartModule",
            cancellButton: "#cancelInstallModule",
            saveButton: "#installModuleSave",
            moduleTypeSection: "#moduleTypeDiv",
            moduleStartModeSection: "moduleStartModeDiv"
        };

        // prevent memory leaks
        $(this).on("unload", function (e) {
            jQuery.event.remove(this);
            jQuery.removeData(this);
        });

        this._documentReadyDelegateModuleWindow = Function.createDelegate(this, this._documentReadyHandlerModuleWindow);
        this._saveModuleDelegate = Function.createDelegate(this, this._saveModuleHandler);
        $(this.getSelectors().saveButton).click(this._saveModuleDelegate);

        var self = this;

        $(this.getSelectors().backToModulesLink).click(function () {
            self.close();
        });

        $(this.getSelectors().cancellButton).click(function () {
            self.close();
        });

        $(document).ready(this._documentReadyDelegateModuleWindow);
    },

    dispose: function () {

        if (this._documentReadyDelegateModuleWindow) {
            delete this._documentReadyDelegateModuleWindow;
        }

        if (this._saveModuleDelegate) {
            delete this._saveModuleDelegate;
        }

        Telerik.Sitefinity.Services.Web.UI.ModuleDetailsWindow.callBaseMethod(this, 'dispose');
    },

    close: function () {
        $(".sfError").remove();
        Telerik.Sitefinity.Services.Web.UI.ModuleDetailsWindow.callBaseMethod(this, 'close');
    },

    getSelectors: function () {
        return this._selectors;
    },

    show: function (manager, module) {

        this._modulesManager = manager;

        this._reset();

        if (module) {
            this._setEditMode(module);
        }
        else {
            this._setInstallMode();
        }

        this.get_kendoWindow().element.parent().addClass("sfMaximizedWindow");
        Telerik.Sitefinity.Services.Web.UI.ModuleDetailsWindow.callBaseMethod(this, "show");
        this.get_kendoWindow().maximize();
    },

    // ----------------------------------------- Event handlers ---------------------------------------

    _documentReadyHandlerModuleWindow: function () {

        $(this.getSelectors().moduleDetailsWindow).sf().form({ animation: false });
        $(this.getSelectors().moduleDetailsWindow).sf().form().formElement.parent().addClass("sfMaximizedWindow");

        this._bindControls();
    },

    _saveModuleHandler: function () {
        var isDynamicModule = this._dataItem.ModuleType == Telerik.Sitefinity.Services.Web.UI.ModuleType.Dynamic;

        if (!this._validateFields(isDynamicModule)) {
            return;
        }

        this._dataItem.Title = this.get_moduleName().get_value();
        this._dataItem.Name = this._dataItem.Title;
        this._dataItem.Description = this.get_moduleDescription().value;
        this._dataItem.Type = this.get_moduleType().get_value();
        this._dataItem.StartupType = $('input[name=startMode]:checked', '#moduleStartModeDiv').val();

        this._modulesManager._executeOperation(this._dataItem, this._operation);
        this.close();
    },

    // ----------------------------------------- Private methods ---------------------------------------

    _bindControls: function () {
        $(this.getSelectors().startModuleOnApplicationStart).val(Telerik.Sitefinity.Services.Web.UI.StartupType.OnApplicationStart);
        $(this.getSelectors().doNotStartModule).val(Telerik.Sitefinity.Services.Web.UI.StartupType.Disabled);
    },

    _validateFields: function (isDynamicModule) {
        var isValid = true;

        if (!this.get_moduleName().validate()) {
            isValid = false;
        }

        if (!isDynamicModule) {
            if (!this.get_moduleType().validate()) {
                isValid = false;
            }
        }

        return isValid;
    },

    _setInstallMode: function () {
        this._dataItem = new Object();
        this._operation = Telerik.Sitefinity.Services.Web.UI.ModuleOperation.Install;
        $(this.getSelectors().moduleEditTitle).hide();
    },

    _setEditMode: function (module) {
        this._dataItem = module;
        this._operation = Telerik.Sitefinity.Services.Web.UI.ModuleOperation.Edit;

        this._loadModuleData(module);

        $(this.get_moduleType()._element).find("input").prop("disabled", true);

        if (this._dataItem.ModuleType == Telerik.Sitefinity.Services.Web.UI.ModuleType.Dynamic) {
            $(this.getSelectors().moduleTypeSection).hide();
            $('div#' + this.getSelectors().moduleStartModeSection).hide();
        }

        $(this.getSelectors().moduleInstallTitle).hide();
    },

    _loadModuleData: function (module) {
        this.get_moduleName().set_value(module.Title);
        this.get_moduleDescription().value = module.Description;
        this.get_moduleType().set_value(module.Type);

        $('input[name=startMode]', '#moduleStartModeDiv').filter('[value=' + module.StartupType + ']').attr('checked', true);
    },

    _reset: function () {
        $(this.getSelectors().moduleEditTitle).show();
        $(this.getSelectors().moduleInstallTitle).show();
        $(this.getSelectors().moduleTypeSection).show();
        $('div#' + this.getSelectors().moduleStartModeSection).show();

        $(this.get_moduleType()._element).find("input").prop("disabled", false);

        this.get_moduleName().set_value("");
        this.get_moduleDescription().value = "";
        this.get_moduleType().set_value("");
        $('input[name=startMode]', '#moduleStartModeDiv').filter('[value=' + Telerik.Sitefinity.Services.Web.UI.StartupType.OnApplicationStart + ']').attr('checked', true);
    },

    // ----------------------------------------- Properties ---------------------------------------

    get_moduleName: function () {
        return this._moduleName;
    },
    set_moduleName: function (value) {
        this._moduleName = value;
    },

    get_moduleDescription: function () {
        return this._moduleDescription;
    },
    set_moduleDescription: function (value) {
        this._moduleDescription = value;
    },

    get_moduleType: function () {
        return this._moduleType;
    },
    set_moduleType: function (value) {
        this._moduleType = value;
    }
}

Telerik.Sitefinity.Services.Web.UI.ModuleDetailsWindow.registerClass('Telerik.Sitefinity.Services.Web.UI.ModuleDetailsWindow',
    Telerik.Sitefinity.Web.UI.Kendo.KendoWindow);