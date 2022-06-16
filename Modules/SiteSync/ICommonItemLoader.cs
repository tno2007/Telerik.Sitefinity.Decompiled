// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.SiteSync.ICommonItemLoader
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using Telerik.Sitefinity.Publishing;

namespace Telerik.Sitefinity.SiteSync
{
  /// <summary>Loads common items and their properties</summary>
  public interface ICommonItemLoader
  {
    /// <summary>Loads data item</summary>
    /// <param name="itemType">The type</param>
    /// <param name="id">The id</param>
    /// <param name="provider">The provider</param>
    /// <returns>The item, or null if it can't be found</returns>
    object LoadDataItem(Type itemType, Guid id, string provider);

    /// <summary>Loads lifecycle data item</summary>
    /// <param name="item">The item</param>
    /// <returns>A list, containing dependent lifecycle versions (i.e. its live version)</returns>
    IList<object> LoadLifecycleDataItem(object item);

    /// <summary>Sets the common properties for an item.</summary>
    /// <param name="item">The item.</param>
    /// <param name="typeName">Name of the type.</param>
    /// <param name="providerName">Name of the provider.</param>
    /// <param name="action">The action.</param>
    /// <param name="language">The language.</param>
    void SetCommonProperties(
      WrapperObject item,
      string typeName,
      string providerName,
      string action,
      string language);

    /// <summary>Sets the parent properties of an item.</summary>
    /// <param name="item">The item.</param>
    /// <param name="dataItem">The data item.</param>
    void SetParentProperties(WrapperObject item, object dataItem);
  }
}
