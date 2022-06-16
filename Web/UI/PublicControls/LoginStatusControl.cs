// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.PublicControls.LoginStatusControl
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using Microsoft.Owin.Security;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Security;
using Telerik.Sitefinity.Security.Claims;
using Telerik.Sitefinity.Security.Configuration;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Web.Events;
using Telerik.Sitefinity.Web.UI.PublicControls.Attributes;

namespace Telerik.Sitefinity.Web.UI.PublicControls
{
  /// <summary>
  /// Mimics <see cref="T:System.Web.UI.WebControls.LoginStatus" />
  /// </summary>
  /// 
  ///             IndexRenderModeAttribute
  public class LoginStatusControl : SimpleView
  {
    private bool? isLoggedIn;
    private const string ChallengeParameter = "challenge";
    private const string ShowLoginNameKey = "ShowLoginName";
    private const string LoginNameFormatStringKey = "LoginNameFormatString";
    private const string ClientIdKey = "ClientId";

    /// <summary>
    /// Default constructor. Sets the visibility of the login name to false.
    /// </summary>
    public LoginStatusControl()
    {
      this.ShowLoginName = false;
      this.LoginNameFormatString = "{UserName}";
    }

    protected override void OnInit(EventArgs e) => this.Visible = this.GetIndexRenderMode() == IndexRenderModes.Normal;

    /// <summary>Initializes the controls.</summary>
    /// <param name="container"></param>
    /// <remarks>
    /// Initialize your controls in this method. Do not override CreateChildControls method.
    /// </remarks>
    protected override void InitializeControls(GenericContainer container)
    {
      if (!SystemManager.IsDesignMode || SystemManager.IsInlineEditingMode)
      {
        HttpRequestBase request = SystemManager.CurrentHttpContext.Request;
        string str = request.QueryStringGet("challenge");
        string returnUrl = request.QueryStringGet(SecurityManager.AuthenticationReturnUrl);
        bool result;
        if (((str.IsNullOrEmpty() ? 0 : (bool.TryParse(str, out result) ? 1 : 0)) & (result ? 1 : 0)) != 0)
        {
          IAuthenticationManager authentication = this.Page.Request.GetOwinContext().Authentication;
          AuthenticationProperties properties = new AuthenticationProperties();
          properties.RedirectUri = ClaimsManager.CurrentAuthenticationModule.GetRealm();
          string[] strArray = Array.Empty<string>();
          authentication.Challenge(properties, strArray);
        }
        else
        {
          SFClaimsAuthenticationManager.ProcessRejectedUserClearQueryString(SystemManager.CurrentHttpContext, returnUrl);
          this.StatusButton.Click += new EventHandler(this.StatusButton_Click);
          if (!this.ShowLoginName || !SystemManager.CurrentHttpContext.User.Identity.IsAuthenticated)
            return;
          this.LoginNameControl.Visible = true;
          this.LoginNameControl.FormatString = this.LoginNameFormatString;
        }
      }
      else
        ((Control) this.StatusButton).Visible = false;
    }

    /// <summary>
    /// Returns whether the current identity is of a logged-in user
    /// </summary>
    protected bool IsLoggedIn
    {
      get
      {
        if (!this.isLoggedIn.HasValue)
        {
          SitefinityIdentity currentIdentity = ClaimsManager.GetCurrentIdentity();
          this.isLoggedIn = currentIdentity == null ? new bool?(false) : new bool?(currentIdentity.IsAuthenticated);
        }
        return this.isLoggedIn.Value;
      }
    }

    /// <summary>
    /// Holds the login page to be redirected, when clicking Log in
    /// </summary>
    public string LoginUrl { get; set; }

    /// <summary>
    /// Indicates what should be the logout action, when clicking Log out
    /// </summary>
    public LogoutAction LogoutAction { get; set; }

    /// <summary>
    /// Holds the url to be redirected, when clicking Log out and the LogoutAction is set to Redirect
    /// </summary>
    public string LogoutUrl { get; set; }

    /// <summary>Indicates whether to show the login name.</summary>
    public bool ShowLoginName { get; set; }

    /// <summary>Gets or sets the login name format.</summary>
    public string LoginNameFormatString { get; set; }

    /// <summary>Raised when the cookie has been deleted</summary>
    public event EventHandler LoggedOut;

    /// <summary>
    /// Raised before the user's cookie is deleted, allowing the user to cancel the action
    /// </summary>
    public event LoginCancelEventHandler LoggingOut;

    /// <summary>
    /// This method is used by the CacheSubstitutionWrapper delegate and is used when the page
    /// uses OutputCache to render the correct markup
    /// </summary>
    internal static string RenderCacheSubstitutionMarkup(Dictionary<string, string> parameters)
    {
      bool flag = false;
      SitefinityIdentity currentIdentity = ClaimsManager.GetCurrentIdentity();
      if (currentIdentity != null)
        flag = currentIdentity.IsAuthenticated;
      StringBuilder stringBuilder = new StringBuilder();
      if (flag)
      {
        if (bool.Parse(parameters["ShowLoginName"]))
        {
          string str = LoginNameControl.FormatLoginName(parameters["LoginNameFormatString"]);
          stringBuilder.Append(str);
        }
        if (SystemManager.IsInlineEditingMode)
          stringBuilder.AppendFormat(" <a href=\"javascript:__doPostBack('{0}','')\" data-sf-permlink=\"true\" >{1}</a>", (object) parameters["ClientId"].ToString(), (object) Res.Get<PublicControlsResources>().LogOutText);
        else
          stringBuilder.AppendFormat(" <a href=\"javascript:__doPostBack('{0}','')\" >{1}</a>", (object) parameters["ClientId"].ToString(), (object) Res.Get<PublicControlsResources>().LogOutText);
      }
      else
        stringBuilder.AppendFormat(" <a href=\"javascript:__doPostBack('{0}','')\" >{1}</a>", (object) parameters["ClientId"].ToString(), (object) Res.Get<PublicControlsResources>().LogInText);
      return stringBuilder.ToString();
    }

    /// <summary>
    /// Renders the contents of the control to the specified writer. This method is used primarily by control developers.
    /// </summary>
    /// <param name="writer">A <see cref="T:System.Web.UI.HtmlTextWriter" /> that represents the output stream to render HTML content on the client.</param>
    protected override void RenderContents(HtmlTextWriter writer)
    {
      if (!SystemManager.IsDesignMode || SystemManager.IsInlineEditingMode)
        this.DoPostCacheSubstitution();
      else
        writer.WriteEncodedText(Res.Get<PublicControlsResources>().ControlCannotBeRenderedInDesignMode.Arrange((object) Res.Get<PublicControlsResources>().LoginStatusControlTitle));
    }

    /// <summary>Renders the specified writer.</summary>
    /// <param name="writer">The writer.</param>
    protected override void Render(HtmlTextWriter writer)
    {
      if (this.GetIndexRenderMode() != IndexRenderModes.Normal)
        return;
      base.Render(writer);
    }

    /// <summary>
    /// When OutputCache is used on the page, we substitute it with the CacheSubstitutionWrapper
    /// </summary>
    private void DoPostCacheSubstitution()
    {
      Dictionary<string, string> parameters = new Dictionary<string, string>();
      parameters.Add("ShowLoginName", this.ShowLoginName.ToString());
      parameters.Add("LoginNameFormatString", this.LoginNameFormatString);
      parameters.Add("ClientId", ((Control) this.StatusButton).UniqueID);
      this.Page.ClientScript.RegisterForEventValidation(((Control) this.StatusButton).UniqueID);
      new CacheSubstitutionWrapper(parameters, new CacheSubstitutionWrapper.RenderMarkupDelegate(LoginStatusControl.RenderCacheSubstitutionMarkup)).RegisterPostCacheCallBack(this.Context);
    }

    /// <summary>
    /// Gets the <see cref="T:System.Web.UI.HtmlTextWriterTag" /> value that corresponds to this Web server control. This property is used primarily by control developers.
    /// </summary>
    /// <value></value>
    /// <returns>One of the <see cref="T:System.Web.UI.HtmlTextWriterTag" /> enumeration values.</returns>
    protected override HtmlTextWriterTag TagKey => HtmlTextWriterTag.Div;

    private void StatusButton_Click(object sender, EventArgs e)
    {
      if (this.IsLoggedIn)
        this.LogOutUser();
      else
        this.LogInUser();
    }

    /// <summary>
    /// This method holds the logic to logout the user, after clicking Log out.
    /// </summary>
    private void LogOutUser()
    {
      if (this.RaiseLoggingOut().Cancel)
        return;
      bool inlineEditingMode = SystemManager.IsInlineEditingMode;
      bool flag = SecurityManager.AuthenticationMode == AuthenticationMode.Claims;
      this.RaiseLoggedOut();
      string str = string.Empty;
      bool endResponse = false;
      IRedirectUriValidator redirectUriValidator = ObjectFactory.Resolve<IRedirectUriValidator>();
      switch (this.LogoutAction)
      {
        case LogoutAction.Refresh:
          if (flag)
          {
            str = this.Page.Request.GetOwinContext().Request.Uri.ToString();
            if (redirectUriValidator.IsValid(str))
              break;
          }
          if (this.Page.Form == null || !string.Equals(this.Page.Form.Method, "get", StringComparison.OrdinalIgnoreCase))
          {
            str = this.Page.Request.RawUrl;
            endResponse = inlineEditingMode;
            if (redirectUriValidator.IsValid(str))
              break;
          }
          str = this.ResolveUrl(this.Page.Request.AppRelativeCurrentExecutionFilePath);
          break;
        case LogoutAction.Redirect:
          if (!string.IsNullOrEmpty(this.LogoutUrl))
          {
            if (flag)
            {
              str = RouteHelper.ResolveUrl(this.LogoutUrl, UrlResolveOptions.Absolute);
              if (redirectUriValidator.IsValid(str))
                break;
            }
            str = this.LogoutUrl;
            endResponse = true;
            if (redirectUriValidator.IsValid(str))
              break;
          }
          str = UrlPath.ResolveUrl("~/", true);
          break;
        case LogoutAction.RedirectToLoginPage:
          if (flag)
          {
            if (!string.IsNullOrEmpty(this.LoginUrl))
            {
              str = RouteHelper.ResolveUrl(this.LoginUrl, UrlResolveOptions.Absolute);
              if (redirectUriValidator.IsValid(str))
                break;
            }
            string frontEndLoginPageUrl = LoginStatusControl.GetFrontEndLoginPageUrl();
            if (!string.IsNullOrEmpty(frontEndLoginPageUrl))
            {
              str = RouteHelper.ResolveUrl(frontEndLoginPageUrl, UrlResolveOptions.Absolute);
              if (redirectUriValidator.IsValid(str))
                break;
            }
            str = new UriBuilder(this.Page.Request.Url.ToString())
            {
              Query = ("challenge=" + true.ToString())
            }.ToString();
            if (redirectUriValidator.IsValid(str))
              break;
          }
          if (!redirectUriValidator.IsValid(str))
          {
            str = this.ResolveClientUrl(Config.Get<SecurityConfig>().Permissions["Backend"].LoginUrl);
            endResponse = true;
            break;
          }
          break;
        default:
          throw new NotImplementedException();
      }
      if (inlineEditingMode)
        str = str.Replace("/Action/InEdit", string.Empty);
      if (!redirectUriValidator.IsValid(str))
        return;
      if (flag)
      {
        string[] array = ClaimsManager.CurrentAuthenticationModule.GetSignOutAuthenticationTypes().ToArray();
        IAuthenticationManager authentication = this.Page.Request.GetOwinContext().Authentication;
        AuthenticationProperties properties = new AuthenticationProperties();
        properties.RedirectUri = str;
        string[] strArray = array;
        authentication.SignOut(properties, strArray);
      }
      else
      {
        SecurityManager.Logout();
        if (str.IsNullOrWhitespace())
          return;
        SystemManager.CurrentHttpContext.Response.Redirect(str, endResponse);
        SystemManager.CurrentHttpContext.ApplicationInstance.CompleteRequest();
      }
    }

    /// <summary>
    /// Holds the logic to Log in the user, after clicking Log in.
    /// </summary>
    private void LogInUser()
    {
      IRedirectUriValidator redirectUriValidator = ObjectFactory.Resolve<IRedirectUriValidator>();
      string loginUrl = this.LoginUrl;
      if (string.IsNullOrEmpty(loginUrl) || !redirectUriValidator.IsValid(loginUrl))
      {
        string frontEndLoginPageUrl = LoginStatusControl.GetFrontEndLoginPageUrl();
        if (!string.IsNullOrEmpty(frontEndLoginPageUrl))
        {
          SystemManager.CurrentHttpContext.Response.Redirect(RouteHelper.ResolveUrl(frontEndLoginPageUrl, UrlResolveOptions.Absolute), true);
        }
        else
        {
          int authenticationMode = (int) SecurityManager.AuthenticationMode;
          string rawUrl = this.Context.Request.RawUrl;
          if (authenticationMode == 0)
          {
            AuthenticationProperties properties = new AuthenticationProperties()
            {
              RedirectUri = RouteHelper.ResolveUrl(rawUrl, UrlResolveOptions.Absolute)
            };
            this.Page.Request.GetOwinContext().Authentication.Challenge(properties);
          }
          else
            SystemManager.CurrentHttpContext.Response.Redirect(this.ResolveClientUrl(Config.Get<SecurityConfig>().Permissions["Backend"].LoginUrl) + "?" + SecurityManager.AuthenticationReturnUrl + "=" + HttpUtility.UrlEncode(rawUrl), true);
        }
      }
      else
        SystemManager.CurrentHttpContext.Response.Redirect(loginUrl, true);
    }

    private void RaiseLoggedOut()
    {
      if (this.LoggedOut == null)
        return;
      this.LoggedOut((object) this, EventArgs.Empty);
    }

    private LoginCancelEventArgs RaiseLoggingOut()
    {
      LoginCancelEventArgs e = new LoginCancelEventArgs();
      if (this.LoggingOut != null)
        this.LoggingOut((object) this, e);
      return e;
    }

    private static string GetFrontEndLoginPageUrl()
    {
      PageSiteNode actualCurrentNode = SiteMapBase.GetActualCurrentNode();
      RedirectStrategyType redirectStrategy = RedirectStrategyType.None;
      string empty = string.Empty;
      return RouteHelper.GetFrontEndLogin(SystemManager.CurrentHttpContext, out redirectStrategy, actualCurrentNode?.Provider);
    }

    /// <summary>Gets the name of the embedded layout template.</summary>
    /// <value></value>
    /// <remarks>
    /// Override this property to change the embedded template to be used with the dialog
    /// </remarks>
    protected override string LayoutTemplateName => !this.IsLoggedIn ? this.LoggetOutLayoutTemplateName : this.LoggedInLayoutTemplateName;

    /// <summary>
    /// Gets or sets the path of the external template to be used by the control.
    /// </summary>
    /// <value></value>
    [Browsable(false)]
    public override string LayoutTemplatePath
    {
      get => !this.IsLoggedIn ? this.LoggedOutLayoutTemplatePath : this.LoggedInLayoutTemplatePath;
      set
      {
      }
    }

    /// <summary>
    /// Gets the name of the embedded layout template when the user is logged in.
    /// </summary>
    /// <value></value>
    /// <remarks>
    /// Override this property to change the embedded template to be used with the dialog
    /// </remarks>
    protected virtual string LoggedInLayoutTemplateName => (string) null;

    /// <summary>
    /// Gets the name of the embedded layout template when the user is logged out.
    /// </summary>
    /// <value></value>
    /// <remarks>
    /// Override this property to change the embedded template to be used with the dialog
    /// </remarks>
    protected virtual string LoggetOutLayoutTemplateName => (string) null;

    /// <summary>
    /// Gets or sets the path of the external template to be used by the control when the user is logged in.
    /// </summary>
    [WebCategory("ControLCategory_Templates")]
    [WebDisplayName("DisplayName_LoggedInLayoutTemplatePath")]
    public virtual string LoggedInLayoutTemplatePath
    {
      get => ControlUtilities.ToVppPath("Telerik.Sitefinity.Resources.Templates.PublicControls.LoginStatus_LoggedIn.ascx");
      set => base.LayoutTemplatePath = value;
    }

    /// <summary>
    /// Gets or sets the path of the external template to be used by the control when the user is logged out.
    /// </summary>
    [WebCategory("ControLCategory_Templates")]
    [WebDisplayName("DisplayName_LoggedOutLayoutTemplatePath")]
    public virtual string LoggedOutLayoutTemplatePath
    {
      get => ControlUtilities.ToVppPath("Telerik.Sitefinity.Resources.Templates.PublicControls.LoginStatus_LoggedOut.ascx");
      set => base.LayoutTemplatePath = value;
    }

    /// <summary>
    /// Reference to the status control in the layout template
    /// </summary>
    protected virtual IButtonControl StatusButton => this.Container.GetControl<IButtonControl>("status", true);

    /// <summary>
    /// Reference to the LoginName control in the layout template
    /// </summary>
    protected virtual LoginNameControl LoginNameControl => this.Container.GetControl<LoginNameControl>("loginNameControl", this.ShowLoginName);
  }
}
