// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.Services.Extensibility.TaxonomiesDisplayStatusProperty
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.Services;

namespace Telerik.Sitefinity.Web.Services.Extensibility
{
  /// <summary>Display status property for taxonomies and taxa items</summary>
  [EditorBrowsable(EditorBrowsableState.Never)]
  internal class TaxonomiesDisplayStatusProperty : CalculatedProperty
  {
    /// <inheritdoc />
    public override Type ReturnType => typeof (IEnumerable<DisplayStatus>);

    /// <inheritdoc />
    public override IDictionary<object, object> GetValues(
      IEnumerable items,
      IManager manager)
    {
      if (items == null)
        throw new ArgumentNullException(nameof (items));
      if (manager == null)
        throw new ArgumentNullException(nameof (manager));
      Dictionary<object, object> values = new Dictionary<object, object>();
      foreach (IDataItem key in items)
      {
        List<DisplayStatus> displayStatusList = new List<DisplayStatus>();
        Status status = StatusResolver.Resolve(key.GetType(), manager.Provider.Name, key.Id);
        if (status != null)
          displayStatusList.Add(new DisplayStatus()
          {
            Name = status.Text,
            Source = status.PrimaryProvider,
            Label = status.Text,
            DetailedLabel = status.Text
          });
        values.Add((object) key, (object) displayStatusList);
      }
      return (IDictionary<object, object>) values;
    }
  }
}
