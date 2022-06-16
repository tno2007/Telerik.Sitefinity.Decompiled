// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.Backend.Elements.Definitions.WidgetBarSectionDefinition
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Web.UI.Backend.Elements.Config;
using Telerik.Sitefinity.Web.UI.Backend.Elements.Contracts;
using Telerik.Sitefinity.Web.UI.Definitions;

namespace Telerik.Sitefinity.Web.UI.Backend.Elements.Definitions
{
  /// <summary>A definition class for widget bar sections.</summary>
  public class WidgetBarSectionDefinition : 
    DefinitionBase,
    IWidgetBarSectionDefinition,
    IDefinition,
    IModuleDependentItem
  {
    private string moduleName;
    private string title;
    private string name;
    private HtmlTextWriterTag titleWrapperTagKey;
    private string resourceClassId;
    private string wrapperTagId;
    private HtmlTextWriterTag wrapperTagKey;
    private string cssClass;
    private bool? visible;
    private List<IWidgetDefinition> items;

    /// <summary>
    /// Initializes a new instance of the <see cref="!:ValidatorDefinition" /> class.
    /// </summary>
    public WidgetBarSectionDefinition()
      : base((ConfigElement) null)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="!:ValidatorDefinition" /> class.
    /// </summary>
    /// <param name="configDefinition">The config definition.</param>
    public WidgetBarSectionDefinition(ConfigElement configDefinition)
      : base(configDefinition)
    {
    }

    /// <summary>Gets the definition.</summary>
    /// <returns></returns>
    public WidgetBarSectionDefinition GetDefinition() => this;

    /// <summary>
    /// Gets or sets the unique name of the widget bar section.
    /// </summary>
    /// <value>The title.</value>
    public string Name
    {
      get => this.ResolveProperty<string>(nameof (Name), this.name);
      set => this.name = value;
    }

    /// <summary>Gets or sets the title.</summary>
    /// <value>The title.</value>
    public string Title
    {
      get => this.ResolveProperty<string>(nameof (Title), this.title);
      set => this.title = value;
    }

    /// <summary>Gets or sets the title wrapper tag name.</summary>
    /// <value>The title wrapper tag name.</value>
    public HtmlTextWriterTag TitleWrapperTagKey
    {
      get => this.ResolveProperty<HtmlTextWriterTag>(nameof (TitleWrapperTagKey), this.titleWrapperTagKey);
      set => this.titleWrapperTagKey = value;
    }

    /// <summary>
    /// Gets or sets the resource class pageId for styling the widget's html.
    /// </summary>
    /// <value>The resource class.</value>
    public string ResourceClassId
    {
      get => this.ResolveProperty<string>(nameof (ResourceClassId), this.resourceClassId);
      set => this.resourceClassId = value;
    }

    string IModuleDependentItem.ModuleName
    {
      get => this.ResolveProperty<string>("ModuleName", this.moduleName);
      set => this.moduleName = value;
    }

    /// <summary>Gets the collection of widget definitions.</summary>
    /// <value>The collection of widget definitions</value>
    [PersistenceMode(PersistenceMode.InnerProperty)]
    public virtual List<IWidgetDefinition> Items
    {
      get
      {
        if (this.items == null)
        {
          this.items = new List<IWidgetDefinition>();
          if (this.ConfigDefinition != null)
          {
            foreach (DefinitionConfigElement definitionConfigElement in ((WidgetBarSectionElement) this.ConfigDefinition).Items)
              this.items.Add((IWidgetDefinition) definitionConfigElement.GetDefinition());
          }
        }
        return this.items;
      }
    }

    IEnumerable<IWidgetDefinition> IWidgetBarSectionDefinition.Items => this.Items.Cast<IWidgetDefinition>();

    /// <summary>Gets or sets wrapper tag pageId for the section.</summary>
    /// <value>The wrapper tag pageId.</value>
    public string WrapperTagId
    {
      get => this.ResolveProperty<string>(nameof (WrapperTagId), this.wrapperTagId);
      set => this.wrapperTagId = value;
    }

    /// <summary>Gets or sets wrapper tag name for the section.</summary>
    /// <value>The wrapper tag name.</value>
    public HtmlTextWriterTag WrapperTagKey
    {
      get => this.ResolveProperty<HtmlTextWriterTag>(nameof (WrapperTagKey), this.wrapperTagKey);
      set => this.wrapperTagKey = value;
    }

    /// <summary>
    /// Gets or sets the CSS class rendered in the widget's html.
    /// </summary>
    /// <value>The CSS class.</value>
    public string CssClass
    {
      get => this.ResolveProperty<string>(nameof (CssClass), this.cssClass);
      set => this.cssClass = value;
    }

    /// <summary>
    /// Gets or sets if the section will be initially visible.
    /// </summary>
    /// <value>The visible.</value>
    public virtual bool? Visible
    {
      get => this.ResolveProperty<bool?>(nameof (Visible), this.visible);
      set => this.visible = value;
    }
  }
}
