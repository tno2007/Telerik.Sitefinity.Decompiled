// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Localization.Web.UI.EditMonolingualResourceDialog
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Localization.Configuration;
using Telerik.Sitefinity.Web.UI;

namespace Telerik.Sitefinity.Localization.Web.UI
{
  /// <summary>
  /// Represents dialog for editing resources in monolingual mode.
  /// </summary>
  public class EditMonolingualResourceDialog : AjaxDialogBase
  {
    public static readonly string layoutTemplatePath = ControlUtilities.ToVppPath("Telerik.Sitefinity.Resources.Templates.Backend.Localization.EditMonolingualResource.ascx");

    /// <summary>Gets the name of the embedded layout template.</summary>
    /// <value></value>
    protected override string LayoutTemplateName => (string) null;

    /// <summary>
    /// Gets or sets the path of the external template to be used by the control.
    /// </summary>
    /// <value></value>
    public override string LayoutTemplatePath
    {
      get => string.IsNullOrEmpty(base.LayoutTemplatePath) ? EditMonolingualResourceDialog.layoutTemplatePath : base.LayoutTemplatePath;
      set => base.LayoutTemplatePath = value;
    }

    /// <summary>
    /// Gets the hidden field which holds the name of the monolingual culture.
    /// </summary>
    protected virtual HiddenField MonolingualCulture => this.Container.GetControl<HiddenField>("monolingualCulture", true);

    protected override HtmlTextWriterTag TagKey => HtmlTextWriterTag.Div;

    /// <summary>Initializes the controls.</summary>
    /// <param name="dialogContainer"></param>
    /// <remarks>
    /// Initialize your controls in this method. Do not override CreateChildControls method.
    /// </remarks>
    protected override void InitializeControls(GenericContainer dialogContainer) => this.MonolingualCulture.Value = Config.Get<ResourcesConfig>().MonolingualCulture;
  }
}
