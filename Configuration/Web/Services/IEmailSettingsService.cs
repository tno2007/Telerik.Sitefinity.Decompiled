// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Configuration.Web.Services.IEmailSettingsService
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.ServiceModel;
using System.ServiceModel.Web;
using Telerik.Sitefinity.Configuration.Web.ViewModels;

namespace Telerik.Sitefinity.Configuration.Web.Services
{
  /// <summary>
  /// Interface that defines the members of the service for email settings.
  /// </summary>
  [ServiceContract]
  public interface IEmailSettingsService
  {
    /// <summary>Endpoint used to send an email</summary>
    /// <param name="testEmailViewModel">The <see cref="T:Telerik.Sitefinity.Configuration.Web.ViewModels.TestEmailViewModel" /> object</param>
    /// <returns>Operation message</returns>
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, UriTemplate = "/sendmail/")]
    [OperationContract]
    string SendTestMail(TestEmailViewModel testEmailViewModel);

    /// <summary>
    /// Endpoint used to test the POP3 server with the provided settings
    /// </summary>
    /// <param name="settings">The POP3 settings <see cref="T:Telerik.Sitefinity.Configuration.Web.ViewModels.Pop3SettingsViewModel" /></param>
    /// <returns>Operation message</returns>
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, UriTemplate = "/pop3test/")]
    [OperationContract]
    string TestPop3Server(Pop3SettingsViewModel settings);
  }
}
