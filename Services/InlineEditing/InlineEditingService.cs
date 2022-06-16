// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Services.InlineEditing.InlineEditingService
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using HtmlAgilityPack;
using ServiceStack;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Hosting;
using System.Web.UI;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.InlineEditing;
using Telerik.Sitefinity.InlineEditing.Resolvers;
using Telerik.Sitefinity.Licensing;
using Telerik.Sitefinity.Lifecycle;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Modules.Pages;
using Telerik.Sitefinity.Multisite;
using Telerik.Sitefinity.Pages.Model;
using Telerik.Sitefinity.Personalization;
using Telerik.Sitefinity.Security;
using Telerik.Sitefinity.Services.InlineEditing.Messages;
using Telerik.Sitefinity.Services.ServiceStack.Filters;
using Telerik.Sitefinity.Utilities.TypeConverters;
using Telerik.Sitefinity.Web;
using Telerik.Sitefinity.Web.Configuration;
using Telerik.Sitefinity.Web.Services;
using Telerik.Sitefinity.Web.UI;
using Telerik.Sitefinity.Web.UI.Fields.Config;
using Telerik.Sitefinity.Web.UI.PublicControls;
using Telerik.Sitefinity.Workflow;

namespace Telerik.Sitefinity.Services.InlineEditing
{
  /// <summary>
  /// This class represents Service used from the Inline Editing feature in sitefinity
  /// </summary>
  [RequestBackendAuthenticationFilter(false)]
  [RequestRootPermissionsFilter("Backend", "UseBrowseAndEdit")]
  public class InlineEditingService : Service
  {
    internal const string WebServiceUrl = "RestApi/Sitefinity/inlineediting";

    /// <summary>
    /// Gets the current temp item if any or creates a new one and return it`s id
    /// </summary>
    /// <param name="message"></param>
    /// <returns></returns>
    /// 
    ///             url: "/inlineediting/temp/{ItemId}"
    public Guid Get(EditableItemMessage message)
    {
      Guid itemId = new Guid(message.ItemId);
      Type type = TypeResolutionService.ResolveType(message.ItemType);
      Guid editableItemId = this.GetInlineEditingStrategy(type).GetEditableItemId(itemId, type, message.Provider);
      ServiceUtility.DisableCache();
      return editableItemId;
    }

    /// <summary>Delete the current temp item</summary>
    /// <param name="message"></param>
    /// <returns></returns>
    /// 
    ///             url: "/inlineediting/temp/{ItemId}"
    public void Delete(EditableItemMessage message)
    {
      Guid itemId = new Guid(message.ItemId);
      Type type = TypeResolutionService.ResolveType(message.ItemType);
      this.GetInlineEditingStrategy(type).DeleteEditableItem(itemId, type, message.Provider);
    }

    /// <summary>Save the current temp item</summary>
    /// <param name="message"></param>
    /// <returns></returns>
    /// 
    ///             url: "/inlineediting/temp/{ItemId}"
    public void Put(EditableItemMessage message)
    {
      Guid itemId = new Guid(message.ItemId);
      Type type = TypeResolutionService.ResolveType(message.ItemType);
      this.GetInlineEditingStrategy(type).SaveEditableItemFields(message.Fields.ToArray(), itemId, type, message.Provider);
    }

    /// <summary>Executes sent workflow operation</summary>
    /// <param name="message"></param>
    /// <returns></returns>
    /// 
    ///             url: "/inlineediting/workflow"
    public List<BaseWorkflowOperationModel> Post(
      WorkflowOperationMessage message)
    {
      bool flag = false;
      List<BaseWorkflowOperationModel> workflowOperationModelList = new List<BaseWorkflowOperationModel>();
      foreach (ItemContextModel itemContextModel in message.Items)
      {
        Guid itemId = new Guid(itemContextModel.ItemId);
        Type type = TypeResolutionService.ResolveType(itemContextModel.ItemType);
        IInlineEditingStrategy inlineEditingStrategy = this.GetInlineEditingStrategy(type);
        if (itemContextModel.FieldValues != null && itemContextModel.FieldValues.Length != 0)
          inlineEditingStrategy.SaveEditableItemFields(itemContextModel.FieldValues, itemId, type, itemContextModel.Provider);
      }
      foreach (ItemContextModel itemContextModel in message.Items.Distinct<ItemContextModel>())
      {
        Guid itemId = new Guid(itemContextModel.ItemId);
        Type type = TypeResolutionService.ResolveType(itemContextModel.ItemType);
        WorkflowOperationResultModel operationResultModel = this.GetInlineEditingStrategy(type).ExecuteOperation(itemId, type, itemContextModel.Provider, message.WorkflowOperation);
        if (!operationResultModel.IsExecutedSuccessfully)
          workflowOperationModelList.Add(new BaseWorkflowOperationModel()
          {
            ItemId = operationResultModel.ItemId,
            ItemName = operationResultModel.ItemName,
            ItemType = operationResultModel.ItemType,
            ItemStatus = operationResultModel.ItemStatus,
            ProviderName = operationResultModel.ProviderName,
            DetailsViewUrl = operationResultModel.DetailsViewUrl
          });
        flag |= operationResultModel.RequiresPageOperation;
      }
      if (flag)
      {
        WorkflowOperationResultModel operationResultModel = this.ExecutePageWorkflowOperation(message.PageId, message.WorkflowOperation.ToString(), message.CustomWorkflowOperation, string.Empty);
        if (!operationResultModel.IsExecutedSuccessfully)
          workflowOperationModelList.Add(new BaseWorkflowOperationModel()
          {
            DetailsViewUrl = operationResultModel.DetailsViewUrl,
            ItemName = operationResultModel.ItemName,
            ItemType = operationResultModel.ItemType,
            ItemStatus = operationResultModel.ItemStatus
          });
      }
      return workflowOperationModelList;
    }

    /// <summary>Executes sent workflow operation</summary>
    /// <param name="message"></param>
    /// <returns></returns>
    /// 
    ///             url: "/inlineediting/workflow"
    public void Delete(WorkflowOperationMessage message)
    {
      foreach (ItemContextModel itemContextModel in message.Items)
      {
        Guid itemId = new Guid(itemContextModel.ItemId);
        Type type = TypeResolutionService.ResolveType(itemContextModel.ItemType);
        this.GetInlineEditingStrategy(type).DeleteEditableItem(itemId, type, itemContextModel.Provider);
      }
    }

    /// <summary>Returns the rendered page inner html</summary>
    /// <param name="message"></param>
    /// <returns></returns>
    /// 
    ///             url: "/inlineediting/render?format=json"
    public string Post(RenderMessage message)
    {
      Guid pageId = new Guid(message.PageId);
      Guid id = new Guid(message.ControlId);
      PageManager manager = PageManager.GetManager();
      PageDraftControl control = manager.GetControl<PageDraftControl>(id);
      SiteMapProvider currentProvider = SitefinitySiteMap.GetCurrentProvider();
      SiteMapNode siteMapNodeFromKey = currentProvider.FindSiteMapNodeFromKey(message.PageId);
      IDictionary items = SystemManager.CurrentHttpContext.Items;
      items[(object) "SF_SiteMap"] = (object) currentProvider;
      items[(object) "ServedPageNode"] = (object) siteMapNodeFromKey;
      Control originalControl = manager.LoadControl((ObjectData) control, (CultureInfo) null);
      if (originalControl is IHasContainerType)
        ((IHasContainerType) originalControl).ContainerType = typeof (PageDraftControlsContainerWrapper);
      SystemManager.HttpContextItems[(object) "RadControlRandomNumber"] = (object) 0;
      string controlHtml = new Telerik.Sitefinity.InlineEditing.ControlLiteralRepresentation(originalControl, pageId, SystemManager.CurrentHttpContext.Server.UrlDecode(message.Url)).GetControlHtml();
      HtmlDocument htmlDocument = new HtmlDocument();
      htmlDocument.LoadHtml(controlHtml);
      Guid guid = Guid.Parse(message.DataItemId);
      if (id == guid)
      {
        string xpath = string.Format("//*[@data-sf-ftype]");
        return htmlDocument.DocumentNode.SelectSingleNode(xpath).OuterHtml;
      }
      string xpath1 = string.Format("//*[@data-sf-id='{0}']", (object) guid.ToString());
      if (!string.IsNullOrEmpty(message.FieldName))
        xpath1 = string.Format("{0}//*[@data-sf-field='{1}']", (object) xpath1, (object) message.FieldName);
      HtmlNode htmlNode = htmlDocument.DocumentNode.SelectSingleNode(xpath1);
      ServiceUtility.DisableCache();
      return htmlNode.InnerHtml;
    }

    /// <summary>Returns the requirejs Configuration</summary>
    /// <param name="cm"></param>
    /// <returns></returns>
    /// 
    ///             url: "/inlineediting/configuration"
    public string Get(ConfigurationMessage cm)
    {
      ConfigValueDictionary requireJsModules = Telerik.Sitefinity.Configuration.Config.Get<InlineEditingConfig>().RequireJsModules;
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("requirejs.config({");
      stringBuilder.Append("baseUrl: '").Append((HostingEnvironment.ApplicationVirtualPath + "/Res").Replace("//", "/")).Append("',");
      stringBuilder.Append("paths: {");
      foreach (ConfigElement configElement in (ConfigElementCollection) requireJsModules)
        stringBuilder.Append(string.Format("{0}: '{1}',", (object) configElement.GetKey(), (object) requireJsModules[configElement.GetKey()]));
      if (requireJsModules.Count > 0)
        stringBuilder.Remove(stringBuilder.Length - 1, 1);
      stringBuilder.Append("}");
      stringBuilder.Append("});");
      return stringBuilder.ToString();
    }

    /// <summary>Returns the container info for a specific</summary>
    /// <param name="data"></param>
    /// <returns></returns>
    /// 
    ///             url: "/inlineediting/containersInfo"
    public object Post(ContainerInfoMessage message)
    {
      Guid id = new Guid(message.PageId);
      PageNode pageNode = (PageNode) null;
      if (id != Guid.Empty)
      {
        pageNode = PageManager.GetManager().GetPageNode(id);
        if (pageNode != null)
          message.PageTitle = (string) pageNode.Title;
      }
      foreach (ContainerInfoModel containerInfoModel in message.ContainersInfo)
      {
        Guid itemId = new Guid(containerInfoModel.ItemId);
        Type type = TypeResolutionService.ResolveType(containerInfoModel.ItemType);
        IInlineEditingStrategy inlineEditingStrategy = this.GetInlineEditingStrategy(type);
        object obj = inlineEditingStrategy.GetItem(itemId, type, containerInfoModel.Provider);
        LifecycleStatusModel itemStatus = inlineEditingStrategy.GetItemStatus(obj);
        containerInfoModel.ItemStatus = itemStatus;
        containerInfoModel.IsPageControl = typeof (ControlData).IsAssignableFrom(type);
        containerInfoModel.DetailsViewUrl = inlineEditingStrategy.GetDetailsViewUrl(obj.GetType(), pageNode);
        InlineEditingResolverBase editingResolverBase = ObjectFactory.Resolve<InlineEditingResolverBase>();
        containerInfoModel.ItemStatus = itemStatus;
        containerInfoModel.IsPageControl = typeof (ControlData).IsAssignableFrom(type);
        foreach (FieldModel field in containerInfoModel.Fields)
        {
          if (obj is ILifecycleDataItem)
          {
            FieldDefinitionElement fieldDefinition = editingResolverBase.GetFieldDefinition(obj as ILifecycleDataItem, field.Name);
            if (fieldDefinition is FieldControlDefinitionElement)
            {
              FieldControlDefinitionElement definitionElement = fieldDefinition as FieldControlDefinitionElement;
              FieldModel fieldModel = field;
              bool? required = definitionElement.Validation.Required;
              bool flag = true;
              string str = required.GetValueOrDefault() == flag & required.HasValue ? "required" : (string) null;
              fieldModel.Required = str;
              field.MaxLength = definitionElement.Validation.MaxLength;
              field.MinLength = definitionElement.Validation.MinLength;
              field.Pattern = definitionElement.Validation.RegularExpression != string.Empty ? definitionElement.Validation.RegularExpression : (string) null;
              field.MinValue = definitionElement.Validation.MinValue;
              field.MaxValue = definitionElement.Validation.MaxValue;
              field.RequiredViolationMessage = Res.Get<PublicControlsResources>().RequiredViolationMessage;
              field.MinLengthViolationMessage = Res.Get<PublicControlsResources>().MinLengthViolationMessage;
              field.MaxLengthViolationMessage = Res.Get<PublicControlsResources>().MaxLengthViolationMessage;
            }
          }
        }
        containerInfoModel.DisplayType = InlineEditingHelper.GetDisplayName(obj, message.PageTitle);
      }
      return (object) message;
    }

    /// <summary>Sets the field value of specific field</summary>
    /// <param name="message"></param>
    /// <returns></returns>
    /// 
    ///             url: "/inlineediting/fieldValue"
    public object Post(FieldValueMessage message)
    {
      IInlineEditingResolver inlineEditingResolver = ObjectFactory.Container.ResolveAll(typeof (IInlineEditingResolver)).Cast<IInlineEditingResolver>().FirstOrDefault<IInlineEditingResolver>((Func<IInlineEditingResolver, bool>) (fr => fr.GetFieldType() == message.FieldType));
      object obj = (object) null;
      if (inlineEditingResolver != null)
        obj = inlineEditingResolver.GetFieldValue(new Guid(message.DataItemId), message.ItemType, message.FieldName, message.Provider);
      return obj;
    }

    public object Get(MultisiteMessage request)
    {
      List<ISite> siteList = new List<ISite>();
      IMultisiteContext multisiteContext = SystemManager.CurrentContext.MultisiteContext;
      if (multisiteContext != null)
      {
        UserActivityRecord userActivityRecord = UserActivityManager.GetCurrentUserActivity();
        if (userActivityRecord != null)
          siteList = multisiteContext.GetSites().Where<ISite>((Func<ISite, bool>) (s => userActivityRecord.AllowedSites.Contains(s.Id))).OrderBy<ISite, string>((Func<ISite, string>) (s => s.Name)).ToList<ISite>();
      }
      List<SiteModel> siteModelList = new List<SiteModel>();
      Guid id = SystemManager.CurrentContext.CurrentSite.Id;
      foreach (ISite site in siteList)
      {
        SiteModel siteModel = new SiteModel()
        {
          Name = site.Name,
          SiteId = site.Id,
          SiteMapRootNodeId = site.SiteMapRootNodeId,
          IsCurrent = id == site.Id
        };
        siteModelList.Add(siteModel);
      }
      return (object) siteModelList;
    }

    private IInlineEditingStrategy GetInlineEditingStrategy(Type itemType) => this.Factory.GetStrategy(itemType);

    private WorkflowOperationResultModel ExecutePageWorkflowOperation(
      string pageId,
      string workflowOperation,
      string customWorkflowOperation,
      string provider)
    {
      WorkflowOperationResultModel operationResultModel = new WorkflowOperationResultModel();
      operationResultModel.IsExecutedSuccessfully = true;
      Guid guid = new Guid(pageId);
      PageManager manager = PageManager.GetManager(provider);
      PageNode pageNode1 = manager.GetPageNode(guid);
      Type type = pageNode1.GetType();
      string empty = string.Empty;
      string providerName = provider;
      Guid id = pageNode1.Id;
      string workflowOperation1 = workflowOperation;
      string customWorkflowOperation1 = customWorkflowOperation;
      ref string local = ref empty;
      if (!InlineEditingHelper.IsWorkflowOperationValid(type, providerName, id, workflowOperation1, customWorkflowOperation1, out local))
      {
        workflowOperation = empty;
        operationResultModel.IsExecutedSuccessfully = false;
        operationResultModel.DetailsViewUrl = VirtualPathUtility.ToAbsolute(pageNode1.GetUrl()) + "/Action/Edit";
        operationResultModel.ItemType = pageNode1.GetType().FullName;
        operationResultModel.ItemName = (string) pageNode1.Title;
      }
      Type itemType = typeof (PageNode);
      Dictionary<string, string> contextBag = new Dictionary<string, string>();
      contextBag.Add("ContentType", itemType.FullName);
      if (pageNode1.GetPageData().IsPersonalized)
      {
        IPersonalizationService personalizationService = SystemManager.GetPersonalizationService();
        if (personalizationService != null)
        {
          ISegment userSegment = personalizationService.GetUserSegment(pageNode1.PageId);
          if (userSegment != null)
            contextBag.Add("SegmentId", userSegment.Id.ToString());
        }
      }
      WorkflowManager.MessageWorkflow(guid, itemType, provider, empty, false, contextBag);
      manager.Dispose();
      PageNode pageNode2 = PageManager.GetManager(provider).GetPageNode(guid);
      operationResultModel.ItemStatus = InlineEditingHelper.GetPageControlLifecycleStatus(pageNode2);
      return operationResultModel;
    }

    public IInlineEditingStrategyFactory Factory { get; set; }
  }
}
