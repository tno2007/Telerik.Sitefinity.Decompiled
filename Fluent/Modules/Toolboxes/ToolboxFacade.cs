// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Fluent.Modules.Toolboxes.ToolboxFacade`1
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using Telerik.Sitefinity.Modules.Pages.Configuration;

namespace Telerik.Sitefinity.Fluent.Modules.Toolboxes
{
  public abstract class ToolboxFacade<TModuleFacade>
  {
    private string moduleName;
    private string toolboxName;
    private TModuleFacade parentFacade;
    private Toolbox toolbox;

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
    public ToolboxFacade(
      CommonToolbox targetToolbox,
      string moduleName,
      TModuleFacade parentFacade)
    {
      if ((object) parentFacade == null)
        throw new ArgumentNullException(nameof (parentFacade));
      if (string.IsNullOrEmpty(moduleName))
        throw new ArgumentNullException(nameof (moduleName));
      this.ResolveToolboxName(targetToolbox);
      this.moduleName = moduleName;
      this.parentFacade = parentFacade;
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
    public ToolboxFacade(string targetToolboxName, string moduleName, TModuleFacade parentFacade)
    {
      if (string.IsNullOrEmpty(targetToolboxName))
        throw new ArgumentNullException(nameof (targetToolboxName));
      if ((object) parentFacade == null)
        throw new ArgumentNullException(nameof (parentFacade));
      if (string.IsNullOrEmpty(moduleName))
        throw new ArgumentNullException(nameof (moduleName));
      this.toolboxName = targetToolboxName;
      this.moduleName = moduleName;
      this.parentFacade = parentFacade;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Fluent.Modules.Toolboxes.ToolboxFacade`1" /> class.
    /// </summary>
    /// <param name="toolbox">The toolbox.</param>
    /// <param name="moduleName">Name of the module.</param>
    /// <param name="parentFacade">The parent facade.</param>
    public ToolboxFacade(Toolbox toolbox, string moduleName, TModuleFacade parentFacade)
    {
      if (toolbox == null)
        throw new ArgumentNullException(nameof (toolbox));
      if ((object) parentFacade == null)
        throw new ArgumentNullException(nameof (parentFacade));
      if (string.IsNullOrEmpty(moduleName))
        throw new ArgumentNullException(nameof (moduleName));
      this.toolbox = toolbox;
      this.moduleName = moduleName;
      this.parentFacade = parentFacade;
    }

    /// <summary>
    /// Loads the section from the current toolbox by the specified section name; if the
    /// section does not exist, creates a new section.
    /// </summary>
    /// <param name="sectionName">Name of the section to load or add.</param>
    /// <returns>A new instance of the <see cref="!:ToolboxSectionFacade" />.</returns>
    public ToolboxSectionFacade<ToolboxFacade<TModuleFacade>> LoadOrAddSection(
      string sectionName)
    {
      return this.LoadOrAddSection<ToolboxFacade<TModuleFacade>>(sectionName, this);
    }

    internal ToolboxSectionFacade<TParentFacade> LoadOrAddSection<TParentFacade>(
      string sectionName,
      TParentFacade parentFacade)
    {
      ToolboxSection toolboxSection = !string.IsNullOrEmpty(sectionName) ? this.Toolbox.GetSection(sectionName) : throw new ArgumentNullException(nameof (sectionName));
      if (toolboxSection == null)
      {
        toolboxSection = new ToolboxSection()
        {
          Name = sectionName
        };
        this.toolbox.Sections.Add(toolboxSection);
      }
      return new ToolboxSectionFacade<TParentFacade>(toolboxSection, this.moduleName, parentFacade);
    }

    /// <summary>Returns to parent facade.</summary>
    /// <returns>The parent facade which initialized this facade.</returns>
    public TModuleFacade Done() => this.parentFacade;

    internal TModuleFacade ModuleFacade => this.parentFacade;

    protected string ModuleName => this.moduleName;

    protected string ToolboxName => this.toolboxName;

    protected Toolbox Toolbox
    {
      get
      {
        if (this.toolbox == null)
          this.toolbox = this.GetToolbox(this.toolboxName);
        return this.toolbox;
      }
    }

    /// <summary>
    /// Gets the instance of <see cref="P:Telerik.Sitefinity.Fluent.Modules.Toolboxes.ToolboxFacade`1.Toolbox" />.
    /// </summary>
    /// <returns>An instance of <see cref="P:Telerik.Sitefinity.Fluent.Modules.Toolboxes.ToolboxFacade`1.Toolbox" />.</returns>
    protected abstract Toolbox GetToolbox(string toolboxName);

    private void ResolveToolboxName(CommonToolbox commonToolbox)
    {
      switch (commonToolbox)
      {
        case CommonToolbox.PageWidgets:
          this.toolboxName = "PageControls";
          break;
        case CommonToolbox.Layouts:
          this.toolboxName = "PageLayouts";
          break;
        case CommonToolbox.FormWidgets:
          this.toolboxName = "FormControls";
          break;
        case CommonToolbox.NewsletterWidgets:
          this.toolboxName = "NewsletterControls";
          break;
      }
    }
  }
}
