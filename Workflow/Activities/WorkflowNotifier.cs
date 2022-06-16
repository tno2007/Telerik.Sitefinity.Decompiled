// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Workflow.Activities.WorkflowNotifier
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Collections.Generic;
using System.Linq;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Security.Sanitizers;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Services.Notifications;
using Telerik.Sitefinity.Workflow.Activities.WorkflowNotifierHelpers;

namespace Telerik.Sitefinity.Workflow.Activities
{
  /// <summary>
  /// Default implementation of <see cref="T:Telerik.Sitefinity.Workflow.Activities.IWorkflowNotifier" />
  /// </summary>
  public class WorkflowNotifier : IWorkflowNotifier
  {
    private static IHtmlSanitizer htmlSanitizer = ObjectFactory.Resolve<IHtmlSanitizer>();
    private EmailContentGenerator emailContentGenerator;
    private WorkflowNotificationContext context;

    /// <summary>Sends the default email for workflow progress.</summary>
    /// <param name="context">Context of the current notification.</param>
    public virtual void SendNotification(WorkflowNotificationContext context)
    {
      if (context.Subscribers.Count<ISubscriberRequest>() == 0)
        return;
      this.context = context;
      this.emailContentGenerator = new EmailContentGenerator(context);
      this.BeforeEmailSend();
      this.SendEmail();
    }

    private void BeforeEmailSend()
    {
      string subject = this.emailContentGenerator.Subject;
      string bodyHtml = this.emailContentGenerator.BodyHtml;
      IDictionary<string, string> contextItems = this.emailContentGenerator.ContextItems;
      IEnumerable<ISubscriberRequest> subscribers = this.context.Subscribers;
      this.BeforeEmailSend(this.context, ref subject, ref bodyHtml, ref contextItems, ref subscribers);
      this.emailContentGenerator.Subject = subject;
      this.emailContentGenerator.BodyHtml = bodyHtml;
      this.emailContentGenerator.ContextItems = contextItems;
      this.context.Subscribers = subscribers;
    }

    /// <summary>Called right before workflow email is sent.</summary>
    /// <param name="context">The context of the notification.</param>
    /// <param name="subjectTemplate">The template to be used for email subject.</param>
    /// <param name="bodyTemplate">The template to be used for email body.</param>
    /// <param name="tokens">Dictionary of tokens to be replaced in subject and body templates.</param>
    /// <param name="subscribers">List of subscribers to whom the email will be sent.</param>
    protected virtual void BeforeEmailSend(
      WorkflowNotificationContext context,
      ref string subjectTemplate,
      ref string bodyTemplate,
      ref IDictionary<string, string> tokens,
      ref IEnumerable<ISubscriberRequest> subscribers)
    {
    }

    private void SendEmail()
    {
      MessageTemplateRequestProxy templateRequestProxy = new MessageTemplateRequestProxy()
      {
        Subject = WorkflowNotifier.htmlSanitizer.Sanitize(this.emailContentGenerator.Subject),
        BodyHtml = WorkflowNotifier.htmlSanitizer.Sanitize(this.emailContentGenerator.BodyHtml),
        TemplateSenderEmailAddress = this.emailContentGenerator.TemplateSenderEmailAddress
      };
      MessageJobRequestProxy messageJob = new MessageJobRequestProxy()
      {
        MessageTemplate = (IMessageTemplateRequest) templateRequestProxy,
        Subscribers = this.context.Subscribers
      };
      ServiceContext context = new ServiceContext((string) null, "Workflow");
      SystemManager.GetNotificationService().SendMessage(context, (IMessageJobRequest) messageJob, this.emailContentGenerator.ContextItems);
    }
  }
}
