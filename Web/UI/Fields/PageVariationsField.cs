// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.Fields.PageVariationsField
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Telerik.Sitefinity.Web.UI.Fields
{
  /// <summary>Field control for input of variations</summary>
  /// <seealso cref="T:Telerik.Sitefinity.Web.UI.Fields.FieldControl" />
  public class PageVariationsField : FieldControl
  {
    private const string ControlScript = "Telerik.Sitefinity.Web.UI.Fields.Scripts.PageVariationsField.js";
    private static readonly string LayoutTemplatePathValue = ControlUtilities.ToVppPath("Telerik.Sitefinity.Resources.Templates.Fields.PageVariationsField.ascx");

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Web.UI.Fields.PageVariationsField" /> class.
    /// </summary>
    public PageVariationsField() => this.LayoutTemplatePath = PageVariationsField.LayoutTemplatePathValue;

    /// <inheritdoc />
    protected internal override WebControl TitleControl => (WebControl) this.TitleLabel;

    /// <summary>Gets the title label.</summary>
    /// <value>The title label.</value>
    protected internal virtual Label TitleLabel => this.Container.GetControl<Label>("titleLabel", true);

    /// <summary>Gets a reference to the client label manager.</summary>
    protected virtual ClientLabelManager ClientLabelManager => this.Container.GetControl<ClientLabelManager>("clientLabelManager", true);

    /// <inheritdoc />
    protected override void InitializeControls(GenericContainer container) => this.TitleLabel.SetTextOrHide(this.Title);

    /// <inheritdoc />
    public override IEnumerable<ScriptDescriptor> GetScriptDescriptors()
    {
      ScriptControlDescriptor controlDescriptor = base.GetScriptDescriptors().LastOrDefault<ScriptDescriptor>() as ScriptControlDescriptor;
      controlDescriptor.AddComponentProperty("clientLabelManager", this.ClientLabelManager.ClientID);
      return (IEnumerable<ScriptDescriptor>) new ScriptControlDescriptor[1]
      {
        controlDescriptor
      };
    }

    /// <inheritdoc />
    public override IEnumerable<ScriptReference> GetScriptReferences() => (IEnumerable<ScriptReference>) new List<ScriptReference>(base.GetScriptReferences())
    {
      new ScriptReference("Telerik.Sitefinity.Web.UI.Fields.Scripts.PageVariationsField.js", typeof (PageVariationsField).Assembly.FullName)
    }.ToArray();
  }
}
