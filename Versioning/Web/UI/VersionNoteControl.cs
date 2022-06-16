// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Versioning.Web.UI.VersionNoteControl
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Sitefinity.Web.UI;
using Telerik.Sitefinity.Web.UI.Fields;
using Telerik.Sitefinity.Web.UI.Fields.Contracts;

namespace Telerik.Sitefinity.Versioning.Web.UI
{
  /// <summary>This control is used for editing version notes</summary>
  [RequiresDataItem]
  public class VersionNoteControl : FieldControl
  {
    public static readonly string layoutTemplateName = ControlUtilities.ToVppPath("Telerik.Sitefinity.Resources.Templates.Backend.Versioning.VersionNoteControl.ascx");
    internal const string script = "Telerik.Sitefinity.Versioning.Web.UI.Scripts.VersionNoteControl.js";
    internal const string serviceBaseUrl = "~/Sitefinity/Services/Versioning/HistoryService.svc";

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Versioning.Web.UI.VersionNoteControl" /> class.
    /// </summary>
    public VersionNoteControl() => this.LayoutTemplatePath = VersionNoteControl.layoutTemplateName;

    /// <inheritdoc />
    protected override string LayoutTemplateName => (string) null;

    /// <summary>
    /// Gets the version info label. (date and who created the version)
    /// </summary>
    /// <value>The version info label.</value>
    /// 
    ///             TODO: make protecteed when JustMock is fixed to mock non public members
    public virtual WebControl VersionInfoLabel => this.Container.GetControl<WebControl>("versionInfoLabel", true);

    /// <summary>Gets the note view label.</summary>
    /// <value>The note view label.</value>
    /// 
    ///             TODO: make protecteed when JustMock is fixed to mock non public members
    public virtual WebControl NoteViewLabel => this.Container.GetControl<WebControl>("noteViewLabel", true);

    /// <summary>Gets the note view label.</summary>
    /// <value>The note view label.</value>
    /// 
    ///             TODO: make protecteed when JustMock is fixed to mock non public members
    public virtual PromptDialog PromptDialog => this.Container.GetControl<PromptDialog>("promptDialog1", true);

    /// <summary>The button that initiates the note editing</summary>
    /// 
    ///             TODO: make protecteed when JustMock is fixed to mock non public members
    internal virtual LinkButton EditButton => this.Container.GetControl<LinkButton>("editButton", true);

    /// <summary>The button that deletes the current note</summary>
    /// 
    ///             TODO: make protecteed when JustMock is fixed to mock non public members
    public virtual LinkButton DeleteButton => this.Container.GetControl<LinkButton>("deleteButton", true);

    /// <summary>Gets the button area panel.</summary>
    /// <value>The button area panel.</value>
    public virtual Panel ButtonAreaPanel => this.Container.GetControl<Panel>("buttonArea", true);

    /// <summary>
    /// Get reference to the client label manager in the template
    /// </summary>
    public virtual ClientLabelManager ClientLabelManager => this.Container.GetControl<ClientLabelManager>("clientLabelManager", true);

    /// <summary>
    /// Initialize properties of the field implementing <see cref="T:Telerik.Sitefinity.Web.UI.Fields.Contracts.IField" />
    /// with default values from the configuration definition object.
    /// </summary>
    /// <param name="definition">The definition configuration.</param>
    public override void Configure(IFieldDefinition definition) => base.Configure(definition);

    protected override void InitializeControls(GenericContainer container)
    {
    }

    /// <summary>
    /// Gets the reference to the control that represents the title of the field control.
    /// Return null if no such control exists in the template.
    /// </summary>
    /// <value>The control displaying the title of the field.</value>
    protected internal override WebControl TitleControl => (WebControl) null;

    /// <summary>
    /// Gets the reference to the control that represents the description of the field control.
    /// Return null if no such control exists in the template.
    /// </summary>
    /// <value>The control displaying the description of the field.</value>
    protected internal override WebControl DescriptionControl => (WebControl) null;

    /// <summary>
    /// Gets the reference to the control that represents the example of the field control.
    /// Return null if no such control exists in the template.
    /// </summary>
    /// <value>The control displaying the sample usage of the field.</value>
    protected internal override WebControl ExampleControl => (WebControl) null;

    public override IEnumerable<ScriptDescriptor> GetScriptDescriptors()
    {
      ScriptControlDescriptor controlDescriptor = this.GetBaseScriptDescriptors().Last<ScriptDescriptor>() as ScriptControlDescriptor;
      controlDescriptor.AddElementProperty("editButton", this.EditButton.ClientID);
      controlDescriptor.AddElementProperty("deleteButton", this.DeleteButton.ClientID);
      controlDescriptor.AddElementProperty("noteViewLabel", this.NoteViewLabel.ClientID);
      controlDescriptor.AddElementProperty("versionInfoLabel", this.VersionInfoLabel.ClientID);
      controlDescriptor.AddComponentProperty("promptDialogComponent", this.PromptDialog.ClientID);
      controlDescriptor.AddProperty("_serviceBaseUrl", (object) this.AbsoluteServiceUrl);
      controlDescriptor.AddProperty("_buttonAreaPanelId", (object) this.ButtonAreaPanel.ClientID);
      controlDescriptor.AddComponentProperty("clientLabelManager", this.ClientLabelManager.ClientID);
      return (IEnumerable<ScriptDescriptor>) new ScriptControlDescriptor[1]
      {
        controlDescriptor
      };
    }

    public override IEnumerable<ScriptReference> GetScriptReferences()
    {
      string fullName = typeof (VersionNoteControl).Assembly.FullName;
      return (IEnumerable<ScriptReference>) new List<ScriptReference>(base.GetScriptReferences())
      {
        new ScriptReference("Telerik.Sitefinity.Web.UI.Fields.Scripts.IRequiresDataItemContext.js", fullName),
        new ScriptReference("Telerik.Sitefinity.Versioning.Web.UI.Scripts.VersionNoteControl.js", fullName)
      };
    }

    internal virtual IEnumerable<ScriptDescriptor> GetBaseScriptDescriptors() => base.GetScriptDescriptors();

    /// TODO: make protecteed when JustMock is fixed to mock non public members
    public virtual string AbsoluteServiceUrl => VirtualPathUtility.ToAbsolute("~/Sitefinity/Services/Versioning/HistoryService.svc");
  }
}
