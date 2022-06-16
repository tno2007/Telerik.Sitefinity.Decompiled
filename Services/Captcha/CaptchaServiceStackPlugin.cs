// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Services.Captcha.CaptchaServiceStackPlugin
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using ServiceStack;
using ServiceStack.Text;
using Telerik.Sitefinity.Modules.Comments.Configuration;
using Telerik.Sitefinity.Services.Captcha.DTO;

namespace Telerik.Sitefinity.Services.Captcha
{
  /// <summary>
  /// Represents a ServiceStack plugin for the captcha web service.
  /// </summary>
  internal class CaptchaServiceStackPlugin : IPlugin
  {
    private const string CommentServiceRoute = "/captcha";

    /// <summary>Adding the captcha service routes</summary>
    /// <param name="appHost">The app host</param>
    public void Register(IAppHost appHost)
    {
      JsConfig.AlwaysUseUtc = Telerik.Sitefinity.Configuration.Config.Get<CommentsModuleConfig>().AlwaysUseUTC;
      appHost.RegisterService(typeof (CaptchaWebService));
      appHost.Routes.Add<CaptchaRequest>("/captcha", "GET").Add<CaptchaInfo>("/captcha", "POST");
    }
  }
}
