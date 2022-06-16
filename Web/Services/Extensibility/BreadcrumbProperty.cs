// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.Services.Extensibility.BreadcrumbProperty
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.Modules.Pages;
using Telerik.Sitefinity.Services;

namespace Telerik.Sitefinity.Web.Services.Extensibility
{
  /// <summary>
  /// A property for retrieving pages breadcrumb full path names.
  /// </summary>
  [EditorBrowsable(EditorBrowsableState.Never)]
  public class BreadcrumbProperty : CalculatedProperty
  {
    /// <inheritdoc />
    public override Type ReturnType => typeof (IEnumerable<string>);

    /// <inheritdoc />
    public override IDictionary<object, object> GetValues(
      IEnumerable items,
      IManager manager)
    {
      Dictionary<object, object> values = new Dictionary<object, object>();
      foreach (object key in items)
      {
        IEnumerable<string> strings = Enumerable.Empty<string>();
        PageSiteNode siteNode = PropertyHelpers.GetSiteMapNode(key);
        if (siteNode != null)
        {
          Guid guid = Guid.Parse(siteNode.ParentKey);
          Guid siteMapRootNodeId = SystemManager.CurrentContext.CurrentSite.SiteMapRootNodeId;
          if (siteMapRootNodeId != siteNode.RootKey)
          {
            Telerik.Sitefinity.Multisite.ISite site = SystemManager.CurrentContext.GetSites().FirstOrDefault<Telerik.Sitefinity.Multisite.ISite>((Func<Telerik.Sitefinity.Multisite.ISite, bool>) (s => s.SiteMapRootNodeId == siteNode.RootKey));
            if (site != null)
              strings = (IEnumerable<string>) new List<string>()
              {
                site.Name
              };
          }
          else if (guid != siteMapRootNodeId)
          {
            List<string> list = siteNode.GetFullTitles().ToList<string>();
            if (list.Count > 1)
              list.RemoveAt(list.Count - 1);
            strings = (IEnumerable<string>) list;
          }
        }
        values.Add(key, (object) strings);
      }
      return (IDictionary<object, object>) values;
    }
  }
}
