Type.registerNamespace("Telerik.Sitefinity.Web.UI.Fields");

Telerik.Sitefinity.Web.UI.Fields.MetaTypeStructureField = function (element) {

    //control IDs
    this._openStructureLinkId = null;
    this._structureEditWindowId = null;
    this._fieldsCountLabelId = null;
    this._clientLabelManagerId = null;

    //control refs
    this._openStructureLink = null;
    this._structureEditWindow = null;
    this._fieldsCountLabel = null;
    this._clientLabelManager = null;

    this._isFirstLoad = true;
    Telerik.Sitefinity.Web.UI.Fields.MetaTypeStructureField.initializeBase(this, [element]);
}

Telerik.Sitefinity.Web.UI.Fields.MetaTypeStructureField.prototype = {

    // ------------------------------------- Initialization -------------------------------------
    initialize: function () {
        Telerik.Sitefinity.Web.UI.Fields.MetaTypeStructureField.callBaseMethod(this, "initialize");

        this._openStructureLink = $get(this._openStructureLinkId);
        this._fieldsCountLabel = $get(this._fieldsCountLabelId);

        Sys.Application.add_load(Function.createDelegate(this, this.onload));
    },

    dispose: function () {
        Telerik.Sitefinity.Web.UI.Fields.MetaTypeStructureField.callBaseMethod(this, "dispose");
    },

    onload: function () {
        this._structureEditWindow = $find(this._structureEditWindowId);
        this._clientLabelManager = $find(this._clientLabelManagerId);

        this._openStructureLink_ClickDelegate = Function.createDelegate(this, this._openStructureLink_Click);
        this._openStructureLink_ShowDelegate = Function.createDelegate(this, this._openStructureLink_Shown);
        this._openStructureLink_OpenDelegate = Function.createDelegate(this, this._openStructureLink_Opened);
        this._openStructureLink_ClosedDelegate = Function.createDelegate(this, this._openStructureLink_Closed);

        $addHandler(this._openStructureLink, "click", this._openStructureLink_ClickDelegate);
        this._structureEditWindow.add_close(this._openStructureLink_ClosedDelegate);

        this._structureEditWindow.add_show(this._openStructureLink_ShowDelegate);
        this._structureEditWindow.add_pageLoad(this._openStructureLink_OpenDelegate);
    },

    _openStructureLink_Click: function () {
        this._structureEditWindow.show();
        Telerik.Sitefinity.centerWindowHorizontally(this._structureEditWindow);
    },

    _openStructureLink_Shown: function () {
        if (this._isFirstLoad)
            this._isFirstLoad = false;
        else {
            var val = this.get_value();
            this._structureEditWindow.get_contentFrame().contentWindow.setDynamicObject(val);
        }
    },


    _openStructureLink_Opened: function () {
        var val = this.get_value();
        this._structureEditWindow.get_contentFrame().contentWindow.setDynamicObject(val);
    },

    _openStructureLink_Closed: function (sender, arg) {
        this.set_value(arg.get_argument());
    },

    //override the set_value method
    set_value: function (value) {
        if (value != null) {
            if (!this._isArray(value)) {
                alert("Value set for the MetaTypeStructureField must be an array of SimpleDefinitionField items.");
            }
            else {
                this._value = value;
                this._setLabelText(this._fieldsCountLabel, String.format(this._clientLabelManager.getLabel("Labels", "NumFieldsDefined"), value.length));
            }
        }
    },

    get_value: function () {       
        var val = Telerik.Sitefinity.Web.UI.Fields.MetaTypeStructureField.callBaseMethod(this, "get_value");
        val = Telerik.Sitefinity.fixArray(val);
        return val;
    },


    //override the validate method
    validate: function () {
        var violationMessageElement = this._validator.get_violationMessageElement();
        if (this._value.length == 0) {
            var addWrappingErrorCssClass = this._controlErrorCssClass.length > 0;
            if (addWrappingErrorCssClass) {
                if (!jQuery(this._element).hasClass(this._controlErrorCssClass)) {
                    jQuery(this._element).addClass(this._controlErrorCssClass);
                }
            }
            this._showViolationMessageElement(violationMessageElement);
            this._setLabelText(violationMessageElement, this._clientLabelManager.getLabel("Labels", "NoFieldsDefinedForThisPublishingPoint"));
            return false;
        }
        else {
            if (addWrappingErrorCssClass) {
                jQuery(this._element).removeClass(this._controlErrorCssClass);
            }
        }
        violationMessageElement.style.display = 'none';
        return true;
    },

    _isArray: function (obj)
    /// <summary>Determines if a certin object is an array.</summary>
    /// <param name="obj">The object suspicious of being an array.</param>
    /// <returns>True if the object is an array, False otherwise.</returns>    
    {
        if (obj.constructor == Array)
            return true;
        if (typeof (obj.length) == "undefined")
            return false;
        else
            return true;
    },

    _setLabelText: function (LabelElement, newText) {
        if (LabelElement != null) {
            if (typeof LabelElement.textContent != "undefined")
                LabelElement.textContent = newText;

            if (typeof LabelElement.innerText != "undefined")
                LabelElement.innerText = newText;
        }
    },

    reset: function () {
        Telerik.Sitefinity.Web.UI.Fields.MetaTypeStructureField.callBaseMethod(this, "reset");
    }
};
Telerik.Sitefinity.Web.UI.Fields.MetaTypeStructureField.registerClass('Telerik.Sitefinity.Web.UI.Fields.MetaTypeStructureField', Telerik.Sitefinity.Web.UI.Fields.FieldControl);