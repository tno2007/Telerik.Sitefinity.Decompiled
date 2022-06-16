// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Utilities.DataProviderNameResolver
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using Telerik.Sitefinity.Data;

namespace Telerik.Sitefinity.Utilities
{
  /// <summary>
  /// Name resolver for <see cref="T:Telerik.Sitefinity.Data.IDataProviderBase" /> providers.
  /// </summary>
  public class DataProviderNameResolver : IProviderNameResolver
  {
    /// <summary>
    /// Attempts to acquire the name of the specified <paramref name="provider" /> by casting it
    /// to <see cref="T:Telerik.Sitefinity.Data.IDataProviderBase" />.
    /// </summary>
    /// <param name="provider">The provider.</param>
    /// <returns>The name of the specified <paramref name="provider" /></returns>
    public string GetProviderName(object provider)
    {
      if (provider is IDataProviderBase dataProviderBase)
        return dataProviderBase.Name;
      return provider != null ? provider.ToString() : string.Empty;
    }
  }
}
