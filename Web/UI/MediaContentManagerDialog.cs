// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.MediaContentManagerDialog
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Libraries.Model;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Modules.Libraries;
using Telerik.Sitefinity.Modules.Libraries.Configuration;
using Telerik.Sitefinity.Modules.Libraries.Documents;
using Telerik.Sitefinity.Modules.Libraries.Images;
using Telerik.Sitefinity.Modules.Libraries.Videos;
using Telerik.Sitefinity.Modules.Libraries.Web.UI.Designers;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Web.UI.Fields;

namespace Telerik.Sitefinity.Web.UI
{
  /// <summary>
  /// A dialog for selecting/uploading and managing a single media content item.
  /// </summary>
  public class MediaContentManagerDialog : AjaxDialogBase
  {
    private const string DialogScript = "Telerik.Sitefinity.Web.Scripts.MediaContentManagerDialog.js";
    private readonly string layoutTemplatePath = ControlUtilities.ToVppPath("Telerik.Sitefinity.Resources.Templates.Backend.MediaContentManagerDialog.ascx");
    private string mediaItemTypeName;

    /// <summary>Gets or sets the layout template path.</summary>
    /// <value>The layout template path.</value>
    public override string LayoutTemplatePath
    {
      get => string.IsNullOrEmpty(base.LayoutTemplatePath) ? this.layoutTemplatePath : base.LayoutTemplatePath;
      set => base.LayoutTemplatePath = value;
    }

    /// <summary>Gets or sets the dialog mode.</summary>
    /// <value>The dialog mode.</value>
    public EditorExternalDialogModes DialogMode { get; set; }

    /// <summary>Gets the name of the media item type.</summary>
    /// <value>The name of the media item type.</value>
    public string MediaItemTypeName
    {
      get
      {
        if (this.mediaItemTypeName == null)
        {
          switch (this.DialogMode)
          {
            case EditorExternalDialogModes.NotSet:
            case EditorExternalDialogModes.Image:
              this.mediaItemTypeName = typeof (Telerik.Sitefinity.Libraries.Model.Image).FullName;
              break;
            case EditorExternalDialogModes.Document:
              this.mediaItemTypeName = typeof (Document).FullName;
              break;
            case EditorExternalDialogModes.Media:
              this.mediaItemTypeName = typeof (Video).FullName;
              break;
          }
        }
        return this.mediaItemTypeName;
      }
    }

    /// <summary>Gets or sets the thumbnail extension prefix.</summary>
    /// <value>The thumbnail extension prefix.</value>
    public string ThumbnailExtensionPrefix { get; set; }

    /// <summary>
    /// Gets the reference to the control for selecting/uploading and managing the selected media content item.
    /// </summary>
    /// <value>The content selector view.</value>
    protected internal virtual SingleMediaContentItemView ContentSelectorView => this.Container.GetControl<SingleMediaContentItemView>("contentSelectorView", true);

    /// <summary>
    /// Gets the reference to the control for setting the display properties of the selected media content item.
    /// </summary>
    /// <value>The item settings view.</value>
    protected internal virtual SingleMediaContentItemSettingsView ItemSettingsView => this.Container.GetControl<SingleMediaContentItemSettingsView>("itemSettingsView", true);

    /// <summary>Gets the reference to the title label.</summary>
    /// <value>The title label.</value>
    protected virtual ITextControl TitleLabel => this.Container.GetControl<ITextControl>("titleLabel", true);

    /// <summary>Gets the reference to the button area.</summary>
    /// <value>The button area.</value>
    protected virtual Control ButtonArea => this.Container.GetControl<Control>("buttonArea", true);

    /// <summary>Gets the reference to the save link button.</summary>
    protected virtual HyperLink SaveLink => this.Container.GetControl<HyperLink>("saveLink", true);

    /// <summary>Gets the reference to the cancel link button.</summary>
    /// <value>The cancel link button.</value>
    protected virtual HyperLink CancelLink => this.Container.GetControl<HyperLink>("cancelLink", true);

    /// <summary>
    /// Represents the textbox containing the AlternativeText to be set to the image.
    /// </summary>
    protected virtual TextField AltTextField => this.Container.GetControl<TextField>("altTextField", true);

    /// <summary>Gets the title text field.</summary>
    /// <value>The title text field.</value>
    protected virtual TextField TitleTextField => this.Container.GetControl<TextField>("titleTextField", true);

    /// <inheritdoc />
    protected override void InitializeControls(GenericContainer container)
    {
      this.ContentSelectorView.ProviderName = "";
      this.InitializeDialogMode();
      this.ContentSelectorView.MediaItemTypeName = this.MediaItemTypeName;
      this.ItemSettingsView.MediaItemTypeName = this.MediaItemTypeName;
      this.ItemSettingsView.DisplayResizingOptionsControl = true;
      this.ThumbnailExtensionPrefix = Config.Get<LibrariesConfig>().ThumbnailExtensionPrefix;
      string str;
      switch (this.DialogMode)
      {
        case EditorExternalDialogModes.Document:
          str = Res.Get<DocumentsResources>().DocumentWithArticle;
          break;
        case EditorExternalDialogModes.Media:
          str = Res.Get<VideosResources>().VideoWithArticle;
          break;
        default:
          str = Res.Get<ImagesResources>().ImageWithArticle;
          break;
      }
      this.TitleLabel.Text = string.Format(Res.Get<LibrariesResources>().InsertAItem, (object) str);
    }

    /// <inheritdoc />
    protected override HtmlTextWriterTag TagKey => HtmlTextWriterTag.Div;

    /// <inheritdoc />
    public override IEnumerable<ScriptDescriptor> GetScriptDescriptors()
    {
      ScriptControlDescriptor controlDescriptor = (ScriptControlDescriptor) base.GetScriptDescriptors().Last<ScriptDescriptor>();
      controlDescriptor.Type = this.GetType().FullName;
      controlDescriptor.AddComponentProperty("contentSelectorView", this.ContentSelectorView.ClientID);
      controlDescriptor.AddComponentProperty("itemSettingsView", this.ItemSettingsView.ClientID);
      controlDescriptor.AddElementProperty("saveLink", this.SaveLink.ClientID);
      controlDescriptor.AddElementProperty("cancelLink", this.CancelLink.ClientID);
      controlDescriptor.AddElementProperty("buttonArea", this.ButtonArea.ClientID);
      controlDescriptor.AddProperty("dialogMode", (object) this.DialogMode);
      controlDescriptor.AddProperty("videoTag", (object) Config.Get<LibrariesConfig>().ContentBlockVideoTag.ToString());
      controlDescriptor.AddComponentProperty("titleTextField", this.TitleTextField.ClientID);
      controlDescriptor.AddComponentProperty("altTextField", this.AltTextField.ClientID);
      string virtualPath = string.Format("~/{0}", (object) "Sitefinity/Services/ThumbnailService.svc");
      controlDescriptor.AddProperty("thumbnailServiceUrl", (object) VirtualPathUtility.ToAbsolute(virtualPath));
      controlDescriptor.AddProperty("thumbnailExtensionPrefix", (object) this.ThumbnailExtensionPrefix);
      return (IEnumerable<ScriptDescriptor>) new ScriptControlDescriptor[1]
      {
        controlDescriptor
      };
    }

    /// <inheritdoc />
    public override IEnumerable<ScriptReference> GetScriptReferences() => (IEnumerable<ScriptReference>) new List<ScriptReference>(base.GetScriptReferences())
    {
      new ScriptReference("Telerik.Sitefinity.Web.Scripts.MediaContentManagerDialog.js", typeof (MediaContentManagerDialog).Assembly.FullName)
    };

    private void InitializeDialogMode()
    {
      if (this.DialogMode != EditorExternalDialogModes.NotSet)
        return;
      string str1 = SystemManager.CurrentHttpContext.Request.QueryString["mode"];
      this.DialogMode = !string.IsNullOrEmpty(str1) ? (EditorExternalDialogModes) Enum.Parse(typeof (EditorExternalDialogModes), str1, true) : throw new ArgumentException("No dialog mode parameter specified. Use one of the Telerik.Sitefinity.Web.UI.SingleMediaContentItemDialog.EditorExternalDialogModes as 'mode' query string parameter.");
      if (this.DialogMode == EditorExternalDialogModes.NotSet)
        throw new ArgumentOutOfRangeException("Invalid mode dialog parameter specified - " + (object) this.DialogMode);
      string str2 = SystemManager.CurrentHttpContext.Request.QueryString["dialogOpenMode"];
    }
  }
}
