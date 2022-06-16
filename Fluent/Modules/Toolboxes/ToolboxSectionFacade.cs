// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Fluent.Modules.Toolboxes.ToolboxSectionFacade`1
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Linq;
using System.Web.UI;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Modules.Pages.Configuration;

namespace Telerik.Sitefinity.Fluent.Modules.Toolboxes
{
  /// <summary>Fluent API facade for working with toolbox section.</summary>
  public class ToolboxSectionFacade<TToolboxFacade>
  {
    private string moduleName;
    private ToolboxSection toolboxSection;
    private TToolboxFacade parentFacade;

    /// <summary>
    /// Creates a new instance of the <see cref="!:ToolboxSectionFacade" /> with specified toolbox section
    /// and parent facade.
    /// </summary>
    /// <param name="toolboxSection">
    /// An instance of the <see cref="T:Telerik.Sitefinity.Modules.Pages.Configuration.ToolboxSection" /> for which the facade is initialized.
    /// </param>
    /// <param name="moduleName">
    /// The name of the module for which the toolbox is being configured.
    /// </param>
    /// <param name="parentFacade">
    /// An instance of the <see cref="T:Telerik.Sitefinity.Fluent.Modules.Toolboxes.ToolboxInitializeFacade" /> that loaded this facade.
    /// </param>
    public ToolboxSectionFacade(
      ToolboxSection toolboxSection,
      string moduleName,
      TToolboxFacade parentFacade)
    {
      if (toolboxSection == null)
        throw new ArgumentNullException(nameof (toolboxSection));
      if ((object) parentFacade == null)
        throw new ArgumentNullException(nameof (parentFacade));
      if (string.IsNullOrEmpty(moduleName))
        throw new ArgumentNullException(nameof (moduleName));
      this.toolboxSection = toolboxSection;
      this.moduleName = moduleName;
      this.parentFacade = parentFacade;
    }

    /// <summary>
    /// Sets the localization class for the toolbox section that should be used
    /// for localizing the properties. When this method is called, all properties
    /// will start to behave as resource keys.
    /// </summary>
    /// <typeparam name="TResourceClass">
    /// The type of the class which should be used to localize the string properties of the toolbox section.
    /// </typeparam>
    /// <returns>An instance of the current <see cref="!:ToolboxSectionFacade" />.</returns>
    public ToolboxSectionFacade<TToolboxFacade> LocalizeUsing<TResourceClass>() where TResourceClass : Resource
    {
      this.toolboxSection.ResourceClassId = typeof (TResourceClass).Name;
      return this;
    }

    /// <summary>
    /// Sets the title of the toolbox section. If you have called LocalizeUsing method,
    /// this is the key of the localization resource; otherwise this is the actual title.
    /// </summary>
    /// <param name="sectionTitle">Title or the resource key of the title when localization is used.</param>
    /// <returns>An instance of the current <see cref="!:ToolboxSectionFacade" />.</returns>
    public ToolboxSectionFacade<TToolboxFacade> SetTitle(string sectionTitle)
    {
      this.toolboxSection.Title = !string.IsNullOrEmpty(sectionTitle) ? sectionTitle : throw new ArgumentNullException(nameof (sectionTitle));
      return this;
    }

    /// <summary>
    /// Sets the description of the toolbox section. If you have called LocalizeUsing method,
    /// this is the key of the localization resource; otherwise this is the actual description.
    /// </summary>
    /// <param name="sectionDescription">
    /// Description or the resource key of the description when localization is used.
    /// </param>
    /// <returns>An instance of the current <see cref="!:ToolboxSectionFacade" />.</returns>
    public ToolboxSectionFacade<TToolboxFacade> SetDescription(
      string sectionDescription)
    {
      this.toolboxSection.Description = !string.IsNullOrEmpty(sectionDescription) ? sectionDescription : throw new ArgumentNullException(nameof (sectionDescription));
      return this;
    }

    /// <summary>Sets the ordinal number of the toolbox section.</summary>
    /// <param name="ordinal">The relative position of the section.</param>
    /// <returns>An instance of the current <see cref="!:ToolboxSectionFacade" />.</returns>
    public ToolboxSectionFacade<TToolboxFacade> SetOrdinal(float ordinal)
    {
      this.toolboxSection.Ordinal = ordinal;
      return this;
    }

    /// <summary>
    /// Sets the tags (comma separated string) of the toolbox section.
    /// </summary>
    /// <param name="sectionTags">
    /// The tags (comma separated string) for this section.
    /// </param>
    /// <returns>An instance of the current <see cref="!:ToolboxSectionFacade" />.</returns>
    public ToolboxSectionFacade<TToolboxFacade> SetTags(string sectionTags)
    {
      this.toolboxSection.Tags = sectionTags;
      return this;
    }

    /// <summary>
    /// Loads the toolbox item from the current toolbox section by the specified widget name; if the
    /// toolbox item does not exist, creates a new toolbox item.
    /// </summary>
    /// <param name="widgetName">Name of the toolbox item to load or add.</param>
    /// <returns>A new instance of the <see cref="!:ToolboxItemFacade" />.</returns>
    public ToolboxItemFacade<ToolboxSectionFacade<TToolboxFacade>> LoadOrAddWidget<TWidget>(
      string widgetName)
      where TWidget : Control
    {
      return this.LoadOrAddWidget<TWidget, ToolboxSectionFacade<TToolboxFacade>>(widgetName, this);
    }

    internal ToolboxItemFacade<TParentFacade> LoadOrAddWidget<TWidget, TParentFacade>(
      string widgetName,
      TParentFacade parentFacade)
      where TWidget : Control
    {
      if (string.IsNullOrEmpty(widgetName))
        throw new ArgumentNullException(nameof (widgetName));
      ToolboxItem toolboxItem = this.toolboxSection.Tools.Where<ToolboxItem>((Func<ToolboxItem, bool>) (t => t.Name == widgetName)).SingleOrDefault<ToolboxItem>();
      if (toolboxItem == null)
      {
        toolboxItem = new ToolboxItem((ConfigElement) this.toolboxSection.Tools);
        toolboxItem.Name = widgetName;
        this.toolboxSection.Tools.Add(toolboxItem);
      }
      else if (toolboxItem.ModuleName != this.moduleName && !toolboxItem.ControlType.StartsWith(typeof (TWidget).FullName))
      {
        toolboxItem = new ToolboxItem((ConfigElement) this.toolboxSection.Tools);
        toolboxItem.Name = widgetName + "_of_" + this.moduleName;
        this.toolboxSection.Tools.Add(toolboxItem);
      }
      toolboxItem.ControlType = typeof (TWidget).AssemblyQualifiedName;
      toolboxItem.ModuleName = this.moduleName;
      return new ToolboxItemFacade<TParentFacade>(toolboxItem, parentFacade);
    }

    /// <summary>Returns to the parent facade.</summary>
    /// <returns>The instance of <see cref="T:Telerik.Sitefinity.Fluent.Modules.Toolboxes.ToolboxInitializeFacade" />.</returns>
    public TToolboxFacade Done() => this.parentFacade;
  }
}
