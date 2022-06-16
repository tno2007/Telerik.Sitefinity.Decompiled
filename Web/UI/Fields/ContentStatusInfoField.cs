// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.Fields.ContentStatusInfoField
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace Telerik.Sitefinity.Web.UI.Fields
{
  /// <summary>
  /// Control that provides functionality for displaying information for the status of the content
  /// </summary>
  /// <seealso cref="T:Telerik.Sitefinity.Web.UI.Fields.FieldControl" />
  public class ContentStatusInfoField : FieldControl
  {
    private static readonly string LayoutTemplatePathName = ControlUtilities.ToVppPath("Telerik.Sitefinity.Resources.Templates.Fields.ContentStatusInfoField.ascx");
    internal const string FieldScript = "Telerik.Sitefinity.Web.UI.Fields.Scripts.ContentStatusInfoField.js";

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Web.UI.Fields.ContentStatusInfoField" /> class.
    /// </summary>
    public ContentStatusInfoField() => this.LayoutTemplatePath = ContentStatusInfoField.LayoutTemplatePathName;

    /// <inheritdoc />
    protected override string LayoutTemplateName => (string) null;

    /// <inheritdoc />
    protected override string ScriptDescriptorType => typeof (ContentStatusInfoField).FullName;

    /// <summary>
    /// Gets the reference to the control that represents the status.
    /// </summary>
    public Label StatusLabel => this.Container.GetControl<Label>("statusLabel", true);

    /// <summary>
    /// Gets the reference to the control that shows the linking phrase/word between the status and the date labels.
    /// </summary>
    public Label OnLabel => this.Container.GetControl<Label>("onLabel", true);

    /// <summary>
    /// Gets the reference to the control that represents the last modified date label.
    /// </summary>
    public Label LastModifiedDateLabel => this.Container.GetControl<Label>("lastModifiedDateLabel", true);

    /// <summary>
    /// Gets the reference to the control that represents the author.
    /// </summary>
    public Label AuthorLabel => this.Container.GetControl<Label>("authorLabel", true);

    /// <summary>
    /// Gets the reference to the control that represents the start date label.
    /// </summary>
    public Label StartDateLabel => this.Container.GetControl<Label>("startDateLabel", true);

    /// <summary>
    /// Gets the reference to the control that represents the stop date label.
    /// </summary>
    public Label StopDateLabel => this.Container.GetControl<Label>("stopDateLabel", true);

    /// <summary>Gets the view results button.</summary>
    /// <value>The view results button.</value>
    public HtmlAnchor ViewResultsButton => this.Container.GetControl<HtmlAnchor>("viewResultsButton", true);

    /// <inheritdoc />
    public override IEnumerable<ScriptDescriptor> GetScriptDescriptors()
    {
      ScriptControlDescriptor controlDescriptor = base.GetScriptDescriptors().Last<ScriptDescriptor>() as ScriptControlDescriptor;
      controlDescriptor.AddElementProperty("statusLabel", this.StatusLabel.ClientID);
      controlDescriptor.AddElementProperty("dateLabel", this.LastModifiedDateLabel.ClientID);
      controlDescriptor.AddElementProperty("authorLabel", this.AuthorLabel.ClientID);
      controlDescriptor.AddElementProperty("startDateLabel", this.StartDateLabel.ClientID);
      controlDescriptor.AddElementProperty("stopDateLabel", this.StopDateLabel.ClientID);
      controlDescriptor.AddElementProperty("viewResultsButton", this.ViewResultsButton.ClientID);
      return (IEnumerable<ScriptDescriptor>) new ScriptControlDescriptor[1]
      {
        controlDescriptor
      };
    }

    /// <inheritdoc />
    public override IEnumerable<ScriptReference> GetScriptReferences()
    {
      string fullName = typeof (ContentStatusInfoField).Assembly.FullName;
      return (IEnumerable<ScriptReference>) new List<ScriptReference>(base.GetScriptReferences())
      {
        new ScriptReference("Telerik.Sitefinity.Web.UI.Fields.Scripts.ContentStatusInfoField.js", fullName)
      };
    }

    /// <inheritdoc />
    protected override void InitializeControls(GenericContainer container)
    {
    }
  }
}
