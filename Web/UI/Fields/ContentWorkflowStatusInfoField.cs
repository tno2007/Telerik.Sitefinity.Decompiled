// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.Fields.ContentWorkflowStatusInfoField
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Web.UI.Fields.Contracts;

namespace Telerik.Sitefinity.Web.UI.Fields
{
  /// <summary>
  /// Control that provides functionality for displaying information for the status of the content workflow
  /// </summary>
  [RequiresDataItem]
  public class ContentWorkflowStatusInfoField : FieldControl, ICommandField
  {
    public static readonly string layoutTemplatePath = ControlUtilities.ToVppPath("Telerik.Sitefinity.Resources.Templates.Fields.ContentWorkflowStatusInfoField.ascx");
    internal const string fieldScript = "Telerik.Sitefinity.Web.UI.Fields.Scripts.ContentWorkflowStatusInfoField.js";
    internal const string requiresDataItemContextInterfaceScript = "Telerik.Sitefinity.Web.UI.Fields.Scripts.IRequiresDataItemContext.js";
    internal const string commandFieldInterfaceScript = "Telerik.Sitefinity.Web.UI.Fields.Scripts.ICommandField.js";

    /// <summary>
    /// Initializes new instances of <c>ContentWorkflowStatusInfoField</c>
    /// </summary>
    public ContentWorkflowStatusInfoField() => this.LayoutTemplatePath = ContentWorkflowStatusInfoField.layoutTemplatePath;

    /// <summary>Gets the name of the embedded layout template.</summary>
    /// <value></value>
    /// <remarks>
    /// Override this property to change the embedded template to be used with the dialog
    /// </remarks>
    protected override string LayoutTemplateName => (string) null;

    /// <summary>
    /// Gets the type (full name) of the script descriptor to be used.
    /// </summary>
    protected override string ScriptDescriptorType => typeof (ContentWorkflowStatusInfoField).FullName;

    /// <summary>
    /// Gets the reference to the control that represents the title of the field control.
    /// Return null if no such control exists in the template.
    /// </summary>
    /// <value></value>
    protected internal override WebControl TitleControl => (WebControl) null;

    /// <summary>
    /// Gets the reference to the control that represents the description of the field control.
    /// Return null if no such control exists in the template.
    /// </summary>
    /// <value></value>
    protected internal override WebControl DescriptionControl => (WebControl) null;

    /// <summary>
    /// Gets the reference to the control that represents the example of the field control.
    /// Return null if no such control exists in the template.
    /// </summary>
    /// <value></value>
    protected internal override WebControl ExampleControl => (WebControl) null;

    /// <summary>
    /// Gets the reference to the control that represents the workflow status.
    /// </summary>
    public Label StatusLabel => this.Container.GetControl<Label>("statusLabel", false);

    /// <summary>
    /// Gets the reference to the control that shows the linking phrase/word between the status and the date labels.
    /// </summary>
    public Label OnLabel => this.Container.GetControl<Label>("onLabel", false);

    /// <summary>
    /// Gets the reference to the control that represents the workflow status date.
    /// </summary>
    public Label DateLabel => this.Container.GetControl<Label>("dateLabel", false);

    /// <summary>
    /// Gets the reference to the control that represents the workflow status date.
    /// </summary>
    public Label PublicationDateLabel => this.Container.GetControl<Label>("publicationDateLabel", false);

    /// <summary>
    /// Gets the reference to the control that represents the hour.
    /// </summary>
    public Label HourLabel => this.Container.GetControl<Label>("hourLabel", false);

    /// <summary>
    /// Gets the reference to the control that represents the author.
    /// </summary>
    public Label AuthorLabel => this.Container.GetControl<Label>("authorLabel", false);

    /// <summary>
    /// Gets or sets the reference to the control that when clicked expands the hidden part of the whole
    /// control.
    /// </summary>
    public LinkButton ExpandButton => this.Container.GetControl<LinkButton>("expandButton", false);

    /// <summary>
    /// Gets or sets the reference to the control that is hidden when <see cref="T:Telerik.Sitefinity.Web.UI.Fields.ContentWorkflowStatusInfoField" /> is not expanded and displayed
    /// upon clicking of the <see cref="P:Telerik.Sitefinity.Web.UI.Fields.ContentWorkflowStatusInfoField.ExpandButton" />.
    /// </summary>
    public Panel ExpandableTarget => this.Container.GetControl<Panel>("expandableTarget", false);

    /// <summary>
    /// Gets the reference to the control that represents "Why rejected" note.
    /// </summary>
    public Label NoteLabel => this.Container.GetControl<Label>("noteLabel", false);

    /// <summary>
    /// Gets or sets the reference to the control that when clicked opens Revision History dialog.
    /// </summary>
    public LinkButton OpenRevisionHistoryButton => this.Container.GetControl<LinkButton>("openRevisionHistoryButton", false);

    /// <summary>
    /// Gets the instance of the <see cref="T:Telerik.Sitefinity.Web.UI.ClientLabelManager" /> component
    /// </summary>
    /// <value>The client label manager.</value>
    public ClientLabelManager ClientLabelManagerComponent => this.Container.GetControl<ClientLabelManager>("clientLabelManager", false);

    /// <summary>
    /// Initialize properties of the field implementing <see cref="T:Telerik.Sitefinity.Web.UI.Fields.Contracts.IField" />
    /// with default values from the configuration definition object.
    /// </summary>
    /// <param name="definition">The definition configuration.</param>
    public override void Configure(IFieldDefinition definition)
    {
      this.ConfigureBaseDefinition(definition);
      this.ConfigureControl((IContentWorkflowStatusInfoFieldDefinition) definition);
    }

    /// <summary>Configures the fields with the definition.</summary>
    /// <param name="statusFieldDefinition">The status field definition.</param>
    internal virtual void ConfigureControl(
      IContentWorkflowStatusInfoFieldDefinition statusFieldDefinition)
    {
    }

    /// <summary>Initializes the controls.</summary>
    /// <param name="container"></param>
    /// <remarks>
    /// Initialize your controls in this method. Do not override CreateChildControls method.
    /// </remarks>
    protected override void InitializeControls(GenericContainer container)
    {
    }

    /// <summary>
    /// Gets a collection of script descriptors that represent ECMAScript (JavaScript) client components.
    /// </summary>
    /// <returns>
    /// An <see cref="T:System.Collections.IEnumerable" /> collection of <see cref="T:System.Web.UI.ScriptDescriptor" /> objects.
    /// </returns>
    public override IEnumerable<ScriptDescriptor> GetScriptDescriptors()
    {
      ScriptControlDescriptor controlDescriptor = this.GetBaseScriptDescriptors().Last<ScriptDescriptor>() as ScriptControlDescriptor;
      controlDescriptor.AddElementProperty("statusLabel", this.StatusLabel.ClientID);
      if (this.OnLabel != null)
        controlDescriptor.AddElementProperty("onLabel", this.OnLabel.ClientID);
      if (this.DateLabel != null)
        controlDescriptor.AddElementProperty("dateLabel", this.DateLabel.ClientID);
      if (this.AuthorLabel != null)
        controlDescriptor.AddElementProperty("authorLabel", this.AuthorLabel.ClientID);
      if (this.ExpandButton != null)
        controlDescriptor.AddElementProperty("expandButton", this.ExpandButton.ClientID);
      if (this.ExpandableTarget != null)
        controlDescriptor.AddElementProperty("expandableTarget", this.ExpandableTarget.ClientID);
      if (this.NoteLabel != null)
        controlDescriptor.AddElementProperty("noteLabel", this.NoteLabel.ClientID);
      if (this.OpenRevisionHistoryButton != null)
      {
        if (SystemManager.IsInlineEditingMode)
          this.OpenRevisionHistoryButton.Visible = false;
        else
          controlDescriptor.AddElementProperty("openRevisionHistoryButton", this.OpenRevisionHistoryButton.ClientID);
      }
      if (this.PublicationDateLabel != null)
        controlDescriptor.AddElementProperty("publicationDateLabel", this.PublicationDateLabel.ClientID);
      if (this.ClientLabelManagerComponent != null)
        controlDescriptor.AddComponentProperty("clientLabelManager", this.ClientLabelManagerComponent.ClientID);
      controlDescriptor.AddProperty("_historyCommandName", (object) "history");
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
      string fullName = typeof (ContentWorkflowStatusInfoField).Assembly.FullName;
      return (IEnumerable<ScriptReference>) new List<ScriptReference>(base.GetScriptReferences())
      {
        new ScriptReference("Telerik.Sitefinity.Web.UI.Fields.Scripts.IRequiresDataItemContext.js", fullName),
        new ScriptReference("Telerik.Sitefinity.Web.UI.Fields.Scripts.ICommandField.js", fullName),
        new ScriptReference("Telerik.Sitefinity.Web.UI.Fields.Scripts.ContentWorkflowStatusInfoField.js", fullName)
      };
    }

    internal virtual IEnumerable<ScriptDescriptor> GetBaseScriptDescriptors() => base.GetScriptDescriptors();

    internal virtual void ConfigureBaseDefinition(IFieldDefinition definition) => base.Configure(definition);
  }
}
