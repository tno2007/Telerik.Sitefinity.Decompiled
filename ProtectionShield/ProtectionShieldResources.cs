// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.ProtectionShield.ProtectionShieldResources
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Diagnostics.CodeAnalysis;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Localization.Data;

namespace Telerik.Sitefinity.ProtectionShield
{
  /// <summary>
  /// Represents string resources for user interface of the shield protection module.
  /// </summary>
  [ObjectInfo("ProtectionShieldResources", ResourceClassId = "ProtectionShieldResources", TitlePlural = "ProtectionShieldResourcesTitlePlural")]
  internal class ProtectionShieldResources : Resource
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.ProtectionShield.ProtectionShieldResources" /> class.
    /// </summary>
    public ProtectionShieldResources()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.ProtectionShield.ProtectionShieldResources" /> class with the provided <see cref="T:Telerik.Sitefinity.Localization.Data.ResourceDataProvider" />.
    /// </summary>
    /// <param name="dataProvider">The data provider.</param>
    public ProtectionShieldResources(ResourceDataProvider dataProvider)
      : base(dataProvider)
    {
    }

    /// <summary>Gets phrase: Site Name</summary>
    [ResourceEntry("SiteNameLabel", Description = "Phrase: Site Name", LastModified = "2016/10/11", Value = "Site Name")]
    public string SiteNameLabel => this[nameof (SiteNameLabel)];

    /// <summary>
    /// Gets phrase: Enable Protection shield for specified site name
    /// </summary>
    [ResourceEntry("SiteNameDescription", Description = "Phrase: Enable Protection shield for specified site name.", LastModified = "2016/10/11", Value = "Enable Protection shield for specified site name")]
    public string SiteNameDescription => this[nameof (SiteNameDescription)];

    /// <summary>Gets phrase: Site shield</summary>
    [ResourceEntry("ProtectionShieldResourcesTitlePlural", Description = "phrase: Site shield", LastModified = "2017/02/08", Value = "Site shield")]
    public string ProtectionShieldResourcesTitlePlural => this[nameof (ProtectionShieldResourcesTitlePlural)];

    /// <summary>Gets phrase: Protection shield resources</summary>
    [ResourceEntry("ProtectionShieldResourcesTitle", Description = "phrase: Protection shield resources", LastModified = "2017/01/24", Value = "Protection shield resources")]
    public string ProtectionShieldResourcesTitle => this[nameof (ProtectionShieldResourcesTitle)];

    /// <summary>Gets phrase: Shield</summary>
    [ResourceEntry("ProtectionShieldUrlName", Description = "phrase: Shield", LastModified = "2016/10/11", Value = "Shield")]
    public string ProtectionShieldUrlName => this[nameof (ProtectionShieldUrlName)];

    /// <summary>
    /// Gets the Site shield settings configuration description.
    /// </summary>
    [ResourceEntry("ProtectionShieldDescription", Description = "The description of this class.", LastModified = "2017/02/08", Value = "Protect your website while it is under development and allow only trusted users to preview it for evaluation purposes")]
    public string ProtectionShieldDescription => this[nameof (ProtectionShieldDescription)];

    /// <summary>Gets phrase: Site Shield settings</summary>
    [ResourceEntry("ProtectionShieldSettings", Description = "Phrase: Site Shield settings", LastModified = "2016/10/11", Value = "Site Shield settings")]
    public string ProtectionShieldSettings => this[nameof (ProtectionShieldSettings)];

    /// <summary>Gets phrase: Who has access to this site</summary>
    [ResourceEntry("WhoHasAccessToThisSiteLabel", Description = "Phrase: Who has access to this site", LastModified = "2017/02/08", Value = "Who has access to this site")]
    public string WhoHasAccessToThisSiteLabel => this[nameof (WhoHasAccessToThisSiteLabel)];

    /// <summary>Gets phrase: Deactivate shield?</summary>
    [ResourceEntry("DeactivateShield", Description = "Phrase: Deactivate shield?", LastModified = "2016/10/11", Value = "Deactivate shield?")]
    public string DeactivateShield => this[nameof (DeactivateShield)];

    /// <summary>Gets phrase: Activate for this site</summary>
    /// <value>Activate for this site</value>
    [ResourceEntry("ActivateForThisSiteMessage", Description = "Phrase: Activate for this site", LastModified = "2016/10/11", Value = "Activate for this site")]
    public string ActivateForThisSiteMessage => this[nameof (ActivateForThisSiteMessage)];

    /// <summary>Gets phrase: Deactivate for this site</summary>
    /// <value>Deactivate for this site</value>
    [ResourceEntry("DeactivateForThisSiteMessage", Description = "Phrase: Deactivate for this site", LastModified = "2016/10/11", Value = "Deactivate for this site")]
    public string DeactivateForThisSiteMessage => this[nameof (DeactivateForThisSiteMessage)];

    /// <summary>
    /// Gets phrase: Are you sure you want to deactivate Site Shield for this site?
    /// </summary>
    /// <value>Are you sure you want to deactivate Site Shield for this site?</value>
    [ResourceEntry("ConfirmDeactivationMessage", Description = "Phrase: Are you sure you want to deactivate Site Shield for this site?", LastModified = "2016/10/11", Value = "Are you sure you want to deactivate Site Shield for this site?")]
    public string ConfirmDeactivationMessage => this[nameof (ConfirmDeactivationMessage)];

    /// <summary>Gets phrase: Yes, deactivate</summary>
    /// <value>Yes, deactivate</value>
    [ResourceEntry("ConfirmDeactivationButtonMessage", Description = "Phrase: Yes, deactivate", LastModified = "2016/10/11", Value = "Yes, deactivate")]
    public string ConfirmDeactivationButtonMessage => this[nameof (ConfirmDeactivationButtonMessage)];

    /// <summary>
    /// Gets phrase: The system has not been configured to send emails. Go to <a href="{0}" target="_blank">Email settings</a> or ask your administrator to set them
    /// </summary>
    [ResourceEntry("TheSystemHasNotBeenConfiguredToSendEmails", Description = "Phrase: The system has not been configured to send emails. Go to <a href='{0}' target='_blank'>Email settings</a> or ask your administrator to set them", LastModified = "2019/03/29", Value = "The system has not been configured to send emails. Go to <a href='{0}' target='_blank'>Email settings</a> or ask your administrator to set them.")]
    public string TheSystemHasNotBeenConfiguredToSendEmails => this[nameof (TheSystemHasNotBeenConfiguredToSendEmails)];

    /// <summary>
    /// Gets phrase: The system has not been configured to send emails. Setup sending emails in <a href="{0}" target="_blank">Email settings</a>.
    /// </summary>
    [ResourceEntry("TheSystemHasNotBeenConfiguredToSendEmailsInBasicSettings", Description = "Phrase: The system has not been configured to send emails. Setup sending emails in <a href='{0}' target='_blank'>Email settings</a>.", LastModified = "2019/03/29", Value = "The system has not been configured to send emails. Setup sending emails in <a href='{0}' target='_blank'>Email settings</a>.")]
    public string TheSystemHasNotBeenConfiguredToSendEmailsInBasicSettings => this[nameof (TheSystemHasNotBeenConfiguredToSendEmailsInBasicSettings)];

    /// <summary>
    /// Gets phrase: The system has not been configured to send emails. Go to <a href="{0}" target="_blank">Notifications</a> and configure the '{1}' sender profile.
    /// </summary>
    [ResourceEntry("TheSystemHasNotBeenConfiguredToSendEmailsInAdvancedSettings", Description = "Phrase: The system has not been configured to send emails. Go to <a href='{0}' target='_blank'>Notifications</a> and configure the '{1}' sender profile.", LastModified = "2019/03/29", Value = "The system has not been configured to send emails. Go to <a href='{0}' target='_blank'>Notifications</a> and configure the '{1}' sender profile.")]
    public string TheSystemHasNotBeenConfiguredToSendEmailsInAdvancedSettings => this[nameof (TheSystemHasNotBeenConfiguredToSendEmailsInAdvancedSettings)];

    /// <summary>
    /// Gets phrase: The system has not been configured to send emails. Go to Email settings or ask your administrator to set them
    /// </summary>
    [ResourceEntry("TheSystemHasNotBeenConfiguredToSendEmailsReadOnlyMode", Description = "Phrase: The system has not been configured to send emails. Go to Email settings or ask your administrator to set them", LastModified = "2019/03/29", Value = "The system has not been configured to send emails. Go to Email settings or ask your administrator to set them.")]
    public string TheSystemHasNotBeenConfiguredToSendEmailsReadOnlyMode => this[nameof (TheSystemHasNotBeenConfiguredToSendEmailsReadOnlyMode)];

    /// <summary>Gets phrase: Block access</summary>
    [ResourceEntry("BlockAccessOption", Description = "Phrase: Block access", LastModified = "2016/10/11", Value = "Block access")]
    public string BlockAccessOption => this[nameof (BlockAccessOption)];

    /// <summary>
    /// Gets phrase: Once blocked the user will appear in the list and will not be able to access the website.
    /// </summary>
    [ResourceEntry("BlockAccessDialogWarningMessage", Description = "Phrase: Once blocked the user will appear in the list and will not be able to access the website.", LastModified = "2016/10/11", Value = "Once blocked the user will appear in the list and will not be able to access the website.")]
    public string BlockAccessDialogWarningMessage => this[nameof (BlockAccessDialogWarningMessage)];

    /// <summary>
    /// Gets phrase: Are you sure you want to block the access of this user?
    /// </summary>
    [ResourceEntry("BlockAccessDialogAreYouSureMessage", Description = "Phrase: Are you sure you want to block the access of this user?", LastModified = "2016/10/11", Value = "Are you sure you want to block the access of this user?")]
    public string BlockAccessDialogAreYouSureMessage => this[nameof (BlockAccessDialogAreYouSureMessage)];

    /// <summary>Gets phrase: Yes, block access</summary>
    [ResourceEntry("BlockAccessDialogConfirmMessage", Description = "Phrase: Yes, block access", LastModified = "2016/10/11", Value = "Yes, block access")]
    public string BlockAccessDialogConfirmMessage => this[nameof (BlockAccessDialogConfirmMessage)];

    /// <summary>Gets phrase: Remove access</summary>
    [ResourceEntry("RemoveAccessOption", Description = "Phrase: Remove access", LastModified = "2016/10/11", Value = "Remove access")]
    public string RemoveAccessOption => this[nameof (RemoveAccessOption)];

    /// <summary>
    /// Gets phrase: Are you sure you want to remove the user?
    /// </summary>
    [ResourceEntry("RemoveAccessDialogAreYouSureMessage", Description = "Phrase: Are you sure you want to remove the user?", LastModified = "2016/10/11", Value = "Are you sure you want to remove the user?")]
    public string RemoveAccessDialogAreYouSureMessage => this[nameof (RemoveAccessDialogAreYouSureMessage)];

    /// <summary>Gets phrase: Yes, remove</summary>
    [ResourceEntry("RemoveAccessDialogConfirmMessage", Description = "Phrase: Yes, remove", LastModified = "2016/10/11", Value = "Yes, remove")]
    public string RemoveAccessDialogConfirmMessage => this[nameof (RemoveAccessDialogConfirmMessage)];

    /// <summary>Gets phrase: Remove</summary>
    [ResourceEntry("RemoveMessage", Description = "Phrase: Remove", LastModified = "2016/10/11", Value = "Remove")]
    public string RemoveMessage => this[nameof (RemoveMessage)];

    /// <summary>Gets phrase: Resend email invitation</summary>
    [ResourceEntry("SendEmailInvitationAgainMessage", Description = "Phrase: Resend email invitation", LastModified = "2017/01/23", Value = "Resend email invitation")]
    public string SendEmailInvitationAgainMessage => this[nameof (SendEmailInvitationAgainMessage)];

    /// <summary>Gets Phrase: Unblock</summary>
    /// <value>Unblock</value>
    [ResourceEntry("UnblockAccessOption", Description = "Phrase: Unblock", LastModified = "2017/01/23", Value = "Unblock")]
    [SuppressMessage("Microsoft.StyleCop.CSharp.DocumentationRules", "SA1630:DocumentationTextMustContainWhitespace", Justification = "Spelling conflicts.")]
    public string UnblockAccessOption => this[nameof (UnblockAccessOption)];

    /// <summary>Gets Phrase: This site can't be reached!</summary>
    /// <value>This site can't be reached!</value>
    [ResourceEntry("SiteCantBeReachedText", Description = "Phrase: This site can't be reached!", LastModified = "2017/01/23", Value = "This site can't be reached!")]
    public string SiteCantBeReachedText => this[nameof (SiteCantBeReachedText)];

    /// <summary>Gets phrase: Allowed</summary>
    [ResourceEntry("AccessStatusAllowed", Description = "Phrase: Allowed", LastModified = "2016/10/11", Value = "Allowed")]
    public string AccessStatusAllowed => this[nameof (AccessStatusAllowed)];

    /// <summary>Gets phrase: Blocked</summary>
    [ResourceEntry("AccessStatusBlocked", Description = "Phrase: Blocked", LastModified = "2016/10/11", Value = "Blocked")]
    public string AccessStatusBlocked => this[nameof (AccessStatusBlocked)];

    /// <summary>Gets phrase: Invitation has not been sent</summary>
    [ResourceEntry("AccessStatusInvitationNotSent", Description = "Phrase: Invitation has not been sent", LastModified = "2016/10/11", Value = "Invitation has not been sent")]
    public string AccessStatusInvitationNotSent => this[nameof (AccessStatusInvitationNotSent)];

    /// <summary>Gets phrase: Sending...</summary>
    [ResourceEntry("AccessStatusSending", Description = "Phrase: Sending...", LastModified = "2016/10/11", Value = "Sending...")]
    public string AccessStatusSending => this[nameof (AccessStatusSending)];

    /// <summary>Gets phrase: Set expiration time</summary>
    [ResourceEntry("SetExpirationTimeLabel", Description = "Phrase: Set expiration time", LastModified = "2016/10/11", Value = "Set expiration time")]
    public string SetExpirationTimeLabel => this[nameof (SetExpirationTimeLabel)];

    /// <summary>Gets phrase: Revoke access for users above on...</summary>
    [ResourceEntry("RemoveAccessForUsersAboveOnLabel", Description = "Phrase: Revoke access for users above on...", LastModified = "2016/10/11", Value = "Revoke access for users above on...")]
    public string RemoveAccessForUsersAboveOnLabel => this[nameof (RemoveAccessForUsersAboveOnLabel)];

    /// <summary>Gets phrase: Email</summary>
    [ResourceEntry("AccessTokenEmailLabel", Description = "Phrase: Email.", LastModified = "2016/10/11", Value = "Email")]
    public string AccessTokenEmailLabel => this[nameof (AccessTokenEmailLabel)];

    /// <summary>Gets phrase: Access</summary>
    [ResourceEntry("AccessTokenAccessTypeLabel", Description = "Phrase: Access.", LastModified = "2016/10/11", Value = "Access")]
    public string AccessTokenAccessTypeLabel => this[nameof (AccessTokenAccessTypeLabel)];

    /// <summary>Gets phrase: Devices</summary>
    [ResourceEntry("AccessTokenDevicesLabel", Description = "Phrase: Devices.", LastModified = "2016/10/11", Value = "Devices")]
    public string AccessTokenDevicesLabel => this[nameof (AccessTokenDevicesLabel)];

    /// <summary>Gets phrase: Allow specific users</summary>
    /// <value>Allow specific users</value>
    [ResourceEntry("SpecificUsersMessage", Description = "Phrase: Allow specific users", LastModified = "2017/02/08", Value = "Allow specific users")]
    public string SpecificUsersMessage => this[nameof (SpecificUsersMessage)];

    /// <summary>
    /// Gets phrase: The users with the specified emails will receive invitations with a link to the websites that contains a unique access key
    /// </summary>
    /// <value>The users with the specified emails will receive invitations with a link to the websites that contains a unique access key</value>
    [ResourceEntry("SendEmailInvitationMessage", Description = "Phrase: The users with the specified emails will receive invitations with a link to the websites that contains a unique access key", LastModified = "2017/02/08", Value = "The users with the specified emails will receive invitations with a link to the websites that contains a unique access key")]
    public string SendEmailInvitationMessage => this[nameof (SendEmailInvitationMessage)];

    /// <summary>Gets phrase: Invite users</summary>
    /// <value>Invite users</value>
    [ResourceEntry("InviteUsersMessage", Description = "Phrase: Invite users", LastModified = "2016/10/11", Value = "Invite users")]
    public string InviteUsersMessage => this[nameof (InviteUsersMessage)];

    /// <summary>Gets phrase: Send email invitations</summary>
    /// <value>Send email invitations</value>
    [ResourceEntry("SendEmailInvitationsPluralTitleMessage", Description = "Phrase: Send email invitations", LastModified = "2016/10/11", Value = "Send email invitations")]
    public string SendEmailInvitationsPluralTitleMessage => this[nameof (SendEmailInvitationsPluralTitleMessage)];

    /// <summary>Gets phrase: Emails</summary>
    /// <value>Emails</value>
    [ResourceEntry("EmailsTitleMessage", Description = "Phrase: Emails", LastModified = "2016/10/11", Value = "Emails")]
    [SuppressMessage("Microsoft.StyleCop.CSharp.DocumentationRules", "SA1630:DocumentationTextMustContainWhitespace", Justification = "Spelling conflicts.")]
    public string EmailsTitleMessage => this[nameof (EmailsTitleMessage)];

    /// <summary>Gets phrase: Send invitation</summary>
    /// <value>Send invitation</value>
    [ResourceEntry("SendInvitationMessage", Description = "Phrase: Send invitation", LastModified = "2016/10/11", Value = "Send invitation")]
    public string SendInvitationMessage => this[nameof (SendInvitationMessage)];

    /// <summary>
    /// Gets phrase: Enter emails separated by comma, semicolon, space or new line.
    /// </summary>
    /// <value>Enter emails separated by comma, semicolon, space or new line.</value>
    [ResourceEntry("SendEmailExampleMessage", Description = "Enter emails separated by comma, semicolon, space or new line.", LastModified = "2017/01/23", Value = "Enter emails separated by comma, semicolon, space or new line.")]
    [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1650:ElementDocumentationMustBeSpelledCorrectly", Justification = "Reviewed.")]
    public string SendEmailExampleMessage => this[nameof (SendEmailExampleMessage)];

    /// <summary>Gets phrase: Access {0}</summary>
    /// <value>Access {0}</value>
    [ResourceEntry("AccessEmailTitle", Description = "Phrase: Access {0}", LastModified = "2016/10/11", Value = "Access {0}")]
    public string AccessEmailTitle => this[nameof (AccessEmailTitle)];

    /// <summary>
    /// Gets phrase: <h2>Access {0}</h2> {0} has limited access to trusted users only. <br /> You ({1}) have been invited to view the website. <br /> <br /> Use the following link: <br /> <a href="{2}" targer="blank">{2}</a> <br /> <br /> Do not share! This link is for personal use only. <br /> <br /> Access expires on: {3}
    /// </summary>
    /// <value><h2>Access {0}</h2> {0} has limited access to trusted users only. <br /> You ({1}) have been invited to view the website. <br /> <br /> Use the following link: <br /> <a href="{2}" targer="blank">{2}</a> <br /> <br /> Do not share! This link is for personal use only. <br /> <br /> Access expires on: {3}</value>
    [ResourceEntry("UseTheFollowingLinkEmailBody", Description = "Phrase: <h2>Access {0}</h2> {0} has limited access to trusted users only. <br/> You ({1}) have been invited to view the website. <br/> <br/> Use the following link: <br/> <a href='{2}' targer='blank'>{2}</a> <br/> <br/> Do not share! This link is for personal use only. <br/> <br/> Access expires on: {3}", LastModified = "2017/02/08", Value = "<h2>Access {0}</h2> {0} has limited access to trusted users only. <br/> You ({1}) have been invited to view the website. <br/> <br/> Use the following link: <br/> <a href='{2}' targer='blank'>{2}</a> <br/> <br/> Do not share! This link is for personal use only. <br/> <br/> Access expires on: {3:dd/MM/yyyy}")]
    public string UseTheFollowingLinkEmailBody => this[nameof (UseTheFollowingLinkEmailBody)];
  }
}
