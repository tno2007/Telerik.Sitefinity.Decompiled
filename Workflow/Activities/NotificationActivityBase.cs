// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Workflow.Activities.NotificationActivityBase
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Activities;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Web;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.DynamicModules.Builder;
using Telerik.Sitefinity.DynamicModules.Model;
using Telerik.Sitefinity.Fluent.AnyContent;
using Telerik.Sitefinity.Fluent.AnyContent.Implementation;
using Telerik.Sitefinity.Fluent.Pages;
using Telerik.Sitefinity.GenericContent.Model;
using Telerik.Sitefinity.Lifecycle;
using Telerik.Sitefinity.Lifecycle.Fluent;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.Pages.Model;
using Telerik.Sitefinity.Security;
using Telerik.Sitefinity.Security.Model;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Services.Notifications;
using Telerik.Sitefinity.Utilities.TypeConverters;
using Telerik.Sitefinity.Web;

namespace Telerik.Sitefinity.Workflow.Activities
{
  public abstract class NotificationActivityBase : CodeActivity
  {
    /// <summary>Gets or sets the email text.</summary>
    /// <value>The email text.</value>
    [RequiredArgument]
    public string EmailText { get; set; }

    /// <summary>
    /// When implemented in a derived class, performs the execution of the activity.
    /// </summary>
    /// <param name="context">The execution context under which the activity executes.</param>
    protected override void Execute(CodeActivityContext context)
    {
      try
      {
        this.OnExcecuting(context);
        WorkflowDataContext dataContext = context.DataContext;
        IWorkflowExecutionDefinition executionDefinition = ActivitiesHelper.ExecutionDefinitionFromDataContext(context.DataContext);
        Lstring approvalState = (Lstring) null;
        if (executionDefinition != null)
        {
          PropertyDescriptorCollection properties = dataContext.GetProperties();
          if (properties["masterFluent"] != null)
          {
            Type type = properties["workflowItem"].GetValue((object) dataContext).GetType();
            if (SystemManager.IsModuleEnabled("Ecommerce") && TypeResolutionService.ResolveType("Telerik.Sitefinity.Ecommerce.Catalog.Model.Product").IsAssignableFrom(type))
            {
              approvalState = ((properties["masterFluent"].GetValue((object) dataContext) as BaseLifecycleFacade).Get() as IApprovalWorkflowItem).ApprovalWorkflowState;
            }
            else
            {
              object obj = dataContext.GetProperties()["masterFluent"].GetValue((object) dataContext);
              switch (obj)
              {
                case AnyDraftFacade _:
                  approvalState = (((AnyDraftFacade) obj).Get() as IApprovalWorkflowItem).ApprovalWorkflowState;
                  break;
                case MasterFacade _:
                  approvalState = (((BaseLifecycleFacade) obj).Get() as IApprovalWorkflowItem).ApprovalWorkflowState;
                  break;
              }
            }
          }
          else
            approvalState = ((PageFacade) properties["fluent"].GetValue((object) dataContext)).Get().ApprovalWorkflowState;
        }
        this.SendMessage(context, approvalState);
      }
      catch (Exception ex)
      {
      }
    }

    /// <summary>Called when [excecuting].</summary>
    /// <param name="context">The context.</param>
    protected virtual void OnExcecuting(CodeActivityContext context)
    {
    }

    /// <summary>Returns the users who need to be notified.</summary>
    /// <param name="context">The workflow activity context</param>
    /// <returns>Collection of <see cref="T:Telerik.Sitefinity.Security.Model.User" /></returns>
    public virtual IEnumerable<User> GetUsers(CodeActivityContext context) => (IEnumerable<User>) new List<User>();

    /// <summary>
    /// Returns a list of email adressess, which we have to send email to.
    /// If those are users from the database, then it is recommended to
    /// use <see cref="M:Telerik.Sitefinity.Workflow.Activities.NotificationActivityBase.GetUsers(System.Activities.CodeActivityContext)" /> instead, because we might prefer to
    /// also notify the user via another channel like SMS, dashboard...
    /// </summary>
    /// <param name="context">The context.</param>
    /// <returns>List of email addresses.</returns>
    public virtual List<string> GetEmails(CodeActivityContext context) => new List<string>();

    /// <summary>Gets the mail message.</summary>
    /// <param name="toEmails">To emails.</param>
    /// <param name="fromEmails">From emails.</param>
    /// <param name="messageText">The message text.</param>
    /// <returns></returns>
    [Obsolete("The message is sent throgh the notification service")]
    protected virtual MailMessage GetMailMessage(
      IList<string> toEmails,
      string fromEmails,
      string messageText)
    {
      MailMessage mailMessage = new MailMessage();
      foreach (string toEmail in (IEnumerable<string>) toEmails)
      {
        try
        {
          mailMessage.To.Add(new MailAddress(toEmail));
        }
        catch
        {
        }
      }
      mailMessage.From = new MailAddress(fromEmails);
      mailMessage.Body = messageText;
      mailMessage.IsBodyHtml = true;
      mailMessage.BodyEncoding = Encoding.Unicode;
      mailMessage.SubjectEncoding = Encoding.Unicode;
      mailMessage.Subject = "Content is waiting for your action in Sitefinity";
      mailMessage.IsBodyHtml = true;
      return mailMessage;
    }

    private void SendMessage(CodeActivityContext context, Lstring approvalState)
    {
      WorkflowDataContext dataContext = context.DataContext;
      PropertyDescriptorCollection properties = dataContext.GetProperties();
      string str1 = string.Empty;
      string str2 = string.Empty;
      string str3 = string.Empty;
      Guid guid1 = Guid.Empty;
      DateTime sitefinityUiTime = DateTime.UtcNow.ToSitefinityUITime();
      if (!(properties["providerName"].GetValue((object) dataContext) is string empty))
        empty = string.Empty;
      Type type = properties["workflowItem"].GetValue((object) dataContext).GetType();
      IDataItem dataItem = this.GetDataItem(type, dataContext, properties);
      int num = SystemManager.CurrentContext.AppSettings.Multilingual ? 1 : 0;
      string culture = string.Empty;
      if (num != 0)
        culture = SystemManager.CurrentContext.Culture.Name;
      CultureInfo defaultBackendLanguage = SystemManager.CurrentContext.AppSettings.DefaultBackendLanguage;
      Guid id = SystemManager.CurrentContext.CurrentSite.Id;
      if (dataItem != null)
      {
        Type result = (Type) null;
        if (typeof (Content).IsAssignableFrom(type))
        {
          Content content = dataItem as Content;
          str2 = (string) content.Title;
          sitefinityUiTime = content.LastModified.ToSitefinityUITime();
          guid1 = content.LastModifiedBy;
          str3 = this.GetItemEditUrl(dataItem.Id.ToString(), type.FullName, empty, culture, id.ToString());
          str1 = this.GetContentItemTypeLabel(type, defaultBackendLanguage);
        }
        else if (typeof (PageNode).IsAssignableFrom(type))
        {
          PageNode pageNode = dataItem as PageNode;
          str3 = this.GetItemEditUrl(dataItem.Id.ToString(), type.FullName, empty, culture, id.ToString());
          str1 = Res.Get("PageResources", "Page", defaultBackendLanguage);
          str2 = (string) pageNode.Title;
          sitefinityUiTime = pageNode.LastModified.ToSitefinityUITime();
          guid1 = pageNode.Owner;
        }
        else if (typeof (DynamicContent).IsAssignableFrom(type))
        {
          str1 = ModuleBuilderManager.GetManager().GetDynamicModuleType(type).TypeName;
          str3 = this.GetItemEditUrl(dataItem.Id.ToString(), type.FullName, empty, culture, id.ToString());
          DynamicContent dynamicContent = dataItem as DynamicContent;
          str2 = DynamicContentExtensions.GetTitle(dynamicContent);
          sitefinityUiTime = dynamicContent.LastModified.ToSitefinityUITime();
          guid1 = dynamicContent.LastModifiedBy;
        }
        else if (this.TryResolveType("Telerik.Sitefinity.Ecommerce.Catalog.Model.Product", out result) && result.IsAssignableFrom(type))
        {
          str3 = this.GetItemEditUrl(dataItem.Id.ToString(), type.FullName, empty, culture, id.ToString());
          str1 = Res.Get("CatalogResources", "ProductTypeName", defaultBackendLanguage);
          IContent content = (IContent) dataItem;
          str2 = (string) content.Title;
          sitefinityUiTime = content.LastModified.ToSitefinityUITime();
          guid1 = ((ILifecycleDataItemGeneric) dataItem).LastModifiedBy;
        }
      }
      if (string.IsNullOrEmpty(str1))
        str1 = Res.Get("Labels", "Content", defaultBackendLanguage);
      string name = SystemManager.CurrentContext.CurrentSite.Name;
      string liveUrl = SystemManager.CurrentContext.CurrentSite.LiveUrl;
      Uri uri = SystemManager.CurrentContext.CurrentSite.GetUri();
      string str4 = RouteHelper.ResolveUrl("~/Sitefinity?sf_site=" + (object) SystemManager.CurrentContext.CurrentSite.Id, UrlResolveOptions.Absolute);
      IWorkflowItem workflowItem = dataItem as IWorkflowItem;
      Dictionary<string, string> dictionary = dataContext.GetProperties()["contextBag"].GetValue((object) dataContext) as Dictionary<string, string>;
      string str5 = dictionary.ContainsKey("Note") ? dictionary["Note"] : "";
      Guid guid2 = dictionary.ContainsKey("StateChangerId") ? Guid.Parse(dictionary["StateChangerId"]) : Guid.Empty;
      if (workflowItem != null && guid2 == Guid.Empty)
        guid2 = workflowItem.GetCurrentApprovalTrackingRecord().UserId;
      List<string> emails = this.GetEmails(context);
      IEnumerable<User> users = this.GetUsers(context);
      IEnumerable<ISubscriberRequest> subscriberRequests = emails.Select<string, ISubscriberRequest>((Func<string, ISubscriberRequest>) (e => (ISubscriberRequest) new SubscriberRequestProxy()
      {
        Email = e
      })).Concat<ISubscriberRequest>(users.Select<User, ISubscriberRequest>((Func<User, ISubscriberRequest>) (u => SecurityManager.GetSubscriberObject(u))));
      WorkflowNotificationContext context1 = new WorkflowNotificationContext()
      {
        Subscribers = subscriberRequests,
        ApprovalStatus = approvalState == (Lstring) null ? string.Empty : approvalState.Value,
        ApprovalNote = str5,
        ItemTypeLabel = str1,
        ItemPreviewUrl = str3,
        ItemTitle = str2,
        SiteName = name,
        SiteUrl = liveUrl,
        SiteAbsoluteUrl = uri,
        ModificationTime = sitefinityUiTime,
        LastModifierId = guid1,
        LastStateChangerId = guid2,
        LoginUrl = str4,
        ActivityDataContext = dataContext
      };
      ObjectFactory.Resolve<IWorkflowNotifier>().SendNotification(context1);
    }

    private string GetContentItemTypeLabel(Type itemType, CultureInfo culture)
    {
      string empty = string.Empty;
      switch (itemType.FullName)
      {
        case "Telerik.Sitefinity.Blogs.Model.BlogPost":
          empty = Res.Get("BlogResources", "BlogsPostsSingleTypeName", culture);
          break;
        case "Telerik.Sitefinity.Events.Model.Event":
          empty = Res.Get("EventsResources", "EventsSingleTypeName", culture);
          break;
        case "Telerik.Sitefinity.Libraries.Model.Document":
          empty = Res.Get("DocumentsResources", "DocumentSingularItemName", culture);
          break;
        case "Telerik.Sitefinity.Libraries.Model.Image":
          empty = Res.Get("ImagesResources", "ImageSingularItemName", culture);
          break;
        case "Telerik.Sitefinity.Libraries.Model.Video":
          empty = Res.Get("VideosResources", "VideoSingularItemName", culture);
          break;
        case "Telerik.Sitefinity.Lists.Model.ListItem":
          empty = Res.Get("ListsResources", "ListItemsSingleTypeName", culture);
          break;
        case "Telerik.Sitefinity.News.Model.NewsItem":
          empty = Res.Get("NewsResources", "NewsItemsSingleTypeName", culture);
          break;
      }
      if (string.IsNullOrEmpty(empty))
      {
        Type result = (Type) null;
        if (this.TryResolveType("Telerik.Sitefinity.Blogs.Model.BlogPost", out result) && result.IsAssignableFrom(itemType))
          empty = Res.Get("BlogResources", "BlogsPostsSingleTypeName", culture);
        else if (this.TryResolveType("Telerik.Sitefinity.News.Model.NewsItem", out result) && result.IsAssignableFrom(itemType))
          empty = Res.Get("NewsResources", "NewsItemsSingleTypeName", culture);
        else if (this.TryResolveType("Telerik.Sitefinity.Events.Model.Event", out result) && result.IsAssignableFrom(itemType))
          empty = Res.Get("EventsResources", "EventsSingleTypeName", culture);
        else if (this.TryResolveType("Telerik.Sitefinity.Libraries.Model.Video", out result) && result.IsAssignableFrom(itemType))
          empty = Res.Get("VideosResources", "VideoSingularItemName", culture);
        else if (this.TryResolveType("Telerik.Sitefinity.Libraries.Model.Image", out result) && result.IsAssignableFrom(itemType))
          empty = Res.Get("ImagesResources", "ImageSingularItemName", culture);
        else if (this.TryResolveType("Telerik.Sitefinity.Libraries.Model.Document", out result) && result.IsAssignableFrom(itemType))
          empty = Res.Get("DocumentsResources", "DocumentSingularItemName", culture);
        else if (this.TryResolveType("Telerik.Sitefinity.Lists.Model.ListItem", out result) && result.IsAssignableFrom(itemType))
          empty = Res.Get("ListsResources", "ListItemsSingleTypeName", culture);
      }
      return empty;
    }

    private bool TryResolveType(string typeFullName, out Type result)
    {
      result = TypeResolutionService.ResolveType(typeFullName, false);
      return result != (Type) null;
    }

    private IDataItem GetDataItem(
      Type itemType,
      WorkflowDataContext dataContext,
      PropertyDescriptorCollection contextProperties)
    {
      IDataItem dataItem = (IDataItem) null;
      if (typeof (Content).IsAssignableFrom(itemType))
        dataItem = (IDataItem) (contextProperties["masterFluent"].GetValue((object) dataContext) as IAnyDraftFacade).Get();
      else if (typeof (PageNode).IsAssignableFrom(itemType))
        dataItem = (IDataItem) (contextProperties["fluent"].GetValue((object) dataContext) as PageFacade).Get();
      else if (SystemManager.IsModuleEnabled("Ecommerce") && TypeResolutionService.ResolveType("Telerik.Sitefinity.Ecommerce.Catalog.Model.Product").IsAssignableFrom(itemType))
        dataItem = (IDataItem) (contextProperties["masterFluent"].GetValue((object) dataContext) as BaseLifecycleFacade).Get();
      else if (typeof (DynamicContent).IsAssignableFrom(itemType))
        dataItem = (IDataItem) (contextProperties["masterFluent"].GetValue((object) dataContext) as BaseLifecycleFacade).Get();
      return dataItem;
    }

    private string GetItemEditUrl(
      string id,
      string type,
      string providerName,
      string culture,
      string siteId)
    {
      NameValueCollection nvc = new NameValueCollection()
      {
        {
          "sf_item_id",
          id
        },
        {
          "sf_item_type",
          type
        },
        {
          "sf_provider",
          providerName
        },
        {
          "sf_site",
          siteId
        }
      };
      if (!culture.IsNullOrEmpty())
        nvc.Add("sf_culture", culture);
      return RouteHelper.ResolveUrl("~/Sitefinity/Dialog/ContentViewEditDialogStandalone" + this.ToQueryString(nvc), UrlResolveOptions.Absolute);
    }

    private string ToQueryString(NameValueCollection nvc) => "?" + string.Join("&", ((IEnumerable<string>) nvc.AllKeys).SelectMany<string, string, string>((Func<string, IEnumerable<string>>) (key => (IEnumerable<string>) nvc.GetValues(key)), (Func<string, string, string>) ((key, value) => string.Format("{0}={1}", (object) HttpUtility.UrlEncode(key), (object) HttpUtility.UrlEncode(value)))).ToArray<string>());
  }
}
