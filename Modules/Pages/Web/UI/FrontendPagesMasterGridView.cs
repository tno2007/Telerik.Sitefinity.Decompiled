// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Pages.Web.UI.FrontendPagesMasterGridView
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web.UI;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Modules.Pages.Configuration;
using Telerik.Sitefinity.Multisite;
using Telerik.Sitefinity.Pages.Model;
using Telerik.Sitefinity.Security;
using Telerik.Sitefinity.Security.Model;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Web.UI.ContentUI.Views.Backend.Master;

namespace Telerik.Sitefinity.Modules.Pages.Web.UI
{
  /// <summary>
  /// A content view control that displays a list of frontend pages
  /// </summary>
  public class FrontendPagesMasterGridView : MasterGridView
  {
    /// <summary>
    /// Gets a collection of script descriptors that represent ECMAScript (JavaScript) client components.
    /// </summary>
    /// <returns>
    /// An <see cref="T:System.Collections.IEnumerable" /> collection of <see cref="T:System.Web.UI.ScriptDescriptor" /> objects.
    /// </returns>
    public override IEnumerable<ScriptDescriptor> GetScriptDescriptors()
    {
      PagesConfig pagesConfig = Config.Get<PagesConfig>();
      Guid currentSiteMapRoot = SystemManager.CurrentContext.CurrentSite.SiteMapRootNodeId;
      PageManager manager = PageManager.GetManager();
      bool flag = true;
      PageNode pageNode = manager.GetPageNodes().FirstOrDefault<PageNode>((Expression<Func<PageNode, bool>>) (n => n.Id == currentSiteMapRoot));
      if (pageNode != null)
        flag = pageNode.IsGranted("Pages", "Create");
      List<ScriptDescriptor> source = new List<ScriptDescriptor>(base.GetScriptDescriptors());
      ScriptControlDescriptor controlDescriptor = (ScriptControlDescriptor) source.Last<ScriptDescriptor>();
      controlDescriptor.AddProperty("_currentSiteMapRootId", (object) currentSiteMapRoot);
      controlDescriptor.AddProperty("_userAllowedToCreatePagsOnTheCurrentSite", (object) flag);
      controlDescriptor.AddProperty("_sharePageLinkEnabled", (object) pagesConfig.EnableLinkSharing);
      return (IEnumerable<ScriptDescriptor>) source;
    }

    /// <summary>
    /// Gets the security root that is used to check permissions
    /// </summary>
    /// <returns></returns>
    protected override ISecuredObject GetSecurityRoot()
    {
      if (!(this.Manager is PageManager manager))
        return (ISecuredObject) null;
      Guid frontendRootNodeId = SiteInitializer.CurrentFrontendRootNodeId;
      return (ISecuredObject) manager.GetPageNode(frontendRootNodeId);
    }

    protected override string GetWebServiceRootKey()
    {
      ISite currentSite = SystemManager.CurrentContext.CurrentSite;
      return currentSite != null ? currentSite.SiteMapRootNodeId.ToString() : string.Empty;
    }
  }
}
