﻿// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.Services.Extensibility.HomepageProperty
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.Services;

namespace Telerik.Sitefinity.Web.Services.Extensibility
{
  /// <summary>A property for retrieving pages ViewUrl.</summary>
  [EditorBrowsable(EditorBrowsableState.Never)]
  internal class HomepageProperty : CalculatedProperty
  {
    /// <inheritdoc />
    public override Type ReturnType => typeof (bool);

    /// <inheritdoc />
    public override IDictionary<object, object> GetValues(
      IEnumerable items,
      IManager manager)
    {
      Dictionary<object, object> values = new Dictionary<object, object>();
      Guid homePageId = SystemManager.CurrentContext.CurrentSite.HomePageId;
      foreach (object key in items)
      {
        PageSiteNode siteMapNode = PropertyHelpers.GetSiteMapNode(key);
        values.Add(key, (object) (siteMapNode.Id == homePageId));
      }
      return (IDictionary<object, object>) values;
    }
  }
}
