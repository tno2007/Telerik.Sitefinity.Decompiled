// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.Backend.Elements.Config.WidgetBarElement
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web.UI;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Web.Configuration;
using Telerik.Sitefinity.Web.UI.Backend.Elements.Contracts;
using Telerik.Sitefinity.Web.UI.Backend.Elements.Definitions;
using Telerik.Sitefinity.Web.UI.Definitions;

namespace Telerik.Sitefinity.Web.UI.Backend.Elements.Config
{
  /// <summary>The configuration element for widgets</summary>
  [ObjectInfo(typeof (ConfigDescriptions), Description = "TitleDescription", Title = "TitleCaption")]
  public class WidgetBarElement : DefinitionConfigElement, IWidgetBarDefinition, IDefinition
  {
    private List<WidgetBarSectionElement> widgetSections;

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Web.UI.Backend.Elements.Config.WidgetElement" /> class.
    /// </summary>
    /// <param name="element">The parent element.</param>
    public WidgetBarElement(ConfigElement element)
      : base(element)
    {
    }

    /// <summary>Gets the definition.</summary>
    /// <returns></returns>
    public override DefinitionBase GetDefinition() => (DefinitionBase) new WidgetBarDefinition((ConfigElement) this);

    /// <summary>Gets or sets the title.</summary>
    /// <value>The title.</value>
    [ConfigurationProperty("title", IsRequired = true)]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "WidgetBarTitleDescription", Title = "WidgetBarTitle")]
    public string Title
    {
      get => (string) this["title"];
      set => this["title"] = (object) value;
    }

    /// <summary>Gets or sets the title wrapper tag name.</summary>
    /// <value>The title wrapper tag name.</value>
    [ConfigurationProperty("titleWrapperTagName")]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "WidgetBarTitleWrapperTagNameDescription", Title = "WidgetBarTitleWrapperTagName")]
    public string TitleWrapperTagName
    {
      get => (string) this["titleWrapperTagName"];
      set => this["titleWrapperTagName"] = (object) value;
    }

    /// <summary>
    /// Gets or sets the global resource class ID for retrieving localized strings.
    /// </summary>
    /// <value>The CSS class.</value>
    [ConfigurationProperty("resourceClassId", DefaultValue = "")]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "ResourceClassIdDescription", Title = "ResourceClassIdCaption")]
    public string ResourceClassId
    {
      get => (string) this["resourceClassId"];
      set => this["resourceClassId"] = (object) value;
    }

    /// <summary>Gets or sets the wrapper tag pageId.</summary>
    /// <value>The wrapper tag pageId.</value>
    [ConfigurationProperty("wrapperTagId", DefaultValue = "")]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "WidgetBarWrapperTagIdDescription", Title = "WidgetBarWrapperTagIdCaption")]
    public string WrapperTagId
    {
      get => (string) this["wrapperTagId"];
      set => this["wrapperTagId"] = (object) value;
    }

    /// <summary>Gets or sets the key of the wrapper tag.</summary>
    /// <value>The key of the wrapper tag.</value>
    [ConfigurationProperty("wrapperTagKey", DefaultValue = HtmlTextWriterTag.Unknown)]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "WidgetBarWrapperTagNameDescription", Title = "WidgetBarWrapperTagNameCaption")]
    public HtmlTextWriterTag WrapperTagKey
    {
      get => (HtmlTextWriterTag) this["wrapperTagKey"];
      set => this["wrapperTagKey"] = (object) value;
    }

    /// <summary>
    /// Gets or sets the CSS class rendered in the widget's html.
    /// </summary>
    /// <value>The CSS class.</value>
    [ConfigurationProperty("cssClass", DefaultValue = "")]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "WidgetBarWrapperCssClassDescription", Title = "WidgetBarWrapperCssClassCaption")]
    public string CssClass
    {
      get => (string) this["cssClass"];
      set => this["cssClass"] = (object) value;
    }

    /// <summary>Gets the collection of widget section definitions.</summary>
    /// <value></value>
    [ConfigurationProperty("sections")]
    [ConfigurationCollection(typeof (WidgetElement), AddItemName = "section")]
    public ConfigElementList<WidgetBarSectionElement> Sections => (ConfigElementList<WidgetBarSectionElement>) this["sections"];

    /// <summary>Gets or sets a list of widget sections.</summary>
    /// <value></value>
    public List<WidgetBarSectionElement> WidgetSections
    {
      get
      {
        if (this.widgetSections == null)
          this.widgetSections = this.Sections.Elements.ToList<WidgetBarSectionElement>();
        return this.widgetSections;
      }
      set => this.widgetSections = value;
    }

    /// <summary>Gets the collection of widget section definitions.</summary>
    /// <value></value>
    IEnumerable<IWidgetBarSectionDefinition> IWidgetBarDefinition.Sections => this.Sections.Select<WidgetBarSectionElement, IWidgetBarSectionDefinition>((Func<WidgetBarSectionElement, IWidgetBarSectionDefinition>) (config => (IWidgetBarSectionDefinition) config.GetDefinition()));

    /// <summary>
    /// Gets a value indicating whether this instance has sections.
    /// </summary>
    /// <value>
    /// 	<c>true</c> if this instance has sections; otherwise, <c>false</c>.
    /// </value>
    public bool HasSections => this.Sections.Count > 0;
  }
}
