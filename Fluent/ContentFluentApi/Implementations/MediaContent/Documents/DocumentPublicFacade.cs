// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Fluent.ContentFluentApi.DocumentPublicFacade`1
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
  /// <summary>Manages the public state of a document</summary>
  /// <typeparam name="TParentFacade">Type of the parent facade</typeparam>
  public class DocumentPublicFacade<TParentFacade> : 
    MediaPublicFacade<DocumentPublicFacade<TParentFacade>, TParentFacade, Document, DocumentTempFacade<TParentFacade>>,
    ISupportComments<DocumentPublicFacade<TParentFacade>, Document>,
    ISupportVersioning<DocumentPublicFacade<TParentFacade>>
    where TParentFacade : BaseFacade, IHasPublicAndTempFacade<DocumentPublicFacade<TParentFacade>, DocumentTempFacade<TParentFacade>, TParentFacade, Document>
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Fluent.ContentFluentApi.DocumentPublicFacade`1" /> class.
    /// </summary>
    /// <param name="settings">The fluent API settings to use for this facade</param>
    /// <exception cref="T:System.ArgumentNullException">When <paramref name="settings" /> is null</exception>
    /// <exception cref="T:System.ArgumentException">When <paramref name="settings" />'s transaction name is null or empty</exception>
    public DocumentPublicFacade(AppSettings settings)
      : base(settings)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Fluent.ContentFluentApi.DocumentPublicFacade`1" /> class.
    /// </summary>
    /// <param name="settings">The fluent API settings to use for this facade</param>
    /// <param name="parentFacade">The parent facade. Can be null</param>
    /// <exception cref="T:System.ArgumentNullException">When <paramref name="settings" /> is null</exception>
    /// <exception cref="T:System.ArgumentException">When <paramref name="settings" />'s transaction name is null or empty</exception>
    public DocumentPublicFacade(AppSettings settings, TParentFacade parentFacade)
      : base(settings, parentFacade)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Fluent.ContentFluentApi.DocumentPublicFacade`1" /> class.
    /// </summary>
    /// <param name="settings">The fluent API settings to use for this facade</param>
    /// <param name="parentFacade">The parent facade. Can be null</param>
    /// <param name="itemID">ID of the content item that is to be initial state of the facade.</param>
    /// <exception cref="T:System.ArgumentNullException">When <paramref name="settings" /> is null</exception>
    /// <exception cref="T:System.ArgumentException">When <paramref name="settings" />'s transaction name is null or empty</exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">When <paramref name="itemID" /> is empty Guid</exception>
    public DocumentPublicFacade(AppSettings settings, TParentFacade parentFacade, Guid itemID)
      : base(settings, parentFacade, itemID)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Fluent.ContentFluentApi.DocumentPublicFacade`1" /> class.
    /// </summary>
    /// <param name="settings">The fluent API settings to use for this facade</param>
    /// <param name="parentFacade">The parent facade. Can be null</param>
    /// <param name="item">Content item to be the initial state of the facade.</param>
    /// <exception cref="T:System.ArgumentNullException">When <paramref name="settings" /> or <paramref name="item" /> is null</exception>
    /// <exception cref="T:System.ArgumentException">When <paramref name="settings" />'s transaction name is null or empty</exception>
    public DocumentPublicFacade(AppSettings settings, TParentFacade parentFacade, Document item)
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
    public virtual CommentsSingularFacade<DocumentPublicFacade<TParentFacade>, Document> Comment() => new CommentsSingularFacade<DocumentPublicFacade<TParentFacade>, Document>(this.settings, this, this.Get());

    /// <summary>Get a comment by ID</summary>
    /// <param name="id">ID of the comment to get</param>
    /// <returns>Facade that manages individual comments</returns>
    public virtual CommentsSingularFacade<DocumentPublicFacade<TParentFacade>, Document> Comment(
      Guid id)
    {
      return new CommentsSingularFacade<DocumentPublicFacade<TParentFacade>, Document>(this.settings, this, this.Get(), id);
    }

    /// <summary>Get a facade that manages this document's comments</summary>
    /// <returns>Facade that manages this document's comments</returns>
    public virtual CommentsPluralFacade<DocumentPublicFacade<TParentFacade>, Document> Comments() => new CommentsPluralFacade<DocumentPublicFacade<TParentFacade>, Document>(this.settings, this, this.Get());

    /// <summary>Manage this item's versions</summary>
    /// <returns>Versioning facade</returns>
    public virtual VersioningFacade<DocumentPublicFacade<TParentFacade>> Versioning() => new VersioningFacade<DocumentPublicFacade<TParentFacade>>(this.settings, this, (IDataItem) this.Get());
  }
}
