// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.DialogRouteHandler
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Web.Routing;
using System.Web.UI;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Web.Configuration;
using Telerik.Sitefinity.Web.UI;
using Telerik.Sitefinity.Web.UI.Templates;

namespace Telerik.Sitefinity.Web
{
  /// <summary>
  /// Represents route handler for all the dialogs inside of Sitefinity
  /// </summary>
  public class DialogRouteHandler : RouteHandlerBase
  {
    /// <summary>Specifies the embedded dialog page template name.</summary>
    public const string DialogPageTemplate = "Telerik.Sitefinity.Resources.Pages.Dialog.aspx";

    /// <summary>
    /// Defines the page template and resources loaded for the current request.
    /// </summary>
    public override TemplateInfo GetTemplateInfo(RequestContext requestContext) => new TemplateInfo()
    {
      TemplateName = "Telerik.Sitefinity.Resources.Pages.Dialog.aspx",
      ControlType = this.GetType()
    };

    /// <summary>
    /// Initializes internal controls. In this method any additional controls can be
    /// instantiated and added to the page controls collection.
    /// </summary>
    /// <param name="handler">
    /// A <see cref="T:System.Web.UI.Page" /> that will handle the current HTTP request.
    /// </param>
    /// <param name="requestContext">
    /// An object that encapsulates information about the request.
    /// </param>
    protected override void InitializeHttpHandler(Page handler, RequestContext requestContext) => ThemeController.SetPageTheme(Config.Get<AppearanceConfig>().BackendTheme, handler);

    /// <summary>
    /// Initializes the content of a backend page.
    /// The main navigation is added prior to this method.
    /// </summary>
    /// <param name="handler">A <see cref="T:System.Web.UI.Page" /> that will handle the current HTTP request.</param>
    /// <param name="requestContext">An object that encapsulates information about the request.</param>
    protected override void InitializeContent(Page handler, RequestContext requestContext)
    {
      ResourceLinks globalStyles = ThemeController.GetGlobalStyles(handler);
      if (globalStyles != null)
        handler.Controls.Add((Control) globalStyles);
      string name = requestContext.RouteData.Values["Name"].ToString();
      DialogBase dialog = Dialogs.GetDialog(name);
      if (requestContext.RouteData.Values["params"] != null)
        dialog.LoadRouteParameters(requestContext.RouteData.Values["params"].ToString());
      Control control = handler.FindControl("pageContent");
      if (!string.IsNullOrEmpty(name))
        ControlUtilities.SetControlIdFromName(name, (Control) dialog);
      control.Controls.Add((Control) dialog);
    }
  }
}
