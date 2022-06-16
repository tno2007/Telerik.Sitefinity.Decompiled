// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.Services.Extensibility.ViewUrlProperty
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.Modules.Pages.Web.Services;
using Telerik.Sitefinity.Modules.Pages.Web.Services.Model;
using Telerik.Sitefinity.Services;

namespace Telerik.Sitefinity.Web.Services.Extensibility
{
  /// <summary>A property for retrieving pages ViewUrl.</summary>
  [EditorBrowsable(EditorBrowsableState.Never)]
  internal class ViewUrlProperty : CalculatedProperty
  {
    /// <inheritdoc />
    public override Type ReturnType => typeof (string);

    /// <inheritdoc />
    public override IDictionary<object, object> GetValues(
      IEnumerable items,
      IManager manager)
    {
      Dictionary<object, object> values = new Dictionary<object, object>();
      if (items == null)
        return (IDictionary<object, object>) values;
      foreach (object key in items)
      {
        string editUrl = this.GetEditUrl(PropertyHelpers.GetSiteMapNode(key));
        values.Add(key, (object) editUrl);
      }
      return (IDictionary<object, object>) values;
    }

    protected string GetEditUrl(PageSiteNode siteNode)
    {
      PageViewModel pageViewModel = new PageViewModel(siteNode, true);
      new PagesService().ValidateViewModel((IEnumerable<WcfPageNode>) new PageViewModel[1]
      {
        pageViewModel
      }, siteNode.RootKey);
      Uri url = SystemManager.CurrentHttpContext.Request.Url;
      return UrlPath.ResolveAbsoluteUrlWithoutNonDefaultUrlSettings(pageViewModel.PageLiveUrl, url.Host);
    }
  }
}
