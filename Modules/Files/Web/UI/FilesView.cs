// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Files.Web.UI.FilesView
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Web.UI;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Web.UI;
using Telerik.Web.UI.FileExplorer;

namespace Telerik.Sitefinity.Modules.Files.Web.UI
{
  /// <summary>Represents a view for displaying File Manager.</summary>
  public class FilesView : ViewModeControl<FilesPanel>
  {
    /// <summary>
    /// Gets the name of resource file representing Files View.
    /// </summary>
    public static readonly string FilesViewPath = ControlUtilities.ToVppPath("Telerik.Sitefinity.Resources.Templates.Backend.Files.FilesView.ascx");

    /// <summary>
    /// Gets the name of the embedded layout template. If the control uses layout template this
    /// property must be overridden to provide the path (key) to an embedded resource file.
    /// </summary>
    /// <value></value>
    protected override string LayoutTemplateName => (string) null;

    /// <summary>Gets or sets the layout template path.</summary>
    /// <value>The layout template path.</value>
    public override string LayoutTemplatePath
    {
      get => string.IsNullOrEmpty(base.LayoutTemplatePath) ? FilesView.FilesViewPath : base.LayoutTemplatePath;
      set => base.LayoutTemplatePath = value;
    }

    /// <summary>
    /// Gets the ASP.NET application's virtual application root path on the server.
    /// </summary>
    /// <value>The virtual path of the current application.</value>
    public string ApplicationPath => this.Context.Request.ApplicationPath;

    /// <summary>Gets the file manager.</summary>
    /// <value>The file manager.</value>
    protected virtual FileManager FileManagerControl => this.Container.GetControl<FileManager>("fileManager", true);

    /// <summary>
    /// Raises the <see cref="E:System.Web.UI.Control.Load" /> event.
    /// </summary>
    /// <param name="e">The <see cref="T:System.EventArgs" /> object that contains the event data.</param>
    protected override void OnLoad(EventArgs e)
    {
      base.OnLoad(e);
      this.EnsureChildControls();
    }

    /// <summary>
    /// Initializes all controls instantiated in the layout container. This method is called at appropriate time for setting initial values and subscribing for events of layout controls.
    /// </summary>
    /// <param name="viewContainer">The control that will host the current view.</param>
    protected override void InitializeControls(Control viewContainer)
    {
      base.InitializeControls(viewContainer);
      string[] strArray = new string[1]
      {
        this.ApplicationPath
      };
      this.FileManagerControl.Configuration.ContentProviderTypeName = typeof (FileProvider).AssemblyQualifiedName;
      this.FileManagerControl.Configuration.ViewPaths = strArray;
      this.FileManagerControl.Configuration.UploadPaths = strArray;
      this.FileManagerControl.Configuration.DeletePaths = strArray;
      this.FileManagerControl.InitialPath = this.ApplicationPath;
      this.FileManagerControl.VisibleControls = this.GetVisibleControls();
      this.FileManagerControl.EnableCopy = true;
      this.FileManagerControl.EnableOpenFile = false;
      this.FileManagerControl.Configuration.MaxUploadFileSize = Config.Get<SystemConfig>().FilesModuleConfig.MaxFileSize;
    }

    /// <summary>
    /// Gets a list with the visible controls in the file explorer.
    /// </summary>
    /// <returns></returns>
    private FileExplorerControls GetVisibleControls() => FileExplorerControls.TreeView | FileExplorerControls.Grid | FileExplorerControls.Toolbar | FileExplorerControls.ContextMenus;
  }
}
