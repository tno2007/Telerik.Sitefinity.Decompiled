// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Libraries.Configuration.LibrariesConfig
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Configuration;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Libraries.Model;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Modules.GenericContent;
using Telerik.Sitefinity.Modules.GenericContent.Configuration;
using Telerik.Sitefinity.Modules.Libraries.BlobStorage;
using Telerik.Sitefinity.Modules.Libraries.Data;
using Telerik.Sitefinity.Modules.Libraries.Documents;
using Telerik.Sitefinity.Modules.Libraries.Images;
using Telerik.Sitefinity.Modules.Libraries.UserFiles;
using Telerik.Sitefinity.Modules.Libraries.Videos;
using Telerik.Sitefinity.Modules.Libraries.Web.UI.BlobStorage;
using Telerik.Sitefinity.Processors.Configuration;
using Telerik.Sitefinity.Security.Sanitizers;
using Telerik.Sitefinity.Utilities.TypeConverters;
using Telerik.Sitefinity.Web.Configuration;
using Telerik.Sitefinity.Web.UI.ContentUI.Config;

namespace Telerik.Sitefinity.Modules.Libraries.Configuration
{
  /// <summary>
  /// Provides configuration information about the Libraries module
  /// </summary>
  [ObjectInfo(typeof (ConfigDescriptions), Description = "LibrariesConfigDescription", Title = "LibrariesConfigCaption")]
  public class LibrariesConfig : ContentModuleConfigBase, IHaveConfigProcessors
  {
    private Dictionary<string, string> mappings;
    internal const string CustomImageSizeAllowedParam = "customImageSizeAllowed";
    internal const string DefaultSanitizerTitle = "Svg sanitizer";
    private const string numberOfChunksToCommit = "numberOfChunksToCommit";
    private const string sizeOfChunk = "sizeOfChunk";
    private const string enableFileSystemFallback = "enableFileSystemFallback";
    private const string contentBlockVideoTagPropName = "videoTag";
    private const string enableAllLanguagesSearchPropName = "enableAllLanguagesSearch";
    internal const string DefaultThumbnailName = "thumbnail";
    private const string enableSelectedFolderSearchPropName = "enableSelectedFolderSearch";
    private const string enableOneClickUploadPropName = "enableOneClickUpload";
    private const string itemsCount = "itemsCount";
    private const string accessKeyId = "accessKeyId";
    private const string secretKey = "secretKey";
    private const string bucketName = "bucketName";
    private const string regionEndpoint = "regionEndpoint";
    private const string urlSchemeKey = "urlScheme";

    /// <summary>Gets the images configuration.</summary>
    /// <value>The images.</value>
    [ConfigurationProperty("images")]
    public ImagesConfigElement Images => (ImagesConfigElement) this["images"];

    /// <summary>Gets the images configuration.</summary>
    /// <value>The images.</value>
    [ConfigurationProperty("documents")]
    public DocumentsConfigElement Documents => (DocumentsConfigElement) this["documents"];

    /// <summary>Gets the images configuration.</summary>
    /// <value>The images.</value>
    [ConfigurationProperty("videos")]
    public VideosConfigElement Videos => (VideosConfigElement) this["videos"];

    /// <summary>Blob storage configuration.</summary>
    [ConfigurationProperty("blobStorage")]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "BlobStorageDescription", Title = "BlobStorageTitle")]
    public BlobStorageConfigElement BlobStorage => (BlobStorageConfigElement) this["blobStorage"];

    [ConfigurationProperty("defaultUrlRoot", DefaultValue = "Libraries")]
    public string DefaultUrlRoot
    {
      get => (string) this["defaultUrlRoot"];
      set => this["defaultUrlRoot"] = (object) value;
    }

    [ConfigurationProperty("urlVersionQueryParameter", DefaultValue = "sfvrsn")]
    public string UrlVersionQueryParameter
    {
      get => (string) this["urlVersionQueryParameter"];
      set => this["urlVersionQueryParameter"] = (object) value;
    }

    [ConfigurationProperty("thumbnailProfilePrefix", DefaultValue = "tmb-")]
    public string ThumbnailExtensionPrefix
    {
      get => (string) this["thumbnailProfilePrefix"];
      set => this["thumbnailProfilePrefix"] = (object) value;
    }

    /// <summary>
    /// Gets the configured 'NumberOfChunksToCommit' settings.
    /// </summary>
    /// <value>The numberOfChunksToCommit.</value>
    [ConfigurationProperty("numberOfChunksToCommit", DefaultValue = 10)]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "NumberOfChunksToCommitDescription", Title = "NumberOfChunksToCommitTitle")]
    public int NumberOfChunksToCommit
    {
      get => (int) this["numberOfChunksToCommit"];
      set => this["numberOfChunksToCommit"] = (object) value;
    }

    /// <summary>
    /// Gets the configured size Of chunk settings needed when upload media data.
    /// </summary>
    /// <value>The size Of chunk when upload media data.</value>
    [ConfigurationProperty("sizeOfChunk", DefaultValue = 81920)]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "SizeOfChunkDescription", Title = "SizeOfChunkTitle")]
    public int SizeOfChunk
    {
      get => (int) this["sizeOfChunk"];
      set => this["sizeOfChunk"] = (object) value;
    }

    /// <summary>
    /// Gets or sets a value indicating whether [handler file system fallback].
    /// </summary>
    /// <value>
    /// 	<c>true</c> if [handler file system fallback]; otherwise, <c>false</c>.
    /// </value>
    [ConfigurationProperty("enableFileSystemFallback", DefaultValue = false)]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "EnableFileSystemFallbackDescription", Title = "EnableFileSystemFallbackTitle")]
    [Browsable(false)]
    [Obsolete("This fallback mechanism should no longer be used. Please, rename the library`s root url if you want to serve static files from directories with conflicting names or use other names for these directories.")]
    public bool EnableFileSystemFallback
    {
      get => (bool) this["enableFileSystemFallback"];
      set => this["enableFileSystemFallback"] = (object) value;
    }

    /// <summary>Gets a collection of mime mapping settings.</summary>
    [DescriptionResource(typeof (ConfigDescriptions), "MimeMappings")]
    [ConfigurationProperty("mimeMappings")]
    public virtual ConfigElementDictionary<string, MimeMappingElement> MimeMappings => (ConfigElementDictionary<string, MimeMappingElement>) this["mimeMappings"];

    /// <summary>Gets the byte range settings.</summary>
    [ConfigurationProperty("byteRangeSettings")]
    [DescriptionResource(typeof (ConfigDescriptions), "ByteRangeSettings")]
    public ByteRangeConfigElement ByteRangeSettings => (ByteRangeConfigElement) this["byteRangeSettings"];

    /// <summary>Gets the settings for additional URLs to media files.</summary>
    [ConfigurationProperty("mediaFileAdditionalUrls")]
    public MediaFileAdditionalUrlsConfigElement MediaFileAdditionalUrls => (MediaFileAdditionalUrlsConfigElement) this["mediaFileAdditionalUrls"];

    /// <summary>
    /// Gets or sets the Html tag that will be used to display video in a content block.
    /// </summary>
    /// <value>The content block video tag.</value>
    [DescriptionResource(typeof (ConfigDescriptions), "ContentBlockVideoTag")]
    [ConfigurationProperty("videoTag", DefaultValue = ContentBlockVideoTag.Video)]
    public virtual ContentBlockVideoTag ContentBlockVideoTag
    {
      get => (ContentBlockVideoTag) this["videoTag"];
      set => this["videoTag"] = (object) value;
    }

    /// <summary>
    /// Gets the configured EnableAllLanguagesSearch settings.
    /// </summary>
    /// <value>The EnableAllLanguagesSearch settings.</value>
    [ConfigurationProperty("enableAllLanguagesSearch", DefaultValue = false)]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "EnableAllLanguagesSearchDescription", Title = "EnableAllLanguagesSearchTitle")]
    public bool EnableAllLanguagesSearch
    {
      get => (bool) this["enableAllLanguagesSearch"];
      set => this["enableAllLanguagesSearch"] = (object) value;
    }

    /// <summary>
    /// Gets the configured EnableSelectedFolderSearch settings.
    /// </summary>
    /// <value>THe EnableSelectedFolderSearch settings.</value>
    [ConfigurationProperty("enableSelectedFolderSearch", DefaultValue = false)]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "EnableSelectedFolderSearchDescription", Title = "EnableSelectedFolderSearchTitle")]
    public bool EnableSelectedFolderSearch
    {
      get => (bool) this["enableSelectedFolderSearch"];
      set => this["enableSelectedFolderSearch"] = (object) value;
    }

    /// <summary>Gets the configured EnableOneClickUpload settings.</summary>
    /// <value>THe EnableOneClickUpload settings.</value>
    [ConfigurationProperty("enableOneClickUpload", DefaultValue = false)]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "EnableOneClickUploadDescription", Title = "EnableOneClickUploadTitle")]
    public bool EnableOneClickUpload
    {
      get => (bool) this["enableOneClickUpload"];
      set => this["enableOneClickUpload"] = (object) value;
    }

    /// <summary>
    /// Gets the configured value for the albums or folder that will be initially bound by MediaSelectorFilterPanel or FolderSelector.
    /// </summary>
    [ConfigurationProperty("itemsCount", DefaultValue = 0)]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "ItemsCountDescription", Title = "ItemsCountTitle")]
    public int ItemsCount
    {
      get => (int) this["itemsCount"];
      set => this["itemsCount"] = (object) value;
    }

    /// <summary>Gets a collection of FileProcessors settings.</summary>
    [ConfigurationProperty("FileProcessors")]
    public virtual ConfigElementDictionary<string, ProcessorConfigElement> FileProcessors => (ConfigElementDictionary<string, ProcessorConfigElement>) this[nameof (FileProcessors)] ?? new ConfigElementDictionary<string, ProcessorConfigElement>();

    /// <inheritdoc />
    public ConfigElementDictionary<string, ProcessorConfigElement> GetConfigProcessors() => this.FileProcessors;

    /// <summary>
    /// Called after the properties of this instance have been initialized.
    /// Load default values here.
    /// </summary>
    protected override void OnPropertiesInitialized()
    {
      base.OnPropertiesInitialized();
      this.ConfigureMimeMappings();
      this.InitializeBlobStorageProviders();
      this.InitializeThumbnailSettings();
      this.ConfigureFileProcessors();
    }

    private void InitializeThumbnailSettings()
    {
      this.Images.Thumbnails.Profiles.Add(new ThumbnailProfileConfigElement((ConfigElement) this.Images.Thumbnails.Profiles)
      {
        Title = "Thumbnail: 120x120 px cropped",
        Name = "thumbnail",
        Method = "CropCropArguments",
        Parameters = new NameValueCollection()
        {
          {
            "Height",
            "120"
          },
          {
            "Width",
            "120"
          },
          {
            "Quality",
            "Medium"
          }
        }
      });
      this.Images.Thumbnails.Profiles.Add(new ThumbnailProfileConfigElement((ConfigElement) this.Images.Thumbnails.Profiles)
      {
        Title = "Small: 240 px width",
        Name = "small",
        Method = "ResizeFitToAreaArguments",
        Parameters = new NameValueCollection()
        {
          {
            "MaxWidth",
            "240"
          },
          {
            "Quality",
            "Medium"
          }
        }
      });
      this.Images.Thumbnails.Profiles.Add(new ThumbnailProfileConfigElement((ConfigElement) this.Images.Thumbnails.Profiles)
      {
        Title = "Medium: 240x300 px resized",
        Name = "medium",
        Method = "ResizeFitToAreaArguments",
        Parameters = new NameValueCollection()
        {
          {
            "MaxHeight",
            "300"
          },
          {
            "MaxWidth",
            "240"
          },
          {
            "Quality",
            "Medium"
          }
        }
      });
      this.Videos.Thumbnails.Profiles.Add(new ThumbnailProfileConfigElement((ConfigElement) this.Videos.Thumbnails.Profiles)
      {
        Title = "Thumbnail: 120x94 px cropped",
        Name = "vthumbnail",
        Method = "CropCropArguments",
        Parameters = new NameValueCollection()
        {
          {
            "Height",
            "94"
          },
          {
            "Width",
            "120"
          },
          {
            "Quality",
            "Medium"
          }
        },
        IsDefault = true
      });
    }

    private void ConfigureFileProcessors() => this.FileProcessors.Add("Svg sanitizer", new ProcessorConfigElement((ConfigElement) this.FileProcessors)
    {
      Enabled = true,
      Description = "The SVG sanitizer removes all non-whitelisted SVG tags, attributes, and JavaScript code lines, embedded in SVG images",
      Name = "Svg sanitizer",
      Type = typeof (SvgSanitizer).FullName
    });

    public override void Upgrade(Version oldVersion, Version newVersion)
    {
      base.Upgrade(oldVersion, newVersion);
      if (oldVersion.Build >= 4100)
        return;
      if (this.ContentViewControls.Contains("LibraryImagesBackend"))
        this.ContentViewControls.Remove("LibraryImagesBackend");
      if (this.ContentViewControls.Contains(DocumentsDefinitions.BackendLibraryDocumentsDefinitionName))
        this.ContentViewControls.Remove(DocumentsDefinitions.BackendLibraryDocumentsDefinitionName);
      if (!this.ContentViewControls.Contains(VideosDefinitions.BackendLibraryVideosDefinitionName))
        return;
      this.ContentViewControls.Remove(VideosDefinitions.BackendLibraryVideosDefinitionName);
    }

    /// <summary>Initializes the default providers.</summary>
    protected override void InitializeDefaultProviders(
      ConfigElementDictionary<string, DataProviderSettings> providers)
    {
      providers.Add(new DataProviderSettings((ConfigElement) providers)
      {
        Name = "OpenAccessDataProvider",
        Title = "Default Libraries",
        Description = "A provider that stores non-system libraries data in database using OpenAccess ORM.",
        ProviderType = typeof (OpenAccessLibrariesProvider),
        Parameters = new NameValueCollection()
        {
          {
            "applicationName",
            "/Libraries"
          },
          {
            "urlName",
            "default-source"
          }
        }
      });
      providers.Add(new DataProviderSettings((ConfigElement) providers)
      {
        Name = "SystemLibrariesProvider",
        Title = "System",
        Description = "A provider that stores system libraries data in database using OpenAccess ORM.",
        ProviderType = typeof (OpenAccessLibrariesProvider),
        Parameters = new NameValueCollection()
        {
          {
            "applicationName",
            "/SystemLibraries"
          },
          {
            "providerGroup",
            "System"
          }
        }
      });
    }

    protected void InitializeBlobStorageProviders()
    {
      ConfigElementDictionary<string, BlobStorageTypeConfigElement> blobStorageTypes = this.BlobStorage.BlobStorageTypes;
      blobStorageTypes.Add(new BlobStorageTypeConfigElement((ConfigElement) blobStorageTypes)
      {
        Name = "Database",
        ProviderType = typeof (OpenAccessBlobStorageProvider),
        Title = "Database",
        Description = "BlobStorageDatabaseTypeDescription",
        ResourceClassId = typeof (LibrariesResources).Name,
        SettingsViewTypeOrPath = typeof (DatabaseBlobSettingsView).FullName
      });
      blobStorageTypes.Add(new BlobStorageTypeConfigElement((ConfigElement) blobStorageTypes)
      {
        Name = "FileSystem",
        ProviderType = typeof (FileSystemStorageProvider),
        Title = "FileSystem",
        Description = "BlobStorageFileSystemTypeDescription",
        ResourceClassId = typeof (LibrariesResources).Name,
        SettingsViewTypeOrPath = typeof (FileSystemBlobSettingsView).FullName
      });
      blobStorageTypes.Add(new BlobStorageTypeConfigElement((ConfigElement) blobStorageTypes)
      {
        Name = "Amazon",
        ProviderType = TypeResolutionService.ResolveType("Telerik.Sitefinity.Amazon.BlobStorage.AmazonBlobStorageProvider, Telerik.Sitefinity.Amazon", false),
        Title = "AmazonS3",
        Description = "BlobStorageAmazonTypeDescription",
        ResourceClassId = typeof (LibrariesResources).Name,
        Parameters = new NameValueCollection()
        {
          {
            "accessKeyId",
            string.Empty
          },
          {
            "secretKey",
            string.Empty
          },
          {
            "bucketName",
            string.Empty
          },
          {
            "regionEndpoint",
            string.Empty
          },
          {
            "urlScheme",
            string.Empty
          }
        }
      });
      ConfigElementDictionary<string, DataProviderSettings> providers = this.BlobStorage.Providers;
      providers.Add(new DataProviderSettings((ConfigElement) providers)
      {
        Name = "Database",
        Title = "BlobStorageDatabaseDefaultProviderTitle",
        Description = "BlobStorageDatabaseDefaultProviderDescription",
        ResourceClassId = typeof (LibrariesResources).Name,
        ProviderType = typeof (OpenAccessBlobStorageProvider),
        Parameters = new NameValueCollection()
        {
          {
            "applicationName",
            "/Blob"
          },
          {
            "customImageSizeAllowed",
            "True"
          }
        }
      });
      providers.Add(new DataProviderSettings((ConfigElement) providers)
      {
        Name = "FileSystem",
        Title = "BlobStorageFileSystemDefaultProviderTitle",
        Description = "BlobStorageFileSystemDefaultProviderDescription",
        ResourceClassId = typeof (LibrariesResources).Name,
        ProviderType = typeof (FileSystemStorageProvider),
        Parameters = new NameValueCollection()
        {
          {
            "applicationName",
            "/Blob"
          },
          {
            "customImageSizeAllowed",
            "True"
          },
          {
            "storageFolder",
            "~/App_Data/Storage/FileSystem"
          }
        }
      });
      this.BlobStorage.DefaultProvider = "Database";
    }

    /// <summary>Initializes the default views.</summary>
    protected override void InitializeDefaultViews(
      ConfigElementDictionary<string, ContentViewControlElement> contentViewControls)
    {
      this.ConfigureLibrariesContentView(contentViewControls);
      this.ConfigureImageContentView(contentViewControls);
      this.ConfigureDocumentContentView(contentViewControls);
      this.ConfigureVideoContentView(contentViewControls);
      this.ConfigureUserFilesContentView(contentViewControls);
    }

    internal ConfigElementDictionary<string, ThumbnailProfileConfigElement> GetThumbnailProfiles(
      string libraryType)
    {
      if (libraryType == typeof (Album).FullName)
        return this.Images.Thumbnails.Profiles;
      return libraryType == typeof (VideoLibrary).FullName ? this.Videos.Thumbnails.Profiles : (ConfigElementDictionary<string, ThumbnailProfileConfigElement>) null;
    }

    internal ConfigElementDictionary<string, ThumbnailProfileConfigElement> GetThumbnailProfiles(
      Type libraryType)
    {
      return this.GetThumbnailProfiles(libraryType.FullName);
    }

    private void ConfigureLibrariesContentView(
      ConfigElementDictionary<string, ContentViewControlElement> contentViewControls)
    {
      contentViewControls.Add(LibrariesDefinitions.DefineBlobStorageBackendContentView((ConfigElement) contentViewControls));
      contentViewControls.Add(LibrariesDefinitions.DefineThumbnailsBackendContentView((ConfigElement) contentViewControls));
    }

    private void ConfigureImageContentView(
      ConfigElementDictionary<string, ContentViewControlElement> contentViewControls)
    {
      contentViewControls.Add(ImagesDefinitions.DefineAlbumsBackendContentView((ConfigElement) contentViewControls));
      contentViewControls.Add(ImagesDefinitions.DefineImagesBackendContentView((ConfigElement) contentViewControls));
      contentViewControls.Add(ImagesDefinitions.DefineFoldersBackendContentView((ConfigElement) contentViewControls));
      contentViewControls.Add(ImagesDefinitions.DefineFrontendImagesContentView((ConfigElement) contentViewControls));
      contentViewControls.Add(CommentsDefinitions.DefineCommentsFrontendView((ConfigElement) contentViewControls, ImagesDefinitions.FrontendCommentsDefinitionName, this.DefaultProvider, typeof (LibrariesManager)));
    }

    private void ConfigureUserFilesContentView(
      ConfigElementDictionary<string, ContentViewControlElement> contentViewControls)
    {
      contentViewControls.Add(UserFilesDefinitions.DefineUserFileLibrariesBackendContentView((ConfigElement) contentViewControls));
      contentViewControls.Add(UserFilesDefinitions.DefineUserFileLibraryDocumentsBackendContentView((ConfigElement) contentViewControls));
    }

    private void ConfigureDocumentContentView(
      ConfigElementDictionary<string, ContentViewControlElement> contentViewControls)
    {
      contentViewControls.Add(DocumentsDefinitions.DefineDocumentsBackendContentView((ConfigElement) contentViewControls));
      contentViewControls.Add(DocumentsDefinitions.DefineLibrariesBackendContentView((ConfigElement) contentViewControls));
      contentViewControls.Add(DocumentsDefinitions.DefineFoldersBackendContentView((ConfigElement) contentViewControls));
      contentViewControls.Add(DocumentsDefinitions.DefineFrontendDocumentContentView((ConfigElement) contentViewControls));
    }

    private void ConfigureVideoContentView(
      ConfigElementDictionary<string, ContentViewControlElement> contentViewControls)
    {
      contentViewControls.Add(VideosDefinitions.DefineVideosBackendContentView((ConfigElement) contentViewControls));
      contentViewControls.Add(VideosDefinitions.DefineLibrariesBackendContentView((ConfigElement) contentViewControls));
      contentViewControls.Add(VideosDefinitions.DefineFoldersBackendContentView((ConfigElement) contentViewControls));
      contentViewControls.Add(VideosDefinitions.DefineFrontendVideosContentView((ConfigElement) contentViewControls));
    }

    private void ConfigureMimeMappings()
    {
      if (this.mappings == null)
        this.mappings = this.InitializeMappings();
      foreach (KeyValuePair<string, string> mapping in this.mappings)
        this.MimeMappings.Add(mapping.Key, new MimeMappingElement((ConfigElement) this.MimeMappings)
        {
          MimeType = mapping.Value,
          FileExtension = mapping.Key
        });
    }

    private Dictionary<string, string> InitializeMappings() => new Dictionary<string, string>((IEqualityComparer<string>) StringComparer.OrdinalIgnoreCase)
    {
      {
        ".bmp",
        "image/bmp"
      },
      {
        ".ico",
        "image/x-icon"
      },
      {
        ".jpg",
        "image/jpeg"
      },
      {
        ".jfif",
        "image/pjpeg"
      },
      {
        ".jpe",
        "image/jpeg"
      },
      {
        ".jpeg",
        "image/jpeg"
      },
      {
        ".tif",
        "image/tiff"
      },
      {
        ".gif",
        "image/gif"
      },
      {
        ".tiff",
        "image/tiff"
      },
      {
        ".png",
        "image/png"
      },
      {
        ".svg",
        "image/svg+xml"
      },
      {
        ".avi",
        "video/x-msvideo"
      },
      {
        ".asf",
        "video/x-ms-asf"
      },
      {
        ".ivf",
        "video/x-ivf"
      },
      {
        ".lsx",
        "video/x-la-asf"
      },
      {
        ".lsf",
        "video/x-la-asf"
      },
      {
        ".mpv2",
        "video/mpeg"
      },
      {
        ".mpeg",
        "video/mpeg"
      },
      {
        ".m1v",
        "video/mpeg"
      },
      {
        ".mpa",
        "video/mpeg"
      },
      {
        ".movie",
        "video/x-sgi-movie"
      },
      {
        ".mpe",
        "video/mpeg"
      },
      {
        ".mp2",
        "video/mpeg"
      },
      {
        ".mov",
        "video/quicktime"
      },
      {
        ".mpg",
        "video/mpeg"
      },
      {
        ".qt",
        "video/quicktime"
      },
      {
        ".swf",
        "application/x-shockwave-flash"
      },
      {
        ".m4v",
        "video/m4v"
      },
      {
        ".ogv",
        "video/ogg"
      },
      {
        ".wmv",
        "video/x-ms-wmv"
      },
      {
        ".mp4",
        "video/mp4"
      },
      {
        ".webm",
        "video/webm"
      },
      {
        ".html",
        "text/html"
      },
      {
        ".htm",
        "text/html"
      },
      {
        ".pdf",
        "application/pdf"
      },
      {
        ".scd",
        "application/x-msschedule"
      },
      {
        ".txt",
        "text/plain"
      },
      {
        ".rtf",
        "text/rtf"
      },
      {
        ".doc",
        "application/msword"
      },
      {
        ".dot",
        "application/msword"
      },
      {
        ".docx",
        "application/vnd.openxmlformats-officedocument.wordprocessingml.document"
      },
      {
        ".dotx",
        "application/vnd.openxmlformats-officedocument.wordprocessingml.template"
      },
      {
        ".docm",
        "application/vnd.ms-word.document.macroEnabled.12"
      },
      {
        ".dotm",
        "application/vnd.ms-word.template.macroEnabled.12"
      },
      {
        ".xls",
        "application/vnd.ms-excel"
      },
      {
        ".xlt",
        "application/vnd.ms-excel"
      },
      {
        ".xla",
        "application/vnd.ms-excel"
      },
      {
        ".xlsx",
        "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"
      },
      {
        ".xltx",
        "application/vnd.openxmlformats-officedocument.spreadsheetml.template"
      },
      {
        ".xlsm",
        "application/vnd.ms-excel.sheet.macroEnabled.12"
      },
      {
        ".xltm",
        "application/vnd.ms-excel.template.macroEnabled.12"
      },
      {
        ".xlam",
        "application/vnd.ms-excel.addin.macroEnabled.12"
      },
      {
        ".xlsb",
        "application/vnd.ms-excel.sheet.binary.macroEnabled.12"
      },
      {
        ".ppt",
        "application/vnd.ms-powerpoint"
      },
      {
        ".pot",
        "application/vnd.ms-powerpoint"
      },
      {
        ".pps",
        "application/vnd.ms-powerpoint"
      },
      {
        ".ppa",
        "application/vnd.ms-powerpoint"
      },
      {
        ".pptx",
        "application/vnd.openxmlformats-officedocument.presentationml.presentation"
      },
      {
        ".potx",
        "application/vnd.openxmlformats-officedocument.presentationml.template"
      },
      {
        ".ppsx",
        "application/vnd.openxmlformats-officedocument.presentationml.slideshow"
      },
      {
        ".ppam",
        "application/vnd.ms-powerpoint.addin.macroEnabled.12"
      },
      {
        ".pptm",
        "application/vnd.ms-powerpoint.presentation.macroEnabled.12"
      },
      {
        ".potm",
        "application/vnd.ms-powerpoint.presentation.macroEnabled.12"
      },
      {
        ".ppsm",
        "application/vnd.ms-powerpoint.slideshow.macroEnabled.12"
      },
      {
        ".odb",
        "application/vnd.oasis.opendocument.database"
      },
      {
        ".odc",
        "application/vnd.oasis.opendocument.chart"
      },
      {
        ".odf",
        "application/vnd.oasis.opendocument.formula"
      },
      {
        ".odg",
        "application/vnd.oasis.opendocument.graphics"
      },
      {
        ".odi",
        "application/vnd.oasis.opendocument.image"
      },
      {
        ".odm",
        "application/vnd.oasis.opendocument.text-master"
      },
      {
        ".odp",
        "application/vnd.oasis.opendocument.presentation"
      },
      {
        ".ods",
        "application/vnd.oasis.opendocument.spreadsheet"
      },
      {
        ".odt",
        "application/vnd.oasis.opendocument.text"
      },
      {
        ".otg",
        "application/vnd.oasis.opendocument.graphics-template"
      },
      {
        ".oth",
        "application/vnd.oasis.opendocument.text-web"
      },
      {
        ".otp",
        "application/vnd.oasis.opendocument.presentation-template"
      },
      {
        ".ots",
        "application/vnd.oasis.opendocument.spreadsheet-template"
      },
      {
        ".ott",
        "application/vnd.oasis.opendocument.text-template"
      },
      {
        ".*",
        "application/octet-stream"
      },
      {
        ".zip",
        "application/x-zip-compressed"
      },
      {
        ".json",
        "application/json"
      },
      {
        ".js",
        "text/javascript"
      },
      {
        ".css",
        "text/css"
      },
      {
        ".xml",
        "text/xml"
      },
      {
        ".scss",
        "text/x-scss"
      },
      {
        ".sass",
        "text/x-sass"
      },
      {
        ".ts",
        "text/x-ts"
      },
      {
        ".config",
        "application/xml"
      },
      {
        ".cs",
        "text/plain"
      }
    };
  }
}
