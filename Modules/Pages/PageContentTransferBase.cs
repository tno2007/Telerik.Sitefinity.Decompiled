// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Pages.PageContentTransferBase
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Text.RegularExpressions;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.ContentLocations.Model;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.Data.Linq.Dynamic;
using Telerik.Sitefinity.GenericContent.Model;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.Modules.GenericContent;
using Telerik.Sitefinity.Packaging.Content;
using Telerik.Sitefinity.Packaging.Import;
using Telerik.Sitefinity.Pages.Model;
using Telerik.Sitefinity.Publishing;
using Telerik.Sitefinity.Security.Model;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.SiteSync;
using Telerik.Sitefinity.SiteSync.Serialization;
using Telerik.Sitefinity.Utilities.TypeConverters;

namespace Telerik.Sitefinity.Modules.Pages
{
  /// <summary>
  /// Base class that processes import/export of page and page templates content items
  /// </summary>
  internal abstract class PageContentTransferBase : StaticContentTransfer
  {
    private readonly PagesDataItemsLoader pagesDataItemsLoader = new PagesDataItemsLoader();
    private readonly Lazy<PackagingPagesImporter> itemsImporter = new Lazy<PackagingPagesImporter>((Func<PackagingPagesImporter>) (() =>
    {
      return new PackagingPagesImporter("Export/Import")
      {
        Serializer = (ISiteSyncSerializer) new SiteSyncSerializer("Export/Import")
      };
    }));
    private const string PlacholderKeyRegEx = "T.*Col\\d\\d$";
    private readonly IEnumerable<string> defaultFeatherTemplatesTitles = (IEnumerable<string>) new string[7]
    {
      "default",
      "1 Column, Header, Footer",
      "2 Equal Columns, Header, Footer",
      "3 Equal Columns, Header, Footer",
      "4 Equal Columns, Header, Footer",
      "Left Sidebar, Header, Footer",
      "Right Sidebar, Header, Footer"
    };
    private readonly IEnumerable<string> defaultFeatherTemplatesNames = (IEnumerable<string>) new string[5]
    {
      "Bootstrap.default",
      "Bootstrap4.default",
      "Foundation.default",
      "SemanticUI.default",
      "Minimal.default"
    };

    /// <inheritdoc />
    public override SiteSyncImporter ItemsImporter => (SiteSyncImporter) this.itemsImporter.Value;

    /// <inheritdoc />
    public override IEnumerable<WrapperObject> Export(
      ExportParams parameters)
    {
      PageContentTransferBase contentTransferBase = this;
      if (contentTransferBase.AllowToProcess(parameters.TypeName))
      {
        if (parameters.Languages == null || !parameters.Languages.Any<string>())
          parameters.Languages = ((IEnumerable<CultureInfo>) SystemManager.CurrentContext.AppSettings.DefinedFrontendLanguages).Select<CultureInfo, string>((Func<CultureInfo, string>) (ci => ci.Name));
        foreach (IQueryable<object> itemsQuery in contentTransferBase.GetItemsQueries(parameters))
        {
          IQueryable<object> query = itemsQuery;
          int page = 0;
label_8:
          IEnumerable<object> items = (IEnumerable<object>) query.Skip<object>(page * parameters.BufferSize).Take<object>(parameters.BufferSize).ToList<object>();
          IDictionary<string, IEnumerable<object>> itemsByLanguage = (IDictionary<string, IEnumerable<object>>) new Dictionary<string, IEnumerable<object>>();
          foreach (string language in parameters.Languages)
            contentTransferBase.LoadItemsForLanguage(items, language, itemsByLanguage);
          foreach (KeyValuePair<string, IEnumerable<object>> keyValuePair in (IEnumerable<KeyValuePair<string, IEnumerable<object>>>) itemsByLanguage)
          {
            KeyValuePair<string, IEnumerable<object>> itemsListByLang = keyValuePair;
            foreach (object obj in itemsListByLang.Value)
            {
              WrapperObject mappedItem = contentTransferBase.PreProcessExportObject(obj, parameters, itemsListByLang.Key);
              if (mappedItem != null)
              {
                contentTransferBase.IgnoreProperties(mappedItem);
                yield return mappedItem;
              }
            }
            itemsListByLang = new KeyValuePair<string, IEnumerable<object>>();
          }
          ++page;
          if (items.Count<object>() != parameters.BufferSize)
          {
            items = (IEnumerable<object>) null;
            query = (IQueryable<object>) null;
          }
          else
            goto label_8;
        }
      }
    }

    /// <inheritdoc />
    public override void CreateItem(WrapperObject obj, string transactionName)
    {
      this.PrepareItemForImport(obj, transactionName);
      (this.ItemsImporter as PagesImporter).ClearTemporaryData();
      base.CreateItem(obj, transactionName);
    }

    /// <inheritdoc />
    protected override WrapperObject PreProcessExportObject(
      object item,
      ExportParams parameters,
      string language)
    {
      if (item is ControlProperty && (item as ControlProperty).Name == "ProviderName")
        return (WrapperObject) null;
      string providerName = (string) null;
      if (item is IDataItem dataItem)
        providerName = dataItem.Provider != null ? dataItem.Provider.ToString() : (string) null;
      Type type = item.GetType();
      string str = (string) null;
      bool flag = false;
      if (item is PageData)
      {
        PageData pageData = item as PageData;
        if (pageData.NavigationNode != null && pageData.NavigationNode.LocalizationStrategy == LocalizationStrategy.Synced)
        {
          str = pageData.Culture;
          flag = true;
          pageData.Culture = language;
        }
      }
      WrapperObject wrapperObject = base.PreProcessExportObject(item, parameters, language);
      if (wrapperObject != null)
      {
        if (flag)
          wrapperObject.SetProperty("Culture", (object) str);
        this.pagesDataItemsLoader.SetPageProperties(type, providerName, wrapperObject, item);
        this.pagesDataItemsLoader.SetControlPropertiesData(type, providerName, wrapperObject, item);
        this.pagesDataItemsLoader.SetContentLocationFilterDataItemData(type, wrapperObject, item);
        this.PrepareItemForExport(wrapperObject, type, item);
      }
      return wrapperObject;
    }

    /// <summary>Modifies the wrapper object that is exported.</summary>
    /// <param name="obj">The wrapper object from the stream.</param>
    /// <param name="itemType">The type of the object that is exported.</param>
    /// <param name="item">The item.</param>
    protected virtual void PrepareItemForExport(WrapperObject obj, Type itemType, object item)
    {
      if (typeof (TemplateControl).IsAssignableFrom(itemType) || typeof (PageControl).IsAssignableFrom(itemType))
      {
        string propertyOrDefault = obj.GetPropertyOrDefault<string>("PlaceHolder");
        Regex regex = new Regex("T.*Col\\d\\d$");
        if (propertyOrDefault == null || !regex.IsMatch(propertyOrDefault))
          return;
        string templateKey = propertyOrDefault.Substring(0, 9);
        PageTemplate template = PageManager.GetManager().GetTemplates().FirstOrDefault<PageTemplate>((Expression<Func<PageTemplate, bool>>) (t => t.Key == templateKey));
        if (template == null)
          return;
        obj.AddProperty("ControlParentTemplateId", (object) template.Id);
        this.AddNameAndTitlePropertyIfDefaultFeatherTemplate(template, obj);
      }
      else if (typeof (PageData).IsAssignableFrom(itemType))
      {
        this.AddNameAndTitlePropertyIfDefaultFeatherTemplate(((PageData) item).Template, obj);
      }
      else
      {
        if (!typeof (PageTemplate).IsAssignableFrom(itemType))
          return;
        PageTemplate pageTemplate = (PageTemplate) item;
        if (pageTemplate.ParentTemplate == null)
          return;
        this.AddNameAndTitlePropertyIfDefaultFeatherTemplate(pageTemplate.ParentTemplate, obj);
      }
    }

    /// <summary>Modifies the imported wrapper object.</summary>
    /// <param name="obj">The wrapper object from the stream.</param>
    /// <param name="transactionName">Name of the transaction.</param>
    protected virtual void PrepareItemForImport(WrapperObject obj, string transactionName)
    {
      Guid id = SystemManager.CurrentContext.CurrentSite.Id;
      Guid frontendRootNodeId = SiteInitializer.CurrentFrontendRootNodeId;
      Type c = TypeResolutionService.ResolveType(obj.GetPropertyOrDefault<string>("objectTypeId"), false);
      if (typeof (TemplateControl).IsAssignableFrom(c) || typeof (PageControl).IsAssignableFrom(c))
      {
        string propertyOrDefault = obj.GetPropertyOrDefault<string>("PlaceHolder");
        Regex regex = new Regex("T.*Col\\d\\d$");
        if (propertyOrDefault == null || !regex.IsMatch(propertyOrDefault))
          return;
        Guid parentTemplateId = obj.GetPropertyOrDefault<Guid>("ControlParentTemplateId");
        if (!(parentTemplateId != Guid.Empty))
          return;
        PageManager manager = PageManager.GetManager((string) null, transactionName);
        PageTemplate pageTemplate = manager.GetTemplates().FirstOrDefault<PageTemplate>((Expression<Func<PageTemplate, bool>>) (t => t.Id == parentTemplateId));
        if (pageTemplate == null && this.IsAssociatedTemplateDefaultFeatherTemplate(obj))
          pageTemplate = this.GetDefaultFeatherTemplate(obj, manager);
        if (pageTemplate == null)
          return;
        string key = pageTemplate.Key;
        string str = key + propertyOrDefault.Substring(key.Count<char>());
        obj.SetProperty("PlaceHolder", (object) str);
      }
      else if (typeof (ContentLocationDataItem).IsAssignableFrom(c))
      {
        obj.SetOrAddProperty("SiteId", (object) id);
        string propertyOrDefault = obj.GetPropertyOrDefault<string>("ItemType");
        if (string.IsNullOrEmpty(propertyOrDefault))
          return;
        string defaultProvider = SystemManager.GetDefaultProvider(propertyOrDefault);
        obj.SetOrAddProperty("ItemProvider", (object) defaultProvider);
      }
      else
      {
        if (!typeof (ControlProperty).IsAssignableFrom(c) || !(obj.GetPropertyOrDefault<string>("Name") == "Html"))
          return;
        string[] strArray = new string[2]
        {
          "Value",
          "MultilingualValue"
        };
        foreach (string name in strArray)
        {
          string propertyOrDefault = obj.GetPropertyOrDefault<string>(name);
          if (!string.IsNullOrEmpty(propertyOrDefault))
          {
            string str = ContentBlocksContentTransfer.PrepareContentForImport(propertyOrDefault, frontendRootNodeId);
            obj.SetOrAddProperty(name, (object) str);
          }
        }
      }
    }

    /// <inheritdoc />
    protected override void OnItemImported(IDataItem dataItem, WrapperObject obj, IManager manager)
    {
      if (!(dataItem is ControlData))
        return;
      PageManager pageManager = manager as PageManager;
      if (pageManager.GetPermissions().Where<Permission>((Expression<Func<Permission, bool>>) (p => p.ObjectId == dataItem.Id)).Count<Permission>() != 0)
        return;
      pageManager.SetControlDefaultPermissions(dataItem as ControlData);
    }

    /// <summary>Gets the default feather templates names.</summary>
    /// <value>The default feather templates names.</value>
    protected IEnumerable<string> DefaultFeatherTemplatesNames => this.defaultFeatherTemplatesNames;

    /// <summary>Gets the default feather templates titles.</summary>
    /// <value>The default feather templates titles.</value>
    protected IEnumerable<string> DefaultFeatherTemplatesTitles => this.defaultFeatherTemplatesTitles;

    protected PageTemplate GetDefaultFeatherTemplate(
      WrapperObject obj,
      PageManager pageManager)
    {
      string name = obj.GetPropertyOrDefault<string>("PageTemplateName");
      string propertyOrDefault = obj.GetPropertyOrDefault<string>("PageTemplateTitle");
      PageTemplate defaultFeatherTemplate = (PageTemplate) null;
      if (!string.IsNullOrEmpty(name) && !string.IsNullOrEmpty(propertyOrDefault))
      {
        string predicate = string.Format("(Title.ToUpper().Equals(\"{0}\") OR Title[\"\"].ToUpper().Equals(\"{0}\"))", (object) propertyOrDefault.ToUpper());
        defaultFeatherTemplate = pageManager.GetTemplates().Where<PageTemplate>(predicate).FirstOrDefault<PageTemplate>((Expression<Func<PageTemplate, bool>>) (t => t.Name == name));
      }
      return defaultFeatherTemplate;
    }

    protected void AddNameAndTitlePropertyIfDefaultFeatherTemplate(
      PageTemplate template,
      WrapperObject obj)
    {
      if (template == null || template.Framework != PageTemplateFramework.Mvc || !this.DefaultFeatherTemplatesTitles.Contains<string>(template.Title.ToString()) || !this.DefaultFeatherTemplatesNames.Contains<string>(template.Name))
        return;
      obj.AddProperty("PageTemplateTitle", (object) template.Title);
      obj.AddProperty("PageTemplateName", (object) template.Name);
    }

    protected bool IsAssociatedTemplateDefaultFeatherTemplate(WrapperObject obj)
    {
      string propertyOrDefault = obj.GetPropertyOrDefault<string>("PageTemplateName");
      return this.DefaultFeatherTemplatesTitles.Contains<string>(obj.GetPropertyOrDefault<string>("PageTemplateTitle")) && this.DefaultFeatherTemplatesNames.Contains<string>(propertyOrDefault);
    }

    private void LoadItemsForLanguage(
      IEnumerable<object> items,
      string lang,
      IDictionary<string, IEnumerable<object>> itemsByLanguage)
    {
      List<object> objectList = new List<object>();
      foreach (object obj in items)
      {
        if (!SystemManager.CurrentContext.AppSettings.Multilingual || !(obj is ILocalizable) || ((IEnumerable<string>) ((ILocalizable) obj).AvailableLanguages).Contains<string>(lang))
        {
          Dictionary<Guid, ObjectData> controlDependency = (Dictionary<Guid, ObjectData>) null;
          if (obj is PageNode)
          {
            PageNode pageNode = obj as PageNode;
            IList<PageData> list = (IList<PageData>) this.pagesDataItemsLoader.GetPageDataList(pageNode, lang, false).Where<PageData>((Func<PageData, bool>) (p => p.Status == ContentLifecycleStatus.Live && p.Visible)).ToList<PageData>();
            if (pageNode.NodeType != NodeType.Standard || list.Count > 0)
              objectList.Add((object) pageNode);
            foreach (PageData pageData in (IEnumerable<PageData>) list)
            {
              objectList.Add((object) pageData);
              controlDependency = new Dictionary<Guid, ObjectData>();
              IList<object> controlObjects = this.pagesDataItemsLoader.GetControlObjects(pageData, pageNode, lang, ref controlDependency, false);
              objectList.AddRange((IEnumerable<object>) controlObjects);
            }
            AppSettings currentSettings = AppSettings.CurrentSettings;
            string persistedLang = currentSettings.GetCultureName(currentSettings.GetCultureByName(lang));
            IEnumerable<object> collection = this.pagesDataItemsLoader.GetContentLocations(pageNode).Where<object>((Func<object, bool>) (c => c is ContentLocationDataItem && ((ContentLocationDataItem) c).Priority > -1 && (c as ContentLocationDataItem).Language == persistedLang));
            objectList.AddRange(collection);
          }
          else if (obj is PageTemplate)
          {
            PageTemplate pageTemplate = obj as PageTemplate;
            if (pageTemplate.Visible)
            {
              objectList.Add((object) pageTemplate);
              controlDependency = new Dictionary<Guid, ObjectData>();
              IList<object> controlObjects = this.pagesDataItemsLoader.GetControlObjects(pageTemplate, ref controlDependency, false);
              objectList.AddRange((IEnumerable<object>) controlObjects);
            }
          }
        }
      }
      itemsByLanguage[lang] = (IEnumerable<object>) objectList;
    }
  }
}
