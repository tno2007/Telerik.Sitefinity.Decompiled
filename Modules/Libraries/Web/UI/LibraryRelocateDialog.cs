// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Libraries.Web.UI.LibraryRelocateDialog
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Web.UI;
using Telerik.Sitefinity.Web.UI.Fields;

namespace Telerik.Sitefinity.Modules.Libraries.Web.UI
{
  public class LibraryRelocateDialog : AjaxDialogBase
  {
    private static RegexStrategy regexStrategy = (RegexStrategy) null;
    public static readonly string layoutTemplatePath = ControlUtilities.ToVppPath("Telerik.Sitefinity.Resources.Templates.Backend.Libraries.LibraryRelocateDialog.ascx");

    public LibraryRelocateDialog() => this.LayoutTemplatePath = LibraryRelocateDialog.layoutTemplatePath;

    protected override string LayoutTemplateName => (string) null;

    protected virtual BlobStorageChoiceField BlobProvider => this.Container.GetControl<BlobStorageChoiceField>("blobProvider", true);

    protected virtual TextField LibraryUrl => this.Container.GetControl<TextField>("libraryUrl", true);

    protected virtual Literal DoneButtonText => this.Container.GetControl<Literal>("doneButtonText", true);

    protected override void InitializeControls(GenericContainer container)
    {
      string str = "RelocateLibrary";
      if (this.Page.Request.QueryString["mode"] != null)
        str = this.Page.Request.QueryString["mode"];
      SitefinityLabel control = this.Container.GetControl<SitefinityLabel>("viewTitle", true);
      if (str == "RelocateLibrary")
      {
        this.BlobProvider.Visible = false;
        control.Text = Res.Get<LibrariesResources>().RelocateLibrary;
        this.DoneButtonText.Text = Res.Get<LibrariesResources>().RelocateLibrary;
      }
      if (str == "TransferLibrary")
      {
        this.LibraryUrl.Visible = false;
        control.Text = Res.Get<LibrariesResources>().MoveLibraryDialogTitle;
        this.DoneButtonText.Text = Res.Get<LibrariesResources>().MoveLibrary;
      }
      if (this.LibraryUrl.ValidatorDefinition == null)
        return;
      this.LibraryUrl.ValidatorDefinition.RegularExpression = "^[" + LibraryRelocateDialog.RgxStrategy.UnicodeCategories + "\\-\\!\\$\\(\\)\\=\\@\\d_\\'~]+$";
    }

    protected override HtmlTextWriterTag TagKey => HtmlTextWriterTag.Div;

    private static RegexStrategy RgxStrategy
    {
      get
      {
        if (LibraryRelocateDialog.regexStrategy == null)
          LibraryRelocateDialog.regexStrategy = ObjectFactory.Resolve<RegexStrategy>();
        return LibraryRelocateDialog.regexStrategy;
      }
    }
  }
}
