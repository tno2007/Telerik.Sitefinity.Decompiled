// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Data.IHierarchyProvider
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using Telerik.Sitefinity.Model;

namespace Telerik.Sitefinity.Data
{
  /// <summary>Base interface for hierarchy providers.</summary>
  public interface IHierarchyProvider
  {
    /// <summary>
    /// Override this method in order to return the type of the Parent object of the specified type.
    /// If the type has no parent type, return null.
    /// </summary>
    /// <param name="contentType">Type of the child object.</param>
    /// <returns></returns>
    Type GetParentType(Type childType);

    /// <summary>
    /// Override this method in order to return the parent of the specified child object.
    /// </summary>
    /// <param name="child">The child object.</param>
    /// <returns></returns>
    IDataItem GetParent(IDataItem child);

    /// <summary>
    /// Override this method in order to return the children of the specified parent object.
    /// </summary>
    /// <param name="parent">The parent object.</param>
    /// <returns></returns>
    IList<IDataItem> GetChildren(IDataItem parent);
  }
}
