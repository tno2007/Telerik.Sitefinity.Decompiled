// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.Fields.StatusField
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Web.UI.Fields.Contracts;
using Telerik.Sitefinity.Web.UI.Fields.Enums;

namespace Telerik.Sitefinity.Web.UI.Fields
{
  /// <summary>
  /// Represents a status field control. Used for viewing and editing the scheduling of content items in
  /// Sitefinity modules
  /// </summary>
  public class StatusField : CompositeFieldControl
  {
    public static readonly string layoutTemplatePath = ControlUtilities.ToVppPath("Telerik.Sitefinity.Resources.Templates.Fields.StatusField.ascx");
    internal const string statusFieldScript = "Telerik.Sitefinity.Web.UI.Fields.Scripts.StatusField.js";
    internal const string fieldDisplayModeScript = "Telerik.Sitefinity.Web.UI.Fields.Scripts.FieldDisplayMode.js";

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Web.UI.Fields.StatusField" /> class.
    /// </summary>
    public StatusField() => this.LayoutTemplatePath = StatusField.layoutTemplatePath;

    /// <summary>Gets or sets the name of the status data field.</summary>
    /// <value>The name of the status data field.</value>
    public string StatusDataFieldName { get; set; }

    /// <summary>Gets the name of the embedded layout template.</summary>
    /// <value></value>
    /// <remarks>
    /// Override this property to change the embedded template to be used with the dialog
    /// </remarks>
    protected override string LayoutTemplateName => (string) null;

    public virtual IDateFieldDefinition ExpirationDateFieldDefinition { get; set; }

    public virtual IDateFieldDefinition PublicationDateFieldDefinition { get; set; }

    public virtual IChoiceFieldDefinition StatusFieldDefinition { get; set; }

    /// <summary>
    /// Gets the reference to the label that displays the title of the status field.
    /// </summary>
    protected internal override WebControl TitleControl => (WebControl) this.Container.GetControl<Label>("titleLabel", true);

    /// <summary>
    /// Gets the reference to the label that displays the description of the status field.
    /// </summary>
    protected internal override WebControl DescriptionControl => (WebControl) this.Container.GetControl<Label>("descriptionLabel", this.DisplayMode == FieldDisplayMode.Write);

    /// <summary>
    /// Gets the reference to the label that displays the example of the status field.
    /// </summary>
    protected internal override WebControl ExampleControl => (WebControl) this.Container.GetControl<Label>("exampleLabel", this.DisplayMode == FieldDisplayMode.Write);

    /// <summary>
    /// Gets the field control used to view and edit the publication date.
    /// </summary>
    /// <value>The publication date field control.</value>
    protected internal virtual DateField PublicationDateField => this.Container.GetControl<DateField>("dateField", this.DisplayMode == FieldDisplayMode.Write);

    /// <summary>
    /// Gets the field control used to view and edit the expiration date.
    /// </summary>
    /// <value>The publication date field control.</value>
    protected internal virtual DateField ExpirationDateField => this.Container.GetControl<DateField>("expirationDateField", this.DisplayMode == FieldDisplayMode.Write);

    /// <summary>
    /// Gets the choice field control used to view and edit the state of the content item.
    /// </summary>
    /// <value>The choice field control.</value>
    protected internal virtual ChoiceField StatusFieldControl => this.Container.GetControl<ChoiceField>("statusField", this.DisplayMode == FieldDisplayMode.Write);

    /// <summary>Gets the label that is displayed in Read Mode</summary>
    protected internal virtual Label ReadModeLabel => this.Container.GetControl<Label>("read", false);

    /// <summary>
    /// Gets the label that shows the message when swithching between choices
    /// </summary>
    protected internal virtual Label MessageLabel => this.Container.GetControl<Label>("messageLabel", this.DisplayMode == FieldDisplayMode.Write);

    /// <summary>Panel containig the both date controls</summary>
    protected internal virtual Panel DatesPanel => this.Container.GetControl<Panel>("datesPanel", this.DisplayMode == FieldDisplayMode.Write);

    /// <summary>Initializes the controls.</summary>
    /// <param name="container"></param>
    /// <remarks>
    /// Initialize your controls in this method. Do not override CreateChildControls method.
    /// </remarks>
    protected override void InitializeControls(GenericContainer container)
    {
      ((ITextControl) this.TitleControl).SetTextOrHide(this.Title);
      ((ITextControl) this.DescriptionControl).SetTextOrHide(this.Description);
      ((ITextControl) this.ExampleControl).SetTextOrHide(this.Example);
      this.PublicationDateField.Configure((IFieldDefinition) this.PublicationDateFieldDefinition);
      this.ExpirationDateField.Configure((IFieldDefinition) this.ExpirationDateFieldDefinition);
      this.StatusFieldControl.Configure((IFieldDefinition) this.StatusFieldDefinition);
      if (!string.IsNullOrEmpty(this.StatusDataFieldName))
        this.StatusFieldControl.DataFieldName = this.StatusDataFieldName;
      if (this.DisplayMode == FieldDisplayMode.Write)
      {
        foreach (ChoiceItem statuse in this.GetStatuses())
          this.StatusFieldControl.Choices.Add(statuse);
        this.StatusFieldControl.TabIndex = this.TabIndex;
        this.PublicationDateField.TabIndex = this.TabIndex;
        this.ExpirationDateField.TabIndex = this.TabIndex;
        this.TabIndex = (short) 0;
      }
      this.DatesPanel.Style.Add("display", "none");
    }

    /// <summary>
    /// Initialize properties of the field implementing <see cref="T:Telerik.Sitefinity.Web.UI.Fields.Contracts.IField" />
    /// with default values from the configuration definition object.
    /// </summary>
    /// <param name="definition">The definition configuration.</param>
    public override void Configure(IFieldDefinition definition)
    {
      this.ConfigureBaseDefinition(definition);
      this.ConfigureFields((IStatusFieldDefinition) definition);
    }

    public override string JavaScriptComponentName => typeof (StatusField).FullName;

    /// <summary>Configures the fields with the definition.</summary>
    /// <param name="statusFieldDefinition">The status field definition.</param>
    public virtual void ConfigureFields(IStatusFieldDefinition statusFieldDefinition)
    {
      this.ExpirationDateFieldDefinition = statusFieldDefinition.ExpirationDateFieldDefinition;
      this.PublicationDateFieldDefinition = statusFieldDefinition.PublicationDateFieldDefinition;
      this.StatusFieldDefinition = statusFieldDefinition.StatusFieldControlDefinition;
    }

    private IEnumerable<ChoiceItem> GetStatuses() => (IEnumerable<ChoiceItem>) new List<ChoiceItem>()
    {
      new ChoiceItem()
      {
        Text = Res.Get<Labels>("Draft"),
        Value = 0.ToString(),
        Description = Res.Get<Labels>().DraftIsNotPublicallyVisibleOnTheWebsite
      },
      new ChoiceItem()
      {
        Text = Res.Get<Labels>("Published"),
        Value = 2.ToString(),
        Description = Res.Get<Labels>().PublishedStatusDescription
      },
      new ChoiceItem()
      {
        Text = Res.Get<Labels>("Scheduled"),
        Value = 3.ToString(),
        Description = Res.Get<Labels>().ScheduledStatusDescription
      }
    }.ToArray();

    /// <summary>
    /// Gets a collection of script descriptors that represent ECMAScript (JavaScript) client components.
    /// </summary>
    /// <returns>
    /// An <see cref="T:System.Collections.IEnumerable" /> collection of <see cref="T:System.Web.UI.ScriptDescriptor" /> objects.
    /// </returns>
    public override IEnumerable<ScriptDescriptor> GetScriptDescriptors()
    {
      ScriptControlDescriptor controlDescriptor = this.GetBaseScriptDescriptors().Last<ScriptDescriptor>() as ScriptControlDescriptor;
      if (this.DisplayMode == FieldDisplayMode.Write)
      {
        controlDescriptor.AddComponentProperty("expirationDateComponent", this.ExpirationDateField.ClientID);
        controlDescriptor.AddComponentProperty("publicationDateComponent", this.PublicationDateField.ClientID);
        controlDescriptor.AddComponentProperty("statusChoiseComponent", this.StatusFieldControl.ClientID);
        controlDescriptor.AddElementProperty("messageLabel", this.MessageLabel.ClientID);
        controlDescriptor.AddElementProperty("datesPanel", this.DatesPanel.ClientID);
        Dictionary<string, string> dictionary = new Dictionary<string, string>();
        foreach (ChoiceItem choice in this.StatusFieldControl.Choices)
          dictionary.Add(choice.Value, choice.Description);
        controlDescriptor.AddProperty("choiceDescriptions", (object) dictionary);
      }
      else if (this.ReadModeLabel != null)
        controlDescriptor.AddElementProperty("readModeLabel", this.ReadModeLabel.ClientID);
      return (IEnumerable<ScriptDescriptor>) new ScriptControlDescriptor[1]
      {
        controlDescriptor
      };
    }

    /// <summary>
    /// Gets a collection of <see cref="T:System.Web.UI.ScriptReference" /> objects that define script resources that the control requires.
    /// </summary>
    /// <returns>
    /// An <see cref="T:System.Collections.IEnumerable" /> collection of <see cref="T:System.Web.UI.ScriptReference" /> objects.
    /// </returns>
    public override IEnumerable<ScriptReference> GetScriptReferences()
    {
      string fullName = typeof (StatusField).Assembly.FullName;
      return (IEnumerable<ScriptReference>) new List<ScriptReference>(base.GetScriptReferences())
      {
        new ScriptReference("Telerik.Sitefinity.Web.UI.Fields.Scripts.FieldDisplayMode.js", fullName),
        new ScriptReference("Telerik.Sitefinity.Web.UI.Fields.Scripts.StatusField.js", fullName)
      };
    }

    internal virtual void ConfigureBaseDefinition(IFieldDefinition definition) => base.Configure(definition);

    internal virtual IEnumerable<ScriptDescriptor> GetBaseScriptDescriptors() => base.GetScriptDescriptors();
  }
}
