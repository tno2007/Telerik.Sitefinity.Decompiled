﻿// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.Services.Extensibility.HasParentBreadcrumbProperty
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using Telerik.Sitefinity.Data;

namespace Telerik.Sitefinity.Web.Services.Extensibility
{
  /// <summary>
  /// A property for retrieving pages breadcrumb full path names.
  /// </summary>
  [EditorBrowsable(EditorBrowsableState.Never)]
  public class HasParentBreadcrumbProperty : CalculatedProperty
  {
    /// <inheritdoc />
    public override Type ReturnType => typeof (IEnumerable<string>);

    /// <inheritdoc />
    public override IDictionary<object, object> GetValues(
      IEnumerable items,
      IManager manager)
    {
      Dictionary<object, object> values = new Dictionary<object, object>();
      foreach (IHasParent key in items)
      {
        if (key.Parent != null)
          values.Add((object) key, (object) new string[1]
          {
            (string) key.Parent.Title
          });
      }
      return (IDictionary<object, object>) values;
    }
  }
}
