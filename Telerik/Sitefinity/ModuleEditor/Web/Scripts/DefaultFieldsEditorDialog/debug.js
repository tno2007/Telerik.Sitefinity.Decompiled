Type.registerNamespace("Telerik.Sitefinity.ModuleEditor.Web");

Telerik.Sitefinity.ModuleEditor.Web.DefaultFieldsEditorDialog = function (element) {
    Telerik.Sitefinity.ModuleEditor.Web.DefaultFieldsEditorDialog.initializeBase(this, [element]);

    this._commandBar = null;
    this._viewsSelector = null;
    this._definition = null;
    this._defaultValueValidationMessage = null;
    //delegates
    this._commandBarCommandDelegate = null;
}

Telerik.Sitefinity.ModuleEditor.Web.DefaultFieldsEditorDialog.prototype = {

    initialize: function () {
        Telerik.Sitefinity.ModuleEditor.Web.DefaultFieldsEditorDialog.callBaseMethod(this, "initialize");

        if (this._definition) {
            this._definition = Sys.Serialization.JavaScriptSerializer.deserialize(this._definition);
        }

        if (this._commandBar != null) {
            this._commandBarCommandDelegate = Function.createDelegate(this, this._commandBar_Command);
            this._commandBar.add_command(this._commandBarCommandDelegate);
        }

        this._loadDelegate = Function.createDelegate(this, this.onLoad);
        Sys.Application.add_load(this._loadDelegate);
    },
    dispose: function () {
        Telerik.Sitefinity.ModuleEditor.Web.DefaultFieldsEditorDialog.callBaseMethod(this, "dispose");

        if (this._commandBarCommandDelegate) {
            if (this._commandBar != null) {
                this._commandBar.remove_command(this._commandBarCommandDelegate);
            }
            delete this._commandBarCommandDelegate;
        }
        if (this._loadDelegate) {
            Sys.Application.remove_load(this._loadDelegate);
            delete this._loadDelegate;
        }
    },
    /* --------------------- Public methods----------------------------- */
    onLoad: function () {
        var selectedViews = this._definition.VisibleViews;
        var hidden = this._definition.Hidden;
        if (selectedViews.length > 0) {
            this.get_viewsSelector().setSelectedViews(selectedViews);
        }
        else if (hidden) {
            this.get_viewsSelector().setSelectedViews("nowhere");
        }
        else {
            this.get_viewsSelector().setSelectedViews("allViews");
        }
    },
    get_definition: function () {
        var queryString = new Telerik.Sitefinity.Web.SitefinityJS.Utility.Querystring();

        var fieldName = this._getQueryStringValue(queryString, "FieldName");
        var fieldType = this._getQueryStringValue(queryString, "FieldTypeName");
        if (this._definition == null) {
            this._definition =
                {
                    'FieldName': fieldName,
                    'FieldType': fieldType,
                    'VisibleViews': []
                };
        }
        return this._definition;
    },
    set_definition: function (value) {
        this._definition = value;
        this.onLoad();
    },
    /* --------------------- Private methods----------------------------- */
    _getQueryStringValue: function (queryString, key) {
        if (!queryString) {
            queryString = new Telerik.Sitefinity.Web.SitefinityJS.Utility.Querystring();
        }

        if (queryString.contains(key)) {
            return queryString.get(key);
        }
        return null;
    },
    _commandBar_Command: function (sender, args) {
        switch (args.get_commandName()) {
            case 'save':
                this._onSaveCommand();
                break;
            case 'cancel':
                this._onCancelCommand();
                break;
        }
    },
    _onSaveCommand: function (e) {
        this._definition.VisibleViews = this.get_viewsSelector().getSelectedViews();
        if (!this.validate(e)) {
            return;
        }
        var context = this._buildContext();
        dialogBase.close(context);
    },
    _buildContext: function () {
        var queryString = new Telerik.Sitefinity.Web.SitefinityJS.Utility.Querystring();
        var fieldName = this._getQueryStringValue(queryString, "FieldName");
        var context =
        {
            'Key': fieldName,
            'Value': {
                'Name': fieldName,
                'FieldTypeKey': this._getQueryStringValue(queryString, "FieldTypeName"),
                'IsCustom': false,
                'Definition': this._definition
            }
        };
        return context;
    },
    _validate: function (e) {
        var maxLength = this._definition.ValidatorDefinition.MaxLength;
        var defaultValue = this._definition.DefaultStringValue;
        if (!this._validateDefaultValue(maxLength, defaultValue)) {
            if (!e) var e = window.event;
            if (e.stopPropagation) {
                e.stopPropagation();
            }
            else {
                e.cancelBubble = true;
            }
            if (e.preventDefault) {
                e.preventDefault();
            }
            else {
                e.returnValue = false;
            }
            alert(this.get_defaultValueValidationMessage());
            return false;
        }
        return true;
    },
    _validateDefaultValue: function (maxLength, defaultValue) {
        if (defaultValue && maxLength && 0 < maxLength && maxLength < defaultValue.length) {
            return false;
        }

        return true;
    },
    _onCancelCommand: function () {
        this.close();
    },
    /* --------------------- Properties ----------------------------- */
    get_commandBar: function () {
        return this._commandBar;
    },
    set_commandBar: function (value) {
        this._commandBar = value;
    },
    get_viewsSelector: function () {
        return this._viewsSelector;
    },
    set_viewsSelector: function (value) {
        this._viewsSelector = value;
    },
    get_defaultValueValidationMessage: function () {
        return this._defaultValueValidationMessage;
    },
    set_defaultValueValidationMessage: function (value) {
        this._defaultValueValidationMessage = value;
    }
};

Telerik.Sitefinity.ModuleEditor.Web.DefaultFieldsEditorDialog.registerClass('Telerik.Sitefinity.ModuleEditor.Web.DefaultFieldsEditorDialog', Telerik.Sitefinity.Web.UI.AjaxDialogBase);

