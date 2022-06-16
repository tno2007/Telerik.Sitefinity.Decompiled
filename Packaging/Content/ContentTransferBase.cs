// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Packaging.Content.ContentTransferBase
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Xml;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.Data.Events;
using Telerik.Sitefinity.GenericContent.Model;
using Telerik.Sitefinity.Lifecycle;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.Multisite;
using Telerik.Sitefinity.Multisite.Model;
using Telerik.Sitefinity.Multisite.Web.Services;
using Telerik.Sitefinity.Multisite.Web.Services.ViewModel;
using Telerik.Sitefinity.Packaging.Events;
using Telerik.Sitefinity.Packaging.Model;
using Telerik.Sitefinity.Packaging.Package;
using Telerik.Sitefinity.Publishing;
using Telerik.Sitefinity.Publishing.Model;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.SitefinityExceptions;
using Telerik.Sitefinity.SiteSync;
using Telerik.Sitefinity.SiteSync.Serialization;
using Telerik.Sitefinity.Taxonomies;
using Telerik.Sitefinity.Taxonomies.Model;
using Telerik.Sitefinity.Utilities.Json;
using Telerik.Sitefinity.Utilities.TypeConverters;
using Telerik.Sitefinity.Versioning;
using Telerik.Sitefinity.Versioning.Serialization.Interfaces;

namespace Telerik.Sitefinity.Packaging.Content
{
  /// <summary>
  /// Implements functionality for converting items from specific type in transferable format.
  /// </summary>
  internal abstract class ContentTransferBase : IContentTransfer, IDataTransfer
  {
    private const string SiteConfigurationViewModelProp = "SiteConfigurationViewModelProp";
    private IDictionary<string, IEnumerable<string>> cachedDependencies;
    private readonly ParentAccessor parentAccessor = new ParentAccessor();
    private readonly IDictionary<Type, MappingSettings> typeMappingSettings = (IDictionary<Type, MappingSettings>) new Dictionary<Type, MappingSettings>();
    private readonly HashSet<string> deletedProperties = new HashSet<string>((IEnumerable<string>) new string[35]
    {
      "AvailableLanguages",
      "CanInheritPermissions",
      "DateCreated",
      "Distance",
      "ExpirationDate",
      "InheritsPermissions",
      "IsDeletable",
      "IsDeleted",
      "IsEditable",
      "ItemDefaultUrl",
      "LanguageData",
      "LastModified",
      "LastModifiedBy",
      "LockedBy",
      "MediaFileUrlsProp",
      "MediaUrl",
      "Owner",
      "Permissions",
      "ProviderName",
      "PublicationDate",
      "PublishedTranslations",
      "SiteIds",
      "StatusDisplayName",
      "SupportedPermissionSets",
      "SystemUrl",
      "ThumbnailUrl",
      "UIStatus",
      "Url",
      "Version",
      "ViewsCount",
      "Visible",
      "VotesCount",
      "VotesSum",
      "WasPublished",
      "IsPersonalized"
    });

    /// <inheritdoc />
    public abstract string Area { get; }

    /// <inheritdoc />
    public abstract IEnumerable<ExportType> SupportedTypes { get; }

    /// <inheritdoc />
    public virtual IDictionary<string, IEnumerable<string>> Dependencies
    {
      get
      {
        if (this.cachedDependencies == null)
          this.cachedDependencies = (IDictionary<string, IEnumerable<string>>) new Dictionary<string, IEnumerable<string>>();
        return this.cachedDependencies;
      }
    }

    /// <inheritdoc />
    public virtual void Delete(string sourceName)
    {
    }

    /// <summary>
    /// Determines whether the specified type can be processed.
    /// </summary>
    /// <param name="typeName">The name of the type.</param>
    /// <returns>True if module can be processed.</returns>
    public virtual bool AllowToProcess(string typeName) => this.ContainsType(this.SupportedTypes, typeName);

    /// <summary>Gets the items query.</summary>
    /// <param name="parameters">The parameters.</param>
    /// <returns>The items query.</returns>
    public abstract IEnumerable<IQueryable<object>> GetItemsQueries(
      ExportParams parameters);

    /// <summary>Gets the items importer</summary>
    public abstract SiteSyncImporter ItemsImporter { get; }

    /// <inheritdoc />
    public virtual bool IsAvailableForCurrentSite() => true;

    /// <summary>Creates an item the item into the system.</summary>
    /// <param name="transferableObject">The transferable object.</param>
    /// <param name="transactionName">The name of the transaction.</param>
    public virtual void CreateItem(WrapperObject transferableObject, string transactionName)
    {
      Type itemType = TypeResolutionService.ResolveType(transferableObject.GetPropertyOrDefault<string>("objectTypeId"), false);
      string propertyOrDefault1 = transferableObject.GetPropertyOrDefault<string>("Provider");
      Guid propertyOrDefault2 = transferableObject.GetPropertyOrDefault<Guid>("objectId");
      this.ItemsImporter.ImportItemInternal(transactionName, itemType, propertyOrDefault2, transferableObject, propertyOrDefault1, (ISiteSyncImportTransaction) new SiteSyncImportTransaction(), new Action<IDataItem, WrapperObject, IManager>(this.OnItemImported));
    }

    /// <summary>Extracts the items.</summary>
    /// <param name="parameters">The parameters.</param>
    /// <returns>
    /// Collection of <see cref="T:Telerik.Sitefinity.Publishing.WrapperObject" />
    /// </returns>
    public virtual IEnumerable<WrapperObject> Export(ExportParams parameters)
    {
      if (this.AllowToProcess(parameters.TypeName))
      {
        IEnumerable<string> languages = this.GetAvailableLanguages(parameters);
        foreach (IQueryable<object> query in this.GetItemsQueries(parameters))
        {
          int page = 0;
          while (true)
          {
            List<object> items = query.Skip<object>(page * parameters.BufferSize).Take<object>(parameters.BufferSize).ToList<object>();
            foreach (object item in items)
            {
              if (item is ILocalizable)
              {
                foreach (string language in languages)
                {
                  WrapperObject mappedItem = this.PreProcessExportObject(item, parameters, language);
                  if (mappedItem != null)
                  {
                    this.IgnoreProperties(mappedItem);
                    yield return mappedItem;
                  }
                }
              }
              else
              {
                WrapperObject mappedItem = this.PreProcessExportObject(item, parameters, string.Empty);
                if (mappedItem != null)
                {
                  this.IgnoreProperties(mappedItem);
                  yield return mappedItem;
                }
              }
            }
            if (items.Count<object>() >= parameters.BufferSize)
            {
              ++page;
              items = (List<object>) null;
            }
            else
              break;
          }
        }
      }
    }

    /// <inheritdoc />
    public virtual string GetExportFolderName(ExportParams parameters) => this.Area;

    /// <summary>Imports the items into the system.</summary>
    /// <param name="transferableObjects">The transferable objects.</param>
    /// <param name="parameters">The parameters.</param>
    /// <param name="itemCreatedAction">The item created action.</param>
    /// <param name="itemFailedAction">The item failed action.</param>
    [SuppressMessage("Microsoft.Globalization", "CA1305:SpecifyIFormatProvider", Justification = "Not needed", MessageId = "System.String.Format(System.String,System.Object)")]
    [SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes", Justification = "Not needed")]
    [SuppressMessage("StyleCopPlus.StyleCopPlusRules", "SP2101:MethodMustNotContainMoreLinesThan", Justification = "Method requires more than 65 lines.")]
    public virtual void Import(
      IEnumerable<WrapperObject> transferableObjects,
      ImportParams parameters,
      Action<WrapperObject, IEnumerable<ExportType>> itemCreatedAction,
      Action<WrapperObject, Exception> itemFailedAction)
    {
      if (!this.AllowToProcess(parameters.TypeName))
        return;
      this.OnImportStart(parameters);
      string transactionName1 = string.Empty;
      bool flag1 = false;
      bool flag2 = true;
      bool isItemCreated = false;
      WrapperObject wrapperObject = (WrapperObject) null;
      IEnumerable<ExportType> supportTypesList = this.GetSupportTypesList();
      foreach (WrapperObject transferableObject in transferableObjects)
      {
        try
        {
          string itemTypeValue = transferableObject.GetPropertyOrDefault<string>("objectTypeId");
          if (supportTypesList.Any<ExportType>((Func<ExportType, bool>) (t => t.TypeName.Equals(itemTypeValue))))
          {
            if (!flag1)
            {
              if (!flag2)
                this.TryCommitTransaction(wrapperObject, transactionName1, ref isItemCreated, itemFailedAction);
              else
                flag2 = false;
            }
            else
            {
              TransactionManager.RollbackTransaction(transactionName1);
              flag1 = false;
            }
            wrapperObject = transferableObject;
            transactionName1 = string.Format("{0}.sfpackage.xml", (object) Guid.NewGuid().ToString());
          }
          string language = transferableObject.GetProperty<string>("LangId");
          if (!string.IsNullOrEmpty(language))
          {
            if (!SystemManager.CurrentContext.AppSettings.AllLanguages.Any<KeyValuePair<int, CultureInfo>>((Func<KeyValuePair<int, CultureInfo>, bool>) (l => l.Value.Name.Equals(language, StringComparison.Ordinal))))
              continue;
          }
          if (SystemManager.CurrentContext.AppSettings.Multilingual && string.IsNullOrEmpty(language))
            language = SystemManager.CurrentContext.AppSettings.DefaultFrontendLanguage.Name;
          using (new CultureRegion(language))
          {
            this.CreateItem(this.OnItemImporting(transferableObject, parameters), transactionName1);
            if (itemCreatedAction != null)
              itemCreatedAction(transferableObject, supportTypesList);
            isItemCreated = true;
          }
        }
        catch (Exception ex)
        {
          flag1 = true;
          if (itemFailedAction != null)
            itemFailedAction(transferableObject, ex);
        }
      }
      if (!flag1)
        this.TryCommitTransaction(wrapperObject, transactionName1, ref isItemCreated, itemFailedAction);
      else
        TransactionManager.RollbackTransaction(transactionName1);
      foreach (object obj in this.ItemsImporter.ItemsToRemove)
      {
        string transactionName2 = string.Format("{0}.sfpackage.xml", (object) Guid.NewGuid().ToString());
        try
        {
          this.ItemsImporter.RemoveUnnecessaryItem(obj, transactionName2, true);
          TransactionManager.CommitTransaction(transactionName2);
        }
        catch (Exception ex)
        {
        }
      }
      this.OnImportComplete(parameters);
    }

    /// <inheritdoc />
    public event EventHandler<ItemImportedEventArgs> ItemImported;

    /// <inheritdoc />
    public virtual void Count(Stream fileStream, ScanOperation operation)
    {
      if (fileStream.Length <= 0L)
        return;
      XmlDocument xmlDocument = new XmlDocument();
      xmlDocument.Load(fileStream);
      XmlElement documentElement = xmlDocument.DocumentElement;
      XmlNamespaceManager nsmgr = new XmlNamespaceManager(xmlDocument.NameTable);
      nsmgr.AddNamespace("cmisra", "http://docs.oasis-open.org/ns/cmis/restatom/200908/");
      nsmgr.AddNamespace("cmis", "http://docs.oasis-open.org/ns/cmis/core/200908/");
      foreach (ExportType supportTypes in this.GetSupportTypesList())
      {
        string xpath = string.Format("./cmisra:object/cmis:properties[./cmis:propertyString[./cmis:value/text() = '{0}' and @propertyDefinitionId = 'cmis:objectTypeId']]/cmis:propertyId[@propertyDefinitionId='cmis:objectId']/cmis:value/text()", (object) supportTypes.TypeName);
        int count = documentElement.SelectNodes(xpath, nsmgr).Cast<XmlNode>().Where<XmlNode>((Func<XmlNode, bool>) (i => Guid.TryParse(i.Value, out Guid _))).Select<XmlNode, Guid>((Func<XmlNode, Guid>) (i => Guid.Parse(i.Value))).Distinct<Guid>().Count<Guid>();
        if (count > 0)
          operation.AddOnInfo.Entries.Add(this.GetAddOnEntry(supportTypes, count));
      }
    }

    /// <inheritdoc />
    public virtual IEnumerable<ExportType> GetSupportTypesList()
    {
      Stack<ExportType> exportTypes = new Stack<ExportType>(this.SupportedTypes);
      while (exportTypes.Any<ExportType>())
      {
        ExportType exportType = exportTypes.Pop();
        yield return exportType;
        foreach (ExportType childType in (IEnumerable<ExportType>) exportType.ChildTypes)
          exportTypes.Push(childType);
        exportType = (ExportType) null;
      }
    }

    /// <inheritdoc />
    protected virtual void DeleteImportedData(
      string sourceName,
      Type managerTypeName,
      IList<Type> itemTypes)
    {
      PackagingManager manager = PackagingManager.GetManager();
      Addon addon = manager.GetAddons().FirstOrDefault<Addon>((Expression<Func<Addon, bool>>) (a => a.Name == sourceName));
      if (addon == null)
        return;
      IEnumerable<string> itemTypeFullNames = itemTypes.Select<Type, string>((Func<Type, string>) (t => t.FullName));
      IOrderedQueryable<AddonLink> source1 = manager.GetAddonLinks().Where<AddonLink>((Expression<Func<AddonLink, bool>>) (l => l.AddonId == addon.Id && itemTypeFullNames.Contains<string>(l.ItemType))).OrderBy<AddonLink, string>((Expression<Func<AddonLink, string>>) (l => l.ItemType)).ThenBy<AddonLink, string>((Expression<Func<AddonLink, string>>) (l => l.ItemProvider));
      int num = 0;
      while (true)
      {
        List<AddonLink> list1 = source1.Take<AddonLink>(100).ToList<AddonLink>();
        foreach (IGrouping<\u003C\u003Ef__AnonymousType23<string, string>, AddonLink> source2 in list1.GroupBy(a => new
        {
          ItemType = a.ItemType,
          ItemProvider = a.ItemProvider
        }, (Func<AddonLink, AddonLink>) (a => a)))
        {
          List<Guid> list2 = source2.Select<AddonLink, Guid>((Func<AddonLink, Guid>) (d => d.ItemId)).ToList<Guid>();
          this.DeleteItems(managerTypeName, TypeResolutionService.ResolveType(source2.Key.ItemType), source2.Key.ItemProvider, (IList<Guid>) list2);
        }
        if (list1.Count >= 100)
          ++num;
        else
          break;
      }
    }

    /// <summary>
    /// Deletes the items with specified ids from the specified type.
    /// </summary>
    /// <param name="managerTypeName">The type of manager.</param>
    /// <param name="itemType">The item type.</param>
    /// <param name="provider">The provider.</param>
    /// <param name="itemIds">Ids of items to delete.</param>
    protected virtual void DeleteItems(
      Type managerTypeName,
      Type itemType,
      string provider,
      IList<Guid> itemIds)
    {
      IManager manager = ManagerBase.GetManager(managerTypeName, provider);
      foreach (IDataItem dataItem in manager.GetItems(itemType, (string) null, (string) null, 0, 0).Cast<IDataItem>().Where<IDataItem>((Func<IDataItem, bool>) (m => itemIds.Contains(m.Id))))
        manager.DeleteItem((object) dataItem);
      manager.SaveChanges();
    }

    protected virtual AddOnEntry GetAddOnEntry(ExportType type, int count) => new AddOnEntry()
    {
      DisplayName = type.DisplayName,
      AddonEntryType = AddOnEntryType.Content,
      Count = count
    };

    /// <summary>
    /// Gets the properties that are to be deleted prior the export starts.
    /// </summary>
    /// <value>The deleted properties.</value>
    protected virtual ISet<string> DeletedProperties => (ISet<string>) this.deletedProperties;

    /// <summary>Method called before all the items are imported</summary>
    /// <param name="parameters">The import parameters</param>
    protected virtual void OnImportStart(ImportParams parameters)
    {
    }

    /// <summary>Method called after all the items are imported</summary>
    /// <param name="parameters">The import parameters</param>
    protected virtual void OnImportComplete(ImportParams parameters)
    {
    }

    /// <summary>Called when [import item transaction committing].</summary>
    /// <param name="obj">The object.</param>
    /// <param name="transactionName">Name of the transaction.</param>
    protected virtual void OnImportTransactionCommitting(WrapperObject obj, string transactionName) => this.RaiseItemImportedEvent(obj, transactionName);

    /// <summary>Builds the export object.</summary>
    /// <param name="item">The item.</param>
    /// <param name="parameters">The export parameters.</param>
    /// <param name="language">The language to use.</param>
    /// <returns>The transferable object.</returns>
    protected virtual WrapperObject PreProcessExportObject(
      object item,
      ExportParams parameters,
      string language)
    {
      Type type = item.GetType();
      MappingSettings typeMappings;
      if (!this.typeMappingSettings.TryGetValue(type, out typeMappings))
      {
        typeMappings = new SiteSyncSerializer("Export/Import").GetTypeMappings(type);
        this.typeMappingSettings[type] = typeMappings;
      }
      WrapperObject mappedItem = new WrapperObject(item)
      {
        Language = language,
        MappingSettings = typeMappings
      };
      mappedItem.Language = language;
      mappedItem.MappingSettings = typeMappings;
      mappedItem.AddProperty("objectTypeId", (object) type.FullName);
      mappedItem.AddProperty("LangId", (object) language);
      mappedItem.AddProperty("ItemAction", (object) DataEventAction.Created);
      if (!string.IsNullOrEmpty(language))
      {
        IEnumerable<CultureInfo> propertyOrDefault = mappedItem.GetPropertyOrDefault<IEnumerable<CultureInfo>>("AvailableCultures");
        if (propertyOrDefault != null && !propertyOrDefault.Any<CultureInfo>((Func<CultureInfo, bool>) (ci => ci.Name == language)))
          return (WrapperObject) null;
      }
      if (item is IDataItem dataItem)
        mappedItem.AddProperty("objectId", (object) dataItem.Id);
      IDataItem parent = this.parentAccessor.GetParent((object) dataItem);
      if (parent != null)
      {
        mappedItem.AdditionalProperties["ParentId"] = (object) parent.Id;
        mappedItem.AdditionalProperties["ParentType"] = (object) parent.GetType().FullName;
      }
      if (item is Site site)
      {
        string json = new MultisiteService().GetSiteConfiguration(site.Id.ToString()).ToJson<SiteConfigurationViewModel>();
        mappedItem.AddProperty("SiteConfigurationViewModelProp", (object) json);
      }
      this.PreprocessClassificationObjects(type, dataItem, mappedItem);
      return mappedItem;
    }

    /// <summary>Modifies the import object.</summary>
    /// <param name="transferableObject">The transferable object.</param>
    /// <param name="parameters">The import parameters.</param>
    /// <returns>The modified transferable object.</returns>
    protected virtual WrapperObject OnItemImporting(
      WrapperObject transferableObject,
      ImportParams parameters)
    {
      transferableObject?.SetOrAddProperty("Provider", (object) parameters.ProviderName);
      return transferableObject;
    }

    /// <summary>Called prior item creation transaction is committed.</summary>
    /// <param name="dataItem">The data item.</param>
    /// <param name="obj">The wrapper object</param>
    /// <param name="manager">The item manager</param>
    protected virtual void OnItemImported(IDataItem dataItem, WrapperObject obj, IManager manager)
    {
      ILifecycleDataItemGeneric lifecycleDataItemGeneric1 = (ILifecycleDataItemGeneric) null;
      CultureInfo culture = (CultureInfo) null;
      if (SystemManager.CurrentContext.AppSettings.Multilingual)
      {
        culture = SystemManager.CurrentContext.Culture;
        string name = culture.Name;
      }
      if (dataItem is ILifecycleDataItem)
      {
        ILifecycleDataItem lifecycleDataItem = dataItem as ILifecycleDataItem;
        if (manager is ILifecycleManager)
        {
          ILifecycleManager lifecycleManager = manager as ILifecycleManager;
          lifecycleManager.Lifecycle.GetOrCreateLanguageData(lifecycleDataItem, culture);
          if (dataItem is ILifecycleDataItemGeneric)
          {
            ILifecycleDataItemGeneric lifecycleDataItemGeneric2 = dataItem as ILifecycleDataItemGeneric;
            if (lifecycleDataItemGeneric2.OriginalContentId != Guid.Empty)
            {
              lifecycleDataItemGeneric2.Status = ContentLifecycleStatus.Live;
              lifecycleDataItemGeneric2.Visible = true;
              try
              {
                lifecycleDataItemGeneric1 = lifecycleManager.GetItem(lifecycleDataItemGeneric2.GetType(), lifecycleDataItemGeneric2.OriginalContentId) as ILifecycleDataItemGeneric;
              }
              catch (ItemNotFoundException ex)
              {
                lifecycleDataItemGeneric1 = lifecycleManager.CreateItem(lifecycleDataItemGeneric2.GetType(), lifecycleDataItemGeneric2.OriginalContentId) as ILifecycleDataItemGeneric;
              }
              if (lifecycleDataItemGeneric1 == null)
                lifecycleDataItemGeneric1 = lifecycleManager.CreateItem(lifecycleDataItemGeneric2.GetType(), lifecycleDataItemGeneric2.OriginalContentId) as ILifecycleDataItemGeneric;
              if (lifecycleDataItemGeneric1 != null && lifecycleDataItemGeneric1.Status == ContentLifecycleStatus.Master)
              {
                lifecycleManager.Lifecycle.Edit(lifecycleDataItem, culture);
                if (lifecycleDataItemGeneric1 is IApprovalWorkflowItem)
                  this.SetApprovalWorkflowState(lifecycleDataItemGeneric1 as IApprovalWorkflowItem, culture);
                this.TrackTaxonomies(manager, (IDataItem) lifecycleDataItem);
                lifecycleManager.Lifecycle.Publish((ILifecycleDataItem) lifecycleDataItemGeneric1, culture);
              }
            }
          }
        }
      }
      if (!(lifecycleDataItemGeneric1 is IVersionSerializable))
        return;
      this.CreateVersion((IDataItem) lifecycleDataItemGeneric1, lifecycleDataItemGeneric1.Id, manager.TransactionName, culture);
    }

    protected IEnumerable<string> GetAvailableLanguages(ExportParams parameters)
    {
      IEnumerable<string> source = parameters.Languages;
      if (source == null || !source.Any<string>())
      {
        if (SystemManager.CurrentContext.AppSettings.Multilingual)
          source = ((IEnumerable<CultureInfo>) SystemManager.CurrentContext.CurrentSite.PublicContentCultures).Where<CultureInfo>((Func<CultureInfo, bool>) (c => !string.IsNullOrEmpty(c.Name))).Select<CultureInfo, string>((Func<CultureInfo, string>) (c => c.Name));
        else
          source = (IEnumerable<string>) new string[1]
          {
            SystemManager.CurrentContext.AppSettings.DefaultFrontendLanguage.Name
          };
      }
      return source;
    }

    protected void SetApprovalWorkflowState(IApprovalWorkflowItem item, CultureInfo culture)
    {
      if (culture == null)
        item.ApprovalWorkflowState = (Lstring) "Published";
      else
        item.ApprovalWorkflowState.SetString(culture, "Published");
    }

    /// <summary>Creates version of the imported item.</summary>
    /// <param name="item">The imported item.</param>
    /// <param name="itemId">The id of the item for which a version will be created.</param>
    /// <param name="transactionName">The name of the transaction in which the item is imported.</param>
    /// <param name="culture">The culture the imported item is in.</param>
    protected void CreateVersion(
      IDataItem item,
      Guid itemId,
      string transactionName,
      CultureInfo culture)
    {
      using (new CultureRegion(culture))
        VersionManager.GetManager((string) null, transactionName).CreateVersion((object) item, itemId, true);
    }

    /// <summary>Adds the dependencies.</summary>
    /// <param name="key">The key.</param>
    /// <param name="dependencies">The dependencies.</param>
    protected void AddDependencies(string key, params string[] dependencies)
    {
      if (this.cachedDependencies == null)
        this.cachedDependencies = (IDictionary<string, IEnumerable<string>>) new Dictionary<string, IEnumerable<string>>();
      if (this.cachedDependencies.ContainsKey(key))
        this.cachedDependencies[key] = this.cachedDependencies[key].Union<string>((IEnumerable<string>) dependencies);
      else
        this.cachedDependencies.Add(new KeyValuePair<string, IEnumerable<string>>(key, (IEnumerable<string>) dependencies));
    }

    /// <summary>Ignores the properties.</summary>
    /// <param name="mappedItem">The mapped item.</param>
    protected void IgnoreProperties(WrapperObject mappedItem)
    {
      if (mappedItem.MappingSettings == null || mappedItem.MappingSettings.Mappings == null || this.DeletedProperties == null || !this.DeletedProperties.Any<string>())
        return;
      for (int index = mappedItem.MappingSettings.Mappings.Count - 1; index >= 0; --index)
      {
        if (this.DeletedProperties.Contains(mappedItem.MappingSettings.Mappings[index].DestinationPropertyName))
          mappedItem.MappingSettings.Mappings.RemoveAt(index);
      }
      foreach (string key in mappedItem.AdditionalProperties.Where<KeyValuePair<string, object>>((Func<KeyValuePair<string, object>, bool>) (p => this.DeletedProperties.Contains(p.Key))).Select<KeyValuePair<string, object>, string>((Func<KeyValuePair<string, object>, string>) (p => p.Key)).ToList<string>())
        mappedItem.AdditionalProperties.Remove(key);
    }

    protected void DeleteProperty(WrapperObject mappedItem, string propertyName)
    {
      if (mappedItem == null)
        throw new ArgumentNullException("mappedItem is null. Cannot delete properties of null.");
      if (mappedItem.MappingSettings != null && mappedItem.MappingSettings.Mappings != null)
      {
        Mapping mapping = mappedItem.MappingSettings.Mappings.FirstOrDefault<Mapping>((Func<Mapping, bool>) (m => m.DestinationPropertyName == propertyName));
        if (mapping != null)
          mappedItem.MappingSettings.Mappings.Remove(mapping);
      }
      if (mappedItem.AdditionalProperties == null || !mappedItem.AdditionalProperties.ContainsKey(propertyName))
        return;
      mappedItem.AdditionalProperties.Remove(propertyName);
    }

    protected void TrackTaxonomies(IManager manager, IDataItem lifecycleItem)
    {
      if (!(manager.Provider.GetExecutionStateData("taxonomy_statistics_changes") is TaxonomyStatisticsTracker executionStateData))
        return;
      executionStateData.Track((object) lifecycleItem, manager.Provider);
    }

    private void TryCommitTransaction(
      WrapperObject obj,
      string transactionName,
      ref bool isItemCreated,
      Action<WrapperObject, Exception> itemFailedAction)
    {
      try
      {
        if (isItemCreated)
        {
          this.OnImportTransactionCommitting(obj, transactionName);
          TransactionManager.CommitTransaction(transactionName);
        }
      }
      catch (Exception ex)
      {
        TransactionManager.RollbackTransaction(transactionName);
        if (itemFailedAction != null)
          itemFailedAction(obj, ex);
      }
      isItemCreated = false;
    }

    private void PreprocessClassificationObjects(
      Type itemType,
      IDataItem dataItem,
      WrapperObject mappedItem)
    {
      TaxonomyManager manager = TaxonomyManager.GetManager();
      if (typeof (Taxon).IsAssignableFrom(itemType))
      {
        ITaxon taxon = manager.GetTaxon(dataItem.Id);
        if (taxon == null)
          return;
        mappedItem.AddProperty("TaxonomyId", (object) taxon.Taxonomy.Id);
        mappedItem.AddProperty("TaxonomyType", (object) taxon.Taxonomy.GetType().FullName);
        if (!(taxon is HierarchicalTaxon) || ((HierarchicalTaxon) taxon).Parent == null)
          return;
        mappedItem.AdditionalProperties["ParentId"] = (object) ((HierarchicalTaxon) taxon).Parent.Id;
        mappedItem.AdditionalProperties["ParentType"] = (object) ((HierarchicalTaxon) taxon).Parent.GetType().FullName;
      }
      else
      {
        if (!typeof (Taxonomy).IsAssignableFrom(itemType))
          return;
        ITaxonomy taxonomy = manager.GetTaxonomy(dataItem.Id);
        List<Guid> list = manager.GetRelatedSites(taxonomy).Select<ISite, Guid>((Func<ISite, Guid>) (s => s.Id)).ToList<Guid>();
        mappedItem.AddProperty("SiteIds", (object) list);
      }
    }

    private bool ContainsType(IEnumerable<ExportType> exportTypes, string typeName)
    {
      foreach (ExportType exportType in exportTypes)
      {
        if (exportType.TypeName == typeName || exportType.ChildTypes.Count > 0 && this.ContainsType((IEnumerable<ExportType>) exportType.ChildTypes, typeName))
          return true;
      }
      return false;
    }

    private void RaiseItemImportedEvent(WrapperObject transferableObject, string transactionName)
    {
      string propertyOrDefault1 = transferableObject.GetPropertyOrDefault<string>("objectTypeId");
      string propertyOrDefault2 = transferableObject.GetPropertyOrDefault<string>("Provider");
      Guid guid = Guid.Empty;
      if (transferableObject.HasProperty("OriginalContentId"))
        guid = transferableObject.GetPropertyOrDefault<Guid>("OriginalContentId");
      if (guid == Guid.Empty)
        guid = transferableObject.GetPropertyOrDefault<Guid>("objectId");
      ItemImportedEventArgs e = new ItemImportedEventArgs()
      {
        ItemId = guid,
        ItemProvider = propertyOrDefault2,
        ItemType = propertyOrDefault1,
        TransactionName = transactionName
      };
      if (this.ItemImported == null)
        return;
      this.ItemImported((object) this, e);
    }
  }
}
