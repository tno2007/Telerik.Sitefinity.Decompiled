// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Workflow.Activities.MessageTemplates.WorkflowActionMessageTemplate
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Collections.Generic;
using Telerik.Sitefinity.Pages.Model;
using Telerik.Sitefinity.Services.Notifications;

namespace Telerik.Sitefinity.Workflow.Activities.MessageTemplates
{
  internal abstract class WorkflowActionMessageTemplate : SitefinityActionMessageTemplate
  {
    private static PlaceholderField[] placeholderFields = new PlaceholderField[13]
    {
      WorkflowActionMessageTemplate.PlaceholderFields.ItemType,
      WorkflowActionMessageTemplate.PlaceholderFields.ApprovalStatus,
      WorkflowActionMessageTemplate.PlaceholderFields.SiteName,
      WorkflowActionMessageTemplate.PlaceholderFields.SiteUrl,
      WorkflowActionMessageTemplate.PlaceholderFields.ItemUrl,
      WorkflowActionMessageTemplate.PlaceholderFields.ItemTitle,
      WorkflowActionMessageTemplate.PlaceholderFields.Language,
      WorkflowActionMessageTemplate.PlaceholderFields.UserFullName,
      WorkflowActionMessageTemplate.PlaceholderFields.Message,
      WorkflowActionMessageTemplate.PlaceholderFields.GoToItemLinkText,
      WorkflowActionMessageTemplate.PlaceholderFields.NoteForApprovers,
      WorkflowActionMessageTemplate.PlaceholderFields.Note,
      WorkflowActionMessageTemplate.PlaceholderFields.LogoUrl
    };

    public WorkflowActionMessageTemplate() => this.ModuleName = WorkflowManager.GetManager().ModuleName;

    /// <inheritdoc />
    public override IEnumerable<PlaceholderField> GetPlaceholderFields() => (IEnumerable<PlaceholderField>) WorkflowActionMessageTemplate.placeholderFields;

    public override IMessageTemplateRequest GetDefaultMessageTemplate()
    {
      MessageTemplateRequestProxy defaultMessageTemplate = new MessageTemplateRequestProxy();
      string subject = this.GetSubject();
      defaultMessageTemplate.Subject = subject;
      defaultMessageTemplate.BodyHtml = ControlTemplateResolver.ResolveTemplate("Telerik.Sitefinity.Workflow.Activities.MessageTemplates.WorkflowActionMessageTemplate.htm", typeof (WorkflowActionMessageTemplate).Assembly.FullName).Replace("#subject", subject);
      return (IMessageTemplateRequest) defaultMessageTemplate;
    }

    protected abstract string GetSubject();

    internal static class PlaceholderFields
    {
      public static readonly PlaceholderField ItemType = new PlaceholderField("Workflow.ItemType", "Item type");
      public static readonly PlaceholderField ApprovalStatus = new PlaceholderField("Workflow.ApprovalStatus", "Approval status");
      public static readonly PlaceholderField SiteName = new PlaceholderField("Workflow.SiteName", "Site name");
      public static readonly PlaceholderField SiteUrl = new PlaceholderField("Workflow.SiteUrl", "Site URL");
      public static readonly PlaceholderField ItemUrl = new PlaceholderField("Workflow.ItemUrl", "Item URL");
      public static readonly PlaceholderField ItemTitle = new PlaceholderField("Workflow.ItemTitle", "Item title");
      public static readonly PlaceholderField Language = new PlaceholderField("Workflow.Language", nameof (Language));
      public static readonly PlaceholderField UserFullName = new PlaceholderField("Workflow.UserFullName", "User full name");
      public static readonly PlaceholderField Message = new PlaceholderField("Workflow.Message", nameof (Message));
      public static readonly PlaceholderField GoToItemLinkText = new PlaceholderField("Workflow.GoToItemLinkText", "Go to item link text");
      public static readonly PlaceholderField NoteForApprovers = new PlaceholderField("Workflow.NoteForApprovers", "Note for approvers");
      public static readonly PlaceholderField Note = new PlaceholderField("Workflow.Note", nameof (Note));
      public static readonly PlaceholderField LogoUrl = new PlaceholderField("Workflow.LogoUrl", "Logo URL");
    }
  }
}
