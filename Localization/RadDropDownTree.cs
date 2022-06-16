// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Localization.RadDropDownTree
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using Telerik.Sitefinity.Localization.Data;

namespace Telerik.Sitefinity.Localization
{
  /// <summary>Represents string resources for RadControls.</summary>
  [ObjectInfo(Description = "RadDropDownTreeDescription", Name = "RadDropDownTree", ResourceClassId = "RadDropDownTree", Title = "RadDropDownTreeTitle", TitlePlural = "RadDropDownTreePlural")]
  public sealed class RadDropDownTree : Resource
  {
    /// <summary>
    /// Initializes new instance of <see cref="T:Telerik.Sitefinity.Localization.RadDropDownTree" /> class with the default <see cref="T:Telerik.Sitefinity.Localization.Data.ResourceDataProvider" />.
    /// </summary>
    public RadDropDownTree()
    {
    }

    /// <summary>
    /// Initializes new instance of <see cref="T:Telerik.Sitefinity.Localization.RadDropDownTree" /> class with the provided <see cref="T:Telerik.Sitefinity.Localization.Data.ResourceDataProvider" />.
    /// </summary>
    /// <param name="dataProvider"><see cref="T:Telerik.Sitefinity.Localization.Data.ResourceDataProvider" /></param>
    public RadDropDownTree(ResourceDataProvider dataProvider)
      : base(dataProvider)
    {
    }

    [ResourceEntry("RadDropDownTreeTitle", Description = "Title of this class.", LastModified = "2014/05/08", Value = "RadDropDownTree")]
    public string RadDropDownTreeTitle => this[nameof (RadDropDownTreeTitle)];

    [ResourceEntry("RadDropDownTreeDescription", Description = "The description of this class.", LastModified = "2014/05/08", Value = "Resource strings for RadDropDownTreeDescription.")]
    public string RadDropDownTreeDescription => this[nameof (RadDropDownTreeDescription)];

    [ResourceEntry("RadDropDownTreePlural", Description = "The title plural of this class", LastModified = "2014/05/08", Value = "RadDatePickerMain")]
    public string RadDropDownTreePlural => this[nameof (RadDropDownTreePlural)];

    [ResourceEntry("CheckAll", Description = "RadDropDownTree resource strings.", LastModified = "2014/06/19", Value = "Check All")]
    public string CheckAll => this[nameof (CheckAll)];

    [ResourceEntry("Clear", Description = "RadDropDownTree resource strings.", LastModified = "2014/06/19", Value = "Clear")]
    public string Clear => this[nameof (Clear)];

    [ResourceEntry("ReservedResource", Description = "RadDropDownTree resource strings.", LastModified = "2014/06/19", Value = "Please do not remove this key.")]
    public string ReservedResource => this[nameof (ReservedResource)];
  }
}
