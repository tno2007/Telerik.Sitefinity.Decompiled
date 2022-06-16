// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Libraries.Configuration.VideosViewMasterLightBoxElement
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Configuration;
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
  /// The configuration element for VideosViewMasterLightBoxDefinition
  /// </summary>
  public class VideosViewMasterLightBoxElement : 
    VideosViewMasterElement,
    IVideosViewMasterLightBoxDefinition,
    IVideosViewMasterDefinition,
    IMediaContentMasterDefinition,
    IContentViewMasterDefinition,
    IContentViewDefinition,
    IDefinition,
    IMediaContentDefinition
  {
    private const string AutoPlayPropName = "autoPlay";

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Modules.Libraries.Configuration.VideosViewMasterLightBoxElement" /> class.
    /// </summary>
    /// <param name="element">The element.</param>
    public VideosViewMasterLightBoxElement(ConfigElement element)
      : base(element)
    {
    }

    /// <summary>Gets the definition.</summary>
    /// <returns>The definition.</returns>
    public override DefinitionBase GetDefinition() => (DefinitionBase) new VideosViewMasterLightBoxDefinition((ConfigElement) this);

    /// <summary>
    /// Gets or sets a value indicating whether the video will be auto played.
    /// </summary>
    [ConfigurationProperty("autoPlay", DefaultValue = false, IsRequired = false)]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "AutoPlayDescription", Title = "AutoPlayTitle")]
    public bool? AutoPlay
    {
      get => (bool?) this["autoPlay"];
      set => this["autoPlay"] = (object) value;
    }
  }
}
