// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Newsletters.Web.UI.LicenseWarning
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Web.UI;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Web.UI;

namespace Telerik.Sitefinity.Modules.Newsletters.Web.UI
{
  /// <summary>
  /// Control which displays the license warning if user has more subscribers than allowed.
  /// </summary>
  public class LicenseWarning : SimpleView
  {
    public static readonly string layoutTemplatePath = ControlUtilities.ToVppPath("Telerik.Sitefinity.Resources.Templates.Backend.Newsletters.LicenseWarning.ascx");
    private NewslettersManager newslettersManager;

    /// <summary>
    /// Gets the instance of the <see cref="P:Telerik.Sitefinity.Modules.Newsletters.Web.UI.LicenseWarning.NewslettersManager" />.
    /// </summary>
    protected NewslettersManager NewslettersManager
    {
      get
      {
        if (this.newslettersManager == null)
          this.newslettersManager = NewslettersManager.GetManager();
        return this.newslettersManager;
      }
    }

    /// <summary>Gets the name of the embedded layout template.</summary>
    /// <remarks>
    /// Override this property to change the embedded template to be used with the dialog
    /// </remarks>
    protected override string LayoutTemplateName => (string) null;

    /// <summary>
    /// Gets or sets the path of the external template to be used by the control.
    /// </summary>
    public override string LayoutTemplatePath
    {
      get
      {
        if (string.IsNullOrEmpty(base.LayoutTemplatePath))
          base.LayoutTemplatePath = LicenseWarning.layoutTemplatePath;
        return base.LayoutTemplatePath;
      }
      set => base.LayoutTemplatePath = value;
    }

    /// <summary>
    /// Gets the reference to the license warning message control.
    /// </summary>
    protected ITextControl LicenseWarningMessage => this.Container.GetControl<ITextControl>("licenseWarning", true);

    /// <summary>Initializes the controls.</summary>
    /// <remarks>
    /// Initialize your controls in this method. Do not override CreateChildControls method.
    /// </remarks>
    protected override void InitializeControls(GenericContainer container)
    {
      if (this.NewslettersManager.HasTooManySubscribers())
        this.LicenseWarningMessage.Text = string.Format(Res.Get<NewslettersResources>().LicenseWarning, (object) this.NewslettersManager.GetTotalSubscribers().ToString());
      else
        this.Visible = false;
    }

    protected override HtmlTextWriterTag TagKey => HtmlTextWriterTag.Div;
  }
}
