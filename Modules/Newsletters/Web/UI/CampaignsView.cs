// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Newsletters.Web.UI.CampaignsView
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Web.UI;

namespace Telerik.Sitefinity.Modules.Newsletters.Web.UI
{
  /// <summary>
  /// The view for managing newsletter campaigns for the newsletters module.
  /// </summary>
  public class CampaignsView : ViewModeControl<NewslettersControlPanel>
  {
    public static readonly string layoutTemplatePath = ControlUtilities.ToVppPath("Telerik.Sitefinity.Resources.Templates.Backend.Newsletters.CampaignsView.ascx");
    private const string webServiceUrl = "~/Sitefinity/Services/Newsletters/Campaign.svc";
    private const string newslettersHandlerUrl = "~/Sitefinity/SFNwslttrs/";

    /// <summary>Gets the name of the layout template.</summary>
    /// <value>The name of the layout template.</value>
    protected override string LayoutTemplateName => (string) null;

    /// <summary>
    /// Gets or sets the path of the external template to be used by the control.
    /// </summary>
    /// <value></value>
    public override string LayoutTemplatePath
    {
      get => string.IsNullOrEmpty(base.LayoutTemplatePath) ? CampaignsView.layoutTemplatePath : base.LayoutTemplatePath;
      set => base.LayoutTemplatePath = value;
    }

    /// <summary>
    /// Gets the reference to the hidden field holding the url of the web service.
    /// </summary>
    protected virtual HiddenField WebServiceUrlHidden => this.Container.GetControl<HiddenField>("webServiceUrlHidden", true);

    protected virtual Message MessageControl => this.Container.GetControl<Message>("message", true);

    /// <summary>
    /// Gets a reference to the hidden field holding the newsletters handler URL.
    /// </summary>
    protected virtual HiddenField NewslettersHandlerUrlHidden => this.Container.GetControl<HiddenField>("newslettersHandlerUrlHidden", true);

    /// <summary>Initializes the controls.</summary>
    /// <param name="viewContainer">The view container.</param>
    protected override void InitializeControls(Control viewContainer)
    {
      this.WebServiceUrlHidden.Value = this.Page.ResolveUrl("~/Sitefinity/Services/Newsletters/Campaign.svc");
      this.NewslettersHandlerUrlHidden.Value = this.Page.ResolveUrl("~/Sitefinity/SFNwslttrs/");
      if (this.Page.Request.QueryString["status"] == null || !(this.Page.Request.QueryString["status"] == "created"))
        return;
      this.MessageControl.ShowPositiveMessage(Res.Get<NewslettersResources>().NewCampaignSuccessfullyCreated);
    }
  }
}
