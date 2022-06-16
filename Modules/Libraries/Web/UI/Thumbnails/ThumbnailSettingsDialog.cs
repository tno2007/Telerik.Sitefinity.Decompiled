// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Libraries.Web.UI.Thumbnails.ThumbnailSettingsDialog
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Telerik.Sitefinity.Libraries.Model;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Web.UI;
using Telerik.Sitefinity.Web.UI.Fields.Enums;

namespace Telerik.Sitefinity.Modules.Libraries.Web.UI.Thumbnails
{
  public class ThumbnailSettingsDialog : AjaxDialogBase
  {
    internal const string viewScript = "Telerik.Sitefinity.Modules.Libraries.Web.UI.Thumbnails.Scripts.ThumbnailSettingsDialog.js";
    internal static readonly string layoutTemplatePath = ControlUtilities.ToVppPath("Telerik.Sitefinity.Resources.Templates.Backend.Libraries.Thumbnails.ThumbnailSettingsDialog.ascx");

    /// <inheritdoc />
    public override string LayoutTemplatePath
    {
      get => string.IsNullOrEmpty(base.LayoutTemplatePath) ? ThumbnailSettingsDialog.layoutTemplatePath : base.LayoutTemplatePath;
      set => base.LayoutTemplatePath = value;
    }

    /// <inheritdoc />
    public override string ClientComponentType => typeof (ThumbnailSettingsDialog).FullName;

    /// <summary>
    /// Gets the reference to the <see cref="T:System.Web.UI.HtmlControls.HtmlContainerControl" /> that is used
    /// for confirming the dialog changes.
    /// </summary>
    protected virtual HtmlContainerControl ButtonDone => this.Container.GetControl<HtmlContainerControl>("buttonDone", true);

    /// <summary>
    /// Gets the reference to the <see cref="T:System.Web.UI.HtmlControls.HtmlContainerControl" /> that is used
    /// to close the dialog.
    /// </summary>
    protected virtual HtmlContainerControl ButtonCancel => this.Container.GetControl<HtmlContainerControl>("buttonCancel", true);

    /// <summary>
    /// Gets the reference to the <see cref="T:System.Web.UI.HtmlControls.HtmlContainerControl" /> that is used
    /// for confirming the thumbnail action.
    /// </summary>
    protected virtual HtmlContainerControl SecondaryButtonDone => this.Container.GetControl<HtmlContainerControl>("secondaryButtonDone", true);

    /// <summary>
    /// Gets the reference to the <see cref="T:System.Web.UI.HtmlControls.HtmlContainerControl" /> that is used
    /// to cancel the thumbnail action.
    /// </summary>
    protected virtual HtmlContainerControl SecondaryButtonCancel => this.Container.GetControl<HtmlContainerControl>("secondaryButtonCancel", true);

    /// <summary>
    /// Gets the reference to the <see cref="T:System.Web.UI.WebControls.Literal" /> that is used
    /// for setting the title of the dialog.
    /// </summary>
    protected virtual Literal LiteralDialogTitle => this.Container.GetControl<Literal>("dialogTitle", true);

    /// <summary>
    /// Gets the reference to the <see cref="T:System.Web.UI.WebControls.Literal" /> that is used
    /// for selecting the profiles of the current library.
    /// </summary>
    protected virtual ThumbnailProfileField ThumbnailProfileField => this.Container.GetControl<ThumbnailProfileField>("thumbnailProfileField", true);

    /// <summary>Gets a reference to the label manager.</summary>
    protected virtual ClientLabelManager LabelManager => this.Container.GetControl<ClientLabelManager>("labelManager", true);

    /// <summary>Gets the warning message label.</summary>
    protected virtual Label LabelWarningMessage => this.Container.GetControl<Label>("labelWarningMessage", true);

    /// <inheritdoc />
    protected override void InitializeControls(GenericContainer container)
    {
      NameValueCollection queryString = SystemManager.CurrentHttpContext.Request.QueryString;
      if (queryString["libraryType"] == typeof (Album).FullName)
        this.LiteralDialogTitle.Text = Res.Get<LibrariesResources>().ThumbnailsOfImages;
      this.ThumbnailProfileField.LibraryType = queryString["libraryType"];
      this.ThumbnailProfileField.ThumbnailSettingsServiceUrl = string.Format("~/{0}/thumbnail-profiles/", (object) "Sitefinity/Services/ThumbnailService.svc");
      this.ThumbnailProfileField.DisplayMode = FieldDisplayMode.Write;
    }

    /// <inheritdoc />
    public override IEnumerable<ScriptDescriptor> GetScriptDescriptors()
    {
      ScriptControlDescriptor controlDescriptor = base.GetScriptDescriptors().Last<ScriptDescriptor>() as ScriptControlDescriptor;
      string virtualPath = string.Format("~/{0}", (object) "Sitefinity/Services/ThumbnailService.svc");
      controlDescriptor.AddProperty("thumbnailServiceUrl", (object) VirtualPathUtility.ToAbsolute(virtualPath));
      controlDescriptor.AddElementProperty("buttonDone", this.ButtonDone.ClientID);
      controlDescriptor.AddElementProperty("buttonCancel", this.ButtonCancel.ClientID);
      controlDescriptor.AddElementProperty("secondaryButtonDone", this.SecondaryButtonDone.ClientID);
      controlDescriptor.AddElementProperty("secondaryButtonCancel", this.SecondaryButtonCancel.ClientID);
      controlDescriptor.AddComponentProperty("thumbnailProfileField", this.ThumbnailProfileField.ClientID);
      controlDescriptor.AddComponentProperty("labelManager", this.LabelManager.ClientID);
      controlDescriptor.AddElementProperty("labelWarningMessage", this.LabelWarningMessage.ClientID);
      return (IEnumerable<ScriptDescriptor>) new ScriptDescriptor[1]
      {
        (ScriptDescriptor) controlDescriptor
      };
    }

    /// <inheritdoc />
    public override IEnumerable<ScriptReference> GetScriptReferences()
    {
      string fullName = typeof (ThumbnailSettingsDialog).Assembly.FullName;
      return (IEnumerable<ScriptReference>) new List<ScriptReference>(base.GetScriptReferences())
      {
        new ScriptReference("Telerik.Sitefinity.Modules.Libraries.Web.UI.Thumbnails.Scripts.ThumbnailSettingsDialog.js", fullName)
      };
    }

    protected override HtmlTextWriterTag TagKey => HtmlTextWriterTag.Div;
  }
}
