// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Data.IProviderResolver
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Collections.Generic;

namespace Telerik.Sitefinity.Data
{
  /// <summary>
  /// Provides functionality for resolving providers in the context of one or more specific factors
  /// </summary>
  public interface IProviderResolver
  {
    /// <summary>Gets the default provider for the current context</summary>
    /// <returns></returns>
    DataProviderBase GetDefaultContextProvider();

    /// <summary>Gets the valid providers for the current context</summary>
    /// <returns></returns>
    IEnumerable<DataProviderBase> GetContextProviders();
  }
}
