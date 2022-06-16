// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.Backend.Header
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Data.ContentLinks;
using Telerik.Sitefinity.Multisite;
using Telerik.Sitefinity.Security;
using Telerik.Sitefinity.Security.Claims;
using Telerik.Sitefinity.Security.Configuration;
using Telerik.Sitefinity.Security.Model;
using Telerik.Sitefinity.Services;

namespace Telerik.Sitefinity.Web.UI.Backend
{
  /// <summary>Represents the header in the backend area.</summary>
  public class Header : SimpleView
  {
    public static readonly string layoutTemplatePath = ControlUtilities.ToVppPath("Telerik.Sitefinity.Resources.Templates.Backend.Header.ascx");

    /// <summary>
    /// Gets the name of the embedded layout template. If the control uses layout template this
    /// property must be overridden to provide the path (key) to an embedded resource file.
    /// </summary>
    /// <value></value>
    protected override string LayoutTemplateName => (string) null;

    /// <summary>
    /// Gets or sets the path of the external template to be used by the control.
    /// </summary>
    /// <value></value>
    public override string LayoutTemplatePath
    {
      get => string.IsNullOrEmpty(base.LayoutTemplatePath) ? Header.layoutTemplatePath : base.LayoutTemplatePath;
      set => base.LayoutTemplatePath = value;
    }

    /// <summary>
    /// Overridden. Cancels the rendering of a beginning HTML tag for the control.
    /// </summary>
    /// <param name="writer">The HtmlTextWriter object used to render the markup.</param>
    public override void RenderBeginTag(HtmlTextWriter writer)
    {
    }

    /// <summary>
    /// Overridden. Cancels the rendering of an ending HTML tag for the control.
    /// </summary>
    /// <param name="writer">The HtmlTextWriter object used to render the markup.</param>
    public override void RenderEndTag(HtmlTextWriter writer)
    {
    }

    /// <summary>Initializes the controls.</summary>
    /// <param name="dialogContainer"></param>
    /// <remarks>
    /// Initialize your controls in this method. Do not override CreateChildControls method.
    /// </remarks>
    protected override void InitializeControls(GenericContainer container)
    {
      SecurityManager.GetCurrentUserId();
      User user = UserManager.GetManager(ClaimsManager.GetCurrentIdentity().MembershipProvider).GetUser(ClaimsManager.GetCurrentUserId());
      SitefinityProfile userProfile = UserProfileManager.GetManager().GetUserProfile<SitefinityProfile>(user);
      Label control1 = this.Container.GetControl<Label>("UserDisplayName", true);
      Label control2 = this.Container.GetControl<Label>("UserDisplayEmail", true);
      System.Web.UI.WebControls.Image control3 = this.Container.GetControl<System.Web.UI.WebControls.Image>("ProfileImageHeader", true);
      System.Web.UI.WebControls.Image control4 = this.Container.GetControl<System.Web.UI.WebControls.Image>("ProfileImageBody", true);
      control2.Text = user.Email;
      control3.ImageUrl = "~/SFRes/images/Telerik.Sitefinity.Resources/Images.DefaultPhoto.png";
      control4.ImageUrl = "~/SFRes/images/Telerik.Sitefinity.Resources/Images.DefaultPhoto.png";
      if (userProfile != null)
      {
        control1.Text = HttpUtility.HtmlEncode(userProfile.GetUserDisplayName());
        if (userProfile.Avatar != null && userProfile.Avatar.GetLinkedItem() is Telerik.Sitefinity.Libraries.Model.Image)
        {
          Telerik.Sitefinity.Libraries.Model.Image linkedItem = userProfile.Avatar.GetLinkedItem() as Telerik.Sitefinity.Libraries.Model.Image;
          control3.ImageUrl = linkedItem.ThumbnailUrl;
          control4.ImageUrl = linkedItem.ThumbnailUrl;
        }
      }
      HyperLink control5 = this.Container.GetControl<HyperLink>("profileLink", false);
      if (control5 != null)
      {
        SiteMapNode siteMapNode = BackendSiteMap.FindSiteMapNode(SiteInitializer.ProfileNodeId, false);
        if (siteMapNode != null)
          control5.NavigateUrl = UrlPath.ResolveUrl(siteMapNode.Url);
      }
      if (SecurityManager.AuthenticationMode == AuthenticationMode.Claims)
      {
        HyperLink control6 = this.Container.GetControl<HyperLink>("logoutButton", false);
        if (control6 != null)
          control6.NavigateUrl = UrlPath.ResolveUrl(ClaimsManager.GetLogoutUrl());
      }
      SitefinityHyperLink control7 = this.Container.GetControl<SitefinityHyperLink>("goToSiteButton", false);
      if (control7 != null)
      {
        IMultisiteContext multisiteContext = SystemManager.CurrentContext.MultisiteContext;
        if (multisiteContext != null && multisiteContext.CurrentSiteContext.ResolutionType != SiteContextResolutionTypes.ByDomain)
        {
          string absoluteUri = SystemManager.CurrentContext.CurrentSite.GetUri().AbsoluteUri;
          control7.NavigateUrl = absoluteUri;
        }
        if (control7.NavigateUrl.StartsWith("~/"))
          control7.NavigateUrl = UrlPath.ResolveUrl(control7.NavigateUrl);
      }
      string str = UrlPath.AddAppVirtualPath("/RestApi");
      this.Container.GetControl<HiddenField>("baseUrl", true).Value = str;
    }
  }
}
