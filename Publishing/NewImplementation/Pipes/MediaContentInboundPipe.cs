// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Publishing.Pipes.MediaContentInboundPipe
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Text.RegularExpressions;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.Libraries.Model;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.Modules;
using Telerik.Sitefinity.Modules.Libraries;
using Telerik.Sitefinity.Publishing.Model;
using Telerik.Sitefinity.Publishing.NewImplementation;
using Telerik.Sitefinity.Utilities.TypeConverters;

namespace Telerik.Sitefinity.Publishing.Pipes
{
  /// <summary>Media content inbound pipe</summary>
  public class MediaContentInboundPipe : 
    BasePipe<SitefinityContentPipeSettings>,
    IPushPipe,
    IInboundPipe
  {
    private readonly ProvidersComponent providersComponent = new ProvidersComponent();
    private IDefinitionField[] definitionFields;
    public static readonly string PipeName = nameof (MediaContentInboundPipe);
    private const int MediaContentChunkSize = 200;

    /// <summary>
    /// Determines whether this instance can process the specified item.
    /// </summary>
    /// <param name="item">The item.</param>
    /// <returns><c>true</c> if this instance can process the specified item; otherwise, <c>false</c>.</returns>
    public override bool CanProcessItem(object item)
    {
      if (item == null)
        return false;
      if (item is PublishingSystemEventInfo publishingSystemEventInfo)
        return this.IsItemSupported(publishingSystemEventInfo) && publishingSystemEventInfo.ItemAction == "SystemObjectDeleted" || this.CanProcessItem(publishingSystemEventInfo.Item);
      if (item is WrapperObject wrapperObject)
        return this.CanProcessItem(wrapperObject.WrappedObject);
      return this.GetSupportedType() == item.GetType().FullName && (!(((IDataItem) item).Provider is IDataProviderBase provider) || !(provider.Name == "SystemLibrariesProvider")) && this.providersComponent.CheckProvider(this.PipeSettings.PublishingPoint, (IDataItem) item);
    }

    /// <summary>Gets the converted items for mapping.</summary>
    /// <param name="items">The items.</param>
    /// <returns>Collection of wrapper objects</returns>
    public virtual IEnumerable<WrapperObject> GetConvertedItemsForMapping(
      params object[] items)
    {
      List<WrapperObject> convertedItemsForMapping = new List<WrapperObject>();
      foreach (PublishingSystemEventInfo publishingSystemEventInfo in items.OfType<PublishingSystemEventInfo>())
      {
        WrapperObject wrapperObject = new WrapperObject(this.PipeSettings, publishingSystemEventInfo.Item, publishingSystemEventInfo.Language);
        MediaContent contentItem = this.GetContentItem(wrapperObject) as MediaContent;
        string fileUrl = this.GetFileUrl(wrapperObject, contentItem);
        wrapperObject.SetOrAddProperty("Link", (object) fileUrl);
        wrapperObject.SetOrAddProperty("SearchableLink", (object) this.GetSearchableLink(fileUrl));
        wrapperObject.SetOrAddProperty("PipeId", (object) string.Empty);
        string providerName = this.GetProviderName(wrapperObject, contentItem);
        wrapperObject.AddProperty("Provider", (object) providerName);
        wrapperObject.SetOrAddProperty("ContentType", (object) this.GetSupportedType());
        wrapperObject.SetOrAddProperty("LibraryIds", (object) this.GetLibraryIds(contentItem?.ParentId, (Guid?) contentItem?.FolderId, providerName));
        wrapperObject.SetOrAddProperty("DateCreated", (object) contentItem?.DateCreated);
        PublishingUtilities.AddItemCategories(wrapperObject, (IDataItem) contentItem);
        PublishingUtilities.AddContentUsages(wrapperObject, (IDataItem) contentItem);
        PublishingUtilities.AddContentLifecycleStatus(wrapperObject, (IDataItem) contentItem);
        this.AddAdditionalProperties(wrapperObject, contentItem);
        wrapperObject.Language = publishingSystemEventInfo.Language;
        convertedItemsForMapping.Add(wrapperObject);
      }
      return (IEnumerable<WrapperObject>) convertedItemsForMapping;
    }

    /// <summary>Gets the name.</summary>
    /// <value>The name.</value>
    public override string Name => MediaContentInboundPipe.PipeName;

    /// <summary>
    /// Gets the data structure of the medium this pipe works with
    /// </summary>
    /// <value>The definition.</value>
    public override IDefinitionField[] Definition
    {
      get
      {
        if (this.definitionFields == null)
          this.definitionFields = PublishingSystemFactory.GetPipeDefinitions(this.Name);
        return this.definitionFields;
      }
    }

    /// <summary>Used to get the default mappings for this pipe</summary>
    /// <returns>The settings</returns>
    public static List<Mapping> GetDefaultMappings() => ContentInboundPipe.GetDefaultMappings();

    /// <summary>
    /// Gets the data from publishing point and "pushes" them to corresponding publishing point
    /// </summary>
    public virtual void ToPublishingPoint()
    {
      IOrderedQueryable<MediaContent> source1 = this.LoadContentItems().OrderBy<MediaContent, DateTime>((Expression<Func<MediaContent, DateTime>>) (d => d.LastModified));
      Func<ILocalizable, bool> func = (Func<ILocalizable, bool>) (item =>
      {
        bool publishingPoint = true;
        if (this.PipeSettings.LanguageIds.Count<string>() > 0)
        {
          IEnumerable<string> second = this.PipeSettings.LanguageIds.Cast<string>();
          publishingPoint = ((IEnumerable<string>) item.AvailableLanguages).Intersect<string>(second).Count<string>() > 0;
        }
        return publishingPoint;
      });
      bool flag = true;
      int count = 0;
      while (flag)
      {
        List<PublishingSystemEventInfo> items = new List<PublishingSystemEventInfo>();
        IQueryable<MediaContent> source2 = source1.Skip<MediaContent>(count).Take<MediaContent>(200);
        flag = source2.Count<MediaContent>() > 0;
        if (!flag)
          break;
        foreach (MediaContent localizableItem in (IEnumerable<MediaContent>) source2)
        {
          if (func((ILocalizable) localizableItem) && this.CanProcessItem((object) localizableItem))
          {
            if (this.PipeSettings.LanguageIds.Count != 0)
            {
              PublishingSystemEventInfo publishingSystemEventInfo = new PublishingSystemEventInfo()
              {
                Item = (object) localizableItem,
                ItemAction = "SystemObjectModified",
                Language = this.PipeSettings.LanguageIds.FirstOrDefault<string>()
              };
              items.Add(publishingSystemEventInfo);
            }
            else
            {
              foreach (string str in localizableItem.GetAvailableLanguagesIgnoringContext().Select<CultureInfo, string>((Func<CultureInfo, string>) (c => c.Name)))
              {
                PublishingSystemEventInfo publishingSystemEventInfo = new PublishingSystemEventInfo()
                {
                  Item = (object) localizableItem,
                  ItemAction = "SystemObjectModified",
                  Language = str
                };
                items.Add(publishingSystemEventInfo);
              }
            }
          }
        }
        this.PushData((IList<PublishingSystemEventInfo>) items, false);
        count += 200;
      }
    }

    /// <summary>Pushes the data.</summary>
    /// <param name="items">The items.</param>
    public virtual void PushData(IList<PublishingSystemEventInfo> items) => this.PushData(items, false);

    protected virtual void PushData(IList<PublishingSystemEventInfo> items, bool runAsynchroniously)
    {
      foreach (DataProviderBase staticProvider in (Collection<PublishingDataProviderBase>) PublishingManager.GetManager().StaticProviders)
      {
        if (PublishingManager.GetManager(staticProvider.Name).GetPipeSettings().Where<PipeSettings>((Expression<Func<PipeSettings, bool>>) (s => s.Id == this.PipeSettings.Id)).FirstOrDefault<PipeSettings>() != null)
          break;
      }
      List<WrapperObject> items1 = new List<WrapperObject>();
      List<WrapperObject> items2 = new List<WrapperObject>();
      foreach (PublishingSystemEventInfo publishingSystemEventInfo in (IEnumerable<PublishingSystemEventInfo>) items)
      {
        if (publishingSystemEventInfo.ItemAction == "SystemObjectDeleted")
        {
          WrapperObject wrapperObject = new WrapperObject(this.PipeSettings, publishingSystemEventInfo.Item, publishingSystemEventInfo.Language);
          items2.Add(wrapperObject);
        }
        if (publishingSystemEventInfo.ItemAction == "SystemObjectModified" || publishingSystemEventInfo.ItemAction == "SystemObjectAdded")
        {
          WrapperObject wrapperObject = this.GetConvertedItemsForMapping((object) publishingSystemEventInfo).First<WrapperObject>();
          items1.Add(wrapperObject);
          items2.Add(wrapperObject);
        }
      }
      this.PublishingPoint.RemoveItems((IList<WrapperObject>) items2);
      this.PublishingPoint.AddItems((IList<WrapperObject>) items1);
    }

    internal static SitefinityContentPipeSettings GetDefaultPipeSettings()
    {
      SitefinityContentPipeSettings defaultPipeSettings = new SitefinityContentPipeSettings();
      defaultPipeSettings.IsInbound = true;
      defaultPipeSettings.PipeName = MediaContentInboundPipe.PipeName;
      defaultPipeSettings.IsActive = true;
      defaultPipeSettings.MaxItems = 0;
      defaultPipeSettings.InvocationMode = PipeInvokationMode.Push;
      defaultPipeSettings.ContentTypeName = typeof (Image).FullName;
      defaultPipeSettings.ResourceClassId = LibrariesModule.ResourceClassId;
      defaultPipeSettings.UIName = Res.Get<LibrariesResources>().ImagesTitle;
      return defaultPipeSettings;
    }

    protected virtual void AddAdditionalProperties(
      WrapperObject wrapperObject,
      MediaContent mediaContent)
    {
    }

    protected virtual IDataItem GetContentItem(WrapperObject wrappedObject)
    {
      if (wrappedObject == null)
        return (IDataItem) null;
      if (wrappedObject.WrappedObject == null)
        return (IDataItem) null;
      return wrappedObject.WrappedObject is IDataItem ? (IDataItem) wrappedObject.WrappedObject : this.GetContentItem((WrapperObject) wrappedObject.WrappedObject);
    }

    protected string GetProviderName(WrapperObject convertedItem, MediaContent mediaContent) => mediaContent.GetProviderName();

    protected virtual bool IsItemSupported(PublishingSystemEventInfo item) => string.Equals(this.PipeSettingsInternal.ContentTypeName, item.ItemType);

    protected string GetSupportedType() => this.PipeSettingsInternal.ContentTypeName;

    protected string GetSearchableLink(string link)
    {
      int length = link.LastIndexOf("?");
      if (length >= 0)
        link = link.Substring(0, length);
      return new Regex("['\"&,-./:?=]").Replace(link, " ").Trim();
    }

    protected List<string> GetLibraryIds(Guid? parentId, Guid? folderId, string providerName)
    {
      List<string> first = new List<string>();
      if (parentId.HasValue)
        first.Add(parentId.Value.ToString("N"));
      if (folderId.HasValue && LibrariesManager.GetManager(providerName).FindFolderById(folderId.Value) is Folder folderById)
      {
        string[] second = folderById.Path.Split('/');
        first = first.Concat<string>((IEnumerable<string>) second).ToList<string>();
      }
      return first;
    }

    protected virtual IQueryable<MediaContent> LoadContentItems()
    {
      PipeSettings pipeSettings = this.PipeSettings;
      if (pipeSettings == null)
        throw new InvalidOperationException("Pipe settings required!");
      LibrariesManager manager1 = LibrariesManager.GetManager();
      Type itemType = TypeResolutionService.ResolveType(this.GetSupportedType());
      IEnumerable<string> providers = this.providersComponent.GetProviders((IManager) manager1, itemType, pipeSettings.PublishingPoint);
      bool flag = this.PipeSettings.PublishingPoint.Settings.ItemFilterStrategy == PublishingItemFilter.All;
      IQueryable<MediaContent> left = (IQueryable<MediaContent>) null;
      foreach (string providerName in providers)
      {
        LibrariesManager manager2 = LibrariesManager.GetManager(providerName);
        string filterExpression = flag ? PredefinedFilters.AllItemsExceptTemp() : PredefinedFilters.PublishedItemsRegardlessSheduledDateFilter();
        IEnumerable<MediaContent> mediaContents = manager2.GetItems(itemType, filterExpression, "", 0, 0).OfType<MediaContent>();
        if (pipeSettings.MaxItems > 0)
          mediaContents = mediaContents.Take<MediaContent>(pipeSettings.MaxItems);
        left = left.UnionOrRight<MediaContent>(mediaContents);
      }
      return left ?? Enumerable.Empty<MediaContent>().AsQueryable<MediaContent>();
    }

    private string GetFileUrl(WrapperObject convertedItem, MediaContent mediaContent)
    {
      if (mediaContent == null)
        return (string) null;
      string empty = string.Empty;
      using (new CultureRegion(convertedItem.Language))
        return mediaContent.ResolveMediaUrl(false, (CultureInfo) null);
    }

    /// <summary>Initializes the specified pipe settings.</summary>
    /// <param name="pipeSettings">The pipe settings.</param>
    public override void Initialize(PipeSettings pipeSettings)
    {
      this.PipeSettingsInternal = (SitefinityContentPipeSettings) pipeSettings;
      this.PublishingPoint = PublishingSystemFactory.GetPublishingPoint(this.PipeSettings.PublishingPoint);
    }

    /// <summary>Gets or sets the publishing point.</summary>
    /// <value>The publishing point.</value>
    public virtual IPublishingPointBusinessObject PublishingPoint { get; set; }
  }
}
