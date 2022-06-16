// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.LoginRouteHandler
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Web;
using System.Web.Routing;
using System.Web.UI;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.Project.Configuration;
using Telerik.Sitefinity.Security.Configuration;
using Telerik.Sitefinity.Security.Web.UI;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Web.UI;
using Telerik.Sitefinity.Web.UI.Templates;

namespace Telerik.Sitefinity.Web
{
  /// <summary>Represents route handler for login page.</summary>
  public class LoginRouteHandler : RouteHandlerBase
  {
    private string view;
    /// <summary>Specifies the embedded login page template name.</summary>
    public const string LoginTemplate = "Telerik.Sitefinity.Resources.Pages.Login.aspx";
    /// <summary>
    /// Specifies the embedded password recovery page template name.
    /// </summary>
    public const string PasswordRecoveryTemplate = "Telerik.Sitefinity.Resources.Pages.PasswordRecovery.aspx";
    /// <summary>
    /// Specifies the embedded change password page template name.
    /// </summary>
    public const string ChangePasswordTemplate = "Telerik.Sitefinity.Resources.Pages.ChangePassword.aspx";
    /// <summary>
    /// Specifies the embedded user registration page template name.
    /// </summary>
    public const string RegisterTemplate = "Telerik.Sitefinity.Resources.Pages.Register.aspx";

    /// <summary>
    /// Defines the page template and resources loaded for the current request.
    /// </summary>
    public override TemplateInfo GetTemplateInfo(RequestContext requestContext)
    {
      this.view = ((string) requestContext.RouteData.Values["View"]).ToUpperInvariant();
      string view = this.view;
      if (!(view == "AJAX") && !(view == "LOGIN") && !(view == "DOLOGOUT"))
      {
        if (!(view == "PASSWORDRECOVERY"))
        {
          if (!(view == "CHANGEPASSWORD"))
          {
            if (!(view == "REGISTER"))
              throw new HttpException(404, "Unknown view for login page.");
            return new TemplateInfo()
            {
              TemplateName = "Telerik.Sitefinity.Resources.Pages.Register.aspx",
              ControlType = this.GetType(),
              ConfigAdditionalKey = this.view
            };
          }
          return new TemplateInfo()
          {
            TemplateName = "Telerik.Sitefinity.Resources.Pages.ChangePassword.aspx",
            ControlType = this.GetType(),
            ConfigAdditionalKey = this.view
          };
        }
        return new TemplateInfo()
        {
          TemplateName = "Telerik.Sitefinity.Resources.Pages.PasswordRecovery.aspx",
          ControlType = this.GetType(),
          ConfigAdditionalKey = this.view
        };
      }
      return new TemplateInfo()
      {
        TemplateName = "Telerik.Sitefinity.Resources.Pages.Login.aspx",
        ControlType = this.GetType(),
        ConfigAdditionalKey = this.view
      };
    }

    /// <summary>
    /// Initializes internal controls. In this method any additional controls can be
    /// instantiated and added to the page controls collection.
    /// </summary>
    /// <param name="handler">A <see cref="T:System.Web.UI.Page" /> that will handle the current HTTP request.</param>
    /// <param name="requestContext">An object that encapsulates information about the request.</param>
    protected override void InitializeHttpHandler(Page handler, RequestContext requestContext)
    {
      BackendRestriction.Current.EndRequestIfForbidden(requestContext.HttpContext);
      if (handler.FindControl("ProjectName") is ITextControl control)
        control.Text = Config.Get<ProjectConfig>().ProjectName;
      string view = this.view;
      if (!(view == "AJAX") && !(view == "LOGIN") && !(view == "DOLOGOUT"))
      {
        if (view == "PASSWORDRECOVERY" || view == "CHANGEPASSWORD")
          return;
        int num = view == "REGISTER" ? 1 : 0;
      }
      else
        LoginRouteHandler.SetLoginLogoutFroms(handler, requestContext, this.view);
    }

    private static void SetLoginLogoutFroms(
      Page handler,
      RequestContext requestContext,
      string view)
    {
      Control control1 = handler.FindControl("LoginForm");
      if (control1 == null)
        throw new TemplateException("Telerik.Sitefinity.Resources.Pages.Login.aspx", "System.Web.UI.PlaceHolder", "LoginForm");
      Control control2 = handler.FindControl("LogoutForm");
      if (control2 == null)
        throw new TemplateException("Telerik.Sitefinity.Resources.Pages.Login.aspx", "System.Web.UI.PlaceHolder", "LogoutForm");
      bool isAuthenticated = requestContext.HttpContext.Request.IsAuthenticated;
      if (!isAuthenticated)
      {
        LoginForm control3 = control1.FindControl("LoginFormControl") as LoginForm;
        LoginConfig loginConfig = Config.Get<LoginConfig>();
        IAppSettings appSettings = SystemManager.CurrentContext.AppSettings;
        if (appSettings.Multilingual && control3.IsBackend() && appSettings.DefaultBackendLanguage != null)
          control3.Page.UICulture = appSettings.DefaultBackendLanguage.Name;
        control3.ShowChangePasswordLink = loginConfig.ShowChangePasswordLinkInLoginForm;
        control3.ShowForgotPasswordLink = loginConfig.ShowForgotPasswordLinkInLoginForm;
        control3.ShowHelpLink = loginConfig.ShowHelpLinkInLoginForm;
        control3.ShowRegisterUserLink = loginConfig.ShowRegisterLinkInLoginForm;
        control3.LoginAction = view.ToUpperInvariant() == "AJAX" ? SuccessfulLoginAction.CloseWindow : SuccessfulLoginAction.Redirect;
      }
      control1.Visible = !isAuthenticated;
      control2.Visible = isAuthenticated;
    }
  }
}
