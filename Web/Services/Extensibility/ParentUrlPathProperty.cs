// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.Services.Extensibility.ParentUrlPathProperty
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections;
using System.Collections.Generic;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.Services;

namespace Telerik.Sitefinity.Web.Services.Extensibility
{
  internal class ParentUrlPathProperty : CalculatedProperty
  {
    public override Type ReturnType => typeof (string);

    public override IDictionary<object, object> GetValues(
      IEnumerable items,
      IManager manager)
    {
      Dictionary<object, object> values = new Dictionary<object, object>();
      if (items == null)
        return (IDictionary<object, object>) values;
      foreach (object key in items)
      {
        string str = PropertyHelpers.GetSiteMapNode(key).GetUrl(SystemManager.CurrentContext.Culture, true, false).TrimStart('~');
        values.Add(key, (object) str);
      }
      return (IDictionary<object, object>) values;
    }
  }
}
