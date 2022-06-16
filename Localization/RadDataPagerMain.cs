// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Localization.RadDataPagerMain
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using Telerik.Sitefinity.Localization.Data;

namespace Telerik.Sitefinity.Localization
{
  /// <summary>Represents string resources for RadControls.</summary>
  [ObjectInfo(Description = "RadDataPagerMainDescription", Name = "RadDataPager.Main", ResourceClassId = "RadDataPager.Main", Title = "RadDataPagerMainTitle", TitlePlural = "RadDataPagerMainTitlePlural")]
  public sealed class RadDataPagerMain : Resource
  {
    /// <summary>
    /// Initializes new instance of <see cref="T:Telerik.Sitefinity.Localization.RadDataPagerMain" /> class with the default <see cref="T:Telerik.Sitefinity.Localization.Data.ResourceDataProvider" />.
    /// </summary>
    public RadDataPagerMain()
    {
    }

    /// <summary>
    /// Initializes new instance of <see cref="T:Telerik.Sitefinity.Localization.RadDataPagerMain" /> class with the provided <see cref="T:Telerik.Sitefinity.Localization.Data.ResourceDataProvider" />.
    /// </summary>
    /// <param name="dataProvider"><see cref="T:Telerik.Sitefinity.Localization.Data.ResourceDataProvider" /></param>
    public RadDataPagerMain(ResourceDataProvider dataProvider)
      : base(dataProvider)
    {
    }

    /// <summary>RadDataPager Main</summary>
    [ResourceEntry("RadDataPagerMainTitle", Description = "The title of this class.", LastModified = "2014/03/20", Value = "RadDataPager Main")]
    public string RadDataPagerMainTitle => this[nameof (RadDataPagerMainTitle)];

    /// <summary>RadDataPager Main</summary>
    [ResourceEntry("RadDataPagerMainTitlePlural", Description = "The title plural of this class.", LastModified = "2014/03/20", Value = "RadDataPager Main")]
    public string RadDataPagerMainTitlePlural => this[nameof (RadDataPagerMainTitlePlural)];

    /// <summary>Resource strings for RadDataPager.</summary>
    [ResourceEntry("RadDataPagerMainDescription", Description = "The description of this class.", LastModified = "2014/03/20", Value = "Resource strings for RadDataPager.")]
    public string RadDataPagerMainDescription => this[nameof (RadDataPagerMainDescription)];

    [ResourceEntry("ReservedResource", Description = "RadDataPagerMain resource strings.", LastModified = "2014/06/19", Value = "Please do not remove this key.")]
    public string ReservedResource => this[nameof (ReservedResource)];

    [ResourceEntry("CurrentPageText", Description = "RadDataPagerMain resource strings.", LastModified = "2014/06/19", Value = "Page")]
    public string CurrentPageText => this[nameof (CurrentPageText)];

    [ResourceEntry("FirstButtonText", Description = "RadDataPagerMain resource strings.", LastModified = "2014/06/19", Value = "")]
    public string FirstButtonText => this[nameof (FirstButtonText)];

    [ResourceEntry("LabelText", Description = "RadDataPagerMain resource strings.", LastModified = "2014/06/19", Value = "Page size")]
    public string LabelText => this[nameof (LabelText)];

    [ResourceEntry("LastButtonText", Description = "RadDataPagerMain resource strings.", LastModified = "2014/06/19", Value = "")]
    public string LastButtonText => this[nameof (LastButtonText)];

    [ResourceEntry("NextButtonText", Description = "RadDataPagerMain resource strings.", LastModified = "2014/06/19", Value = "")]
    public string NextButtonText => this[nameof (NextButtonText)];

    [ResourceEntry("PageSizeSubmitButtonText", Description = "RadDataPagerMain resource strings.", LastModified = "2014/06/19", Value = "Change")]
    public string PageSizeSubmitButtonText => this[nameof (PageSizeSubmitButtonText)];

    [ResourceEntry("PageSizeText", Description = "RadDataPagerMain resource strings.", LastModified = "2014/06/19", Value = "Page size")]
    public string PageSizeText => this[nameof (PageSizeText)];

    [ResourceEntry("PrevButtonText", Description = "RadDataPagerMain resource strings.", LastModified = "2014/06/19", Value = "")]
    public string PrevButtonText => this[nameof (PrevButtonText)];

    [ResourceEntry("SliderDecreaseText", Description = "RadDataPagerMain resource strings.", LastModified = "2014/06/19", Value = "Decrease")]
    public string SliderDecreaseText => this[nameof (SliderDecreaseText)];

    [ResourceEntry("SliderDragText", Description = "RadDataPagerMain resource strings.", LastModified = "2014/06/19", Value = "Drag")]
    public string SliderDragText => this[nameof (SliderDragText)];

    [ResourceEntry("SliderIncreaseText", Description = "RadDataPagerMain resource strings.", LastModified = "2014/06/19", Value = "Increase")]
    public string SliderIncreaseText => this[nameof (SliderIncreaseText)];

    [ResourceEntry("SubmitButtonText", Description = "RadDataPagerMain resource strings.", LastModified = "2014/06/19", Value = "Go")]
    public string SubmitButtonText => this[nameof (SubmitButtonText)];

    [ResourceEntry("TotalPageText", Description = "RadDataPagerMain resource strings.", LastModified = "2014/06/19", Value = "of")]
    public string TotalPageText => this[nameof (TotalPageText)];
  }
}
