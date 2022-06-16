// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Security.Web.UI.ProfileView
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Security.Claims;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Telerik.Sitefinity.Security.Claims;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Web.UI;
using Telerik.Sitefinity.Web.UI.ContentUI.Contracts;
using Telerik.Sitefinity.Web.UI.ContentUI.Enums;
using Telerik.Sitefinity.Web.UI.ContentUI.Views.Backend;

namespace Telerik.Sitefinity.Security.Web.UI
{
  /// <summary>Represents the profile view.</summary>
  public class ProfileView : ViewBase, IScriptControl
  {
    public static readonly string layoutTemplatePath = ControlUtilities.ToVppPath("Telerik.Sitefinity.Resources.Templates.Backend.Security.ProfileView.ascx");

    /// <summary>Gets the current user.</summary>
    /// <value>The current user.</value>
    [Obsolete("Use SystemManager.CurrentHttpContext.User instead.")]
    public ClaimsPrincipal CurrentUser => SystemManager.CurrentHttpContext.User as ClaimsPrincipal;

    /// <summary>
    /// Gets the reference to the hidden field containing the user id.
    /// </summary>
    public HiddenField UserIdHidden => this.Container.GetControl<HiddenField>("hfUserId", true);

    /// <summary>
    /// Gets the reference to the hidden field containing the provider name.
    /// </summary>
    public HiddenField ProviderName => this.Container.GetControl<HiddenField>("hfProviderName", true);

    /// <summary>Gets the reference to the profile container.</summary>
    public HtmlControl ProfileDataContainer => this.Container.GetControl<HtmlControl>("profileData", true);

    /// <summary>Gets the name of the embedded layout template.</summary>
    /// <value></value>
    /// <remarks>
    /// Override this property to change the embedded template to be used with the dialog
    /// </remarks>
    protected override string LayoutTemplateName => (string) null;

    /// <summary>
    /// Gets or sets the path of the external template to be used by the control.
    /// </summary>
    /// <value></value>
    public override string LayoutTemplatePath
    {
      get => string.IsNullOrEmpty(base.LayoutTemplatePath) ? ProfileView.layoutTemplatePath : base.LayoutTemplatePath;
      set => base.LayoutTemplatePath = value;
    }

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
      UserProfileView child = new UserProfileView();
      child.DisplayCurrentUser = new bool?(true);
      child.ControlDefinitionName = "BackendSingleProfile";
      child.ChangePasswordModeViewName = "BackendChangePasswordDetailView";
      child.ChangeQuestionAndAnswerModeViewName = "BackendChangeQuestionAndAnswerDetailView";
      child.ReadModeViewName = "UserProfilesBackendDetailsRead";
      child.WriteModeViewName = "UserProfilesBackendDetailsWrite";
      child.ContentViewDisplayMode = ContentViewDisplayMode.Automatic;
      child.ProfileViewMode = new UserProfileViewMode?(UserProfileViewMode.Read);
      this.ProfileDataContainer.Controls.Add((Control) child);
    }

    protected override void OnLoad(EventArgs e)
    {
      base.OnLoad(e);
      this.SetUserInfo();
    }

    protected override void OnPreRender(EventArgs e)
    {
      base.OnPreRender(e);
      this.SetUserInfo();
    }

    private void SetUserInfo()
    {
      if (this.CurrentUser == null)
        return;
      this.UserIdHidden.Value = ClaimsManager.GetCurrentUserId().ToString();
      this.ProviderName.Value = ClaimsManager.GetMembershipProvider();
    }

    /// <summary>
    /// Gets the <see cref="T:System.Web.UI.HtmlTextWriterTag" /> value that corresponds to this Web server control. This property is used primarily by control developers.
    /// </summary>
    /// <value></value>
    /// <returns>One of the <see cref="T:System.Web.UI.HtmlTextWriterTag" /> enumeration values.</returns>
    protected override HtmlTextWriterTag TagKey => HtmlTextWriterTag.Div;
  }
}
