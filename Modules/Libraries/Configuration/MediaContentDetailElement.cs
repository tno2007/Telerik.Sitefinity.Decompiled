// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Libraries.Configuration.MediaContentDetailElement
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
using Telerik.Sitefinity.Web.UI.ContentUI.Config;
using Telerik.Sitefinity.Web.UI.ContentUI.Contracts;
using Telerik.Sitefinity.Web.UI.Definitions;

namespace Telerik.Sitefinity.Modules.Libraries.Configuration
{
  /// <summary>
  /// The configuration element for MediaContentDetailDefinition
  /// </summary>
  public class MediaContentDetailElement : 
    ContentViewDetailElement,
    IMediaContentDetailDefinition,
    IContentViewDetailDefinition,
    IContentViewDefinition,
    IDefinition,
    IMediaContentDefinition
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Modules.Libraries.Configuration.MediaContentDetailElement" /> class.
    /// </summary>
    /// <param name="element">The element.</param>
    public MediaContentDetailElement(ConfigElement element)
      : base(element)
    {
    }

    /// <summary>Gets the definition.</summary>
    /// <returns></returns>
    public override DefinitionBase GetDefinition() => (DefinitionBase) new MediaContentDetailDefinition((ConfigElement) this);

    /// <summary>
    /// Gets or sets the width of the single item. The single item could be an image or video.
    /// </summary>
    /// <value></value>
    [ConfigurationProperty("singleItemWidth", IsRequired = false)]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "SingleItemWidthDescription", Title = "SingleItemWidthCaption")]
    [Obsolete("Use SingleItemThumbnailsName to define the thumbnails settings(profile/size) you want to use.")]
    public int? SingleItemWidth
    {
      get => (int?) this["singleItemWidth"];
      set => this["singleItemWidth"] = (object) value;
    }

    /// <summary>Gets or sets the name of the single item thumbnails.</summary>
    /// <value>The name of the single item thumbnails.</value>
    [ConfigurationProperty("singleItemThumbnailsName", IsRequired = false)]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "SingleItemThumbnailsNameDescription", Title = "SingleItemThumbnailsNameCaption")]
    public string SingleItemThumbnailsName
    {
      get => (string) this["singleItemThumbnailsName"];
      set => this["singleItemThumbnailsName"] = (object) value;
    }

    /// <summary>
    /// Constants to hold the string keys for configuration properties of MediaContentDetailElement
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Size = 1)]
    internal struct MediaContentDetailProps
    {
      public const string SingleItemWidth = "singleItemWidth";
      public const string SingleItemThumbnailsName = "singleItemThumbnailsName";
    }
  }
}
