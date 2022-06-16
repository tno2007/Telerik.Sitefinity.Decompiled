// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.Services.Extensibility.HierarchicalItemBreadcrumbProperty
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.Model;

namespace Telerik.Sitefinity.Web.Services.Extensibility
{
  /// <summary>
  /// A property for retrieving pages breadcrumb full path names.
  /// </summary>
  [EditorBrowsable(EditorBrowsableState.Never)]
  public class HierarchicalItemBreadcrumbProperty : CalculatedProperty
  {
    /// <inheritdoc />
    public override Type ReturnType => typeof (IEnumerable<string>);

    /// <inheritdoc />
    public override IDictionary<object, object> GetValues(
      IEnumerable items,
      IManager manager)
    {
      Dictionary<object, object> values = new Dictionary<object, object>();
      foreach (IHierarchicalItem key in items)
        values.Add((object) key, (object) this.GetParentTitle(key));
      return (IDictionary<object, object>) values;
    }

    private IEnumerable GetParentTitle(IHierarchicalItem item)
    {
      List<string> source = new List<string>();
      for (IHierarchicalItem parent = item.Parent; parent != null; parent = parent.Parent)
      {
        if (parent is IHasTitle)
          source.Add(((IHasTitle) parent).GetTitle(CultureInfo.CurrentCulture));
      }
      return (IEnumerable) source.Reverse<string>();
    }
  }
}
