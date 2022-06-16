// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Newsletters.Web.UI.SubscribersView
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.Newsletters.Model;
using Telerik.Sitefinity.Web.UI;
using Telerik.Sitefinity.Web.UI.Backend;
using Telerik.Web.UI;

namespace Telerik.Sitefinity.Modules.Newsletters.Web.UI
{
  /// <summary>
  /// The view for managing subscribers of the newsletters module.
  /// </summary>
  public class SubscribersView : ViewModeControl<NewslettersControlPanel>
  {
    public static readonly string layoutTemplatePath = ControlUtilities.ToVppPath("Telerik.Sitefinity.Resources.Templates.Backend.Newsletters.SubscribersView.ascx");
    private const string webServiceUrl = "~/Sitefinity/Services/Newsletters/Subscriber.svc/";

    /// <summary>Gets the name of the layout template.</summary>
    /// <value>The name of the layout template.</value>
    protected override string LayoutTemplateName => (string) null;

    /// <summary>
    /// Gets or sets the path of the external template to be used by the control.
    /// </summary>
    /// <value></value>
    public override string LayoutTemplatePath
    {
      get => string.IsNullOrEmpty(base.LayoutTemplatePath) ? SubscribersView.layoutTemplatePath : base.LayoutTemplatePath;
      set => base.LayoutTemplatePath = value;
    }

    /// <summary>Gets the tag key.</summary>
    /// <value>The tag key.</value>
    protected override HtmlTextWriterTag TagKey => HtmlTextWriterTag.Div;

    /// <summary>Gets the reference to the subscribers grid binder.</summary>
    protected virtual RadGrid SubscribersGrid => this.Container.GetControl<RadGrid>("subscribersGrid", true);

    /// <summary>Gets the reference to the subscribers grid binder.</summary>
    protected virtual RadGridBinder SubscribersGridBinder => this.Container.GetControl<RadGridBinder>("subscribersGridBinder", true);

    /// <summary>
    /// Gets the reference to the mailing list id hidden field.
    /// </summary>
    protected virtual HiddenField MailingListIdHidden => this.Container.GetControl<HiddenField>("mailingListIdHidden", true);

    /// <summary>
    /// Gets the reference to the mailing list title hidden field.
    /// </summary>
    protected virtual HiddenField MailingListTitleHidden => this.Container.GetControl<HiddenField>("mailingListTitleHidden", true);

    /// <summary>
    /// Gets the reference to the service base url hidden field.
    /// </summary>
    protected virtual HiddenField ServiceBaseUrlHidden => this.Container.GetControl<HiddenField>("serviceBaseUrlHidden", true);

    /// <summary>Gets the reference to the remove button wrapper.</summary>
    protected virtual HtmlGenericControl RemoveButtonWrapper => this.Container.GetControl<HtmlGenericControl>("removeButtonWrapper", true);

    /// <summary>Initializes the controls.</summary>
    /// <param name="viewContainer">The view container.</param>
    protected override void InitializeControls(Control viewContainer)
    {
      base.InitializeControls(viewContainer);
      this.SubscribersGrid.ItemEvent += new GridItemEventHandler(this.ItemGrid_ItemEvent);
      string str = this.Page.ResolveUrl("~/Sitefinity/Services/Newsletters/Subscriber.svc/");
      this.ServiceBaseUrlHidden.Value = str;
      if (this.Page.GetRequestContext().RouteData.Values["Params"] is string[] strArray && strArray[0].IsGuid())
      {
        string g = strArray[0];
        this.MailingListIdHidden.Value = g;
        this.SubscribersGridBinder.ServiceUrl = str + "mailingList/" + g;
        MailingList mailingList = NewslettersManager.GetManager(this.Page.Request.QueryString["provider"]).GetMailingList(new Guid(g));
        if (mailingList == null)
          return;
        Lstring title = mailingList.Title;
        this.Host.Title = HttpUtility.HtmlEncode(string.Format(Res.Get<NewslettersResources>().SubscribersForMailingList, (object) title));
        this.MailingListTitleHidden.Value = (string) title;
      }
      else
      {
        this.Host.Title = Res.Get<NewslettersResources>().Subscribers;
        this.RemoveButtonWrapper.Visible = false;
      }
    }

    private void ItemGrid_ItemEvent(object sender, GridItemEventArgs e)
    {
      if (!(e.EventInfo is GridInitializePagerItem))
        return;
      e.Canceled = true;
      (e.Item as GridPagerItem).PagerContentCell.Controls.Add((Control) new ClientPager(this.SubscribersGridBinder));
    }
  }
}
