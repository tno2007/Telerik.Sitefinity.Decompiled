// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Libraries.Configuration.ImagesViewDetailElement
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
  /// The configuration element for ImagesViewDetailDefinition
  /// </summary>
  public class ImagesViewDetailElement : 
    MediaContentDetailElement,
    IImagesViewDetailDefinition,
    IMediaContentDetailDefinition,
    IContentViewDetailDefinition,
    IContentViewDefinition,
    IDefinition,
    IMediaContentDefinition,
    IImagesViewDefinition
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Modules.Libraries.Configuration.ImagesViewDetailElement" /> class.
    /// </summary>
    /// <param name="element">The element.</param>
    public ImagesViewDetailElement(ConfigElement element)
      : base(element)
    {
    }

    /// <summary>Gets the definition.</summary>
    /// <returns></returns>
    public override DefinitionBase GetDefinition() => (DefinitionBase) new ImagesViewDetailDefinition((ConfigElement) this);

    /// <summary>
    /// Gets or sets a value indicating whether previous and next links are enabled.
    /// </summary>
    /// <value>
    /// 	<c>true</c> if previous and next links are enabled; otherwise, <c>false</c>.
    /// </value>
    [ConfigurationProperty("enablePrevNextLinks", DefaultValue = false, IsRequired = false)]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "EnablePrevNextLinksDescription", Title = "EnablePrevNextLinksCaption")]
    public bool? EnablePrevNextLinks
    {
      get => (bool?) this["enablePrevNextLinks"];
      set => this["enablePrevNextLinks"] = (object) value;
    }

    /// <summary>Gets or sets the type of the previous and next links.</summary>
    /// <value>The type of the previous and next links.</value>
    [ConfigurationProperty("prevNextLinksDisplayMode", DefaultValue = PrevNextLinksDisplayMode.Text, IsRequired = false)]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "PrevNextLinksDisplayModeDescription", Title = "PrevNextLinksDisplayModeCaption")]
    public PrevNextLinksDisplayMode PrevNextLinksDisplayMode
    {
      get => (PrevNextLinksDisplayMode) this["prevNextLinksDisplayMode"];
      set => this["prevNextLinksDisplayMode"] = (object) value;
    }

    /// <summary>
    /// Constants to hold the string keys for configuration properties of ImagesViewDetailElement
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Size = 1)]
    internal struct ImagesViewDetailProps
    {
      public const string EnablePrevNextLinks = "enablePrevNextLinks";
      public const string PrevNextLinksDisplayMode = "prevNextLinksDisplayMode";
    }
  }
}
