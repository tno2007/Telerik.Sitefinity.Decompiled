// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Localization.Web.UI.MonolingualBatchEditorDialog
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
  /// Control which represents dialog for batch editing of resource entries in monolingual mode.
  /// </summary>
  public class MonolingualBatchEditorDialog : AjaxDialogBase
  {
    private const string layoutTemplatePath = "~/SFRs/Telerik.Sitefinity.Resources.Templates.Backend.Localization.MonolingualBatchEditor.ascx";

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Localization.Web.UI.MonolingualBatchEditorDialog" /> class.
    /// </summary>
    public MonolingualBatchEditorDialog() => this.LayoutTemplatePath = "~/SFRs/Telerik.Sitefinity.Resources.Templates.Backend.Localization.MonolingualBatchEditor.ascx";

    /// <summary>Initializes the controls.</summary>
    /// <param name="dialogContainer"></param>
    /// <remarks>
    /// Initialize your controls in this method. Do not override CreateChildControls method.
    /// </remarks>
    protected override void InitializeControls(GenericContainer dialogContainer) => this.MonolingualCulture.Value = Config.Get<ResourcesConfig>().MonolingualCulture;

    /// <summary>Gets the name of the embedded layout template.</summary>
    /// <value></value>
    /// <remarks>
    /// Override this property to change the embedded template to be used with the dialog
    /// </remarks>
    protected override string LayoutTemplateName => (string) null;

    /// <summary>Gets the monolingual culture.</summary>
    /// <value>The monolingual culture.</value>
    protected virtual HiddenField MonolingualCulture => this.Container.GetControl<HiddenField>("monolingualCulture", true);

    protected override HtmlTextWriterTag TagKey => HtmlTextWriterTag.Div;
  }
}
