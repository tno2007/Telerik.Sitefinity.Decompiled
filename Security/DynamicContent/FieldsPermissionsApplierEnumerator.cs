// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Security.FieldsPermissionsApplierEnumerator`1
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Collections.Generic;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.Data.Linq;
using Telerik.Sitefinity.DynamicModules.Model;
using Telerik.Sitefinity.Model;

namespace Telerik.Sitefinity.Security
{
  internal sealed class FieldsPermissionsApplierEnumerator<T> : PermissionApplierEnumeratorBase<T>
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Security.FieldsPermissionsApplierEnumerator`1" /> class.
    /// </summary>
    /// <param name="enumerable">The enumerable.</param>
    public FieldsPermissionsApplierEnumerator(IEnumerator<T> enumerable)
      : base(enumerable)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Security.FieldsPermissionsApplierEnumerator`1" /> class.
    /// </summary>
    /// <param name="enumerator">The enumerator.</param>
    /// <param name="dataProvider">The data provider.</param>
    public FieldsPermissionsApplierEnumerator(
      IEnumerator<T> enumerator,
      DataProviderBase dataProvider)
      : base(enumerator, dataProvider)
    {
    }

    /// <summary>Demands the specified for item.</summary>
    /// <param name="forItem">For item.</param>
    protected override void Demand(T forItem)
    {
      DynamicContent dynamicContent = (object) forItem as DynamicContent;
      DataItemEnumerator.EnsureProviderSet((object) dynamicContent, (IDataProviderBase) this.DataProvider);
      if (dynamicContent.Provider is ISecuredFieldsProvider)
        ((ISecuredFieldsProvider) dynamicContent.Provider).ApplyViewFieldPermissions((IDataItem) dynamicContent);
      if (!(dynamicContent.Provider is IHtmlFilterProvider))
        return;
      ((IHtmlFilterProvider) dynamicContent.Provider).ApplyFilters((IDataItem) dynamicContent);
    }
  }
}
