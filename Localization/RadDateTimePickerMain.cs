// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Localization.RadDateTimePickerMain
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using Telerik.Sitefinity.Localization.Data;

namespace Telerik.Sitefinity.Localization
{
  /// <summary>Represents string resources for RadControls.</summary>
  [ObjectInfo(Description = "RadDateTimePickerMainDescription", Name = "RadDateTimePicker.Main", ResourceClassId = "RadDateTimePicker.Main", Title = "RadDateTimePickerMainTitle", TitlePlural = "RadDateTimePickerMainPlural")]
  public sealed class RadDateTimePickerMain : Resource
  {
    /// <summary>
    /// Initializes new instance of <see cref="T:Telerik.Sitefinity.Localization.RadDateTimePickerMain" /> class with the default <see cref="T:Telerik.Sitefinity.Localization.Data.ResourceDataProvider" />.
    /// </summary>
    public RadDateTimePickerMain()
    {
    }

    /// <summary>
    /// Initializes new instance of <see cref="T:Telerik.Sitefinity.Localization.RadDateTimePickerMain" /> class with the provided <see cref="T:Telerik.Sitefinity.Localization.Data.ResourceDataProvider" />.
    /// </summary>
    /// <param name="dataProvider"><see cref="T:Telerik.Sitefinity.Localization.Data.ResourceDataProvider" /></param>
    public RadDateTimePickerMain(ResourceDataProvider dataProvider)
      : base(dataProvider)
    {
    }

    [ResourceEntry("RadDateTimePickerMainTitle", Description = "Title of this class.", LastModified = "2014/05/08", Value = "RadDateTimePickerMain")]
    public string RadDateTimePickerMainTitle => this[nameof (RadDateTimePickerMainTitle)];

    [ResourceEntry("RadDateTimePickerMainDescription", Description = "The description of this class.", LastModified = "2014/05/08", Value = "Resource strings for RadDateTimePickerMainDescription")]
    public string RadDateTimePickerMainDescription => this[nameof (RadDateTimePickerMainDescription)];

    [ResourceEntry("RadDateTimePickerMainPlural", Description = "The title plural of this class", LastModified = "2014/05/08", Value = "RadDateTimePickerMain")]
    public string RadDateTimePickerMainPlural => this[nameof (RadDateTimePickerMainPlural)];

    [ResourceEntry("DatePopupButtonToolTip", Description = "RadDateTimePickerMain resource strings.", LastModified = "2014/06/19", Value = "Open the calendar popup.")]
    public string DatePopupButtonToolTip => this[nameof (DatePopupButtonToolTip)];

    [ResourceEntry("ReservedResource", Description = "RadDateTimePickerMain resource strings.", LastModified = "2014/06/19", Value = "Please do not remove this key.")]
    public string ReservedResource => this[nameof (ReservedResource)];

    [ResourceEntry("TimePopupButtonToolTip", Description = "RadDateTimePickerMain resource strings.", LastModified = "2014/06/19", Value = "Open the time view popup.")]
    public string TimePopupButtonToolTip => this[nameof (TimePopupButtonToolTip)];
  }
}
