// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.ContentUI.Config.ContentViewSectionElement
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Linq;
using System.Web.UI;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Web.Configuration;
using Telerik.Sitefinity.Web.UI.ContentUI.Contracts;
using Telerik.Sitefinity.Web.UI.Definitions;
using Telerik.Sitefinity.Web.UI.Extenders.Config;
using Telerik.Sitefinity.Web.UI.Extenders.Contracts;
using Telerik.Sitefinity.Web.UI.Fields.Config;
using Telerik.Sitefinity.Web.UI.Fields.Contracts;
using Telerik.Sitefinity.Web.UI.Fields.Definitions;
using Telerik.Sitefinity.Web.UI.Fields.Enums;

namespace Telerik.Sitefinity.Web.UI.ContentUI.Config
{
  /// <summary>
  /// Represents a configuration element for Sitefinity content view section element.
  /// </summary>
  [ObjectInfo(typeof (ConfigDescriptions), Description = "ContentViewSectionElementDescription", Title = "ContentViewSectionElementCaption")]
  public class ContentViewSectionElement : 
    DefinitionConfigElement,
    IContentViewSectionDefinition,
    IDefinition
  {
    private string controlDefinitionName;
    private string viewName;
    private IContentViewDefinition view;

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Web.UI.ContentUI.Config.ContentViewSectionElement" /> class.
    /// </summary>
    /// <param name="parent">The parent element.</param>
    /// <remarks>
    /// ConfigElementCollection generally needs to have a parent, however, sometimes it is necessary
    /// to create a collection in memory only which is then used later on in the context of a parent.
    /// Therefore, is the element is of ConfigElementCollection, exception for a non existing parent
    /// will not be thrown.
    /// </remarks>
    public ContentViewSectionElement(ConfigElement parent)
      : base(parent)
    {
      this.view = parent as IContentViewDefinition;
      if (this.view != null)
        return;
      this.view = parent.Parent as IContentViewDefinition;
    }

    /// <summary>Gets the definition.</summary>
    /// <returns></returns>
    public override DefinitionBase GetDefinition() => (DefinitionBase) new ContentViewSectionDefinition((ConfigElement) this);

    /// <summary>Gets or sets the name of the control definition.</summary>
    /// <value>The name of the control definition.</value>
    public string ControlDefinitionName
    {
      get
      {
        if (string.IsNullOrEmpty(this.controlDefinitionName) && this.view != null)
          this.controlDefinitionName = this.view.ControlDefinitionName;
        return this.controlDefinitionName;
      }
      set => this.controlDefinitionName = value;
    }

    /// <summary>Gets or sets the name of the view.</summary>
    /// <value>The name of the view.</value>
    public string ViewName
    {
      get
      {
        if (string.IsNullOrEmpty(this.viewName) && this.view != null)
          this.viewName = this.view.ViewName;
        return this.viewName;
      }
      set => this.viewName = value;
    }

    /// <summary>Gets the definition instance.</summary>
    /// <returns></returns>
    public IContentViewSectionDefinition GetDefinitionInstance() => (IContentViewSectionDefinition) new ContentViewSectionDefinition()
    {
      ControlDefinitionName = this.View.ControlDefinitionName,
      ViewName = this.View.ViewName,
      Name = this.Name
    };

    /// <summary>
    /// Gets or sets the definition of the view to which this section belongs to.
    /// </summary>
    /// <value></value>
    [Browsable(false)]
    public IContentViewDefinition View
    {
      get => this.view;
      set => this.view = value;
    }

    /// <summary>Gets or sets the fields config.</summary>
    /// <value>The fields config.</value>
    [ConfigurationProperty("fields")]
    [ConfigurationCollection(typeof (FieldControlDefinitionElement), AddItemName = "field")]
    public ConfigElementDictionary<string, FieldDefinitionElement> Fields => (ConfigElementDictionary<string, FieldDefinitionElement>) this["fields"];

    /// <summary>Gets or sets the CSS class for this section.</summary>
    /// <value>The CSS class.</value>
    [ConfigurationProperty("cssClass")]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "CssClassDescription", Title = "CssClassCaption")]
    public string CssClass
    {
      get => (string) this["cssClass"];
      set => this["cssClass"] = (object) value;
    }

    /// <summary>
    /// Gets or sets the <see cref="P:Telerik.Sitefinity.Web.UI.ContentUI.Config.ContentViewSectionElement.DisplayMode" />.
    /// </summary>
    /// <value>The display mode.</value>
    [ConfigurationProperty("displayMode", DefaultValue = null)]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "DisplayModeDescription", Title = "DisplayModeCaption")]
    public FieldDisplayMode? DisplayMode
    {
      get => (FieldDisplayMode?) this["displayMode"];
      set => this["displayMode"] = (object) value;
    }

    /// <summary>Gets or sets the name of the section.</summary>
    /// <value></value>
    [ConfigurationProperty("name", IsKey = true)]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "NameDescription", Title = "NameCaption")]
    public string Name
    {
      get => (string) this["name"];
      set => this["name"] = (object) value;
    }

    /// <summary>Gets or sets the ordinal position of the section.</summary>
    /// <value>The ordinal.</value>
    [ConfigurationProperty("ordinal")]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "OrdinalDescription", Title = "OrdinalCaption")]
    public int? Ordinal
    {
      get => (int?) this["ordinal"];
      set => this["ordinal"] = (object) value;
    }

    /// <summary>
    /// Gets or sets the global resource class ID to use for localized strings.
    /// </summary>
    /// <value></value>
    [ConfigurationProperty("resourceClassId")]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "ResourceClassIdDescription", Title = "ResourceClassIdCaption")]
    public string ResourceClassId
    {
      get => (string) this["resourceClassId"];
      set => this["resourceClassId"] = (object) value;
    }

    /// <summary>Gets or sets the title of the section.</summary>
    /// <value></value>
    [ConfigurationProperty("title")]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "TitleDescription", Title = "TitleCaption")]
    public string Title
    {
      get => (string) this["title"];
      set => this["title"] = (object) value;
    }

    /// <summary>
    /// Gets or sets the value for the <see cref="T:System.Web.UI.Control" /> ID property of the control that will be constructed based on this definition.
    /// </summary>
    /// <value>The control id.</value>
    [ConfigurationProperty("controlId")]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "ControlIdDescription", Title = "ControlIdCaption")]
    public string ControlId
    {
      get => (string) this["controlId"];
      set => this["controlId"] = (object) value;
    }

    /// <summary>
    /// Gets or sets the tag that will be rendered as a wrapper.
    /// </summary>
    /// <value>The wrapper tag.</value>
    [ConfigurationProperty("wrapperTag", DefaultValue = HtmlTextWriterTag.Div)]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "WrapperTagDescription", Title = "WrapperTagCaption")]
    public HtmlTextWriterTag WrapperTag
    {
      get => (HtmlTextWriterTag) this["wrapperTag"];
      set => this["wrapperTag"] = (object) value;
    }

    /// <summary>
    /// Gets or sets the tag that will be rendered as a wrapper.
    /// </summary>
    /// <value>The wrapper tag.</value>
    [ConfigurationProperty("isHiddenInTranslationMode", DefaultValue = false)]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "IsHiddenInTranslationModeDescription", Title = "IsHiddenInTranslationModeCaption")]
    public bool IsHiddenInTranslationMode
    {
      get => (bool) this["isHiddenInTranslationMode"];
      set => this["isHiddenInTranslationMode"] = (object) value;
    }

    IEnumerable<IFieldDefinition> IContentViewSectionDefinition.Fields => (IEnumerable<IFieldDefinition>) this.Fields.Elements.Select<FieldDefinitionElement, FieldDefinition>((Func<FieldDefinitionElement, FieldDefinition>) (fld =>
    {
      FieldDefinition definition = (FieldDefinition) fld.GetDefinition();
      definition.ControlDefinitionName = this.ControlDefinitionName;
      definition.ViewName = this.ViewName;
      definition.SectionName = this.Name;
      definition.FieldName = fld.FieldName;
      return definition;
    }));

    /// <summary>
    /// Gets or sets the object that defines the expandable behavior of the text field.
    /// </summary>
    [ConfigurationProperty("expandableDefinition")]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "ExpandableControlElementDescription", Title = "ExpandableControlElementCaption")]
    public ExpandableControlElement ExpandableDefinitionConfig
    {
      get => (ExpandableControlElement) this["expandableDefinition"];
      set => this["expandableDefinition"] = (object) value;
    }

    /// <summary>
    /// Gets or sets the object that defines the expandable behavior of the text field.
    /// </summary>
    /// <value></value>
    public IExpandableControlDefinition ExpandableDefinition => (IExpandableControlDefinition) this.ExpandableDefinitionConfig;
  }
}
