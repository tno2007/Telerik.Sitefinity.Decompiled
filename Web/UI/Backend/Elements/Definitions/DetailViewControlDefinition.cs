// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.Backend.Elements.Definitions.DetailViewControlDefinition
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Linq;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Web.UI.Backend.Elements.Config;
using Telerik.Sitefinity.Web.UI.Backend.Elements.Contracts;
using Telerik.Sitefinity.Web.UI.ContentUI.Config;
using Telerik.Sitefinity.Web.UI.ContentUI.Contracts;
using Telerik.Sitefinity.Web.UI.Definitions;

namespace Telerik.Sitefinity.Web.UI.Backend.Elements.Definitions
{
  internal class DetailViewControlDefinition : 
    ViewDefinition,
    IDetailViewControlDefinition,
    IViewDefinition,
    IDefinition
  {
    private string sectionCssClass;
    private List<IContentViewSectionDefinition> sections;
    private bool? showSections;
    private IWidgetBarDefinition toolbar;
    private IWidgetBarDefinition sidebar;

    /// <summary>
    /// Initializes a new instance of the <see cref="!:CommentsMasterViewDefinition" /> class.
    /// </summary>
    public DetailViewControlDefinition()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="!:CommentsMasterViewDefinition" /> class.
    /// </summary>
    /// <param name="configDefinition">The config definition.</param>
    public DetailViewControlDefinition(ConfigElement configDefinition)
      : base(configDefinition)
    {
    }

    /// <summary>Gets the definition.</summary>
    /// <returns></returns>
    public override DefinitionBase GetDefinition() => (DefinitionBase) this;

    /// <summary>Defines the name of the CSS class for all sections.</summary>
    /// <value></value>
    public string SectionCssClass
    {
      get => this.ResolveProperty<string>(nameof (SectionCssClass), this.sectionCssClass);
      set => this.sectionCssClass = value;
    }

    /// <summary>
    /// Gets the <see cref="T:Telerik.Sitefinity.Web.UI.ContentUI.Contracts.IContentViewSectionDefinition" /> sections.
    /// </summary>
    /// <value></value>
    public virtual List<IContentViewSectionDefinition> Sections
    {
      get
      {
        if (this.sections == null && this.ConfigDefinition != null)
          this.sections = ((DetailViewControlElement) this.ConfigDefinition).Sections.Elements.Select<ContentViewSectionElement, IContentViewSectionDefinition>((Func<ContentViewSectionElement, IContentViewSectionDefinition>) (s => (IContentViewSectionDefinition) s.GetDefinition())).ToList<IContentViewSectionDefinition>();
        return this.sections;
      }
    }

    /// <summary>Gets or sets whether to show sections.</summary>
    /// <value></value>
    public bool? ShowSections
    {
      get => this.ResolveProperty<bool?>(nameof (ShowSections), this.showSections);
      set => this.showSections = value;
    }

    IEnumerable<IContentViewSectionDefinition> IDetailViewControlDefinition.Sections => (IEnumerable<IContentViewSectionDefinition>) this.Sections;

    /// <summary>Gets the definitions to be display the toolbar.</summary>
    /// <value></value>
    public IWidgetBarDefinition Toolbar => this.ResolveProperty<IWidgetBarDefinition>(nameof (Toolbar), this.toolbar);

    /// <summary>Gets the definitions to be display the sidebar.</summary>
    /// <value></value>
    public IWidgetBarDefinition Sidebar => this.ResolveProperty<IWidgetBarDefinition>(nameof (Sidebar), this.sidebar);
  }
}
