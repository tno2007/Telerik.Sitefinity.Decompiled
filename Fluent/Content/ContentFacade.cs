// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Fluent.Content.ContentFacade`3
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Globalization;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.Fluent.Versioning;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.Modules.GenericContent;

namespace Telerik.Sitefinity.Fluent.Content
{
  [Obsolete]
  public class ContentFacade<TCurrentFacade, TContent, TParentFacade> : 
    IItemFacade<TCurrentFacade, TContent>,
    IFacade<TCurrentFacade>
    where TCurrentFacade : class
    where TContent : Telerik.Sitefinity.GenericContent.Model.Content
    where TParentFacade : class
  {
    private IContentManager contentManager;
    private AppSettings appSettings;
    private TContent item;
    private TParentFacade parentFacade;
    private Guid itemId;

    public ContentFacade()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="!:ContentFacade" /> class.
    /// </summary>
    /// <param name="appSettings">
    /// The app settings that configure the way fluent API will function.
    /// </param>
    public ContentFacade(AppSettings appSettings)
    {
      if (appSettings == null)
        throw new ArgumentNullException(nameof (appSettings));
      if (string.IsNullOrEmpty(appSettings.TransactionName))
        appSettings.TransactionName = Guid.NewGuid().ToString();
      this.appSettings = appSettings;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="!:ContentFacade" /> class.
    /// </summary>
    /// <param name="appSettings">The app settings that configure the way fluent API will function.</param>
    /// <param name="itemId">The content id.</param>
    public ContentFacade(AppSettings appSettings, Guid itemId)
      : this(appSettings)
    {
      this.itemId = !(itemId == Guid.Empty) ? itemId : throw new ArgumentNullException("pageId");
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="!:ContentFacade" /> class.
    /// </summary>
    /// <param name="appSettings">The app settings that configure the way fluent API will function.</param>
    /// <param name="parentFacade">The parent facade.</param>
    public ContentFacade(AppSettings appSettings, TParentFacade parentFacade)
      : this(appSettings)
    {
      this.parentFacade = (object) parentFacade != null ? parentFacade : throw new ArgumentNullException(nameof (parentFacade));
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="!:ContentFacade" /> class.
    /// </summary>
    /// <param name="appSettings">The app settings that configure the way fluent API will function.</param>
    /// <param name="itemId">The content id.</param>
    /// <param name="parentFacade">The parent facade.</param>
    public ContentFacade(AppSettings appSettings, Guid itemId, TParentFacade parentFacade)
      : this(appSettings, itemId)
    {
      this.parentFacade = (object) parentFacade != null ? parentFacade : throw new ArgumentNullException(nameof (parentFacade));
    }

    /// <summary>Creates a new instance of the content type.</summary>
    /// <returns>An instance of the current facade type.</returns>
    public TCurrentFacade CreateNew()
    {
      this.ContentItem = this.ContentManager.CreateItem(typeof (TContent)) as TContent;
      return this.CurrentFacade();
    }

    /// <summary>
    /// Creates a new instance of the content type with the specified id.
    /// </summary>
    /// <param name="itemId">The content id.</param>
    /// <returns>An instance of the current facade type.</returns>
    public TCurrentFacade CreateNew(Guid itemId)
    {
      this.ContentItem = this.ContentManager.CreateItem(typeof (TContent), itemId) as TContent;
      return this.CurrentFacade();
    }

    /// <summary>
    /// Returns the instance of the content type currently loaded by the fluent API.
    /// </summary>
    /// <returns>An instance of the content type.</returns>
    public TContent Get()
    {
      this.EnsureExistence(true);
      return this.ContentItem;
    }

    /// <summary>
    /// Sets an instance of the content type to currently loaded fluent API.
    /// </summary>
    /// <returns>An instance of the current facade type.</returns>
    public TCurrentFacade Set(TContent item)
    {
      this.ContentItem = item;
      return this.CurrentFacade();
    }

    /// <summary>Performs an arbitrary action on the content object.</summary>
    /// <param name="setAction">An action to be performed on the content object.</param>
    /// <exception cref="T:System.InvalidOperationException">
    /// thrown if the content object has not been initialized either through constructor, CreateNew() or Set() method.
    /// </exception>
    /// <returns>An instance of the current facade type.</returns>
    public TCurrentFacade Do(Action<TContent> setAction)
    {
      this.EnsureExistence(true);
      setAction(this.ContentItem);
      CommonMethods.RecompileItemUrls((Telerik.Sitefinity.GenericContent.Model.Content) this.ContentItem, (IManager) this.ContentManager);
      return this.CurrentFacade();
    }

    /// <summary>
    /// Saves all the changes that were performed through the fluent API operations.
    /// </summary>
    /// <remarks>
    /// This method needs to be used if you have configured the fluent API not to auto-commit. By default
    /// fluent API will auto-commit all operations as they are called. Use this method when you wish to
    /// work in transactions.
    /// </remarks>
    /// <returns>An instance of the current facade type.</returns>
    public TCurrentFacade SaveChanges()
    {
      TransactionManager.CommitTransaction(this.ContentManager.TransactionName);
      return this.CurrentFacade();
    }

    /// <summary>
    /// Cancels all the changes that were performed through the fluent API operations.
    /// </summary>
    /// <remarks>
    /// This method needs to be used if you have configured the fluent API not to auto-commit. By default
    /// fluent API will auto-commit all operations as they are called. Use this method when you wish to
    /// work in transactions.
    /// </remarks>
    /// <returns>An instance of the current facade type.</returns>
    public TCurrentFacade CancelChanges()
    {
      TransactionManager.RollbackTransaction(this.ContentManager.TransactionName);
      return this.CurrentFacade();
    }

    /// <summary>
    /// Deletes the instance of the content type currently loaded by the fluent API.
    /// </summary>
    /// <exception cref="T:System.InvalidOperationException">
    /// thrown if the object has not been initialized either through constructor, CreateNew() or Set() method.
    /// </exception>
    /// <returns>An instance of the current facade type.</returns>
    public TCurrentFacade Delete()
    {
      this.EnsureExistence(true);
      this.ContentManager.DeleteItem((object) this.ContentItem);
      return this.CurrentFacade();
    }

    /// <summary>Deletes the specified language.</summary>
    /// <param name="language">The language.</param>
    /// <returns></returns>
    public TCurrentFacade Delete(CultureInfo language)
    {
      this.EnsureExistence(true);
      this.ContentManager.DeleteItem((object) this.ContentItem, language);
      return this.CurrentFacade();
    }

    /// <summary>
    /// Initializes and returns a new child facade of type <see cref="!:CommentFacade" /> with a parent facade the current facade.
    /// </summary>
    /// <returns>
    /// The initialized child facade of type <see cref="!:CommentFacade" />.
    /// </returns>
    public virtual CommentFacade<TCurrentFacade> Comment()
    {
      this.EnsureExistence(true);
      return new CommentFacade<TCurrentFacade>(this.AppSettings, (Telerik.Sitefinity.GenericContent.Model.Content) this.ContentItem, this as TCurrentFacade, this.ContentManager as ICommentsManager);
    }

    /// <summary>
    /// Initializes and returns a new child facade of type <see cref="!:CommentFacade" /> with loaded a <see cref="M:Telerik.Sitefinity.Fluent.Content.ContentFacade`3.Comment" /> object
    /// with the specified id and with a parent facade the current facade.
    /// </summary>
    /// <param name="commentId">The id of the <see cref="M:Telerik.Sitefinity.Fluent.Content.ContentFacade`3.Comment" /> to be loaded.</param>
    /// <returns>
    /// The initialized child facade of type <see cref="!:CommentFacade" />.
    /// </returns>
    public virtual CommentFacade<TCurrentFacade> Comment(Guid commentId)
    {
      this.EnsureExistence(true);
      return new CommentFacade<TCurrentFacade>(this.AppSettings, commentId, (Telerik.Sitefinity.GenericContent.Model.Content) this.ContentItem, this as TCurrentFacade, this.ContentManager as ICommentsManager);
    }

    /// <summary>
    /// Initializes and returns a new child facade of type <see cref="!:CommentsFacade" /> with loaded a collection of <see cref="M:Telerik.Sitefinity.Fluent.Content.ContentFacade`3.Comment" /> objects
    /// belonging to the current content object and with a parent facade the current facade.
    /// </summary>
    /// <returns>
    /// The initialized child facade of type <see cref="!:CommentsFacade" />.
    /// </returns>
    public virtual CommentsFacade<TCurrentFacade> Comments()
    {
      this.EnsureExistence(true);
      return new CommentsFacade<TCurrentFacade>(this.AppSettings, (Telerik.Sitefinity.GenericContent.Model.Content) this.ContentItem, this as TCurrentFacade, this.ContentManager as ICommentsManager);
    }

    public virtual OrganizationFacade<TCurrentFacade> Organization() => new OrganizationFacade<TCurrentFacade>(this.AppSettings, (IOrganizable) this.ContentItem, this as TCurrentFacade);

    public TCurrentFacade Schedule(DateTime publicationDate) => throw new NotImplementedException();

    public TCurrentFacade Schedule(DateTime publicationDate, DateTime expirationDate) => throw new NotImplementedException();

    public ItemVersioningFacade<ContentFacade<TCurrentFacade, TContent, TParentFacade>> Versioning() => new ItemVersioningFacade<ContentFacade<TCurrentFacade, TContent, TParentFacade>>((IDataItem) this.ContentItem, this, this.appSettings);

    /// <summary>
    /// Returns the parent facade that initialized this child facade.
    /// </summary>
    /// <exception cref="T:System.InvalidOperationException">
    /// thrown if method is called and parentFacade is null; meaning that facade is not a child facade in this context.
    /// </exception>
    /// <returns>An instance of the parent facade type that initialized this facade as a child facade.</returns>
    public TParentFacade Done() => (object) this.ParentFacade != null ? this.ParentFacade : throw new InvalidOperationException("The facade instance is not set a parent facade instance.");

    /// <summary>
    /// Gets an instance of the <see cref="!:PageManager" /> to be used by this facade.
    /// </summary>
    /// <value>An initialized instance of the <see cref="!:PageManager" /> class.</value>
    public virtual IContentManager ContentManager
    {
      get
      {
        if (this.contentManager == null)
        {
          this.contentManager = ManagerBase.GetMappedManagerInTransaction(typeof (TContent), this.appSettings.ContentProviderName, this.appSettings.TransactionName) as IContentManager;
          if (this.contentManager == null)
            throw new InvalidOperationException("Unable to find a manager for " + (object) typeof (TContent));
          if (!(this.contentManager is ICommentsManager))
            throw new InvalidOperationException("The manage does not implement ICommentsManager interface");
        }
        return this.contentManager;
      }
      set => this.contentManager = value;
    }

    /// <summary>Gets or sets the content item.</summary>
    /// <value>The content item.</value>
    public virtual TContent ContentItem
    {
      get
      {
        if ((object) this.item == null)
          this.LoadItem(this.itemId);
        return this.item;
      }
      set => this.item = value;
    }

    /// <summary>Gets or sets the app settings.</summary>
    /// <value>The app settings.</value>
    public virtual AppSettings AppSettings
    {
      get => this.appSettings;
      set => this.appSettings = value;
    }

    /// <summary>
    /// Loads the content item into the API state. This method should be called only if the class have been
    /// constructed with the item id parameter.
    /// </summary>
    /// <param name="itemId">Id of the content that is to be loaded in the API state.</param>
    public virtual void LoadItem(Guid itemId) => this.item = !(itemId == Guid.Empty) ? this.ContentManager.GetItem(typeof (TContent), itemId) as TContent : throw new ArgumentNullException(nameof (itemId));

    /// <summary>
    /// This method is called to ensure that a content object has been loaded (either from id or created).
    /// </summary>
    /// <param name="throwExceptionIfNot">
    /// If this parameter is set to true, an exception will be thrown; otherwise method can be used in
    /// a less obtrusive way when it only returns the boolean value indicating whether a page has been loaded.
    /// </param>
    /// <exception cref="T:System.InvalidOperationException">
    /// thrown when there is no loaded content object and the <paramref name="throwExceptionIfNot" /> is True
    /// </exception>
    public virtual void EnsureExistence(bool throwExceptionIfNot)
    {
      if (throwExceptionIfNot && (object) this.ContentItem == null)
        throw new InvalidOperationException("This method must not be called before the content object has been loaded either through the constructor or by calling the CreateNew method.");
    }

    /// <summary>Gets or sets the parent facade.</summary>
    /// <value>The parent facade.</value>
    public virtual TParentFacade ParentFacade
    {
      get => this.parentFacade;
      set => this.parentFacade = value;
    }

    /// <summary>
    /// Casts the current instance to the type parameter of the current facade.
    /// </summary>
    /// <returns>An instance of the current facade type.</returns>
    /// <exception cref="T:System.InvalidCastException">
    /// thrown if the current instance cannot be cast to the type parameter of the current facade
    /// </exception>
    private TCurrentFacade CurrentFacade() => this is TCurrentFacade currentFacade ? currentFacade : throw new InvalidCastException(string.Format("Cannot cast {0} to {1}", (object) this.GetType().FullName, (object) typeof (TCurrentFacade).FullName));
  }
}
