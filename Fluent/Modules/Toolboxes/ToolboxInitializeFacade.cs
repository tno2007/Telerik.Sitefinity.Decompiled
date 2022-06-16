// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Fluent.Modules.Toolboxes.ToolboxInitializeFacade
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Modules.Pages.Configuration;

namespace Telerik.Sitefinity.Fluent.Modules.Toolboxes
{
  /// <summary>
  /// This facade provides functionality for initializing various Sitefinity toolboxes.
  /// </summary>
  public class ToolboxInitializeFacade : ToolboxFacade<ModuleInitializeFacade>
  {
    /// <summary>
    /// Creates a new instance of <see cref="T:Telerik.Sitefinity.Fluent.Modules.Toolboxes.ToolboxInitializeFacade" /> with the common toolbox.
    /// </summary>
    /// <param name="targetToolbox">
    /// The common toolbox for which the toolbox fluent API should be initialized.
    /// </param>
    /// <param name="moduleName">
    /// Name of the module for which the toolbox is to be initialized.
    /// </param>
    /// <param name="parentFacade">
    /// The instance of <see cref="T:Telerik.Sitefinity.Fluent.Modules.ModuleInitializeFacade" /> that instantiated this facade.
    /// </param>
    public ToolboxInitializeFacade(
      CommonToolbox targetToolbox,
      string moduleName,
      ModuleInitializeFacade parentFacade)
      : base(targetToolbox, moduleName, parentFacade)
    {
    }

    /// <summary>
    /// Creates a new instance of <see cref="T:Telerik.Sitefinity.Fluent.Modules.Toolboxes.ToolboxInitializeFacade" /> with the specified toolbox name.
    /// </summary>
    /// <param name="targetToolboxName">
    /// Name of the toolbox for which the toolbox fluent API should be initialized.
    /// </param>
    /// <param name="moduleName">
    /// Name of the module for which the toolbox is to be initialized.
    /// </param>
    /// <param name="parentFacade">
    /// The instance of <see cref="T:Telerik.Sitefinity.Fluent.Modules.ModuleInitializeFacade" /> that instantiated this facade.
    /// </param>
    public ToolboxInitializeFacade(
      string targetToolboxName,
      string moduleName,
      ModuleInitializeFacade parentFacade)
      : base(targetToolboxName, moduleName, parentFacade)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Fluent.Modules.Toolboxes.ToolboxInitializeFacade" /> class.
    /// </summary>
    /// <param name="toolbox">The toolbox.</param>
    /// <param name="moduleName">Name of the module.</param>
    /// <param name="parentFacade">The parent facade.</param>
    public ToolboxInitializeFacade(
      Toolbox toolbox,
      string moduleName,
      ModuleInitializeFacade parentFacade)
      : base(toolbox, moduleName, parentFacade)
    {
    }

    protected override Toolbox GetToolbox(string toolboxName) => Config.Get<ToolboxesConfig>().Toolboxes[toolboxName];
  }
}
