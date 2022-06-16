// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Libraries.Configuration.ImagesConfigElement
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.ComponentModel;
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
  public class ImagesConfigElement : ConfigElement
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
    public ImagesConfigElement(ConfigElement parent)
      : base(parent)
    {
    }

    /// <summary>Gets or sets the name of the connection string.</summary>
    /// <value>The name of the connection string.</value>
    [ConfigurationProperty("urlRoot", DefaultValue = "images", IsRequired = true)]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "LibrariesUrlRootDescription", Title = "LibrariesUrlRootCaption")]
    public string UrlRoot
    {
      get => (string) this["urlRoot"];
      set => this["urlRoot"] = (object) value;
    }

    /// <summary>Gets the configured AllowedExensionsElement settings.</summary>
    /// <value>The AllowedExensionsElement settings.</value>
    [ConfigurationProperty("allowedExensionsSettings")]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "AllowedExtensionSettingsDescription", Title = "AllowedExtensionSettingsTitle")]
    public string AllowedExensionsSettings
    {
      get => (string) this["allowedExensionsSettings"];
      set => this["allowedExensionsSettings"] = (object) value;
    }

    /// <summary>Maximum allowed size in KB.</summary>
    /// <value>Maximum allowed size in KB.</value>
    [ConfigurationProperty("allowedMaxImageSize", DefaultValue = 0)]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "AllowedMaxSizeDescription", Title = "AllowedMaxSizeTitle")]
    public int AllowedMaxImageSize
    {
      get => (int) this["allowedMaxImageSize"];
      set => this["allowedMaxImageSize"] = (object) value;
    }

    /// <summary>Gets the configured AllowedExensionsElement settings.</summary>
    /// <value>The AllowDynamicResizing settings.</value>
    [ConfigurationProperty("allowDynamicResizing", DefaultValue = true)]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "AllowDynamicResizingDescription", Title = "AllowDynamicResizingTitle")]
    public bool AllowDynamicResizing
    {
      get => (bool) this["allowDynamicResizing"];
      set => this["allowDynamicResizing"] = (object) value;
    }

    /// <summary>
    /// Gets the configured AllowLegacyDynamicResizing settings.
    /// </summary>
    /// <value>The AllowLegacyDynamicResizing settings.</value>
    [ConfigurationProperty("allowUnsignedDynamicResizing", DefaultValue = false)]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "AllowUnsignedDynamicResizingDescription", Title = "AllowUnsignedDynamicResizingTitle")]
    public bool AllowUnsignedDynamicResizing
    {
      get => (bool) this["allowUnsignedDynamicResizing"];
      set => this["allowUnsignedDynamicResizing"] = (object) value;
    }

    /// <summary>Gets the configured AllowedExensionsElement settings.</summary>
    /// <value>The AllowDynamicResizing settings.</value>
    [ConfigurationProperty("storeDynamicResizedImagesAsThumbnails", DefaultValue = true)]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "StoreDynamicResizedImagesAsThumbnailsDescription", Title = "StoreDynamicResizedImagesAsThumbnailsTitle")]
    public bool StoreDynamicResizedImagesAsThumbnails
    {
      get => (bool) this["storeDynamicResizedImagesAsThumbnails"];
      set => this["storeDynamicResizedImagesAsThumbnails"] = (object) value;
    }

    /// <summary>Gets the configured EnableImageUrlSignature settings.</summary>
    /// <value>The EnableImageUrlSignature settings.</value>
    [ConfigurationProperty("enableImageUrlSignature", DefaultValue = true)]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "EnableImageUrlSignatureDescription", Title = "EnableImageUrlSignatureTitle")]
    public bool EnableImageUrlSignature
    {
      get => (bool) this["enableImageUrlSignature"];
      set => this["enableImageUrlSignature"] = (object) value;
    }

    /// <summary>Gets or sets the image signature hash algorithm.</summary>
    [ConfigurationProperty("imageUrlHashAlgorithm", DefaultValue = ImageUrlSignatureHashAlgorithm.SHA1)]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "ImageUrlSignatureHashAlgorithmDescription", Title = "ImageUrlSignatureHashAlgorithmTitle")]
    public ImageUrlSignatureHashAlgorithm ImageUrlSignatureHashAlgorithm
    {
      get => (ImageUrlSignatureHashAlgorithm) this["imageUrlHashAlgorithm"];
      set => this["imageUrlHashAlgorithm"] = (object) value;
    }

    /// <summary>
    /// Enables Multiple Hash Algorithms for thumbnail hash generation.
    /// When set to true it will force the <see cref="!:LazyThumbnailGenerator" /> to check all algorithms
    /// </summary>
    [ConfigurationProperty("enableMultipleHashAlgorithmsSupport", DefaultValue = false)]
    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public bool EnableMultipleHashAlgorithmsSupport
    {
      get => (bool) this["enableMultipleHashAlgorithmsSupport"];
      set => this["enableMultipleHashAlgorithmsSupport"] = (object) value;
    }

    [ConfigurationProperty("dynamicResizingThreadsCount", DefaultValue = 16)]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "DynamicResizingThreadsCountDescription", Title = "DynamicResizingThreadsCountTitle")]
    public int DynamicResizingThreadsCount
    {
      get => (int) this["dynamicResizingThreadsCount"];
      set => this["dynamicResizingThreadsCount"] = (object) value;
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
    private void InitializeAllowedExensionsSettings() => this.AllowedExensionsSettings = ".gif, .jpg, .jpeg, .png, .bmp";

    [StructLayout(LayoutKind.Sequential, Size = 1)]
    internal struct PropNames
    {
      public const string AllowedExensionsSettings = "allowedExensionsSettings";
      public const string AllowDynamicResizing = "allowDynamicResizing";
      public const string AllowUnsignedDynamicResizing = "allowUnsignedDynamicResizing";
      public const string StoreDynamicResizedImagesAsThumbnails = "storeDynamicResizedImagesAsThumbnails";
      public const string EnableImageUrlSignature = "enableImageUrlSignature";
      public const string DynamicResizingThreadsCount = "dynamicResizingThreadsCount";
      public const string Thumbnails = "thumbnails";
      public const string UrlRoot = "urlRoot";
      public const string ImageUrlHashAlgorithm = "imageUrlHashAlgorithm";
      public const string EnableMultipleHashAlgorithmsSupport = "enableMultipleHashAlgorithmsSupport";
      public const string AllowedMaxImageSize = "allowedMaxImageSize";
    }
  }
}
