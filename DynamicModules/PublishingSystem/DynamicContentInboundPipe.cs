// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.DynamicModules.PublishingSystem.DynamicContentInboundPipe
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.Data.Linq.Dynamic;
using Telerik.Sitefinity.Data.QueryBuilder;
using Telerik.Sitefinity.DynamicModules.Builder;
using Telerik.Sitefinity.DynamicModules.Builder.Install;
using Telerik.Sitefinity.DynamicModules.Builder.Model;
using Telerik.Sitefinity.DynamicModules.Model;
using Telerik.Sitefinity.GenericContent.Model;
using Telerik.Sitefinity.Lifecycle;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.Modules;
using Telerik.Sitefinity.Modules.Pages;
using Telerik.Sitefinity.Pages.Model;
using Telerik.Sitefinity.Publishing;
using Telerik.Sitefinity.Publishing.Model;
using Telerik.Sitefinity.Publishing.NewImplementation;
using Telerik.Sitefinity.Publishing.Pipes;
using Telerik.Sitefinity.Security;
using Telerik.Sitefinity.Security.Model;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Utilities.TypeConverters;
using Telerik.Sitefinity.Web;
using Telerik.Sitefinity.Web.DataResolving;
using Telerik.Sitefinity.Web.Model;

namespace Telerik.Sitefinity.DynamicModules.PublishingSystem
{
  [PipeDesigner(typeof (DynamicContentPipeDesignerView), null)]
  public class DynamicContentInboundPipe : 
    BasePipe<SitefinityContentPipeSettings>,
    IDynamicPipe,
    IPipe,
    IPushPipe,
    IPullPipe,
    IQueryablePullPipe,
    IQueryablePullable,
    IInboundPipe
  {
    private IPublishingPointBusinessObject publishingPoint;
    private IDefinitionField[] definitionFields;
    private string pipeName;
    private readonly ProvidersComponent providersComponent = new ProvidersComponent();
    protected ModuleBuilderManager moduleBuilderManager;
    private Type contentType;
    private const int ContentItemsChunkSize = 500;

    /// <summary>Gets an instance of the ModuleBuilderManager</summary>
    protected ModuleBuilderManager ModuleBuilderManager
    {
      get
      {
        if (this.moduleBuilderManager == null)
          this.moduleBuilderManager = ModuleBuilderManager.GetManager();
        return this.moduleBuilderManager;
      }
    }

    private Type ContentType
    {
      get
      {
        if (this.contentType == (Type) null)
          this.contentType = this.ResolveTypeFromContentSettings();
        return this.contentType;
      }
    }

    private SitefinityContentPipeSettings ContentSettings => this.PipeSettings is SitefinityContentPipeSettings pipeSettings ? pipeSettings : throw new InvalidOperationException("Pipe settings required!");

    /// <summary>Sets the name of the Pipe</summary>
    public void SetPipeName(string pipeName) => this.pipeName = pipeName;

    /// <summary>
    /// Defines the data structure of the medium this pipe works with
    /// </summary>
    public override IDefinitionField[] Definition
    {
      get
      {
        if (this.definitionFields == null)
        {
          string contentType = (string) null;
          if (this.PipeSettingsInternal != null)
            contentType = this.PipeSettingsInternal.ContentTypeName;
          this.definitionFields = string.IsNullOrEmpty(contentType) || !PublishingSystemFactory.ContentPipeDefinitionsRegistered(this.Name, contentType) ? PublishingSystemFactory.GetPipeDefinitions(this.Name) : PublishingSystemFactory.GetContentPipeDefinitions(this.Name, contentType);
        }
        return this.definitionFields;
      }
    }

    /// <summary>Initializes the specified pipe settings.</summary>
    public override void Initialize(PipeSettings pipeSettings)
    {
      this.PipeSettingsInternal = pipeSettings is SitefinityContentPipeSettings ? pipeSettings as SitefinityContentPipeSettings : throw new ArgumentException("Expected pipe settings of type SitefinityContentPipeSettings");
      this.publishingPoint = PublishingSystemFactory.GetPublishingPoint(this.PipeSettingsInternal.PublishingPoint);
      this.definitionFields = (IDefinitionField[]) null;
    }

    /// <summary>
    /// Determines whether this instance [can process item] the specified item.
    /// </summary>
    /// <param name="item">The item.</param>
    /// <returns>
    /// 	<c>true</c> if this instance [can process item] the specified item; otherwise, <c>false</c>.
    /// </returns>
    public override bool CanProcessItem(object item)
    {
      if (item == null)
        return false;
      if (item is PublishingSystemEventInfo)
      {
        PublishingSystemEventInfo publishingSystemEventInfo = (PublishingSystemEventInfo) item;
        return this.IsItemSupported(publishingSystemEventInfo) && publishingSystemEventInfo.ItemAction == "SystemObjectDeleted" || this.CanProcessItem(publishingSystemEventInfo.Item);
      }
      if (item is WrapperObject)
        return this.CanProcessItem(((WrapperObject) item).WrappedObject);
      return item.GetType().FullName.Equals(this.PipeSettingsInternal.ContentTypeName, StringComparison.InvariantCultureIgnoreCase) && this.providersComponent.CheckProvider(this.PipeSettings.PublishingPoint, (IDataItem) item);
    }

    /// <summary>Gets the name.</summary>
    public override string Name => this.pipeName;

    /// <summary>Gets or sets the pipe settings.</summary>
    public override PipeSettings PipeSettings
    {
      get => (PipeSettings) this.PipeSettingsInternal;
      set => this.Initialize(value);
    }

    /// <summary>Gets the pipe settings short description.</summary>
    public override string GetPipeSettingsShortDescription(PipeSettings initSettings)
    {
      string typeName = (string) null;
      string typeNamespace = (string) null;
      ModuleNamesGenerator.ResolveDynamicTypeNameFromPipeName(initSettings.PipeName, out typeNamespace, out typeName);
      return this.ModuleBuilderManager.GetDynamicModule(this.ModuleBuilderManager.GetDynamicModuleTypes().Where<DynamicModuleType>((Expression<Func<DynamicModuleType, bool>>) (t => t.TypeName == typeName && t.TypeNamespace == typeNamespace)).Single<DynamicModuleType>().ParentModuleId).Title + " : " + Res.Get<ModuleBuilderResources>().AllItems;
    }

    /// <summary>Pushes the data.</summary>
    public virtual void PushData(IList<PublishingSystemEventInfo> items)
    {
      IPublishingPointBusinessObject publishingPoint = this.publishingPoint;
      List<WrapperObject> items1 = new List<WrapperObject>();
      List<WrapperObject> items2 = new List<WrapperObject>();
      foreach (PublishingSystemEventInfo publishingSystemEventInfo in (IEnumerable<PublishingSystemEventInfo>) items)
      {
        try
        {
          WrapperObject wrapperObject = new WrapperObject(this.PipeSettings, publishingSystemEventInfo.Item);
          wrapperObject.Language = this.IsDynamicContentTypeMultilingual() ? publishingSystemEventInfo.Language : (string) null;
          if (this.PipeSettingsInternal.LanguageIds.Count > 0)
          {
            if (!this.PipeSettingsInternal.LanguageIds.Contains(wrapperObject.Language))
              continue;
          }
          string itemAction = publishingSystemEventInfo.ItemAction;
          if (!(itemAction == "SystemObjectDeleted"))
          {
            if (!(itemAction == "SystemObjectAdded"))
            {
              if (itemAction == "SystemObjectModified")
              {
                DynamicContent dynamicContentItem = this.GetDynamicContentItem(publishingSystemEventInfo);
                this.SetProperties(wrapperObject, dynamicContentItem);
                items2.Add(wrapperObject);
                items1.Add(wrapperObject);
              }
            }
            else
            {
              DynamicContent dynamicContentItem = this.GetDynamicContentItem(publishingSystemEventInfo);
              this.SetProperties(wrapperObject, dynamicContentItem);
              items1.Add(wrapperObject);
            }
          }
          else
          {
            PropertyDescriptorCollection properties = TypeDescriptor.GetProperties((object) wrapperObject);
            Guid propertyValue = wrapperObject.GetPropertyValue<Guid>(properties, "OriginalContentId");
            if (propertyValue != Guid.Empty)
              wrapperObject.AddProperty("OriginalItemId", (object) propertyValue);
            items2.Add(wrapperObject);
          }
        }
        catch (Exception ex)
        {
          this.HandleError("Error when push data for item action {0} for item {1}.".Arrange((object) publishingSystemEventInfo.ItemAction, publishingSystemEventInfo.Item), ex);
        }
      }
      if (items2.Count > 0)
        publishingPoint.RemoveItems((IList<WrapperObject>) items2);
      if (items1.Count <= 0)
        return;
      publishingPoint.AddItems((IList<WrapperObject>) items1);
    }

    /// <summary>Gets the data.</summary>
    public virtual IQueryable<WrapperObject> GetData() => this.GetWrappedDynamicItems((string) null, (string) null, 0, 0);

    /// <inheritdoc />
    IQueryable<WrapperObject> IQueryablePullable.GetItems(
      string filter,
      string order,
      int skip,
      int take)
    {
      return this.GetWrappedDynamicItems(filter, order, skip, take);
    }

    /// <summary>Ties the publishing point.</summary>
    public virtual void ToPublishingPoint()
    {
      string name = SystemManager.CurrentContext.AppSettings.DefaultFrontendLanguage.Name;
      bool flag = (uint) this.PipeSettings.LanguageIds.Count > 0U;
      string language1 = this.PipeSettings.LanguageIds.FirstOrDefault<string>();
      int num1 = 0;
      int num2 = 0;
      while (true)
      {
        DynamicContent[] array = this.LoadDynamicContentItems((string) null, (string) null, num1 * 500, 500).ToArray<DynamicContent>();
        if (array.Length != 0)
        {
          List<PublishingSystemEventInfo> publishingSystemEventInfoList = new List<PublishingSystemEventInfo>();
          foreach (DynamicContent contentItem in array)
          {
            if (flag)
            {
              DynamicContentInboundPipe.AddModifiedEventInfo(publishingSystemEventInfoList, contentItem, language1);
            }
            else
            {
              ILifecycleDataItem lifecycleDataItem = (ILifecycleDataItem) contentItem;
              if (lifecycleDataItem != null)
              {
                IList<string> publishedTranslations = lifecycleDataItem.PublishedTranslations;
                if (!this.IsDynamicContentTypeMultilingual())
                  DynamicContentInboundPipe.AddModifiedEventInfo(publishingSystemEventInfoList, contentItem, (string) null);
                else if (publishedTranslations.Count == 0)
                {
                  DynamicContentInboundPipe.AddModifiedEventInfo(publishingSystemEventInfoList, contentItem, name);
                }
                else
                {
                  foreach (string language2 in (IEnumerable<string>) publishedTranslations)
                    DynamicContentInboundPipe.AddModifiedEventInfo(publishingSystemEventInfoList, contentItem, language2);
                }
              }
            }
          }
          this.PushDataInternal((IList<PublishingSystemEventInfo>) publishingSystemEventInfoList);
          ++num1;
          num2 += array.Length;
        }
        else
          break;
      }
    }

    private void PushDataInternal(IList<PublishingSystemEventInfo> items) => this.PushData(items);

    private static void AddModifiedEventInfo(
      List<PublishingSystemEventInfo> eventInfo,
      DynamicContent contentItem,
      string language)
    {
      PublishingSystemEventInfo publishingSystemEventInfo = new PublishingSystemEventInfo()
      {
        Item = (object) contentItem,
        ItemAction = "SystemObjectModified",
        Language = language
      };
      eventInfo.Add(publishingSystemEventInfo);
    }

    /// <summary>Gets the wrapped dynamic items.</summary>
    protected virtual IQueryable<WrapperObject> GetWrappedDynamicItems(
      string filter,
      string order,
      int skip,
      int take)
    {
      // ISSUE: object of a compiler-generated type is created
      // ISSUE: variable of a compiler-generated type
      DynamicContentInboundPipe.\u003C\u003Ec__DisplayClass23_0 cDisplayClass230 = new DynamicContentInboundPipe.\u003C\u003Ec__DisplayClass23_0();
      // ISSUE: reference to a compiler-generated field
      cDisplayClass230.\u003C\u003E4__this = this;
      IAppSettings appSettings = SystemManager.CurrentContext.AppSettings;
      // ISSUE: reference to a compiler-generated field
      cDisplayClass230.defaultFrontendLanguage = appSettings.DefaultFrontendLanguage.Name;
      IQueryable<DynamicContent> source1 = this.LoadDynamicContentItems(filter, order, skip, take);
      IQueryable<WrapperObject> source2;
      if (!this.IsDynamicContentTypeMultilingual())
      {
        source2 = source1.Select<DynamicContent, WrapperObject>((Expression<Func<DynamicContent, WrapperObject>>) (item => new WrapperObject(item)
        {
          Language = default (string)
        }));
      }
      else
      {
        // ISSUE: reference to a compiler-generated field
        source2 = source1.SelectMany<DynamicContent, WrapperObject>((Expression<Func<DynamicContent, IEnumerable<WrapperObject>>>) ((item, parameterExpression) => item.PublishedTranslations.Count == 0 ? (IEnumerable<WrapperObject>) new WrapperObject[]
        {
          new WrapperObject(item)
          {
            Language = cDisplayClass230.defaultFrontendLanguage
          }
        } : item.PublishedTranslations.Select<string, WrapperObject>((Expression<Func<string, WrapperObject>>) ((item, parameterExpression) => new WrapperObject(item)
        {
          Language = parameterExpression
        }))));
      }
      if (this.PipeSettingsInternal.LanguageIds.Count > 0 && this.IsDynamicContentTypeMultilingual())
        source2 = source2.Where<WrapperObject>((Expression<Func<WrapperObject, bool>>) (item => this.PipeSettingsInternal.LanguageIds.Contains(item.Language)));
      return source2.Select<WrapperObject, WrapperObject>((Expression<Func<WrapperObject, WrapperObject>>) (i => this.SetWrapperObjectProperties(i)));
    }

    /// <summary>Loads the dynamic content items.</summary>
    protected IQueryable<DynamicContent> LoadDynamicContentItems() => this.LoadDynamicContentItems((string) null, (string) null, 0, 0);

    /// <summary>Loads the dynamic content items.</summary>
    protected virtual IQueryable<DynamicContent> LoadDynamicContentItems(
      string filter,
      string order,
      int skip,
      int take)
    {
      IEnumerable<string> providers = this.providersComponent.GetProviders((IManager) DynamicModuleManager.GetManager(this.ContentSettings.ProviderName), this.ContentType, this.ContentSettings.PublishingPoint);
      IQueryable<DynamicContent> left = (IQueryable<DynamicContent>) null;
      foreach (string providerName in providers)
      {
        DynamicModuleManager manager = DynamicModuleManager.GetManager(providerName);
        string predicate = this.PipeSettings.PublishingPoint.Settings.ItemFilterStrategy == PublishingItemFilter.Live ? PredefinedFilters.PublishedItemsRegardlessSheduledDateFilter() : PredefinedFilters.AllItemsExceptTemp();
        if (filter != null)
          predicate = string.Format("({0}) AND ({1})", (object) predicate, (object) filter);
        QueryData expressionQueryData = this.ContentSettings.FilterExpressionQueryData;
        if (expressionQueryData != null)
          predicate = string.Format("({0}) AND ({1})", (object) LinqTranslator.ToDynamicLinq(expressionQueryData).Trim(), (object) predicate);
        IQueryable<DynamicContent> source1 = manager.GetDataItems(this.ContentType).Where<DynamicContent>(predicate);
        IQueryable<DynamicContent> source2;
        if (order != null)
          source2 = source1.OrderBy<DynamicContent>(order);
        else
          source2 = (IQueryable<DynamicContent>) source1.OrderByDescending<DynamicContent, DateTime>((Expression<Func<DynamicContent, DateTime>>) (i => i.LastModified));
        if (skip > 0)
          source2 = source2.Skip<DynamicContent>(skip);
        if (take > 0)
          source2 = source2.Take<DynamicContent>(take);
        else if (this.ContentSettings.MaxItems > 0)
          source2 = source2.Take<DynamicContent>(this.ContentSettings.MaxItems);
        IEnumerable<DynamicContent> right = Enumerable.OfType<DynamicContent>(source2);
        left = left.UnionOrRight<DynamicContent>(right);
      }
      return left ?? Enumerable.Empty<DynamicContent>().AsQueryable<DynamicContent>();
    }

    /// <summary>Gets the dynamic content item object.</summary>
    /// <param name="item">The instance of the <see cref="T:Telerik.Sitefinity.Publishing.PublishingSystemEventInfo" />.</param>
    /// <returns>The instance of the <see cref="T:Telerik.Sitefinity.DynamicModules.Model.DynamicContent" />.</returns>
    protected virtual DynamicContent GetDynamicContentItem(
      PublishingSystemEventInfo item)
    {
      return !(item.Item is WrapperObject) ? (DynamicContent) item.Item : this.GetDynamicContentItem((WrapperObject) item.Item);
    }

    /// <summary>Gets the content item.</summary>
    protected virtual DynamicContent GetDynamicContentItem(WrapperObject wrappedObject)
    {
      if (wrappedObject == null)
        return (DynamicContent) null;
      if (wrappedObject.WrappedObject == null)
        return (DynamicContent) null;
      return wrappedObject.WrappedObject is DynamicContent ? (DynamicContent) wrappedObject.WrappedObject : this.GetDynamicContentItem((WrapperObject) wrappedObject.WrappedObject);
    }

    /// <summary>
    /// Sets the properties of the wrapper object of the dynamic content item.
    /// </summary>
    /// <param name="wrapperObject">The wrapper object.</param>
    /// <param name="contentItem">The content item.</param>
    protected virtual void SetProperties(WrapperObject wrapperObject, DynamicContent contentItem)
    {
      PropertyDescriptorCollection properties = TypeDescriptor.GetProperties((object) wrapperObject);
      PropertyDescriptor propertyDescriptor1 = properties.Find("Owner", false);
      if (propertyDescriptor1 != null && properties.Find("Username", false) == null)
      {
        object userId = propertyDescriptor1.GetValue((object) wrapperObject);
        this.SetUserProperties(wrapperObject, (Guid) userId);
      }
      string itemUrl = this.GetItemUrl(contentItem);
      if (!string.IsNullOrEmpty(itemUrl))
      {
        PropertyDescriptor propertyDescriptor2 = properties.Find("Link", false);
        if (propertyDescriptor2 == null)
          wrapperObject.AddProperty("Link", (object) itemUrl);
        else
          propertyDescriptor2.SetValue((object) wrapperObject, (object) itemUrl);
      }
      PropertyDescriptor propertyDescriptor3 = properties.Find("PipeId", false);
      if (propertyDescriptor3 == null)
        wrapperObject.AddProperty("PipeId", (object) this.PipeSettings.Id);
      else
        propertyDescriptor3.SetValue((object) wrapperObject, (object) this.PipeSettings.Id);
      PropertyDescriptor propertyDescriptor4 = properties.Find("ContentType", false);
      if (propertyDescriptor4 == null)
        wrapperObject.AddProperty("ContentType", (object) this.PipeSettingsInternal.ContentTypeName);
      else
        propertyDescriptor4.SetValue((object) wrapperObject, (object) this.PipeSettingsInternal.ContentTypeName);
      wrapperObject.AddProperty("OriginalItemId", (object) contentItem.OriginalContentId);
      string providerName = this.GetProviderName(wrapperObject);
      PropertyDescriptor propertyDescriptor5 = properties.Find("Provider", true);
      if (propertyDescriptor5 != null)
        propertyDescriptor5.SetValue((object) wrapperObject, (object) providerName);
      else
        wrapperObject.AddProperty("Provider", (object) providerName);
      if (this.PipeSettings.Mappings.Mappings.Count == 0)
      {
        Type contentType = contentItem.GetType();
        DynamicModuleType moduleType = this.ModuleBuilderManager.GetDynamicModuleTypes().Where<DynamicModuleType>((Expression<Func<DynamicModuleType, bool>>) (t => t.TypeName == contentType.Name && t.TypeNamespace == contentType.Namespace)).Single<DynamicModuleType>();
        IQueryable<DynamicModuleField> dynamicModuleFields = this.ModuleBuilderManager.GetDynamicModuleFields();
        Expression<Func<DynamicModuleField, bool>> predicate = (Expression<Func<DynamicModuleField, bool>>) (f => f.ParentTypeId == moduleType.Id);
        foreach (DynamicModuleField dynamicModuleField in (IEnumerable<DynamicModuleField>) dynamicModuleFields.Where<DynamicModuleField>(predicate))
        {
          if (dynamicModuleField.SpecialType != FieldSpecialType.Actions && dynamicModuleField.SpecialType != FieldSpecialType.Translations)
            wrapperObject.AddProperty(dynamicModuleField.Name, contentItem.GetValue(dynamicModuleField.Name));
        }
      }
      PublishingUtilities.AddItemCategories(wrapperObject, (IDataItem) contentItem);
      PublishingUtilities.AddContentUsages(wrapperObject, (IDataItem) contentItem);
      PublishingUtilities.AddContentLifecycleStatus(wrapperObject, (IDataItem) contentItem);
    }

    /// <summary>Sets the user properties.</summary>
    /// <param name="item">The item.</param>
    /// <param name="userId">The user id.</param>
    protected virtual void SetUserProperties(WrapperObject item, Guid userId)
    {
      if (!(userId != Guid.Empty))
        return;
      User user = UserManager.GetManager().GetUser(userId);
      if (user == null)
        return;
      item.AddProperty("Username", (object) user.UserName);
      item.AddProperty("OwnerFirstName", (object) user.FirstName);
      item.AddProperty("OwnerLastName", (object) user.LastName);
      item.AddProperty("OwnerEmail", (object) user.Email);
    }

    /// <summary>Gets the URL of the content item.</summary>
    /// <param name="contentItem">The content item.</param>
    /// <returns></returns>
    protected virtual string GetItemUrl(DynamicContent contentItem)
    {
      string itemUrl = (string) null;
      Guid? defaultPageId = this.GetDefaultPageId(contentItem);
      if (defaultPageId.HasValue)
      {
        Guid pageId = defaultPageId.Value;
        PageNode pageNode = PageManager.GetManager().GetPageNodes().SingleOrDefault<PageNode>((Expression<Func<PageNode, bool>>) (n => n.Id == pageId));
        if (pageNode != null)
          itemUrl = RouteHelper.ResolveUrl(DataResolver.Resolve((object) contentItem, "URL", (string) null, pageNode.Id.ToString()), UrlResolveOptions.Absolute);
      }
      return itemUrl;
    }

    /// <summary>Gets the default page id for the given content.</summary>
    /// <param name="item">The content.</param>
    /// <returns>The page id.</returns>
    protected virtual Guid? GetDefaultPageId(DynamicContent item)
    {
      if (this.PipeSettingsInternal.BackLinksPageId.HasValue)
        return new Guid?();
      Guid? defaultPageId;
      if (item is IHasParent)
      {
        for (Content parent = ((IHasParent) item).Parent; parent != null; parent = ((IHasParent) parent).Parent)
        {
          defaultPageId = parent.DefaultPageId;
          if (defaultPageId.HasValue)
            return parent.DefaultPageId;
          if (!(parent is IHasParent))
            break;
        }
      }
      defaultPageId = new Guid?();
      return defaultPageId;
    }

    private WrapperObject SetWrapperObjectProperties(WrapperObject item)
    {
      item.MappingSettings = this.PipeSettingsInternal.Mappings;
      DynamicContent wrappedObject = (DynamicContent) item.WrappedObject;
      this.SetProperties(item, wrappedObject);
      return item;
    }

    private string GetProviderName(WrapperObject convertedItem) => this.GetDynamicContentItem(convertedItem).GetProviderName();

    private Type ResolveTypeFromContentSettings()
    {
      string typeName = (string) null;
      string typeNamespace = (string) null;
      ModuleNamesGenerator.ResolveDynamicTypeNameFromPipeName(this.ContentSettings.PipeName, out typeNamespace, out typeName);
      return TypeResolutionService.ResolveType(typeNamespace + "." + typeName);
    }

    private bool IsDynamicContentTypeMultilingual()
    {
      DynamicModuleType dynamicModuleType = this.ModuleBuilderManager.GetDynamicModuleType(this.ContentType);
      this.ModuleBuilderManager.LoadDynamicModuleTypeGraph(dynamicModuleType, false);
      return ModuleInstallerHelper.ContainsLocalizableFields((IEnumerable<IDynamicModuleField>) dynamicModuleType.Fields);
    }

    private bool IsItemSupported(PublishingSystemEventInfo item) => string.Equals(this.PipeSettingsInternal.ContentTypeName, item.ItemType);
  }
}
