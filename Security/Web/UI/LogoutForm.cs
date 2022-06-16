// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Security.Web.UI.LogoutForm
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Web;
using Telerik.Sitefinity.Web.UI;

namespace Telerik.Sitefinity.Security.Web.UI
{
  /// <summary>
  /// Detects the user's authentication state and provides a link to log out of the Web site
  /// or login with another user.
  /// </summary>
  public class LogoutForm : SimpleView
  {
    /// <summary>Specifies the name of the embeded template.</summary>
    public static readonly string LayoutTemplatePathConst = ControlUtilities.ToVppPath("Telerik.Sitefinity.Resources.Templates.Backend.Security.LogoutForm.ascx");

    /// <summary>Gets or sets the URL to navigate after logout.</summary>
    /// <value>The logout URL.</value>
    public virtual string LogoutUrl
    {
      get => (string) this.ViewState[nameof (LogoutUrl)] ?? "~/";
      set => this.ViewState[nameof (LogoutUrl)] = (object) value;
    }

    /// <summary>
    /// Gets the name of the embedded layout template. If the control uses layout template this
    /// property must be overridden to provide the path (key) to an embedded resource file.
    /// </summary>
    protected override string LayoutTemplateName => (string) null;

    /// <summary>
    /// Gets or sets the path of the external template to be used by the control.
    /// </summary>
    /// <value></value>
    public override string LayoutTemplatePath
    {
      get => string.IsNullOrEmpty(base.LayoutTemplatePath) ? LogoutForm.LayoutTemplatePathConst : base.LayoutTemplatePath;
      set => base.LayoutTemplatePath = value;
    }

    /// <summary>Gets the login status control.</summary>
    /// <value>The login status.</value>
    public ITextControl LoginStatus => this.Container.GetControl<ITextControl>(nameof (LoginStatus), true);

    /// <summary>Gets the message holder control.</summary>
    /// <value>The message holder.</value>
    public Control MessageHolder => this.Container.GetControl<Control>(nameof (MessageHolder), true);

    /// <summary>Gets the message control.</summary>
    /// <value>The message.</value>
    public ITextControl Message => this.Container.GetControl<ITextControl>(nameof (Message), true);

    /// <summary>Gets the logout button.</summary>
    /// <value>The logout button.</value>
    public IButtonControl LogoutButton => this.Container.GetControl<IButtonControl>("Logout", true);

    /// <summary>Gets the SwitchUser button.</summary>
    /// <value>The SwitchUser button.</value>
    public IButtonControl SwitchUserButton => this.Container.GetControl<IButtonControl>("SwitchUser", true);

    /// <summary>Gets the ChangePassword button.</summary>
    /// <value>The ChangePassword button.</value>
    public HyperLink ChangePasswordLink => this.Container.GetControl<HyperLink>("ChangePassword", true);

    /// <summary>
    /// Raises the <see cref="E:System.Web.UI.Control.Init" /> event.
    /// </summary>
    /// <param name="e">An <see cref="T:System.EventArgs" /> object that contains the event data.</param>
    protected override void OnInit(EventArgs e)
    {
      base.OnInit(e);
      if (!"DoLogout".Equals((string) this.GetRequestContext().RouteData.Values["View"], StringComparison.OrdinalIgnoreCase))
        return;
      this.DoLogout(this.LogoutUrl);
    }

    /// <summary>
    /// Initializes all controls instantiated in the layout container. This method is called at appropriate time for setting initial values and subscribing for events of layout controls.
    /// </summary>
    /// <param name="viewContainer">The control that will host the current view.</param>
    protected override void InitializeControls(GenericContainer viewContainer)
    {
      Labels labels = Res.Get<Labels>();
      string str = HttpUtility.UrlDecode(this.Page.Request.QueryStringGet(SecurityManager.AuthenticationReturnUrl));
      if (string.IsNullOrEmpty(str))
        this.MessageHolder.Visible = false;
      else if (!RouteHelper.IsAbsoluteUrl(str))
      {
        this.MessageHolder.Visible = true;
        this.Message.Text = labels.AutomaticallyNavigated.Arrange((object) HttpUtility.HtmlEncode(str));
      }
      this.LogoutButton.Command += new CommandEventHandler(this.LogoutButton_Command);
      this.SwitchUserButton.Command += new CommandEventHandler(this.SwitchUserButton_Command);
    }

    private void DoLogout(string navigateUrl)
    {
      SecurityManager.Logout();
      HttpContextBase currentHttpContext = SystemManager.CurrentHttpContext;
      if (string.IsNullOrEmpty(navigateUrl))
        navigateUrl = currentHttpContext.Request.Path;
      if (RouteHelper.IsAbsoluteUrl(navigateUrl))
        return;
      currentHttpContext.Response.Redirect(navigateUrl, true);
    }

    private void SwitchUserButton_Command(object sender, CommandEventArgs e) => this.DoLogout((string) null);

    private void LogoutButton_Command(object sender, CommandEventArgs e) => this.DoLogout(this.LogoutUrl);
  }
}
