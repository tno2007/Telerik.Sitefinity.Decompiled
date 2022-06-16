// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.Services.Extensibility.ProviderNameProperty
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections;
using System.Collections.Generic;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.Utilities;

namespace Telerik.Sitefinity.Web.Services.Extensibility
{
  /// <summary>
  /// A calculated property for retrieving Provider of items.
  /// </summary>
  public class ProviderNameProperty : CalculatedProperty
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
      IProviderNameResolver providerNameResolver = ObjectFactory.Resolve<IProviderNameResolver>();
      foreach (IDataItem key in items)
        values.Add((object) key, (object) providerNameResolver.GetProviderName(key.Provider));
      return (IDictionary<object, object>) values;
    }
  }
}
