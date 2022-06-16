// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Localization.RadProgressArea
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using Telerik.Sitefinity.Localization.Data;

namespace Telerik.Sitefinity.Localization
{
  /// <summary>Represents string resources for RadControls.</summary>
  [ObjectInfo(Description = "RadProgressAreaDescription", Name = "RadProgressArea", ResourceClassId = "RadProgressArea", Title = "RadProgressAreaTitle")]
  public sealed class RadProgressArea : Resource
  {
    /// <summary>
    /// Initializes new instance of <see cref="T:Telerik.Sitefinity.Localization.RadProgressArea" /> class with the default <see cref="T:Telerik.Sitefinity.Localization.Data.ResourceDataProvider" />.
    /// </summary>
    public RadProgressArea()
    {
    }

    /// <summary>
    /// Initializes new instance of <see cref="T:Telerik.Sitefinity.Localization.RadProgressArea" /> class with the provided <see cref="T:Telerik.Sitefinity.Localization.Data.ResourceDataProvider" />.
    /// </summary>
    /// <param name="dataProvider"><see cref="T:Telerik.Sitefinity.Localization.Data.ResourceDataProvider" /></param>
    public RadProgressArea(ResourceDataProvider dataProvider)
      : base(dataProvider)
    {
    }

    /// <summary>RadProgressArea</summary>
    [ResourceEntry("RadProgressAreaTitle", Description = "The title of this class.", LastModified = "2009/09/16", Value = "RadProgressArea")]
    public string RadProgressAreaTitle => this[nameof (RadProgressAreaTitle)];

    /// <summary>RadProgressAreaTitlePlural</summary>
    [ResourceEntry("RadProgressAreaTitlePlural", Description = "The title plural of this class.", LastModified = "2009/09/16", Value = "RadProgressAreaTitlePlural")]
    public string RadProgressAreaTitlePlural => this[nameof (RadProgressAreaTitlePlural)];

    /// <summary>Resource strings for RadProgressArea.</summary>
    [ResourceEntry("RadProgressAreaDescription", Description = "The description of this class.", LastModified = "2009/09/16", Value = "Resource strings for RadProgressArea.")]
    public string RadProgressAreaDescription => this[nameof (RadProgressAreaDescription)];

    [ResourceEntry("Cancel", Description = "RadProgressArea resource strings.", LastModified = "2014/06/19", Value = "Cancel")]
    public string Cancel => this[nameof (Cancel)];

    [ResourceEntry("CurrentFileName", Description = "RadProgressArea resource strings.", LastModified = "2014/06/19", Value = "Uploading file: ")]
    public string CurrentFileName => this[nameof (CurrentFileName)];

    [ResourceEntry("ElapsedTime", Description = "RadProgressArea resource strings.", LastModified = "2014/06/19", Value = "Elapsed time: ")]
    public string ElapsedTime => this[nameof (ElapsedTime)];

    [ResourceEntry("EstimatedTime", Description = "RadProgressArea resource strings.", LastModified = "2014/06/19", Value = "Estimated time: ")]
    public string EstimatedTime => this[nameof (EstimatedTime)];

    [ResourceEntry("ReservedResource", Description = "RadProgressArea resource strings.", LastModified = "2014/06/19", Value = "Please do not remove this key.")]
    public string ReservedResource => this[nameof (ReservedResource)];

    [ResourceEntry("Total", Description = "RadProgressArea resource strings.", LastModified = "2014/06/19", Value = "Total ")]
    public string Total => this[nameof (Total)];

    [ResourceEntry("TotalFiles", Description = "RadProgressArea resource strings.", LastModified = "2014/06/19", Value = "Total files: ")]
    public string TotalFiles => this[nameof (TotalFiles)];

    [ResourceEntry("TransferSpeed", Description = "RadProgressArea resource strings.", LastModified = "2014/06/19", Value = "Speed: ")]
    public string TransferSpeed => this[nameof (TransferSpeed)];

    [ResourceEntry("Uploaded", Description = "RadProgressArea resource strings.", LastModified = "2014/06/19", Value = "Uploaded")]
    public string Uploaded => this[nameof (Uploaded)];

    [ResourceEntry("UploadedFiles", Description = "RadProgressArea resource strings.", LastModified = "2014/06/19", Value = "Uploaded files: ")]
    public string UploadedFiles => this[nameof (UploadedFiles)];
  }
}
