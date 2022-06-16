// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Publishing.Pipes.ContentInboundPipe
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.Data.QueryBuilder;
using Telerik.Sitefinity.GenericContent.Model;
using Telerik.Sitefinity.Lifecycle;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.Modules;
using Telerik.Sitefinity.Modules.Pages;
using Telerik.Sitefinity.Pages.Model;
using Telerik.Sitefinity.Publishing.Model;
using Telerik.Sitefinity.Publishing.NewImplementation;
using Telerik.Sitefinity.Publishing.Web.UI.Designers;
using Telerik.Sitefinity.Security;
using Telerik.Sitefinity.Security.Model;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Utilities.TypeConverters;
using Telerik.Sitefinity.Web.DataResolving;
using Telerik.Sitefinity.Web.Model;

namespace Telerik.Sitefinity.Publishing.Pipes
{
  /// <summary>The content pipe</summary>
  [PipeDesigner(typeof (ContentPipeDesignerView), null)]
  public class ContentInboundPipe : 
    BasePipe<SitefinityContentPipeSettings>,
    IPushPipe,
    IPullPipe,
    IInboundPipe
  {
    private ISet<string> pipeSettingsLanguages;
    private IDefinitionField[] definitionFields;
    private IPublishingPointBusinessObject publishingPoint;
    private ProvidersComponent providersComponent = new ProvidersComponent();
    private int maxItems;
    public const string PipeName = "ContentInboundPipe";
    private const int ContentItemsChunkSize = 500;
    public static readonly string AllItems = Res.Get<PublishingMessages>().AllItems;

    /// <summary>Gets the name.</summary>
    /// <value>The name.</value>
    public override string Name => nameof (ContentInboundPipe);

    /// <summary>
    /// Defines the data structure of the medium this pipe works with
    /// </summary>
    /// <value></value>
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

    /// <summary>Gets or sets the pipe settings.</summary>
    /// <value>The pipe settings.</value>
    public override PipeSettings PipeSettings
    {
      get => (PipeSettings) this.PipeSettingsInternal;
      set => this.Initialize(value);
    }

    internal ProvidersComponent ProvidersComponent
    {
      get => this.providersComponent;
      set => this.providersComponent = value;
    }

    /// <summary>
    /// Used to get the default settings for this pipe, without creating persistent object
    /// </summary>
    /// <returns>The settings</returns>
    public static SitefinityContentPipeSettings GetTemplatePipeSettings()
    {
      SitefinityContentPipeSettings templatePipeSettings = new SitefinityContentPipeSettings();
      templatePipeSettings.ContentTypeName = "Telerik.Sitefinity.Blogs.Model.BlogPost";
      templatePipeSettings.IsInbound = true;
      templatePipeSettings.PipeName = nameof (ContentInboundPipe);
      templatePipeSettings.IsActive = true;
      templatePipeSettings.MaxItems = 0;
      templatePipeSettings.InvocationMode = PipeInvokationMode.Push;
      return templatePipeSettings;
    }

    /// <summary>Used to get the default mappings for this pipe</summary>
    /// <returns>The settings</returns>
    public static List<Mapping> GetDefaultMappings() => new List<Mapping>()
    {
      PublishingSystemFactory.CreateMapping("Title", "concatenationtranslator", true, "Title"),
      PublishingSystemFactory.CreateMapping("PublicationDate", "TransparentTranslator", false, "PublicationDate"),
      PublishingSystemFactory.CreateMapping("LastModified", "TransparentTranslator", false, "LastModified"),
      PublishingSystemFactory.CreateMapping("Link", "TransparentTranslator", true, "Link"),
      PublishingSystemFactory.CreateMapping("Content", "concatenationtranslator", true, "Content"),
      PublishingSystemFactory.CreateMapping("Summary", "concatenationtranslator", true, "Summary"),
      PublishingSystemFactory.CreateMapping("Categories", "TransparentTranslator", false, "Categories"),
      PublishingSystemFactory.CreateMapping("Username", "TransparentTranslator", false, "Username"),
      PublishingSystemFactory.CreateMapping("Owner", "TransparentTranslator", false, "Owner"),
      PublishingSystemFactory.CreateMapping("OwnerFirstName", "TransparentTranslator", false, "OwnerFirstName"),
      PublishingSystemFactory.CreateMapping("OwnerLastName", "TransparentTranslator", false, "OwnerLastName"),
      PublishingSystemFactory.CreateMapping("OwnerEmail", "TransparentTranslator", false, "OwnerEmail"),
      PublishingSystemFactory.CreateMapping("Id", "TransparentTranslator", true, "Id"),
      PublishingSystemFactory.CreateMapping("OriginalItemId", "TransparentTranslator", true, "OriginalContentId"),
      PublishingSystemFactory.CreateMapping("OriginalParentId", "TransparentTranslator", false, "OriginalParentId"),
      PublishingSystemFactory.CreateMapping("ExpirationDate", "TransparentTranslator", false, "ExpirationDate"),
      PublishingSystemFactory.CreateMapping("ItemHash", "TransparentTranslator", false, "ItemHash"),
      PublishingSystemFactory.CreateMapping("PipeId", "TransparentTranslator", false, "PipeId"),
      PublishingSystemFactory.CreateMapping("ContentType", "TransparentTranslator", false, "ContentType"),
      PublishingSystemFactory.CreateMapping("LifecycleStatus", "TransparentTranslator", false, "LifecycleStatus")
    };

    /// <summary>Initializes the specified pipe settings.</summary>
    /// <param name="pipeSettings">The pipe settings.</param>
    public override void Initialize(PipeSettings pipeSettings)
    {
      this.PipeSettingsInternal = pipeSettings as SitefinityContentPipeSettings;
      if (this.PipeSettingsInternal == null)
        throw new ArgumentException("Expected pipe settings of type SitefinityContentPipeSettings");
      this.publishingPoint = PublishingSystemFactory.GetPublishingPoint(this.PipeSettings.PublishingPoint);
      this.maxItems = this.PipeSettingsInternal.MaxItems;
      IList<string> languageIds = this.PipeSettingsInternal.LanguageIds;
      if (languageIds.Count > 0)
        this.pipeSettingsLanguages = (ISet<string>) new HashSet<string>((IEnumerable<string>) languageIds);
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
      switch (item)
      {
        case null:
          return false;
        case PublishingSystemEventInfo publishingSystemEventInfo:
          return this.IsItemSupported(publishingSystemEventInfo) && publishingSystemEventInfo.ItemAction == "SystemObjectDeleted" || this.CanProcessItem(publishingSystemEventInfo.Item);
        case WrapperObject wrapperObject:
          return this.CanProcessItem(wrapperObject.WrappedObject);
        case IContent content:
          if (TypeResolutionService.ResolveType(this.PipeSettingsInternal.ContentTypeName).IsAssignableFrom(item.GetType()) && this.ProvidersComponent.CheckProvider(this.PipeSettings.PublishingPoint, (IDataItem) content))
          {
            IList<Guid> contentLinks = this.PipeSettingsInternal.ContentLinks;
            if (contentLinks.Count == 0 || this.CheckContentLinks(content, contentLinks))
              return true;
            break;
          }
          break;
      }
      return false;
    }

    /// <summary>Pushes the data.</summary>
    /// <param name="items">The items.</param>
    public virtual void PushData(IList<PublishingSystemEventInfo> items)
    {
      List<WrapperObject> items1 = new List<WrapperObject>();
      List<WrapperObject> items2 = new List<WrapperObject>();
      foreach (PublishingSystemEventInfo publishingSystemEventInfo in (IEnumerable<PublishingSystemEventInfo>) items)
      {
        try
        {
          string contentTypeName = ((SitefinityContentPipeSettings) this.PipeSettings).ContentTypeName;
          if (!string.IsNullOrEmpty(contentTypeName) && publishingSystemEventInfo.ItemType != contentTypeName)
          {
            if (!TypeResolutionService.ResolveType(contentTypeName).IsAssignableFrom(TypeResolutionService.ResolveType(publishingSystemEventInfo.ItemType)))
              continue;
          }
          WrapperObject wrapperObject = new WrapperObject(this.PipeSettings, publishingSystemEventInfo.Item);
          wrapperObject.Language = publishingSystemEventInfo.Language;
          if (this.PipeSettings.LanguageIds.Count > 0)
          {
            if (!this.PipeSettings.LanguageIds.Contains(wrapperObject.Language))
              continue;
          }
          string itemAction = publishingSystemEventInfo.ItemAction;
          if (!(itemAction == "SystemObjectDeleted"))
          {
            if (!(itemAction == "SystemObjectAdded"))
            {
              if (itemAction == "SystemObjectModified")
              {
                IContent contentItem = this.GetContentItem(publishingSystemEventInfo);
                this.SetProperties(wrapperObject, contentItem);
                items2.Add(wrapperObject);
                items1.Add(wrapperObject);
              }
            }
            else
            {
              IContent contentItem = this.GetContentItem(publishingSystemEventInfo);
              this.SetProperties(wrapperObject, contentItem);
              items1.Add(wrapperObject);
            }
          }
          else
            items2.Add(wrapperObject);
        }
        catch (Exception ex)
        {
          this.HandleError("Error when push data for item action {0} for item {1}.".Arrange((object) publishingSystemEventInfo.ItemAction, publishingSystemEventInfo.Item), ex);
        }
      }
      if (items2.Count > 0)
        this.publishingPoint.RemoveItems((IList<WrapperObject>) items2);
      if (items1.Count <= 0)
        return;
      this.publishingPoint.AddItems((IList<WrapperObject>) items1);
    }

    /// <summary>Gets the data.</summary>
    /// <returns></returns>
    public virtual IQueryable<WrapperObject> GetData()
    {
      IQueryable<WrapperObject> left = (IQueryable<WrapperObject>) null;
      IQueryable<ILocalizable> queryable = Queryable.OfType<ILocalizable>(this.LoadContentItems(true));
      IQueryable<WrapperObject> source;
      if (this.PipeSettings.LanguageIds.Count<string>() == 0)
        source = left.UnionOrRight<WrapperObject>(this.GetWrappedContentItems(queryable));
      else
        source = left.UnionOrRight<WrapperObject>(queryable.Select<ILocalizable, WrapperObject>((Expression<Func<ILocalizable, WrapperObject>>) (c => new WrapperObject(c))));
      return source.Select<WrapperObject, WrapperObject>((Expression<Func<WrapperObject, WrapperObject>>) (i => this.SetWrapperObjectProperties(i)));
    }

    private WrapperObject SetWrapperObjectProperties(WrapperObject item)
    {
      item.MappingSettings = this.PipeSettings.Mappings;
      IContent wrappedObject = (IContent) item.WrappedObject;
      this.SetProperties(item, wrappedObject);
      return item;
    }

    private IEnumerable<WrapperObject> GetWrappedContentItems(
      IQueryable<ILocalizable> items)
    {
      string defaultFrontendLanguage = SystemManager.CurrentContext.AppSettings.DefaultFrontendLanguage.Name;
      foreach (ILocalizable contentItem in (IEnumerable<ILocalizable>) items)
      {
        if (contentItem is ILifecycleDataItem lifecycleDataItem)
        {
          IList<string> publishedTranslations = lifecycleDataItem.PublishedTranslations;
          if (publishedTranslations.Count == 0)
          {
            yield return new WrapperObject((object) contentItem)
            {
              Language = defaultFrontendLanguage
            };
          }
          else
          {
            foreach (string str in (IEnumerable<string>) publishedTranslations)
              yield return new WrapperObject((object) contentItem)
              {
                Language = str
              };
          }
        }
        else
        {
          CultureInfo[] cultureInfoArray = contentItem.AvailableCultures;
          for (int index = 0; index < cultureInfoArray.Length; ++index)
          {
            CultureInfo cultureInfo = cultureInfoArray[index];
            yield return new WrapperObject((object) contentItem)
            {
              Language = cultureInfo.Name
            };
          }
          cultureInfoArray = (CultureInfo[]) null;
        }
      }
    }

    protected virtual IContent GetContentItem(WrapperObject wrappedObject)
    {
      if (wrappedObject == null)
        return (IContent) null;
      if (wrappedObject.WrappedObject == null)
        return (IContent) null;
      return wrappedObject.WrappedObject is IContent ? (IContent) wrappedObject.WrappedObject : this.GetContentItem((WrapperObject) wrappedObject.WrappedObject);
    }

    /// <summary>Gets the content item.</summary>
    /// <param name="item">The item.</param>
    /// <returns></returns>
    protected virtual IContent GetContentItem(PublishingSystemEventInfo item) => item.Item is WrapperObject wrappedObject ? this.GetContentItem(wrappedObject) : (IContent) item.Item;

    /// <summary>Sets the properties.</summary>
    /// <param name="wrapperObject">The wrapper object.</param>
    /// <param name="contentItem">The content item.</param>
    protected virtual void SetProperties(WrapperObject wrapperObject, IContent contentItem)
    {
      this.SetLanguageWrapperObject(wrapperObject);
      PropertyDescriptor propertyDescriptor = TypeDescriptor.GetProperties((object) wrapperObject).Find("Owner", false);
      if (propertyDescriptor != null)
      {
        object userId = propertyDescriptor.GetValue((object) wrapperObject);
        this.SetUserProperties(wrapperObject, (Guid) userId);
      }
      string itemUrl = this.GetItemUrl(contentItem);
      if (!string.IsNullOrEmpty(itemUrl))
        wrapperObject.SetOrAddProperty("Link", (object) itemUrl);
      wrapperObject.SetOrAddProperty("PipeId", (object) this.PipeSettings.Id);
      wrapperObject.SetOrAddProperty("ContentType", (object) this.PipeSettingsInternal.ContentTypeName);
      PublishingUtilities.AddItemCategories(wrapperObject, (IDataItem) contentItem);
      PublishingUtilities.AddContentUsages(wrapperObject, (IDataItem) contentItem);
      PublishingUtilities.AddContentLifecycleStatus(wrapperObject, (IDataItem) contentItem);
      this.SetProvider(wrapperObject, contentItem);
    }

    protected virtual IQueryable<IContent> LoadContentItems(
      bool addaptFilterExpression = false,
      int? chunkIndex = null)
    {
      SitefinityContentPipeSettings settingsInternal = this.PipeSettingsInternal;
      if (settingsInternal == null)
        throw new InvalidOperationException("Pipe settings required!");
      IQueryable<IContent> left = ((IEnumerable<IContent>) new IContent[0]).AsQueryable<IContent>();
      Type type;
      IManager mappedManager1;
      try
      {
        type = TypeResolutionService.ResolveType(settingsInternal.ContentTypeName);
        mappedManager1 = ManagerBase.GetMappedManager(type, settingsInternal.ProviderName);
      }
      catch
      {
        return (IQueryable<IContent>) null;
      }
      int skip = 0;
      int take = 0;
      if (chunkIndex.HasValue)
      {
        skip = chunkIndex.Value * 500;
        take = 500;
      }
      if (this.maxItems > 0)
        take = this.maxItems;
      if (!string.IsNullOrEmpty(settingsInternal.ProviderName))
        return this.GetProviderItems(settingsInternal, addaptFilterExpression, mappedManager1, type, skip, take).AsQueryable<IContent>();
      foreach (string provider in this.ProvidersComponent.GetProviders(mappedManager1, type, settingsInternal.PublishingPoint))
      {
        IManager mappedManager2 = ManagerBase.GetMappedManager(type, provider);
        IEnumerable<IContent> providerItems = this.GetProviderItems(settingsInternal, addaptFilterExpression, mappedManager2, type, skip, take);
        left = left.UnionOrRight<IContent>(providerItems);
      }
      return left;
    }

    internal virtual IEnumerable<IContent> GetProviderItems(
      SitefinityContentPipeSettings contentSettings,
      bool addaptFilterExpression,
      IManager manager,
      Type contentType,
      int skip,
      int take)
    {
      string str1 = this.PipeSettings.PublishingPoint.Settings.ItemFilterStrategy == PublishingItemFilter.Live ? PredefinedFilters.PublishedItemsRegardlessSheduledDateFilter() : PredefinedFilters.AllItemsExceptTemp();
      QueryData expressionQueryData = contentSettings.FilterExpressionQueryData;
      string str2 = expressionQueryData != null ? string.Format("{0} AND {1}", (object) LinqTranslator.ToDynamicLinq(expressionQueryData).Trim(), (object) str1) : str1;
      if (addaptFilterExpression)
      {
        string name = this.PipeSettings.LanguageIds.FirstOrDefault<string>();
        str2 = name != null ? ContentHelper.AdaptMultilingualFilterExpressionRaw(str2, CultureInfo.GetCultureInfo(name)) : ContentHelper.AdaptMultilingualFilterExpressionRaw(str2, (CultureInfo) null, true);
      }
      IList<Guid> contentLinks = this.PipeSettingsInternal.ContentLinks;
      if (contentLinks.Count > 0)
        str2 = this.AppendContentLinksFilter(str2, contentType, contentLinks);
      string sortingExpression = this.GetSortingExpression();
      return manager.GetItems(contentType, str2, sortingExpression, skip, take).OfType<ILocalizable>().Where<ILocalizable>(new Func<ILocalizable, bool>(this.LanguageFilter)).OfType<IContent>();
    }

    protected virtual bool LanguageFilter(ILocalizable item) => this.pipeSettingsLanguages == null || this.pipeSettingsLanguages.Overlaps((IEnumerable<string>) item.AvailableLanguages);

    protected virtual string GetSortingExpression() => "LastModified DESC";

    /// <summary>Sets the user properties.</summary>
    /// <param name="item">The item.</param>
    /// <param name="userId">The user id.</param>
    protected virtual void SetUserProperties(WrapperObject item, Guid userId)
    {
      if (!(userId != Guid.Empty))
        return;
      User user = SecurityManager.GetUser(userId);
      if (user == null)
        return;
      item.SetOrAddProperty("Username", (object) user.UserName);
      item.SetOrAddProperty("OwnerFirstName", (object) user.FirstName);
      item.SetOrAddProperty("OwnerLastName", (object) user.LastName);
      item.SetOrAddProperty("OwnerEmail", (object) user.Email);
    }

    private void SetProvider(WrapperObject item, IContent contentItem)
    {
      if (!(contentItem.Provider is IDataProviderBase provider))
        return;
      if (item.AdditionalProperties.ContainsKey(provider.Name))
        item.SetProperty("Provider", (object) provider.Name);
      else
        item.AddProperty("Provider", (object) provider.Name);
    }

    /// <summary>Toes the publishing point.</summary>
    public virtual void ToPublishingPoint()
    {
      string name = SystemManager.CurrentContext.AppSettings.DefaultFrontendLanguage.Name;
      bool flag = (uint) this.PipeSettings.LanguageIds.Count > 0U;
      string language1 = this.PipeSettings.LanguageIds.FirstOrDefault<string>();
      int num1 = 0;
      int num2 = 0;
      do
      {
        IContent[] array = this.LoadContentItems(chunkIndex: new int?(num1)).ToArray<IContent>();
        if (array.Length != 0)
        {
          List<PublishingSystemEventInfo> publishingSystemEventInfoList = new List<PublishingSystemEventInfo>();
          foreach (IContent contentItem in array)
          {
            if (flag)
            {
              this.AddModifiedEventInfo(publishingSystemEventInfoList, contentItem, language1);
            }
            else
            {
              Enumerable.Empty<string>();
              IEnumerable<string> source = !(contentItem is ILifecycleDataItem lifecycleDataItem) ? (IEnumerable<string>) (contentItem as Content).AvailableLanguages : (IEnumerable<string>) lifecycleDataItem.PublishedTranslations;
              if (source.Count<string>() == 0)
              {
                this.AddModifiedEventInfo(publishingSystemEventInfoList, contentItem, name);
              }
              else
              {
                foreach (string language2 in source)
                  this.AddModifiedEventInfo(publishingSystemEventInfoList, contentItem, language2);
              }
            }
          }
          this.PushDataInternal((IList<PublishingSystemEventInfo>) publishingSystemEventInfoList);
          ++num1;
          num2 += array.Length;
        }
        else
          goto label_11;
      }
      while (this.maxItems <= 0 || num2 < this.maxItems);
      goto label_18;
label_11:
      return;
label_18:;
    }

    internal virtual void PushDataInternal(IList<PublishingSystemEventInfo> items) => this.PushData(items);

    private void AddModifiedEventInfo(
      List<PublishingSystemEventInfo> eventInfo,
      IContent contentItem,
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

    /// <summary>Gets the pipe settings short description.</summary>
    /// <param name="initSettings">The init settings.</param>
    /// <returns></returns>
    public override string GetPipeSettingsShortDescription(PipeSettings initSettings) => this.GetInboundPipeSettingsShortDescription((SitefinityContentPipeSettings) initSettings);

    /// <summary>Sets the language wrapper object.</summary>
    /// <param name="wrapperObject">The wrapper object.</param>
    protected virtual void SetLanguageWrapperObject(WrapperObject wrapperObject)
    {
      if (this.PipeSettings.LanguageIds.Count<string>() <= 0)
        return;
      wrapperObject.Language = this.PipeSettings.LanguageIds.FirstOrDefault<string>();
    }

    /// <summary>Gets the inbound pipe settings short description.</summary>
    /// <param name="contentSettings">The content settings.</param>
    /// <returns></returns>
    protected virtual string GetInboundPipeSettingsShortDescription(
      SitefinityContentPipeSettings contentSettings)
    {
      string description;
      return PublishingSystemFactory.PipeDescriptionProviderRegistered(this.Name) && PublishingSystemFactory.GetPipeDescriptionProvider(this.Name).GetPipeSettingsShortDescription((PipeSettings) contentSettings, out description) ? description : string.Format("<strong>{0}</strong>: ", (object) Telerik.Sitefinity.Modules.ContentExtensions.GetTypeUIPluralName(contentSettings.ContentTypeName)) + ContentInboundPipe.AllItems;
    }

    /// <summary>Gets the URL of the content item.</summary>
    /// <param name="contentItem">The content item.</param>
    /// <returns></returns>
    protected virtual string GetItemUrl(IContent contentItem)
    {
      string itemUrl = (string) null;
      Guid? defaultPageId = this.GetDefaultPageId(contentItem);
      if (defaultPageId.HasValue)
      {
        Guid pageId = defaultPageId.Value;
        PageNode pageNode = PageManager.GetManager().GetPageNodes().SingleOrDefault<PageNode>((Expression<Func<PageNode, bool>>) (n => n.Id == pageId));
        if (pageNode != null)
          itemUrl = DataResolver.Resolve((object) contentItem, "URL", (string) null, pageNode.Id.ToString());
      }
      return itemUrl;
    }

    /// <summary>Gets the default page id for the given content.</summary>
    /// <param name="item">The content.</param>
    /// <returns>The page id.</returns>
    protected virtual Guid? GetDefaultPageId(IContent item)
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

    /// <summary>
    /// Checks if the content links refers to the given content item.
    /// </summary>
    /// <param name="content">The content item.</param>
    /// <param name="contentLinks">The content links.</param>
    /// <returns></returns>
    protected virtual bool CheckContentLinks(IContent content, IList<Guid> contentLinks)
    {
      if (content is IHasParent)
      {
        IHasParent hasParent = (IHasParent) content;
        if (contentLinks.Contains(hasParent.Parent.Id))
          return true;
      }
      if (content is Content)
      {
        Content content1 = (Content) content;
        if (content1.SupportsContentLifecycle && content1.OriginalContentId != Guid.Empty && contentLinks.Contains(content1.OriginalContentId))
          return true;
      }
      return contentLinks.Contains(content.Id);
    }

    /// <summary>
    /// Builds a filter for the given content links, appends it to the existing one and returns it.
    /// </summary>
    /// <param name="filterExpression">The filter expression.</param>
    /// <param name="contentType">Type of the content.</param>
    /// <param name="contentLinks">The content links.</param>
    /// <returns></returns>
    protected virtual string AppendContentLinksFilter(
      string filterExpression,
      Type contentType,
      IList<Guid> contentLinks)
    {
      StringBuilder stringBuilder = new StringBuilder();
      foreach (Guid contentLink in (IEnumerable<Guid>) contentLinks)
        stringBuilder.AppendFormat("Id = ({0}) OR ", (object) contentLink);
      stringBuilder.Remove(stringBuilder.Length - 4, 4);
      if (contentType.ImplementsInterface(typeof (IHasParent)))
      {
        foreach (Guid contentLink in (IEnumerable<Guid>) contentLinks)
          stringBuilder.AppendFormat(" OR Parent.Id = ({0})", (object) contentLink);
      }
      if (contentType.ImplementsInterface(typeof (IHasIDataItemParent)))
      {
        foreach (Guid contentLink in (IEnumerable<Guid>) contentLinks)
          stringBuilder.AppendFormat(" OR Parent.Id = ({0})", (object) contentLink);
      }
      if (typeof (Content).IsAssignableFrom(contentType))
      {
        foreach (Guid contentLink in (IEnumerable<Guid>) contentLinks)
          stringBuilder.AppendFormat(" OR OriginalContentId = ({0})", (object) contentLink);
      }
      return string.Format("{0} AND ({1})", (object) filterExpression, (object) stringBuilder.ToString());
    }

    private bool IsItemSupported(PublishingSystemEventInfo item) => TypeResolutionService.ResolveType(this.PipeSettingsInternal.ContentTypeName).IsAssignableFrom(TypeResolutionService.ResolveType(item.ItemType));
  }
}
