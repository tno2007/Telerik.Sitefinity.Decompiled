// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.ContentUI.ContentViewDetailDefinition
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Web.UI.ContentUI.Config;
using Telerik.Sitefinity.Web.UI.ContentUI.Contracts;
using Telerik.Sitefinity.Web.UI.Definitions;

namespace Telerik.Sitefinity.Web.UI.ContentUI
{
  /// <summary>
  /// A definition class containing all information needed to construct an instance of the respective detail view control.
  /// </summary>
  public class ContentViewDetailDefinition : 
    ContentViewDefinition,
    IContentViewDetailDefinition,
    IContentViewDefinition,
    IDefinition
  {
    private string wrapperTagName;
    private string additionalControlData;
    private string wrapperCssClass;
    private string fieldCssClass;
    private string sectionCssClass;
    private List<IContentViewSectionDefinition> sections;
    private bool? showSections;
    private bool? createBlankItem;

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Web.UI.ContentUI.ContentViewDetailDefinition" /> class.
    /// </summary>
    public ContentViewDetailDefinition()
      : base((ConfigElement) null)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Web.UI.ContentUI.ContentViewDetailDefinition" /> class.
    /// </summary>
    /// <param name="configDefinition">The config definition.</param>
    public ContentViewDetailDefinition(ConfigElement configDefinition)
      : base(configDefinition)
    {
    }

    /// <summary>Gets the definition.</summary>
    /// <returns></returns>
    public ContentViewDetailDefinition GetDefinition() => this;

    /// <summary>
    /// Gets or sets the name of the tag in which the view should be wrapped.
    /// </summary>
    /// <value></value>
    /// <remarks>Default value is UL.</remarks>
    public string WrapperTagName
    {
      get => this.ResolveProperty<string>(nameof (WrapperTagName), this.wrapperTagName);
      set => this.wrapperTagName = value;
    }

    /// <summary>
    /// Gets or sets additional data that may be used by some controls.
    /// </summary>
    public string AdditionalControlData
    {
      get => this.ResolveProperty<string>(nameof (AdditionalControlData), this.additionalControlData);
      set => this.additionalControlData = value;
    }

    /// <summary>
    /// Gets or sets the CSS class that should be applied to the wrapper tag.
    /// </summary>
    /// <value></value>
    public string WrapperCssClass
    {
      get => this.ResolveProperty<string>(nameof (WrapperCssClass), this.wrapperCssClass);
      set => this.wrapperCssClass = value;
    }

    /// <summary>Defines the name of the CSS class for all fields.</summary>
    /// <value></value>
    public string FieldCssClass
    {
      get => this.ResolveProperty<string>(nameof (FieldCssClass), this.fieldCssClass);
      set => this.fieldCssClass = value;
    }

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
    [PersistenceMode(PersistenceMode.InnerProperty)]
    public virtual List<IContentViewSectionDefinition> Sections
    {
      get
      {
        if (this.sections == null && this.ConfigDefinition != null)
          this.sections = ((ContentViewDetailElement) this.ConfigDefinition).Sections.Elements.Select<ContentViewSectionElement, IContentViewSectionDefinition>((Func<ContentViewSectionElement, IContentViewSectionDefinition>) (s => (IContentViewSectionDefinition) s.GetDefinition())).ToList<IContentViewSectionDefinition>();
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

    IEnumerable<IContentViewSectionDefinition> IContentViewDetailDefinition.Sections => (IEnumerable<IContentViewSectionDefinition>) this.Sections;

    /// <summary>
    /// Gets or sets the ID of the page that should display the master view.
    /// If this property is not set the current page is assumed.
    /// </summary>
    /// <value>The master page pageId.</value>
    public Guid MasterPageId { get; set; }

    /// <summary>
    /// Gets or sets the data ID of the content item that should be displayed.
    /// </summary>
    /// <value>The data item pageId.</value>
    public Guid DataItemId { get; set; }

    public bool? CreateBlankItem
    {
      get => this.ResolveProperty<bool?>(nameof (CreateBlankItem), this.createBlankItem);
      set => this.createBlankItem = value;
    }
  }
}
