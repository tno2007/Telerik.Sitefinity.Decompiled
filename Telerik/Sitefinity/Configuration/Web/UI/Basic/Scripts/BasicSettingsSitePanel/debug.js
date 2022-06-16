Type.registerNamespace("Telerik.Sitefinity.Configuration.Web.UI.Basic");

Telerik.Sitefinity.Configuration.Web.UI.Basic.BasicSettingsSitePanel = function (element) {
    Telerik.Sitefinity.Configuration.Web.UI.Basic.BasicSettingsSitePanel.initializeBase(this, [element]);

    this._globalContextQueryStringParamName = null;
    this._globalContextQueryStringParamValueTrue = null;
    this._siteName = null;
    this._specificSettingsLabel = null;
    this._settingsStatusLabel = null;
    this._changeInheritanceButton = null;
    this._inherits = null;
    this._clientLabelManager = null;

    this._changeInheritanceDelegate = null;
    this._documentReadyDelegate = null;

    this._commandName = null;
    this._commandArgument = null;
}

Telerik.Sitefinity.Configuration.Web.UI.Basic.BasicSettingsSitePanel.prototype = {

    /* ----------------------------- setup and teardown ----------------------------- */
    initialize: function () {
        Telerik.Sitefinity.Configuration.Web.UI.Basic.BasicSettingsSitePanel.callBaseMethod(this, 'initialize');

        if (this.get_changeInheritanceButton() != null) {
            this._changeInheritanceDelegate = Function.createDelegate(this, this._changeInheritance);
            $addHandler(this.get_changeInheritanceButton(), "click", this._changeInheritanceDelegate);
        }

        this._documentReadyDelegate = Function.createDelegate(this, this._documentReadyHandler);
        $(document).ready(this._documentReadyDelegate);
    },

    dispose: function () {
        Telerik.Sitefinity.Configuration.Web.UI.Basic.BasicSettingsSitePanel.callBaseMethod(this, 'dispose');

        if (this._changeInheritanceDelegate) {
            if (this.get_changeInheritanceButton())
                $removeHandler(this.get_changeInheritanceButton(), "click", this._changeInheritanceDelegate);

            delete this._changeInheritanceDelegate;
        }

        if (this._documentReadyDelegate) {
            delete this._documentReadyDelegate;
        }
    },

    // ----------------------------------------- Event handlers ---------------------------------------

    _changeInheritance: function () {
        if (this.get_changeInheritanceButton()) {
            this.set_commandName('changeInheritance');
            if (this.get_inherits())
                this.set_commandArgument('break');
            else
                this.set_commandArgument('inherit');

            var commandEventArgs = new Telerik.Sitefinity.UI.CommandEventArgs(this.get_commandName(), this.get_commandArgument());
            var h = this.get_events().getHandler('command');
            if (h) h(this, commandEventArgs);
        }
    },

    _documentReadyHandler: function () {
        this.refresh();
    },

    _enableSettingsInputs: function (flag) {
        var fields = jQuery(".sfBasicSettingsWrp select, .sfBasicSettingsWrp input[type='text'], .sfBasicSettingsWrp input[type='radio'], .sfBasicSettingsWrp input[type='checkbox']");
        var linkBtns = jQuery(".sfBasicSettingsWrp .sfSettingsSection .sfLinkBtn");
        var enableFields = flag;
        if (!flag) enableFields = false;

        fields.each(function () {
            jQuery(this).prop('disabled', enableFields);
        });
        linkBtns.each(function () {
            jQuery(this).toggle(!enableFields);
        });
    },

    /* ------------------------------------ Public Methods ----------------------------------- */

    refresh: function () {
        var statusInfoText;
        var changeInheritanceText;
        if (this.get_inherits()) {
            statusInfoText = this.get_clientLabelManager().getLabel('Labels', 'InheritedAllSitesSettingsText');
            changeInheritanceText = this.get_clientLabelManager().getLabel('Labels', 'BreakInheritance');

            $(this.get_specificSettingsLabel()).show();
            this._enableSettingsInputs(true);
        }
        else {
            statusInfoText = this.get_clientLabelManager().getLabel('Labels', 'BasicSettingsStatusPerSite').replace("{0}", this.get_siteName());
            changeInheritanceText = this.get_clientLabelManager().getLabel('Labels', 'InheritAllSitesSettings');

            $(this.get_specificSettingsLabel()).hide();
            this._enableSettingsInputs(false);
        }

        $(this.get_settingsStatusLabel()).text(statusInfoText).show();

        $(this.get_changeInheritanceButton()).text(changeInheritanceText).show();
    },


    add_command: function (handler) {
        this.get_events().addHandler('command', handler);
    },
    remove_command: function (handler) {
        this.get_events().removeHandler('command', handler);
    },
    // ----------------------------------------- Properties ---------------------------------------

    get_commandName: function () {
        return this._commandName;
    },
    set_commandName: function (value) {
        if (this._commandName != value) {
            this._commandName = value;
            this.raisePropertyChanged('commandName');
        }
    },

    get_commandArgument: function () {
        return this._commandArgument;
    },
    set_commandArgument: function (value) {
        if (this._commandArgument != value) {
            this._commandArgument = value;
            this.raisePropertyChanged('commandArgument');
        }
    },

    get_globalContextQueryStringParamName: function () {
        return this._globalContextQueryStringParamName;
    },
    set_globalContextQueryStringParamName: function (value) {
        this._globalContextQueryStringParamName = value;
    },

    get_globalContextQueryStringParamValueTrue: function () {
        return this._globalContextQueryStringParamValueTrue;
    },
    set_globalContextQueryStringParamValueTrue: function (value) {
        this._globalContextQueryStringParamValueTrue = value;
    },

    get_siteName: function () {
        return this._siteName;
    },
    set_siteName: function (value) {
        this._siteName = value;
    },

    get_specificSettingsLabel: function () {
        return this._specificSettingsLabel;
    },
    set_specificSettingsLabel: function (value) {
        this._specificSettingsLabel = value;
    },

    get_settingsStatusLabel: function () {
        return this._settingsStatusLabel;
    },
    set_settingsStatusLabel: function (value) {
        this._settingsStatusLabel = value;
    },

    get_changeInheritanceButton: function () {
        return this._changeInheritanceButton;
    },
    set_changeInheritanceButton: function (value) {
        this._changeInheritanceButton = value;
    },

    get_inherits: function () {
        return this._inherits;
    },
    set_inherits: function (value) {
        this._inherits = value;
    },

    get_clientLabelManager: function () {
        return this._clientLabelManager;
    },
    set_clientLabelManager: function (value) {
        this._clientLabelManager = value;
    }
}

Telerik.Sitefinity.Configuration.Web.UI.Basic.BasicSettingsSitePanel.registerClass('Telerik.Sitefinity.Configuration.Web.UI.Basic.BasicSettingsSitePanel', Sys.UI.Control, Telerik.Sitefinity.UI.ICommandWidget);
if (typeof (Sys) !== 'undefined') Sys.Application.notifyScriptLoaded();
