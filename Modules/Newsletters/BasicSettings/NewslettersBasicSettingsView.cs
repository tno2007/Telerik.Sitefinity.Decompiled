// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Newsletters.BasicSettings.NewslettersBasicSettingsView
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Sitefinity.Web.UI;
using Telerik.Sitefinity.Web.UI.Fields;

namespace Telerik.Sitefinity.Modules.Newsletters.BasicSettings
{
  /// <summary>Basic settings view for the newsletters settings.</summary>
  [Obsolete("Use the EmailSettingsBasicSettingsView instead")]
  public class NewslettersBasicSettingsView : SimpleView
  {
    private const string webServiceUrl = "~/Sitefinity/Services/Newsletters/Settings.svc/";
    public static readonly string layoutTemplatePath = ControlUtilities.ToVppPath("Telerik.Sitefinity.Resources.Templates.Backend.Configuration.Basic.NewslettersBasicSettingsView.ascx");
    public const string DefaultProfileDefaultName = "Default";
    public const string SendGridProfileName = "SendGrid";
    public const string MandrillProfileName = "Mandrill";
    public const string MailGunProfileName = "MailGun";

    /// <summary>Gets the name of the layout template.</summary>
    /// <value>The name of the layout template.</value>
    protected override string LayoutTemplateName => (string) null;

    /// <summary>Gets or sets the layout template path.</summary>
    /// <value>The layout template path.</value>
    public override string LayoutTemplatePath
    {
      get => string.IsNullOrEmpty(base.LayoutTemplatePath) ? NewslettersBasicSettingsView.layoutTemplatePath : base.LayoutTemplatePath;
      set => base.LayoutTemplatePath = value;
    }

    protected virtual HiddenField TestingServiceUrl => this.Container.GetControl<HiddenField>("testingServiceUrl", true);

    protected virtual ChoiceField SmtpProfiles => this.Container.GetControl<ChoiceField>(nameof (SmtpProfiles), true);

    protected override void InitializeControls(GenericContainer container)
    {
      this.TestingServiceUrl.Value = VirtualPathUtility.ToAbsolute("~/Sitefinity/Services/Newsletters/Settings.svc/");
      this.SmtpProfiles.Choices.Add(new ChoiceItem()
      {
        Text = "Default",
        Value = "0"
      });
      this.SmtpProfiles.Choices.Add(new ChoiceItem()
      {
        Text = "SendGrid",
        Value = "1"
      });
      this.SmtpProfiles.Choices.Add(new ChoiceItem()
      {
        Text = "Mandrill",
        Value = "2"
      });
      this.SmtpProfiles.Choices.Add(new ChoiceItem()
      {
        Text = "MailGun",
        Value = "3"
      });
    }

    protected override HtmlTextWriterTag TagKey => HtmlTextWriterTag.Div;
  }
}
