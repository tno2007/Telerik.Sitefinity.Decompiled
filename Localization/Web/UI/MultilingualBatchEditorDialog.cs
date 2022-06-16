// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Localization.Web.UI.MultilingualBatchEditorDialog
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Web.UI;
using Telerik.Sitefinity.Web.UI;

namespace Telerik.Sitefinity.Localization.Web.UI
{
  /// <summary>
  /// Control which represents dialog for batch editing of resource entries in multilingual mode.
  /// </summary>
  public class MultilingualBatchEditorDialog : AjaxDialogBase
  {
    public static readonly string layoutTemplatePath = ControlUtilities.ToVppPath("Telerik.Sitefinity.Resources.Templates.Backend.Localization.MultilingualBatchEditor.ascx");

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Localization.Web.UI.MultilingualBatchEditorDialog" /> class.
    /// </summary>
    public MultilingualBatchEditorDialog() => this.LayoutTemplatePath = MultilingualBatchEditorDialog.layoutTemplatePath;

    /// <summary>Initializes the controls.</summary>
    /// <remarks>
    /// Initialize your controls in this method. Do not override CreateChildControls method.
    /// </remarks>
    protected override void InitializeControls(GenericContainer dialogContainer)
    {
    }

    /// <summary>Gets the name of the embedded layout template.</summary>
    /// <value></value>
    /// <remarks>
    /// Override this property to change the embedded template to be used with the dialog
    /// </remarks>
    protected override string LayoutTemplateName => (string) null;

    /// <summary>Gets the <see cref="T:System.Web.UI.HtmlTextWriterTag" /> value that
    /// corresponds to this Web server control. This property is used primarily by control
    /// developers.</summary>
    /// <returns>One of the <see cref="T:System.Web.UI.HtmlTextWriterTag" /> enumeration
    /// values.</returns>
    protected override HtmlTextWriterTag TagKey => HtmlTextWriterTag.Div;
  }
}
