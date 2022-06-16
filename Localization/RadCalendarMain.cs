// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Localization.RadCalendarMain
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using Telerik.Sitefinity.Localization.Data;

namespace Telerik.Sitefinity.Localization
{
  /// <summary>Represents string resources for RadControls.</summary>
  [ObjectInfo(Description = "RadCalendarMainDescription", Name = "RadCalendar.Main", ResourceClassId = "RadCalendar.Main", Title = "RadCalendarMainTitle", TitlePlural = "RadCalendarMainPlural")]
  public sealed class RadCalendarMain : Resource
  {
    /// <summary>
    /// Initializes new instance of <see cref="T:Telerik.Sitefinity.Localization.RadCalendarMain" /> class with the default <see cref="T:Telerik.Sitefinity.Localization.Data.ResourceDataProvider" />.
    /// </summary>
    public RadCalendarMain()
    {
    }

    /// <summary>
    /// Initializes new instance of <see cref="T:Telerik.Sitefinity.Localization.RadCalendarMain" /> class with the provided <see cref="T:Telerik.Sitefinity.Localization.Data.ResourceDataProvider" />.
    /// </summary>
    /// <param name="dataProvider"><see cref="T:Telerik.Sitefinity.Localization.Data.ResourceDataProvider" /></param>
    public RadCalendarMain(ResourceDataProvider dataProvider)
      : base(dataProvider)
    {
    }

    [ResourceEntry("RadCalendarMainTitle", Description = "Title of this class.", LastModified = "2014/05/08", Value = "RadCalendarMain")]
    public string RadCalendarMainTitle => this[nameof (RadCalendarMainTitle)];

    [ResourceEntry("RadCalendarMainDescription", Description = "The description of this class.", LastModified = "2014/05/08", Value = "Resource strings for RadCalendarMainDescription")]
    public string RadCalendarMainDescription => this[nameof (RadCalendarMainDescription)];

    [ResourceEntry("RadCalendarMainPlural", Description = "The title plural of this class", LastModified = "2014/05/08", Value = "RadCalendarMain")]
    public string RadCalendarMainPlural => this[nameof (RadCalendarMainPlural)];

    [ResourceEntry("FastNavigationCancelButtonCaption", Description = "RadCalendarMain resource strings.", LastModified = "2014/06/19", Value = "Cancel")]
    public string FastNavigationCancelButtonCaption => this[nameof (FastNavigationCancelButtonCaption)];

    [ResourceEntry("FastNavigationDateIsOutOfRangeMessage", Description = "RadCalendarMain resource strings.", LastModified = "2014/06/19", Value = "Date is out of range.")]
    public string FastNavigationDateIsOutOfRangeMessage => this[nameof (FastNavigationDateIsOutOfRangeMessage)];

    [ResourceEntry("FastNavigationNextText", Description = "RadCalendarMain resource strings.", LastModified = "2014/06/19", Value = "&lt;&lt;")]
    public string FastNavigationNextText => this[nameof (FastNavigationNextText)];

    [ResourceEntry("FastNavigationNextToolTip", Description = "RadCalendarMain resource strings.", LastModified = "2014/06/19", Value = ">>")]
    public string FastNavigationNextToolTip => this[nameof (FastNavigationNextToolTip)];

    [ResourceEntry("FastNavigationOkButtonCaption", Description = "RadCalendarMain resource strings.", LastModified = "2014/06/19", Value = "OK")]
    public string FastNavigationOkButtonCaption => this[nameof (FastNavigationOkButtonCaption)];

    [ResourceEntry("FastNavigationPrevText", Description = "RadCalendarMain resource strings.", LastModified = "2014/06/19", Value = "&lt;&lt;")]
    public string FastNavigationPrevText => this[nameof (FastNavigationPrevText)];

    [ResourceEntry("FastNavigationPrevToolTip", Description = "RadCalendarMain resource strings.", LastModified = "2014/06/19", Value = "<<")]
    public string FastNavigationPrevToolTip => this[nameof (FastNavigationPrevToolTip)];

    [ResourceEntry("FastNavigationTodayButtonCaption", Description = "RadCalendarMain resource strings.", LastModified = "2014/06/19", Value = "Today")]
    public string FastNavigationTodayButtonCaption => this[nameof (FastNavigationTodayButtonCaption)];

    [ResourceEntry("NavigationNextText", Description = "RadCalendarMain resource strings.", LastModified = "2014/06/19", Value = "&gt;")]
    public string NavigationNextText => this[nameof (NavigationNextText)];

    [ResourceEntry("NavigationNextToolTip", Description = "RadCalendarMain resource strings.", LastModified = "2014/06/19", Value = ">")]
    public string NavigationNextToolTip => this[nameof (NavigationNextToolTip)];

    [ResourceEntry("NavigationPrevText", Description = "RadCalendarMain resource strings.", LastModified = "2014/06/19", Value = "&lt;")]
    public string NavigationPrevText => this[nameof (NavigationPrevText)];

    [ResourceEntry("NavigationPrevToolTip", Description = "RadCalendarMain resource strings.", LastModified = "2014/06/19", Value = "<")]
    public string NavigationPrevToolTip => this[nameof (NavigationPrevToolTip)];

    [ResourceEntry("ReservedResource", Description = "RadCalendarMain resource strings.", LastModified = "2014/06/19", Value = "Please do not remove this key.")]
    public string ReservedResource => this[nameof (ReservedResource)];
  }
}
