// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Pages.Web.UI.PagesPanel
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Web.UI;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Web.UI.Backend;

namespace Telerik.Sitefinity.Modules.Pages.Web.UI
{
  /// <summary>
  /// 
  /// </summary>
  public class PagesPanel : ProviderControlPanel<Page>
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Modules.Pages.Web.UI.PagesPanel" /> class.
    /// </summary>
    public PagesPanel()
      : base(true)
    {
      this.ResourceClassId = typeof (PageResources).Name;
    }

    /// <summary>The title of the view.</summary>
    /// <value></value>
    public override string Title
    {
      get => this.CurrentView.Title;
      set => base.Title = value;
    }

    /// <summary>Loads configured views.</summary>
    protected override void CreateViews()
    {
      this.AddView<PagesView>("Pages", Res.Get<PageResources>().Pages, (string) null, (string) null);
      this.AddView<TemplatesView>("PageTemplates", Res.Get<PageResources>().Templates, (string) null, (string) null);
      this.AddView<BackendPagesView>("BackendPages", Res.Get<PageResources>().BackendPagesTitle, (string) null, (string) null);
      this.AddView<BackendPagesWarningView>("BackendPagesWarning", Res.Get<PageResources>().BackendPagesWarningTitle, (string) null, (string) null);
      this.AddView<BackendTemplatesView>("BackendTemplates", Res.Get<PageResources>().BackendTemplates, (string) null, (string) null);
    }

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
