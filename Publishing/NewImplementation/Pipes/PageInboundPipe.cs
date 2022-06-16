// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Publishing.Pipes.PageInboundPipe
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Web.UI;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Fluent.Pages;
using Telerik.Sitefinity.InlineEditing;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.Modules;
using Telerik.Sitefinity.Modules.Pages;
using Telerik.Sitefinity.Multisite;
using Telerik.Sitefinity.Pages.Model;
using Telerik.Sitefinity.Publishing.Model;
using Telerik.Sitefinity.Publishing.NewImplementation;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Utilities;
using Telerik.Sitefinity.Web.ResourceCombining;

namespace Telerik.Sitefinity.Publishing.Pipes
{
  /// <summary>Page inbound pipe</summary>
  public class PageInboundPipe : 
    BasePipe<PagePipeSettings>,
    IAsyncPushPipe,
    IPushPipe,
    IPullPipe,
    IInboundPipe
  {
    private IDefinitionField[] definitionFields;
    private PagePipeComponent pipeComponent = new PagePipeComponent();
    public const string PipeName = "PagePipe";

    /// <summary>Gets or sets the publishing point.</summary>
    /// <value>The publishing point.</value>
    public virtual IPublishingPointBusinessObject PublishingPoint { get; set; }

    /// <summary>Gets the name.</summary>
    /// <value>The name.</value>
    public override string Name => "PagePipe";

    internal PagePipeComponent PipeComponent
    {
      get => this.pipeComponent;
      set => this.pipeComponent = value;
    }

    /// <summary>
    /// Defines the data structure of the medium this pipe works with
    /// </summary>
    /// <value></value>
    public override IDefinitionField[] Definition
    {
      get
      {
        if (this.definitionFields == null)
          this.definitionFields = PublishingSystemFactory.GetPipeDefinitions(this.Name);
        return this.definitionFields;
      }
    }

    /// <summary>
    /// Used to get the default settings for this pipe, without creating persistent object
    /// </summary>
    /// <returns>The settings</returns>
    public static PagePipeSettings GetTemplatePipeSettings()
    {
      PagePipeSettings templatePipeSettings = new PagePipeSettings();
      templatePipeSettings.IsInbound = true;
      templatePipeSettings.IsActive = true;
      templatePipeSettings.InvocationMode = PipeInvokationMode.Push;
      templatePipeSettings.FilterExpression = PredefinedFilters.PublishedItems();
      templatePipeSettings.PipeName = "PagePipe";
      return templatePipeSettings;
    }

    /// <summary>Used to get the default mappings for this pipe</summary>
    /// <returns>The settings</returns>
    public static List<Mapping> GetDefaultMappings() => new List<Mapping>()
    {
      PublishingSystemFactory.CreateMapping("Id", "TransparentTranslator", true, "Id"),
      PublishingSystemFactory.CreateMapping("OriginalItemId", "TransparentTranslator", true, "Id"),
      PublishingSystemFactory.CreateMapping("Content", "TransparentTranslator", true, "Content"),
      PublishingSystemFactory.CreateMapping("Title", "concatenationtranslator", true, "Title"),
      PublishingSystemFactory.CreateMapping("Summary", "concatenationtranslator", true, "Description"),
      PublishingSystemFactory.CreateMapping("Categories", "concatenationtranslator", true, "Keywords"),
      PublishingSystemFactory.CreateMapping("Link", "concatenationtranslator", true, "Link"),
      PublishingSystemFactory.CreateMapping("PipeId", "concatenationtranslator", true, "PipeId"),
      PublishingSystemFactory.CreateMapping("ContentType", "TransparentTranslator", false, "ContentType"),
      PublishingSystemFactory.CreateMapping("LastModified", "TransparentTranslator", true, "LastModified")
    };

    /// <summary>Initializes the specified pipe settings.</summary>
    /// <param name="pipeSettings">The pipe settings.</param>
    public override void Initialize(PipeSettings pipeSettings)
    {
      this.PipeSettingsInternal = (PagePipeSettings) pipeSettings;
      this.PublishingPoint = PublishingSystemFactory.GetPublishingPoint(this.PipeSettings.PublishingPoint);
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
        case WrapperObject _:
          return this.CanProcessItem(((WrapperObject) item).WrappedObject);
        case PublishingSystemEventInfo _:
          PublishingSystemEventInfo publishingSystemEventInfo = (PublishingSystemEventInfo) item;
          return this.IsItemSupported(publishingSystemEventInfo) && publishingSystemEventInfo.ItemAction == "SystemObjectDeleted" || this.CanProcessItem(publishingSystemEventInfo.Item);
        case PageNode _:
          return this.ShouldProcessNode(item as PageNode);
        case PageDraft _:
        case PageData _:
          return true;
        default:
          return false;
      }
    }

    private bool IsItemSupported(PublishingSystemEventInfo item) => string.Equals(item.ItemType, typeof (PageNode).FullName) || string.Equals(item.ItemType, typeof (PageDraft).FullName) || string.Equals(item.ItemType, typeof (PageData).FullName);

    /// <summary>Pushes the data.</summary>
    /// <param name="items">The items.</param>
    public virtual void PushData(IList<PublishingSystemEventInfo> items) => this.PushDataAsync<PageInboundPipe>(items);

    /// <summary>Pushes the data synchronously.</summary>
    /// <param name="items">The items.</param>
    public virtual void PushDataSynchronously(IList<PublishingSystemEventInfo> items)
    {
      List<WrapperObject> items1 = new List<WrapperObject>();
      List<WrapperObject> items2 = new List<WrapperObject>();
      foreach (PublishingSystemEventInfo itemInfo in (IEnumerable<PublishingSystemEventInfo>) items)
      {
        if (!(itemInfo.ItemType != typeof (PageData).FullName) || !(itemInfo.ItemType != typeof (PageNode).FullName))
        {
          string itemAction = itemInfo.ItemAction;
          if (!(itemAction == "SystemObjectDeleted"))
          {
            if (!(itemAction == "SystemObjectAdded"))
            {
              if (itemAction == "SystemObjectModified")
              {
                PageNode pageNode = this.GetPageNode(itemInfo);
                if (this.ShouldProcessNode(pageNode, itemInfo))
                {
                  WrapperObject wrapperObject = this.GetConvertedItemsForMapping((object) itemInfo).First<WrapperObject>();
                  if (pageNode.IncludeInSearchIndex)
                    items1.AddRange((IEnumerable<WrapperObject>) new List<WrapperObject>()
                    {
                      wrapperObject
                    });
                  items2.AddRange((IEnumerable<WrapperObject>) new List<WrapperObject>()
                  {
                    wrapperObject
                  });
                }
              }
            }
            else if (this.ShouldProcessNode(this.GetPageNode(itemInfo), itemInfo))
            {
              WrapperObject wrapperObject = this.GetConvertedItemsForMapping((object) itemInfo).First<WrapperObject>();
              items1.AddRange((IEnumerable<WrapperObject>) new List<WrapperObject>()
              {
                wrapperObject
              });
            }
          }
          else
          {
            WrapperObject wrapperObject = new WrapperObject(this.PipeSettings, itemInfo.Item, itemInfo.Language);
            items2.AddRange((IEnumerable<WrapperObject>) new List<WrapperObject>()
            {
              wrapperObject
            });
          }
        }
      }
      if (items2.Count > 0)
        this.PublishingPoint.RemoveItems((IList<WrapperObject>) items2);
      if (items1.Count <= 0)
        return;
      this.PublishingPoint.AddItems((IList<WrapperObject>) items1);
    }

    private bool ShouldProcessNode(PageNode node, PublishingSystemEventInfo item = null)
    {
      if (this.PipeSettings.PublishingPoint.Settings.ItemFilterStrategy == PublishingItemFilter.Live)
      {
        if (node == null || node.NodeType == NodeType.Group || node.HasLinkedNode() || node.IsBackend || node.Parent == null)
          return false;
        if (item != null && item.ItemAction != "SystemObjectDeleted")
        {
          CultureInfo culture = item.Language != null ? CultureInfo.GetCultureInfo(item.Language) : (CultureInfo) null;
          if (!node.IsPublished(culture))
            return false;
        }
      }
      else if (this.PipeSettings.PublishingPoint.Settings.ItemFilterStrategy == PublishingItemFilter.All && (node == null || node.IsBackend || node.Parent == null))
        return false;
      return this.PipeComponent.CheckNode(node, this.PipeSettings.PublishingPoint);
    }

    /// <summary>Gets the data.</summary>
    /// <returns>Returns a list of the wrapped page nodes.</returns>
    public virtual IQueryable<WrapperObject> GetData()
    {
      PagesFacade pagesFacade = App.WorkWith().Pages().LocatedIn(PageLocation.Frontend).OrderBy<DateTime>((Expression<Func<PageNode, DateTime>>) (p => p.LastModified));
      if (this.PipeSettings.PublishingPoint.Settings.ItemFilterStrategy == PublishingItemFilter.Live)
        pagesFacade = pagesFacade.ThatArePublished();
      return this.GetWrappedPageNodes(pagesFacade.Get()).AsQueryable<WrapperObject>();
    }

    private IEnumerable<WrapperObject> GetWrappedPageNodes(
      IQueryable<PageNode> items)
    {
      PipeSettings pageSettings = this.PipeSettings;
      foreach (PageNode theInstance in (IEnumerable<PageNode>) items)
      {
        WrapperObject wrapperObject = new WrapperObject((object) theInstance);
        if (pageSettings.LanguageIds.Count<string>() == 0)
        {
          if (theInstance.AvailableCultures != null && theInstance.AvailableLanguages.Length != 0)
          {
            CultureInfo[] cultureInfoArray = theInstance.AvailableCultures;
            for (int index = 0; index < cultureInfoArray.Length; ++index)
            {
              wrapperObject.Language = cultureInfoArray[index].Name;
              yield return wrapperObject;
            }
            cultureInfoArray = (CultureInfo[]) null;
          }
          else
            yield return wrapperObject;
        }
        else
        {
          wrapperObject.Language = pageSettings.LanguageIds.FirstOrDefault<string>();
          yield return wrapperObject;
        }
        wrapperObject = (WrapperObject) null;
      }
    }

    /// <summary>Gets content item.</summary>
    /// <param name="wrappedObject">The wapped object.</param>
    /// <returns>The page node from the wrapped object.</returns>
    protected virtual PageNode GetContentItem(WrapperObject wrappedObject)
    {
      if (wrappedObject == null)
        return (PageNode) null;
      if (wrappedObject.WrappedObject == null)
        return (PageNode) null;
      return wrappedObject.WrappedObject is PageNode ? (PageNode) wrappedObject.WrappedObject : this.GetContentItem((WrapperObject) wrappedObject.WrappedObject);
    }

    /// <summary>Gets page node form publishing system event info.</summary>
    /// <param name="itemInfo">The event info.</param>
    /// <returns>The page node from the event info.</returns>
    protected virtual PageNode GetPageNode(PublishingSystemEventInfo itemInfo) => !(itemInfo.Item is WrapperObject) || ((WrapperObject) itemInfo.Item).WrappedObject == null ? itemInfo.Item as PageNode : this.GetContentItem((WrapperObject) itemInfo.Item);

    /// <summary>Gets the converted items for mapping.</summary>
    /// <param name="items">The items.</param>
    /// <returns>The converted items.</returns>
    public virtual IEnumerable<WrapperObject> GetConvertedItemsForMapping(
      params object[] itemInfos)
    {
      List<WrapperObject> convertedItemsForMapping = new List<WrapperObject>();
      foreach (PublishingSystemEventInfo itemInfo in itemInfos.OfType<PublishingSystemEventInfo>())
      {
        WrapperObject wrapperObject = new WrapperObject(this.PipeSettings, itemInfo.Item, itemInfo.Language);
        PageNode pageNode = this.GetPageNode(itemInfo);
        if (pageNode == null)
          throw new ArgumentException("Unable to find Page Node in the WrapperObject", "items");
        if (!wrapperObject.Language.IsNullOrEmpty())
          this.SwitchCultureAndSetupWrapperObject(wrapperObject, pageNode);
        else
          this.SetWrapperObjectProperties(wrapperObject, pageNode);
        convertedItemsForMapping.Add(wrapperObject);
      }
      return (IEnumerable<WrapperObject>) convertedItemsForMapping;
    }

    /// <summary>
    /// Switches the culture before render the page and generate page content that  is used to setup wrapper object properties.
    /// </summary>
    /// <param name="wrapperObject">The wrapper object.</param>
    /// <param name="node">The node.</param>
    protected virtual void SwitchCultureAndSetupWrapperObject(
      WrapperObject wrapperObject,
      PageNode node)
    {
      CultureInfo cultureInfo = new CultureInfo(wrapperObject.Language);
      if (cultureInfo == null)
        return;
      CultureInfo culture = SystemManager.CurrentContext.Culture;
      try
      {
        if (!SystemManager.CurrentContext.Culture.Equals((object) cultureInfo))
          SystemManager.CurrentContext.Culture = cultureInfo;
        this.SetWrapperObjectProperties(wrapperObject, node);
      }
      finally
      {
        if (!SystemManager.CurrentContext.Culture.Equals((object) culture))
          SystemManager.CurrentContext.Culture = culture;
      }
    }

    /// <summary>Sets the wrapper object properties.</summary>
    /// <param name="wrapperObject">The wrapper object.</param>
    /// <param name="node">The node.</param>
    protected virtual void SetWrapperObjectProperties(WrapperObject wrapperObject, PageNode node)
    {
      string str1 = string.Empty;
      try
      {
        str1 = this.RenderPage(node);
      }
      catch (Exception ex)
      {
        if (Exceptions.HandleException(ex, ExceptionPolicyName.IgnoreExceptions))
          throw ex;
      }
      wrapperObject.AddProperty("Content", (object) str1);
      PageData pageData = node.GetPageData();
      if (pageData != null)
        this.AddPersonalizedContent(wrapperObject, node, pageData);
      PageNode contentItem = this.GetContentItem(wrapperObject);
      PublishingUtilities.AddItemCategories(wrapperObject, (IDataItem) contentItem);
      wrapperObject.AddProperty("UnresolveContentField", (object) true);
      PublishingUtilities.AddContentUsages(wrapperObject, (IDataItem) contentItem);
      if (pageData != null)
        wrapperObject.AddProperty("Keywords", (object) pageData.Keywords.ToString());
      else
        wrapperObject.AddProperty("Keywords", (object) string.Empty);
      string str2 = string.Format("[{0}]{1}", (object) node.RootNodeId, (object) node.Id);
      wrapperObject.AddProperty("Link", (object) str2);
      if (pageData != null && (node.NodeType == NodeType.Standard || node.NodeType == NodeType.External))
        wrapperObject.AdditionalProperties["Title"] = (object) pageData.HtmlTitle.ToString();
      if (!wrapperObject.HasProperty("PipeId"))
        wrapperObject.AddProperty("PipeId", (object) string.Empty);
      if (!wrapperObject.HasProperty("ContentType"))
        wrapperObject.AddProperty("ContentType", (object) typeof (PageNode).FullName);
      if (wrapperObject.HasProperty("PublicationDate"))
        return;
      wrapperObject.AddProperty("PublicationDate", (object) pageData?.PublicationDate);
    }

    /// <summary>Renders the page.</summary>
    /// <param name="node">The node.</param>
    /// <param name="segmentId">The segment identifier.</param>
    /// <returns></returns>
    protected virtual string RenderPage(PageNode node, Guid? segmentId = null)
    {
      InMemoryPageRender memoryPageRender = new InMemoryPageRender();
      using (SiteRegion.FromSiteMapRoot(node.RootNodeId))
        return memoryPageRender.RenderPage(node, false, true, segmentId);
    }

    /// <summary>Gets the pipe settings short description.</summary>
    /// <param name="initSettings">The init settings.</param>
    /// <returns></returns>
    public new virtual string GetPipeSettingsShortDescription(PipeSettings initSettings) => string.Empty;

    /// <summary>
    /// Gets the data from publishing point and "pushes" them to corresponding publishing point
    /// </summary>
    public virtual void ToPublishingPoint()
    {
      IEnumerable<PageNode[]> pageNodeArrays = this.LoadPageNodes().OnBatchesOf<PageNode>(20);
      string empty = string.Empty;
      foreach (PageNode[] pageNodeArray in pageNodeArrays)
      {
        List<PublishingSystemEventInfo> publishingSystemEventInfoList = new List<PublishingSystemEventInfo>();
        foreach (PageNode pageNode in pageNodeArray)
        {
          try
          {
            if (pageNode.HasLinkedNode())
            {
              PageNode terminalLinkedNode = pageNode.GetTerminalLinkedNode();
              PageData pageData = (PageData) null;
              if (terminalLinkedNode != null)
                pageData = terminalLinkedNode.GetPageData();
              if (terminalLinkedNode != null)
              {
                if ((terminalLinkedNode.NodeType == NodeType.Standard || terminalLinkedNode.NodeType == NodeType.External) && pageData != null)
                {
                  if (pageData.Version != 0)
                  {
                    if (!pageData.Visible)
                      continue;
                  }
                  else
                    continue;
                }
              }
              else
                continue;
            }
            if (pageNode.AvailableLanguages.Length != 0)
            {
              foreach (string availableLanguage in pageNode.AvailableLanguages)
                this.CreatePublishingSystemEventInfo(publishingSystemEventInfoList, pageNode, availableLanguage);
            }
            else
            {
              string name = SystemManager.CurrentContext.Culture.Name;
              this.CreatePublishingSystemEventInfo(publishingSystemEventInfoList, pageNode, name);
            }
          }
          catch (Exception ex)
          {
            Log.Error("Failed to process page {0} through InboundPipe. Class: {1}, Method: {2}, Exception {3}, Stack Trace {4}", (object) pageNode.Id, (object) this.GetType().Name, (object) MethodBase.GetCurrentMethod().Name, (object) ex.Message, (object) ex.StackTrace);
          }
        }
        try
        {
          this.PushDataSynchronously((IList<PublishingSystemEventInfo>) publishingSystemEventInfoList);
        }
        catch (Exception ex)
        {
          Log.Error("Failed to push data. Method name:{0}. Message: {1}. StackTrace: {2}", (object) MethodBase.GetCurrentMethod().Name, (object) ex.Message, (object) ex.StackTrace);
        }
      }
    }

    private void CreatePublishingSystemEventInfo(
      List<PublishingSystemEventInfo> eventInfo,
      PageNode item,
      string language)
    {
      PublishingSystemEventInfo publishingSystemEventInfo = new PublishingSystemEventInfo()
      {
        Item = (object) item,
        ItemAction = "SystemObjectModified",
        Language = language
      };
      eventInfo.Add(publishingSystemEventInfo);
    }

    /// <summary>
    /// Loads the page nodes that are to be passed to the publishing point.
    /// </summary>
    /// <returns></returns>
    protected virtual IEnumerable<PageNode> LoadPageNodes()
    {
      PageInboundPipe pageInboundPipe = this;
      foreach (Guid siteRootId in (IEnumerable<Guid>) pageInboundPipe.PipeComponent.GetSiteRootIds(pageInboundPipe.PipeSettings.PublishingPoint))
      {
        PagesFacade pagesFacade = App.WorkWith().Pages().LocatedIn(siteRootId).OrderBy<DateTime>((Expression<Func<PageNode, DateTime>>) (p => p.LastModified));
        if (pageInboundPipe.PipeSettings.PublishingPoint.Settings.ItemFilterStrategy == PublishingItemFilter.Live)
          pagesFacade = pagesFacade.ThatArePublished();
        foreach (PageNode pageNode in (IEnumerable<PageNode>) pagesFacade.ThatAreForAnyLanguage().Get())
          yield return pageNode;
      }
    }

    private void AddPersonalizedContent(
      WrapperObject wrapperObject,
      PageNode node,
      PageData pageData)
    {
      if (node == null || pageData == null)
        return;
      string empty = string.Empty;
      PageManager manager = PageManager.GetManager();
      string personalizedControlsContent1 = this.GetPersonalizedControlsContent(manager, node);
      string str1 = empty + personalizedControlsContent1;
      if (pageData.IsPersonalized)
      {
        IQueryable<PageData> source = manager.GetPageDataList().Where<PageData>((Expression<Func<PageData, bool>>) (p => p.PersonalizationMasterId == pageData.Id));
        Expression<Func<PageData, Guid>> selector = (Expression<Func<PageData, Guid>>) (p => p.PersonalizationSegmentId);
        foreach (Guid guid in (IEnumerable<Guid>) source.Select<PageData, Guid>(selector))
        {
          try
          {
            string str2 = this.RenderPage(node, new Guid?(guid));
            str1 += str2;
            string personalizedControlsContent2 = this.GetPersonalizedControlsContent(manager, node, new Guid?(guid));
            str1 += personalizedControlsContent2;
          }
          catch (Exception ex)
          {
            if (Exceptions.HandleException(ex, ExceptionPolicyName.IgnoreExceptions))
              throw ex;
          }
        }
      }
      wrapperObject.AddProperty("PersonalizedContent", (object) str1);
    }

    private string GetPersonalizedControlsContent(
      PageManager pageManager,
      PageNode node,
      Guid? pageSegmentId = null)
    {
      string empty1 = string.Empty;
      try
      {
        PageData pageData;
        if (pageSegmentId.HasValue)
        {
          Guid? nullable = pageSegmentId;
          Guid empty2 = Guid.Empty;
          if ((nullable.HasValue ? (nullable.HasValue ? (nullable.GetValueOrDefault() != empty2 ? 1 : 0) : 0) : 1) != 0)
          {
            pageData = node.GetPersonalizedPage(pageSegmentId.Value);
            goto label_5;
          }
        }
        pageData = node.GetPageData();
label_5:
        empty1 += PageInboundPipe.GetPersonalizedControlsContent(pageManager, node, (IEnumerable<ControlData>) pageData.Controls);
        List<PageTemplate> templates = PageHelper.GetTemplates(pageData);
        if (templates != null)
        {
          foreach (PageTemplate pageTemplate in templates)
            empty1 += PageInboundPipe.GetPersonalizedControlsContent(pageManager, node, (IEnumerable<ControlData>) pageTemplate.Controls);
        }
      }
      catch (Exception ex)
      {
        if (Exceptions.HandleException(ex, ExceptionPolicyName.IgnoreExceptions))
          throw ex;
      }
      return empty1;
    }

    private static string GetPersonalizedControlsContent(
      PageManager pageManager,
      PageNode node,
      IEnumerable<ControlData> originalControls)
    {
      string empty = string.Empty;
      List<Guid> personalizedMasterIds = originalControls.Where<ControlData>((Func<ControlData, bool>) (c => c.IsPersonalized)).Select<ControlData, Guid>((Func<ControlData, Guid>) (c => c.Id)).ToList<Guid>();
      if (personalizedMasterIds.Count > 0)
      {
        IEnumerable<Control> controls = pageManager.GetControls<ControlData>().Where<ControlData>((Expression<Func<ControlData, bool>>) (c => personalizedMasterIds.Contains(c.PersonalizationMasterId))).ToList<ControlData>().Select<ControlData, Control>((Func<ControlData, Control>) (p => pageManager.LoadControl((ObjectData) p, (CultureInfo) null)));
        if (controls.Count<Control>() > 0)
        {
          string liveUrl = node.GetLiveUrl();
          List<string> controlsHtml = new ControlLiteralRepresentation(controls, node.Id, liveUrl, true).GetControlsHtml();
          empty += string.Join(string.Empty, (IEnumerable<string>) controlsHtml);
        }
      }
      return empty;
    }
  }
}
