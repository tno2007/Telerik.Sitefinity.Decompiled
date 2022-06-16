// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Libraries.Web.UI.ImagesViewDetailDefinition
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Web.UI.ContentUI.Contracts;
using Telerik.Sitefinity.Web.UI.Definitions;

namespace Telerik.Sitefinity.Modules.Libraries.Web.UI
{
  /// <summary>
  /// A definition class containing all information needed to construct an instance of the respective detail view control.
  /// </summary>
  public class ImagesViewDetailDefinition : 
    MediaContentDetailDefinition,
    IImagesViewDetailDefinition,
    IMediaContentDetailDefinition,
    IContentViewDetailDefinition,
    IContentViewDefinition,
    IDefinition,
    IMediaContentDefinition,
    IImagesViewDefinition
  {
    private bool? enablePrevNextLinks;
    private PrevNextLinksDisplayMode prevNextLinksDisplayMode;

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Modules.Libraries.Web.UI.ImagesViewDetailDefinition" /> class.
    /// </summary>
    public ImagesViewDetailDefinition()
      : base((ConfigElement) null)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Modules.Libraries.Web.UI.ImagesViewDetailDefinition" /> class.
    /// </summary>
    /// <param name="configDefinition">The config definition.</param>
    public ImagesViewDetailDefinition(ConfigElement configDefinition)
      : base(configDefinition)
    {
    }

    /// <summary>Gets the definition.</summary>
    /// <returns></returns>
    public ImagesViewDetailDefinition GetDefinition() => this;

    /// <summary>
    /// Gets or sets a value indicating whether previous and next links are enabled.
    /// </summary>
    /// <value>
    /// 	<c>true</c> if previous and next links are enabled; otherwise, <c>false</c>.
    /// </value>
    public bool? EnablePrevNextLinks
    {
      get => this.ResolveProperty<bool?>(nameof (EnablePrevNextLinks), this.enablePrevNextLinks);
      set => this.enablePrevNextLinks = value;
    }

    /// <summary>
    /// Gets or sets a value indicating how the previous and the next links will be displayed - as text/thumbnails etc.
    /// </summary>
    /// <value>
    /// The value indicating how the previous and the next links will be displayed.
    /// </value>
    public PrevNextLinksDisplayMode PrevNextLinksDisplayMode
    {
      get => this.ResolveProperty<PrevNextLinksDisplayMode>(nameof (PrevNextLinksDisplayMode), this.prevNextLinksDisplayMode);
      set => this.prevNextLinksDisplayMode = value;
    }
  }
}
