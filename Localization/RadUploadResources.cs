// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Localization.RadUploadResources
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using Telerik.Sitefinity.Localization.Data;

namespace Telerik.Sitefinity.Localization
{
  /// <summary>Represents string resources for RadControls.</summary>
  [ObjectInfo(Description = "RadUploadResourcesDescription", Name = "RadUpload", ResourceClassId = "RadUpload", Title = "RadUploadResourcesTitle", TitlePlural = "RadUploadResourcesTitlePlural")]
  public sealed class RadUploadResources : Resource
  {
    /// <summary>
    /// Initializes new instance of <see cref="T:Telerik.Sitefinity.Localization.RadUploadResources" /> class with the default <see cref="T:Telerik.Sitefinity.Localization.Data.ResourceDataProvider" />.
    /// </summary>
    public RadUploadResources()
    {
    }

    /// <summary>
    /// Initializes new instance of <see cref="T:Telerik.Sitefinity.Localization.RadUploadResources" /> class with the provided <see cref="T:Telerik.Sitefinity.Localization.Data.ResourceDataProvider" />.
    /// </summary>
    /// <param name="dataProvider"><see cref="T:Telerik.Sitefinity.Localization.Data.ResourceDataProvider" /></param>
    public RadUploadResources(ResourceDataProvider dataProvider)
      : base(dataProvider)
    {
    }

    /// <summary>RadSpell Dialog</summary>
    [ResourceEntry("RadUploadResourcesTitle", Description = "The title of this class.", LastModified = "2009/09/16", Value = "RadUpload")]
    public string RadUploadResourcesTitle => this[nameof (RadUploadResourcesTitle)];

    /// <summary>RadSpell Dialog</summary>
    [ResourceEntry("RadUploadResourcesTitlePlural", Description = "The title plural of this class.", LastModified = "2009/09/16", Value = "RadUpload title plural")]
    public string RadUploadResourcesTitlePlural => this[nameof (RadUploadResourcesTitlePlural)];

    /// <summary>Resource strings for RadUpload.</summary>
    [ResourceEntry("RadUploadResourcesDescription", Description = "The description of this class.", LastModified = "2009/09/16", Value = "Resource strings for RadUpload.")]
    public string RadUploadResourcesDescription => this[nameof (RadUploadResourcesDescription)];

    [ResourceEntry("Add", Description = "RadUpload resource strings.", LastModified = "2014/06/19", Value = "Add")]
    public string Add => this[nameof (Add)];

    [ResourceEntry("Clear", Description = "RadUpload resource strings.", LastModified = "2014/06/19", Value = "Clear")]
    public string Clear => this[nameof (Clear)];

    [ResourceEntry("Delete", Description = "RadUpload resource strings.", LastModified = "2014/06/19", Value = "Delete")]
    public string Delete => this[nameof (Delete)];

    [ResourceEntry("Remove", Description = "RadUpload resource strings.", LastModified = "2014/06/19", Value = "Remove")]
    public string Remove => this[nameof (Remove)];

    [ResourceEntry("ReservedResource", Description = "RadUpload resource strings.", LastModified = "2014/06/19", Value = "Please do not remove this key.")]
    public string ReservedResource => this[nameof (ReservedResource)];

    [ResourceEntry("Select", Description = "RadUpload resource strings.", LastModified = "2014/06/19", Value = "Select")]
    public string Select => this[nameof (Select)];
  }
}
