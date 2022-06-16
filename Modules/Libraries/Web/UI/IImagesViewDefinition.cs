// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Libraries.Web.UI.IImagesViewDefinition
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

namespace Telerik.Sitefinity.Modules.Libraries.Web.UI
{
  /// <summary>
  /// An interface that provides all common properties to construct the actual images view.
  /// </summary>
  public interface IImagesViewDefinition
  {
    /// <summary>
    /// Gets or sets a value indicating whether previous and next links are enabled.
    /// </summary>
    bool? EnablePrevNextLinks { get; set; }

    /// <summary>
    /// Gets or sets a value indicating how the previous and the next links will be displayed - as text/thumbnails etc.
    /// </summary>
    PrevNextLinksDisplayMode PrevNextLinksDisplayMode { get; set; }
  }
}
