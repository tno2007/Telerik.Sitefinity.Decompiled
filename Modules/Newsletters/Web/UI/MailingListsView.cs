// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Newsletters.Web.UI.MailingListsView
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Sitefinity.Web;
using Telerik.Sitefinity.Web.UI;

namespace Telerik.Sitefinity.Modules.Newsletters.Web.UI
{
  /// <summary>
  /// The recipients list view control for the newsletters module.
  /// </summary>
  public class MailingListsView : ViewModeControl<NewslettersControlPanel>
  {
    public static readonly string layoutTemplatePath = ControlUtilities.ToVppPath("Telerik.Sitefinity.Resources.Templates.Backend.Newsletters.MailingListsView.ascx");

    /// <summary>Gets the name of the layout template.</summary>
    /// <value>The name of the layout template.</value>
    protected override string LayoutTemplateName => (string) null;

    /// <summary>
    /// Gets or sets the path of the external template to be used by the control.
    /// </summary>
    /// <value></value>
    public override string LayoutTemplatePath
    {
      get => string.IsNullOrEmpty(base.LayoutTemplatePath) ? MailingListsView.layoutTemplatePath : base.LayoutTemplatePath;
      set => base.LayoutTemplatePath = value;
    }

    /// <summary>
    /// Gets the reference to the subscribers url hidden field.
    /// </summary>
    protected virtual HiddenField SubscribersUrlHidden => this.Container.GetControl<HiddenField>("subscribersUrlHidden", true);

    /// <summary>Initializes the controls.</summary>
    /// <param name="viewContainer">The view container.</param>
    protected override void InitializeControls(Control viewContainer)
    {
      base.InitializeControls(viewContainer);
      this.SubscribersUrlHidden.Value = this.GetSubscribersUrl();
    }

    /// <summary>Gets the subscribers URL.</summary>
    /// <returns></returns>
    protected string GetSubscribersUrl()
    {
      string subscribersUrl = string.Empty;
      SiteMapNode siteMapNode = BackendSiteMap.FindSiteMapNode(NewslettersModule.subscribersPageId, false);
      if (siteMapNode != null)
        subscribersUrl = RouteHelper.ResolveUrl(siteMapNode.Url, UrlResolveOptions.Rooted | UrlResolveOptions.AppendTrailingSlash);
      return subscribersUrl;
    }
  }
}
