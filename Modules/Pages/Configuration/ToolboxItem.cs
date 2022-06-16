// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Pages.Configuration.ToolboxItem
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.DesignerToolbox;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Web.Configuration;

namespace Telerik.Sitefinity.Modules.Pages.Configuration
{
  /// <summary>Represents configuration element for toolbox items.</summary>
  [ObjectInfo(typeof (ConfigDescriptions), Description = "ToolboxItemElementDescription", Title = "ToolboxItemElementTitle")]
  public class ToolboxItem : ConfigElement, IModuleDependentItem, IToolboxItem
  {
    /// <summary>
    /// Initializes new instance of configuration element with the provided parent element.
    /// </summary>
    /// <param name="parent">The parent element.</param>
    public ToolboxItem(ConfigElement parent)
      : base(parent)
    {
    }

    /// <summary>
    /// Gets or sets a Boolean value indicating whether a defined toolbox item should appear in the toolbox.
    /// </summary>
    /// <value>The name.</value>
    [ConfigurationProperty("enabled", DefaultValue = true, IsKey = false, IsRequired = false)]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "ToolboxItemEnabledDescription", Title = "EnabledCaption")]
    public bool Enabled
    {
      get => (bool) this["enabled"];
      set => this["enabled"] = (object) value;
    }

    /// <summary>The value used for sorting.</summary>
    [ConfigurationProperty("ordinal", DefaultValue = 1f)]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "ToolboxOrdinalDescription", Title = "ToolboxOrdinalTitle")]
    public float Ordinal
    {
      get => (float) this["ordinal"];
      set => this["ordinal"] = (object) value;
    }

    /// <summary>
    /// Specifies CLR type for custom controls or the virtual path for user controls.
    /// </summary>
    /// <value>The type of the control.</value>
    [ConfigurationProperty("type", DefaultValue = "")]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "ControlTypeDescription", Title = "ControlTypeTitle")]
    public string ControlType
    {
      get => (string) this["type"];
      set
      {
        if (!value.IsNullOrEmpty())
        {
          string[] strArray = value.Split(new char[1]{ ',' }, 3);
          if (strArray.Length > 2)
            value = strArray[0] + "," + strArray[1];
        }
        this["type"] = (object) value;
      }
    }

    /// <summary>
    /// Specifies the controller CLR type that is being proxied by the control. If control is not acting as a proxy, this
    /// property will be null.
    /// </summary>
    /// <value>The controller type that is being proxied by the control.</value>
    [ConfigurationProperty("controllerType", DefaultValue = "")]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "ControllerTypeDescription", Title = "ControllerTypeTitle")]
    public string ControllerType
    {
      get => (string) this["controllerType"];
      set => this["controllerType"] = (object) value;
    }

    /// <summary>
    /// Gets or sets the programmatic name of the toolbox item.
    /// </summary>
    /// <value>The name.</value>
    [ConfigurationProperty("name", DefaultValue = "", IsKey = true)]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "ItemName", Title = "ItemNameCaption")]
    public string Name
    {
      get => (string) this["name"];
      set => this["name"] = (object) value;
    }

    /// <summary>
    /// Defines what name will be displayed for the item on the user interface.
    /// </summary>
    /// <value>The title.</value>
    [ConfigurationProperty("title", DefaultValue = "")]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "ItemTitle", Title = "ItemTitleCaption")]
    public string Title
    {
      get => (string) this["title"];
      set => this["title"] = (object) value;
    }

    /// <summary>
    /// Describes the purpose of the control represented by this toolbox item.
    /// </summary>
    /// <value>The description.</value>
    [ConfigurationProperty("description", DefaultValue = "")]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "ToolboxItemDescription", Title = "ItemDescriptionCaption")]
    public string Description
    {
      get => (string) this["description"];
      set => this["description"] = (object) value;
    }

    /// <summary>
    /// Defines global resource class ID for retrieving localized strings.
    /// </summary>
    /// <value>The resource class ID.</value>
    [ConfigurationProperty("resourceClassId", DefaultValue = "")]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "ResourceClassIdDescription", Title = "ResourceClassIdCaption")]
    public string ResourceClassId
    {
      get => (string) this["resourceClassId"];
      set => this["resourceClassId"] = (object) value;
    }

    /// <summary>
    /// Gets or sets a comma separated list of tags that describe the toolbox item.
    /// These tags are used for toolbox item filtering.
    /// </summary>
    [ConfigurationProperty("tags")]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "ToolboxTagsDescription", Title = "ToolboxTagsTitle")]
    public string Tags
    {
      get => (string) this["tags"];
      set => this["tags"] = (object) value;
    }

    /// <inheritdoc />
    ISet<string> IToolboxItem.Tags
    {
      get => ToolboxTags.Parse(this.Tags);
      set => this.Tags = ToolboxTags.ToString(value);
    }

    /// <summary>Gets or sets the CSS class for this item.</summary>
    /// <value>The CSS class.</value>
    [ConfigurationProperty("cssClass", DefaultValue = "")]
    public string CssClass
    {
      get => (string) this["cssClass"];
      set => this["cssClass"] = (object) value;
    }

    /// <summary>
    /// Gets or sets the name of an embedded layout template or the path to an external (.ascx) template.
    /// </summary>
    [ConfigurationProperty("layoutTemplate", DefaultValue = "")]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "LayoutTemplateDescription", Title = "LayoutTemplateTitle")]
    public string LayoutTemplate
    {
      get => (string) this["layoutTemplate"];
      set => this["layoutTemplate"] = (object) value;
    }

    /// <summary>
    /// Gets or sets the module name the toolbox item depends on.
    /// </summary>
    /// <value>The name.</value>
    [ConfigurationProperty("moduleName", DefaultValue = "", IsKey = false)]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "ToolboxItemModuleNameDescription", Title = "ToolboxItemModuleNameTitle")]
    public string ModuleName
    {
      get => (string) this["moduleName"];
      set => this["moduleName"] = (object) value;
    }

    [ConfigurationProperty("parameters")]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "ToolboxItemParamsDescription", Title = "ToolboxItemParamsTitle")]
    public NameValueCollection Parameters
    {
      get => (NameValueCollection) this["parameters"];
      set => this["parameters"] = (object) value;
    }

    /// <summary>Gets or sets the visibility mode of the toolbox item.</summary>
    /// <value>The visibility mode.</value>
    [ConfigurationProperty("visibilityMode", DefaultValue = ToolboxItemVisibilityMode.None)]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "ToolboxItemVisibilityModeDescription", Title = "ToolboxItemVisibilityModeTitle")]
    public ToolboxItemVisibilityMode VisibilityMode
    {
      get => (ToolboxItemVisibilityMode) this["visibilityMode"];
      set => this["visibilityMode"] = (object) value;
    }
  }
}
