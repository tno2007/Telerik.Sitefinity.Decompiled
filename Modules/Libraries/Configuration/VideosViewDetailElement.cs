// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Libraries.Configuration.VideosViewDetailElement
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Configuration;
using System.Runtime.InteropServices;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Modules.Libraries.Web.UI;
using Telerik.Sitefinity.Web.Configuration;
using Telerik.Sitefinity.Web.UI;
using Telerik.Sitefinity.Web.UI.ContentUI.Contracts;
using Telerik.Sitefinity.Web.UI.Definitions;

namespace Telerik.Sitefinity.Modules.Libraries.Configuration
{
  /// <summary>
  /// The configuration element for VideosViewDetailDefinition
  /// </summary>
  public class VideosViewDetailElement : 
    MediaContentDetailElement,
    IVideosViewDetailDefinition,
    IMediaContentDetailDefinition,
    IContentViewDetailDefinition,
    IContentViewDefinition,
    IDefinition,
    IMediaContentDefinition
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Modules.Libraries.Configuration.VideosViewDetailElement" /> class.
    /// </summary>
    /// <param name="element">The element.</param>
    public VideosViewDetailElement(ConfigElement element)
      : base(element)
    {
    }

    /// <summary>Gets the definition.</summary>
    /// <returns></returns>
    public override DefinitionBase GetDefinition() => (DefinitionBase) new VideosViewDetailDefinition((ConfigElement) this);

    /// <summary>
    /// Gets or sets a value indicating whether the option for embedding will be shown.
    /// </summary>
    [ConfigurationProperty("showEmbeddingOption", IsRequired = false)]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "ShowEmbeddingOptionDescription", Title = "ShowEmbeddingOptionCaption")]
    public bool? ShowEmbeddingOption
    {
      get => (bool?) this["showEmbeddingOption"];
      set => this["showEmbeddingOption"] = (object) value;
    }

    /// <summary>
    /// Gets or sets a value indicating whether the related videos will be shown.
    /// </summary>
    [ConfigurationProperty("showRelatedVideos", IsRequired = false)]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "ShowRelatedVideosDescription", Title = "ShowRelatedVideosCaption")]
    public bool? ShowRelatedVideos
    {
      get => (bool?) this["showRelatedVideos"];
      set => this["showRelatedVideos"] = (object) value;
    }

    /// <summary>
    /// Gets or sets a value indicating whether the full size of the video player is allowed.
    /// </summary>
    [ConfigurationProperty("allowFullSize", DefaultValue = true, IsRequired = false)]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "AllowFullSizeDescription", Title = "AllowFullSizeCaption")]
    public bool? AllowFullSize
    {
      get => (bool?) this["allowFullSize"];
      set => this["allowFullSize"] = (object) value;
    }

    /// <summary>
    /// Gets or sets a value indicating whether the video will be playe in full screen mode.
    /// </summary>
    [ConfigurationProperty("fullScreen", DefaultValue = false, IsRequired = false)]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "FullScreenDescription", Title = "FullScreenCaption")]
    public bool? FullScreen
    {
      get => (bool?) this["fullScreen"];
      set => this["fullScreen"] = (object) value;
    }

    /// <summary>
    /// Gets or sets a value indicating whether the video will be auto played.
    /// </summary>
    [ConfigurationProperty("autoPlay", DefaultValue = false, IsRequired = false)]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "AutoPlayDescription", Title = "AutoPlayCaption")]
    public bool? AutoPlay
    {
      get => (bool?) this["autoPlay"];
      set => this["autoPlay"] = (object) value;
    }

    /// <summary>Gets or sets the start volume of the video player.</summary>
    [ConfigurationProperty("startVolume", DefaultValue = 50, IsRequired = false)]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "StartVolumeDescription", Title = "StartVolumeCaption")]
    public int? StartVolume
    {
      get => (int?) this["startVolume"];
      set => this["startVolume"] = (object) value;
    }

    /// <summary>Gets or sets the start time of the video player.</summary>
    [ConfigurationProperty("startTime", DefaultValue = 0, IsRequired = false)]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "StartTimeDescription", Title = "StartTimeeCaption")]
    public int? StartTime
    {
      get => (int?) this["startTime"];
      set => this["startTime"] = (object) value;
    }

    /// <summary>
    /// Constants to hold the string keys for configuration properties of VideosViewDetailElement
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Size = 1)]
    internal struct VideosViewDetailProps
    {
      public const string ShowEmbeddingOption = "showEmbeddingOption";
      public const string ShowRelatedVideos = "showRelatedVideos";
      public const string AllowFullSize = "allowFullSize";
      public const string FullScreen = "fullScreen";
      public const string AutoPlay = "autoPlay";
      public const string StartVolume = "startVolume";
      public const string StartTime = "startTime";
    }
  }
}
