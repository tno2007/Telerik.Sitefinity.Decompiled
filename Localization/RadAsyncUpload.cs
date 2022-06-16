// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Localization.RadAsyncUpload
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using Telerik.Sitefinity.Localization.Data;

namespace Telerik.Sitefinity.Localization
{
  /// <summary>Represents string resources for RadControls.</summary>
  [ObjectInfo(Description = "ResourcesDescription", Name = "RadAsyncUpload", ResourceClassId = "RadAsyncUpload", Title = "ResourcesTitle", TitlePlural = "TitlePlural")]
  public sealed class RadAsyncUpload : Resource
  {
    /// <summary>
    /// Initializes new instance of <see cref="T:Telerik.Sitefinity.Localization.RadAsyncUpload" /> class with the default <see cref="T:Telerik.Sitefinity.Localization.Data.ResourceDataProvider" />.
    /// </summary>
    public RadAsyncUpload()
    {
    }

    /// <summary>
    /// Initializes new instance of <see cref="T:Telerik.Sitefinity.Localization.RadAsyncUpload" /> class with the provided <see cref="T:Telerik.Sitefinity.Localization.Data.ResourceDataProvider" />.
    /// </summary>
    /// <param name="dataProvider"><see cref="T:Telerik.Sitefinity.Localization.Data.ResourceDataProvider" /></param>
    public RadAsyncUpload(ResourceDataProvider dataProvider)
      : base(dataProvider)
    {
    }

    [ResourceEntry("ResourcesTitle", Description = "The title of this class.", LastModified = "2015/10/16", Value = "RadAsyncUpload")]
    public string ResourcesTitle => this[nameof (ResourcesTitle)];

    [ResourceEntry("Cancel", Description = "RadAsyncUpload resource strings.", LastModified = "2014/06/19", Value = "Cancel")]
    public string Cancel => this[nameof (Cancel)];

    [ResourceEntry("Remove", Description = "RadAsyncUpload resource strings.", LastModified = "2014/06/19", Value = "Remove")]
    public string Remove => this[nameof (Remove)];

    [ResourceEntry("ReservedResource", Description = "RadAsyncUpload resource strings.", LastModified = "2014/06/19", Value = "Please do not remove this key.")]
    public string ReservedResource => this[nameof (ReservedResource)];

    [ResourceEntry("Select", Description = "RadAsyncUpload resource strings.", LastModified = "2014/06/19", Value = "Select")]
    public string Select => this[nameof (Select)];

    [ResourceEntry("DropZone", Description = "RadAsyncUpload DropZone strings.", LastModified = "2015/07/08", Value = "Drop files here")]
    public string DropZone => this[nameof (DropZone)];
  }
}
