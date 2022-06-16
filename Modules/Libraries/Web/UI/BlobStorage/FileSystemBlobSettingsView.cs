// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Libraries.Web.UI.BlobStorage.FileSystemBlobSettingsView
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Collections.Specialized;
using Telerik.Sitefinity.Web.UI;
using Telerik.Sitefinity.Web.UI.Fields;

namespace Telerik.Sitefinity.Modules.Libraries.Web.UI.BlobStorage
{
  public class FileSystemBlobSettingsView : AjaxDialogBase, IBlobSettingsView
  {
    private NameValueCollection settings;
    private bool IsInitialized;
    public static readonly string layoutTemplatePath = ControlUtilities.ToVppPath("Telerik.Sitefinity.Resources.Templates.Backend.Configuration.Basic.FileSystemBlobSettingsView.ascx");

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Modules.Libraries.Web.UI.BlobStorage.FileSystemBlobSettingsView" /> class.
    /// </summary>
    public FileSystemBlobSettingsView() => this.LayoutTemplatePath = FileSystemBlobSettingsView.layoutTemplatePath;

    /// <inheritdoc />
    protected override string LayoutTemplateName => (string) null;

    public TextField FolderName => this.Container.GetControl<TextField>("folderName", true);

    /// <inheritdoc />
    protected override void InitializeControls(GenericContainer container)
    {
      if (this.settings != null)
      {
        this.Settings = this.settings;
        this.settings = (NameValueCollection) null;
      }
      this.IsInitialized = true;
    }

    public NameValueCollection Settings
    {
      get => new NameValueCollection()
      {
        {
          "storageFolder",
          this.FolderName.Value as string
        }
      };
      set
      {
        if (!this.IsInitialized)
          this.settings = value;
        if (value["storageFolder"] == null)
          return;
        this.FolderName.Value = (object) value["storageFolder"];
      }
    }
  }
}
