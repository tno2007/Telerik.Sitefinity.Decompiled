// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Forms.FormsContentTransfer
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Forms.Model;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.Modules.GenericContent;
using Telerik.Sitefinity.Multisite;
using Telerik.Sitefinity.Multisite.Model;
using Telerik.Sitefinity.Packaging;
using Telerik.Sitefinity.Packaging.Content;
using Telerik.Sitefinity.Packaging.Model;
using Telerik.Sitefinity.Packaging.Package;
using Telerik.Sitefinity.Pages.Model;
using Telerik.Sitefinity.Publishing;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.SiteSync;
using Telerik.Sitefinity.SiteSync.Serialization;
using Telerik.Sitefinity.Utilities.TypeConverters;

namespace Telerik.Sitefinity.Modules.Forms
{
  /// <summary>Processes import/export of forms items</summary>
  internal class FormsContentTransfer : StaticContentTransfer, IMultisiteTransfer
  {
    private readonly FormsDataItemsLoader formsDataItemsLoader = new FormsDataItemsLoader();
    private readonly Lazy<FormsImporter> itemsImporter = new Lazy<FormsImporter>((Func<FormsImporter>) (() =>
    {
      return new FormsImporter("Export/Import")
      {
        Serializer = (ISiteSyncSerializer) new SiteSyncSerializer("Export/Import")
      };
    }));
    private const string AreaName = "Forms";
    private const string FormDeleteFailedMessage = "Form \"{0}\" could not be deleted because it is used in existing page or page template.";
    private IEnumerable<ExportType> supportedTypes;

    /// <inheritdoc />
    public override SiteSyncImporter ItemsImporter => (SiteSyncImporter) this.itemsImporter.Value;

    /// <inheritdoc />
    public override string Area => "Forms";

    /// <inheritdoc />
    public void Activate(string sourceName, Guid siteId)
    {
      PackagingManager manager = PackagingManager.GetManager();
      Addon addon = manager.GetAddons().FirstOrDefault<Addon>((Expression<Func<Addon, bool>>) (a => a.Name == sourceName));
      if (addon == null)
        return;
      IQueryable<Guid> addonFormIds = manager.GetAddonLinks().Where<AddonLink>((Expression<Func<AddonLink, bool>>) (l => l.AddonId == addon.Id && l.ItemType == typeof (FormDescription).FullName)).Select<AddonLink, Guid>((Expression<Func<AddonLink, Guid>>) (l => l.ItemId));
      this.LinkFormsToSite(siteId, (IEnumerable<Guid>) addonFormIds);
    }

    /// <inheritdoc />
    public void Activate(ICollection<ItemLink> itemLinks, Guid siteId)
    {
      if (itemLinks == null || itemLinks.Count <= 0)
        return;
      IEnumerable<Guid> addonFormIds = itemLinks.Where<ItemLink>((Func<ItemLink, bool>) (l => l.ItemType == typeof (FormDescription).FullName)).Select<ItemLink, Guid>((Func<ItemLink, Guid>) (l => l.ItemId));
      this.LinkFormsToSite(siteId, addonFormIds);
    }

    /// <inheritdoc />
    public void Deactivate(string sourceName, Guid siteId)
    {
      PackagingManager manager1 = PackagingManager.GetManager();
      Addon addon = manager1.GetAddons().FirstOrDefault<Addon>((Expression<Func<Addon, bool>>) (a => a.Name == sourceName));
      if (addon == null)
        return;
      Guid[] addonFormIds = manager1.GetAddonLinks().Where<AddonLink>((Expression<Func<AddonLink, bool>>) (l => l.AddonId == addon.Id && l.ItemType == typeof (FormDescription).FullName)).Select<AddonLink, Guid>((Expression<Func<AddonLink, Guid>>) (l => l.ItemId)).ToArray<Guid>();
      FormsManager manager2 = FormsManager.GetManager();
      IQueryable<FormDescription> forms = manager2.GetForms();
      Expression<Func<FormDescription, bool>> predicate = (Expression<Func<FormDescription, bool>>) (f => addonFormIds.Contains<Guid>(f.Id));
      foreach (FormDescription formDescription in (IEnumerable<FormDescription>) forms.Where<FormDescription>(predicate))
      {
        FormDescription form = formDescription;
        SiteItemLink link = manager2.GetSiteFormLinks().Where<SiteItemLink>((Expression<Func<SiteItemLink, bool>>) (l => l.SiteId == siteId && l.ItemId == form.Id)).FirstOrDefault<SiteItemLink>();
        if (link != null)
          manager2.Delete(link);
      }
      manager2.SaveChanges();
    }

    /// <inheritdoc />
    public override IEnumerable<ExportType> SupportedTypes
    {
      get
      {
        if (this.supportedTypes == null)
        {
          IList<ExportType> exportTypeList = (IList<ExportType>) new List<ExportType>();
          ExportType exportType = new ExportType("Forms", typeof (FormDescription).FullName);
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
      return (IEnumerable<IQueryable<object>>) new List<IQueryable<object>>()
      {
        (IQueryable<object>) this.GetFormsQuery(FormsManager.GetManager(settings.ProviderName))
      };
    }

    /// <inheritdoc />
    public override IEnumerable<WrapperObject> Export(
      ExportParams parameters)
    {
      FormsContentTransfer formsContentTransfer = this;
      if (formsContentTransfer.AllowToProcess(parameters.TypeName))
      {
        if (parameters.Languages == null || !parameters.Languages.Any<string>())
        {
          ExportParams exportParams = parameters;
          IEnumerable<string> strings;
          if (!SystemManager.CurrentContext.AppSettings.Multilingual)
            strings = (IEnumerable<string>) new string[1]
            {
              string.Empty
            };
          else
            strings = ((IEnumerable<CultureInfo>) SystemManager.CurrentContext.AppSettings.DefinedFrontendLanguages).Select<CultureInfo, string>((Func<CultureInfo, string>) (ci => ci.Name));
          exportParams.Languages = strings;
        }
        foreach (IQueryable<object> itemsQuery in formsContentTransfer.GetItemsQueries(parameters))
        {
          IQueryable<object> query = itemsQuery;
          int page = 0;
label_11:
          IEnumerable<object> items = (IEnumerable<object>) query.Skip<object>(page * parameters.BufferSize).Take<object>(parameters.BufferSize).ToList<object>();
          IDictionary<string, IEnumerable<object>> dictionary = (IDictionary<string, IEnumerable<object>>) new Dictionary<string, IEnumerable<object>>();
          foreach (string language in parameters.Languages)
          {
            List<object> objectList = new List<object>();
            foreach (object obj in items)
            {
              if (!SystemManager.CurrentContext.AppSettings.Multilingual || !(obj is ILocalizable) || ((IEnumerable<string>) ((ILocalizable) obj).AvailableLanguages).Contains<string>(language))
              {
                objectList.Add(obj);
                Dictionary<Guid, ObjectData> controlDependency = (Dictionary<Guid, ObjectData>) null;
                objectList.AddRange((IEnumerable<object>) formsContentTransfer.formsDataItemsLoader.LoadDataItem(obj, language, out controlDependency));
              }
            }
            dictionary[language] = (IEnumerable<object>) objectList;
          }
          foreach (KeyValuePair<string, IEnumerable<object>> keyValuePair in (IEnumerable<KeyValuePair<string, IEnumerable<object>>>) dictionary)
          {
            KeyValuePair<string, IEnumerable<object>> itemsListByLang = keyValuePair;
            foreach (object obj in itemsListByLang.Value)
            {
              WrapperObject mappedItem = formsContentTransfer.PreProcessExportObject(obj, parameters, itemsListByLang.Key);
              if (mappedItem != null)
              {
                formsContentTransfer.IgnoreProperties(mappedItem);
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
            goto label_11;
        }
      }
    }

    /// <inheritdoc />
    public override void CreateItem(WrapperObject obj, string transactionName)
    {
      this.PrepareItemForImport(obj);
      (this.ItemsImporter as FormsImporter).ClearTemporaryData();
      base.CreateItem(obj, transactionName);
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
      IEnumerable<Guid> itemIds = (IEnumerable<Guid>) null;
      PackagingManager manager = PackagingManager.GetManager();
      Addon addon = manager.GetAddons().FirstOrDefault<Addon>((Expression<Func<Addon, bool>>) (a => a.Name == operation.AddOnInfo.Name));
      if (addon != null)
        itemIds = (IEnumerable<Guid>) manager.GetAddonLinks().Where<AddonLink>((Expression<Func<AddonLink, bool>>) (a => a.ItemType == typeof (FormDescription).FullName && a.AddonId == addon.Id)).Select<AddonLink, Guid>((Expression<Func<AddonLink, Guid>>) (a => a.ItemId)).ToList<Guid>();
      if (!(itemIds != null & itemIds.Count<Guid>() > 0))
        return;
      IQueryable<FormDescription> source = FormsManager.GetManager().GetForms().Where<FormDescription>((Expression<Func<FormDescription, bool>>) (f => itemIds.Contains<Guid>(f.Id)));
      Expression<Func<FormDescription, string>> selector = (Expression<Func<FormDescription, string>>) (f => f.FieldValue<string>("Title_"));
      foreach (string str in (IEnumerable<string>) source.Select<FormDescription, string>(selector))
      {
        AddOnEntry addOnEntry = new AddOnEntry()
        {
          DisplayName = str,
          AddonEntryType = AddOnEntryType.Form
        };
        operation.AddOnInfo.Entries.Add(addOnEntry);
      }
    }

    /// <inheritdoc />
    public override void Delete(string sourceName) => this.DeleteImportedData(sourceName, typeof (FormsManager), (IList<Type>) new List<Type>()
    {
      typeof (FormDescription)
    });

    /// <inheritdoc />
    protected override void DeleteItems(
      Type managerTypeName,
      Type itemType,
      string provider,
      IList<Guid> itemIds)
    {
      FormsManager manager = FormsManager.GetManager(provider);
      IQueryable<FormDescription> forms = manager.GetForms();
      Expression<Func<FormDescription, bool>> predicate = (Expression<Func<FormDescription, bool>>) (f => itemIds.Contains(f.Id));
      foreach (FormDescription formDescription in (IEnumerable<FormDescription>) forms.Where<FormDescription>(predicate))
      {
        IQueryable<FormEntry> formEntries = manager.GetFormEntries(formDescription);
        manager.Delete(formDescription);
        foreach (FormEntry formEntry in (IEnumerable<FormEntry>) formEntries)
          manager.DeleteItem((object) formEntry);
      }
      manager.SaveChanges();
    }

    /// <inheritdoc />
    protected override WrapperObject PreProcessExportObject(
      object item,
      ExportParams parameters,
      string language)
    {
      string providerName = (string) null;
      if (item is IDataItem dataItem)
        providerName = dataItem.Provider != null ? dataItem.Provider.ToString() : (string) null;
      Type type = item.GetType();
      WrapperObject wrapperObject = base.PreProcessExportObject(item, parameters, language);
      if (wrapperObject != null)
      {
        this.formsDataItemsLoader.SetFormProperties(type, providerName, wrapperObject, item);
        this.formsDataItemsLoader.SetControlPropertiesData(type, providerName, wrapperObject, item);
      }
      return wrapperObject;
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
      FormsManager manager = FormsManager.GetManager(obj.GetPropertyOrDefault<string>("Provider"), transactionName);
      FormDescription form = manager.GetForm(propertyOrDefault);
      FormDraft formDraft = manager.Lifecycle.Edit(form, culture);
      manager.Lifecycle.Copy(form, formDraft, culture);
      manager.Lifecycle.Publish(formDraft, culture);
    }

    private void LinkFormsToSite(Guid siteId, IEnumerable<Guid> addonFormIds)
    {
      FormsManager manager = FormsManager.GetManager();
      IQueryable<FormDescription> forms = manager.GetForms();
      Expression<Func<FormDescription, bool>> predicate = (Expression<Func<FormDescription, bool>>) (f => addonFormIds.Contains<Guid>(f.Id));
      foreach (FormDescription formDescription in (IEnumerable<FormDescription>) forms.Where<FormDescription>(predicate))
      {
        FormDescription form = formDescription;
        if (manager.GetSiteFormLinks().Where<SiteItemLink>((Expression<Func<SiteItemLink, bool>>) (l => l.SiteId == siteId && l.ItemId == form.Id)).FirstOrDefault<SiteItemLink>() == null)
          manager.LinkFormToSite(form, siteId);
      }
      manager.SaveChanges();
    }

    private void PrepareItemForImport(WrapperObject obj)
    {
      Guid frontendRootNodeId = SiteInitializer.CurrentFrontendRootNodeId;
      if (!typeof (ControlProperty).IsAssignableFrom(TypeResolutionService.ResolveType(obj.GetPropertyOrDefault<string>("objectTypeId"), false)) || !(obj.GetPropertyOrDefault<string>("Name") == "Html"))
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

    private IQueryable<FormDescription> GetFormsQuery(
      FormsManager formsManager)
    {
      IQueryable<FormDescription> forms = formsManager.GetForms();
      return ((IMultisiteEnabledOAProvider) formsManager.Provider).FilterBySite<FormDescription>(forms, SystemManager.CurrentContext.CurrentSite.Id);
    }
  }
}
