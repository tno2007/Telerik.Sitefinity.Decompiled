// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Lifecycle.Cleanup.Configuration.TempItemsCleanupElement
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using NCrontab;
using System;
using System.Configuration;
using System.Runtime.InteropServices;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Web.Configuration;

namespace Telerik.Sitefinity.Lifecycle.Cleanup.Configuration
{
  /// <summary>Represents temp items cleanup configuration section.</summary>
  [ObjectInfo(typeof (ConfigDescriptions), Description = "TempItemsCleanupConfigDescription", Title = "TempItemsCleanupConfigCaption")]
  public class TempItemsCleanupElement : ConfigElement
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Lifecycle.Cleanup.Configuration.TempItemsCleanupElement" /> class.
    /// </summary>
    /// <param name="parent">The parent element.</param>
    public TempItemsCleanupElement(ConfigElement parent)
      : base(parent)
    {
    }

    /// <summary>
    ///  Gets or sets a value indicating whether to enable temp items cleanup.
    /// </summary>
    [ObjectInfo(typeof (ConfigDescriptions), Description = "EnableTempItemsCleanupDescription", Title = "EnableTempItemsCleanupTitle")]
    [ConfigurationProperty("enableTempItemsCleanup", DefaultValue = true)]
    public bool EnableTempItemsCleanup
    {
      get => (bool) this["enableTempItemsCleanup"];
      set => this["enableTempItemsCleanup"] = (object) value;
    }

    /// <summary>
    ///  Gets or sets a value indicating how old temps should be deleted.
    /// </summary>
    [ObjectInfo(typeof (ConfigDescriptions), Description = "TempItemsMaxAgeDescription", Title = "TempItemsMaxAgeTitle")]
    [ConfigurationProperty("tempItemsMaxAge", DefaultValue = 30)]
    public int TempItemsMaxAge
    {
      get => (int) this["tempItemsMaxAge"];
      set => this["tempItemsMaxAge"] = (object) value;
    }

    /// <summary>Gets or sets the cron specification for cleanup task.</summary>
    [ConfigurationProperty("autoCleanupCronSpec", DefaultValue = "0 0 * * 0")]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "AutoCleanupCronSpecDescription", Title = "AutoCleanupCronSpecTitle")]
    public string AutoCleanupCronSpec
    {
      get => (string) this["autoCleanupCronSpec"];
      set
      {
        value = value != null ? value.Trim() : throw new ArgumentNullException("autoCleanupCronSpec");
        bool flag = true;
        if (!string.IsNullOrEmpty(value))
        {
          if (value.Split(new char[1]{ ' ' }, StringSplitOptions.RemoveEmptyEntries).Length != 5 || CrontabSchedule.TryParse(value).IsError)
            flag = false;
        }
        if (!flag)
          throw new FormatException("Invalid cron date");
        this["autoCleanupCronSpec"] = (object) value;
      }
    }

    [StructLayout(LayoutKind.Sequential, Size = 1)]
    internal struct ConfigProps
    {
      public const string EnableTempItemsCleanup = "enableTempItemsCleanup";
      public const string TempItemsMaxAge = "tempItemsMaxAge";
      public const string AutoCleanupCronSpec = "autoCleanupCronSpec";
    }
  }
}
