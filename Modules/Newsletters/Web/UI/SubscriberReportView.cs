// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Newsletters.Web.UI.SubscriberReportView
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Web;
using System.Web.UI;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Modules.Newsletters.Data.Reports;
using Telerik.Sitefinity.Newsletters.Model;
using Telerik.Sitefinity.Web.UI;
using Telerik.Web.UI;

namespace Telerik.Sitefinity.Modules.Newsletters.Web.UI
{
  /// <summary>
  /// The view for exploring the report for a given campaign.
  /// </summary>
  public class SubscriberReportView : ViewModeControl<NewslettersControlPanel>
  {
    private SubscriberReport subscriberReport;
    private Guid subscriberId;
    private Subscriber subscriber;
    public static readonly string layoutTemplatePath = ControlUtilities.ToVppPath("Telerik.Sitefinity.Resources.Templates.Backend.Newsletters.SubscriberReportView.ascx");

    protected virtual Guid SubscriberId
    {
      get
      {
        if (this.subscriberId == Guid.Empty)
        {
          string[] urlParameters = this.GetUrlParameters();
          if (urlParameters.Length == 0)
            throw new InvalidOperationException("It is unclear for which subscriber report ought to be generated. The id of the subscriber is missing from the url.");
          this.subscriberId = urlParameters[0].IsGuid() ? new Guid(urlParameters[0]) : throw new ArgumentException("The url parameter carrying information about the subscriber id is not a valid GUID.");
        }
        return this.subscriberId;
      }
    }

    protected virtual Subscriber Subscriber
    {
      get
      {
        if (this.subscriber == null)
          this.subscriber = NewslettersManager.GetManager().GetSubscriber(this.SubscriberId);
        return this.subscriber;
      }
    }

    protected virtual SubscriberReport SubscriberReport
    {
      get
      {
        if (this.subscriberReport == null)
          this.subscriberReport = new SubscriberReport(string.Empty, this.SubscriberId);
        return this.subscriberReport;
      }
    }

    /// <summary>Gets the name of the layout template.</summary>
    /// <value>The name of the layout template.</value>
    protected override string LayoutTemplateName => (string) null;

    /// <summary>
    /// Gets or sets the path of the external template to be used by the control.
    /// </summary>
    /// <value></value>
    public override string LayoutTemplatePath
    {
      get => string.IsNullOrEmpty(base.LayoutTemplatePath) ? SubscriberReportView.layoutTemplatePath : base.LayoutTemplatePath;
      set => base.LayoutTemplatePath = value;
    }

    protected virtual RadGrid ClickedLinksGrid => this.Container.GetControl<RadGrid>("clickedLinksGrid", true);

    protected override void InitializeControls(Control viewContainer)
    {
      string str = !this.Subscriber.FirstName.IsNullOrWhitespace() || !this.Subscriber.LastName.IsNullOrWhitespace() ? (!this.Subscriber.FirstName.IsNullOrWhitespace() ? (!this.Subscriber.LastName.IsNullOrWhitespace() ? string.Format("{0}, {1}", (object) this.Subscriber.LastName, (object) this.Subscriber.FirstName) : string.Format("{0}", (object) this.Subscriber.FirstName)) : string.Format("{0}", (object) this.Subscriber.LastName)) : string.Format("{0}", (object) this.Subscriber.Email);
      this.Host.Title = HttpUtility.HtmlEncode(string.Format("{0} | {1}", (object) Res.Get<NewslettersResources>().SubscriberReportHtmlTitle, (object) str));
      this.ClickedLinksGrid.NeedDataSource += new GridNeedDataSourceEventHandler(this.ClickedLinksGrid_NeedDataSource);
    }

    private void ClickedLinksGrid_NeedDataSource(object sender, GridNeedDataSourceEventArgs e) => this.ClickedLinksGrid.DataSource = (object) this.SubscriberReport.LinksClicked;
  }
}
