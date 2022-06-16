// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Pages.PageTemplatesContentTransfer
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.Forms.Model;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.Multisite;
using Telerik.Sitefinity.Multisite.Model;
using Telerik.Sitefinity.Packaging;
using Telerik.Sitefinity.Packaging.Content;
using Telerik.Sitefinity.Packaging.Logging;
using Telerik.Sitefinity.Packaging.Model;
using Telerik.Sitefinity.Packaging.Package;
using Telerik.Sitefinity.Pages.Model;
using Telerik.Sitefinity.Publishing;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Utilities.TypeConverters;

namespace Telerik.Sitefinity.Modules.Pages
{
  /// <summary>
  /// Processes import/export of page templates content items
  /// </summary>
  internal class PageTemplatesContentTransfer : PageContentTransferBase, IMultisiteTransfer
  {
    private const string AreaName = "Page templates";
    private const string DeleteFailedMessage = "Deleting page template \"{0}\" failed. {1}";
    private IEnumerable<Guid> defaultTemplatesIds = (IEnumerable<Guid>) new Guid[9]
    {
      SiteInitializer.TemplateId1ColumnHeaderFooter,
      SiteInitializer.TemplateIdLeftbarHeaderFooter,
      SiteInitializer.TemplateIdRightSidebarHeaderFooter,
      SiteInitializer.TemplateIdLeftSideBar,
      SiteInitializer.TemplateIdRightSideBar,
      SiteInitializer.TemplateId2EqualHeaderFooter,
      SiteInitializer.TemplateId3EqualHeaderFooter,
      SiteInitializer.TemplateId2Sidebars,
      SiteInitializer.TemplateIdPromo3ColumnsHeaderFooter
    };
    private IEnumerable<ExportType> supportedTypes;

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Modules.Pages.PageTemplatesContentTransfer" /> class.
    /// </summary>
    public PageTemplatesContentTransfer() => this.AddDependencies(typeof (PageTemplate).FullName, typeof (FormDescription).FullName);

    /// <inheritdoc />
    public override string Area => "Page templates";

    /// <inheritdoc />
    public override IEnumerable<ExportType> SupportedTypes
    {
      get
      {
        if (this.supportedTypes == null)
        {
          IList<ExportType> exportTypeList = (IList<ExportType>) new List<ExportType>();
          ExportType exportType = new ExportType("Page templates", typeof (PageTemplate).FullName);
          exportTypeList.Add(exportType);
          this.supportedTypes = (IEnumerable<ExportType>) exportTypeList;
        }
        return this.supportedTypes;
      }
    }

    /// <inheritdoc />
    public override IEnumerable<IQueryable<object>> GetItemsQueries(
      ExportParams settings)
    {
      PageTemplatesContentTransfer templatesContentTransfer = this;
      PageManager pagesManager = PageManager.GetManager(settings.ProviderName);
      Guid backendTemplatesCategoryId = DataExtensions.AppSettings.BackendTemplatesCategoryId;
      IQueryable<PageTemplate> source1 = pagesManager.GetTemplates().Where<PageTemplate>((Expression<Func<PageTemplate, bool>>) (tmpl => tmpl.Category != backendTemplatesCategoryId && tmpl.ParentTemplate == default (object)));
      ISite currentSite = SystemManager.CurrentContext.CurrentSite;
      IQueryable<Guid> pageTemplateIdsForCurrentSite = pagesManager.GetSiteItemLinks().Where<SiteItemLink>((Expression<Func<SiteItemLink, bool>>) (l => l.SiteId == currentSite.Id && l.ItemType == typeof (PageTemplate).FullName)).Select<SiteItemLink, Guid>((Expression<Func<SiteItemLink, Guid>>) (l => l.ItemId));
      Queue<IQueryable<PageTemplate>> queue = new Queue<IQueryable<PageTemplate>>((IEnumerable<IQueryable<PageTemplate>>) new IQueryable<PageTemplate>[1]
      {
        source1.Where<PageTemplate>((Expression<Func<PageTemplate, bool>>) (tmpl => pageTemplateIdsForCurrentSite.Contains<Guid>(tmpl.Id)))
      });
      while (queue.Any<IQueryable<PageTemplate>>())
      {
        IQueryable<PageTemplate> next = queue.Dequeue();
        ParameterExpression parameterExpression;
        // ISSUE: method reference
        // ISSUE: method reference
        // ISSUE: method reference
        // ISSUE: method reference
        // ISSUE: method reference
        yield return (IQueryable<object>) next.Where<PageTemplate>(Expression.Lambda<Func<PageTemplate, bool>>((Expression) Expression.AndAlso(!templatesContentTransfer.defaultTemplatesIds.Contains<Guid>(tmpl.Id), (Expression) Expression.Not((Expression) Expression.AndAlso((Expression) Expression.Call((Expression) null, (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (Enumerable.Contains)), new Expression[2]
        {
          templatesContentTransfer.DefaultFeatherTemplatesTitles,
          (Expression) Expression.Call(tmpl.Title, (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (object.ToString)), Array.Empty<Expression>())
        }), (Expression) Expression.Call((Expression) null, (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (Enumerable.Contains)), new Expression[2]
        {
          (Expression) Expression.Property((Expression) Expression.Constant((object) templatesContentTransfer, typeof (PageTemplatesContentTransfer)), (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (PageContentTransferBase.get_DefaultFeatherTemplatesNames))),
          (Expression) Expression.Property((Expression) parameterExpression, (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (PageTemplate.get_Name)))
        })))), parameterExpression));
        IQueryable<PageTemplate> source2 = next;
        Expression<Func<PageTemplate, Guid>> selector = (Expression<Func<PageTemplate, Guid>>) (pn => pn.Id);
        foreach (Guid parentId in (IEnumerable<Guid>) source2.Select<PageTemplate, Guid>(selector))
          queue.Enqueue(templatesContentTransfer.GetChildPageTemplates(pagesManager, parentId, (IEnumerable<Guid>) pageTemplateIdsForCurrentSite));
        next = (IQueryable<PageTemplate>) null;
      }
    }

    /// <inheritdoc />
    public void Activate(string sourceName, Guid siteId)
    {
      PackagingManager manager = PackagingManager.GetManager();
      Addon addon = manager.GetAddons().FirstOrDefault<Addon>((Expression<Func<Addon, bool>>) (a => a.Name == sourceName));
      if (addon == null)
        return;
      IQueryable<Guid> pageTemplateIds = manager.GetAddonLinks().Where<AddonLink>((Expression<Func<AddonLink, bool>>) (l => l.AddonId == addon.Id && l.ItemType == typeof (PageTemplate).FullName)).Select<AddonLink, Guid>((Expression<Func<AddonLink, Guid>>) (l => l.ItemId));
      this.LinkTemplatesToSite(siteId, (IEnumerable<Guid>) pageTemplateIds);
    }

    /// <inheritdoc />
    public void Activate(ICollection<ItemLink> itemLinks, Guid siteId)
    {
      if (itemLinks == null || itemLinks.Count <= 0)
        return;
      IEnumerable<Guid> pageTemplateIds = itemLinks.Where<ItemLink>((Func<ItemLink, bool>) (l => l.ItemType == typeof (PageTemplate).FullName)).Select<ItemLink, Guid>((Func<ItemLink, Guid>) (l => l.ItemId));
      this.LinkTemplatesToSite(siteId, pageTemplateIds);
    }

    /// <inheritdoc />
    public void Deactivate(string sourceName, Guid siteId)
    {
      PackagingManager manager1 = PackagingManager.GetManager();
      Addon addon = manager1.GetAddons().FirstOrDefault<Addon>((Expression<Func<Addon, bool>>) (a => a.Name == sourceName));
      if (addon == null)
        return;
      IQueryable<Guid> addonPageTemplateIds = manager1.GetAddonLinks().Where<AddonLink>((Expression<Func<AddonLink, bool>>) (l => l.AddonId == addon.Id && l.ItemType == typeof (PageTemplate).FullName)).Select<AddonLink, Guid>((Expression<Func<AddonLink, Guid>>) (l => l.ItemId));
      PageManager manager2 = PageManager.GetManager();
      IQueryable<PageTemplate> templates = manager2.GetTemplates();
      Expression<Func<PageTemplate, bool>> predicate = (Expression<Func<PageTemplate, bool>>) (p => addonPageTemplateIds.Contains<Guid>(p.Id));
      foreach (PageTemplate pageTemplate1 in (IEnumerable<PageTemplate>) templates.Where<PageTemplate>(predicate))
      {
        PageTemplate pageTemplate = pageTemplate1;
        SiteItemLink link = manager2.GetSiteTemplateLinks().Where<SiteItemLink>((Expression<Func<SiteItemLink, bool>>) (l => l.SiteId == siteId && l.ItemId == pageTemplate.Id)).FirstOrDefault<SiteItemLink>();
        if (link != null)
          manager2.Delete(link);
      }
      manager2.SaveChanges();
    }

    /// <inheritdoc />
    public override void Import(
      IEnumerable<WrapperObject> transferableObjects,
      ImportParams parameters,
      System.Action<WrapperObject, IEnumerable<ExportType>> itemCreatedAction,
      System.Action<WrapperObject, Exception> itemFailedAction)
    {
      using (SiteRegion.SingleSiteMode())
        base.Import(transferableObjects, parameters, itemCreatedAction, itemFailedAction);
    }

    /// <inheritdoc />
    public override void Count(Stream fileStream, ScanOperation operation)
    {
      IQueryable<Guid> itemIds = (IQueryable<Guid>) null;
      PackagingManager manager = PackagingManager.GetManager();
      Addon addon = manager.GetAddons().FirstOrDefault<Addon>((Expression<Func<Addon, bool>>) (a => a.Name == operation.AddOnInfo.Name));
      if (addon != null)
        itemIds = manager.GetAddonLinks().Where<AddonLink>((Expression<Func<AddonLink, bool>>) (a => a.ItemType == typeof (PageTemplate).FullName && a.AddonId == addon.Id)).Select<AddonLink, Guid>((Expression<Func<AddonLink, Guid>>) (a => a.ItemId));
      if (!(itemIds != null & itemIds.Count<Guid>() > 0))
        return;
      IQueryable<PageTemplate> source = PageManager.GetManager().GetTemplates().Where<PageTemplate>((Expression<Func<PageTemplate, bool>>) (f => itemIds.Contains<Guid>(f.Id)));
      Expression<Func<PageTemplate, string>> selector = (Expression<Func<PageTemplate, string>>) (f => f.FieldValue<string>("Title_"));
      foreach (string str in (IEnumerable<string>) source.Select<PageTemplate, string>(selector))
      {
        AddOnEntry addOnEntry = new AddOnEntry()
        {
          DisplayName = str,
          AddonEntryType = AddOnEntryType.Template
        };
        operation.AddOnInfo.Entries.Add(addOnEntry);
      }
    }

    /// <inheritdoc />
    public override void Delete(string sourceName) => this.DeleteImportedData(sourceName, typeof (PageManager), (IList<Type>) new List<Type>()
    {
      typeof (PageTemplate)
    });

    /// <inheritdoc />
    protected override void DeleteItems(
      Type managerTypeName,
      Type itemType,
      string provider,
      IList<Guid> itemIds)
    {
      PageManager manager = ManagerBase.GetManager(managerTypeName, provider) as PageManager;
      IQueryable<PageTemplate> templates = manager.GetTemplates();
      Expression<Func<PageTemplate, bool>> predicate = (Expression<Func<PageTemplate, bool>>) (tmpl => itemIds.Contains(tmpl.Id));
      foreach (PageTemplate pageTemplate in (IEnumerable<PageTemplate>) this.SortTemplatesInDeleteOrder(templates.Where<PageTemplate>(predicate)))
      {
        try
        {
          manager.Delete(pageTemplate);
        }
        catch (Exception ex)
        {
          PackagingLog.Log((object) string.Format("Deleting page template \"{0}\" failed. {1}", (object) pageTemplate.Title, (object) ex.Message));
        }
      }
      manager.SaveChanges();
    }

    protected override void PrepareItemForImport(WrapperObject obj, string transactionName)
    {
      if (typeof (PageTemplate).IsAssignableFrom(TypeResolutionService.ResolveType(obj.GetPropertyOrDefault<string>("objectTypeId"), false)))
      {
        if (!this.IsAssociatedTemplateDefaultFeatherTemplate(obj))
          return;
        PageManager manager = PageManager.GetManager((string) null, transactionName);
        PageTemplate defaultFeatherTemplate = this.GetDefaultFeatherTemplate(obj, manager);
        if (defaultFeatherTemplate != null)
          obj.SetOrAddProperty("ParentId", (object) defaultFeatherTemplate.Id);
        else
          this.DeleteProperty(obj, "ParentId");
      }
      else
        base.PrepareItemForImport(obj, transactionName);
    }

    /// <inheritdoc />
    protected override void OnItemImported(IDataItem dataItem, WrapperObject obj, IManager manager)
    {
      base.OnItemImported(dataItem, obj, manager);
      if (!(dataItem is PageTemplate) || !(manager is PageManager))
        return;
      PageManager pageManager = manager as PageManager;
      PageTemplate liveItem = dataItem as PageTemplate;
      liveItem.Controls.Clear();
      pageManager.TemplatesLifecycle.DiscardAllDrafts(liveItem);
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
        if (string.IsNullOrEmpty(language))
          language = SystemManager.CurrentContext.AppSettings.DefaultFrontendLanguage.Name;
        culture = new CultureInfo(language);
      }
      Guid propertyOrDefault = obj.GetPropertyOrDefault<Guid>("objectId");
      PageManager manager = PageManager.GetManager((string) null, transactionName);
      PageTemplate template = manager.GetTemplate(propertyOrDefault);
      TemplateDraft master = manager.TemplatesLifecycle.Edit(template, culture);
      manager.TemplatesLifecycle.Publish(master, culture);
      this.CreateVersion((IDataItem) master, template.Id, transactionName, culture);
    }

    private void LinkTemplatesToSite(Guid siteId, IEnumerable<Guid> pageTemplateIds)
    {
      PageManager manager = PageManager.GetManager();
      IQueryable<PageTemplate> templates = manager.GetTemplates();
      Expression<Func<PageTemplate, bool>> predicate = (Expression<Func<PageTemplate, bool>>) (p => pageTemplateIds.Contains<Guid>(p.Id));
      foreach (PageTemplate pageTemplate1 in (IEnumerable<PageTemplate>) templates.Where<PageTemplate>(predicate))
      {
        PageTemplate pageTemplate = pageTemplate1;
        if (manager.GetSiteTemplateLinks().Where<SiteItemLink>((Expression<Func<SiteItemLink, bool>>) (l => l.SiteId == siteId && l.ItemId == pageTemplate.Id)).FirstOrDefault<SiteItemLink>() == null)
          manager.LinkTemplateToSite(pageTemplate, siteId);
      }
      manager.SaveChanges();
    }

    private IQueryable<PageTemplate> GetChildPageTemplates(
      PageManager pageManager,
      Guid parentId,
      IEnumerable<Guid> pageTemplateIdsForCurrentSite)
    {
      IQueryable<PageTemplate> source = pageManager.GetTemplates().Where<PageTemplate>((Expression<Func<PageTemplate, bool>>) (tmpl => tmpl.ParentTemplate != default (object) && tmpl.ParentTemplate.Id == parentId));
      if (pageTemplateIdsForCurrentSite != null)
        source = source.Where<PageTemplate>((Expression<Func<PageTemplate, bool>>) (tmpl => pageTemplateIdsForCurrentSite.Contains<Guid>(tmpl.Id)));
      return source;
    }

    private IList<PageTemplate> SortTemplatesInDeleteOrder(
      IQueryable<PageTemplate> templates)
    {
      List<PageTemplate> pageTemplateList = new List<PageTemplate>();
      Queue<IQueryable<PageTemplate>> source1 = new Queue<IQueryable<PageTemplate>>((IEnumerable<IQueryable<PageTemplate>>) new IQueryable<PageTemplate>[1]
      {
        templates.Where<PageTemplate>((Expression<Func<PageTemplate, bool>>) (tmpl => tmpl.ParentTemplate == default (object)))
      });
      while (source1.Any<IQueryable<PageTemplate>>())
      {
        IQueryable<PageTemplate> collection = source1.Dequeue();
        pageTemplateList.AddRange((IEnumerable<PageTemplate>) collection);
        IQueryable<PageTemplate> source2 = collection;
        Expression<Func<PageTemplate, Guid>> selector = (Expression<Func<PageTemplate, Guid>>) (pn => pn.Id);
        foreach (Guid guid in (IEnumerable<Guid>) source2.Select<PageTemplate, Guid>(selector))
        {
          Guid parentId = guid;
          IQueryable<PageTemplate> queryable = templates.Where<PageTemplate>((Expression<Func<PageTemplate, bool>>) (tmpl => tmpl.ParentTemplate.Id == parentId));
          source1.Enqueue(queryable);
        }
      }
      pageTemplateList.Reverse();
      return (IList<PageTemplate>) pageTemplateList;
    }
  }
}
