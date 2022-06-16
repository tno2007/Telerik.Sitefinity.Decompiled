// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.RecycleBin.RecycleBinStrategyFactory
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using Telerik.Microsoft.Practices.Unity;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Data;

namespace Telerik.Sitefinity.RecycleBin
{
  /// <summary>
  /// Factory class that creates Recycle Bin strategy instances.
  /// Users can register their own implementations of recycle bin for specific managers.
  /// </summary>
  public static class RecycleBinStrategyFactory
  {
    /// <summary>Creates a Recycle Bin strategy.</summary>
    /// <param name="manager">The corresponding manager.</param>
    /// <returns>Implementation of IRecycleBinStrategy</returns>
    public static IRecycleBinStrategy CreateRecycleBin(IManager manager)
    {
      string fullName = manager.GetType().FullName;
      int num = ObjectFactory.IsTypeRegistered<IRecycleBinStrategy>(fullName) ? 1 : 0;
      ParameterOverrides parameterOverrides1 = new ParameterOverrides();
      parameterOverrides1.Add(nameof (manager), (object) manager);
      ParameterOverrides parameterOverrides2 = parameterOverrides1;
      return num != 0 ? ObjectFactory.Container.Resolve<IRecycleBinStrategy>(fullName, (ResolverOverride) parameterOverrides2) : ObjectFactory.Container.Resolve<IRecycleBinStrategy>((ResolverOverride) parameterOverrides2);
    }
  }
}
