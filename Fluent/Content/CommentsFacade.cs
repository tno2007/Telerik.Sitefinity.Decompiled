// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Fluent.Content.CommentsFacade`1
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.GenericContent.Model;
using Telerik.Sitefinity.Model;

namespace Telerik.Sitefinity.Fluent.Content
{
  [Obsolete]
  public class CommentsFacade<TParentFacade> : 
    ICollectionFacade<CommentsFacade<TParentFacade>, Comment>,
    IFacade<CommentsFacade<TParentFacade>>
  {
    private ICommentsManager contentManager;
    private IQueryable<Comment> items;
    private TParentFacade parentFacade;
    private AppSettings appSettings;
    private Telerik.Sitefinity.GenericContent.Model.Content parent;

    internal CommentsFacade()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="!:CommentsFacade" /> class.
    /// </summary>
    /// <param name="appSettings">The app settings that configure the way fluent API will function.</param>
    /// <param name="parentItem">The parent item that contains the collection of comments.</param>
    /// <param name="parentFacade">The parent facade.</param>
    /// <param name="manager">The content manager used by the facade.</param>
    public CommentsFacade(
      AppSettings appSettings,
      Telerik.Sitefinity.GenericContent.Model.Content parentItem,
      TParentFacade parentFacade,
      ICommentsManager manager)
    {
      if (parentItem == null)
        throw new ArgumentNullException(nameof (parentItem));
      if ((object) parentFacade == null)
        throw new ArgumentNullException(nameof (parentFacade));
      if (manager == null)
        throw new ArgumentNullException(nameof (manager));
      if (appSettings == null)
        throw new ArgumentNullException("appSettings ");
      this.parent = parentItem;
      this.items = parentItem.Comments.AsQueryable<Comment>();
      this.parentFacade = parentFacade;
      this.contentManager = manager;
      this.appSettings = appSettings;
    }

    /// <summary>
    /// Gets an instance of the <see cref="T:Telerik.Sitefinity.Model.ICommentsManager" /> used by this facade.
    /// </summary>
    /// <value>The content manager.</value>
    protected internal virtual ICommentsManager ContentManager
    {
      get => this.contentManager;
      set => this.contentManager = value;
    }

    /// <summary>Gets or sets the collection of comments.</summary>
    /// <value>The collection of comments.</value>
    protected internal virtual IQueryable<Comment> Items
    {
      get => this.items;
      set => this.items = value;
    }

    /// <summary>Gets or sets the parent facade.</summary>
    /// <value>The parent facade.</value>
    protected internal virtual TParentFacade ParentFacade
    {
      get => this.parentFacade;
      set => this.parentFacade = value;
    }

    /// <summary>Gets or sets the app settings.</summary>
    /// <value>The app settings.</value>
    protected internal virtual AppSettings AppSettings
    {
      get => this.appSettings;
      set => this.appSettings = value;
    }

    /// <summary>Gets or sets the parent content.</summary>
    /// <value>The parent content.</value>
    protected internal virtual Telerik.Sitefinity.GenericContent.Model.Content ParentContent
    {
      get => this.parent;
      set => this.parent = value;
    }

    /// <summary>
    /// Blocks the comments from the user with the specified email.
    /// </summary>
    /// <param name="email">The email.</param>
    /// <returns>An instance of the <see cref="!:CommentsFacade" /> type.</returns>
    public virtual CommentsFacade<TParentFacade> BlockEmail(string email)
    {
      this.EnsureItemsLoaded(true);
      this.ContentManager.BlockCommentsForEmail(email);
      return this;
    }

    /// <summary>Blocks the comments coming from the given IP address.</summary>
    /// <param name="ip">The IP address.</param>
    /// <returns>An instance of the <see cref="!:CommentsFacade" /> type.</returns>
    public virtual CommentsFacade<TParentFacade> BlockIP(string ip)
    {
      this.EnsureItemsLoaded(true);
      this.ContentManager.BlockCommentsForIP(ip);
      return this;
    }

    /// <summary>
    /// Returns the parent facade that initialized this child facade.
    /// </summary>
    /// <exception cref="T:System.InvalidOperationException">
    /// thrown if method is called and parentFacade is null; meaning that facade is not a child facade in this context.
    /// </exception>
    /// <returns>An instance of the parent facade type that initialized this facade as a child facade.</returns>
    public virtual TParentFacade Done() => (object) this.parentFacade != null ? this.parentFacade : throw new InvalidOperationException("The facade instance is not set a parent facade instance.");

    /// <summary>
    /// Hides the instances of the <see cref="T:Telerik.Sitefinity.GenericContent.Model.Comment" /> type currently loaded by the fluent API.
    /// </summary>
    /// <returns>An instance of the <see cref="!:CommentsFacade" /> type.</returns>
    public virtual CommentsFacade<TParentFacade> Hide()
    {
      this.EnsureItemsLoaded(true);
      foreach (Comment comment in (IEnumerable<Comment>) this.items)
      {
        comment.Visible = false;
        comment.CommentStatus = CommentStatus.Hidden;
      }
      return this;
    }

    /// <summary>
    /// Marks the instances of the <see cref="T:Telerik.Sitefinity.GenericContent.Model.Comment" /> type currently loaded by the fluent API as favourite.
    /// </summary>
    /// <returns>An instance of the <see cref="!:CommentsFacade" /> type.</returns>
    /// <exception cref="T:System.NotImplementedException">the method is not implemented</exception>
    public virtual CommentsFacade<TParentFacade> MarkAsFavourite() => throw new NotImplementedException();

    /// <summary>
    /// Marks the instances of the <see cref="T:Telerik.Sitefinity.GenericContent.Model.Comment" /> type currently loaded by the fluent API as spam.
    /// </summary>
    /// <returns>An instance of the <see cref="!:CommentsFacade" /> type.</returns>
    public virtual CommentsFacade<TParentFacade> MarkAsSpam()
    {
      this.EnsureItemsLoaded(true);
      foreach (Comment comment in (IEnumerable<Comment>) this.items)
        comment.CommentStatus = CommentStatus.Spam;
      return this;
    }

    /// <summary>
    /// Shows the instances of the <see cref="T:Telerik.Sitefinity.GenericContent.Model.Comment" /> type currently loaded by the fluent API.
    /// </summary>
    /// <returns>An instance of the <see cref="!:CommentsFacade" /> type.</returns>
    public virtual CommentsFacade<TParentFacade> Show()
    {
      this.EnsureItemsLoaded(true);
      foreach (Comment comment in (IEnumerable<Comment>) this.items)
      {
        comment.Visible = true;
        comment.CommentStatus = CommentStatus.Published;
      }
      return this;
    }

    /// <summary>
    /// Gets the count of the instances of the <see cref="T:Telerik.Sitefinity.GenericContent.Model.Comment" /> type currently loaded by the fluent API.
    /// </summary>
    /// <param name="result">The count of <see cref="T:Telerik.Sitefinity.GenericContent.Model.Comment" /> instances.</param>
    /// <returns>An instance of the <see cref="!:CommentsFacade" /> type.</returns>
    public virtual CommentsFacade<TParentFacade> Count(out int result)
    {
      this.EnsureItemsLoaded(true);
      result = this.Items.Count<Comment>();
      return this;
    }

    /// <summary>
    /// Performs an arbitrary action for each instance of the <see cref="T:Telerik.Sitefinity.GenericContent.Model.Comment" /> type in the collection of the facade.
    /// </summary>
    /// <param name="action">An action to be performed for each instance of the <see cref="T:Telerik.Sitefinity.GenericContent.Model.Comment" /> in the collection.</param>
    /// <returns>An instance of the <see cref="!:CommentsFacade" /> type.</returns>
    public virtual CommentsFacade<TParentFacade> ForEach(Action<Comment> action)
    {
      this.EnsureItemsLoaded(true);
      foreach (Comment comment in (IEnumerable<Comment>) this.items)
        action(comment);
      return this;
    }

    /// <summary>
    /// Gets query with the instances of the <see cref="T:Telerik.Sitefinity.GenericContent.Model.Comment" /> type currently loaded by the fluent API.
    /// </summary>
    /// <returns>An instance of IQueryable object with the <see cref="T:Telerik.Sitefinity.GenericContent.Model.Comment" /> instances.</returns>
    public virtual IQueryable<Comment> Get()
    {
      this.EnsureItemsLoaded(true);
      return this.items;
    }

    /// <summary>
    /// Orders the items in the collection in ascending order with keys specified with keySelector parameter.
    /// </summary>
    /// <param name="keySelector">The key selector.</param>
    /// <returns>An instance of the <see cref="!:CommentsFacade" /> type.</returns>
    public virtual CommentsFacade<TParentFacade> OrderBy<TKey>(
      Expression<Func<Comment, TKey>> keySelector)
    {
      this.EnsureItemsLoaded(true);
      this.items = (IQueryable<Comment>) this.items.OrderBy<Comment, TKey>(keySelector);
      return this;
    }

    /// <summary>
    /// Orders the items in the collection in descending order with keys specified with keySelector parameter.
    /// </summary>
    /// <param name="keySelector">The key selector.</param>
    /// <returns>An instance of the <see cref="!:CommentsFacade" /> type.</returns>
    public virtual CommentsFacade<TParentFacade> OrderByDescending<TKey>(
      Expression<Func<Comment, TKey>> keySelector)
    {
      this.EnsureItemsLoaded(true);
      this.items = (IQueryable<Comment>) this.items.OrderByDescending<Comment, TKey>(keySelector);
      return this;
    }

    /// <summary>Sets the specified query.</summary>
    /// <param name="query">The query to be set.</param>
    /// <returns>An instance of the <see cref="!:CommentsFacade" /> type.</returns>
    public virtual CommentsFacade<TParentFacade> Set(IQueryable<Comment> query)
    {
      this.items = query != null ? query : throw new ArgumentNullException(nameof (query));
      return this;
    }

    /// <summary>
    /// Skips the specified count of <see cref="T:Telerik.Sitefinity.GenericContent.Model.Comment" /> instances from the collection.
    /// </summary>
    /// <param name="count">The count of <see cref="T:Telerik.Sitefinity.GenericContent.Model.Comment" /> instances to be skipped.</param>
    /// <returns>An instance of the <see cref="!:CommentsFacade" /> type.</returns>
    public virtual CommentsFacade<TParentFacade> Skip(int count)
    {
      this.EnsureItemsLoaded(true);
      this.items = this.items.Skip<Comment>(count);
      return this;
    }

    /// <summary>
    /// Takes the specified count of <see cref="T:Telerik.Sitefinity.GenericContent.Model.Comment" /> instances from the collection.
    /// </summary>
    /// <param name="count">The count of <see cref="T:Telerik.Sitefinity.GenericContent.Model.Comment" /> instances to be taken.</param>
    /// <returns>An instance of the <see cref="!:CommentsFacade" /> type.</returns>
    public virtual CommentsFacade<TParentFacade> Take(int count)
    {
      this.EnsureItemsLoaded(true);
      this.items = this.items.Take<Comment>(count);
      return this;
    }

    /// <summary>
    /// Filters the <see cref="T:Telerik.Sitefinity.GenericContent.Model.Comment" /> instances in the collection by the specified <paramref name="predicate" /> parameter.
    /// </summary>
    /// <param name="predicate">The predicate to filter by.</param>
    /// <returns>An instance of the <see cref="!:CommentsFacade" /> type.</returns>
    public virtual CommentsFacade<TParentFacade> Where(Func<Comment, bool> predicate)
    {
      this.EnsureItemsLoaded(true);
      this.items = this.items.Where<Comment>(predicate).AsQueryable<Comment>();
      return this;
    }

    /// <summary>
    /// Saves all the changes that were performed through the fluent API operations.
    /// </summary>
    /// <remarks>
    /// This method needs to be used if you have configured the fluent API not to auto-commit. By default
    /// fluent API will auto-commit all operations as they are called. Use this method when you wish to
    /// work in transactions.
    /// </remarks>
    /// <returns>An instance of the <see cref="!:CommentsFacade" /> type.</returns>
    public virtual CommentsFacade<TParentFacade> SaveChanges()
    {
      TransactionManager.CommitTransaction(this.appSettings.TransactionName);
      return this;
    }

    /// <summary>
    /// Cancels all the changes that were performed through the fluent API operations.
    /// </summary>
    /// <remarks>
    /// This method needs to be used if you have configured the fluent API not to auto-commit. By default
    /// fluent API will auto-commit all operations as they are called. Use this method when you wish to
    /// work in transactions.
    /// </remarks>
    /// <returns>An instance of the <see cref="!:CommentsFacade" /> type.</returns>
    public virtual CommentsFacade<TParentFacade> CancelChanges()
    {
      TransactionManager.RollbackTransaction(this.appSettings.TransactionName);
      return this;
    }

    /// <summary>
    /// Deletes the instances of the <see cref="T:Telerik.Sitefinity.GenericContent.Model.Comment" /> type currently loaded by the fluent API
    /// </summary>
    /// <exception cref="T:System.InvalidOperationException">
    /// thrown if the collection has not been initialized either through constructor or Set() method.
    /// </exception>
    /// <returns>An instance of the <see cref="!:CommentsFacade" /> type.</returns>
    public virtual CommentsFacade<TParentFacade> Delete()
    {
      this.EnsureItemsLoaded(true);
      foreach (Comment comment in this.Items.ToList<Comment>())
        this.ParentContent.Comments.Remove(comment);
      return this;
    }

    /// <summary>
    /// This method is called to ensure that a collection of comments has been set.
    /// </summary>
    /// <param name="throwExceptionIfNot">
    /// If this parameter is set to true, an exception will be thrown; otherwise method can be used in
    /// a less obtrusive way when it only returns the boolean value indicating whether a page has been loaded.
    /// </param>
    /// <returns>True if the collection has been set; otherwise false.</returns>
    /// <exception cref="T:System.InvalidOperationException">
    /// thrown when there is no loaded <see cref="T:Telerik.Sitefinity.GenericContent.Model.Comment" /> collection and the <paramref name="throwExceptionIfNot" /> is True
    /// </exception>
    protected internal virtual bool EnsureItemsLoaded(bool throwExceptionIfNot)
    {
      bool flag = this.Items != null;
      return !throwExceptionIfNot || flag ? flag : throw new InvalidOperationException("This method must not be called before the collection of Comments is set.");
    }
  }
}
