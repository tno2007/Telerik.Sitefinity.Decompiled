// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Libraries.Web.UI.TextEditorToolSetDialog
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Security.Sanitizers;
using Telerik.Sitefinity.Web.UI;

namespace Telerik.Sitefinity.Modules.Libraries.Web.UI
{
  public class TextEditorToolSetDialog : AjaxDialogBase
  {
    public static readonly string layoutTemplatePath = ControlUtilities.ToVppPath("Telerik.Sitefinity.Resources.Templates.Backend.Configuration.Basic.TextEditorToolSetDialog.ascx");
    private const string serviceBaseUrl = "~/Sitefinity/Services/Configuration/ConfigSectionItems.svc/texteditortoolset/";
    private const string serviceBaseSaveUrl = "~/Sitefinity/Services/Configuration/ConfigSectionItems.svc/texteditor/";

    public TextEditorToolSetDialog() => this.LayoutTemplatePath = TextEditorToolSetDialog.layoutTemplatePath;

    protected override string LayoutTemplateName => (string) null;

    protected override void InitializeControls(GenericContainer container)
    {
      IHtmlSanitizer htmlSanitizer = ObjectFactory.Resolve<IHtmlSanitizer>();
      this.ServiceBaseUrl.Value = htmlSanitizer.Sanitize(this.ResolveClientUrl("~/Sitefinity/Services/Configuration/ConfigSectionItems.svc/texteditortoolset/"));
      this.ServiceBaseSaveUrl.Value = htmlSanitizer.Sanitize(this.ResolveClientUrl("~/Sitefinity/Services/Configuration/ConfigSectionItems.svc/texteditor/"));
    }

    protected override HtmlTextWriterTag TagKey => HtmlTextWriterTag.Div;

    /// <summary>
    /// Gets the reference to the hidden field that holds the url of the config section items WCF service.
    /// </summary>
    protected virtual HiddenField ServiceBaseUrl => this.Container.GetControl<HiddenField>("serviceBaseUrl", true);

    /// <summary>
    /// Gets the reference to the hidden field that holds the url of the config section items WCF service.
    /// </summary>
    protected virtual HiddenField ServiceBaseSaveUrl => this.Container.GetControl<HiddenField>("serviceBaseSaveUrl", true);

    /// <summary>The Toolset to load</summary>
    public string ToolSetName { get; set; }
  }
}
