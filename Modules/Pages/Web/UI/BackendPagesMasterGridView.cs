// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Pages.Web.UI.BackendPagesMasterGridView
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Security.Model;
using Telerik.Sitefinity.Web.UI.ContentUI.Views.Backend.Master;

namespace Telerik.Sitefinity.Modules.Pages.Web.UI
{
  /// <summary>
  /// A content view control which displays the a list of pages in the backend
  /// </summary>
  public class BackendPagesMasterGridView : MasterGridView
  {
    private string currentLanguage;

    /// <summary>
    /// Override this method to return the web service root key if needed.
    /// </summary>
    /// <returns></returns>
    protected override string GetWebServiceRootKey() => "backend";

    /// <summary>
    /// Gets the security root that is used to check permissions
    /// </summary>
    /// <returns></returns>
    protected override ISecuredObject GetSecurityRoot() => this.Manager is PageManager manager ? (ISecuredObject) manager.GetPageNode(SiteInitializer.BackendRootNodeId) : (ISecuredObject) null;

    /// <summary>
    /// Gets or sets the UI culture used by the client manager.
    /// </summary>
    public override string CurrentLanguage
    {
      get
      {
        if (string.IsNullOrEmpty(this.currentLanguage))
          this.currentLanguage = AppSettings.CurrentSettings.DefaultBackendLanguage.Name;
        return this.currentLanguage;
      }
    }
  }
}
