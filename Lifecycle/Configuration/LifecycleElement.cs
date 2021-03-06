// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Lifecycle.Configuration.LifecycleElement
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Configuration;
using System.Runtime.InteropServices;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Lifecycle.Cleanup.Configuration;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Web.Configuration;

namespace Telerik.Sitefinity.Lifecycle.Configuration
{
  /// <summary>Represents Lifecycle configuration section.</summary>
  [ObjectInfo(typeof (ConfigDescriptions), Description = "LifecycleConfigDescription", Title = "LifecycleConfigCaption")]
  public class LifecycleElement : ConfigElement
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Lifecycle.Configuration.LifecycleElement" /> class.
    /// </summary>
    /// <param name="parent">The parent element.</param>
    public LifecycleElement(ConfigElement parent)
      : base(parent)
    {
    }

    /// <summary>Gets or sets the config for temp items cleanup.</summary>
    [ConfigurationProperty("tempItemsCleanup")]
    [DescriptionResource(typeof (ConfigDescriptions), "TempItemsCleanupConfig")]
    public virtual TempItemsCleanupElement TempItemsCleanup
    {
      get => (TempItemsCleanupElement) this["tempItemsCleanup"];
      set => this["tempItemsCleanup"] = (object) value;
    }

    [StructLayout(LayoutKind.Sequential, Size = 1)]
    internal struct ConfigProps
    {
      public const string TempItemsCleanup = "tempItemsCleanup";
    }
  }
}
