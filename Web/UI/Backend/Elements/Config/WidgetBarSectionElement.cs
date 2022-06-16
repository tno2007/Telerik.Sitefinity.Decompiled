// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.Backend.Elements.Config.WidgetBarSectionElement
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
  public class WidgetBarSectionElement : 
    DefinitionConfigElement,
    IWidgetBarSectionDefinition,
    IDefinition
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Web.UI.Backend.Elements.Config.WidgetElement" /> class.
    /// </summary>
    /// <param name="element">The parent element.</param>
    public WidgetBarSectionElement(ConfigElement element)
      : base(element)
    {
    }

    /// <summary>Gets the definition.</summary>
    /// <returns></returns>
    public override DefinitionBase GetDefinition() => (DefinitionBase) new WidgetBarSectionDefinition((ConfigElement) this);

    /// <summary>Gets or sets the unique name of the section.</summary>
    /// <value>The title.</value>
    [ConfigurationProperty("name", IsKey = true, IsRequired = true)]
    public string Name
    {
      get => (string) this["name"];
      set => this["name"] = (object) value;
    }

    /// <summary>Gets or sets the title.</summary>
    /// <value>The title.</value>
    [ConfigurationProperty("title", IsRequired = true)]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "WidgetBarSectionTitleDescription", Title = "WidgetBarTitle")]
    public string Title
    {
      get => (string) this["title"];
      set => this["title"] = (object) value;
    }

    /// <summary>Gets or sets the title wrapper tag name.</summary>
    /// <value>The title.</value>
    [ConfigurationProperty("titleWrapperTagKey", DefaultValue = HtmlTextWriterTag.Unknown)]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "WidgetBarSectionTitleWrapperTagNameDescription", Title = "WidgetBarSectionTitleWrapperTagName")]
    public HtmlTextWriterTag TitleWrapperTagKey
    {
      get => (HtmlTextWriterTag) this["titleWrapperTagKey"];
      set => this["titleWrapperTagKey"] = (object) value;
    }

    /// <summary>Gets or sets the wrapper tag id of the section</summary>
    /// <value>The wrapper tag pageId.</value>
    [ConfigurationProperty("wrapperTagId")]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "WidgetBarSectionWrapperTagIdDescription", Title = "WidgetBarSectionWrapperTagId")]
    public string WrapperTagId
    {
      get => (string) this["wrapperTagId"];
      set => this["wrapperTagId"] = (object) value;
    }

    /// <summary>
    /// Gets or sets the name of the wrapper tag of the section
    /// </summary>
    /// <value>The name of the wrapper tag.</value>
    [ConfigurationProperty("wrapperTagKey", DefaultValue = HtmlTextWriterTag.Unknown)]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "WidgetBarSectionWrapperTagNameDescription", Title = "WidgetBarSectionWrapperTagName")]
    public HtmlTextWriterTag WrapperTagKey
    {
      get => (HtmlTextWriterTag) this["wrapperTagKey"];
      set => this["wrapperTagKey"] = (object) value;
    }

    /// <summary>
    /// Gets or sets the CSS class rendered in the widget's html.
    /// </summary>
    /// <value>The CSS class.</value>
    [ConfigurationProperty("cssClass")]
    public string CssClass
    {
      get => (string) this["cssClass"];
      set => this["cssClass"] = (object) value;
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

    /// <summary>
    /// Gets or sets if the section will be initially visible.
    /// </summary>
    /// <value>The visible.</value>
    [ConfigurationProperty("visible", DefaultValue = true)]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "VisibleDescription", Title = "VisibleCaption")]
    public virtual bool? Visible
    {
      get => (bool?) this["visible"];
      set => this["visible"] = (object) value;
    }

    /// <summary>
    /// Defines a dictionary of <see cref="T:Telerik.Sitefinity.Web.UI.Backend.Elements.Config.WidgetElement" /> configuration elements.
    /// </summary>
    /// <value>The dictionary of <see cref="T:Telerik.Sitefinity.Web.UI.Backend.Elements.Config.WidgetElement" /> configuration element.</value>
    [ConfigurationProperty("items")]
    [ConfigurationCollection(typeof (WidgetElement), AddItemName = "item")]
    public ConfigElementList<WidgetElement> Items => (ConfigElementList<WidgetElement>) this["items"];

    IEnumerable<IWidgetDefinition> IWidgetBarSectionDefinition.Items => this.Items.Select<WidgetElement, IWidgetDefinition>((Func<WidgetElement, IWidgetDefinition>) (config => (IWidgetDefinition) config.GetDefinition()));
  }
}
