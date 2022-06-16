// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Fluent.Pages.PageDraftFacade
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.Fluent.Pages.Contracts;
using Telerik.Sitefinity.Fluent.Versioning;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.Model.Localization;
using Telerik.Sitefinity.Modules.GenericContent;
using Telerik.Sitefinity.Modules.Pages;
using Telerik.Sitefinity.Pages.Model;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Versioning;

namespace Telerik.Sitefinity.Fluent.Pages
{
  /// <summary>
  /// Fluent API that provides most common functionality related to a Sitefinity page drafts.
  /// </summary>
  public class PageDraftFacade : BaseFacadeWithManager, IPageFacade
  {
    private PageDraft currentState;
    private StandardPageFacade parentFacade;
    private AppSettings appSettings;

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Fluent.Pages.PageDraftFacade" /> class.
    /// </summary>
    /// <param name="parentFacade">The parent facade.</param>
    /// <param name="appSettings">The app settings.</param>
    /// <param name="currentState">The current data item for the facade.</param>
    public PageDraftFacade(
      StandardPageFacade parentFacade,
      AppSettings appSettings,
      PageDraft currentState)
      : base(appSettings)
    {
      if (parentFacade == null)
        throw new ArgumentNullException(nameof (parentFacade));
      if (appSettings == null)
        throw new ArgumentNullException(nameof (appSettings));
      this.parentFacade = parentFacade;
      this.appSettings = appSettings;
      this.currentState = currentState;
    }

    /// <summary>
    /// Gets the instance of <see cref="P:Telerik.Sitefinity.Fluent.Pages.PageDraftFacade.PageManager" /> to be used by the facade.
    /// </summary>
    protected virtual PageManager PageManager => this.parentFacade.PageManager;

    /// <summary>Gets the current state.</summary>
    public IControlsContainer CurrentState => (IControlsContainer) this.currentState;

    /// <summary>Sets the template of the current page.</summary>
    /// <param name="templateId">The template ID.</param>
    /// <returns>An instance of <see cref="T:Telerik.Sitefinity.Fluent.Pages.PageDraftFacade" /> object.</returns>
    public virtual PageDraftFacade SetTemplateTo(Guid templateId)
    {
      if (this.currentState != null)
      {
        if (templateId != Guid.Empty)
        {
          if (this.PageManager.GetTemplates().Where<PageTemplate>((Expression<Func<PageTemplate, bool>>) (t => t.Id == templateId)).Count<PageTemplate>() == 0)
            throw new ArgumentException("There is no template with ID \"{0}\".".Arrange((object) templateId));
        }
        this.currentState.TemplateId = templateId;
      }
      return this;
    }

    /// <summary>Sets the template of the current page.</summary>
    /// <param name="template">The template.</param>
    /// <returns>An instance of <see cref="T:Telerik.Sitefinity.Fluent.Pages.PageDraftFacade" /> object.</returns>
    public virtual PageDraftFacade SetTemplateTo(PageTemplate template)
    {
      if (this.currentState != null)
        this.currentState.TemplateId = template != null ? template.Id : Guid.Empty;
      return this;
    }

    /// <summary>Sets the theme for the current page.</summary>
    /// <param name="themeName">
    /// The name of the theme. It must be a valid existing theme, otherwise an exception will be thrown.
    /// </param>
    /// <returns>An instance of <see cref="T:Telerik.Sitefinity.Fluent.Pages.PageDraftFacade" /> object.</returns>
    public virtual PageDraftFacade SetTheme(string themeName) => this.SetTheme(SystemManager.CurrentContext.Culture, themeName);

    /// <summary>
    /// Sets the theme for the specified language of the current page.
    /// </summary>
    /// <param name="language">The language.</param>
    /// <param name="themeName">
    /// The name of the theme. It must be a valid existing theme, otherwise an exception will be thrown.
    /// </param>
    /// <returns>An instance of <see cref="T:Telerik.Sitefinity.Fluent.Pages.PageDraftFacade" /> object.</returns>
    public virtual PageDraftFacade SetTheme(CultureInfo language, string themeName)
    {
      if (this.currentState != null)
        this.currentState.Themes[language] = themeName;
      return this;
    }

    /// <summary>
    /// Provides the access to the child facade for working with a single page control.
    /// </summary>
    /// <returns>
    /// An instance of the <see cref="!:ControlsFacade[PageDraftFacade]" />.
    /// </returns>
    public virtual ControlFacade Control() => new ControlFacade(this, this.appSettings);

    /// <summary>
    /// Provides the access to the child facade for working with the child controls of the current page.
    /// </summary>
    /// <returns>
    /// An instance of the <see cref="!:ControlsFacade[PageDraftFacade]" />.
    /// </returns>
    public virtual ControlsFacade<PageDraftFacade> Controls() => new ControlsFacade<PageDraftFacade>(this, this.appSettings);

    /// <summary>
    /// Provides versioning functionality for the current page draft.
    /// </summary>
    /// <returns>An instance of <see cref="!:ItemVersioningFacade[PageDraftFacade]" /> object.</returns>
    public virtual ItemVersioningFacade<PageDraftFacade> Versioning() => new ItemVersioningFacade<PageDraftFacade>((IDataItem) this.currentState, this, this.appSettings);

    /// <summary>
    /// Performs an arbitrary action on the <see cref="T:Telerik.Sitefinity.Pages.Model.PageDraft" /> object.
    /// </summary>
    /// <param name="action">An action to be performed on the <see cref="T:Telerik.Sitefinity.Pages.Model.PageDraft" /> object.</param>
    /// <exception cref="T:System.InvalidOperationException">
    /// thrown if the <see cref="T:Telerik.Sitefinity.Pages.Model.PageDraft" /> has not been initialized.
    /// </exception>
    /// <returns>An instance of <see cref="T:Telerik.Sitefinity.Fluent.Pages.PageDraftFacade" /> object.</returns>
    public virtual PageDraftFacade Do(Action<PageDraft> action)
    {
      if (action == null)
        throw new ArgumentNullException(nameof (action));
      if (this.currentState != null)
        action(this.currentState);
      return this;
    }

    /// <summary>
    /// Applies all current changes to the draft version and unlocks the draft so other users can continue editing.
    /// </summary>
    /// <returns>An instance of <see cref="T:Telerik.Sitefinity.Fluent.Pages.StandardPageFacade" /> object.</returns>
    public virtual StandardPageFacade CheckIn()
    {
      if (this.currentState != null)
      {
        this.currentState = this.PageManager.SavePageDraft(this.currentState);
        this.currentState.ParentPage.LockedBy = Guid.Empty;
        this.Versioning().CreateNewVersion(VersionType.Minor, this.currentState.ParentPage.Id);
        this.SavePersonalizedPagesDrafts();
      }
      return this.parentFacade;
    }

    /// <summary>
    /// Pulls updated shared content into a page draft which contains related content blocks.
    /// </summary>
    /// <param name="contentItemId">The id of the shared content item to update.</param>
    /// <param name="contentProviderName">Name of the content provider related to the content item.</param>
    /// <returns>An instance of <see cref="T:Telerik.Sitefinity.Fluent.Pages.PageDraftFacade" /> object.</returns>
    public virtual PageDraftFacade PullUpdatedContent(
      Guid contentItemId,
      string contentProviderName)
    {
      if (this.currentState != null)
        ContentManager.GetManager(contentProviderName, this.appSettings.TransactionName).PushUpdatedContentToPageDraft(contentItemId, this.currentState);
      return this;
    }

    /// <summary>Applies all current changes to a new draft version.</summary>
    /// <returns>An instance of <see cref="T:Telerik.Sitefinity.Fluent.Pages.StandardPageFacade" /> object.</returns>
    public virtual StandardPageFacade SaveDraft()
    {
      if (this.currentState != null)
      {
        this.PageManager.SavePageDraft(this.currentState);
        this.Versioning().CreateNewVersion(VersionType.Minor, this.currentState.ParentPage.Id);
        this.SavePersonalizedPagesDrafts();
      }
      return this.parentFacade;
    }

    /// <summary>
    /// Publishes this page draft. The changes become immediately publically visible.
    /// </summary>
    /// <returns>An instance of <see cref="T:Telerik.Sitefinity.Fluent.Pages.StandardPageFacade" /> object.</returns>
    public virtual StandardPageFacade Publish() => this.Publish((CultureInfo) null);

    /// <summary>
    /// Publishes this page draft. The changes become immediately publically visible.
    /// </summary>
    /// <param name="culture">The culture.</param>
    /// <returns>An instance of <see cref="T:Telerik.Sitefinity.Fluent.Pages.StandardPageFacade" /> object.</returns>
    public virtual StandardPageFacade Publish(CultureInfo culture)
    {
      if (this.currentState != null)
      {
        PageData pageData = this.currentState.ParentPage;
        PageNode navigationNode = this.currentState.ParentPage.NavigationNode;
        if (navigationNode != null)
          this.appSettings.TrackPublishedItem((IDataItem) navigationNode);
        this.PageManager.PublishPageDraft(this.currentState, culture.GetCurrent());
        this.Versioning().CreateNewVersion(VersionType.Major, pageData.Id);
        if (pageData.IsPersonalized)
        {
          IQueryable<PageDraft> source1 = this.PageManager.GetPageDataList().Where<PageData>((Expression<Func<PageData, bool>>) (p => p.PersonalizationMasterId == pageData.Id)).SelectMany<PageData, PageDraft>((Expression<Func<PageData, IEnumerable<PageDraft>>>) (p => p.Drafts)).Where<PageDraft>((Expression<Func<PageDraft, bool>>) (p => p.Version > p.ParentPage.Version));
          Expression<Func<PageDraft, Guid>> keySelector = (Expression<Func<PageDraft, Guid>>) (p => p.PersonalizationSegmentId);
          foreach (IEnumerable<PageDraft> source2 in (IEnumerable<IGrouping<Guid, PageDraft>>) source1.GroupBy<PageDraft, Guid>(keySelector))
          {
            PageDraft draft = source2.OrderByDescending<PageDraft, bool>((Func<PageDraft, bool>) (d => d.IsTempDraft)).FirstOrDefault<PageDraft>();
            if (draft != null)
            {
              if (draft.Owner != this.currentState.Owner)
                draft.Owner = this.currentState.Owner;
              this.PageManager.PublishPageDraft(draft, culture.GetCurrent());
              this.Versioning((IDataItem) draft).CreateNewVersion(VersionType.Major, draft.ParentPage.Id);
            }
          }
        }
        if (this.PageManager.DeleteTempAfterPublish)
          this.PageManager.Delete(this.currentState);
        this.currentState = (PageDraft) null;
      }
      return this.parentFacade;
    }

    /// <summary>
    /// Discards the current changes. E.g discards the temp draft
    /// </summary>
    /// <returns>An instance of <see cref="T:Telerik.Sitefinity.Fluent.Pages.StandardPageFacade" /> object.</returns>
    public virtual StandardPageFacade Undo()
    {
      if (this.currentState != null)
        this.PageManager.DiscardPageDraft(this.currentState);
      return this.parentFacade;
    }

    /// <summary>
    /// Undoes the draft. Discards the current master draft and all temporary drafts.
    /// This is used for Discard Draft functionality - when we want to rollback to the published version of the page
    /// </summary>
    /// <returns>An instance of <see cref="T:Telerik.Sitefinity.Fluent.Pages.StandardPageFacade" /> object.</returns>
    public virtual StandardPageFacade UndoDraft()
    {
      if (this.currentState != null)
      {
        PageData pageData = this.currentState.ParentPage;
        List<PageData> pageDataList = new List<PageData>();
        pageDataList.Add(pageData);
        if (pageData.IsPersonalized)
        {
          List<PageData> list = this.PageManager.GetPageDataList().Where<PageData>((Expression<Func<PageData, bool>>) (p => p.PersonalizationMasterId == pageData.Id)).ToList<PageData>();
          pageDataList.AddRange((IEnumerable<PageData>) list);
        }
        foreach (PageData pageData1 in pageDataList)
          this.PageManager.DiscardAllPageDrafts(pageData1.Id);
      }
      return this.parentFacade;
    }

    /// <summary>Saves the changes made to the current page draft.</summary>
    /// <returns>An instance of <see cref="T:Telerik.Sitefinity.Fluent.Pages.StandardPageFacade" /> object.</returns>
    public virtual StandardPageFacade Done() => this.parentFacade;

    /// <summary>
    /// Saves all the changes that were performed through the fluent API operations.
    /// </summary>
    /// <returns>An instance of <see cref="T:Telerik.Sitefinity.Fluent.Pages.PageDraftFacade" /> object.</returns>
    public new virtual bool SaveChanges()
    {
      base.SaveChanges();
      return true;
    }

    /// <summary>
    /// Commit the changes and return the current facade for additional fluent calls
    /// </summary>
    /// <returns>An instance of <see cref="T:Telerik.Sitefinity.Fluent.Pages.PageDraftFacade" /> object.</returns>
    public virtual PageDraftFacade SaveAndContinue()
    {
      this.SaveChanges();
      return this;
    }

    /// <summary>
    /// Cancels all the changes that were performed through the fluent API operations.
    /// </summary>
    /// <returns>An instance of <see cref="T:Telerik.Sitefinity.Fluent.Pages.PageDraftFacade" /> object.</returns>
    public virtual PageDraftFacade CancelChanges()
    {
      base.CancelChanges();
      return this;
    }

    /// <summary>
    /// Returns the query with pages with all currently selected pages.
    /// </summary>
    /// <returns>An instance of IQueryable[PageTaxon] objects.</returns>
    public virtual PageDraft Get() => this.currentState;

    /// <summary>
    /// Ensures that the state of the facade has been initialized and throws an exception if not.
    /// </summary>
    protected virtual void EnsureState()
    {
      if (this.currentState == null)
        throw new InvalidOperationException("This method cannot be executed when the state of the facade is not initialized.");
    }

    /// <summary>
    /// Provides versioning functionality for a specific page draft.
    /// </summary>
    /// <param name="dataItem">The data item.</param>
    /// <returns>An instance of <see cref="!:ItemVersioningFacade[PageDraftFacade]" /> object.</returns>
    private ItemVersioningFacade<PageDraftFacade> Versioning(
      IDataItem dataItem)
    {
      return new ItemVersioningFacade<PageDraftFacade>(dataItem, this, this.appSettings);
    }

    /// <summary>
    /// When the current page has personalizations, this method saves their drafts as well.
    /// </summary>
    private void SavePersonalizedPagesDrafts()
    {
      PageData pageData = this.currentState.ParentPage;
      if (!pageData.IsPersonalized)
        return;
      IQueryable<PageDraft> drafts = this.PageManager.GetDrafts<PageDraft>();
      Expression<Func<PageDraft, bool>> predicate = (Expression<Func<PageDraft, bool>>) (p => p.PersonalizationMasterId == pageData.Id && p.IsTempDraft && p.Version > p.ParentPage.Version);
      foreach (PageDraft draft in (IEnumerable<PageDraft>) drafts.Where<PageDraft>(predicate))
        this.PageManager.SavePageDraft(draft);
    }

    /// <summary>
    /// Create a new instance of the manager in a named transaction using <see cref="!:settings" />
    /// </summary>
    /// <returns>Instance of this facade's manager</returns>
    /// <remarks>This is called internally by <see cref="!:GetManager" />. Do not call this manually unless you override GetManager as well.</remarks>
    protected override IManager InitializeManager() => (IManager) this.PageManager;
  }
}
