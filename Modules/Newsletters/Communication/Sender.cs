// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Newsletters.Communication.Sender
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Net;
using System.Net.Mail;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Modules.Newsletters.Configuration;

namespace Telerik.Sitefinity.Modules.Newsletters.Communication
{
  /// <summary>
  /// This class provides functionality for sending the messages through Newsletter module.
  /// </summary>
  public class Sender : IDisposable
  {
    private SmtpClient smtpClient;

    /// <summary>Sends a mail message.</summary>
    /// <param name="message"></param>
    public void SendMessage(MailMessage message)
    {
      if (message == null)
        throw new ArgumentNullException(nameof (message));
      this.GetSmtpClient().Send(message);
    }

    /// <summary>
    /// Gets the configured instance of the <see cref="T:System.Net.Mail.SmtpClient" />.
    /// </summary>
    /// <returns>An instance of the <see cref="T:System.Net.Mail.SmtpClient" />.</returns>
    public SmtpClient GetSmtpClient()
    {
      if (this.smtpClient == null)
      {
        NewslettersConfig moduleConfig = this.GetModuleConfig();
        NewsletterValidator.VerifySmtpSettings(moduleConfig);
        SmtpClient smtpClient = new SmtpClient(moduleConfig.SmtpHost, moduleConfig.SmtpPort);
        if (moduleConfig.UseSmtpAuthentication)
          smtpClient.Credentials = (ICredentialsByHost) new NetworkCredential(moduleConfig.SmtpUsername, moduleConfig.SmtpPassword);
        smtpClient.EnableSsl = moduleConfig.UseSmtpSSL;
        this.smtpClient = smtpClient;
      }
      return this.smtpClient;
    }

    /// <summary>Gets the module configuration</summary>
    /// <returns>An instance of the <see cref="T:Telerik.Sitefinity.Modules.Newsletters.Configuration.NewslettersConfig" /> class.</returns>
    protected NewslettersConfig GetModuleConfig() => Config.Get<NewslettersConfig>();

    /// <summary>
    /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
    /// </summary>
    public void Dispose()
    {
      this.Dispose(true);
      GC.SuppressFinalize((object) this);
    }

    /// <summary>
    /// Releases unmanaged and - optionally - managed resources
    /// </summary>
    /// <param name="disposed"><c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources.</param>
    protected virtual void Dispose(bool disposing)
    {
      if (!disposing)
        return;
      if (this.smtpClient != null)
        this.smtpClient.Dispose();
      this.smtpClient = (SmtpClient) null;
    }
  }
}
