// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Localization.RadColorPicker
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using Telerik.Sitefinity.Localization.Data;

namespace Telerik.Sitefinity.Localization
{
  /// <summary>Represents string resources for RadControls.</summary>
  [ObjectInfo(Description = "RadColorPickerDescription", Name = "RadColorPicker", ResourceClassId = "RadColorPicker", Title = "RadColorPickerTitle", TitlePlural = "RadColorPickerTitlePlural")]
  public sealed class RadColorPicker : Resource
  {
    /// <summary>
    /// Initializes new instance of <see cref="T:Telerik.Sitefinity.Localization.RadColorPicker" /> class with the default <see cref="T:Telerik.Sitefinity.Localization.Data.ResourceDataProvider" />.
    /// </summary>
    public RadColorPicker()
    {
    }

    /// <summary>
    /// Initializes new instance of <see cref="T:Telerik.Sitefinity.Localization.RadColorPicker" /> class with the provided <see cref="T:Telerik.Sitefinity.Localization.Data.ResourceDataProvider" />.
    /// </summary>
    /// <param name="dataProvider"><see cref="T:Telerik.Sitefinity.Localization.Data.ResourceDataProvider" /></param>
    public RadColorPicker(ResourceDataProvider dataProvider)
      : base(dataProvider)
    {
    }

    /// <summary>RadColorPicker</summary>
    [ResourceEntry("RadColorPickerTitle", Description = "The title of this class.", LastModified = "2020/10/01", Value = "RadColorPicker")]
    public string RadColorPickerTitle => this[nameof (RadColorPickerTitle)];

    /// <summary>RadColorPicker</summary>
    [ResourceEntry("RadColorPickerTitlePlural", Description = "The title plural of this class.", LastModified = "2020/10/01", Value = "RadColorPicker")]
    public string RadColorPickerTitlePlural => this[nameof (RadColorPickerTitlePlural)];

    /// <summary>Resource strings for RadColorPicker dialog.</summary>
    [ResourceEntry("RadColorPickerDescription", Description = "The description of this class.", LastModified = "2020/10/01", Value = "Resource strings for RadColorPicker.")]
    public string RadColorPickerDescription => this[nameof (RadColorPickerDescription)];

    [ResourceEntry("ApplyButtonText", Description = "RadColorPicker resource strings.", LastModified = "2018/09/17", Value = "Apply")]
    public string ApplyButtonText => this[nameof (ApplyButtonText)];

    [ResourceEntry("BlankColorText", Description = "RadColorPicker resource strings.", LastModified = "2018/09/17", Value = "blank")]
    public string BlankColorText => this[nameof (BlankColorText)];

    [ResourceEntry("CurrentColorText", Description = "RadColorPicker resource strings.", LastModified = "2018/09/17", Value = "(Current Color is {0})")]
    public string CurrentColorText => this[nameof (CurrentColorText)];

    [ResourceEntry("HSBSliderDragText", Description = "RadColorPicker resource strings.", LastModified = "2018/09/17", Value = "Drag")]
    public string HSBSliderDragText => this[nameof (HSBSliderDragText)];

    [ResourceEntry("HSBTabText", Description = "RadColorPicker resource strings.", LastModified = "2018/09/17", Value = "HSB")]
    public string HSBTabText => this[nameof (HSBTabText)];

    [ResourceEntry("HSVSliderDragText", Description = "RadColorPicker resource strings.", LastModified = "2018/09/17", Value = "Drag")]
    public string HSVSliderDragText => this[nameof (HSVSliderDragText)];

    [ResourceEntry("HSVTabText", Description = "RadColorPicker resource strings.", LastModified = "2018/09/17", Value = "HSV")]
    public string HSVTabText => this[nameof (HSVTabText)];

    [ResourceEntry("NoColorText", Description = "RadColorPicker resource strings.", LastModified = "2018/09/17", Value = "No Color")]
    public string NoColorText => this[nameof (NoColorText)];

    [ResourceEntry("PickColorText", Description = "RadColorPicker resource strings.", LastModified = "2018/09/17", Value = "Pick Color")]
    public string PickColorText => this[nameof (PickColorText)];

    [ResourceEntry("RGBSlidersDecreaseText", Description = "RadColorPicker resource strings.", LastModified = "2018/09/17", Value = "Decrease")]
    public string RGBSlidersDecreaseText => this[nameof (RGBSlidersDecreaseText)];

    [ResourceEntry("RGBSlidersDragText", Description = "RadColorPicker resource strings.", LastModified = "2018/09/17", Value = "Drag")]
    public string RGBSlidersDragText => this[nameof (RGBSlidersDragText)];

    [ResourceEntry("RGBSlidersIncreaseText", Description = "RadColorPicker resource strings.", LastModified = "2018/09/17", Value = "Increase")]
    public string RGBSlidersIncreaseText => this[nameof (RGBSlidersIncreaseText)];

    [ResourceEntry("RGBSlidersTabText", Description = "RadColorPicker resource strings.", LastModified = "2018/09/17", Value = "RGB Sliders")]
    public string RGBSlidersTabText => this[nameof (RGBSlidersTabText)];

    [ResourceEntry("WebPaletteTabText", Description = "RadColorPicker resource strings.", LastModified = "2018/09/17", Value = "Web Palette")]
    public string WebPaletteTabText => this[nameof (WebPaletteTabText)];
  }
}
