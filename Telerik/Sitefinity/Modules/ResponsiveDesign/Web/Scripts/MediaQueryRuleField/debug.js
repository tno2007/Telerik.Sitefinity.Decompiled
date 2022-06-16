/// <reference name="MicrosoftAjax.js"/>
/// <reference name="Telerik.Sitefinity.Resources.Scripts.jquery-1.6.3-vsdoc.js" assembly="Telerik.Sitefinity.Resources"/>
Type.registerNamespace("Telerik.Sitefinity.Modules.ResponsiveDesign.Web.FieldControls");

Telerik.Sitefinity.Modules.ResponsiveDesign.Web.FieldControls.MediaQueryRuleField = function (element) {

    Telerik.Sitefinity.Modules.ResponsiveDesign.Web.FieldControls.MediaQueryRuleField.initializeBase(this, [element]);
    this._element = element;
    this._addNewRuleButton = null;
    this._mediaQueryRuleDialog = null;
    this._deviceTypesDropDown = null;

    this._defaultRules = [];
    this._rules = [];
    this._editedRuleIndex = -1;
    this._rulesDataSource = null;
}

Telerik.Sitefinity.Modules.ResponsiveDesign.Web.FieldControls.MediaQueryRuleField.prototype =
{
    initialize: function () {
        Telerik.Sitefinity.Modules.ResponsiveDesign.Web.FieldControls.MediaQueryRuleField.callBaseMethod(this, "initialize");

        this._defaultRules = Telerik.Sitefinity.JSON.parse(this._defaultRules);

        // initialize the dialog for adding new media query rules
        $(this.get_mediaQueryRuleDialog()).kendoWindow({
            resizable: false,
            width: "425px",
            height: "auto",
            title: "Modal Window",
            modal: true,
            visible: false,
            animation: false,
            actions: ["Refresh", "Maximize", "Minimize", "Close"]
        });

        var me = this;

        // wire up the rule width type
        $(this._selectors.designer.ruleWidthType).change(function () {
            me._setWidthTypeInterface();
        });

        // wire up the rule height type
        $(this._selectors.designer.ruleHeightType).change(function () {
            me._setHeightTypeInterface();
        });

        // wire up the "add new rule" button
        $(this.get_addNewRuleButton()).click(function () {
            me._openRuleEditor();
        });

        // wire up the "save / done" button
        $(this._selectors.designer.doneButton).click(function () {
            if (me._saveRule()) {
                me._closeDialog();
                me._bindRules();
            }
        });

        // wire up the "cancel rule" button
        $(this._selectors.designer.cancelButton).click(function () {
            me._closeDialog();
        });

        // add the change event to elements that affect media query
        $(this._selectors.designer.mediaQueryAffectingElement).change(function () {
            me._refreshMediaQuery();
        });

        // wire up the edit media query button
        $(this._selectors.designer.editQueryButton).click(function () {
            me._switchToEditMode();
        });

        // wire up the reset media query button
        $(this._selectors.designer.resetQueryButton).click(function () {
            me._resetMediaQuery();
        });

        $(this.get_deviceTypesDropDown()).change(function () {
            me._loadDefaultRules();
        });

        this._rulesDataSource = new kendo.data.DataSource({ data: this.get_rules() });
        $(this._selectors.rulesGrid).kendoGrid({
            rowTemplate: kendo.template($(me._selectors.ruleRowTemplate).html()),
            scrollable: false,
            dataSource: me._rulesDataSource,
            dataBound: function (e) {
                // wire up the edit media query rule command
                $('a[data-sf-command="' + me._constants.editMediaQueryRuleCommand + '"]').click(function () {
                    var description = $(this).attr("data-sf-description");
                    var rIndex = me._findRuleIndexByDescription(description);
                    me._editedRuleIndex = rIndex;
                    me._openRuleEditor();
                });
                // wire up the delete media query rule command
                $('a[data-sf-command="' + me._constants.deleteMediaQueryRuleCommand + '"]').click(function () {
                    var description = $(this).attr("data-sf-description");
                    var rIndex = me._findRuleIndexByDescription(description);
                    me._rules.splice(rIndex, 1);
                    me._bindRules();
                });
            }
        });
    },

    dispose: function () {
        Telerik.Sitefinity.Modules.ResponsiveDesign.Web.FieldControls.MediaQueryRuleField.callBaseMethod(this, "dispose");
    },

    /* --------------------  public methods ----------- */

    reset: function () {
        this._rules.length = 0;
        this._editedRuleIndex = -1;
        this._bindRules();
        this._loadDefaultRules();
    },

    // Gets the value of the field control.
    get_value: function () {
        return this._rules;
    },

    // Sets the value of the text field control depending on DisplayMode.
    set_value: function (value) {
        if (value != null) {
            this._rules.length = 0;
            for (var rIter = 0; rIter < value.length; rIter++) {
                this._rules.push(value[rIter]);
            }
            this._bindRules();
        }
    },

    // Returns true if the value of the field is changed
    isChanged: function () {
    },

    /* -------------------- events -------------------- */

    /* -------------------- event handlers ------------ */

    /* -------------------- private methods ----------- */

    /* This function saves the defined rule. Returns true if saved, otherwise false. If the rule does not exist new rule
    * will be added; otherwise rule will be edited.
    */
    _saveRule: function () {
        // validate
        if (this._validateRuleForm()) {
            if (this._editedRuleIndex > -1) {
                this.get_rules().splice(this._editedRuleIndex, 1, this._getCurrentRuleObject());
            } else {
                this.get_rules().push(this._getCurrentRuleObject());
            }
            return true;
        }
        return false;
    },

    /* Validates the form for creating or editing the media query rule */
    _validateRuleForm: function () {
        var isValid = true;

        // description cannot be empty
        if ($(this._selectors.designer.description).val().length == 0) {
            isValid = false;
            $(this._selectors.designer.validation.descriptionEmpty).show();
        } else {
            $(this._selectors.designer.validation.descriptionEmpty).hide();
        }

        // exact width is number
        var widthType = $(this._selectors.designer.ruleWidthType).val();
        var exactWidth = $(this._selectors.designer.exactWidth).val();
        if ((widthType == 0 || widthType == 2) && exactWidth.length > 0) {
            if (!(this._isInteger(exactWidth))) {
                isValid = false;
                $(this._selectors.designer.validation.exactWidthNotNumber).show();
            } else {
                $(this._selectors.designer.validation.exactWidthNotNumber).hide();
            }
        } else {
            $(this._selectors.designer.validation.exactWidthNotNumber).hide();
        }

        // min width is number
        var minWidth = $(this._selectors.designer.minWidth).val();
        if ((widthType == 1 || widthType == 3) && minWidth.length > 0) {
            if (!(this._isInteger(minWidth))) {
                isValid = false;
                $(this._selectors.designer.validation.minWidthNotNumber).show();
            } else {
                $(this._selectors.designer.validation.minWidthNotNumber).hide();
            }
        } else {
            $(this._selectors.designer.validation.minWidthNotNumber).hide();
        }

        // max width is number
        var maxWidth = $(this._selectors.designer.maxWidth).val();
        if ((widthType == 1 || widthType == 3) && maxWidth.length > 0) {
            if (!(this._isInteger(maxWidth))) {
                isValid = false;
                $(this._selectors.designer.validation.maxWidthNotNumber).show();
            } else {
                $(this._selectors.designer.validation.maxWidthNotNumber).hide();
            }
        } else {
            $(this._selectors.designer.validation.maxWidthNotNumber).hide();
        }

        // exact height is number
        var heightType = $(this._selectors.designer.ruleHeightType).val();
        var exactHeight = $(this._selectors.designer.exactHeight).val();
        if ((heightType == 0 || heightType == 2) && exactHeight.length > 0) {
            if (!(this._isInteger(exactHeight))) {
                isValid = false;
                $(this._selectors.designer.validation.exactHeightNotNumber).show();
            } else {
                $(this._selectors.designer.validation.exactHeightNotNumber).hide();
            }
        } else {
            $(this._selectors.designer.validation.exactHeightNotNumber).hide();
        }

        // min height is a number
        var minHeight = $(this._selectors.designer.minHeight).val();
        if ((heightType == 1 || heightType == 3) && minHeight.length > 0) {
            if (!(this._isInteger(minHeight))) {
                isValid = false;
                $(this._selectors.designer.validation.minHeightNotNumber).show();
            } else {
                $(this._selectors.designer.validation.minHeightNotNumber).hide();
            }
        } else {
            $(this._selectors.designer.validation.minHeightNotNumber).hide();
        }

        // max height is a number
        var maxHeight = $(this._selectors.designer.maxHeight).val();
        if ((heightType == 1 || heightType == 3) && maxHeight.length > 0) {
            if (!(this._isInteger(maxHeight))) {
                isValid = false;
                $(this._selectors.designer.validation.maxHeightNotNumber).show();
            } else {
                $(this._selectors.designer.validation.maxHeightNotNumber).hide();
            }
        } else {
            $(this._selectors.designer.validation.maxHeightNotNumber).hide();
        }

        // resolution is a number
        var resolution = $(this._selectors.designer.resolution).val();
        if (resolution.length > 0) {
            if (!(this._isInteger(resolution))) {
                isValid = false;
                $(this._selectors.designer.validation.resolutionNotNumber).show();
            } else {
                $(this._selectors.designer.validation.resolutionNotNumber).hide();
            }
        } else {
            $(this._selectors.designer.validation.resolutionNotNumber).hide();
        }

        return isValid;
    },

    /* Checks if the string is an integer. Returns true if yes, otherwise false. */
    _isInteger: function (val) {
        var isIntegretRegex = /^\s*\d+\s*$/;
        return String(val).search(isIntegretRegex) != -1
    },

    /* Binds the rules grid */
    _bindRules: function () {
        // get a reference to the grid
        var grid = $(this._selectors.rulesGrid).data("kendoGrid");
        // refreshes the grid
        grid.dataSource.read();
        $(this._selectors.rulesGrid).find(".sfOrLabel:last").hide();
    },

    /* This function loads the default rules based on the device type */
    _loadDefaultRules: function () {

        // remove other rules
        this.get_rules().length = 0;

        var deviceType = $(this.get_deviceTypesDropDown()).val();
        for (var i = 0; i < this._defaultRules.length; i++) {
            if (this._defaultRules[i].DeviceTypeName == deviceType) {
                this.get_rules().push(this._defaultRules[i]);
            }
        }
        this._bindRules();
    },

    /* Opens the rule editor dialog and loads the current rule. If the 
    * current rule is null the rule editor will be reset; use it for creating new rules.
    */
    _openRuleEditor: function () {

        if (this._editedRuleIndex > -1) {
            this._loadRuleInTheForm(this.get_rules()[this._editedRuleIndex]);
        } else {
            this._resetDialogForm();
        }

        // show the dialog for adding new media query rules
        var window = $(this.get_mediaQueryRuleDialog()).data("kendoWindow");
        window.center();
        jQuery(window.element).parent().css({ "top": this._dialogScrollTop() });
        window.open();
    },
    /* calculates top position of kendo dialog */
    _dialogScrollTop: function () {
        var scrollTopHtml = jQuery("html").eq(0).scrollTop();
        var scrollTopBody = jQuery("body").eq(0).scrollTop();
        var scrollTop = ((scrollTopHtml > scrollTopBody) ? scrollTopHtml : scrollTopBody) + 50;
        return scrollTop;
    },
    /* This function switches the designer into the manual edit mode.
    * In this mode, the UI elements are disabled and the CSS is
    * editable.
    */
    _switchToEditMode: function () {

        // get the current media query CSS
        var _css = this._generateMediaQuery();

        // hide the read element
        $(this._selectors.designer.mediaQueryReadElement).hide();

        // show the edit element
        $(this._selectors.designer.mediaQueryEditElement).show();

        // set the css in the edit element
        $(this._selectors.designer.mediaQueryEditElement).val(_css);

        // hide the edit button
        $(this._selectors.designer.editQueryButton).hide();

        // show the reset button
        $(this._selectors.designer.resetQueryButton).show();

        // disable all media query affecting elements
        $(this._selectors.designer.mediaQueryAffectingElement).attr("disabled", "disabled");
    },

    /* This function resets all the changes done to the media query manually
    * and reverts the designer in the UI mode. This function is the opposite
    * from _switchToEditMode function
    */
    _resetMediaQuery: function () {

        // get the current media query CSS
        var _css = this._generateMediaQuery();

        // hide the edit element
        $(this._selectors.designer.mediaQueryEditElement).hide();

        // show the read element
        $(this._selectors.designer.mediaQueryReadElement).show();

        // set the css in the read element
        $(this._selectors.designer.mediaQueryReadElement).val(_css);

        // hide the reset button
        $(this._selectors.designer.resetQueryButton).hide();

        // show the edit button
        $(this._selectors.designer.editQueryButton).show();

        // enable all media query affecting elements
        $(this._selectors.designer.mediaQueryAffectingElement).removeAttr("disabled");
    },

    /* This function refreshes the media query generated by the UI */
    _refreshMediaQuery: function () {
        $(this._selectors.designer.mediaQueryReadElement).html(this._generateMediaQuery());
    },

    /* This function generates the media query based on the selection user made in the
    * user interface 
    */
    _generateMediaQuery: function () {

        var css = "@media all ";

        var _widthType = $(this._selectors.designer.ruleWidthType).val();
        var _exactWidth = $(this._selectors.designer.exactWidth).val();
        var _minWidth = $(this._selectors.designer.minWidth).val();
        var _maxWidth = $(this._selectors.designer.maxWidth).val();

        var _heightType = $(this._selectors.designer.ruleHeightType).val();
        var _exactHeight = $(this._selectors.designer.exactHeight).val();
        var _minHeight = $(this._selectors.designer.minHeight).val();
        var _maxHeight = $(this._selectors.designer.maxHeight).val();

        var _isPortraitOrientation = $(this._selectors.designer.portraitOrientation).is(":checked");
        var _isLandscapeOrientation = $(this._selectors.designer.landscapeOrientation).is(":checked");

        var _aspectRatio = $(this._selectors.designer.aspectRatio).val();
        var _resolution = $(this._selectors.designer.resolution).val();

        var _isMonochrome = $(this._selectors.designer.isMonochrome).is(":checked");
        var _isGrid = $(this._selectors.designer.isGrid).is(":checked");

        // write width, if applicable
        if (_widthType == "0" && _exactWidth.length > 0) {
            css += "and (width:" + _exactWidth + "px) ";
        }

        // write range width, if applicable
        if (_widthType == "1") {
            if (_minWidth.length > 0) {
                css += "and (min-width:" + _minWidth + "px) ";
            }
            if (_maxWidth.length > 0) {
                css += "and (max-width:" + _maxWidth + "px) ";
            }
        }

        // write device width, if applicable
        if (_widthType == "2" && _exactWidth.length > 0) {
            css += "and (device-width:" + _exactWidth + "px) ";
        }

        // write device range width, if applicable
        if (_widthType == "3") {
            if (_minWidth.length > 0) {
                css += "and (min-device-width:" + _minWidth + "px) ";
            }
            if (_maxWidth.length > 0) {
                css += "and (max-device-width:" + _maxWidth + "px) ";
            }
        }

        // write height, if applicable
        if (_heightType == "0" && _exactHeight.length > 0) {
            css += "and (height:" + _exactHeight + "px) ";
        }

        // write range height, if applicable
        if (_heightType == "1") {
            if (_minHeight.length > 0) {
                css += "and (min-height:" + _minHeight + "px) ";
            }
            if (_maxHeight.length > 0) {
                css += "and (max-height:" + _maxHeight + "px) ";
            }
        }

        // write device height, if applicable
        if (_heightType == "2" && _exactHeight.length > 0) {
            css += "and (device-height:" + _exactHeight + "px) ";
        }

        // write device range height, if applicable
        if (_heightType == "3") {
            if (_minHeight.length > 0) {
                css += "and (min-device-height:" + _minHeight + "px) ";
            }
            if (_maxHeight.length > 0) {
                css += "and (max-device-height:" + _maxHeight + "px) ";
            }
        }

        // write orientation, if applicable
        if (_isPortraitOrientation) {
            css += "and (orientation:portrait) ";
        } else if (_isLandscapeOrientation) {
            css += "and (orientation:landscape) ";
        }

        // write aspect ratio, if applicable
        if (_aspectRatio.length > 0) {
            css += "and (aspect-ratio:" + _aspectRatio + ") ";
        }

        // write resolution, if applicable
        if (_resolution.length > 0) {
            css += "and (resolution:" + _resolution + ") ";
        }

        // write monochrome, if applicable
        if (_isMonochrome) {
            css += "and (monochrome) ";
        }

        // write grid, if applicable
        if (_isGrid) {
            css += "and (grid) ";
        }

        return css;

    },

    /* This function generates the rule object */
    _getCurrentRuleObject: function () {

        var rule = new Object();

        rule.Description = $(this._selectors.designer.description).val();

        rule.WidthType = $(this._selectors.designer.ruleWidthType).val();
        rule.ExactWidth = $(this._selectors.designer.exactWidth).val();
        rule.MinWidth = $(this._selectors.designer.minWidth).val();
        rule.MaxWidth = $(this._selectors.designer.maxWidth).val();
        rule.HeightType = $(this._selectors.designer.ruleHeightType).val();
        rule.ExactHeight = $(this._selectors.designer.exactHeight).val();
        rule.MinHeight = $(this._selectors.designer.minHeight).val();
        rule.MaxHeight = $(this._selectors.designer.maxHeight).val();

        rule.Orientation = 0;
        if ($(this._selectors.designer.portraitOrientation).is(":checked")) {
            rule.Orientation = 1;
        } else if ($(this._selectors.designer.landscapeOrientation).is(":checked")) {
            rule.Orientation = 2;
        }

        rule.AspectRatio = $(this._selectors.designer.aspectRatio).val();
        rule.Resolution = $(this._selectors.designer.resolution).val();
        rule.IsMonochrome = $(this._selectors.designer.isMonochrome).is(":checked");
        rule.IsGrid = $(this._selectors.designer.isGrid).is(":checked");
        rule.MediaQueryRule = $(this._selectors.designer.mediaQueryEditElement).is(":visible") ?
                              $(this._selectors.designer.mediaQueryEditElement).val() :
                              $(this._selectors.designer.mediaQueryReadElement).html();

        return rule;
    },

    /* Closes the media query rule designer dialog */
    _closeDialog: function () {
        // reset the dialog form
        this._resetDialogForm();
        // hide the dialog for managing media query rules
        var window = $(this.get_mediaQueryRuleDialog()).data("kendoWindow");
        window.close();
    },

    /* Load rule in the dialog form */
    _loadRuleInTheForm: function (rule) {
        this._resetMediaQuery();
        $(this._selectors.designer.description).val(rule.Description);
        $(this._selectors.designer.ruleWidthType).val(rule.WidthType);
        $(this._selectors.designer.exactWidth).val(rule.ExactWidth);
        $(this._selectors.designer.minWidth).val(rule.MinWidth);
        $(this._selectors.designer.maxWidth).val(rule.MaxWidth);
        $(this._selectors.designer.ruleHeightType).val(rule.HeightType);
        $(this._selectors.designer.exactHeight).val(rule.ExactHeight);
        $(this._selectors.designer.minHeight).val(rule.MinHeight);
        $(this._selectors.designer.maxHeight).val(rule.MaxHeight);

        switch (rule.Orientation) {
            case 0:
                $(this._selectors.designer.bothOrientations).attr("checked", "checked");
                break;
            case 1:
                $(this._selectors.designer.portraitOrientation).attr("checked", "checked");
                break;
            case 2:
                $(this._selectors.designer.landscapeOrientation).attr("checked", "checked");
                break;
        }

        $(this._selectors.designer.aspectRatio).val(rule.AspectRatio);
        $(this._selectors.designer.resolution).val(rule.Resolution);

        if (rule.IsMonochrome) {
            $(this._selectors.designer.isMonochrome).attr("checked", "checked");
        } else {
            $(this._selectors.designer.isMonochrome).prop("checked", false);
        }

        if (rule.IsGrid) {
            $(this._selectors.designer.isGrid).attr("checked", "checked");
        } else {
            $(this._selectors.designer.isGrid).prop("checked", false);
        }

        if (rule.IsManualMediaQuery) {
            this._switchToEditMode();
        }

        $(this._selectors.designer.mediaQueryEditElement).val(rule.MediaQueryRule);
        $(this._selectors.designer.mediaQueryReadElement).html(rule.MediaQueryRule);
        this._setWidthTypeInterface();
        this._setHeightTypeInterface();
    },

    /* Resets the fields in the form for creating and editing media query rules */
    _resetDialogForm: function () {
        this._resetMediaQuery();
        $(this._selectors.designer.description).val("");
        $(this._selectors.designer.ruleWidthType).val($(this._selectors.designer.ruleWidthType + " option:first").val());
        $(this._selectors.designer.exactWidth).val("");
        $(this._selectors.designer.minWidth).val("");
        $(this._selectors.designer.maxWidth).val("");
        $(this._selectors.designer.ruleHeightType).val($(this._selectors.designer.ruleHeightType + " option:first").val());
        $(this._selectors.designer.exactHeight).val("");
        $(this._selectors.designer.minHeight).val("");
        $(this._selectors.designer.maxHeight).val("");
        $(this._selectors.designer.bothOrientations).attr("checked", "checked");
        $(this._selectors.designer.aspectRatio).val("");
        $(this._selectors.designer.resolution).val("");
        $(this._selectors.designer.isMonochrome).prop("checked", false);
        $(this._selectors.designer.isGrid).prop("checked", false);
        $(this._selectors.designer.mediaQueryEditElement).val("");
        $(this._selectors.designer.mediaQueryReadElement).html("");
        this._editedRuleIndex = -1;
        $(this._selectors.designer.validation.allMessages).hide();
    },

    /* Sets the user interface in accordance to the
    * currently selected width type 
    */
    _setWidthTypeInterface: function () {
        var val = $(this._selectors.designer.ruleWidthType).val();
        if (val == "0" || val == "2") {
            $(this._selectors.designer.exactWidthsPanel).show();
            $(this._selectors.designer.rangeWidthsPanel).hide();
        } else {
            $(this._selectors.designer.exactWidthsPanel).hide();
            $(this._selectors.designer.rangeWidthsPanel).show();
        }
    },

    /* Sets the user interface in accordance to the
    * currently selected height type
    */
    _setHeightTypeInterface: function () {
        var val = $(this._selectors.designer.ruleHeightType).val();
        if (val == "0" || val == "2") {
            $(this._selectors.designer.exactHeightsPanel).show();
            $(this._selectors.designer.rangeHeightsPanel).hide();
        } else {
            $(this._selectors.designer.exactHeightsPanel).hide();
            $(this._selectors.designer.rangeHeightsPanel).show();
        }
    },

    /* Finds the index of the rule by rule's description and returns the index.
    * If the rule is not found, -1 is returned
    */
    _findRuleIndexByDescription: function (description) {
        for (var count = 0; count < this._rules.length; count++) {
            if (this._rules[count].Description == description) {
                return count;
            }
        }
        return -1;
    },

    /* Provides a structured way to acces the UI elements of the media query rule designer */
    _selectors: {
        rulesGrid: "#media-query-rules-grid",
        ruleRowTemplate: "#media-query-rule-row-template",
        designer: {
            description: "#media-query-rule-description",
            mediaQueryAffectingElement: ".sf_affectsMediaQuery",
            doneButton: "#media-query-rule-done-button",
            cancelButton: "#media-query-rule-cancel-button",
            ruleWidthType: "#media-query-rule-width-type",
            ruleHeightType: "#media-query-rule-height-type",
            exactWidthsPanel: ".exact-widths-panel",
            exactHeightsPanel: ".exact-heights-panel",
            rangeWidthsPanel: ".range-widths-panel",
            rangeHeightsPanel: ".range-heights-panel",
            exactWidth: "#media-query-rule-exact-width",
            minWidth: "#media-query-rule-min-width",
            maxWidth: "#media-query-rule-max-width",
            exactHeight: "#media-query-rule-exact-height",
            minHeight: "#media-query-rule-min-height",
            maxHeight: "#media-query-rule-max-height",
            mediaQueryReadElement: "#media-query-rule-css-read",
            mediaQueryEditElement: "#media-query-rule-css-edit",
            portraitOrientation: "#media-query-rule-orientation-portrait",
            landscapeOrientation: "#media-query-rule-orientation-landscape",
            bothOrientations: "#media-query-rule-orientation-both",
            aspectRatio: "#media-query-rule-aspect-ratio",
            resolution: "#media-query-rule-resolution",
            isMonochrome: "#media-query-rule-is-monochrome",
            isGrid: "#media-query-rule-is-grid",
            editQueryButton: "#media-query-rule-edit-css-button",
            resetQueryButton: "#media-query-rule-reset-css-button",
            validation: {
                allMessages: ".sfError",
                descriptionEmpty: "#media-query-rule-description-required",
                exactWidthNotNumber: "#media-query-rule-exact-width-not-number",
                exactHeightNotNumber: "#media-query-rule-exact-height-not-number",
                minWidthNotNumber: "#media-query-rule-min-width-not-number",
                maxWidthNotNumber: "#media-query-rule-max-width-not-number",
                minHeightNotNumber: "#media-query-rule-min-height-not-number",
                maxHeightNotNumber: "#media-query-rule-max-height-not-number",
                resolutionNotNumber: "#media-query-rule-resolution-not-number"
            }
        }
    },

    _constants: {
        editMediaQueryRuleCommand: "edit-media-query-rule",
        deleteMediaQueryRuleCommand: "delete-media-query-rule"
    },

    /* -------------------- properties ---------------- */

    get_element: function () {
        return this._element;
    },
    set_element: function (value) {
        this._element = value;
    },
    get_addNewRuleButton: function () {
        return this._addNewRuleButton;
    },
    set_addNewRuleButton: function (value) {
        this._addNewRuleButton = value;
    },
    get_mediaQueryRuleDialog: function () {
        return this._mediaQueryRuleDialog;
    },
    set_mediaQueryRuleDialog: function (value) {
        this._mediaQueryRuleDialog = value;
    },
    get_deviceTypesDropDown: function () {
        return this._deviceTypesDropDown;
    },
    set_deviceTypesDropDown: function (value) {
        this._deviceTypesDropDown = value;
    },
    /* Gets the collection of media query rules that define this media query */
    get_rules: function () {
        return this._rules;
    }
};

Telerik.Sitefinity.Modules.ResponsiveDesign.Web.FieldControls.MediaQueryRuleField.registerClass("Telerik.Sitefinity.Modules.ResponsiveDesign.Web.FieldControls.MediaQueryRuleField", Telerik.Sitefinity.Web.UI.Fields.FieldControl);
