// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Configuration.ConfigSection
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Globalization;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Configuration.Data;

namespace Telerik.Sitefinity.Configuration
{
  /// <summary>Represents configuration section.</summary>
  public class ConfigSection : ConfigElement
  {
    private bool isDefaultConfig;
    protected internal bool isLoadingDefaults;
    private bool initialized;

    /// <summary>
    /// Initializes new instance of ConfigSection.
    /// The type name is used for root tag.
    /// </summary>
    public ConfigSection()
    {
      this.tagName = this.GetType().Name;
      char[] charArray = this.tagName.ToCharArray();
      charArray[0] = char.ToLower(charArray[0], CultureInfo.InvariantCulture);
      int length = charArray.Length;
      if (this.tagName.EndsWith("Section", StringComparison.Ordinal) && this.tagName.Length > 10)
        length = this.tagName.Length - 7;
      this.tagName = new string(charArray, 0, length);
      this.Source = ConfigSource.Default;
    }

    /// <summary>
    /// Initializes new instance of ConfigSection with the provided root name.
    /// </summary>
    /// <param name="tagName">then name of the root tag</param>
    public ConfigSection(string tagName)
    {
      this.tagName = tagName;
      this.Source = ConfigSource.Default;
    }

    /// <summary>
    /// Gets a value indicating whether display the section in the configuration UI tree.
    /// </summary>
    /// <value><c>true</c> if [visible in UI]; otherwise, <c>false</c>.</value>
    public virtual bool VisibleInUI => true;

    /// <summary>
    /// Gets a <see cref="T:System.Boolean" /> value indicating if this instance have been initialized.
    /// </summary>
    public bool Initialized => this.initialized;

    internal bool? SafeMode { get; set; }

    internal bool InstallMode { get; set; }

    /// <summary>
    /// Initializes this instance with the provided data provider.
    /// </summary>
    /// <param name="provider">The data provider to initialize this instance with.</param>
    public virtual void Initialize(ConfigProvider provider)
    {
      if (provider == null)
        throw new ArgumentNullException(nameof (provider));
      if (this.initialized)
        return;
      lock (this)
      {
        if (this.initialized)
          return;
        this.Provider = provider;
        this.Provider.LoadSection(this);
        this.initialized = true;
        if (!Bootstrapper.IsReady)
          return;
        Log.Write((object) string.Format("ConfigSection with name {0} was initialized.", (object) this.GetType().Name), ConfigurationPolicy.TestTracing);
      }
    }

    protected override void InitializeProperties()
    {
      this.isLoadingDefaults = true;
      base.InitializeProperties();
      this.isLoadingDefaults = false;
    }

    /// <summary>
    /// Called when the configuration section needs to be upgraded from older version.
    /// </summary>
    /// <param name="oldVersion">
    /// The version indicated in the current configuration before upgrade.
    /// Can be <c>null</c>, if the old configuration does not support versioning.
    /// </param>
    /// <param name="newVersion">The target version, to be saved after upgrade.</param>
    public virtual void Upgrade(Version oldVersion, Version newVersion)
    {
    }

    internal virtual void InitUpgradeContext(
      Version oldVersion,
      ConfigUpgradeContext upgradeContext)
    {
    }

    internal ConfigUpgradeContext UpgradeContext { get; set; }

    /// <summary>
    /// Called when the corresponding XML element is read and properties loaded.
    /// </summary>
    protected internal virtual void OnPropertiesLoaded()
    {
    }

    /// <summary>
    /// Called when a section is updated internally from additional source, e.g database.
    /// </summary>
    protected internal virtual void OnSectionChanged()
    {
    }

    internal ConfigSection GetDefaultConfig(SaveOptions saveOptions = null)
    {
      if (saveOptions == null)
        saveOptions = new SaveOptions();
      ConfigSection instance = (ConfigSection) Activator.CreateInstance(this.GetType());
      if (this.Provider.StorageMode != ConfigStorageMode.FileSystem && !saveOptions.SkipLoadFromFile && (this.Provider.StorageMode != ConfigStorageMode.Auto || saveOptions.OperationType != OperationType.Export))
        this.Provider.LoadSectionFromFile(instance);
      instance.isDefaultConfig = true;
      return instance;
    }

    protected internal virtual bool TryGenerateLazyElementFileName(
      ConfigElement element,
      out string fileName,
      int maxLength = 0)
    {
      fileName = (string) null;
      return false;
    }

    internal bool IsDefaultConfig => this.isDefaultConfig;
  }
}
