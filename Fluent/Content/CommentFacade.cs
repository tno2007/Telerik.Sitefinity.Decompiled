// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Fluent.Content.CommentFacade`1
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Linq;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.GenericContent.Model;
using Telerik.Sitefinity.Model;

namespace Telerik.Sitefinity.Fluent.Content
{
  [Obsolete]
  public class CommentFacade<TParentFacade> : 
    IItemFacade<CommentFacade<TParentFacade>, Comment>,
    IFacade<CommentFacade<TParentFacade>>
  {
    private AppSettings appSettings;
    private Guid itemId;
    private Comment comment;
    private ICommentsManager contentManager;
    private Telerik.Sitefinity.GenericContent.Model.Content parent;
    private TParentFacade parentFacade;

    internal CommentFacade()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="!:CommentFacade" /> class.
    /// </summary>
    /// <param name="appSettings">The app settings that configure the way fluent API will function.</param>
    /// <param name="itemId">The id of the <see cref="T:Telerik.Sitefinity.GenericContent.Model.Comment" /> to be loaded.</param>
    /// <param name="parent">The parent content of the <see cref="T:Telerik.Sitefinity.GenericContent.Model.Comment" /> instance.</param>
    /// <param name="parentFacade">The parent facade.</param>
    /// <param name="manager">The content manager used by the facade.</param>
    public CommentFacade(
      AppSettings appSettings,
      Guid itemId,
      Telerik.Sitefinity.GenericContent.Model.Content parent,
      TParentFacade parentFacade,
      ICommentsManager manager)
      : this(appSettings, parent, parentFacade, manager)
    {
      this.itemId = !(itemId == Guid.Empty) ? itemId : throw new ArgumentException(nameof (itemId));
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="!:CommentFacade" /> class.
    /// </summary>
    /// <param name="appSettings">The app settings that configure the way fluent API will function.</param>
    /// <param name="parent">The parent content of the <see cref="T:Telerik.Sitefinity.GenericContent.Model.Comment" /> instance.</param>
    /// <param name="parentFacade">The parent facade.</param>
    /// <param name="manager">The manager that will be used by the facade.</param>
    public CommentFacade(
      AppSettings appSettings,
      Telerik.Sitefinity.GenericContent.Model.Content parent,
      TParentFacade parentFacade,
      ICommentsManager manager)
    {
      if (parent == null)
        throw new ArgumentNullException(nameof (parent));
      if ((object) parentFacade == null)
        throw new ArgumentNullException(nameof (parentFacade));
      if (manager == null)
        throw new ArgumentNullException(nameof (manager));
      if (appSettings == null)
        throw new ArgumentNullException("appSettings ");
      this.parent = parent;
      this.parentFacade = parentFacade;
      this.contentManager = manager;
      this.appSettings = appSettings;
    }

    /// <summary>
    /// Gets an instance of the <see cref="P:Telerik.Sitefinity.Fluent.Content.CommentFacade`1.ContentManager" /> to be used by this facade.
    /// </summary>
    /// <value>An initialized instance of the <see cref="P:Telerik.Sitefinity.Fluent.Content.CommentFacade`1.ContentManager" /> class.</value>
    protected internal virtual ICommentsManager ContentManager
    {
      get => this.contentManager;
      set => this.contentManager = value;
    }

    /// <summary>
    /// Gets the comment on which all the methods of the fluent API are being performed.
    /// </summary>
    protected internal virtual Comment CurrentComment
    {
      get
      {
        if (this.comment == null)
          this.LoadItem(this.itemId);
        return this.comment;
      }
      set => this.comment = value;
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

    /// <summary>Gets or sets the parent facade.</summary>
    /// <value>The parent facade.</value>
    protected internal virtual TParentFacade ParentFacade
    {
      get => this.parentFacade;
      set => this.parentFacade = value;
    }

    /// <summary>
    /// Gets or sets the <see cref="T:Telerik.Sitefinity.GenericContent.Model.Comment" /> id.
    /// </summary>
    /// <value>The <see cref="T:Telerik.Sitefinity.GenericContent.Model.Comment" /> id.</value>
    protected internal virtual Guid ItemId
    {
      get => this.itemId;
      set => this.itemId = value;
    }

    /// <summary>
    /// Blocks the comments from the user with the specified email.
    /// </summary>
    /// <param name="email">The email.</param>
    /// <returns>An instance of the <see cref="!:CommentFacade" /> type.</returns>
    public virtual CommentFacade<TParentFacade> BlockEmail(string email)
    {
      this.EnsureItemLoaded(true);
      this.ContentManager.BlockCommentsForEmail(email);
      return this;
    }

    /// <summary>Blocks the comments coming from the given IP address.</summary>
    /// <param name="ip">The IP address.</param>
    /// <returns>An instance of the <see cref="!:CommentFacade" /> type.</returns>
    public virtual CommentFacade<TParentFacade> BlockIP(string ip)
    {
      this.EnsureItemLoaded(true);
      this.ContentManager.BlockCommentsForIP(ip);
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
    /// <returns>An instance of the <see cref="!:CommentFacade" /> type.</returns>
    public virtual CommentFacade<TParentFacade> CancelChanges()
    {
      TransactionManager.RollbackTransaction(this.AppSettings.TransactionName);
      return this;
    }

    /// <summary>
    /// Creates a new instance of the <see cref="T:Telerik.Sitefinity.GenericContent.Model.Comment" /> type.
    /// </summary>
    /// <returns>An instance of the <see cref="!:CommentFacade" /> type.</returns>
    public virtual CommentFacade<TParentFacade> CreateNew()
    {
      this.comment = this.ContentManager.CreateComment((ICommentable) this.parent);
      return this;
    }

    /// <summary>
    /// Creates a new instance of the <see cref="T:Telerik.Sitefinity.GenericContent.Model.Comment" /> type with the specified id.
    /// </summary>
    /// <param name="itemId">The id of the new comment.</param>
    /// <returns>An instance of the <see cref="!:CommentFacade" /> type.</returns>
    public virtual CommentFacade<TParentFacade> CreateNew(Guid itemId)
    {
      this.comment = this.ContentManager.CreateComment((ICommentable) this.parent, itemId);
      return this;
    }

    /// <summary>
    /// Removes from its parent collection and deletes the instance of the <see cref="T:Telerik.Sitefinity.GenericContent.Model.Comment" /> type currently loaded by the fluent API.
    /// </summary>
    /// <exception cref="T:System.InvalidOperationException">
    /// thrown if the object has not been initialized either through constructor, CreateNew() or Set() method.
    /// </exception>
    /// <returns>An instance of the <see cref="!:CommentFacade" /> type.</returns>
    public virtual CommentFacade<TParentFacade> Delete()
    {
      this.EnsureItemLoaded(true);
      this.ParentContent.Comments.Remove(this.CurrentComment);
      this.ContentManager.Delete(this.CurrentComment);
      return this;
    }

    /// <summary>Performs an arbitrary action on the comment object.</summary>
    /// <param name="setAction">An action to be performed on the page taxon object.</param>
    /// <exception cref="T:System.InvalidOperationException">
    /// thrown if the <see cref="T:Telerik.Sitefinity.GenericContent.Model.Comment" /> object has not been initialized either through constructor, CreateNew() or Set() method.
    /// </exception>
    /// <returns>An instance of the <see cref="!:CommentFacade" /> type.</returns>
    public virtual CommentFacade<TParentFacade> Do(Action<Comment> action)
    {
      this.EnsureItemLoaded(true);
      action(this.CurrentComment);
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
    /// Hides the instance of the <see cref="T:Telerik.Sitefinity.GenericContent.Model.Comment" /> type currently loaded by the fluent API.
    /// </summary>
    /// <returns>An instance of the <see cref="!:CommentFacade" /> type.</returns>
    public virtual CommentFacade<TParentFacade> Hide()
    {
      this.EnsureItemLoaded(true);
      this.CurrentComment.Visible = false;
      this.CurrentComment.CommentStatus = CommentStatus.Hidden;
      return this;
    }

    /// <summary>
    /// Marks the instance of the <see cref="T:Telerik.Sitefinity.GenericContent.Model.Comment" /> type currently loaded by the fluent API as favourite.
    /// </summary>
    /// <returns>An instance of the <see cref="!:CommentFacade" /> type.</returns>
    /// <exception cref="T:System.NotImplementedException">the method is not implemented</exception>
    public virtual CommentFacade<TParentFacade> MarkAsFavourite() => throw new NotImplementedException();

    /// <summary>
    /// Marks the instance of the <see cref="T:Telerik.Sitefinity.GenericContent.Model.Comment" /> type currently loaded by the fluent API as spam.
    /// </summary>
    /// <returns>An instance of the <see cref="!:CommentFacade" /> type.</returns>
    public virtual CommentFacade<TParentFacade> MarkAsSpam()
    {
      this.EnsureItemLoaded(true);
      this.CurrentComment.CommentStatus = CommentStatus.Spam;
      return this;
    }

    /// <summary>
    /// Replies to the loaded <see cref="T:Telerik.Sitefinity.GenericContent.Model.Comment" /> instance by the facade with the <see cref="T:Telerik.Sitefinity.GenericContent.Model.Comment" /> instance wiht the specified id.
    /// </summary>
    /// <param name="commentId">The id of the <see cref="T:Telerik.Sitefinity.GenericContent.Model.Comment" /> instance that is replied with.</param>
    /// <returns>An instance of the <see cref="!:CommentFacade" /> type.</returns>
    public virtual CommentFacade<TParentFacade> Reply(Guid commentId)
    {
      if (commentId == Guid.Empty)
        throw new ArgumentException(nameof (commentId));
      this.EnsureItemLoaded(true);
      Comment comment = this.ContentManager.GetComment(commentId);
      this.CurrentComment.CommentedItemID = commentId;
      this.CurrentComment.CommentedItemType = comment.GetType().FullName;
      return this;
    }

    /// <summary>
    /// Replies to the loaded <see cref="T:Telerik.Sitefinity.GenericContent.Model.Comment" /> instance by the facade with the specified <see cref="T:Telerik.Sitefinity.GenericContent.Model.Comment" /> instance.
    /// </summary>
    /// <param name="comment">The <see cref="T:Telerik.Sitefinity.GenericContent.Model.Comment" /> instance that is replied with.</param>
    /// <returns>An instance of the <see cref="!:CommentFacade" /> type.</returns>
    public virtual CommentFacade<TParentFacade> Reply(Comment comment)
    {
      if (comment == null)
        throw new ArgumentNullException(nameof (comment));
      this.EnsureItemLoaded(true);
      this.CurrentComment.CommentedItemID = comment.Id;
      this.CurrentComment.CommentedItemType = comment.GetType().FullName;
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
    /// <returns>An instance of the <see cref="!:CommentFacade" /> type.</returns>
    public virtual CommentFacade<TParentFacade> SaveChanges()
    {
      TransactionManager.CommitTransaction(this.appSettings.TransactionName);
      return this;
    }

    /// <summary>
    /// Sets a <see cref="T:Telerik.Sitefinity.GenericContent.Model.Comment" /> instance to currently loaded fluent API.
    /// </summary>
    /// <returns>An instance of the <see cref="!:CommentFacade" /> type.</returns>
    public virtual CommentFacade<TParentFacade> Set(Comment item)
    {
      if (item == null)
        throw new ArgumentNullException(nameof (item));
      if (item.CommentedItemID != Guid.Empty && item.CommentedItemID != this.parent.Id || !string.IsNullOrEmpty(item.CommentedItemType) && item.CommentedItemType != this.parent.GetType().FullName)
        throw new InvalidOperationException("You cannot set a comment with a different type or a different ID.");
      Comment comment = this.ContentManager.GetComment(item.Id);
      if (this.parent.Comments.SingleOrDefault<Comment>((Func<Comment, bool>) (c => c.Id == item.Id)) == null)
      {
        comment.CommentedItemID = this.parent.Id;
        comment.CommentedItemType = this.parent.GetType().FullName;
        this.parent.Comments.Add(comment);
      }
      this.comment = comment;
      return this;
    }

    /// <summary>
    /// Shows the instance of the <see cref="T:Telerik.Sitefinity.GenericContent.Model.Comment" /> type currently loaded by the fluent API.
    /// </summary>
    /// <returns>An instance of the <see cref="!:CommentFacade" /> type.</returns>
    public virtual CommentFacade<TParentFacade> Show()
    {
      this.EnsureItemLoaded(true);
      this.CurrentComment.Visible = true;
      this.CurrentComment.CommentStatus = CommentStatus.Published;
      return this;
    }

    /// <summary>
    /// Returns the instance of the <see cref="T:Telerik.Sitefinity.GenericContent.Model.Comment" /> type currently loaded by the fluent API.
    /// </summary>
    /// <returns>An instance of the <see cref="T:Telerik.Sitefinity.GenericContent.Model.Comment" /> type.</returns>
    public virtual Comment Get() => this.CurrentComment;

    /// <summary>
    /// Loads the instance of the <see cref="T:Telerik.Sitefinity.GenericContent.Model.Comment" /> type with the specified id.
    /// </summary>
    /// <param name="itemId">The id of the <see cref="T:Telerik.Sitefinity.GenericContent.Model.Comment" /> instance to be loaded.</param>
    /// <exception cref="T:System.ArgumentException">
    /// thrown when the specified id is empty
    /// </exception>
    protected internal virtual void LoadItem(Guid itemId) => this.comment = !(itemId == Guid.Empty) ? this.ContentManager.GetComment(itemId) : throw new ArgumentException(nameof (itemId));

    /// <summary>
    /// This method is called to ensure that a comment has been loaded (either from id or created).
    /// </summary>
    /// <param name="throwExceptionIfNot">
    /// If this parameter is set to true, an exception will be thrown; otherwise method can be used in
    /// a less obtrusive way when it only returns the boolean value indicating whether a page has been loaded.
    /// </param>
    /// <returns>True if the item has been loaded; otherwise false.</returns>
    /// <exception cref="T:System.InvalidOperationException">
    /// thrown when there is no loaded <see cref="T:Telerik.Sitefinity.GenericContent.Model.Comment" /> instance and the <paramref name="throwExceptionIfNot" /> is True
    /// </exception>
    protected internal virtual bool EnsureItemLoaded(bool throwExceptionIfNot)
    {
      bool flag = this.CurrentComment != null;
      return !throwExceptionIfNot || flag ? flag : throw new InvalidOperationException("This method must not be called before the comment object has been loaded either through the Comment's constructors, by calling the CreateNew method or by calling the Set method.");
    }
  }
}
