// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Forms.MessageTemplates.FormActionMessageTemplate
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Collections.Generic;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Modules.Forms.Configuration;
using Telerik.Sitefinity.Services.Notifications;
using Telerik.Sitefinity.Services.Notifications.Configuration;

namespace Telerik.Sitefinity.Modules.Forms.MessageTemplates
{
  internal abstract class FormActionMessageTemplate : 
    SitefinityActionMessageTemplate,
    ISenderProfileConfigurable
  {
    private static PlaceholderField[] placeholderFields = new PlaceholderField[12]
    {
      FormActionMessageTemplate.PlaceholderFields.FormProjectName,
      FormActionMessageTemplate.PlaceholderFields.FormHost,
      FormActionMessageTemplate.PlaceholderFields.FormUrl,
      FormActionMessageTemplate.PlaceholderFields.FormSiteUrl,
      FormActionMessageTemplate.PlaceholderFields.FormLogoUrl,
      FormActionMessageTemplate.PlaceholderFields.FormTitle,
      FormActionMessageTemplate.PlaceholderFields.FormEntryUrl,
      FormActionMessageTemplate.PlaceholderFields.FormEntryReferralCode,
      FormActionMessageTemplate.PlaceholderFields.FormUsername,
      FormActionMessageTemplate.PlaceholderFields.FormIpAddress,
      FormActionMessageTemplate.PlaceholderFields.FormSubmittedOn,
      FormActionMessageTemplate.PlaceholderFields.FormFields
    };

    public FormActionMessageTemplate() => this.ModuleName = "Forms";

    /// <inheritdoc />
    public override IEnumerable<PlaceholderField> GetPlaceholderFields() => (IEnumerable<PlaceholderField>) FormActionMessageTemplate.placeholderFields;

    /// <inheritdoc />
    public string GetSenderProfileName() => Config.Get<FormsConfig>()?.Notifications?.SenderProfile;

    internal static class PlaceholderFields
    {
      public static readonly PlaceholderField FormProjectName = new PlaceholderField("Form.ProjectName", "Project name");
      public static readonly PlaceholderField FormHost = new PlaceholderField("Form.Host", "Form host");
      public static readonly PlaceholderField FormUrl = new PlaceholderField("Form.Url", "Form URL");
      public static readonly PlaceholderField FormSiteUrl = new PlaceholderField("Form.SiteUrl", "Site URL");
      public static readonly PlaceholderField FormLogoUrl = new PlaceholderField("Form.LogoUrl", "Logo URL");
      public static readonly PlaceholderField FormTitle = new PlaceholderField("Form.Title", "Form title");
      public static readonly PlaceholderField FormEntryUrl = new PlaceholderField("FormEntry.Url", "Entry URL");
      public static readonly PlaceholderField FormEntryReferralCode = new PlaceholderField("FormEntry.ReferralCode", "Entry referral code");
      public static readonly PlaceholderField FormUsername = new PlaceholderField("Form.Username", "Username");
      public static readonly PlaceholderField FormIpAddress = new PlaceholderField("Form.IpAddress", "IP address");
      public static readonly PlaceholderField FormSubmittedOn = new PlaceholderField("Form.SubmittedOn", "Submitted on");
      public static readonly PlaceholderField FormFields = new PlaceholderField("Form.Fields", "Fields");
    }
  }
}
