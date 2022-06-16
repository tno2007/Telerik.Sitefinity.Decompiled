// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Localization.RadCloudUpload
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using Telerik.Sitefinity.Localization.Data;

namespace Telerik.Sitefinity.Localization
{
  /// <summary>Represents string resources for RadControls.</summary>
  [ObjectInfo(Description = "ResourcesDescription", Name = "RadCloudUpload", ResourceClassId = "RadCloudUpload", Title = "ResourcesTitle", TitlePlural = "TitlePlural")]
  public sealed class RadCloudUpload : Resource
  {
    /// <summary>
    /// Initializes new instance of <see cref="T:Telerik.Sitefinity.Localization.RadCloudUpload" /> class with the default <see cref="T:Telerik.Sitefinity.Localization.Data.ResourceDataProvider" />.
    /// </summary>
    public RadCloudUpload()
    {
    }

    /// <summary>
    /// Initializes new instance of <see cref="T:Telerik.Sitefinity.Localization.RadCloudUpload" /> class with the provided <see cref="T:Telerik.Sitefinity.Localization.Data.ResourceDataProvider" />.
    /// </summary>
    /// <param name="dataProvider"><see cref="T:Telerik.Sitefinity.Localization.Data.ResourceDataProvider" /></param>
    public RadCloudUpload(ResourceDataProvider dataProvider)
      : base(dataProvider)
    {
    }

    [ResourceEntry("Cancel", Description = "RadCloudUpload resource strings.", LastModified = "2014/06/19", Value = "Cancel")]
    public string Cancel => this[nameof (Cancel)];

    [ResourceEntry("CollapseButton", Description = "RadCloudUpload resource strings.", LastModified = "2014/06/19", Value = "Collapse Button")]
    public string CollapseButton => this[nameof (CollapseButton)];

    [ResourceEntry("Error", Description = "RadCloudUpload resource strings.", LastModified = "2014/06/19", Value = "Error")]
    public string Error => this[nameof (Error)];

    [ResourceEntry("ExpandButton", Description = "RadCloudUpload resource strings.", LastModified = "2014/06/19", Value = "Expand Button")]
    public string ExpandButton => this[nameof (ExpandButton)];

    [ResourceEntry("ExtensionValidationFailedMessage", Description = "RadCloudUpload resource strings.", LastModified = "2014/06/19", Value = "Extension validation failed")]
    public string ExtensionValidationFailedMessage => this[nameof (ExtensionValidationFailedMessage)];

    [ResourceEntry("Remove", Description = "RadCloudUpload resource strings.", LastModified = "2014/06/19", Value = "Remove")]
    public string Remove => this[nameof (Remove)];

    [ResourceEntry("ReservedResource", Description = "RadCloudUpload resource strings.", LastModified = "2014/06/19", Value = "Please do not remove this key.")]
    public string ReservedResource => this[nameof (ReservedResource)];

    [ResourceEntry("SelectButtonText", Description = "RadCloudUpload resource strings.", LastModified = "2014/06/19", Value = "Select")]
    public string SelectButtonText => this[nameof (SelectButtonText)];

    [ResourceEntry("ServerErrorMessage", Description = "RadCloudUpload resource strings.", LastModified = "2014/06/19", Value = "Error occured during file upload")]
    public string ServerErrorMessage => this[nameof (ServerErrorMessage)];

    [ResourceEntry("SizeValidationFailedMessage", Description = "RadCloudUpload resource strings.", LastModified = "2014/06/19", Value = "Size validation failed")]
    public string SizeValidationFailedMessage => this[nameof (SizeValidationFailedMessage)];

    [ResourceEntry("UploadedFilesMessage", Description = "RadCloudUpload resource strings.", LastModified = "2014/06/19", Value = "Uploaded Files")]
    public string UploadedFilesMessage => this[nameof (UploadedFilesMessage)];

    [ResourceEntry("UploadingFilesMessage", Description = "RadCloudUpload resource strings.", LastModified = "2014/06/19", Value = "Uploading Files")]
    public string UploadingFilesMessage => this[nameof (UploadingFilesMessage)];
  }
}
