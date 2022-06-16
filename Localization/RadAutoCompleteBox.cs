// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Localization.RadAutoCompleteBox
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using Telerik.Sitefinity.Localization.Data;

namespace Telerik.Sitefinity.Localization
{
  /// <summary>Represents string resources for RadControls.</summary>
  [ObjectInfo(Description = "RadAutoCompleteBoxDescription", Name = "RadAutoCompleteBox", ResourceClassId = "RadAutoCompleteBox", Title = "RadAutoCompleteBoxTitle", TitlePlural = "RadAutoCompleteBoxTitlePlural")]
  public sealed class RadAutoCompleteBox : Resource
  {
    /// <summary>
    /// Initializes new instance of <see cref="T:Telerik.Sitefinity.Localization.RadAutoCompleteBox" /> class with the default <see cref="T:Telerik.Sitefinity.Localization.Data.ResourceDataProvider" />.
    /// </summary>
    public RadAutoCompleteBox()
    {
    }

    /// <summary>
    /// Initializes new instance of <see cref="T:Telerik.Sitefinity.Localization.RadAutoCompleteBox" /> class with the provided <see cref="T:Telerik.Sitefinity.Localization.Data.ResourceDataProvider" />.
    /// </summary>
    /// <param name="dataProvider"><see cref="T:Telerik.Sitefinity.Localization.Data.ResourceDataProvider" /></param>
    public RadAutoCompleteBox(ResourceDataProvider dataProvider)
      : base(dataProvider)
    {
    }

    /// <summary>RadAutoCompleteBox</summary>
    [ResourceEntry("RadAutoCompleteBoxTitle", Description = "The title of this class.", LastModified = "2014/06/19", Value = "RadAutoCompleteBox")]
    public string RadAutoCompleteBoxTitle => this[nameof (RadAutoCompleteBoxTitle)];

    /// <summary>RadAutoCompleteBox</summary>
    [ResourceEntry("RadAutoCompleteBoxTitlePlural", Description = "The title plural of this class.", LastModified = "2014/06/19", Value = "RadAutoCompleteBox")]
    public string RadAutoCompleteBoxTitlePlural => this[nameof (RadAutoCompleteBoxTitlePlural)];

    /// <summary>Resource strings for RadAutoCompleteBox.</summary>
    [ResourceEntry("RadAutoCompleteBoxDescription", Description = "The description of this class.", LastModified = "2014/06/19", Value = "Resource strings for RadAutoCompleteBox.")]
    public string RadAutoCompleteBoxDescription => this[nameof (RadAutoCompleteBoxDescription)];

    [ResourceEntry("ReservedResource", Description = "RadAutoCompleteBox resource strings.", LastModified = "2014/06/19", Value = "Please do not remove this key.")]
    public string ReservedResource => this[nameof (ReservedResource)];

    [ResourceEntry("RemoveTokenTitle", Description = "RadAutoCompleteBox resource strings.", LastModified = "2014/06/19", Value = "Remove token")]
    public string RemoveTokenTitle => this[nameof (RemoveTokenTitle)];
  }
}
