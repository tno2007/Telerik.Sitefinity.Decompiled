// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.Fields.DateField
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Sitefinity.Web.UI.Extenders;
using Telerik.Sitefinity.Web.UI.Fields.Config;
using Telerik.Sitefinity.Web.UI.Fields.Contracts;
using Telerik.Sitefinity.Web.UI.Fields.Enums;

namespace Telerik.Sitefinity.Web.UI.Fields
{
  /// <summary>
  /// Represents a field control for representing and editing dates
  /// </summary>
  [FieldDefinitionElement(typeof (DateFieldElement))]
  [RequiresDataItem]
  public class DateField : FieldControl, IExpandableControl
  {
    private bool? expanded = new bool?(true);
    internal const string utcOffsetModeScript = "Telerik.Sitefinity.Web.UI.Fields.Scripts.UtcOffsetMode.js";
    internal const string dateTimeDisplayModeScript = "Telerik.Sitefinity.Web.UI.Fields.Scripts.DateTimeDisplayMode.js";
    private const string dateFieldScript = "Telerik.Sitefinity.Web.UI.Fields.Scripts.DateField.js";
    private const string fieldDisplayModeScript = "Telerik.Sitefinity.Web.UI.Fields.Scripts.FieldDisplayMode.js";
    private const string reqDataItemScriptFileName = "Telerik.Sitefinity.Web.UI.Fields.Scripts.IRequiresDataItem.js";
    public static readonly string layoutTemplatePath = ControlUtilities.ToVppPath("Telerik.Sitefinity.Resources.Templates.Fields.DateField.ascx");

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Web.UI.Fields.DateField" /> class.
    /// </summary>
    public DateField() => this.LayoutTemplatePath = DateField.layoutTemplatePath;

    /// <summary>Gets the name of the embedded layout template.</summary>
    /// <value></value>
    /// <remarks>
    /// Override this property to change the embedded template to be used with the dialog
    /// </remarks>
    protected override string LayoutTemplateName => (string) null;

    /// <summary>Gets or sets the display mode of the control.</summary>
    /// <value>The display mode.</value>
    [DefaultValue(DateTimeDisplayMode.DateTime)]
    public DateTimeDisplayMode DateTimeDisplayMode { get; set; }

    /// <summary>
    /// Gets or sets the UtcOffset mode of the control. When Client is selected the control returns the selected date and time using the client's utc offset.
    /// When Custom is selected the control returns the selected date and time regarding to the Utc offset specified by the DataItem. The Utc offset is stored into the DataItem's property which name is equal to the UtcOffsetFieldName property value.
    /// </summary>
    /// <value>The UtcOffset mode.</value>
    [DefaultValue(UtcOffsetMode.Client)]
    public UtcOffsetMode UtcOffsetMode { get; set; }

    /// <summary>
    /// Gets or sets the name of the field of the DataItem that contains the UtcOffset data.
    /// </summary>
    /// <value>The name of the field.</value>
    public string UtcOffsetFieldName { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the field is localizable.
    /// </summary>
    /// <value>A value indicating whether the field is localizable.</value>
    public bool IsLocalizable { get; set; }

    /// <summary>
    /// Gets or sets the text that will be displayed on the control that can expand the hidden part.
    /// </summary>
    /// <value></value>
    public string ExpandText { get; set; }

    /// <summary>
    /// Gets or sets the value indicating whether the control is expanded by default. If control is to
    /// be expanded by default true; otherwise false.
    /// </summary>
    /// <value></value>
    public bool? Expanded
    {
      get => this.expanded;
      set => this.expanded = value;
    }

    /// <summary>
    /// Gets or sets the reference to the control that when clicked expands the hidden part of the whole
    /// control.
    /// </summary>
    /// <value></value>
    public WebControl ExpandControl => (WebControl) this.ExpandLink;

    /// <summary>
    /// Gets or sets the reference to the control that is hidden when Expanded is false and displayed
    /// upon clicking of the ExpandControl.
    /// </summary>
    /// <value></value>
    public WebControl ExpandTarget => this.Container.GetControl<WebControl>("expandableTarget", this.DisplayMode == FieldDisplayMode.Write);

    /// <summary>
    /// Gets the reference to the control that represents the title of the field control.
    /// Return null if no such control exists in the template.
    /// </summary>
    /// <value></value>
    protected internal override WebControl TitleControl => (WebControl) this.TitleLabel;

    /// <summary>
    /// Gets the reference to the control that represents the example of the field control.
    /// Return null if no such control exists in the template.
    /// </summary>
    /// <value></value>
    protected internal override WebControl ExampleControl => (WebControl) this.ExampleLabel;

    /// <summary>
    /// Gets the reference to the control that represents the description of the field control.
    /// Return null if no such control exists in the template.
    /// </summary>
    /// <value></value>
    protected internal override WebControl DescriptionControl => (WebControl) this.DescriptionLabel;

    /// <summary>
    /// Gets the reference to the label that displays the title of date field.
    /// </summary>
    protected internal virtual Label TitleLabel => this.Container.GetControl<Label>("titleLabel_" + this.DisplayMode.ToString().ToLower(), false);

    /// <summary>
    /// Gets the reference to the label that displays the description of the date field.
    /// </summary>
    protected internal virtual Label DescriptionLabel => this.Container.GetControl<Label>("descriptionLabel", false);

    /// <summary>
    /// Gets the reference to the label that displays the example of the date field.
    /// </summary>
    protected internal virtual Label ExampleLabel => this.Container.GetControl<Label>("exampleLabel", false);

    /// <summary>
    /// Gets the control representing the date field in read mode
    /// </summary>
    /// <value>The control representing the date field in read mode.</value>
    protected internal virtual ITextControl TextControl => this.Container.GetControl<ITextControl>("dateAsText", this.DisplayMode == FieldDisplayMode.Read);

    /// <summary>
    /// Reference to the link which causes the control to expand or collapse
    /// </summary>
    /// <value>The expand link.</value>
    protected internal virtual LinkButton ExpandLink => this.Container.GetControl<LinkButton>("expandButton", this.DisplayMode == FieldDisplayMode.Write);

    /// <summary>
    /// Gets the text box used for the jQuery Datepicker control.
    /// </summary>
    protected internal virtual TextBox DatePicker => this.Container.GetControl<TextBox>("datePicker", this.DisplayMode == FieldDisplayMode.Write);

    /// <summary>Initializes the controls.</summary>
    /// <param name="container"></param>
    /// <remarks>
    /// Initialize your controls in this method. Do not override CreateChildControls method.
    /// </remarks>
    protected override void InitializeControls(GenericContainer container)
    {
      this.TitleLabel.SetTextOrHide(this.Title);
      this.DescriptionLabel.SetTextOrHide(this.Description);
      this.ExampleLabel.SetTextOrHide(this.Example);
      DateTime? nullable = new DateTime?();
      if (this.Value != null)
        nullable = this.Value as DateTime?;
      switch (this.DisplayMode)
      {
        case FieldDisplayMode.Read:
          if (nullable.HasValue)
            this.TextControl.Text = nullable.Value.ToString(this.DataFormat);
          (this.TextControl as Label).TabIndex = this.TabIndex;
          break;
        case FieldDisplayMode.Write:
          this.DatePicker.TextChanged += new EventHandler(this.DatePicker_SelectedDateChanged);
          if (!this.Expanded.GetValueOrDefault())
            this.ExpandControl.TabIndex = this.TabIndex;
          this.TabIndex = (short) 0;
          break;
      }
    }

    /// <summary>
    /// Initialize properties of the field implementing <see cref="T:Telerik.Sitefinity.Web.UI.Fields.Contracts.IField" />
    /// with default values from the configuration definition object.
    /// </summary>
    /// <param name="definition">The definition configuration.</param>
    public override void Configure(IFieldDefinition definition)
    {
      base.Configure(definition);
      if (!(definition is IDateFieldDefinition dateFieldDefinition))
        return;
      this.UtcOffsetMode = dateFieldDefinition.UtcOffsetMode;
      this.UtcOffsetFieldName = dateFieldDefinition.UtcOffsetFiledName;
      this.IsLocalizable = dateFieldDefinition.IsLocalizable;
    }

    /// <summary>
    /// Gets a collection of script descriptors that represent ECMAScript (JavaScript) client components.
    /// </summary>
    /// <returns>
    /// An <see cref="T:System.Collections.IEnumerable" /> collection of <see cref="T:System.Web.UI.ScriptDescriptor" /> objects.
    /// </returns>
    public override IEnumerable<ScriptDescriptor> GetScriptDescriptors()
    {
      List<ScriptDescriptor> scriptDescriptors = new List<ScriptDescriptor>();
      if (this.DisplayMode == FieldDisplayMode.Write)
        scriptDescriptors.Add((ScriptDescriptor) this.GetExpandableExtenderDescriptor(this.ClientID));
      ScriptControlDescriptor controlDescriptor = (ScriptControlDescriptor) base.GetScriptDescriptors().Last<ScriptDescriptor>();
      if (this.DisplayMode == FieldDisplayMode.Write)
        controlDescriptor.AddProperty("datePickerId", (object) this.DatePicker.ClientID);
      else
        controlDescriptor.AddElementProperty("textControl", ((Control) this.TextControl).ClientID);
      controlDescriptor.AddProperty("_dateTimeDisplayMode", (object) this.DateTimeDisplayMode);
      controlDescriptor.AddProperty("_utcOffsetMode", (object) this.UtcOffsetMode);
      controlDescriptor.AddProperty("_utcOffsetFieldName", (object) this.UtcOffsetFieldName);
      controlDescriptor.AddProperty("_isLocalizable", (object) this.IsLocalizable);
      scriptDescriptors.Add((ScriptDescriptor) controlDescriptor);
      return (IEnumerable<ScriptDescriptor>) scriptDescriptors;
    }

    /// <summary>
    /// Gets a collection of <see cref="T:System.Web.UI.ScriptReference" /> objects that define script resources that the control requires.
    /// </summary>
    /// <returns>
    /// An <see cref="T:System.Collections.IEnumerable" /> collection of <see cref="T:System.Web.UI.ScriptReference" /> objects.
    /// </returns>
    public override IEnumerable<ScriptReference> GetScriptReferences()
    {
      string fullName = typeof (DateField).Assembly.FullName;
      return (IEnumerable<ScriptReference>) new List<ScriptReference>(base.GetScriptReferences())
      {
        this.GetExpandableExtenderScript(),
        new ScriptReference("Telerik.Sitefinity.Web.UI.Fields.Scripts.IRequiresDataItem.js", fullName),
        new ScriptReference("Telerik.Sitefinity.Web.UI.Fields.Scripts.DateField.js", fullName),
        new ScriptReference("Telerik.Sitefinity.Web.UI.Fields.Scripts.FieldDisplayMode.js", fullName),
        new ScriptReference("Telerik.Sitefinity.Web.Scripts.UserPreferences.js", fullName),
        new ScriptReference("Telerik.Sitefinity.Resources.Scripts.jquery-ui-1.12.1.custom.min.js", "Telerik.Sitefinity.Resources"),
        new ScriptReference("Telerik.Sitefinity.Resources.Scripts.jquery-ui-timepicker-addon.js", "Telerik.Sitefinity.Resources"),
        new ScriptReference("Telerik.Sitefinity.Web.UI.Fields.Scripts.DateTimeDisplayMode.js", fullName),
        new ScriptReference("Telerik.Sitefinity.Web.UI.Fields.Scripts.UtcOffsetMode.js", fullName)
      }.ToArray();
    }

    /// <summary>
    /// Handles the ServerChanged event of the textbox component of the DatePicker control.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="T:System.EventArgs" /> instance containing the event data.</param>
    private void DatePicker_SelectedDateChanged(object sender, EventArgs e) => this.Value = (object) DateTime.Parse(this.DatePicker.Text);
  }
}
