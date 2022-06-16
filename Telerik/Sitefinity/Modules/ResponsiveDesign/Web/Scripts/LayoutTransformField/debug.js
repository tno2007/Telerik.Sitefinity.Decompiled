/// <reference name="MicrosoftAjax.js"/>
/// <reference name="Telerik.Sitefinity.Resources.Scripts.jquery-1.6.3-vsdoc.js" assembly="Telerik.Sitefinity.Resources"/>
Type.registerNamespace("Telerik.Sitefinity.Modules.ResponsiveDesign.Web.FieldControls");

Telerik.Sitefinity.Modules.ResponsiveDesign.Web.FieldControls.LayoutTransformField = function (element) {

    Telerik.Sitefinity.Modules.ResponsiveDesign.Web.FieldControls.LayoutTransformField.initializeBase(this, [element]);
    this._element = element;
    this._transformations = [];
}

Telerik.Sitefinity.Modules.ResponsiveDesign.Web.FieldControls.LayoutTransformField.prototype =
{
    initialize: function () {

        var me = this;

        // wire up all the select buttons
        $(this._selectors.selectButtons).click(function () {
            var name = $(this).attr("data-sf-name");
            var groupName = $(this).attr("data-sf-group-name");
            me._deselectAllLayoutsInGroup(groupName);
            // add the sfSel class to the button that was clicked
            $(this).addClass("sfSel");
            // register transformation
            me._registerTransformation(name, groupName);
        });

        Telerik.Sitefinity.Modules.ResponsiveDesign.Web.FieldControls.LayoutTransformField.callBaseMethod(this, "initialize");
    },

    dispose: function () {
        Telerik.Sitefinity.Modules.ResponsiveDesign.Web.FieldControls.LayoutTransformField.callBaseMethod(this, "dispose");
    },

    /* --------------------  public methods ----------- */

    reset: function () {
        this._transformations.length = 0;
        $(this._selectors.selectButtons).removeClass("sfSel");
        $(this._selectors.originalLayouts).addClass("sfSel");
    },

    // Gets the value of the field control.
    get_value: function () {
        return this._transformations;
    },

    // Sets the value of the text field control depending on DisplayMode.
    set_value: function (value) {
        if (value) {
            this._transformations = value;
            var transformationsCount = this._transformations.length;
            while (transformationsCount--) {
                // deselect all items in the group
                var transformation = this._transformations[transformationsCount];
                this._deselectAllLayoutsInGroup(transformation.OriginalLayoutElementName);
                this._selectLayout(transformation.AlternatLayoutElementName, transformation.OriginalLayoutElementName);
            }
        }
    },

    // Returns true if the value of the field is changed
    isChanged: function () {
    },

    /* -------------------- events -------------------- */

    /* -------------------- event handlers ------------ */

    /* -------------------- private methods ----------- */

    _registerTransformation: function (layoutName, groupName) {

        
        var existingIndex = -1;
        for (i = 0; i < this._transformations.length; i++) {
            var transformation = this._transformations[i];
            if (transformation.OriginalLayoutElementName == groupName) {
                existingIndex = i;
                break;
            }
        }

        if (layoutName == groupName) {
            // means no transformations should be done; an alternation item from the array should be removed
            if (existingIndex > -1) {
                this._transformations.splice(existingIndex, 1);
            }

        } 
        else {
            if (existingIndex > -1) {
                this._transformations[existingIndex].AlternatLayoutElementName = layoutName;
            }
            else {
                this._transformations.push({ "OriginalLayoutElementName": groupName, "AlternatLayoutElementName": layoutName });
            }
        }
    },


    /* deselects all the layouts in the given group */
    _deselectAllLayoutsInGroup: function (groupName) {
        // remove sfSel class for all the group members
        $('a[data-sf-group-name="' + groupName + '"]').removeClass("sfSel");
    },

    _selectLayout: function (layoutName, groupName) {
        $('a[data-sf-group-name="' + groupName + '"]').each(function () {
            if ($(this).attr("data-sf-name") == layoutName) {
                $(this).addClass("sfSel");
            }
        });
    },

    _selectors: {
        selectButtons: ".layout-transform-select-button",
        originalLayouts: ".sfOriginalLayout"
    },

    /* -------------------- properties ---------------- */

    get_element: function () {
        return this._element;
    },
    set_element: function (value) {
        this._element = value;
    }
};

Telerik.Sitefinity.Modules.ResponsiveDesign.Web.FieldControls.LayoutTransformField.registerClass("Telerik.Sitefinity.Modules.ResponsiveDesign.Web.FieldControls.LayoutTransformField", Telerik.Sitefinity.Web.UI.Fields.FieldControl);
