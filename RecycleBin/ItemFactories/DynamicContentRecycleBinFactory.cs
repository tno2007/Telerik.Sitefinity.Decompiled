// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.RecycleBin.ItemFactories.DynamicContentRecycleBinFactory
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Collections.Generic;
using Telerik.Microsoft.Practices.Unity;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.DynamicModules.Model;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.RecycleBin.Adapters;

namespace Telerik.Sitefinity.RecycleBin.ItemFactories
{
  /// <summary>
  /// Recycle Bin item factory that can fill Recycle Bin items from a <see cref="T:Telerik.Sitefinity.DynamicModules.Model.DynamicContent" /> object.
  /// </summary>
  public class DynamicContentRecycleBinFactory : DataItemRecycleBinFactory
  {
    /// <summary>
    /// Gets a list of <see cref="T:Telerik.Sitefinity.RecycleBin.IRecycleBinItemAdapter" /> for the specified <paramref name="item" />.
    /// </summary>
    /// <param name="item">The item that will be inspected to configure what adapters will be used.</param>
    /// <returns>
    /// List of <see cref="T:Telerik.Sitefinity.RecycleBin.IRecycleBinItemAdapter" /> for the specified <paramref name="item" />.
    /// </returns>
    protected override IList<IRecycleBinItemAdapter> GetRecycleBinItemAdapters(
      IDataItem item)
    {
      IList<IRecycleBinItemAdapter> recycleBinItemAdapters = base.GetRecycleBinItemAdapters(item);
      if (item is DynamicContent)
      {
        recycleBinItemAdapters.Add((IRecycleBinItemAdapter) ObjectFactory.Container.Resolve<DynamicContentAdapter>());
        recycleBinItemAdapters.Add((IRecycleBinItemAdapter) ObjectFactory.Container.Resolve<ApprovalWorkflowItemAdapter>());
      }
      return recycleBinItemAdapters;
    }
  }
}
