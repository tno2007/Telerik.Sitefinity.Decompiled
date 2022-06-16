// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.Backend.Elements.Definitions.WidgetBarDefinition
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Web.UI.Backend.Elements.Config;
using Telerik.Sitefinity.Web.UI.Backend.Elements.Contracts;
using Telerik.Sitefinity.Web.UI.Definitions;

namespace Telerik.Sitefinity.Web.UI.Backend.Elements.Definitions
{
  /// <summary>A base abstract definition class for all widgets</summary>
  public class WidgetBarDefinition : DefinitionBase, IWidgetBarDefinition, IDefinition
  {
    private string title;
    private string titleWrapperTagName;
    private string resourceClassId;
    private string wrapperTagId;
    private HtmlTextWriterTag wrapperTagKey;
    private string cssClass;
    private List<IWidgetBarSectionDefinition> sections;

    /// <summary>
    /// Initializes a new instance of the <see cref="!:ValidatorDefinition" /> class.
    /// </summary>
    public WidgetBarDefinition()
      : base((ConfigElement) null)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="!:ValidatorDefinition" /> class.
    /// </summary>
    /// <param name="configDefinition">The config definition.</param>
    public WidgetBarDefinition(ConfigElement configDefinition)
      : base(configDefinition)
    {
    }

    /// <summary>Gets the definition.</summary>
    /// <returns></returns>
    public WidgetBarDefinition GetDefinition() => this;

    /// <summary>Gets or sets the title.</summary>
    /// <value>The title.</value>
    public string Title
    {
      get => this.ResolveProperty<string>(nameof (Title), this.title);
      set => this.title = value;
    }

    /// <summary>Gets or sets the title wrapper tag name.</summary>
    /// <value>The title wrapper tag name.</value>
    public string TitleWrapperTagName
    {
      get => this.ResolveProperty<string>(nameof (TitleWrapperTagName), this.titleWrapperTagName);
      set => this.titleWrapperTagName = value;
    }

    /// <summary>
    /// Gets or sets the resource class pageId for styling the widget's html.
    /// </summary>
    /// <value>The CSS class.</value>
    public string ResourceClassId
    {
      get => this.ResolveProperty<string>(nameof (ResourceClassId), this.resourceClassId);
      set => this.resourceClassId = value;
    }

    /// <summary>Gets or sets the wrapper tag pageId.</summary>
    /// <value>The wrapper tag pageId.</value>
    public string WrapperTagId
    {
      get => this.ResolveProperty<string>(nameof (WrapperTagId), this.wrapperTagId);
      set => this.wrapperTagId = value;
    }

    /// <summary>Gets or sets the name of the wrapper tag.</summary>
    /// <value>The name of the wrapper tag.</value>
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

    /// <summary>Gets the collection of widget section definitions.</summary>
    /// <value></value>
    [PersistenceMode(PersistenceMode.InnerProperty)]
    public IList<IWidgetBarSectionDefinition> Sections
    {
      get
      {
        if (this.sections == null)
        {
          this.sections = new List<IWidgetBarSectionDefinition>();
          if (this.ConfigDefinition != null)
          {
            foreach (DefinitionConfigElement section in ((WidgetBarElement) this.ConfigDefinition).Sections)
              this.sections.Add((IWidgetBarSectionDefinition) section.GetDefinition());
          }
        }
        return (IList<IWidgetBarSectionDefinition>) this.sections;
      }
    }

    /// <summary>Gets the collection of widget section definitions.</summary>
    /// <value></value>
    [PersistenceMode(PersistenceMode.InnerProperty)]
    IEnumerable<IWidgetBarSectionDefinition> IWidgetBarDefinition.Sections => this.Sections.Cast<IWidgetBarSectionDefinition>();

    /// <summary>
    /// Gets a value indicating whether this instance has sections.
    /// </summary>
    /// <value>
    /// 	<c>true</c> if this instance has sections; otherwise, <c>false</c>.
    /// </value>
    public bool HasSections => this.Sections.Count > 0;
  }
}
