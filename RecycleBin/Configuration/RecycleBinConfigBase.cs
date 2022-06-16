// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.RecycleBin.Configuration.RecycleBinConfigBase
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Modules.GenericContent.Configuration;

namespace Telerik.Sitefinity.RecycleBin.Configuration
{
  /// <summary>
  /// Defines the common properties of a Recycle Bin configuration section.
  /// </summary>
  public abstract class RecycleBinConfigBase : ModuleConfigBase
  {
    /// <summary>
    /// The section name that will be used when registering and resolving
    /// inheritors of <see cref="T:Telerik.Sitefinity.RecycleBin.Configuration.RecycleBinConfigBase" />.
    /// </summary>
    public const string SectionName = "RecycleBinConfig";

    /// <summary>
    /// Gets or sets a value indicating whether the Recycle Bin feature is enabled for the modules (data items) that support it.
    /// </summary>
    /// <remarks>If this setting is enabled all deletions from the UI of the supported data items will move them to the Recycle Bin.</remarks>
    /// <value>The recycle bin enabled value.</value>
    public abstract bool RecycleBinEnabled { get; set; }

    /// <summary>
    /// Gets or sets a value indicating the maximum period of time that
    /// an item will be kept in the Recycle Bin before being permanently deleted.
    /// </summary>
    /// <value>The recycle bin enabled value.</value>
    public abstract TimeSpan RetentionPeriod { get; set; }

    /// <summary>
    /// Initializes the default providers for the Recycle Bin module.
    /// </summary>
    /// <param name="providers">The collection of providers to initialize.</param>
    protected abstract override void InitializeDefaultProviders(
      ConfigElementDictionary<string, DataProviderSettings> providers);
  }
}
