// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Configuration.Web.UI.BaseSettingsPanel
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Sitefinity.Modules.Pages;
using Telerik.Sitefinity.Web.UI;
using Telerik.Sitefinity.Web.UI.Backend;

namespace Telerik.Sitefinity.Configuration.Web.UI
{
  /// <summary>
  /// Represents the base class for control panels that manages settings.
  /// </summary>
  public class BaseSettingsPanel : ProviderControlPanel<Page>
  {
    private string templatePath;
    public new static readonly string layoutTemplatePath = ControlUtilities.ToVppPath("Telerik.Sitefinity.Resources.Templates.Backend.Configuration.BaseSettingsPanel.ascx");

    /// <summary>
    /// Initializes new instance of <see cref="T:Telerik.Sitefinity.Configuration.Web.UI.BaseSettingsPanel" />.
    /// </summary>
    public BaseSettingsPanel()
      : base(false)
    {
      this.ResourceClassId = typeof (PageResources).Name;
    }

    /// <summary>
    /// Gets the name of the embedded layout template. This property must be overridden to provide the path (key) to an embedded resource file.
    /// </summary>
    /// <value></value>
    protected override string LayoutTemplateName => (string) null;

    /// <summary>Gets the layout template path.</summary>
    public override string LayoutTemplatePath
    {
      get => string.IsNullOrEmpty(this.templatePath) ? BaseSettingsPanel.layoutTemplatePath : this.templatePath;
      set => this.templatePath = value;
    }

    /// <summary>Gets the settings link.</summary>
    /// <value>The settings link.</value>
    protected virtual HyperLink SettingsLink => this.Container.GetControl<HyperLink>("settingsLink", true);

    /// <summary>Loads configured views.</summary>
    protected override void CreateViews()
    {
    }
  }
}
