// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Libraries.Configuration.ImagesViewMasterElement
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
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
  /// The configuration element for ImagesViewMasterDefinition
  /// </summary>
  public class ImagesViewMasterElement : 
    MediaContentMasterElement,
    IImagesViewMasterDefinition,
    IMediaContentMasterDefinition,
    IContentViewMasterDefinition,
    IContentViewDefinition,
    IDefinition,
    IMediaContentDefinition,
    IImagesViewDefinition
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Modules.Libraries.Configuration.ImagesViewMasterElement" /> class.
    /// </summary>
    /// <param name="element">The element.</param>
    public ImagesViewMasterElement(ConfigElement element)
      : base(element)
    {
    }

    /// <summary>Gets the definition.</summary>
    /// <returns></returns>
    public override DefinitionBase GetDefinition() => (DefinitionBase) new ImagesViewMasterDefinition((ConfigElement) this);

    /// <summary>
    /// Gets or sets a value indicating whether previous and next links are enabled.
    /// </summary>
    /// <value></value>
    [ConfigurationProperty("enablePrevNextLinks", IsRequired = false)]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "EnablePrevNextLinksDescription", Title = "EnablePrevNextLinksCaption")]
    public bool? EnablePrevNextLinks
    {
      get => (bool?) this["enablePrevNextLinks"];
      set => this["enablePrevNextLinks"] = (object) value;
    }

    /// <summary>
    /// Gets or sets a value indicating how the previous and the next links will be displayed - as text/thumbnails etc.
    /// </summary>
    /// <value></value>
    [ConfigurationProperty("prevNextLinksDisplayMode", DefaultValue = PrevNextLinksDisplayMode.Text, IsRequired = false)]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "PrevNextLinksDisplayModeDescription", Title = "PrevNextLinksDisplayModeCaption")]
    public PrevNextLinksDisplayMode PrevNextLinksDisplayMode
    {
      get => (PrevNextLinksDisplayMode) this["prevNextLinksDisplayMode"];
      set => this["prevNextLinksDisplayMode"] = (object) value;
    }

    /// <summary>
    /// Gets or sets the height of the canvas in thumbnail strip view
    /// </summary>
    /// <value>The height of the canvas.</value>
    [ConfigurationProperty("canvasHeight", IsRequired = false)]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "CanvasHeightDescription", Title = "CanvasHeightCaption")]
    public int? CanvasHeight
    {
      get => (int?) this["canvasHeight"];
      set => this["canvasHeight"] = (object) value;
    }

    /// <summary>Height of the thumbnails</summary>
    [ConfigurationProperty("thumbnailsHeight", IsRequired = false)]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "ThumbnailsHeightDescription", Title = "ThumbnailsHeightCaption")]
    [Obsolete("Use ThumbnailsName to define the thumbnails settings(profile/size) you want to use.")]
    public int? ThumbnailsHeight
    {
      get => (int?) this["thumbnailsHeight"];
      set => this["thumbnailsHeight"] = (object) value;
    }

    /// <summary>
    /// Constants to hold the string keys for configuration properties of ImagesViewMasterElement
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Size = 1)]
    internal struct ImagesViewMasterProps
    {
      public const string EnablePrevNextLinks = "enablePrevNextLinks";
      public const string PrevNextLinksDisplayMode = "prevNextLinksDisplayMode";
      public const string CanvasHeight = "canvasHeight";
      public const string ThumbnailsHeight = "thumbnailsHeight";
    }
  }
}
