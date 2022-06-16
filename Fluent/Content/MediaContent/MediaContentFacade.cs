// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Fluent.Content.MediaContent.MediaContentFacade`3
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.IO;
using Telerik.Sitefinity.Modules.Libraries;

namespace Telerik.Sitefinity.Fluent.Content.MediaContent
{
  [Obsolete]
  public class MediaContentFacade<TCurrentFacade, TContent, TParentFacade> : 
    ContentFacade<TCurrentFacade, TContent, TParentFacade>
    where TCurrentFacade : class
    where TContent : Telerik.Sitefinity.Libraries.Model.MediaContent
    where TParentFacade : class
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Fluent.Content.ImageFacade" /> class.
    /// </summary>
    /// <param name="appSettings">
    /// The app settings that configure the way fluent API will function.
    /// </param>
    public MediaContentFacade(AppSettings appSettings)
      : base(appSettings)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Fluent.Content.ImageFacade" /> class.
    /// </summary>
    /// <param name="appSettings">The app settings that configure the way fluent API will function.</param>
    /// <param name="itemId">The image id.</param>
    public MediaContentFacade(AppSettings appSettings, Guid itemId)
      : base(appSettings, itemId)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Fluent.Content.ImageFacade" /> class.
    /// </summary>
    /// <param name="appSettings">The app settings that configure the way fluent API will function.</param>
    /// <param name="parentFacade">The parent facade.</param>
    public MediaContentFacade(AppSettings appSettings, TParentFacade parentFacade)
      : base(appSettings, parentFacade)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Fluent.Content.ImageFacade" /> class.
    /// </summary>
    /// <param name="appSettings">The app settings that configure the way fluent API will function.</param>
    /// <param name="itemId">The image id.</param>
    /// <param name="parentFacade">The parent facade.</param>
    public MediaContentFacade(AppSettings appSettings, Guid itemId, TParentFacade parentFacade)
      : base(appSettings, itemId, parentFacade)
    {
    }

    /// <summary>Uploads the specified content.</summary>
    /// <param name="source">The source.</param>
    /// <param name="extension">The extension.</param>
    public virtual TCurrentFacade UploadMedia(Stream source, string extension)
    {
      this.EnsureExistence(true);
      if (this.ContentItem.Parent == null)
        throw new InvalidOperationException("the item must be into a library.");
      ((LibrariesManager) this.ContentManager).Upload((Telerik.Sitefinity.Libraries.Model.MediaContent) this.ContentItem, source, extension);
      return this as TCurrentFacade;
    }

    /// <summary>Downloads the specified source.</summary>
    /// <param name="source">The source.</param>
    /// <returns></returns>
    public virtual TCurrentFacade Download(out Stream source)
    {
      this.EnsureExistence(true);
      source = ((LibrariesManager) this.ContentManager).Download((Telerik.Sitefinity.Libraries.Model.MediaContent) this.ContentItem);
      return this as TCurrentFacade;
    }
  }
}
