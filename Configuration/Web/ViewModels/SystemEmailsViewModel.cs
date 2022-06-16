// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Configuration.Web.ViewModels.SystemEmailsViewModel
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using Telerik.Sitefinity.Modules.UserProfiles;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Services.Notifications;
using Telerik.Sitefinity.Services.Notifications.Configuration;

namespace Telerik.Sitefinity.Configuration.Web.ViewModels
{
  /// <summary>Represents a view model for the system emails.</summary>
  [DataContract]
  public class SystemEmailsViewModel
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Configuration.Web.ViewModels.SystemEmailsViewModel" /> class.
    /// </summary>
    public SystemEmailsViewModel()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Configuration.Web.ViewModels.SystemEmailsViewModel" /> class.
    /// </summary>
    /// <param name="messageTemplate">The <see cref="T:Telerik.Sitefinity.Services.Notifications.IActionMessageTemplate" /> used to initialize the view model</param>
    /// <param name="messageTemplateVariation">The <see cref="T:Telerik.Sitefinity.Services.Notifications.IMessageTemplateResponse" /> used to initialize the view model</param>
    public SystemEmailsViewModel(
      IActionMessageTemplate messageTemplate,
      IMessageTemplateResponse messageTemplateVariation)
    {
      bool senderVerified;
      IMessageTemplateRequest messageTemplateRequest = SystemEmailsViewModel.ResolveDefaultMessageTemplate(messageTemplate, out senderVerified);
      this.Subject = messageTemplateRequest.Subject;
      this.BodyHtml = messageTemplateRequest.BodyHtml;
      this.SenderEmailAddress = messageTemplateRequest.TemplateSenderEmailAddress;
      this.SenderName = messageTemplateRequest.TemplateSenderName;
      this.SenderVerified = senderVerified;
      this.Key = messageTemplate.GetKey();
      this.PlaceholderFields = messageTemplate.GetPlaceholderFields().OrderBy<PlaceholderField, string>((Func<PlaceholderField, string>) (p => p.DisplayName)).Select<PlaceholderField, SystemEmailsPlaceholderViewModel>((Func<PlaceholderField, SystemEmailsPlaceholderViewModel>) (x => new SystemEmailsPlaceholderViewModel(x)));
      this.DynamicPlaceholderFields = messageTemplate.GetDynamicPlaceholderFields().Select<PlaceholderField, SystemEmailsPlaceholderViewModel>((Func<PlaceholderField, SystemEmailsPlaceholderViewModel>) (x => new SystemEmailsPlaceholderViewModel(x)));
      this.UsedIn = messageTemplate.ModuleName;
      if (messageTemplateVariation == null)
        return;
      this.IsModified = true;
      this.Id = messageTemplateVariation.Id;
      DateTime? lastModified = messageTemplateVariation.LastModified;
      ref DateTime? local = ref lastModified;
      string str;
      if (!local.HasValue)
      {
        str = (string) null;
      }
      else
      {
        DateTime dateTime = local.GetValueOrDefault();
        dateTime = dateTime.ToLocalTime();
        str = dateTime.ToString("d MMM, yyyy, hh:mm tt");
      }
      this.LastModified = str;
      this.VariationSubject = messageTemplateVariation.Subject;
      this.VariationBodyHtml = messageTemplateVariation.BodyHtml;
      this.VariationSenderEmailAddress = messageTemplateVariation.TemplateSenderEmailAddress;
      this.VariationSenderName = messageTemplateVariation.TemplateSenderName;
      if (!messageTemplateVariation.LastModifiedById.HasValue)
        return;
      this.LastModifiedBy = UserProfilesHelper.GetUserDisplayName(messageTemplateVariation.LastModifiedById.Value);
    }

    /// <summary>Gets or sets the subject of the system email.</summary>
    [DataMember]
    public string Subject { get; set; }

    /// <summary>Gets or sets the html body of the system email.</summary>
    [DataMember]
    public string BodyHtml { get; set; }

    /// <summary>
    /// Gets or sets the sender email address of the system email.
    /// </summary>
    [DataMember]
    public string SenderEmailAddress { get; set; }

    /// <summary>
    /// Gets or sets the sender email name of the system email.
    /// </summary>
    [DataMember]
    public string SenderName { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the sender is verified correctly.
    /// </summary>
    [DataMember]
    public bool SenderVerified { get; set; }

    /// <summary>Gets or sets the subject of the modified template.</summary>
    [DataMember]
    public string VariationSubject { get; set; }

    /// <summary>Gets or sets the html body of the modified template.</summary>
    [DataMember]
    public string VariationBodyHtml { get; set; }

    /// <summary>
    /// Gets or sets the sender email address of the modified template.
    /// </summary>
    [DataMember]
    public string VariationSenderEmailAddress { get; set; }

    /// <summary>
    /// Gets or sets the sender email name of the modified template.
    /// </summary>
    [DataMember]
    public string VariationSenderName { get; set; }

    /// <summary>
    /// Gets or sets the sender email name of the modified template.
    /// </summary>
    [DataMember]
    public string LastModifiedBy { get; set; }

    /// <summary>Gets or sets the name of the key of the system email.</summary>
    [DataMember]
    public string Key { get; set; }

    /// <summary>Gets or sets the system email placeholder fields.</summary>
    [DataMember]
    public IEnumerable<SystemEmailsPlaceholderViewModel> PlaceholderFields { get; set; }

    /// <summary>Gets or sets the dynamic placeholder fields.</summary>
    [DataMember]
    public IEnumerable<SystemEmailsPlaceholderViewModel> DynamicPlaceholderFields { get; set; }

    /// <summary>
    /// Gets or sets the name of the module that uses the system email.
    /// </summary>
    [DataMember]
    public string UsedIn { get; set; }

    /// <summary>Gets or sets the Id of the system email.</summary>
    [DataMember]
    public Guid Id { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the system email template has been modified.
    /// </summary>
    [DataMember]
    public bool IsModified { get; set; }

    /// <summary>Gets or sets the last modified date</summary>
    [DataMember]
    public string LastModified { get; set; }

    internal static IMessageTemplateRequest ResolveDefaultMessageTemplate(
      IActionMessageTemplate messageTemplate,
      out bool senderVerified)
    {
      senderVerified = false;
      INotificationService notificationService = SystemManager.GetNotificationService();
      IMessageTemplateRequest defaultMessageTemplate = messageTemplate.GetDefaultMessageTemplate();
      ServiceContext context = new ServiceContext("ThisAppName", messageTemplate.ModuleName);
      try
      {
        ISenderProfile senderProfile = (ISenderProfile) null;
        if (messageTemplate is ISenderProfileConfigurable profileConfigurable)
        {
          string senderProfileName = profileConfigurable.GetSenderProfileName();
          if (!senderProfileName.IsNullOrEmpty())
          {
            senderProfile = notificationService.GetSenderProfile(context, senderProfileName);
            senderVerified = true;
          }
        }
        if (senderProfile == null && !senderVerified)
          senderProfile = notificationService.GetDefaultSenderProfile(context, "smtp");
        if (senderProfile != null)
        {
          defaultMessageTemplate.TemplateSenderEmailAddress = senderProfile.CustomProperties["defaultSenderEmailAddress"];
          defaultMessageTemplate.TemplateSenderName = senderProfile.CustomProperties["defaultSenderName"];
          if (notificationService is ISenderProfileVerifiable profileVerifiable)
            senderVerified = profileVerifiable.VerifySenderProfile(senderProfile.ProfileName, out string _);
        }
      }
      catch
      {
      }
      return defaultMessageTemplate;
    }
  }
}
