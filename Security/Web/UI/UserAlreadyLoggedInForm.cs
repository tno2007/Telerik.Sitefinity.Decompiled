// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Security.Web.UI.UserAlreadyLoggedInForm
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using Microsoft.Owin;
using System;
using System.Security.Claims;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Sitefinity.Security.Claims;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Web;
using Telerik.Sitefinity.Web.UI;
using Telerik.Sitefinity.Web.UI.ContentUI.Contracts;
using Telerik.Sitefinity.Web.UI.ContentUI.Views.Backend;

namespace Telerik.Sitefinity.Security.Web.UI
{
  /// <summary>
  /// 
  /// </summary>
  public class UserAlreadyLoggedInForm : ViewBase
  {
    private static readonly string layoutTemplatePath = ControlUtilities.ToVppPath("Telerik.Sitefinity.Resources.Templates.Backend.Security.UserAlreadyLoggedInForm.ascx");

    /// <summary>Gets the self logout button.</summary>
    /// <value>The self logout button.</value>
    public LinkButton SelfLogoutButton => this.Container.GetControl<LinkButton>("selfLogoutButton", true);

    /// <summary>Gets the self logout cancel button.</summary>
    /// <value>The self logout cancel button.</value>
    public LinkButton SelfLogoutCancelButton => this.Container.GetControl<LinkButton>("selfLogoutCancelButton", true);

    /// <summary>Gets the owin context.</summary>
    /// <value>The owin context.</value>
    public IOwinContext OwinContext => SystemManager.CurrentHttpContext.GetOwinContext();

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Security.Web.UI.UserAlreadyLoggedInForm" /> class.
    /// </summary>
    public UserAlreadyLoggedInForm() => this.LayoutTemplatePath = UserAlreadyLoggedInForm.layoutTemplatePath;

    /// <summary>Initializes the controls.</summary>
    /// <param name="container">The controls container.</param>
    /// <param name="definition">The content view definition.</param>
    /// <remarks>
    /// Initialize your controls in this method. Do not override CreateChildControls method.
    /// </remarks>
    protected override void InitializeControls(
      GenericContainer container,
      IContentViewDefinition definition)
    {
      this.SelfLogoutButton.Click += new EventHandler(this.SelfLogoutButton_Click);
      this.SelfLogoutCancelButton.Click += new EventHandler(this.SelfLogoutCancelButton_Click);
    }

    protected override HtmlTextWriterTag TagKey => HtmlTextWriterTag.Div;

    private void SelfLogoutButton_Click(object sender, EventArgs e)
    {
      SitefinityIdentity currentIdentity = ClaimsManager.GetCurrentIdentity();
      if (currentIdentity.UserId != Guid.Empty)
      {
        SecurityManager.Logout(currentIdentity.MembershipProvider, currentIdentity.UserId);
        this.OwinContext.Authentication.SignIn((ClaimsIdentity) currentIdentity);
      }
      else
        HttpContext.Current.Response.Redirect(UrlPath.ResolveUrl("~/Sitefinity", true));
    }

    private void SelfLogoutCancelButton_Click(object sender, EventArgs e)
    {
      if (ClaimsManager.GetCurrentIdentity().UserId != Guid.Empty)
        this.OwinContext.Authentication.SignOut();
      else
        HttpContext.Current.Response.Redirect(UrlPath.ResolveUrl("~/", true));
    }
  }
}
