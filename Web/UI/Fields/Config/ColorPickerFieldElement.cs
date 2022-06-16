// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.Fields.Config.ColorPickerFieldElement
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Web.Configuration;
using Telerik.Sitefinity.Web.UI.Definitions;
using Telerik.Sitefinity.Web.UI.Fields.Contracts;
using Telerik.Sitefinity.Web.UI.Fields.Definitions;
using Telerik.Web.UI;

namespace Telerik.Sitefinity.Web.UI.Fields.Config
{
  /// <summary>The configuration element for color picker fields.</summary>
  public class ColorPickerFieldElement : 
    FieldControlDefinitionElement,
    IColorPickerFieldDefinition,
    IFieldControlDefinition,
    IFieldDefinition,
    IDefinition
  {
    private List<IChoiceDefinition> colorPickerItems;

    /// <summary>
    /// Initializes new instance of configuration element with the provided parent element.
    /// </summary>
    /// <param name="parent">The parent element.</param>
    public ColorPickerFieldElement(ConfigElement parent)
      : base(parent)
    {
    }

    /// <summary>Gets the definition.</summary>
    /// <returns></returns>
    public override DefinitionBase GetDefinition() => (DefinitionBase) new ColorPickerDefinition((ConfigElement) this);

    /// <summary>
    /// Gets a collection of <see cref="T:Telerik.Sitefinity.Web.UI.Fields.Config.ChoiceElement" /> objects.
    /// </summary>
    /// <value>The collection of <see cref="T:Telerik.Sitefinity.Web.UI.Fields.Config.ChoiceElement" /> objects.</value>
    [ConfigurationProperty("colorPickerConfig")]
    [ConfigurationCollection(typeof (ChoiceElement), AddItemName = "element")]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "ChoicesConfigDescription", Title = "ChoicesConfigTitle")]
    public ConfigElementList<ColorPickerFieldElement> ColorPickerConfig => (ConfigElementList<ColorPickerFieldElement>) this["colorPickerConfig"];

    /// <summary>
    /// Gets a collection of <see cref="T:Telerik.Sitefinity.Web.UI.Fields.Contracts.IChoiceDefinition" /> objects, representing the items
    /// that the control ought to render.
    /// </summary>
    /// <value></value>
    public List<IChoiceDefinition> ColorPickerItems
    {
      get
      {
        if (this.colorPickerItems == null)
          this.colorPickerItems = this.ColorPickerConfig.Elements.Select<ColorPickerFieldElement, IChoiceDefinition>((Func<ColorPickerFieldElement, IChoiceDefinition>) (ch => (IChoiceDefinition) ch.ToDefinition())).ToList<IChoiceDefinition>();
        return this.colorPickerItems;
      }
    }

    /// <summary>Gets or sets a value indicating whether to show icon.</summary>
    [ConfigurationProperty("showIcon", DefaultValue = true)]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "ShowIconDescription", Title = "ShowIconTitle")]
    public bool ShowIcon
    {
      get => (bool) this["showIcon"];
      set => this["showIcon"] = (object) value;
    }

    /// <summary>
    /// Gets or sets a value indicating one of the 25 preset color palettes.
    /// </summary>
    [ConfigurationProperty("preset", DefaultValue = ColorPreset.None)]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "PresetDescription", Title = "PresetTitle")]
    public ColorPreset Preset
    {
      get => (ColorPreset) this["preset"];
      set => this["preset"] = (object) value;
    }

    /// <summary>
    /// Gets or sets a value indicating whether to show an empty color button.
    /// </summary>
    [ConfigurationProperty("showEmptyColor", DefaultValue = false)]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "ShowEmptyColorDescription", Title = "ShowEmptyColorTitle")]
    public bool ShowEmptyColor
    {
      get => (bool) this["showEmptyColor"];
      set => this["showEmptyColor"] = (object) value;
    }

    /// <summary>
    /// Gets or sets a value indicating whether to show color preview color.
    /// </summary>
    [ConfigurationProperty("enableColorPreview", DefaultValue = false)]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "EnableColorPreviewDescription", Title = "EnableColorPreviewTitle")]
    public bool EnableColorPreview
    {
      get => (bool) this["enableColorPreview"];
      set => this["enableColorPreview"] = (object) value;
    }

    /// <summary>
    /// Gets or sets a value indicating whether to show the recently used colors.
    /// </summary>
    [ConfigurationProperty("showRecentlyUsedColors", DefaultValue = false)]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "ShowRecentlyUsedColorsDescription", Title = "ShowRecentlyUsedColorsTitle")]
    public bool ShowRecentlyUsedColors
    {
      get => (bool) this["showRecentlyUsedColors"];
      set => this["showRecentlyUsedColors"] = (object) value;
    }

    /// <summary>
    /// Gets or sets a value indicating whether to show custom color button.
    /// </summary>
    [ConfigurationProperty("enableCustomColor", DefaultValue = false)]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "EnableCustomColorDescription", Title = "EnableCustomColorTitle")]
    public bool EnableCustomColor
    {
      get => (bool) this["enableCustomColor"];
      set => this["enableCustomColor"] = (object) value;
    }
  }
}
