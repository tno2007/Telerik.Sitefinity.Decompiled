// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Files.Web.UI.FilesPanel
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Web.UI;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Web.UI.Backend;

namespace Telerik.Sitefinity.Modules.Files.Web.UI
{
  /// <summary>Represents Control Panel for managing files.</summary>
  public class FilesPanel : ProviderControlPanel<Page>
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Modules.Files.Web.UI.FilesPanel" /> class.
    /// </summary>
    public FilesPanel()
      : base(true)
    {
      this.ResourceClassId = typeof (FilesResources).Name;
      this.Title = Res.Get<FilesResources>().ModuleTitle;
    }

    /// <summary>Loads configured views.</summary>
    protected override void CreateViews() => this.AddView<FilesView>();

    /// <summary>
    /// Raises the <see cref="E:System.Web.UI.Control.Load" /> event.
    /// </summary>
    /// <param name="e">The <see cref="T:System.EventArgs" /> object that contains the event data.</param>
    protected override void OnLoad(EventArgs e)
    {
      base.OnLoad(e);
      this.EnsureChildControls();
    }
  }
}
