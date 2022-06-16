// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Newsletters.Web.UI.NewslettersCommandPanel
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Web;
using Telerik.Sitefinity.Web.UI;
using Telerik.Sitefinity.Web.UI.Backend;

namespace Telerik.Sitefinity.Modules.Newsletters.Web.UI
{
  /// <summary>
  /// The control that represents the command panel of the newsletters module.
  /// </summary>
  public class NewslettersCommandPanel : ViewModeControl<NewslettersControlPanel>, ICommandPanel
  {
    public static readonly string layoutTemplatePath = ControlUtilities.ToVppPath("Telerik.Sitefinity.Resources.Templates.Backend.Newsletters.NewslettersCommandPanel.ascx");

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Modules.Newsletters.Web.UI.NewslettersCommandPanel" /> class.
    /// </summary>
    public NewslettersCommandPanel() => this.LayoutTemplatePath = NewslettersCommandPanel.layoutTemplatePath;

    /// <summary>
    /// Reference to the control panel tied to the command panel instance.
    /// </summary>
    /// <value></value>
    /// <remarks>
    /// This property is used for communication between the command panel and its control
    /// panel.
    /// </remarks>
    /// <example>
    /// You can refer to <see cref="T:Telerik.Sitefinity.Web.UI.Backend.ICommandPanel">ICommandPanel</see> interface for more
    /// complicated example implementing the whole
    /// <see cref="T:Telerik.Sitefinity.Web.UI.Backend.ICommandPanel">ICommandPanel</see> interface.
    /// </example>
    public IControlPanel ControlPanel { get; set; }

    /// <summary>Gets the name of the layout template.</summary>
    /// <value>The name of the layout template.</value>
    protected override string LayoutTemplateName => (string) null;

    /// <summary>
    /// Gets the reference to the literal control for the settings view
    /// </summary>
    protected virtual Literal SettingsLiteral => this.Container.GetControl<Literal>("settingsForNewslettersLiteral", true);

    /// <summary>
    /// Gets the reference to the hyperlink control for the settings view
    /// </summary>
    protected virtual HyperLink SettingsLink => this.Container.GetControl<HyperLink>("settingsLink", true);

    /// <summary>Initializes the controls.</summary>
    /// <param name="viewContainer">The view container.</param>
    protected override void InitializeControls(Control viewContainer)
    {
      base.InitializeControls(viewContainer);
      this.SettingsLink.NavigateUrl = "~/Sitefinity/Administration/Settings/Basic/EmailSettings/";
      this.SettingsLink.CssClass = BackendSiteMap.GetCurrentProvider().CurrentNode.Url == this.SettingsLink.NavigateUrl ? "sfSel" : "";
      this.SettingsLink.Visible = SystemManager.IsOperationEnabled(RestrictionLevel.ReadOnlyConfigFile);
      this.SettingsLiteral.Visible = SystemManager.IsOperationEnabled(RestrictionLevel.ReadOnlyConfigFile);
    }
  }
}
