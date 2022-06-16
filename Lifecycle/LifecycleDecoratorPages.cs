// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Lifecycle.LifecycleDecoratorPages
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Licensing;
using Telerik.Sitefinity.Model.Localization;
using Telerik.Sitefinity.Modules;
using Telerik.Sitefinity.Modules.Newsletters;
using Telerik.Sitefinity.Modules.Pages;
using Telerik.Sitefinity.Multisite;
using Telerik.Sitefinity.Pages.Model;
using Telerik.Sitefinity.Services;

namespace Telerik.Sitefinity.Lifecycle
{
  public class LifecycleDecoratorPages : LifecycleDecorator<PageData, PageDraft>
  {
    public LifecycleDecoratorPages(PageManager manager)
      : base((ILifecycleManager<PageData, PageDraft>) manager)
    {
    }

    public PageManager PageManager => (PageManager) this.Manager;

    /// <summary>
    /// Checks in the item in the "temp" state. Item becomes master after the check in.
    /// </summary>
    /// <param name="temp"></param>
    /// <param name="culture">The culture in which to perform the operation.
    /// <remarks>In monolingual the culture is ignored.
    /// In multilingual mode if null - the current ui culture will be used.
    /// </remarks></param>
    /// <param name="deleteTemp"></param>
    /// <returns>An item in master state.</returns>
    public override PageDraft CheckIn(
      PageDraft temp,
      CultureInfo culture = null,
      bool deleteTemp = true)
    {
      PageDraft master = base.CheckIn(temp, culture, deleteTemp);
      this.PageManager.SetMasterSynced(master);
      return master;
    }

    /// <summary>
    /// Publishes the content in master state. Item becomes live after the publish.
    /// </summary>
    /// <param name="draft"></param>
    /// <param name="culture">The culture in which to perform the operation.
    /// <remarks>In monolingual the culture is ignored.
    /// In multilingual mode if null - the current ui culture will be used.
    /// </remarks></param>
    /// <returns>Published content item</returns>
    public override PageData Publish(PageDraft draft, CultureInfo culture = null)
    {
      if (draft.ParentPage.IsPersonalizationPage)
        return base.Publish(draft, culture);
      if (draft.ParentPage != null && draft.ParentPage.NavigationNode.RootNodeId != SiteInitializer.BackendRootNodeId)
      {
        if (draft.ParentPage.Visible)
          LicenseLimitations.CanSaveItems(draft.ParentPage.GetType(), 0);
        else
          LicenseLimitations.CanSaveItems(draft.ParentPage.GetType(), 1);
      }
      PageData pageData = base.Publish(draft, culture);
      if (!this.IsPageDraftAnEmailCampaign(draft) && draft.ParentPage != null && draft.ParentPage.NavigationNode != null && !draft.ParentPage.NavigationNode.IsBackend)
      {
        PageNode navigationNode = draft.ParentPage.NavigationNode;
        using (SiteRegion.FromSiteMapRoot(navigationNode.RootNodeId))
        {
          Guid currentHomePageId = this.PageManager.GetHomePageNodeId();
          if (!(currentHomePageId == Guid.Empty))
          {
            if (this.PageManager.GetPageNodes().Any<PageNode>((Expression<Func<PageNode, bool>>) (n => n.Id == currentHomePageId && !n.IsDeleted)))
              goto label_14;
          }
          this.PageManager.SetHomePage(navigationNode.Id);
        }
      }
label_14:
      return pageData;
    }

    /// <summary>
    /// Copies data from the specified draft item to the specified live item.
    /// </summary>
    /// <param name="draftItem">The draft item to get data from.</param>
    /// <param name="liveItem">The live item to wrtie data to.</param>
    /// <param name="culture">The culture of the operation.</param>
    public override void Copy(PageDraft draftItem, PageData liveItem, CultureInfo culture)
    {
      this.PageManager.CopyPageCommonData<PageDraftControl, PageDraftPresentation, PageControl, PagePresentation>((IPageCommonData<PageDraftControl, PageDraftPresentation>) draftItem, (IPageCommonData<PageControl, PagePresentation>) liveItem, culture, culture, CopyDirection.CopyToOriginal);
      liveItem.LastModifiedBy = draftItem.Owner;
      liveItem.LastModified = draftItem.LastModified;
      if (draftItem.TemplateId != Guid.Empty)
      {
        bool suppressSecurityChecks = this.PageManager.Provider.SuppressSecurityChecks;
        this.PageManager.Provider.SuppressSecurityChecks = true;
        PageTemplate template = this.PageManager.GetTemplate(draftItem.TemplateId);
        liveItem.Template = template;
        this.PageManager.Provider.SuppressSecurityChecks = suppressSecurityChecks;
      }
      else
        liveItem.Template = (PageTemplate) null;
    }

    /// <summary>
    /// Copies data from the specified source draft item to the specified target draft item.
    /// </summary>
    /// <param name="sourceDraft">The draft item to get data from.</param>
    /// <param name="targetDraft">The draft item to wrtie data to.</param>
    /// <param name="sourceCulture"></param>
    public override void Copy(
      PageDraft sourceDraft,
      PageDraft targetDraft,
      CultureInfo sourceCulture)
    {
      this.PageManager.CopyTemporaryPage(sourceDraft, targetDraft, sourceCulture);
    }

    /// <summary>
    /// Copies data from the specified live item to the specified draft item.
    /// </summary>
    /// <param name="liveItem">The live item to wrtie data to.</param>
    /// <param name="draftItem">The draft item to get data from.</param>
    /// <param name="culture">The culture of the operation.</param>
    public override void Copy(PageData liveItem, PageDraft draftItem, CultureInfo culture)
    {
      this.PageManager.CopyPageCommonData<PageControl, PagePresentation, PageDraftControl, PageDraftPresentation>((IPageCommonData<PageControl, PagePresentation>) liveItem, (IPageCommonData<PageDraftControl, PageDraftPresentation>) draftItem, culture, culture, CopyDirection.OriginalToCopy);
      draftItem.Owner = liveItem.LastModifiedBy;
      draftItem.LastModified = liveItem.LastModified;
      if (liveItem.Template == null)
        return;
      draftItem.TemplateId = liveItem.Template.Id;
    }

    public override PageDraft GetMaster(PageData liveItem)
    {
      PageDraft master = base.GetMaster(liveItem);
      if (master != null)
        master.ClearFlag(1);
      return master;
    }

    public override PageDraft GetMaster(PageDraft draftItem)
    {
      PageDraft master = base.GetMaster(draftItem);
      if (master != null)
        master.ClearFlag(1);
      return master;
    }

    internal override bool CopyDuringCheckOut(PageDraft master) => !master.Synced;

    public override PageDraft CheckOut(PageDraft masterItem, CultureInfo culture = null)
    {
      PageDraft pageDraft = base.CheckOut(masterItem, culture);
      if (pageDraft.Version < masterItem.Version)
        pageDraft.Version = masterItem.Version;
      return pageDraft;
    }

    public override PageDraft Edit(PageData item, CultureInfo culture = null)
    {
      bool flag = false;
      if (item.Drafts.FirstOrDefault<PageDraft>((Func<PageDraft, bool>) (x => !x.IsTempDraft)) == null)
        flag = true;
      PageDraft pageDraft = base.Edit(item, culture);
      if (flag)
        pageDraft.ClearFlag(1);
      return pageDraft;
    }

    public override void DiscardAllDrafts(PageData liveItem)
    {
      if (((int) (SystemManager.CurrentHttpContext.Items[(object) "OptimizedCopy"] as bool?) ?? 0) != 0)
      {
        CultureInfo sitefinityCulture = ((CultureInfo) null).GetSitefinityCulture();
        PageDraft pageDraft = liveItem.Drafts.FirstOrDefault<PageDraft>((Func<PageDraft, bool>) (x => !x.IsTempDraft));
        PageDraft targetDraft = liveItem.Drafts.FirstOrDefault<PageDraft>((Func<PageDraft, bool>) (x => x.IsTempDraft));
        this.Copy(liveItem, pageDraft, sitefinityCulture);
        LanguageData languageData1 = this.GetOrCreateLanguageData((ILifecycleDataItem) liveItem, sitefinityCulture);
        LanguageData languageData2 = this.GetOrCreateLanguageData((ILifecycleDataItem) pageDraft, sitefinityCulture);
        languageData2.CopyFrom(languageData1);
        this.Copy(pageDraft, targetDraft, sitefinityCulture);
        this.GetOrCreateLanguageData((ILifecycleDataItem) pageDraft, sitefinityCulture).CopyFrom(languageData2);
        this.PageManager.UnlockPage(liveItem.Id, false, true);
      }
      else
        base.DiscardAllDrafts(liveItem);
    }

    /// <summary>
    /// Detects if the page draft is actually an email campaign or a campaign template draft.
    /// </summary>
    /// <param name="draft">The draft to check</param>
    /// <returns>True if the page draft is actually an email campaign or a campaign template draft; False otherwise.</returns>
    private bool IsPageDraftAnEmailCampaign(PageDraft draft) => draft.ParentPage != null && draft.ParentPage.NavigationNode != null && draft.ParentPage.NavigationNode.RootNode != null && draft.ParentPage.NavigationNode.RootNode.Id == NewslettersModule.standardCampaignRootNodeId;
  }
}
