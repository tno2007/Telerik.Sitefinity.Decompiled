// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.DataResolving.UrlResolver
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Specialized;
using System.Linq;
using System.Web;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.GenericContent.Model;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.Modules.GenericContent;
using Telerik.Sitefinity.Modules.Pages;
using Telerik.Sitefinity.Security;
using Telerik.Sitefinity.Security.Claims;

namespace Telerik.Sitefinity.Web.DataResolving
{
  /// <summary>
  /// Resolves URLs for data items implementing ILocatable interface.
  /// </summary>
  public class UrlResolver : IDataResolver
  {
    /// <summary>
    /// Resolves and formats specific data form the specified item.
    /// </summary>
    /// <param name="dataItem">The data item.</param>
    /// <param name="format">The format.</param>
    /// <param name="args">Optional arguments.</param>
    /// <returns></returns>
    public string Resolve(object dataItem, string format, string args)
    {
      string str;
      switch (dataItem)
      {
        case ISimpleLocatable simpleLocatable:
          str = simpleLocatable.ItemUrl;
          break;
        case ILocatable locatable:
          str = (!(dataItem is IDataItem dataItem1) || dataItem1.Provider == null ? (UrlDataProviderBase) ManagerBase.GetMappedManager(locatable.GetType()).Provider : (UrlDataProviderBase) dataItem1.Provider).GetItemUrl(locatable);
          break;
        default:
          throw new ArgumentException("UrlResolver can be used only on data items implementing ILocatable interface.");
      }
      if (!string.IsNullOrEmpty(format))
        str = string.Format("/!{0}/{1}", (object) format, (object) str);
      string virtualPath = string.Empty;
      SiteMapProvider currentProvider = SiteMapBase.GetCurrentProvider();
      if (currentProvider != null)
      {
        if (!string.IsNullOrEmpty(args))
        {
          PageDataProvider provider = PageManager.GetManager().Provider;
          bool suppressSecurityChecks = provider.SuppressSecurityChecks;
          provider.SuppressSecurityChecks = true;
          SiteMapNode siteMapNodeFromKey = currentProvider.FindSiteMapNodeFromKey(args);
          provider.SuppressSecurityChecks = suppressSecurityChecks;
          PageSiteNode pageSiteNode = siteMapNodeFromKey != null ? (PageSiteNode) siteMapNodeFromKey : throw new ArgumentException("Invalid details page specified: \"{0}\".".Arrange((object) args));
          SitefinityIdentity user = ClaimsManager.GetCurrentIdentity();
          if (!user.IsUnrestricted)
          {
            bool flag1 = true;
            if (pageSiteNode.DeniedRoles != null && pageSiteNode.DeniedRoles.Count > 0)
              flag1 = !pageSiteNode.DeniedRoles.Cast<Guid>().Any<Guid>((Func<Guid, bool>) (r => user.UserId == r || user.Roles.Any<RoleInfo>((Func<RoleInfo, bool>) (ur => ur.Id == r))));
            if (flag1)
            {
              bool flag2 = false;
              if (pageSiteNode.Roles.Contains((object) user.UserId))
              {
                flag2 = true;
              }
              else
              {
                foreach (RoleInfo role in user.Roles)
                {
                  if (pageSiteNode.Roles.Contains((object) role.Id))
                  {
                    flag2 = true;
                    break;
                  }
                }
              }
              flag1 = flag2;
            }
            if (!flag1)
              return string.Empty;
          }
          virtualPath = pageSiteNode.UrlWithoutExtension;
        }
        else
        {
          PageSiteNode actualCurrentNode = SiteMapBase.GetActualCurrentNode();
          if (actualCurrentNode != null)
            virtualPath = actualCurrentNode.UrlWithoutExtension;
          else if (currentProvider.CurrentNode != null)
            virtualPath = ((PageSiteNode) currentProvider.CurrentNode).UrlWithoutExtension;
        }
      }
      return VirtualPathUtility.RemoveTrailingSlash(virtualPath) + str;
    }

    /// <summary>
    /// Initializes this instance with the provided configuration information.
    /// </summary>
    /// <param name="config"></param>
    public void Initialize(NameValueCollection config)
    {
    }
  }
}
