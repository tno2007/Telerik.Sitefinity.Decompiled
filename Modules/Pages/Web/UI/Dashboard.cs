// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Pages.Web.UI.Dashboard
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Web.UI;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Web.UI.Backend;

namespace Telerik.Sitefinity.Modules.Pages.Web.UI
{
  public class Dashboard : ControlPanel<Page>
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Modules.Pages.Web.UI.Dashboard" /> class.
    /// </summary>
    public Dashboard()
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
    protected override void CreateViews() => this.AddView<DashboardView>(nameof (Dashboard), Res.Get<PageResources>().Dashboard, (string) null, (string) null);
  }
}
