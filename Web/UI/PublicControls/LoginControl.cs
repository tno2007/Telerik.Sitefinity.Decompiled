// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.PublicControls.LoginControl
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Modules.UserProfiles;
using Telerik.Sitefinity.Security;
using Telerik.Sitefinity.Security.Configuration;
using Telerik.Sitefinity.Security.Model;
using Telerik.Sitefinity.Security.Web.UI;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Web.UI.Fields;

namespace Telerik.Sitefinity.Web.UI.PublicControls
{
  /// <summary>
  /// Customized <see cref="T:System.Web.UI.WebControls.Login" /> that
  /// uses Sitefinity's authentication system
  /// </summary>
  public class LoginControl : LoginForm
  {
    private ListControl userListChoiceOverride;
    private Control rememberMeContainer;

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Web.UI.PublicControls.LoginControl" /> class.
    /// </summary>
    public LoginControl()
    {
      bool visibilityOfLinksDisabled = this.updateVisibilityOfLinksDisabled;
      this.updateVisibilityOfLinksDisabled = true;
      this.ShowRegisterUserLink = false;
      this.ShowHelpLink = false;
      this.DestinationPageUrl = SystemManager.CurrentHttpContext.Request.RawUrl;
      this.updateVisibilityOfLinksDisabled = visibilityOfLinksDisabled;
      this.LayoutTemplatePath = ControlUtilities.ToVppPath("Telerik.Sitefinity.Resources.Templates.PublicControls.LoginControl.ascx");
    }

    public override bool DisplayRememberMe
    {
      get => base.DisplayRememberMe;
      set
      {
        base.DisplayRememberMe = value;
        this.RememberMeContainer.Visible = value;
      }
    }

    /// <summary>Gets the name of the layout template.</summary>
    /// <value>The name of the layout template.</value>
    protected override string LayoutTemplateName => (string) null;

    /// <summary>Gets the user list choice.</summary>
    /// <value>The user list choice.</value>
    [Browsable(false)]
    public override ChoiceField UserListChoice => (ChoiceField) null;

    /// <summary>Gets the user list choice override.</summary>
    /// <value>The user list choice override.</value>
    protected virtual ListControl UserListChoiceOverride
    {
      get
      {
        if (this.userListChoiceOverride == null)
          this.userListChoiceOverride = this.FindControl("userListChoice") as ListControl;
        return this.userListChoiceOverride;
      }
    }

    /// <summary>Binds the logged in users list.</summary>
    protected override void BindLoggedInUsersList()
    {
      if (!(this.Mode == "AdminLogsOutUser"))
        return;
      foreach (User loggedInBackendUser in (IEnumerable<User>) SecurityManager.GetLoggedInBackendUsers())
        this.UserListChoiceOverride.Items.Add(new ListItem(UserProfilesHelper.GetUserDisplayName(loggedInBackendUser.Id)));
    }

    /// <summary>
    /// Renders the contents of the control to the specified writer. This method is used primarily by control developers.
    /// </summary>
    /// <param name="writer">A <see cref="T:System.Web.UI.HtmlTextWriter" /> that represents the output stream to render HTML content on the client.</param>
    protected override void RenderContents(HtmlTextWriter writer)
    {
      if (!SystemManager.IsDesignMode)
        base.RenderContents(writer);
      else if (SecurityManager.AuthenticationMode == AuthenticationMode.Forms)
      {
        base.RenderContents(writer);
      }
      else
      {
        if (SecurityManager.AuthenticationMode != AuthenticationMode.Claims)
          return;
        string str = Res.Get<PublicControlsResources>().LoginControlAvailability.Arrange((object) Res.Get<PublicControlsResources>().ExternalLinkLoginControlAvailability);
        writer.Write(str);
      }
    }

    /// <summary>Reference to the RememberMe checkbox container</summary>
    protected virtual Control RememberMeContainer
    {
      get
      {
        if (this.rememberMeContainer == null)
          this.rememberMeContainer = this.FindControl("rememberMeContainer");
        return this.rememberMeContainer;
      }
    }
  }
}
