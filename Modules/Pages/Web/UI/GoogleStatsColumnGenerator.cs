// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Pages.Web.UI.GoogleStatsColumnGenerator
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Web.UI;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Analytics;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Web.UI.ContentUI.Views.Backend.Master.Contracts;

namespace Telerik.Sitefinity.Modules.Pages.Web.UI
{
  public class GoogleStatsColumnGenerator : Control, IDynamicMarkupGenerator
  {
    private string hrefPath = string.Empty;

    public GoogleStatsColumnGenerator() => this.hrefPath = string.Format("{0}{1}{2}", SystemManager.CurrentHttpContext.Request.ApplicationPath == "/" ? (object) string.Empty : (object) SystemManager.CurrentHttpContext.Request.ApplicationPath, (object) "/Sitefinity/marketing/Analytics#/Content/Top_content/", (object) "sf:allurls:");

    /// <summary>
    /// Initialize properties of the markup generator implementing <see cref="T:Telerik.Sitefinity.Web.UI.ContentUI.Views.Backend.Master.Contracts.IDynamicMarkupGenerator" />
    /// with default values from the configuration definition object.
    /// </summary>
    /// <param name="definition">The definition configuration.</param>
    public void Configure(IDynamicMarkupGeneratorDefinition definition)
    {
    }

    /// <summary>Generates HTML markup for a dynamic column.</summary>
    /// <returns>The generated HTML markup.</returns>
    public string GetMarkup()
    {
      try
      {
        return ObjectFactory.Resolve<IAnalyticsApiAccessManager>().IsConfigured ? string.Format("<a sys:class=\"{{{{ (PageLifecycleStatus && PageLifecycleStatus.IsPublished) ? 'sfViewStats' : 'sfVisibilityHidden' }}}}\" sys:href=\"{{{{ '{0}' + FullUrl + '[' + Id + ']' }}}}\"></a>", (object) this.hrefPath) : "<a sys:class=\"sfVisibilityHidden\"></a>";
      }
      catch
      {
        return "<a sys:class=\"sfVisibilityHidden\"></a>";
      }
    }
  }
}
