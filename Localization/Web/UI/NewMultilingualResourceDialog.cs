// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Localization.Web.UI.NewMultilingualResourceDialog
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Web.UI;
using Telerik.Sitefinity.Web.UI;

namespace Telerik.Sitefinity.Localization.Web.UI
{
  /// <summary>
  /// Represents a dialog for creating new resources in multilingual mode.
  /// </summary>
  public class NewMultilingualResourceDialog : AjaxDialogBase
  {
    public static readonly string layoutTemplatePath = ControlUtilities.ToVppPath("Telerik.Sitefinity.Resources.Templates.Backend.Localization.NewMultilingualResource.ascx");

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Localization.Web.UI.NewMultilingualResourceDialog" /> class.
    /// </summary>
    public NewMultilingualResourceDialog() => this.LayoutTemplatePath = NewMultilingualResourceDialog.layoutTemplatePath;

    /// <inheritdoc />
    protected override void InitializeControls(GenericContainer dialogContainer)
    {
    }

    /// <inheritdoc />
    protected override string LayoutTemplateName => (string) null;

    /// <inheritdoc />
    protected override HtmlTextWriterTag TagKey => HtmlTextWriterTag.Div;
  }
}
