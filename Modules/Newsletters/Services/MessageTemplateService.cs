// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Newsletters.Services.MessageTemplateService
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.ServiceModel;
using System.ServiceModel.Activation;
using Telerik.Sitefinity.Data.Linq.Dynamic;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Modules.Newsletters.Services.ViewModel;
using Telerik.Sitefinity.Modules.Pages;
using Telerik.Sitefinity.Modules.Pages.Web.Services.Model;
using Telerik.Sitefinity.Newsletters.Model;
using Telerik.Sitefinity.Pages.Model;
using Telerik.Sitefinity.Web.Services;

namespace Telerik.Sitefinity.Modules.Newsletters.Services
{
  /// <summary>The service that works with message templates.</summary>
  [ServiceBehavior(ConcurrencyMode = ConcurrencyMode.Single, IncludeExceptionDetailInFaults = true, InstanceContextMode = InstanceContextMode.Single)]
  [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Required)]
  public class MessageTemplateService : IMessageTemplateService
  {
    /// <summary>
    /// Saves a template. If the template with specified id exists that template will be updated; otherwise new template will be created.
    /// The saved template is returned in JSON format.
    /// </summary>
    /// <param name="templateId">Id of the template to be saved.</param>
    /// <param name="template">The view model class that represents the message template.</param>
    /// <param name="provider">The provider through which the template ought to be saved.</param>
    /// <param name="isPageBased">Determines weather the template is based on the sitefinity page.</param>
    /// <returns>The saved template.</returns>
    public MessageBodyViewModel SaveTemplate(
      string templateId,
      MessageBodyViewModel template,
      string provider,
      bool isPageBased)
    {
      ServiceUtility.RequestBackendUserAuthentication();
      return this.SaveCampaignInternal(templateId, template, provider, isPageBased);
    }

    /// <summary>
    /// Saves a template. If the template with specified id exists that template will be updated; otherwise new template will be created.
    /// The saved template is returned in XML format.
    /// </summary>
    /// <param name="templateId">The id of the template.</param>
    /// <param name="template">The view model class that represents the message template.</param>
    /// <param name="provider">The provider through which the template ought to be saved.</param>
    /// <param name="isPageBased">Determines weather the template is based on the sitefinity page.</param>
    /// <returns>The saved template.</returns>
    public MessageBodyViewModel SaveTemplateInXml(
      string templateId,
      MessageBodyViewModel template,
      string provider,
      bool isPageBased)
    {
      ServiceUtility.RequestBackendUserAuthentication();
      return this.SaveCampaignInternal(templateId, template, provider, isPageBased);
    }

    /// <summary>
    /// Gets all message templates of the newsletter module for the given provider. The results are returned in JSON format.
    /// </summary>
    /// <param name="provider">Name of the provider from which the message templates ought to be retrieved.</param>
    /// <param name="sortExpression">Sort expression used to order the message templates.</param>
    /// <param name="skip">Number of message templates to skip in result set. (used for paging)</param>
    /// <param name="take">Number of message templates to take in the result set. (used for paging)</param>
    /// <param name="filter">Dynamic LINQ expression used to filter the wanted result set.</param>
    /// <returns>
    /// Collection context object of <see cref="T:Telerik.Sitefinity.Modules.Newsletters.Services.ViewModel.MessageBodyViewModel" /> objects.
    /// </returns>
    public CollectionContext<MessageBodyViewModel> GetTemplates(
      string provider,
      string sortExpression,
      int skip,
      int take,
      string filter)
    {
      ServiceUtility.RequestBackendUserAuthentication();
      return this.GetTemplatesInternal(provider, sortExpression, skip, take, filter);
    }

    /// <summary>
    /// Gets all message templates of the newsletter module for the given provider. The results are returned in XML format.
    /// </summary>
    /// <param name="provider">Name of the provider from which the message templates ought to be retrieved.</param>
    /// <param name="sortExpression">Sort expression used to order the message templates.</param>
    /// <param name="skip">Number of message templates to skip in result set. (used for paging)</param>
    /// <param name="take">Number of message templates to take in the result set. (used for paging)</param>
    /// <param name="filter">Dynamic LINQ expression used to filter the wanted result set.</param>
    /// <returns>
    /// Collection context object of <see cref="T:Telerik.Sitefinity.Modules.Newsletters.Services.ViewModel.MessageBodyViewModel" /> objects.
    /// </returns>
    public CollectionContext<MessageBodyViewModel> GetTemplatesInXml(
      string provider,
      string sortExpression,
      int skip,
      int take,
      string filter)
    {
      ServiceUtility.RequestBackendUserAuthentication();
      return this.GetTemplatesInternal(provider, sortExpression, skip, take, filter);
    }

    /// <summary>
    /// Deletes the message template by id and returns true if the message template has been deleted; otherwise false.
    /// Result is returned in JSON format.
    /// </summary>
    /// <param name="templateId">Id of the message template to be deleted.</param>
    /// <param name="provider">The name of provider.</param>
    /// <returns></returns>
    public bool DeleteTemplate(string templateId, string provider)
    {
      ServiceUtility.RequestBackendUserAuthentication();
      return this.DeleteTemplateInternal(templateId, provider);
    }

    /// <summary>
    /// Deletes the message template by id and returns true if the message template has been deleted; otherwise false.
    /// Result is returned in XML format.
    /// </summary>
    /// <param name="templateId">Id of the message template to be deleted.</param>
    /// <param name="provider">The name of provider.</param>
    /// <returns></returns>
    public bool DeleteTemplateInXml(string templateId, string provider)
    {
      ServiceUtility.RequestBackendUserAuthentication();
      return this.DeleteTemplateInternal(templateId, provider);
    }

    /// <summary>
    /// Deletes a collection of message templates. Result is returned in JSON format.
    /// </summary>
    /// <param name="templateIds">An array of the ids of the message templates to delete.</param>
    /// <param name="provider">The name of the newsletter provider.</param>
    /// <returns>
    /// True if all message templates have been deleted; otherwise false.
    /// </returns>
    public bool BatchDeleteTemplates(string[] templateIds, string provider)
    {
      ServiceUtility.RequestBackendUserAuthentication();
      return this.BatchDeleteTemplatesInternal(templateIds, provider);
    }

    /// <summary>
    /// Deletes a collection of message templates. Result is returned in XML format.
    /// </summary>
    /// <param name="templateIds">An array of the ids of the message templates to delete.</param>
    /// <param name="provider">The name of the newsletter provider.</param>
    /// <returns>
    /// True if all message templates have been deleted; otherwise false.
    /// </returns>
    public bool BatchDeleteTemplatesInXml(string[] templateIds, string provider)
    {
      ServiceUtility.RequestBackendUserAuthentication();
      return this.BatchDeleteTemplatesInternal(templateIds, provider);
    }

    /// <summary>
    /// Gets the message template and returns it in JSON format.
    /// </summary>
    /// <param name="templateId">Id of the message template that ought to be retrieved.</param>
    /// <param name="provider">Name of the provider that is to be used to retrieve the message template.</param>
    /// <returns>An instance of MessageBodyViewModel.</returns>
    public MessageBodyViewModel GetTemplate(string templateId, string provider) => this.GetTemplateInternal(templateId, provider);

    /// <summary>
    /// Gets the message template and returns it in XML format.
    /// </summary>
    /// <param name="templateId">Id of the message template that ought to be retrieved.</param>
    /// <param name="provider">Name of the provider that is to be used to retrieve the message template.</param>
    /// <returns>An instance of MessageBodyViewModel.</returns>
    public MessageBodyViewModel GetTemplateInXml(
      string templateId,
      string provider)
    {
      return this.GetTemplateInternal(templateId, provider);
    }

    private MessageBodyViewModel SaveCampaignInternal(
      string templateId,
      MessageBodyViewModel template,
      string provider,
      bool isPageBased)
    {
      NewslettersManager manager = NewslettersManager.GetManager(provider);
      Guid messageBodyId = new Guid(templateId);
      MessageBody messageBody;
      if (messageBodyId == Guid.Empty)
      {
        messageBody = manager.CreateMessageBody();
        messageBody.IsTemplate = true;
      }
      else
        messageBody = manager.GetMessageBody(messageBodyId);
      this.CopyToMessageBody(template, messageBody);
      manager.SaveChanges();
      this.CopyToViewModel(messageBody, template);
      if (isPageBased)
        manager.CreateMessageBodyPage(messageBody);
      ServiceUtility.DisableCache();
      return template;
    }

    private void CreateTemplateInternalPage(
      MessageBody newTemplate,
      NewslettersManager newslettersManager)
    {
      PageManager manager = PageManager.GetManager();
      PageNode pageNode = manager.CreatePageNode(newTemplate.Id);
      pageNode.RootNode = newslettersManager.GetStandardCampaignRootNode();
      pageNode.SupportedPermissionSets = new string[1]
      {
        "Pages"
      };
      PageData pageData = manager.CreatePageData(newTemplate.Id);
      if (newTemplate.InternalPageTemplateId != Guid.Empty)
        pageData.Template = manager.GetTemplate(newTemplate.InternalPageTemplateId);
      pageData.NavigationNode = pageNode;
      manager.SaveChanges();
    }

    private CollectionContext<MessageBodyViewModel> GetTemplatesInternal(
      string provider,
      string sortExpression,
      int skip,
      int take,
      string filter)
    {
      IQueryable<MessageBody> source1 = NewslettersManager.GetManager(provider).GetMessageBodies().Where<MessageBody>((Expression<Func<MessageBody, bool>>) (mb => mb.IsTemplate == true));
      IQueryable<MessageBody> source2;
      if (!string.IsNullOrEmpty(sortExpression))
        source2 = source1.OrderBy<MessageBody>(sortExpression);
      else
        source2 = (IQueryable<MessageBody>) source1.OrderBy<MessageBody, DateTime>((Expression<Func<MessageBody, DateTime>>) (t => t.LastModified));
      if (!string.IsNullOrEmpty(filter))
        source2 = source2.Where<MessageBody>(filter);
      int num = source2.Count<MessageBody>();
      if (skip > 0)
        source2 = source2.Skip<MessageBody>(skip);
      if (take > 0)
        source2 = source2.Take<MessageBody>(take);
      List<MessageBodyViewModel> items = new List<MessageBodyViewModel>();
      foreach (MessageBody source3 in (IEnumerable<MessageBody>) source2)
      {
        MessageBodyViewModel target = new MessageBodyViewModel();
        this.CopyToViewModel(source3, target);
        items.Add(target);
      }
      ServiceUtility.DisableCache();
      return new CollectionContext<MessageBodyViewModel>((IEnumerable<MessageBodyViewModel>) items)
      {
        TotalCount = num
      };
    }

    private bool DeleteTemplateInternal(string templateId, string provider)
    {
      NewslettersManager manager = NewslettersManager.GetManager(provider);
      manager.DeleteMessageBody(new Guid(templateId));
      manager.SaveChanges();
      ServiceUtility.DisableCache();
      return true;
    }

    private void CopyToMessageBody(MessageBodyViewModel source, MessageBody target)
    {
      target.BodyText = source.BodyText;
      target.MessageBodyType = source.MessageBodyType;
      target.Name = source.Name;
      target.InternalPageTemplateId = source.InternalPageTemplateId;
    }

    private void CopyToViewModel(MessageBody source, MessageBodyViewModel target)
    {
      target.Id = source.Id;
      target.BodyText = source.BodyText;
      target.MessageBodyType = source.MessageBodyType;
      target.InternalPageTemplateId = source.InternalPageTemplateId;
      if (target.InternalPageTemplateId != Guid.Empty)
        target.InternalPageTemplate = new WcfPageTemplate(PageManager.GetManager().GetTemplate(target.InternalPageTemplateId));
      target.Name = source.Name;
      switch (source.MessageBodyType)
      {
        case MessageBodyType.PlainText:
          target.TemplateTypeUX = Res.Get<NewslettersResources>().PlainTextTemplate;
          break;
        case MessageBodyType.HtmlText:
          target.TemplateTypeUX = Res.Get<NewslettersResources>().HtmlTemplate;
          break;
        case MessageBodyType.InternalPage:
          target.TemplateTypeUX = Res.Get<NewslettersResources>().StandardTemplate;
          break;
        default:
          throw new NotSupportedException();
      }
    }

    private bool BatchDeleteTemplatesInternal(string[] templateIds, string provider)
    {
      NewslettersManager manager = NewslettersManager.GetManager(provider);
      foreach (string templateId in templateIds)
        manager.DeleteMessageBody(new Guid(templateId));
      manager.SaveChanges();
      return true;
    }

    private MessageBodyViewModel GetTemplateInternal(
      string templateId,
      string provider)
    {
      ServiceUtility.RequestBackendUserAuthentication();
      MessageBody messageBody = NewslettersManager.GetManager(provider).GetMessageBody(Telerik.Sitefinity.Utilities.Utility.StringToGuid(templateId));
      MessageBodyViewModel target = new MessageBodyViewModel();
      this.CopyToViewModel(messageBody, target);
      ServiceUtility.DisableCache();
      return target;
    }
  }
}
