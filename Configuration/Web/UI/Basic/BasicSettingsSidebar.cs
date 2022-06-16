// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Configuration.Web.UI.Basic.BasicSettingsSidebar
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Web;
using Telerik.Sitefinity.Web.UI;
using Telerik.Sitefinity.Web.UI.Backend;

namespace Telerik.Sitefinity.Configuration.Web.UI.Basic
{
  /// <summary>
  /// Sidebar control for navigating between basic settings.
  /// </summary>
  public class BasicSettingsSidebar : ViewModeControl<BasicSettingsPanel>, ICommandPanel
  {
    public static readonly string layoutTemplateName = ControlUtilities.ToVppPath("Telerik.Sitefinity.Resources.Templates.Backend.Configuration.Basic.BasicSettingsSidebar.ascx");

    /// <summary>
    /// Gets the name of the embedded layout template. If the control uses layout template this
    /// property must be overridden to provide the path (key) to an embedded resource file.
    /// </summary>
    /// <value></value>
    protected override string LayoutTemplateName => (string) null;

    /// <summary>Gets the layout template path.</summary>
    public override string LayoutTemplatePath
    {
      get => string.IsNullOrEmpty(base.LayoutTemplatePath) ? BasicSettingsSidebar.layoutTemplateName : base.LayoutTemplatePath;
      set => base.LayoutTemplatePath = value;
    }

    /// <summary>Initializes the controls.</summary>
    /// <param name="viewContainer">The view container.</param>
    protected override void InitializeControls(Control viewContainer)
    {
      base.InitializeControls(viewContainer);
      SiteMapNode siteMapNode1 = BackendSiteMap.FindSiteMapNode(SiteInitializer.BasicSettingsNodeId, false);
      if (siteMapNode1 == null)
        return;
      string str1 = RouteHelper.ResolveUrl(siteMapNode1.Url, UrlResolveOptions.Rooted | UrlResolveOptions.AppendTrailingSlash);
      string viewMode = (this.ControlPanel as BasicSettingsPanel).ViewMode;
      HtmlGenericControl child1 = new HtmlGenericControl("ul");
      child1.Attributes["class"] = "sfBasicSettingsList";
      IEnumerable<BasicSettingsRegistration> settingsRegistrations = SystemManager.GetEditableBasicSettingsRegistrations();
      if (settingsRegistrations == null || settingsRegistrations.Count<BasicSettingsRegistration>() == 0)
      {
        SiteMapNode siteMapNode2 = BackendSiteMap.FindSiteMapNode(SiteInitializer.AdvancedSettingsNodeId, true);
        if (siteMapNode2 != null)
        {
          HttpContextBase currentHttpContext = SystemManager.CurrentHttpContext;
          currentHttpContext.Response.Redirect(siteMapNode2.Url, false);
          if (currentHttpContext.ApplicationInstance != null)
            currentHttpContext.ApplicationInstance.CompleteRequest();
        }
      }
      string str2 = str1;
      int num = str2.IndexOf('?');
      string str3;
      if (num != -1)
      {
        str3 = str2.Substring(num);
        str2 = str2.Substring(0, num);
      }
      else
        str3 = string.Empty;
      foreach (BasicSettingsRegistration settingsRegistration in settingsRegistrations)
      {
        HtmlGenericControl child2 = new HtmlGenericControl("li");
        HyperLink hyperLink = new HyperLink();
        BasicSettingsSidebar.SetLink(hyperLink, string.Format("{0}{1}/{2}", (object) str2, (object) settingsRegistration.SettingsName, (object) str3), viewMode == settingsRegistration.SettingsName);
        hyperLink.Text = string.IsNullOrEmpty(settingsRegistration.SettingsResourceClass) ? settingsRegistration.SettingsTitle : Res.Get(settingsRegistration.SettingsResourceClass, settingsRegistration.SettingsTitle);
        if (hyperLink.NavigateUrl.EndsWith("/"))
          hyperLink.NavigateUrl = hyperLink.NavigateUrl.Remove(hyperLink.NavigateUrl.Length - 1);
        child2.Controls.Add((Control) hyperLink);
        child1.Controls.Add((Control) child2);
      }
      viewContainer.Controls.Add((Control) child1);
    }

    private static string AppendQueryStringParameter(
      string query,
      string paramName,
      string paramValue)
    {
      string str = query.Contains("?") ? "&" : "?";
      query = string.Format("{0}{1}{2}={3}", (object) query, (object) str, (object) paramName, (object) paramValue);
      return query;
    }

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

    private static void SetLink(HyperLink link, string navigateUrl, bool isSelected)
    {
      link.NavigateUrl = navigateUrl;
      if (!isSelected)
        return;
      link.CssClass = "sfSel";
    }
  }
}
