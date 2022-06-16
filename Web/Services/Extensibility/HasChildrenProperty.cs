// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.Services.Extensibility.HasChildrenProperty
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.Modules.Pages;
using Telerik.Sitefinity.Pages.Model;

namespace Telerik.Sitefinity.Web.Services.Extensibility
{
  /// <summary>A property that determine if a page has children</summary>
  [EditorBrowsable(EditorBrowsableState.Never)]
  public class HasChildrenProperty : CalculatedProperty
  {
    /// <inheritdoc />
    public override Type ReturnType => typeof (bool);

    /// <inheritdoc />
    public override IDictionary<object, object> GetValues(
      IEnumerable items,
      IManager manager)
    {
      Dictionary<object, object> values = new Dictionary<object, object>();
      foreach (PageNode pageNode in items)
      {
        PageSiteNode siteMapNode = pageNode.GetSiteMapNode();
        bool flag = false;
        if (siteMapNode != null)
          flag = siteMapNode.HasChildNodes;
        values.Add((object) pageNode, (object) flag);
      }
      return (IDictionary<object, object>) values;
    }
  }
}
