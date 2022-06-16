// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Fluent.Pages.StandardPageFacade
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Globalization;
using System.Web;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Modules.Pages;
using Telerik.Sitefinity.Pages.Model;

namespace Telerik.Sitefinity.Fluent.Pages
{
  /// <summary>
  /// Fluent API that provides most common functionality related to a standard Sitefinity page.
  /// </summary>
  public class StandardPageFacade : BaseFacadeWithManager
  {
    private PageNode currentState;
    private PageFacade parentFacade;
    private AppSettings appSettings;

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Fluent.Pages.StandardPageFacade" /> class.
    /// </summary>
    /// <param name="parentFacade">The parent facade.</param>
    /// <param name="appSettings">The app settings.</param>
    /// <param name="currentState">The current data item for the facade.</param>
    public StandardPageFacade(
      PageFacade parentFacade,
      AppSettings appSettings,
      PageNode currentState)
      : base(appSettings)
    {
      if (parentFacade == null)
        throw new ArgumentNullException(nameof (parentFacade));
      if (appSettings == null)
        throw new ArgumentNullException(nameof (appSettings));
      if (currentState == null)
        throw new ArgumentNullException(nameof (currentState));
      this.parentFacade = parentFacade;
      this.appSettings = appSettings;
      this.currentState = currentState;
    }

    /// <summary>
    /// Gets the instance of <see cref="P:Telerik.Sitefinity.Fluent.Pages.StandardPageFacade.PageManager" /> to be used by the facade.
    /// </summary>
    public virtual PageManager PageManager => this.parentFacade.PageManager;

    /// <summary>
    /// Creates a draft and locks the current page to the current user.
    /// </summary>
    /// <returns></returns>
    public virtual PageDraftFacade CheckOut()
    {
      this.EnsureState();
      string g = HttpContext.Current.Items[(object) "SegmentId"] as string;
      PageData pageData = g.IsNullOrEmpty() ? this.currentState.GetPageData() : this.PageManager.GetPageData(this.currentState, segmentId: new Guid(g));
      return pageData != null ? new PageDraftFacade(this, this.appSettings, this.PageManager.EditPage(pageData.Id)) : new PageDraftFacade(this, this.appSettings, (PageDraft) null);
    }

    /// <summary>Converts the current standard page to page group.</summary>
    /// <param name="preservePageData">if set to <c>true</c> the associated page data is preserved otherwise it is deleted.</param>
    /// <remarks>
    /// If the preserved page data doesn't have additional navigational nodes, will not be accessible through the user interface until it is reassigned to a node.
    /// </remarks>
    /// <returns></returns>
    public virtual PageGroupFacade ConvertToPageGroup(bool preservePageData = true)
    {
      this.EnsureState();
      if (!preservePageData)
      {
        this.currentState.LocalizationStrategy = LocalizationStrategy.NotSelected;
        this.currentState.RemoveAssociatedPageDataObjects(this.PageManager);
      }
      this.currentState.NodeType = NodeType.Group;
      return new PageGroupFacade(this.parentFacade, this.appSettings, this.currentState);
    }

    /// <summary>
    /// Performs an arbitrary action on the page of type <see cref="T:Telerik.Sitefinity.Pages.Model.PageNode" /> currently loaded at Fluent API.
    /// </summary>
    /// <param name="setAction">An action to be performed on the current page loaded at Fluent API.</param>
    /// <exception cref="T:System.InvalidOperationException">
    /// thrown if the object of type <see cref="T:Telerik.Sitefinity.Pages.Model.PageNode" /> has not been initialized either through PageFacade(Guid pageId) constructor or CreateNew() method.
    /// </exception>
    /// <returns>An instance of <see cref="T:Telerik.Sitefinity.Fluent.Pages.PageFacade" /> object.</returns>
    public virtual StandardPageFacade Do(Action<PageNode> setAction)
    {
      if (setAction == null)
        throw new ArgumentNullException(nameof (setAction));
      this.EnsureState();
      if (this.currentState != null)
        setAction(this.currentState);
      return this;
    }

    /// <summary>
    /// Publishes this page. The changes become immediately publically visible.
    /// </summary>
    /// <returns></returns>
    public virtual StandardPageFacade Publish() => this.Publish((CultureInfo) null);

    /// <summary>
    /// Publishes this page. The changes become immediately publically visible.
    /// </summary>
    /// <returns></returns>
    public virtual StandardPageFacade Publish(CultureInfo culture) => this.CheckOut().Publish();

    /// <summary>Unpublishes this page.</summary>
    /// <returns></returns>
    public virtual StandardPageFacade Unpublish() => this.Unpublish((CultureInfo) null);

    /// <summary>Unpublishes this page.</summary>
    /// <returns></returns>
    public virtual StandardPageFacade Unpublish(CultureInfo culture)
    {
      this.PageManager.UnpublishPage(this.currentState.GetPageData(culture), culture);
      return this;
    }

    /// <summary>Returns the page currently loaded by the fluent API.</summary>
    /// <returns>An instance of <see cref="!:Node" /> object.</returns>
    public virtual PageNode Get() => this.currentState;

    /// <summary>
    /// Returns the parent facade that initialized this child facade.
    /// </summary>
    /// <returns>An instance of <see cref="T:Telerik.Sitefinity.Fluent.Pages.PageFacade" /> that initialized this facade as a child facade.</returns>
    public virtual PageFacade Done() => this.parentFacade;

    /// <summary>
    /// Returns whether the current standard page is locked by someone.
    /// </summary>
    /// <param name="lockedBy">The Guid of the user that has locked the page. If the page is not currently
    /// locked, the value is Guid.Empty.</param>
    /// <returns>
    /// 	<c>true</c> if the current standard page is locked; otherwise, <c>false</c>.
    /// </returns>
    public virtual bool IsLocked(out Guid lockedBy)
    {
      PageData pageData = this.currentState.GetPageData();
      lockedBy = pageData.LockedBy;
      return lockedBy != Guid.Empty;
    }

    /// <summary>
    /// Saves all the changes that were performed through the fluent API operations.
    /// </summary>
    /// <returns>An instance of <see cref="T:Telerik.Sitefinity.Fluent.Pages.StandardPageFacade" /> object.</returns>
    public new virtual bool SaveChanges()
    {
      base.SaveChanges();
      return true;
    }

    /// <summary>
    /// Commit the changes and return the current facade for additional fluent calls
    /// </summary>
    public virtual StandardPageFacade SaveAndContinue()
    {
      this.SaveChanges();
      return this;
    }

    /// <summary>
    /// Cancels all the changes that were performed through the fluent API operations.
    /// </summary>
    /// <returns>An instance of <see cref="T:Telerik.Sitefinity.Fluent.Pages.StandardPageFacade" /> object.</returns>
    public virtual StandardPageFacade CancelChanges()
    {
      base.CancelChanges();
      return this;
    }

    /// <summary>
    /// Ensures that the state of the facade has been initialized and throws an exception if not.
    /// </summary>
    protected virtual void EnsureState()
    {
      if (this.currentState == null)
        throw new InvalidOperationException("This method cannot be executed when the state of the facade is not initialized.");
    }

    /// <summary>
    /// Create a new instance of the manager in a named transaction using <see cref="!:settings" />
    /// </summary>
    /// <returns>Instance of this facade's manager</returns>
    /// <remarks>This is called internally by <see cref="!:GetManager" />. Do not call this manually unless you override GetManager as well.</remarks>
    protected override IManager InitializeManager() => (IManager) this.PageManager;
  }
}
