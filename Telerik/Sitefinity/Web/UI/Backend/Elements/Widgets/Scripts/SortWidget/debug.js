Type.registerNamespace("Telerik.Sitefinity.Web.UI.Backend.Elements.Widgets");

Telerik.Sitefinity.Web.UI.Backend.Elements.Widgets.SortWidget = function (element) {
    Telerik.Sitefinity.Web.UI.Backend.Elements.Widgets.SortWidget.initializeBase(this, [element]);
    this._radWindowClientID = null;
    this._customSortingUsed = false;    
    this._selectedValue = null;
    this._customOption = null;

    this._customSortDialog = null;
}

Telerik.Sitefinity.Web.UI.Backend.Elements.Widgets.SortWidget.prototype = {

    initialize: function () {
        Telerik.Sitefinity.Web.UI.Backend.Elements.Widgets.SortWidget.callBaseMethod(this, 'initialize');
        Sys.Application.add_load(Function.createDelegate(this, this._onLoad));
    },

    _onLoad: function (sender, args) {
        this._customSortDialog = $find(this._radWindowClientID);
        if (this._customSortDialog) {
            this._customSortDialog.set_skin("Default");
            this._customSortDialog.set_autoSizeBehaviors(5);
            this._customSortDialog.set_showContentDuringLoad(false);
            this._customSortDialog.add_close(Function.createDelegate(this, this._onWindowClose));
        }
        var commandListSelect = $get(this._dropDownId);
        var separator = $("#" + commandListSelect.id + " optgroup[value='----------']");
        if (separator.length == 0) {
            var separator = document.createElement("optgroup");
            separator.setAttribute('label', '----------');
            separator.setAttribute('value', '----------');
            // find the first custom command
            var customCommandCount = this._customCommandItemValues.length;
            var firstCustomOption = null;
            for (var c = 0; c < customCommandCount; c++) {
                var commandValue = this._customCommandItemValues[c];
                firstCustomOption = $("#" + commandListSelect.id + " option[value='" + commandValue + "']");
                if (firstCustomOption.length == 1)
                    break;
            }
            if (firstCustomOption) {
                commandListSelect.insertBefore(separator, firstCustomOption.get(0));
            }
        }

        if (this._selectedValue != null) {            

            if (this._selectedValue == 'Custom') {
                this._bindDropDown();
            }
            else {
                this._selectOptionInDropDown(this._selectedValue);
            }
        }
    },

    _bindDropDown: function () {
        var commandListSelect = $get(this._dropDownId);
        var commands = commandListSelect.options.length;
        if (!this._customSortingUsed) {
            // should arrange the items in the dropdown
            for (var comCounter = 0; comCounter < commands; comCounter++) {
                var option = commandListSelect.options[comCounter];
                if ($.inArray(option.value, this._customCommandItemValues) >= 0) {
                    // the option is a custom command
                    var newCustomOption = document.createElement("option");
                    newCustomOption.setAttribute('text', option.text.replace('...', ''));
                    newCustomOption.setAttribute('value', '#custom');
                    newCustomOption.innerHTML = option.text.replace('...', '');

                    var newOption = document.createElement("option");
                    newOption.setAttribute('text', "Edit custom sorting...");
                    newOption.setAttribute('value', option.value);
                    newOption.innerHTML = "Edit custom sorting...";

                    commandListSelect.removeChild(option);

                    commandListSelect.appendChild(newCustomOption);
                    commandListSelect.appendChild(newOption);

                    this._selectCustomOptionInDropDown();
                    this._customSortingUsed = true;
                }
            }
        }
        else {
            $("#" + commandListSelect.id + " option[value='#custom']").remove();
            var customCommandCount = this._customCommandItemValues.length;
            for (var c = 0; c < customCommandCount; c++) {
                var commandValue = this._customCommandItemValues[c];
                var option = $("#" + commandListSelect.id + " option[value='" + commandValue + "']").attr('text', this._customCommandItemTexts[c]);
            }
            this._customSortingUsed = false;
        }
    },

    _selectCustomOptionInDropDown: function () {
        this._selectOptionInDropDown("#custom");
    },

    _selectOptionInDropDown: function (value) {
        var commandListSelect = $get(this._dropDownId);
        var commands = commandListSelect.options.length;
        for (var comCounter = 0; comCounter < commands; comCounter++) {
            var option = commandListSelect.options[comCounter];
            if (option.value == value) {
                commandListSelect.selectedIndex = comCounter;
            }
        }
    },

    dispose: function () {
        Telerik.Sitefinity.Web.UI.Backend.Elements.Widgets.SortWidget.callBaseMethod(this, 'dispose');
        this._handlerPageLoadDelegate = null;
    },

    _dropDownChange: function (sender) {
        var select = $get(this._dropDownId);
        selectedValue = jQuery(select).find("option:selected").attr("value");

        this._selectValue(selectedValue);
    },

    _selectValue: function (selectedValue) {
        if (selectedValue === "showHierarchical") {
            // non-custom sorting - should rebind the dropdown only if it was custom until now
            if (this._customSortingUsed)
                this._bindDropDown();

            this._raiseCommand('showHierarchical', selectedValue);
        }
        else if ($.inArray(selectedValue, this._customCommandItemValues) >= 0) {
            this._customSortDialog.show();
            Telerik.Sitefinity.centerWindowHorizontally(this._customSortDialog);
            if (this._customSortDialog.get_width() == 100 && this._customSortDialog.get_height() == 100) {
                this._customSortDialog.maximize();
            }
        }
        else {
            // non-custom sorting - should rebind the dropdown only if it was custom until now
            if (this._customSortingUsed)
                this._bindDropDown();
            this._raiseCommand('sort', selectedValue);
        }
    },

    _raiseCommand: function (command, selectedValue) {
        var commandEventArgs = new Telerik.Sitefinity.UI.CommandEventArgs(command, selectedValue);
        var h = this.get_events().getHandler('command');
        if (h) h(this, commandEventArgs);
    },

    _onWindowClose: function (sender, args) {
        var sortExpression = args.get_argument();
        var commandEventArgs = new Telerik.Sitefinity.UI.CommandEventArgs('sort', sortExpression);
        var h = this.get_events().getHandler('command');
        if (h) h(this, commandEventArgs);
        // custom sorting - should rebind the dropdown only if it was not custom until now
        if (!this._customSortingUsed)
            this._bindDropDown();
        this._selectCustomOptionInDropDown();
    },

    // -------------------------------------- Public Properties -----------------------------------------

    get_radWindowManager: function () {
        return this._radWindowManager;
    },

    set_radWindowManager: function (value) {
        if (this._radWindowManager != value) {
            this._radWindowManager = value;
            this.raisePropertyChanged("radWindowManager");
        }
    },

    get_selectedValue: function () {
        return this._selectedValue;
    },

    set_selectedValue: function (value) {
        this._selectedValue = value;
    }
}

Telerik.Sitefinity.Web.UI.Backend.Elements.Widgets.SortWidget.registerClass("Telerik.Sitefinity.Web.UI.Backend.Elements.Widgets.SortWidget", Telerik.Sitefinity.Web.UI.Backend.Elements.Widgets.DynamicCommandWidget);
