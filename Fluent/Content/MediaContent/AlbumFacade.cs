// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Fluent.Content.AlbumFacade
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using Telerik.Sitefinity.Libraries.Model;
using Telerik.Sitefinity.Modules.Libraries;

namespace Telerik.Sitefinity.Fluent.Content
{
  [Obsolete]
  public class AlbumFacade : ContentFacade<AlbumFacade, Album, AlbumFacade>
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Fluent.Content.AlbumFacade" /> class.
    /// </summary>
    /// <param name="appSettings">
    /// The app settings that configure the way fluent API will function.
    /// </param>
    public AlbumFacade(AppSettings appSettings)
      : base(appSettings)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Fluent.Content.AlbumFacade" /> class.
    /// </summary>
    /// <param name="appSettings">The app settings that configure the way fluent API will function.</param>
    /// <param name="itemId">The album id.</param>
    public AlbumFacade(AppSettings appSettings, Guid itemId)
      : base(appSettings, itemId)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Fluent.Content.AlbumFacade" /> class.
    /// </summary>
    /// <param name="appSettings">The app settings that configure the way fluent API will function.</param>
    /// <param name="parentFacade">The parent facade.</param>
    public AlbumFacade(AppSettings appSettings, AlbumFacade parentFacade)
      : base(appSettings, parentFacade)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Fluent.Content.AlbumFacade" /> class.
    /// </summary>
    /// <param name="appSettings">The app settings that configure the way fluent API will function.</param>
    /// <param name="itemId">The album id.</param>
    /// <param name="parentFacade">The parent facade.</param>
    public AlbumFacade(AppSettings appSettings, Guid itemId, AlbumFacade parentFacade)
      : base(appSettings, itemId, parentFacade)
    {
    }

    /// <summary>
    /// Creates a facade of the images in the album with a parent facade the current facade.
    /// </summary>
    /// <returns>
    /// The child facade of type <see cref="T:Telerik.Sitefinity.Fluent.Content.ImagesFacade" />.
    /// </returns>
    public ImagesFacade Images()
    {
      this.EnsureExistence(true);
      return new ImagesFacade(this.AppSettings, this.ContentItem.Images(), this);
    }

    /// <summary>
    /// Creates a facade of a image in the album with a parent facade the current facade.
    /// </summary>
    /// <returns>
    /// The child facade of type <see cref="T:Telerik.Sitefinity.Fluent.Content.ImageFacade" />.
    /// </returns>
    public ImageFacade Image() => new ImageFacade(this.AppSettings, this);

    /// <summary>
    /// Creates a facade of the given image in the album with a parent facade the current facade.
    /// </summary>
    /// <param name="itemId">The id of the image.</param>
    /// <returns>
    /// The child facade of type <see cref="T:Telerik.Sitefinity.Fluent.Content.ImageFacade" />.
    /// </returns>
    public ImageFacade Image(Guid itemId) => new ImageFacade(this.AppSettings, itemId, this);
  }
}
