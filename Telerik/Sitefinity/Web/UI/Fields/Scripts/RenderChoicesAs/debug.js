Type.registerNamespace("Telerik.Sitefinity.Web.UI.Fields");

Telerik.Sitefinity.Web.UI.Fields.RenderChoicesAs = function() {
    /// <summary>
    /// Enumeration represents different ways in which choices of the choice field can be rendered.
    /// </summary>
};
Telerik.Sitefinity.Web.UI.Fields.RenderChoicesAs.prototype =
{
        /// <summary>
        /// Choices are rendered as checkboxes.
        /// </summary>
        CheckBoxes : 0,

        /// <summary>
        /// Choices are rendered as items of a drop down box.
        /// </summary>
        DropDown : 1,

        /// <summary>
        /// Choices are rendered as items of a list box.
        /// </summary>
        ListBox : 2,

        /// <summary>
        /// Choices are rendered as radiobuttons.
        /// </summary>
        RadioButtons : 3,

        /// <summary>
        /// A single checkbox for setting values to boolean properties
        /// </summary>
        SingleCheckBox : 4,

        /// <summary>
        /// Choices are rendered as horizontal radiobuttons
        /// </summary>
        HorizontalRadioButtons : 5
}
Telerik.Sitefinity.Web.UI.Fields.RenderChoicesAs.registerEnum("Telerik.Sitefinity.Web.UI.Fields.RenderChoicesAs");