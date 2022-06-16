// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.SiteSync.PagesImporter
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using Telerik.Sitefinity.ContentLocations;
using Telerik.Sitefinity.ContentLocations.Model;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.DynamicModules.Web.UI.Frontend;
using Telerik.Sitefinity.Fluent;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.Modules.Pages;
using Telerik.Sitefinity.Multisite;
using Telerik.Sitefinity.Pages.Model;
using Telerik.Sitefinity.Publishing;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Taxonomies.Model;
using Telerik.Sitefinity.Utilities.TypeConverters;

namespace Telerik.Sitefinity.SiteSync
{
  internal class PagesImporter : SiteSyncImporter
  {
    private List<ContentLocationDataItem> locationsCache = new List<ContentLocationDataItem>();
    private Guid homePageId;
    private PageNode currentProcessedPageNode;
    private PageTemplate currentProcessedTemplate;

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.SiteSync.PagesImporter" /> class.
    /// </summary>
    public PagesImporter()
      : this((string) null)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.SiteSync.PagesImporter" /> class.
    /// </summary>
    /// <param name="registrationPrefix">The registration prefix.</param>
    public PagesImporter(string registrationPrefix) => this.RegistrationPrefix = registrationPrefix;

    public override void Import(ISiteSyncImportTransaction transaction)
    {
      this.ClearTemporaryData();
      base.Import(transaction);
      this.ClearTemporaryData();
    }

    internal void ClearTemporaryData()
    {
      this.homePageId = Guid.Empty;
      this.currentProcessedPageNode = (PageNode) null;
      this.currentProcessedTemplate = (PageTemplate) null;
      this.locationsCache.Clear();
    }

    protected override void OnTransactionComitted()
    {
      if (this.currentProcessedPageNode != null && this.homePageId != Guid.Empty)
      {
        SitefinityContextBase currentContext = SystemManager.CurrentContext;
        IMultisiteContext multisiteContext = SystemManager.CurrentContext.MultisiteContext;
        ISite site = multisiteContext == null ? currentContext.CurrentSite : multisiteContext.GetSiteBySiteMapRoot(this.currentProcessedPageNode.RootNodeId);
        using (new SiteRegion(site))
        {
          if (this.currentProcessedPageNode.Id == this.homePageId)
            site.SetHomePage(this.homePageId);
        }
      }
      base.OnTransactionComitted();
    }

    [SuppressMessage("StyleCopPlus.StyleCopPlusRules", "SP2101:MethodMustNotContainMoreLinesThan", Justification = "Ignored so that the file can be included in StyleCop")]
    internal override void ImportItemInternal(
      string transactionName,
      Type itemType,
      Guid itemId,
      WrapperObject item,
      string provider,
      ISiteSyncImportTransaction importTransaction,
      Action<IDataItem, WrapperObject, IManager> postProcessingAction)
    {
      if (itemType == typeof (ContentLocationDataItem))
      {
        ContentLocationsManager manager = ContentLocationsManager.GetManager(string.Empty, transactionName);
        ContentLocationDataItem location = manager.GetLocations().Where<ContentLocationDataItem>((Expression<Func<ContentLocationDataItem, bool>>) (l => l.Id == itemId)).FirstOrDefault<ContentLocationDataItem>();
        if (location == null)
          location = manager.CreateLocation(itemId);
        this.Serializer.SetProperties((object) location, (object) item);
        this.locationsCache.Add(location);
        this.itemsToRemove.RemoveAll((Predicate<object>) (i => i is ContentLocationDataItem && ((ContentLocationDataItem) i).Id == location.Id));
      }
      else if (itemType == typeof (ContentLocationFilterDataItem))
      {
        Guid locationId = item.GetProperty<Guid>("ContentLocationId");
        ContentLocationsManager manager = ContentLocationsManager.GetManager(string.Empty, transactionName);
        ContentLocationDataItem locationDataItem = this.locationsCache.FirstOrDefault<ContentLocationDataItem>((Func<ContentLocationDataItem, bool>) (l => l.Id == locationId));
        if (locationDataItem == null)
          locationDataItem = manager.GetLocations().Where<ContentLocationDataItem>((Expression<Func<ContentLocationDataItem, bool>>) (l => l.Id == locationId)).FirstOrDefault<ContentLocationDataItem>();
        if (locationDataItem == null)
          throw new ArgumentNullException(string.Format("The ContentLocation for filter with id '{0}' can't be found!", (object) itemId));
        ContentLocationFilterDataItem filter = manager.GetContentFilters().Where<ContentLocationFilterDataItem>((Expression<Func<ContentLocationFilterDataItem, bool>>) (f => f.Id == itemId)).FirstOrDefault<ContentLocationFilterDataItem>();
        if (filter == null)
          filter = manager.CreateContentFilter(itemId);
        filter.ContentLocation = locationDataItem;
        this.Serializer.SetProperties((object) filter, (object) item);
        this.itemsToRemove.RemoveAll((Predicate<object>) (i => i is ContentLocationFilterDataItem && ((ContentLocationFilterDataItem) i).Id == filter.Id));
      }
      else
      {
        if (itemType == typeof (ControlPresentation))
        {
          string embeddedTemplateName = item.GetProperty<string>("EmbeddedTemplateName");
          string controlType = item.GetProperty<string>("ControlType");
          if (!string.IsNullOrEmpty(embeddedTemplateName))
          {
            if (PageManager.GetManager(provider, transactionName).GetPresentationItems<ControlPresentation>().Where<ControlPresentation>((Expression<Func<ControlPresentation, bool>>) (p => p.ControlType == controlType && p.EmbeddedTemplateName == embeddedTemplateName && p.Id != itemId)).Any<ControlPresentation>())
            {
              if (string.IsNullOrEmpty(item.GetProperty<string>("Data")))
                return;
              item.SetProperty("EmbeddedTemplateName", (object) null);
            }
          }
          this.RemoveExistingDynamicModuleControlPresentation(item, provider, transactionName, itemId);
        }
        base.ImportItemInternal(transactionName, itemType, itemId, item, provider, importTransaction, postProcessingAction);
        this.locationsCache.Clear();
      }
    }

    private void RemoveExistingDynamicModuleControlPresentation(
      WrapperObject item,
      string provider,
      string transactionName,
      Guid itemId)
    {
      string controlType = item.GetProperty<string>("ControlType");
      if (!(controlType == typeof (DynamicContentViewMaster).FullName) && !(controlType == typeof (DynamicContentViewDetail).FullName))
        return;
      string templateName = item.GetProperty<string>("Name");
      string areaName = item.GetProperty<string>("AreaName");
      string moduleTypeFullName = item.GetProperty<string>("Condition");
      PageManager manager = PageManager.GetManager(provider, transactionName);
      ControlPresentation controlPresentation = manager.GetPresentationItems<ControlPresentation>().Where<ControlPresentation>((Expression<Func<ControlPresentation, bool>>) (p => p.ControlType == controlType && p.Name == templateName && p.AreaName == areaName && p.Condition == moduleTypeFullName && p.Id != itemId)).FirstOrDefault<ControlPresentation>();
      if (controlPresentation == null)
        return;
      manager.Delete((PresentationData) controlPresentation);
    }

    protected override void ValidateDataItemConstraints(
      IDataItem dataItem,
      IManager manager,
      ISiteSyncImportTransaction importTransaction)
    {
      if (!(dataItem is PageTemplate) || importTransaction.Headers.ContainsKey("MultisiteMigrationTarget"))
        return;
      PageTemplate pageTemplate = dataItem as PageTemplate;
      ((PageManager) manager).ValidateTemplateConstraints((string) pageTemplate.Title, pageTemplate.Id, pageTemplate.Category);
    }

    protected override void ImportItem(
      string transactionName,
      Type itemType,
      Guid itemId,
      WrapperObject item,
      string provider,
      ISiteSyncImportTransaction importTransaction)
    {
      if (itemType == typeof (TaxonomyStatistic))
      {
        this.ImportTaxonomyStatistic(transactionName, itemId, (object) item, provider);
      }
      else
      {
        this.ImportItemInternal(transactionName, itemType, itemId, item, provider, importTransaction, (Action<IDataItem, WrapperObject, IManager>) null);
        this.PrepareStatisticsToRemove(transactionName, itemType, itemId, provider);
      }
    }

    protected override void RemoveDataItem(
      string transactionName,
      Type itemType,
      Guid itemId,
      string provider,
      string language)
    {
      if (itemType == typeof (ControlPresentation))
      {
        FluentSitefinity fluent = App.Prepare().SetContentProvider(provider).SetTransactionName(transactionName).WorkWith();
        Telerik.Sitefinity.Fluent.IDataItemFacade.IDataItemFacade facade = this.GetFacade(itemType, fluent, provider);
        if (!facade.Exists(itemId))
          return;
        facade.Load(itemId).Delete();
      }
      else
      {
        if (itemType == typeof (PageTemplate))
        {
          PageTemplate template = PageManager.GetManager().GetTemplates().Where<PageTemplate>((Expression<Func<PageTemplate, bool>>) (t => t.Id == itemId)).FirstOrDefault<PageTemplate>();
          if (template != null)
          {
            int num = template.GetPageDataBasedOnTemplate().Count<PageData>();
            if (num > 0)
              throw new InvalidOperationException(string.Format("Template cannot be deleted since {0} pages are based on it.", (object) num));
          }
        }
        base.RemoveDataItem(transactionName, itemType, itemId, provider, language);
      }
    }

    protected override void SetAdditionalValues(
      IDataItem dataItem,
      string provider,
      WrapperObject wrapperObject,
      FluentSitefinity fluent,
      ISiteSyncImportTransaction importTransaction)
    {
      string transactionName = fluent.AppSettings.TransactionName;
      PageManager manager = PageManager.GetManager(string.Empty, transactionName);
      this.ProcessDataItem(dataItem, provider, wrapperObject, fluent, importTransaction);
      this.SetPageNodeProperties(dataItem, wrapperObject, manager);
      this.SetPageDataProperties(dataItem, wrapperObject, manager);
      this.ProcessContentLocations(dataItem, transactionName);
      base.SetAdditionalValues(dataItem, provider, wrapperObject, fluent, importTransaction);
      if (dataItem is IRendererCommonData rendererCommonData)
      {
        if (wrapperObject.GetPropertyOrNull("Renderer") is string propertyOrNull1)
          rendererCommonData.Renderer = propertyOrNull1;
        if (wrapperObject.GetPropertyOrNull("TemplateName") is string propertyOrNull2)
          rendererCommonData.TemplateName = propertyOrNull2;
      }
      if (!(dataItem is IFlagsContainer flagsContainer))
        return;
      int propertyOrDefault = (int) wrapperObject.GetPropertyOrDefault<long>("Flags");
      if (propertyOrDefault <= 0)
        return;
      flagsContainer.Flags = propertyOrDefault;
    }

    protected override bool ShouldProcessCustomParent(Type itemType) => itemType == typeof (PageNode);

    protected override void ProcessCustomParent(IDataItem item, IDataItem parent, IManager manager)
    {
      if (!(item.GetType() == typeof (PageNode)))
        return;
      PageNode childNode = (PageNode) item;
      if (parent == null && childNode.Parent == null)
        return;
      ((PageManager) manager).ChangeParent(childNode, (PageNode) parent);
    }

    internal override void RemoveUnnecessaryItem(
      object item,
      string transactionName,
      bool retrieveItemBeforeDelete = false)
    {
      ContentLocationsManager manager = ContentLocationsManager.GetManager(string.Empty, transactionName);
      ContentLocationDataItem contentLocationDataItem = item as ContentLocationDataItem;
      if (contentLocationDataItem != null)
      {
        if (retrieveItemBeforeDelete)
          contentLocationDataItem = manager.GetLocations().Where<ContentLocationDataItem>((Expression<Func<ContentLocationDataItem, bool>>) (c => c.Id == contentLocationDataItem.Id)).FirstOrDefault<ContentLocationDataItem>();
        if (retrieveItemBeforeDelete && contentLocationDataItem == null)
          return;
        manager.Delete(contentLocationDataItem);
      }
      else
      {
        ContentLocationFilterDataItem contentLocationFilterDataItem = item as ContentLocationFilterDataItem;
        if (contentLocationFilterDataItem != null)
        {
          if (retrieveItemBeforeDelete)
            contentLocationFilterDataItem = manager.GetContentFilters().Where<ContentLocationFilterDataItem>((Expression<Func<ContentLocationFilterDataItem, bool>>) (c => c.Id == contentLocationFilterDataItem.Id)).FirstOrDefault<ContentLocationFilterDataItem>();
          if (retrieveItemBeforeDelete && contentLocationFilterDataItem == null)
            return;
          manager.Delete(contentLocationFilterDataItem);
        }
        else
        {
          if (item is ControlData controlData && controlData.PersonalizationSegmentId != Guid.Empty)
            TransactionManager.FlushTransaction(transactionName);
          base.RemoveUnnecessaryItem(item, transactionName, retrieveItemBeforeDelete);
        }
      }
    }

    protected override void SetFacadeManager(Type itemType, Telerik.Sitefinity.Fluent.IDataItemFacade.IDataItemFacade facade)
    {
      if (typeof (ControlData).IsAssignableFrom(itemType) || typeof (ControlProperty).IsAssignableFrom(itemType) || typeof (PageDraft).IsAssignableFrom(itemType) || typeof (ObjectData).IsAssignableFrom(itemType) || typeof (PageTemplate).IsAssignableFrom(itemType) || typeof (TemplateDraft).IsAssignableFrom(itemType))
        facade.SetManagerType(typeof (PageManager));
      else if (typeof (ContentLocationDataItem).IsAssignableFrom(itemType) || typeof (ContentLocationFilterDataItem).IsAssignableFrom(itemType))
        facade.SetManagerType(typeof (ContentLocationsManager));
      else
        base.SetFacadeManager(itemType, facade);
    }

    internal virtual void SetPageDataProperties(
      IDataItem dataItem,
      WrapperObject wrapperObject,
      PageManager manager)
    {
      if (!(dataItem is PageData pageData) || pageData.NavigationNode != null && pageData.NavigationNode.IsSplitPage)
        return;
      pageData.PublishedTranslations.Clear();
      object propertyOrNull = wrapperObject.GetPropertyOrNull("PublishedTranslations");
      if (propertyOrNull == null)
        return;
      if (propertyOrNull.GetType() == typeof (string))
      {
        pageData.PublishedTranslations.Add((string) propertyOrNull);
      }
      else
      {
        foreach (string str in (IEnumerable<string>) propertyOrNull)
          pageData.PublishedTranslations.Add(str);
      }
    }

    private void SetPageNodeProperties(
      IDataItem dataItem,
      WrapperObject wrapperObject,
      PageManager manager)
    {
      if (!(dataItem is PageNode node))
        return;
      object propertyOrNull1 = wrapperObject.GetPropertyOrNull("HomePageId");
      if (propertyOrNull1 != null)
      {
        this.homePageId = (Guid) propertyOrNull1;
        this.currentProcessedPageNode = (PageNode) dataItem;
      }
      object propertyOrNull2 = wrapperObject.GetPropertyOrNull("SitemapPriority");
      if (propertyOrNull2 != null)
        node.Priority = (float) propertyOrNull2;
      if (node.NodeType != NodeType.InnerRedirect && node.NodeType != NodeType.OuterRedirect && node.NodeType != NodeType.Group)
        return;
      node.RemoveAssociatedPageDataObjects(manager);
    }

    [SuppressMessage("StyleCopPlus.StyleCopPlusRules", "SP2101:MethodMustNotContainMoreLinesThan", Justification = "Ignored so that the file can be included in StyleCop")]
    protected virtual void ProcessDataItem(
      IDataItem dataItem,
      string provider,
      WrapperObject wrapperObject,
      FluentSitefinity fluent,
      ISiteSyncImportTransaction importTransaction)
    {
      switch (dataItem)
      {
        case PageData _:
          PageData pageData = (PageData) dataItem;
          object propertyOrNull1 = wrapperObject.GetPropertyOrNull("PageTemplateId");
          if (propertyOrNull1 != null)
          {
            PageTemplate pageTemplate = (PageTemplate) fluent.DataItemFacade(typeof (PageTemplate), (Guid) propertyOrNull1).Get();
            pageData.Template = pageTemplate;
          }
          else
            pageData.Template = (PageTemplate) null;
          object propertyOrNull2 = wrapperObject.GetPropertyOrNull("PageNodeId");
          if (propertyOrNull2 != null)
          {
            PageNode pageNode = (PageNode) fluent.DataItemFacade(typeof (PageNode), (Guid) propertyOrNull2).Get();
            pageData.NavigationNode = pageNode;
          }
          this.itemsToRemove.AddRange((IEnumerable<object>) (fluent.Page().GetManager() as PageManager).GetPageDataList().Where<PageData>((Expression<Func<PageData, bool>>) (p => p.PersonalizationMasterId == pageData.Id)));
          this.itemsToRemove.AddRange((IEnumerable<object>) pageData.Controls.SelectMany<PageControl, ControlData>((Func<PageControl, IEnumerable<ControlData>>) (c => (IEnumerable<ControlData>) c.PersonalizedControls)));
          this.itemsToRemove.AddRange((IEnumerable<object>) pageData.Controls);
          this.itemsToRemove.AddRange((IEnumerable<object>) pageData.Drafts);
          break;
        case PageDraft _:
          PageDraft pageDraft1 = (PageDraft) dataItem;
          Guid property = wrapperObject.GetProperty<Guid>("ParentId");
          PageData pageData1 = (PageData) fluent.DataItemFacade(typeof (PageData), property).Get();
          pageDraft1.ParentPage = pageData1;
          this.itemsToRemove.AddRange((IEnumerable<object>) pageDraft1.Controls);
          this.itemsToRemove.AddRange((IEnumerable<object>) pageDraft1.Controls.SelectMany<PageDraftControl, ControlData>((Func<PageDraftControl, IEnumerable<ControlData>>) (c => (IEnumerable<ControlData>) c.PersonalizedControls)));
          break;
        case PageTemplate _:
          PageTemplate template = (PageTemplate) dataItem;
          this.currentProcessedTemplate = template;
          this.itemsToRemove.AddRange((IEnumerable<object>) template.Controls.SelectMany<TemplateControl, ControlData>((Func<TemplateControl, IEnumerable<ControlData>>) (c => (IEnumerable<ControlData>) c.PersonalizedControls)));
          this.itemsToRemove.AddRange((IEnumerable<object>) template.Controls);
          this.itemsToRemove.AddRange((IEnumerable<object>) template.Drafts);
          List<Guid> propertyOrDefault1 = wrapperObject.GetPropertyOrDefault<List<Guid>>("SiteIds");
          if (propertyOrDefault1 != null)
            this.LinkItemToSites((IDataItem) template, (IList<Guid>) propertyOrDefault1, fluent);
          string transactionName = fluent.Page().GetManager().TransactionName;
          string propertyOrDefault2 = wrapperObject.GetPropertyOrDefault<string>("ThumbnailTitle");
          if (propertyOrDefault2 != null)
            PageTemplateHelper.RelateThumbnailImage(template, propertyOrDefault2, transactionName);
          string propertyOrDefault3 = wrapperObject.GetPropertyOrDefault<string>("PageTemplateTheme");
          if (propertyOrDefault3 == null)
            break;
          template.Themes.SetString(CultureInfo.InvariantCulture, propertyOrDefault3);
          break;
        case ControlProperty _:
          ControlProperty controlProperty = (ControlProperty) dataItem;
          controlProperty.ChildProperties.Clear();
          string propertyOrDefault4 = wrapperObject.GetPropertyOrDefault<string>("ParentControlPropertyType");
          object propertyOrNull3 = wrapperObject.GetPropertyOrNull("ParentControlPropertyId");
          if (propertyOrDefault4 != null && propertyOrNull3 != null)
          {
            IDataItem dataItem1 = this.GetFacade(TypeResolutionService.ResolveType(propertyOrDefault4), fluent, provider).Load((Guid) propertyOrNull3).Get();
            controlProperty.ParentProperty = (ControlProperty) dataItem1;
          }
          string propertyOrDefault5 = wrapperObject.GetPropertyOrDefault<string>("ParentControlType");
          object propertyOrNull4 = wrapperObject.GetPropertyOrNull("ParentControlId");
          bool flag = importTransaction.Headers.ContainsKey("MultisiteMigrationTarget");
          if (propertyOrDefault5 != null && propertyOrNull4 != null)
          {
            ObjectData objectData = (ObjectData) this.GetFacade(TypeResolutionService.ResolveType(propertyOrDefault5), fluent, provider).Load((Guid) propertyOrNull4).Get();
            controlProperty.Control = objectData;
            if (flag && objectData.ObjectType == "Telerik.Sitefinity.Modules.GenericContent.Web.UI.ContentBlock" && controlProperty.Name == "Html")
              this.TrySetMigratedValue(wrapperObject, controlProperty);
          }
          if (!(wrapperObject.GetPropertyOrDefault<string>("IsMvcContentBlock") == "true" & flag))
            break;
          this.TrySetMigratedValue(wrapperObject, controlProperty);
          break;
        case ObjectData _:
          ((ObjectData) dataItem).Properties.Clear();
          string propertyOrDefault6 = wrapperObject.GetPropertyOrDefault<string>("ParentControlPropertyType");
          object propertyOrNull5 = wrapperObject.GetPropertyOrNull("ParentControlPropertyId");
          if (propertyOrDefault6 != null && propertyOrNull5 != null)
          {
            IDataItem dataItem2 = this.GetFacade(TypeResolutionService.ResolveType(propertyOrDefault6), fluent, provider).Load((Guid) propertyOrNull5).Get();
            ((ObjectData) dataItem).ParentProperty = (ControlProperty) dataItem2;
          }
          Guid propertyOrDefault7 = wrapperObject.GetPropertyOrDefault<Guid>("ParentObjectDataId");
          ((ObjectData) dataItem).ParentId = propertyOrDefault7;
          object propertyOrNull6 = wrapperObject.GetPropertyOrNull("ParentTemplateId");
          if (propertyOrNull6 != null)
          {
            TemplateControl templateControl = (TemplateControl) dataItem;
            if (this.currentProcessedTemplate != null)
            {
              templateControl.Page = this.currentProcessedTemplate;
            }
            else
            {
              PageTemplate pageTemplate = this.GetFacade(typeof (PageTemplate), fluent, provider).Load((Guid) propertyOrNull6).Get() as PageTemplate;
              templateControl.Page = pageTemplate;
            }
          }
          if (dataItem is PageDraftControl)
          {
            object propertyOrNull7 = wrapperObject.GetPropertyOrNull("PageDraftId");
            if (propertyOrNull7 != null)
            {
              PageDraft pageDraft2 = (PageDraft) this.GetFacade(typeof (PageDraft), fluent, provider).Load((Guid) propertyOrNull7).Get();
              ((PageDraftControl) dataItem).Page = pageDraft2;
            }
          }
          if (!(dataItem is ControlData))
            break;
          ControlData ctrlData = (ControlData) dataItem;
          if (!importTransaction.Headers.ContainsKey("DisablePermissionsSync"))
            break;
          (fluent.Page().GetManager() as PageManager).SetControlDefaultPermissions(ctrlData);
          break;
        case ControlPresentation _:
          ControlPresentation controlPresentation = (ControlPresentation) dataItem;
          List<Guid> propertyOrDefault8 = wrapperObject.GetPropertyOrDefault<List<Guid>>("SiteIds");
          if (propertyOrDefault8 == null)
            break;
          this.LinkItemToSites((IDataItem) controlPresentation, (IList<Guid>) propertyOrDefault8, fluent);
          break;
      }
    }

    private void TrySetMigratedValue(WrapperObject wrapperObject, ControlProperty controlProperty)
    {
      string propertyOrDefault = wrapperObject.GetPropertyOrDefault<string>("migratedContent");
      if (propertyOrDefault == null)
        return;
      controlProperty.Value = propertyOrDefault;
    }

    private void ProcessContentLocations(IDataItem page, string transactionName)
    {
      PageNode pageItem = page as PageNode;
      if (pageItem == null)
        return;
      ContentLocationsManager manager = ContentLocationsManager.GetManager(string.Empty, transactionName);
      IQueryable<ContentLocationDataItem> locations = manager.GetLocations();
      Expression<Func<ContentLocationDataItem, bool>> predicate1 = (Expression<Func<ContentLocationDataItem, bool>>) (l => l.PageId == pageItem.Id);
      foreach (ContentLocationDataItem locationDataItem in (IEnumerable<ContentLocationDataItem>) locations.Where<ContentLocationDataItem>(predicate1))
      {
        ContentLocationDataItem item = locationDataItem;
        this.itemsToRemove.Add((object) item);
        IQueryable<ContentLocationFilterDataItem> contentFilters = manager.GetContentFilters();
        Expression<Func<ContentLocationFilterDataItem, bool>> predicate2 = (Expression<Func<ContentLocationFilterDataItem, bool>>) (f => f.ContentLocation.Id == item.Id);
        foreach (object obj in (IEnumerable<ContentLocationFilterDataItem>) contentFilters.Where<ContentLocationFilterDataItem>(predicate2))
          this.itemsToRemove.Add(obj);
      }
    }

    protected override void ClearSchedulingRemains(
      IManager manager,
      Type itemType,
      Guid itemId,
      WrapperObject item)
    {
      if (!typeof (PageNode).IsAssignableFrom(itemType))
        return;
      this.RemoveWorkflowTask(itemId.ToString(), manager.TransactionName);
    }
  }
}
