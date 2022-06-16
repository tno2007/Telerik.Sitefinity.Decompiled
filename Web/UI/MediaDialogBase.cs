// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.MediaDialogBase
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Linq;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Modules.Libraries;

namespace Telerik.Sitefinity.Web.UI
{
  /// <summary>Base class for media related dialogs.</summary>
  public abstract class MediaDialogBase : AjaxDialogBase
  {
    /// <summary>Gets the no libraries warning div.</summary>
    protected virtual HtmlGenericControl NoLibrariesWarning => this.Container.GetControl<HtmlGenericControl>("noLibrariesWarning", false);

    /// <summary>Gets the wrapper div.</summary>
    protected virtual HtmlGenericControl WrapperDiv => this.Container.GetControl<HtmlGenericControl>("editorContentManagerWrapper", false);

    /// <summary>Gets the no libraries warning label.</summary>
    protected virtual Label NoLibrariesWarningLabel => this.Container.GetControl<Label>("noLibrariesWarningLabel", false);

    /// <summary>Gets or sets the dialog mode.</summary>
    /// <value>The dialog mode.</value>
    public EditorExternalDialogModes DialogMode { get; set; }

    /// <summary>Gets the libraries manager.</summary>
    public abstract LibrariesManager LibrariesManager { get; }

    /// <summary>Initialize the libraries dialog.</summary>
    protected void InitializeNoLibrariesWarning()
    {
      if (this.NoLibrariesWarning == null)
        return;
      this.NoLibrariesWarning.Visible = this.LibrariesManager.GetContextProviders().Count<DataProviderBase>() == 0;
      if (!this.NoLibrariesWarning.Visible)
        return;
      if (this.WrapperDiv != null)
        this.WrapperDiv.Style.Add(HtmlTextWriterStyle.Display, "none");
      if (this.NoLibrariesWarningLabel == null)
        return;
      switch (this.DialogMode)
      {
        case EditorExternalDialogModes.Image:
          this.NoLibrariesWarningLabel.Text = Res.Get<LibrariesResources>().NoImageLibrariesWarningMessage;
          break;
        case EditorExternalDialogModes.Document:
          this.NoLibrariesWarningLabel.Text = Res.Get<LibrariesResources>().NoDocumentLibrariesWarningMessage;
          break;
        case EditorExternalDialogModes.Media:
          this.NoLibrariesWarningLabel.Text = Res.Get<LibrariesResources>().NoVideoLibrariesWarningMessage;
          break;
        default:
          this.NoLibrariesWarningLabel.Text = Res.Get<LibrariesResources>().NoLibrariesWarningMessage;
          break;
      }
    }
  }
}
