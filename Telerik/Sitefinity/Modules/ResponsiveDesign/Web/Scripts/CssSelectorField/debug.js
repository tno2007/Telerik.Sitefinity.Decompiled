/// <reference name="MicrosoftAjax.js"/>
/// <reference name="Telerik.Sitefinity.Resources.Scripts.jquery-1.6.3-vsdoc.js" assembly="Telerik.Sitefinity.Resources"/>
Type.registerNamespace("Telerik.Sitefinity.Modules.ResponsiveDesign.Web.FieldControls");

Telerik.Sitefinity.Modules.ResponsiveDesign.Web.FieldControls.CssSelectorField = function (element) {

    Telerik.Sitefinity.Modules.ResponsiveDesign.Web.FieldControls.CssSelectorField.initializeBase(this, [element]);
    this._element = element;
    this._cssFileSelectorDialog = null;
}

Telerik.Sitefinity.Modules.ResponsiveDesign.Web.FieldControls.CssSelectorField.prototype =
{
    initialize: function () {
        Telerik.Sitefinity.Modules.ResponsiveDesign.Web.FieldControls.CssSelectorField.callBaseMethod(this, "initialize");
        var me = this;

        this._cssFileSelectorDialogCloseDelegate = Function.createDelegate(this, this._cssFileSelectorDialogCloseHandler);
        this._cssFileSelectorDialog.add_close(this._cssFileSelectorDialogCloseDelegate);

        // hook up the select css file button
        $(this._selectors.selectButton).click(function () {
            me._cssFileSelectorDialog.show();
            Telerik.Sitefinity.centerWindowHorizontally(me._cssFileSelectorDialog);
        });
    },

    dispose: function () {

        if (this._cssFileSelectorDialogCloseDelegate) {
            if (this._masterPageSelectorDialog) {
                this._masterPageSelectorDialog.remove_close(this._cssFileSelectorDialogCloseHandler);
            }

            delete this._cssFileSelectorDialogCloseDelegate;
        }

        Telerik.Sitefinity.Modules.ResponsiveDesign.Web.FieldControls.CssSelectorField.callBaseMethod(this, "dispose");
    },

    /* --------------------  public methods ----------- */

    reset: function () {
    },

    // Gets the value of the field control.
    get_value: function () {

        var selectedFile = $(this._selectors.selectedFileLabel).html();
        var cssValue = {};

        if (selectedFile != null && selectedFile.length > 0) {
            cssValue.CssFilePath = selectedFile;
        }

        return cssValue;
    },

    // Sets the value of the text field control depending on DisplayMode.
    set_value: function (value) {
        if (value) {
            $(this._selectors.selectedFileLabel).show().html(value.CssFilePath);
        }
    },

    // Returns true if the value of the field is changed
    isChanged: function () {
    },

    /* -------------------- events -------------------- */

    /* -------------------- event handlers ------------ */

    _cssFileSelectorDialogCloseHandler: function (sender, args) {
        //This is the path to the master page but we need Id
        var cssValue = args.get_argument();

        if (cssValue && cssValue.Path) {
            $(this._selectors.selectedFileLabel).show().html(cssValue.Path);
        }
        else if (cssValue) {
            $(this._selectors.selectedFileLabel).hide().html('');
        }
    },

    /* -------------------- private methods ----------- */

    _selectors: {
        selectorDialog: "#css-selector-file-explorer-dialog",
        selectButton: "#css-selector-open-file-explorer-button",
        selectedFileLabel: "#css-selector-selected-file"
    },

    /* -------------------- properties ---------------- */

    get_element: function () {
        return this._element;
    },
    set_element: function (value) {
        this._element = value;
    },

    // Gets the dialog for selection a css file
    get_cssFileSelectorDialog: function () {
        return this._cssFileSelectorDialog;
    },
    // Sets the dialog for selection a css file
    set_cssFileSelectorDialog: function (value) {
        this._cssFileSelectorDialog = value;
    }
};

Telerik.Sitefinity.Modules.ResponsiveDesign.Web.FieldControls.CssSelectorField.registerClass("Telerik.Sitefinity.Modules.ResponsiveDesign.Web.FieldControls.CssSelectorField", Telerik.Sitefinity.Web.UI.Fields.FieldControl);
