// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Versioning.Web.UI.Basic.RevisionHistorySettingsContract
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Runtime.Serialization;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.SiteSettings;
using Telerik.Sitefinity.Versioning.Cleaner;
using Telerik.Sitefinity.Versioning.Configuration;

namespace Telerik.Sitefinity.Versioning.Web.UI.Basic
{
  /// <summary>Represents the model for Revision history settings.</summary>
  [DataContract]
  public class RevisionHistorySettingsContract : ISettingsDataContract
  {
    /// <summary>
    /// Gets or sets a value indicating whether there is a limit for keeping revision history.
    /// </summary>
    [DataMember]
    public bool HistoryLimitEnabled { get; set; }

    /// <summary>
    /// Gets or sets a value indicating the major versions to keep.
    /// </summary>
    [DataMember]
    public int MajorVersionsToKeep { get; set; }

    /// <summary>Gets or sets a value indicating the time to keep.</summary>
    [DataMember]
    public TimeToKeepEnum TimeToKeep { get; set; }

    /// <inheritdoc />
    public void LoadDefaults(bool forEdit = false)
    {
      VersionConfig versionConfig = Config.Get<VersionConfig>();
      this.HistoryLimitEnabled = versionConfig.Cleaner.HistoryLimitEnabled;
      this.MajorVersionsToKeep = versionConfig.Cleaner.MajorVersionsToKeep;
      this.TimeToKeep = versionConfig.Cleaner.TimeToKeep;
    }

    /// <inheritdoc />
    public void SaveDefaults()
    {
      this.ValidateDefaults();
      ConfigManager manager = Config.GetManager();
      VersionConfig section = manager.GetSection<VersionConfig>();
      section.Cleaner.HistoryLimitEnabled = this.HistoryLimitEnabled;
      if (this.HistoryLimitEnabled)
      {
        section.Cleaner.MajorVersionsToKeep = this.MajorVersionsToKeep;
        section.Cleaner.TimeToKeep = this.TimeToKeep;
      }
      if (VersioningCleanerTask.IsTaskRunning())
        throw new InvalidOperationException(Res.Get<Labels>().VersionCleanerRunning);
      manager.SaveSection((ConfigSection) section);
    }

    private void ValidateDefaults()
    {
      if (this.MajorVersionsToKeep <= 0 || this.MajorVersionsToKeep > 100)
        throw new ArgumentException("Major versions count cannot be less than or equal to 0 and more than 100");
      if (!Enum.IsDefined(typeof (TimeToKeepEnum), (object) this.TimeToKeep))
        throw new ArgumentException("Invalid time to keep enum value.");
    }
  }
}
