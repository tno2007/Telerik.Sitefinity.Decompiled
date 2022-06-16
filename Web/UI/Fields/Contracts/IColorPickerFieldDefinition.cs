// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.Fields.Contracts.IColorPickerFieldDefinition
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Collections.Generic;
using Telerik.Sitefinity.Web.UI.Definitions;
using Telerik.Web.UI;

namespace Telerik.Sitefinity.Web.UI.Fields.Contracts
{
  /// <summary>
  /// An interface that provides the common members for the definition of a color picker field element.
  /// </summary>
  public interface IColorPickerFieldDefinition : 
    IFieldControlDefinition,
    IFieldDefinition,
    IDefinition
  {
    /// <summary>Gets the color picker items.</summary>
    List<IChoiceDefinition> ColorPickerItems { get; }

    /// <summary>
    /// Gets or sets the property to whether show the color palette with a button or not.
    /// </summary>
    bool ShowIcon { get; set; }

    /// <summary>
    /// Gets or sets a value indicating one of the 25 preset color palettes.
    /// </summary>
    ColorPreset Preset { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether to show an empty color button.
    /// </summary>
    bool ShowEmptyColor { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether to show color preview color.
    /// </summary>
    bool EnableColorPreview { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether to show the recently used colors.
    /// </summary>
    bool ShowRecentlyUsedColors { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether to show custom color button.
    /// </summary>
    bool EnableCustomColor { get; set; }
  }
}
