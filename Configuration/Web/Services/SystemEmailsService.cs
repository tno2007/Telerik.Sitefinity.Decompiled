// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Configuration.Web.Services.SystemEmailsService
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.Text.RegularExpressions;
using Telerik.Sitefinity.Configuration.Web.ViewModels;
using Telerik.Sitefinity.Data.Linq.Dynamic;
using Telerik.Sitefinity.Security;
using Telerik.Sitefinity.Security.Claims;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Services.Notifications;
using Telerik.Sitefinity.Utilities.MS.ServiceModel.Web;
using Telerik.Sitefinity.Web.Services;

namespace Telerik.Sitefinity.Configuration.Web.Services
{
  /// <summary>
  /// Service implementation for the system emails in the basic settings.
  /// </summary>
  [ServiceBehavior(ConcurrencyMode = ConcurrencyMode.Single, IncludeExceptionDetailInFaults = true, InstanceContextMode = InstanceContextMode.Single)]
  [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Required)]
  internal class SystemEmailsService : ISystemEmailsService
  {
    internal const string WebServiceUrl = "Sitefinity/Services/SystemEmails/Settings.svc";

    /// <summary>Endpoint used to return all system email</summary>
    /// <param name="siteId">The site Id</param>
    /// <param name="sort">Sort by</param>
    /// <param name="skip">Skip count</param>
    /// <param name="take">Take count</param>
    /// <param name="filter">Filtering options</param>
    /// <returns>System emails view model</returns>
    public CollectionContext<SystemEmailsViewModel> GetSystemEmails(
      string siteId,
      string sort,
      int skip,
      int take,
      string filter)
    {
      ServiceUtility.RequestBackendUserAuthentication();
      Guid result = Guid.Empty;
      Guid.TryParse(siteId, out result);
      SecurityManager.EnsureNonGlobalAdminHasSiteAccess(result);
      INotificationService notificationService = SystemManager.GetNotificationService();
      IEnumerable<IActionMessageTemplate> source = notificationService.GetSystemMessageTemplates((ServiceContext) null, (QueryParameters) null);
      IEnumerable<IMessageTemplateResponse> templateVariations = notificationService.GetMessageTemplates((ServiceContext) null, (QueryParameters) null);
      int num = source.Count<IActionMessageTemplate>();
      if (!string.IsNullOrEmpty(filter))
        source = (IEnumerable<IActionMessageTemplate>) source.AsQueryable<IActionMessageTemplate>().Where<IActionMessageTemplate>(filter);
      if (!string.IsNullOrEmpty(sort))
        source = (IEnumerable<IActionMessageTemplate>) source.AsQueryable<IActionMessageTemplate>().OrderBy<IActionMessageTemplate>(sort);
      if (skip != 0)
        source = (IEnumerable<IActionMessageTemplate>) source.Skip<IActionMessageTemplate>(skip).ToList<IActionMessageTemplate>();
      if (take != 0)
        source = (IEnumerable<IActionMessageTemplate>) source.Take<IActionMessageTemplate>(take).ToList<IActionMessageTemplate>();
      foreach (IActionMessageTemplate actionMessageTemplate in source)
        actionMessageTemplate.ApplyVariations(("siteid", result.ToString()));
      bool flag = !source.Any<IActionMessageTemplate>((Func<IActionMessageTemplate, bool>) (x => templateVariations.Any<IMessageTemplateResponse>((Func<IMessageTemplateResponse, bool>) (mt => mt.ResolveKey == x.GetKey()))));
      if (!ClaimsManager.GetCurrentIdentity().IsGlobalUser)
        flag = false;
      if (result != Guid.Empty & flag)
      {
        foreach (IActionMessageTemplate actionMessageTemplate in source)
          actionMessageTemplate.ApplyVariations(("siteid", Guid.Empty.ToString()));
      }
      return new CollectionContext<SystemEmailsViewModel>(source.OrderBy<IActionMessageTemplate, string>((Func<IActionMessageTemplate, string>) (x => x.ModuleName)).Select<IActionMessageTemplate, SystemEmailsViewModel>((Func<IActionMessageTemplate, SystemEmailsViewModel>) (x => new SystemEmailsViewModel(x, templateVariations.Where<IMessageTemplateResponse>((Func<IMessageTemplateResponse, bool>) (mt => mt.ResolveKey == x.GetKey())).FirstOrDefault<IMessageTemplateResponse>()))))
      {
        TotalCount = num,
        Context = (IDictionary<string, string>) new Dictionary<string, string>()
        {
          {
            "IsInherited",
            flag.ToString()
          }
        }
      };
    }

    /// <summary>
    /// Endpoint used to save the edited system email template
    /// </summary>
    /// <param name="model">The model</param>
    /// <returns>A value representing success</returns>
    public bool UpdateTemplateVariation(SystemEmailsViewModel model)
    {
      ServiceUtility.RequestBackendUserAuthentication();
      Guid siteId;
      if (SystemEmailsService.TryParseSiteId(model.Key, out siteId))
        SecurityManager.EnsureNonGlobalAdminHasSiteAccess(siteId);
      INotificationService notificationService = SystemManager.GetNotificationService();
      ServiceContext context = new ServiceContext("ThisApplicationKey", model.UsedIn);
      try
      {
        MessageTemplateRequestProxy template = new MessageTemplateRequestProxy()
        {
          BodyHtml = model.BodyHtml,
          Subject = model.Subject,
          ResolveKey = model.Key,
          ModuleName = model.UsedIn,
          TemplateSenderEmailAddress = model.SenderEmailAddress,
          TemplateSenderName = model.SenderName,
          LastModifiedById = new Guid?(SecurityManager.CurrentUserId),
          LastModifiedByProvider = ClaimsManager.GetCurrentIdentity().MembershipProvider
        };
        IMessageTemplateResponse systemMessageTemplate = notificationService.GetSystemMessageTemplate((ServiceContext) null, model.Key);
        if (systemMessageTemplate == null)
          notificationService.CreateMessageTemplate(context, (IMessageTemplateRequest) template);
        else
          notificationService.UpdateMessageTemplate(context, systemMessageTemplate.Id, (IMessageTemplateRequest) template);
      }
      catch (Exception ex)
      {
        throw new WebProtocolException(HttpStatusCode.InternalServerError, ex.Message, ex);
      }
      return true;
    }

    /// <summary>Endpoint used to create the template variations</summary>
    /// <param name="siteId">The site id</param>
    /// <returns>A value representing success</returns>
    public bool CreateTemplateVariations(string siteId)
    {
      ServiceUtility.RequestBackendUserAuthentication();
      Guid result;
      if (!string.IsNullOrEmpty(siteId) && Guid.TryParse(siteId, out result) && result != Guid.Empty)
      {
        SecurityManager.EnsureNonGlobalAdminHasSiteAccess(result);
        INotificationService notificationService = SystemManager.GetNotificationService();
        IEnumerable<IActionMessageTemplate> messageTemplates1 = notificationService.GetSystemMessageTemplates((ServiceContext) null, (QueryParameters) null);
        IEnumerable<IMessageTemplateResponse> messageTemplates2 = notificationService.GetMessageTemplates((ServiceContext) null, (QueryParameters) null);
        foreach (IActionMessageTemplate actionMessageTemplate in messageTemplates1)
        {
          IActionMessageTemplate systemTemplate = actionMessageTemplate;
          ServiceContext context = new ServiceContext("ThisApplicationKey", systemTemplate.ModuleName);
          IMessageTemplateResponse templateResponse = messageTemplates2.FirstOrDefault<IMessageTemplateResponse>((Func<IMessageTemplateResponse, bool>) (mt => mt.ResolveKey == systemTemplate.GetKey()));
          systemTemplate.ApplyVariations(("siteid", siteId));
          if (!messageTemplates2.Any<IMessageTemplateResponse>((Func<IMessageTemplateResponse, bool>) (mt => mt.ResolveKey == systemTemplate.GetKey())))
          {
            IMessageTemplateRequest template = SystemEmailsViewModel.ResolveDefaultMessageTemplate(systemTemplate, out bool _);
            if (templateResponse != null)
            {
              template.BodyHtml = templateResponse.BodyHtml;
              template.Subject = templateResponse.Subject;
              template.TemplateSenderEmailAddress = templateResponse.TemplateSenderEmailAddress;
              template.TemplateSenderName = templateResponse.TemplateSenderName;
            }
            template.ResolveKey = systemTemplate.GetKey();
            template.ModuleName = systemTemplate.ModuleName;
            template.LastModifiedById = new Guid?(SecurityManager.CurrentUserId);
            notificationService.CreateMessageTemplate(context, template);
          }
        }
      }
      return true;
    }

    /// <summary>
    /// Endpoint used to delete all template variations for a specific site
    /// </summary>
    /// <param name="siteId">The site id</param>
    /// <returns>A value representing success</returns>
    public bool DeleteTemplateVariations(string siteId)
    {
      ServiceUtility.RequestBackendUserAuthentication();
      Guid result;
      if (!string.IsNullOrEmpty(siteId) && Guid.TryParse(siteId, out result) && result != Guid.Empty)
      {
        SecurityManager.EnsureNonGlobalAdminHasSiteAccess(result);
        INotificationService notificationService = SystemManager.GetNotificationService();
        IEnumerable<IActionMessageTemplate> messageTemplates1 = notificationService.GetSystemMessageTemplates((ServiceContext) null, (QueryParameters) null);
        IEnumerable<IMessageTemplateResponse> messageTemplates2 = notificationService.GetMessageTemplates((ServiceContext) null, (QueryParameters) null);
        foreach (IActionMessageTemplate actionMessageTemplate in messageTemplates1)
        {
          IActionMessageTemplate systemTemplate = actionMessageTemplate;
          ServiceContext context = new ServiceContext("ThisApplicationKey", systemTemplate.ModuleName);
          systemTemplate.ApplyVariations(("siteid", siteId));
          IMessageTemplateResponse templateResponse = messageTemplates2.FirstOrDefault<IMessageTemplateResponse>((Func<IMessageTemplateResponse, bool>) (mt => mt.ResolveKey == systemTemplate.GetKey()));
          if (templateResponse != null)
            notificationService.DeleteMessageTemplate(context, templateResponse.Id);
        }
      }
      return true;
    }

    /// <summary>Endpoint used to delete a template variation</summary>
    /// <param name="model">The model</param>
    /// <returns>A value representing success</returns>
    public bool DeleteTemplateVariation(SystemEmailsViewModel model)
    {
      ServiceUtility.RequestBackendUserAuthentication();
      Guid siteId;
      if (SystemEmailsService.TryParseSiteId(model.Key, out siteId))
        SecurityManager.EnsureNonGlobalAdminHasSiteAccess(siteId);
      INotificationService notificationService = SystemManager.GetNotificationService();
      ServiceContext context = new ServiceContext("ThisApplicationKey", model.UsedIn);
      try
      {
        IMessageTemplateResponse systemMessageTemplate = notificationService.GetSystemMessageTemplate((ServiceContext) null, model.Key);
        if (systemMessageTemplate != null)
          notificationService.DeleteMessageTemplate(context, systemMessageTemplate.Id);
      }
      catch (Exception ex)
      {
        throw new WebProtocolException(HttpStatusCode.InternalServerError, ex.Message, ex);
      }
      return true;
    }

    internal static IMessageTemplateRequest ResolveTemplate(
      IActionMessageTemplate actionMessageTemplate)
    {
      INotificationService notificationService = SystemManager.GetNotificationService();
      actionMessageTemplate.ApplyVariations(("siteid", SystemManager.CurrentContext.CurrentSite.Id.ToString()));
      IMessageTemplateRequest messageTemplateRequest = (IMessageTemplateRequest) notificationService.GetSystemMessageTemplate((ServiceContext) null, actionMessageTemplate.GetKey());
      if (messageTemplateRequest == null)
      {
        actionMessageTemplate.ApplyVariations(("siteid", (string) null));
        messageTemplateRequest = (IMessageTemplateRequest) notificationService.GetSystemMessageTemplate((ServiceContext) null, actionMessageTemplate.GetKey());
      }
      if (messageTemplateRequest == null)
        messageTemplateRequest = actionMessageTemplate.GetDefaultMessageTemplate();
      return messageTemplateRequest;
    }

    private static bool TryParseSiteId(string key, out Guid siteId)
    {
      siteId = Guid.Empty;
      if (!string.IsNullOrEmpty(key))
      {
        MatchCollection matchCollection = Regex.Matches(key, "{([^{}]*)}");
        if (matchCollection.Count == 5 && Guid.TryParse(matchCollection[3].Value, out siteId))
          return true;
      }
      return false;
    }
  }
}
