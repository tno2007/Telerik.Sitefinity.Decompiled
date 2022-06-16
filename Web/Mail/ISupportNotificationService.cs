// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.Mail.ISupportNotificationService
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using Telerik.Sitefinity.Services.Notifications;
using Telerik.Sitefinity.Services.Notifications.Configuration;

namespace Telerik.Sitefinity.Web.Mail
{
  /// <summary>
  /// Common interface for supporting the Notification Service functionality
  /// </summary>
  public interface ISupportNotificationService
  {
    /// <summary>
    /// Gets or sets the name of the <see cref="T:Telerik.Sitefinity.Services.Notifications.Configuration.ISenderProfile" /> that will be used for sending the Emails
    /// </summary>
    string SenderProfileName { get; set; }

    /// <summary>
    /// Gets or sets the <see cref="T:Telerik.Sitefinity.Services.Notifications.Configuration.ISenderProfile" /> that will be used for sending the Emails
    /// </summary>
    ISenderProfile SenderProfile { get; set; }

    /// <summary>
    /// Gets or sets the <see cref="T:Telerik.Sitefinity.Services.Notifications.ServiceContext" /> that will be used for sending the Emails
    /// </summary>
    ServiceContext Context { get; set; }
  }
}
