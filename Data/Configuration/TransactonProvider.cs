// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Data.Configuration.TransactonProvider
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

namespace Telerik.Sitefinity.Data.Configuration
{
  /// <summary>
  /// Defines the transaction provider used by the application.
  /// </summary>
  public enum TransactonProvider
  {
    /// <summary>The build-in transaction model will be used.</summary>
    BuildIn,
    /// <summary>
    /// Specifies that <see cref="T:System.Transactions.TransactionScope" /> will be used as transaction provider.
    /// Note that this options is not supported by all databases and requires additional operating system and data base configurations.
    /// </summary>
    TransactionScope,
  }
}
