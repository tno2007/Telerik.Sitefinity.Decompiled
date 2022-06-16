// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Localization.RadMonthYearPickerMain
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using Telerik.Sitefinity.Localization.Data;

namespace Telerik.Sitefinity.Localization
{
  /// <summary>Represents string resources for RadControls.</summary>
  [ObjectInfo(Description = "RadMonthYearPickerMainDescription", Name = "RadMonthYearPicker.Main", ResourceClassId = "RadMonthYearPicker.Main", Title = "RadMonthYearPickerMainTitle", TitlePlural = "RadMonthYearPickerMainTitlePlural")]
  public sealed class RadMonthYearPickerMain : Resource
  {
    /// <summary>
    /// Initializes new instance of <see cref="T:Telerik.Sitefinity.Localization.RadMonthYearPickerMain" /> class with the default <see cref="T:Telerik.Sitefinity.Localization.Data.ResourceDataProvider" />.
    /// </summary>
    public RadMonthYearPickerMain()
    {
    }

    /// <summary>
    /// Initializes new instance of <see cref="T:Telerik.Sitefinity.Localization.RadMonthYearPickerMain" /> class with the provided <see cref="T:Telerik.Sitefinity.Localization.Data.ResourceDataProvider" />.
    /// </summary>
    /// <param name="dataProvider"><see cref="T:Telerik.Sitefinity.Localization.Data.ResourceDataProvider" /></param>
    public RadMonthYearPickerMain(ResourceDataProvider dataProvider)
      : base(dataProvider)
    {
    }

    [ResourceEntry("RadMonthYearPickerMainTitle", Description = "Title of this class.", LastModified = "2014/05/08", Value = "RadMonthYearPickerMain")]
    public string RadMonthYearPickerMainTitle => this[nameof (RadMonthYearPickerMainTitle)];

    [ResourceEntry("RadMonthYearPickerMainDescription", Description = "The description of this class.", LastModified = "2014/05/08", Value = "Resource strings for RadMonthYearPickerMain.")]
    public string RadMonthYearPickerMainDescription => this[nameof (RadMonthYearPickerMainDescription)];

    [ResourceEntry("RadMonthYearPickerMainTitlePlural", Description = "The title plural of this class.", LastModified = "2014/05/08", Value = "RadMonthYearPickerMain")]
    public string RadMonthYearPickerMainTitlePlural => this[nameof (RadMonthYearPickerMainTitlePlural)];

    [ResourceEntry("MonthYearNavigationCancelButtonCaption", Description = "RadMonthYearPickerMain resource strings.", LastModified = "2014/06/19", Value = "Cancel")]
    public string MonthYearNavigationCancelButtonCaption => this[nameof (MonthYearNavigationCancelButtonCaption)];

    [ResourceEntry("MonthYearNavigationNextText", Description = "RadMonthYearPickerMain resource strings.", LastModified = "2014/06/19", Value = "&gt;")]
    public string MonthYearNavigationNextText => this[nameof (MonthYearNavigationNextText)];

    [ResourceEntry("MonthYearNavigationNextToolTip", Description = "RadMonthYearPickerMain resource strings.", LastModified = "2014/06/19", Value = ">")]
    public string MonthYearNavigationNextToolTip => this[nameof (MonthYearNavigationNextToolTip)];

    [ResourceEntry("MonthYearNavigationOkButtonCaption", Description = "RadMonthYearPickerMain resource strings.", LastModified = "2014/06/19", Value = "OK")]
    public string MonthYearNavigationOkButtonCaption => this[nameof (MonthYearNavigationOkButtonCaption)];

    [ResourceEntry("MonthYearNavigationPrevText", Description = "RadMonthYearPickerMain resource strings.", LastModified = "2014/06/19", Value = "&lt;")]
    public string MonthYearNavigationPrevText => this[nameof (MonthYearNavigationPrevText)];

    [ResourceEntry("MonthYearNavigationPrevToolTip", Description = "RadMonthYearPickerMain resource strings.", LastModified = "2014/06/19", Value = "<")]
    public string MonthYearNavigationPrevToolTip => this[nameof (MonthYearNavigationPrevToolTip)];

    [ResourceEntry("MonthYearNavigationTodayButtonCaption", Description = "RadMonthYearPickerMain resource strings.", LastModified = "2014/06/19", Value = "Today")]
    public string MonthYearNavigationTodayButtonCaption => this[nameof (MonthYearNavigationTodayButtonCaption)];

    [ResourceEntry("MonthYearViewCaptionText", Description = "RadMonthYearPickerMain resource strings.", LastModified = "2014/06/19", Value = "Month year picker")]
    public string MonthYearViewCaptionText => this[nameof (MonthYearViewCaptionText)];

    [ResourceEntry("MonthYearViewSummary", Description = "RadMonthYearPickerMain resource strings.", LastModified = "2014/06/19", Value = "Table holding time picker for selecting time of day.")]
    public string MonthYearViewSummary => this[nameof (MonthYearViewSummary)];

    [ResourceEntry("PopupButtonToolTip", Description = "RadMonthYearPickerMain resource strings.", LastModified = "2014/06/19", Value = "Open the monthyear view popup.")]
    public string PopupButtonToolTip => this[nameof (PopupButtonToolTip)];

    [ResourceEntry("ReservedResource", Description = "RadMonthYearPickerMain resource strings.", LastModified = "2014/06/19", Value = "Please do not remove this key.")]
    public string ReservedResource => this[nameof (ReservedResource)];
  }
}
