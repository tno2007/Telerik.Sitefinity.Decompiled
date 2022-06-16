// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Pages.PagesContentTransfer
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.ContentLocations;
using Telerik.Sitefinity.ContentLocations.Model;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.Forms.Model;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.Multisite;
using Telerik.Sitefinity.Packaging;
using Telerik.Sitefinity.Packaging.Content;
using Telerik.Sitefinity.Packaging.Model;
using Telerik.Sitefinity.Pages.Model;
using Telerik.Sitefinity.Publishing;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.SiteSync;
using Telerik.Sitefinity.Utilities.TypeConverters;

namespace Telerik.Sitefinity.Modules.Pages
{
  /// <summary>Processes import/export of page content items</summary>
  internal class PagesContentTransfer : PageContentTransferBase
  {
    private const string AreaName = "Pages";
    private Guid homePageId = Guid.Empty;
    private IEnumerable<ExportType> supportedTypes;
    private IList<ContentLocationDataItem> contentLocations = (IList<ContentLocationDataItem>) new List<ContentLocationDataItem>();

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Modules.Pages.PagesContentTransfer" /> class.
    /// </summary>
    public PagesContentTransfer()
    {
      this.AddDependencies(typeof (PageNode).FullName, typeof (PageTemplate).FullName);
      this.AddDependencies(typeof (PageNode).FullName, typeof (FormDescription).FullName);
    }

    /// <inheritdoc />
    public override string Area => "Pages";

    /// <inheritdoc />
    public override IEnumerable<ExportType> SupportedTypes
    {
      get
      {
        if (this.supportedTypes == null)
        {
          IList<ExportType> exportTypeList = (IList<ExportType>) new List<ExportType>();
          ExportType exportType = new ExportType("Pages", typeof (PageNode).FullName);
          exportTypeList.Add(exportType);
          this.supportedTypes = (IEnumerable<ExportType>) exportTypeList;
        }
        return this.supportedTypes;
      }
    }

    /// <inheritdoc />
    public override void CreateItem(WrapperObject obj, string transactionName)
    {
      this.PrepareItemForImport(obj, transactionName);
      Type type = TypeResolutionService.ResolveType(obj.GetPropertyOrDefault<string>("objectTypeId"), false);
      if (!typeof (ContentLocationDataItem).IsAssignableFrom(type))
      {
        (this.ItemsImporter as PagesImporter).ClearTemporaryData();
        string propertyOrDefault1 = obj.GetPropertyOrDefault<string>("Provider");
        Guid propertyOrDefault2 = obj.GetPropertyOrDefault<Guid>("objectId");
        this.ItemsImporter.ImportItemInternal(transactionName, type, propertyOrDefault2, obj, propertyOrDefault1, (ISiteSyncImportTransaction) new SiteSyncImportTransaction(), new Action<IDataItem, WrapperObject, IManager>(((ContentTransferBase) this).OnItemImported));
      }
      else
      {
        try
        {
          this.StoreContentLocationDataItem(obj);
        }
        catch (Exception ex)
        {
          Exceptions.HandleException(ex, ExceptionPolicyName.Global);
        }
      }
    }

    /// <inheritdoc />
    public override IEnumerable<IQueryable<object>> GetItemsQueries(
      ExportParams settings)
    {
      PageManager pageManager = PageManager.GetManager(settings.ProviderName);
      Queue<IQueryable<PageNode>> queue = new Queue<IQueryable<PageNode>>((IEnumerable<IQueryable<PageNode>>) new IQueryable<PageNode>[1]
      {
        this.GetChildPages(pageManager, SiteInitializer.CurrentFrontendRootNodeId)
      });
      while (queue.Any<IQueryable<PageNode>>())
      {
        IQueryable<PageNode> next = queue.Dequeue();
        yield return (IQueryable<object>) next;
        IQueryable<PageNode> source = next;
        Expression<Func<PageNode, Guid>> selector = (Expression<Func<PageNode, Guid>>) (pn => pn.Id);
        foreach (Guid parentId in (IEnumerable<Guid>) source.Select<PageNode, Guid>(selector))
          queue.Enqueue(this.GetChildPages(pageManager, parentId));
        next = (IQueryable<PageNode>) null;
      }
    }

    /// <inheritdoc />
    public override void Delete(string sourceName)
    {
      PackagingManager manager = PackagingManager.GetManager();
      Addon addon = manager.GetAddons().FirstOrDefault<Addon>((Expression<Func<Addon, bool>>) (a => a.Name == sourceName));
      if (addon == null)
        return;
      List<Guid> list = manager.GetAddonLinks().Where<AddonLink>((Expression<Func<AddonLink, bool>>) (l => l.AddonId == addon.Id && l.ItemType == typeof (PageNode).FullName)).Select<AddonLink, Guid>((Expression<Func<AddonLink, Guid>>) (i => i.ItemId)).ToList<Guid>();
      this.DeleteItems(typeof (PageManager), typeof (PageNode), string.Empty, (IList<Guid>) list);
    }

    /// <inheritdoc />
    protected override void DeleteItems(
      Type managerTypeName,
      Type itemType,
      string provider,
      IList<Guid> itemIds)
    {
      List<Guid> frontendRootNodeIds = new List<Guid>();
      IMultisiteContext multisiteContext = SystemManager.CurrentContext.MultisiteContext;
      if (multisiteContext != null)
      {
        foreach (ISite site in multisiteContext.GetSites())
          frontendRootNodeIds.Add(site.SiteMapRootNodeId);
      }
      else
        frontendRootNodeIds.Add(SiteInitializer.CurrentFrontendRootNodeId);
      IManager manager = ManagerBase.GetManager(managerTypeName, provider);
      foreach (PageNode pageNode in manager.GetItems(itemType, (string) null, (string) null, 0, 0).Cast<PageNode>().Where<PageNode>((Func<PageNode, bool>) (m => frontendRootNodeIds.Contains(m.RootNodeId) && itemIds.Contains(m.Id))))
        manager.DeleteItem((object) pageNode);
      manager.SaveChanges();
    }

    /// <inheritdoc />
    protected override void PrepareItemForExport(WrapperObject obj, Type itemType, object item)
    {
      if (typeof (PageNode).IsAssignableFrom(itemType))
      {
        Guid property = obj.GetProperty<Guid>("RootNodeId");
        if (obj.GetProperty<Guid>("ParentId") == property)
          obj.AdditionalProperties["ParentId"] = (object) Guid.Empty;
        obj.AdditionalProperties["RootNodeId"] = (object) Guid.Empty;
      }
      base.PrepareItemForExport(obj, itemType, item);
    }

    /// <inheritdoc />
    protected override void PrepareItemForImport(WrapperObject obj, string transactionName)
    {
      Guid frontendRootNodeId = SiteInitializer.CurrentFrontendRootNodeId;
      Type c = TypeResolutionService.ResolveType(obj.GetPropertyOrDefault<string>("objectTypeId"), false);
      if (typeof (PageNode).IsAssignableFrom(c))
      {
        obj.SetOrAddProperty("RootNodeId", (object) frontendRootNodeId);
        if (obj.GetProperty<Guid>("ParentId") == Guid.Empty)
          obj.SetProperty("ParentId", (object) frontendRootNodeId);
        if (!(this.homePageId == Guid.Empty))
          return;
        Guid propertyOrDefault = obj.GetPropertyOrDefault<Guid>("HomePageId");
        if (!(propertyOrDefault != new Guid()))
          return;
        this.homePageId = propertyOrDefault;
      }
      else if (typeof (PageData).IsAssignableFrom(c))
      {
        if (!this.IsAssociatedTemplateDefaultFeatherTemplate(obj))
          return;
        PageManager manager = PageManager.GetManager((string) null, transactionName);
        PageTemplate defaultFeatherTemplate = this.GetDefaultFeatherTemplate(obj, manager);
        if (defaultFeatherTemplate != null)
          obj.SetOrAddProperty("PageTemplateId", (object) defaultFeatherTemplate.Id);
        else
          this.DeleteProperty(obj, "PageTemplateId");
      }
      else
        base.PrepareItemForImport(obj, transactionName);
    }

    /// <inheritdoc />
    protected override void OnItemImported(IDataItem dataItem, WrapperObject obj, IManager manager)
    {
      base.OnItemImported(dataItem, obj, manager);
      if (!(dataItem is PageData) || !(manager is PageManager))
        return;
      PageManager pageManager = manager as PageManager;
      PageData liveItem = dataItem as PageData;
      liveItem.Controls.Clear();
      pageManager.PagesLifecycle.DiscardAllDrafts(liveItem);
    }

    /// <inheritdoc />
    protected override void OnImportStart(ImportParams parameters)
    {
      this.contentLocations = (IList<ContentLocationDataItem>) new List<ContentLocationDataItem>();
      PageManager manager = PageManager.GetManager();
      Guid homePageId = SystemManager.CurrentContext.CurrentSite.HomePageId;
      if (!manager.GetPageNodes().Any<PageNode>((Expression<Func<PageNode, bool>>) (p => p.Id == homePageId)))
        return;
      this.homePageId = homePageId;
    }

    /// <inheritdoc />
    protected override void OnImportComplete(ImportParams parameters)
    {
      PageManager manager = PageManager.GetManager();
      if (manager.GetPageNodes().Any<PageNode>((Expression<Func<PageNode, bool>>) (p => p.Id == this.homePageId)))
      {
        SystemManager.CurrentContext.CurrentSite.SetHomePage(this.homePageId);
      }
      else
      {
        Guid frontEndRootNodeId = SiteInitializer.CurrentFrontendRootNodeId;
        PageNode pageNode = manager.GetPageNodes().FirstOrDefault<PageNode>((Expression<Func<PageNode, bool>>) (p => p.ParentId == frontEndRootNodeId));
        if (pageNode != null)
          SystemManager.CurrentContext.CurrentSite.SetHomePage(pageNode.Id);
      }
      try
      {
        this.UpdateContentLocationsPriority();
      }
      catch (Exception ex)
      {
        Exceptions.HandleException(ex, ExceptionPolicyName.Global);
      }
    }

    /// <inheritdoc />
    protected override void OnImportTransactionCommitting(WrapperObject obj, string transactionName)
    {
      base.OnImportTransactionCommitting(obj, transactionName);
      string language = obj.GetProperty<string>("LangId");
      if (!string.IsNullOrEmpty(language) && !SystemManager.CurrentContext.AppSettings.AllLanguages.Any<KeyValuePair<int, CultureInfo>>((Func<KeyValuePair<int, CultureInfo>, bool>) (l => l.Value.Name.Equals(language, StringComparison.Ordinal))))
        return;
      CultureInfo culture = (CultureInfo) null;
      if (SystemManager.CurrentContext.AppSettings.Multilingual)
      {
        if (CultureInfo.InvariantCulture.Name.Equals(language))
          language = SystemManager.CurrentContext.AppSettings.DefaultFrontendLanguage.Name;
        culture = new CultureInfo(language);
      }
      Guid propertyOrDefault = obj.GetPropertyOrDefault<Guid>("objectId");
      PageManager manager = PageManager.GetManager((string) null, transactionName);
      PageData pageData = manager.GetPageNode(propertyOrDefault).GetPageData(culture);
      if (pageData == null)
        return;
      PageDraft master = manager.PagesLifecycle.Edit(pageData, culture);
      manager.PagesLifecycle.Publish(master, culture);
      this.CreateVersion((IDataItem) master, pageData.Id, transactionName, culture);
    }

    private IQueryable<PageNode> GetChildPages(
      PageManager pageManager,
      Guid parentId)
    {
      // ISSUE: object of a compiler-generated type is created
      // ISSUE: variable of a compiler-generated type
      PagesContentTransfer.\u003C\u003Ec__DisplayClass15_0 cDisplayClass150 = new PagesContentTransfer.\u003C\u003Ec__DisplayClass15_0();
      // ISSUE: reference to a compiler-generated field
      cDisplayClass150.parentId = parentId;
      // ISSUE: reference to a compiler-generated field
      return pageManager.GetPageNodes().Where<PageNode>((Expression<Func<PageNode, bool>>) (p => p.ParentId == cDisplayClass150.parentId && !p.IsDeleted && ((int) p.NodeType != 0 || p.PageDataList.Any<PageData>((Func<PageData, bool>) (pd => pd.PublishedTranslations.Count<string>() == 0 && pd.Visible || pd.PublishedTranslations.Count<string>() > 0)))));
    }

    private void StoreContentLocationDataItem(WrapperObject obj) => this.contentLocations.Add(new ContentLocationDataItem()
    {
      Language = obj.GetProperty<string>("Language"),
      ItemProvider = obj.GetProperty<string>("ItemProvider"),
      ItemType = obj.GetProperty<string>("ItemType"),
      PageId = obj.GetProperty<Guid>("PageId"),
      SiteId = obj.GetProperty<Guid>("SiteId"),
      Priority = int.Parse(obj.GetProperty("Priority").ToString())
    });

    private void UpdateContentLocationsPriority()
    {
      ContentLocationsManager manager = ContentLocationsManager.GetManager();
      foreach (IGrouping<\u003C\u003Ef__AnonymousType21<string, string, string, Guid>, ContentLocationDataItem> grouping in this.contentLocations.GroupBy(l => new
      {
        ItemType = l.ItemType,
        Language = l.Language,
        ItemProvider = l.ItemProvider,
        SiteId = l.SiteId
      }))
      {
        IGrouping<\u003C\u003Ef__AnonymousType21<string, string, string, Guid>, ContentLocationDataItem> locationGroup = grouping;
        IQueryable<ContentLocationDataItem> queryable = manager.GetLocations().Where<ContentLocationDataItem>((Expression<Func<ContentLocationDataItem, bool>>) (l => l.ItemType == locationGroup.Key.ItemType && l.ItemProvider == locationGroup.Key.ItemProvider && l.Priority != -1 && (l.Language == string.Empty || l.Language == locationGroup.Key.Language) && l.SiteId == locationGroup.Key.SiteId));
        IEnumerable<Guid> newLocationsPageIds = locationGroup.Select<ContentLocationDataItem, Guid>((Func<ContentLocationDataItem, Guid>) (l => l.PageId));
        int num1;
        if (newLocationsPageIds.Count<Guid>() >= queryable.Count<ContentLocationDataItem>())
          num1 = !queryable.Any<ContentLocationDataItem>((Expression<Func<ContentLocationDataItem, bool>>) (l => newLocationsPageIds.Contains<Guid>(l.PageId))) ? 1 : 0;
        else
          num1 = 1;
        if (num1 == 0)
        {
          IOrderedEnumerable<ContentLocationDataItem> orderedEnumerable = locationGroup.OrderBy<ContentLocationDataItem, int>((Func<ContentLocationDataItem, int>) (l => l.Priority));
          int num2 = 1;
          foreach (ContentLocationDataItem location in (IEnumerable<ContentLocationDataItem>) orderedEnumerable)
          {
            location.Priority = num2;
            this.UpdateContentLocationPriority(location, queryable);
            ++num2;
          }
        }
      }
    }

    private void UpdateContentLocationPriority(
      ContentLocationDataItem location,
      IQueryable<ContentLocationDataItem> itemTypeLocations)
    {
      ContentLocationDataItem locationDataItem = itemTypeLocations.FirstOrDefault<ContentLocationDataItem>((Expression<Func<ContentLocationDataItem, bool>>) (l => l.PageId == location.PageId));
      if (locationDataItem == null)
        return;
      using (new CultureRegion(AppSettings.CurrentSettings.GetCultureByName(location.Language)))
      {
        int priority1 = locationDataItem.Priority;
        int priority2 = location.Priority;
        ContentLocationService contentLocationService = new ContentLocationService();
        if (priority1 < priority2)
        {
          int num = itemTypeLocations.Max<ContentLocationDataItem, int>((Expression<Func<ContentLocationDataItem, int>>) (l => l.Priority));
          MovingDirection direction = MovingDirection.Down;
          for (; priority1 < priority2 && priority1 < num; ++priority1)
            contentLocationService.ChangeContentLocationPriority(locationDataItem.Id, direction);
        }
        else
        {
          if (priority1 <= priority2)
            return;
          MovingDirection direction = MovingDirection.Up;
          for (; priority1 > priority2 && priority1 > 0; --priority1)
            contentLocationService.ChangeContentLocationPriority(locationDataItem.Id, direction);
        }
      }
    }
  }
}
