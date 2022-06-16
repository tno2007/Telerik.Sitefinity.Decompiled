// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Security.Web.UI.LoginFailedView
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using Microsoft.Owin.Security;
using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Sitefinity.Security.Claims;
using Telerik.Sitefinity.Web;
using Telerik.Sitefinity.Web.UI;
using Telerik.Sitefinity.Web.UI.ContentUI.Contracts;
using Telerik.Sitefinity.Web.UI.ContentUI.Views.Backend;

namespace Telerik.Sitefinity.Security.Web.UI
{
  /// <summary>Login failed view</summary>
  public class LoginFailedView : ViewBase
  {
    private static string layoutTemplatePath = ControlUtilities.ToVppPath("Telerik.Sitefinity.Resources.Templates.Backend.Security.LoginFailedView.ascx");

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Security.Web.UI.LoginFailedView" /> class.
    /// </summary>
    public LoginFailedView() => this.LayoutTemplatePath = LoginFailedView.layoutTemplatePath;

    /// <summary>Gets a reference to the try again button.</summary>
    protected LinkButton TryAgainBtn => this.Container.GetControl<LinkButton>("tryAgainBtn", false);

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
      this.TryAgainBtn.Click += new EventHandler(this.TryAgainBtn_Click);
    }

    private void TryAgainBtn_Click(object sender, EventArgs e) => this.Page.Request.GetOwinContext().Authentication.Challenge(new AuthenticationProperties()
    {
      RedirectUri = UrlPath.ResolveUrl("~/", true)
    }, ClaimsManager.CurrentAuthenticationModule.STSAuthenticationType);

    /// <inheritdoc />
    protected override HtmlTextWriterTag TagKey => HtmlTextWriterTag.Div;
  }
}
