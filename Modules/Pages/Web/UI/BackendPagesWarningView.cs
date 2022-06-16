// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Pages.Web.UI.BackendPagesWarningView
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Web.UI;
using System.Web.UI.HtmlControls;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Web;
using Telerik.Sitefinity.Web.UI;

namespace Telerik.Sitefinity.Modules.Pages.Web.UI
{
  /// <summary>Represents the view for backend pages.</summary>
  public class BackendPagesWarningView : ViewModeControl<PagesPanel>
  {
    /// <summary>
    ///  Gets the name of resource file representing Backend Pages View.
    /// </summary>
    public static readonly string BackendPagesWarningViewPath = ControlUtilities.ToVppPath("Telerik.Sitefinity.Resources.Templates.Backend.Pages.BackendPagesWarningView.ascx");

    /// <summary>
    /// Gets the name of the embedded layout template. If the control uses layout template this
    /// property must be overridden to provide the path (key) to an embedded resource file.
    /// </summary>
    /// <value></value>
    protected override string LayoutTemplateName => (string) null;

    /// <inheritdoc />
    protected override string DefaultLayoutTemplatePath => BackendPagesWarningView.BackendPagesWarningViewPath;

    private HtmlAnchor ContinueAnchor => this.Container.GetControl<HtmlAnchor>("continue", true);

    /// <summary>
    /// Initializes all controls instantiated in the layout container. This method is called at appropriate time for setting initial values and subscribing for events of layout controls.
    /// </summary>
    /// <param name="viewContainer">The control that will host the current view.</param>
    protected override void InitializeControls(Control viewContainer)
    {
      this.ContinueAnchor.HRef = BackendSiteMap.FindSiteMapNode(SiteInitializer.BackendPagesActualNodeId, false).Url;
      base.InitializeControls(viewContainer);
    }
  }
}
