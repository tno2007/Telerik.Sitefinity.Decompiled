// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Data.IDataProviderInfo
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

namespace Telerik.Sitefinity.Data
{
  /// <summary>
  /// Interface for data provider information describing the provider
  /// </summary>
  public interface IDataProviderInfo
  {
    /// <summary>Gets the name of the provider</summary>
    string Name { get; }

    /// <summary>Gets the title of the provider displayed in the UI</summary>
    string Title { get; }

    /// <summary>
    /// Gets a value indicating whether the providers is virtual and does not have own configuration setting
    /// </summary>
    bool IsVirtual { get; }

    /// <summary>
    /// Gets a value indicating whether the providers is system
    /// </summary>
    bool IsSystem { get; }
  }
}
