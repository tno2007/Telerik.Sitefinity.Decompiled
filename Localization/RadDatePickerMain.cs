// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Localization.RadDatePickerMain
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using Telerik.Sitefinity.Localization.Data;

namespace Telerik.Sitefinity.Localization
{
  /// <summary>Represents string resources for RadControls.</summary>
  [ObjectInfo(Description = "RadDatePickerMainDescription", Name = "RadDatePicker.Main", ResourceClassId = "RadDatePicker.Main", Title = "RadDatePickerMainTitle", TitlePlural = "RadDatePickerMainPlural")]
  public sealed class RadDatePickerMain : Resource
  {
    /// <summary>
    /// Initializes new instance of <see cref="T:Telerik.Sitefinity.Localization.RadDatePickerMain" /> class with the default <see cref="T:Telerik.Sitefinity.Localization.Data.ResourceDataProvider" />.
    /// </summary>
    public RadDatePickerMain()
    {
    }

    /// <summary>
    /// Initializes new instance of <see cref="T:Telerik.Sitefinity.Localization.RadDatePickerMain" /> class with the provided <see cref="T:Telerik.Sitefinity.Localization.Data.ResourceDataProvider" />.
    /// </summary>
    /// <param name="dataProvider"><see cref="T:Telerik.Sitefinity.Localization.Data.ResourceDataProvider" /></param>
    public RadDatePickerMain(ResourceDataProvider dataProvider)
      : base(dataProvider)
    {
    }

    [ResourceEntry("RadDatePickerMainTitle", Description = "Title of this class.", LastModified = "2014/05/08", Value = "RadDatePickerMain")]
    public string RadDatePickerMainTitle => this[nameof (RadDatePickerMainTitle)];

    [ResourceEntry("RadDatePickerMainDescription", Description = "The description of this class.", LastModified = "2014/05/08", Value = "Resource strings for RadDatePickerMainDescription.")]
    public string RadDatePickerMainDescription => this[nameof (RadDatePickerMainDescription)];

    [ResourceEntry("RadDatePickerMainPlural", Description = "The title plural of this class", LastModified = "2014/05/08", Value = "RadDatePickerMain")]
    public string RadDatePickerMainPlural => this[nameof (RadDatePickerMainPlural)];

    [ResourceEntry("PopupButtonToolTip", Description = "RadDatePickerMain resource strings.", LastModified = "2014/06/19", Value = "Open the calendar popup.")]
    public string PopupButtonToolTip => this[nameof (PopupButtonToolTip)];

    [ResourceEntry("ReservedResource", Description = "RadDatePickerMain resource strings.", LastModified = "2014/06/19", Value = "Please do not remove this key.")]
    public string ReservedResource => this[nameof (ReservedResource)];
  }
}
