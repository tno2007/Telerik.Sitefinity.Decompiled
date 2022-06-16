// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Localization.RadComboBoxResources
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using Telerik.Sitefinity.Localization.Data;

namespace Telerik.Sitefinity.Localization
{
  /// <summary>Represents string resources for RadControls.</summary>
  [ObjectInfo(Description = "RadComboBoxResourcesDescription", Name = "RadComboBox", ResourceClassId = "RadComboBox", Title = "RadComboBoxResourcesTitle", TitlePlural = "RadComboBoxResourcesTitlePlural")]
  public sealed class RadComboBoxResources : Resource
  {
    /// <summary>
    /// Initializes new instance of <see cref="T:Telerik.Sitefinity.Localization.RadComboBoxResources" /> class with the default <see cref="T:Telerik.Sitefinity.Localization.Data.ResourceDataProvider" />.
    /// </summary>
    public RadComboBoxResources()
    {
    }

    /// <summary>
    /// Initializes new instance of <see cref="T:Telerik.Sitefinity.Localization.RadComboBoxResources" /> class with the provided <see cref="T:Telerik.Sitefinity.Localization.Data.ResourceDataProvider" />.
    /// </summary>
    /// <param name="dataProvider"><see cref="T:Telerik.Sitefinity.Localization.Data.ResourceDataProvider" /></param>
    public RadComboBoxResources(ResourceDataProvider dataProvider)
      : base(dataProvider)
    {
    }

    /// <summary>RadComboBox</summary>
    [ResourceEntry("RadComboBoxResourcesTitle", Description = "The title of this class.", LastModified = "2010/09/28", Value = "RadComboBox")]
    public string RadComboBoxResourcesTitle => this[nameof (RadComboBoxResourcesTitle)];

    /// <summary>RadComboBox</summary>
    [ResourceEntry("RadComboBoxResourcesTitlePlural", Description = "The title plural of this class.", LastModified = "2010/09/28", Value = "RadComboBox")]
    public string RadComboBoxResourcesTitlePlural => this[nameof (RadComboBoxResourcesTitlePlural)];

    /// <summary>Resource strings for RadComboBox.</summary>
    [ResourceEntry("RadComboBoxResourcesDescription", Description = "The description of this class.", LastModified = "2010/09/28", Value = "Resource strings for RadComboBox.")]
    public string RadComboBoxResourcesDescription => this[nameof (RadComboBoxResourcesDescription)];

    [ResourceEntry("AllItemsCheckedString", Description = "RadComboBox resource strings.", LastModified = "2014/06/19", Value = "All items checked")]
    public string AllItemsCheckedString => this[nameof (AllItemsCheckedString)];

    [ResourceEntry("CheckAllString", Description = "RadComboBox resource strings.", LastModified = "2014/06/19", Value = "Check All")]
    public string CheckAllString => this[nameof (CheckAllString)];

    [ResourceEntry("ItemsCheckedString", Description = "RadComboBox resource strings.", LastModified = "2014/06/19", Value = "items checked")]
    public string ItemsCheckedString => this[nameof (ItemsCheckedString)];

    [ResourceEntry("NoMatches", Description = "RadComboBox resource strings.", LastModified = "2014/06/19", Value = "No matches")]
    public string NoMatches => this[nameof (NoMatches)];

    [ResourceEntry("ReservedResource", Description = "RadComboBox resource strings.", LastModified = "2014/06/19", Value = "Please do not remove this key.")]
    public string ReservedResource => this[nameof (ReservedResource)];

    [ResourceEntry("ShowMoreFormatString", Description = "RadComboBox resource strings.", LastModified = "2014/06/19", Value = "Items <b>1</b>-<b>{0}</b> out of <b>{1}</b>")]
    public string ShowMoreFormatString => this[nameof (ShowMoreFormatString)];
  }
}
