// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Fluent.ContentFluentApi.VideoPublicFacade`1
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.Libraries.Model;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.Modules.Libraries;

namespace Telerik.Sitefinity.Fluent.ContentFluentApi
{
  /// <summary>Manages the public state of a video</summary>
  /// <typeparam name="TParentFacade">The type of the parent facade.</typeparam>
  public class VideoPublicFacade<TParentFacade> : 
    MediaPublicFacade<VideoPublicFacade<TParentFacade>, TParentFacade, Video, VideoTempFacade<TParentFacade>>,
    ISupportComments<VideoPublicFacade<TParentFacade>, Video>,
    ISupportVersioning<VideoPublicFacade<TParentFacade>>
    where TParentFacade : BaseFacade, IHasPublicAndTempFacade<VideoPublicFacade<TParentFacade>, VideoTempFacade<TParentFacade>, TParentFacade, Video>
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Fluent.ContentFluentApi.VideoPublicFacade`1" /> class.
    /// </summary>
    /// <param name="settings">The fluent API settings to use for this facade</param>
    /// <exception cref="T:System.ArgumentNullException">When <paramref name="settings" /> is null</exception>
    /// <exception cref="T:System.ArgumentException">When <paramref name="settings" />'s transaction name is null or empty</exception>
    public VideoPublicFacade(AppSettings settings)
      : base(settings)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Fluent.ContentFluentApi.VideoPublicFacade`1" /> class.
    /// </summary>
    /// <param name="settings">The fluent API settings to use for this facade</param>
    /// <param name="parentFacade">The parent facade. Can be null</param>
    /// <exception cref="T:System.ArgumentNullException">When <paramref name="settings" /> is null</exception>
    /// <exception cref="T:System.ArgumentException">When <paramref name="settings" />'s transaction name is null or empty</exception>
    public VideoPublicFacade(AppSettings settings, TParentFacade parentFacade)
      : base(settings, parentFacade)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Fluent.ContentFluentApi.VideoPublicFacade`1" /> class.
    /// </summary>
    /// <param name="settings">The fluent API settings to use for this facade</param>
    /// <param name="parentFacade">The parent facade. Can be null</param>
    /// <param name="itemID">ID of the content item that is to be initial state of the facade.</param>
    /// <exception cref="T:System.ArgumentNullException">When <paramref name="settings" /> is null</exception>
    /// <exception cref="T:System.ArgumentException">When <paramref name="settings" />'s transaction name is null or empty</exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">When <paramref name="itemID" /> is empty Guid</exception>
    public VideoPublicFacade(AppSettings settings, TParentFacade parentFacade, Guid itemID)
      : base(settings, parentFacade, itemID)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Fluent.ContentFluentApi.VideoPublicFacade`1" /> class.
    /// </summary>
    /// <param name="settings">The fluent API settings to use for this facade</param>
    /// <param name="parentFacade">The parent facade. Can be null</param>
    /// <param name="item">Content item to be the initial state of the facade.</param>
    /// <exception cref="T:System.ArgumentNullException">When <paramref name="settings" /> or <paramref name="item" /> is null</exception>
    /// <exception cref="T:System.ArgumentException">When <paramref name="settings" />'s transaction name is null or empty</exception>
    public VideoPublicFacade(AppSettings settings, TParentFacade parentFacade, Video item)
      : base(settings, parentFacade, item)
    {
    }

    /// <summary>
    /// Create a new instance of the manager in a named transaction using <see cref="!:settings" />
    /// </summary>
    /// <returns>Instance of this facade's manager</returns>
    /// <remarks>This is called internally by <see cref="!:GetManager" />. Do not call this manually unless you override GetManager as well.</remarks>
    protected override IManager InitializeManager() => (IManager) LibrariesManager.GetManager(this.settings.ContentProviderName, this.settings.TransactionName);

    /// <summary>Get a facade that manages single comment entries</summary>
    /// <returns>Facade that manages single comments</returns>
    public virtual CommentsSingularFacade<VideoPublicFacade<TParentFacade>, Video> Comment() => new CommentsSingularFacade<VideoPublicFacade<TParentFacade>, Video>(this.settings, this, this.Get());

    /// <summary>Get a comment by ID</summary>
    /// <param name="id">ID of the comment to get</param>
    /// <returns>Facade that manages individual comments</returns>
    public virtual CommentsSingularFacade<VideoPublicFacade<TParentFacade>, Video> Comment(
      Guid id)
    {
      return new CommentsSingularFacade<VideoPublicFacade<TParentFacade>, Video>(this.settings, this, this.Get(), id);
    }

    /// <summary>Get a facade that manages this video's comments</summary>
    /// <returns>Facade that manages this video's comments</returns>
    public virtual CommentsPluralFacade<VideoPublicFacade<TParentFacade>, Video> Comments() => new CommentsPluralFacade<VideoPublicFacade<TParentFacade>, Video>(this.settings, this, this.Get());

    /// <summary>Manage this item's versions</summary>
    /// <returns>Versioning facade</returns>
    public virtual VersioningFacade<VideoPublicFacade<TParentFacade>> Versioning() => new VersioningFacade<VideoPublicFacade<TParentFacade>>(this.settings, this, (IDataItem) this.Get());
  }
}
