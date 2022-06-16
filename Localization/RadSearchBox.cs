// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Localization.RadSearchBox
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using Telerik.Sitefinity.Localization.Data;

namespace Telerik.Sitefinity.Localization
{
  /// <summary>Represents string resources for RadControls.</summary>
  [ObjectInfo(Description = "RadSearchBoxDescription", Name = "RadSearchBox", ResourceClassId = "RadSearchBox", Title = "RadSearchBoxTitle", TitlePlural = "RadSearchBoxTitlePlural")]
  public sealed class RadSearchBox : Resource
  {
    /// <summary>
    /// Initializes new instance of <see cref="T:Telerik.Sitefinity.Localization.RadSearchBox" /> class with the default <see cref="T:Telerik.Sitefinity.Localization.Data.ResourceDataProvider" />.
    /// </summary>
    public RadSearchBox()
    {
    }

    /// <summary>
    /// Initializes new instance of <see cref="T:Telerik.Sitefinity.Localization.RadSearchBox" /> class with the provided <see cref="T:Telerik.Sitefinity.Localization.Data.ResourceDataProvider" />.
    /// </summary>
    /// <param name="dataProvider"><see cref="T:Telerik.Sitefinity.Localization.Data.ResourceDataProvider" /></param>
    public RadSearchBox(ResourceDataProvider dataProvider)
      : base(dataProvider)
    {
    }

    [ResourceEntry("DefaultItemText", Description = "RadSearchBox resource strings.", LastModified = "2014/06/19", Value = "All")]
    public string DefaultItemText => this[nameof (DefaultItemText)];

    /// <summary>RadSearchBox</summary>
    [ResourceEntry("RadSearchBoxTitle", Description = "The title of this class.", LastModified = "2013/06/19", Value = "RadSearchBox")]
    public string RadSearchBoxTitle => this[nameof (RadSearchBoxTitle)];

    /// <summary>RadSearchBox</summary>
    [ResourceEntry("RadSearchBoxTitlePlural", Description = "The title plural of this class.", LastModified = "2013/06/19", Value = "RadSearchBox")]
    public string RadSearchBoxTitlePlural => this[nameof (RadSearchBoxTitlePlural)];

    /// <summary>Resource strings for RadSearchBox.</summary>
    [ResourceEntry("RadSearchBoxDescription", Description = "The description of this class.", LastModified = "2013/06/19", Value = "Resource strings for RadSearchBox.")]
    public string RadSearchBoxDescription => this[nameof (RadSearchBoxDescription)];

    [ResourceEntry("ReservedResource", Description = "RadSearchBox resource strings.", LastModified = "2014/06/19", Value = "Please do not remove this key.")]
    public string ReservedResource => this[nameof (ReservedResource)];

    [ResourceEntry("ShowAllResults", Description = "RadSearchBox resource strings.", LastModified = "2014/06/19", Value = "Show All Results")]
    public string ShowAllResults => this[nameof (ShowAllResults)];
  }
}
