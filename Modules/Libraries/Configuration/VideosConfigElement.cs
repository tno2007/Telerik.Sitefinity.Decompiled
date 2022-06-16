// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Libraries.Configuration.VideosConfigElement
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Configuration;
using System.Runtime.InteropServices;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Web.Configuration;

namespace Telerik.Sitefinity.Modules.Libraries.Configuration
{
  /// <summary>
  /// Provides configuration information about the Images in the Libraries module
  /// </summary>
  public class VideosConfigElement : ConfigElement
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Modules.Libraries.Configuration.ImagesConfigElement" /> class.
    /// </summary>
    /// <param name="parent">The parent element.</param>
    /// <remarks>
    /// ConfigElementCollection generally needs to have a parent, however, sometimes it is necessary
    /// to create a collection in memory only which is then used later on in the context of a parent.
    /// Therefore, is the element is of ConfigElementCollection, exception for a non existing parent
    /// will not be thrown.
    /// </remarks>
    public VideosConfigElement(ConfigElement parent)
      : base(parent)
    {
    }

    /// <summary>Gets or sets the name of the connection string.</summary>
    /// <value>The name of the connection string.</value>
    [ConfigurationProperty("urlRoot", DefaultValue = "videos", IsRequired = true)]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "LibrariesUrlRootDescription", Title = "LibrariesUrlRootCaption")]
    public string UrlRoot
    {
      get => (string) this["urlRoot"];
      set => this["urlRoot"] = (object) value;
    }

    /// <summary>Gets the configured AllowedExensionsElement settings.</summary>
    /// <value>The AllowedExensionsElement settings.</value>
    [ConfigurationProperty("AllowedExensionsSettings")]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "AllowedExtensionSettingsDescription", Title = "AllowedExtensionSettingsTitle")]
    public string AllowedExensionsSettings
    {
      get => (string) this[nameof (AllowedExensionsSettings)];
      set => this[nameof (AllowedExensionsSettings)] = (object) value;
    }

    /// <summary>
    /// Gets the configured AllowedVideoFoldersSettings settings.
    /// </summary>
    /// <value>The AllowedVideoFoldersSettings settings.</value>
    [ConfigurationProperty("AllowedTempFileFoldersSettings")]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "AllowedTempFileFoldersSettingsDescription", Title = "AllowedTempFileFoldersSettingsTitle")]
    public string AllowedTempFileFoldersSettings
    {
      get => (string) this[nameof (AllowedTempFileFoldersSettings)];
      set => this[nameof (AllowedTempFileFoldersSettings)] = (object) value;
    }

    /// <summary>Maximum allowed size in KB.</summary>
    /// <value>Maximum allowed size in KB.</value>
    [ConfigurationProperty("AllowedMaxVideoSize", DefaultValue = 0)]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "AllowedMaxSizeDescription", Title = "AllowedMaxSizeTitle")]
    public int AllowedMaxVideoSize
    {
      get => (int) this[nameof (AllowedMaxVideoSize)];
      set => this[nameof (AllowedMaxVideoSize)] = (object) value;
    }

    /// <summary>Gets the thumbnails configuration.</summary>
    [ConfigurationProperty("thumbnails")]
    public ThumbnailsConfigElement Thumbnails => (ThumbnailsConfigElement) this["thumbnails"];

    /// <summary>
    /// Called after the properties of this instance have been initialized.
    /// Load default values here.
    /// </summary>
    protected override void OnPropertiesInitialized() => this.InitializeAllowedExensionsSettings();

    /// <summary>Initializes the allowed exensions settings.</summary>
    private void InitializeAllowedExensionsSettings() => this.AllowedExensionsSettings = ".mp4, .webm, .ogv";

    [StructLayout(LayoutKind.Sequential, Size = 1)]
    private struct PropNames
    {
      public const string AllowedExensionsSettings = "AllowedExensionsSettings";
      public const string AllowedMaxVideoSize = "AllowedMaxVideoSize";
      public const string Thumbnails = "thumbnails";
      public const string UrlRoot = "urlRoot";
      public const string AllowedTempFileFoldersSettings = "AllowedTempFileFoldersSettings";
    }
  }
}
