// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Publishing.Pipes.ContentOutboundPipe
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Syndication;
using System.Text.RegularExpressions;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.Descriptors;
using Telerik.Sitefinity.GenericContent.Model;
using Telerik.Sitefinity.Lifecycle;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.Model.Localization;
using Telerik.Sitefinity.Modules.GenericContent;
using Telerik.Sitefinity.Publishing.Model;
using Telerik.Sitefinity.Publishing.Web.UI.Designers;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Utilities;
using Telerik.Sitefinity.Utilities.TypeConverters;
using Telerik.Sitefinity.Workflow;

namespace Telerik.Sitefinity.Publishing.Pipes
{
  /// <summary>Content Outbound Pipe</summary>
  [PipeDesigner(null, typeof (ContentPipeExportDesignerView))]
  public class ContentOutboundPipe : 
    BasePipe<SitefinityContentPipeSettings>,
    IPushPipe,
    IOutboundPipe
  {
    public const string PipeName = "ContentOutboundPipe";
    public static readonly string NonexistentBlog = Res.Get<PublishingMessages>().NonexistentBlog;
    public static readonly string NonexistentList = Res.Get<PublishingMessages>().NonexistentList;
    public static readonly string PostsAllBlogs = Res.Get<PublishingMessages>().PostsAllBlogs;
    public static readonly string ItemsAllLists = Res.Get<PublishingMessages>().ItemsAllLists;
    public static readonly string AllItems = Res.Get<PublishingMessages>().AllItems;
    private IDefinitionField[] definitionFields;
    private IManager manager;

    /// <summary>Gets the type of the content.</summary>
    /// <value>The type of the content.</value>
    public virtual Type ContentItemType => TypeResolutionService.ResolveType(this.PipeSettingsInternal.ContentTypeName);

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

    /// <summary>Gets the name.</summary>
    /// <value>The name.</value>
    public override string Name => nameof (ContentOutboundPipe);

    /// <summary>Gets the manager.</summary>
    /// <value>The manager.</value>
    protected virtual IManager Manager
    {
      get
      {
        if (this.manager == null)
          this.manager = ManagerBase.GetMappedManager(this.PipeSettingsInternal.ContentTypeName, this.PipeSettingsInternal.ProviderName);
        return this.manager;
      }
    }

    /// <summary>Gets the lifecycle manager.</summary>
    /// <value>The lifecycle manager.</value>
    protected virtual ILifecycleManager LifecycleManager => (ILifecycleManager) this.Manager;

    /// <summary>Gets a value indicating whether [should publish].</summary>
    /// <value><c>true</c> if [should publish]; otherwise, <c>false</c>.</value>
    protected virtual bool ShouldPublish => this.Manager is ILifecycleManager && this.PipeSettingsInternal.ImportItemAsPublished;

    /// <summary>Gets a value indicating whether [use workflow].</summary>
    /// <value><c>true</c> if [use workflow]; otherwise, <c>false</c>.</value>
    protected virtual bool UseWorkflow => typeof (IApprovalWorkflowItem).IsAssignableFrom(this.ContentItemType);

    /// <summary>Gets the content item type descriptros.</summary>
    /// <value>The content item type descriptros.</value>
    protected virtual PropertyDescriptorCollection ContentItemTypeDescriptros => TypeDescriptor.GetProperties(this.ContentItemType);

    /// <summary>
    /// Used to get the default settings for this pipe, without creating persistent object
    /// </summary>
    /// <returns>The settings</returns>
    public static SitefinityContentPipeSettings GetTemplatePipeSettings()
    {
      SitefinityContentPipeSettings templatePipeSettings = new SitefinityContentPipeSettings();
      templatePipeSettings.ContentTypeName = "Telerik.Sitefinity.Blogs.Model.BlogPost";
      templatePipeSettings.IsInbound = false;
      templatePipeSettings.PipeName = nameof (ContentOutboundPipe);
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
      PublishingSystemFactory.CreateMapping("Link", "TransparentTranslator", true, "Link"),
      PublishingSystemFactory.CreateMapping("Content", "concatenationtranslator", true, "Content"),
      PublishingSystemFactory.CreateMapping("Summary", "concatenationtranslator", true, "Summary"),
      PublishingSystemFactory.CreateMapping("Categories", "TransparentTranslator", false, "Categories"),
      PublishingSystemFactory.CreateMapping("Username", "TransparentTranslator", false, "Username"),
      PublishingSystemFactory.CreateMapping("OwnerFirstName", "TransparentTranslator", false, "OwnerFirstName"),
      PublishingSystemFactory.CreateMapping("OwnerLastName", "TransparentTranslator", false, "OwnerLastName"),
      PublishingSystemFactory.CreateMapping("OwnerEmail", "TransparentTranslator", false, "OwnerEmail"),
      PublishingSystemFactory.CreateMapping("Id", "TransparentTranslator", true, "Id"),
      PublishingSystemFactory.CreateMapping("OriginalParentId", "TransparentTranslator", false, "OriginalParentId"),
      PublishingSystemFactory.CreateMapping("SourceKey", "TransparentTranslator", false, "ItemHash"),
      PublishingSystemFactory.CreateMapping("PipeId", "TransparentTranslator", false, "PipeId"),
      PublishingSystemFactory.CreateMapping("ContentType", "TransparentTranslator", false, "ContentType")
    };

    /// <summary>Initializes the specified pipe settings.</summary>
    /// <param name="pipeSettings">The pipe settings.</param>
    public override void Initialize(PipeSettings pipeSettings)
    {
      this.PipeSettingsInternal = pipeSettings is SitefinityContentPipeSettings ? pipeSettings as SitefinityContentPipeSettings : throw new ArgumentException("Expected pipe settings of type SitefinityContentPipeSettings");
      this.definitionFields = (IDefinitionField[]) null;
    }

    /// <summary>
    /// Determines whether this instance [can process item] the specified item.
    /// </summary>
    /// <param name="item">The item.</param>
    /// <returns></returns>
    public override bool CanProcessItem(object item) => true;

    /// <summary>Pushes the data.</summary>
    /// <param name="itemsList">The items list.</param>
    public virtual void PushData(IList<PublishingSystemEventInfo> itemsList)
    {
      try
      {
        if (this.Manager == null)
          return;
      }
      catch
      {
        return;
      }
      foreach (PublishingSystemEventInfo items1 in (IEnumerable<PublishingSystemEventInfo>) itemsList)
      {
        try
        {
          List<object> items2 = new List<object>();
          List<object> objectList = new List<object>();
          Dictionary<Guid, DateTime> publicationDates = new Dictionary<Guid, DateTime>();
          using (new CultureRegion(items1.Language))
          {
            if (this.FilterPublicationItem(items1))
            {
              WrapperObject wrapperObj = this.ConstructWrapperObject(items1);
              IContent contentItem = this.GetContentItem(wrapperObj);
              if (items1.ItemAction == "SystemObjectDeleted")
              {
                this.Manager.DeleteItem((object) contentItem);
                objectList.Add((object) contentItem);
              }
              else
              {
                items2.Add((object) contentItem);
                this.SetPropertiesThroughPropertyDescriptor(contentItem, wrapperObj);
                this.SetAdditionalProperties((IScheduleable) contentItem, wrapperObj, publicationDates);
              }
            }
            if (items2.Count + objectList.Count > 0)
              this.Manager.SaveChanges();
            this.SaveChanges(items2, publicationDates);
          }
        }
        catch (ContentOutboundPipe.ParentNotFoundException ex)
        {
          this.Manager.Provider.RollbackTransaction();
          this.HandleError(ex.Message, (Exception) ex);
          break;
        }
        catch (Exception ex)
        {
          this.Manager.Provider.RollbackTransaction();
          this.HandleError("Error when push data for item action {0} for item {1}.".Arrange((object) items1.ItemAction, items1.Item), ex);
        }
      }
    }

    /// <summary>Gets the pipe settings short description.</summary>
    /// <param name="initSettings">The init settings.</param>
    /// <returns></returns>
    public override string GetPipeSettingsShortDescription(PipeSettings initSettings) => this.GetOutboundPipeSettingsShortDescription((SitefinityContentPipeSettings) initSettings);

    /// <summary>Constructs the wrapper object.</summary>
    /// <param name="propertyValues">The property values.</param>
    /// <returns></returns>
    protected virtual WrapperObject ConstructWrapperObject(
      PublishingSystemEventInfo propertyValues)
    {
      WrapperObject wrapperObject = new WrapperObject(this.PipeSettings, propertyValues.Item, propertyValues.Language);
      if (wrapperObject.MappingSettings == null)
        wrapperObject.MappingSettings = this.PipeSettings.Mappings;
      return wrapperObject;
    }

    /// <summary>Gets the content item.</summary>
    /// <param name="wrapperObj">The wrapper obj.</param>
    /// <returns></returns>
    protected virtual IContent GetContentItem(WrapperObject wrapperObj)
    {
      PropertyDescriptorCollection properties = TypeDescriptor.GetProperties((object) wrapperObj);
      IContent contentItem;
      if (properties.Find("SourceKey", true) != null)
      {
        object itemHash = properties.Find("SourceKey", true).GetValue((object) wrapperObj);
        contentItem = itemHash == null ? this.GetItem(this.ContentItemType, this.Manager, "") : this.GetItem(this.ContentItemType, this.Manager, (string) itemHash);
      }
      else
        contentItem = this.GetItem(this.ContentItemType, this.Manager, "");
      return contentItem;
    }

    /// <summary>Sets the properties through property descriptor.</summary>
    /// <param name="item">The item.</param>
    /// <param name="wrapperObj">The wrapper obj.</param>
    protected virtual void SetPropertiesThroughPropertyDescriptor(
      IContent item,
      WrapperObject wrapperObj)
    {
      foreach (PropertyDescriptor property in TypeDescriptor.GetProperties((object) wrapperObj))
      {
        PropertyDescriptor propertyDescriptor = this.ContentItemTypeDescriptros.Find(property.Name, false);
        if (propertyDescriptor != null)
        {
          if (propertyDescriptor is LstringPropertyDescriptor)
          {
            string str = (object) (property.GetValue((object) wrapperObj) as Lstring) == null ? (!(property.GetValue((object) wrapperObj) is TextSyndicationContent) ? (string) property.GetValue((object) wrapperObj) : ((TextSyndicationContent) property.GetValue((object) wrapperObj)).Text) : ((Lstring) property.GetValue((object) wrapperObj)).Value;
            ((LstringPropertyDescriptor) propertyDescriptor).SetString((object) item, str, this.GetWrapperObjectCulture(wrapperObj));
          }
          else if (!propertyDescriptor.IsReadOnly && !propertyDescriptor.PropertyType.IsList() && !Extensions.IsCollection(propertyDescriptor.PropertyType) && !propertyDescriptor.PropertyType.IsDictionary())
            (item as IDynamicFieldsContainer).SetValue(property.Name, property.GetValue((object) wrapperObj));
        }
      }
    }

    /// <summary>Sets the additional properties.</summary>
    /// <param name="item">The item.</param>
    /// <param name="wrapperObj">The wrapper obj.</param>
    /// <param name="publicationDates">The publication dates.</param>
    protected virtual void SetAdditionalProperties(
      IScheduleable item,
      WrapperObject wrapperObj,
      Dictionary<Guid, DateTime> publicationDates)
    {
      PropertyDescriptorCollection properties = TypeDescriptor.GetProperties((object) wrapperObj);
      if (typeof (IHasParent).IsAssignableFrom(item.GetType()) || typeof (IHasIDataItemParent).IsAssignableFrom(item.GetType()))
      {
        PropertyDescriptor propertyDescriptor = this.ContentItemTypeDescriptros.Find("Parent", true);
        if (propertyDescriptor != null)
        {
          Type propertyType = propertyDescriptor.PropertyType;
          IManager mappedManager = ManagerBase.GetMappedManager(propertyType, this.PipeSettingsInternal.ProviderName);
          object obj;
          try
          {
            obj = mappedManager.GetItem(propertyType, this.PipeSettingsInternal.ImportedItemParentId);
          }
          catch (Exception ex)
          {
            throw new ContentOutboundPipe.ParentNotFoundException(string.Format("Error importing content! Can't find parent of type '{0}' with id '{1}'.", (object) propertyType.FullName, (object) this.PipeSettingsInternal.ImportedItemParentId), ex);
          }
          if (item is IHasParent)
          {
            if (obj is Content content)
              ((IHasParent) item).Parent = content;
          }
          else if (item is IHasIDataItemParent && obj is IDataItem dataItem)
            ((IHasIDataItemParent) item).ItemParent = dataItem;
        }
      }
      if (item is ILocatable locatable)
      {
        if (string.IsNullOrEmpty((string) locatable.UrlName) && item is IContent)
          locatable.UrlName = (Lstring) Regex.Replace((string) ((IContent) item).Title, "[^\\d\\w]+", "-");
        IContentManager manager = this.Manager as IContentManager;
        manager.GetType().GetMethod("RecompileItemUrls").MakeGenericMethod(item.GetType()).Invoke((object) manager, new object[1]
        {
          (object) item
        });
      }
      if (!this.ShouldPublish)
        return;
      if (!this.UseWorkflow)
        this.LifecycleManager.Lifecycle.Publish((ILifecycleDataItem) item);
      if (item == null || properties.Find("PublicationDate", true) == null)
        return;
      DateTime? nullable = properties.Find("PublicationDate", true).GetValue((object) wrapperObj) as DateTime?;
      if (!this.UseWorkflow)
      {
        item.PublicationDate = nullable ?? item.PublicationDate;
      }
      else
      {
        if (!nullable.HasValue)
          return;
        publicationDates.Add(((IDataItem) item).Id, nullable.Value);
      }
    }

    /// <summary>Saves the changes.</summary>
    /// <param name="items">The items.</param>
    /// <param name="publicationDates">The publication dates.</param>
    protected virtual void SaveChanges(
      List<object> items,
      Dictionary<Guid, DateTime> publicationDates)
    {
      ILifecycleManager lifecycleManager = this.LifecycleManager;
      if (this.ShouldPublish)
      {
        if (!this.UseWorkflow)
          return;
        try
        {
          bool flag = false;
          foreach (ILifecycleDataItem cnt in items.OfType<IApprovalWorkflowItem>().OfType<ILifecycleDataItem>())
          {
            this.CallWorkflow(cnt, "Publish");
            ILifecycleDataItem live = lifecycleManager.Lifecycle.GetLive(cnt);
            DateTime dateTime;
            if (live != null && live is IScheduleable scheduleable && publicationDates.TryGetValue(cnt.Id, out dateTime))
            {
              scheduleable.PublicationDate = dateTime;
              flag = true;
            }
          }
          if (!flag)
            return;
          this.Manager.SaveChanges();
        }
        catch (FaultException<ExceptionDetail> ex)
        {
          if (ex.Detail.Type != typeof (InvalidWorkflowOperationException).FullName)
            throw ex;
        }
      }
      else
      {
        if (!this.UseWorkflow)
          return;
        AppSettings currentSettings = AppSettings.CurrentSettings;
        CultureInfo culture = (CultureInfo) null;
        if (SystemManager.CurrentContext.AppSettings.Multilingual)
        {
          string name = this.PipeSettingsInternal.LanguageIds.FirstOrDefault<string>();
          culture = name != null ? CultureInfo.GetCultureInfo(name) : currentSettings.DefaultFrontendLanguage;
        }
        try
        {
          foreach (ILifecycleDataItem cnt in items.OfType<IApprovalWorkflowItem>().OfType<ILifecycleDataItem>())
          {
            if (!((cnt as IApprovalWorkflowItem).ApprovalWorkflowState == (Lstring) "Published"))
            {
              ILifecycleDataItem lifecycleDataItem = lifecycleManager.Lifecycle.CheckOut(cnt, culture);
              lifecycleManager.SaveChanges();
              this.CallWorkflow(lifecycleDataItem, "SaveDraft");
              if (lifecycleManager.Lifecycle.IsCheckedOut(cnt, culture))
              {
                ILifecycleDataItem temp = lifecycleManager.Lifecycle.GetTemp(cnt, culture);
                lifecycleManager.Lifecycle.CheckIn(temp, culture);
                lifecycleManager.SaveChanges();
              }
            }
          }
        }
        catch (FaultException<ExceptionDetail> ex)
        {
          if (ex.Detail.Type != typeof (InvalidWorkflowOperationException).FullName)
            throw ex;
        }
      }
    }

    /// <summary>Gets the wrapper object culture.</summary>
    /// <param name="wrapperObj">The wrapper obj.</param>
    /// <returns></returns>
    protected virtual CultureInfo GetWrapperObjectCulture(WrapperObject wrapperObj) => wrapperObj.Language.IsNullOrEmpty() ? this.GetDefaultCulture() : new CultureInfo(wrapperObj.Language);

    /// <summary>Gets the default culture.</summary>
    /// <returns></returns>
    protected virtual CultureInfo GetDefaultCulture() => AppSettings.CurrentSettings.DefaultFrontendLanguage;

    /// <summary>Gets the item.</summary>
    /// <param name="itemType">Type of the item.</param>
    /// <param name="manager">The manager.</param>
    /// <param name="itemHash">The item hash.</param>
    /// <returns></returns>
    protected virtual IContent GetItem(Type itemType, IManager manager, string itemHash)
    {
      if (string.IsNullOrEmpty(itemHash))
        return manager.CreateItem(itemType) as IContent;
      IEnumerator enumerator = manager.GetItems(itemType, "SourceKey=\"" + itemHash + "\"", "", 0, 0).GetEnumerator();
      return enumerator.MoveNext() ? (IContent) enumerator.Current : manager.CreateItem(itemType) as IContent;
    }

    /// <summary>Filters the publication item.</summary>
    /// <param name="propertyValues">The property values.</param>
    /// <returns></returns>
    protected virtual bool FilterPublicationItem(PublishingSystemEventInfo propertyValues)
    {
      bool flag = true;
      if (!(propertyValues.ItemAction == "SystemObjectAdded") && !(propertyValues.ItemAction == "SystemObjectModified") && !(propertyValues.ItemAction == "SystemObjectDeleted"))
        flag = false;
      if (this.PipeSettingsInternal.LanguageIds.Count > 0 && !this.PipeSettingsInternal.LanguageIds.Contains(propertyValues.Language))
        flag = false;
      return flag;
    }

    /// <summary>Gets the outbound pipe settings short description.</summary>
    /// <param name="contentSettings">The content settings.</param>
    /// <returns></returns>
    protected virtual string GetOutboundPipeSettingsShortDescription(
      SitefinityContentPipeSettings contentSettings)
    {
      string description;
      if (PublishingSystemFactory.PipeDescriptionProviderRegistered(this.Name) && PublishingSystemFactory.GetPipeDescriptionProvider(this.Name).GetPipeSettingsShortDescription((PipeSettings) contentSettings, out description))
        return description;
      PublishingMessages publishingMessages = Res.Get<PublishingMessages>();
      string[] strArray = contentSettings.ContentTypeName.Split('.');
      string str = string.Format("<strong>{0}</strong> ", (object) strArray[strArray.Length - 1]);
      return publishingMessages.PublishAs + " " + str;
    }

    /// <summary>Calls the workflow.</summary>
    /// <param name="item">The item.</param>
    /// <param name="operationName">Name of the operation.</param>
    /// <returns></returns>
    protected virtual string CallWorkflow(ILifecycleDataItem item, string operationName)
    {
      Dictionary<string, string> contextBag = new Dictionary<string, string>();
      string str = this.PipeSettingsInternal.LanguageIds.FirstOrDefault<string>() ?? AppSettings.CurrentSettings.DefaultFrontendLanguage.GetLanguageKeyRaw();
      contextBag.Add("Language", str);
      string globalTransaction = SystemManager.CurrentContext.GlobalTransaction;
      try
      {
        SystemManager.CurrentContext.GlobalTransaction = (string) null;
        return WorkflowManager.MessageWorkflow(item.Id, item.GetType(), this.Manager.Provider.Name, operationName, true, contextBag);
      }
      finally
      {
        SystemManager.CurrentContext.GlobalTransaction = globalTransaction;
      }
    }

    private class ParentNotFoundException : Exception
    {
      public ParentNotFoundException(string message, Exception innerException)
        : base(message, innerException)
      {
      }
    }
  }
}
