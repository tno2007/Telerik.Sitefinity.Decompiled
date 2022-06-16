// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.Backend.Elements.Config.DetailViewControlElement
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Runtime.InteropServices;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Web.Configuration;
using Telerik.Sitefinity.Web.UI.Backend.Elements.Contracts;
using Telerik.Sitefinity.Web.UI.Backend.Elements.Definitions;
using Telerik.Sitefinity.Web.UI.ContentUI.Config;
using Telerik.Sitefinity.Web.UI.ContentUI.Contracts;
using Telerik.Sitefinity.Web.UI.Definitions;

namespace Telerik.Sitefinity.Web.UI.Backend.Elements.Config
{
  internal class DetailViewControlElement : 
    ViewDefinitionElement,
    IDetailViewControlDefinition,
    IViewDefinition,
    IDefinition
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="!:CommentsMasterViewElement" /> class.
    /// </summary>
    /// <param name="parent">The parent element.</param>
    public DetailViewControlElement(ConfigElement parent)
      : base(parent)
    {
    }

    /// <summary>Gets the toolbar.</summary>
    /// <value>The toolbar.</value>
    [ConfigurationProperty("toolbar")]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "ToolbarDescription", Title = "ToolbarCaption")]
    public WidgetBarElement ToolbarConfig => (WidgetBarElement) this["toolbar"];

    /// <summary>Gets the sidebar.</summary>
    /// <value>The sidebar.</value>
    [ConfigurationProperty("sidebar")]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "SidebarDescription", Title = "SidebarCaption")]
    public WidgetBarElement SidebarConfig => (WidgetBarElement) this["sidebar"];

    /// <summary>Gets or sets the sections config.</summary>
    /// <value>The sections config.</value>
    [ConfigurationProperty("sections")]
    [ConfigurationCollection(typeof (ContentViewSectionElement), AddItemName = "sections")]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "SectionsConfigDescription", Title = "SectionsConfigCaption")]
    public ConfigElementDictionary<string, ContentViewSectionElement> Sections => (ConfigElementDictionary<string, ContentViewSectionElement>) this["sections"];

    /// <summary>Defines the name of the CSS class for all sections.</summary>
    /// <value></value>
    [ConfigurationProperty("sectionCssClass", DefaultValue = "", IsRequired = false)]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "SectionCssClassDescription", Title = "SectionCssClassCaption")]
    public string SectionCssClass
    {
      get => (string) this["sectionCssClass"];
      set => this["sectionCssClass"] = (object) value;
    }

    /// <summary>Gets or sets weather to show sections.</summary>
    /// <value></value>
    [ConfigurationProperty("showSections", DefaultValue = true, IsRequired = false)]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "ShowSectionsDescription", Title = "ShowSectionsCaption")]
    public bool? ShowSections
    {
      get => (bool?) this["showSections"];
      set => this["showSections"] = (object) value;
    }

    IEnumerable<IContentViewSectionDefinition> IDetailViewControlDefinition.Sections => this.Sections.Cast<IContentViewSectionDefinition>();

    public IWidgetBarDefinition Toolbar => (IWidgetBarDefinition) this.ToolbarConfig;

    public IWidgetBarDefinition Sidebar => (IWidgetBarDefinition) this.SidebarConfig;

    public override DefinitionBase GetDefinition() => (DefinitionBase) new DetailViewControlDefinition((ConfigElement) this);

    [StructLayout(LayoutKind.Sequential, Size = 1)]
    internal struct DetailProps
    {
      public const string showSections = "showSections";
      public const string sectionCssClass = "sectionCssClass";
      public const string sectionsConfig = "sections";
      public const string toolbar = "toolbar";
      public const string sidebar = "sidebar";
    }
  }
}
