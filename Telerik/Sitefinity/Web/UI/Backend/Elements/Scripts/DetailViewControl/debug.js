Type.registerNamespace("Telerik.Sitefinity.Web.UI.Backend.Elements");

detailFormView = null;

Telerik.Sitefinity.Web.UI.Backend.Elements.DetailViewControl = function (element) {
    this._element = element;
    Telerik.Sitefinity.Web.UI.Backend.Elements.DetailViewControl.initializeBase(this, [element]);

    this._sectionIds = [];
    this._widgetBarIds = null;
    this._buttonBarIds = null;
    this._widgetCommandDelegate = null;
    this.dataItem = null;
    this._loadingView = null;
    this._messageControl = null;
    this._backButton = null;
    this._moreActonsWidgets = [];
}

Telerik.Sitefinity.Web.UI.Backend.Elements.DetailViewControl.prototype =
{
    initialize: function () {
        Telerik.Sitefinity.Web.UI.Backend.Elements.DetailViewControl.callBaseMethod(this, "initialize");
     
        if (this._sectionIds) {
            this._sectionIds = Sys.Serialization.JavaScriptSerializer.deserialize(this._sectionIds);
        }

        if (this._widgetBarIds) {
            this._widgetBarIds = Sys.Serialization.JavaScriptSerializer.deserialize(this._widgetBarIds);
        }

        if (this.get_backButton()) {
            this._backButtonClickDelegate = Function.createDelegate(this, this._backButtonClickHandler);
            $addHandler(this.get_backButton(), "click", this._backButtonClickDelegate);
        }

        Sys.Application.add_load(Function.createDelegate(this, this._handlePageLoad));
    },

    dispose: function () {
        if (this._widgetCommandDelegate) {
            var wLength = this._widgetBarIds.length;
            for (var wCounter = 0; wCounter < wLength; wCounter++) {
                var widget = $find(this._widgetBarIds[wCounter]);
                if (widget !== null) {
                    widget.remove_command(this._widgetCommandDelegate);
                }
            }
            delete this._widgetCommandDelegate;
        }

        if (this.get_backButton()) {
            $removeHandler(this.get_backButton(), "click", this._backButtonClickDelegate);
        }
        if (this._backButtonClickDelegate) {
            delete this._backButtonClickDelegate;
        }

        Telerik.Sitefinity.Web.UI.Backend.Elements.DetailViewControl.callBaseMethod(this, "dispose");
    },

    _handlePageLoad: function (sender, args) {
        this._subscribeToWidgetBarEvents();
    },

    //set the values of all fields depending on the current data item
    set_dataItem: function (dataItem) {
        var fieldcontrols = this.get_fieldControls();
        this.dataItem = dataItem;
        for (var i = 0; i < fieldcontrols.length; i++) {
            var control = fieldcontrols[i];
            control.set_value(dataItem[control.get_dataFieldName()]);
        }
    },

    //creates current data item from the values in the fields
    get_dataItem: function () {
        var fieldcontrols = this.get_fieldControls();
        for (var i = 0; i < fieldcontrols.length; i++) {
            var control = fieldcontrols[i];           
            this.dataItem[control.get_dataFieldName()] = control.get_value();
        }

        return this.dataItem;
      
    },
    
    getFieldControlByName: function (fieldName) {
        var fieldcontrols = this.get_fieldControls();
        for (var i = 0; i < fieldcontrols.length; i++) {
            var control = fieldcontrols[i];
            if (control.get_dataFieldName() == fieldName)
                return control;
        }
    },

    //get all field controls
    get_fieldControls: function () {
        var fieldControls = new Array();
        //go over the sections defined for this control
        for (var s = 0; s < this._sectionIds.length; s++) {
            var section = $find(this._sectionIds[s]);
            if (section != null) {
                //get field controls on the section
                var ctlIds = section.get_fieldControlIds();
                for (var c = 0; c < ctlIds.length; c++) { // c++ ;-)
                    var fieldcontrol = $find(ctlIds[c]);
                    if ((fieldcontrol != null) && (fieldcontrol.constructor != null) && (typeof (fieldcontrol.constructor) != "undefined")) {
                        fieldControls.push(fieldcontrol);
                    }
                }
            }
        }
        return fieldControls;
    },

    //subscribe for the widget bar events
    _subscribeToWidgetBarEvents: function () {
        if (this._widgetBarIds) {
            var wLength = this._widgetBarIds.length;
            for (var wCounter = 0; wCounter < wLength; wCounter++) {
                var widget = $find(this._widgetBarIds[wCounter]);
                if (widget !== null) {
                    if (this._widgetCommandDelegate == null) {
                        this._widgetCommandDelegate = Function.createDelegate(this, this._widgetCommandHandler);
                    }
                    widget.add_command(this._widgetCommandDelegate);
                    this._moreActonsWidgets.push(widget.getWidgetByName("moreActions"));
                }
            }
        }
    },

    //show/hide widgetbars
    showWidgetBars: function (show) {
        if (this._widgetBarIds) {
            var wLength = this._widgetBarIds.length;
            for (var wCounter = 0; wCounter < wLength; wCounter++) {
                var widget = $find(this._widgetBarIds[wCounter]);
                if (widget !== null && show)
                    jQuery(widget.get_element()).show();
                else if (widget !== null && !show)
                    jQuery(widget.get_element()).hide();
            }
        }
    },

    // fired when one of the toolbars fires a command event
    _widgetCommandHandler: function (sender, args) {
        var isEnterPressed = false;
        if (args && args.enterKey) {
            isEnterPressed = true;
        }

        if (args.get_cancel()) {
            return;
        }

        switch (args.get_commandName()) {

            case "cancel":
                this.close();
                break;
        }
    },

    //handler for the back button click event
    _backButtonClickHandler: function () {
        this.close();
    },

    //reset all fields
    reset: function () {
        var fieldcontrols = this.get_fieldControls();
        for (var i = 0; i < fieldcontrols.length; i++) {
            var control = fieldcontrols[i];
            control.set_value("");
            control._clearViolationMessage();
        }

        this.get_messageControl().hide();
    },


    //validate fields
    validate: function () {
        var fieldControlsCount = this.get_fieldControls().length;
        var isValid = true;

        //Resets this flag in order to give focus to the first element that has a validation error
        Telerik.Sitefinity.Web.UI.Fields.FieldControl.isValidationMessagedFocused = false;

        while (fieldControlsCount--) {
            var fieldControl = this.get_fieldControls()[fieldControlsCount];
            var isCurrentValid = fieldControl.validate();
            isValid = isValid && isCurrentValid;
        }
        return isValid;
    },

    //closes the dialog
    close: function () {
        dialogBase.close();
    },

    /* -------------------- properties -------------------- */

    get_sectionIds: function () {
        return this._sectionIds;
    },
    set_sectionIds: function (value) {
        this._sectionIds = value;
    },

    get_buttonBarIds: function () {
        return this._buttonBarIds;
    },
    set_buttonBarIds: function (value) {
        this._buttonBarIds = value;
    },

    get_widgetBarIds: function () {
        return this._widgetBarIds;
    },

    set_widgetBarIds: function (value) {
        this._widgetBarIds = value;
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

    get_backButton: function () {
        return this._backButton;
    },
    set_backButton: function (value) {
        this._backButton = value;
    },

    get_moreActonsWidgets: function () {
        return this._moreActonsWidgets;
    }

};

Telerik.Sitefinity.Web.UI.Backend.Elements.DetailViewControl.registerClass("Telerik.Sitefinity.Web.UI.Backend.Elements.DetailViewControl", Sys.UI.Control);
