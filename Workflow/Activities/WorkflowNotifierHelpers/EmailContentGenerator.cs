// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Workflow.Activities.WorkflowNotifierHelpers.EmailContentGenerator
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Globalization;
using Telerik.Sitefinity.Configuration.Web.Services;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Security;
using Telerik.Sitefinity.Security.Model;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Services.Notifications;
using Telerik.Sitefinity.Utilities;
using Telerik.Sitefinity.Web;
using Telerik.Sitefinity.Workflow.Activities.MessageTemplates;

namespace Telerik.Sitefinity.Workflow.Activities.WorkflowNotifierHelpers
{
  /// <summary>
  /// Generates the email content for the workflow notifications using templates with placeholders and models with actual content for the placeholders.
  /// </summary>
  internal class EmailContentGenerator
  {
    private WorkflowNotificationContext context;
    private IDictionary<string, string> contextItems;
    private IActionMessageTemplate actionMessageTemplate;
    private IMessageTemplateRequest messageTemplateRequest;

    public string Subject { get; set; }

    public string BodyHtml { get; set; }

    public string TemplateSenderEmailAddress { get; set; }

    public IDictionary<string, string> ContextItems
    {
      get
      {
        if (this.contextItems == null)
          this.contextItems = this.GetContextItems();
        return this.contextItems;
      }
      set => this.contextItems = value;
    }

    public EmailContentGenerator(WorkflowNotificationContext context)
    {
      this.context = context;
      this.actionMessageTemplate = this.GetActionMessageTemplate();
      this.messageTemplateRequest = SystemEmailsService.ResolveTemplate(this.actionMessageTemplate);
      this.Subject = this.messageTemplateRequest.Subject;
      this.BodyHtml = this.messageTemplateRequest.BodyHtml;
      this.TemplateSenderEmailAddress = this.messageTemplateRequest.TemplateSenderEmailAddress;
    }

    private IDictionary<string, string> GetContextItems()
    {
      CultureInfo defaultBackendLanguage = SystemManager.CurrentContext.AppSettings.DefaultBackendLanguage;
      string userFullName = this.GetUserFullName();
      string currentUiCulture = this.GetCurrentUICulture();
      string messageText = this.GetMessageText();
      string str = string.Format(Res.Get("ApprovalWorkflowResources", "GoToItemLinkText", defaultBackendLanguage), (object) this.context.ItemTypeLabel.ToLower());
      string noteLabelText = this.GetNoteLabelText(defaultBackendLanguage);
      string url = this.context.SiteAbsoluteUrl.ToString().TrimEnd('/') + "/SFRes/Images/Telerik.Sitefinity.Resources/Images.Sitefinity_Primary.png";
      RouteHelper.ResolveUrl(url, UrlResolveOptions.Absolute);
      return (IDictionary<string, string>) new Dictionary<string, string>()
      {
        {
          WorkflowActionMessageTemplate.PlaceholderFields.ItemType.FieldName,
          this.context.ItemTypeLabel
        },
        {
          WorkflowActionMessageTemplate.PlaceholderFields.ApprovalStatus.FieldName,
          this.GetFriendlyApprovalStatus(this.context.ApprovalStatus)
        },
        {
          WorkflowActionMessageTemplate.PlaceholderFields.SiteName.FieldName,
          this.context.SiteName
        },
        {
          WorkflowActionMessageTemplate.PlaceholderFields.SiteUrl.FieldName,
          this.context.SiteUrl
        },
        {
          WorkflowActionMessageTemplate.PlaceholderFields.ItemUrl.FieldName,
          this.context.ItemPreviewUrl
        },
        {
          WorkflowActionMessageTemplate.PlaceholderFields.ItemTitle.FieldName,
          this.context.ItemTitle
        },
        {
          WorkflowActionMessageTemplate.PlaceholderFields.Language.FieldName,
          currentUiCulture.IsNullOrEmpty() ? string.Empty : string.Format("({0})", (object) currentUiCulture)
        },
        {
          WorkflowActionMessageTemplate.PlaceholderFields.UserFullName.FieldName,
          userFullName
        },
        {
          WorkflowActionMessageTemplate.PlaceholderFields.Message.FieldName,
          messageText
        },
        {
          WorkflowActionMessageTemplate.PlaceholderFields.GoToItemLinkText.FieldName,
          str
        },
        {
          WorkflowActionMessageTemplate.PlaceholderFields.NoteForApprovers.FieldName,
          noteLabelText
        },
        {
          WorkflowActionMessageTemplate.PlaceholderFields.Note.FieldName,
          string.IsNullOrWhiteSpace(this.context.ApprovalNote) ? string.Empty : "\"" + this.context.ApprovalNote + "\""
        },
        {
          WorkflowActionMessageTemplate.PlaceholderFields.LogoUrl.FieldName,
          url
        }
      };
    }

    private string GetFriendlyApprovalStatus(string approvalStatus)
    {
      if (approvalStatus == "AwaitingApproval")
        return "approval";
      if (approvalStatus == "AwaitingReview")
        return "review";
      return approvalStatus == "AwaitingPublishing" ? "publishing" : approvalStatus;
    }

    private string GetNoteLabelText(CultureInfo defaultBackendCulture)
    {
      string empty = Res.Get("ApprovalWorkflowResources", "NoteForApproversText", defaultBackendCulture);
      if (this.context.ApprovalStatus.StartsWith("Rejected"))
        empty = Res.Get("ApprovalWorkflowResources", "RejectionReason", defaultBackendCulture);
      if (string.IsNullOrEmpty(this.context.ApprovalNote))
        empty = string.Empty;
      return empty;
    }

    private string GetCurrentUICulture()
    {
      string currentUiCulture = string.Empty;
      if (SystemManager.CurrentContext.AppSettings.Multilingual)
        currentUiCulture = SystemManager.CurrentContext.Culture.Name;
      return currentUiCulture;
    }

    private string GetUserFullName()
    {
      User user = UserManager.FindUser(this.context.LastStateChangerId);
      if (user == null)
        return (string) null;
      SitefinityProfile userProfile = UserProfileManager.GetManager().GetUserProfile(user.Id, typeof (SitefinityProfile).FullName) as SitefinityProfile;
      string userFullName = (string) null;
      if (userProfile != null)
      {
        if (userProfile.FirstName != null)
          userFullName = userProfile.FirstName.ToPascalCase();
        if (userProfile.LastName != null)
          userFullName = (userFullName + " " + userProfile.LastName.ToPascalCase()).Trim();
      }
      if (string.IsNullOrEmpty(userFullName))
        userFullName = user.Email;
      return userFullName;
    }

    private string GetMessageText()
    {
      string userFullName = this.GetUserFullName();
      return Merger.MergeTags(this.GetMessageTemplate(userFullName), (IDictionary<string, string>) new Dictionary<string, string>()
      {
        {
          WorkflowActionMessageTemplate.PlaceholderFields.ItemType.FieldName,
          this.context.ItemTypeLabel.ToLower()
        },
        {
          WorkflowActionMessageTemplate.PlaceholderFields.UserFullName.FieldName,
          userFullName
        }
      });
    }

    private string GetMessageTemplate(string userName)
    {
      string messageTemplate = string.Format(this.GetStringResource("DetailedMessage"), (object) ("{|" + WorkflowActionMessageTemplate.PlaceholderFields.ItemType.FieldName + "|}"), (object) ("{|" + WorkflowActionMessageTemplate.PlaceholderFields.UserFullName.FieldName + "|}"));
      if (string.IsNullOrEmpty(userName))
        messageTemplate = string.Format(this.GetStringResource("DetailedMessageNoUser"), (object) ("{|" + WorkflowActionMessageTemplate.PlaceholderFields.ItemType.FieldName + "|}"));
      return messageTemplate;
    }

    private string GetStringResource(string resourceKeySuffix) => Res.Get("ApprovalWorkflowResources", this.GetStringResourceKey() + resourceKeySuffix, SystemManager.CurrentContext.AppSettings.DefaultBackendLanguage);

    private string GetStringResourceKey()
    {
      if (this.context.ApprovalStatus != null && this.context.ApprovalStatus.Equals("Rejected"))
        return "ItemRejected";
      if (this.context.ApprovalStatus != null && this.context.ApprovalStatus.Equals("RejectedForReview"))
        return "ItemRejectedForReview";
      if (this.context.ApprovalStatus != null && this.context.ApprovalStatus.Equals("RejectedForPublishing"))
        return "ItemRejectedForPublishing";
      if (this.context.ApprovalStatus != null && this.context.ApprovalStatus.Equals("AwaitingPublishing"))
        return "ItemAwaitingPublishing";
      return this.context.ApprovalStatus != null && this.context.ApprovalStatus.Equals("AwaitingReview") ? "ItemAwaitingReview" : "ItemAwaitingApproval";
    }

    private IActionMessageTemplate GetActionMessageTemplate() => this.context.ApprovalStatus != null && this.context.ApprovalStatus.Contains("Rejected") ? (IActionMessageTemplate) new ItemRejectedMessageTemplate() : (IActionMessageTemplate) new ItemAwaitingForActionMessageTemplate();
  }
}
