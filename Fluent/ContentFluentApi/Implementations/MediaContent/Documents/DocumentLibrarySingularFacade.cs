// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Fluent.ContentFluentApi.DocumentLibrarySingularFacade`1
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Linq;
using System.Linq.Expressions;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.Libraries.Model;
using Telerik.Sitefinity.Modules.Libraries;

namespace Telerik.Sitefinity.Fluent.ContentFluentApi
{
  /// <summary>Manages document libraries</summary>
  /// <typeparam name="TParentFacade">Type of the parent facade</typeparam>
  public class DocumentLibrarySingularFacade<TParentFacade> : 
    BaseContentSingularFacadeWithoutLifeCycle<DocumentLibrarySingularFacade<TParentFacade>, TParentFacade, DocumentLibrary>
    where TParentFacade : BaseFacade
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Fluent.ContentFluentApi.DocumentLibrarySingularFacade`1" /> class.
    /// </summary>
    /// <param name="settings">The fluent API settings to use for this facade</param>
    /// <exception cref="T:System.ArgumentNullException">When <paramref name="settings" /> is null</exception>
    /// <exception cref="T:System.ArgumentException">When <paramref name="settings" />'s transaction name is null or empty</exception>
    public DocumentLibrarySingularFacade(AppSettings settings)
      : base(settings)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Fluent.ContentFluentApi.DocumentLibrarySingularFacade`1" /> class.
    /// </summary>
    /// <param name="settings">The fluent API settings to use for this facade</param>
    /// <param name="parentFacade">The parent facade. Can be null</param>
    /// <exception cref="T:System.ArgumentNullException">When <paramref name="settings" /> is null</exception>
    /// <exception cref="T:System.ArgumentException">When <paramref name="settings" />'s transaction name is null or empty</exception>
    public DocumentLibrarySingularFacade(AppSettings settings, TParentFacade parentFacade)
      : base(settings, parentFacade)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Fluent.ContentFluentApi.DocumentLibrarySingularFacade`1" /> class.
    /// </summary>
    /// <param name="settings">The fluent API settings to use for this facade</param>
    /// <param name="parentFacade">The parent facade. Can be null</param>
    /// <param name="itemID">ID of the content item that is to be initial state of the facade.</param>
    /// <exception cref="T:System.ArgumentNullException">When <paramref name="settings" /> is null</exception>
    /// <exception cref="T:System.ArgumentException">When <paramref name="settings" />'s transaction name is null or empty</exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">When itemId is emtpy Guid</exception>
    public DocumentLibrarySingularFacade(
      AppSettings settings,
      TParentFacade parentFacade,
      Guid itemID)
      : base(settings, parentFacade, itemID)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Fluent.ContentFluentApi.DocumentLibrarySingularFacade`1" /> class.
    /// </summary>
    /// <param name="settings">The fluent API settings to use for this facade</param>
    /// <param name="parentFacade">The parent facade. Can be null</param>
    /// <param name="item">Content item to be the initial state of the facade.</param>
    /// <exception cref="T:System.ArgumentNullException">When <paramref name="settings" /> or <paramref name="item" /> is null</exception>
    /// <exception cref="T:System.ArgumentException">When <paramref name="settings" />'s transaction name is null or empty</exception>
    public DocumentLibrarySingularFacade(
      AppSettings settings,
      TParentFacade parentFacade,
      DocumentLibrary item)
      : base(settings, parentFacade, item)
    {
    }

    /// <summary>
    /// Create a new instance of the manager in a named transaction using <see cref="!:settings" />
    /// </summary>
    /// <returns>Instance of this facade's manager</returns>
    /// <remarks>This is called internally by <see cref="!:GetManager" />. Do not call this manually unless you override GetManager as well.</remarks>
    protected override IManager InitializeManager() => (IManager) LibrariesManager.GetManager(this.settings.ContentProviderName, this.settings.TransactionName);

    /// <summary>Manage documents in this library</summary>
    /// <returns>Facade that can manage this library's documents</returns>
    public DocumentsPluralFacade<DocumentLibrarySingularFacade<TParentFacade>> Documents()
    {
      Guid itemId = this.Get().Id;
      return new DocumentsPluralFacade<DocumentLibrarySingularFacade<TParentFacade>>(this.settings, this, this.GetManager().GetItems<Document>().Where<Document>((Expression<Func<Document, bool>>) (item => item.Parent.Id == itemId)));
    }

    /// <summary>Create a document in this library</summary>
    /// <returns>Facade that can manage a document in this library</returns>
    public DocumentDraftFacade<DocumentLibrarySingularFacade<TParentFacade>> CreateDocument() => new DocumentDraftFacade<DocumentLibrarySingularFacade<TParentFacade>>(this.settings, this).CreateNew().Do((Action<Document>) (item => item.Parent = (Library) this.Get()));
  }
}
