// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.Fields.Definitions.ColorPickerDefinition
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Linq;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Web.UI.Definitions;
using Telerik.Sitefinity.Web.UI.Fields.Config;
using Telerik.Sitefinity.Web.UI.Fields.Contracts;
using Telerik.Web.UI;

namespace Telerik.Sitefinity.Web.UI.Fields.Definitions
{
  public class ColorPickerDefinition : 
    FieldControlDefinition,
    IColorPickerFieldDefinition,
    IFieldControlDefinition,
    IFieldDefinition,
    IDefinition
  {
    private List<IChoiceDefinition> colorPickerItems;
    private bool showIcon;
    private ColorPreset colorPreset;
    private bool showEmptyColor;
    private bool enableColorPreview;
    private bool showRecentlyUsedColors;
    private bool enableCustomColor;

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Web.UI.Fields.Definitions.ColorPickerDefinition" /> class.
    /// </summary>
    public ColorPickerDefinition()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Web.UI.Fields.Definitions.ColorPickerDefinition" /> class.
    /// </summary>
    /// <param name="configDefinition">The config definition.</param>
    public ColorPickerDefinition(ConfigElement configDefinition)
      : base(configDefinition)
    {
    }

    /// <summary>
    /// Gets a collection of <see cref="T:Telerik.Sitefinity.Web.UI.Fields.Contracts.IChoiceDefinition" /> objects, representing the items
    /// that the control ought to render.
    /// </summary>
    public List<IChoiceDefinition> ColorPickerItems
    {
      get
      {
        if (this.colorPickerItems == null)
          this.colorPickerItems = ((IEnumerable<IChoiceDefinition>) ((ColorPickerFieldElement) this.ConfigDefinition).ColorPickerConfig.Elements.Select<ColorPickerFieldElement, ChoiceDefinition>((Func<ColorPickerFieldElement, ChoiceDefinition>) (c => new ChoiceDefinition((ConfigElement) c)))).ToList<IChoiceDefinition>();
        return this.colorPickerItems;
      }
    }

    /// <summary>
    /// Gets or sets the property to whether show the color palette with a button or not.
    /// </summary>
    public bool ShowIcon
    {
      get => this.ResolveProperty<bool>(nameof (ShowIcon), this.showIcon);
      set => this.showIcon = value;
    }

    /// <summary>
    /// Gets or sets a value indicating one of the 25 preset color palettes.
    /// </summary>
    public ColorPreset Preset
    {
      get => this.ResolveProperty<ColorPreset>(nameof (Preset), this.colorPreset);
      set => this.colorPreset = value;
    }

    /// <summary>
    /// Gets or sets a value indicating whether to show an empty color button.
    /// </summary>
    public bool ShowEmptyColor
    {
      get => this.ResolveProperty<bool>(nameof (ShowEmptyColor), this.showEmptyColor);
      set => this.showEmptyColor = value;
    }

    /// <summary>
    /// Gets or sets a value indicating whether to show preview color.
    /// </summary>
    public bool EnableColorPreview
    {
      get => this.ResolveProperty<bool>(nameof (EnableColorPreview), this.enableColorPreview);
      set => this.enableColorPreview = value;
    }

    /// <summary>
    /// Gets or sets a value indicating whether to show the recently used colors.
    /// </summary>
    public bool ShowRecentlyUsedColors
    {
      get => this.ResolveProperty<bool>(nameof (ShowRecentlyUsedColors), this.showRecentlyUsedColors);
      set => this.showRecentlyUsedColors = value;
    }

    /// <summary>
    /// Gets or sets a value indicating whether to show custom color button.
    /// </summary>
    public bool EnableCustomColor
    {
      get => this.ResolveProperty<bool>(nameof (EnableCustomColor), this.enableCustomColor);
      set => this.enableCustomColor = value;
    }
  }
}
