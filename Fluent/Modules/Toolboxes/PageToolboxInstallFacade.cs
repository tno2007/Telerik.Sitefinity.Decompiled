// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Fluent.Modules.Toolboxes.PageToolboxInstallFacade
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using Telerik.Sitefinity.Modules.Pages;

namespace Telerik.Sitefinity.Fluent.Modules.Toolboxes
{
  public class PageToolboxInstallFacade : ToolboxInstallFacade
  {
    public PageToolboxInstallFacade(string moduleName, ModuleInstallFacade parentFacade)
      : base(CommonToolbox.PageWidgets, moduleName, parentFacade)
    {
    }

    public ToolboxSectionFacade<PageToolboxInstallFacade> ContentSection() => this.LoadOrAddSection<PageToolboxInstallFacade>("ContentToolboxSection", this).SetTitle("ContentToolboxSectionTitle").SetDescription("ContentToolboxSectionDescription").LocalizeUsing<PageResources>();

    public ToolboxSectionFacade<PageToolboxInstallFacade> NavigationControlsSection() => this.LoadOrAddSection<PageToolboxInstallFacade>(nameof (NavigationControlsSection), this).SetTitle("NavigationControlsSectionTitle").SetDescription("NavigationControlsSectionDescription").LocalizeUsing<PageResources>();
  }
}
